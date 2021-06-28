//using PuroTouch.App_Code;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using DAL;

public partial class Home : System.Web.UI.Page
{
    PuroTouchRepository repository = new PuroTouchRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CHECK Cookie, otherwise redirect to Login Page
            HttpCookie userCookie = Request.Cookies["userInfo"];
            if (userCookie != null)
            {
                Session["userName"] = userCookie["userName"];
                Session["appName"] = userCookie["appName"];
                Session["userRole"] = userCookie["userRole"];
            }
            if (Session["userName"] != null && Session["appName"] != null)
            {
                loadHomeGrid1();
                lblGridtitle.Text = "Requests ";
                try
                {
                    getRevChartData();
                    getPieChartData();
                    getITBAs();
                }
                catch (Exception ex)
                {

                }
              
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
             //Role Based Viewing
            string userRole = Session["userRole"].ToString().ToLower();
            if (userRole == "sales" || userRole == "salesdm" || userRole == "salesmanager")
            {
                btnNewRequest.Visible = true;
                
            }
            if (userRole == "itba")
            {
                btnNewRequest.Visible = false;
                
            }

            if (userRole == "itmanager" || userRole == "itadmin")
            {
                btnNewRequest.Visible = true;               
                
                lblGridtitle.Text = "New Requests";

            }
            
        }
    }

    protected void getITBAs()
    {
        List<ClsITBA> balist = repository.GetITBAs();
        rddlITBA.DataSource = balist;
        rddlITBA.DataTextField = "ITBA";
        rddlITBA.DataValueField = "ITBA";
        rddlITBA.DataBind();
    }
    
    private void loadHomeGrid1(bool doBind=false)
    {
        try
        {
        }
        catch (Exception ex)
        {
        }
    }

    private void getPieChartData()
    {
        ClsPieChart pc = new ClsPieChart();
        string userRole = Session["userRole"].ToString().ToLower();
        string userLogin = Session["userName"].ToString();
        List<ClsPieChart> pieChartdata = new List<ClsPieChart>();

        if (userRole == "sales")
        {
            pieChartdata = pc.getOnboardingPhaseCountSales(userLogin);
            PieChart1.DataSource = pieChartdata;
        }

        if (userRole == "salesdm")
        {
            clsDistrictRestriction RestrictedDistricts = new clsDistrictRestriction();
            string district = RestrictedDistricts.GetDistrictRestriction(Session["userName"].ToString(), Session["appName"].ToString());
            pieChartdata = pc.getOnboardingPhaseCountDistrict(district);
            PieChart1.DataSource = pieChartdata;
        }
        if (userRole == "salesmanager")
        {
            pieChartdata = pc.getOnboardingPhaseCountAll();
            PieChart1.DataSource = pieChartdata;
        }
        if (userRole == "itba" || userRole == "itadmin" || userRole == "admin")
        {
            pieChartdata = pc.getOnboardingPhaseCountITBA(userLogin);
            PieChart1.DataSource = pieChartdata;
        }
        if (userRole == "itmanager")
        {
            pieChartdata = pc.getOnboardingPhaseCountAll();
            PieChart1.DataSource = pieChartdata;
        }
        PieChart1.DataBind();
    }

    private void getRevChartData()
    {
        ClsProjRevChart pr = new ClsProjRevChart();
        string userRole = Session["userRole"].ToString().ToLower();
        string userLogin = Session["userName"].ToString();
        List<ClsProjRevChart> revChartData = new List<ClsProjRevChart>();
        if (userRole == "sales")
        {
            revChartData = pr.getProjectedRevenueSales(userLogin);
            ColumnChart.DataSource = revChartData; 
        }
        if (userRole == "salesdm")
        {
            clsDistrictRestriction RestrictedDistricts = new clsDistrictRestriction();
            string district = RestrictedDistricts.GetDistrictRestriction(Session["userName"].ToString(), Session["appName"].ToString());
            revChartData = pr.getProjectedRevenueDistrict(district);
            ColumnChart.DataSource = revChartData;
        }
        if (userRole == "salesmanager")
        {
            revChartData = pr.getProjectedRevenueAll();
            ColumnChart.DataSource = revChartData;
        }
        if (userRole == "itba" || userRole == "itadmin" || userRole == "admin")
        {
            revChartData = pr.getProjectedRevenueITBA(userLogin);
            ColumnChart.DataSource = revChartData; 
        }
        if (userRole == "itmanager")
        {
            revChartData = pr.getProjectedRevenueAll();
            ColumnChart.DataSource = revChartData; 
        }
        ColumnChart.DataBind();
    }
    protected void rgHomeGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
        }
    }
    protected void rgHomeGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
    }

    protected void btnNewRequest_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("DiscoveryRequestForm");
    }
    protected void btnAssign_Click(object sender, System.EventArgs e)
    {
        windowManager.RadAlert("Assign this Request to " + rddlITBA.SelectedText + "?", 350, 200, "Assign", "assignCallBackFn", "Please confirm");
    }

    protected void rgRequests_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        
        PuroTouchRepository rep = new PuroTouchRepository();
        string userName = Session["userName"].ToString();
        string userRole = Session["userRole"].ToString().ToLower();
        List<ClsDiscoveryRequest> oDRList;
        //if (userRole == "sales" || userRole == "salesdm" || userRole == "itadmin")
        
        switch (userRole)
        {
            case "salesdm":
                clsDistrictRestriction RestrictedDistricts = new clsDistrictRestriction();
                string district = RestrictedDistricts.GetDistrictRestriction(Session["userName"].ToString(), Session["appName"].ToString());
                 oDRList = rep.GetAllDiscoveryRequestsForDistrict(district);
                break;
            case "sales":
                 oDRList = rep.GetAllDiscoveryRequestsForSP(userName);
                break;
            case "salesmanager":
                oDRList = rep.GetAllDiscoveryRequests();
                break;
            case "itmanager":
                oDRList = rep.GetUnassignedDiscoveryRequests();
                break;
            case "itadmin":
                oDRList = rep.GetAllDiscoveryRequests();
                break;
            default:
                oDRList = rep.GetAllDiscoveryRequests(userName);
                break;
        }
        
        rgRequests.DataSource = oDRList;
    }

    protected void rgRequests_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            //adding all the links to the hyperlink column, to navigate when it is clicked. 
            GridDataItem item = (GridDataItem)e.Item;

            string userName = Session["userName"].ToString().ToLower();
            string userRole = Session["userRole"].ToString().ToLower();
            string val = item["UpdatedBy"].Text;

            if (val.ToLower() != userName && userRole == "itba")
            {
                item.ForeColor = System.Drawing.Color.Green;
            }

            HyperLink hLink = (HyperLink)item["CustomerName"].Controls[0];
            hLink.ForeColor = System.Drawing.Color.Blue;
            ClsDiscoveryRequest row = (ClsDiscoveryRequest)item.DataItem;
            hLink.Attributes["onclick"] = "OpenWin('" + row.idRequest + "');";
        }
    }

    protected void rgRequests_ItemCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == "EditRequest")
        {
            Response.Redirect("DiscoveryRequestForm.aspx?requestID=" + e.CommandArgument);
        }
    }
}
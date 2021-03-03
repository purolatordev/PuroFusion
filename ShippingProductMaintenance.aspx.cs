using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Telerik.Web.UI;

public partial class ShippingProductMaintenance : System.Web.UI.Page
{

    PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
    ClsShippingProducts cls = new ClsShippingProducts();
    PuroTouchRepository rep = new PuroTouchRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userName"] != null && Session["appName"] != null)
            {
                getDataList();
                if (Session["userRole"].ToString().ToLower() != "itmanager" && Session["userRole"].ToString().ToLower() != "itadmin" && Session["userRole"].ToString().ToLower() != "admin")
                {
                    rgGrid.MasterTableView.GetColumn("Edit").Display = false;
                    rgGrid.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

    }

    private void getDataList()
    {
        List<ClsShippingProducts> dataList = rep.GetShippingProducts();
        rgGrid.DataSource = dataList;
    }

    protected void rgGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        getDataList();
    }
    protected void rgGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Information";
            rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        else
        {
            rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Add Information";
            rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            rgGrid.ExportSettings.FileName = "ShippingProducts";
            rgGrid.AllowFilteringByColumn = false;
            rgGrid.MasterTableView.GetColumn("Edit").Visible = false;
            rgGrid.ExportSettings.IgnorePaging = true;
            rgGrid.ExportSettings.ExportOnlyData = true;
            rgGrid.ExportSettings.OpenInNewWindow = true;
            rgGrid.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
        }
    }


    protected void rgGrid_InsertCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            ClsShippingProducts oRow = populateObj(userControl);
            string insertMsg = "";
            if (IsValid)
            {

                if (oRow != null)
                {

                    insertMsg = cls.InsertShippingProduct(oRow);
                    if (insertMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully Added New Record " + oRow.ShippingProduct;

                    }
                    else
                    {

                        errorMsg.Visible = true;
                        errorMsg.Text = insertMsg;
                        e.Canceled = true;
                    }
                }
            }
            else
            {
                // display error
                errorMsg.Visible = true;
                errorMsg.Text = "Please enter Required fields";
                e.Canceled = true;
            }

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
            e.Canceled = true;
        }
    }



    protected void rgGrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            ClsShippingProducts oRow = populateObj(userControl);
            oRow.idShippingProduct = Convert.ToInt16((userControl.FindControl("idShippingProduct") as Label).Text);
            string updateMsg = "";
            if (IsValid)
            {


                if (oRow != null)
                {
                    updateMsg = cls.UpdateShippingProduct(oRow);
                    if (updateMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully updated information for " + "'" + oRow.ShippingProduct + "'";
                    }
                    else
                    {
                        errorMsg.Visible = true;
                        errorMsg.Text = updateMsg;
                        e.Canceled = true;
                    }
                }
            }
            else
            {
                // display error
                errorMsg.Visible = true;
                errorMsg.Text = "Please enter Required fields";
                e.Canceled = true;
            }

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
            e.Canceled = true;
        }
    }

    protected void rgGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                PuroTouchRepository repository = new PuroTouchRepository();
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                RadDropDownList rddlService = userControl.FindControl("rddlService") as RadDropDownList;
                List<ClsShippingService> svclist = repository.GetServices();
                rddlService.DataSource = svclist;
                rddlService.DataTextField = "serviceDesc";
                rddlService.DataValueField = "idShippingSvc";
                rddlService.DataBind();

                //Choose the right Shipping Service
                //GridEditFormItem editform = (GridEditFormItem)e.Item;
                
                
                string hiddenSvcID = (userControl.FindControl("hdServieID") as HiddenField).Value;
                rddlService.SelectedValue = hiddenSvcID;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }

    }

    private ClsShippingProducts populateObj(UserControl userControl)
    {

        ClsShippingProducts oRow = new ClsShippingProducts();


        oRow.ShippingProduct = (userControl.FindControl("txtShippingProduct") as RadTextBox).Text;
        oRow.idShippingSvc = Convert.ToInt16((userControl.FindControl("rddlService") as RadDropDownList).SelectedValue);
        
        oRow.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;


        oRow.UpdatedBy = (string)(Session["userName"]);
        oRow.UpdatedOn = Convert.ToDateTime(DateTime.Now);
        return oRow;
    }


}
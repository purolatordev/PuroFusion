using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DAL;
using Telerik.Web.UI;
using PI_Application;
using PI_People;
using System.Data.SqlClient;
using System.Data;

public partial class ITBAMaintenance : System.Web.UI.Page
{
    String strConnString = ConfigurationManager.ConnectionStrings["PurolatorReportingConnectionString"].ConnectionString;
    PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
    ClsITBA cls = new ClsITBA();
    PuroTouchRepository rep = new PuroTouchRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["userName"] != null && Session["appName"] != null && ((string)Session["userRole"] == "Admin" || (string)Session["userRole"] == "ITAdmin" || (string)Session["userRole"] == "ITManager"))
            {
                //loadEmployees();
                
            }
            else
            {
                Response.Redirect("NoAccess.aspx");

            }
        }        
    }
    

    protected void rgITBA_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        loadUsers();
    }


    protected void loadUsers()
    {

        ClsITBA ba = new ClsITBA();
        List<ClsITBA> itbaList = rep.GetITBAs();
        rgITBA.DataSource = itbaList;

    }

    protected void rgITBA_InsertCommand(object sender, GridCommandEventArgs e)
    {

        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            ClsITBA oRow = populateObj(userControl);
            string insertMsg = "";
            if (IsValid)
            {

                if (oRow != null)
                {

                    insertMsg = cls.InsertITBA(oRow);
                    if (insertMsg == "")
                    {
                        //pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully Added New Record " + oRow.ITBAName;

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

    

    //protected void rgITBA_DeleteCommand(object source, GridCommandEventArgs e)
    //{
    //}

    protected void rgITBA_UpdateCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            ClsITBA oRow = populateObj(userControl);
            oRow.idITBA = Convert.ToInt16((userControl.FindControl("hdnITBAID") as HiddenField).Value);
            string updateMsg = "";
            if (IsValid)
            {


                if (oRow != null)
                {
                    updateMsg = cls.UpdateITBA(oRow);
                    if (updateMsg == "")
                    {
                        //pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully updated information for " + "'" + oRow.ITBAName + "'";
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

    protected void rgITBA_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            var msg = "";

            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                  UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                  RadDropDownList rddlEmployee = userControl.FindControl("rddlEmployee") as RadDropDownList;
                  string hiddenEmpID = (userControl.FindControl("hdEmployeeID") as HiddenField).Value;
                  rddlEmployee.SelectedValue = hiddenEmpID;
            }

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }
    }

    private ClsITBA populateObj(UserControl userControl)
    {

        ClsITBA oRow = new ClsITBA();


        oRow.ITBAEmail = (userControl.FindControl("txtEmail") as RadTextBox).Text;
        oRow.idEmployee = Convert.ToInt16((userControl.FindControl("rddlEmployee") as RadDropDownList).SelectedValue);
        oRow.ITBAName = (userControl.FindControl("rddlEmployee") as RadDropDownList).SelectedText;

        oRow.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;
        oRow.ReceiveNewReqEmail = (userControl.FindControl("NewReqEmail") as RadButton).Checked;
        oRow.login = (userControl.FindControl("txtLogin") as RadTextBox).Text;

        oRow.UpdatedBy = (string)(Session["userName"]);
        oRow.UpdatedOn = Convert.ToDateTime(DateTime.Now);
        return oRow;
    }
}
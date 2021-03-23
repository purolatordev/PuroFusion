using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DAL;
using Telerik.Web.UI;


public partial class EDISpecialistMaint : System.Web.UI.Page
{
    String strConnString = ConfigurationManager.ConnectionStrings["PurolatorReportingConnectionString"].ConnectionString;
    PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
    //clsEDISpecialist cls = new clsEDISpecialist();
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
        List<clsEDISpecialist> qShipMeth = SrvEDISpecialist.GetEDISpecialistView();
        rgITBA.DataSource = qShipMeth;
    }

    protected void rgITBA_InsertCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            clsEDISpecialist oRow = populateObj(userControl);
            string insertMsg = "";
            if (IsValid)
            {
                if (oRow != null)
                {
                    insertMsg = SrvEDISpecialist.InsertEDISpecialist(oRow);
                    if (insertMsg == "")
                    {
                        lblSuccess.Text = "Successfully Added New Record " + oRow.Name;
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

    protected void rgITBA_UpdateCommand(object source, GridCommandEventArgs e)
    {
        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            clsEDISpecialist oRow = populateObj(userControl);
            oRow.idEDISpecialist = Convert.ToInt16((userControl.FindControl("hdnITBAID") as HiddenField).Value);
            string updateMsg = "";
            if (IsValid)
            {
                if (oRow != null)
                {
                    updateMsg = SrvEDISpecialist.UpdateEDISpecialist(oRow);
                    if (updateMsg == "")
                    {
                        lblSuccess.Text = "Successfully updated information for " + "'" + oRow.Name + "'";
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

    private clsEDISpecialist populateObj(UserControl userControl)
    {
        clsEDISpecialist oRow = new clsEDISpecialist();

        oRow.email = (userControl.FindControl("txtEmail") as RadTextBox).Text;
        oRow.idEmployee = Convert.ToInt16((userControl.FindControl("rddlEmployee") as RadDropDownList).SelectedValue);
        oRow.Name = (userControl.FindControl("rddlEmployee") as RadDropDownList).SelectedText;

        oRow.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;
        oRow.ReceiveNewReqEmail = (userControl.FindControl("NewReqEmail") as RadButton).Checked;
        oRow.login = (userControl.FindControl("txtLogin") as RadTextBox).Text;

        oRow.CreatedBy = (string)(Session["userName"]);
        oRow.UpdatedOn = Convert.ToDateTime(DateTime.Now);

        oRow.UpdatedBy = (string)(Session["userName"]);
        oRow.UpdatedOn = Convert.ToDateTime(DateTime.Now);
        return oRow;
    }
}
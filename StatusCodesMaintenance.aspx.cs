using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Telerik.Web.UI;

public partial class StatusCodesMaint : System.Web.UI.Page
{
    PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
    ClsShippingVendor sv = new ClsShippingVendor();
    PuroTouchRepository rep = new PuroTouchRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Session["userName"] != null && Session["appName"] != null)
            {
                getShippingVendors();
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
    private void getShippingVendors()
    {
        List<clsStatusCode> listVend = SrvStatusCode.GetStatusCodes();
        rgGrid.DataSource = listVend;
    }

    protected void rgGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        getShippingVendors();
    }
    protected void rgGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Status Codes";
            rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        else
        {
            rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Add Status Codes";
            rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            rgGrid.ExportSettings.FileName = "StatusCodes";
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
            clsStatusCode oVend = populateObj(userControl);
            string insertMsg = "";
            if (IsValid)
            {
                if (oVend != null)
                {
                    insertMsg = SrvStatusCode.UpdatetatusCode(oVend);
                    if (insertMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully Added New Status Code " + oVend.StatusCode;
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
    protected void rgGrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            clsStatusCode oVend = populateObj(userControl);
            oVend.idStatusCodes = Convert.ToInt16((userControl.FindControl("lblShippingVendorID") as Label).Text);
            string updateMsg = "";
            if (IsValid)
            {
                if (oVend != null)
                {
                    updateMsg = SrvStatusCode.UpdatetatusCode(oVend);
                    if (updateMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully updated Status Code " + "'" + oVend.StatusCode + "'";
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

    protected void rgGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            //Need to select drop downs after data binding
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                //************First calling dropdown list values selected in pop up edit form**************/ 
                UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;
                RadTextBox TaskTypeTXT = userControl.FindControl("txtCloseReason") as RadTextBox;

                RadButton activeFlag = userControl.FindControl("ActiveFlag") as RadButton;

                Label lastup = userControl.FindControl("lblLastUpdated") as Label;
                Label lastupby = userControl.FindControl("lblLastUpdatedBy") as Label;
                Label lastupon = userControl.FindControl("lblLastUpdatedOn") as Label;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }
    }

    private clsStatusCode populateObj(UserControl userControl)
    {
        clsStatusCode oVend = new clsStatusCode();
        
        oVend.StatusCode = (userControl.FindControl("txtVendorName") as RadTextBox).Text;
        oVend.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;
        oVend.CreatedBy = (string)(Session["userName"]);
        oVend.CreatedOn = Convert.ToDateTime(DateTime.Now);
        oVend.UpdatedBy = (string)(Session["userName"]);
        oVend.UpdatedOn = Convert.ToDateTime(DateTime.Now);
        return oVend;
    }
}
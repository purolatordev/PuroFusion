using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Telerik.Web.UI;

public partial class EDIOnboardingPhaseMaint : System.Web.UI.Page
{
    PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
    //ClsOnboardingPhase cls = new ClsOnboardingPhase();
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
        List<ClsEDIOnboardingPhase> dataList = SrvEDIOnboardingPhase.GetEDIOnboardingPhase();
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
            rgGrid.ExportSettings.FileName = "EDIOnboardingPhase";
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
            ClsEDIOnboardingPhase oRow = populateObj(userControl);
            string insertMsg = "";
            if (IsValid)
            {
                if (oRow != null)
                {
                    insertMsg = SrvEDIOnboardingPhase.InsertEDIOnboardingPhase(oRow);
                    if (insertMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully Added New Record " + oRow.EDIOnboardingPhaseType;
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
            ClsEDIOnboardingPhase oRow = populateObj(userControl);
            oRow.idEDIOnboardingPhase = Convert.ToInt16((userControl.FindControl("lblOnboardingPhaseID") as Label).Text);
            oRow.SortValue = Convert.ToInt16((userControl.FindControl("txtSortValue") as RadTextBox).Text);
            string updateMsg = "";
            if (IsValid)
            {
                if (oRow != null)
                {
                    updateMsg = SrvEDIOnboardingPhase.InsertEDIOnboardingPhase(oRow);
                    if (updateMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully updated information for " + "'" + oRow.EDIOnboardingPhaseType + "'";
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

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }
    }

    private ClsEDIOnboardingPhase populateObj(UserControl userControl)
    {
        ClsEDIOnboardingPhase oRow = new ClsEDIOnboardingPhase();
        oRow.EDIOnboardingPhaseType = (userControl.FindControl("txtOnboardingPhase") as RadTextBox).Text;
        oRow.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;
        oRow.UpdatedBy = (string)(Session["userName"]);
        oRow.UpdatedOn = Convert.ToDateTime(DateTime.Now);
        oRow.SortValue = Convert.ToInt16((userControl.FindControl("txtSortValue") as RadTextBox).Text);
        return oRow;
    }
}
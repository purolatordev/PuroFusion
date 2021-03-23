using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Telerik.Web.UI;



public partial class TaskTypeMaintenance : System.Web.UI.Page
{
    PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
    ClsTaskType tt = new ClsTaskType();
    PuroTouchRepository rep = new PuroTouchRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //getServiceType();
            if (Session["userName"] != null && Session["appName"] != null)
            {
                getTaskType();
                if (Session["userRole"].ToString().ToLower() != "itmanager" && Session["userRole"].ToString().ToLower() != "itadmin" && Session["userRole"].ToString().ToLower() != "admin")
                {
                    rgTaskType.MasterTableView.GetColumn("Edit").Display = false;
                    rgTaskType.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
    private void getTaskType()
    {
        List<ClsTaskType> listTasktype = rep.GetTaskTypes();
        rgTaskType.DataSource = listTasktype;
    }

    protected void rgTaskType_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        getTaskType();
    }
    protected void rgTaskType_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            rgTaskType.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Task Type Information";
            rgTaskType.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        else
        {
            rgTaskType.MasterTableView.EditFormSettings.CaptionFormatString = "Add Task Type Information";
            rgTaskType.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            rgTaskType.ExportSettings.FileName = "TaskType";
            rgTaskType.AllowFilteringByColumn = false;
            rgTaskType.MasterTableView.GetColumn("Edit").Visible = false;
            rgTaskType.ExportSettings.IgnorePaging = true;
            rgTaskType.ExportSettings.ExportOnlyData = true;
            rgTaskType.ExportSettings.OpenInNewWindow = true;
            rgTaskType.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
        }
    }


    protected void rgTaskType_InsertCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            ClsTaskType oTaskType = populateObj(userControl);           
            string insertMsg = "";
            if (IsValid)
            {

                if (oTaskType != null)
                {
                    
                    insertMsg = tt.InsertTaskType(oTaskType);
                    if (insertMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully Added New Task Type " + oTaskType.TaskType;
                       
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



    protected void rgTaskType_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            ClsTaskType oTaskType = populateObj(userControl);
            oTaskType.idTaskType = Convert.ToInt16((userControl.FindControl("lblTaskTypeID") as Label).Text);
            RadComboBox cbxOnboardingPhase = (userControl.FindControl("cbxOnboardingPhase") as RadComboBox);
            oTaskType.idOnboardingPhase = Convert.ToInt16(cbxOnboardingPhase.SelectedValue);
            string updateMsg = "";
            if (IsValid)
            {


                if (oTaskType != null)
                {
                    updateMsg = tt.UpdateTaskType(oTaskType);
                    if (updateMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully updated Task Type " + "'" + oTaskType.TaskType + "'";
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

    protected void rgTaskType_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            var msg = "";
            //Need to select drop downs after data binding
            if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
            {
                UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;            

                RadComboBox cbxOnboardingPhase = userControl.FindControl("cbxOnboardingPhase") as RadComboBox;

                List<ClsOnboardingPhase> listOnboardingPhases = rep.GetOnboardingPhasesAll();
                cbxOnboardingPhase.DataTextField = "OnboardingPhase";
                cbxOnboardingPhase.DataValueField = "idOnboardingPhase";
                cbxOnboardingPhase.DataSource = listOnboardingPhases;
                cbxOnboardingPhase.DataBind();
            }
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                //************First calling dropdown list values selected in pop up edit form**************/ 
                UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;
                RadTextBox TaskTypeTXT = userControl.FindControl("txtTaskType") as RadTextBox;               

               
                Label lastup = userControl.FindControl("lblLastUpdated") as Label;
                Label lastupby = userControl.FindControl("lblLastUpdatedBy") as Label;
                Label lastupon = userControl.FindControl("lblLastUpdatedOn") as Label;
                //if (lastupby.Text == "")
                //{
                //    lastup.Visible = false;
                //    lastupon.Visible = false;
                //}

                RadComboBox cbxOnboardingPhase = userControl.FindControl("cbxOnboardingPhase") as RadComboBox;

                List<ClsOnboardingPhase> listOnboardingPhases = rep.GetOnboardingPhasesAll();
                cbxOnboardingPhase.DataTextField = "OnboardingPhase";
                cbxOnboardingPhase.DataValueField = "idOnboardingPhase";
                cbxOnboardingPhase.DataSource = listOnboardingPhases;
                cbxOnboardingPhase.DataBind();

                HiddenField hdnOnboardingPhase = userControl.FindControl("hdnOnboardingPhase") as HiddenField;
                string idOnboardingPhase = hdnOnboardingPhase.Value;
                cbxOnboardingPhase.SelectedValue = idOnboardingPhase;

            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }

    }

    private ClsTaskType populateObj(UserControl userControl)
    {

        ClsTaskType oTaskType = new ClsTaskType();
        //oTaskType.idTaskType = Convert.ToInt16((userControl.FindControl("lblTaskTypeID") as Label).Text);
        
        oTaskType.TaskType = (userControl.FindControl("txtTaskType") as RadTextBox).Text;        
        oTaskType.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;       
       

        oTaskType.UpdatedBy = (string)(Session["userName"]);
        oTaskType.UpdatedOn = Convert.ToDateTime(DateTime.Now);
        RadComboBox cbxOnboardingPhase = (userControl.FindControl("cbxOnboardingPhase") as RadComboBox);
        oTaskType.idOnboardingPhase = Convert.ToInt16(cbxOnboardingPhase.SelectedValue);
        return oTaskType;
    }
   
}
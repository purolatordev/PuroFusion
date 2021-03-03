using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Telerik.Web.UI;

public partial class EquipmentMaintenance : System.Web.UI.Page
{
    PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
    ClsEquipment eq = new ClsEquipment();
    PuroTouchRepository rep = new PuroTouchRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userName"] != null && Session["appName"] != null)
            {
                getEquipment();
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
    private void getEquipment()
    {
        List<ClsEquipment> listEquipment = rep.GetEquipmentList();
        rgGrid.DataSource = listEquipment;
    }

    protected void rgGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        getEquipment();
    }
    protected void rgGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Equipment Information";
            rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        else
        {
            rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Add Equipment Information";
            rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            rgGrid.ExportSettings.FileName = "Equipment";
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
            ClsEquipment oEquipment = populateObj(userControl);
            string insertMsg = "";
            if (IsValid)
            {

                if (oEquipment != null)
                {

                    insertMsg = eq.InsertEquipment(oEquipment);
                    if (insertMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully Added New CloseReason " + oEquipment.Equipment;

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
            ClsEquipment oEquipment = populateObj(userControl);
            oEquipment.idEquipment = Convert.ToInt16((userControl.FindControl("lblEquipmentID") as Label).Text);
            string updateMsg = "";
            if (IsValid)
            {


                if (oEquipment != null)
                {
                    updateMsg = eq.UpdateEquipment(oEquipment);
                    if (updateMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully updated Task Type " + "'" + oEquipment.Equipment + "'";
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



            var msg = "";
            //Need to select drop downs after data binding
            if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
            {
                //************First calling dropdown list values selected in pop up edit form**************/ 
                UserControl userControl = e.Item.FindControl(GridEditFormItem.EditFormUserControlID) as UserControl;
                GridDataItem parentItem = (e.Item as GridEditFormItem).ParentItem;
                RadTextBox EquipmentTXT = userControl.FindControl("txtEquipment") as RadTextBox;

                RadButton activeFlag = userControl.FindControl("ActiveFlag") as RadButton;

                Label lastup = userControl.FindControl("lblLastUpdated") as Label;
                Label lastupby = userControl.FindControl("lblLastUpdatedBy") as Label;
                Label lastupon = userControl.FindControl("lblLastUpdatedOn") as Label;
                //if (lastupby.Text == "")
                //{
                //    lastup.Visible = false;
                //    lastupon.Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }

    }

    private ClsEquipment populateObj(UserControl userControl)
    {

        ClsEquipment oEquipment = new ClsEquipment();


        oEquipment.Equipment = (userControl.FindControl("txtEquipment") as RadTextBox).Text;

        oEquipment.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;


        oEquipment.UpdatedBy = (string)(Session["userName"]);
        oEquipment.UpdatedOn = Convert.ToDateTime(DateTime.Now);
        return oEquipment;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Telerik.Web.UI;

public partial class ContactTypeMaintenance : System.Web.UI.Page
{
    PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
    ClsContactType cr = new ClsContactType();
    PuroTouchRepository rep = new PuroTouchRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //getServiceType();
            if (Session["userName"] != null && Session["appName"] != null)
            {
                getContactType();
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

    private void getContactType()
    {
        List<ClsContactType> listContactType = rep.GetContactTypes();
        rgGrid.DataSource = listContactType;
    }

    protected void rgGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        getContactType();
    }
    protected void rgGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Contact Type";
            rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        else
        {
            rgGrid.MasterTableView.EditFormSettings.CaptionFormatString = "Add Contact Type";
            rgGrid.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            rgGrid.ExportSettings.FileName = "ContactType";
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
            ClsContactType oContactType = populateObj(userControl);
            string insertMsg = "";
            if (IsValid)
            {

                if (oContactType != null)
                {

                    insertMsg = cr.InsertContactType(oContactType);
                    if (insertMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully Added New Contact Type " + oContactType.ContactType;

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
            ClsContactType oContactType = populateObj(userControl);
            oContactType.idContactType = Convert.ToInt16((userControl.FindControl("lblContactTypeID") as Label).Text);
            string updateMsg = "";
            if (IsValid)
            {


                if (oContactType != null)
                {
                    updateMsg = cr.UpdateContactType(oContactType);
                    if (updateMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully updated " + "'" + oContactType.ContactType + "'";
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
                RadTextBox TaskTypeTXT = userControl.FindControl("txtContactType") as RadTextBox;

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

    private ClsContactType populateObj(UserControl userControl)
    {

        ClsContactType oContactType = new ClsContactType();


        oContactType.ContactType = (userControl.FindControl("txtContactType") as RadTextBox).Text;

        oContactType.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;


        oContactType.UpdatedBy = (string)(Session["userName"]);
        oContactType.UpdatedOn = Convert.ToDateTime(DateTime.Now);
        return oContactType;
    }
}
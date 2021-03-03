using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using Telerik.Web.UI;

public partial class CustomsTypeMaintenance : System.Web.UI.Page
{

    PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
    ClsCustomsType ct = new ClsCustomsType();
    PuroTouchRepository rep = new PuroTouchRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] != null && Session["appName"] != null)
        {
            getCustomsType();
            if (Session["userRole"].ToString().ToLower() != "itmanager" && Session["userRole"].ToString().ToLower() != "itadmin" && Session["userRole"].ToString().ToLower() != "admin")
            {
                rgCustoms.MasterTableView.GetColumn("Edit").Display = false;
                rgCustoms.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void getCustomsType()
    {
        List<ClsCustomsType> listCustoms = rep.GetListCustomsTypes();
        rgCustoms.DataSource = listCustoms;
    }

    protected void rgCustoms_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        getCustomsType();
    }
    protected void rgCustoms_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            rgCustoms.MasterTableView.EditFormSettings.CaptionFormatString = "Edit Customs Type Information";
            rgCustoms.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        else
        {
            rgCustoms.MasterTableView.EditFormSettings.CaptionFormatString = "Add Customs Type Information";
            rgCustoms.MasterTableView.EditFormSettings.FormCaptionStyle.Font.Bold = true;
        }
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            rgCustoms.ExportSettings.FileName = "CustomsType";
            rgCustoms.AllowFilteringByColumn = false;
            rgCustoms.MasterTableView.GetColumn("Edit").Visible = false;
            rgCustoms.ExportSettings.IgnorePaging = true;
            rgCustoms.ExportSettings.ExportOnlyData = true;
            rgCustoms.ExportSettings.OpenInNewWindow = true;
            rgCustoms.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
        }
    }


    protected void rgCustoms_InsertCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            ClsCustomsType oCType = populateObj(userControl);
            string insertMsg = "";
            if (IsValid)
            {

                if (oCType != null)
                {

                    insertMsg = ct.InsertCustomsType(oCType);
                    if (insertMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully Added New Customs Type " + oCType.CustomsType;

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
                 //display error
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



    protected void rgCustoms_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            Label errorMsg = (Label)userControl.FindControl("lblErrorMessage");
            ClsCustomsType oCType = populateObj(userControl);
            oCType.idCustomsType = Convert.ToInt16((userControl.FindControl("lblCustomsTypeID") as Label).Text);
            string updateMsg = "";
            if (IsValid)
            {


                if (oCType != null)
                {
                    updateMsg = ct.UpdateCustomsType(oCType);
                    if (updateMsg == "")
                    {
                        pnlsuccess.Visible = true;
                        lblSuccess.Text = "Successfully updated Customs Type " + "'" + oCType.CustomsType + "'";
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

    protected void rgCustomsType_ItemDataBound(object sender, GridItemEventArgs e)
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
                RadTextBox CustomsTypeTXT = userControl.FindControl("txtCustomsType") as RadTextBox;



                Label lastup = userControl.FindControl("lblLastUpdated") as Label;
                Label lastupby = userControl.FindControl("lblLastUpdatedBy") as Label;
                Label lastupon = userControl.FindControl("lblLastUpdatedOn") as Label;
                if (lastupby.Text == "")
                {
                    lastup.Visible = false;
                    lastupon.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }

    }

    private ClsCustomsType populateObj(UserControl userControl)
    {

        ClsCustomsType oCType = new ClsCustomsType();
        oCType.idCustomsType = Convert.ToInt16((userControl.FindControl("lblCustomsTypeID") as Label).Text);

        oCType.CustomsType = (userControl.FindControl("txtCustomsType") as RadTextBox).Text;

        oCType.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;


        oCType.UpdatedBy = (string)(Session["userName"]);
        oCType.UpdatedOn = Convert.ToDateTime(DateTime.Now);
        return oCType;
    }
}
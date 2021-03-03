using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditNote : System.Web.UI.UserControl
{
    PuroTouchRepository repository = new PuroTouchRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        
            //Role Based Viewing
            string userRole = Session["userRole"].ToString().ToLower();
            if (userRole == "admin" || userRole == "itadmin" || userRole == "itba" || userRole == "itmanager")
            {
                getTaskTypes();               
                lblInternalNotesEdit.Visible = true;
                txtInternalNotesEdit.Visible = true;
                lblInternalTaskEdit.Visible = true;
                lblInternalTaskAskEdit.Visible = true;
                rddlInternalTypeEdit.Visible = true;
                lblInternalTimeSpentEdit.Visible = true;
                lblInternalTimeSpentAskEdit.Visible = true;
                txtInternalTimeSpentEdit.Visible = true;

            //int selectedind = Convert.ToInt32(rddlInternalTypeEdit.SelectedValue);
            //if (selectedind == 1014)
            //{
            //    note1ast.Visible = false;
            //    note2ast.Visible = true;
            //    rfvNotesEdit.Enabled = false;
            //    rfvNotes2Edit.Enabled = true;
            //}
            //else
            //{
            //    note1ast.Visible = true;
            //    note2ast.Visible = false;
            //    rfvNotesEdit.Enabled = true;
            //    rfvNotes2Edit.Enabled = false;
            //}
            }
            if (userRole == "sales" || userRole == "salesdm" || userRole == "salesmanager")
            {
                rfvTaskTypeEdit.Enabled = false;
                rfvTimeEdit.Enabled = false;
            }
       
    }

    protected void rddlInternalType_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {

            int selectedind = Convert.ToInt32(rddlInternalTypeEdit.SelectedValue);
            if (selectedind == 1014)
            {
                note1ast.Visible = false;
                note2ast.Visible = true;
                rfvNotesEdit.Enabled = false;
                rfvNotes2Edit.Enabled = true;
            }
            else
            {
                note1ast.Visible = true;
                note2ast.Visible = false;
                rfvNotesEdit.Enabled = true;
                rfvNotes2Edit.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            //var msg = ex.Message;
            //pnlDanger.Visible = true;
            //lblDanger.Text = msg;
        }

    }

    protected void getTaskTypes()
    {
        try
        {
            List<ClsTaskType> typelist = repository.GetTaskTypes();
            rddlInternalTypeEdit.DataSource = typelist;
            rddlInternalTypeEdit.DataTextField = "TaskType";
            rddlInternalTypeEdit.DataValueField = "idTaskType";
            rddlInternalTypeEdit.DataBind();
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            //pnlDanger.Visible = true;
            //lblDanger.Text = msg;
        }
    }

    
}
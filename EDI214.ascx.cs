using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EDI214 : System.Web.UI.UserControl
{
    public event EventHandler RemoveUserControl2;
    public event EventHandler buttonClick;
    private string m_FirstName = string.Empty;
    public string FirstName
    {
        get { return m_FirstName; }
        set { m_FirstName = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtBoxTestValue2.Text = "the Value is: " + FirstName;
        //DetailsView customerDetailsView = new DetailsView();
        //customerDetailsView.ItemCommand += new DetailsViewCommandEventHandler(this.RoomForm_ItemCommand);
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        //this. += new DetailsViewCommandEventHandler(RoomForm_ItemCommand);
    }
    void RoomForm_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            int er = 0;
            er++;
            //this.DataSourceView.CancelEdit();
            //this.RedirectToPreviousPage();
        }
    }
    protected internal void btnRemove_Click(object sender, System.EventArgs e)
    {
        //Raise this event so the parent page can handle it   
        RemoveUserControl2(sender, e);
    }

    protected void ddlCountry_SelectedIndexChanged2(object sender, EventArgs e)
    {
        if (ddlCountry2.SelectedValue.ToString().ToLower().Contains("yes"))
        {
            lblTestValue2.Visible = true;
            txtBoxTestValue2.Visible = true;
        }
        else
        {
            lblTestValue2.Visible = false;
            txtBoxTestValue2.Visible = false;
        }
        int er = 0;
        er++;
    }

    protected void btnfirst_Click(object sender, EventArgs e)
    {
        txtBoxTestValue2.Text = "Button was pressed";

        int er = 0;
        er++;
    }
}
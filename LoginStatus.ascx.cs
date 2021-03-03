using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginStatus : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (WindowsIdentity.GetCurrent().IsAuthenticated)
        {
            try
            {
                this.pnlLogin.Visible = true;
                lblName.Text = Session["userName"].ToString();
                lblRole.Text = Session["userRole"].ToString();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Default.aspx");
            }
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        Response.Redirect("~/Default.aspx");
    }
}
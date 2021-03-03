using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
public partial class EditUser : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void cbxUserRole_Changed(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        try
        {
            string role = cbxUserRole.SelectedItem.Text;


            switch (role.ToLower())
            {

                case "salesdm":
                    //Districts
                    cbxDistrict.Visible = true;
                    rfDistrict.Enabled = true;
                    lblDistrict.Visible = true;
                    break;

                default:
                    cbxDistrict.Visible = false;
                    rfDistrict.Enabled = false;
                    lblDistrict.Visible = false;
                    break;
            }

        }
        catch (Exception ex)
        {
        }
    }
}
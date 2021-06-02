using PI_ApplicationSecurity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.Data.SqlClient;
using System.Data;


public partial class _Default : Page
{
    private string m_sUserName { get; set; }
    private string m_appName = ConfigurationManager.AppSettings["appName"].ToString();
    private int m_appid = Convert.ToInt32(ConfigurationManager.AppSettings["appID"]);


    protected void Page_Load(object sender, EventArgs e)
    {
        if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
        {
            txtUser.Text = ConfigurationManager.AppSettings["debugUser"];
            txtPasswrd.Text = "*";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            string username = txtUser.Text;
            m_sUserName = username;
            string password = txtPasswrd.Text;

            bool isOk;
            if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
                isOk = true;
            else
                isOk = validateLDAP(m_sUserName, password);

            if (isOk)
            {
                string userrole = getUserRole(m_sUserName, m_appid);
                if (!String.IsNullOrEmpty(userrole))
                {
                    UserInfo.Username = m_sUserName;
                    UserInfo.Roles = userrole;
                    Session["userName"] = m_sUserName;
                    Session["appName"] = m_appName;
                    if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
                        Session["userRole"] = ConfigurationManager.AppSettings["role"];
                    else
                        Session["userRole"] = UserInfo.Roles;
                    //User is Logged In
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    lblInvalid.Text = "No User Role Assigned. Please contact Administrator";
                }
            }
            else
            {
                lblInvalid.Text = "Invalid User Name or Password. ";
            }
        }
        else
        {
            lblInvalid.Text = "Missing Name or Password.";
        }

    }
   

   private Boolean validateLDAP(string username, string password)
    {
        string path = System.Configuration.ConfigurationManager.AppSettings["LDAP"];
        bool validuser = false;

        try
        {
            DirectoryEntry DE = new DirectoryEntry(@"LDAP://" + path, username, password);
            DirectorySearcher DS = new DirectorySearcher(DE);
            DS.Filter = "sAMAccountName=" + username;
            SearchResult SR = DS.FindOne();
            DirectoryEntry USER = SR.GetDirectoryEntry();

            if (USER != null)
            {
                string displayname = USER.Properties["displayName"].Value.ToString();
                string firstname = USER.Properties["givenName"].Value.ToString();
                string lastname = USER.Properties["sn"].Value.ToString();
                string email = USER.Properties["userPrincipalName"].Value.ToString();
                string objectname = USER.Properties["objectCategory"].Value.ToString();
                string accountname = USER.Properties["sAMAccountName"].Value.ToString();
                string name = USER.Properties["name"].Value.ToString();
                string distinguishedName = USER.Properties["distinguishedName"].Value.ToString();

                validuser = true;

            }

        }
        catch (System.Exception e)
        {

        }

        return validuser;
    }

    //MK Replace call to dll
    private string getUserRole(string username, int appid)
    {
        string userrole = "";
        SqlConnection cnn;        
        String strConnString = ConfigurationManager.ConnectionStrings["PurolatorReportingConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();       
        try
        {
            cmd = new SqlCommand("sp_GetUserRole", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@user_name",SqlDbType.VarChar).Value=username;
            cmd.Parameters.Add("@appid", SqlDbType.Int).Value=appid;          
            cnn.Open();
            userrole = (string)cmd.ExecuteScalar();

        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
        }
        finally
        {
            cnn.Close();
        }

        return userrole;
    }

}
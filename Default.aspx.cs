﻿using System;
using System.Configuration;
using System.Web.UI;
using System.DirectoryServices;
using System.Data.SqlClient;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Web.Helpers;

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
            //if (username.Contains("@purolator.com"))
            //{
            //    validuser = ValidateCredentials(username, password);
            //    return validuser;
            //}

            DirectoryEntry DE = new DirectoryEntry(@"LDAP://" + path, username, password);
            DirectorySearcher DS = new DirectorySearcher(DE);
            DS.Filter = "sAMAccountName=" + username;
            SearchResult SR = DS.FindOne();
            DirectoryEntry USER = null;
            if (SR != null)
            {           
             USER = SR.GetDirectoryEntry();

       
                string displayname = USER.Properties["displayName"].Value.ToString();
                string firstname = USER.Properties["givenName"].Value.ToString();
                string lastname = USER.Properties["sn"].Value.ToString();
                string email = USER.Properties["userPrincipalName"].Value.ToString();
                string objectname = USER.Properties["objectCategory"].Value.ToString();
                string accountname = USER.Properties["sAMAccountName"].Value.ToString();
                string name = USER.Properties["name"].Value.ToString();
                string distinguishedName = USER.Properties["distinguishedName"].Value.ToString();

                validuser = true;
            }  else
            {
                //New Users will use name with @purolator.com to login, and their AccountName will be their  EmployeeID
                //First get sAMAccountName using the login, which inclues @purolator.com
                //then use the Directory Searcher using that ID 

                String accountName = getAccountName(username);


                DE = new DirectoryEntry(@"LDAP://" + path, accountName, password);
                DS = new DirectorySearcher(DE);
                DS.Filter = "sAMAccountName=" + accountName;
                SR = DS.FindOne();
                USER = SR.GetDirectoryEntry();
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

        }
        catch (System.Exception e)
        {

        }

        return validuser;
    }
    public static bool ValidateCredentials(string username, string password)
    {
        bool validuser = false;
        try
        {
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                validuser = context.ValidateCredentials(username, password);
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

    private string getAccountName(string username)
    {
        string userrole = "";
        SqlConnection cnn;
        String strConnString = ConfigurationManager.ConnectionStrings["PurolatorReportingConnectionString"].ConnectionString;
        cnn = new SqlConnection(strConnString);
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd = new SqlCommand("sp_GetADAccountNameFromPrincipalName", cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;            
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
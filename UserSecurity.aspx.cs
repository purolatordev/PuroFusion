using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DAL;
using Telerik.Web.UI;
using PI_Application;
using PI_People;
using System.Data.SqlClient;
using System.Data;

public partial class UserSecurity : System.Web.UI.Page
{
    String strConnString = ConfigurationManager.ConnectionStrings["PurolatorReportingConnectionString"].ConnectionString;
    Int16 idPI_Application = Convert.ToInt16(ConfigurationManager.AppSettings["AppID"]);
    PuroTouchRepository rep = new PuroTouchRepository();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["userName"] != null && Session["appName"] != null && ((string)Session["userRole"] == "Admin" || (string)Session["userRole"] == "ITAdmin" || (string)Session["userRole"] == "ITManager"))
            {
                
                loadUsers();
                rgUsers.Rebind();

            }
            else
            {
                Response.Redirect("NoAccess.aspx");

            }
        }
        else
        {
            
        }
    }
   
   

    protected void rgUsers_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        loadUsers();
    }


    protected void loadUsers()
    {

        ClsAppUsers au = new ClsAppUsers();
        List<ClsAppUsers> appusers = au.GetListClsAppUsers(idPI_Application);
        rgUsers.DataSource = appusers;

    }


    protected void rgUsers_InsertCommand(object sender, GridCommandEventArgs e)
    {
        //hardcoding application for now - should do lookup backed on appNameContracts

        Int32 idEmployee;
        Int32 idPI_ApplicationRole;
        Int32 idPI_ApplicationUser;

        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string msg = "";

            //Check that passwords match
            string pwd1 = "";
            //string pwd1 = (userControl.FindControl("txtPWD") as RadTextBox).Text;
            //string pwd2 = (userControl.FindControl("txtPWD2") as RadTextBox).Text;
            //if (pwd1=="")
            //{
            //    //msg = "Password is Required";
            //}
            //if (pwd1 != pwd2)
            //{
            //    //msg = "Passwords do not match";
            //}
            if (msg != "")
            {
                pnlDanger.Visible = true;
                lblDanger.Text = msg;
            }
            else
            {

                //SET UP USER
                RadComboBox cbxUsers = userControl.FindControl("cbxUsers") as RadComboBox;
                idEmployee = Convert.ToInt16(cbxUsers.SelectedValue);


                //ClsEncryptDecrypt decrypt = new ClsEncryptDecrypt();
                //string encryptedPwd = decrypt.EncryptString(pwd1);
                string encryptedPwd = "";


                //Do Insert into tblPI_ApplicationUser, then use the id generated (@idPI_ApplicaitonUSer) and insert into tblPI_ApplicationUserRole

                //PROCEDURE [dbo].[spPI_ApplicationUser_Insert]
                //@idPI_Application int,
                //@idEmployee int,
                //@idPI_ApplicationUser int output
                //MK - change to add password

                //Check if user already there
                bool userExists = checkUserExists(idPI_Application, idEmployee);
                if (userExists == true)
                {
                    pnlDanger.Visible = true;
                    lblDanger.Text = "User with EmployeeID " + idEmployee.ToString() + " already exists";
                }
                else
                {
                    SqlConnection cnn;
                    SqlCommand cmd;
                    cnn = new SqlConnection(strConnString);

                    cnn.Open();
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spPI_ApplicationUser_Insert";

                    cmd.Parameters.Add("@idPI_Application", SqlDbType.Int).Value = idPI_Application;
                    cmd.Parameters.Add("@idEmployee", SqlDbType.Int).Value = idEmployee;
                    cmd.Parameters.Add("@encryptedPassword", SqlDbType.VarChar).Value = encryptedPwd;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = pwd1;
                    var idPI_ApplicationUserOut = new SqlParameter
                    {
                        ParameterName = "@idPI_ApplicationUser",
                        Direction = ParameterDirection.Output,
                        SqlDbType = System.Data.SqlDbType.Int

                    };

                    cmd.Parameters.Add(idPI_ApplicationUserOut);

                    cmd.Connection = cnn;
                    cmd.ExecuteNonQuery();
                    //assign output paramter so we can use it in next stp
                    idPI_ApplicationUser = (Int32)cmd.Parameters["@idPI_ApplicationUser"].Value;

                    cmd.Dispose();
                    cnn.Close();

                    //SET UP USER ROLE
                    RadComboBox cbxUserRoles = userControl.FindControl("cbxUserRole") as RadComboBox;
                    string userRole = cbxUserRoles.SelectedValue;
                    idPI_ApplicationRole = Convert.ToInt16(cbxUserRoles.SelectedValue);

                    //PROCEDURE [dbo].[spPI_ApplicationUserRole_Insert]
                    //@idPI_ApplicationUser int,
                    //@idPI_ApplicationRole int,
                    //@idPI_ApplicationUserRole int output    
                    //SqlConnection cnn;
                    SqlCommand cmd2;
                    cnn = new SqlConnection(strConnString);

                    cnn.Open();
                    cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "spPI_ApplicationUserRole_Insert";

                    cmd2.Parameters.Add("@idPI_ApplicationUser", SqlDbType.Int).Value = idPI_ApplicationUser;
                    cmd2.Parameters.Add("@idPI_ApplicationRole", SqlDbType.Int).Value = idPI_ApplicationRole;

                    var idPI_ApplicationUserRoleOut = new SqlParameter
                    {
                        ParameterName = "@idPI_ApplicationUserRole",
                        Direction = ParameterDirection.Output,
                        SqlDbType = System.Data.SqlDbType.Int
                    };
                    cmd2.Parameters.Add(idPI_ApplicationUserRoleOut);
                    cmd2.Connection = cnn;
                    cmd2.ExecuteNonQuery();
                    string struserRole = cbxUserRoles.Text;

                    //Insert district restriction, if Sales Manager
                    if (struserRole == "SalesDM")
                    {
                        RadComboBox cbxDistricts = userControl.FindControl("cbxDistrict") as RadComboBox;
                        //string district = cbxDistricts.SelectedValue;
                        string district = cbxDistricts.Text;
                        InsertDistrictRestriction(district, idPI_ApplicationUser);
                    }

                }
                 

            }

           
          

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }

    }

    protected bool checkUserExists(int idApplication, int idEmployee)
    {
        bool exists = false;       
        

        SqlConnection cnn;
        cnn = new SqlConnection(strConnString);
        cnn.Open();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd = new SqlCommand("spPI_CheckifUserExists", cnn);
            cmd.Parameters.Add("@idPI_Application", SqlDbType.Int).Value = idPI_Application;
            cmd.Parameters.Add("@idEmployee", SqlDbType.Int).Value = idEmployee;
            cmd.CommandType = CommandType.StoredProcedure;
            exists = (bool)cmd.ExecuteScalar();

        }
        catch (Exception ex)
        {
            string errMsg = ex.Message.ToString();
        }
        finally
        {
            cnn.Close();
        }
        

        return exists;
    }

    protected void rgUsers_UpdateCommand(object sender, GridCommandEventArgs e)
    {

        Int16 idPI_ApplicationUser;
        Int16 idPI_ApplicationRole;

        try
        {
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //If Passwords were changed, Check That Passwords Match
            //
            //bool pwdChanged = (userControl.FindControl("CheckBox1") as CheckBox).Checked;
            //string pwd1 = (userControl.FindControl("txtPWD") as RadTextBox).Text;
            bool allowUpdate = true;
           

            if (allowUpdate)
            {
                HiddenField hdApplicationUser = userControl.FindControl("hdnApplicationUser") as HiddenField;
                idPI_ApplicationUser = Convert.ToInt16(hdApplicationUser.Value);
                RadComboBox cbxUserRoles = userControl.FindControl("cbxUserRole") as RadComboBox;
                string userRole = cbxUserRoles.Text;
                idPI_ApplicationRole = Convert.ToInt16(cbxUserRoles.SelectedValue);

                //Do update of tblPI_ApplicationUserRole, set idPI_ApplicationRole for idPI_ApplicationUser
                //stp: PROCEDURE [dbo].[spPI_ApplicationUserRole_Update]
                //@idPI_ApplicationUser int,
                //@idPI_ApplicationRole int

                SqlConnection cnn;
                SqlCommand cmd;
                cnn = new SqlConnection(strConnString);

                cnn.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spPI_ApplicationUserRole_Update";

                cmd.Parameters.Add("@idPI_ApplicationUser", SqlDbType.Int).Value = idPI_ApplicationUser;
                cmd.Parameters.Add("@idPI_ApplicationRole", SqlDbType.Int).Value = idPI_ApplicationRole;

                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                //Insert district restriction, if Sales Manager
                if (userRole == "SalesDM")
                {
                    RadComboBox cbxDistricts = userControl.FindControl("cbxDistrict") as RadComboBox;
                    string district = cbxDistricts.Text;
                    rgUsers_DeleteRestrictions(idPI_ApplicationUser);
                    InsertDistrictRestriction(district, idPI_ApplicationUser);
                }

                
            }

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }
    }

    protected void rgUsers_DeleteCommand(object source, GridCommandEventArgs e)
    {

        Int32 idPI_ApplicationUser = 0;
        int? idPI_ApplicationUserRole = 0;

        try
        {
            //MK - get the idPI_ApplicationUser and idPI_ApplicationUserRole from the grid item
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["idPI_ApplicationUser"].ToString();
            idPI_ApplicationUser = Convert.ToInt32(ID);
            //get idPI_ApplicationUserRole
            ClsAppUsers appuser = new ClsAppUsers();
            ClsAppUsers thisuser = appuser.GetAppUser(idPI_ApplicationUser);
            idPI_ApplicationUserRole = thisuser.idPI_ApplicationUserRole;



            //Delete from tblPI_ApplicationUserRole and tblPI_ApplicationUser
            //stp: [dbo].[spPI_ApplicationUserRole_Delete]
            //@idPI_ApplicationUserRole int
            SqlConnection cnn;
            SqlCommand cmd;
            cnn = new SqlConnection(strConnString);

            cnn.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPI_ApplicationUserRole_Delete";

            cmd.Parameters.Add("@idPI_ApplicationUserRole", SqlDbType.Int).Value = idPI_ApplicationUserRole;

            cmd.Connection = cnn;
            cmd.ExecuteNonQuery();


            //Delete District Restriction, if there is one
            rgUsers_DeleteRestrictions(idPI_ApplicationUser);


            //stp: PROCEDURE [dbo].[spPI_ApplicationUser_Delete]
            //@idPI_ApplicationUser int
            SqlCommand cmd2;
            cnn = new SqlConnection(strConnString);

            cnn.Open();
            cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "spPI_ApplicationUser_Delete";

            cmd2.Parameters.Add("@idPI_ApplicationUser", SqlDbType.Int).Value = idPI_ApplicationUser;

            cmd2.Connection = cnn;
            cmd2.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }


    }

    protected void rgUsers_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            var msg = "";


            if (e.Item is GridEditFormInsertItem || e.Item is GridDataInsertItem)
            {
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                //Users
                RadComboBox cbxUsers = userControl.FindControl("cbxUsers") as RadComboBox;
                ClsEmployee emp = new ClsEmployee();
                List<ClsEmployee> listUsers = emp.GetListClsEmployees();
                cbxUsers.DataTextField = "UserName";
                cbxUsers.DataValueField = "idEmployee";
                cbxUsers.DataSource = listUsers;
                cbxUsers.DataBind();

                //Role
                string UserRoleHidden = (userControl.FindControl("hdnUserRole") as HiddenField).Value;
                RadComboBox cbxUserRoles = userControl.FindControl("cbxUserRole") as RadComboBox;
                List<clsPI_ApplicationRole> listRoles = clsPI_ApplicationRole.GetApplicationRoles(idPI_Application);
                cbxUserRoles.DataTextField = "RoleName";
                cbxUserRoles.DataValueField = "ApplicationRoleId";
                cbxUserRoles.DataSource = listRoles;
                cbxUserRoles.DataBind();

                //District
                RadComboBox cbxDistrict = userControl.FindControl("cbxDistrict") as RadComboBox;
                List<ClsDistrict> listDistricts = rep.GetDistricts();
                cbxDistrict.DataTextField = "District";
                cbxDistrict.DataValueField = "District";
                cbxDistrict.DataSource = listDistricts;
                cbxDistrict.DataBind();

             
            }
            else
            {

                if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
                {
                    //************First calling dropdown list values selected in pop up edit form**************/ 
                    UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                    string UserHidden = (userControl.FindControl("hdnUser") as HiddenField).Value;

                    //USER NAME
                    try
                    {


                        RadComboBox cbxUsers = userControl.FindControl("cbxUsers") as RadComboBox;
                        ClsEmployee emp = new ClsEmployee();
                        List<ClsEmployee> listUsers = emp.GetListClsEmployees();

                        cbxUsers.DataTextField = "UserName";
                        cbxUsers.DataValueField = "idEmployee";
                        cbxUsers.DataSource = listUsers;
                        cbxUsers.DataBind();
                        cbxUsers.SelectedValue = UserHidden;
                        cbxUsers.Enabled = false;            
                       
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }                   


                    //USER ROLE
                    try
                    {

                        string UserRoleHidden = (userControl.FindControl("hdnUserRole") as HiddenField).Value;
                        RadComboBox cbxUserRoles = userControl.FindControl("cbxUserRole") as RadComboBox;


                        List<clsPI_ApplicationRole> listRoles = clsPI_ApplicationRole.GetApplicationRoles(idPI_Application);

                        cbxUserRoles.DataTextField = "RoleName";
                        cbxUserRoles.DataValueField = "ApplicationRoleId";
                        cbxUserRoles.DataSource = listRoles;
                        cbxUserRoles.DataBind();
                        cbxUserRoles.SelectedValue = UserRoleHidden;                        

                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }


                    //User Restrictions                  
                    try
                    {
                        RadComboBox cbxDistrict = userControl.FindControl("cbxDistrict") as RadComboBox;
                        List<ClsDistrict> listDistricts = rep.GetDistricts();
                        cbxDistrict.DataTextField = "District";
                        cbxDistrict.DataValueField = "District";
                        cbxDistrict.DataSource = listDistricts;
                        cbxDistrict.DataBind();
                        RequiredFieldValidator rfDistrict = userControl.FindControl("rfDistrict") as RequiredFieldValidator;
                        Label lblDistrict = userControl.FindControl("lblDistrict") as Label;
                        RadComboBox cbxUserRoles = userControl.FindControl("cbxUserRole") as RadComboBox;
                        string UserRoleText = cbxUserRoles.SelectedItem.Text;
                        HiddenField hdApplicationUser = userControl.FindControl("hdnApplicationUser") as HiddenField;
                        int idPI_ApplicationUser = Convert.ToInt16(hdApplicationUser.Value);

                        string appname = Session["appName"].ToString();

                        switch (UserRoleText.ToLower())
                        {

                            case "salesdm":
                                string district = "";
                                //Get District for user if this a Sales District Manager
                                clsDistrictRestriction dr = new clsDistrictRestriction();
                                district = dr.GetDistrictRestriction(idPI_ApplicationUser, appname);
                                if (district != "")
                                {
                                    cbxDistrict.SelectedValue = district;
                                }
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
                        msg = ex.Message;
                    }

                }
            }
            
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }
    }

    protected void rgUsers_DeleteRestrictions(int appuserID)
    {

        try
        {

            //Delete from tblPI_ApplicationDistrictRestriction for this user
            SqlConnection cnn;
            SqlCommand cmd;
            cnn = new SqlConnection(strConnString);

            cnn.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPI_ApplicationDistrictRestriction_UserDelete";

            cmd.Parameters.Add("@idPI_ApplicationUser", SqlDbType.Int).Value = appuserID;

            cmd.Connection = cnn;
            cmd.ExecuteNonQuery();


        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }


        
    }


      

    protected void InsertDistrictRestriction(string District, int idPIApplicationUser)
    {
        try
        {

            SqlConnection cnn;
            SqlCommand cmd;
            cnn = new SqlConnection(strConnString);

            cnn.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPI_ApplicationDistrictRestriction_Insert";

            cmd.Parameters.Add("@idPI_ApplicationUser", SqlDbType.Int).Value = idPIApplicationUser;
            cmd.Parameters.Add("@District", SqlDbType.NVarChar).Value = District.Trim();
            cmd.Connection = cnn;
            cmd.ExecuteNonQuery();


        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
        }

    }
}
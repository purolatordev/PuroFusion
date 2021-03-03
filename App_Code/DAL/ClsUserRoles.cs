using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsUserRoles
/// </summary>
public class ClsUserRoles
{
    public string UserName { get; set; }
    public string ActiveDirectoryName { get; set; }
    public string RoleName { get; set; }
    public string EncryptedPassword { get; set; }


    public List<ClsUserRoles> GetListClsAppUsers(string appname)
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        List<ClsUserRoles> oAppUserlist = (from data in puroTouchContext.GetTable<vw_UserRole>()
                                           where data.ApplicationName == appname
                                           orderby data.UserName
                                           select new ClsUserRoles
                                           {
                                               UserName = data.UserName,
                                               ActiveDirectoryName = data.ActiveDirectoryName,
                                               RoleName = data.RoleName
                                           }).ToList();
        return oAppUserlist;
    }

    public ClsUserRoles GetAppUserWithPassword(string appname, string username)
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        ClsUserRoles oAppUser = (from data in puroTouchContext.GetTable<vw_UserRole>()
                                 where data.ApplicationName == appname
                                 where data.ActiveDirectoryName == username
                                 orderby data.UserName
                                 select new ClsUserRoles
                                 {
                                     UserName = data.UserName,
                                     ActiveDirectoryName = data.ActiveDirectoryName,
                                     RoleName = data.RoleName,
                                     EncryptedPassword = data.EncryptedPassword
                                 }).SingleOrDefault<ClsUserRoles>();
        return oAppUser;
    }
}
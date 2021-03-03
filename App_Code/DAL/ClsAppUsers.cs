using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClsAppUsers
/// </summary>
/// 
 public class ClsAppUsers
    {

      //constructor
      // public ClsAppUsers()
      //{

      //}

        public string UserName { get; set; }
        public string ActiveDirectoryName { get; set; }
        public int? idPI_ApplicationUserRole { get; set; }
        public int? idPI_ApplicationUser { get; set; }
        public string RoleName { get; set; }
        public string role_UpdatedBy { get; set; }
        public DateTime? role_UpdatedOn { get; set; }
        public int? idEmployee { get; set; }
        public int? idPI_ApplicationRole { get; set; }


        public ClsAppUsers GetAppUser(Int32 idApplicationUser)
        {
            PurolatorReportingSQLDataContext prContext = new PurolatorReportingSQLDataContext();
            ClsAppUsers oAppUser = (from data in prContext.GetTable<vw_PI_ApplicationUser>()
                                    where data.idPI_ApplicationUser == idApplicationUser

                                    select new ClsAppUsers
                                    {
                                        UserName = data.UserName,
                                        ActiveDirectoryName = data.ActiveDirectoryName,
                                        idPI_ApplicationUserRole = data.idPI_ApplicationUserRole,
                                        idPI_ApplicationUser = data.idPI_ApplicationUser,
                                        RoleName = data.RoleName,
                                        role_UpdatedBy = data.role_UpdatedBy,
                                        role_UpdatedOn = data.role_UpdatedOn,
                                        idEmployee = data.idEmployee,
                                        idPI_ApplicationRole = data.idPI_ApplicationRole

                                    }).FirstOrDefault();
            return oAppUser;
        }

        public List<ClsAppUsers> GetListClsAppUsers(int idPIApplication)
        {
            PurolatorReportingSQLDataContext prContext = new PurolatorReportingSQLDataContext();
            List<ClsAppUsers> oAppUserlist = (from data in prContext.GetTable<vw_PI_ApplicationUser>()
                                              where data.idPI_Application == idPIApplication
                                              orderby data.UserName
                                              select new ClsAppUsers
                                              {
                                                  UserName = data.UserName,
                                                  ActiveDirectoryName = data.ActiveDirectoryName,
                                                  idPI_ApplicationUserRole = data.idPI_ApplicationUserRole,
                                                  idPI_ApplicationUser = data.idPI_ApplicationUser,
                                                  RoleName = data.RoleName,
                                                  role_UpdatedBy = data.role_UpdatedBy,
                                                  role_UpdatedOn = data.role_UpdatedOn,
                                                  idEmployee = data.idEmployee,
                                                  idPI_ApplicationRole = data.idPI_ApplicationRole

                                              }).ToList();
            return oAppUserlist;
        }


    
}
 public class ClsEmployee
 {
     //constructor
     //public ClsEmployee()
     //{
     //}

     public int? idEmployee { get; set; }
     public string FirstName { get; set; }
     public string LastName { get; set; }
     public string UserName { get; set; }
     public List<ClsEmployee> GetListClsEmployees()
     {
         PurolatorReportingSQLDataContext prContext = new PurolatorReportingSQLDataContext();
         List<ClsEmployee> oEmployeelist = (from data in prContext.GetTable<tblEmployee>()
                                            orderby data.FirstName
                                            select new ClsEmployee
                                            {
                                                idEmployee = data.idEmployee,
                                                FirstName = data.FirstName,
                                                LastName = data.LastName,
                                                UserName = data.FirstName + " " + data.LastName
                                            }).ToList();
         return oEmployeelist;
     }
    
 }

 public class ClsApp
 {
     //constructor
     //public ClsApp()
     //{
     //}

     public int? idPIApplication { get; set; }
     public string AppName { get; set; }

     public List<ClsApp> GetListClsApps()
     {
         PurolatorReportingSQLDataContext prContext = new PurolatorReportingSQLDataContext();
         List<ClsApp> oApplist = (from data in prContext.GetTable<tblPI_Application>()
                                  orderby data.ApplicationName
                                  select new ClsApp
                                  {
                                      idPIApplication = data.idPI_Application,
                                      AppName = data.ApplicationName
                                  }).ToList();
         return oApplist;
     }
    
 }

 public class clsDistrictRestriction
 {
     public clsDistrictRestriction()
     {
     }

     public int idPI_ApplicationDistrictRestriction { get; set; }
     public string userName { get; set; }
     public string District { get; set; }

     public string GetDistrictRestriction(string userName, string appName)
     {
         PurolatorReportingSQLDataContext prContext = new PurolatorReportingSQLDataContext();
         clsDistrictRestriction oDistlist = (from data in prContext.GetTable<tblPI_ApplicationDistrictRestriction>()
                                                   join users in prContext.GetTable<tblPI_ApplicationUser>() on data.idPI_ApplicationUser equals users.idPI_ApplicationUser
                                                   join app in prContext.GetTable<tblPI_Application>() on users.idPI_Application equals app.idPI_Application
                                                   join emp in prContext.GetTable<tblEmployee>() on users.idEmployee equals emp.idEmployee
                                                   where app.ApplicationName == appName
                                                   where emp.ActiveDirectoryName == userName
                                                   orderby data.District
                                                   select new clsDistrictRestriction
                                                   {
                                                       District = data.District
                                                   }).FirstOrDefault();
         return oDistlist.District;
     }



     public string GetDistrictRestriction(int userID, string appName)
     {
         PurolatorReportingSQLDataContext prContext = new PurolatorReportingSQLDataContext();
         clsDistrictRestriction oDist = (from data in prContext.GetTable<tblPI_ApplicationDistrictRestriction>()
                         join users in prContext.GetTable<tblPI_ApplicationUser>() on data.idPI_ApplicationUser equals users.idPI_ApplicationUser
                         join app in prContext.GetTable<tblPI_Application>() on users.idPI_Application equals app.idPI_Application
                         join emp in prContext.GetTable<tblEmployee>() on users.idEmployee equals emp.idEmployee
                         where app.ApplicationName == appName
                         where data.idPI_ApplicationUser == userID
                         orderby data.District
                         select new clsDistrictRestriction
                         {
                             District = data.District
                         }).FirstOrDefault();
         return oDist.District;
     }
 }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for clsEDISpecialist
/// </summary>
public class clsEDISpecialist
{
    public int idEDISpecialist { get; set; }
    public int idEmployee { get; set; }
    public string ActiveDirectoryName { get; set; }
    public string Name { get; set; }
    public string email { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public bool? ReceiveNewReqEmail { get; set; }
    public string login { get; set; }
}
public static class SrvEDISpecialist
{
    public static List<clsEDISpecialist> GetEDISpecialist()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDISpecialist> qEDISpecialisth = o.GetTable<tblEDISpecialist>()
                                            .Select(p => new clsEDISpecialist() { idEDISpecialist = p.idEDISpecialist, idEmployee = p.idEmployee, login = p.login, email = p.email,ReceiveNewReqEmail = p.ReceiveNewReqEmail,ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qEDISpecialisth;
    }
    public static List<clsEDISpecialist> GetEDISpecialistView()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDISpecialist> qEDISpecialisth = o.GetTable<vw_EDISpecialist>()
                                            .Select(p => new clsEDISpecialist() { ActiveDirectoryName = p.ActiveDirectoryName,Name = p.Name, idEDISpecialist = p.idEDISpecialist, idEmployee = p.idEmployee, login = p.login, email = p.email, ReceiveNewReqEmail = p.ReceiveNewReqEmail, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qEDISpecialisth;
    }
    public static clsEDISpecialist GetEDISpecialistByIDView(int idEDISpecialist)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        clsEDISpecialist qEDISpecialisth = o.GetTable<vw_EDISpecialist>()
                                            .Where(p => p.idEDISpecialist == idEDISpecialist)
                                            .Select(p => new clsEDISpecialist() { ActiveDirectoryName = p.ActiveDirectoryName, Name = p.Name, idEDISpecialist = p.idEDISpecialist, idEmployee = p.idEmployee, login = p.login, email = p.email, ReceiveNewReqEmail = p.ReceiveNewReqEmail, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .FirstOrDefault();

        return qEDISpecialisth;
    }
    public static string InsertEDISpecialist(clsEDISpecialist data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            tblEDISpecialist oNewRow = new tblEDISpecialist()
            {
                email = data.email,
                idEmployee = data.idEmployee,
                CreatedBy = data.CreatedBy,
                CreatedOn = DateTime.Now,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag,
                ReceiveNewReqEmail = data.ReceiveNewReqEmail,
                login = data.login
            };

            o.GetTable<tblEDISpecialist>().InsertOnSubmit(oNewRow);
            o.SubmitChanges();
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
    public static string UpdateEDISpecialist(clsEDISpecialist data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            if (data.idEmployee > 0)
            {
                var query = from qdata in o.GetTable<tblEDISpecialist>()
                    where qdata.idEmployee == data.idEmployee
                    select qdata;

                foreach (tblEDISpecialist updRow in query)
                {
                    updRow.email = data.email;
                    updRow.idEmployee = data.idEmployee;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = DateTime.Now;
                    updRow.ReceiveNewReqEmail = data.ReceiveNewReqEmail;
                    updRow.login = data.login;
                }

                // Submit the changes to the database. 
                o.SubmitChanges();
            }
            else
            {
                errMsg = "There is No Shipping Product with ID = " + "'" + data.idEmployee + "'";
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}

public class clsBillingSpecialist
{
    public int idBillingSpecialist { get; set; }
    public int idEmployee { get; set; }
    public string ActiveDirectoryName { get; set; }
    public string Name { get; set; }
    public string email { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public bool? ReceiveNewReqEmail { get; set; }
    public string login { get; set; }
}
public static class SrvBillingSpecialist
{
    public static List<clsBillingSpecialist> GetBillingSpecialist()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsBillingSpecialist> qEDISpecialisth = o.GetTable<tblBillingSpecialist>()
                                            .Select(p => new clsBillingSpecialist() { idBillingSpecialist = p.idBillingSpecialist, idEmployee = p.idEmployee, login = p.login, email = p.email, ReceiveNewReqEmail = p.ReceiveNewReqEmail, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qEDISpecialisth;
    }
    public static List<clsBillingSpecialist> GetBillingSpecialistView()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsBillingSpecialist> qEDISpecialisth = o.GetTable<vw_BillingSpecialist>()
                                            .Select(p => new clsBillingSpecialist() { ActiveDirectoryName = p.ActiveDirectoryName, Name = p.Name, idBillingSpecialist = p.idBillingSpecialist, idEmployee = p.idEmployee, login = p.login, email = p.email, ReceiveNewReqEmail = p.ReceiveNewReqEmail, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qEDISpecialisth;
    }
    public static string InsertBillingSpecialist(clsBillingSpecialist data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            tblBillingSpecialist oNewRow = new tblBillingSpecialist()
            {
                email = data.email,
                idEmployee = data.idEmployee,
                CreatedBy = data.CreatedBy,
                CreatedOn = DateTime.Now,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag,
                ReceiveNewReqEmail = data.ReceiveNewReqEmail,
                login = data.login
            };

            o.GetTable<tblBillingSpecialist>().InsertOnSubmit(oNewRow);
            o.SubmitChanges();
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
    public static string UpdateBillingSpecialist(clsBillingSpecialist data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            if (data.idEmployee > 0)
            {
                var query = from qdata in o.GetTable<tblBillingSpecialist>()
                            where qdata.idEmployee == data.idEmployee
                            select qdata;

                foreach (tblBillingSpecialist updRow in query)
                {
                    updRow.email = data.email;
                    updRow.idEmployee = data.idEmployee;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = DateTime.Now;
                    updRow.ReceiveNewReqEmail = data.ReceiveNewReqEmail;
                    updRow.login = data.login;
                }

                // Submit the changes to the database. 
                o.SubmitChanges();
            }
            else
            {
                errMsg = "There is No Shipping Product with ID = " + "'" + data.idEmployee + "'";
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
public class clsCollectionSpecialist
{
    public int idCollectionSpecialist { get; set; }
    public int idEmployee { get; set; }
    public string ActiveDirectoryName { get; set; }
    public string Name { get; set; }
    public string email { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public bool? ReceiveNewReqEmail { get; set; }
    public string login { get; set; }
}
public static class SrvCollectionSpecialist
{
    public static List<clsCollectionSpecialist> GetCollectionSpecialist()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsCollectionSpecialist> qEDISpecialisth = o.GetTable<tblCollectionSpecialist>()
                                            .Select(p => new clsCollectionSpecialist() { idCollectionSpecialist = p.idCollectionSpecialist, idEmployee = p.idEmployee, login = p.login, email = p.email, ReceiveNewReqEmail = p.ReceiveNewReqEmail, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qEDISpecialisth;
    }
    public static List<clsCollectionSpecialist> GetCollectionSpecialistView()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsCollectionSpecialist> qEDISpecialisth = o.GetTable<vw_CollectionSpecialist>()
                                            .Select(p => new clsCollectionSpecialist() { ActiveDirectoryName = p.ActiveDirectoryName, Name = p.Name, idCollectionSpecialist = p.idCollectionSpecialist, idEmployee = p.idEmployee, login = p.login, email = p.email, ReceiveNewReqEmail = p.ReceiveNewReqEmail, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qEDISpecialisth;
    }
    public static string InsertCollectionSpecialist(clsCollectionSpecialist data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            tblCollectionSpecialist oNewRow = new tblCollectionSpecialist()
            {
                email = data.email,
                idEmployee = data.idEmployee,
                CreatedBy = data.CreatedBy,
                CreatedOn = DateTime.Now,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag,
                ReceiveNewReqEmail = data.ReceiveNewReqEmail,
                login = data.login
            };

            o.GetTable<tblCollectionSpecialist>().InsertOnSubmit(oNewRow);
            o.SubmitChanges();
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
    public static string UpdateCollectionSpecialist(clsCollectionSpecialist data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            if (data.idEmployee > 0)
            {
                var query = from qdata in o.GetTable<tblCollectionSpecialist>()
                            where qdata.idEmployee == data.idEmployee
                            select qdata;

                foreach (tblCollectionSpecialist updRow in query)
                {
                    updRow.email = data.email;
                    updRow.idEmployee = data.idEmployee;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = DateTime.Now;
                    updRow.ReceiveNewReqEmail = data.ReceiveNewReqEmail;
                    updRow.login = data.login;
                }

                // Submit the changes to the database. 
                o.SubmitChanges();
            }
            else
            {
                errMsg = "There is No Shipping Product with ID = " + "'" + data.idEmployee + "'";
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using context = System.Web.HttpContext;

/// <summary>
/// Summary description for clsEDITransaction
/// </summary>
public class clsEDITransaction
{
	public int idEDITranscation { get; set; }
	public int idRequest { get; set; }
	public int idEDITranscationType { get; set; }
    public string EDITranscationType { get; set; }
    public int TotalRequests { get; set; }
    public System.Nullable<bool> CombinePayer { get; set; }

    public System.Nullable<bool> BatchInvoices { get; set; }

    public string CreatedBy { get; set; }

	public System.Nullable<System.DateTime> CreatedOn { get; set; }

	public string UpdatedBy { get; set; }

	public System.Nullable<System.DateTime> UpdatedOn { get; set; }

	public System.Nullable<bool> ActiveFlag { get; set; }
}
public static class SrvEDITransaction
{
    public static List<clsEDITransaction> GetEDITransactionsByidRequest(int idRequest)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDITransaction> qShipMeth = o.GetTable<tblEDITranscation>()
                                            .Where(p => p.idRequest == idRequest && p.tblEDITranscationType.CategoryID == 0)
                                            .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, TotalRequests = p.TotalRequests, BatchInvoices = p.BatchInvoices, CombinePayer = p.CombinePayer ,ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qShipMeth;
    }
    public static List<clsEDITransaction> GetEDITransactionsByidRequest(int idRequest, int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDITransaction> qShipMeth = o.GetTable<tblEDITranscation>()
                                            .Where(p => p.idRequest == idRequest && p.idEDITranscationType == idEDITranscationType && p.ActiveFlag == true)
                                            .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, TotalRequests = p.TotalRequests, BatchInvoices = p.BatchInvoices.HasValue ? p.BatchInvoices:false, CombinePayer = p.CombinePayer.HasValue ? p.CombinePayer : false, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qShipMeth;
    }
    public static clsEDITransaction GetAEDITransactionsByidRequest(int idRequest, int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        clsEDITransaction qShipMeth = null;
        qShipMeth = o.GetTable<tblEDITranscation>()
                                            .Where(p => p.idRequest == idRequest && p.idEDITranscationType == idEDITranscationType && p.ActiveFlag == true)
                                            .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, TotalRequests = p.TotalRequests, BatchInvoices = p.BatchInvoices.HasValue ? p.BatchInvoices : false, CombinePayer = p.CombinePayer.HasValue ? p.CombinePayer : false, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .FirstOrDefault();

        return qShipMeth;
    }
    public static clsEDITransaction GetAnyEDITransactionsByidRequest(int idRequest, int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        clsEDITransaction qShipMeth = null;
        qShipMeth = o.GetTable<tblEDITranscation>()
                                            .Where(p => p.idRequest == idRequest && p.idEDITranscationType == idEDITranscationType )
                                            .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, TotalRequests = p.TotalRequests, BatchInvoices = p.BatchInvoices.HasValue ? p.BatchInvoices : false, CombinePayer = p.CombinePayer.HasValue ? p.CombinePayer : false, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .FirstOrDefault();
        return qShipMeth;
    }
    public static string Remove(int idEDITranscation)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        try
        {
            var q = puroTouchContext.GetTable<tblEDITranscation>().Where(f => f.idEDITranscation == idEDITranscation).FirstOrDefault();
            puroTouchContext.GetTable<tblEDITranscation>().DeleteOnSubmit(q);
            puroTouchContext.SubmitChanges();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
        return errMsg;
    }
    public static string Remove(int idRequest, int idEDITranscationType)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            var q = o.GetTable<tblEDITranscation>().Where(f=> f.idRequest == idRequest && f.idEDITranscationType == idEDITranscationType).FirstOrDefault();
            o.GetTable<tblEDITranscation>().DeleteOnSubmit(q);
            o.SubmitChanges();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
        return errMsg;
    }
    public static string Remove(clsEDITransaction data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            var q = o.GetTable<tblEDITranscation>().Where(f => f.idRequest == data.idRequest && f.idEDITranscationType == data.idEDITranscationType).FirstOrDefault();
            q.ActiveFlag = false;
            //o.GetTable<tblEDITranscation>().(q);
            //o.GetTable<tblEDITranscation>().DeleteOnSubmit(q);
            o.SubmitChanges();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
        return errMsg;
    }

    public static string Insert(clsEDITransaction data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        newID = -1;
        try
        {
            tblEDITranscation EDITrans = o.GetTable<tblEDITranscation>()
                                    .Where(p => p.idRequest == data.idRequest && p.idEDITranscationType == data.idEDITranscationType)
                                    .FirstOrDefault();
            if (EDITrans == null)
            {
                tblEDITranscation oNewRow = new tblEDITranscation()
                {
                    idEDITranscationType = data.idEDITranscationType,
                    TotalRequests = data.TotalRequests,
                    BatchInvoices = data.BatchInvoices.HasValue ? data.BatchInvoices : false,
                    CombinePayer = data.CombinePayer.HasValue ? data.CombinePayer : false,
                    idRequest = data.idRequest,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = data.CreatedOn,
                    ActiveFlag = true
                };
                o.GetTable<tblEDITranscation>().InsertOnSubmit(oNewRow);
                o.SubmitChanges();
                newID = oNewRow.idEDITranscation;
                data.idRequest = newID;
            }
            else
            {
                EDITrans.ActiveFlag = true;
                EDITrans.BatchInvoices = data.BatchInvoices.HasValue ? data.BatchInvoices : false;
                EDITrans.CombinePayer = data.CombinePayer.HasValue ? data.CombinePayer : false;
                EDITrans.TotalRequests = data.TotalRequests;
                o.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
        return errMsg;
    }

}

public class clsEDIAccount
{
    public int idEDIAccount { get; set; }
    public string AccountNumber { get; set; }

    public int idEDITranscation { get; set; }
    public int idRequest { get; set; }
    public int idEDITranscationType { get; set; }
    public string EDITranscationType { get; set; }

    public string Category { get; set; }

    public string CreatedBy { get; set; }

    public System.Nullable<System.DateTime> CreatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public System.Nullable<System.DateTime> UpdatedOn { get; set; }

    public System.Nullable<bool> ActiveFlag { get; set; }
}
public static class SrvEDIAccount
{
    public static List<clsEDIAccount> GetEDIAccountByidRequest(int idRequest)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDIAccount> qShipMeth = o.GetTable<tblEDIAccount>()
                                            .Where(p => p.idRequest == idRequest )
                                            .Select(p => new clsEDIAccount() { idEDIAccount = p.idEDIAccount, idEDITranscation = p.idEDITranscation,AccountNumber = p.AccountNumber ,idRequest = p.idRequest, EDITranscationType = p.EDITranscationType, Category = p.Category, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qShipMeth;
    }
    public static List<clsEDIAccount> GetEDIAccountByidRequest(int idRequest, int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        clsEDITransactionType TransType = SrvEDITransactionType.GetEDITransactionTypes(idEDITranscationType).FirstOrDefault();
        clsEDITransaction Trans = SrvEDITransaction.GetEDITransactionsByidRequest(idRequest, idEDITranscationType).FirstOrDefault();
        List<clsEDIAccount> qShipMeth = null;
        if (Trans != null)
        {
            qShipMeth = o.GetTable<tblEDIAccount>()
                               .Where(p => p.idEDITranscation == Trans.idEDITranscation)
                               .Select(p => new clsEDIAccount() { idEDIAccount = p.idEDIAccount, idEDITranscation = p.idEDITranscation, AccountNumber = p.AccountNumber, idRequest = p.idRequest, EDITranscationType = p.EDITranscationType, Category = p.Category, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                               .ToList();
        }

        return qShipMeth;
    }
    public static clsEDITransaction GetAEDITransactionsByidRequest(int idRequest, int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        clsEDITransaction qShipMeth = null;
        qShipMeth = o.GetTable<tblEDITranscation>()
                                            .Where(p => p.idRequest == idRequest && p.idEDITranscationType == idEDITranscationType && p.ActiveFlag == true)
                                            .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .FirstOrDefault();

        return qShipMeth;
    }
    public static string Remove(int idEDIAccount)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            var q = o.GetTable<tblEDIAccount>().Where(f => f.idEDIAccount == idEDIAccount).FirstOrDefault();
            o.GetTable<tblEDIAccount>().DeleteOnSubmit(q);
            o.SubmitChanges();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
        return errMsg;
    }
    //public static string Remove(int idRequest, int idEDITranscationType)
    //{
    //    string errMsg = "";
    //    PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
    //    try
    //    {
    //        var q = o.GetTable<tblEDITranscation>().Where(f => f.idRequest == idRequest && f.idEDITranscationType == idEDITranscationType).FirstOrDefault();
    //        o.GetTable<tblEDITranscation>().DeleteOnSubmit(q);
    //        o.SubmitChanges();
    //    }
    //    catch (Exception ex)
    //    {
    //        errMsg = ex.Message.ToString();
    //    }
    //    return errMsg;
    //}
    //public static string Remove(clsEDITransaction data)
    //{
    //    string errMsg = "";
    //    PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
    //    try
    //    {
    //        var q = o.GetTable<tblEDITranscation>().Where(f => f.idRequest == data.idRequest && f.idEDITranscationType == data.idEDITranscationType).FirstOrDefault();
    //        o.GetTable<tblEDITranscation>().DeleteOnSubmit(q);
    //        o.SubmitChanges();
    //    }
    //    catch (Exception ex)
    //    {
    //        errMsg = ex.Message.ToString();
    //    }
    //    return errMsg;
    //}

    public static string Insert(clsEDIAccount data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        newID = -1;
        try
        {
            List<clsEDIAccount> EDITrans = GetEDIAccountByidRequest(data.idRequest);
            clsEDITransactionType TransType = SrvEDITransactionType.GetEDITransactionTypes(data.idEDITranscationType).FirstOrDefault();
            clsEDITransaction Trans = SrvEDITransaction.GetEDITransactionsByidRequest(data.idRequest, data.idEDITranscationType).FirstOrDefault();
            tblEDIAccount oNewRow = new tblEDIAccount()
            {
                AccountNumber = data.AccountNumber,
                idEDITranscation = Trans.idEDITranscation,
                Category = (String.IsNullOrEmpty(TransType.Category)) ? "nada" : TransType.Category,
                EDITranscationType = (String.IsNullOrEmpty(TransType.EDITranscationType)) ? "nada" : TransType.EDITranscationType,
                idRequest = data.idRequest,
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn,
                ActiveFlag = true
            };
            o.GetTable<tblEDIAccount>().InsertOnSubmit(oNewRow);
            o.SubmitChanges();
            newID = oNewRow.idEDITranscation;
            data.idRequest = newID;
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
        return errMsg;
    }

}
public class clsEDIRecipReq
{
    public int idEDIRecipReqs{ get; set; }

    public int idFileType{ get; set; }

    public string X12_ISA{ get; set; }

    public string X12_GS{ get; set; }

    public string X12_Qualifier{ get; set; }

    public int idCommunicationMethod{ get; set; }

    public string FTPAddress{ get; set; }

    public string UserName{ get; set; }

    public string Password{ get; set; }

    public string FolderPath{ get; set; }

    public string Email{ get; set; }

    public int idTriggerMechanism{ get; set; }

    public int idTiming{ get; set; }

    public System.Nullable<System.DateTime> TimeOfFile{ get; set; }

    public int idStatusCodes{ get; set; }

    public int idEDITranscation { get; set; }
    public int idRequest { get; set; }
    public int idEDITranscationType { get; set; }
    public string EDITranscationType { get; set; }

    public string CreatedBy { get; set; }

    public System.Nullable<System.DateTime> CreatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public System.Nullable<System.DateTime> UpdatedOn { get; set; }

    public System.Nullable<bool> ActiveFlag { get; set; }
}
public static class SrvEDIRecipReq
{
    public static string Insert(clsEDIRecipReq data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        newID = -1;
        try
        {
            //List<clsEDIAccount> EDITrans = GetEDIAccountByidRequest(data.idRequest);
            clsEDITransactionType TransType = SrvEDITransactionType.GetEDITransactionTypes(data.idEDITranscationType).FirstOrDefault();
            clsEDITransaction Trans = SrvEDITransaction.GetEDITransactionsByidRequest(data.idRequest, data.idEDITranscationType).FirstOrDefault();
            clsEDIRecipReq oNewRow = new clsEDIRecipReq()
            {
                idFileType = data.idFileType,
                idEDITranscation = Trans.idEDITranscation,
                X12_ISA = data.X12_ISA,
                X12_GS = data.X12_GS,
                X12_Qualifier = data.X12_Qualifier,
                idCommunicationMethod = data.idCommunicationMethod,
                FTPAddress = data.FTPAddress,
                UserName = data.UserName,
                Password = data.Password,
                FolderPath = data.FolderPath,
                Email = data.Email,
                idTriggerMechanism = data.idTriggerMechanism,
                idTiming = data.idTiming,
                TimeOfFile = data.TimeOfFile,
                idStatusCodes = data.idStatusCodes,
                EDITranscationType = (String.IsNullOrEmpty(TransType.EDITranscationType)) ? "nada" : TransType.EDITranscationType,
                idRequest = data.idRequest,
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn,
                ActiveFlag = true
            };
            o.GetTable<clsEDIRecipReq>().InsertOnSubmit(oNewRow);
            o.SubmitChanges();
            newID = oNewRow.idEDITranscation;
            data.idRequest = newID;
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
        return errMsg;
    }

}
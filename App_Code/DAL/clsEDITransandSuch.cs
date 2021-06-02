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
    public string SFTPFolder { get; set; }
    public Nullable<bool> TestEnvironment { get; set; }
    public int TestSentMethod { get; set; }

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
                                            .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, TotalRequests = p.TotalRequests, BatchInvoices = p.BatchInvoices, CombinePayer = p.CombinePayer , SFTPFolder = p.SFTPFolder, TestEnvironment = p.TestEnvironment, TestSentMethod = p.TestSentMethod, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qShipMeth;
    }
    public static List<clsEDITransaction> GetEDITransactionsByidRequest(int idRequest, int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDITransaction> qShipMeth = o.GetTable<tblEDITranscation>()
                                            .Where(p => p.idRequest == idRequest && p.idEDITranscationType == idEDITranscationType && p.ActiveFlag == true)
                                            .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, TotalRequests = p.TotalRequests, BatchInvoices = p.BatchInvoices.HasValue ? p.BatchInvoices:false, CombinePayer = p.CombinePayer.HasValue ? p.CombinePayer : false, SFTPFolder = p.SFTPFolder, TestEnvironment = p.TestEnvironment, TestSentMethod = p.TestSentMethod, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qShipMeth;
    }
    public static clsEDITransaction GetAEDITransactionsByidRequest(int idRequest, int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        clsEDITransaction qShipMeth = null;
        try
        {
            qShipMeth = o.GetTable<tblEDITranscation>()
                                .Where(p => p.idRequest == idRequest && p.idEDITranscationType == idEDITranscationType && p.ActiveFlag == true)
                                .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, TotalRequests = p.TotalRequests, BatchInvoices = p.BatchInvoices.HasValue ? p.BatchInvoices : false, CombinePayer = p.CombinePayer.HasValue ? p.CombinePayer : false, SFTPFolder = p.SFTPFolder, TestEnvironment = p.TestEnvironment, TestSentMethod = p.TestSentMethod, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                .FirstOrDefault();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
        return qShipMeth;
    }
    public static clsEDITransaction GetAnyEDITransactionsByidRequest(int idRequest, int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        clsEDITransaction qShipMeth = null;
        qShipMeth = o.GetTable<tblEDITranscation>()
                                            .Where(p => p.idRequest == idRequest && p.idEDITranscationType == idEDITranscationType )
                                            .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, TotalRequests = p.TotalRequests, BatchInvoices = p.BatchInvoices.HasValue ? p.BatchInvoices : false, CombinePayer = p.CombinePayer.HasValue ? p.CombinePayer : false, SFTPFolder = p.SFTPFolder, TestEnvironment = p.TestEnvironment, TestSentMethod = p.TestSentMethod, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
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
            if (q != null)
            {
                q.ActiveFlag = false;
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
                    SFTPFolder = data.SFTPFolder,
                    TestEnvironment = data.TestEnvironment,
                    TestSentMethod = data.TestSentMethod,
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
                EDITrans.TotalRequests = data.TotalRequests;
                EDITrans.idEDITranscationType = data.idEDITranscationType;
                EDITrans.idRequest = data.idRequest;
                EDITrans.ActiveFlag = true;
                EDITrans.BatchInvoices = data.BatchInvoices.HasValue ? data.BatchInvoices : false;
                EDITrans.CombinePayer = data.CombinePayer.HasValue ? data.CombinePayer : false;
                EDITrans.TotalRequests = data.TotalRequests;
                EDITrans.TestEnvironment = data.TestEnvironment;
                EDITrans.TestSentMethod = data.TestSentMethod;
                EDITrans.SFTPFolder = data.SFTPFolder;
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
    public static string Insert(List<clsEDITransaction> TransList, Int32 ID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            foreach(clsEDITransaction edi in TransList)
            {
                tblEDITranscation EDITrans = o.GetTable<tblEDITranscation>()
                                    .Where(p => p.idRequest == ID && p.idEDITranscationType == edi.idEDITranscationType)
                                    .FirstOrDefault();
                if (EDITrans == null)
                {
                    tblEDITranscation oNewRow = new tblEDITranscation()
                    {
                        idEDITranscationType = edi.idEDITranscationType,
                        TotalRequests = 0,
                        BatchInvoices =  false,
                        CombinePayer = false,
                        idRequest = ID,
                        CreatedBy = edi.CreatedBy,
                        CreatedOn = edi.CreatedOn,
                        SFTPFolder = "",
                        TestEnvironment = false,
                        TestSentMethod = 0,
                        ActiveFlag = true
                    };
                    o.GetTable<tblEDITranscation>().InsertOnSubmit(oNewRow);
                }
                else
                {
                    EDITrans.idEDITranscationType = edi.idEDITranscationType;
                    EDITrans.ActiveFlag = true;
                }
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

    public static string Insert(clsEDITransaction data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
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
                    TestEnvironment = data.TestEnvironment,
                    TestSentMethod = data.TestSentMethod,
                    SFTPFolder = data.SFTPFolder,
                    ActiveFlag = true
                };
                o.GetTable<tblEDITranscation>().InsertOnSubmit(oNewRow);
                o.SubmitChanges();
            }
            else
            {
                EDITrans.TotalRequests = data.TotalRequests;
                EDITrans.idEDITranscationType = data.idEDITranscationType;
                EDITrans.idRequest = data.idRequest;
                EDITrans.ActiveFlag = true;
                EDITrans.BatchInvoices = data.BatchInvoices.HasValue ? data.BatchInvoices : false;
                EDITrans.CombinePayer = data.CombinePayer.HasValue ? data.CombinePayer : false;
                EDITrans.TotalRequests = data.TotalRequests;
                EDITrans.TestEnvironment = data.TestEnvironment;
                EDITrans.TestSentMethod = data.TestSentMethod;
                EDITrans.SFTPFolder = data.SFTPFolder;
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
    public int RecipReqNum { get; set; }
    public string PanelTitle { get; set; }

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
    public class PassBack
    {
        public int idEDIRecipReqs { get; set; }
        public int idFileType { get; set; }
        public int idCommunicationMethod { get; set; }
        public bool bNewRecord { get; set; }
    }
    public static clsEDIRecipReq GetEDIRecipReqsByID(int idEDIRecipReqs)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        clsEDIRecipReq qEDIRecipReq = o.GetTable<tblEDIRecipReq>()
                            .Where(p => p.idEDIRecipReqs == idEDIRecipReqs)
                            .Select(p => new clsEDIRecipReq() { idEDIRecipReqs = p.idEDIRecipReqs, RecipReqNum = p.RecipReqNum, PanelTitle = p.PanelTitle, idFileType = p.idFileType, X12_GS = p.X12_GS, X12_ISA = p.X12_ISA, X12_Qualifier = p.X12_Qualifier, idCommunicationMethod = p.idCommunicationMethod, FTPAddress = p.FTPAddress, UserName = p.UserName, Password = p.Password, FolderPath = p.FolderPath, Email = p.Email, idTriggerMechanism = p.idTriggerMechanism, idTiming = p.idTiming, TimeOfFile = p.TimeOfFile, idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, idEDITranscationType = p.tblEDITranscation.idEDITranscationType, EDITranscationType = p.EDITranscationType, idStatusCodes = p.idStatusCodes, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                            .FirstOrDefault();
        return qEDIRecipReq;
    }
    public static List<int> GetEDIRecipReqsList(int idEDITranscation)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<int> EDIRecipReqs = o.GetTable<tblEDIRecipReq>()
                            .Where(p => p.idEDITranscation == idEDITranscation)
                            .Select(p => p.idEDIRecipReqs )
                            .ToList();
        return EDIRecipReqs;
    }
    public static List<PassBack> GetEDIRecipReqsList2(int idEDITranscation)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<PassBack> EDIRecipReqs = o.GetTable<tblEDIRecipReq>()
                            .Where(p => p.idEDITranscation == idEDITranscation)
                            .Select(p => new PassBack() { idEDIRecipReqs = p.idEDIRecipReqs, bNewRecord = (p.idCommunicationMethod == 0 && p.idFileType == 0) ? true:false, idFileType = p.idFileType, idCommunicationMethod = p.idCommunicationMethod } )
                            .ToList();
        return EDIRecipReqs;
    }
    public static List<clsEDIRecipReq> GetEDIRecipReqs( int idEDITranscation)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDIRecipReq> qEDIRecipReq = o.GetTable<tblEDIRecipReq>()
                            .Where(p => p.ActiveFlag == true && p.idEDITranscation == idEDITranscation)
                            .Select(p => new clsEDIRecipReq() { idEDIRecipReqs = p.idEDIRecipReqs, RecipReqNum = p.RecipReqNum, PanelTitle = p.PanelTitle, idFileType = p.idFileType,X12_GS = p.X12_GS,X12_ISA = p.X12_ISA,X12_Qualifier = p.X12_Qualifier,idCommunicationMethod = p.idCommunicationMethod,FTPAddress = p.FTPAddress,UserName = p.UserName,Password = p.Password,FolderPath = p.FolderPath,Email = p.Email,idTriggerMechanism = p.idTriggerMechanism,idTiming = p.idTiming, TimeOfFile = p.TimeOfFile,idEDITranscation = p.idEDITranscation,idRequest = p.idRequest, idEDITranscationType = p.tblEDITranscation.idEDITranscationType,EDITranscationType = p.EDITranscationType ,idStatusCodes = p.idStatusCodes, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                            .ToList();
        return qEDIRecipReq;
    }
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
            var qRec = o.GetTable<tblEDIRecipReq>().Where(p => p.idEDIRecipReqs == data.idEDIRecipReqs).FirstOrDefault();
            if (qRec == null)
            {
                tblEDIRecipReq oNewRow = new tblEDIRecipReq()
                {
                    idFileType = data.idFileType,
                    RecipReqNum = data.RecipReqNum,
                    PanelTitle = data.PanelTitle,
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
                    Category = TransType.Category,
                    idRequest = data.idRequest,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = data.CreatedOn,
                    ActiveFlag = true
                };
                o.GetTable<tblEDIRecipReq>().InsertOnSubmit(oNewRow);
                o.SubmitChanges();
                newID = oNewRow.idEDITranscation;
                data.idRequest = newID;
            }
            else
            {
                qRec.idFileType = data.idFileType;
                qRec.UpdatedBy = data.UpdatedBy;
                qRec.UpdatedOn = DateTime.Now;
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
    public static string Insert(clsEDIRecipReq data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            //List<clsEDIAccount> EDITrans = GetEDIAccountByidRequest(data.idRequest);
            clsEDITransactionType TransType = SrvEDITransactionType.GetEDITransactionTypes(data.idEDITranscationType).FirstOrDefault();
            clsEDITransaction Trans = SrvEDITransaction.GetEDITransactionsByidRequest(data.idRequest, data.idEDITranscationType).FirstOrDefault();
            var qRec = o.GetTable<tblEDIRecipReq>().Where(p => p.idEDIRecipReqs == data.idEDIRecipReqs).FirstOrDefault();
            if (qRec == null)
            {
                tblEDIRecipReq oNewRow = new tblEDIRecipReq()
                {
                    idFileType = data.idFileType,
                    RecipReqNum = data.RecipReqNum,
                    PanelTitle = data.PanelTitle,
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
                    Category = TransType.Category,
                    idRequest = data.idRequest,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = data.CreatedOn,
                    ActiveFlag = true
                };
                o.GetTable<tblEDIRecipReq>().InsertOnSubmit(oNewRow);
                o.SubmitChanges();
            }
            else
            {
                qRec.idFileType = data.idFileType;
                qRec.idCommunicationMethod = data.idCommunicationMethod;
                qRec.PanelTitle = data.PanelTitle;
                qRec.X12_ISA = data.X12_ISA;
                qRec.X12_GS = data.X12_GS;
                qRec.X12_Qualifier = data.X12_Qualifier;
                qRec.FTPAddress = data.FTPAddress;
                qRec.UserName = data.UserName;
                qRec.Password = data.Password;
                qRec.FolderPath = data.FolderPath;
                qRec.Email = data.Email;
                qRec.idTriggerMechanism = data.idTriggerMechanism;
                qRec.idTiming = data.idTiming;
                qRec.idStatusCodes = data.idStatusCodes;
                qRec.TimeOfFile = data.TimeOfFile;
                qRec.UpdatedBy = data.UpdatedBy;
                qRec.UpdatedOn = DateTime.Now;
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

    public static string Remove(int idEDIRecipReqs)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        try
        {
            var q = puroTouchContext.GetTable<tblEDIRecipReq>().Where(f => f.idEDIRecipReqs == idEDIRecipReqs).FirstOrDefault();
            puroTouchContext.GetTable<tblEDIRecipReq>().DeleteOnSubmit(q);
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

}
public class clsTriggerMechanism
{
    public int idTriggerMechanism { get; set; }
    public string TriggerMechanism { get; set; }

    public string CreatedBy { get; set; }

    public System.Nullable<System.DateTime> CreatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public System.Nullable<System.DateTime> UpdatedOn { get; set; }

    public System.Nullable<bool> ActiveFlag { get; set; }
}
public static class SrvTriggerMechanism
{
    public static List<clsTriggerMechanism> GetTriggerMechanisms()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsTriggerMechanism> qTrigMeth = o.GetTable<tblTriggerMechanism>()
                                        .Where(p => p.ActiveFlag == true)
                                        .Select(p => new clsTriggerMechanism() { idTriggerMechanism = p.idTriggerMechanism, TriggerMechanism = p.TriggerMechanism, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                         .ToList();
        return qTrigMeth;
    }
}
public class clsTiming
{
    public int idTiming { get; set; }
    public string Timing { get; set; }

    public string CreatedBy { get; set; }

    public System.Nullable<System.DateTime> CreatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public System.Nullable<System.DateTime> UpdatedOn { get; set; }

    public System.Nullable<bool> ActiveFlag { get; set; }
}
public static class SrvTiming
{
    public static List<clsTiming> GetTiming()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsTiming> qTiming = o.GetTable<tblTiming>()
                            .Where(p=> p.ActiveFlag == true)
                            .Select(p => new clsTiming() { idTiming = p.idTiming, Timing = p.Timing, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                            .ToList();
        return qTiming;
    }
}
public class clsStatusCode
{
    public int idStatusCodes { get; set; }
    public string StatusCode { get; set; }

    public string CreatedBy { get; set; }

    public System.Nullable<System.DateTime> CreatedOn { get; set; }

    public string UpdatedBy { get; set; }

    public System.Nullable<System.DateTime> UpdatedOn { get; set; }

    public System.Nullable<bool> ActiveFlag { get; set; }
}
public static class SrvStatusCode
{
    public static List<clsStatusCode> GetStatusCodes()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsStatusCode> qStatusCode = o.GetTable<tblStatusCode>()
                            .Where(p => p.ActiveFlag == true)
                            .Select(p => new clsStatusCode() { idStatusCodes = p.idStatusCodes, StatusCode = p.StatusCode, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                            .ToList();
        return qStatusCode;
    }
}
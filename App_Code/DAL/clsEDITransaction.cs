using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for clsEDITransaction
/// </summary>
public class clsEDITransaction
{
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
public static class SrvEDITransaction
{
    public static List<clsEDITransaction> GetEDITransactionsByidRequest(int idRequest)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDITransaction> qShipMeth = o.GetTable<tblEDITranscation>()
                                            .Where(p => p.idRequest == idRequest)
                                            .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qShipMeth;
    }
    private static List<clsEDITransaction> GetEDITransactionsByidRequest(int idRequest, int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDITransaction> qShipMeth = o.GetTable<tblEDITranscation>()
                                            .Where(p => p.idRequest == idRequest && p.idEDITranscationType == idEDITranscationType)
                                            .Select(p => new clsEDITransaction() { idEDITranscation = p.idEDITranscation, idRequest = p.idRequest, EDITranscationType = p.tblEDITranscationType.EDITranscationType, idEDITranscationType = p.idEDITranscationType, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

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
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
    public static string Insert(clsEDITransaction data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;
        try
        {
            List<clsEDITransaction> EDITrans = GetEDITransactionsByidRequest(data.idRequest, data.idEDITranscationType);
            if (EDITrans.Count() < 1)
            {
                tblEDITranscation oNewRow = new tblEDITranscation()
                {
                    idEDITranscationType = data.idEDITranscationType,
                    idRequest = data.idRequest,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = data.CreatedOn,
                    ActiveFlag = true
                };
                puroTouchContext.GetTable<tblEDITranscation>().InsertOnSubmit(oNewRow);
                puroTouchContext.SubmitChanges();
                newID = oNewRow.idEDITranscation;
                data.idRequest = newID;
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

}
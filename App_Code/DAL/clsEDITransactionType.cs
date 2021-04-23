using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;


/// <summary>
/// Summary description for clsEDITransactionType
/// </summary>
public class clsEDITransactionType
{
	public int idEDITranscationType { get; set; }

    public string EDITranscationType { get; set; }
    public int CategoryID { get; set; }

    public string Category { get; set; }

    public string CreatedBy { get; set; }

	public System.Nullable<System.DateTime> CreatedOn { get; set; }

	public string UpdatedBy { get; set; }

	public System.Nullable<System.DateTime> UpdatedOn { get; set; }

	public System.Nullable<bool> ActiveFlag { get; set; }
}
public static class SrvEDITransactionType
{
    public static List<clsEDITransactionType> GetEDITransactionTypes()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDITransactionType> qShipMeth = o.GetTable<tblEDITranscationType>()
                                            .Select(p => new clsEDITransactionType() { idEDITranscationType = p.idEDITranscationType, EDITranscationType = p.EDITranscationType, Category = p.Category, CategoryID = p.CategoryID, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .Where(f=> f.CategoryID == 0 && f.ActiveFlag == true)
                                            .ToList();
        return qShipMeth;
    }
    public static List<clsEDITransactionType> GetEDITransactionTypes(int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDITransactionType> qShipMeth = o.GetTable<tblEDITranscationType>()
                                            .Select(p => new clsEDITransactionType() { idEDITranscationType = p.idEDITranscationType, EDITranscationType = p.EDITranscationType, Category = p.Category, CategoryID = p.CategoryID, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .Where(f => f.idEDITranscationType == idEDITranscationType)
                                            .ToList();
        return qShipMeth;
    }
    public static clsEDITransactionType GetOneEDITransactionType(int idEDITranscationType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        clsEDITransactionType qEDITransType = o.GetTable<tblEDITranscationType>()
                                            .Select(p => new clsEDITransactionType() { idEDITranscationType = p.idEDITranscationType, EDITranscationType = p.EDITranscationType, Category = p.Category, CategoryID = p.CategoryID, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .Where(f => f.idEDITranscationType == idEDITranscationType)
                                            .FirstOrDefault();
        return qEDITransType;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

public class clsEDIShipMethodType
{
    public int idEDIShipMethod { get; set; }

    public string MethodType { get; set; }

    public string UpdatedBy { get; set; }

    public System.Nullable<System.DateTime> UpdatedOn { get; set; }

    public string CreatedBy { get; set; }

    public System.Nullable<System.DateTime> CreatedOn { get; set; }

    public System.Nullable<bool> ActiveFlag { get; set; }
}
public static class SrvEDIShipMethodType
{
    public static List<clsEDIShipMethodType> GetEDIShipMethodTypes()
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        List<clsEDIShipMethodType> oShipMeth = (from data in puroTouchContext.GetTable<tblEDIShipMethodType>()
                                   select new clsEDIShipMethodType
                                   {
                                       idEDIShipMethod = data.idEDIShipMethodTypes,
                                       MethodType = data.MethodType,
                                       UpdatedBy = data.UpdatedBy,
                                       UpdatedOn = data.UpdatedOn,
                                       CreatedBy = data.CreatedBy,
                                       CreatedOn = data.CreatedOn,
                                       ActiveFlag = data.ActiveFlag
                                   }).ToList<clsEDIShipMethodType>();
        return oShipMeth;
    }
}
public class ClsEDIOnboardingPhase
{
    public int idEDIOnboardingPhase { get; set; }
    public string EDIOnboardingPhaseType { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public int? SortValue { get; set; }
}
public static class SrvEDIOnboardingPhase
{
    public static List<ClsEDIOnboardingPhase> GetEDIOnboardingPhase()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<ClsEDIOnboardingPhase> qEDISpecialisth = o.GetTable<tblEDIOnboardingPhase>()
                                            .Where(p=>p.idEDIOnboardingPhase != 0)
                                            .Select(p => new ClsEDIOnboardingPhase() { idEDIOnboardingPhase = p.idEDIOnboardingPhase, EDIOnboardingPhaseType = p.EDIOnboardingPhaseType, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();
        return qEDISpecialisth;
    }
    public static string InsertEDIOnboardingPhase(ClsEDIOnboardingPhase data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            tblEDIOnboardingPhase oNewRow = new tblEDIOnboardingPhase()
            {
                EDIOnboardingPhaseType = data.EDIOnboardingPhaseType,
                CreatedBy = data.CreatedBy,
                CreatedOn = DateTime.Now,
                ActiveFlag = data.ActiveFlag
            };

            o.GetTable<tblEDIOnboardingPhase>().InsertOnSubmit(oNewRow);
            o.SubmitChanges();
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
    public static string UpdateEDIOnboardingPhase(ClsEDIOnboardingPhase data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            if (data.idEDIOnboardingPhase > 0)
            {
                var query = from qdata in o.GetTable<tblEDIOnboardingPhase>()
                            where qdata.idEDIOnboardingPhase == data.idEDIOnboardingPhase
                            select qdata;

                foreach (tblEDIOnboardingPhase updRow in query)
                {
                    updRow.idEDIOnboardingPhase = data.idEDIOnboardingPhase;
                    updRow.EDIOnboardingPhaseType = data.EDIOnboardingPhaseType;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = DateTime.Now;
                }
                o.SubmitChanges();
            }
            else
            {
                errMsg = "There is No Shipping Product with ID = " + "'" + data.idEDIOnboardingPhase + "'";
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
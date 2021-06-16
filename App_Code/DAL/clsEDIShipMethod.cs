using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
/// <summary>
/// Summary description for clsEDIShipMethod
/// </summary>
public class clsEDIShipMethod
{
    public int idEDIShipMethod { get; set; }

    public int idRequest { get; set; }

    public int idEDIShipMethodType { get; set; }
    public string MethodType { get; set; }

    public string UpdatedBy { get; set; }

    public System.Nullable<System.DateTime> UpdatedOn { get; set; }

    public string CreatedBy { get; set; }

    public System.Nullable<System.DateTime> CreatedOn { get; set; }

    public System.Nullable<bool> ActiveFlag { get; set; }
}
public static class SrvEDIShipMethod
{
    public static List<clsEDIShipMethod> GetEDIShipMethodTypes()
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        List<clsEDIShipMethod> oShipMeth = (from data in puroTouchContext.GetTable<tblEDIShipMethod>()
                                                select new clsEDIShipMethod
                                                {
                                                    idEDIShipMethod = data.idEDIShipMethod,
                                                    idEDIShipMethodType = data.idEDIShipMethodType,
                                                    MethodType = data.tblEDIShipMethodType.MethodType,
                                                    idRequest = data.idRequest,
                                                    UpdatedBy = data.UpdatedBy,
                                                    UpdatedOn = data.UpdatedOn,
                                                    CreatedBy = data.CreatedBy,
                                                    CreatedOn = data.CreatedOn,
                                                    ActiveFlag = data.ActiveFlag
                                                }).ToList<clsEDIShipMethod>();
        return oShipMeth;
    }
    public static List<clsEDIShipMethod> GetEDIShipMethodTypesByidRequest(int idRequest)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDIShipMethod> qShipMeth = o.GetTable<tblEDIShipMethod>()
                                            .Where(p => p.idRequest == idRequest)
                                            .Select(p => new clsEDIShipMethod() { idEDIShipMethod = p.idEDIShipMethod, idRequest = p.idRequest, MethodType = p.tblEDIShipMethodType.MethodType ,idEDIShipMethodType = p.idEDIShipMethodType, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qShipMeth;
    }
    public static List<clsEDIShipMethod> GetEDIShipMethodMockData()
    {
        List<clsEDIShipMethod> qShipMeth = new List<clsEDIShipMethod>() 
        {
            new clsEDIShipMethod() { MethodType = "Courier", idEDIShipMethodType = 1, ActiveFlag = true, CreatedBy = "scott.cardinale", CreatedOn = DateTime.Now },
            new clsEDIShipMethod() { MethodType = "PuroPost", idEDIShipMethodType = 2, ActiveFlag = true, CreatedBy = "scott.cardinale", CreatedOn = DateTime.Now },
            new clsEDIShipMethod() { MethodType = "Freight", idEDIShipMethodType = 3, ActiveFlag = true, CreatedBy = "scott.cardinale", CreatedOn = DateTime.Now }
        };
        return qShipMeth;
    }
    public static List<clsEDIShipMethod> GetEDIShipMethodTypesByidRequest(int idRequest,int idEDIShipMethodTypes)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<clsEDIShipMethod> qShipMeth = o.GetTable<tblEDIShipMethod>()
                                            .Where(p => p.idRequest == idRequest && p.idEDIShipMethod == idEDIShipMethodTypes)
                                            .Select(p => new clsEDIShipMethod() { idEDIShipMethod = p.idEDIShipMethod, idRequest = p.idRequest, MethodType = p.tblEDIShipMethodType.MethodType, idEDIShipMethodType = p.idEDIShipMethodType, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qShipMeth;
    }
    public static string Insert(clsEDIShipMethod data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;
        try
        {
            List<clsEDIShipMethod> qShipMeth = GetEDIShipMethodTypesByidRequest(data.idRequest, data.idEDIShipMethodType);
            if (qShipMeth.Count() < 1)
            {
                tblEDIShipMethod oNewRow = new tblEDIShipMethod()
                {
                    idEDIShipMethodType = data.idEDIShipMethodType,
                    idRequest = data.idRequest,
                    ActiveFlag = data.ActiveFlag,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = data.CreatedOn
                };
                puroTouchContext.GetTable<tblEDIShipMethod>().InsertOnSubmit(oNewRow);
                puroTouchContext.SubmitChanges();
                newID = oNewRow.idEDIShipMethod;
                data.idRequest = newID;
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
    public static string Insert(List<clsEDIShipMethod> ShipList, Int32 ID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        try
        {
            foreach (clsEDIShipMethod edi in ShipList)
            {
                tblEDIShipMethod qShipMeth = o.GetTable<tblEDIShipMethod>().Where(p => p.idRequest == ID && p.idEDIShipMethodType == edi.idEDIShipMethodType).FirstOrDefault();
                if (qShipMeth != null)
                {
                    qShipMeth.UpdatedBy = edi.CreatedBy;
                    qShipMeth.UpdatedOn = DateTime.Now;
                    qShipMeth.ActiveFlag = true;
                }
                else
                {
                    tblEDIShipMethod oNewRow = new tblEDIShipMethod()
                    {
                        idEDIShipMethodType = edi.idEDIShipMethodType,
                        idRequest = ID,
                        ActiveFlag = true,
                        CreatedBy = edi.CreatedBy,
                        CreatedOn = edi.CreatedOn
                    };
                    o.GetTable<tblEDIShipMethod>().InsertOnSubmit(oNewRow);
                }
                o.SubmitChanges();
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public static string Remove(int idEDIShipMethod)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        try
        {
            var q = puroTouchContext.GetTable<tblEDIShipMethod>().Where(f => f.idEDIShipMethod == idEDIShipMethod).FirstOrDefault();
            puroTouchContext.GetTable<tblEDIShipMethod>().DeleteOnSubmit(q);
            puroTouchContext.SubmitChanges();
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
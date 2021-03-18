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
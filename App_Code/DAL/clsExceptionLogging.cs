using System;
using context = System.Web.HttpContext;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for clsExceptionLogging
/// </summary>
public class clsExceptionLogging
{
    public long Logid { get; set; }
    public string Method { get; set; }
    public string ExceptionMsg { get; set; }
    public string ExceptionType { get; set; }
    public string ExceptionSource { get; set; }
    public string ExceptionURL { get; set; }
    public string CreatedBy { get; set; }
    public System.Nullable<System.DateTime> CreatedOn { get; set; }
  
}
public static class SrvExceptionLogging
{
    public static string Insert(clsExceptionLogging data, out long newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        newID = -1;
        try
        {
            tblExceptionLogging oNewRow = new tblExceptionLogging()
            {
                ExceptionMsg = data.ExceptionMsg,
                Method = data.Method,
                ExceptionType = data.ExceptionType,
                ExceptionSource = data.ExceptionSource,
                ExceptionURL = data.ExceptionURL,
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn,
            };
            o.GetTable<tblExceptionLogging>().InsertOnSubmit(oNewRow);
            o.SubmitChanges();
            newID = oNewRow.Logid;
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
//using context = System.Web.HttpContext;

/// <summary>
/// Summary description for ClsFileType
/// </summary>
public class ClsFileType
{
    public int idFileType { get; set; }
    public string FileType { get; set; }
    public bool NonCourierEDI { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
}
public static class SrvFileType
{
    public static List<ClsFileType> GetFileTypes()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<ClsFileType> qFileTypes = o.GetTable<tblFileType>()
                                            .Where(p=> p.ActiveFlag == true)
                                            .Select(p => new ClsFileType() { idFileType = p.idFileType, FileType = p.FileType, NonCourierEDI = p.NonCourierEDI, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qFileTypes;
    }
    public static List<ClsFileType> GetFileTypes(bool bNonCourierEDI)
    {
        List<ClsFileType> qFileTypes = new List<ClsFileType>();
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        if (bNonCourierEDI)
        {
            qFileTypes = o.GetTable<tblFileType>()
                            .Where(p => p.ActiveFlag == true)
                            .Select(p => new ClsFileType() { idFileType = p.idFileType, FileType = p.FileType, NonCourierEDI = p.NonCourierEDI, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                            .ToList();
        }
        else
        {
            qFileTypes = o.GetTable<tblFileType>()
                            .Where(p => p.ActiveFlag == true && p.NonCourierEDI == false)
                            .Select(p => new ClsFileType() { idFileType = p.idFileType, FileType = p.FileType, NonCourierEDI = p.NonCourierEDI, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                            .ToList();
        }
        return qFileTypes;
    }
}
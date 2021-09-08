using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserControlParams
/// </summary>
public class UserControlParams
{
    public enum CourierType {Zero,CourierEDI,NonCourierEDI };
    public CourierType ct;// = CourierType.Zero;
    public bool bUseTimeOfFile { get; set; }

    public int iTotalRecs { get; set; }
    public int idRequest { get; set; }
    public int iRecordID { get; set; }
    public int idEDIRecipReqs { get; set; }
    public IList<int> EDIRecipReqs;
    public List<SrvEDIRecipReq.PassBack> passbacks { get; set; }
    public bool bNewDialog { get; set; }

    public UserControlParams()
    {
        iTotalRecs = 0;
        idEDIRecipReqs = 0;
        EDIRecipReqs = new List<int>();
        passbacks = new List<SrvEDIRecipReq.PassBack>();
        bNewDialog = false;
    }
    public UserControlParams(int iCount)
    {
        iTotalRecs = iCount;
        bNewDialog = false;
    }
   
    public UserControlParams(int iCount, int Request, CourierType ct, bool bUseTimeOfFile)
    {
        idRequest = Request;
        iTotalRecs = iCount;
        EDIRecipReqs = new List<int>();
        passbacks = new List<SrvEDIRecipReq.PassBack>();
        idEDIRecipReqs = 0;
        bNewDialog = false;
        this.ct = ct;
        this.bUseTimeOfFile = bUseTimeOfFile;
    }
    public bool CheckPassBacks(int idEDIRecipReqs, bool bNewDialog)
    {
        bool bRetVal = false;
        this.bNewDialog = bNewDialog;
        var q = passbacks.Where(p => p.idEDIRecipReqs == idEDIRecipReqs).FirstOrDefault();
        q.bNewRecord = bNewDialog;
        return bRetVal;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsProposedSvcs
/// </summary>
public class ClsDiscoveryRequestSvcs
{
    public int idRequestSvc { get; set; }
    public int idRequest { get; set; }
    public int idShippingSvc { get; set; }
    public string serviceDesc { get; set; }
    public int volume { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }

   

    public string InsertServices(ClsDiscoveryRequestSvcs data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;
       
        try
        {

            tblDiscoveryRequestService oNewRow = new tblDiscoveryRequestService()
            {
                idRequest = (Int32)data.idRequest,
                idShippingSvc = (Int32)data.idShippingSvc,
                volume = (Int32)data.volume,
                UpdatedBy = data.UpdatedBy,
                UpdatedOn = data.UpdatedOn,
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn                
            };



            puroTouchContext.GetTable<tblDiscoveryRequestService>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();
            newID = oNewRow.idRequestSvc;

        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string DeleteServices(int idRequest)
    {
        string errMsg="";
        try
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
  
            var deleteServices = from details in puroTouchContext.GetTable<tblDiscoveryRequestService>()
                                 where details.idRequest == idRequest
                                 select details;

            foreach (var idRequestSvc in deleteServices)
            {
                puroTouchContext.GetTable<tblDiscoveryRequestService>().DeleteOnSubmit(idRequestSvc);
            }


            try
            {
                puroTouchContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }    
            
        return errMsg;
    }
}
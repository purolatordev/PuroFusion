using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;


/// <summary>
/// Summary description for ClsDiscoveryRequestEDI
/// </summary>
public class ClsDiscoveryRequestEDI
{
    public int idDREDI { get; set; }
    public int idRequest { get; set; }
    public string Solution { get; set; }
    public string FileFormat { get; set; }
    public string CommunicationMethod { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }

    public string InsertEDI(ClsDiscoveryRequestEDI data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;

        try
        {

            tblDiscoveryRequestEDI oNewRow = new tblDiscoveryRequestEDI()
            {
                idRequest = (Int32)data.idRequest,
                Solution = data.Solution,
                FileFormat = data.FileFormat,
                CommunicationMethod = data.CommunicationMethod,
                UpdatedBy = data.UpdatedBy,
                UpdatedOn = data.UpdatedOn,
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn
            };



            puroTouchContext.GetTable<tblDiscoveryRequestEDI>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();
            newID = oNewRow.idDREDI;

        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string DeleteEDI(int idRequest)
    {
        string errMsg = "";
        try
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

            var deleteEDI = from details in puroTouchContext.GetTable<tblDiscoveryRequestEDI>()
                                  where details.idRequest == idRequest
                                  select details;

            foreach (var idRequestEDI in deleteEDI)
            {
                puroTouchContext.GetTable<tblDiscoveryRequestEDI>().DeleteOnSubmit(idRequestEDI);
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
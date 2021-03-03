using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsDiscoveryRequestEquip
/// </summary>
public class ClsDiscoveryRequestEquip
{
    public int idDREquipment { get; set; }
    public int idRequest { get; set; }
    public string EquipmentDesc { get; set; }
    public int number { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }

    public string InsertEquipment(ClsDiscoveryRequestEquip data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;

        try
        {

            tblDiscoveryRequestEquipment oNewRow = new tblDiscoveryRequestEquipment()
            {
                idRequest = (Int32)data.idRequest,
                EquipmentDesc = data.EquipmentDesc,
                number = (Int32)data.number,
                UpdatedBy = data.UpdatedBy,
                UpdatedOn = data.UpdatedOn,
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn
            };



            puroTouchContext.GetTable<tblDiscoveryRequestEquipment>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();
            newID = oNewRow.idDREquipment;

        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string DeleteEquipment(int idRequest)
    {
        string errMsg = "";
        try
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

            var deleteEquipment = from details in puroTouchContext.GetTable<tblDiscoveryRequestEquipment>()
                                 where details.idRequest == idRequest
                                 select details;

            foreach (var idRequestEquip in deleteEquipment)
            {
                puroTouchContext.GetTable<tblDiscoveryRequestEquipment>().DeleteOnSubmit(idRequestEquip);
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
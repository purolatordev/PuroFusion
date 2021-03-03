using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsDiscoveryRequestProds
/// </summary>
public class ClsDiscoveryRequestProds
{
    public int idDRProduct { get; set; }
    public int idRequest { get; set; }
    public int idShippingProduct { get; set; }
    public string productDesc { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }

    public string InsertProducts(ClsDiscoveryRequestProds data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;

        try
        {

            tblDiscoveryRequestProduct oNewRow = new tblDiscoveryRequestProduct()
            {
                idRequest = (Int32)data.idRequest,
                idShippingProduct = (Int32)data.idShippingProduct,
                UpdatedBy = data.UpdatedBy,
                UpdatedOn = data.UpdatedOn,
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn
            };



            puroTouchContext.GetTable<tblDiscoveryRequestProduct>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();
            newID = oNewRow.idDRProduct;

        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string DeleteProducts(int idRequest)
    {
        string errMsg = "";
        try
        {
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

            var deleteProducts = from details in puroTouchContext.GetTable<tblDiscoveryRequestProduct>()
                                 where details.idRequest == idRequest
                                 select details;

            foreach (var idRequestProd in deleteProducts)
            {
                puroTouchContext.GetTable<tblDiscoveryRequestProduct>().DeleteOnSubmit(idRequestProd);
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
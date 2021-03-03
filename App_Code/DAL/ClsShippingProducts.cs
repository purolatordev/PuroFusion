using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsShippingProducts
/// </summary>
public class ClsShippingProducts
{
    public int idShippingProduct { get; set; }
    public int idShippingSvc { get; set; }
    public string ShippingProduct { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public string ShippingService { get; set; }

    public string InsertShippingProduct(ClsShippingProducts data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblShippingProduct oNewRow = new tblShippingProduct()
            {
                ShippingProduct = data.ShippingProduct,
                idShippingSvc = data.idShippingSvc,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag
            };



            puroTouchContext.GetTable<tblShippingProduct>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();
            //newID = oNewRow.idTaskType;

        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string UpdateShippingProduct(ClsShippingProducts data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idShippingSvc > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblShippingProduct>()
                    where qdata.idShippingProduct == data.idShippingProduct
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblShippingProduct updRow in query)
                {

                    updRow.ShippingProduct = data.ShippingProduct;
                    updRow.idShippingSvc = data.idShippingSvc;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idShippingProduct = data.idShippingProduct;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Shipping Product with ID = " + "'" + data.idShippingProduct + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
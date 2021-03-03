using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsShippingChannel
/// </summary>
public class ClsShippingChannel
{
    public int idShippingChannel { get; set; }
    public string ShippingChannel { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }

    public string InsertShippingChannel(ClsShippingChannel data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblShippingChannel oNewRow = new tblShippingChannel()
            {
                //idTaskType = (Int32)data.idTaskType,
                ShippingChannel = data.ShippingChannel,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag
            };



            puroTouchContext.GetTable<tblShippingChannel>().InsertOnSubmit(oNewRow);
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

    public string UpdateShippingChannel(ClsShippingChannel data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idShippingChannel > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblShippingChannel>()
                    where qdata.idShippingChannel == data.idShippingChannel
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblShippingChannel updRow in query)
                {

                    updRow.ShippingChannel = data.ShippingChannel;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idShippingChannel = data.idShippingChannel;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Shipping Channel with ID = " + "'" + data.idShippingChannel + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
   
}
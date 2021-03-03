using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsBroker
/// </summary>
public class ClsBroker
{
    public int idBroker { get; set; }
    public string Broker { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }

    public string InsertBroker(ClsBroker data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblBroker oNewRow = new tblBroker()
            {
                idBroker = (Int32)data.idBroker,
                Broker = data.Broker,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag
            };



            puroTouchContext.GetTable<tblBroker>().InsertOnSubmit(oNewRow);
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

    public string UpdateBroker(ClsBroker data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idBroker > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblBroker>()
                    where qdata.idBroker == data.idBroker
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblBroker updRow in query)
                {

                    updRow.Broker = data.Broker;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idBroker = data.idBroker;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Broker with ID = " + "'" + data.idBroker + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
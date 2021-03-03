using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsDataEntryMethods
/// </summary>
public class ClsDataEntryMethods
{
    public Int16 idDataEntry { get; set; }
    public string DataEntry { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }

    public string InsertDataEntryMethod(ClsDataEntryMethods data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblDataEntryMethod oNewRow = new tblDataEntryMethod()
            {
                DataEntry = data.DataEntry,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag
            };



            puroTouchContext.GetTable<tblDataEntryMethod>().InsertOnSubmit(oNewRow);
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

    public string UpdateDataEntryMethod(ClsDataEntryMethods data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idDataEntry > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblDataEntryMethod>()
                    where qdata.idDataEntry == data.idDataEntry
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblDataEntryMethod updRow in query)
                {

                    updRow.DataEntry = data.DataEntry;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idDataEntry = data.idDataEntry;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Data Entry Method with ID = " + "'" + data.idDataEntry + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
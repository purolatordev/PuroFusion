using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsTargetDates
/// </summary>
public class ClsTargetDates
{

    public int idTargetDate { get; set; }
    public int idRequest { get; set; }
    public DateTime? TargetDate { get; set; }
    public string ChangeReason { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string CustomerName { get; set; }

    public string InsertTargetDate(ClsTargetDates data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblDiscoveryRequestTargetDate oNewRow = new tblDiscoveryRequestTargetDate()
            {
                idRequest = (Int32)data.idRequest,
                TargetDate = (DateTime)data.TargetDate,
                ChangeReason = data.ChangeReason,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn
            };


            puroTouchContext.GetTable<tblDiscoveryRequestTargetDate>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string UpdateChangeReason(ClsTargetDates data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idTargetDate > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblDiscoveryRequestTargetDate>()
                    where qdata.idTargetDate == data.idTargetDate
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblDiscoveryRequestTargetDate updRow in query)
                {

                    updRow.ChangeReason = data.ChangeReason;
                    //updRow.TargetDate = data.TargetDate;
                    //updRow.idRequest = data.idRequest;
                    //updRow.CreatedBy = data.CreatedBy;
                    //updRow.CreatedOn = data.CreatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Row with ID = " + "'" + data.idTargetDate + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
	
}
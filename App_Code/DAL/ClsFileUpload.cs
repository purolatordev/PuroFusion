using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsFileUpload
/// </summary>
public class ClsFileUpload
{
    public int idFileUpload { get; set; }
    public int? idRequest { get; set; }
    public DateTime? UploadDate { get; set; }
    public string Description { get; set; }
    public string FilePath { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool ActiveFlag { get; set; }

    public ClsFileUpload GetFileUpload(int ID)
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        ClsFileUpload oNote = (from data in puroTouchContext.GetTable<tblDiscoveryRequestUpload>()
                          where data.idFileUpload == ID
                          where data.ActiveFlag != false
                               select new ClsFileUpload
                          {
                              idFileUpload = data.idFileUpload,
                              idRequest = data.idRequest,
                              Description = data.Description,
                              UploadDate = data.UploadDate,
                              FilePath = data.FilePath,
                              CreatedBy = data.CreatedBy,
                              UpdatedBy = data.UpdatedBy,
                              UpdatedOn = (DateTime?)data.UpdatedOn,
                              ActiveFlag = (bool)data.ActiveFlag
                          }).FirstOrDefault();
        return oNote;
    }

    public string InsertFileUpload(ClsFileUpload data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;
        try
        {
            tblDiscoveryRequestUpload oNewRow = new tblDiscoveryRequestUpload()
            {
                idRequest = (Int32)data.idRequest,
                UploadDate = data.UploadDate,
                Description = data.Description,
                FilePath = data.FilePath,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                
                ActiveFlag = data.ActiveFlag
            };
            if (oNewRow.idRequest != 0)
            {
                puroTouchContext.GetTable<tblDiscoveryRequestUpload>().InsertOnSubmit(oNewRow);
                puroTouchContext.SubmitChanges();
                newID = oNewRow.idFileUpload;
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string UpdateFileUpload(ClsFileUpload data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        try
        {
            if (data.idFileUpload > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblDiscoveryRequestUpload>()
                    where qdata.idFileUpload == data.idFileUpload
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblDiscoveryRequestUpload updRow in query)
                {
                    updRow.UploadDate = data.UploadDate;
                    updRow.Description = data.Description;
                    updRow.FilePath = data.FilePath;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;
                    updRow.ActiveFlag = data.ActiveFlag;
                }
                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();
            }
            else
            {
                errMsg = "There is No File Upload with ID = " + "'" + data.idFileUpload + "'";
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    //DELETE NOTE (set ActiveFlag to false)
    public string deActivateFileUpload(Int32 ID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        try
        {

            if (ID > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblDiscoveryRequestUpload>()
                    where qdata.idFileUpload == ID
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblDiscoveryRequestUpload updRow in query)
                {

                    updRow.ActiveFlag = false;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No UploadFile File Upload with idNote = " + "'" + ID + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
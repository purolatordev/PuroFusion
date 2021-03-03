using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;


/// <summary>
/// Summary description for ClsNotes
/// </summary>
public class ClsNotes
{
    public int idNote { get; set; }
    public int? idRequest { get; set; }
    public int? idTaskType { get; set; }
    public DateTime? noteDate { get; set; }
    public int? timeSpent { get; set; }
    public string publicNote { get; set; }
    public string privateNote { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string TaskType { get; set; }
    public bool ActiveFlag { get; set; }

   

    public ClsNotes GetNote(int noteID)
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        ClsNotes oNote = (from data in puroTouchContext.GetTable<tblDiscoveryRequestNote>()
                          join tt in puroTouchContext.GetTable<tblTaskType>() on data.idTaskType equals tt.idTaskType
                                where data.idNote == noteID
                                where data.ActiveFlag != false
                                select new ClsNotes
                                {
                                    idNote = data.idNote,
                                    idRequest = data.idRequest,
                                    idTaskType = data.idTaskType,
                                    TaskType = tt.TaskType,
                                    noteDate = data.noteDate,
                                    timeSpent = data.timeSpent,
                                    publicNote = data.publicNote,
                                    privateNote = data.privateNote,
                                    CreatedBy = data.CreatedBy

                                }).FirstOrDefault();
        return oNote;
    }

   

    public string InsertNote(ClsNotes data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;

        try
        {

            tblDiscoveryRequestNote oNewRow = new tblDiscoveryRequestNote()
            {
                idTaskType = (Int32)data.idTaskType,
                idRequest = (Int32)data.idRequest,
                noteDate = data.noteDate,
                timeSpent = data.timeSpent,
                publicNote = data.publicNote,
                privateNote = data.privateNote,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag
            };



            puroTouchContext.GetTable<tblDiscoveryRequestNote>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();
            newID = oNewRow.idNote;

        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string UpdateNote(ClsNotes data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idNote > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblDiscoveryRequestNote>()
                    where qdata.idNote == data.idNote
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblDiscoveryRequestNote updRow in query)
                {

                    updRow.noteDate = data.noteDate;
                    updRow.publicNote = data.publicNote;
                    updRow.idTaskType = data.idTaskType;
                    updRow.timeSpent = data.timeSpent;
                    updRow.privateNote = data.privateNote;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Discovery Request Note with idNote = " + "'" + data.idNote + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    //DELETE NOTE (set ActiveFlag to false)
    public string deActivateNote(Int32 noteID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        try
        {

            if (noteID > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblDiscoveryRequestNote>()
                    where qdata.idNote == noteID
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblDiscoveryRequestNote updRow in query)
                {

                    updRow.ActiveFlag = false;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Discovery Request Note with idNote = " + "'" + noteID + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsContactType
/// </summary>
public class ClsContactType
{
    public int idContactType { get; set; }
    public string ContactType { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public ClsContactType()
    {
    }
    public string InsertContactType(ClsContactType data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {
            ClsContactType oNewRow = new ClsContactType()
            {
                ContactType = data.ContactType,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                ActiveFlag = data.ActiveFlag
            };
            puroTouchContext.GetTable<ClsContactType>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string UpdateContactType(ClsContactType data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {
            if (data.idContactType > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblContactType>()
                    where qdata.idContactType == data.idContactType
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblContactType updRow in query)
                {
                    updRow.ContactType = data.ContactType;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idContactType = data.idContactType;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;
                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();
            }
            else
            {
                errMsg = "There is No Contact Type with ID = " + "'" + data.idContactType + "'";
            }
        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
public static class SrvContactType
{
    public static ClsContactType GetContactsTypeByID(int idContactType)
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        ClsContactType qContactType = o.GetTable<tblContactType>()
                            .Where(p => p.ActiveFlag == true && p.idContactType == idContactType)
                            .Select(p => new ClsContactType() { idContactType = p.idContactType, ContactType = p.ContactType, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                            .FirstOrDefault();
        return qContactType;
    }
}
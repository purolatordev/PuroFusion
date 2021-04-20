using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;


/// <summary>
/// Summary description for ClsCommunicationMethod
/// </summary>
public class ClsCommunicationMethod
{
    public int idCommunicationMethod { get; set; }
    public string CommunicationMethod { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }

    public string InsertCommMethod(ClsCommunicationMethod data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblCommunicationMethod oNewRow = new tblCommunicationMethod()
            {
                CommunicationMethod = data.CommunicationMethod,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag
            };



            puroTouchContext.GetTable<tblCommunicationMethod>().InsertOnSubmit(oNewRow);
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

    public string UpdateCommMethod(ClsCommunicationMethod data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idCommunicationMethod > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblCommunicationMethod>()
                    where qdata.idCommunicationMethod == data.idCommunicationMethod
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblCommunicationMethod updRow in query)
                {

                    updRow.CommunicationMethod = data.CommunicationMethod;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idCommunicationMethod = data.idCommunicationMethod;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Shipping Channel with ID = " + "'" + data.idCommunicationMethod + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
public static class SrvCommunicationMethod
{
    public static List<ClsCommunicationMethod> GetCommunicationMethods()
    {
        PuroTouchSQLDataContext o = new PuroTouchSQLDataContext();
        List<ClsCommunicationMethod> qCommMeth = o.GetTable<tblCommunicationMethod>()
                                            .Where(p => p.ActiveFlag == true)
                                            .Select(p => new ClsCommunicationMethod() { idCommunicationMethod = p.idCommunicationMethod, CommunicationMethod = p.CommunicationMethod, ActiveFlag = p.ActiveFlag, CreatedBy = p.CreatedBy, CreatedOn = p.CreatedOn, UpdatedBy = p.UpdatedBy, UpdatedOn = p.UpdatedOn })
                                            .ToList();

        return qCommMeth;
    }

}
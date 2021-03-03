using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsSolutionType
/// </summary>
public class ClsSolutionType
{

    public int idSolutionType { get; set; }
    public string SolutionType { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public ClsSolutionType()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string InsertSolutionType(ClsSolutionType data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            ClsSolutionType oNewRow = new ClsSolutionType()
            {

                SolutionType = data.SolutionType,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag
            };



            puroTouchContext.GetTable<ClsSolutionType>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string UpdateSolutionType(ClsSolutionType data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idSolutionType > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblSolutionType>()
                    where qdata.idSolutionType == data.idSolutionType
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblSolutionType updRow in query)
                {

                    updRow.SolutionType = data.SolutionType;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idSolutionType = data.idSolutionType;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Solution Type with ID = " + "'" + data.idSolutionType + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
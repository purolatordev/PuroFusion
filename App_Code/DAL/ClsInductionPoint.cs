using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsInductionPoint
/// </summary>
public class ClsInductionPoint
{
    public int idInduction { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Country { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public bool? PuroPostFlag { get; set; }

    public string InsertInductionPoint(ClsInductionPoint data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblInductionPoint oNewRow = new tblInductionPoint()
            {
                Description = data.Description,
                Address = data.Address,
                City = data.City,
                State = data.State,
                Zip = data.Zip,
                Country = data.Country,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag,
                PuroPostFlag = data.PuroPostFlag
            };



            puroTouchContext.GetTable<tblInductionPoint>().InsertOnSubmit(oNewRow);
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

    public string UpdateInductionPoint(ClsInductionPoint data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idInduction > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblInductionPoint>()
                    where qdata.idInduction == data.idInduction
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblInductionPoint updRow in query)
                {

                    updRow.Description = data.Description;
                    updRow. Address = data.Address;
                    updRow.City = data.City;
                    updRow.State = data.State;
                    updRow.Zip = data.Zip;
                    updRow.Country = data.Country;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.PuroPostFlag = data.PuroPostFlag;
                    updRow.idInduction = data.idInduction;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Induction Point with ID = " + "'" + data.idInduction + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
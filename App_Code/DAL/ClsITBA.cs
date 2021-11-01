using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsITBA
/// </summary>
public class ClsITBA
{
    public int idITBA { get; set; }
    public int idEmployee { get; set; }
    public string ITBA { get; set; }
    public string ITBAName { get; set; }
    public string ITBAEmail { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public bool? ReceiveNewReqEmail { get; set; }
    public string login { get; set; }
    public bool EDIFlag { get; set; }
    public ClsITBA GetITBA(int idITBA)
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        ClsITBA oITBA = (from data in puroTouchContext.GetTable<vw_ITBA>()
                              where data.idITBA == idITBA
                               select new ClsITBA
                               {
                                   idITBA = data.idITBA,
                                   ITBAName = data.ITBA,
                                   ITBA = data.ITBA,
                                   ITBAEmail = data.ITBAemail,
                                   idEmployee = data.idEmployee,
                                   ReceiveNewReqEmail = data.ReceiveNewReqEmail,
                                   login = data.login,
                                   EDIFlag = data.EDIFlag
                               }).FirstOrDefault();
        return oITBA;
    }

    public string InsertITBA(ClsITBA data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblITBA oNewRow = new tblITBA()
            {
                ITBAemail = data.ITBAEmail,
                idEmployee = data.idEmployee,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag,
                ReceiveNewReqEmail = data.ReceiveNewReqEmail,
                login = data.login
            };



            puroTouchContext.GetTable<tblITBA>().InsertOnSubmit(oNewRow);
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

    public string UpdateITBA(ClsITBA data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idEmployee > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblITBA>()
                    where qdata.idEmployee == data.idEmployee
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblITBA updRow in query)
                {

                    updRow.ITBAemail = data.ITBAEmail;
                    updRow.idEmployee = data.idEmployee;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;
                    updRow.ReceiveNewReqEmail = data.ReceiveNewReqEmail;
                    updRow.login = data.login;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Shipping Product with ID = " + "'" + data.idEmployee + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
    
    
}
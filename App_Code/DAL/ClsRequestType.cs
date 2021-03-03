using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsRequestType
/// </summary>
public class ClsRequestType
{

    public int idRequestType { get; set; }
    public string RequestType { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }

	public ClsRequestType()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string InsertRequestType(ClsRequestType data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            ClsRequestType oNewRow = new ClsRequestType()
            {

                RequestType = data.RequestType,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag
            };



            puroTouchContext.GetTable<ClsRequestType>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    public string UpdateRequestType(ClsRequestType data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idRequestType > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblRequestType>()
                    where qdata.idRequestType == data.idRequestType
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblRequestType updRow in query)
                {

                    updRow.RequestType = data.RequestType;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idRequestType = data.idRequestType;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Request Type with ID = " + "'" + data.idRequestType + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
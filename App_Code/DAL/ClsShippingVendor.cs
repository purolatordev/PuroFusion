using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;


/// <summary>
/// Summary description for ClsShippingVendor
/// </summary>
public class ClsShippingVendor
{
    public int idShippingVendor { get; set; }
    public string VendorName { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }

	public ClsShippingVendor()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string InsertVendor(ClsShippingVendor data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblShippingVendor oNewRow = new tblShippingVendor()
            {
                idShippingVendor = (Int32)data.idShippingVendor,
                VendorName = data.VendorName,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag
            };



            puroTouchContext.GetTable<tblShippingVendor>().InsertOnSubmit(oNewRow);
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

    public string UpdateVendor(ClsShippingVendor data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idShippingVendor > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblShippingVendor>()
                    where qdata.idShippingVendor == data.idShippingVendor
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblShippingVendor updRow in query)
                {

                    updRow.VendorName = data.VendorName;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idShippingVendor = data.idShippingVendor;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Vendor with ID = " + "'" + data.idShippingVendor + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
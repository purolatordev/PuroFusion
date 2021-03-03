using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsThirdPartyVendor
/// </summary>
public class ClsThirdPartyVendor
{
    public int idThirdPartyVendor { get; set; }
    public string VendorName { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }

    public string InsertVendor(ClsThirdPartyVendor data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            tblThirdPartyVendor oNewRow = new tblThirdPartyVendor()
            {
                idThirdPartyVendor = (Int32)data.idThirdPartyVendor,
                VendorName = data.VendorName,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                //UpdatedBy = data.UpdatedBy,
                //UpdatedOn = (DateTime?)data.UpdatedOn,
                ActiveFlag = data.ActiveFlag
            };



            puroTouchContext.GetTable<tblThirdPartyVendor>().InsertOnSubmit(oNewRow);
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

    public string UpdateVendor(ClsThirdPartyVendor data)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

        try
        {

            if (data.idThirdPartyVendor > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblThirdPartyVendor>()
                    where qdata.idThirdPartyVendor == data.idThirdPartyVendor
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblThirdPartyVendor updRow in query)
                {

                    updRow.VendorName = data.VendorName;
                    updRow.ActiveFlag = data.ActiveFlag;
                    updRow.idThirdPartyVendor = data.idThirdPartyVendor;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = data.UpdatedOn;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Vendor with ID = " + "'" + data.idThirdPartyVendor + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
}
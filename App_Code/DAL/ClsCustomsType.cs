using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;


/// <summary>
/// Summary description for ClsCustomsType
/// </summary>
public class ClsCustomsType
{
    public int idCustomsType { get; set; }
    public string CustomsType { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }

	public ClsCustomsType()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string InsertCustomsType(ClsCustomsType data)
        {
            string errMsg = "";
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
           
            try
            {

                tblCustomsType oNewRow = new tblCustomsType()
                {
                    
                    CustomsType = data.CustomsType,
                    CreatedBy = data.CreatedBy,
                    CreatedOn = (DateTime?)data.CreatedOn,
                    //UpdatedBy = data.UpdatedBy,
                    //UpdatedOn = (DateTime?)data.UpdatedOn,
                    ActiveFlag = data.ActiveFlag
                };



                puroTouchContext.GetTable<tblCustomsType>().InsertOnSubmit(oNewRow);
                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();
                

            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }

        public string UpdateCustomsType(ClsCustomsType data)
        {
            string errMsg = "";
            PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();

            try
            {

                if (data.idCustomsType > 0)
                {
                    // Query the database for the row to be updated. 
                    var query =
                        from qdata in puroTouchContext.GetTable<tblCustomsType>()
                        where qdata.idCustomsType == data.idCustomsType
                        select qdata;

                    // Execute the query, and change the column values 
                    // you want to change. 
                    foreach (tblCustomsType updRow in query)
                    {

                        updRow.CustomsType = data.CustomsType;
                        updRow.ActiveFlag = data.ActiveFlag;
                        updRow.idCustomsType = data.idCustomsType;
                        updRow.UpdatedBy = data.UpdatedBy;
                        updRow.UpdatedOn = data.UpdatedOn;

                    }

                    // Submit the changes to the database. 
                    puroTouchContext.SubmitChanges();


                }
                else
                {
                    errMsg = "There is No Customs Type with ID = " + "'" + data.idCustomsType + "'";
                }


            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
            }
            return errMsg;
        }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsDiscoveryRequestDetails
/// </summary>
public class ClsDiscoveryRequestDetails
{
    public int idRequestDetails { get; set; }
    public int idRequest { get; set; }
    public string CourierAcctNbr { get; set; }
    public string CourierPinPrefix { get; set; }
    public string CourierContractNbr { get; set; }
    public Int16? CourierTransitDays { get; set; }
    public string CourierInductionDesc { get; set; }
    public string CourierInductionAddress { get; set; }
    public string CourierInductionCity { get; set; }
    public string CourierInductionState { get; set; }
    public string CourierInductionZip { get; set; }
    public string CourierInductionCountry { get; set; }
    public string CourierFTPusername { get; set; }
    public string CourierFTPpwd { get; set; }
    public string CourierFTPsenderID { get; set; }
    public bool? CourierFTPCustOwnFlag { get; set; }
    public string LTLAcctNbr { get; set; }
    public string LTLMinProNbr { get; set; }
    public string LTLMaxProNbr { get; set; }
    public string CPCAcctNbr { get; set; }
    public string CPCSiteNbr { get; set; }
    public string CPCContractNbr { get; set; }
    public string CPCInductionNbr { get; set; }
    public string CPCUsername { get; set; }
    public string CPCpwd { get; set; }
    public string PPSTAcctNbr { get; set; }
    public Int16? PPSTTransitDays { get; set; }
    public string PPSTInductionDesc { get; set; }
    public string PPSTInductionAddress { get; set; }
    public string PPSTInductionCity { get; set; }
    public string PPSTInductionState { get; set; }
    public string PPSTInductionZip { get; set; }
    public string PPSTInductionCountry { get; set; }
    public string PPSTRoute { get; set; }
    public string PPlusAcctNbr { get; set; }
    public Int16? PPlusTransitDays { get; set; }
    public string PPlusInductionDesc { get; set; }
    public string PPlusInductionAddress { get; set; }
    public string PPlusInductionCity { get; set; }
    public string PPlusInductionState { get; set; }
    public string PPlusInductionZip { get; set; }
    public string PPlusInductionCountry { get; set; }
    public string PPlusRoute { get; set; } 
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool? ActiveFlag { get; set; }
    public string LTLPinPrefix { get; set; }
    public bool? LTLAutomatedFlag { get; set; }
    public string CourierSandboxFTPusername { get; set; }
    public string CourierSandboxFTPpwd { get; set; }
    public string ShipRecordType { get; set; }


	public ClsDiscoveryRequestDetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    
    public ClsDiscoveryRequestDetails GetDiscoveryRequestDetails(int idRequest, string shipType)
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        ClsDiscoveryRequestDetails oReq = (from data in puroTouchContext.GetTable<tblDiscoveryRequestDetail>()
                                           join req in puroTouchContext.GetTable<tblDiscoveryRequest>() on data.idRequest equals req.idRequest
                                           where data.idRequest == idRequest
                                           where data.ShipRecordType == shipType
                                           where req.ActiveFlag != false
                                           select new ClsDiscoveryRequestDetails
                                           {
                                               idRequest = data.idRequest,
                                               idRequestDetails = data.idRequestDetails,
                                               ShipRecordType = data.ShipRecordType,
                                               CourierAcctNbr = data.CourierAcctNbr,
                                               CourierPinPrefix = data.CourierPinPrefix,
                                               CourierContractNbr = data.CourierContractNbr,
                                               CourierTransitDays = Convert.ToInt16(data.CourierTransitDays),
                                               CourierInductionDesc = data.CourierInductionDesc,
                                               CourierInductionAddress = data.CourierInductionAddress,
                                               CourierInductionCity = data.CourierInductionCity,
                                               CourierInductionState = data.CourierInductionState,
                                               CourierInductionZip = data.CourierInductionZip,
                                               CourierInductionCountry = data.CourierInductionCountry,
                                               CourierFTPusername = data.CourierFTPusername,
                                               CourierFTPpwd = data.CourierFTPpwd,
                                               CourierFTPsenderID = data.CourierFTPsenderID,
                                               CourierSandboxFTPusername = data.CourierSandboxFTPusername,
                                               CourierSandboxFTPpwd = data.CourierSandboxFTPpwd,
                                               CourierFTPCustOwnFlag = (bool?)data.CourierFTPCustOwnFlag,
                                               LTLAcctNbr = data.LTLAcctNbr,
                                               LTLMinProNbr = data.LTLMinProNbr,
                                               LTLMaxProNbr = data.LTLMaxProNbr,
                                               LTLPinPrefix = data.LTLPinPrefix,
                                               LTLAutomatedFlag = (bool?)data.LTLAutomatedFlag,
                                               CPCAcctNbr = data.CPCAcctNbr,
                                               CPCSiteNbr = data.CPCSiteNbr,
                                               CPCContractNbr = data.CPCContractNbr,
                                               CPCInductionNbr = data.CPCInductionNbr,
                                               CPCUsername = data.CPCUsername,
                                               CPCpwd = data.CPCpwd,
                                               PPSTAcctNbr = data.PPSTAcctNbr,
                                               PPSTTransitDays = Convert.ToInt16(data.PPSTTransitDays),
                                               PPSTInductionDesc = data.PPSTInductionDesc,
                                               PPSTInductionAddress = data.PPSTInductionAddress,
                                               PPSTInductionCity = data.PPSTInductionCity,
                                               PPSTInductionState = data.PPSTInductionState,
                                               PPSTInductionZip = data.PPSTInductionZip,
                                               PPSTInductionCountry = data.PPSTInductionCountry,
                                               PPlusAcctNbr = data.PPlusAcctNbr,
                                               PPlusTransitDays = Convert.ToInt16(data.PPlusTransitDays),
                                               PPlusInductionDesc = data.PPlusInductionDesc,
                                               PPlusInductionAddress = data.PPlusInductionAddress,
                                               PPlusInductionCity = data.PPlusInductionCity,
                                               PPlusInductionState = data.PPlusInductionState,
                                               PPlusInductionZip = data.PPlusInductionZip,
                                               PPlusInductionCountry = data.PPlusInductionCountry,
                                               PPSTRoute = data.PPSTRoute,
                                               UpdatedBy = data.UpdatedBy,
                                               UpdatedOn = (DateTime?)data.UpdatedOn,
                                               CreatedBy = data.CreatedBy,
                                               CreatedOn = (DateTime?)data.CreatedOn,
                                               ActiveFlag = (bool?)data.ActiveFlag


                                           }).FirstOrDefault();
        return oReq;
    }

    //INSERT A NEW REQUEST 
    public string InsertDiscoveryRequestDetails(ClsDiscoveryRequestDetails data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;
        //DateTime localDate = DateTime.Now;

        try
        {

            tblDiscoveryRequestDetail oNewRow = new tblDiscoveryRequestDetail()
            {
                idRequest = data.idRequest,
                ShipRecordType = data.ShipRecordType,
                CourierAcctNbr = data.CourierAcctNbr,
                CourierPinPrefix = data.CourierPinPrefix,
                CourierContractNbr = data.CourierContractNbr,
                CourierTransitDays = Convert.ToInt16(data.CourierTransitDays),
                CourierInductionDesc = data.CourierInductionDesc,
                CourierInductionAddress = data.CourierInductionAddress,
                CourierInductionCity = data.CourierInductionCity,
                CourierInductionState = data.CourierInductionState,
                CourierInductionZip = data.CourierInductionZip,
                CourierInductionCountry = data.CourierInductionCountry,
                CourierFTPusername = data.CourierFTPusername,
                CourierFTPpwd = data.CourierFTPpwd,
                CourierFTPsenderID = data.CourierFTPsenderID,
                CourierSandboxFTPusername = data.CourierSandboxFTPusername,
                CourierSandboxFTPpwd = data.CourierSandboxFTPpwd,
                CourierFTPCustOwnFlag = (bool?)data.CourierFTPCustOwnFlag,
                LTLAcctNbr = data.LTLAcctNbr,
                LTLMinProNbr = data.LTLMinProNbr,
                LTLMaxProNbr = data.LTLMaxProNbr,
                LTLPinPrefix = data.LTLPinPrefix,
                LTLAutomatedFlag = (bool?)data.LTLAutomatedFlag,
                CPCAcctNbr = data.CPCAcctNbr,
                CPCSiteNbr = data.CPCSiteNbr,
                CPCContractNbr = data.CPCContractNbr,
                CPCInductionNbr = data.CPCInductionNbr,
                CPCUsername = data.CPCUsername,
                CPCpwd = data.CPCpwd,
                PPSTAcctNbr = data.PPSTAcctNbr,
                PPSTTransitDays = Convert.ToInt16(data.PPSTTransitDays),
                PPSTInductionDesc = data.PPSTInductionDesc,
                PPSTInductionAddress = data.PPSTInductionAddress,
                PPSTInductionCity = data.PPSTInductionCity,
                PPSTInductionState = data.PPSTInductionState,
                PPSTInductionZip = data.PPSTInductionZip,
                PPSTInductionCountry = data.PPSTInductionCountry,
                PPlusAcctNbr = data.PPlusAcctNbr,
                PPlusTransitDays = Convert.ToInt16(data.PPlusTransitDays),
                PPlusInductionDesc = data.PPlusInductionDesc,
                PPlusInductionAddress = data.PPlusInductionAddress,
                PPlusInductionCity = data.PPlusInductionCity,
                PPlusInductionState = data.PPlusInductionState,
                PPlusInductionZip = data.PPlusInductionZip,
                PPlusInductionCountry = data.PPlusInductionCountry, 
                PPSTRoute = data.PPSTRoute,
                UpdatedBy = data.UpdatedBy,
                UpdatedOn = (DateTime?)data.UpdatedOn,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                ActiveFlag = (bool?)data.ActiveFlag
            };
            puroTouchContext.GetTable<tblDiscoveryRequestDetail>().InsertOnSubmit(oNewRow);
            // Submit the changes to the database. 
            puroTouchContext.SubmitChanges();
            newID = oNewRow.idRequest;
            data.idRequest = newID;



        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    //UPDATE A REQUEST
    public string UpdateDiscoveryRequestDetails(ClsDiscoveryRequestDetails data, Int32 RequestID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        DateTime localDate = DateTime.Now;
        try
        {
            // right now oExisting comes back null, need to fix this
            ClsDiscoveryRequestDetails oExisting = GetDiscoveryRequestDetails(RequestID,data.ShipRecordType);

            if (oExisting != null && data != null)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblDiscoveryRequestDetail>()
                    where qdata.idRequest == RequestID
                    where qdata.ShipRecordType == data.ShipRecordType
                    select qdata;

                //// Execute the query, and change the column values 
                //// you want to change. 
                foreach (tblDiscoveryRequestDetail updRow in query)
                {
                    updRow.CourierAcctNbr = data.CourierAcctNbr;
                    updRow.ShipRecordType = data.ShipRecordType;
                    updRow.CourierPinPrefix = data.CourierPinPrefix;
                    updRow.CourierContractNbr = data.CourierContractNbr;
                    updRow.CourierTransitDays = Convert.ToInt16(data.CourierTransitDays);
                    updRow.CourierInductionDesc = data.CourierInductionDesc;
                    updRow.CourierInductionAddress = data.CourierInductionAddress;
                    updRow.CourierInductionCity = data.CourierInductionCity;
                    updRow.CourierInductionState = data.CourierInductionState;
                    updRow.CourierInductionZip = data.CourierInductionZip;
                    updRow.CourierInductionCountry = data.CourierInductionCountry;
                    updRow.CourierFTPusername = data.CourierFTPusername;
                    updRow.CourierFTPpwd = data.CourierFTPpwd;
                    updRow.CourierFTPsenderID = data.CourierFTPsenderID;
                    updRow.CourierSandboxFTPusername = data.CourierSandboxFTPusername;
                    updRow.CourierSandboxFTPpwd = data.CourierSandboxFTPpwd;
                    updRow.CourierFTPCustOwnFlag = (bool?)data.CourierFTPCustOwnFlag;
                    updRow.LTLAcctNbr = data.LTLAcctNbr;
                    updRow.LTLMinProNbr = data.LTLMinProNbr;
                    updRow.LTLMaxProNbr = data.LTLMaxProNbr;
                    updRow.LTLPinPrefix = data.LTLPinPrefix;
                    updRow.LTLAutomatedFlag = (bool?)data.LTLAutomatedFlag;
                    updRow.CPCAcctNbr = data.CPCAcctNbr;
                    updRow.CPCSiteNbr = data.CPCSiteNbr;
                    updRow.CPCContractNbr = data.CPCContractNbr;
                    updRow.CPCInductionNbr = data.CPCInductionNbr;
                    updRow.CPCUsername = data.CPCUsername;
                    updRow.CPCpwd = data.CPCpwd;
                    updRow.PPSTAcctNbr = data.PPSTAcctNbr;
                    updRow.PPSTTransitDays = Convert.ToInt16(data.PPSTTransitDays);
                    updRow.PPSTInductionDesc = data.PPSTInductionDesc;
                    updRow.PPSTInductionAddress = data.PPSTInductionAddress;
                    updRow.PPSTInductionCity = data.PPSTInductionCity;
                    updRow.PPSTInductionState = data.PPSTInductionState;
                    updRow.PPSTInductionZip = data.PPSTInductionZip;
                    updRow.PPSTInductionCountry = data.PPSTInductionCountry; 
                    updRow.PPlusAcctNbr = data.PPlusAcctNbr;
                    updRow.PPlusTransitDays = Convert.ToInt16(data.PPlusTransitDays);
                    updRow.PPlusInductionDesc = data.PPlusInductionDesc;
                    updRow.PPlusInductionAddress = data.PPlusInductionAddress;
                    updRow.PPlusInductionCity = data.PPlusInductionCity;
                    updRow.PPlusInductionState = data.PPlusInductionState;
                    updRow.PPlusInductionZip = data.PPlusInductionZip;
                    updRow.PPlusInductionCountry = data.PPlusInductionCountry; 
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = (DateTime?)data.UpdatedOn;
                    updRow.CreatedBy = data.CreatedBy;
                    updRow.CreatedOn = (DateTime?)data.CreatedOn;
                    updRow.ActiveFlag = (bool?)data.ActiveFlag;
                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();

            }
            else
            {
                errMsg = "There is No Discovery Request with idRequest = " + "'" + data.idRequest + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }

    //DELETE A REQUEST (set ActiveFlag to false)
    public string deActivateDiscoveryRequestDetails(Int32 requestID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        try
        {

            if (requestID > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblDiscoveryRequestDetail>()
                    where qdata.idRequest == requestID
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblDiscoveryRequestDetail updRow in query)
                {

                    updRow.ActiveFlag = false;

                }

                // Submit the changes to the database. 
                puroTouchContext.SubmitChanges();


            }
            else
            {
                errMsg = "There is No Discovery Request with idRequest = " + "'" + requestID + "'";
            }


        }
        catch (Exception ex)
        {
            errMsg = ex.Message.ToString();
        }
        return errMsg;
    }
 
}
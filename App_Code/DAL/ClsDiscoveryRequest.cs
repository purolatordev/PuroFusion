using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

/// <summary>
/// Summary description for ClsDiscoveryRequest
/// </summary>
public class ClsDiscoveryRequest
{
    public int idRequest { get; set; }
    public bool flagNewRequest { get; set; }
    public string SalesRepName { get; set; }
    public string SalesRepEmail { get; set; }
    public int? idOnboardingPhase { get; set; }
    public string District  { get; set; }
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zipcode { get; set; }
    public string Country { get; set; }
    public string Commodity { get; set; }
    public decimal ProjectedRevenue { get; set; }
    public string CurrentSolution { get; set; }
    public string CustomerWebsite { get; set; }
    public string ProposedCustoms { get; set; }
    public DateTime? CallDate1 { get; set; }
    public DateTime? CallDate2 { get; set; }
    public DateTime? CallDate3 { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public bool ActiveFlag { get; set; }
    public string SalesComments { get; set; }
    public int? idITBA { get; set; }
    public int? idShippingChannel { get; set; }
    public DateTime? TargetGoLive { get; set; }
    public DateTime? ActualGoLive { get; set; }
    public string SolutionSummary { get; set; }
    public string OnboardingPhase { get; set; }
    public string ShippingChannel { get; set; }
    public string ITBA { get; set; }
    public string Branch { get; set; }
    public int? idVendor { get; set; }
    public bool? worldpakFlag { get; set; }
    public bool? thirdpartyFlag { get; set; }
    public bool? customFlag { get; set; }
    public string invoiceType { get; set; }
    public string billtoAccount { get; set; }
    public string FTPUsername { get; set; }
    public string FTPPassword { get; set; }
    public bool? customsFlag { get; set; }
    public bool? elinkFlag { get; set; }
    public string PARS { get; set; }
    public string PASS { get; set; }
    public string customsBroker { get; set; }
    public string SupportUser { get; set; }
    public string SupportGroup { get; set; }
    public string Office { get; set; }
    public string Group { get; set; }
    public DateTime? MigrationDate { get; set; }
    public string PreMigrationSolution { get; set; }
    public string PostMigrationSolution { get; set; }
    public string ControlBranch { get; set; }
    public string ContractNumber { get; set; }
    public DateTime? ContractStartDate { get; set; }
    public DateTime? ContractEndDate { get; set; }
    public string ContractCurrency  { get; set; }
    public string PaymentTerms { get; set; }
    public string ThirdPartyVendor { get; set; }
    public string CloseReason { get; set; }
    public string CRR { get; set; }
    public string BrokerNumber { get; set; }
    public bool? DataScrubFlag { get; set; }
    public bool? EDICustomizedFlag { get; set; }
    public bool? StrategicFlag { get; set; }
    public string ReturnsAcctNbr { get; set; }
    public string ReturnsAddress { get; set; }
    public string ReturnsCity { get; set; }
    public string ReturnsState { get; set; }
    public string ReturnsZip { get; set; }
    public string ReturnsCountry { get; set; }
    public bool? ReturnsDestroyFlag { get; set; }
    public bool? ReturnsCreateLabelFlag { get; set; }
    public string WPKSandboxUsername { get; set; }
    public string WPKSandboxPwd { get; set; }
    public string WPKProdUsername { get; set; }
    public string WPKProdPwd { get; set; }
    public bool? WPKCustomExportFlag { get; set; }
    public bool? WPKGhostScanFlag { get; set; }
    public bool? WPKEastWestSplitFlag { get; set; }
    public bool? WPKAddressUploadFlag { get; set; }
    public bool? WPKProductUploadFlag { get; set; }
    public bool? WPKEquipmentFlag { get; set; }
    public string WPKDataEntryMethod { get; set; }
    public string EWSelectBy { get; set; }
    public bool? EWSortCodeFlag { get; set; }
    public string EWEastSortCode { get; set; }
    public string EWWestSortCode { get; set; }
    public bool? EWSepCloseoutFlag { get; set; }
    public bool? EWSepPUFlag { get; set; }
    public string EWSortDetails { get; set; }
    public string EWMissortDetails { get; set; }
    public string TotalTimeSpent { get; set; }
    public string NewRequestYesNo { get; set; }
    public string WorldpakYesNo { get; set; }
    public DateTime? CurrentGoLive { get; set; }
    public DateTime? PhaseChangeDate { get; set; }
    public int? idRequestType { get; set; }
    public int? idSolutionType { get; set; }
    public string SolutionType { get; set; }
    public string RequestType { get; set; }
    public bool? CurrentlyShippingFlag { get; set; }
    public int? idShippingVendor { get; set; }
    public string OtherVendorName { get; set; }
    public int? idBroker { get; set; }
    public string OtherBrokerName { get; set; }
    public int? idVendorType { get; set; }
    public string Route { get; set; }
    public string VendorType { get; set; }

    public string VendorName { get; set; }

    public System.Nullable<bool> FreightAuditor { get; set; }

    public string EDIDetails { get; set; }

    public System.Nullable<int> idEDISpecialist { get; set; }
    public string EDISpecialistName { get; set; }

    public System.Nullable<int> idBillingSpecialist { get; set; }

    public System.Nullable<int> idCollectionSpecialist { get; set; }

    public System.Nullable<bool> AuditorPortal { get; set; }

    public string AuditorURL { get; set; }

    public string AuditorUserName { get; set; }

    public string AuditorPassword { get; set; }

    public System.Nullable<System.DateTime> EDITargetGoLive { get; set; }

    public System.Nullable<System.DateTime> EDICurrentGoLive { get; set; }

    public System.Nullable<System.DateTime> EDIActualGoLive { get; set; }
    public int idEDIOnboardingPhase { get; set; }
    public string EDIOnboardingPhaseType { get; set; }
    public ClsDiscoveryRequest()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //GET DATA FOR A REQUEST ID
    public ClsDiscoveryRequest GetDiscoveryRequest(int idRequest)
    {
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        ClsDiscoveryRequest oReq = (from data in puroTouchContext.GetTable<tblDiscoveryRequest>()
                                    join phase in puroTouchContext.GetTable<tblOnboardingPhase>() on data.idOnboardingPhase equals phase.idOnboardingPhase into tmpphase
                                    join channel in puroTouchContext.GetTable<tblShippingChannel>() on data.idShippingChannel equals channel.idShippingChannel into tmpchannel
                                    join vwitba in puroTouchContext.GetTable<vw_ITBA>() on data.idITBA equals vwitba.idITBA into tmpitba
                                    join tpv in puroTouchContext.GetTable<tblThirdPartyVendor>() on data.idVendor equals tpv.idThirdPartyVendor into tmpthirdparty
                                    from phase in tmpphase.DefaultIfEmpty()
                                    from channel in tmpchannel.DefaultIfEmpty()
                                    from vwitba in tmpitba.DefaultIfEmpty()
                                    from tpv in tmpthirdparty.DefaultIfEmpty()
                                               where data.idRequest == idRequest
                                               where data.ActiveFlag != false
                                               select new ClsDiscoveryRequest
                                               {
                                                   idRequest = data.idRequest,
                                                   flagNewRequest = Convert.ToBoolean(data.isNewRequest),
                                                   SalesRepName = data.SalesRepName,
                                                   SalesRepEmail = data.SalesRepEmail,
                                                   idOnboardingPhase = (int?)data.idOnboardingPhase,
                                                   District = data.District,
                                                   CustomerName = data.CustomerName,
                                                   Address = data.Address,
                                                   City = data.City,
                                                   State = data.State,
                                                   Zipcode = data.Zipcode,
                                                   Country = data.Country,
                                                   Commodity = data.Commodity,
                                                   ProjectedRevenue = (decimal)data.ProjectedRevenue,
                                                   CurrentSolution = data.CurrentSolution,
                                                   ProposedCustoms = data.ProposedCustoms,
                                                   CallDate1 = (DateTime?)data.CallDate1,
                                                   CallDate2 = (DateTime?)data.CallDate2,
                                                   CallDate3 = (DateTime?)data.CallDate3,
                                                   UpdatedBy = data.UpdatedBy,
                                                   UpdatedOn = (DateTime?)data.UpdatedOn,
                                                   CreatedBy = data.CreatedBy,
                                                   CreatedOn = (DateTime?)data.CreatedOn,
                                                   ActiveFlag = Convert.ToBoolean(data.ActiveFlag),
                                                   SalesComments = data.SalesComments,
                                                   idITBA = (int?)data.idITBA,
                                                   idShippingChannel = (int?)data.idShippingChannel,
                                                   SolutionSummary = data.SolutionSummary,
                                                   TargetGoLive = (DateTime?)data.TargetGoLive,
                                                   ActualGoLive = (DateTime?)data.ActualGoLive,
                                                   OnboardingPhase = phase.OnboardingPhase,
                                                   ShippingChannel = channel.ShippingChannel,
                                                   ITBA = vwitba.ITBA,
                                                   CustomerWebsite = data.CustomerWebsite,
                                                   Branch = data.Branch,
                                                   idVendor = (int?)data.idVendor,
                                                   worldpakFlag = (Boolean)data.worldpakFlag,
                                                   thirdpartyFlag = (Boolean)data.thirdpartyFlag,
                                                   customFlag = (Boolean)data.customFlag,
                                                   DataScrubFlag = (Boolean)data.DataScrubFlag,
                                                   invoiceType = data.InvoiceType,
                                                   billtoAccount = data.BilltoAccount,
                                                   FTPUsername = data.FTPUsername,
                                                   FTPPassword = data.FTPPassword,
                                                   customsFlag = (Boolean)data.CustomsSupportedFlag,
                                                   elinkFlag = (Boolean)data.ElinkFlag,
                                                   PARS = data.PARS,
                                                   PASS = data.PASS,
                                                   customsBroker = data.CustomsBroker,
                                                   BrokerNumber = data.BrokerNumber,
                                                   SupportUser = data.SupportUser,
                                                   SupportGroup = data.SupportGroup,
                                                   Office = data.Office,
                                                   Group = data.Group,
                                                   CRR = data.CRR,
                                                   MigrationDate = (DateTime?)data.MigrationDate,
                                                   PreMigrationSolution = data.PreMigrationSolution,
                                                   PostMigrationSolution = data.PostMigrationSolution,
                                                   ControlBranch = data.ControlBranch,
                                                   ContractNumber = data.ContractNumber,
                                                   ContractStartDate = (DateTime?)data.ContractStartDate,
                                                   ContractEndDate = (DateTime?)data.ContractEndDate,
                                                   ContractCurrency = data.ContractCurrency,
                                                   PaymentTerms = data.PaymentTerms,
                                                   ThirdPartyVendor = tpv.VendorName,
                                                   CloseReason = data.CloseReason,
                                                   EDICustomizedFlag = data.EDICustomizedFlag,
                                                   StrategicFlag = data.StrategicFlag,
                                                   ReturnsAcctNbr = data.ReturnsAcctNbr,
                                                   ReturnsAddress = data.ReturnsAddress,
                                                   ReturnsCity = data.ReturnsCity,
                                                   ReturnsState = data.ReturnsState,
                                                   ReturnsZip = data.ReturnsZip,
                                                   ReturnsCountry = data.ReturnsCountry,
                                                   ReturnsDestroyFlag = (bool?)data.ReturnsDestroyFlag,
                                                   ReturnsCreateLabelFlag = (bool?)data.ReturnsCreateLabelFlag,
                                                   WPKSandboxUsername = data.WPKSandboxUsername,
                                                   WPKSandboxPwd = data.WPKSandboxPwd,
                                                   WPKProdUsername = data.WPKProdUsername,
                                                   WPKProdPwd = data.WPKProdPwd,
                                                   WPKCustomExportFlag = (bool?)data.WPKCustomExportFlag,
                                                   WPKGhostScanFlag = (bool?)data.WPKGhostScanFlag,
                                                   WPKEastWestSplitFlag = (bool?)data.WPKEastWestSplitFlag,
                                                   WPKAddressUploadFlag = (bool?)data.WPKAddressUploadFlag,
                                                   WPKProductUploadFlag = (bool?)data.WPKProductUploadFlag,
                                                   WPKEquipmentFlag = (bool?)data.WPKEquipmentFlag,
                                                   WPKDataEntryMethod = data.WPKDataEntryMethod,
                                                   EWSelectBy = data.EWSelectBy,
                                                   EWSortCodeFlag = (bool?)data.EWSortCodeFlag,
                                                   EWEastSortCode = data.EWEastSortCode,
                                                   EWWestSortCode = data.EWWestSortCode,
                                                   EWSepCloseoutFlag = data.EWSepCloseoutFlag,
                                                   EWSepPUFlag = data.EWSepPUFlag,
                                                   EWSortDetails = data.EWSortDetails,
                                                   EWMissortDetails = data.EWMissortDetails,
                                                   CurrentGoLive = (DateTime?)data.CurrentGoLive,
                                                   PhaseChangeDate = (DateTime?)data.PhaseChangeDate,
                                                   idRequestType = (int?)data.idRequestType,
                                                   idSolutionType = (int?)data.idSolutionType,
                                                   CurrentlyShippingFlag = (bool?)data.CurrentlyShippingFlag,
                                                   idShippingVendor = (int?)data.idShippingVendor,
                                                   OtherVendorName = data.OtherVendorName,
                                                   idBroker = (int?)data.idBroker,
                                                   OtherBrokerName = data.OtherBrokerName,
                                                   idVendorType = (int?)data.idVendorType,
                                                   FreightAuditor = (bool?)data.FreightAuditor,
                                                   Route = data.Route,
                                                   EDIDetails = data.EDIDetails,
                                                   idEDISpecialist = data.idEDISpecialist,
                                                   idBillingSpecialist = data.idBillingSpecialist,
                                                   idCollectionSpecialist = data.idCollectionSpecialist,
                                                   AuditorPortal = (bool?)data.AuditorPortal,
                                                   AuditorURL = data.AuditorURL,
                                                   AuditorUserName = data.AuditorUserName,
                                                   AuditorPassword = data.AuditorPassword,
                                                   EDITargetGoLive = (DateTime?)data.EDITargetGoLive,
                                                   EDICurrentGoLive = (DateTime?)data.EDICurrentGoLive,
                                                   EDIActualGoLive = (DateTime?)data.EDIActualGoLive,
                                                   idEDIOnboardingPhase = data.idEDIOnboardingPhase
                                               }).FirstOrDefault();
        return oReq;
    }
    //INSERT A NEW REQUEST 
    public string InsertDiscoveryRequest(ClsDiscoveryRequest data, out Int32 newID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        newID = -1;
        try
        {
            tblDiscoveryRequest oNewRow = new tblDiscoveryRequest()
            {
                isNewRequest = data.flagNewRequest,
                SalesRepName = data.SalesRepName,
                SalesRepEmail = data.SalesRepEmail,
                idOnboardingPhase = (int?)data.idOnboardingPhase,
                District = data.District,
                CustomerName = data.CustomerName,
                Address = data.Address,
                City = data.City,
                State = data.State,
                Zipcode = data.Zipcode,
                Country = data.Country,
                Commodity = data.Commodity,
                ProjectedRevenue = (decimal)data.ProjectedRevenue,
                CurrentSolution = data.CurrentSolution,
                ProposedCustoms = data.ProposedCustoms,
                CallDate1 = (DateTime?)data.CallDate1,
                CallDate2 = (DateTime?)data.CallDate2,
                CallDate3 = (DateTime?)data.CallDate3,
                CreatedBy = data.CreatedBy,
                CreatedOn = (DateTime?)data.CreatedOn,
                ActiveFlag = data.ActiveFlag,
                SalesComments = data.SalesComments,
                idITBA = (int?)data.idITBA,
                idShippingChannel = (int?)data.idShippingChannel,
                SolutionSummary = data.SolutionSummary,
                CustomerWebsite = data.CustomerWebsite,
                Branch = data.Branch,
                idVendor = (int?)data.idVendor,
                worldpakFlag = (bool?)data.worldpakFlag,
                thirdpartyFlag = (bool?)data.thirdpartyFlag,
                customFlag = (bool?)data.customFlag,
                DataScrubFlag = (Boolean)data.DataScrubFlag,
                InvoiceType = data.invoiceType,
                BilltoAccount = data.billtoAccount,
                FTPUsername = data.FTPUsername,
                FTPPassword = data.FTPPassword,
                CustomsSupportedFlag = data.customsFlag,
                ElinkFlag = data.elinkFlag,
                PARS = data.PARS,
                PASS = data.PASS,
                CustomsBroker = data.customsBroker,
                BrokerNumber = data.BrokerNumber,
                SupportUser = data.SupportUser,
                SupportGroup = data.SupportGroup,
                Office = data.Office,
                Group = data.Group,
                MigrationDate = (DateTime?)data.MigrationDate,
                PreMigrationSolution = data.PreMigrationSolution,
                PostMigrationSolution = data.PostMigrationSolution,
                ControlBranch = data.ControlBranch,
                ContractNumber = data.ContractNumber,
                ContractStartDate = (DateTime?)data.ContractStartDate,
                ContractEndDate = (DateTime?)data.ContractEndDate,
                ContractCurrency = data.ContractCurrency,
                PaymentTerms = data.PaymentTerms,
                CloseReason = data.CloseReason,
                CRR = data.CRR,
                EDICustomizedFlag = data.EDICustomizedFlag,
                StrategicFlag = data.StrategicFlag,
                ReturnsAcctNbr = data.ReturnsAcctNbr,
                ReturnsAddress = data.ReturnsAddress,
                ReturnsCity = data.ReturnsCity,
                ReturnsState = data.ReturnsState,
                ReturnsZip = data.ReturnsZip,
                ReturnsCountry = data.ReturnsCountry,
                ReturnsDestroyFlag = (bool?)data.ReturnsDestroyFlag,
                ReturnsCreateLabelFlag = (bool?)data.ReturnsCreateLabelFlag,
                WPKSandboxUsername = data.WPKSandboxUsername,
                WPKSandboxPwd = data.WPKSandboxPwd,
                WPKProdUsername = data.WPKProdUsername,
                WPKProdPwd = data.WPKProdPwd,
                WPKCustomExportFlag = (bool?)data.WPKCustomExportFlag,
                WPKGhostScanFlag = (bool?)data.WPKGhostScanFlag,
                WPKEastWestSplitFlag = (bool?)data.WPKEastWestSplitFlag,
                WPKAddressUploadFlag = (bool?)data.WPKAddressUploadFlag,
                WPKProductUploadFlag = (bool?)data.WPKProductUploadFlag,
                WPKEquipmentFlag = (bool?)data.WPKEquipmentFlag,
                WPKDataEntryMethod = data.WPKDataEntryMethod,
                EWSelectBy = data.EWSelectBy,
                EWSortCodeFlag = (bool?)data.EWSortCodeFlag,
                EWEastSortCode = data.EWEastSortCode,
                EWWestSortCode = data.EWWestSortCode,
                EWSepCloseoutFlag = data.EWSepCloseoutFlag,
                EWSepPUFlag = data.EWSepPUFlag,
                EWSortDetails = data.EWSortDetails,
                EWMissortDetails = data.EWMissortDetails,
                CurrentGoLive = (DateTime?)data.CurrentGoLive,
                PhaseChangeDate = (DateTime?)data.PhaseChangeDate,
                idRequestType = (int?)data.idRequestType,
                idSolutionType = (int)data.idSolutionType,
                CurrentlyShippingFlag = (bool?)data.CurrentlyShippingFlag,
                idShippingVendor = (int?)data.idShippingVendor,
                OtherVendorName = data.OtherVendorName,
                idBroker = (int?)data.idBroker,
                OtherBrokerName = data.OtherBrokerName,
                idVendorType = (int?)data.idVendorType,
                FreightAuditor = (bool?)data.FreightAuditor,
                Route = data.Route,
                EDIDetails = data.EDIDetails,
                idEDISpecialist = (int?)data.idEDISpecialist,
                idBillingSpecialist = (int?)data.idBillingSpecialist,
                idCollectionSpecialist = (int?)data.idCollectionSpecialist,
                AuditorPortal = (bool?)data.AuditorPortal,
                AuditorURL = data.AuditorURL,
                AuditorUserName = data.AuditorUserName,
                AuditorPassword = data.AuditorPassword,
                EDITargetGoLive = (DateTime?)data.EDITargetGoLive,
                EDICurrentGoLive = (DateTime?)data.EDICurrentGoLive,
                EDIActualGoLive = (DateTime?)data.EDIActualGoLive,
                idEDIOnboardingPhase = (int) data.idEDIOnboardingPhase
            };
            puroTouchContext.GetTable<tblDiscoveryRequest>().InsertOnSubmit(oNewRow);
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
    public string UpdateDiscoveryRequest(ClsDiscoveryRequest data, Int32 RequestID)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        DateTime localDate = DateTime.Now;
        try
        {
            // right now oExisting comes back null, need to fix this
            ClsDiscoveryRequest oExisting = GetDiscoveryRequest(RequestID);

            if (oExisting != null && data != null)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblDiscoveryRequest>()
                    where qdata.idRequest == RequestID
                    select qdata;

                //// Execute the query, and change the column values 
                //// you want to change. 
                foreach (tblDiscoveryRequest updRow in query)
                {
                    updRow.SalesRepName = data.SalesRepName;
                    updRow.SalesRepEmail = data.SalesRepEmail;
                    updRow.idOnboardingPhase = (int?)data.idOnboardingPhase;
                    updRow.District = data.District;
                    updRow.CustomerName = data.CustomerName;
                    updRow.Address = data.Address;
                    updRow.City = data.City;
                    updRow.State = data.State;
                    updRow.Zipcode = data.Zipcode;
                    updRow.Country = data.Country;
                    updRow.Commodity = data.Commodity;
                    updRow.ProjectedRevenue = (decimal)data.ProjectedRevenue;
                    updRow.CurrentSolution = data.CurrentSolution;
                    updRow.ProposedCustoms = data.ProposedCustoms;
                    updRow.CallDate1 = (DateTime?)(data.CallDate1);
                    updRow.CallDate2 = (DateTime?)(data.CallDate2);
                    updRow.CallDate3 = (DateTime?)(data.CallDate3);
                    updRow.CreatedBy = data.CreatedBy;
                    updRow.CreatedOn = data.CreatedOn;
                    updRow.UpdatedBy = data.UpdatedBy;
                    updRow.UpdatedOn = localDate;
                    updRow.ActiveFlag = true;
                    updRow.SalesComments = data.SalesComments;
                    updRow.idITBA = (int?)data.idITBA;
                    updRow.idShippingChannel = (int?)data.idShippingChannel;
                    updRow.SolutionSummary = data.SolutionSummary;
                    updRow.TargetGoLive = (DateTime?)data.TargetGoLive;
                    updRow.ActualGoLive = (DateTime?)data.ActualGoLive;
                    updRow.CustomerWebsite = data.CustomerWebsite;
                    updRow.Branch = data.Branch;
                    updRow.idVendor = (int?)data.idVendor;
                    updRow.worldpakFlag = data.worldpakFlag;
                    updRow.thirdpartyFlag = data.thirdpartyFlag;
                    updRow.customFlag = data.customFlag;
                    updRow.DataScrubFlag = data.DataScrubFlag;
                    updRow.InvoiceType = data.invoiceType;
                    updRow.BilltoAccount = data.billtoAccount;
                    updRow.FTPUsername = data.FTPUsername;
                    updRow.FTPPassword = data.FTPPassword;
                    updRow.CustomsSupportedFlag = data.customsFlag;
                    updRow.ElinkFlag = data.elinkFlag;
                    updRow.PARS = data.PARS;
                    updRow.PASS = data.PASS;
                    updRow.CustomsBroker = data.customsBroker;
                    updRow.BrokerNumber = data.BrokerNumber;
                    updRow.SupportUser = data.SupportUser;
                    updRow.SupportGroup = data.SupportGroup;
                    updRow.Office = data.Office;
                    updRow.Group = data.Group;
                    updRow.MigrationDate = (DateTime?)data.MigrationDate;
                    updRow.PreMigrationSolution = data.PreMigrationSolution;
                    updRow.PostMigrationSolution = data.PostMigrationSolution;
                    updRow.ControlBranch = data.ControlBranch;
                    updRow.ContractNumber = data.ContractNumber;
                    updRow.ContractStartDate = (DateTime?)data.ContractStartDate;
                    updRow.ContractEndDate = (DateTime?)data.ContractEndDate;
                    updRow.ContractCurrency = data.ContractCurrency;
                    updRow.PaymentTerms = data.PaymentTerms;
                    updRow.CloseReason = data.CloseReason;
                    updRow.CRR = data.CRR;
                    updRow.EDICustomizedFlag = data.EDICustomizedFlag;
                    updRow.StrategicFlag = data.StrategicFlag;
                    updRow.ReturnsAcctNbr = data.ReturnsAcctNbr;
                    updRow.ReturnsAddress = data.ReturnsAddress;
                    updRow.ReturnsCity = data.ReturnsCity;
                    updRow.ReturnsState = data.ReturnsState;
                    updRow.ReturnsZip = data.ReturnsZip;
                    updRow.ReturnsCountry = data.ReturnsCountry;
                    updRow.ReturnsDestroyFlag = data.ReturnsDestroyFlag;
                    updRow.ReturnsCreateLabelFlag = (bool?)data.ReturnsCreateLabelFlag;
                    updRow.WPKSandboxUsername = data.WPKSandboxUsername;
                    updRow.WPKSandboxPwd = data.WPKSandboxPwd;
                    updRow.WPKProdUsername = data.WPKProdUsername;
                    updRow.WPKProdPwd = data.WPKProdPwd;
                    updRow.WPKCustomExportFlag = (bool)data.WPKCustomExportFlag;
                    updRow.WPKGhostScanFlag = (bool)data.WPKGhostScanFlag;
                    updRow.WPKEastWestSplitFlag = (bool)data.WPKEastWestSplitFlag;
                    updRow.WPKAddressUploadFlag = (bool)data.WPKAddressUploadFlag;
                    updRow.WPKProductUploadFlag = (bool)data.WPKProductUploadFlag;
                    updRow.WPKEquipmentFlag = (bool?)data.WPKEquipmentFlag;
                    updRow.WPKDataEntryMethod = data.WPKDataEntryMethod;
                    updRow.EWSelectBy = data.EWSelectBy;
                    updRow.EWSortCodeFlag = (bool?)data.EWSortCodeFlag;
                    updRow.EWEastSortCode = data.EWEastSortCode;
                    updRow.EWWestSortCode = data.EWWestSortCode;
                    updRow.EWSepCloseoutFlag = data.EWSepCloseoutFlag;
                    updRow.EWSepPUFlag = data.EWSepPUFlag;
                    updRow.EWSortDetails = data.EWSortDetails;
                    updRow.EWMissortDetails = data.EWMissortDetails;
                    updRow.CurrentGoLive = (DateTime?)data.CurrentGoLive;
                    updRow.PhaseChangeDate = (DateTime?)data.PhaseChangeDate;
                    updRow.idRequestType = (int?)data.idRequestType;
                    updRow.idSolutionType = (int)data.idSolutionType;
                    updRow.CurrentlyShippingFlag = (bool?)data.CurrentlyShippingFlag;
                    updRow.idShippingVendor = (int?)data.idShippingVendor;
                    updRow.OtherVendorName = data.OtherVendorName;
                    updRow.idBroker = (int?)data.idBroker;
                    updRow.OtherBrokerName = data.OtherBrokerName;
                    updRow.idVendorType = (int?)data.idVendorType;
                    updRow.Route = data.Route;
                    updRow.FreightAuditor = (bool?)data.FreightAuditor;
                    updRow.EDIDetails = data.EDIDetails;
                    updRow.idEDISpecialist = (int?)data.idEDISpecialist;
                    updRow.idBillingSpecialist = (int?)data.idBillingSpecialist;
                    updRow.idCollectionSpecialist = (int?)data.idCollectionSpecialist;
                    updRow.AuditorPortal = (bool?)data.AuditorPortal;
                    updRow.AuditorURL = data.AuditorURL;
                    updRow.AuditorUserName = data.AuditorUserName;
                    updRow.AuditorPassword = data.AuditorPassword;
                    updRow.EDITargetGoLive = (DateTime?)data.EDITargetGoLive;
                    updRow.EDICurrentGoLive = (DateTime?)data.EDICurrentGoLive;
                    updRow.EDIActualGoLive = (DateTime?)data.EDIActualGoLive;
                    updRow.idEDIOnboardingPhase = data.idEDIOnboardingPhase;
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
    public string deActivateDiscoveryRequest(Int32 requestID, string userName)
    {
        string errMsg = "";
        PuroTouchSQLDataContext puroTouchContext = new PuroTouchSQLDataContext();
        try
        {
            if (requestID > 0)
            {
                // Query the database for the row to be updated. 
                var query =
                    from qdata in puroTouchContext.GetTable<tblDiscoveryRequest>()
                    where qdata.idRequest == requestID
                    select qdata;

                // Execute the query, and change the column values 
                // you want to change. 
                foreach (tblDiscoveryRequest updRow in query)
                {
                    updRow.ActiveFlag = false;
                    updRow.UpdatedBy = userName;
                    updRow.UpdatedOn=DateTime.Now;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Drawing;
using DAL;

public partial class View : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ClsDiscoveryRequest request = new ClsDiscoveryRequest();
        ClsDiscoveryRequestDetails requestDetails = new ClsDiscoveryRequestDetails();
        ClsDiscoveryRequestDetails requestDetailsWest = new ClsDiscoveryRequestDetails();

        
        if (Request.QueryString["requestID"] != null)
        {          
           
            try
            {
                int requestID = Convert.ToInt32(Request.QueryString["requestID"].Trim());
                ClsDiscoveryRequest dr = new ClsDiscoveryRequest();
                request = dr.GetDiscoveryRequest(requestID);
                ClsDiscoveryRequestDetails drd = new ClsDiscoveryRequestDetails();
                requestDetails = drd.GetDiscoveryRequestDetails(requestID,"");
                if (request.WPKEastWestSplitFlag == true)
                {
                    requestDetailsWest = drd.GetDiscoveryRequestDetails(requestID, "West");
                }
                fillInData(request,requestDetails,requestDetailsWest);                        
               
            }
            catch (Exception ex)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = ex.Message.ToString();
            }

        }
    }

    protected void fillInData(ClsDiscoveryRequest request, ClsDiscoveryRequestDetails requestDetails, ClsDiscoveryRequestDetails requestDetailsWest)
    {
        //HEADER
        string rtype = "";
        if (request.flagNewRequest == false)
        {
            rtype = "Existing Relationship";
        }
        else
        {
            rtype = "New Relationship";
        }
        lblCustomerName.Text = "" + request.CustomerName + "  - " + rtype;
        lblSalesProfessional.Text = request.SalesRepName + " - " + request.District + " District" + " [" + request.SalesRepEmail + "]";
        lblSubmittedBy.Text = request.CreatedBy;
        lblUpdatedBy.Text = request.UpdatedBy;
        lblUpdatedOn.Text = request.UpdatedOn.ToString();

        //CUSTOMER INFO        
        lblAnnualRevenue.Text = request.ProjectedRevenue.ToString("$###,###,###.00");
        lblAdress.Text = request.Address;
        lblCommodity.Text = request.Commodity;        
        lblCityStateZip.Text = request.City + ", " + request.State + " " + request.Zipcode + " " + request.Country;
        if (request.CustomerWebsite == "")
        {
            lblWebsitel.Visible = false;
        }
        else
        {
            lblWebsite.Text = request.CustomerWebsite;
        }
        

        //CONTACT INFO
        //lblBusContactName.Text = request.CustomerBusContact;
        //lblITContactName.Text = request.CustomerITContact;
        //lblBusTitle.Text = request.CustomerBusTitle;
        //lblITtitle.Text = request.CustomerITTitle;
        //lblBusPhone.Text = request.CustomerBusPhone;
        //lblITPhone.Text = request.CustomerITPhone;
        //lblBusEmail.Text = request.CustomerBusEmail;
        //lblITEmail.Text = request.CustomerITEmail;

        //CURRENT SOLUTION
        lblCurrentSolution.Text = request.CurrentSolution;

        //PROPOSED SERVICES
        if (request.SalesComments != "" && request.SalesComments != null)
        {
            lblSalesNotesl.Visible = true;
            lblSalesNotes.Visible = true;
            lblSalesNotes.Text = request.SalesComments;
        }        
        lblCustoms.Text = request.ProposedCustoms;
        String potentialDates = "";
        DateTime pdate;
        if (request.CallDate1 != null)
        {
            pdate = (DateTime)request.CallDate1;
            potentialDates = pdate.ToString("MM/dd/yyyy");
        }
        if (request.CallDate2 != null)
        {
            pdate = (DateTime)request.CallDate2;
            potentialDates = potentialDates + " or " + pdate.ToString("MM/dd/yyyy");
        }
        if (request.CallDate3 != null)
        {
            pdate = (DateTime)request.CallDate3;
            potentialDates = potentialDates + " or " + pdate.ToString("MM/dd/yyyy");
        }
        if (potentialDates != "")
        {
            lblCallDatesl.Visible = true;
            lblCallDates.Visible = true;
            lblCallDates.Text = potentialDates;
        }
        

        //PROFILE
        lblITBA.Text = "ITBA: " + request.ITBA;
        string TargetDate = "";
        if (request.TargetGoLive != null)
        {
            DateTime td;
            td = (DateTime)request.TargetGoLive;
            TargetDate = TargetDate + td.ToString("MM/dd/yyyy");
        }
        else
        {
            lblTargetGoLivel.Visible = false;
        }
        if (request.ActualGoLive != null)
        {
            DateTime ad;
            ad = (DateTime)request.ActualGoLive;
            //TargetDate = " - Actual Go-Live Date: " + ad.ToString("MM/dd/yyyy");
            lblActualGoLive.Visible = true;
            lblGoLiveDate.Text = ad.ToString("MM/dd/yyyy");
        }
        lblTargetGoLive.Text = TargetDate;
        if (request.SolutionSummary == "")
        {
            lblSolutionSummaryl.Visible = false;
        }
        else
        {
            lblSolutionSummary.Text = request.SolutionSummary;
        }
        
        lblPhase.Text = request.OnboardingPhase;
        if (request.ShippingChannel == null) request.ShippingChannel = "";
        if(request.ShippingChannel != "")
        {
            lblShippingChannel.Text = request.ShippingChannel;
        }
        else
        {
            lblShippingChannell.Visible = false;
        }
                
        //WPK
        if (request.worldpakFlag == true)
        {
            lblWPK.Text="WorkPak Solution";
        }
        //else
        //{
        //    if (request.worldpakFlag != null)
        //       lblWPK.Text = "NON-WorldPak Solution";
        //}
        string othertype = "";
        if (request.thirdpartyFlag == true)
        {
            othertype = "Third Party Vendor";
        }
        if (request.ThirdPartyVendor != null && request.ThirdPartyVendor != "")
        {
            othertype = othertype + " - " + request.ThirdPartyVendor;
        }
        if (request.customFlag == true)
        {
            othertype = othertype + " *Customized Solution*";
        }
        lblOtherType.Text = othertype;
        

        //proposed services
        PuroTouchRepository rep = new PuroTouchRepository();
        List<ClsDiscoveryRequestSvcs> svcList = rep.GetProposedServices(request.idRequest);
        string serviceList="";
        serviceList = "<table><tr><td style='text-decoration:underline'>Service</td><td style='text-decoration:underline'>Volume</td></tr>";
        foreach (ClsDiscoveryRequestSvcs svc in svcList)
        {
            serviceList = serviceList + "<tr><td >" + svc.serviceDesc + "</td><td>" + svc.volume.ToString() + "</td></tr>";
        }
        serviceList = serviceList + "</table>";
        lblProposedServices.Text = serviceList;

        //customs
        if (request.customsFlag == null) request.customsFlag = false;
        if (request.customsFlag == false)
        {
            lblCustomsNotSupported.Visible=true;
            lblBrokerName.Visible = false;
        }
        else
        {
            lblCustomsNotSupported.Visible = false;
            request.customsBroker = request.customsBroker.Trim();
            if (request.customsBroker != null && request.customsBroker != "")
            {
                lblCustomsNotSupported.Visible = false;
                lblBrokerName.Visible = false;
                if (request.customsBroker != "")
                {
                    lblBrokerName.Visible = true;
                    lblCustomsBroker.Text = request.customsBroker;
                }                
            }
            if (request.BrokerNumber != null && request.BrokerNumber != "")
            {
                lblBrokerNumberl.Visible = true;
                lblBrokerNumber.Visible = true;
                lblBrokerNumber.Text = request.BrokerNumber;
            }
            if (request.elinkFlag == null) request.elinkFlag = false;
            if (request.elinkFlag == true)
            {
                lblElinkSupported.Text = "ELINK Supported";
            }
            if (request.PARS != null && request.PARS.Trim() != "")
            {
                lblPars.Text = "PARS#: " + request.PARS;
            }
            if (request.PASS != null && request.PASS.Trim() != "")
            {
                lblPass.Text = "PASS#: " + request.PASS;
            }
        }

        //proposed products
        List<ClsDiscoveryRequestProds> prodList = rep.GetProposedProducts(request.idRequest);
        if (prodList.Count > 0)
        {
            string productList = "";
            productList = "<table>";
            foreach (ClsDiscoveryRequestProds prd in prodList)
            {
                productList = productList + "<tr><td >" + prd.productDesc + "</td></tr>";
            }
            productList = productList + "</table>";
            lblProducts.Text = productList;
        }
        else
        {
            lblProposedProd.Visible = false;
        }
        

        //equipment needed
        if (request.WPKEquipmentFlag != true)
        {
            lblequipneeded.Visible = false;
        }
        else
        {
            List<ClsDiscoveryRequestEquip> equipList = rep.GetProposedEquipment(request.idRequest);
            string equipmentList = "";
            equipmentList = "<table>";
            foreach (ClsDiscoveryRequestEquip equip in equipList)
            {
                equipmentList = equipmentList + "<tr><td >" + equip.EquipmentDesc + "</td><td>" + equip.number.ToString() + "</td></tr>";
            }
            equipmentList = equipmentList + "</table>";
            lblEquipment.Text = equipmentList;
        }
        

        //Billing
        if (request.invoiceType == null) request.invoiceType = "";
        if (request.billtoAccount == null) request.billtoAccount = "";
        if (request.invoiceType != "" || request.billtoAccount != "")
        {
            lblBillingInfo.Visible = true;
            lblInvoiceTypel.Visible = true;
            lblInvoiceType.Text = request.invoiceType;
            lblBillTol.Visible = true;
            lblBillTo.Text = request.billtoAccount;
            if (request.EDICustomizedFlag == true)
            {
                lblInvoiceType.Text = lblInvoiceType.Text + " *Custom EDI*";
            }
        }
      
       
        //EDI
        if (request.invoiceType == "EDI")
        {
            //show EDI requests
            List<ClsDiscoveryRequestEDI> edilst = rep.GetProposedEDI(request.idRequest);
            string ediList = "";
            ediList = "<table>";
            foreach (ClsDiscoveryRequestEDI edi in edilst)
            {
                ediList = ediList + "<tr><td >" + edi.Solution + "</td><td>" + edi.FileFormat +  " / " + edi.CommunicationMethod + "</td></tr>";
            }            
            if (request.FTPUsername != null)
            {
                ediList = ediList + "<tr><td colspan='2'>FTP Username: " + request.FTPUsername + "</td></tr>";
                ediList = ediList + "<tr><td colspan='2'>FTP Password: " + request.FTPPassword + "</td></tr>";
            }
            ediList = ediList + "</table>";
            lblEDIRequests.Text = ediList;
            lblEDIRequests.Visible = true;
        }
        else
        {
            lblEdiLabel.Visible = false;
        }

        //WorldPak
        if (request.worldpakFlag == false)
        {
            lblWPKDetails.Visible = false;
            lblDataEntrylbl.Visible = false;
            lblSandboxlbl.Visible = false;
            lblProdCredlbl.Visible = false;
        }
        else
        {
            lblWPKDetails.Visible = false;
            if (request.WPKDataEntryMethod == null) request.WPKDataEntryMethod = "";
            if (request.WPKSandboxUsername == null) request.WPKSandboxUsername = "";
            if (request.WPKSandboxPwd == null) request.WPKSandboxPwd = "";
            if (request.WPKProdUsername == null) request.WPKProdUsername = "";
            if (request.WPKProdPwd == null) request.WPKProdPwd = "";
            string otherText = "";
            if (request.WPKCustomExportFlag == true)
                otherText = otherText + " *Custom Export File* ";
            if (request.WPKAddressUploadFlag == true)
                otherText = otherText + " *Address Book Upload* ";
            if (request.WPKProductUploadFlag == true)
                otherText = otherText + " *Product File Upload* ";
            if (request.WPKGhostScanFlag == true)
                otherText = otherText + " *Ghost Scan* ";
            if (request.WPKEastWestSplitFlag == true)
                otherText = otherText + " *East/West Split* ";
            if (request.DataScrubFlag == true)
                otherText = otherText + " *Data Scrubbing* ";
            if (request.WPKDataEntryMethod != "" || request.WPKSandboxUsername != "" || request.WPKProdUsername != "" || otherText != "")
            {
                lblWPKDetails.Visible = true;
            }  
            if (request.WPKDataEntryMethod != "")
            {
                lblDataEntrylbl.Visible = true;
                lbldataEntry.Text = request.WPKDataEntryMethod;
            }          
            if (request.WPKSandboxUsername != "")
            {
                lblSandboxlbl.Visible = true;
                lblSandbox.Text = request.WPKSandboxUsername + " / " + request.WPKSandboxPwd;
            }                
            if (request.WPKProdUsername != "")
            {
                lblProdCredlbl.Visible = true;
                lblWPKProd.Text = request.WPKProdUsername + " / " + request.WPKProdPwd;
            }                
            
            lblWPKOther.Text = otherText;
        }
        //East West Splits
        if (request.WPKEastWestSplitFlag == true)
        {
            lblEWSplitl.Visible = true;
            lblewselectl.Visible = true;
            lblewselect.Visible = true;
            lblewselect.Text = request.EWSelectBy;
            string ewflags = "";
            if (request.EWSortCodeFlag == true)
            {
                ewflags = ewflags + "*Using Sort Codes*";
                lblewesrtl.Visible = true;
                lblewesrt.Visible = true;
                lblewesrt.Text = request.EWEastSortCode;
                lblewwsrtl.Visible = true;
                lblewwsrt.Visible = true;
                lblewwsrt.Text = request.EWWestSortCode;
            }
            if (request.EWSepCloseoutFlag == true)
            {
                ewflags = ewflags + " *Separate Closeout*";
            }
            if (request.EWSepPUFlag == true)
            {
                ewflags = ewflags + " *Separate Pick Up*";
            }
            lblewflags.Visible = true;
            lblewflags.Text = ewflags;
            lblewsortdetailsl.Visible = true;
            lblewmissortl.Visible = true;
            lblewsortdetails.Visible = true;
            lblewmissort.Visible = true;
            lblewsortdetails.Text = request.EWSortDetails;
            lblewmissort.Text = request.EWMissortDetails;
        }
        
        //Contract Info
        if (request.ContractNumber == null) request.ContractNumber = "";
        if (request.ContractCurrency == null) request.ContractCurrency = "";
        if(request.ContractNumber == "" && request.ContractStartDate == null && request.ContractEndDate == null && request.ContractCurrency =="")
        {
            lblContractNuml.Visible = false;
            lblConStartl.Visible = false;
            lblConEndl.Visible = false;
            lblConCurl.Visible = false;
            lblPaymentTermsl.Visible = false;
            lblContractInfo.Visible = false;
        }
        else
        {
            lblContractNum.Text = request.ContractNumber;
            if (request.ContractStartDate != null)
            {
                DateTime startdate = Convert.ToDateTime(request.ContractStartDate);
                lblConStart.Text = startdate.ToString("MM/dd/yyyy");
            }
            if (request.ContractStartDate != null)
            {
                DateTime enddate = Convert.ToDateTime(request.ContractEndDate);
                lblConEnd.Text = enddate.ToString("MM/dd/yyyy");
            }
            lblConCur.Text = request.ContractCurrency;
            lblPaymentTerms.Text = request.PaymentTerms;
        }
       

        //Account SUpport
        if (request.ControlBranch == null) request.ControlBranch = "";
        if (request.SupportUser == null) request.SupportUser = "";
        if (request.SupportGroup == null) request.SupportGroup = "";
        if (request.Office == null) request.Office = "";
        if (request.Group == null) request.Group = "";
        if (request.CRR == null) request.CRR = "";
        if (request.ControlBranch == "" && request.SupportUser == "" && request.SupportGroup == "" && request.Office == "" && request.Group == "" && request.CRR == "")
        {
            lblControlBranchl.Visible = false;
            lblSupportUserl.Visible = false;
            lblSupportGroupl.Visible = false;
            lblOfficel.Visible = false;
            lblGroupl.Visible = false;
            lblCRRl.Visible = false;
            lblAcctSupport.Visible = false;
        }
        else
        {
            lblControlBranch.Text = request.ControlBranch;
            lblSupportUser.Text = request.SupportUser;
            lblSupportGroup.Text = request.SupportGroup;
            lblOffice.Text = request.Office;
            lblGroup.Text = request.Group;
            lblCRR.Text = request.CRR;
        }
       

        //Migration Details
        if (request.PreMigrationSolution == null) request.PreMigrationSolution = "";
        if (request.PostMigrationSolution == null) request.PostMigrationSolution = "";
        if (request.MigrationDate != null || request.PreMigrationSolution != "" || request.PostMigrationSolution != "")
        {
            lblMigration.Visible = true;
            if (request.MigrationDate != null)
            {
                DateTime mdate = Convert.ToDateTime(request.MigrationDate);
                lblMigrationDate.Text = mdate.ToString("MM/dd/yyyy");
                lblMigrationDate.Visible = true;
                lblMigDate.Visible = true;
            }
            lblPre.Visible = true;
            lblPremigration.Visible = true;
            lblPremigration.Text = request.PreMigrationSolution;
            lblPost.Visible = true;
            lblPostmigration.Visible = true;
            lblPostmigration.Text = request.PostMigrationSolution;
        }

        //SHIPPINT DETAILS
        //CanadaPost
        if (requestDetails.CPCAcctNbr != "" || requestDetails.CPCContractNbr != "" || requestDetails.CPCSiteNbr != "" || requestDetails.CPCInductionNbr != "" || requestDetails.CPCUsername != "" || requestDetails.CPCpwd != "")
        {
            lblCPCDetails.Visible = true;
            lblCPCAcctl.Visible = true;
            lblCPCAcct.Visible = true;
            lblCPCAcct.Text = requestDetails.CPCAcctNbr;
            lblCPCContractl.Visible = true;
            lblCPCContract.Visible = true;
            lblCPCContract.Text = requestDetails.CPCContractNbr;
            lblCPCSitel.Visible = true;
            lblCPCSite.Visible = true;
            lblCPCSite.Text = requestDetails.CPCSiteNbr;
            lblCPCIndl.Visible = true;
            lblCPCInd.Visible = true;
            lblCPCInd.Text = requestDetails.CPCInductionNbr;
            lblCPCUserl.Visible = true;
            lblCPCUser.Visible = true;
            lblCPCUser.Text = requestDetails.CPCUsername;
            lblCPCPwdl.Visible = true;
            lblCPCPwd.Visible = true;
            lblCPCPwd.Text = requestDetails.CPCpwd;
        }        

        //Courier
        if (requestDetails.CourierAcctNbr != "" || requestDetails.CourierContractNbr != "" || requestDetails.CourierPinPrefix != "" || requestDetails.CourierTransitDays > 0 || requestDetails.CourierInductionAddress != "")
        {
            lblCourierDetail.Visible = true;
            lblCourierAcctl.Visible = true;
            lblCourierAcct.Visible = true;
            lblCourierAcct.Text = requestDetails.CourierAcctNbr;
            lblCourierContractl.Visible = true;
            lblCourierContract.Visible = true;
            lblCourierContract.Text = requestDetails.CourierContractNbr;
            lblCourierPinl.Visible = true;
            lblCourierPin.Visible = true;
            lblCourierPin.Text = requestDetails.CourierPinPrefix;
            lblCourierTransitl.Visible = true;
            lblCourierTransit.Visible = true;
            lblCourierTransit.Text = requestDetails.CourierTransitDays.ToString();
            lblCourierIndl.Visible = true;
            lblCourierInd.Visible = true;
            lblCourierInd.Text = requestDetails.CourierInductionDesc;
            lblCourierIndAddress.Visible = true;
            lblCourierIndAddress.Text = requestDetails.CourierInductionAddress;
            lblCourierIndCSZ.Visible = true;
            lblCourierIndCSZ.Text = requestDetails.CourierInductionCity;
            if (requestDetails.CourierInductionState != "")
            {
                lblCourierIndCSZ.Text = lblCourierIndCSZ.Text + ", " + requestDetails.CourierInductionState + " " + requestDetails.CourierInductionZip + " " + requestDetails.CourierInductionCountry;
            }
            lblCourierftpuserl.Visible = true;
            lblCourierftpuser.Visible = true;
            lblCourierftpuser.Text = requestDetails.CourierFTPusername;
            lblCourierftppwdl.Visible = true;
            lblCourierftppwd.Visible = true;
            lblCourierftppwd.Text = requestDetails.CourierFTPpwd;
            lblCourierftpsenderl.Visible = true;
            lblCourierftpsender.Visible = true;
            lblCourierftpsender.Text = requestDetails.CourierFTPsenderID;
            if (requestDetails.CourierFTPCustOwnFlag == true)
            {
                lblCourierFTPFlag.Text = "*Customer Own FTP*";
                lblCourierFTPFlag.Visible=true;
            }
            if (requestDetails.CourierSandboxFTPusername != "" && requestDetails.CourierSandboxFTPusername != null)
            {
                lblCourierftpuserSandboxl.Visible = true;
                lblCourierftpuserSandbox.Visible = true;
                lblCourierftpuserSandbox.Text = requestDetails.CourierSandboxFTPusername;
                lblCourierftppwdSandboxl.Visible = true;
                lblCourierftppwdSandbox.Visible = true;
                lblCourierftppwdSandbox.Text = requestDetails.CourierSandboxFTPpwd;
            }           
        }

        //LTL
        if (requestDetails.LTLAcctNbr != "" || requestDetails.LTLMinProNbr != "" || requestDetails.LTLMaxProNbr != "")
        {
            lblLTLDetails.Visible = true;
            lblLTLAcctl.Visible = true;
            lblLTLAcct.Visible = true;
            lblLTLAcct.Text = requestDetails.LTLAcctNbr;
            lblLTLMinl.Visible = true;
            lblLTLMin.Visible = true;
            lblLTLMin.Text = requestDetails.LTLMinProNbr;
            lblLTLMaxl.Visible = true;
            lblLTLMax.Visible = true;
            lblLTLMax.Text = requestDetails.LTLMaxProNbr;
            lblLTLPinPrefixl.Visible = true;
            lblLTLPinPrefix.Visible = true;
            lblLTLPinPrefix.Text = requestDetails.LTLPinPrefix;
            if (requestDetails.LTLAutomatedFlag == true)
                lblLTLAutoFlag.Text = " *LTL Automated Process* ";
        }

        //PuroPost
        if (requestDetails.PPSTAcctNbr != "" || requestDetails.PPSTTransitDays>0 || requestDetails.PPSTInductionAddress != "")
        {
            lblPuroPostDetails.Visible = true;
            lblPPSTAcctl.Visible = true;
            lblPPSTAcct.Visible = true;
            lblPPSTAcct.Text = requestDetails.PPSTAcctNbr;
            lblPPSTTransitl.Visible = true;
            lblPPSTTransit.Visible = true;
            lblPPSTTransit.Text = requestDetails.PPSTTransitDays.ToString();
            lblPPSTInductionl.Visible = true;
            lblPPSTInduction.Visible = true;
            lblPPSTInduction.Text = requestDetails.PPSTInductionDesc;
            lblPPSTIndAddr.Visible = true;
            lblPPSTIndAddr.Text = requestDetails.PPSTInductionAddress;
            lblPPSTIndCSZ.Visible = true;
            lblPPSTIndCSZ.Text = requestDetails.PPSTInductionCity;
            if (requestDetails.PPSTInductionState != "")
            {
                lblPPSTIndCSZ.Text = lblPPSTIndCSZ.Text + ", " + requestDetails.PPSTInductionState + " " + requestDetails.PPSTInductionZip + " " + requestDetails.PPSTInductionCountry;
            }
        }

        //PuroPost Plus
        if (requestDetails.PPlusAcctNbr == null) requestDetails.PPlusAcctNbr = "";
        if (requestDetails.PPlusInductionAddress == null) requestDetails.PPlusInductionAddress = "";
        if (requestDetails.PPlusAcctNbr != "" || requestDetails.PPlusTransitDays > 0 || requestDetails.PPlusInductionAddress != "")
        {
            lblPuroPostPlusDetails.Visible = true;
            lblPPlusAcctl.Visible = true;
            lblPPlusAcct.Visible = true;
            lblPPlusAcct.Text = requestDetails.PPlusAcctNbr;
            lblPPlusTransitl.Visible = true;
            lblPPlusTransit.Visible = true;
            lblPPlusTransit.Text = requestDetails.PPlusTransitDays.ToString();
            lblPPlusInductionl.Visible = true;
            lblPPlusInduction.Visible = true;
            lblPPlusInduction.Text = requestDetails.PPlusInductionDesc;
            lblPPlusIndAddr.Visible = true;
            lblPPlusIndAddr.Text = requestDetails.PPlusInductionAddress;
            lblPPlusIndCSZ.Visible = true;
            lblPPlusIndCSZ.Text = requestDetails.PPlusInductionCity;
            if (requestDetails.PPlusInductionState != "")
            {
                lblPPlusIndCSZ.Text = lblPPlusIndCSZ.Text + ", " + requestDetails.PPlusInductionState + " " + requestDetails.PPlusInductionZip + " " + requestDetails.PPlusInductionCountry;
            }
        }

        //East West Splits
        if (request.WPKEastWestSplitFlag == true)
        {
            lblCPCDetails.Text = "Canada Post East";
            lblCourierDetail.Text = "Courier East";
            lblLTLDetails.Text = "LTL East";
            lblPuroPostDetails.Text = "PuroPost East";
            lblPuroPostPlusDetails.Text = "PuroPost Plus East";
            ClsDiscoveryRequestDetails drd = new ClsDiscoveryRequestDetails();

            //Courier West
            if (requestDetailsWest.CourierAcctNbr != "" || requestDetailsWest.CourierContractNbr != "" || requestDetailsWest.CourierPinPrefix != "" || requestDetailsWest.CourierTransitDays > 0 || requestDetailsWest.CourierInductionAddress != "")
            {
                lblCourierDetailWest.Visible = true;
                lblCourierAcctlWest.Visible = true;
                lblCourierAcctWest.Visible = true;
                lblCourierAcctWest.Text = requestDetailsWest.CourierAcctNbr;
                lblCourierContractlWest.Visible = true;
                lblCourierContractWest.Visible = true;
                lblCourierContractWest.Text = requestDetailsWest.CourierContractNbr;
                lblCourierPinlWest.Visible = true;
                lblCourierPinWest.Visible = true;
                lblCourierPinWest.Text = requestDetailsWest.CourierPinPrefix;
                lblCourierTransitlWest.Visible = true;
                lblCourierTransitWest.Visible = true;
                lblCourierTransitWest.Text = requestDetailsWest.CourierTransitDays.ToString();
                lblCourierIndlWest.Visible = true;
                lblCourierIndWest.Visible = true;
                lblCourierIndWest.Text = requestDetailsWest.CourierInductionDesc;
                lblCourierIndAddressWest.Visible = true;
                lblCourierIndAddressWest.Text = requestDetailsWest.CourierInductionAddress;
                lblCourierIndCSZWest.Visible = true;
                lblCourierIndCSZWest.Text = requestDetailsWest.CourierInductionCity;
                if (requestDetailsWest.CourierInductionState != "")
                {
                    lblCourierIndCSZWest.Text = lblCourierIndCSZWest.Text + ", " + requestDetailsWest.CourierInductionState + " " + requestDetailsWest.CourierInductionZip + " " + requestDetailsWest.CourierInductionCountry;
                }
                lblCourierftpuserlWest.Visible = true;
                lblCourierftpuserWest.Visible = true;
                lblCourierftpuserWest.Text = requestDetailsWest.CourierFTPusername;
                lblCourierftppwdlWest.Visible = true;
                lblCourierftppwdWest.Visible = true;
                lblCourierftppwdWest.Text = requestDetailsWest.CourierFTPpwd;
                lblCourierftpsenderlWest.Visible = true;
                lblCourierftpsenderWest.Visible = true;
                lblCourierftpsenderWest.Text = requestDetailsWest.CourierFTPsenderID;
                if (requestDetailsWest.CourierFTPCustOwnFlag == true)
                {
                    lblCourierFTPFlagWest.Text = "*Customer Own FTP*";
                    lblCourierFTPFlagWest.Visible = true;
                }
                if (requestDetailsWest.CourierSandboxFTPusername != "" && requestDetailsWest.CourierSandboxFTPusername != null)
                {
                    lblCourierftpuserSandboxlWest.Visible = true;
                    lblCourierftpuserSandboxWest.Visible = true;
                    lblCourierftpuserSandboxWest.Text = requestDetailsWest.CourierSandboxFTPusername;
                    lblCourierftppwdSandboxlWest.Visible = true;
                    lblCourierftppwdSandboxWest.Visible = true;
                    lblCourierftppwdSandboxWest.Text = requestDetailsWest.CourierSandboxFTPpwd;
                }
            }
            //LTL West
            if (requestDetailsWest.LTLAcctNbr != "" || requestDetailsWest.LTLMinProNbr != "" || requestDetailsWest.LTLMaxProNbr != "")
            {
                lblLTLDetailsWest.Visible = true;
                lblLTLAcctlWest.Visible = true;
                lblLTLAcctWest.Visible = true;
                lblLTLAcctWest.Text = requestDetails.LTLAcctNbr;
                lblLTLMinlWest.Visible = true;
                lblLTLMinWest.Visible = true;
                lblLTLMinWest.Text = requestDetails.LTLMinProNbr;
                lblLTLMaxlWest.Visible = true;
                lblLTLMaxWest.Visible = true;
                lblLTLMaxWest.Text = requestDetails.LTLMaxProNbr;
                lblLTLPinPrefixlWest.Visible = true;
                lblLTLPinPrefixWest.Visible = true;
                lblLTLPinPrefixWest.Text = requestDetails.LTLPinPrefix;
            }

            //CPC West
            if (requestDetailsWest.CPCAcctNbr != "" || requestDetailsWest.CPCContractNbr != "" || requestDetailsWest.CPCSiteNbr != "" || requestDetailsWest.CPCInductionNbr != "" || requestDetailsWest.CPCUsername != "" || requestDetailsWest.CPCpwd != "")
            {
                lblCPCDetailsWest.Visible = true;
                lblCPCAcctlWest.Visible = true;
                lblCPCAcctWest.Visible = true;
                lblCPCAcctWest.Text = requestDetailsWest.CPCAcctNbr;
                lblCPCContractlWest.Visible = true;
                lblCPCContractWest.Visible = true;
                lblCPCContractWest.Text = requestDetailsWest.CPCContractNbr;
                lblCPCSitelWest.Visible = true;
                lblCPCSiteWest.Visible = true;
                lblCPCSiteWest.Text = requestDetailsWest.CPCSiteNbr;
                lblCPCIndlWest.Visible = true;
                lblCPCIndWest.Visible = true;
                lblCPCIndWest.Text = requestDetailsWest.CPCInductionNbr;
                lblCPCUserlWest.Visible = true;
                lblCPCUserWest.Visible = true;
                lblCPCUserWest.Text = requestDetailsWest.CPCUsername;
                lblCPCPwdlWest.Visible = true;
                lblCPCPwdWest.Visible = true;
                lblCPCPwdWest.Text = requestDetailsWest.CPCpwd;
            }

            //PuroPost West
            if (requestDetailsWest.PPSTAcctNbr != "" || requestDetailsWest.PPSTTransitDays > 0 || requestDetailsWest.PPSTInductionAddress != "")
            {
                lblPuroPostDetailsWest.Visible = true;
                lblPPSTAcctlWest.Visible = true;
                lblPPSTAcctWest.Visible = true;
                lblPPSTAcctWest.Text = requestDetailsWest.PPSTAcctNbr;
                lblPPSTTransitlWest.Visible = true;
                lblPPSTTransitWest.Visible = true;
                lblPPSTTransitWest.Text = requestDetailsWest.PPSTTransitDays.ToString();
                lblPPSTInductionlWest.Visible = true;
                lblPPSTInductionWest.Visible = true;
                lblPPSTInductionWest.Text = requestDetailsWest.PPSTInductionDesc;
                lblPPSTIndAddrWest.Visible = true;
                lblPPSTIndAddrWest.Text = requestDetailsWest.PPSTInductionAddress;
                lblPPSTIndCSZWest.Visible = true;
                lblPPSTIndCSZWest.Text = requestDetailsWest.PPSTInductionCity;
                if (requestDetailsWest.PPSTInductionState != "")
                {
                    lblPPSTIndCSZWest.Text = lblPPSTIndCSZWest.Text + ", " + requestDetailsWest.PPSTInductionState + " " + requestDetailsWest.PPSTInductionZip + " " + requestDetailsWest.PPSTInductionCountry;
                }
            }

            //PuroPost Plus West
            if (requestDetailsWest.PPlusAcctNbr != "" || requestDetailsWest.PPlusTransitDays > 0 || requestDetailsWest.PPlusInductionAddress != "")
            {
                lblPuroPostPlusDetailsWest.Visible = true;
                lblPPlusAcctlWest.Visible = true;
                lblPPlusAcctWest.Visible = true;
                lblPPlusAcctWest.Text = requestDetailsWest.PPlusAcctNbr;
                lblPPlusTransitlWest.Visible = true;
                lblPPlusTransitWest.Visible = true;
                lblPPlusTransitWest.Text = requestDetailsWest.PPlusTransitDays.ToString();
                lblPPlusInductionlWest.Visible = true;
                lblPPlusInductionWest.Visible = true;
                lblPPlusInductionWest.Text = requestDetailsWest.PPlusInductionDesc;
                lblPPlusIndAddrWest.Visible = true;
                lblPPlusIndAddrWest.Text = requestDetailsWest.PPlusInductionAddress;
                lblPPlusIndCSZWest.Visible = true;
                lblPPlusIndCSZWest.Text = requestDetailsWest.PPlusInductionCity;
                if (requestDetailsWest.PPlusInductionState != "")
                {
                    lblPPlusIndCSZWest.Text = lblPPlusIndCSZWest.Text + ", " + requestDetailsWest.PPlusInductionState + " " + requestDetailsWest.PPlusInductionZip + " " + requestDetailsWest.PPlusInductionCountry;
                }
            }

        }
        //Returns
        if (request.ReturnsAcctNbr == null) request.ReturnsAcctNbr = "";
        if (request.ReturnsAcctNbr != "")
        {
            lblReturndDetals.Visible=true;
            lblReturnsAcctl.Visible=true;
            lblReturnsAcct.Visible = true;
            lblReturnsAcct.Text = request.ReturnsAcctNbr;
            lblReturnsAddressl.Visible = true;
            lblReturnsAddress.Visible = true;
            lblReturnsAddress.Text = request.ReturnsAddress;
            lblReturnsCSZ.Visible = true;
            lblReturnsCSZ.Text = request.ReturnsCity;
            if (request.ReturnsState != "")
            {
                lblReturnsCSZ.Text = lblReturnsCSZ.Text + ", " + request.ReturnsState + " " + request.ReturnsZip + " " + request.ReturnsCountry;
            }
            string flagText = "";
            if (request.ReturnsDestroyFlag == true)
                flagText = "*Returns Are Destroyed* ";
            if (request.ReturnsCreateLabelFlag == true)
                flagText = flagText + " *Returns Labels Created with Shipments*";
            if (flagText != "")
                lblReturnsFlags.Text = flagText;
            
            
        }
        

        //NOTES
        ClsNotes cn = new ClsNotes();        
        List<ClsNotes> notesList = rep.GetNotes(request.idRequest);
        if (notesList.Count > 0)
        {
            string noteString = "";
            noteString = "<table><tr style='background-color:gray'><td style='vertical-align:top;text-decoration:underline'>Date</td><td style='vertical-align:top;text-decoration:underline'>Entered By</td><td style='text-decoration:underline'>Note</td></tr>";
            DateTime notedate;
            string notevalue = "";
            string bgcolor = "";
            int num = 0;

            foreach (ClsNotes note in notesList)
            {
                num = num + 1;
                if (num % 2 == 1)
                {
                    bgcolor = "lightgray";
                }
                else
                {
                    bgcolor = "white";
                }
                notedate = (DateTime)note.noteDate;
                notevalue = note.publicNote;
                notevalue = notevalue.Replace("\n", "<br>");
                noteString = noteString + "<tr style='background-color:" + bgcolor + " !important; -webkit-print-color-adjust: exact'><td style='vertical-align:top;font-family:Calibri'>" + notedate.ToString("MM/dd/yyyy") + "</td><td style='vertical-align:top;font-family:Calibri'>" + note.CreatedBy + "</td><td style='color:blue;'>" + notevalue + "</td></tr>";
            }
            noteString = noteString + "</table>";
            lblNotes.Text = noteString;
        }
        else
        {
            lblNotesl.Visible = false;
        }
       
        
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "window.print()", true);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), "key", "window.close()", true);
    }

    protected string formatDate(DateTime? thisdate)
    {
        string sdt = "";
        if (!String.IsNullOrEmpty(thisdate.ToString()))
        {
            DateTime dt = Convert.ToDateTime(thisdate);
            sdt = dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        }
        return sdt;
    }

    
}
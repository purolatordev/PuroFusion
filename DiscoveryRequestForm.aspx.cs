using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using DAL;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using context = System.Web.HttpContext;

public partial class DiscoveryRequestForm2 : System.Web.UI.Page
{
    ClsDiscoveryRequest objDiscoveryRequest = new ClsDiscoveryRequest();
    ClsDiscoveryRequestDetails objDiscoveryRequestDetails = new ClsDiscoveryRequestDetails();
    ClsDiscoveryRequestSvcs objDiscoveryRequestSvcs = new ClsDiscoveryRequestSvcs();
    List<ClsDiscoveryRequestSvcs> listServices = new List<ClsDiscoveryRequestSvcs>();
    PuroTouchRepository repository = new PuroTouchRepository();
    Int16 ClosedID = Convert.ToInt16(ConfigurationManager.AppSettings["ClosedID"]);
    Int16 OnHoldID = Convert.ToInt16(ConfigurationManager.AppSettings["OnHoldID"]);
    UserControlParams ParamsFor210 = new UserControlParams();
    UserControlParams ParamsFor214 = new UserControlParams();
    UserControlParams ParamsForNonCourier210 = new UserControlParams();
    UserControlParams ParamsForNonCourier210Test = new UserControlParams();
    UserControlParams ParamsForNonCourier214 = new UserControlParams();
    UserControlParams ParamsForNonCourier214Test = new UserControlParams();
    UserControlParams ParamsForPuroPostStand = new UserControlParams();

    const int INVOICE_COURIER_EDI = 3;
    const int SHIPMENT_STATUS_COURIER_EDI = 4;
    const int INVOICE_NON_COURIER_EDI = 5;
    const int SHIPMENT_STATUS_NON_COURIER_EDI = 6;
    const int PUROPOST_NON_COURIER_EDI = 7;
    const int INVOICE_NON_COURIER_EDI_TEST = 8;
    const int SHIPMENT_STATUS_NON_COURIER_EDI_TEST = 9;
    const int SOLUTION_TYPE_SHIPPING = 0; 
    const int SOLUTION_TYPE_EDI = 1;
    const int SOLUTION_TYPE_BOTH = 2;
    bool bOneTimeContactGrid = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
        {
            Session["userName"] = ConfigurationManager.AppSettings["debugUser"];
            Session["appName"] = "PuroTouch";
            //Session["userRole"] = "ITAdmin";
            Session["userRole"] = ConfigurationManager.AppSettings["role"];
            btnDebugLoad.Visible = true;
            btnDebugLoadContactInfo.Visible = true;
            btnDebugLoadEDI.Visible = true;
            btnDebugLoadShipping.Visible = true;
            txtBoxMultiDebug.Visible = true;
            btnClearDebug.Visible = true;
        }

        if (Session["userName"] == null)
            Response.Redirect("Default.aspx");

        string username = Session["userName"].ToString();
        if (username != null && Session["appName"] != null)
        {
            string ID = Request.QueryString["requestID"];
            if (!String.IsNullOrEmpty(ID))
            {
                CourierEDI210(ID);
                CourierEDI214(ID);
                NonCourierEDI210(ID);
                NonCourierEDI210Test(ID);
                NonCourierEDI214(ID);
                NonCourierEDI214Test(ID);
                PuroPostStand(ID);
            }
            if (!IsPostBack)
            {
                //INITIAL DATA LOAD
                lblSubmittedBy.Text = username;
                getDistricts();
                getBranches();
                getCurrency();
                getRelationships();
                getITBAs();
                getEDISpecialists();
                getBillingSpecialists();
                getCollectionSpecialists();
                getShippingChannels();
                getOnboardingPhases();
                getEDIOnboardingPhases();
                getTaskTypes();
                getServices();
                getDataEntryMethods();
                getShippingVendors();
                getThirdPartyVendors();
                getBrokers();
                getEquipmentList();
                getInvoiceTypes();
                getFileTypes();
                getCommunicationMethods();
                getEDISolutions();
                getInductionPoints();
                getCustomsTypes();
                getCloseReasons();
                getRequestTypes();
                getVendorTypes();
                getSolutionTypes();
                //getFreightAuditors();

                dpNoteDate.SelectedDate = DateTime.Now;
                //Start with blank lists
                List<ClsDiscoveryRequestSvcs> svcList = new List<ClsDiscoveryRequestSvcs>();
                Session["proposedSvcList"] = svcList;
                getShippingProducts();
                List<ClsDiscoveryRequestEDI> ediList = new List<ClsDiscoveryRequestEDI>();
                Session["ediList"] = ediList;
                List<ClsDiscoveryRequestProds> prodList = new List<ClsDiscoveryRequestProds>();
                Session["productList"] = prodList;
                List<ClsDiscoveryRequestEquip> equipList = new List<ClsDiscoveryRequestEquip>();
                Session["equipmentList"] = equipList;
                List<clsContact> contactList = new List<clsContact>();
                Session["contactList"] = contactList;
                List<clsEDIShipMethod> EDIShipMethList = new List<clsEDIShipMethod>();
                Session["EDIShipMethList"] = EDIShipMethList;
                List<clsEDITransaction> EDITransList = new List<clsEDITransaction>();
                Session["EDITransList"] = EDITransList;
                List<clsFreightAuditorsDiscReq> FreightAuditorList = new List<clsFreightAuditorsDiscReq>();
                Session["FreightAuditorsList"] = FreightAuditorList;

                //save Original ITBA and EDISpecialist, later send email if changed
                Session["ITBA"] = "";
                Session["EDISpecialist"] = "";
                string requestID = Request.QueryString["requestID"];
                if (!String.IsNullOrEmpty(requestID))
                {
                    //edit mode
                    //If Existing Request, populate form
                    lblRequestID.Text = requestID;
                    displayExistingRequest(Convert.ToInt32(requestID));
                    btnNextTab1.Visible = false;
                    btnNextTab2.Visible = false;
                    btnNextTab3.Visible = false;
                    btnSubmit.Enabled = true;
                    btnSubmit.Visible = false;
                    btnSubmitChanges.Visible = true;
                    hideshowbars();
                }
                else
                {
                    //If New, disable tabs initially
                    lblRequestID.Text = "0";
                    txtEmail.Text = username + "@purolator.com";
                    txtSalesProfessional.Text = username.Replace(".", " ");
                    RadTabStrip1.Tabs[0].Visible = true;
                    RadTabStrip1.Tabs[1].Visible = true;
                    RadTabStrip1.Tabs[2].Visible = true;
                    RadTabStrip1.Tabs[3].Visible = true;
                    RadTabStrip1.Tabs[4].Visible = true;
                    RadTabStrip1.Tabs[5].Visible = false;
                    RadTabStrip1.Tabs[6].Visible = false;
                    RadTabStrip1.Tabs[7].Visible = false;
                    RadTabStrip1.Tabs[8].Visible = false;
                    RadTabStrip1.Tabs[0].Enabled = true;
                    RadTabStrip1.Tabs[1].Enabled = false;
                    RadTabStrip1.Tabs[2].Enabled = false;
                    RadTabStrip1.Tabs[3].Enabled = false;
                    RadTabStrip1.Tabs[4].Enabled = false;
                    RadTabStrip1.Tabs[5].Enabled = false;
                    RadTabStrip1.Tabs[6].Enabled = false;
                    RadTabStrip1.Tabs[7].Enabled = false;
                    RadTabStrip1.Tabs[8].Enabled = false;
                    RadTabStrip1.Tabs[0].Selected = true;
                    RadMultiPage1.SelectedIndex = 0;
                    rgNotesGrid.Visible = false;
                    btnSubmit.Enabled = false;
                    btnSubmit.Visible = true;
                    btnSubmitChanges.Visible = false;
                    hideshowbars();
                }
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }

        UpdateTabs();
    }

    private void UpdateTabs()
    {
        if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
        {
            txtBoxMultiDebug.Text += "UpdateTabs()\r\n";
        }
        #region Role Based Viewing
        string userRole = Session["userRole"].ToString().ToLower();
        if (userRole == "admin" || userRole == "itadmin" || userRole == "itba" || userRole == "itmanager")
        {
            lblInternalNotes.Visible = true;
            txtInternalNotes.Visible = true;
            lblInternalTask.Visible = true;
            lblInternalTaskAsk.Visible = true;
            rddlInternalType.Visible = true;
            lblInternalTimeSpent.Visible = true;
            lblInternalTimeSpentAsk.Visible = true;
            txtInternalTimeSpent.Visible = true;
            RadTabStrip1.Tabs[4].Visible = true;
            RadTabStrip1.Tabs[6].Visible = true;
        }
        else if (userRole == "sales")
        {
            RadTabStrip1.Tabs[9].Visible = false;
            RadTabStrip1.Tabs[9].Enabled = false;
        }
        #endregion
        string ID = Request.QueryString["requestID"];
        #region Solution Type Checking
        if (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_SHIPPING)
        {
            RadTabStrip1.Tabs[2].Visible = true;
            RadTabStrip1.Tabs[3].Enabled = false;
            RadTabStrip1.Tabs[3].Visible = false;
            RadTabStrip1.Tabs[4].Visible = true;

            if (userRole != "sales")
            {
                RadTabStrip1.Tabs[5].Enabled = true;
                RadTabStrip1.Tabs[5].Visible = true;
                RadTabStrip1.Tabs[4].Enabled = true;
            }
            btnSubmitEDIServices.Visible = false;

            RadTabStrip1.Tabs[6].Visible = false; // Courier EDI
            RadTabStrip1.Tabs[6].Enabled = false;
            RadTabStrip1.Tabs[7].Visible = false;// Non-Courier EDI
            RadTabStrip1.Tabs[7].Enabled = false;

            RadPanelBar1.Items.FindItemByText("EDI Summary").Visible = false;
            RadPanelBar1.Items.FindItemByText("Solution Summary").Visible = true;
        }
        else if (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_EDI)
        {
            if (userRole == "admin" || userRole == "itadmin" || userRole == "itba" || userRole == "itmanager")
            {
                RadTabStrip1.Tabs[2].Visible = true;
                RadTabStrip1.Tabs[3].Enabled = true;
            }
            else
                RadTabStrip1.Tabs[2].Visible = false;

            RadTabStrip1.Tabs[3].Visible = true;
            RadTabStrip1.Tabs[4].Visible = false;
            RadTabStrip1.Tabs[4].Enabled = false;

            if (String.IsNullOrEmpty(ID))
            {
                btnSubmitEDIServices.Visible = true;
                btnSubmitEDIServices.Enabled = true;
            }
            else
            {
                btnSubmitEDIServices.Visible = false;
                btnSubmitEDIServices.Enabled = false;
            }

            RadPanelBar1.Items.FindItemByText("EDI Summary").Visible = true;
            RadPanelBar1.Items.FindItemByText("Solution Summary").Visible = false;
        }
        else if (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_BOTH)
        {
            RadTabStrip1.Tabs[2].Visible = true;
            RadTabStrip1.Tabs[3].Visible = true;
            RadTabStrip1.Tabs[4].Visible = true;

            if (userRole != "sales")
            {
                RadTabStrip1.Tabs[5].Enabled = true;
                RadTabStrip1.Tabs[5].Visible = true;
                RadTabStrip1.Tabs[3].Enabled = true;
            }
            btnSubmitEDIServices.Visible = false;

            RadPanelBar1.Items.FindItemByText("EDI Summary").Visible = true;
            RadPanelBar1.Items.FindItemByText("Solution Summary").Visible = true;
        }
        #endregion
        #region Current Shipping Solution Text Area Check
        if ( string.IsNullOrEmpty(txtareaCurrentSolution.Text) )
        {
            btnNextTab3.Visible = true;
            btnNextTab3.Enabled = true;
        }
        else
        {
            btnNextTab3.Visible = false;
            btnNextTab3.Enabled = false;
        }
        #endregion

        #region EDIShipMethList Checking
        List<clsEDIShipMethod> EDIShipMethList = (List<clsEDIShipMethod>)Session["EDIShipMethList"];
        var qEDIShipMethList = EDIShipMethList.Select(f => f.MethodType).OrderBy(f=>f).ToList();
        if (qEDIShipMethList.Count == 3 && userRole != "sales")
        {
            RadTabStrip1.Tabs[7].Visible = true;
            RadTabStrip1.Tabs[7].Enabled = true;
            RadTabStrip1.Tabs[6].Visible = true;
            RadTabStrip1.Tabs[6].Enabled = true;
        }
        else if (qEDIShipMethList.Count > 0 && userRole != "sales")
        {
            var qListNonCourierEDI = qEDIShipMethList.Where(f => f.Contains("Freight") || f.Contains("PuroPost")).ToList();
            if (qListNonCourierEDI.Count > 0 )
            {
                RadTabStrip1.Tabs[7].Visible = true;
                RadTabStrip1.Tabs[7].Enabled = true;
            }
            else
            {
                RadTabStrip1.Tabs[7].Visible = false;
                RadTabStrip1.Tabs[7].Enabled = false;
            }
            var qListCourierEDI = qEDIShipMethList.Where(f => f.Contains("Courier") ).ToList();
            if (qListCourierEDI.Count > 0 )
            {
                RadTabStrip1.Tabs[6].Visible = true;
                RadTabStrip1.Tabs[6].Enabled = true;
            }
            else
            {
                RadTabStrip1.Tabs[6].Visible = false;
                RadTabStrip1.Tabs[6].Enabled = false;
            }
        }
        else
        {
            RadTabStrip1.Tabs[6].Visible = false;
            RadTabStrip1.Tabs[6].Enabled = false;
            RadTabStrip1.Tabs[7].Visible = false;
            RadTabStrip1.Tabs[7].Enabled = false;
        }
        #endregion

        //if (!String.IsNullOrEmpty(ID))
        //{
        //    btnEDISerivesSaveFile.Enabled = true;
        //}
        //else
        //{
        //    btnEDISerivesSaveFile.Enabled = false;
        //}
    }
    #region Debugging
    protected void btnPre_Load_Click(object sender, System.EventArgs e)
    {
        rddlDistrict.SelectedIndex = 1;
        rddlBranch.SelectedIndex = 1;
        rddlSolutionType.SelectedIndex = 1;
        rddlRequestType.SelectedIndex = 1;
        txtCustomerName.Text = "Pre Customer " + DateTime.Now.ToString();
        txtCustomerAddress.Text = "Pre Address " + DateTime.Now.ToString();
        txtCustomerZip.Text = "12121";
        txtCustomerCity.Text = "Pre City";
        txtCustomerState.Text = "NY";
        txtRevenue.Text = "100";
        txtCommodity.Text = "Pre commodity";
        //bOneTimeContactGrid = true;
        //contactGrid.Rebind();
        //List<ClsDiscoveryRequestSvcs> svcList = repository.GetProposedServices(7026);
        //Session["proposedSvcList"] = svcList;
        //rgSvcGrid.DataSource = svcList;
        //rgSvcGrid.Rebind();
    }
    protected void btnDebugLoadContactInfo_Click(object sender, System.EventArgs e)
    {
        bOneTimeContactGrid = true;
        contactGrid.Rebind();
        btnNextTab2.Visible = true;
        btnNextTab2.Enabled = true;
    }
    protected void btnDebugLoadShipping_Click(object sender, System.EventArgs e)
    {
        List<ClsDiscoveryRequestSvcs> svcList = repository.GetProposedServices(7026);
        Session["proposedSvcList"] = svcList;
        rgSvcGrid.DataSource = svcList;
        rgSvcGrid.Rebind();
    }
    protected void btnDebugLoadEDI_Click(object sender, System.EventArgs e)
    {
        bOneTimeContactGrid = true;
        gridShipmentMethods.Rebind();
        bOneTimeContactGrid = true;
        gridEDITransactions.Rebind();
        //List<ClsDiscoveryRequestSvcs> svcList = repository.GetProposedServices(7026);
        //Session["proposedSvcList"] = svcList;
        //gridShipmentMethods.DataSource = svcList;
    }
    
    protected void btnClearDebug_Click(object sender, System.EventArgs e)
    {
        txtBoxMultiDebug.Text = "";
    }
    #endregion
    #region User controls
    private void PuroPostStand(string ID)
    {
        if (IsPostBack)
        {
            clsEDITransaction EDITrans = new clsEDITransaction()
            {
                idEDITranscationType = PUROPOST_NON_COURIER_EDI,
                TotalRequests = string.IsNullOrEmpty(txtBxNumRecipNonCourierPuroPostStand.Text) ? 0 : int.Parse(txtBxNumRecipNonCourierPuroPostStand.Text),
                idRequest = int.Parse(ID),
                ActiveFlag = true,
                CreatedOn = DateTime.Now,
                CreatedBy = (string)(Session["userName"])
            };
            Int32 t = 0;
            SrvEDITransaction.Insert(EDITrans, out t);
        }
        clsEDITransaction EDIInvoiceTrans = SrvEDITransaction.GetAEDITransactionsByidRequest(Convert.ToInt32(ID), PUROPOST_NON_COURIER_EDI);
        if (EDIInvoiceTrans != null)
        {
            int iTotalRequest = EDIInvoiceTrans.TotalRequests;

            List<clsEDIRecipReq> qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqs(EDIInvoiceTrans.idEDITranscation);
            // Nothing there
            if (qEDIRecipReq.Count <= 0)
            {
                for (int i = 0; i < iTotalRequest; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIInvoiceTrans.idEDITranscation, RecipReqNum = i + 1, idRequest = EDIInvoiceTrans.idRequest, idEDITranscationType = EDIInvoiceTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }

            //Add new value
            else if (qEDIRecipReq.Count < iTotalRequest)
            {
                int iTotNewRecNum = iTotalRequest - qEDIRecipReq.Count;
                int iStartRecNum = qEDIRecipReq[qEDIRecipReq.Count - 1].RecipReqNum + 1;
                for (int i = 0; i < iTotNewRecNum; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIInvoiceTrans.idEDITranscation, RecipReqNum = iStartRecNum, idRequest = EDIInvoiceTrans.idRequest, idEDITranscationType = EDIInvoiceTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Remove value
            else if (qEDIRecipReq.Count > iTotalRequest)
            {
                int iTotalRecRemove = qEDIRecipReq.Count - iTotalRequest;
                for (int i = 1; i < iTotalRecRemove + 1; i++)
                {
                    int idEDIRecipReqs = qEDIRecipReq[qEDIRecipReq.Count - i].idEDIRecipReqs;
                    SrvEDIRecipReq.Remove(idEDIRecipReqs);
                }
            }
            ParamsForPuroPostStand = new UserControlParams(iTotalRequest, int.Parse(ID),UserControlParams.CourierType.Zero, true);
            List<int> EDIRecipReqs = SrvEDIRecipReq.GetEDIRecipReqsList(EDIInvoiceTrans.idEDITranscation);
            List<SrvEDIRecipReq.PassBack> passbacks = SrvEDIRecipReq.GetEDIRecipReqsList2(EDIInvoiceTrans.idEDITranscation);
            ParamsForPuroPostStand.EDIRecipReqs = EDIRecipReqs;
            ParamsForPuroPostStand.passbacks = passbacks;
            if (!IsPostBack)
                txtBxNumRecipNonCourierPuroPostStand.Text = EDIInvoiceTrans.TotalRequests.ToString();
            AddPuroPostStandDynamicControls(ParamsForPuroPostStand);
            SetCourierNonCourierPuroPostStandControls();
        }
    }
    private void NonCourierEDI210(string ID)
    {
        if (IsPostBack)
        {
            clsEDITransaction EDITrans = new clsEDITransaction()
            {
                idEDITranscationType = INVOICE_NON_COURIER_EDI,
                TotalRequests = string.IsNullOrEmpty(txtBxNumRecipNonCourier210.Text) ? 0 : int.Parse(txtBxNumRecipNonCourier210.Text),
                idRequest = int.Parse(ID),
                ActiveFlag = true,
                CreatedOn = DateTime.Now,
                CreatedBy = (string)(Session["userName"])
            };
            Int32 t = 0;
            SrvEDITransaction.Insert(EDITrans, out t);
        }
        clsEDITransaction EDIInvoiceTrans = SrvEDITransaction.GetAEDITransactionsByidRequest(Convert.ToInt32(ID), INVOICE_NON_COURIER_EDI);
        if (EDIInvoiceTrans != null)
        {
            int iTotalRequest = EDIInvoiceTrans.TotalRequests;

            List<clsEDIRecipReq> qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqs(EDIInvoiceTrans.idEDITranscation);
            // Nothing there
            if (qEDIRecipReq.Count <= 0)
            {
                for (int i = 0; i < iTotalRequest; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIInvoiceTrans.idEDITranscation, RecipReqNum = i + 1, idRequest = EDIInvoiceTrans.idRequest, idEDITranscationType = EDIInvoiceTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Add new value
            else if (qEDIRecipReq.Count < iTotalRequest)
            {
                int iTotNewRecNum = iTotalRequest - qEDIRecipReq.Count;
                int iStartRecNum = qEDIRecipReq[qEDIRecipReq.Count - 1].RecipReqNum + 1;
                for (int i = 0; i < iTotNewRecNum; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIInvoiceTrans.idEDITranscation, RecipReqNum = iStartRecNum, idRequest = EDIInvoiceTrans.idRequest, idEDITranscationType = EDIInvoiceTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Remove value
            else if (qEDIRecipReq.Count > iTotalRequest)
            {
                int iTotalRecRemove = qEDIRecipReq.Count - iTotalRequest;
                for (int i = 1; i < iTotalRecRemove + 1; i++)
                {
                    int idEDIRecipReqs = qEDIRecipReq[qEDIRecipReq.Count - i].idEDIRecipReqs;
                    SrvEDIRecipReq.Remove(idEDIRecipReqs);
                }
            }
            ParamsForNonCourier210 = new UserControlParams(iTotalRequest, int.Parse(ID), UserControlParams.CourierType.NonCourierEDI, true);
            List<int> EDIRecipReqs = SrvEDIRecipReq.GetEDIRecipReqsList(EDIInvoiceTrans.idEDITranscation);
            List<SrvEDIRecipReq.PassBack> passbacks = SrvEDIRecipReq.GetEDIRecipReqsList2(EDIInvoiceTrans.idEDITranscation);
            ParamsForNonCourier210.EDIRecipReqs = EDIRecipReqs;
            ParamsForNonCourier210.passbacks = passbacks;
            if (!IsPostBack)
                txtBxNumRecipNonCourier210.Text = EDIInvoiceTrans.TotalRequests.ToString();
            AddNonCourier210DynamicControls(ParamsForNonCourier210);
            SetCourierNonCourierEDI210Controls();
        }
    }
    private void NonCourierEDI210Test(string ID)
    {
        if (IsPostBack)
        {
            clsEDITransaction EDITrans = new clsEDITransaction()
            {
                idEDITranscationType = INVOICE_NON_COURIER_EDI_TEST,
                TotalRequests = string.IsNullOrEmpty(txtBxNumRecipNonCourier210Test.Text) ? 0 : int.Parse(txtBxNumRecipNonCourier210Test.Text),
                idRequest = int.Parse(ID),
                ActiveFlag = true,
                CreatedOn = DateTime.Now,
                CreatedBy = (string)(Session["userName"])
            };
            Int32 t = 0;
            SrvEDITransaction.Insert(EDITrans, out t);
        }
        clsEDITransaction EDIInvoiceTrans = SrvEDITransaction.GetAEDITransactionsByidRequest(Convert.ToInt32(ID), INVOICE_NON_COURIER_EDI_TEST);
        if (EDIInvoiceTrans != null)
        {
            int iTotalRequest = EDIInvoiceTrans.TotalRequests;

            List<clsEDIRecipReq> qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqs(EDIInvoiceTrans.idEDITranscation);
            // Nothing there
            if (qEDIRecipReq.Count <= 0)
            {
                for (int i = 0; i < iTotalRequest; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIInvoiceTrans.idEDITranscation, RecipReqNum = i + 1, idRequest = EDIInvoiceTrans.idRequest, idEDITranscationType = EDIInvoiceTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Add new value
            else if (qEDIRecipReq.Count < iTotalRequest)
            {
                int iTotNewRecNum = iTotalRequest - qEDIRecipReq.Count;
                int iStartRecNum = qEDIRecipReq[qEDIRecipReq.Count - 1].RecipReqNum + 1;
                for (int i = 0; i < iTotNewRecNum; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIInvoiceTrans.idEDITranscation, RecipReqNum = iStartRecNum, idRequest = EDIInvoiceTrans.idRequest, idEDITranscationType = EDIInvoiceTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Remove value
            else if (qEDIRecipReq.Count > iTotalRequest)
            {
                int iTotalRecRemove = qEDIRecipReq.Count - iTotalRequest;
                for (int i = 1; i < iTotalRecRemove + 1; i++)
                {
                    int idEDIRecipReqs = qEDIRecipReq[qEDIRecipReq.Count - i].idEDIRecipReqs;
                    SrvEDIRecipReq.Remove(idEDIRecipReqs);
                }
            }
            ParamsForNonCourier210Test = new UserControlParams(iTotalRequest, int.Parse(ID), UserControlParams.CourierType.NonCourierEDI, true);
            List<int> EDIRecipReqs = SrvEDIRecipReq.GetEDIRecipReqsList(EDIInvoiceTrans.idEDITranscation);
            List<SrvEDIRecipReq.PassBack> passbacks = SrvEDIRecipReq.GetEDIRecipReqsList2(EDIInvoiceTrans.idEDITranscation);
            ParamsForNonCourier210Test.EDIRecipReqs = EDIRecipReqs;
            ParamsForNonCourier210Test.passbacks = passbacks;
            if (!IsPostBack)
                txtBxNumRecipNonCourier210Test.Text = EDIInvoiceTrans.TotalRequests.ToString();
            AddNonCourier210TestDynamicControls(ParamsForNonCourier210Test);
            SetCourierNonCourierEDI210Controls();
        }
    }
    private void NonCourierEDI214(string ID)
    {
        if (IsPostBack)
        {
            clsEDITransaction EDITrans = new clsEDITransaction()
            {
                idEDITranscationType = SHIPMENT_STATUS_NON_COURIER_EDI,
                TotalRequests = string.IsNullOrEmpty(txtBxNumRecipNonCourier214.Text) ? 0 : int.Parse(txtBxNumRecipNonCourier214.Text),
                idRequest = int.Parse(ID),
                ActiveFlag = true,
                CreatedOn = DateTime.Now,
                CreatedBy = (string)(Session["userName"])
            };
            SrvEDITransaction.Insert(EDITrans);
        }
        clsEDITransaction EDIShipmentTrans = SrvEDITransaction.GetAEDITransactionsByidRequest(Convert.ToInt32(ID), SHIPMENT_STATUS_NON_COURIER_EDI);
        if (EDIShipmentTrans != null)
        {
            int iTotalRequest = EDIShipmentTrans.TotalRequests;
            List<clsEDIRecipReq> qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqs(EDIShipmentTrans.idEDITranscation);
            // Nothing there
            if (qEDIRecipReq.Count <= 0)
            {
                for (int i = 0; i < iTotalRequest; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIShipmentTrans.idEDITranscation, RecipReqNum = i + 1, idRequest = EDIShipmentTrans.idRequest, idEDITranscationType = EDIShipmentTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Add new value
            else if (qEDIRecipReq.Count < iTotalRequest)
            {
                int iTotNewRecNum = iTotalRequest - qEDIRecipReq.Count;
                int iStartRecNum = qEDIRecipReq[qEDIRecipReq.Count - 1].RecipReqNum + 1;
                for (int i = 0; i < iTotNewRecNum; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIShipmentTrans.idEDITranscation, RecipReqNum = iStartRecNum, idRequest = EDIShipmentTrans.idRequest, idEDITranscationType = EDIShipmentTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Remove value
            else if (qEDIRecipReq.Count > iTotalRequest)
            {
                int iTotalRecRemove = qEDIRecipReq.Count - iTotalRequest;
                for (int i = 1; i < iTotalRecRemove + 1; i++)
                {
                    int idEDIRecipReqs = qEDIRecipReq[qEDIRecipReq.Count - i].idEDIRecipReqs;
                    SrvEDIRecipReq.Remove(idEDIRecipReqs);
                }
            }
            ParamsForNonCourier214 = new UserControlParams(iTotalRequest, int.Parse(ID), UserControlParams.CourierType.NonCourierEDI, false);
            List<int> EDIRecipReqs = SrvEDIRecipReq.GetEDIRecipReqsList(EDIShipmentTrans.idEDITranscation);
            List<SrvEDIRecipReq.PassBack> passbacks = SrvEDIRecipReq.GetEDIRecipReqsList2(EDIShipmentTrans.idEDITranscation);
            ParamsForNonCourier214.EDIRecipReqs = EDIRecipReqs;
            ParamsForNonCourier214.passbacks = passbacks;
            //ParamsForNonCourier214.bUseTimeOfFile = false;
            //ParamsForNonCourier214.ct = UserControlParams.CourierType.NonCourierEDI;

            if (!IsPostBack)
                txtBxNumRecipNonCourier214.Text = EDIShipmentTrans.TotalRequests.ToString();
            AddNonCourier214DynamicControls(ParamsForNonCourier214);
            SetCourierNonCourierEDI214Controls();
        }
    }
    private void NonCourierEDI214Test(string ID)
    {
        if (IsPostBack)
        {
            clsEDITransaction EDITrans = new clsEDITransaction()
            {
                idEDITranscationType = SHIPMENT_STATUS_NON_COURIER_EDI_TEST,
                TotalRequests = string.IsNullOrEmpty(txtBxNumRecipNonCourier214Test.Text) ? 0 : int.Parse(txtBxNumRecipNonCourier214Test.Text),
                idRequest = int.Parse(ID),
                ActiveFlag = true,
                CreatedOn = DateTime.Now,
                CreatedBy = (string)(Session["userName"])
            };
            SrvEDITransaction.Insert(EDITrans);
        }
        clsEDITransaction EDIShipmentTrans = SrvEDITransaction.GetAEDITransactionsByidRequest(Convert.ToInt32(ID), SHIPMENT_STATUS_NON_COURIER_EDI_TEST);
        if (EDIShipmentTrans != null)
        {
            int iTotalRequest = EDIShipmentTrans.TotalRequests;
            List<clsEDIRecipReq> qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqs(EDIShipmentTrans.idEDITranscation);
            // Nothing there
            if (qEDIRecipReq.Count <= 0)
            {
                for (int i = 0; i < iTotalRequest; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIShipmentTrans.idEDITranscation, RecipReqNum = i + 1, idRequest = EDIShipmentTrans.idRequest, idEDITranscationType = EDIShipmentTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Add new value
            else if (qEDIRecipReq.Count < iTotalRequest)
            {
                int iTotNewRecNum = iTotalRequest - qEDIRecipReq.Count;
                int iStartRecNum = qEDIRecipReq[qEDIRecipReq.Count - 1].RecipReqNum + 1;
                for (int i = 0; i < iTotNewRecNum; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIShipmentTrans.idEDITranscation, RecipReqNum = iStartRecNum, idRequest = EDIShipmentTrans.idRequest, idEDITranscationType = EDIShipmentTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Remove value
            else if (qEDIRecipReq.Count > iTotalRequest)
            {
                int iTotalRecRemove = qEDIRecipReq.Count - iTotalRequest;
                for (int i = 1; i < iTotalRecRemove + 1; i++)
                {
                    int idEDIRecipReqs = qEDIRecipReq[qEDIRecipReq.Count - i].idEDIRecipReqs;
                    SrvEDIRecipReq.Remove(idEDIRecipReqs);
                }
            }
            ParamsForNonCourier214Test = new UserControlParams(iTotalRequest, int.Parse(ID), UserControlParams.CourierType.NonCourierEDI, false);
            List<int> EDIRecipReqs = SrvEDIRecipReq.GetEDIRecipReqsList(EDIShipmentTrans.idEDITranscation);
            List<SrvEDIRecipReq.PassBack> passbacks = SrvEDIRecipReq.GetEDIRecipReqsList2(EDIShipmentTrans.idEDITranscation);
            ParamsForNonCourier214Test.EDIRecipReqs = EDIRecipReqs;
            ParamsForNonCourier214Test.passbacks = passbacks;

            if (!IsPostBack)
                txtBxNumRecipNonCourier214Test.Text = EDIShipmentTrans.TotalRequests.ToString();
            AddNonCourier214TestDynamicControls(ParamsForNonCourier214Test);
            SetCourierNonCourierEDI214Controls();
        }
    }

    private void CourierEDI214(string ID)
    {
        if (IsPostBack)
        {
            clsEDITransaction EDITrans = new clsEDITransaction()
            {
                idEDITranscationType = SHIPMENT_STATUS_COURIER_EDI,
                TotalRequests = string.IsNullOrEmpty(txtBxNumberRecipients214.Text) ? 0 : int.Parse(txtBxNumberRecipients214.Text),
                idRequest = int.Parse(ID),
                ActiveFlag = true,
                CreatedOn = DateTime.Now,
                CreatedBy = (string)(Session["userName"])
            };
            SrvEDITransaction.Insert(EDITrans);
        }
        clsEDITransaction EDIShipmentTrans = SrvEDITransaction.GetAEDITransactionsByidRequest(Convert.ToInt32(ID), SHIPMENT_STATUS_COURIER_EDI);
        if (EDIShipmentTrans != null)
        {
            int iTotalRequest = EDIShipmentTrans.TotalRequests;
            List<clsEDIRecipReq> qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqs(EDIShipmentTrans.idEDITranscation);
            // Nothing there
            if (qEDIRecipReq.Count <= 0)
            {
                for (int i = 0; i < iTotalRequest; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIShipmentTrans.idEDITranscation, RecipReqNum = i + 1, idRequest = EDIShipmentTrans.idRequest, idEDITranscationType = EDIShipmentTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Add new value
            else if (qEDIRecipReq.Count < iTotalRequest)
            {
                int iTotNewRecNum = iTotalRequest - qEDIRecipReq.Count;
                int iStartRecNum = qEDIRecipReq[qEDIRecipReq.Count - 1].RecipReqNum + 1;
                for (int i = 0; i < iTotNewRecNum; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIShipmentTrans.idEDITranscation, RecipReqNum = iStartRecNum, idRequest = EDIShipmentTrans.idRequest, idEDITranscationType = EDIShipmentTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Remove value
            else if (qEDIRecipReq.Count > iTotalRequest)
            {
                int iTotalRecRemove = qEDIRecipReq.Count - iTotalRequest;
                for (int i = 1; i < iTotalRecRemove + 1; i++)
                {
                    int idEDIRecipReqs = qEDIRecipReq[qEDIRecipReq.Count - i].idEDIRecipReqs;
                    SrvEDIRecipReq.Remove(idEDIRecipReqs);
                }
            }
            ParamsFor214 = new UserControlParams(iTotalRequest, int.Parse(ID), UserControlParams.CourierType.CourierEDI, true);
            List<int> EDIRecipReqs = SrvEDIRecipReq.GetEDIRecipReqsList(EDIShipmentTrans.idEDITranscation);
            List<SrvEDIRecipReq.PassBack> passbacks = SrvEDIRecipReq.GetEDIRecipReqsList2(EDIShipmentTrans.idEDITranscation);
            ParamsFor214.EDIRecipReqs = EDIRecipReqs;
            ParamsFor214.passbacks = passbacks;

            if (!IsPostBack)
                txtBxNumberRecipients214.Text = EDIShipmentTrans.TotalRequests.ToString();
            AddAndRemoveDynamicControls2(ParamsFor214);
            SetCourierEDI214Controls();
        }
    }
    private void CourierEDI210(string ID)
    {
        if (IsPostBack)
        {
            clsEDITransaction EDITrans = new clsEDITransaction()
            {
                idEDITranscationType = INVOICE_COURIER_EDI,
                TotalRequests = string.IsNullOrEmpty(txtBxNumberRecipients210.Text) ? 0 : int.Parse(txtBxNumberRecipients210.Text),
                idRequest = int.Parse(ID),
                ActiveFlag = true,
                CreatedOn = DateTime.Now,
                CreatedBy = (string)(Session["userName"])
            };
            Int32 t = 0;
            SrvEDITransaction.Insert(EDITrans, out t);
        }
        clsEDITransaction EDIInvoiceTrans = SrvEDITransaction.GetAEDITransactionsByidRequest(Convert.ToInt32(ID), INVOICE_COURIER_EDI);
        if (EDIInvoiceTrans != null)
        {
            int iTotalRequest = EDIInvoiceTrans.TotalRequests;

            List<clsEDIRecipReq> qEDIRecipReq = SrvEDIRecipReq.GetEDIRecipReqs(EDIInvoiceTrans.idEDITranscation);
            // Nothing there
            if (qEDIRecipReq.Count <= 0)
            {
                for (int i = 0; i < iTotalRequest; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIInvoiceTrans.idEDITranscation, RecipReqNum = i + 1, idRequest = EDIInvoiceTrans.idRequest, idEDITranscationType = EDIInvoiceTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Add new value
            else if (qEDIRecipReq.Count < iTotalRequest)
            {
                int iTotNewRecNum = iTotalRequest - qEDIRecipReq.Count;
                int iStartRecNum = qEDIRecipReq[qEDIRecipReq.Count - 1].RecipReqNum + 1;
                for (int i = 0; i < iTotNewRecNum; i++)
                {
                    clsEDIRecipReq data = new clsEDIRecipReq() { idEDITranscation = EDIInvoiceTrans.idEDITranscation, RecipReqNum = iStartRecNum, idRequest = EDIInvoiceTrans.idRequest, idEDITranscationType = EDIInvoiceTrans.idEDITranscationType, idFileType = 0, X12_GS = "X12_GS", X12_ISA = "X12_ISA", X12_Qualifier = "X12_Qualifier", idCommunicationMethod = 0, FTPAddress = "FTPAddress", UserName = "UserName", Password = "Password", FolderPath = "FolderPath", Email = "Email", idTriggerMechanism = 0, idTiming = 0, TimeOfFile = DateTime.Now, EDITranscationType = "EDITranscationType", ActiveFlag = true, CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                    Int32 newID = 0;
                    SrvEDIRecipReq.Insert(data, out newID);
                }
            }
            // Remove value
            else if (qEDIRecipReq.Count > iTotalRequest)
            {
                int iTotalRecRemove = qEDIRecipReq.Count - iTotalRequest;
                for (int i = 1; i < iTotalRecRemove + 1; i++)
                {
                    int idEDIRecipReqs = qEDIRecipReq[qEDIRecipReq.Count - i].idEDIRecipReqs;
                    SrvEDIRecipReq.Remove(idEDIRecipReqs);
                }
            }
            ParamsFor210 = new UserControlParams(iTotalRequest, int.Parse(ID), UserControlParams.CourierType.CourierEDI, true);
            List<int> EDIRecipReqs = SrvEDIRecipReq.GetEDIRecipReqsList(EDIInvoiceTrans.idEDITranscation);
            List<SrvEDIRecipReq.PassBack> passbacks = SrvEDIRecipReq.GetEDIRecipReqsList2(EDIInvoiceTrans.idEDITranscation);
            ParamsFor210.EDIRecipReqs = EDIRecipReqs;
            ParamsFor210.passbacks = passbacks;
            if (!IsPostBack)
                txtBxNumberRecipients210.Text = EDIInvoiceTrans.TotalRequests.ToString();
            AddAndRemoveDynamicControls(ParamsFor210);
            SetCourierEDI210Controls();
        }
    }
    private void AddAndRemoveDynamicControls(UserControlParams p)
    {
        Control c = GetPostBackControl(Page);
        if ((c != null))
        {
            if (c.ID.ToString() == "btnAdd")
            {
                //p.xNames.Add((p.xTimes.Count + 1).ToString());
                //p.yNames.Add((p.xTimes.Count + 1).ToString());
                //p.xTimes.Add(p.xTimes.Count + 1);
            }
        }

        ltlCount.Text = p.iTotalRecs.ToString();

        ph1.Controls.Clear();
        int ControlID = 0;
        for (int i = 0; i <= (Convert.ToInt16(ltlCount.Text) - 1); i++)
        {
            EDI210 DynamicUserControl = (EDI210)LoadControl("EDI210.ascx");

            while (InDeletedList("uc1" + ControlID) == true)
            {
                ControlID += 1;
            }

            DynamicUserControl.ID = "uc1" + ControlID;
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            
            UserControlParams p1 = new UserControlParams() { idRequest = requestID, iRecordID = i, bNewDialog = p.passbacks[i].bNewRecord, idEDIRecipReqs = p.EDIRecipReqs[i], passbacks = p.passbacks, bUseTimeOfFile = p.bUseTimeOfFile }; 
            DynamicUserControl.LoadParams(p1);
            DynamicUserControl.RemoveUserControl += this.HandleRemoveUserControl;
            DynamicUserControl.UserControlSaved += this.HandleUserControl210Saved;
            ph1.Controls.Add(DynamicUserControl);
            ControlID += 1;
        }
    }
    private void AddAndRemoveDynamicControls2(UserControlParams p)
    {
        Control c = GetPostBackControl(Page);

        //ltlCount2.Text = p.xTimes.Count.ToString();
        ltlCount2.Text = p.iTotalRecs.ToString();

        ph2.Controls.Clear();
        int ControlID = 0;
        for (int i = 0; i <= (Convert.ToInt16(ltlCount2.Text) - 1); i++)
        {
            EDI214 DynamicUserControl = (EDI214)LoadControl("EDI214.ascx");

            while (InDeletedList2("uc" + ControlID) == true)
            {
                ControlID += 1;
            }

            DynamicUserControl.ID = "uc" + ControlID;
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            UserControlParams p1 = new UserControlParams() { idRequest = requestID, iRecordID = i, bNewDialog = p.passbacks[i].bNewRecord, idEDIRecipReqs = p.EDIRecipReqs[i], passbacks = p.passbacks, ct = p.ct, bUseTimeOfFile = p.bUseTimeOfFile };
            DynamicUserControl.LoadParams(p1);
            DynamicUserControl.RemoveUserControl2 += this.HandleRemoveUserControl2;
            DynamicUserControl.UserControlSaved += this.HandleUserControl214Saved;

            ph2.Controls.Add(DynamicUserControl);
            ControlID += 1;
        }
    }
    private void AddNonCourier210DynamicControls(UserControlParams p)
    {
        Control c = GetPostBackControl(Page);
        if ((c != null))
        {
            if (c.ID.ToString() == "btnAdd")
            {
            }
        }

        ltlCountNonCourier210.Text = p.iTotalRecs.ToString();

        placeNonCourier210.Controls.Clear();
        int ControlID = 0;
        for (int i = 0; i <= (Convert.ToInt16(ltlCountNonCourier210.Text) - 1); i++)
        {
            EDI210 DynamicUserControl = (EDI210)LoadControl("EDI210.ascx");

            while (InDeletedNonCourier210("uc3" + ControlID) == true)
            {
                ControlID += 1;
            }

            DynamicUserControl.ID = "uc3" + ControlID;
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);

            UserControlParams p1 = new UserControlParams() { idRequest = requestID, iRecordID = i, bNewDialog = p.passbacks[i].bNewRecord, idEDIRecipReqs = p.EDIRecipReqs[i], passbacks = p.passbacks, ct = p.ct, bUseTimeOfFile = p.bUseTimeOfFile };
            DynamicUserControl.LoadParams(p1);

            DynamicUserControl.RemoveUserControl += this.HandleRemoveUserControlNonCourier210;
            DynamicUserControl.UserControlSaved += this.HandleUserControlNonCourier210Saved;
            placeNonCourier210.Controls.Add(DynamicUserControl);
            ControlID += 1;
        }
    }
    private void AddNonCourier210TestDynamicControls(UserControlParams p)
    {
        Control c = GetPostBackControl(Page);
        if ((c != null))
        {
            if (c.ID.ToString() == "btnAdd")
            {
            }
        }

        ltlCountNonCourier210Test.Text = p.iTotalRecs.ToString();

        placeNonCourier210Test.Controls.Clear();
        int ControlID = 0;
        for (int i = 0; i <= (Convert.ToInt16(ltlCountNonCourier210Test.Text) - 1); i++)
        {
            EDI210 DynamicUserControl = (EDI210)LoadControl("EDI210.ascx");

            while (InDeletedNonCourier210Test("uc6" + ControlID) == true)
            {
                ControlID += 1;
            }

            DynamicUserControl.ID = "uc6" + ControlID;
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);

            UserControlParams p1 = new UserControlParams() { idRequest = requestID, iRecordID = i, bNewDialog = p.passbacks[i].bNewRecord, idEDIRecipReqs = p.EDIRecipReqs[i], passbacks = p.passbacks, bUseTimeOfFile = p.bUseTimeOfFile };
            DynamicUserControl.LoadParams(p1);

            DynamicUserControl.RemoveUserControl += this.HandleRemoveUserControlNonCourier210Test;
            DynamicUserControl.UserControlSaved += this.HandleUserControlNonCourier210TestSaved;
            placeNonCourier210Test.Controls.Add(DynamicUserControl);
            ControlID += 1;
        }
    }

    private void AddNonCourier214DynamicControls(UserControlParams p)
    {
        Control c = GetPostBackControl(Page);
        if ((c != null))
        {
            if (c.ID.ToString() == "btnAdd")
            {
            }
        }

        ltlCountNonCourier214.Text = p.iTotalRecs.ToString();

        placeNonCourier214.Controls.Clear();
        int ControlID = 0;
        for (int i = 0; i <= (Convert.ToInt16(ltlCountNonCourier214.Text) - 1); i++)
        {
            EDI214 DynamicUserControl = (EDI214)LoadControl("EDI214.ascx");

            while (InDeletedNonCourier214("uc4" + ControlID) == true)
            {
                ControlID += 1;
            }

            DynamicUserControl.ID = "uc4" + ControlID;
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);

            UserControlParams p1 = new UserControlParams() { idRequest = requestID, iRecordID = i, bNewDialog = p.passbacks[i].bNewRecord, idEDIRecipReqs = p.EDIRecipReqs[i], passbacks = p.passbacks,ct = p.ct, bUseTimeOfFile = p.bUseTimeOfFile };
            DynamicUserControl.LoadParams(p1);

            DynamicUserControl.RemoveUserControl2 += this.HandleRemoveUserControlNonCourier214;
            DynamicUserControl.UserControlSaved += this.HandleUserControlNonCourier214Saved;
            placeNonCourier214.Controls.Add(DynamicUserControl);
            ControlID += 1;
        }
    }
    private void AddNonCourier214TestDynamicControls(UserControlParams p)
    {
        Control c = GetPostBackControl(Page);
        if ((c != null))
        {
            if (c.ID.ToString() == "btnAdd")
            {
            }
        }

        ltlCountNonCourier214Test.Text = p.iTotalRecs.ToString();

        placeNonCourier214Test.Controls.Clear();
        int ControlID = 0;
        for (int i = 0; i <= (Convert.ToInt16(ltlCountNonCourier214Test.Text) - 1); i++)
        {
            EDI214 DynamicUserControl = (EDI214)LoadControl("EDI214.ascx");

            while (InDeletedNonCourier214Test("uc7" + ControlID) == true)
            {
                ControlID += 1;
            }

            DynamicUserControl.ID = "uc7" + ControlID;
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);

            UserControlParams p1 = new UserControlParams() { idRequest = requestID, iRecordID = i, bNewDialog = p.passbacks[i].bNewRecord, idEDIRecipReqs = p.EDIRecipReqs[i], passbacks = p.passbacks, ct = p.ct, bUseTimeOfFile = p.bUseTimeOfFile };
            DynamicUserControl.LoadParams(p1);

            DynamicUserControl.RemoveUserControl2 += this.HandleRemoveUserControlNonCourier214Test;
            DynamicUserControl.UserControlSaved += this.HandleUserControlNonCourier214TestSaved;
            placeNonCourier214Test.Controls.Add(DynamicUserControl);
            ControlID += 1;
        }
    }

    private void AddPuroPostStandDynamicControls(UserControlParams p)
    {
        Control c = GetPostBackControl(Page);
        if ((c != null))
        {
            if (c.ID.ToString() == "btnAdd")
            {
            }
        }

        ltlCountPuroPostStand.Text = p.iTotalRecs.ToString();

        placePuroPostStand.Controls.Clear();
        int ControlID = 0;
        for (int i = 0; i <= (Convert.ToInt16(ltlCountPuroPostStand.Text) - 1); i++)
        {
            //PuroPostStandard DynamicUserControl = (PuroPostStandard)LoadControl("PuroPostStandard.ascx");
            EDI210 DynamicUserControl = (EDI210)LoadControl("EDI210.ascx");

            while (InDeletedPuroPostStand("uc5" + ControlID) == true)
            {
                ControlID += 1;
            }

            DynamicUserControl.ID = "uc5" + ControlID;
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            UserControlParams p1 = new UserControlParams() { idRequest = requestID, iRecordID = i, bNewDialog = p.passbacks[i].bNewRecord, idEDIRecipReqs = p.EDIRecipReqs[i], passbacks = p.passbacks, bUseTimeOfFile = p.bUseTimeOfFile };
            DynamicUserControl.LoadParams(p1); ;

            DynamicUserControl.RemoveUserControl += this.HandleRemoveUserControlPuroPostStand;
            DynamicUserControl.UserControlSaved += this.HandleUserControlPuroPostStandSaved;
            placePuroPostStand.Controls.Add(DynamicUserControl);
            ControlID += 1;
        }
    }

    private bool InDeletedList(string ControlID)
    {
        string[] DeletedList = ltlRemoved.Text.Split('|');
        for (int i = 0; i <= DeletedList.GetLength(0) - 1; i++)
        {
            if (ControlID.ToLower() == DeletedList[i].ToLower())
            {
                return true;
            }
        }
        return false;
    }
    private bool InDeletedList2(string ControlID)
    {
        string[] DeletedList = ltlRemoved2.Text.Split('|');
        for (int i = 0; i <= DeletedList.GetLength(0) - 1; i++)
        {
            if (ControlID.ToLower() == DeletedList[i].ToLower())
            {
                return true;
            }
        }
        return false;
    }
    private bool InDeletedNonCourier210(string ControlID)
    {
        string[] DeletedList = ltlRemovedNonCourier210.Text.Split('|');
        for (int i = 0; i <= DeletedList.GetLength(0) - 1; i++)
        {
            if (ControlID.ToLower() == DeletedList[i].ToLower())
            {
                return true;
            }
        }
        return false;
    }
    private bool InDeletedNonCourier210Test(string ControlID)
    {
        string[] DeletedList = ltlRemovedNonCourier210Test.Text.Split('|');
        for (int i = 0; i <= DeletedList.GetLength(0) - 1; i++)
        {
            if (ControlID.ToLower() == DeletedList[i].ToLower())
            {
                return true;
            }
        }
        return false;
    }

    private bool InDeletedNonCourier214(string ControlID)
    {
        string[] DeletedList = ltlRemovedNonCourier214.Text.Split('|');
        for (int i = 0; i <= DeletedList.GetLength(0) - 1; i++)
        {
            if (ControlID.ToLower() == DeletedList[i].ToLower())
            {
                return true;
            }
        }
        return false;
    }
    private bool InDeletedNonCourier214Test(string ControlID)
    {
        string[] DeletedList = ltlRemovedNonCourier214Test.Text.Split('|');
        for (int i = 0; i <= DeletedList.GetLength(0) - 1; i++)
        {
            if (ControlID.ToLower() == DeletedList[i].ToLower())
            {
                return true;
            }
        }
        return false;
    }

    private bool InDeletedPuroPostStand(string ControlID)
    {
        string[] DeletedList = ltlRemovedPuroPostStand.Text.Split('|');
        for (int i = 0; i <= DeletedList.GetLength(0) - 1; i++)
        {
            if (ControlID.ToLower() == DeletedList[i].ToLower())
            {
                return true;
            }
        }
        return false;
    }

    public void HandleRemoveUserControl(object sender, EventArgs e)
    {
        ParamsFor210 = ((EDI210)sender).Params;
    }
    public void HandleUserControl210Saved(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 6;
        RadTabStrip1.Tabs[6].Selected = true;
    }
    public void HandleRemoveUserControl2(object sender, EventArgs e)
    {
        ParamsFor214 = ((EDI214)sender).Params;
        //Button remove = (sender as Button);
        //UserControl DynamicUserControl = (UserControl)remove.Parent;
        //ph2.Controls.Remove((UserControl)remove.Parent);
        //ltlRemoved2.Text += DynamicUserControl.ID + "|";
        //ltlCount2.Text = (Convert.ToInt16(ltlCount2.Text) - 1).ToString();
    }
    public void HandleUserControl214Saved(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 6;
        RadTabStrip1.Tabs[6].Selected = true;
    }
    public void HandleRemoveUserControlNonCourier210(object sender, EventArgs e)
    {
        ParamsForNonCourier210 = ((EDI210)sender).Params;
    }
    public void HandleRemoveUserControlNonCourier210Test(object sender, EventArgs e)
    {
        ParamsForNonCourier210Test = ((EDI210)sender).Params;
    }
    public void HandleUserControlNonCourier210Saved(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 7;
        RadTabStrip1.Tabs[7].Selected = true;
    }
    public void HandleUserControlNonCourier210TestSaved(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 7;
        RadTabStrip1.Tabs[7].Selected = true;
    }
    public void HandleRemoveUserControlNonCourier214(object sender, EventArgs e)
    {
        ParamsForNonCourier214 = ((EDI214)sender).Params;
    }
    public void HandleRemoveUserControlNonCourier214Test(object sender, EventArgs e)
    {
        ParamsForNonCourier214Test = ((EDI214)sender).Params;
    }

    public void HandleUserControlNonCourier214Saved(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 7;
        RadTabStrip1.Tabs[7].Selected = true;
    }
    public void HandleUserControlNonCourier214TestSaved(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 7;
        RadTabStrip1.Tabs[7].Selected = true;
    }

    public void HandleRemoveUserControlPuroPostStand(object sender, EventArgs e)
    {
        //ParamsForPuroPostStand = ((PuroPostStandard)sender).Params;
        ParamsForPuroPostStand = ((EDI210)sender).Params;
    }
    public void HandleUserControlPuroPostStandSaved(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 7;
        RadTabStrip1.Tabs[7].Selected = true;
    }
    protected void txtBxNumberRecipients210_TextChanged(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 6;
        RadTabStrip1.Tabs[6].Selected = true;
    }
    protected void txtBxNumberRecipients214_TextChanged(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 6;
        RadTabStrip1.Tabs[6].Selected = true;
    }
    protected void txtBxNumRecipNonCourier210_TextChanged(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 7;
        RadTabStrip1.Tabs[7].Selected = true;
    }
    protected void txtBxNumRecipNonCourier214_TextChanged(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 7;
        RadTabStrip1.Tabs[7].Selected = true;
    }
    protected void txtBxNumRecipNonCourierPuroPostStand_TextChanged(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 7;
        RadTabStrip1.Tabs[7].Selected = true;
    }
    protected void comboBxCourierEDI210_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        SetCourierEDI210Controls();
    }
    protected void comboBxCourierEDI214_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        SetCourierEDI214Controls();
    }
    private void SetCourierEDI210Controls()
    {
        if (comboBxCourierEDI210.SelectedValue.ToString().ToLower().Contains("yes"))
        {
            lbl210Accounnts.Visible = true;
            gridEDI210Accounts.Visible = true;
            lbl210AccounntStar.Visible = true;
            comboxCombinepayer.Visible = true;
            lblCombinepayer.Visible = true;
            comboBoxBatchInvoices.Visible = true;
            lblBatchInvoices.Visible = true;
            txtBxNumberRecipients210.Visible = true;
            lbl210InvoiceRecipients.Visible = true;
            btnAdd210.Visible = true;
            ph1.Visible = true;
        }
        else
        {
            lbl210Accounnts.Visible = false;
            gridEDI210Accounts.Visible = false;
            lbl210AccounntStar.Visible = false;
            comboxCombinepayer.Visible = false;
            lblCombinepayer.Visible = false;
            comboBoxBatchInvoices.Visible = false;
            lblBatchInvoices.Visible = false;
            txtBxNumberRecipients210.Visible = false;
            lbl210InvoiceRecipients.Visible = false;
            btnAdd210.Visible = false;
            ph1.Visible = false;
        }
    }
    private void SetCourierEDI214Controls()
    {
        if (comboBxCourierEDI214.SelectedValue.ToString().ToLower().Contains("yes"))
        {
            lbl214Accounnts.Visible = true;
            gridEDI214Accounts.Visible = true;
            lbl214AccounntStar.Visible = true;
            lbl214InvoiceRecipients.Visible = true;
            txtBxNumberRecipients214.Visible = true;
            btnAdd214.Visible = true;
            ph2.Visible = true;
        }
        else
        {
            lbl214Accounnts.Visible = false;
            gridEDI214Accounts.Visible = false;
            lbl214AccounntStar.Visible = false;
            lbl214InvoiceRecipients.Visible = false;
            txtBxNumberRecipients214.Visible = false;
            btnAdd214.Visible = false;
            ph2.Visible = false;

        }
    }
    private void SetCourierNonCourierEDI210Controls()
    {
        if (comboBxNonCourierEDI210.SelectedValue.ToString().ToLower().Contains("yes"))
        {
            //lblNonCourier210SFTP.Visible = true;
            //txtNonCourier210SFTP.Visible = true;
            lblNonCourier210Accounnts.Visible = true;
            gridNonCourierEDI210Accounts.Visible = true;
            lblNonCourier210AccounntStar.Visible = true;
            txtBxNumRecipNonCourier210.Visible = true;
            lblNumRecipNonCourier210.Visible = true;
            btnNumRecipNonCourier210.Visible = true;
            lblNonCourier210TestSent.Visible = true;
            comboNonCourier210TestSent.Visible = true;
            lblNumRecipNonCourier210Test.Visible = true;
            txtBxNumRecipNonCourier210Test.Visible = true;
            btnNumRecipNonCourier210Test.Visible = true;
            placeNonCourier210.Visible = true;
            placeNonCourier210Test.Visible = true;
        }
        else
        {
            //lblNonCourier210SFTP.Visible = false;
            //txtNonCourier210SFTP.Visible = false;
            lblNonCourier210Accounnts.Visible = false;
            gridNonCourierEDI210Accounts.Visible = false;
            lblNonCourier210AccounntStar.Visible = false;
            txtBxNumRecipNonCourier210.Visible = false;
            lblNumRecipNonCourier210.Visible = false;
            btnNumRecipNonCourier210.Visible = false;
            lblNonCourier210TestSent.Visible = false;
            comboNonCourier210TestSent.Visible = false;
            lblNumRecipNonCourier210Test.Visible = false;
            txtBxNumRecipNonCourier210Test.Visible = false;
            btnNumRecipNonCourier210Test.Visible = false;
            placeNonCourier210.Visible = false;
            placeNonCourier210Test.Visible = false;
        }
    }

    private void SetCourierNonCourierEDI214Controls()
    {
        if (comboBxNonCourierEDI214.SelectedValue.ToString().ToLower().Contains("yes"))
        {
            //lblNonCourier214SFTP.Visible = true;
            //txtNonCourier214SFTP.Visible = true;
            lblNonCourier214Accounnts.Visible = true;
            gridNonCourierEDI214Accounts.Visible = true;
            lblNonCourier214AccounntStar.Visible = true;
            txtBxNumRecipNonCourier214.Visible = true;
            lblNumRecipNonCourier214.Visible = true;
            btnNumRecipNonCourier214.Visible = true;
            lblNonCourier214TestSent.Visible = true;
            comboNonCourier214TestSent.Visible = true;
            lblNumRecipNonCourier214Test.Visible = true;
            txtBxNumRecipNonCourier214Test.Visible = true;
            btnNumRecipNonCourier214Test.Visible = true;
            placeNonCourier214.Visible = true;
            placeNonCourier214Test.Visible = true;
        }
        else
        {
            //lblNonCourier214SFTP.Visible = false;
            //txtNonCourier214SFTP.Visible = false;
            lblNonCourier214Accounnts.Visible = false;
            gridNonCourierEDI214Accounts.Visible = false;
            lblNonCourier214AccounntStar.Visible = false;
            txtBxNumRecipNonCourier214.Visible = false;
            lblNumRecipNonCourier214.Visible = false;
            btnNumRecipNonCourier214.Visible = false;
            lblNonCourier214TestSent.Visible = false;
            comboNonCourier214TestSent.Visible = false;
            lblNumRecipNonCourier214Test.Visible = false;
            txtBxNumRecipNonCourier214Test.Visible = false;
            btnNumRecipNonCourier214Test.Visible = false;
            placeNonCourier214.Visible = false;
            placeNonCourier214Test.Visible = false;
        }
    }
    private void SetCourierNonCourierPuroPostStandControls()
    {
        if (comboBxNonCourierPuroPost.SelectedValue.ToString().ToLower().Contains("yes"))
        {
            //lblNonCourierPuroPostSFTP.Visible = true;
            //txtNonCourierPuroPostSFTP.Visible = true;
            lblNonCourierPuroPostAccounnts.Visible = true;
            lblNonCourierPuroPostAccounntStar.Visible = true;
            gridNonCourierPuroPostAccounts.Visible = true;
            lblNumRecipNonCourierPuroPostStand.Visible = true;
            txtBxNumRecipNonCourierPuroPostStand.Visible = true;
            btnNumRecipNonCourierPuroPostStand.Visible = true;

            placePuroPostStand.Visible = true;
        }
        else
        {
            //lblNonCourierPuroPostSFTP.Visible = false;
            //txtNonCourierPuroPostSFTP.Visible = false;
            lblNonCourierPuroPostAccounnts.Visible = false;
            lblNonCourierPuroPostAccounntStar.Visible = false;
            gridNonCourierPuroPostAccounts.Visible = false;
            lblNumRecipNonCourierPuroPostStand.Visible = false;
            txtBxNumRecipNonCourierPuroPostStand.Visible = false;
            btnNumRecipNonCourierPuroPostStand.Visible = false;

            placePuroPostStand.Visible = false;
        }
    }

    protected void comboBxNonCourierEDI210_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        SetCourierNonCourierEDI210Controls();
    }
    protected void comboBxNonCourierEDI214_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        SetCourierNonCourierEDI214Controls();
    }
    protected void comboBxNonCourierPuroPost_SelectedIndexChanged(object sender, DropDownListEventArgs e)
    {
        SetCourierNonCourierPuroPostStandControls();
    }
    #endregion
    protected void hideshowbars()
    {
        //hide and show sections based on servcies
        //hide all, and then show if they have that service
        RadPanelBar1.FindItemByValue("CPC").Visible = false;
        RadPanelBar1.FindItemByValue("CPCWest").Visible = false;
        RadPanelBar1.FindItemByValue("LTL").Visible = false;
        RadPanelBar1.FindItemByValue("LTLWest").Visible = false;
        RadPanelBar1.FindItemByValue("Courier").Visible = false;
        RadPanelBar1.FindItemByValue("CourierWest").Visible = false;
        RadPanelBar1.FindItemByValue("PuroPost").Visible = false;
        RadPanelBar1.FindItemByValue("PuroPostWest").Visible = false;
        RadPanelBar1.FindItemByValue("PuroPostPlus").Visible = false;
        RadPanelBar1.FindItemByValue("PuroPostPlusWest").Visible = false;
        RadPanelBar1.FindItemByValue("Returns").Visible = false;

        //UNTIL NEXT PHASE, HIDE THESE TABS ALSO
        RadPanelBar1.FindItemByValue("WorldPak").Visible = false;
        RadPanelBar1.FindItemByValue("Customs").Visible = false;
        RadPanelBar1.FindItemByValue("BillingandEDI").Visible = false;
        RadPanelBar1.FindItemByValue("ContractInformation").Visible = false;
        RadPanelBar1.FindItemByValue("AccountSupport").Visible = false;
        RadPanelBar1.FindItemByValue("Migration").Visible = false;

        //Go by what's in proposedSvcList in case they have just added services before saving
        List<ClsDiscoveryRequestSvcs> svcList = (List<ClsDiscoveryRequestSvcs>)Session["proposedSvcList"];

        foreach (ClsDiscoveryRequestSvcs svc in svcList)
        {
        }
    }

    protected void getCurrency()
    {
        try
        {
            List<ClsCurrency> currencylist = repository.GetCurrency();
            rddlCurrency.DataSource = currencylist;
            rddlCurrency.DataTextField = "Currency";
            rddlCurrency.DataValueField = "Currency";
            rddlCurrency.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getDataEntryMethods()
    {
        try
        {
            List<ClsDataEntryMethods> methodlist = repository.GetDataEntryMethodsInactiveNoted();
            rddlDataEntry.DataSource = methodlist;
            rddlDataEntry.DataTextField = "DataEntry";
            rddlDataEntry.DataValueField = "DataEntry";
            rddlDataEntry.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getShippingProducts()
    {
        try
        {
            List<ClsDiscoveryRequestSvcs> svcList = new List<ClsDiscoveryRequestSvcs>();
            if (Session["proposedSvcList"] != null)
            {
                svcList = (List<ClsDiscoveryRequestSvcs>)Session["proposedSvcList"];
            }
            DataTable prodlist = repository.GetShippingProducts(svcList);
            rddlProducts.DataSource = prodlist;
            rddlProducts.DataTextField = "ShippingProduct";
            rddlProducts.DataValueField = "idShippingProduct";
            rddlProducts.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void getShippingProductsAll()
    {
        try
        {
            List<ClsShippingProducts> prodlist = repository.GetShippingProducts();
            rddlProducts.DataSource = prodlist;
            rddlProducts.DataTextField = "ShippingProduct";
            rddlProducts.DataValueField = "idShippingProduct";
            rddlProducts.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getThirdPartyVendors()
    {
        try
        {
            List<ClsThirdPartyVendor> vendorlist = repository.GetThirdPartyVendorsInactiveNoted();
            rddlThirdPartyVendor.DataSource = vendorlist;
            rddlThirdPartyVendor.DataTextField = "VendorName";
            rddlThirdPartyVendor.DataValueField = "idThirdPartyVendor";
            rddlThirdPartyVendor.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getShippingVendors()
    {
        try
        {
            List<ClsShippingVendor> vendorlist = repository.GetShippingVendors();
            rddlShippingVendor.DataSource = vendorlist;
            rddlShippingVendor.DataTextField = "VendorName";
            rddlShippingVendor.DataValueField = "idShippingVendor";
            rddlShippingVendor.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getBrokers()
    {
        try
        {
            List<ClsBroker> brokerlist = repository.GetActiveBrokers();
            rddlCustomsBroker.DataSource = brokerlist;
            rddlCustomsBroker.DataTextField = "Broker";
            rddlCustomsBroker.DataValueField = "idBroker";
            rddlCustomsBroker.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getEquipmentList()
    {
        try
        {
            List<ClsEquipment> equiplist = repository.GetEquipmentList();
            rddlEquipment.DataSource = equiplist;
            rddlEquipment.DataTextField = "Equipment";
            rddlEquipment.DataValueField = "Equipment";
            rddlEquipment.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getInvoiceTypes()
    {
        try
        {
            List<ClsInvoiceType> invlist = repository.GetInvoiceType();
            rddlInvoiceType.DataSource = invlist;
            rddlInvoiceType.DataTextField = "InvoiceType";
            rddlInvoiceType.DataValueField = "InvoiceType";
            rddlInvoiceType.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getFileTypes()
    {
        try
        {
            List<ClsFileType> filelist = repository.GetFileTypes();
            rddlFileFormat.DataSource = filelist;
            rddlFileFormat.DataTextField = "FileType";
            rddlFileFormat.DataValueField = "FileType";
            rddlFileFormat.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getCommunicationMethods()
    {
        try
        {
            List<ClsCommunicationMethod> commlist = repository.GetCommunicationMethods();
            rddlCommunicationMethod.DataSource = commlist;
            rddlCommunicationMethod.DataTextField = "CommunicationMethod";
            rddlCommunicationMethod.DataValueField = "CommunicationMethod";
            rddlCommunicationMethod.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void getEDISolutions()
    {
        try
        {
            List<ClsEDISolution> edilist = repository.GetEDISolutions();
            rddlEDISolution.DataSource = edilist;
            rddlEDISolution.DataTextField = "Solution";
            rddlEDISolution.DataValueField = "Solution";
            rddlEDISolution.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getInductionPoints()
    {
        try
        {
            //Courier
            List<ClsInductionPoint> inductionlist = repository.GetInductionPointsNoPuroPost();
            rddlCourierInduction.DataSource = inductionlist;
            rddlCourierInduction.DataTextField = "Description";
            rddlCourierInduction.DataValueField = "idInduction";
            rddlCourierInduction.DataBind();

            //Courier west
            rddlCourierInductionWest.DataSource = inductionlist;
            rddlCourierInductionWest.DataTextField = "Description";
            rddlCourierInductionWest.DataValueField = "idInduction";
            rddlCourierInductionWest.DataBind();

            //PuroPost
            List<ClsInductionPoint> inductionlistPP = repository.GetInductionPointsPuroPost();
            rddlPPSTInduction.DataSource = inductionlistPP;
            rddlPPSTInduction.DataTextField = "Description";
            rddlPPSTInduction.DataValueField = "idInduction";
            rddlPPSTInduction.DataBind();

            //PuroPost west
            rddlPPSTInductionWest.DataSource = inductionlistPP;
            rddlPPSTInductionWest.DataTextField = "Description";
            rddlPPSTInductionWest.DataValueField = "idInduction";
            rddlPPSTInductionWest.DataBind();

            //PuroPost Plus
            rddlPPlusInduction.DataSource = inductionlistPP;
            rddlPPlusInduction.DataTextField = "Description";
            rddlPPlusInduction.DataValueField = "idInduction";
            rddlPPlusInduction.DataBind();

            //PuroPost Plus west
            rddlPPlusInductionWest.DataSource = inductionlistPP;
            rddlPPlusInductionWest.DataTextField = "Description";
            rddlPPlusInductionWest.DataValueField = "idInduction";
            rddlPPlusInductionWest.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getInductionPointsPuroPost()
    {
        try
        {
            List<ClsInductionPoint> inductionlist = repository.GetInductionPointsPuroPost();
            rddlCourierInduction.DataSource = inductionlist;
            rddlCourierInduction.DataTextField = "Description";
            rddlCourierInduction.DataValueField = "idInduction";
            rddlCourierInduction.DataBind();

            rddlPPSTInduction.DataSource = inductionlist;
            rddlPPSTInduction.DataTextField = "Description";
            rddlPPSTInduction.DataValueField = "idInduction";
            rddlPPSTInduction.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getDistricts()
    {
        try
        {
            List<ClsDistrict> districtlist = repository.GetDistricts();
            rddlDistrict.DataSource = districtlist;
            rddlDistrict.DataTextField = "District";
            rddlDistrict.DataValueField = "District";
            rddlDistrict.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getCustomsTypes()
    {
        try
        {
            List<ClsCustomsType> customslist = repository.GetListCustomsTypesInactiveNoted();
            rddlCustomsList.DataSource = customslist;
            rddlCustomsList.DataTextField = "CustomsType";
            rddlCustomsList.DataValueField = "CustomsType";
            rddlCustomsList.DataBind();
            rddlCustomsList.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void rddlSolutionType_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            UpdateTabs();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void getSolutionTypes()
    {
        try
        {
            List<ClsSolutionType> solutionlist = repository.GetSolutionTypes();
            rddlSolutionType.DataSource = solutionlist;
            rddlSolutionType.DataTextField = "SolutionType";
            rddlSolutionType.DataValueField = "idSolutionType";
            rddlSolutionType.DataBind();
            rddlSolutionType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void getRequestTypes()
    {
        try
        {
            List<ClsRequestType> requestlist = repository.GetRequestTypes();
            rddlRequestType.DataSource = requestlist;
            rddlRequestType.DataTextField = "RequestType";
            rddlRequestType.DataValueField = "idRequestType";
            rddlRequestType.DataBind();
            rddlRequestType.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getVendorTypes()
    {
        try
        {
            List<ClsVendorType> vtypelist = repository.GetActiveVendorTypes();
            rddlVendorType.DataSource = vtypelist;
            rddlVendorType.DataTextField = "VendorType";
            rddlVendorType.DataValueField = "idVendorType";
            rddlVendorType.DataBind();
            rddlVendorType.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    //protected void getFreightAuditors()
    //{
    //    try
    //    {
    //        List<clsFreightAuditor> FreightAuditorList = SrvFreightAuditor.GetFreightAuditors();
    //        cmbBoxFreightAuditor.DataSource = FreightAuditorList;
    //        cmbBoxFreightAuditor.DataTextField = "CompanyName";
    //        cmbBoxFreightAuditor.DataValueField = "idFreightAuditor";
    //        cmbBoxFreightAuditor.DataBind();
    //        cmbBoxFreightAuditor.SelectedIndex = -1;
    //    }
    //    catch (Exception ex)
    //    {
    //        pnlDanger.Visible = true;
    //        lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
    //    }
    //}
    protected void rdCurrentTarget_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        try
        {
            //Check for Existing Request
            ClsDiscoveryRequest dr = new ClsDiscoveryRequest();
            Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
            if (RequestID != 0)
            {
                ClsDiscoveryRequest editRequest = dr.GetDiscoveryRequest(RequestID);
                if (editRequest.CurrentGoLive != null)
                {
                    DateTime priorGoLive = Convert.ToDateTime(editRequest.CurrentGoLive);
                    if (rdCurrentTarget.SelectedDate != priorGoLive)
                    {
                        lblChangeReason.Visible = true;
                        txtTargetChangeReason.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void getCloseReasons()
    {
        try
        {
            List<ClsCloseReason> reasonlist = repository.GetCloseReasonsInactiveNoted();
            rddlCloseReason.DataSource = reasonlist;
            rddlCloseReason.DataTextField = "CloseReason";
            rddlCloseReason.DataValueField = "CloseReason";
            rddlCloseReason.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getBranches()
    {
        try
        {
            List<ClsRegions> regionlist = repository.GetAllRegions();
            rddlControlBranch.DataSource = regionlist;
            rddlControlBranch.DataTextField = "Region";
            rddlControlBranch.DataValueField = "Region";
            rddlControlBranch.DataBind();

            rddlBranch.DataSource = regionlist;
            rddlBranch.DataTextField = "Region";
            rddlBranch.DataValueField = "Region";
            rddlBranch.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rddlRequestType_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            int selectedind = Convert.ToInt32(rddlRequestType.SelectedValue);
            if (selectedind == 3)
            {
                chkNewBus.Checked = true;
                rddlShippingVendor.SelectedIndex = -1;
                rddlCurrentCA.SelectedText = "Yes";
            }
            else
            {
                rddlShippingVendor.SelectedValue = "3";
                rddlCurrentCA.SelectedText = "Yes";
                chkNewBus.Checked = true;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rddlInternalType_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            int selectedind = Convert.ToInt32(rddlInternalType.SelectedValue);
            if (selectedind == 1014)
            {
                note1ast.Visible = false;
                note2ast.Visible = true;
                rfvNotes.Enabled = false;
                rfvNotes2.Enabled = true;
            }
            else
            {
                note1ast.Visible = true;
                note2ast.Visible = false;
                rfvNotes.Enabled = true;
                rfvNotes2.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void rddlShippingVendor_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string vendor = rddlShippingVendor.SelectedText;
            if (vendor.ToLower() == "other")
            {
                lblVendorName.Visible = true;
                txtCurrentVendor.Visible = true;
            }
            else
            {
                lblVendorName.Visible = false;
                txtCurrentVendor.Visible = false;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rddlCurrentCA_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string shipflag = rddlCurrentCA.SelectedText;
            if (shipflag.ToLower() == "no")
            {
                rddlShippingVendor.SelectedText = "None";
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rddlCustomsList_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string customsval = rddlCustomsList.SelectedText.ToLower().Trim();
            if (customsval != "none" && customsval != "tbd")
            {
                lblCustomsBroker.Visible = true;
                rddlCustomsBroker.Visible = true;
                string customsbroker = rddlCustomsBroker.SelectedText.ToLower().Trim();
                if (customsbroker == "other")
                {
                    lblOtherBroker.Visible = true;
                    txtCustomsBroker.Visible = true;
                }
            }
            else
            {
                lblCustomsBroker.Visible = false;
                rddlCustomsBroker.Visible = false;
                lblOtherBroker.Visible = false;
                txtCustomsBroker.Visible = false;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rddlCustomsBroker_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string customsbroker = rddlCustomsBroker.SelectedText.ToLower().Trim();
            if (customsbroker == "other")
            {
                lblOtherBroker.Visible = true;
                txtCustomsBroker.Visible = true;
            }
            else
            {
                lblOtherBroker.Visible = false;
                txtCustomsBroker.Visible = false;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rddlVendorType_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            int vendorTypeID = Convert.ToInt32(rddlVendorType.SelectedValue);
            if (vendorTypeID == 1)
            {
                cbx3pv.Checked = true;
                rddlThirdPartyVendor.Visible = true;
                lbl3pv.Visible = true;
            }
            else
            {
                cbx3pv.Checked = false;
                rddlThirdPartyVendor.Visible = false;
                lbl3pv.Visible = false;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rddlDistrict_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            string district = rddlDistrict.SelectedText;
            List<ClsRegions> regionlist = repository.GetRegionsForDistrict(district);
            rddlBranch.Items.Clear();
            rddlBranch.DataSource = regionlist;
            rddlBranch.DataTextField = "Region";
            rddlBranch.DataValueField = "Region";
            rddlBranch.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rddlPhase_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            int phase = Convert.ToInt16(rddlPhase.SelectedValue);

            if (phase == ClosedID || phase == OnHoldID)
            {
                rddlCloseReason.Visible = true;
            }
            else
            {
                rddlCloseReason.Visible = false;
            }

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    
    protected void CourierInduction_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            Int16 idInduction = Convert.ToInt16(rddlCourierInduction.SelectedValue);
            ClsInductionPoint inductionAddress = repository.GetInductionPointDetails(idInduction);
            courierIaddress.Text = inductionAddress.Address;
            courierIcity.Text = inductionAddress.City;
            courierIstate.Text = inductionAddress.State;
            courierIzip.Text = inductionAddress.Zip;
            courierIcountry.Text = inductionAddress.Country;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void onIdxChangedCustAuditPortal(object sender, System.EventArgs e)
    {
        try
        {
            bool bShowAudit = false;
            if (comboxCustAuditPortal.SelectedText.ToString().ToLower().Contains("yes"))
                bShowAudit = true;

            lblCustAuditPortalYes.Visible = bShowAudit;
            lblCustAuditPortalStar.Visible = bShowAudit;
            lblCustAuditPortalURL.Visible = bShowAudit;
            txtBxAuditoURL.Visible = bShowAudit;
            lblCustAuditPortalUserName.Visible = bShowAudit;
            txtBxAuditoUserName.Visible = bShowAudit;
            lblCustAuditPortalPassword.Visible = bShowAudit;
            txtBxAuditoPassword.Visible = bShowAudit;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void CourierInductionWest_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            Int16 idInduction = Convert.ToInt16(rddlCourierInductionWest.SelectedValue);
            ClsInductionPoint inductionAddress = repository.GetInductionPointDetails(idInduction);
            courierIaddressWest.Text = inductionAddress.Address;
            courierIcityWest.Text = inductionAddress.City;
            courierIstateWest.Text = inductionAddress.State;
            courierIzipWest.Text = inductionAddress.Zip;
            courierIcountryWest.Text = inductionAddress.Country;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void PuroPostInduction_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            Int16 idInduction = Convert.ToInt16(rddlPPSTInduction.SelectedValue);
            ClsInductionPoint inductionAddress = repository.GetInductionPointDetails(idInduction);
            ppstIaddress.Text = inductionAddress.Address;
            ppstIcity.Text = inductionAddress.City;
            ppstIstate.Text = inductionAddress.State;
            ppstIzip.Text = inductionAddress.Zip;
            ppstIcountry.Text = inductionAddress.Country;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void PuroPostInductionWest_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            Int16 idInduction = Convert.ToInt16(rddlPPSTInductionWest.SelectedValue);
            ClsInductionPoint inductionAddress = repository.GetInductionPointDetails(idInduction);
            ppstIaddressWest.Text = inductionAddress.Address;
            ppstIcityWest.Text = inductionAddress.City;
            ppstIstateWest.Text = inductionAddress.State;
            ppstIzipWest.Text = inductionAddress.Zip;
            ppstIcountryWest.Text = inductionAddress.Country;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void PuroPostPlusInduction_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            Int16 idInduction = Convert.ToInt16(rddlPPlusInduction.SelectedValue);
            ClsInductionPoint inductionAddress = repository.GetInductionPointDetails(idInduction);
            PPlusIaddress.Text = inductionAddress.Address;
            PPlusIcity.Text = inductionAddress.City;
            PPlusIstate.Text = inductionAddress.State;
            PPlusIzip.Text = inductionAddress.Zip;
            PPlusIcountry.Text = inductionAddress.Country;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void PuroPostPlusInductionWest_IndexChanged(object sender, System.EventArgs e)
    {
        try
        {
            Int16 idInduction = Convert.ToInt16(rddlPPSTInductionWest.SelectedValue);
            ClsInductionPoint inductionAddress = repository.GetInductionPointDetails(idInduction);
            PPlusIaddressWest.Text = inductionAddress.Address;
            PPlusIcityWest.Text = inductionAddress.City;
            PPlusIstateWest.Text = inductionAddress.State;
            PPlusIzipWest.Text = inductionAddress.Zip;
            PPlusIcountryWest.Text = inductionAddress.Country;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void chkMartinGrove_Clicked(object sender, System.EventArgs e)
    {
        try
        {
            Int16 idInduction = 108;
            ClsInductionPoint inductionAddress = repository.GetInductionPointDetails(idInduction);
            txtReturnsAddress.Text = inductionAddress.Address;
            txtReturnsCity.Text = inductionAddress.City;
            txtReturnsState.Text = inductionAddress.State;
            txtReturnsZip.Text = inductionAddress.Zip;
            txtReturnsCountry.Text = inductionAddress.Country;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getRelationships()
    {
        try
        {
            List<ClsRelationshipName> relatiobnshiplist = repository.GetRelationships();
            rddlRelationships.DataSource = relatiobnshiplist;
            rddlRelationships.DataTextField = "RelationshipName";
            rddlRelationships.DataValueField = "RelationshipName";
            rddlRelationships.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getITBAs()
    {
        try
        {
            List<ClsITBA> balist = repository.GetITBAs();
            rddlITBA.DataSource = balist;
            rddlITBA.DataTextField = "ITBA";
            rddlITBA.DataValueField = "idITBA";
            rddlITBA.DataBind();

            rddlITBA2.DataSource = balist;
            rddlITBA2.DataTextField = "ITBA";
            rddlITBA2.DataValueField = "idITBA";
            rddlITBA2.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void getEDISpecialists()
    {
        try
        {
            List<clsEDISpecialist> qEDISpecialist = SrvEDISpecialist.GetEDISpecialistView();
            cmboxEDISpecialist.DataSource = qEDISpecialist;
            cmboxEDISpecialist.DataTextField = "Name";
            cmboxEDISpecialist.DataValueField = "idEDISpecialist";
            cmboxEDISpecialist.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void getBillingSpecialists()
    {
        try
        {
            List<clsBillingSpecialist> qBillingSpecialist = SrvBillingSpecialist.GetBillingSpecialistView();
            cmboxBillingSpecialist.DataSource = qBillingSpecialist;
            cmboxBillingSpecialist.DataTextField = "Name";
            cmboxBillingSpecialist.DataValueField = "idBillingSpecialist";
            cmboxBillingSpecialist.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void getCollectionSpecialists()
    {
        try
        {
            List<clsCollectionSpecialist> qCollectionSpecialist = SrvCollectionSpecialist.GetCollectionSpecialistView();
            cmboxCollectionSpecialist.DataSource = qCollectionSpecialist;
            cmboxCollectionSpecialist.DataTextField = "Name";
            cmboxCollectionSpecialist.DataValueField = "idCollectionSpecialist";
            cmboxCollectionSpecialist.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void getShippingChannels()
    {
        try
        {
            List<ClsShippingChannel> shippinglist = repository.GetShippingChannelsInactiveNoted();
            rddlShippingChannel.DataSource = shippinglist;
            rddlShippingChannel.DataTextField = "ShippingChannel";
            rddlShippingChannel.DataValueField = "idShippingChannel";
            rddlShippingChannel.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getOnboardingPhases()
    {
        try
        {
            List<ClsOnboardingPhase> phaselist = repository.GetOnboardingPhasesInactiveNoted();
            rddlPhase.DataSource     =  phaselist;
            rddlPhase.DataTextField  = "OnboardingPhase";
            rddlPhase.DataValueField = "idOnboardingPhase";
            rddlPhase.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void getEDIOnboardingPhases()
    {
        try
        {
            List<ClsEDIOnboardingPhase> phaselist = SrvEDIOnboardingPhase.GetEDIOnboardingPhase();

            cmboxOnboardingPhase.DataSource = phaselist;
            cmboxOnboardingPhase.DataTextField = "EDIOnboardingPhaseType";
            cmboxOnboardingPhase.DataValueField = "idEDIOnboardingPhase";
            cmboxOnboardingPhase.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void getTaskTypes()
    {
        try
        {
            List<ClsTaskType> typelist = repository.GetTaskTypesInactiveNoted();
            rddlInternalType.DataSource = typelist;
            rddlInternalType.DataTextField = "TaskType";
            rddlInternalType.DataValueField = "idTaskType";
            rddlInternalType.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void getServices()
    {
        try
        {
            List<ClsShippingService> svclist = repository.GetServicesInactiveNoted();
            rddlService.DataSource = svclist;
            rddlService.DataTextField = "serviceDesc";
            rddlService.DataValueField = "idShippingSvc";
            rddlService.DataBind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rgSvcGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {

        try
        {
            List<ClsDiscoveryRequestSvcs> svcList = new List<ClsDiscoveryRequestSvcs>();
            if (Session["proposedSvcList"] != null)
            {
                svcList = (List<ClsDiscoveryRequestSvcs>)Session["proposedSvcList"];
            }
            Session["proposedSvcList"] = svcList;
            rgSvcGrid.DataSource = svcList;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rgSolutionsGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestEDI> solutionList = new List<ClsDiscoveryRequestEDI>();
            if (Session["ediList"] != null)
            {
                solutionList = (List<ClsDiscoveryRequestEDI>)Session["ediList"];
            }
            Session["ediList"] = solutionList;
            rgSolutionsGrid.DataSource = solutionList;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rgProductGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestProds> prodList = new List<ClsDiscoveryRequestProds>();
            if (Session["productList"] != null)
            {
                prodList = (List<ClsDiscoveryRequestProds>)Session["productList"];
            }
            Session["productList"] = prodList;
            rgSvcGrid.DataSource = prodList;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rgEquipmentGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestEquip> equipList = new List<ClsDiscoveryRequestEquip>();
            if (Session["equipmentList"] != null)
            {
                equipList = (List<ClsDiscoveryRequestEquip>)Session["equipmentList"];
            }
            Session["equipmentList"] = equipList;
            rgEquipmentGrid.DataSource = equipList;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    [MethodImpl(MethodImplOptions.NoInlining)]
    public string GetCurrentMethod()
    {
        var st = new StackTrace();
        var sf = st.GetFrame(1);
        return sf.GetMethod().Name;
    }
    #region contactGrid
    protected void contactGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            if (requestID != 0)
            {
                (sender as RadGrid).DataSource = SrvContact.GetContactsByRequestID(requestID);
            }
            // Only used during debug
            else if(bOneTimeContactGrid)
            {
                List<clsContact> contactList = SrvContact.GetMockData();
                Session["contactList"] = contactList;
                (sender as RadGrid).DataSource = contactList;
                bOneTimeContactGrid = false;
            }
            else
            {
                (sender as RadGrid).DataSource =(List<clsContact>)Session["contactList"];
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void contactGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
                RadTextBox txtBxContactName2 = (RadTextBox)edit.FindControl("txtBxContactName2");
                RadTextBox txtBxContactTitle2 = (RadTextBox)edit.FindControl("txtBxContactTitle2");
                RadTextBox txtBxContactEmail2 = (RadTextBox)edit.FindControl("txtBxContactEmail2");
                RadTextBox txtBxContactPhone2 = (RadTextBox)edit.FindControl("txtBxContactPhone2");

                List<ClsContactType> solutionlist = repository.GetContactTypes();
                RadDropDownList radlist = (RadDropDownList)edit.FindControl("radListContactType");
                radlist.DataSource = solutionlist;
                radlist.DataTextField = "ContactType";
                radlist.DataValueField = "idContactType";
                radlist.DataBind();
                radlist.SelectedIndex = -1;

                List<clsContact> contactList = (List<clsContact>)Session["contactList"];
                int rownum = e.Item.ItemIndex;
                if (rownum >= 0)
                {
                    clsContact contact = contactList[rownum];
                    txtBxContactName2.Text = contact.Name;
                    txtBxContactTitle2.Text = contact.Title;
                    txtBxContactEmail2.Text = contact.Email;
                    txtBxContactPhone2.Text = contact.Phone;

                    if (contact.idContactType - 1 > -1)
                    {
                        radlist.SelectedIndex = contact.idContactType - 1;
                        radlist.Enabled = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void contactGrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<clsContact> contactList = (List<clsContact>)Session["contactList"];
            int rownum = e.Item.ItemIndex;
            clsContact currentrow = contactList[rownum];
            SrvContact.Remove(currentrow.idContact);
            contactList.Remove(currentrow);
            if (contactList.Count == 0)
            {
                btnNextTab2.Visible = true; 
                btnNextTab2.Enabled = false;
            }
            Session["contactList"] = contactList;
            contactGrid.DataSource = contactList;
            contactGrid.Rebind();
            RadMultiPage1.SelectedIndex = 1;
            RadTabStrip1.Tabs[1].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void contactGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if( e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
                RadDropDownList radlist = (RadDropDownList)edit.FindControl("radListContactType");
                RadTextBox txtBxContactName2 = (RadTextBox)edit.FindControl("txtBxContactName2");
                RadTextBox txtBxContactTitle2 = (RadTextBox)edit.FindControl("txtBxContactTitle2");
                RadTextBox txtBxContactEmail2 = (RadTextBox)edit.FindControl("txtBxContactEmail2");
                RadTextBox txtBxContactPhone2 = (RadTextBox)edit.FindControl("txtBxContactPhone2");

                int requestID = 0;
                int.TryParse(Request.QueryString["requestID"], out requestID);

                if (!String.IsNullOrEmpty(radlist.SelectedText) && !String.IsNullOrEmpty(txtBxContactName2.Text) && !String.IsNullOrEmpty(txtBxContactEmail2.Text) && !String.IsNullOrEmpty(txtBxContactPhone2.Text))
                {
                    txtBxContactEmail2.Text = txtBxContactEmail2.Text.Replace("<", "");
                    clsContact contact = new clsContact()
                    {
                        idContactType = Convert.ToInt16(radlist.SelectedValue),
                        idRequest = requestID,
                        Name = txtBxContactName2.Text,
                        Title = txtBxContactTitle2.Text,
                        Email = txtBxContactEmail2.Text,
                        Phone = txtBxContactPhone2.Text,
                        CreatedOn = DateTime.Now,
                        CreatedBy = (string)(Session["userName"])
                    };
                    List<clsContact> contactList = new List<clsContact>();
                    if (requestID != 0)
                    {
                        int inewID = 0;
                        SrvContact.Insert(contact, out inewID);
                        contactList = repository.GetContacts(requestID);
                    }
                    else
                    {
                        contactList = (List<clsContact>)Session["contactList"];
                        ClsContactType qContactType = SrvContactType.GetContactsTypeByID(contact.idContactType);
                        contact.ContactTypeName = qContactType.ContactType;
                        contactList.Add(contact);
                    }
                    Session["contactList"] = contactList;
                    contactGrid.DataSource = contactList;
                    contactGrid.DataBind();
                    btnNextTab2.Visible = true;
                    btnNextTab2.Enabled = true;
                }
                else
                    e.Canceled = true;
            }
            else if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
                {
                    //GridEditFormItem edit = (GridEditFormItem)e.Item;
                    //RadDropDownList radlist = (RadDropDownList)edit.FindControl("radListContactType");
                    //radlist.SelectedIndex = 1; ;
                    //RadTextBox txtBxContactName2 = (RadTextBox)edit.FindControl("txtBxContactName2");
                    //txtBxContactName2.Text = "Pre-Contact " + DateTime.Now.ToString();
                    //RadTextBox txtBxContactTitle2 = (RadTextBox)edit.FindControl("txtBxContactTitle2");
                    //txtBxContactTitle2.Text = "Pre-Title " + DateTime.Now.ToString();
                    //RadTextBox txtBxContactEmail2 = (RadTextBox)edit.FindControl("txtBxContactEmail2");
                    //txtBxContactEmail2.Text = "Pre-Email " + DateTime.Now.ToString();
                    //RadTextBox txtBxContactPhone2 = (RadTextBox)edit.FindControl("txtBxContactPhone2");
                    //txtBxContactPhone2.Text = "123 456789 ";
                }
            }

            RadMultiPage1.SelectedIndex = 1;
            RadTabStrip1.Tabs[1].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void contactGrid_UpdateCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.UpdateCommandName)
        {
            if (e.Item is GridEditFormItem)
            {
                GridEditFormItem item = (GridEditFormItem)e.Item;
                int id = Convert.ToInt32(item.GetDataKeyValue("idContact"));
                if (id != 0)
                {
                    RadTextBox txtBxContactName2 = (RadTextBox)item.FindControl("txtBxContactName2");
                    RadTextBox txtBxContactTitle2 = (RadTextBox)item.FindControl("txtBxContactTitle2");
                    RadTextBox txtBxContactEmail2 = (RadTextBox)item.FindControl("txtBxContactEmail2");
                    RadTextBox txtBxContactPhone2 = (RadTextBox)item.FindControl("txtBxContactPhone2");

                    txtBxContactEmail2.Text = txtBxContactEmail2.Text.Replace("<", "");

                    List<clsContact> contactList = (List<clsContact>)Session["contactList"];
                    int itemIndex = e.Item.ItemIndex;
                    contactList[itemIndex].Name = txtBxContactName2.Text;
                    contactList[itemIndex].Title = txtBxContactTitle2.Text;
                    contactList[itemIndex].Email = txtBxContactEmail2.Text;
                    contactList[itemIndex].Phone = txtBxContactPhone2.Text;
                    Session["contactList"] = contactList;

                    SrvContact.Update(contactList[itemIndex]);
                    contactGrid.DataSource = contactList;
                    contactGrid.Rebind();
                }
            }
        }
    }
    protected void radListContactTypeIdxChanged(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 1;
        RadTabStrip1.Tabs[1].Selected = true;
    }
    #endregion
    #region  ShipmentMethods grid
    protected void gridShipmentMethods_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            if (requestID != 0)
            {
                (sender as RadGrid).DataSource = SrvEDIShipMethod.GetEDIShipMethodTypesByidRequest(requestID);
            }
            else if(bOneTimeContactGrid)
            {
                List<clsEDIShipMethod> EDIShipMethList = SrvEDIShipMethod.GetEDIShipMethodMockData();
                Session["EDIShipMethList"] = EDIShipMethList;
                (sender as RadGrid).DataSource = EDIShipMethList;
                bOneTimeContactGrid = false;
            }
            else
            {
                (sender as RadGrid).DataSource = (List<clsEDIShipMethod>)Session["EDIShipMethList"];
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridShipmentMethods_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;

                List<clsEDIShipMethodType> solutionlist = SrvEDIShipMethodType.GetEDIShipMethodTypes();
                RadDropDownList radlist = (RadDropDownList)edit.FindControl("radListEDIShipMethod");
                radlist.DataSource = solutionlist;
                radlist.DataTextField = "MethodType";
                radlist.DataValueField = "idEDIShipMethod";
                radlist.DataBind();
                radlist.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridShipmentMethods_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
                RadDropDownList radlist = (RadDropDownList)edit.FindControl("radListEDIShipMethod");

                int requestID = 0;
                int.TryParse(Request.QueryString["requestID"], out requestID);

                if (!String.IsNullOrEmpty(radlist.SelectedText)  )
                {
                    clsEDIShipMethod contact = new clsEDIShipMethod()
                    {
                        idEDIShipMethodType = Convert.ToInt16(radlist.SelectedValue),
                        idRequest = requestID,
                        MethodType = radlist.SelectedText,
                        ActiveFlag = true,
                        CreatedOn = DateTime.Now,
                        CreatedBy = (string)(Session["userName"])
                    };
                    List<clsEDIShipMethod> EDIShipMethList = new List<clsEDIShipMethod>();
                    if (requestID != 0)
                    {
                        int inewID = 0;
                        SrvEDIShipMethod.Insert(contact, out inewID);
                        EDIShipMethList = SrvEDIShipMethod.GetEDIShipMethodTypesByidRequest(requestID);
                    }
                    else
                    {
                        EDIShipMethList = (List<clsEDIShipMethod>)Session["EDIShipMethList"];
                        EDIShipMethList.Add(contact);
                    }
                    Session["EDIShipMethList"] = EDIShipMethList;
                    gridShipmentMethods.DataSource = EDIShipMethList;
                    gridShipmentMethods.DataBind();
                    UpdateTabs();
                }
                else
                    e.Canceled = true;
            }
            RadMultiPage1.SelectedIndex = 3;
            RadTabStrip1.Tabs[3].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void gridShipmentMethods_UpdateCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.UpdateCommandName)
        {
            if (e.Item is GridEditFormItem)
            {
                GridEditFormItem item = (GridEditFormItem)e.Item;
            }
        }
    }
    protected void gridShipmentMethods_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<clsEDIShipMethod> EDIShipMethList = (List<clsEDIShipMethod>)Session["EDIShipMethList"];
            int rownum = e.Item.ItemIndex;
            clsEDIShipMethod currentrow = EDIShipMethList[rownum];
            SrvEDIShipMethod.Remove(currentrow.idEDIShipMethod);
            EDIShipMethList.Remove(currentrow);
            Session["EDIShipMethList"] = EDIShipMethList;
            gridShipmentMethods.DataSource = EDIShipMethList;
            gridShipmentMethods.Rebind();
            RadMultiPage1.SelectedIndex = 3;
            RadTabStrip1.Tabs[3].Selected = true;
            UpdateTabs();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void radListEDIShipMethodIdxChanged(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 3;
        RadTabStrip1.Tabs[3].Selected = true;
    }

    #endregion
    #region  FreightAuditors grid
    protected void gridFreightAuditors_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            if (requestID != 0)
            {
                (sender as RadGrid).DataSource = SrvFreightAuditorsDiscReq.GetFreightAuditorsByID(requestID);
            }
            //else if (bOneTimeContactGrid)
            //{
            //    List<clsEDIShipMethod> EDIShipMethList = SrvEDIShipMethod.GetEDIShipMethodMockData();
            //    Session["EDIShipMethList"] = EDIShipMethList;
            //    (sender as RadGrid).DataSource = EDIShipMethList;
            //    bOneTimeContactGrid = false;
            //}
            else
            {
                (sender as RadGrid).DataSource = (List<clsFreightAuditorsDiscReq>)Session["FreightAuditorsList"];
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridFreightAuditors_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;

                List<clsFreightAuditor> solutionlist = SrvFreightAuditor.GetFreightAuditors();
                RadDropDownList radlist = (RadDropDownList)edit.FindControl("radListCompanyName");
                radlist.DataSource = solutionlist;
                radlist.DataTextField = "CompanyName";
                radlist.DataValueField = "idFreightAuditor";
                radlist.DataBind();
                radlist.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridFreightAuditors_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
                RadDropDownList radlist = (RadDropDownList)edit.FindControl("radListCompanyName");

                int requestID = 0;
                int.TryParse(Request.QueryString["requestID"], out requestID);

                if (!String.IsNullOrEmpty(radlist.SelectedText))
                {
                    clsFreightAuditorsDiscReq FreightAuditor = new clsFreightAuditorsDiscReq()
                    {
                        idFreightAuditor = Convert.ToInt16(radlist.SelectedValue),
                        CompanyName = radlist.SelectedText,
                        idRequest = requestID,
                        ActiveFlag = true,
                        CreatedOn = DateTime.Now,
                        CreatedBy = (string)(Session["userName"])
                    };
                    List<clsFreightAuditorsDiscReq> FreightAuditorList = new List<clsFreightAuditorsDiscReq>();
                    if (requestID != 0)
                    {
                        SrvFreightAuditorsDiscReq.UpdateFreightAuditor(FreightAuditor);
                        FreightAuditorList = SrvFreightAuditorsDiscReq.GetFreightAuditorsByID(requestID);
                    }
                    else
                    {
                        FreightAuditorList = (List<clsFreightAuditorsDiscReq>)Session["FreightAuditorsList"];
                        FreightAuditorList.Add(FreightAuditor);
                    }
                    Session["FreightAuditorsList"] = FreightAuditorList;
                    gridFreightAuditors.DataSource = FreightAuditorList;
                    gridFreightAuditors.DataBind();
                }
                else
                    e.Canceled = true;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void gridFreightAuditors_UpdateCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.UpdateCommandName)
        {
            if (e.Item is GridEditFormItem)
            {
                GridEditFormItem item = (GridEditFormItem)e.Item;
            }
        }
    }
    protected void gridFreightAuditors_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<clsFreightAuditorsDiscReq> FreightAuditorList = (List<clsFreightAuditorsDiscReq>)Session["FreightAuditorsList"];
            int rownum = e.Item.ItemIndex;
            clsFreightAuditorsDiscReq currentrow = FreightAuditorList[rownum];
            SrvFreightAuditorsDiscReq.Remove(currentrow.idFreightAuditorDiscReq);
            FreightAuditorList.Remove(currentrow);
            Session["FreightAuditorsList"] = FreightAuditorList;
            gridFreightAuditors.DataSource = FreightAuditorList;
            gridFreightAuditors.Rebind();
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }

    #endregion
    #region  EDITransactions grid
    protected void gridEDITransactions_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            if (requestID != 0)
            {
                (sender as RadGrid).DataSource = SrvEDITransaction.GetEDITransactionsByidRequest(requestID);
            }
            else if(bOneTimeContactGrid)
            {
                List<clsEDITransaction> EDITransList = SrvEDITransaction.GetEDITransactionsMockData();
                Session["EDITransList"] = EDITransList;
                (sender as RadGrid).DataSource = EDITransList;
                bOneTimeContactGrid = false;
            }
            else
            {
                (sender as RadGrid).DataSource = (List<clsEDITransaction>)Session["EDITransList"];
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridEDITransactions_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;

                List<clsEDITransactionType> qTransType = SrvEDITransactionType.GetEDITransactionTypes();
                RadDropDownList radlist = (RadDropDownList)edit.FindControl("radListEDITransList");
                radlist.DataSource = qTransType;
                radlist.DataTextField = "EDITranscationType";
                radlist.DataValueField = "idEDITranscationType";
                radlist.DataBind();
                radlist.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridEDITransactions_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
                RadDropDownList radlist = (RadDropDownList)edit.FindControl("radListEDITransList");

                int requestID = 0;
                int.TryParse(Request.QueryString["requestID"], out requestID);

                if (!String.IsNullOrEmpty(radlist.SelectedText))
                {
                    clsEDITransaction EDITrans = new clsEDITransaction()
                    {
                        idEDITranscationType = Convert.ToInt16(radlist.SelectedValue),
                        idRequest = requestID,
                        ActiveFlag = true,
                        CreatedOn = DateTime.Now,
                        CreatedBy = (string)(Session["userName"])
                    };
                    List<clsEDITransaction> EDITransList = new List<clsEDITransaction>();
                    if (requestID != 0)
                    {
                        int inewID = 0;
                        SrvEDITransaction.Insert(EDITrans, out inewID);
                        EDITransList = SrvEDITransaction.GetEDITransactionsByidRequest(requestID);
                    }
                    else
                    {
                        EDITransList = (List<clsEDITransaction>)Session["EDITransList"];
                        clsEDITransactionType qEDITransType = SrvEDITransactionType.GetOneEDITransactionType(EDITrans.idEDITranscationType);
                        EDITrans.EDITranscationType = qEDITransType.EDITranscationType;
                        EDITransList.Add(EDITrans);
                    }
                    Session["EDITransList"] = EDITransList;
                    gridEDITransactions.DataSource = EDITransList;
                    gridEDITransactions.DataBind();
                }
                else
                    e.Canceled = true;
            }
            RadMultiPage1.SelectedIndex = 3;
            RadTabStrip1.Tabs[3].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void gridEDITransactions_UpdateCommand(object source, GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.UpdateCommandName)
        {
            if (e.Item is GridEditFormItem)
            {
                GridEditFormItem item = (GridEditFormItem)e.Item;
            }
        }
    }
    protected void gridEDITransactions_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<clsEDITransaction> EDITransList = (List<clsEDITransaction>)Session["EDITransList"];
            int rownum = e.Item.ItemIndex;
            clsEDITransaction currentrow = EDITransList[rownum];
            SrvEDITransaction.Remove(currentrow.idEDITranscation);
            EDITransList.Remove(currentrow);
            Session["EDITransList"] = EDITransList;
            gridEDITransactions.DataSource = EDITransList;
            gridEDITransactions.Rebind();
            RadMultiPage1.SelectedIndex = 3;
            RadTabStrip1.Tabs[3].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void radListEDITransIdxChanged(object sender, EventArgs e)
    {
        RadMultiPage1.SelectedIndex = 3;
        RadTabStrip1.Tabs[3].Selected = true;
    }

    #endregion
    #region  EDI 210 Accounts grid
    protected void gridEDI210Accounts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            List<clsEDIAccount> EDI210AccountsList = SrvEDIAccount.GetEDIAccountByidRequest(requestID, INVOICE_COURIER_EDI);
            if (EDI210AccountsList != null)
            {
                (sender as RadGrid).DataSource = EDI210AccountsList;
                Session["EDI210AccountsList"] = EDI210AccountsList;
            }
            else
                (sender as RadGrid).DataSource = new List<clsEDIAccount>();

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridEDI210Accounts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;

                //List<clsEDITransactionType> qTransType = SrvEDITransactionType.GetEDITransactionTypes();
                //RadTextBox radTextBx = (RadTextBox)edit.FindControl("txtAccountNum");
                //radTextBx.Text = "Hello";
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridEDI210Accounts_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
                RadTextBox radTextBx = (RadTextBox)edit.FindControl("txtAccountNum");

                int requestID = 0;
                int.TryParse(Request.QueryString["requestID"], out requestID);

                if (!String.IsNullOrEmpty(radTextBx.Text))
                {
                    clsEDIAccount EDIAcct = new clsEDIAccount()
                    {
                        AccountNumber = radTextBx.Text.ToString(),
                        idRequest = requestID,
                        idEDITranscationType = INVOICE_COURIER_EDI,
                        ActiveFlag = true,
                        CreatedOn = DateTime.Now,
                        CreatedBy = (string)(Session["userName"])
                    };
                    int inewID = 0;
                    SrvEDIAccount.Insert(EDIAcct, out inewID);
                    List<clsEDIAccount> EDI210AccountsList = SrvEDIAccount.GetEDIAccountByidRequest(requestID, INVOICE_COURIER_EDI);
                    Session["EDI210AccountsList"] = EDI210AccountsList;
                    gridEDI210Accounts.DataSource = EDI210AccountsList;
                    gridEDI210Accounts.DataBind();
                }
                else
                    e.Canceled = true;
            }
            RadMultiPage1.SelectedIndex = 6;
            RadTabStrip1.Tabs[6].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void gridEDI210Accounts_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<clsEDIAccount> EDI210AccountsList = (List<clsEDIAccount>)Session["EDI210AccountsList"];
            int rownum = e.Item.ItemIndex;
            clsEDIAccount currentrow = EDI210AccountsList[rownum];
            SrvEDIAccount.Remove(currentrow.idEDIAccount);
            EDI210AccountsList.Remove(currentrow);
            Session["EDI210AccountsList"] = EDI210AccountsList;
            gridEDI210Accounts.DataSource = EDI210AccountsList;
            gridEDI210Accounts.Rebind();
            RadMultiPage1.SelectedIndex = 6;
            RadTabStrip1.Tabs[6].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    #endregion
    #region  EDI 214 Accounts grid
    protected void gridEDI214Accounts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            List<clsEDIAccount> EDI214AccountsList = SrvEDIAccount.GetEDIAccountByidRequest(requestID, SHIPMENT_STATUS_COURIER_EDI);
            if (EDI214AccountsList != null)
            {
                (sender as RadGrid).DataSource = EDI214AccountsList;
                Session["EDI214AccountsList"] = EDI214AccountsList;
            }
            else
                (sender as RadGrid).DataSource = new List<clsEDIAccount>();
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { Method = GetCurrentMethod(), ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now, CreatedBy = Session["userName"].ToString() };
            SrvExceptionLogging.Insert(error, out lnewID);

            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridEDI214Accounts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;

                //List<clsEDITransactionType> qTransType = SrvEDITransactionType.GetEDITransactionTypes();
                //RadTextBox radTextBx = (RadTextBox)edit.FindControl("txtAccountNum");
                //radTextBx.Text = "Hello";
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridEDI214Accounts_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
                RadTextBox radTextBx = (RadTextBox)edit.FindControl("txtAccountNum");

                int requestID = 0;
                int.TryParse(Request.QueryString["requestID"], out requestID);

                if (!String.IsNullOrEmpty(radTextBx.Text))
                {
                    clsEDIAccount EDIAcct = new clsEDIAccount()
                    {
                        AccountNumber = radTextBx.Text.ToString(),
                        idRequest = requestID,
                        idEDITranscationType = SHIPMENT_STATUS_COURIER_EDI,
                        ActiveFlag = true,
                        CreatedOn = DateTime.Now,
                        CreatedBy = (string)(Session["userName"])
                    };
                    int inewID = 0;
                    SrvEDIAccount.Insert(EDIAcct, out inewID);
                    List<clsEDIAccount> EDI214AccountsList = SrvEDIAccount.GetEDIAccountByidRequest(requestID, SHIPMENT_STATUS_COURIER_EDI);
                    Session["EDI214AccountsList"] = EDI214AccountsList;
                    gridEDI214Accounts.DataSource = EDI214AccountsList;
                    gridEDI214Accounts.DataBind();
                }
                else
                    e.Canceled = true;
            }
            RadMultiPage1.SelectedIndex = 6;
            RadTabStrip1.Tabs[6].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void gridEDI214Accounts_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<clsEDIAccount> EDI214AccountsList = (List<clsEDIAccount>)Session["EDI214AccountsList"];
            int rownum = e.Item.ItemIndex;
            clsEDIAccount currentrow = EDI214AccountsList[rownum];
            SrvEDIAccount.Remove(currentrow.idEDIAccount);
            EDI214AccountsList.Remove(currentrow);
            Session["EDI214AccountsList"] = EDI214AccountsList;
            gridEDI214Accounts.DataSource = EDI214AccountsList;
            gridEDI214Accounts.Rebind();
            RadMultiPage1.SelectedIndex = 6;
            RadTabStrip1.Tabs[6].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    #endregion
    #region  NonCourier EDI 210 Accounts grid
    protected void gridNonCourierEDI210Accounts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            List<clsEDIAccount> EDI210AccountsList = SrvEDIAccount.GetEDIAccountByidRequest(requestID, INVOICE_NON_COURIER_EDI);
            if (EDI210AccountsList != null)
            {
                (sender as RadGrid).DataSource = EDI210AccountsList;
                Session["NonCourierEDI210AccountsList"] = EDI210AccountsList;
            }
            else
                (sender as RadGrid).DataSource = new List<clsEDIAccount>();

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridNonCourierEDI210Accounts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridNonCourierEDI210Accounts_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
                RadTextBox radTextBx = (RadTextBox)edit.FindControl("txtAccountNum");

                int requestID = 0;
                int.TryParse(Request.QueryString["requestID"], out requestID);

                if (!String.IsNullOrEmpty(radTextBx.Text))
                {
                    clsEDIAccount EDIAcct = new clsEDIAccount()
                    {
                        AccountNumber = radTextBx.Text.ToString(),
                        idRequest = requestID,
                        idEDITranscationType = INVOICE_NON_COURIER_EDI,
                        ActiveFlag = true,
                        CreatedOn = DateTime.Now,
                        CreatedBy = (string)(Session["userName"])
                    };
                    int inewID = 0;
                    SrvEDIAccount.Insert(EDIAcct, out inewID);
                    List<clsEDIAccount> EDI210AccountsList = SrvEDIAccount.GetEDIAccountByidRequest(requestID, INVOICE_NON_COURIER_EDI);
                    Session["NonCourierEDI210AccountsList"] = EDI210AccountsList;
                    gridNonCourierEDI210Accounts.DataSource = EDI210AccountsList;
                    gridNonCourierEDI210Accounts.DataBind();
                }
                else
                    e.Canceled = true;
            }
            RadMultiPage1.SelectedIndex = 7;
            RadTabStrip1.Tabs[7].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void gridNonCourierEDI210Accounts_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<clsEDIAccount> EDI210AccountsList = (List<clsEDIAccount>)Session["NonCourierEDI210AccountsList"];
            int rownum = e.Item.ItemIndex;
            clsEDIAccount currentrow = EDI210AccountsList[rownum];
            SrvEDIAccount.Remove(currentrow.idEDIAccount);
            EDI210AccountsList.Remove(currentrow);
            Session["NonCourierEDI210AccountsList"] = EDI210AccountsList;
            gridNonCourierEDI210Accounts.DataSource = EDI210AccountsList;
            gridNonCourierEDI210Accounts.Rebind();
            RadMultiPage1.SelectedIndex = 7;
            RadTabStrip1.Tabs[7].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    #endregion
    #region  NonCourier EDI 214 Accounts grid
    protected void gridNonCourierEDI214Accounts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            List<clsEDIAccount> EDI214AccountsList = SrvEDIAccount.GetEDIAccountByidRequest(requestID, SHIPMENT_STATUS_NON_COURIER_EDI);
            if (EDI214AccountsList != null)
            {
                (sender as RadGrid).DataSource = EDI214AccountsList;
                Session["NonCourierEDI214AccountsList"] = EDI214AccountsList;
            }
            else
                (sender as RadGrid).DataSource = new List<clsEDIAccount>();

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridNonCourierEDI214Accounts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridNonCourierEDI214Accounts_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
                RadTextBox radTextBx = (RadTextBox)edit.FindControl("txtAccountNum");

                int requestID = 0;
                int.TryParse(Request.QueryString["requestID"], out requestID);

                if (!String.IsNullOrEmpty(radTextBx.Text))
                {
                    clsEDIAccount EDIAcct = new clsEDIAccount()
                    {
                        AccountNumber = radTextBx.Text.ToString(),
                        idRequest = requestID,
                        idEDITranscationType = SHIPMENT_STATUS_NON_COURIER_EDI,
                        ActiveFlag = true,
                        CreatedOn = DateTime.Now,
                        CreatedBy = (string)(Session["userName"])
                    };
                    int inewID = 0;
                    SrvEDIAccount.Insert(EDIAcct, out inewID);
                    List<clsEDIAccount> EDI214AccountsList = SrvEDIAccount.GetEDIAccountByidRequest(requestID, SHIPMENT_STATUS_NON_COURIER_EDI);
                    Session["NonCourierEDI214AccountsList"] = EDI214AccountsList;
                    gridNonCourierEDI214Accounts.DataSource = EDI214AccountsList;
                    gridNonCourierEDI214Accounts.DataBind();
                }
                else
                    e.Canceled = true;
            }
            RadMultiPage1.SelectedIndex = 7;
            RadTabStrip1.Tabs[7].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void gridNonCourierEDI214Accounts_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<clsEDIAccount> EDI214AccountsList = (List<clsEDIAccount>)Session["NonCourierEDI214AccountsList"];
            int rownum = e.Item.ItemIndex;
            clsEDIAccount currentrow = EDI214AccountsList[rownum];
            SrvEDIAccount.Remove(currentrow.idEDIAccount);
            EDI214AccountsList.Remove(currentrow);
            Session["NonCourierEDI214AccountsList"] = EDI214AccountsList;
            gridNonCourierEDI214Accounts.DataSource = EDI214AccountsList;
            gridNonCourierEDI214Accounts.Rebind();
            RadMultiPage1.SelectedIndex = 7;
            RadTabStrip1.Tabs[7].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    #endregion
    #region  NonCourier PuroPost Accounts grid
    protected void gridNonCourierPuroPostAccounts_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            int requestID = 0;
            int.TryParse(Request.QueryString["requestID"], out requestID);
            List<clsEDIAccount> EDI214AccountsList = SrvEDIAccount.GetEDIAccountByidRequest(requestID, PUROPOST_NON_COURIER_EDI);
            if (EDI214AccountsList != null)
            {
                (sender as RadGrid).DataSource = EDI214AccountsList;
                Session["NonCourierPUROPOSTAccountsList"] = EDI214AccountsList;
            }
            else
                (sender as RadGrid).DataSource = new List<clsEDIAccount>();

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridNonCourierPuroPostAccounts_ItemDataBound(object sender, GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void gridNonCourierPuroPostAccounts_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                GridEditFormItem edit = (GridEditFormItem)e.Item;
                RadTextBox radTextBx = (RadTextBox)edit.FindControl("txtAccountNum");

                int requestID = 0;
                int.TryParse(Request.QueryString["requestID"], out requestID);

                if (!String.IsNullOrEmpty(radTextBx.Text))
                {
                    clsEDIAccount EDIAcct = new clsEDIAccount()
                    {
                        AccountNumber = radTextBx.Text.ToString(),
                        idRequest = requestID,
                        idEDITranscationType = PUROPOST_NON_COURIER_EDI,
                        ActiveFlag = true,
                        CreatedOn = DateTime.Now,
                        CreatedBy = (string)(Session["userName"])
                    };
                    int inewID = 0;
                    SrvEDIAccount.Insert(EDIAcct, out inewID);
                    List<clsEDIAccount> EDI214AccountsList = SrvEDIAccount.GetEDIAccountByidRequest(requestID, PUROPOST_NON_COURIER_EDI);
                    Session["NonCourierPUROPOSTAccountsList"] = EDI214AccountsList;
                    gridNonCourierPuroPostAccounts.DataSource = EDI214AccountsList;
                    gridNonCourierPuroPostAccounts.DataBind();
                }
                else
                    e.Canceled = true;
            }
            RadMultiPage1.SelectedIndex = 7;
            RadTabStrip1.Tabs[7].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    protected void gridNonCourierPuroPostAccounts_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<clsEDIAccount> EDI214AccountsList = (List<clsEDIAccount>)Session["NonCourierPUROPOSTAccountsList"];
            int rownum = e.Item.ItemIndex;
            clsEDIAccount currentrow = EDI214AccountsList[rownum];
            SrvEDIAccount.Remove(currentrow.idEDIAccount);
            EDI214AccountsList.Remove(currentrow);
            Session["NonCourierPUROPOSTAccountsList"] = EDI214AccountsList;
            gridNonCourierPuroPostAccounts.DataSource = EDI214AccountsList;
            gridNonCourierPuroPostAccounts.Rebind();
            RadMultiPage1.SelectedIndex = 7;
            RadTabStrip1.Tabs[7].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }
    #endregion

    protected void displayExistingRequest(Int32 requestID)
    {
        try
        {
            //DISPLAY EXISTING REQUEST
            ClsDiscoveryRequest dr = new ClsDiscoveryRequest();
            ClsDiscoveryRequest request = dr.GetDiscoveryRequest(requestID);
            ClsDiscoveryRequestDetails drd = new ClsDiscoveryRequestDetails();
            ClsDiscoveryRequestDetails details = drd.GetDiscoveryRequestDetails(requestID, "");
            clsContact contact = SrvContact.GetContactsByRequestID(requestID).FirstOrDefault();
            //Existing Request may not have Details entered yet
            if (details == null)
            {
                details = new ClsDiscoveryRequestDetails();
            }
            //< td > Strategic ? &nbsp;
            string userRole = ((string)Session["userRole"]).ToLower();
            if (request.StrategicFlag == false || request.StrategicFlag == null)
            {
                rddlStrategic.SelectedText = "No";
            }
            else
            {
                rddlStrategic.SelectedText = "Yes";
            }

            //HEADING
            lblDRF.Text = request.CustomerName;
            txtSalesProfessional.Text = request.SalesRepName;
            rddlDistrict.SelectedText = request.District;
            rddlBranch.SelectedText = request.Branch;
            txtEmail.Text = request.SalesRepEmail;
            lblSubmittedBy.Text = request.CreatedBy;
            lblUpdatedBy.Text = request.UpdatedBy;
            lblUpdatedOn.Text = request.UpdatedOn.ToString();
            lblReqID.Visible = true;
            lblRequestID.Visible = true;
            lblRequestID.Text = requestID.ToString();

            //Tab 1 - Customer Information          
            rddlSolutionType.SelectedValue = request.idSolutionType.ToString();
            if (request.idRequestType != null)
                rddlRequestType.SelectedValue = request.idRequestType.ToString();
            txtCustomerName.Text = request.CustomerName;
            txtCustomerAddress.Text = request.Address;
            txtCustomerCity.Text = request.City;
            txtCustomerState.Text = request.State;
            txtCustomerZip.Text = request.Zipcode;
            txtCustomerCountry.Text = request.Country;
            string annualRev = String.Format("{0:C}", request.ProjectedRevenue);
            txtRevenue.Text = annualRev;
            txtCommodity.Text = request.Commodity;
            txtWebsite.Text = request.CustomerWebsite;
            //only ITmanager can change the name
            if (userRole == "itmanager" || userRole == "itadmin")
            {
                txtCustomerName.Enabled = true;
            }
            else
            {
                txtCustomerName.Enabled = false;
            }

            //Tab2 - Contact Information

            //Tab3 - Current Solution
            txtareaCurrentSolution.Text = request.CurrentSolution;

            //Tab - EDI Services
            txtBxEDISolutionSummary.Text = request.EDIDetails;
            txtBxCustomerEDIDetails.Text = request.EDIDetails;
            if (request.FreightAuditor == false || request.FreightAuditor == null)
                comboxFreightAuditor.SelectedText = "No";
            else
                comboxFreightAuditor.SelectedText = "Yes";

            //Tab4 - Shipping Services
            //services
            List<ClsDiscoveryRequestSvcs> svcList = repository.GetProposedServices(requestID);
            Session["proposedSvcList"] = svcList;
            //service list and product list are tied together
            getShippingProducts();
            rgSvcGrid.DataSource = svcList;
            rgSvcGrid.DataBind();

            txtProposedNotes.Text = request.SalesComments;
            //txtProposedCustoms.Text = request.ProposedCustoms;
            rddlCustomsList.SelectedText = request.ProposedCustoms;
            string customsval = rddlCustomsList.SelectedText.ToLower().Trim();
            if (customsval != "none" && customsval != "tbd")
            {
                lblCustomsBroker.Visible = true;
                rddlCustomsBroker.Visible = true;
                if (request.idBroker != null)
                    rddlCustomsBroker.SelectedValue = request.idBroker.ToString();
                string customsbroker = rddlCustomsBroker.SelectedText.ToLower().Trim();
                if (customsbroker == "other")
                {
                    lblOtherBroker.Visible = true;
                    txtCustomsBroker.Visible = true;
                    txtCustomsBroker.Text = request.OtherBrokerName;
                }
            }
            //Tab5 - Profile
            //Solution Summary
            rddlITBA.SelectedValue = request.idITBA.ToString();
            rddlITBA2.SelectedValue = request.idITBA.ToString();
            Session["ITBA"] = request.idITBA.ToString();
            cmboxEDISpecialist.SelectedValue = request.idEDISpecialist.ToString();
            Session["EDISpecialist"] = request.idEDISpecialist.ToString();
            cmboxBillingSpecialist.SelectedValue = request.idBillingSpecialist.ToString();
            cmboxCollectionSpecialist.SelectedValue = request.idCollectionSpecialist.ToString();
            dateTargetGoLive.SelectedDate = request.EDITargetGoLive;
            dateCurrentGoLive.SelectedDate = request.EDICurrentGoLive;
            dateActualGoLive.SelectedDate = request.EDIActualGoLive;

            if (request.FreightAuditor == false || request.FreightAuditor == null)
                comboxFreightAuditorInvolved.SelectedText = "No";
            else
                comboxFreightAuditorInvolved.SelectedText = "Yes";
            bool bShowAudit = false;
            if (request.AuditorPortal == false || request.AuditorPortal == null)
            {
                comboxCustAuditPortal.SelectedText = "No";
                hiddenShowAuditorPortal.Value = "false";
            }
            else
            {
                comboxCustAuditPortal.SelectedText = "Yes";
                hiddenShowAuditorPortal.Value = "true";
                bShowAudit = true;
            }
            lblCustAuditPortalYes.Visible = bShowAudit;
            lblCustAuditPortalStar.Visible = bShowAudit;
            lblCustAuditPortalURL.Visible = bShowAudit;
            txtBxAuditoURL.Visible = bShowAudit;
            lblCustAuditPortalUserName.Visible = bShowAudit;
            txtBxAuditoUserName.Visible = bShowAudit;
            lblCustAuditPortalPassword.Visible = bShowAudit;
            txtBxAuditoPassword.Visible = bShowAudit;

            if (request.idVendorType != null)
                rddlVendorType.SelectedValue = request.idVendorType.ToString();
            rddlPhase.SelectedValue = request.idOnboardingPhase.ToString();
            cmboxOnboardingPhase.SelectedValue = request.idEDIOnboardingPhase.ToString();
            int phase = Convert.ToInt16(rddlPhase.SelectedValue);
            if (phase == ClosedID || phase == OnHoldID)
            {
                rddlCloseReason.Visible = true;
                rddlCloseReason.SelectedText = request.CloseReason;
            }
            else
            {
                rddlCloseReason.Visible = false;
            }
            rddlShippingChannel.SelectedValue = request.idShippingChannel.ToString();
            rddlThirdPartyVendor.SelectedValue = request.idVendor.ToString();
            rdCurrentTarget.SelectedDate = request.CurrentGoLive;
            rdTarget.SelectedDate = request.TargetGoLive;
            rdActual.SelectedDate = request.ActualGoLive;
            txtSolutionSummary.Text = request.SolutionSummary;
            txtRoute.Text = request.Route;
            txtBxAuditoURL.Text = request.AuditorURL;
            txtBxAuditoUserName.Text = request.AuditorUserName;
            txtBxAuditoPassword.Text = request.AuditorPassword;

            if (request.worldpakFlag == null) request.worldpakFlag = false;
            if (request.thirdpartyFlag == null) request.thirdpartyFlag = false;
            if (request.customFlag == null) request.customFlag = false;
            if (request.DataScrubFlag == null) request.DataScrubFlag = false;
            if (request.StrategicFlag == null) request.StrategicFlag = false;
            if (request.EDICustomizedFlag == null) request.EDICustomizedFlag = false;
            cbxWPK.Checked = (Boolean)request.worldpakFlag;
            cbx3pv.Checked = (Boolean)request.thirdpartyFlag;
            cbxCustom.Checked = (Boolean)request.customFlag;
            cbxDataScrub.Checked = (Boolean)request.DataScrubFlag;

            if (cbx3pv.Checked == true)
            {
                rddlThirdPartyVendor.Visible = true;
                lbl3pv.Visible = true;
            }

            //Proposed Products
            List<ClsDiscoveryRequestProds> prodList = repository.GetProposedProducts(requestID);
            Session["productList"] = prodList;
            rgProductGrid.DataSource = prodList;
            rgProductGrid.DataBind();

            //SHIPPING DETAILS

            //Courier
            txtCourierAcct.Text = details.CourierAcctNbr;
            txtCourierContract.Text = details.CourierContractNbr;
            txtCourierPin.Text = details.CourierPinPrefix;
            txtCourierTransitDays.Text = details.CourierTransitDays.ToString();
            courierIaddress.Text = details.CourierInductionAddress;
            courierIcity.Text = details.CourierInductionCity;
            courierIstate.Text = details.CourierInductionState;
            courierIzip.Text = details.CourierInductionZip;
            courierIcountry.Text = details.CourierInductionCountry;
            txtCourierFTPuser.Text = details.CourierFTPusername;
            txtCourierFTPpwd.Text = details.CourierFTPpwd;
            txtCourierSenderID.Text = details.CourierFTPsenderID;
            txtSandboxFTPuser.Text = details.CourierSandboxFTPusername;
            txtSandboxFTPpwd.Text = details.CourierSandboxFTPpwd;
            if (details.CourierFTPCustOwnFlag == null) details.CourierFTPCustOwnFlag = false;
            cbxCourierFTPCustOwn.Checked = (bool)details.CourierFTPCustOwnFlag;
            //If InductionDesc in list, select it otherwise select Other 
            if (details.CourierInductionDesc != null)
                rddlCourierInduction.SelectedText = details.CourierInductionDesc.Trim();
            if (details.CourierInductionDesc == null || rddlCourierInduction.SelectedIndex < 0)
            {
                rddlCourierInduction.SelectedText = "OTHER";
            }
            //Courier EDI
            clsEDITransaction EDITrans = SrvEDITransaction.GetAEDITransactionsByidRequest(requestID, INVOICE_COURIER_EDI);
            if (EDITrans != null)
            {
                comboBxCourierEDI210.SelectedText = "Yes";
                if(EDITrans.CombinePayer.Value)
                    comboxCombinepayer.SelectedText = "Yes";
                else
                    comboxCombinepayer.SelectedText = "No";
                if (EDITrans.BatchInvoices.Value)
                    comboBoxBatchInvoices.SelectedText = "Yes";
                else
                    comboBoxBatchInvoices.SelectedText = "No";
            }
            else
                comboBxCourierEDI210.SelectedText = "No";
            SetCourierEDI210Controls();

            if (SrvEDITransaction.GetAEDITransactionsByidRequest(requestID, SHIPMENT_STATUS_COURIER_EDI) != null)
                comboBxCourierEDI214.SelectedText = "Yes";
            else
                comboBxCourierEDI214.SelectedText = "No";
            SetCourierEDI214Controls();

            EDITrans = SrvEDITransaction.GetAEDITransactionsByidRequest(requestID, INVOICE_NON_COURIER_EDI);
            if (EDITrans != null)
            {
                //if (SrvEDITransaction.GetAEDITransactionsByidRequest(requestID, INVOICE_NON_COURIER_EDI) != null)
                comboBxNonCourierEDI210.SelectedText = "Yes";
                //txtNonCourier210SFTP.Text = EDITrans.SFTPFolder;
                //comboNonCourier210TestEnvironment.SelectedText = (EDITrans.TestEnvironment.HasValue) ? (EDITrans.TestEnvironment.Value) ? "Yes" : "No" : "No"; 
                comboNonCourier210TestSent.SelectedText = (EDITrans.TestSentMethod == 1) ? "SFTP" : "via Email";
            }
            else
                comboBxNonCourierEDI210.SelectedText = "No";
            SetCourierNonCourierEDI210Controls();

            EDITrans = SrvEDITransaction.GetAEDITransactionsByidRequest(requestID, SHIPMENT_STATUS_NON_COURIER_EDI);
            if (EDITrans != null)
            {
                comboBxNonCourierEDI214.SelectedText = "Yes";
                //txtNonCourier214SFTP.Text = EDITrans.SFTPFolder;
                //comboNonCourier214TestEnvironment.SelectedText = (EDITrans.TestEnvironment.HasValue) ? (EDITrans.TestEnvironment.Value) ? "Yes" : "No" : "No"; 
                comboNonCourier214TestSent.SelectedText = (EDITrans.TestSentMethod == 1) ? "SFTP" : "via Email";
            }
            else
                comboBxNonCourierEDI214.SelectedText = "No";
            SetCourierNonCourierEDI214Controls();

            EDITrans = SrvEDITransaction.GetAEDITransactionsByidRequest(requestID, PUROPOST_NON_COURIER_EDI);
            if (EDITrans != null)
            {
                //if (SrvEDITransaction.GetAEDITransactionsByidRequest(requestID, PUROPOST_NON_COURIER_EDI) != null)
                comboBxNonCourierPuroPost.SelectedText = "Yes";
                //txtNonCourierPuroPostSFTP.Text = EDITrans.SFTPFolder;
            }
            else
                comboBxNonCourierPuroPost.SelectedText = "No";
            SetCourierNonCourierPuroPostStandControls();

            //LTL
            txtLTLAccount.Text = details.LTLAcctNbr;
            txtLTLminPro.Text = details.LTLMinProNbr;
            txtLTLmaxPro.Text = details.LTLMaxProNbr;
            txtLTLPin.Text = details.LTLPinPrefix;
            if (details.LTLAutomatedFlag == null)
                details.LTLAutomatedFlag = false;
            cbxLTLAuto.Checked = (bool)details.LTLAutomatedFlag;
            //CPC
            txtCPCAcct.Text = details.CPCAcctNbr;
            txtCPCContract.Text = details.CPCContractNbr;
            txtCPCSite.Text = details.CPCSiteNbr;
            txtCPCInduction.Text = details.CPCInductionNbr;
            txtCPCUser.Text = details.CPCUsername;
            txtCPCPwd.Text = details.CPCpwd;
            //PPST
            txtPPSTAcct.Text = details.PPSTAcctNbr;
            txtPPSTTransitDays.Text = details.PPSTTransitDays.ToString();
            ppstIaddress.Text = details.PPSTInductionAddress;
            ppstIcity.Text = details.PPSTInductionCity;
            ppstIstate.Text = details.PPSTInductionState;
            ppstIzip.Text = details.PPSTInductionZip;
            ppstIcountry.Text = details.PPSTInductionCountry;
            //If InductionDesc in list, select it otherwise select Other 
            if (details.PPSTInductionDesc != null)
                rddlPPSTInduction.SelectedText = details.PPSTInductionDesc.Trim();
            if (details.PPSTInductionDesc == null || rddlPPSTInduction.SelectedIndex < 0)
            {
                rddlPPSTInduction.SelectedText = "OTHER";
            }
            //PPST PLUS
            txtPPlusAcct.Text = details.PPlusAcctNbr;
            txtPPlusTransitDays.Text = details.PPlusTransitDays.ToString();
            PPlusIaddress.Text = details.PPlusInductionAddress;
            PPlusIcity.Text = details.PPlusInductionCity;
            PPlusIstate.Text = details.PPlusInductionState;
            PPlusIzip.Text = details.PPlusInductionZip;
            PPlusIcountry.Text = details.PPlusInductionCountry;
            //If InductionDesc in list, select it otherwise select Other 
            if (details.PPlusInductionDesc != null)
                rddlPPlusInduction.SelectedText = details.PPlusInductionDesc.Trim();
            if (details.PPlusInductionDesc == null || rddlPPlusInduction.SelectedIndex < 0)
            {
                rddlPPlusInduction.SelectedText = "OTHER";
            }
            //RETURNS
            txtReturnsAcct.Text = request.ReturnsAcctNbr;
            txtReturnsAddress.Text = request.ReturnsAddress;
            txtReturnsCity.Text = request.ReturnsCity;
            txtReturnsState.Text = request.ReturnsState;
            txtReturnsZip.Text = request.ReturnsZip;
            txtReturnsCountry.Text = request.ReturnsCountry;
            if (request.ReturnsDestroyFlag == null) request.ReturnsDestroyFlag = false;
            cbxDestroy.Checked = (bool)request.ReturnsDestroyFlag;
            if (request.ReturnsCreateLabelFlag == null) request.ReturnsCreateLabelFlag = false;
            cbxReturnLabel.Checked = (bool)request.ReturnsCreateLabelFlag;

            //WorldPak
            txtWPKsboxuser.Text = request.WPKSandboxUsername;
            txtWPKsboxpwd.Text = request.WPKSandboxPwd;
            txtWPKproduser.Text = request.WPKProdUsername;
            txtWPKprodpwd.Text = request.WPKProdPwd;
            if (request.WPKCustomExportFlag == null)
                request.WPKCustomExportFlag = false;
            cbxExportFile.Checked = (bool)request.WPKCustomExportFlag;
            if (request.WPKGhostScanFlag == null)
                request.WPKGhostScanFlag = false;
            cbxGhostScan.Checked = (bool)request.WPKGhostScanFlag;
            if (request.WPKEastWestSplitFlag == null)
                request.WPKEastWestSplitFlag = false;
            cbxSplit.Checked = (bool)request.WPKEastWestSplitFlag;
            if (request.WPKAddressUploadFlag == null)
                request.WPKAddressUploadFlag = false;
            cbxAddressBook.Checked = (bool)request.WPKAddressUploadFlag;
            if (request.WPKProductUploadFlag == null)
                request.WPKProductUploadFlag = false;
            cbxProdUpload.Checked = (bool)request.WPKProductUploadFlag;
            if (request.WPKDataEntryMethod == null) request.WPKDataEntryMethod = "";
            if (request.WPKEquipmentFlag == null)
                request.WPKEquipmentFlag = false;
            cbxEquipment.Checked = (bool)request.WPKEquipmentFlag;
            //show/hide Equipment bar based on flag value
            showhideEquipmentBar(cbxEquipment.Checked);
            rddlDataEntry.SelectedText = request.WPKDataEntryMethod.Trim();
            rddlEWselection.SelectedText = request.EWSelectBy;
            if (request.EWSortCodeFlag == null)
                request.EWSortCodeFlag = false;
            cbxEWsortcode.Checked = (bool)request.EWSortCodeFlag;
            txtEWesortcode.Text = request.EWEastSortCode;
            txtEWwsortcode.Text = request.EWWestSortCode;
            rddlEWselection.SelectedText = request.EWSelectBy;
            if (request.EWSepCloseoutFlag == null)
                request.EWSepCloseoutFlag = false;
            cbxEWcloseout.Checked = (bool)request.EWSepCloseoutFlag;
            if (request.EWSepPUFlag == null)
                request.EWSepPUFlag = false;
            cbxEWpickups.Checked = (bool)request.EWSepPUFlag;
            txtEWsorting.Text = request.EWSortDetails;
            txtEWmissort.Text = request.EWMissortDetails;
            ShowHideEastWest(cbxSplit.Checked);

            //FOR EAST WEST SPLIT, READ IN SECOND DETAIL RECORD WITH ShipRecordType = "West", DISPLAY VALUES
            if (request.WPKEastWestSplitFlag == true)
            {
                ClsDiscoveryRequestDetails detailsWest = drd.GetDiscoveryRequestDetails(requestID, "West");
                if (detailsWest != null)
                {
                    //COURIER WEST
                    txtCourierAcctWest.Text = detailsWest.CourierAcctNbr;
                    txtCourierContractWest.Text = detailsWest.CourierContractNbr;
                    txtCourierPinWest.Text = detailsWest.CourierPinPrefix;
                    txtCourierTransitDaysWest.Text = detailsWest.CourierTransitDays.ToString();
                    courierIaddressWest.Text = detailsWest.CourierInductionAddress;
                    courierIcityWest.Text = detailsWest.CourierInductionCity;
                    courierIstateWest.Text = detailsWest.CourierInductionState;
                    courierIzipWest.Text = detailsWest.CourierInductionZip;
                    courierIcountryWest.Text = detailsWest.CourierInductionCountry;
                    txtCourierFTPuserWest.Text = detailsWest.CourierFTPusername;
                    txtCourierFTPpwdWest.Text = detailsWest.CourierFTPpwd;
                    txtCourierSenderIDWest.Text = detailsWest.CourierFTPsenderID;
                    txtSandboxFTPuserWest.Text = detailsWest.CourierSandboxFTPusername;
                    txtSandboxFTPpwdWest.Text = detailsWest.CourierSandboxFTPpwd;
                    if (detailsWest.CourierFTPCustOwnFlag == null) detailsWest.CourierFTPCustOwnFlag = false;
                    cbxCourierFTPCustOwnWest.Checked = (bool)detailsWest.CourierFTPCustOwnFlag;
                    //If InductionDesc in list, select it otherwise select Other 
                    if (detailsWest.CourierInductionDesc != null)
                        rddlCourierInductionWest.SelectedText = detailsWest.CourierInductionDesc.Trim();
                    if (detailsWest.CourierInductionDesc == null || rddlCourierInductionWest.SelectedIndex < 0)
                    {
                        rddlCourierInductionWest.SelectedText = "OTHER";
                    }
                    //LTL WEST
                    txtLTLAccountWest.Text = detailsWest.LTLAcctNbr;
                    txtLTLminProWest.Text = detailsWest.LTLMinProNbr;
                    txtLTLmaxProWest.Text = detailsWest.LTLMaxProNbr;
                    txtLTLPinWest.Text = detailsWest.LTLPinPrefix;
                    if (detailsWest.LTLAutomatedFlag == null)
                        detailsWest.LTLAutomatedFlag = false;
                    //CPC WEST
                    txtCPCAcctWest.Text = detailsWest.CPCAcctNbr;
                    txtCPCContractWest.Text = detailsWest.CPCContractNbr;
                    txtCPCSiteWest.Text = detailsWest.CPCSiteNbr;
                    txtCPCInductionWest.Text = detailsWest.CPCInductionNbr;
                    txtCPCUserWest.Text = detailsWest.CPCUsername;
                    txtCPCPwdWest.Text = detailsWest.CPCpwd;
                    //PPST WEST
                    txtPPSTAcctWest.Text = detailsWest.PPSTAcctNbr;
                    txtPPSTTransitDaysWest.Text = detailsWest.PPSTTransitDays.ToString();
                    ppstIaddressWest.Text = detailsWest.PPSTInductionAddress;
                    ppstIcityWest.Text = detailsWest.PPSTInductionCity;
                    ppstIstateWest.Text = detailsWest.PPSTInductionState;
                    ppstIzipWest.Text = detailsWest.PPSTInductionZip;
                    ppstIcountryWest.Text = detailsWest.PPSTInductionCountry;
                    //If InductionDesc in list, select it otherwise select Other 
                    if (detailsWest.PPSTInductionDesc != null)
                        rddlPPSTInductionWest.SelectedText = detailsWest.PPSTInductionDesc.Trim();
                    if (details.PPSTInductionDesc == null || rddlPPSTInductionWest.SelectedIndex < 0)
                    {
                        rddlPPSTInductionWest.SelectedText = "OTHER";
                    }
                    //PPST PLUS WEST
                    txtPPlusAcctWest.Text = detailsWest.PPlusAcctNbr;
                    txtPPlusTransitDaysWest.Text = detailsWest.PPlusTransitDays.ToString();
                    PPlusIaddressWest.Text = detailsWest.PPlusInductionAddress;
                    PPlusIcityWest.Text = detailsWest.PPlusInductionCity;
                    PPlusIstateWest.Text = detailsWest.PPlusInductionState;
                    PPlusIzipWest.Text = detailsWest.PPlusInductionZip;
                    PPlusIcountryWest.Text = detailsWest.PPlusInductionCountry;
                    //If InductionDesc in list, select it otherwise select Other 
                    if (detailsWest.PPlusInductionDesc != null)
                        rddlPPlusInductionWest.SelectedText = detailsWest.PPlusInductionDesc.Trim();
                    if (detailsWest.PPlusInductionDesc == null || rddlPPlusInductionWest.SelectedIndex < 0)
                    {
                        rddlPPlusInductionWest.SelectedText = "OTHER";
                    }
                }
            }

            //Proposed Equipment
            List<ClsDiscoveryRequestEquip> equipList = repository.GetProposedEquipment(requestID);
            Session["equipmentList"] = equipList;
            rgEquipmentGrid.DataSource = equipList;
            rgEquipmentGrid.DataBind();

            //Contract Information
            txtContract.Text = request.ContractNumber;
            rdpStartDate.SelectedDate = request.ContractStartDate;
            rdpEndDate.SelectedDate = request.ContractEndDate;
            txtPaymentTerms.Text = request.PaymentTerms;
            rddlCurrency.SelectedText = request.ContractCurrency;

            //Proposed EDI
            rddlInvoiceType.SelectedText = request.invoiceType;
            List<ClsDiscoveryRequestEDI> ediList = repository.GetProposedEDI(requestID);
            Session["ediList"] = ediList;
            rgSolutionsGrid.DataSource = ediList;
            rgSolutionsGrid.DataBind();
            txtFTPLogin.Text = request.FTPUsername;
            txtFTPpwd.Text = request.FTPPassword;
            txtBillto.Text = request.billtoAccount;
            cbxCustomEDI.Checked = (bool)request.EDICustomizedFlag;

            //Customs
            if (request.customsFlag == null)
                request.customsFlag = false;
            if (request.elinkFlag == null)
                request.elinkFlag = false;
            chkCustoms.Checked = (bool)request.customsFlag;
            chkElink.Checked = (bool)request.elinkFlag;
            txtPARS.Text = request.PARS;
            txtPASS.Text = request.PASS;
            //If broker in list, select it otherwise show it in the Other box
            if (request.customsBroker != null)
                rddlBroker.SelectedValue = request.customsBroker.Trim();
            if (request.customsBroker == null || rddlBroker.SelectedIndex < 0)
            {
                txtOtherBroker.Text = request.customsBroker;
            }
            txtBrokerNumber.Text = request.BrokerNumber;

            //Account Support
            rddlControlBranch.SelectedText = request.ControlBranch;
            txtSupportUser.Text = request.SupportUser;
            txtSupportGroup.Text = request.SupportGroup;
            txtOffice.Text = request.Office;
            txtGroup.Text = request.Group;
            txtCRR.Text = request.CRR;

            //Migration Details
            rdpMigrationDate.SelectedDate = request.MigrationDate;
            txtPreMigration.Text = request.PreMigrationSolution;
            txtPostMigration.Text = request.PostMigrationSolution;

            // Contact List
            List<clsContact> contactList = repository.GetContacts(requestID);
            Session["contactList"] = contactList;
            contactGrid.DataSource = contactList;
            contactGrid.DataBind();

            // EDI Shipment Methods List
            List<clsEDIShipMethod> EDIShipMethList = SrvEDIShipMethod.GetEDIShipMethodTypesByidRequest(requestID);
            Session["EDIShipMethList"] = EDIShipMethList;
            gridShipmentMethods.DataSource = EDIShipMethList;
            gridShipmentMethods.DataBind();

            List<clsEDITransaction> EDITransList = SrvEDITransaction.GetEDITransactionsByidRequest(requestID);
            Session["EDITransList"] = EDITransList;
            gridEDITransactions.DataSource = EDITransList;
            gridEDITransactions.DataBind();

            List<clsFreightAuditorsDiscReq> FreightAuditorList = SrvFreightAuditorsDiscReq.GetFreightAuditorsByID(requestID);
            Session["FreightAuditorsList"] = FreightAuditorList;
            gridFreightAuditors.DataSource = FreightAuditorList;
            gridFreightAuditors.DataBind();

            if (userRole == "sales" || userRole == "salesdm" || userRole == "salesmanager")
            {
                //Do  not show Profile to Sales
                RadTabStrip1.Tabs[5].Visible = false;
                //Do not show File Uploads to Sales
                RadTabStrip1.Tabs[9].Visible = false;

                rfvTaskType.Enabled = false;
            }
            if (userRole != "itmanager" && userRole != "admin" && userRole != "itadmin")
            {
                rddlITBA.Enabled = false;
                rddlITBA2.Enabled = false;
            }

            //Display Total Minutes
            if (userRole == "itmanager" || userRole == "admin" || userRole == "itadmin" || userRole == "itba")
            {
                //get total minutes, turn into hours / minutes and display
                UpdateTimeSpent(requestID);
            }
        }
        catch (Exception ex)
        {
            //var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void UpdateTimeSpent(int requestID)
    {
        Int32 totalMinutes = repository.GetTotalTimeSpent(requestID);
        int hrs;
        int mins;
        hrs = totalMinutes / 60;
        mins = totalMinutes % 60;
        string hrstring;
        switch (hrs)
        {
            case 0:
                {
                    hrstring = "";
                    break;
                }
            case 1:
                {
                    hrstring = "1 hr";
                    break;
                }
            default:
                {
                    hrstring = hrs.ToString() + " hrs";
                    break;
                }
        }
        string hrmin = hrstring + " " + mins + " mins";
        lblTotalTime.Text = hrmin;
        lblTime.Visible = true;
        lblTotalTime.Visible = true;
    }

    //VALIDATORS

    protected void CustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            args.IsValid = true;
            CustomValidator.ErrorMessage = "";
            String ErrorMessage = sharedValidator();
            if (ErrorMessage != "")
            {
                args.IsValid = false;
                CustomValidator.ErrorMessage = ErrorMessage;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void CustomValidatorNew_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            args.IsValid = true;
            CustomValidatorNew.ErrorMessage = "";
            String ErrorMessage = sharedValidator();
            if (ErrorMessage != "")
            {
                args.IsValid = false;
                CustomValidatorNew.ErrorMessage = ErrorMessage;
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    private bool CheckForSalesUser(string ur)
    {
        bool bRetVal = false;
        if (ur == "sales" || ur == "salesdm" || ur == "salesmanager")
        {
            bRetVal = true;
        }
        return bRetVal;
    }
    public string sharedValidator()
    {
        string ErrorMessage = "";
        //header
        if (rddlDistrict.SelectedIndex < 0)
        {
            ErrorMessage = ErrorMessage + "<br>Please Select District";
        }
        if (rddlBranch.SelectedIndex < 0)
        {
            ErrorMessage = ErrorMessage + "<br>Please Select Branch";
        }
        //check tab1
        //Make sure Request Type is selected
        if (rddlRequestType.SelectedIndex < 0)
        {
            ErrorMessage = ErrorMessage + "<br>Request Type not selected on Customer Information tab";
        }
        //Check Customer Name or Relationship Name
        if (chkNewBus.Checked == true && txtCustomerName.Text == "")
        {
            ErrorMessage = ErrorMessage + "<br>Customer Name is missing";
        }
        if (chkNewBus.Checked == false && rddlRelationships.SelectedIndex < 0)
        {
            ErrorMessage = ErrorMessage + "<br>No Relationship Name Selected";
        }
        if (txtCustomerAddress.Text == "" || txtCustomerCity.Text == "" || txtCustomerState.Text == "" || txtCustomerZip.Text == "" || txtCustomerCountry.Text == "" || txtRevenue.Text == "" || txtCommodity.Text == "")
        {
            ErrorMessage = ErrorMessage + "<br>Missing one or more required fields on the Customer Information Tab";
        }
        //make sure revenue is a number
        decimal rev;
        string txtRev = txtRevenue.Text;
        txtRev = txtRev.Replace("$", "");
        bool isNumeric = decimal.TryParse(txtRev, out rev);
        if (isNumeric != true)
        {
            ErrorMessage = ErrorMessage + "<br>Annualized Revenue must be numeric";
        }
        //checked tab2
        List<clsContact> contactList = (List<clsContact>)Session["contactList"];
        if (contactList.Count() == 0)
        {
            ErrorMessage = ErrorMessage + "<br>At Least One Contact Must Be Supplied";
        }
        if ((rddlSolutionType.SelectedIndex == SOLUTION_TYPE_SHIPPING || rddlSolutionType.SelectedIndex == SOLUTION_TYPE_BOTH) && string.IsNullOrEmpty(txtareaCurrentSolution.Text))
        {
            ErrorMessage = ErrorMessage + "<br>Current Solution Description is Missing";
        }
        List<clsEDITransaction> EDITransList = (List<clsEDITransaction>)Session["EDITransList"];
        if (EDITransList.Count() == 0 && (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_EDI || rddlSolutionType.SelectedIndex == SOLUTION_TYPE_BOTH))
        {   // Shipping System
            ErrorMessage = ErrorMessage + "<br>At Least One EDI Transaction Must Be Supplied";
        }
        List<clsEDIShipMethod> EDIShipMethList = (List<clsEDIShipMethod>)Session["EDIShipMethList"];
        if (EDIShipMethList.Count() == 0 && (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_EDI || rddlSolutionType.SelectedIndex == SOLUTION_TYPE_BOTH))
        {   // Shipping System
            ErrorMessage = ErrorMessage + "<br>At Least One EDI Ship Method Must Be Supplied";
        }
        //check tab4
        string snumSvcs = rgSvcGrid.MasterTableView.Items.Count.ToString();
        Int16 numSvcs = Convert.ToInt16(snumSvcs);
        if ( (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_SHIPPING || rddlSolutionType.SelectedIndex == SOLUTION_TYPE_BOTH) && (numSvcs < 1) )
        {
            ErrorMessage = ErrorMessage + "<br>Must Choose At Least One Service";
        }

        if (txtTargetChangeReason.Visible == true && txtTargetChangeReason.Text == "")
        {
            ErrorMessage = ErrorMessage + "<br>Please Fill in Current Go-Live Date Change Reason";
        }
       
        //if(cmboxEDISpecialist.SelectedIndex < 0 && !CheckForSalesUser(Session["userRole"].ToString().ToLower()) )
        //    ErrorMessage = ErrorMessage + "<br>At Least One EDI Specialist Assignment Must Be Supplied";

        //if (cmboxBillingSpecialist.SelectedIndex < 0 && !CheckForSalesUser(Session["userRole"].ToString().ToLower()) )
        //    ErrorMessage = ErrorMessage + "<br>At Least One Billing Specialist Assignment Must Be Supplied";

        //if (cmboxCollectionSpecialist.SelectedIndex < 0 && !CheckForSalesUser(Session["userRole"].ToString().ToLower()) )
        //    ErrorMessage = ErrorMessage + "<br>At Least One Collection Specialist Assignment Must Be Supplied";

        if (comboxCustAuditPortal.SelectedText == "Yes")
        {
            if (string.IsNullOrEmpty(txtBxAuditoURL.Text) || string.IsNullOrEmpty(txtBxAuditoUserName.Text) || string.IsNullOrEmpty(txtBxAuditoPassword.Text))
            {
                ErrorMessage = ErrorMessage + "<br>Customer/Auditor Portal Information Must Be Supplied";
            }
        }
        return ErrorMessage;
    }
    protected void CustomValidatorContact_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        CustomValidatorContact.ErrorMessage = "";
        //bool contact1 = false;
    }

    protected void CustomValidatorCustomer_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        CustomValidatorCustomer.ErrorMessage = "";
        //Check Customer Name or Relationship Name
        if (chkNewBus.Checked == true && txtCustomerName.Text == "")
        {
            args.IsValid = false;
            CustomValidatorCustomer.ErrorMessage = "Customer Name is missing";
        }
        if (chkNewBus.Checked == false && rddlRelationships.SelectedIndex < 0)
        {
            args.IsValid = false;
            CustomValidatorCustomer.ErrorMessage = "No Relationship Name Selected";
        }
        //Check for full address
        if (txtCustomerAddress.Text == "" || txtCustomerCity.Text == "" || txtCustomerState.Text == "" || txtCustomerZip.Text == "" || txtCustomerCountry.Text == "")
        {
            args.IsValid = false;
            CustomValidatorCustomer.ErrorMessage = CustomValidatorCustomer.ErrorMessage + "<br>Please Enter Full Address";
        }
        //make sure revenue is a number
        decimal rev;
        string txtRev = txtRevenue.Text;
        txtRev = txtRev.Replace("$", "");
        bool isNumeric = decimal.TryParse(txtRev, out rev);
        if (isNumeric != true)
        {
            args.IsValid = false;
            CustomValidatorCustomer.ErrorMessage = CustomValidatorCustomer.ErrorMessage + "<br>Annualized Revenue must be numeric";
        }
    }

    protected void btnAddSvc_Click(object sender, System.EventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestSvcs> svcList = (List<ClsDiscoveryRequestSvcs>)Session["proposedSvcList"];
            ClsDiscoveryRequestSvcs ps = new ClsDiscoveryRequestSvcs();
            ps.idShippingSvc = Convert.ToInt16(rddlService.SelectedValue);
            ps.serviceDesc = rddlService.SelectedText;
            ps.volume = Convert.ToInt16(txtVolume.Text);

            svcList.Add(ps);

            rgSvcGrid.DataSource = svcList;
            rgSvcGrid.Rebind();
            Session["proposedSvcList"] = svcList;
            rddlService.SelectedIndex = -1;
            rddlService.DataTextField = "";
            txtVolume.Text = "";

            //update Product List based on Services
            getShippingProducts();

            //if this is not a new request, hide and show bars
            //bool newFlag = (bool)Session["newFlag"];
            bool newFlag = false;
            if (lblRequestID.Text == "0" || lblRequestID.Text == "")
                newFlag = true;
            if (newFlag == false)
            {
                hideshowbars();
            }

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rgSvcGrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestSvcs> svcList = (List<ClsDiscoveryRequestSvcs>)Session["proposedSvcList"];
            int rownum = e.Item.ItemIndex;
            ClsDiscoveryRequestSvcs currentrow = svcList[rownum];
            svcList.Remove(currentrow);
            rgSvcGrid.DataSource = svcList;
            rgSvcGrid.Rebind();
            Session["proposedSvcList"] = svcList;

            //update Product List based on Services
            getShippingProducts();

            //if this is not a new request, hide and show bars
            //bool newFlag = (bool)Session["newFlag"];
            bool newFlag = false;
            if (lblRequestID.Text == "0" || lblRequestID.Text == "")
                newFlag = true;
            if (newFlag == false)
            {
                hideshowbars();
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }

    protected void btnAddEDI_Click(object sender, System.EventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestEDI> ediList = (List<ClsDiscoveryRequestEDI>)Session["ediList"];
            if (ediList == null)
            {
                ediList = new List<ClsDiscoveryRequestEDI>();
            }
            ClsDiscoveryRequestEDI es = new ClsDiscoveryRequestEDI();
            es.Solution = rddlEDISolution.SelectedText;
            es.FileFormat = rddlFileFormat.SelectedText;
            es.CommunicationMethod = rddlCommunicationMethod.SelectedText;

            ediList.Add(es);

            rgSolutionsGrid.DataSource = ediList;
            rgSolutionsGrid.Rebind();
            Session["ediList"] = ediList;
            rddlEDISolution.SelectedIndex = -1;
            rddlFileFormat.DataTextField = "";
            rddlCommunicationMethod.DataTextField = "";

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rgSolutionsGrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestSvcs> ediList = (List<ClsDiscoveryRequestSvcs>)Session["ediList"];
            int rownum = e.Item.ItemIndex;
            ClsDiscoveryRequestSvcs currentrow = ediList[rownum];
            ediList.Remove(currentrow);
            rgSolutionsGrid.Rebind();
            Session["ediList"] = ediList;

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }

    protected void btnAddEquipment_Click(object sender, System.EventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestEquip> equipList = (List<ClsDiscoveryRequestEquip>)Session["equipmentList"];
            if (equipList == null)
            {
                equipList = new List<ClsDiscoveryRequestEquip>();
            }
            ClsDiscoveryRequestEquip eq = new ClsDiscoveryRequestEquip();
            eq.EquipmentDesc = rddlEquipment.SelectedText;
            eq.number = Convert.ToInt16(txtNbrEquipment.Text);

            equipList.Add(eq);

            rgEquipmentGrid.DataSource = equipList;
            rgEquipmentGrid.Rebind();
            Session["equipmentList"] = equipList;
            rddlEquipment.SelectedIndex = -1;
            txtNbrEquipment.Text = "";


        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void btnAddOtherEquipment_Click(object sender, System.EventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestEquip> equipList = (List<ClsDiscoveryRequestEquip>)Session["equipmentList"];
            if (equipList == null)
            {
                equipList = new List<ClsDiscoveryRequestEquip>();
            }
            ClsDiscoveryRequestEquip eq = new ClsDiscoveryRequestEquip();
            eq.EquipmentDesc = txtOtherEquipment.Text;
            eq.number = Convert.ToInt16(txtNbrOther.Text);

            equipList.Add(eq);

            rgEquipmentGrid.DataSource = equipList;
            rgEquipmentGrid.Rebind();
            Session["equipmentList"] = equipList;
            txtOtherEquipment.Text = "";
            txtNbrOther.Text = "";


        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void rgEquipmentGrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestEquip> equipList = (List<ClsDiscoveryRequestEquip>)Session["equipmentList"];
            int rownum = e.Item.ItemIndex;
            ClsDiscoveryRequestEquip currentrow = equipList[rownum];
            equipList.Remove(currentrow);
            rgEquipmentGrid.Rebind();
            Session["equipmentList"] = equipList;

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }

    protected void btnAddProduct_Click(object sender, System.EventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestProds> prodList = (List<ClsDiscoveryRequestProds>)Session["productList"];
            ClsDiscoveryRequestProds prods = new ClsDiscoveryRequestProds();
            prods.idShippingProduct = Convert.ToInt16(rddlProducts.SelectedValue);
            prods.productDesc = rddlProducts.SelectedText;


            prodList.Add(prods);

            rgProductGrid.DataSource = prodList;
            rgProductGrid.Rebind();
            Session["productList"] = prodList;
            rddlProducts.SelectedIndex = -1;

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();

        }
    }


    protected void rgProductGrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            List<ClsDiscoveryRequestProds> prodList = (List<ClsDiscoveryRequestProds>)Session["productList"];
            int rownum = e.Item.ItemIndex;
            ClsDiscoveryRequestProds currentrow = prodList[rownum];
            prodList.Remove(currentrow);
            Session["productList"] = prodList;
            rgProductGrid.DataSource = prodList;
            rgProductGrid.Rebind();

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }

    protected void rgNotesGrid_DeleteCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            int rownum = e.Item.ItemIndex;
            ClsNotes cn = new ClsNotes();
            GridDataItem item = (GridDataItem)e.Item;
            TableCell cell = item["idNote"];
            string strnoteId = cell.Text;
            //string strnoteId = Convert.ToString(item.Cells[1].Text);
            int noteID = Convert.ToInt32(strnoteId);
            ClsNotes currentrow = cn.GetNote(noteID);

            //Only allow delete if the Submitted by is the same user
            string username = Session["userName"].ToString();
            string userRole = Session["userRole"].ToString().ToLower();
            if (username != currentrow.CreatedBy && userRole != "itadmin")
            {
                pnlDanger.Visible = true;
                lblDanger.Text = "Cannot delete notes that were not orignally submitted by you.";
                e.Canceled = true;
            }
            else
            {
                //mark as inactive
                cn.deActivateNote(noteID);
                rgNotesGrid.Rebind();
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }

    protected void rgNotesGrid_InsertCommand(object sender, GridCommandEventArgs e)
    {
    }

    protected void rgNotesGrid_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            int rownum = e.Item.ItemIndex;
            ClsNotes cn = new ClsNotes();
            GridEditFormItem item = (GridEditFormItem)e.Item;
            TableCell cell = item["idNote"];
            string strnoteId = cell.Text;
            int noteID = Convert.ToInt32(strnoteId);
            ClsNotes objNote = cn.GetNote(noteID);
            int DRID = Convert.ToInt32(lblRequestID.Text);

            //Only allow update if the Submitted by is the same user
            string username = Session["userName"].ToString();
            string userRole = Session["userRole"].ToString().ToLower();
            if (username != objNote.CreatedBy && userRole != "itadmin" && objNote.CreatedBy != null)
            {
                pnlDanger.Visible = true;
                lblDanger.Text = "Cannot edit notes that were not orignally submitted by you.";
                e.Canceled = true;
            }
            else
            {
                //Make changes and submit
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                TextBox updatedNote = (TextBox)userControl.FindControl("txtNotesEdit");
                objNote.publicNote = updatedNote.Text;

                RadDatePicker updatedDate = (RadDatePicker)userControl.FindControl("dpNoteDateEdit");
                objNote.noteDate = updatedDate.SelectedDate;

                if (rddlInternalType.Visible == true)
                {
                    RadDropDownList updatedTypeID = (RadDropDownList)userControl.FindControl("rddlInternalTypeEdit");
                    objNote.idTaskType = Convert.ToInt16(updatedTypeID.SelectedValue);
                }
                if (txtInternalTimeSpent.Visible == true)
                {
                    RadTextBox updatedTimeSpent = (RadTextBox)userControl.FindControl("txtInternalTimeSpentEdit");
                    objNote.timeSpent = Convert.ToInt16(updatedTimeSpent.Text);
                }
                if (txtInternalNotes.Visible == true)
                {
                    TextBox updatedPrivateNote = (TextBox)userControl.FindControl("txtInternalNotesEdit");
                    objNote.privateNote = updatedPrivateNote.Text;
                }
                objNote.UpdatedBy = (string)Session["userName"];
                objNote.UpdatedOn = Convert.ToDateTime(DateTime.Now);
                objNote.ActiveFlag = true;

                String msg = cn.UpdateNote(objNote);
                rgNotesGrid.Rebind();
                UpdateTimeSpent(DRID);
            }


        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }


    protected void chkNewBus_Click(object sender, System.EventArgs e)
    {
        bool newBusiness = chkNewBus.Checked;
        if (newBusiness == false)
        {
            rddlRelationships.Visible = true;
            txtCustomerName.Visible = false;
            lblCustomerName.Text = "Select Relationship";

        }
        else
        {
            rddlRelationships.Visible = false;
            txtCustomerName.Visible = true;
            lblCustomerName.Text = "Enter Customer Name";

        }
    }

    protected void txtCustomerZip_TextChanged(object sender, System.EventArgs e)
    {

        string zipcode = txtCustomerZip.Text;

        // ClsZipCode zc = new ClsZipCode();
        ClsZipCode zipInfo = repository.GetZipInfo(zipcode);
        if (zipInfo != null)
        {
            txtCustomerCity.Text = zipInfo.City;
            txtCustomerState.Text = zipInfo.State;
            txtCustomerCountry.Text = zipInfo.Country;
        }

    }

    protected void btnNextTab1_Click(object sender, System.EventArgs e)
    {
        if (Page.IsValid)
        {
            RadTabStrip1.Tabs[1].Enabled = true;
            RadTabStrip1.Tabs[1].Selected = true;
            RadMultiPage1.SelectedIndex = 1;
            btnNextTab1.Visible = false;
            List<clsContact> contactList = (List<clsContact>)Session["contactList"];
            if (contactList.Count() == 0)
            {
                btnNextTab2.Visible = false;
            }
            if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
            {
                txtBoxMultiDebug.Text += "btnNextTab1_Click()\r\n";
            }
        }
    }
    protected void btnNextTab2_Click(object sender, System.EventArgs e)
    {
        if (Page.IsValid)
        {
            if (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_EDI)
            {
                RadTabStrip1.Tabs[3].Enabled = true;
                RadTabStrip1.Tabs[3].Selected = true;
                RadMultiPage1.SelectedIndex = 3;
                btnSubmit.Enabled = true;
                btnNextTab3.Visible = false;
            }
            else if (String.IsNullOrEmpty(txtareaCurrentSolution.Text.ToString()))
            {
                RadTabStrip1.Tabs[2].Enabled = true;
                RadTabStrip1.Tabs[2].Selected = true;
                RadMultiPage1.SelectedIndex = 2;
                btnNextTab2.Visible = false;
            }
            else if (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_SHIPPING)
            {
                RadTabStrip1.Tabs[4].Enabled = true;
                RadTabStrip1.Tabs[4].Selected = true;
                RadMultiPage1.SelectedIndex = 4;
                btnSubmit.Enabled = true;
            }
            if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
            {
                txtBoxMultiDebug.Text += "btnNextTab2_Click()\r\n";
            }
        }
    }
    protected void btnNextTab3_Click(object sender, System.EventArgs e)
    {
        if (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_EDI)
        {
            RadTabStrip1.Tabs[3].Enabled = true;
            RadTabStrip1.Tabs[3].Selected = true;
            RadMultiPage1.SelectedIndex = 3;
            btnSubmit.Enabled = true;
            btnNextTab3.Visible = false;
        }
        else if (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_SHIPPING) 
        {
            RadTabStrip1.Tabs[4].Enabled = true;
            RadTabStrip1.Tabs[4].Selected = true;
            RadMultiPage1.SelectedIndex = 4;
            btnSubmit.Enabled = true;
            //btnNextTab3.Visible = false;
        }
        else if (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_BOTH)
        {
            RadTabStrip1.Tabs[3].Enabled = true;
            RadTabStrip1.Tabs[3].Selected = true;
            RadMultiPage1.SelectedIndex = 3;
            //btnSubmit.Enabled = true;
            btnNextTab3.Visible = true;
        }
        if (bool.Parse(ConfigurationManager.AppSettings["debug"]))
        {
            txtBoxMultiDebug.Text += "btnNextTab3_Click()\r\n";
        }
    }
    protected void btnNextTab4_Click(object sender, System.EventArgs e)
    {
        string userRole = Session["userRole"].ToString().ToLower();
        if (rddlSolutionType.SelectedIndex == SOLUTION_TYPE_BOTH && userRole.Contains("sales"))
        {
            List<clsEDIShipMethod> EDIShipMethList = (List<clsEDIShipMethod>)Session["EDIShipMethList"];
            List<clsEDITransaction> EDITransList = (List<clsEDITransaction>)Session["EDITransList"];
            if (EDIShipMethList.Count() != 0 && EDITransList.Count() != 0)
            {
                RadTabStrip1.Tabs[4].Enabled = true;
                RadTabStrip1.Tabs[4].Selected = true;
                RadMultiPage1.SelectedIndex = 4;
                btnSubmit.Enabled = true;
                btnEDIServicesNext.Visible = false;
                //CustomValidator.Visible = false;
                //CustomValidator.ErrorMessage = "";
            }
            else
            {
                string ErrorMessage = "";
                if (EDITransList.Count() == 0)
                    ErrorMessage = "<br>At Least One EDI Transaction Must Be Supplied";
                if (EDIShipMethList.Count() == 0 )
                    ErrorMessage = ErrorMessage + "<br>At Least One EDI Ship Method Must Be Supplied";

                CustomValidator.ErrorMessage = ErrorMessage;
                CustomValidator.IsValid = false;
                CustomValidator.Visible = true;
            }
        }
        else
        {
            RadTabStrip1.Tabs[4].Enabled = true;
            RadTabStrip1.Tabs[4].Selected = true;
            RadMultiPage1.SelectedIndex = 4;
            btnSubmit.Enabled = true;
            btnEDIServicesNext.Visible = false;
        }
    }
    protected void date1_Changed(object sender, System.EventArgs e)
    {
        btnAddDate1.Enabled = true;
    }
    protected void btnAddDate1_Click(object sender, System.EventArgs e)
    {
        rdCall2.Visible = true;
        btnAddDate2.Visible = true;
        btnAddDate2.Enabled = false;
    }
    protected void date2_Changed(object sender, System.EventArgs e)
    {
        btnAddDate2.Enabled = true;
    }
    protected void btnAddDate2_Click(object sender, System.EventArgs e)
    {
        rdCall3.Visible = true;
    }
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                btnSubmit.Enabled = false;
                doSubmit();
            }
        }
        catch (Exception ex)
        {
            lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }
    }
    protected void btnSubmitChanges_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                doSubmit();
            }
        }
        catch (Exception ex)
        {
            lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }
    }

    protected void doSubmit()
    {
        if (Session["userName"] == null)
            Response.Redirect("Default.aspx");

        //DO SUBMIT
        bool newFlag = false;
        if (lblRequestID.Text == "0" || lblRequestID.Text == "")
            newFlag = true;

        objDiscoveryRequest = populateDiscoveryRequestObj(newFlag);
        ClsDiscoveryRequest dr = new ClsDiscoveryRequest();

        objDiscoveryRequestDetails = populateDiscoveryRequestDetails(newFlag);
        ClsDiscoveryRequestDetails drd = new ClsDiscoveryRequestDetails();
        String msg = "";

        if (newFlag == true)
        {
            //DO INSERT    
            int newID;
            int newDID;
            msg = dr.InsertDiscoveryRequest(objDiscoveryRequest, out newID);
            if (msg != "")
            {
                lblDanger.Text = msg;
                pnlDanger.Visible = true;
            }
            else
            {
                //GET REQUEST ID
                Int32 requestID = newID;
                objDiscoveryRequest.idRequest = requestID;
                lblReqID.Visible = true;
                lblRequestID.Visible = true;
                lblRequestID.Text = requestID.ToString();

                //SHIPPING DETAILS
                objDiscoveryRequestDetails.idRequest = requestID;
                //DO INSERT OF DETAILS
                msg = drd.InsertDiscoveryRequestDetails(objDiscoveryRequestDetails, out newDID);

                if (msg != "")
                {
                    lblDanger.Text = msg;
                    pnlDanger.Visible = true;
                }
                SrvContact.Insert((List<clsContact>)Session["contactList"], requestID);
                SrvEDITransaction.Insert( (List<clsEDITransaction>)Session["EDITransList"],requestID);
                SrvEDIShipMethod.Insert((List<clsEDIShipMethod>)Session["EDIShipMethList"],requestID);
                SrvFreightAuditorsDiscReq.InsertFreightAuditor((List<clsFreightAuditorsDiscReq>)Session["FreightAuditorsList"], requestID);

                btnSubmit.Visible = false;
                btnSubmitChanges.Visible = true;
            }
        }
        else
        {
            //DO UPDATE  
            Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
            msg = dr.UpdateDiscoveryRequest(objDiscoveryRequest, RequestID);
            msg = msg + drd.UpdateDiscoveryRequestDetails(objDiscoveryRequestDetails, RequestID);
            if (msg != "")
            {
                lblDanger.Text = msg;
                pnlDanger.Visible = true;
            }
            else
            {
                //DELETE EXISTING SERVICES, PRODUCTS, EQUIPMENT & EDI BEFORE INSERTING LIST OF NEW ONES
                ClsDiscoveryRequestSvcs svc = new ClsDiscoveryRequestSvcs();
                msg = svc.DeleteServices(RequestID);
                ClsDiscoveryRequestProds prd = new ClsDiscoveryRequestProds();
                msg = msg + prd.DeleteProducts(RequestID);
                ClsDiscoveryRequestEquip equip = new ClsDiscoveryRequestEquip();
                msg = msg + equip.DeleteEquipment(RequestID);
                ClsDiscoveryRequestEDI edi = new ClsDiscoveryRequestEDI();
                msg = msg + edi.DeleteEDI(RequestID);
            }
        }

        //if East West Split, Create Second Detail Record for West
        bool EastWestFlag = (bool)cbxSplit.Checked;
        if (EastWestFlag == true)
        {
            try
            {
                ClsDiscoveryRequestDetails objDiscoveryRequestDetailsWest = new ClsDiscoveryRequestDetails();
                objDiscoveryRequestDetailsWest = populateDiscoveryRequestDetailsWest(newFlag);

                //Int32 RequestID = (Int32)Session["requestID"];
                Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
                //can't use newflag to determine existing record - may be existing Request, but East/West split is new.
                ClsDiscoveryRequestDetails westDetails = drd.GetDiscoveryRequestDetails(RequestID, "West");
                if (westDetails == null)
                {
                    //DO INSERT
                    int newDID;
                    //SHIPPING DETAILS
                    objDiscoveryRequestDetailsWest.idRequest = RequestID;
                    //DO INSERT OF DETAILS WEST
                    msg = msg + drd.InsertDiscoveryRequestDetails(objDiscoveryRequestDetailsWest, out newDID);
                    if (msg != "")
                    {
                        lblDanger.Text = msg;
                        pnlDanger.Visible = true;
                    }
                }
                else
                {
                    //DO UPDATE  
                    msg = msg + drd.UpdateDiscoveryRequestDetails(objDiscoveryRequestDetailsWest, RequestID);
                    if (msg != "")
                    {
                        lblDanger.Text = msg;
                        pnlDanger.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
                lblWarning.Visible = true;
                pnlwarning.Visible = true;
            }
        }
        else
        {
            //IF East/West Split is false, check if there was a previously existing West Record
            //Int32 RequestID = (Int32)Session["requestID"];
            Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
            ClsDiscoveryRequestDetails westDetails = drd.GetDiscoveryRequestDetails(RequestID, "West");
            if (westDetails != null)
            {
                //deactivate west record - no longer an East/West split
                westDetails.ActiveFlag = false;
                msg = msg + drd.UpdateDiscoveryRequestDetails(westDetails, RequestID);
            }
        }

        if (msg == "")
        {
            //INSERT SERVICES
            Int32 newSvcID;
            List<ClsDiscoveryRequestSvcs> svcList = (List<ClsDiscoveryRequestSvcs>)Session["proposedSvcList"];
            Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
            foreach (ClsDiscoveryRequestSvcs svc in svcList)
            {
                svc.idRequest = RequestID;
                svc.UpdatedBy = objDiscoveryRequest.UpdatedBy;
                svc.UpdatedOn = objDiscoveryRequest.UpdatedOn;
                svc.CreatedBy = objDiscoveryRequest.CreatedBy;
                svc.CreatedOn = objDiscoveryRequest.CreatedOn;
                msg = svc.InsertServices(svc, out newSvcID);
                if (msg != "")
                {
                    lblDanger.Text = msg;
                    pnlDanger.Visible = true;
                }
            }
        }

        if (msg == "")
        {
            //INSERT PRODUCTS
            Int32 newProdID;
            List<ClsDiscoveryRequestProds> prodList = (List<ClsDiscoveryRequestProds>)Session["productList"];
            Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
            foreach (ClsDiscoveryRequestProds prod in prodList)
            {
                prod.idRequest = RequestID;
                prod.UpdatedBy = objDiscoveryRequest.UpdatedBy;
                prod.UpdatedOn = objDiscoveryRequest.UpdatedOn;
                prod.CreatedBy = objDiscoveryRequest.CreatedBy;
                prod.CreatedOn = objDiscoveryRequest.CreatedOn;
                msg = prod.InsertProducts(prod, out newProdID);
                if (msg != "")
                {
                    lblDanger.Text = msg;
                    pnlDanger.Visible = true;
                }
            }
        }

        if (msg == "")
        {
            //INSERT EQUIPMENT
            Int32 newEquipID;
            List<ClsDiscoveryRequestEquip> equipList = (List<ClsDiscoveryRequestEquip>)Session["equipmentList"];
            Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
            foreach (ClsDiscoveryRequestEquip equip in equipList)
            {
                equip.idRequest = RequestID;
                equip.UpdatedBy = objDiscoveryRequest.UpdatedBy;
                equip.UpdatedOn = objDiscoveryRequest.UpdatedOn;
                equip.CreatedBy = objDiscoveryRequest.CreatedBy;
                equip.CreatedOn = objDiscoveryRequest.CreatedOn;
                msg = equip.InsertEquipment(equip, out newEquipID);
                if (msg != "")
                {
                    lblDanger.Text = msg;
                    pnlDanger.Visible = true;
                }
            }
        }

        if (msg == "")
        {
            //INSERT EDI
            Int32 newEDIID;
            List<ClsDiscoveryRequestEDI> ediList = (List<ClsDiscoveryRequestEDI>)Session["ediList"];
            Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
            foreach (ClsDiscoveryRequestEDI edi in ediList)
            {
                edi.idRequest = RequestID;
                edi.UpdatedBy = objDiscoveryRequest.UpdatedBy;
                edi.UpdatedOn = objDiscoveryRequest.UpdatedOn;
                edi.CreatedBy = objDiscoveryRequest.CreatedBy;
                edi.CreatedOn = objDiscoveryRequest.CreatedOn;
                msg = edi.InsertEDI(edi, out newEDIID);
                if (msg != "")
                {
                    lblDanger.Text = msg;
                    pnlDanger.Visible = true;
                }
            }
        }
        if (msg == "")
        {
            // INSERT or update Courier EDI
            Int32 newEDIID = 0;
            Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
            if (comboBxCourierEDI210.SelectedText == "Yes")
            {
                clsEDITransaction data = new clsEDITransaction() { idRequest = RequestID, idEDITranscationType = INVOICE_COURIER_EDI, TotalRequests = GetIntFromField(txtBxNumberRecipients210.Text), CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                data.CombinePayer = comboxCombinepayer.SelectedText.Contains("Yes") ? true : false;
                data.BatchInvoices = comboBoxBatchInvoices.SelectedText.Contains("Yes") ? true : false;
                SrvEDITransaction.Insert(data,out newEDIID);
            }
            else
            {
                clsEDITransaction data = new clsEDITransaction() { idRequest = RequestID, idEDITranscationType = INVOICE_COURIER_EDI, TotalRequests = GetIntFromField(txtBxNumberRecipients210.Text), CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                SrvEDITransaction.Remove(data);
            }
            if (comboBxCourierEDI214.SelectedText == "Yes")
            {
                clsEDITransaction data = new clsEDITransaction() { idRequest = RequestID, idEDITranscationType = SHIPMENT_STATUS_COURIER_EDI, TotalRequests = GetIntFromField(txtBxNumberRecipients214.Text), CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                SrvEDITransaction.Insert(data, out newEDIID);
            }
            else
            {
                clsEDITransaction data = new clsEDITransaction() { idRequest = RequestID, idEDITranscationType = SHIPMENT_STATUS_COURIER_EDI, TotalRequests = GetIntFromField(txtBxNumberRecipients214.Text), CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                SrvEDITransaction.Remove(data);
            }
            if (comboBxNonCourierEDI210.SelectedText == "Yes")
            {
                clsEDITransaction data = new clsEDITransaction() { idRequest = RequestID, idEDITranscationType = INVOICE_NON_COURIER_EDI, TotalRequests = GetIntFromField(txtBxNumRecipNonCourier210.Text), CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                //data.TestEnvironment = comboNonCourier210TestEnvironment.SelectedText.Contains("Yes") ? true : false;
                data.TestSentMethod = comboNonCourier210TestSent.SelectedText.Contains("SFTP") ? 1 : 2;
                //data.SFTPFolder = txtNonCourier210SFTP.Text;
                SrvEDITransaction.Insert(data, out newEDIID);
            }
            else
            {
                clsEDITransaction data = new clsEDITransaction() { idRequest = RequestID, idEDITranscationType = INVOICE_NON_COURIER_EDI, TotalRequests = GetIntFromField(txtBxNumRecipNonCourier210.Text), CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                SrvEDITransaction.Remove(data);
            }
            if (comboBxNonCourierEDI214.SelectedText == "Yes")
            {
                clsEDITransaction data = new clsEDITransaction() { idRequest = RequestID, idEDITranscationType = SHIPMENT_STATUS_NON_COURIER_EDI, TotalRequests = GetIntFromField(txtBxNumRecipNonCourier214.Text), CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                //data.TestEnvironment = comboNonCourier214TestEnvironment.SelectedText.Contains("Yes") ? true : false;
                data.TestSentMethod = comboNonCourier214TestSent.SelectedText.Contains("SFTP") ? 1 : 2;
                //data.SFTPFolder = txtNonCourier214SFTP.Text;
                SrvEDITransaction.Insert(data, out newEDIID);
            }
            else
            {
                clsEDITransaction data = new clsEDITransaction() { idRequest = RequestID, idEDITranscationType = SHIPMENT_STATUS_NON_COURIER_EDI, TotalRequests = GetIntFromField(txtBxNumRecipNonCourier214.Text), CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                SrvEDITransaction.Remove(data);
            }
            if (comboBxNonCourierPuroPost.SelectedText == "Yes")
            {
                clsEDITransaction data = new clsEDITransaction() { idRequest = RequestID, idEDITranscationType = PUROPOST_NON_COURIER_EDI, TotalRequests = GetIntFromField(txtBxNumRecipNonCourierPuroPostStand.Text), CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                //data.SFTPFolder = txtNonCourierPuroPostSFTP.Text; 
                SrvEDITransaction.Insert(data, out newEDIID);
            }
            else
            {
                clsEDITransaction data = new clsEDITransaction() { idRequest = RequestID, idEDITranscationType = PUROPOST_NON_COURIER_EDI, TotalRequests = GetIntFromField(txtBxNumRecipNonCourierPuroPostStand.Text), CreatedBy = Session["userName"].ToString(), CreatedOn = DateTime.Now };
                SrvEDITransaction.Remove(data);
            }
        }
        //SEND EMAIL TO ITBA IF NEW OR CHANGED
        string OrigITBA = Session["ITBA"].ToString();
        string CurrentITBA = objDiscoveryRequest.idITBA.ToString();
        string emailmsg = "";
        if (OrigITBA != CurrentITBA)
        {
            //send email
            int idITBA = (int)objDiscoveryRequest.idITBA;
            sendITBAEmail(objDiscoveryRequest);
            emailmsg = "Email sent to ITBA.";
        }
        string OrigEDISpecialist = Session["EDISpecialist"].ToString();
        string CurrentEDISpecialist = objDiscoveryRequest.idEDISpecialist.ToString();
        if (OrigEDISpecialist != CurrentEDISpecialist)
        {
            sendEDISpecialistEmail(objDiscoveryRequest);
        }

        //FINAL STEP
        if (msg == "")
        {
            //SHOW LAST UPDATED
            lblUpdatedBy.Text = objDiscoveryRequest.UpdatedBy;
            lblUpdatedOn.Text = objDiscoveryRequest.UpdatedOn.ToString();

            string alertmsg = "";
            if (newFlag)
            {
                sendManagerEmail(objDiscoveryRequest);
                //string RequestID = Session["requestID"].ToString();
                string RequestID = lblRequestID.Text;
                alertmsg = "Discovery Request Has Been Submitted. Request ID #" + RequestID;
                windowManager.RadAlert(alertmsg, 350, 200, "Request Submitted", "submitCallBackFn", "Please confirm");
            }
            else
            {
                //alertmsg = alertmsg + "Discovery Request Changes Have Been Saved. " + emailmsg;
                //don't exit
                //Check if note tab is active, and save notes 
                bool savenotesflag = checkforNotes();
                if (savenotesflag == true)
                {
                    //Notes were entered, do checks and then save
                    saveNotes();
                }
            }
        }
    }
    public int GetIntFromField(string strInt)
    {
        int iRetVal = 0;
        try
        {
            if (!string.IsNullOrEmpty(strInt))
            {
                iRetVal = int.Parse(strInt);
            }
        }
        catch (Exception ex)
        {
            //string s = ex.Message.ToString();
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { Method = GetCurrentMethod(), ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now, CreatedBy = Session["userName"].ToString() };
            SrvExceptionLogging.Insert(error, out lnewID);
        }
        return iRetVal;
    }
    protected Boolean checkforNotes()
    {
        //If Notes have been entered, return true so notes can be saved
        Boolean notesEnteredflag = false;
        if (txtNotes.Text != "")
        {
            notesEnteredflag = true;
        }
        if (txtInternalNotes.Text != "")
        {
            notesEnteredflag = true;
        }
        Int16 numval = 0;
        if (Int16.TryParse(rddlInternalType.SelectedValue, out numval))
        {
            notesEnteredflag = true;
        }
        if (Int16.TryParse(txtInternalTimeSpent.Text, out numval))
        {
            if (numval > 0)
                notesEnteredflag = true;
        }
        return notesEnteredflag;
    }

    protected void btnSaveNotes_Click(object sender, System.EventArgs e)
    {
        saveNotes();
    }

    protected void saveNotes()
    {
        try
        {
            //MK - check if time is numeric, notes entered and task type selected
            int min;
            string txtMin = txtInternalTimeSpent.Text;
            bool isNumeric = int.TryParse(txtMin, out min);
            string warningtxt = "";
            bool newNoteflag = true;
            ClsNotes noteObj = new ClsNotes();
            noteObj = populateNoteObj(newNoteflag);
            int newID;
            //Check for required values
            if (noteObj.idTaskType != 1014)
            {
                if (txtNotes.Text == "")
                {
                    warningtxt = warningtxt + " - Notes value is Empty";
                }
            }
            else
            {
                if (txtInternalNotes.Text == "")
                {
                    warningtxt = warningtxt + " - Customer Delay value is Empty";
                }
            }

            Int16 numval = 0;
            //for ITBA notes, check task type and time
            string userRole = Session["userRole"].ToString().ToLower();
            if (userRole == "itba")
            {
                if (Int16.TryParse(rddlInternalType.SelectedValue, out numval))
                {
                }
                else
                {
                    warningtxt = warningtxt + " - Notes Task Type not selected";
                }
                if (Int16.TryParse(txtInternalTimeSpent.Text, out numval))
                {
                    if (numval <= 0)
                        warningtxt = warningtxt + " - Notes Time Spent must be greater than zero";
                }
                else
                {
                    warningtxt = warningtxt + " - Notes Time Spent must be numeric";
                }
                //end required check
            }

            if (warningtxt != "")
            {
                lblWarning.Text = warningtxt;
                lblWarning.Visible = true;
                pnlwarning.Visible = true;
            }
            else
            {
                String msg = noteObj.InsertNote(noteObj, out newID);
                if (msg != "" || isNumeric != true)
                {
                    lblWarning.Text = msg;
                    lblWarning.Visible = true;
                    pnlwarning.Visible = true;
                }
                else
                {
                    RadTabStrip1.Tabs[5].Visible = true;
                    RadTabStrip1.Tabs[5].Selected = true;
                    RadMultiPage1.SelectedIndex = 5;
                    txtNotes.Text = "";
                    txtInternalNotes.Text = "";
                    rddlInternalType.SelectedIndex = -1;

                    int DRID = Convert.ToInt32(lblRequestID.Text);
                    //ClsNotes cn = new ClsNotes();
                    List<ClsNotes> allnotes = repository.GetNotes(DRID);
                    rgNotesGrid.DataSource = allnotes;
                    rgNotesGrid.DataBind();
                    string ur = Session["userRole"].ToString().ToLower();
                    if (ur == "sales" || ur == "salesdm" || ur == "salesmangaer")
                    {
                        rgNotesGrid.MasterTableView.GetColumn("timeSpent").Visible = false;
                        rgNotesGrid.MasterTableView.GetColumn("TaskType").Visible = false;
                        rgNotesGrid.MasterTableView.GetColumn("privateNote").Visible = false;
                    }
                    rgNotesGrid.Visible = true;
                    UpdateTimeSpent(DRID);

                    note1ast.Visible = true;
                    note2ast.Visible = false;
                    rfvNotes.Enabled = true;
                    rfvNotes2.Enabled = false;
                }
                lblWarning.Text = "";
                lblWarning.Visible = false;
                pnlwarning.Visible = false;
            }
        }

        catch (Exception ex)
        {
            lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        string referrer = Request.QueryString["from"];
        if (!String.IsNullOrEmpty(referrer))
        {
            Response.Redirect("DiscoveryTracker.aspx");
        }
        else
        {
            Response.Redirect("Home.aspx");
        }

    }
    protected void rgNotesGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            bool newFlag = false;
            if (lblRequestID.Text == "0" || lblRequestID.Text == "")
                newFlag = true;
            if (newFlag != true)
            {
                Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
                // ClsNotes cn = new ClsNotes();
                List<ClsNotes> allnotes = repository.GetNotes(RequestID);
                rgNotesGrid.DataSource = allnotes;
                rgNotesGrid.Visible = true;
                string ur = Session["userRole"].ToString().ToLower();
                if (ur == "sales" || ur == "salesdm" || ur == "salesmanager")
                {
                    rgNotesGrid.MasterTableView.GetColumn("timeSpent").Visible = false;
                    rgNotesGrid.MasterTableView.GetColumn("TaskType").Visible = false;
                    rgNotesGrid.MasterTableView.GetColumn("privateNote").Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }
    }

    private ClsDiscoveryRequest populateDiscoveryRequestObj(bool newflag)
    {
        try
        {
            bool newbus = false;
            if (chkNewBus.Checked == true)
                newbus = true;
            DateTime? priorGoLive;
            Int32 priorPhase = 0;
            DateTime? priorDateChange = null;
            ClsDiscoveryRequest editRequest = new ClsDiscoveryRequest();
            if (newflag == true)
            {
                //set onboarding phase = discovery on new requests
                string InitialPhase = ConfigurationManager.AppSettings["InitialPhase"].ToString();
                objDiscoveryRequest.idOnboardingPhase = Convert.ToInt16(InitialPhase);
                priorGoLive = null;
            }
            else
            {
                //Get Existing Request
                ClsDiscoveryRequest dr = new ClsDiscoveryRequest();
                Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
                editRequest = dr.GetDiscoveryRequest(RequestID);
                priorGoLive = editRequest.CurrentGoLive;
                priorPhase = (Int32)editRequest.idOnboardingPhase;
                priorDateChange = (DateTime?)editRequest.PhaseChangeDate;
                //If just assigned, change onboarding phase                               
                if (editRequest.idITBA == null && rddlITBA.SelectedIndex > 0)
                {
                    //Change Phase when first assigne to ITBA
                    string AssignedPhase = ConfigurationManager.AppSettings["AssignedPhase"].ToString();
                    rddlPhase.SelectedValue = AssignedPhase;
                }
            }
            //HEADING
            objDiscoveryRequest.flagNewRequest = newbus;
            objDiscoveryRequest.SalesRepName = txtSalesProfessional.Text;
            objDiscoveryRequest.SalesRepEmail = txtEmail.Text;
            objDiscoveryRequest.District = rddlDistrict.SelectedValue;
            objDiscoveryRequest.Branch = rddlBranch.SelectedValue;
            //CUSTOMER INFO
            //if new relationship, use text box othewise use drop down selection
            objDiscoveryRequest.flagNewRequest = chkNewBus.Checked;
            objDiscoveryRequest.CustomerName = txtCustomerName.Text;

            if (rddlSolutionType.SelectedValue != "")
                objDiscoveryRequest.idSolutionType = Convert.ToInt32(rddlSolutionType.SelectedValue);
            if (rddlRequestType.SelectedValue != "")
                objDiscoveryRequest.idRequestType = Convert.ToInt32(rddlRequestType.SelectedValue);
            bool strategicFlag = false;
            if (rddlStrategic.SelectedText == "Yes")
            {
                strategicFlag = true;
            }
            objDiscoveryRequest.StrategicFlag = strategicFlag;
            objDiscoveryRequest.Address = txtCustomerAddress.Text;
            objDiscoveryRequest.City = txtCustomerCity.Text;
            objDiscoveryRequest.State = txtCustomerState.Text;
            objDiscoveryRequest.Zipcode = txtCustomerZip.Text;
            objDiscoveryRequest.Country = txtCustomerCountry.Text;
            objDiscoveryRequest.Commodity = txtCommodity.Text;
            objDiscoveryRequest.CustomerWebsite = txtWebsite.Text;
            string txtRev = txtRevenue.Text;
            txtRev = txtRev.Replace("$", "");
            objDiscoveryRequest.ProjectedRevenue = Convert.ToDecimal(txtRev);
            //CONTACT INFO
            //CURRENT SOLUTION
            objDiscoveryRequest.CurrentSolution = txtareaCurrentSolution.Text;
            bool curshipflag = true;
            if (rddlCurrentCA.SelectedText == "No")
                curshipflag = false;
            objDiscoveryRequest.CurrentlyShippingFlag = curshipflag;
            if (rddlShippingVendor.SelectedValue != "")
                objDiscoveryRequest.idShippingVendor = Convert.ToInt32(rddlShippingVendor.SelectedValue);
            objDiscoveryRequest.OtherVendorName = txtCurrentVendor.Text;
            //SHIPPING SERVICES
            objDiscoveryRequest.ProposedCustoms = rddlCustomsList.SelectedText;
            if (rddlCustomsBroker.SelectedValue != "")
                objDiscoveryRequest.idBroker = Convert.ToInt32(rddlCustomsBroker.SelectedValue);
            objDiscoveryRequest.OtherBrokerName = txtCustomsBroker.Text;
            objDiscoveryRequest.CallDate1 = rdCall1.SelectedDate;
            objDiscoveryRequest.CallDate2 = rdCall2.SelectedDate;
            objDiscoveryRequest.CallDate3 = rdCall3.SelectedDate;
            objDiscoveryRequest.SalesComments = txtProposedNotes.Text;

            //--PROFILE--//

            //SOLUTION SUMMARY
            if (rddlITBA.SelectedValue != "")
                objDiscoveryRequest.idITBA = Convert.ToInt32(rddlITBA.SelectedValue);
            else if(rddlITBA2.SelectedValue != "")
                objDiscoveryRequest.idITBA = Convert.ToInt32(rddlITBA2.SelectedValue);
            if (cmboxEDISpecialist.SelectedValue != "")
                objDiscoveryRequest.idEDISpecialist = Convert.ToInt32(cmboxEDISpecialist.SelectedValue);
            if (cmboxBillingSpecialist.SelectedValue != "")
                objDiscoveryRequest.idBillingSpecialist = Convert.ToInt32(cmboxBillingSpecialist.SelectedValue);
            if (cmboxCollectionSpecialist.SelectedValue != "")
                objDiscoveryRequest.idCollectionSpecialist = Convert.ToInt32(cmboxCollectionSpecialist.SelectedValue);
            if (rddlShippingChannel.SelectedValue != "")
                objDiscoveryRequest.idShippingChannel = Convert.ToInt32(rddlShippingChannel.SelectedValue);
            if (rddlPhase.SelectedValue != "")
                objDiscoveryRequest.idOnboardingPhase = Convert.ToInt32(rddlPhase.SelectedValue);
            //save phasechange date, only if the phase is different
            if (priorPhase != objDiscoveryRequest.idOnboardingPhase)
                objDiscoveryRequest.PhaseChangeDate = DateTime.Now;
            else
                objDiscoveryRequest.PhaseChangeDate = priorDateChange;
            if (rddlThirdPartyVendor.SelectedValue != "")
                objDiscoveryRequest.idVendor = Convert.ToInt32(rddlThirdPartyVendor.SelectedValue);
            if (rddlCloseReason.SelectedValue != "")
                objDiscoveryRequest.CloseReason = rddlCloseReason.SelectedText;
            if (cmboxOnboardingPhase.SelectedValue != "")
                objDiscoveryRequest.idEDIOnboardingPhase = Convert.ToInt32(cmboxOnboardingPhase.SelectedValue);
            if (comboxCustAuditPortal.SelectedText == "Yes")
                objDiscoveryRequest.AuditorPortal = true;
            else
                objDiscoveryRequest.AuditorPortal = false;

            objDiscoveryRequest.CurrentGoLive = rdCurrentTarget.SelectedDate;
            objDiscoveryRequest.TargetGoLive = rdTarget.SelectedDate;
            objDiscoveryRequest.ActualGoLive = rdActual.SelectedDate;
            objDiscoveryRequest.SolutionSummary = txtSolutionSummary.Text;
            if (rddlVendorType.SelectedValue != "")
                objDiscoveryRequest.idVendorType = Convert.ToInt32(rddlVendorType.SelectedValue);
            objDiscoveryRequest.worldpakFlag = Convert.ToBoolean(cbxWPK.Checked);
            objDiscoveryRequest.thirdpartyFlag = Convert.ToBoolean(cbx3pv.Checked);
            objDiscoveryRequest.customFlag = Convert.ToBoolean(cbxCustom.Checked);
            objDiscoveryRequest.DataScrubFlag = Convert.ToBoolean(cbxDataScrub.Checked);
            objDiscoveryRequest.Route = txtRoute.Text;
            objDiscoveryRequest.AuditorURL = txtBxAuditoURL.Text;
            objDiscoveryRequest.AuditorUserName = txtBxAuditoUserName.Text;
            objDiscoveryRequest.AuditorPassword = txtBxAuditoPassword.Text;
            objDiscoveryRequest.EDITargetGoLive = dateTargetGoLive.SelectedDate;
            objDiscoveryRequest.EDICurrentGoLive = dateCurrentGoLive.SelectedDate;
            objDiscoveryRequest.EDIActualGoLive = dateActualGoLive.SelectedDate;

            //CHECK FOR CHANGE IN CURRENT TARGET DATE
            if (priorGoLive != null && objDiscoveryRequest.CurrentGoLive != priorGoLive)
            {
                ClsTargetDates targetRecord = new ClsTargetDates();
                Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
                targetRecord.idRequest = RequestID;
                targetRecord.TargetDate = Convert.ToDateTime(objDiscoveryRequest.CurrentGoLive);
                targetRecord.ChangeReason = txtTargetChangeReason.Text;
                targetRecord.CreatedBy = (string)Session["userName"];
                targetRecord.CreatedOn = Convert.ToDateTime(DateTime.Now);
                targetRecord.InsertTargetDate(targetRecord);
            }

            //WORLDPAK

            //CONTRACT INFORMATION
            objDiscoveryRequest.ContractNumber = txtContract.Text;
            objDiscoveryRequest.ContractStartDate = rdpStartDate.SelectedDate;
            objDiscoveryRequest.ContractEndDate = rdpEndDate.SelectedDate;
            objDiscoveryRequest.PaymentTerms = txtPaymentTerms.Text;
            objDiscoveryRequest.ContractCurrency = rddlCurrency.SelectedValue;

            //BILLING AND EDI
            objDiscoveryRequest.invoiceType = rddlInvoiceType.SelectedValue;
            objDiscoveryRequest.billtoAccount = txtBillto.Text;
            objDiscoveryRequest.FTPUsername = txtFTPLogin.Text;
            objDiscoveryRequest.FTPPassword = txtFTPpwd.Text;
            objDiscoveryRequest.EDICustomizedFlag = cbxCustomEDI.Checked;
            if (comboxFreightAuditor.SelectedText == "Yes")
                objDiscoveryRequest.FreightAuditor = true;
            else
                objDiscoveryRequest.FreightAuditor = false;

            objDiscoveryRequest.EDIDetails = txtBxEDISolutionSummary.Text;

            //CUSTOMS
            objDiscoveryRequest.customsFlag = chkCustoms.Checked;
            objDiscoveryRequest.elinkFlag = chkElink.Checked;
            objDiscoveryRequest.PARS = txtPARS.Text;
            objDiscoveryRequest.PASS = txtPASS.Text;
            if (rddlBroker.SelectedIndex > 0 && rddlBroker.SelectedValue != "OTHER")
            {
                objDiscoveryRequest.customsBroker = rddlBroker.SelectedValue;
            }
            else
            {
                objDiscoveryRequest.customsBroker = txtOtherBroker.Text;
            }
            objDiscoveryRequest.BrokerNumber = txtBrokerNumber.Text;

            //ACCOUNT SUPPORT
            objDiscoveryRequest.ControlBranch = rddlControlBranch.SelectedValue;
            objDiscoveryRequest.SupportUser = txtSupportUser.Text;
            objDiscoveryRequest.SupportGroup = txtSupportGroup.Text;
            objDiscoveryRequest.Office = txtOffice.Text;
            objDiscoveryRequest.Group = txtGroup.Text;
            objDiscoveryRequest.CRR = txtCRR.Text;

            //MIGRATION DETAILS
            objDiscoveryRequest.MigrationDate = rdpMigrationDate.SelectedDate;
            objDiscoveryRequest.PreMigrationSolution = txtPreMigration.Text;
            objDiscoveryRequest.PostMigrationSolution = txtPostMigration.Text;

            //RETURNS
            objDiscoveryRequest.ReturnsAcctNbr = txtReturnsAcct.Text;
            objDiscoveryRequest.ReturnsAddress = txtReturnsAddress.Text;
            objDiscoveryRequest.ReturnsCity = txtReturnsCity.Text;
            objDiscoveryRequest.ReturnsState = txtReturnsState.Text;
            objDiscoveryRequest.ReturnsZip = txtReturnsZip.Text;
            objDiscoveryRequest.ReturnsCountry = txtReturnsCountry.Text;
            objDiscoveryRequest.ReturnsDestroyFlag = cbxDestroy.Checked;
            objDiscoveryRequest.ReturnsCreateLabelFlag = cbxReturnLabel.Checked;

            //WORLDPAK
            objDiscoveryRequest.WPKSandboxUsername = txtWPKproduser.Text;
            objDiscoveryRequest.WPKSandboxPwd = txtWPKprodpwd.Text;
            objDiscoveryRequest.WPKProdUsername = txtWPKproduser.Text;
            objDiscoveryRequest.WPKProdPwd = txtWPKprodpwd.Text;
            objDiscoveryRequest.WPKCustomExportFlag = cbxExportFile.Checked;
            objDiscoveryRequest.WPKGhostScanFlag = cbxGhostScan.Checked;
            objDiscoveryRequest.WPKEastWestSplitFlag = cbxSplit.Checked;
            objDiscoveryRequest.WPKAddressUploadFlag = cbxAddressBook.Checked;
            objDiscoveryRequest.WPKProductUploadFlag = cbxProdUpload.Checked;
            objDiscoveryRequest.WPKEastWestSplitFlag = cbxSplit.Checked;
            objDiscoveryRequest.WPKEquipmentFlag = cbxEquipment.Checked;
            objDiscoveryRequest.WPKDataEntryMethod = rddlDataEntry.SelectedText;
            objDiscoveryRequest.EWSelectBy = rddlEWselection.SelectedText;
            objDiscoveryRequest.EWSortCodeFlag = cbxEWsortcode.Checked;
            objDiscoveryRequest.EWEastSortCode = txtEWesortcode.Text;
            objDiscoveryRequest.EWWestSortCode = txtEWwsortcode.Text;
            objDiscoveryRequest.EWSepCloseoutFlag = cbxEWcloseout.Checked;
            objDiscoveryRequest.EWSepPUFlag = cbxEWpickups.Checked;
            objDiscoveryRequest.EWSortDetails = txtEWsorting.Text;
            objDiscoveryRequest.EWMissortDetails = txtEWmissort.Text;

            //Need to make sure these values are not overwritten on update
            if (newflag == true)
            {
                objDiscoveryRequest.CreatedBy = (string)Session["userName"];
                objDiscoveryRequest.CreatedOn = Convert.ToDateTime(DateTime.Now);
                objDiscoveryRequest.ActiveFlag = true;
            }
            else
            {
                //maintain original CREATED BY values
                objDiscoveryRequest.idRequest = editRequest.idRequest;
                objDiscoveryRequest.CreatedBy = editRequest.CreatedBy;
                objDiscoveryRequest.CreatedOn = editRequest.CreatedOn;
                //UPDATED BY
                objDiscoveryRequest.UpdatedBy = (string)Session["userName"];
                objDiscoveryRequest.UpdatedOn = Convert.ToDateTime(DateTime.Now);
                objDiscoveryRequest.ActiveFlag = true;
            }
        }
        catch (Exception ex)
        {
            lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }

        return objDiscoveryRequest;
    }

    private ClsDiscoveryRequestDetails populateDiscoveryRequestDetails(bool newflag)
    {
        try
        {
            string ShipRecordType = "";


            ClsDiscoveryRequestDetails editDetails = new ClsDiscoveryRequestDetails();
            if (newflag == true)
            {

            }
            else
            {
                //Get Existing Request, if Details have been entered
                //Int32 RequestID = (Int32)Session["requestID"];
                Int32 RequestID = Convert.ToInt32(lblRequestID.Text);

                //Get Existing Request Shipping Details
                ClsDiscoveryRequestDetails drd = new ClsDiscoveryRequestDetails();
                editDetails = drd.GetDiscoveryRequestDetails(RequestID, ShipRecordType);
            }

            //CANADA POST
            objDiscoveryRequestDetails.CPCAcctNbr = txtCPCAcct.Text;
            objDiscoveryRequestDetails.CPCContractNbr = txtCPCContract.Text;
            objDiscoveryRequestDetails.CPCInductionNbr = txtCPCInduction.Text;
            objDiscoveryRequestDetails.CPCSiteNbr = txtCPCSite.Text;
            objDiscoveryRequestDetails.CPCUsername = txtCPCUser.Text;
            objDiscoveryRequestDetails.CPCpwd = txtCPCPwd.Text;

            //COURIER
            objDiscoveryRequestDetails.CourierAcctNbr = txtCourierAcct.Text;
            objDiscoveryRequestDetails.CourierContractNbr = txtCourierContract.Text;
            objDiscoveryRequestDetails.CourierFTPsenderID = txtCourierSenderID.Text;
            objDiscoveryRequestDetails.CourierFTPusername = txtCourierFTPuser.Text;
            objDiscoveryRequestDetails.CourierFTPpwd = txtCourierFTPpwd.Text;
            objDiscoveryRequestDetails.CourierSandboxFTPusername = txtSandboxFTPuser.Text;
            objDiscoveryRequestDetails.CourierSandboxFTPpwd = txtSandboxFTPpwd.Text;
            objDiscoveryRequestDetails.CourierPinPrefix = txtCourierPin.Text;
            if (txtCourierTransitDays.Text == "")
                txtCourierTransitDays.Text = "0";
            objDiscoveryRequestDetails.CourierTransitDays = Convert.ToInt16(txtCourierTransitDays.Text);
            objDiscoveryRequestDetails.CourierInductionDesc = rddlCourierInduction.SelectedText;
            objDiscoveryRequestDetails.CourierInductionAddress = courierIaddress.Text;
            objDiscoveryRequestDetails.CourierInductionCity = courierIcity.Text;
            objDiscoveryRequestDetails.CourierInductionState = courierIstate.Text;
            objDiscoveryRequestDetails.CourierInductionZip = courierIzip.Text;
            objDiscoveryRequestDetails.CourierInductionCountry = courierIcountry.Text;
            objDiscoveryRequestDetails.CourierFTPCustOwnFlag = cbxCourierFTPCustOwn.Checked;

            //LTL
            objDiscoveryRequestDetails.LTLAcctNbr = txtLTLAccount.Text;
            objDiscoveryRequestDetails.LTLMinProNbr = txtLTLminPro.Text;
            objDiscoveryRequestDetails.LTLMaxProNbr = txtLTLmaxPro.Text;
            objDiscoveryRequestDetails.LTLPinPrefix = txtLTLPin.Text;
            objDiscoveryRequestDetails.LTLAutomatedFlag = cbxLTLAuto.Checked;

            //PUROPOST
            objDiscoveryRequestDetails.PPSTAcctNbr = txtPPSTAcct.Text;
            if (txtPPSTTransitDays.Text == "")
                txtPPSTTransitDays.Text = "0";
            objDiscoveryRequestDetails.PPSTTransitDays = Convert.ToInt16(txtPPSTTransitDays.Text);
            objDiscoveryRequestDetails.PPSTInductionDesc = rddlPPSTInduction.SelectedText;
            objDiscoveryRequestDetails.PPSTInductionAddress = ppstIaddress.Text;
            objDiscoveryRequestDetails.PPSTInductionCity = ppstIcity.Text;
            objDiscoveryRequestDetails.PPSTInductionState = ppstIstate.Text;
            objDiscoveryRequestDetails.PPSTInductionZip = ppstIzip.Text;
            objDiscoveryRequestDetails.PPSTInductionCountry = ppstIcountry.Text;
            objDiscoveryRequestDetails.PPSTRoute = txtPPSTRoute.Text;

            //PUROPOST Plus
            objDiscoveryRequestDetails.PPlusAcctNbr = txtPPlusAcct.Text;
            if (txtPPlusTransitDays.Text == "")
                txtPPlusTransitDays.Text = "0";
            objDiscoveryRequestDetails.PPlusTransitDays = Convert.ToInt16(txtPPlusTransitDays.Text);
            objDiscoveryRequestDetails.PPlusInductionDesc = rddlPPlusInduction.SelectedText;
            objDiscoveryRequestDetails.PPlusInductionAddress = PPlusIaddress.Text;
            objDiscoveryRequestDetails.PPlusInductionCity = PPlusIcity.Text;
            objDiscoveryRequestDetails.PPlusInductionState = PPlusIstate.Text;
            objDiscoveryRequestDetails.PPlusInductionZip = PPlusIzip.Text;
            objDiscoveryRequestDetails.PPlusInductionCountry = PPlusIcountry.Text;
            objDiscoveryRequestDetails.PPlusRoute = txtPPlusRoute.Text;

            //ShipRecordType is blank for regular shipments
            //In case of East West, East is blank, ShipRecordType="West" for west
            objDiscoveryRequestDetails.ShipRecordType = ShipRecordType;

            //Need to make sure these values are not overwritten on update
            if (newflag == true)
            {
                objDiscoveryRequestDetails.CreatedBy = (string)Session["userName"];
                objDiscoveryRequestDetails.CreatedOn = Convert.ToDateTime(DateTime.Now);
                objDiscoveryRequestDetails.ActiveFlag = true;

            }
            else
            {
                //maintain original CREATED BY values
                objDiscoveryRequestDetails.idRequest = editDetails.idRequest;
                objDiscoveryRequestDetails.CreatedBy = editDetails.CreatedBy;
                objDiscoveryRequestDetails.CreatedOn = editDetails.CreatedOn;
                //UPDATED BY
                objDiscoveryRequestDetails.UpdatedBy = (string)Session["userName"];
                objDiscoveryRequestDetails.UpdatedOn = Convert.ToDateTime(DateTime.Now);
                objDiscoveryRequestDetails.ActiveFlag = true;
            }

        }
        catch (Exception ex)
        {
            lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }
        return objDiscoveryRequestDetails;
    }

    private ClsDiscoveryRequestDetails populateDiscoveryRequestDetailsWest(bool newflag)
    {
        try
        {

            string ShipRecordType = "West";



            //CANADA POST
            objDiscoveryRequestDetails.CPCAcctNbr = txtCPCAcctWest.Text;
            objDiscoveryRequestDetails.CPCContractNbr = txtCPCContractWest.Text;
            objDiscoveryRequestDetails.CPCInductionNbr = txtCPCInductionWest.Text;
            objDiscoveryRequestDetails.CPCSiteNbr = txtCPCSiteWest.Text;
            objDiscoveryRequestDetails.CPCUsername = txtCPCUserWest.Text;
            objDiscoveryRequestDetails.CPCpwd = txtCPCPwdWest.Text;

            //COURIER
            objDiscoveryRequestDetails.CourierAcctNbr = txtCourierAcctWest.Text;
            objDiscoveryRequestDetails.CourierContractNbr = txtCourierContractWest.Text;
            objDiscoveryRequestDetails.CourierFTPsenderID = txtCourierSenderIDWest.Text;
            objDiscoveryRequestDetails.CourierFTPusername = txtCourierFTPuserWest.Text;
            objDiscoveryRequestDetails.CourierFTPpwd = txtCourierFTPpwdWest.Text;
            objDiscoveryRequestDetails.CourierSandboxFTPusername = txtSandboxFTPuserWest.Text;
            objDiscoveryRequestDetails.CourierSandboxFTPpwd = txtSandboxFTPpwdWest.Text;
            objDiscoveryRequestDetails.CourierPinPrefix = txtCourierPinWest.Text;
            if (txtCourierTransitDaysWest.Text == "")
                txtCourierTransitDaysWest.Text = "0";
            objDiscoveryRequestDetails.CourierTransitDays = Convert.ToInt16(txtCourierTransitDaysWest.Text);
            objDiscoveryRequestDetails.CourierInductionDesc = rddlCourierInductionWest.SelectedText;
            objDiscoveryRequestDetails.CourierInductionAddress = courierIaddressWest.Text;
            objDiscoveryRequestDetails.CourierInductionCity = courierIcityWest.Text;
            objDiscoveryRequestDetails.CourierInductionState = courierIstateWest.Text;
            objDiscoveryRequestDetails.CourierInductionZip = courierIzipWest.Text;
            objDiscoveryRequestDetails.CourierInductionCountry = courierIcountryWest.Text;
            objDiscoveryRequestDetails.CourierFTPCustOwnFlag = cbxCourierFTPCustOwnWest.Checked;

            //LTL
            objDiscoveryRequestDetails.LTLAcctNbr = txtLTLAccountWest.Text;
            objDiscoveryRequestDetails.LTLMinProNbr = txtLTLminProWest.Text;
            objDiscoveryRequestDetails.LTLMaxProNbr = txtLTLmaxProWest.Text;
            objDiscoveryRequestDetails.LTLPinPrefix = txtLTLPinWest.Text;
            //there is only one flag for LTL automated
            objDiscoveryRequestDetails.LTLAutomatedFlag = cbxLTLAuto.Checked;

            //PUROPOST
            objDiscoveryRequestDetails.PPSTAcctNbr = txtPPSTAcctWest.Text;
            if (txtPPSTTransitDaysWest.Text == "")
                txtPPSTTransitDaysWest.Text = "0";
            objDiscoveryRequestDetails.PPSTTransitDays = Convert.ToInt16(txtPPSTTransitDaysWest.Text);
            objDiscoveryRequestDetails.PPSTInductionDesc = rddlPPSTInductionWest.SelectedText;
            objDiscoveryRequestDetails.PPSTInductionAddress = ppstIaddressWest.Text;
            objDiscoveryRequestDetails.PPSTInductionCity = ppstIcityWest.Text;
            objDiscoveryRequestDetails.PPSTInductionState = ppstIstateWest.Text;
            objDiscoveryRequestDetails.PPSTInductionZip = ppstIzipWest.Text;
            objDiscoveryRequestDetails.PPSTInductionCountry = ppstIcountryWest.Text;
            objDiscoveryRequestDetails.PPSTRoute = txtPPSTRouteWest.Text;

            //PUROPOST PLUS
            objDiscoveryRequestDetails.PPlusAcctNbr = txtPPlusAcctWest.Text;
            if (txtPPlusTransitDaysWest.Text == "")
                txtPPlusTransitDaysWest.Text = "0";
            objDiscoveryRequestDetails.PPlusTransitDays = Convert.ToInt16(txtPPlusTransitDaysWest.Text);
            objDiscoveryRequestDetails.PPlusInductionDesc = rddlPPlusInductionWest.SelectedText;
            objDiscoveryRequestDetails.PPlusInductionAddress = PPlusIaddressWest.Text;
            objDiscoveryRequestDetails.PPlusInductionCity = PPlusIcityWest.Text;
            objDiscoveryRequestDetails.PPlusInductionState = PPlusIstateWest.Text;
            objDiscoveryRequestDetails.PPlusInductionZip = PPlusIzipWest.Text;
            objDiscoveryRequestDetails.PPlusInductionCountry = PPlusIcountryWest.Text;
            objDiscoveryRequestDetails.PPlusRoute = txtPPlusRouteWest.Text;

            objDiscoveryRequestDetails.ShipRecordType = ShipRecordType;

            //West record may be new, even on existing, if they just checked the East/West box so check for null record
            ClsDiscoveryRequestDetails westDetails = new ClsDiscoveryRequestDetails();
            //Get Existing Request, if West record already created have been entered
            //Int32 RequestID = (Int32)Session["requestID"];
            Int32 RequestID = Convert.ToInt32(lblRequestID.Text);

            ClsDiscoveryRequestDetails drd = new ClsDiscoveryRequestDetails();
            westDetails = drd.GetDiscoveryRequestDetails(RequestID, ShipRecordType);

            if (westDetails == null)
            {
                objDiscoveryRequestDetails.CreatedBy = (string)Session["userName"];
                objDiscoveryRequestDetails.CreatedOn = Convert.ToDateTime(DateTime.Now);
                objDiscoveryRequestDetails.ActiveFlag = true;

            }
            else
            {
                //maintain original CREATED BY values
                objDiscoveryRequestDetails.idRequest = westDetails.idRequest;
                objDiscoveryRequestDetails.CreatedBy = westDetails.CreatedBy;
                objDiscoveryRequestDetails.CreatedOn = westDetails.CreatedOn;
                //UPDATED BY
                objDiscoveryRequestDetails.UpdatedBy = (string)Session["userName"];
                objDiscoveryRequestDetails.UpdatedOn = Convert.ToDateTime(DateTime.Now);
                objDiscoveryRequestDetails.ActiveFlag = true;
            }

        }
        catch (Exception ex)
        {
            lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }
        return objDiscoveryRequestDetails;
    }

    private ClsNotes populateNoteObj(bool newflag)
    {
        ClsNotes objNote = new ClsNotes();
        try
        {
            objNote.idRequest = Convert.ToInt32(lblRequestID.Text);
            objNote.noteDate = dpNoteDate.SelectedDate;
            objNote.publicNote = txtNotes.Text;

            if (rddlInternalType.Visible == true)
            {
                objNote.idTaskType = Convert.ToInt16(rddlInternalType.SelectedValue);
            }
            else
            {
                //default to Other for Sales
                objNote.idTaskType = 4;
            }
            if (txtInternalTimeSpent.Visible == true)
            {
                objNote.timeSpent = Convert.ToInt16(txtInternalTimeSpent.Text);
            }
            else
            {
                objNote.timeSpent = 0;
            }
            if (txtInternalNotes.Visible == true)
            {
                objNote.privateNote = txtInternalNotes.Text;
            }
            else
            {
                objNote.privateNote = "";
            }

            //Need to make sure these three are not overwritten on update
            if (newflag == true)
            {
                objNote.CreatedBy = (string)Session["userName"];
                objNote.CreatedOn = Convert.ToDateTime(DateTime.Now);
                objNote.ActiveFlag = true;
            }
            else
            {
                objNote.UpdatedBy = (string)Session["userName"];
                objNote.UpdatedOn = Convert.ToDateTime(DateTime.Now);
                objNote.ActiveFlag = true;
            }
        }
        catch (Exception ex)
        {
            lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }

        return objNote;

    }
    protected void rgNotesGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;
                string enteredBy = item["Createdby"].Text;
                string userName = Session["userName"].ToString();
                string userRole = Session["userRole"].ToString().ToLower();
                if (enteredBy != userName && userRole != "itadmin")
                {
                    //remove edit and delete                  
                    ImageButton img = (ImageButton)item["EditLink"].Controls[0]; //Accessing EditCommandColumn
                    img.Visible = false;
                    ImageButton img2 = (ImageButton)item["DeleteLink"].Controls[0]; //Accessing EditCommandColumn
                    img2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
                lblWarning.Visible = true;
                pnlwarning.Visible = true;
            }
        }
        if ((e.Item is GridEditFormItem) && (e.Item.IsInEditMode))
        {
            try
            {
                //************First calling dropdown list values selected in pop up edit form**************/ 
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                RadDropDownList ddl = (RadDropDownList)userControl.FindControl("rddlInternalTypeEdit") as RadDropDownList;
                int selectedind = Convert.ToInt32(ddl.SelectedValue);
                RequiredFieldValidator rfvNotesEdit = (RequiredFieldValidator)userControl.FindControl("rfvNotesEdit") as RequiredFieldValidator;
                RequiredFieldValidator rfvNotes2Edit = (RequiredFieldValidator)userControl.FindControl("rfvNotes2Edit") as RequiredFieldValidator;
                Label enote1ast = (Label)userControl.FindControl("note1ast") as Label;
                Label enote2ast = (Label)userControl.FindControl("note2ast") as Label;
                int seelectedind = Convert.ToInt32(ddl.SelectedValue);
                if (selectedind == 1014)
                {
                    enote1ast.Visible = false;
                    enote2ast.Visible = true;
                    rfvNotesEdit.Enabled = false;
                    rfvNotes2Edit.Enabled = true;
                }
                else
                {
                    enote1ast.Visible = true;
                    enote2ast.Visible = false;
                    rfvNotesEdit.Enabled = true;
                    rfvNotes2Edit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
                lblWarning.Visible = true;
                pnlwarning.Visible = true;
            }
        }
    }

    protected void rgNotesGrid_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == RadGrid.ExportToExcelCommandName)
        {
            rgNotesGrid.MasterTableView.GetColumn("EditLink").Visible = false;
            rgNotesGrid.MasterTableView.GetColumn("DeleteLink").Visible = false;
        }
    }
    
    #region  File Uploads Tab with File Grid
    protected void rgUpload_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        getUploads();
    }

    protected void getUploads()
    {
        //Int32 RequestID = Convert.ToInt32(Request.QueryString["requestID"]);
        Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
        List<ClsFileUpload> alluploads = repository.GetFileList(RequestID);
        rgUpload.DataSource = alluploads;
        rgUpload.Visible = true;
    }
    protected void rgUpload_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem fileitem = (GridDataItem)e.Item;
                HyperLink fhLink = (HyperLink)fileitem["FilePath"].Controls[0];
                fhLink.ForeColor = System.Drawing.Color.Blue;
                ClsFileUpload row = (ClsFileUpload)fileitem.DataItem;
                fhLink.Attributes["onclick"] = "OpenWin('" + row.FilePath + "');";
            }
        }
        catch (Exception ex)
        {
            lblWarning.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }
    }
    protected void rgUpload_DeleteCommand(object sender, GridCommandEventArgs e)
    {
    }

    protected void rgUpload_UpdateCommand(object sender, GridCommandEventArgs e)
    {
        try
        {
            int rownum = e.Item.ItemIndex;
            ClsFileUpload fileup = new ClsFileUpload();
            GridEditFormItem item = (GridEditFormItem)e.Item;
            TableCell cell = item["idFileUpload"];
            string strFileUploadId = cell.Text;
            int fileUploadID = Convert.ToInt32(strFileUploadId);
            ClsFileUpload objFileUpload = fileup.GetFileUpload(fileUploadID);

            //Make changes and submit
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

            RadTextBox updatedDesc = (RadTextBox)userControl.FindControl("txtDescription");
            objFileUpload.Description = updatedDesc.Text;


            objFileUpload.UpdatedBy = (string)Session["userName"];
            objFileUpload.UpdatedOn = Convert.ToDateTime(DateTime.Now);
            objFileUpload.ActiveFlag = (userControl.FindControl("ActiveFlag") as RadButton).Checked;

            String msg = fileup.UpdateFileUpload(objFileUpload);
            rgUpload.Rebind();

        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
            e.Canceled = true;
        }
    }

    protected void rgUpload_InsertCommand(object sender, GridCommandEventArgs e)
    {
    }
    protected void btnSaveUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string errmsg;
            int fileID;
            string FilePath = ConfigurationManager.AppSettings["FileUploadPath"].ToString();
            Int32 RequestID = Convert.ToInt32(lblRequestID.Text);

            foreach (UploadedFile f in RadAsyncUpload1.UploadedFiles)
            {
                string fileName = f.GetName();
                string title = f.GetFieldValue("TextBox");
                ClsFileUpload filedata = new ClsFileUpload();
                filedata.idRequest = RequestID;
                filedata.FilePath = FilePath + fileName;
                filedata.Description = title;
                filedata.ActiveFlag = true;
                filedata.CreatedBy = (string)(Session["userName"]);
                filedata.CreatedOn = Convert.ToDateTime(DateTime.Now);
                filedata.UploadDate = Convert.ToDateTime(DateTime.Now);

                errmsg = filedata.InsertFileUpload(filedata, out fileID);
                if (errmsg != "")
                {
                    pnlDanger.Visible = true;
                    lblDanger.Text = errmsg;
                }

                getUploads();
                rgUpload.Rebind();
            }
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    #endregion
    #region  EDI Services Tab For File Grid
    protected void getEDIServicesFilesUpload()
    {
        Int32 RequestID = Convert.ToInt32(lblRequestID.Text);
        string FilePath = ConfigurationManager.AppSettings["FileEDIServicesUploadPath"].ToString();
        List<ClsFileUpload> alluploads = repository.GetFileList(RequestID, FilePath);
        //gridEDIServicesUpload.DataSource = alluploads;
        //gridEDIServicesUpload.Visible = true;
    }
    protected void gridEDIServiesUpload_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        getEDIServicesFilesUpload();
    }
    //protected void btnSaveEDIServicesUpload_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string errmsg;
    //        int fileID;
    //        string FilePath = ConfigurationManager.AppSettings["FileEDIServicesUploadPath"].ToString();
    //        Int32 RequestID = Convert.ToInt32(lblRequestID.Text);

    //        foreach (UploadedFile f in RadAsynEDIServicesUpload.UploadedFiles)
    //        {
    //            string fileName = f.GetName();
    //            string title = f.GetFieldValue("TextBox");
    //            ClsFileUpload filedata = new ClsFileUpload();
    //            filedata.idRequest = RequestID;
    //            filedata.FilePath = FilePath + fileName;
    //            filedata.Description = title;
    //            filedata.ActiveFlag = true;
    //            filedata.CreatedBy = (string)(Session["userName"]);
    //            filedata.CreatedOn = Convert.ToDateTime(DateTime.Now);
    //            filedata.UploadDate = Convert.ToDateTime(DateTime.Now);

    //            errmsg = filedata.InsertFileUpload(filedata, out fileID);
    //            if (errmsg != "")
    //            {
    //                pnlDanger.Visible = true;
    //                lblDanger.Text = errmsg;
    //            }

    //            getEDIServicesFilesUpload();
    //            gridEDIServicesUpload.Rebind();
    //        }
    //        RadMultiPage1.SelectedIndex = 3;
    //        RadTabStrip1.Tabs[3].Selected = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        pnlDanger.Visible = true;
    //        lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
    //    }
    //}
    #endregion
    protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }

    protected void sendManagerEmail(ClsDiscoveryRequest objDiscoveryRequest)
    {
        try
        {
            string subject = "Discovery Request Notification";

            string msgBody = "";

            msgBody = composeEmail(objDiscoveryRequest);


            string host = ConfigurationManager.AppSettings["host"].ToString();
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string userName = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];

            string fromEmail = ConfigurationManager.AppSettings["fromEmail"];

            string toEmail = repository.GetNewReqEmailList();



            SmtpClient client = new SmtpClient(host, port);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(userName, password);

            string errorMsg = "Error Sending Email";
            MailMessage message = new MailMessage(fromEmail, toEmail, subject, errorMsg);
            message.Body = msgBody;
            client.Send(message);

            //MK 7-30, Send email to person submitting
            string subject2 = "Discovery Request Received";
            string errorMsg2 = "Error Sending Requestor Email";
            string ccEmail = objDiscoveryRequest.SalesRepEmail;
            MailMessage message2 = new MailMessage(fromEmail, ccEmail, subject2, errorMsg2);
            message2.Body = "We have received your Discovery Request:\n" + msgBody;
            client.Send(message2);
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected string composeEmail(ClsDiscoveryRequest objDiscoveryRequest)
    {
        string msgBody = "";

        // strategic 
        if (objDiscoveryRequest.StrategicFlag == true)
            msgBody = msgBody + "\nSTRATEGIC ACCOUNT\n";

        //Customer info
        msgBody = msgBody + objDiscoveryRequest.CustomerName;

        msgBody = msgBody + "\n" + objDiscoveryRequest.Address;
        msgBody = msgBody + "\n" + objDiscoveryRequest.City + " " + objDiscoveryRequest.State + " " + objDiscoveryRequest.Zipcode + " " + objDiscoveryRequest.Country;
        msgBody = msgBody + "\n\nProjected Revenue: " + objDiscoveryRequest.ProjectedRevenue.ToString("$#,###,###.00");
        msgBody = msgBody + "\nCommodity: " + objDiscoveryRequest.Commodity;
        if (objDiscoveryRequest.CustomerWebsite != "")
            msgBody = msgBody + "\nWebSite: " + objDiscoveryRequest.CustomerWebsite;


        //Sales Professional info
        msgBody = msgBody + "\n\nSales Professional: " + objDiscoveryRequest.SalesRepName + " " + objDiscoveryRequest.SalesRepEmail + " " + objDiscoveryRequest.District + " District - " + objDiscoveryRequest.Branch;


        //current solution
        msgBody = msgBody + "\n\nCurrent Solution:";
        msgBody = msgBody + "\n" + objDiscoveryRequest.CurrentSolution;

        //proposed services
        msgBody = msgBody + "\n\nProposed Services:";
        List<ClsDiscoveryRequestSvcs> svcList = repository.GetProposedServices(objDiscoveryRequest.idRequest);
        foreach (ClsDiscoveryRequestSvcs svc in svcList)
        {
            msgBody = msgBody + "\n" + svc.serviceDesc + " volume: " + svc.volume;
        }
        msgBody = msgBody + "\n\nProposed Customs:";
        msgBody = msgBody + "\n" + objDiscoveryRequest.ProposedCustoms;

        if (objDiscoveryRequest.SalesComments != "")
        {
            msgBody = msgBody + "\n\nCustomer Notes: " + objDiscoveryRequest.SalesComments;
        }
        // New Request Flag  
        if (objDiscoveryRequest.flagNewRequest == true)
            msgBody = msgBody + "\n*New Business";

        //contact info
        //if (objDiscoveryRequest.CustomerBusContact != "")
        //{
        //    msgBody = msgBody + "\n\nBusiness Contact:";
        //    msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerBusContact + " " + objDiscoveryRequest.CustomerBusTitle;
        //    msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerBusEmail;
        //    msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerBusPhone;
        //}

        //if (objDiscoveryRequest.CustomerITContact != "")
        //{
        //    msgBody = msgBody + "\n\nIT Contact:";
        //    msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerITContact + " " + objDiscoveryRequest.CustomerITTitle;
        //    msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerITEmail;
        //    msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerITPhone;
        //}

        return msgBody;
    }


    protected void sendITBAEmail(ClsDiscoveryRequest objDiscoveryRequest)
    {
        try
        {
            ClsITBA ba = new ClsITBA();
            ClsITBA currentITBA = ba.GetITBA(Convert.ToInt16(objDiscoveryRequest.idITBA));
            string ITBAemail = currentITBA.ITBAEmail;
            //get email address
            string subject = "Discovery Request Assigned To You";
            //string msgBody = "Discovery Request Notification:\n\n Request Assigned to You For Customer: " + Customer + " \nSubmitted On " + DateTime.Now.ToString("MM-dd-yyyy");
            string msgBody = composeEmail(objDiscoveryRequest);

            string host = ConfigurationManager.AppSettings["host"].ToString();
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string userName = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];

            string fromEmail = ConfigurationManager.AppSettings["fromEmail"];
            string toEmail = ITBAemail;

            SmtpClient client = new SmtpClient(host, port);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(userName, password);

            string errorMsg = "Error Sending Email";
            MailMessage message = new MailMessage(fromEmail, toEmail, subject, errorMsg);

            message.Body = msgBody;

            client.Send(message);
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }
    protected void sendEDISpecialistEmail(ClsDiscoveryRequest objDiscoveryRequest)
    {
        try
        {
            clsEDISpecialist currentEDISpecialist = SrvEDISpecialist.GetEDISpecialistByIDView(Convert.ToInt16(objDiscoveryRequest.idEDISpecialist));
            string EDISpecialistemail = currentEDISpecialist.email;
            string subject = "Discovery Request Assigned To You";
            string msgBody = composeEmail(objDiscoveryRequest);

            string host = ConfigurationManager.AppSettings["host"].ToString();
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string userName = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];

            string fromEmail = ConfigurationManager.AppSettings["fromEmail"];
            string toEmail = EDISpecialistemail;

            SmtpClient client = new SmtpClient(host, port);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(userName, password);

            string errorMsg = "Error Sending Email";
            MailMessage message = new MailMessage(fromEmail, toEmail, subject, errorMsg);

            message.Body = msgBody;

            client.Send(message);
        }
        catch (Exception ex)
        {
            long lnewID = 0;
            clsExceptionLogging error = new clsExceptionLogging() { Method = GetCurrentMethod(), ExceptionMsg = ex.Message.ToString(), ExceptionType = ex.GetType().Name.ToString(), ExceptionURL = context.Current.Request.Url.ToString(), ExceptionSource = ex.StackTrace.ToString(), CreatedOn = DateTime.Now, CreatedBy = Session["userName"].ToString() };
            SrvExceptionLogging.Insert(error, out lnewID);

            pnlDanger.Visible = true;
            lblDanger.Text = GetCurrentMethod() + " - " + ex.Message.ToString();
        }
    }

    protected void cbxWPK_Click(object sender, System.EventArgs e)
    {
        bool wpk = cbxWPK.Checked;
    }

    protected void cbxEquipment_Click(object sender, System.EventArgs e)
    {
        bool flag = cbxEquipment.Checked;
        showhideEquipmentBar(flag);
    }

    protected void showhideEquipmentBar(bool flag)
    {
        if (flag == true)
        {
            RadPanelBar1.FindItemByValue("Equipment").Visible = true;
        }
        else
        {
            RadPanelBar1.FindItemByValue("Equipment").Visible = false;
        }
    }



    protected void cbx3pv_Click(object sender, System.EventArgs e)
    {
        bool tpv = cbx3pv.Checked;
        if (tpv == true)
        {
            rddlThirdPartyVendor.Visible = true;
            lbl3pv.Visible = true;
        }
        else
        {
            rddlThirdPartyVendor.Visible = false;
            lbl3pv.Visible = false;
        }

    }

    protected void cbxEWsortcode_Click(object sender, System.EventArgs e)
    {
        bool sortcode = cbxEWsortcode.Checked;
        ShowHideSortCodes(sortcode);

    }

    protected void ShowHideSortCodes(bool flag)
    {
        if (flag == true)
        {
            lblEWesort.Visible = true;
            txtEWesortcode.Visible = true;
            lblEWwsort.Visible = true;
            txtEWwsortcode.Visible = true;
        }
        else
        {
            lblEWesort.Visible = false;
            txtEWesortcode.Visible = false;
            lblEWwsort.Visible = false;
            txtEWwsortcode.Visible = false;
        }
    }
    protected void cbxSplit_Click(object sender, System.EventArgs e)
    {
        bool split = cbxSplit.Checked;
        ShowHideEastWest(split);

    }

    protected void ShowHideEastWest(bool flag)
    {
        if (flag == true)
        {
            lblEWselection.Visible = true;
            rddlEWselection.Visible = true;
            cbxEWsortcode.Visible = true;
            cbxEWcloseout.Visible = true;
            cbxEWpickups.Visible = true;
            lblEWsorting.Visible = true;
            txtEWsorting.Visible = true;
            lblEWmissort.Visible = true;
            txtEWmissort.Visible = true;
            bool sortcode = cbxEWsortcode.Checked;
            ShowHideSortCodes(sortcode);

            if (RadPanelBar1.FindItemByValue("Courier").Visible == true)
            {
                RadPanelBar1.FindItemByValue("Courier").Text = "Courier East";
                RadPanelBar1.FindItemByValue("CourierWest").Visible = true;
            }
            if (RadPanelBar1.FindItemByValue("LTL").Visible == true)
            {
                RadPanelBar1.FindItemByValue("LTL").Text = "LTL East";
                RadPanelBar1.FindItemByValue("LTLWest").Visible = true;
            }
            if (RadPanelBar1.FindItemByValue("PuroPost").Visible == true)
            {
                RadPanelBar1.FindItemByValue("PuroPost").Text = "PuroPost East";
                RadPanelBar1.FindItemByValue("PuroPostWest").Visible = true;
            }
            if (RadPanelBar1.FindItemByValue("PuroPostPlus").Visible == true)
            {
                RadPanelBar1.FindItemByValue("PuroPostPlus").Text = "PuroPost Plus East";
                RadPanelBar1.FindItemByValue("PuroPostPlusWest").Visible = true;
            }
            if (RadPanelBar1.FindItemByValue("CPC").Visible == true)
            {
                RadPanelBar1.FindItemByValue("CPC").Text = "CPC East";
                RadPanelBar1.FindItemByValue("CPCWest").Visible = true;
            }
        }
        else
        {
            lblEWselection.Visible = false;
            rddlEWselection.Visible = false;
            cbxEWsortcode.Visible = false;
            cbxEWcloseout.Visible = false;
            cbxEWpickups.Visible = false;
            lblEWesort.Visible = false;
            txtEWesortcode.Visible = false;
            lblEWwsort.Visible = false;
            txtEWwsortcode.Visible = false;
            lblEWsorting.Visible = false;
            txtEWsorting.Visible = false;
            lblEWmissort.Visible = false;
            txtEWmissort.Visible = false;
            if (RadPanelBar1.FindItemByValue("Courier").Visible == true)
            {
                RadPanelBar1.FindItemByValue("Courier").Text = "Courier";
                RadPanelBar1.FindItemByValue("CourierWest").Visible = false;
            }
            if (RadPanelBar1.FindItemByValue("LTL").Visible == true)
            {
                RadPanelBar1.FindItemByValue("LTL").Text = "LTL";
                RadPanelBar1.FindItemByValue("LTLWest").Visible = false;
            }
            if (RadPanelBar1.FindItemByValue("PuroPost").Visible == true)
            {
                RadPanelBar1.FindItemByValue("PuroPost").Text = "PuroPost";
                RadPanelBar1.FindItemByValue("PuroPostWest").Visible = false;
            }
            if (RadPanelBar1.FindItemByValue("PuroPostPlus").Visible == true)
            {
                RadPanelBar1.FindItemByValue("PuroPostPlus").Text = "PuroPost Plus";
                RadPanelBar1.FindItemByValue("PuroPostPlusWest").Visible = false;
            }
            if (RadPanelBar1.FindItemByValue("CPC").Visible == true)
            {
                RadPanelBar1.FindItemByValue("CPC").Text = "CPC";
                RadPanelBar1.FindItemByValue("CPCWest").Visible = false;
            }
        }

    }


    protected void btnDisplayValues_Click(object sender, System.EventArgs e)
    {
        ltlValues.Text = "";
        foreach (Control c in ph1.Controls)
        {
            if (c.GetType().Name.ToLower().Contains("usercontrol_ascx"))
            {
                UserControl uc = (UserControl)c;
                TextBox tbx1 = uc.FindControl("txtName") as TextBox;
                DropDownList ddl1 = uc.FindControl("ddlCountry") as DropDownList;
                CheckBoxList cbx1 = uc.FindControl("cblEducation") as CheckBoxList;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("Name: " + tbx1.Text + "<br />");
                sb.Append("Country: " + ddl1.SelectedValue + "<br />");
                sb.AppendLine("Education: ");
                foreach (ListItem li in cbx1.Items)
                {
                    if (li.Selected == true)
                    {
                        sb.Append(li.Value + "<br />");
                    }
                }
                sb.Append("<hr />");
                ltlValues.Text += sb.ToString();
            }
        }
    }

    //Find the control that caused the postback.
    public Control GetPostBackControl(Page page)
    {
        Control control = null;

        string ctrlname = page.Request.Params.Get("__EVENTTARGET");
        if ((ctrlname != null) & ctrlname != string.Empty)
        {
            control = page.FindControl(ctrlname);
        }
        else
        {
            foreach (string ctl in page.Request.Form)
            {
                Control c = page.FindControl(ctl);
                if (c is System.Web.UI.WebControls.Button)
                {
                    control = c;
                    break;
                }
            }
        }
        return control;
    }

   
}


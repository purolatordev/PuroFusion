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

public partial class DiscoveryRequestForm2 : System.Web.UI.Page
{
    ClsDiscoveryRequest objDiscoveryRequest = new ClsDiscoveryRequest();
    ClsDiscoveryRequestDetails objDiscoveryRequestDetails = new ClsDiscoveryRequestDetails();
    ClsDiscoveryRequestSvcs objDiscoveryRequestSvcs = new ClsDiscoveryRequestSvcs();
    List<ClsDiscoveryRequestSvcs> listServices = new List<ClsDiscoveryRequestSvcs>();
    PuroTouchRepository repository = new PuroTouchRepository();
    Int16 ClosedID = Convert.ToInt16(ConfigurationManager.AppSettings["ClosedID"]);
    Int16 OnHoldID = Convert.ToInt16(ConfigurationManager.AppSettings["OnHoldID"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userName"] == null)
            Response.Redirect("Default.aspx");

        string username = Session["userName"].ToString();
        if (username != null && Session["appName"] != null)
        {
            if (!IsPostBack)
            {
                //INITIAL DATA LOAD
                lblSubmittedBy.Text = username;
                getDistricts();
                getBranches();
                getCurrency();
                getRelationships();
                getITBAs();
                getShippingChannels();
                getOnboardingPhases();
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
                getContactTypes();

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

                //save Original ITBA, later send email if changed
                Session["ITBA"] = "";
                string requestID = Request.QueryString["requestID"];
                if (!String.IsNullOrEmpty(requestID))
                {
                    //edit mode
                    //If Existing Request, populate form
                    lblRequestID.Text = requestID;
                    displayExistingRequest(Convert.ToInt32(requestID));
                    //Session["newFlag"] = false;
                    //Session["requestID"] = Convert.ToInt32(requestID);
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
                    //Session["newFlag"] = true;
                    lblRequestID.Text = "0";
                    txtEmail.Text = username + "@purolator.com";
                    txtSalesProfessional.Text = username.Replace(".", " ");
                    RadTabStrip1.Tabs[0].Visible = true;
                    RadTabStrip1.Tabs[1].Enabled = false;
                    RadTabStrip1.Tabs[2].Enabled = false;
                    RadTabStrip1.Tabs[3].Enabled = false;
                    RadTabStrip1.Tabs[4].Visible = false;
                    RadTabStrip1.Tabs[5].Visible = false;
                    RadTabStrip1.Tabs[6].Visible = false;
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


        //Role Based Viewing
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
            RadTabStrip1.Tabs[4].Selected = true;
            RadTabStrip1.Tabs[6].Visible = true;
            RadMultiPage1.SelectedIndex = 4;

        }
    }

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


        //Int32 requestID = Convert.ToInt32(Request.QueryString["requestID"]);
        //List<ClsDiscoveryRequestSvcs> svcList = repository.GetProposedServices(requestID);
        //Go by what's in proposedSvcList in case they have just added services before saving
        List<ClsDiscoveryRequestSvcs> svcList = (List<ClsDiscoveryRequestSvcs>)Session["proposedSvcList"];


        foreach (ClsDiscoveryRequestSvcs svc in svcList)
        {

            //SAVE FOR NEXT PHASE
            //switch (svc.serviceDesc)
            //{
            //    case "CPC":
            //        RadPanelBar1.FindItemByValue("CPC").Visible = true;
            //        break;
            //    case "LTL":
            //        RadPanelBar1.FindItemByValue("LTL").Visible = true;
            //        break;
            //    case "Courier":
            //        RadPanelBar1.FindItemByValue("Courier").Visible = true;
            //        break;
            //    case "PuroPost":
            //        RadPanelBar1.FindItemByValue("PuroPost").Visible = true;
            //        break;
            //    case "PuroPost Plus":
            //        RadPanelBar1.FindItemByValue("PuroPostPlus").Visible = true;
            //        break;
            //    case "Returns":
            //        RadPanelBar1.FindItemByValue("Returns").Visible = true;
            //        break;                   
            //}
        }

        //East West Splits
        //bool flag = cbxSplit.Checked;
        //if (flag == true)
        //{
        //    if (RadPanelBar1.FindItemByValue("Courier").Visible == true)
        //    {
        //        RadPanelBar1.FindItemByValue("Courier").Text = "Courier East";
        //        RadPanelBar1.FindItemByValue("CourierWest").Visible = true;
        //    }
        //    if (RadPanelBar1.FindItemByValue("LTL").Visible == true)
        //    {
        //        RadPanelBar1.FindItemByValue("LTL").Text = "LTL East";
        //        RadPanelBar1.FindItemByValue("LTLWest").Visible = true;
        //    }
        //    if (RadPanelBar1.FindItemByValue("PuroPost").Visible == true)
        //    {
        //        RadPanelBar1.FindItemByValue("PuroPost").Text = "PuroPost East";
        //        RadPanelBar1.FindItemByValue("PuroPostWest").Visible = true;
        //    }
        //    if (RadPanelBar1.FindItemByValue("PuroPostPlus").Visible == true)
        //    {
        //        RadPanelBar1.FindItemByValue("PuroPostPlus").Text = "PuroPost Plus East";
        //        RadPanelBar1.FindItemByValue("PuroPostPlusWest").Visible = true;
        //    }
        //    if (RadPanelBar1.FindItemByValue("CPC").Visible == true)
        //    {
        //        RadPanelBar1.FindItemByValue("CPC").Text = "CPC East";
        //        RadPanelBar1.FindItemByValue("CPCWest").Visible = true;
        //    }
        //}
        //else
        //{
        //    if (RadPanelBar1.FindItemByValue("Courier").Visible == true)
        //    {
        //        RadPanelBar1.FindItemByValue("Courier").Text = "Courier";
        //        RadPanelBar1.FindItemByValue("CourierWest").Visible = false;
        //    }
        //    if (RadPanelBar1.FindItemByValue("LTL").Visible == true)
        //    {
        //        RadPanelBar1.FindItemByValue("LTL").Text = "LTL";
        //        RadPanelBar1.FindItemByValue("LTLWest").Visible = false;
        //    }
        //    if (RadPanelBar1.FindItemByValue("PuroPost").Visible == true)
        //    {
        //        RadPanelBar1.FindItemByValue("PuroPost").Text = "PuroPost";
        //        RadPanelBar1.FindItemByValue("PuroPostWest").Visible = false;
        //    }
        //    if (RadPanelBar1.FindItemByValue("PuroPostPlus").Visible == true)
        //    {
        //        RadPanelBar1.FindItemByValue("PuroPostPlus").Text = "PuroPost Plus";
        //        RadPanelBar1.FindItemByValue("PuroPostPlusWest").Visible = false;
        //    }
        //    if (RadPanelBar1.FindItemByValue("CPC").Visible == true)
        //    {
        //        RadPanelBar1.FindItemByValue("CPC").Text = "CPC";
        //        RadPanelBar1.FindItemByValue("CPCWest").Visible = false;
        //    }
        //}
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
        }

    }

    protected void getShippingProducts()
    {
        try
        {

            //List<ClsShippingProducts> prodlist = repository.GetShippingProducts();
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
        }

    }

    protected void getDistricts()
    {
        try
        {
            //ClsDistrict district = new ClsDistrict();
            // PuroTouchRepository rep = new PuroTouchRepository();
            List<ClsDistrict> districtlist = repository.GetDistricts();
            rddlDistrict.DataSource = districtlist;
            rddlDistrict.DataTextField = "District";
            rddlDistrict.DataValueField = "District";
            //rddlDistrict.SelectedText = "UNKNOWN";
            rddlDistrict.DataBind();
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            rddlSolutionType.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
        }
    }
    protected void getContactTypes()
    {
        try
        {
            List<ClsContactType> solutionlist = repository.GetContactTypes();
            rddlContactType.DataSource = solutionlist;
            rddlContactType.DataTextField = "ContactType";
            rddlContactType.DataValueField = "idContactType";
            rddlContactType.DataBind();
            rddlContactType.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
        }
    }

    protected void rdCurrentTarget_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
    {
        try
        {
            //Check for Existing Request
            ClsDiscoveryRequest dr = new ClsDiscoveryRequest();
            //Int32 RequestID = (Int32)Session["requestID"];
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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

            //MK not using Relationship Drop Down for now - previous entries were entered free form and may not match our list
            //if (newrelationship == false)
            //{
            //chkNewBus.Checked = false;
            //rddlRelationships.Visible = true;
            //txtCustomerName.Visible = false;
            //lblCustomerName.Text = "Select Relationship";
            //}
            //else
            //{
            //chkNewBus.Checked = true;
            //rddlRelationships.Visible = false;
            //txtCustomerName.Visible = true;
            //lblCustomerName.Text = "Enter Customer Name";
            //}

        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
        }

    }


    protected void chkMartinGrove_Clicked(object sender, System.EventArgs e)
    {
        try
        {
            //put Martin Grove ID into web.config
            //if (chkMartinGrove.Checked == true)
            //{
            Int16 idInduction = 108;
            ClsInductionPoint inductionAddress = repository.GetInductionPointDetails(idInduction);
            txtReturnsAddress.Text = inductionAddress.Address;
            txtReturnsCity.Text = inductionAddress.City;
            txtReturnsState.Text = inductionAddress.State;
            txtReturnsZip.Text = inductionAddress.Zip;
            txtReturnsCountry.Text = inductionAddress.Country;
            //}
            //else
            //{
            //    txtReturnsAddress.Text = "";
            //    txtReturnsCity.Text = "";
            //    txtReturnsState.Text = "";
            //    txtReturnsZip.Text = "";
            //    txtReturnsCountry.Text = "";
            //}

        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
        }

    }

    protected void getOnboardingPhases()
    {
        try
        {

            List<ClsOnboardingPhase> phaselist = repository.GetOnboardingPhasesInactiveNoted();
            rddlPhase.DataSource = phaselist;
            rddlPhase.DataTextField = "OnboardingPhase";
            rddlPhase.DataValueField = "idOnboardingPhase";
            rddlPhase.DataBind();
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
        }
    }
    #region Grid Testing
    protected void contactGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        int requestID = 0;
        int.TryParse(Request.QueryString["requestID"], out requestID);
        
        (sender as RadGrid).DataSource = SrvContact.GetContactsByRequestID(requestID);
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
            Session["contactList"] = contactList;
            contactGrid.DataSource = contactList;
            contactGrid.Rebind();
            RadMultiPage1.SelectedIndex = 1;
            RadTabStrip1.Tabs[1].Selected = true;
        }
        catch (Exception ex)
        {
            pnlDanger.Visible = true;
            lblDanger.Text = ex.Message.ToString();
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
            //Existing Request may not have Details entered yet
            if (details == null)
            {
                details = new ClsDiscoveryRequestDetails();
            }
            string userRole = ((string)Session["userRole"]).ToLower();
            //if (request.StrategicFlag == null) request.StrategicFlag = false;
            //chkStrategic.Checked = (Boolean)request.StrategicFlag;
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
            txtContactName.Text = request.CustomerBusContact;
            txtContactTitle.Text = request.CustomerBusTitle;
            txtContactEmail.Text = request.CustomerBusEmail;
            txtContactPhone.Text = request.CustomerBusPhone;
            txtContactName2.Text = request.CustomerITContact;
            txtContactTitle2.Text = request.CustomerITTitle;
            txtContactEmail2.Text = request.CustomerITEmail;
            txtContactPhone2.Text = request.CustomerITPhone;

            //Tab3 - Current Solution
            txtareaCurrentSolution.Text = request.CurrentSolution;

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
            Session["ITBA"] = request.idITBA.ToString();
            if (request.idVendorType != null)
                rddlVendorType.SelectedValue = request.idVendorType.ToString();
            rddlPhase.SelectedValue = request.idOnboardingPhase.ToString();
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

            if (userRole == "sales" || userRole == "salesdm" || userRole == "salesmanager")
            {

                //Do  not show Profile to Sales
                RadTabStrip1.Tabs[4].Visible = false;
                //Do not show File Uploads to Sales
                RadTabStrip1.Tabs[6].Visible = false;

                rfvTaskType.Enabled = false;

            }
            if (userRole != "itmanager" && userRole != "admin" && userRole != "itadmin")
            {
                rddlITBA.Enabled = false;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
        }

    }

    protected void UpdateTimeSpent(int requestID)
    {
        //ClsNotes cn = new ClsNotes();
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
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
            var msg = ex.Message;
            pnlDanger.Visible = true;
            lblDanger.Text = msg;
        }

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
        bool contact1 = false;
        bool contact2 = false;
        //for the message you get about dangerous data
        //txtContactEmail.Text = txtContactEmail.Text.Replace("<","");
        //txtContactEmail.Text = txtContactEmail.Text.Replace(">", "");
        //txtContactEmail2.Text = txtContactEmail2.Text.Replace("<", "");
        //txtContactEmail2.Text = txtContactEmail2.Text.Replace(">", "");

        if (txtContactName.Text != "" && txtContactEmail.Text != "" && txtContactPhone.Text != "")
        {
            contact1 = true;
        }
        if (txtContactName2.Text != "" && txtContactEmail2.Text != "" && txtContactPhone2.Text != "")
        {
            contact2 = true;
        }
        if (contact1 == false && contact2 == false)
        {

            ErrorMessage = ErrorMessage + "<br>At Least One Contact Must Be Supplied";
        }
        //check tab3
        string strFromTextArea = txtareaCurrentSolution.Text;
        if (strFromTextArea == "")
        {

            ErrorMessage = ErrorMessage + "<br>Current Solution Description is Missing";
        }
        //check tab4
        string snumSvcs = rgSvcGrid.MasterTableView.Items.Count.ToString();
        Int16 numSvcs = Convert.ToInt16(snumSvcs);
        if (numSvcs < 1)
        {
            ErrorMessage = ErrorMessage + "<br>Must Choose At Least One Service";
        }

        if (txtTargetChangeReason.Visible == true && txtTargetChangeReason.Text == "")
        {
            ErrorMessage = ErrorMessage + "<br>Please Fill in Current Go-Live Date Change Reason";
        }

        return ErrorMessage;
    }



    protected void CustomValidatorContact_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        CustomValidatorContact.ErrorMessage = "";
        bool contact1 = false;
        bool contact2 = false;

        if (txtContactName.Text != "" && txtContactEmail.Text != "" && txtContactPhone.Text != "")
        {
            contact1 = true;
        }

        if (txtContactName2.Text != "" && txtContactEmail2.Text != "" && txtContactPhone2.Text != "")
        {
            contact2 = true;
        }

        if (contact1 == false && contact2 == false)
        {
            args.IsValid = false;
            CustomValidatorContact.ErrorMessage = CustomValidatorContact.ErrorMessage + "Please enter contact name, email and phone";
        }

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
            lblDanger.Text = ex.Message.ToString();

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
            lblDanger.Text = ex.Message.ToString();
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
            lblDanger.Text = ex.Message.ToString();

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
            lblDanger.Text = ex.Message.ToString();
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
            lblDanger.Text = ex.Message.ToString();

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
            lblDanger.Text = ex.Message.ToString();

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
            lblDanger.Text = ex.Message.ToString();
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
            lblDanger.Text = ex.Message.ToString();
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
            lblDanger.Text = ex.Message.ToString();
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
            lblDanger.Text = ex.Message.ToString();
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
        }

    }
    protected void btnNextTab2_Click(object sender, System.EventArgs e)
    {
        if (Page.IsValid)
        {
            RadTabStrip1.Tabs[2].Enabled = true;
            RadTabStrip1.Tabs[2].Selected = true;
            RadMultiPage1.SelectedIndex = 2;
            btnNextTab2.Visible = false;
        }
    }
    protected void btnNextTab3_Click(object sender, System.EventArgs e)
    {
        RadTabStrip1.Tabs[3].Enabled = true;
        RadTabStrip1.Tabs[3].Selected = true;
        RadMultiPage1.SelectedIndex = 3;
        btnSubmit.Enabled = true;
        btnNextTab3.Visible = false;
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
            lblWarning.Text = ex.Message;
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }

    }
    protected void btnAddContact_Click(object sender, System.EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                int requestID = 0;
                int.TryParse(Request.QueryString["requestID"], out requestID);

                RadMultiPage1.SelectedIndex = 1;
                RadTabStrip1.Tabs[1].Selected = true;

                clsContact contact = new clsContact()
                {
                    idContactType = Convert.ToInt16(rddlContactType.SelectedValue),
                    idRequest = requestID,
                    Name = txtBxContactName.Text,
                    Title = txtBxContactTitle.Text,
                    Email = txtBxContactEmail.Text,
                    Phone = txtBxContactPhone.Text,
                    CreatedOn = DateTime.Now,
                    CreatedBy = (string)(Session["userName"])
                };

                int inewID = 0;
                SrvContact.Insert(contact, out inewID);
                List<clsContact> contactList = repository.GetContacts(requestID);
                Session["contactList"] = contactList;
                contactGrid.DataSource = contactList;
                contactGrid.DataBind();
                int er = 0;
                er++;
            }
        }
        catch (Exception ex)
        {
            lblWarning.Text = ex.Message;
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
            lblWarning.Text = ex.Message;
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }

    }


    protected void doSubmit()
    {

        if (Session["userName"] == null)
            Response.Redirect("Default.aspx");

        //FOR EAST WEST SPLITS, THERE ARE TWO DETAIL RECORDS - ShipRecordType is "West" for second record


        //DO SUBMIT
        //bool newFlag = (bool)Session["newFlag"];
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
            //Disable Submit Button so they can't keep submitting duplicates
            //btnSubmit.Enabled = false;

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
                //objDiscoveryRequest.idRequest = newID;
                Int32 requestID = newID;
                objDiscoveryRequest.idRequest = requestID;
                //Session["requestID"] = Convert.ToInt32(requestID);
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

                btnSubmit.Visible = false;
                btnSubmitChanges.Visible = true;

            }

        }
        else
        {
            //DO UPDATE  
            //Int32 RequestID = (Int32)Session["requestID"];
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
                lblWarning.Text = ex.Message;
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
            //IF NEW ASSIGNMENT, CHANGE STATUS
            //Better way to do this?
            //if (objDiscoveryRequest.idOnboardingPhase == 8)
            //{
            //    objDiscoveryRequest.idOnboardingPhase = 1;
            //}
        }


        //FINAL STEP
        if (msg == "")
        {
            //SHOW LAST UPDATED
            lblUpdatedBy.Text = objDiscoveryRequest.UpdatedBy;
            lblUpdatedOn.Text = objDiscoveryRequest.UpdatedOn.ToString();

            //AFTER SAVE, SET TO EXISTING
            //Session["newFlag"] = false;        


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
            //windowManager.RadAlert(alertmsg, 350, 200, "Request Submitted", "submitCallBackFn", "Please confirm");
            //Response.Redirect("Home.aspx");


        }
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
        //try
        //{
        //     //MK - check if time is numeric
        //     int min;
        //     string txtMin = txtInternalTimeSpent.Text;       
        //     bool isNumeric = int.TryParse(txtMin, out min);
        //     if (isNumeric != true)
        //     {
        //         lblTimeWarning.Text = "Time must be numeric";
        //     }
        //     else
        //     {
        //         lblTimeWarning.Text = "";
        //     }


        //     bool newNoteflag = true;
        //     ClsNotes noteObj = new ClsNotes();           
        //     noteObj=populateNoteObj(newNoteflag);
        //     int newID;
        //     String msg = noteObj.InsertNote(noteObj, out newID);
        //    if (msg != "" || isNumeric != true)
        //    {
        //        lblWarning.Text = msg;
        //        lblWarning.Visible = true;
        //        pnlwarning.Visible = true;
        //    }
        //    else
        //    {
        //    RadTabStrip1.Tabs[5].Visible = true;
        //    RadTabStrip1.Tabs[5].Selected = true;
        //    RadMultiPage1.SelectedIndex = 5;
        //        txtNotes.Text = "";
        //        txtInternalTimeSpent.Text = "0";
        //        rddlInternalType.SelectedIndex = -1;

        //        int DRID = Convert.ToInt32(lblRequestID.Text);
        //        //ClsNotes cn = new ClsNotes();
        //        List<ClsNotes> allnotes = repository.GetNotes(DRID);
        //        rgNotesGrid.DataSource = allnotes;
        //        rgNotesGrid.DataBind();
        //        if (Session["userRole"].ToString().ToLower() == "sales" || Session["userRole"].ToString().ToLower() == "salesdm")
        //        {
        //            rgNotesGrid.MasterTableView.GetColumn("timeSpent").Visible = false;
        //            rgNotesGrid.MasterTableView.GetColumn("TaskType").Visible = false;
        //            rgNotesGrid.MasterTableView.GetColumn("privateNote").Visible = false;
        //        }
        //        rgNotesGrid.Visible = true;
        //        UpdateTimeSpent(DRID);
        //    }
        //}

        //catch (Exception ex)
        //{
        //    lblWarning.Text = ex.Message;
        //    lblWarning.Visible = true;
        //    pnlwarning.Visible = true;
        //}

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
            lblWarning.Text = ex.Message;
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, System.EventArgs e)
    {
        //string userRole = Session["userRole"].ToString().ToLower();
        //if (userRole == "admin" || userRole == "itadmin" || userRole == "itba" || userRole == "itmanager")
        //{
        //    Response.Redirect("DiscoveryTracker.aspx");
        //}
        //else
        //{
        //    Response.Redirect("Home.aspx");
        //}
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
            //bool newflag = (bool)Session["newFlag"];
            bool newFlag = false;
            if (lblRequestID.Text == "0" || lblRequestID.Text == "")
                newFlag = true;
            if (newFlag != true)
            {
                //Int32 requestID = Convert.ToInt32(Request.QueryString["requestID"]);
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
            lblWarning.Text = ex.Message;
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
                //Int32 RequestID = (Int32)Session["requestID"];
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
            //if (chkNewBus.Checked == true)
            //{                
            objDiscoveryRequest.CustomerName = txtCustomerName.Text;

            //}
            //else
            //{
            //    objDiscoveryRequest.CustomerName=rddlRelationships.SelectedValue;
            //}
            //objDiscoveryRequest.StrategicFlag = chkStrategic.Checked;
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
            objDiscoveryRequest.CustomerBusContact = txtContactName.Text;
            objDiscoveryRequest.CustomerBusTitle = txtContactTitle.Text;
            objDiscoveryRequest.CustomerBusEmail = txtContactEmail.Text;
            objDiscoveryRequest.CustomerBusPhone = txtContactPhone.Text;
            objDiscoveryRequest.CustomerITContact = txtContactName2.Text;
            objDiscoveryRequest.CustomerITTitle = txtContactTitle2.Text;
            objDiscoveryRequest.CustomerITEmail = txtContactEmail2.Text;
            objDiscoveryRequest.CustomerITPhone = txtContactPhone2.Text;
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
                //if (targetRecord.idRequest != 0)
                //{
                targetRecord.InsertTargetDate(targetRecord);
                //}
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
            lblWarning.Text = ex.Message;
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
            lblWarning.Text = ex.Message;
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
            lblWarning.Text = ex.Message;
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
                //maintain original values
                //objNote.CreatedBy = editRequest.CreatedBy;
                //objNote.CreatedOn = editRequest.CreatedOn;
                objNote.UpdatedBy = (string)Session["userName"];
                objNote.UpdatedOn = Convert.ToDateTime(DateTime.Now);
                objNote.ActiveFlag = true;
            }



        }
        catch (Exception ex)
        {
            lblWarning.Text = ex.Message;
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
                lblWarning.Text = ex.Message;
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
                lblWarning.Text = ex.Message;
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
            lblWarning.Text = ex.Message;
            lblWarning.Visible = true;
            pnlwarning.Visible = true;
        }
    }

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
            lblDanger.Text = ex.Message.ToString();
            e.Canceled = true;
        }
    }

    protected void rgUpload_InsertCommand(object sender, GridCommandEventArgs e)
    {
    }



    protected void RadAsyncUpload1_FileUploaded(object sender, FileUploadedEventArgs e)
    {

        try
        {

        }

        catch (Exception ex)
        {

        }

    }

    protected void btnSaveUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string errmsg;
            int fileID;
            string FilePath = ConfigurationManager.AppSettings["FileUploadPath"].ToString();
            //Int32 RequestID = Convert.ToInt32(Request.QueryString["requestID"]);
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
            lblDanger.Text = ex.Message.ToString();
        }
    }

    //protected void sendManagerEmail(ClsDiscoveryRequest objDiscoveryRequest)
    //{
    //    try
    //    {
    //        string subject = "Discovery Request Notification";
    //        //string msgBody = "New Request Submitted by: " + SubmittedBy + " \n For Customer: " + Customer + " \nSubmitted On " + DateTime.Now.ToString("MM-dd-yyyy");
    //        string msgBody = composeEmail(objDiscoveryRequest);

    //        string host = ConfigurationManager.AppSettings["host"].ToString();
    //        int port = int.Parse(ConfigurationManager.AppSettings["port"]);
    //        string userName = ConfigurationManager.AppSettings["userName"];
    //        string password = ConfigurationManager.AppSettings["password"];

    //        string fromEmail = ConfigurationManager.AppSettings["fromEmail"];
    //        //string toEmail = ConfigurationManager.AppSettings["ManagerEmail"];

    //        string toEmail = repository.GetNewReqEmailList();

    //        SmtpClient client = new SmtpClient(host, port);
    //        client.EnableSsl = true;
    //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
    //        client.UseDefaultCredentials = false;
    //        client.Credentials = new NetworkCredential(userName, password);

    //        string errorMsg = "Error Sending Email";
    //        MailMessage message = new MailMessage(fromEmail, toEmail, subject, errorMsg);

    //        message.Body = msgBody;

    //        client.Send(message);
    //    }
    //    catch (Exception ex)
    //    {
    //        pnlDanger.Visible = true;
    //        lblDanger.Text = ex.Message.ToString();
    //    }

    //}

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
            lblDanger.Text = ex.Message.ToString();
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
        if (objDiscoveryRequest.CustomerBusContact != "")
        {
            msgBody = msgBody + "\n\nBusiness Contact:";
            msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerBusContact + " " + objDiscoveryRequest.CustomerBusTitle;
            msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerBusEmail;
            msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerBusPhone;
        }

        if (objDiscoveryRequest.CustomerITContact != "")
        {
            msgBody = msgBody + "\n\nIT Contact:";
            msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerITContact + " " + objDiscoveryRequest.CustomerITTitle;
            msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerITEmail;
            msgBody = msgBody + "\n" + objDiscoveryRequest.CustomerITPhone;
        }

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
            lblDanger.Text = ex.Message.ToString();
        }


    }

    protected void cbxWPK_Click(object sender, System.EventArgs e)
    {
        bool wpk = cbxWPK.Checked;
        //MK - This part is not in use for now
        //if (wpk == true)
        //{
        //    RadPanelBar1.FindItemByValue("WorldPak").Visible = true;
        //}
        //else
        //{
        //    RadPanelBar1.FindItemByValue("WorldPak").Visible = false;
        //}        

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

}
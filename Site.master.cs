using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }




        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["userName"] != null)
        {
            if (((string)Session["userRole"]).ToLower() == "itmanager" || ((string)Session["userRole"]).ToLower() == "itba" || ((string)Session["userRole"]).ToLower() == "admin" || ((string)Session["userRole"]).ToLower() == "itadmin")
            {
                //Add Tracker
                RadMenuItem trItem = new RadMenuItem();
                trItem.Text = "Discovery Tracker";
                trItem.NavigateUrl = "~/DiscoveryTracker.aspx";
                RadMenu1.Items.Add(trItem);

                //Add Reports
                RadMenuItem repItem = new RadMenuItem();
                repItem.Text = "Reports";
                repItem.NavigateUrl = "~/home.aspx";
                RadMenu1.Items.Add(repItem);

                //report list
                RadMenuItem rptStats = new RadMenuItem();
                rptStats.Text = "Stats Dashboard";
                rptStats.NavigateUrl = "~/getDashboardStats.aspx";
                repItem.Items.Add(rptStats);
                RadMenuItem rptsched = new RadMenuItem();
                rptsched.Text = "Scheduled Go-Live List";
                rptsched.NavigateUrl = "~/scheduledGoLive.aspx";
                repItem.Items.Add(rptsched);
                RadMenuItem rptsched2 = new RadMenuItem();
                rptsched2.Text = "Scheduled Go-Live and Work In Progress";
                rptsched2.NavigateUrl = "~/scheduledGoLiveWithWIP.aspx";
                repItem.Items.Add(rptsched2);
                
               

                RadMenuItem sep = new RadMenuItem();
                sep.IsSeparator = true;
                repItem.Items.Add(sep);

                RadMenuItem rpt8 = new RadMenuItem();
                rpt8.Text = "Onboarding Detail";
                rpt8.NavigateUrl = "~/rptRequestDetails.aspx";
                repItem.Items.Add(rpt8);
                RadMenuItem rpt3 = new RadMenuItem();
                rpt3.Text = "Onboarding Notes";
                rpt3.NavigateUrl = "~/rptRequestNotes.aspx";
                repItem.Items.Add(rpt3);
                RadMenuItem rpt1 = new RadMenuItem();
                rpt1.Text = "Onboarding Time Per Phase";
                rpt1.NavigateUrl = "~/rptTimePerPhase.aspx";
                repItem.Items.Add(rpt1);
                RadMenuItem rpt2 = new RadMenuItem();
                rpt2.Text = "Onboarding Time Per Customer";
                rpt2.NavigateUrl = "~/rptTimePerCustomer.aspx";
                repItem.Items.Add(rpt2);

                RadMenuItem rpt9 = new RadMenuItem();
                rpt9.Text = "Onboarding Time Per Shipping Channel";
                rpt9.NavigateUrl = "~/rptTimePerShippingChannel.aspx";
                repItem.Items.Add(rpt9);       

                RadMenuItem rpt4 = new RadMenuItem();
                rpt4.Text = "Top Ten Customers by Resource Allocation";
                rpt4.NavigateUrl = "~/rptTopTenTimeSpent.aspx";
                repItem.Items.Add(rpt4);
                RadMenuItem rpt5 = new RadMenuItem();
                rpt5.Text = "Top Ten Customers by Projected Revenue";
                rpt5.NavigateUrl = "~/rptTopTenRevenue.aspx";
                repItem.Items.Add(rpt5);
                RadMenuItem rpt6 = new RadMenuItem();
                rpt6.Text = "Closed Request Report";
                rpt6.NavigateUrl = "~/rptClosedRequests.aspx";
                repItem.Items.Add(rpt6);

                RadMenuItem rptC = new RadMenuItem();
                rptC.Text = "Changed Target Dates Report";
                rptC.NavigateUrl = "~/rptChangingTargets.aspx";
                repItem.Items.Add(rptC);

                RadMenuItem rpt7 = new RadMenuItem();
                rpt7.Text = "Time Per Onboarding Specialist";
                rpt7.NavigateUrl = "~/rptTimebyITBA.aspx";
                repItem.Items.Add(rpt7);

                RadMenuItem rpt11 = new RadMenuItem();
                rpt11.Text = "Time Per Onboarding Specialist EDI";
                rpt11.NavigateUrl = "~/rptTimebyITBAEdi.aspx";
                repItem.Items.Add(rpt11);

                RadMenuItem rpt10 = new RadMenuItem();
                rpt10.Text = "Customer Delay Notes";
                rpt10.NavigateUrl = "~/rptCustomerDelay.aspx";
                repItem.Items.Add(rpt10);
            }

            if (((string)Session["userRole"]).ToLower() == "itmanager" || ((string)Session["userRole"]).ToLower() == "admin" || ((string)Session["userRole"]).ToLower() == "itadmin" || ((string)Session["userRole"]).ToLower() == "itba")
            {
                //Add Maintenance
                RadMenuItem repItem = new RadMenuItem();
                repItem.Text = "Maintenance";
                repItem.NavigateUrl = "~/home.aspx";
                RadMenu1.Items.Add(repItem);

                //HIDING MAINTENANCE SCREENS THAT ARE NOT USED IN PHASE 1
                RadMenuItem maintItemCR = new RadMenuItem();
                maintItemCR.Text = "Edit a Target Change Reason";
                maintItemCR.NavigateUrl = "~/ChangeReasonMaintenance.aspx";
                repItem.Items.Add(maintItemCR);
                RadMenuItem maintItem2 = new RadMenuItem();
                maintItem2.Text = "Close Reasons";
                maintItem2.NavigateUrl = "~/ClosedReasonMaintenance.aspx";
                repItem.Items.Add(maintItem2);
                RadMenuItem maintItem17 = new RadMenuItem();
                maintItem17.Text = "Communications Methods";
                maintItem17.NavigateUrl = "~/CommunicationMethodsMaintenance.aspx";
                RadMenuItem maintItem16 = new RadMenuItem();
                maintItem16.Text = "Customs Solutions";
                maintItem16.NavigateUrl = "~/CustomsTypeMaintenance.aspx";
                repItem.Items.Add(maintItem16);
                RadMenuItem maintItemCB = new RadMenuItem();
                maintItemCB.Text = "Customs Brokers";
                maintItemCB.NavigateUrl = "~/BrokerMaintenance.aspx";
                repItem.Items.Add(maintItemCB);
                RadMenuItem maintItem10 = new RadMenuItem();
                maintItem10.Text = "Data Entry Methods";
                maintItem10.NavigateUrl = "~/DataEntryMethodsMaintenance.aspx";
                RadMenuItem maintItem14 = new RadMenuItem();
                maintItem14.Text = "EDI Solutions";
                maintItem14.NavigateUrl = "~/EDISolutionsMaintenance.aspx";
                RadMenuItem maintItem3 = new RadMenuItem();
                maintItem3.Text = "Equipment";
                maintItem3.NavigateUrl = "~/EquipmentMaintenance.aspx";
                RadMenuItem maintItem11 = new RadMenuItem();
                maintItem11.Text = "Induction Points";
                maintItem11.NavigateUrl = "~/InductionPointMaintenance.aspx";
                RadMenuItem maintItem13 = new RadMenuItem();
                maintItem13.Text = "Onboarding Specialist Maintenance";
                maintItem13.NavigateUrl = "~/ITBAMaintenance.aspx";
                RadMenuItem maintItem18 = new RadMenuItem();
                maintItem18.Text = "EDI Specialist Maintenance";
                maintItem18.NavigateUrl = "~/EDISpecialistMaint.aspx";
                RadMenuItem maintItem19 = new RadMenuItem();
                maintItem19.Text = "Billing Specialist Maintenance";
                maintItem19.NavigateUrl = "~/BillingSpecialistMaint.aspx";
                RadMenuItem maintItem20 = new RadMenuItem();
                maintItem20.Text = "Collection Specialist Maintenance";
                maintItem20.NavigateUrl = "~/CollectionSpecialistMaint.aspx";

                RadMenuItem maintItem5 = new RadMenuItem();
                maintItem5.Text = "Onboarding Phases";
                maintItem5.NavigateUrl = "~/OnboardingPhaseMaintenance.aspx";
                repItem.Items.Add(maintItem5);
                RadMenuItem maintItem21 = new RadMenuItem();
                maintItem21.Text = "EDI Onboarding Phases";
                maintItem21.NavigateUrl = "~/EDIOnboardingPhaseMaint.aspx";
                repItem.Items.Add(maintItem21);
                RadMenuItem maintItemRT = new RadMenuItem();
                maintItemRT.Text = "Request Types";
                maintItemRT.NavigateUrl = "~/RequestTypeMaintenance.aspx";
                repItem.Items.Add(maintItemRT);
                RadMenuItem maintItem6 = new RadMenuItem();
                maintItem6.Text = "Shipping Channels";
                maintItem6.NavigateUrl = "~/ShippingChannelMaintenance.aspx";
                repItem.Items.Add(maintItem6);
                RadMenuItem maintItem7 = new RadMenuItem();
                maintItem7.Text = "Shipping Products";
                maintItem7.NavigateUrl = "~/ShippingProductMaintenance.aspx";
                repItem.Items.Add(maintItem7);
                RadMenuItem maintItem8 = new RadMenuItem();
                maintItem8.Text = "Shipping Services";
                maintItem8.NavigateUrl = "~/ShippingServicesMaintenance.aspx";
                repItem.Items.Add(maintItem8);
                RadMenuItem maintItemSV = new RadMenuItem();
                maintItemSV.Text = "Shipping Vendors";
                maintItemSV.NavigateUrl = "~/ShippingVendorMaintenance.aspx";
                repItem.Items.Add(maintItemSV);
                //RadMenuItem maintItemStatusCodes = new RadMenuItem();
                //maintItemStatusCodes.Text = "Status Codes";
                //maintItemStatusCodes.NavigateUrl = "~/StatusCodesMaintenance.aspx";
                //repItem.Items.Add(maintItemStatusCodes);
                RadMenuItem maintItemStatusCodesNonCourierEDI = new RadMenuItem();
                maintItemStatusCodesNonCourierEDI.Text = "Status Codes Non Courier EDI";
                maintItemStatusCodesNonCourierEDI.NavigateUrl = "~/StatusCodesNonCourierEDIMaint.aspx";
                repItem.Items.Add(maintItemStatusCodesNonCourierEDI);
                RadMenuItem maintItemStatusCodesCourierEDI = new RadMenuItem();
                maintItemStatusCodesCourierEDI.Text = "Status Codes Courier EDI";
                maintItemStatusCodesCourierEDI.NavigateUrl = "~/StatusCodesCourierEDIMaint.aspx";
                repItem.Items.Add(maintItemStatusCodesCourierEDI);

                RadMenuItem maintItem1 = new RadMenuItem();
                maintItem1.Text = "Task Types";
                maintItem1.NavigateUrl = "~/TaskTypeMaintenance.aspx";
                repItem.Items.Add(maintItem1);
                RadMenuItem maintItem9 = new RadMenuItem();
                maintItem9.Text = "Third Party Vendors";
                maintItem9.NavigateUrl = "~/ThirdPartyVendorMaintenance.aspx";
                repItem.Items.Add(maintItem9);

                RadMenuItem maintItemVT = new RadMenuItem();
                maintItemVT.Text = "Vendor Types";
                maintItemVT.NavigateUrl = "~/VendorTypeMaintenance.aspx";
                repItem.Items.Add(maintItemVT);

                RadMenuItem maintItemFA = new RadMenuItem();
                maintItemFA.Text = "Freight Auditor";
                maintItemFA.NavigateUrl = "~/FreightAuditMaintenance.aspx";
                repItem.Items.Add(maintItemFA);

                RadMenuItem sep = new RadMenuItem();
                sep.IsSeparator = true;

                //EDI Items
                RadMenuItem maintItemConType = new RadMenuItem();
                maintItemConType.Text = "Contact Type";
                maintItemConType.NavigateUrl = "~/ContactTypeMaintenance.aspx";
                
                repItem.Items.Add(sep);
                repItem.Items.Add(maintItemConType);

                //USER Maint
                RadMenuItem maintItem15 = new RadMenuItem();
                maintItem15.Text = "User Maintenance";
                maintItem15.NavigateUrl = "~/UserSecurity.aspx";                               
                repItem.Items.Add(sep);
                repItem.Items.Add(maintItem13);
                repItem.Items.Add(maintItem15);
                repItem.Items.Add(maintItem18);
                repItem.Items.Add(maintItem19);
                repItem.Items.Add(maintItem20);
            }

        }

        string menuName = Request.Url.PathAndQuery;
        menuName = menuName + ".aspx";
        RadMenuItem currentItem = RadMenu1.FindItemByUrl(menuName);

        if (currentItem != null)
        {
            //Select the current item and his parents
            currentItem.HighlightPath();
        }
        else
            RadMenu1.Items[0].HighlightPath();

    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }
}
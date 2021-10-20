<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DiscoveryRequestForm.aspx.cs" Inherits="DiscoveryRequestForm2" %>
<%@ Register Src="EDI210.ascx" TagName="EDI210" TagPrefix="uc1" %>
<%@ Register Src="EDI214.ascx" TagName="EDI214" TagPrefix="uc2" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" src="~/Scripts/telerikEditorScript.js"></script>
    <%--<style type="text/css">
        table, th, td {
            border: 1px solid black;
        }
    </style>--%>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" >

            function submitCallBackFn(arg) {
                window.location.href = "Home.aspx";F
            } 		
            function profileCallBackFn(arg) {
            }
            function notesCallBackFn(arg) {
            }
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToWordButton") >= 0 ||
                    args.get_eventTarget().indexOf("ExportToCsvButton") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function OpenWin(path) {
                window.open(path);
                return false;
            }
            var $ = $telerik.$;

            function onClientFileUploaded(radAsyncUpload, args) {
                var $row = $(args.get_row());
                var inputName = radAsyncUpload.getAdditionalFieldID("TextBox");
                var inputType = "text";
                var inputID = inputName;
                var input = createInput(inputType, inputID, inputName);
                var label = createLabel(inputID);
                $row.append("<br/>");
                $row.append(label);
                $row.append(input);
            }

            function createInput(inputType, inputID, inputName) {
                var input = '<input type="' + inputType + '" id="' + inputID + '" name="' + inputName + '" />';
                return input;
            }

            function createLabel(forArrt) {
                var label = '<label for=' + forArrt + '>Enter Description: </label>';
                return label;
            }
        </script>
        <style type="text/css">
            td.Spacer {
                width: 100px;
            }

            td.SpacerNonCourier {
                width: 20px;
            }
        </style>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rdCall1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnAddDate1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAddDate1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdCall2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdCall2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="btnAddDate2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAddDate2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdCall3" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rddlRequestType">
                <UpdatedControls>
                    <%--<telerik:AjaxUpdatedControl ControlID="rddlSolutionType" />--%>
                    <telerik:AjaxUpdatedControl ControlID="rddlRequestType" />
                    <telerik:AjaxUpdatedControl ControlID="rddlRelationships" />
                    <telerik:AjaxUpdatedControl ControlID="txtCustomerName" />
                    <telerik:AjaxUpdatedControl ControlID="lblCustomerName" />
                    <telerik:AjaxUpdatedControl ControlID="rddlShippingVendor" />
                    <telerik:AjaxUpdatedControl ControlID="rddlCurrentCA" />
                    <telerik:AjaxUpdatedControl ControlID="rddlContactType" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtCustomerZip">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtCustomerCity" />
                    <telerik:AjaxUpdatedControl ControlID="txtCustomerState" />
                    <telerik:AjaxUpdatedControl ControlID="txtCustomerCountry" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="dpNoteDate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dpNoteDate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdTarget">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdTarget" />
                    <telerik:AjaxUpdatedControl ControlID="lblChangeReason" />
                    <telerik:AjaxUpdatedControl ControlID="txtTargetChangeReason" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rdActual">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rdActual" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSaveNotes">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgNotesGrid" />
                    <telerik:AjaxUpdatedControl ControlID="txtNotes" />
                    <telerik:AjaxUpdatedControl ControlID="txtInternalTimeSpent" />
                    <telerik:AjaxUpdatedControl ControlID="rddlInternalType" />
                    <telerik:AjaxUpdatedControl ControlID="lblTimeWarning" />
                    <telerik:AjaxUpdatedControl ControlID="lblTime" />
                    <telerik:AjaxUpdatedControl ControlID="lblTotalTime" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgNotesGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgNotesGrid" />
                    <telerik:AjaxUpdatedControl ControlID="lblTotalTime" />
                    <telerik:AjaxUpdatedControl ControlID="rddlInternalTypeEdit" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSaveUpload">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgUpload" />
                    <telerik:AjaxUpdatedControl ControlID="RadAsyncUpload1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgUpload">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgUpload" />
                    <telerik:AjaxUpdatedControl ControlID="RadAsyncUpload1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAddSvc">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSvcGrid" />
                    <telerik:AjaxUpdatedControl ControlID="rddlService" />
                    <telerik:AjaxUpdatedControl ControlID="txtVolume" />
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnAddProduct">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgProductGrid" />
                    <telerik:AjaxUpdatedControl ControlID="rddlProducts" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rddlDistrict">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rddlBranch" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="txtSalesProfessional">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtSalesProfessional" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbxWPK">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cbxCustom" />
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbxEquipment">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cbxEquipment" />
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbxCustom">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSvcGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cbx3pv">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgSvcGrid" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="chkStrategic">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rgSvcGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanelBar1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rddlPPSTInduction">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="courierIAddress" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSubmit">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CustomValidatorNew" />
                    <telerik:AjaxUpdatedControl ControlID="btnSubmit" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rddlShippingVendor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rddlShippingVendor" />
                    <telerik:AjaxUpdatedControl ControlID="lblVendorName" />
                    <telerik:AjaxUpdatedControl ControlID="txtCurrentVendor" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rddlCurrentCA">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rddlShippingVendor" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rddlInternalType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="note1ast" />
                    <telerik:AjaxUpdatedControl ControlID="note2ast" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rddlCustomsList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblCustomsBroker" />
                    <telerik:AjaxUpdatedControl ControlID="rddlCustomsBroker" />
                    <telerik:AjaxUpdatedControl ControlID="lblOtherBroker" />
                    <telerik:AjaxUpdatedControl ControlID="txtCustomsBroker" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rddlCustomsBroker">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lblOtherBroker" />
                    <telerik:AjaxUpdatedControl ControlID="txtCustomsBroker" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="rddlVendorType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rddlThirdPartyVendor" />
                    <telerik:AjaxUpdatedControl ControlID="lbl3pv" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="rddlSolutionType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rddlSolutionType" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="rddlContactType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rddlContactType" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="edi">
                <UpdatedControls>
                    <%--<telerik:AjaxUpdatedControl ControlID="gridShipmentMethods" />--%>
                    <telerik:AjaxUpdatedControl ControlID="gridEDITransactions" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--  Courier EDI Tab  --%>
            <telerik:AjaxSetting AjaxControlID="courierEDI">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridEDI210Accounts" />
                    <telerik:AjaxUpdatedControl ControlID="gridEDI214Accounts" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--  Non-Courier EDI Tab  --%>
            <telerik:AjaxSetting AjaxControlID="noncourierEDI">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridNonCourierEDI210Accounts" />
                    <telerik:AjaxUpdatedControl ControlID="gridNonCourierEDI214Accounts" />
                    <telerik:AjaxUpdatedControl ControlID="gridNonCourierPuroPostAccounts" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>


    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="40">
        <asp:Image ID="imgLoading" Style="margin-top: 90px" runat="server" ImageUrl="~/Images/Loading.gif" Height="50px"
            BorderWidth="0px" AlternateText="Loading" />
    </telerik:RadAjaxLoadingPanel>
    <div style="width: 853px">
        <asp:Panel ID="pnlsuccess" runat="server" Visible="false">
            <div class="alert alert-success" role="alert">
                <asp:Label ID="lblSuccess" CssClass="alert-link" runat="server"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlInfo" runat="server" Visible="false">
            <div class="alert alert-info" role="alert">
                <asp:Label ID="lblInfo" CssClass="alert-link" runat="server"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlwarning" runat="server" Visible="false">
            <div class="alert alert-warning" role="alert">
                <asp:Label ID="lblWarning" CssClass="alert-link" runat="server"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlDanger" runat="server" Visible="false">
            <div class="alert alert-danger" role="alert">
                <asp:Label ID="lblDanger" runat="server" CssClass="alert-link"></asp:Label>
            </div>
        </asp:Panel>
    </div>
    <%-- <div style="width: 1120px">--%>
    <div style="width: 100%">
        <asp:HiddenField id="hiddenShowAuditorPortal" runat="server"/>
        <table style="padding-top: 2px; width: 100%;" border="0">
            <tr>
                <td style="color: #4b6c9e; width: 20%; font-size: medium; text-align: right;"></td>
                <td style="width: 1%"></td>
                <td style="color: #4b6c9e; width: 40%; font-size: large; text-align: left;">
                    <asp:Label ID="lblDRF" runat="server" Text="Discovery Request Form" Font-Bold="true"></asp:Label>
                </td>
                <td style="color: #4b6c9e; text-align: right; width: 20%">
                    <i>Submitted By: </i>
                </td>
                <td style="width: 1%"></td>
                <td>
                    <asp:Label ID="lblSubmittedBy" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">Sales Professional 
                </td>
                <td style="color: red; width: 1%; text-align: left">*</td>
                <td>
                    <telerik:RadTextBox ID="txtSalesProfessional" runat="server" MaxLength="75" Width="250px" ToolTip="Enter Your Email Address" />
                </td>
                <td style="color: #4b6c9e; text-align: right;">
                    <i>Last Updated By: </i>
                </td>
                <td style="width: 1%"></td>
                <td>
                    <asp:Label ID="lblUpdatedBy" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">District 
                </td>
                <td style="color: red; width: 1%; text-align: left">*</td>
                <td>
                    <telerik:RadDropDownList ID="rddlDistrict" runat="server" DefaultMessage="Select District" ToolTip="Select Your District" OnSelectedIndexChanged="rddlDistrict_IndexChanged" AutoPostBack="true"></telerik:RadDropDownList>
                </td>
                <td style="color: #4b6c9e; text-align: right;">
                    <i>Last Updated On: </i>
                </td>
                <td></td>
                <td>
                    <asp:Label ID="lblUpdatedOn" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">Branch
                </td>
                <td style="color: red; width: 1%; text-align: left">*</td>
                <td>
                    <telerik:RadDropDownList ID="rddlBranch" runat="server" DefaultMessage="Select Branch" ToolTip="Select Branch"></telerik:RadDropDownList>
                </td>
                <td style="color: #4b6c9e; text-align: right;">
                    <asp:Label ID="lblReqID" runat="server" Text="Request ID:" Visible="false"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblRequestID" runat="server" Text="0" Visible="false"></asp:Label></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: right"></td>
                <td style="color: red; width: 1%; text-align: left"></td>
                <td>Strategic?&nbsp;
                    <telerik:RadButton ID="chkStrategic" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false" Visible="false">
                        <ToggleStates>
                            <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                            <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />
                        </ToggleStates>
                    </telerik:RadButton>
                    <telerik:RadDropDownList ID="rddlStrategic" runat="server" Visible="true" Width="90px">
                        <Items>
                            <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                            <telerik:DropDownListItem Value="yes" Text="Yes" />
                        </Items>
                    </telerik:RadDropDownList>
                </td>
                <td style="color: #4b6c9e; text-align: right;"></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td style="text-align: right">Sales Professional Email
                </td>
                <td style="color: red; width: 1%; text-align: left">*</td>
                <td>
                    <telerik:RadTextBox ID="txtEmail" runat="server" MaxLength="75" Width="250px" ToolTip="Enter Sales Professional" />
                </td>
                <td style="color: #4b6c9e; text-align: right;">
                    <telerik:RadButton ID="btnSubmitChanges" CausesValidation="true" ValidationGroup="submitChangesButton" runat="server" Text="Save Changes" OnClick="btnSubmitChanges_Click" AutoPostBack="true" Enabled="true" />
                </td>

                <td colspan="2">
                    <telerik:RadButton ID="btnExit1" CausesValidation="true" runat="server" Text="Exit" OnClick="btnCancel_Click" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td style="color: #4b6c9e; text-align: right;"></td>
                <td></td>
                <td style="color: #4b6c9e; text-align: left;"></td>
                <td style="text-align: right"></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:CustomValidator runat="server" ID="CustomValidator" ValidationGroup="submitChangesButton" OnServerValidate="CustomValidator_ServerValidate" Style="color: red"></asp:CustomValidator><br />
                </td>
            </tr>
        </table>
        <telerik:RadTabStrip RenderMode="Lightweight" runat="server" ID="RadTabStrip1" MultiPageID="RadMultiPage1" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab runat="server" Text="Customer Info" Width="190px" PageViewID="customer"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Contact Info" Width="190px" PageViewID="contact"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Current Solution" Width="150px" PageViewID="current"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="EDI Services" Width="150px" PageViewID="edi"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Shipping Services" Width="150px" PageViewID="services"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Profile" Width="140px" PageViewID="profile"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Courier EDI" Width="140px" PageViewID="courierEDI"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Non-Courier EDI" Width="140px" PageViewID="noncourierEDI"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="Add'l Notes" Width="150px" PageViewID="notes"></telerik:RadTab>
                <telerik:RadTab runat="server" Text="File Uploads" Width="140px" PageViewID="uploads"></telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        
        <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0">
            <%--  CUSTOMER INFORMATION  --%>
            <telerik:RadPageView runat="server" ID="customer">
                <hr />
                <table border="0">
                    <tr>
                        <td>
                            <telerik:RadButton ID="chkNewBus" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="true" OnClick="chkNewBus_Click" Visible="false">
                                <ToggleStates>
                                    <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                    <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />
                                </ToggleStates>
                            </telerik:RadButton>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top">
                            <asp:Label ID="Label4" runat="server" Text="Solution Type"></asp:Label>
                        </td>
                        <td style="color: red; width: 1%; text-align: left; vertical-align: top">
                            <asp:Label ID="Label5" runat="server" Text="*"></asp:Label></td>
                        <td>
                            <telerik:RadDropDownList ID="rddlSolutionType" runat="server" DefaultMessage="Select Solution Type" AutoPostBack="true" ToolTip="Select Your Solution Type" Visible="true" Width="250px" OnSelectedIndexChanged="rddlSolutionType_IndexChanged">
                            </telerik:RadDropDownList>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="custInfo" ControlToValidate="rddlSolutionType" ErrorMessage="Solution Type is required" Style="color: red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top">
                            <asp:Label ID="Label2" runat="server" Text="Request Type"></asp:Label>
                        </td>
                        <td style="color: red; width: 1%; text-align: left; vertical-align: top">
                            <asp:Label ID="Label3" runat="server" Text="*"></asp:Label></td>
                        <td>
                            <telerik:RadDropDownList ID="rddlRequestType" runat="server" DefaultMessage="Select Request Type" AutoPostBack="true" OnSelectedIndexChanged="rddlRequestType_IndexChanged" ToolTip="Select Your Request Type" Visible="true" Width="250px">
                            </telerik:RadDropDownList>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="custInfo" ControlToValidate="rddlRequestType" ErrorMessage="Request Type is required" Style="color: red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top">
                            <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"></asp:Label>
                        </td>
                        <td style="color: red; width: 1%; text-align: left; vertical-align: top">
                            <asp:Label ID="lblCustNameStar" runat="server" Text="*"></asp:Label></td>
                        <td>
                            <telerik:RadTextBox ID="txtCustomerName" runat="server" MaxLength="75" Text="" Width="250px" ToolTip="Enter Customer Name" Style="text-transform: uppercase;" />
                            <telerik:RadComboBox ID="rddlRelationships" runat="server" Filter="StartsWith" EmptyMessage="Select Relationship Name" ToolTip="Select Your Relationship Name" Visible="false" Width="250px"></telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Address                     
                        </td>
                        <td style="color: red; width: 1%; text-align: left">*</td>
                        <td>
                            <telerik:RadTextBox ID="txtCustomerAddress" runat="server" MaxLength="75" Text="" Width="250px" ToolTip="Enter Customer Address" Style="text-transform: uppercase;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Zip                     
                        </td>
                        <td style="color: red; width: 1%; text-align: left">*</td>
                        <td>
                            <telerik:RadTextBox ID="txtCustomerZip" runat="server" MaxLength="75" Text="" Width="75px" ToolTip="Enter Customer Zip" AutoPostBack="true" OnTextChanged="txtCustomerZip_TextChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">City                     
                        </td>
                        <td style="color: red; width: 1%; text-align: left">*</td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadTextBox ID="txtCustomerCity" runat="server" MaxLength="75" Text="" Width="150px" ToolTip="Enter City" Style="text-transform: uppercase;" />
                                    </td>
                                    <td></td>
                                    <td>State
                                    </td>
                                    <td style="color: red; width: 1%; text-align: left">*</td>
                                    <td>
                                        <telerik:RadTextBox ID="txtCustomerState" runat="server" MaxLength="75" Text="" Width="50px" ToolTip="Enter State" Style="text-transform: uppercase;" />
                                    </td>
                                    <td style="width: 15px"></td>
                                    <td>Country 
                                    </td>
                                    <td style="color: red; width: 1%; text-align: left">*</td>
                                    <td>
                                        <telerik:RadTextBox ID="txtCustomerCountry" runat="server" MaxLength="75" Text="USA" Width="100px" ToolTip="Enter Country" Style="text-transform: uppercase;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Annualized Revenue                     
                        </td>
                        <td style="color: red; width: 1%; text-align: left">*</td>
                        <td>
                            <telerik:RadTextBox ID="txtRevenue" runat="server" MaxLength="50" Text='' Width="125px" ToolTip="Enter Annualized Revenue" />
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="custInfo" ControlToValidate="txtRevenue" ErrorMessage="Annualized Revenue is required" Style="color: red"></asp:RequiredFieldValidator><br />

                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 20px;"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Commodity            
                        </td>
                        <td style="color: red; width: 1%; text-align: left">*</td>
                        <td>
                            <telerik:RadTextBox ID="txtCommodity" runat="server" MaxLength="175" Text='' Width="285px" ToolTip="Enter Commodity" />
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="custInfo" ControlToValidate="txtCommodity" ErrorMessage="Commodity is required" Style="color: red"></asp:RequiredFieldValidator><br />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">Website            
                        </td>
                        <td style="color: red; width: 1%; text-align: left"></td>
                        <td>
                            <telerik:RadTextBox ID="txtWebsite" runat="server" MaxLength="175" Text='' Width="285px" ToolTip="Enter Website" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 30px"></td>
                    </tr>
                </table>
                <table style="padding-top: 2px; width: 100%;" border="0">
                    <tr>
                        <td>
                            <p style="color: red"><i>*Required Fields</i></p>
                        </td>
                        <td style="width: 20%; text-align: left;">
                            <asp:CustomValidator runat="server" ID="CustomValidatorCustomer" ValidationGroup="custInfo" OnServerValidate="CustomValidatorCustomer_ServerValidate" Style="color: red"></asp:CustomValidator><br />
                            <td style="color: blue; width: 60%; font-size: medium; text-align: right;">
                                <telerik:RadButton RenderMode="Lightweight" CausesValidation="true" ValidationGroup="custInfo" ID="btnNextTab1" runat="server" Text="Next" OnClick="btnNextTab1_Click"></telerik:RadButton>
                            </td>
                    </tr>
                </table>
            </telerik:RadPageView>

            <%--  CONTACT INFORMATION  --%>
            <telerik:RadPageView runat="server" ID="contact">
                <hr />
                <table style="padding-top: 2px; width: 100%;" border="0">
                    <tr>
                        <td colspan="2">
                            <telerik:RadGrid ID="contactGrid" runat="server" AllowPaging="True" 
                                AllowSorting="false" AllowFilteringByColumn="false" 
                                AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false" 
                                OnNeedDataSource="contactGrid_NeedDataSource" OnDeleteCommand="contactGrid_DeleteCommand"
                                OnItemDataBound="contactGrid_ItemDataBound"  OnUpdateCommand="contactGrid_UpdateCommand"
                                OnItemCommand="contactGrid_ItemCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="idContact" CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                                            <HeaderStyle Width="36px"></HeaderStyle>
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridBoundColumn DataField="ContactTypeName" FilterControlAltText="ContactTypeName" SortExpression="Team" HeaderText="Contact Type" UniqueName="ContactTypeName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="idContact" DataType="System.Int32" HeaderText="OrderID" ReadOnly="True" UniqueName="idContact" Display="false"  >
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Name" FilterControlAltText="Filter Name column" SortExpression="Name" HeaderText="Name" UniqueName="Name" >
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Title"  FilterControlAltText="Title" SortExpression="Title" HeaderText="Title" UniqueName="Title">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Phone"  FilterControlAltText="Phone" SortExpression="Phone" HeaderText="Phone" UniqueName="Phone">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Email"  FilterControlAltText="Email" SortExpression="Email" HeaderText="Email" UniqueName="Email">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <EditColumn UniqueName="EditColumn"></EditColumn>
                                        <FormTemplate>
                                            <table id="Table2" style="padding-top: 2px; width: 100%; margin-left:50px" border="0">
                                                <tr>
                                                    <td style="text-align: right; width: 100px;vertical-align: top">Contact Type</td>
                                                    <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                    <td>
                                                        <telerik:RadDropDownList ID="radListContactType" runat="server" OnSelectedIndexChanged="radListContactTypeIdxChanged" DefaultMessage="Select Contact Type" AutoPostBack="true" ToolTip="Select Your Contact Type" Visible="true" Width="280px">
                                                        </telerik:RadDropDownList>
                                                    </td>
                                                    <td style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 100px">Contact Name:</td>
                                                    <td style="color: red; text-align: left; width: 2px">*</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtBxContactName2" autocomplete="chrome-off" runat="server" MaxLength="75" Text='' Width="300px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 100px">Contact Title:</td>
                                                    <td style="color: red; text-align: left; width: 2px"></td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtBxContactTitle2" autocomplete="chrome-off" runat="server" MaxLength="75" Text='' Width="300px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 100px">Contact Email:</td>
                                                    <td style="color: red; text-align: left; width: 2px">*</td>
                                                    <td style="width: 160px">
                                                        <telerik:RadTextBox ID="txtBxContactEmail2" autocomplete="chrome-off" runat="server" MaxLength="75" Text='' Width="300px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 100px">Contact Phone:</td>
                                                    <td style="color: red; text-align: left; width: 2px">*</td>
                                                    <td style="width: 160px">
                                                        <telerik:RadTextBox ID="txtBxContactPhone2" autocomplete="chrome-off" runat="server" MaxLength="75" Text='' Width="300px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;"></td>
                                                    <td style="width: 2px;"></td>
                                                    <td align="center" ">
                                                        <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                        </telerik:RadButton>
                                                        &nbsp;
                                                    <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                        CommandName="Cancel">
                                                    </telerik:RadButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7">
                            <p style="color: red"><i>*Required Fields - At least one contact must be supplied</i></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="text-align: left;">
                            <asp:CustomValidator runat="server" ID="CustomValidatorContact" ValidationGroup="contactInfo" OnServerValidate="CustomValidatorContact_ServerValidate" Style="color: red"></asp:CustomValidator><br />
                        </td>
                        <td style="text-align: right;">
                            <telerik:RadButton RenderMode="Lightweight" CausesValidation="true" ValidationGroup="contactInfo" ID="btnNextTab2" runat="server" Text="Next" AutoPostBack="true" OnClick="btnNextTab2_Click"></telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </telerik:RadPageView>

            <%--  CURRENT SOLUTION  --%>
            <telerik:RadPageView runat="server" ID="current">
                <hr />
                <table border="0">
                    <tr>
                        <td colspan="5" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td><%--Currently Shipping to Canada?--%></td>
                        <td></td>
                        <td>
                            <telerik:RadDropDownList ID="rddlCurrentCA" runat="server" OnSelectedIndexChanged="rddlCurrentCA_IndexChanged" AutoPostBack="true" Visible="false" Width="90px">
                                <Items>
                                    <telerik:DropDownListItem Value="no" Text="No" />
                                    <telerik:DropDownListItem Value="yes" Text="Yes" Selected="true" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr>
                        <td>
                            <%-- Current Shipping Vendor--%>
                        </td>
                        <td style="color: red; text-align: left"></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <telerik:RadDropDownList ID="rddlShippingVendor" runat="server" DefaultMessage="Select Current Vendor" OnSelectedIndexChanged="rddlShippingVendor_IndexChanged" AutoPostBack="true" ToolTip="Select Current Vendor" Visible="false" Width="200px"></telerik:RadDropDownList>

                                    </td>
                                    <td>
                                        <asp:Label ID="lblVendorName" runat="server" Text="Enter Vendor Name" Visible="false"></asp:Label></td>
                                    <td>
                                        <telerik:RadTextBox ID="txtCurrentVendor" Width="200px" runat="server" Visible="false" />

                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>Current Shipping Solution
                        </td>
                        <td style="color: red; text-align: left">*</td>
                        <td>
                            <asp:TextBox ID="txtareaCurrentSolution" TextMode="multiline" Columns="100" Rows="5" runat="server" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="currentInfo" ControlToValidate="rddlShippingVendor" ErrorMessage="Current Vendor is required." Style="color: red"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="currentInfo" ControlToValidate="txtareaCurrentSolution" ErrorMessage="Current Solution is required." Style="color: red"></asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <hr />
                <table style="padding-top: 2px; width: 100%;" border="0">
                    <tr>
                        <td>
                            <p style="color: red"><i>*Required Fields</i></p>
                        </td>
                        <td style="color: blue; width: 20%; font-size: medium; text-align: right;">
                            <telerik:RadButton RenderMode="Lightweight" CausesValidation="true" ValidationGroup="currentInfo" ID="btnNextTab3" runat="server" Text="Next" OnClick="btnNextTab3_Click"></telerik:RadButton>

                        </td>
                    </tr>
                </table>
            </telerik:RadPageView>

            <%--  EDI Service  --%>
            <telerik:RadPageView runat="server" ID="edi">
                <hr />
                <table border="0">
                    <tr>
                        <td colspan="5" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td style="color: red; text-align: right">*</td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Ship methods in scope for EDI - Add all that apply" Visible="true"></asp:Label>
                        </td>
                        <td style="color: red; text-align: left"></td>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="*" Visible="true" style="color: red; text-align: right"></asp:Label>
                            <asp:Label ID="Label8" runat="server" Text="Does customer use a freight auditor?" Visible="true"></asp:Label>
                            &nbsp;
                            <telerik:RadDropDownList ID="comboxFreightAuditor" runat="server" Visible="true" Width="70px">
                                <Items>
                                    <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                                    <telerik:DropDownListItem Value="yes" Text="Yes" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadGrid ID="gridShipmentMethods" runat="server" AllowPaging="True" 
                                AllowSorting="false" AllowFilteringByColumn="false" 
                                AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false" 
                                OnNeedDataSource="gridShipmentMethods_NeedDataSource" OnDeleteCommand="gridShipmentMethods_DeleteCommand"
                                OnItemDataBound="gridShipmentMethods_ItemDataBound"  OnUpdateCommand="gridShipmentMethods_UpdateCommand"
                                OnItemCommand="gridShipmentMethods_ItemCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="idEDIShipMethod" CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="MethodType" FilterControlAltText="MethodType" SortExpression="Team" HeaderText="Method Type" UniqueName="MethodType">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <EditColumn UniqueName="EditColumn"></EditColumn>
                                        <FormTemplate>
                                            <table id="Table2" style="padding-top: 2px; width: 100%; margin-left:50px" border="0">
                                                <tr>
                                                    <td style="text-align: right; width: 100px;vertical-align: top">Shipment Method</td>
                                                    <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                    <td>
                                                        <telerik:RadDropDownList ID="radListEDIShipMethod" runat="server" OnSelectedIndexChanged="radListEDIShipMethodIdxChanged" DefaultMessage="Select EDI Ship Method" AutoPostBack="true" ToolTip="Select Your Ship Method" Visible="true" Width="280px">
                                                        </telerik:RadDropDownList>
                                                    </td>
                                                    <td style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;"></td>
                                                    <td style="width: 2px;"></td>
                                                    <td align="center" >
                                                        <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                        </telerik:RadButton>
                                                        &nbsp;
                                                    <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                        CommandName="Cancel">
                                                    </telerik:RadButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td></td>
                        <td style="text-align:left">
                            <asp:Label ID="Label22" runat="server" Text="Auditor Name" Visible="true"></asp:Label>
                            <%--<telerik:RadDropDownList ID="cmbBoxFreightAuditor" runat="server" DefaultMessage="Select Freight Auditor" ToolTip="Select Freight Auditor" OnSelectedIndexChanged="rddlDistrict_IndexChanged" AutoPostBack="true"></telerik:RadDropDownList>--%>
                            <telerik:RadGrid ID="gridFreightAuditors" runat="server" AllowPaging="True" 
                                AllowSorting="false" AllowFilteringByColumn="false" 
                                AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false" 
                                OnNeedDataSource="gridFreightAuditors_NeedDataSource" OnDeleteCommand="gridFreightAuditors_DeleteCommand"
                                OnItemDataBound="gridFreightAuditors_ItemDataBound"  OnUpdateCommand="gridFreightAuditors_UpdateCommand"
                                OnItemCommand="gridFreightAuditors_ItemCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="idFreightAuditorDiscReq" CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="CompanyName" FilterControlAltText="CompanyName" SortExpression="Team" HeaderText="Company Name" UniqueName="CompanyName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <EditColumn UniqueName="EditColumn"></EditColumn>
                                        <FormTemplate>
                                            <table id="Table2" style="padding-top: 2px; width: 100%; margin-left:50px" border="0">
                                                <tr>
                                                    <td style="text-align: right; width: 100px;vertical-align: top">Company Name</td>
                                                    <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                    <td>
                                                        <telerik:RadDropDownList ID="radListCompanyName" runat="server"  DefaultMessage="Select Company Name" AutoPostBack="true" ToolTip="Select Company Name" Visible="true" Width="280px">
                                                        </telerik:RadDropDownList>
                                                    </td>
                                                    <td style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;"></td>
                                                    <td style="width: 2px;"></td>
                                                    <td align="center" >
                                                        <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                        </telerik:RadButton>
                                                        &nbsp;
                                                    <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                        CommandName="Cancel">
                                                    </telerik:RadButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <br />
                        </td>
                    </tr>
                    <tr>
                        <td style="color: red; text-align: right">*</td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="EDI Solution Requested - Add all that apply" Visible="true"></asp:Label></td>
                        <td></td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Customer EDI Details" Visible="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadGrid ID="gridEDITransactions" runat="server" AllowPaging="True" 
                                AllowSorting="false" AllowFilteringByColumn="false" 
                                AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false" 
                                OnNeedDataSource="gridEDITransactions_NeedDataSource" OnDeleteCommand="gridEDITransactions_DeleteCommand"
                                OnItemDataBound="gridEDITransactions_ItemDataBound"  OnUpdateCommand="gridEDITransactions_UpdateCommand"
                                OnItemCommand="gridEDITransactions_ItemCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="idEDITranscation" CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="EDITranscationType" FilterControlAltText="EDITranscationType" SortExpression="Team" HeaderText="EDI Solution Type" UniqueName="EDITranscationType">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <EditColumn UniqueName="EditColumn"></EditColumn>
                                        <FormTemplate>
                                            <table id="Table2" style="padding-top: 2px; width: 100%; margin-left:50px" border="0">
                                                <tr>
                                                    <td style="text-align: right; width: 100px;vertical-align: top">Solution Requested</td>
                                                    <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                    <td>
                                                        <telerik:RadDropDownList ID="radListEDITransList" runat="server" OnSelectedIndexChanged="radListEDITransIdxChanged" DefaultMessage="Select EDI Solution Req" AutoPostBack="true" ToolTip="Select Your Ship Method" Visible="true" Width="280px">
                                                        </telerik:RadDropDownList>
                                                    </td>
                                                    <td style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;"></td>
                                                    <td style="width: 2px;"></td>
                                                    <td align="center" >
                                                        <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                        </telerik:RadButton>
                                                        &nbsp;
                                                    <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                        CommandName="Cancel">
                                                    </telerik:RadButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td width="5%">
                            
                        </td>
                        <td>
                            <asp:TextBox ID="txtBxCustomerEDIDetails" TextMode="multiline" Columns="100" Rows="5" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                </table>
               <%-- <table border="0">
                    <tr>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="RadAsynEDIServicesUpload" HideFileInput="true"
                                AllowedFileExtensions=".jpeg,.jpg,.png,.doc,.docx,.xls,.xlsx,.pdf,.txt,.gif,.csv,.msg" OnFileUploaded="RadAsyncUpload1_FileUploaded" TargetFolder="~/FileUploads/EDIServices" Localization-Select="Browse"
                                OnClientFileUploaded="onClientFileUploaded" />
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Supply EDI documentation received, e.g. Specification documents, Letter of Authorization, etc." Visible="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            <telerik:RadButton ID="btnEDISerivesSaveFile"  Text="Save Files" runat="server" OnClick="btnSaveEDIServicesUpload_Click" Visible="true"></telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadGrid ID="gridEDIServicesUpload" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowFilteringByColumn="true" 
                                OnNeedDataSource="gridEDIServiesUpload_NeedDataSource" 
                                OnUpdateCommand="rgUpload_UpdateCommand" 
                                OnItemDataBound="rgUpload_ItemDataBound">
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
                                </ExportSettings>
                                <MasterTableView AutoGenerateColumns="False" AllowPaging="True" CommandItemDisplay="Top" EditMode="PopUp">
                                    <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToWordButton="False" ShowExportToExcelButton="False"></CommandItemSettings>
                                    <EditFormSettings UserControlName="EditFileUpload.ascx" EditFormType="WebUserControl" PopUpSettings-Height="250px" PopUpSettings-Width="550px">
                                        <FormStyle Height="500px"></FormStyle>
                                        <PopUpSettings Modal="true" />
                                    </EditFormSettings>
                                    <Columns>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="EditLink">
                                            <HeaderStyle Width="36px"></HeaderStyle>
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridBoundColumn DataField="idFileUpload" FilterControlAltText="Filter idFileUpload column" HeaderText="idFileUpload" SortExpression="idFileUpload" UniqueName="idFileUpload" Visible="true" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="idRequest" FilterControlAltText="Filter idRequest column" HeaderText="idRequest" SortExpression="idRequest" UniqueName="idRequest" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UploadDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" FilterControlAltText="Filter UploadDate column" HeaderText="Upload Date" SortExpression="UploadDate" UniqueName="UploadDate" HeaderStyle-Width="10%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Createdby" AllowFiltering="true" FilterControlAltText="Filter Createdby column" HeaderText="Uploaded By" SortExpression="Createdby" UniqueName="Createdby" HeaderStyle-Width="10%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Description" AllowFiltering="true" FilterControlAltText="Filter Description column" HeaderText="Description" SortExpression="Description" UniqueName="Description">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ActiveFlag" AllowFiltering="true" FilterControlAltText="Filter ActiveFlag column" HeaderText="ActiveFlag" SortExpression="ActiveFlag" UniqueName="ActiveFlag" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridHyperLinkColumn DataTextField="FilePath" Target="_parent" NavigateUrl="javascript:void(0);"
                                            AllowFiltering="true" FilterControlAltText="Filter FilePath column" HeaderText="File Path" SortExpression="FilePath" UniqueName="FilePath" ItemStyle-ForeColor="Blue">
                                        </telerik:GridHyperLinkColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" HeaderText="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false" ConfirmDialogType="RadWindow" ConfirmText="Delete?" Visible="false">
                                            <HeaderStyle Width="36px"></HeaderStyle>
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>--%>
                <hr />
                <table style="padding-top: 2px; width: 100%;" border="0">
                    <tr>
                        <td>
                            <p style="color: red"><i>*Required Fields</i></p>
                        </td>
                        <td style="color: blue; width: 20%; font-size: medium; text-align: right;">
                            <telerik:RadButton ID="btnSubmitEDIServices" CausesValidation="true" ValidationGroup="submitChangesButton" runat="server" Text="Submit Request" OnClick="btnSubmit_Click" AutoPostBack="true" Enabled="false" />
                            <telerik:RadButton RenderMode="Lightweight" CausesValidation="true" ValidationGroup="currentInfo" ID="btnEDIServicesNext" runat="server" Text="Next" OnClick="btnNextTab4_Click"></telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
            </tr>
                </table>
            </telerik:RadPageView>

            <%--  SHIPPING SERVICES  --%>
            <telerik:RadPageView runat="server" ID="services">
                <hr />
                <table>
                    <tr>
                        <td style="height: 30px; text-align: right">Shipping Services - 
                        </td>
                        <td style="text-align: left">Add all that apply</td>
                        <td style="color: red; text-align: left"></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadDropDownList ID="rddlService" ValidationGroup="addSvcButton" runat="server" Width="150px" DefaultMessage="Add Shipping Service" ToolTip="Add Shipping Service"></telerik:RadDropDownList>
                        </td>
                        <td>Proposed Weekly Volume (pieces per week)
                        </td>
                        <td style="color: red; text-align: left">*</td>
                        <td>
                            <telerik:RadTextBox ID="txtVolume" ValidationGroup="addSvcButton" runat="server" Text="" Width="80px" ToolTip="Enter Proposed Volume" />
                        </td>

                        <td>
                            <telerik:RadButton ID="btnAddSvc" CausesValidation="true" ValidationGroup="addSvcButton" runat="server" Text="Add" OnClick="btnAddSvc_Click" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="addSvcButton" ControlToValidate="rddlService" ErrorMessage="Select Service" Style="color: red"></asp:RequiredFieldValidator>
                        </td>
                        <td colspan="3" style="color: red; text-align: right">
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="addSvcButton" ControlToValidate="txtVolume" ErrorMessage="Volume is required" Style="color: red"></asp:RequiredFieldValidator>

                        </td>
                        <td></td>
                    </tr>
                </table>
                <table border="0">
                    <tr>
                        <td style="width: 380px">
                            <telerik:RadGrid ID="rgSvcGrid" runat="server" CellSpacing="-1" GridLines="None" GroupPanelPosition="Top" AllowFilteringByColumn="false" OnNeedDataSource="rgSvcGrid_NeedDataSource" OnDeleteCommand="rgSvcGrid_DeleteCommand">
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
                                </ExportSettings>
                                <MasterTableView AutoGenerateColumns="False" AllowPaging="False" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToExcelButton="False"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="idService" HeaderText="ServiceID" UniqueName="ServiceID" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="serviceDesc" HeaderText="Shipping Service" UniqueName="Service">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Volume" HeaderText="Proposed Volume" UniqueName="Volume">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td style="width: 40px"></td>
                        <td>Customer Notes</td>
                        <td style="width: 10px"></td>
                        <td>
                            <asp:TextBox ID="txtProposedNotes" TextMode="multiline" Columns="70" Rows="5" runat="server" /></td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td colspan="6" style="height: 30px"></td>
                    </tr>
                    <tr>
                        <td>Customs Solution
                        </td>
                        <td style="color: red; text-align: left">*</td>
                        <td>
                            <telerik:RadDropDownList ID="rddlCustomsList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rddlCustomsList_IndexChanged" DefaultMessage="Select Customs Solution" ToolTip="Select Customs Solution" Visible="true"></telerik:RadDropDownList>
                        </td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblCustomsBroker" runat="server" Text="Customs Broker" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadDropDownList ID="rddlCustomsBroker" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rddlCustomsBroker_IndexChanged" DefaultMessage="Select Broker" ToolTip="Select Broker" Visible="false" Width="300px"></telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td colspan="3" style="text-align: right">
                            <asp:Label ID="lblOtherBroker" runat="server" Text="Enter Broker Name" Visible="false"></asp:Label></td>
                        <td>
                            <telerik:RadTextBox ID="txtCustomsBroker" runat="server" MaxLength="200" Text="" Width="550px" ToolTip="Enter Broker" Visible="false" /></td>
                    </tr>
                </table>

                <table border="0">
                    <tr>
                        <td colspan="7" style="height: 20px"></td>
                    </tr>
                    <tr>
                        <td>
                            <%--Potential Discovery Call Dates--%>
                        </td>
                        <td></td>
                        <td>
                            <telerik:RadDatePicker RenderMode="Lightweight" ID="rdCall1" runat="server" AutoPostBack="true" Enabled="true" OnSelectedDateChanged="date1_Changed" Visible="false"></telerik:RadDatePicker>
                        </td>
                        <td></td>
                        <td>
                            <telerik:RadButton ID="btnAddDate1" CausesValidation="true" runat="server" Text="Add Another Date" Enabled="false" OnClick="btnAddDate1_Click" Visible="false" />
                        </td>
                        <td></td>
                        <td>
                            <telerik:RadDatePicker RenderMode="Lightweight" ID="rdCall2" runat="server" AutoPostBack="true" Visible="false" OnSelectedDateChanged="date2_Changed"></telerik:RadDatePicker>
                        </td>
                        <td></td>
                        <td>
                            <telerik:RadButton ID="btnAddDate2" CausesValidation="true" runat="server" Text="Add Another Date" Enabled="false" Visible="false" OnClick="btnAddDate2_Click" />
                        </td>
                        <td></td>
                        <td>
                            <telerik:RadDatePicker RenderMode="Lightweight" ID="rdCall3" runat="server" AutoPostBack="true" Visible="false"></telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" style="height: 30px">
                            <asp:CustomValidator runat="server" ID="CustomValidatorNew" ValidationGroup="submitButton" OnServerValidate="CustomValidatorNew_ServerValidate" Style="color: red"></asp:CustomValidator><br />
                        </td>
                    </tr>
                </table>
                <p></p>
                <table style="padding-top: 2px; width: 100%;" border="0">
                    <tr>
                        <td>
                            <p style="color: red"><i>*Required Fields</i></p>
                        </td>
                        <td></td>
                        <td style="color: blue; width: 20%; font-size: medium; text-align: right;">
                            <telerik:RadButton ID="btnSubmit" CausesValidation="true" ValidationGroup="submitButton" runat="server" Text="Submit Request" OnClick="btnSubmit_Click" AutoPostBack="true" Enabled="true" />
                        </td>
                    </tr>
                </table>

            </telerik:RadPageView>

            <%--  PROFILE  --%>
            <telerik:RadPageView runat="server" ID="profile">
                <hr />
                <div style="width: 90%; margin-left: 5%">
                    <%--  SOLUTION SUMMARY  --%>
                    <telerik:RadPanelBar RenderMode="Lightweight" ID="RadPanelBar1" runat="server" Visible="true" Width="100%">
                        <Items>
                            <%--    Solution Summary    --%>
                            <telerik:RadPanelItem Text="Solution Summary" Expanded="True">
                                <ContentTemplate>
                                    <table border="0">
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 140px">ITBA Assigned
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlITBA" runat="server" DefaultMessage="Select Onboarding Specialist" ToolTip="Select Onboarding Specialist" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 15px"></td>
                                            <td style="width: 140px">
                                                <telerik:RadButton ID="cbxWPK" Text="WorldPak Customer?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false" OnClick="cbxWPK_Click" Visible="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                                Vendor Type
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadButton ID="cbx3pv" Text="3rd Party Vendor?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false" OnClick="cbx3pv_Click" Visible="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                                <telerik:RadDropDownList ID="rddlVendorType" runat="server" DefaultMessage="Select Vendor Type" ToolTip="Select Vendor Type" AutoPostBack="true" OnSelectedIndexChanged="rddlVendorType_IndexChanged" Visible="true">
                                                </telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 150px">
                                                <telerik:RadButton ID="cbxCustom" Text="Customized Solution?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false" Visible="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td>
                                                <telerik:RadButton ID="cbxDataScrub" Text="Data Scrubbing?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false" Visible="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td style="width: 15px"></td>
                                            <td>
                                                <asp:Label ID="lbl3pv" CssClass="alert-link" Text="Third Party Vendor" runat="server" Visible="false"></asp:Label></td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlThirdPartyVendor" runat="server" DefaultMessage="Select Vendor" ToolTip="Select Vendor" Visible="false"></telerik:RadDropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblClosel" CssClass="alert-link" Text="Select Closed Reason" runat="server" Visible="false"></asp:Label></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Target Go-Live Date
                                            </td>
                                            <td style="width: 25px"></td>
                                            <td>
                                                <telerik:RadDatePicker RenderMode="Lightweight" ID="rdTarget" runat="server" AutoPostBack="true" Visible="true"></telerik:RadDatePicker>
                                            </td>
                                            <td></td>
                                            <td>Onboarding Phase
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlPhase" runat="server" DefaultMessage="Select Phase" ToolTip="Select Phase" Visible="true" OnSelectedIndexChanged="rddlPhase_IndexChanged"></telerik:RadDropDownList>
                                            </td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlCloseReason" Width="250px" runat="server" DefaultMessage="Select Reason" ToolTip="Select Reason" Visible="false"></telerik:RadDropDownList>
                                            </td>
                                            <td></td>


                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Current Go-Live Date
                                            </td>
                                            <td></td>
                                            <td>

                                                <telerik:RadDatePicker RenderMode="Lightweight" ID="rdCurrentTarget" runat="server" AutoPostBack="true" OnSelectedDateChanged="rdCurrentTarget_SelectedDateChanged" Visible="true"></telerik:RadDatePicker>
                                            </td>
                                            <td></td>
                                            <td>
                                                <asp:Label ID="lblChangeReason" CssClass="alert-link" Text="Go-Live Change Reason" runat="server" Visible="false" /></td>
                                            <td></td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtTargetChangeReason" TextMode="multiline" Columns="80" Rows="1" runat="server" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>Actual Go-Live Date
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDatePicker RenderMode="Lightweight" ID="rdActual" runat="server" AutoPostBack="true" Visible="true"></telerik:RadDatePicker>
                                            </td>
                                            <td></td>
                                            <td>Shipping Channel</td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlShippingChannel" runat="server" DefaultMessage="Select Shipping Channel" ToolTip="Select Shipping Channel" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Solution Summary
                                            </td>
                                            <td></td>
                                            <td colspan="6">
                                                <asp:TextBox ID="txtSolutionSummary" TextMode="multiline" Columns="100" Rows="3" runat="server" Visible="true" />
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Routing 
                                            </td>
                                            <td></td>
                                            <td colspan="6">
                                                <asp:TextBox ID="txtRoute" TextMode="multiline" Columns="100" Rows="1" runat="server" Visible="true" />
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Shipping Products
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlProducts" runat="server" Width="200px" DefaultMessage="Select Shipping Product" ToolTip="Select Product" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadButton ID="btnAddProduct" CausesValidation="true" ValidationGroup="addProdButton" runat="server" Text="Add" OnClick="btnAddProduct_Click" AutoPostBack="true" />
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                            <td colspan="5">
                                                <telerik:RadGrid ID="rgProductGrid" runat="server" CellSpacing="-1" GridLines="None" GroupPanelPosition="Top" AllowFilteringByColumn="false" OnNeedDataSource="rgProductGrid_NeedDataSource" OnDeleteCommand="rgProductGrid_DeleteCommand">
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                    <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
                                                    </ExportSettings>
                                                    <MasterTableView AutoGenerateColumns="False" AllowPaging="False" CommandItemDisplay="Top">
                                                        <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToExcelButton="False"></CommandItemSettings>
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="idShippingProduct" HeaderText="Proposed ServiceID" UniqueName="ServiceID" Visible="false"></telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="productDesc" HeaderText="Proposed Products" UniqueName="Service">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                    </MasterTableView>
                                                </telerik:RadGrid><p></p>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <%--    EDI Summary    --%>
                            <telerik:RadPanelItem Text="EDI Summary"  Expanded="True">
                                <ContentTemplate>
                                    <table border="0">
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 140px">ITBA Assigned
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlITBA2" runat="server" DefaultMessage="Select Onboarding Specialist" ToolTip="Select Onboarding Specialist" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 140px">EDI Specialist Assigned</td>
                                            <td style="color: red;"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="cmboxEDISpecialist" runat="server" DefaultMessage="Select EDI Specialist" ToolTip="Select EDI Specialist" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px">Onboarding Phase</td>
                                            <td style="color: red;"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="cmboxOnboardingPhase" runat="server" Width="240"  DefaultMessage="Select Onboarding Phase" ToolTip="Select Onboarding Phase" Visible="true">
                                                </telerik:RadDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px">Billing Specialist Assigned</td>
                                            <td style="color: red;"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="cmboxBillingSpecialist" runat="server" DefaultMessage="Select Billing Specialist" ToolTip="Select Billing Specialist" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px">Collection Specialist Assigned</td>
                                            <td style="color: red;"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="cmboxCollectionSpecialist" runat="server" DefaultMessage="Select Collection Specialist" ToolTip="Select Collection Specialist" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px">Target Go-Live Date</td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDatePicker RenderMode="Lightweight" ID="dateTargetGoLive" runat="server" AutoPostBack="true" OnSelectedDateChanged="rdCurrentTarget_SelectedDateChanged" Visible="true"></telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px"></td>
                                            <td></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px">Current Go-Live Date</td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDatePicker RenderMode="Lightweight" ID="dateCurrentGoLive" runat="server" AutoPostBack="true" OnSelectedDateChanged="rdCurrentTarget_SelectedDateChanged" Visible="true"></telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px"></td>
                                            <td></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px">Actual Go-Live Date</td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDatePicker RenderMode="Lightweight" ID="dateActualGoLive" runat="server" AutoPostBack="true" OnSelectedDateChanged="rdCurrentTarget_SelectedDateChanged" Visible="true"></telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px;color:red;text-align:right;"><asp:Label runat="server" Text="" Visible="true"></asp:Label></td>
                                            <td>EDI Solution Summary</td>
                                            <td></td>
                                            <td colspan="7">
                                                <asp:TextBox ID="txtBxEDISolutionSummary" TextMode="multiline" Columns="100" Rows="3" runat="server" Visible="true" />
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 140px">Freight Auditor Involved</td>
                                            <td></td>
                                            <td>
                                                <telerik:RadDropDownList ID="comboxFreightAuditorInvolved" runat="server" Visible="true" Width="70px">
                                                    <Items>
                                                    <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                                                    <telerik:DropDownListItem Value="yes" Text="Yes" />
                                                </Items>
                                            </telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px;color:red;text-align:right;"><asp:Label runat="server" Text="" Visible="true"></asp:Label></td>
                                            <td style="width: 140px">EDI Transactions</td>
                                            <td></td>
                                            <td >
                                                <telerik:RadGrid ID="gridProfileEDITrans" runat="server" AllowPaging="True"
                                                    AllowSorting="false" AllowFilteringByColumn="false"
                                                    AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false"
                                                    OnNeedDataSource="gridEDITransactions_NeedDataSource" OnDeleteCommand="gridEDITransactions_DeleteCommand"
                                                    OnItemDataBound="gridEDITransactions_ItemDataBound" OnUpdateCommand="gridEDITransactions_UpdateCommand"
                                                    OnItemCommand="gridEDITransactions_ItemCommand">
                                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="idEDITranscation" CommandItemDisplay="Top">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="EDITranscationType" FilterControlAltText="EDITranscationType" SortExpression="Team" HeaderText="EDI Trans Type" UniqueName="EDITranscationType">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                        <EditFormSettings EditFormType="Template">
                                                            <EditColumn UniqueName="EditColumn"></EditColumn>
                                                            <FormTemplate>
                                                                <table id="Table2" style="padding-top: 2px; width: 100%; margin-left: 50px" border="0">
                                                                    <tr>
                                                                        <td style="text-align: right; width: 100px; vertical-align: top">Transaction Requested</td>
                                                                        <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                                        <td>
                                                                            <telerik:RadDropDownList ID="radListEDITransList" runat="server" OnSelectedIndexChanged="radListEDITransIdxChanged" DefaultMessage="Select EDI Trans Req" AutoPostBack="true" ToolTip="Select Your Ship Method" Visible="true" Width="280px">
                                                                            </telerik:RadDropDownList>
                                                                        </td>
                                                                        <td style="height: 20px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" style="height: 20px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px;"></td>
                                                                        <td style="width: 2px;"></td>
                                                                        <td align="center">
                                                                            <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                                            </telerik:RadButton>
                                                                            &nbsp;
                                                                            <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                                                CommandName="Cancel">
                                                                            </telerik:RadButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FormTemplate>
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                            <td style="width: 10px;color:red;text-align:right;"><asp:Label runat="server" Text="" Visible="true"></asp:Label></td>
                                            <td style="width: 160px">Ship Methods in Scope</td>
                                            <td></td>
                                            <td>
                                                <telerik:RadGrid ID="gridProfileShipMethod" runat="server" AllowPaging="True"
                                                    AllowSorting="false" AllowFilteringByColumn="false"
                                                    AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false"
                                                    OnNeedDataSource="gridShipmentMethods_NeedDataSource" OnDeleteCommand="gridShipmentMethods_DeleteCommand"
                                                    OnItemDataBound="gridShipmentMethods_ItemDataBound" OnUpdateCommand="gridShipmentMethods_UpdateCommand"
                                                    OnItemCommand="gridShipmentMethods_ItemCommand">
                                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="idEDIShipMethod" CommandItemDisplay="Top">
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="MethodType" FilterControlAltText="MethodType" SortExpression="Team" HeaderText="Method Type" UniqueName="MethodType">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                        <EditFormSettings EditFormType="Template">
                                                            <EditColumn UniqueName="EditColumn"></EditColumn>
                                                            <FormTemplate>
                                                                <table id="Table2" style="padding-top: 2px; width: 100%; margin-left: 50px" border="0">
                                                                    <tr>
                                                                        <td style="text-align: right; width: 100px; vertical-align: top">Shipment Method</td>
                                                                        <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                                        <td>
                                                                            <telerik:RadDropDownList ID="radListEDIShipMethod" runat="server" OnSelectedIndexChanged="radListEDIShipMethodIdxChanged" DefaultMessage="Select EDI Ship Method" AutoPostBack="true" ToolTip="Select Your Ship Method" Visible="true" Width="280px">
                                                                            </telerik:RadDropDownList>
                                                                        </td>
                                                                        <td style="height: 20px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" style="height: 20px"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 100px;"></td>
                                                                        <td style="width: 2px;"></td>
                                                                        <td align="center">
                                                                            <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                                            </telerik:RadButton>
                                                                            &nbsp;
                                                                            <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                                            </telerik:RadButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </FormTemplate>
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                        <tr><td></td></tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 140px">Customer/Auditor Portal</td>
                                            <td></td>
                                            <td>
                                                 <telerik:RadDropDownList ID="comboxCustAuditPortal" runat="server" Visible="true" Width="70px" OnSelectedIndexChanged="onIdxChangedCustAuditPortal" AutoPostBack="true">
                                                   <Items>
                                                        <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                                                        <telerik:DropDownListItem Value="yes" Text="Yes" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px;color:red;text-align:right;"><asp:Label ID="lblCustAuditPortalStar" runat="server" Text="" Visible="true"></asp:Label></td>
                                            <td style="width: 140px;">
                                                <asp:Label ID="lblCustAuditPortalYes" runat="server" Text="If Yes, then:" Visible="true"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 140px">
                                                <asp:Label ID="lblCustAuditPortalURL" runat="server" Text="URL" Visible="true"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtBxAuditoURL" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter URL" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 140px">
                                                <asp:Label ID="lblCustAuditPortalUserName" runat="server" Text="User Name" Visible="true"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtBxAuditoUserName" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Username" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td style="width: 140px">
                                                <asp:Label ID="lblCustAuditPortalPassword" runat="server" Text="Password" Visible="true"></asp:Label>
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtBxAuditoPassword" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Password" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="width: 160px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>
                            <%-- WORLDPAK    --%>
                            <telerik:RadPanelItem Text="WorldPak" Value="WorldPak">
                                <ContentTemplate>
                                    <table border="0">
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadButton ID="cbxExportFile" Text="Custom Export File?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadButton ID="cbxEquipment" Text="Equipment Provided?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false" OnClick="cbxEquipment_Click">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadButton ID="cbxAddressBook" Text="Address Book Upload?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td>
                                                <telerik:RadButton ID="cbxProdUpload" Text="Product File Upload?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                                &nbsp;<telerik:RadButton ID="cbxGhostScan" Text="Ghost Scan?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="7"></td>
                                        </tr>

                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Data Entry Method
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlDataEntry" runat="server" DefaultMessage="Select Data Entry Mentod" ToolTip="Select Entry Method" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Sandbox UserName
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtWPKsboxuser" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Sandbox Username" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Production Username
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtWPKproduser" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Production Username" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Sandbox Password
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtWPKsboxpwd" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Sandbox Password" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Production Password
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtWPKprodpwd" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Production Password" />
                                            </td>
                                        </tr>
                                        <%-- East / West split  --%>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadButton ID="cbxSplit" Text="East/West Split?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false" OnClick="cbxSplit_Click">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:Label ID="lblEWselection" runat="server" Text="Account Selected By:" Visible="false"></asp:Label>
                                            </td>

                                            <td colspan="2">
                                                <telerik:RadDropDownList ID="rddlEWselection" runat="server" DefaultMessage="Selection Mentod" ToolTip="Selection Method" Visible="false">
                                                    <Items>
                                                        <telerik:DropDownListItem Text="Manually" />
                                                        <telerik:DropDownListItem Text="System" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadButton ID="cbxEWsortcode" Text="Sort Code on Label?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false" OnClick="cbxEWsortcode_Click" Visible="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td style="width: 10px">
                                            <td colspan="2">
                                                <asp:Label ID="lblEWesort" runat="server" Text="East Sort Code:" Visible="false"></asp:Label>
                                                <telerik:RadTextBox ID="txtEWesortcode" runat="server" MaxLength="20" Text='' Width="100px" ToolTip="Enter East Sort Code" Visible="false" />&nbsp;<asp:Label ID="lblEWwsort" runat="server" Text="West Sort Code:" Visible="false"></asp:Label><telerik:RadTextBox ID="txtEWwsortcode" runat="server" MaxLength="20" Text='' Width="100px" ToolTip="Enter West Sort Code" Visible="false" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadButton ID="cbxEWcloseout" Text="Two Seperate Closeouts?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false" Visible="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>

                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadButton ID="cbxEWpickups" Text="Two Separate Pickups?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false" Visible="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>

                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td colspan="3">
                                                <asp:Label ID="lblEWsorting" runat="server" Text="How will packages be sorted and by Whom?" Visible="false"> </asp:Label>
                                            </td>


                                            <td>
                                                <asp:TextBox ID="txtEWsorting" TextMode="multiline" Columns="50" Rows="2" runat="server" Visible="false" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td colspan="3">
                                                <asp:Label ID="lblEWmissort" runat="server" Text="How will mis-sorts be handled?" Visible="false"> </asp:Label>
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtEWmissort" TextMode="multiline" Columns="50" Rows="2" runat="server" Visible="false" />
                                            </td>
                                        </tr>

                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- SHIPPING CHANNELS --%>

                            <%-- COURIER --%>
                            <telerik:RadPanelItem Text="Courier" Value="Courier">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Courier Account#
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierAcct" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Courier Account" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Courier Contract#
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierContract" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Courier Contract" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Pin Prefix
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierPin" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Courier Pin Prefix" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Transit Days
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierTransitDays" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Courier Transit Days" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Select Induction Address or Enter One
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlCourierInduction" Width="200px" runat="server" DefaultMessage="Select Induction" ToolTip="Select Induction" OnSelectedIndexChanged="CourierInduction_IndexChanged" Visible="true" AutoPostBack="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Induction Address
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="courierIaddress" runat="server" MaxLength="50" Text='' Width="200px" ToolTip="Enter Address" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>City, State Zip
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="courierIcity" runat="server" MaxLength="75" Text='' Width="150px" ToolTip="Enter City" />
                                                <telerik:RadTextBox ID="courierIstate" runat="server" MaxLength="10" Text='' Width="50px" ToolTip="Enter State" />
                                                <telerik:RadTextBox ID="courierIzip" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Zip" />
                                                Country
                                                <telerik:RadTextBox ID="courierIcountry" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Country" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>eManifest FTP User Name
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierFTPuser" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter FTP Username" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td colspan="2">
                                                <telerik:RadButton ID="cbxCourierFTPCustOwn" Text="Customer Own FTP?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>eManifest FTP Password
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierFTPpwd" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter FTP password" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>eManifest FTP Sender ID
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierSenderID" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Sender ID" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Sandbox FTP User Name
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtSandboxFTPuser" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Sandbox FTP Username" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Sandbox FTP Password
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtSandboxFTPpwd" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Sandbox FTP Password" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- COURIER WEST --%>
                            <telerik:RadPanelItem Text="Courier West" Value="CourierWest">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Courier Account#
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierAcctWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Courier Account" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Courier Contract#
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierContractWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Courier Contract" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Pin Prefix
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierPinWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Courier Pin Prefix" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Transit Days
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierTransitDaysWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Courier Transit Days" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Select Induction Address or Enter One
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlCourierInductionWest" Width="200px" runat="server" DefaultMessage="Select Induction" ToolTip="Select Induction" OnSelectedIndexChanged="CourierInductionWest_IndexChanged" Visible="true" AutoPostBack="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Induction Address
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="courierIaddressWest" runat="server" MaxLength="50" Text='' Width="200px" ToolTip="Enter Address" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>City, State Zip
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="courierIcityWest" runat="server" MaxLength="75" Text='' Width="150px" ToolTip="Enter City" />
                                                <telerik:RadTextBox ID="courierIstateWest" runat="server" MaxLength="10" Text='' Width="50px" ToolTip="Enter State" />
                                                <telerik:RadTextBox ID="courierIzipWest" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Zip" />
                                                Country
                                                <telerik:RadTextBox ID="courierIcountryWest" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Country" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>eManifest FTP User Name
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierFTPuserWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter FTP Username" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td colspan="2">
                                                <telerik:RadButton ID="cbxCourierFTPCustOwnWest" Text="Customer Own FTP?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>eManifest FTP Password
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierFTPpwdWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter FTP password" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>eManifest FTP Sender ID
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCourierSenderIDWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Sender ID" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Sandbox FTP User Name
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtSandboxFTPuserWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Sandbox FTP Username" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Sandbox FTP Password
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtSandboxFTPpwdWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Sandbox FTP Password" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- LTL --%>
                            <telerik:RadPanelItem Text="LTL" Value="LTL">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>LTL Account Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLTLAccount" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter LTL Account" />
                                            </td>

                                            <td style="width: 10px"></td>
                                            <td>LTL Min Pro Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLTLminPro" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Min Pro Number" />
                                            </td>

                                            <td style="width: 10px"></td>
                                            <td>LTL Max Pro Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLTLmaxPro" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Max Pro Number" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>LTL PIN Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLTLPin" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter LTL PIN" />
                                            </td>

                                            <td style="width: 10px"></td>
                                            <td colspan="5">
                                                <telerik:RadButton ID="cbxLTLAuto" Text="LTL Automated Process?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>

                                            </td>


                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- LTL WEST --%>
                            <telerik:RadPanelItem Text="LTL West" Value="LTLWest">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>LTL Account Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLTLAccountWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter LTL Account" />
                                            </td>

                                            <td style="width: 10px"></td>
                                            <td>LTL Min Pro Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLTLminProWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Min Pro Number" />
                                            </td>

                                            <td style="width: 10px"></td>
                                            <td>LTL Max Pro Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLTLmaxProWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Max Pro Number" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>LTL PIN Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLTLPinWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter LTL PIN" />
                                            </td>

                                            <td style="width: 10px"></td>
                                            <td colspan="5">
                                                <%-- <telerik:RadButton ID="cbxLTLAutoWest" Text="LTL Automated Process?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                <ToggleStates>
                                <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true"/>
                                </ToggleStates>
                                </telerik:RadButton>       --%>                       
                                                                 
                                            </td>


                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- PUROPOST --%>
                            <telerik:RadPanelItem Text="Puropost" Value="PuroPost">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>PuroPost Account Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPPSTAcct" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter PPST Account" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Transit Days
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPPSTTransitDays" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter PPST Transit Days" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>PuroPost Induction Point or Enter One
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlPPSTInduction" runat="server" Width="200px" DefaultMessage="Select Induction" ToolTip="Select Induction" OnSelectedIndexChanged="PuroPostInduction_IndexChanged" Visible="true" AutoPostBack="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Induction Address
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="ppstIaddress" runat="server" MaxLength="50" Text='' Width="200px" ToolTip="Enter Address" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>City, State Zip
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="ppstIcity" runat="server" MaxLength="75" Text='' Width="150px" ToolTip="Enter City" />
                                                <telerik:RadTextBox ID="ppstIstate" runat="server" MaxLength="10" Text='' Width="50px" ToolTip="Enter State" />
                                                <telerik:RadTextBox ID="ppstIzip" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Zip" />
                                                Country
                                                <telerik:RadTextBox ID="ppstIcountry" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Country" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Route
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtPPSTRoute" TextMode="multiline" Columns="50" Rows="2" runat="server" Visible="true" />
                                            </td>

                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- PUROPOST WEST--%>
                            <telerik:RadPanelItem Text="Puropost West" Value="PuroPostWest">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>PuroPost Account Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPPSTAcctWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter PPST Account" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Transit Days
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPPSTTransitDaysWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter PPST Transit Days" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>PuroPost Induction Point or Enter One
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlPPSTInductionWest" runat="server" Width="200px" DefaultMessage="Select Induction" ToolTip="Select Induction" OnSelectedIndexChanged="PuroPostInductionWest_IndexChanged" Visible="true" AutoPostBack="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Induction Address
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="ppstIaddressWest" runat="server" MaxLength="50" Text='' Width="200px" ToolTip="Enter Address" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>City, State Zip
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="ppstIcityWest" runat="server" MaxLength="75" Text='' Width="150px" ToolTip="Enter City" />
                                                <telerik:RadTextBox ID="ppstIstateWest" runat="server" MaxLength="10" Text='' Width="50px" ToolTip="Enter State" />
                                                <telerik:RadTextBox ID="ppstIzipWest" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Zip" />
                                                Country
                                                <telerik:RadTextBox ID="ppstIcountryWest" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Country" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Route
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtPPSTRouteWest" TextMode="multiline" Columns="50" Rows="2" runat="server" Visible="true" />
                                            </td>

                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- PUROPOST PLUS --%>
                            <telerik:RadPanelItem Text="Puropost Plus" Value="PuroPostPlus">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>PuroPost Plus Account Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPPlusAcct" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter PuroPost Plus Account" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Transit Days
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPPlusTransitDays" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter PuroPost Plus Transit Days" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>PuroPost Plus Induction Point or Enter One
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlPPlusInduction" runat="server" Width="200px" DefaultMessage="Select Induction" ToolTip="Select Induction" OnSelectedIndexChanged="PuroPostPlusInduction_IndexChanged" Visible="true" AutoPostBack="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Induction Address
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="PPlusIaddress" runat="server" MaxLength="50" Text='' Width="200px" ToolTip="Enter Address" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>City, State Zip
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="PPlusIcity" runat="server" MaxLength="75" Text='' Width="150px" ToolTip="Enter City" />
                                                <telerik:RadTextBox ID="PPlusIstate" runat="server" MaxLength="10" Text='' Width="50px" ToolTip="Enter State" />
                                                <telerik:RadTextBox ID="PPlusIzip" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Zip" />
                                                Country
                                                <telerik:RadTextBox ID="PPlusIcountry" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Country" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Route
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtPPlusRoute" TextMode="multiline" Columns="50" Rows="2" runat="server" Visible="true" />
                                            </td>

                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- PUROPOST PLUS WEST--%>
                            <telerik:RadPanelItem Text="Puropost Plus West" Value="PuroPostPlusWest">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>PuroPost Plus Account Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPPlusAcctWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter PuroPost Plus Account" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Transit Days
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPPlusTransitDaysWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter PuroPost Plus Transit Days" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>PuroPost Induction Point or Enter One
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlPPlusInductionWest" runat="server" Width="200px" DefaultMessage="Select Induction" ToolTip="Select Induction" OnSelectedIndexChanged="PuroPostInductionWest_IndexChanged" Visible="true" AutoPostBack="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Induction Address
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="PPlusIaddressWest" runat="server" MaxLength="50" Text='' Width="200px" ToolTip="Enter Address" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>City, State Zip
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="PPlusIcityWest" runat="server" MaxLength="75" Text='' Width="150px" ToolTip="Enter City" />
                                                <telerik:RadTextBox ID="PPlusIstateWest" runat="server" MaxLength="10" Text='' Width="50px" ToolTip="Enter State" />
                                                <telerik:RadTextBox ID="PPlusIzipWest" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Zip" />
                                                Country
                                                <telerik:RadTextBox ID="PPlusIcountryWest" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Country" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Route
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td colspan="4">
                                                <asp:TextBox ID="txtPPlusRouteWest" TextMode="multiline" Columns="50" Rows="2" runat="server" Visible="true" />
                                            </td>

                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- CANADA POST --%>
                            <telerik:RadPanelItem Text="Canada Post" Value="CPC">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Account Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCAcct" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Account" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Contract Number
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCContract" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Contract" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Site Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCSite" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Site#" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Induction Number
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCInduction" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Induction#" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Username
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCUser" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Username" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Password
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCPwd" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Password" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- CANADA POST WEST --%>
                            <telerik:RadPanelItem Text="Canada Post WEST" Value="CPCWest">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Account Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCAcctWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Account" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Contract Number
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCContractWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Contract" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Site Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCSiteWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Site#" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Induction Number
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCInductionWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Induction#" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Username
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCUserWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Username" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Canada Post Password
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCPCPwdWest" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter CPC Password" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- RETURNS --%>
                            <telerik:RadPanelItem Text="Returns" Value="Returns">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Returns Account Number
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtReturnsAcct" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Returns Account" />
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadButton ID="cbxDestroy" Text="Destroy?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                                <telerik:RadButton ID="cbxReturnLabel" Text="Create Return Label with Shipments?" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" Selected="true" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadButton ID="chkMartinGrove" Text="Fill in Martin Grove Address" runat="server" OnClick="chkMartinGrove_Clicked" SingleClick="true" AutoPostBack="true">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Returns Address
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtReturnsAddress" runat="server" MaxLength="50" Text='' Width="200px" ToolTip="Enter Address" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>City, State Zip
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtReturnsCity" runat="server" MaxLength="75" Text='' Width="150px" ToolTip="Enter City" />
                                                <telerik:RadTextBox ID="txtReturnsState" runat="server" MaxLength="10" Text='' Width="50px" ToolTip="Enter State" />
                                                <telerik:RadTextBox ID="txtReturnsZip" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Zip" />
                                                Country
                                                <telerik:RadTextBox ID="txtReturnsCountry" runat="server" MaxLength="25" Text='' Width="75px" ToolTip="Enter Country" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%--  CUSTOMS  --%>
                            <telerik:RadPanelItem Text="Customs" Value="Customs">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadButton Text="Customs Supported?" ID="chkCustoms" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="true">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadButton Text="Elink?" ID="chkElink" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>PARS#
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPARS" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Number" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="PASS#" Visible="true"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtPASS" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Number" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Select Broker
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlBroker" Width="300px" runat="server" DefaultMessage="Select Broker Name" ToolTip="Select Broker" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>or Other</td>
                                            <td>
                                                <telerik:RadTextBox ID="txtOtherBroker" runat="server" MaxLength="130" Text='' Width="300px" ToolTip="Enter Broker" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Broker#</td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtBrokerNumber" runat="server" MaxLength="50" Text='' Width="100px" ToolTip="Enter Broker Number" /></td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%-- BILLING INFORMATION --%>
                            <telerik:RadPanelItem Text="Billing and EDI" Value="BillingandEDI">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Invoice Type
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlInvoiceType" runat="server" DefaultMessage="Select Invoice Type" ToolTip="Select Invoice Type" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td></td>
                                            <td>
                                                <telerik:RadButton Text="Customized EDI?" ID="cbxCustomEDI" runat="server" ToggleType="CheckBox" ButtonType="ToggleButton" AutoPostBack="true" Checked="false">
                                                    <ToggleStates>
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                                        <telerik:RadButtonToggleState Text="" PrimaryIconCssClass="rbToggleCheckbox" />
                                                    </ToggleStates>
                                                </telerik:RadButton>
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>EDI Solution
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlEDISolution" runat="server" DefaultMessage="Select EDI Solution" ToolTip="Select EDI Solution" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td>File Format
                                            </td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlFileFormat" runat="server" DefaultMessage="Select File Format" ToolTip="Select File Format" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td>Communication Method
                                            </td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlCommunicationMethod" runat="server" DefaultMessage="Select Communication" ToolTip="Select Communication" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td>
                                                <telerik:RadButton ID="btnAddEDI" CausesValidation="true" ValidationGroup="addProdButton" OnClick="btnAddEDI_Click" runat="server" Text="Add" AutoPostBack="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td style="width: 10px"></td>
                                            <td colspan="4">
                                                <telerik:RadGrid ID="rgSolutionsGrid" runat="server" CellSpacing="-1" GridLines="None" GroupPanelPosition="Top" AllowFilteringByColumn="false" OnNeedDataSource="rgSolutionsGrid_NeedDataSource" OnDeleteCommand="rgSolutionsGrid_DeleteCommand">
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                    <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
                                                    </ExportSettings>
                                                    <MasterTableView AutoGenerateColumns="False" AllowPaging="False" CommandItemDisplay="Top">
                                                        <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToExcelButton="False"></CommandItemSettings>
                                                        <Columns>
                                                            <telerik:GridBoundColumn DataField="Solution" HeaderText="Solution" UniqueName="Solution">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="FileFormat" HeaderText="File Format" UniqueName="FileFormat">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="CommunicationMethod" HeaderText="Communication Method" UniqueName="CommunicationMethod">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                    </MasterTableView>
                                                </telerik:RadGrid><p></p>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>FTP Username
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtFTPLogin" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter FTP Login" />
                                            </td>
                                            <td>FTP Password</td>
                                            <td>
                                                <telerik:RadTextBox ID="txtFTPpwd" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter FTP Password" /></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Billto Account
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtBillto" runat="server" MaxLength="50" Text='' Width="150px" ToolTip="Enter Billto Account" />
                                            </td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%--  EQUPMENT --%>
                            <telerik:RadPanelItem Text="Equipment" Value="Equipment">
                                <ContentTemplate>
                                    <table border="0">
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Equpment Type
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlEquipment" Width="300px" runat="server" DefaultMessage="Select Equipment" ToolTip="Select Equipment" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Number Requested
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtNbrEquipment" runat="server" MaxLength="25" Text='' Width="50px" ToolTip="Enter Number" />
                                                <asp:RequiredFieldValidator ID="rfvEquip" runat="server" ValidationGroup="addEquip" ControlToValidate="txtNbrEquipment" ErrorMessage="Please enter Number" Style="color: red"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvEquipType" runat="server" ValidationGroup="addEquip" ControlToValidate="rddlEquipment" ErrorMessage="Please Select Equipment" Style="color: red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="text-align: left">
                                                <telerik:RadButton ID="btnAddEquipment" CausesValidation="true" ValidationGroup="addEquip" OnClick="btnAddEquipment_Click" runat="server" Text="Add" AutoPostBack="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Other
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="text-align: left">
                                                <telerik:RadTextBox ID="txtOtherEquipment" runat="server" MaxLength="150" Text='' Width="300px" ToolTip="Enter Equipment" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Number Requested
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtNbrOther" runat="server" MaxLength="25" Text='' Width="50px" ToolTip="Enter Number" />
                                                <asp:RequiredFieldValidator ID="rfvEquip2" runat="server" ValidationGroup="addEquip2" ControlToValidate="txtNbrOther" ErrorMessage="Please enter Number" Style="color: red"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvOther" runat="server" ValidationGroup="addEquip2" ControlToValidate="txtOtherEquipment" ErrorMessage="Please enter Description" Style="color: red"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td style="text-align: left">
                                                <telerik:RadButton ID="btnAddOtherEquipment" CausesValidation="true" ValidationGroup="addEquip2" OnClick="btnAddOtherEquipment_Click" runat="server" Text="Add" AutoPostBack="true" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                            <td colspan="7">
                                                <telerik:RadGrid ID="rgEquipmentGrid" runat="server" CellSpacing="-1" GridLines="None" GroupPanelPosition="Top" AllowFilteringByColumn="false" OnNeedDataSource="rgEquipmentGrid_NeedDataSource" OnDeleteCommand="rgEquipmentGrid_DeleteCommand">
                                                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                                    <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
                                                    </ExportSettings>
                                                    <MasterTableView AutoGenerateColumns="False" AllowPaging="False" CommandItemDisplay="Top">
                                                        <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToExcelButton="False"></CommandItemSettings>
                                                        <Columns>
                                                            <%-- <telerik:GridBoundColumn DataField="idService" HeaderText="Proposed ServiceID" UniqueName="ServiceID" Visible="false">
                                                            </telerik:GridBoundColumn>--%>
                                                            <telerik:GridBoundColumn DataField="EquipmentDesc" HeaderText="Equipment" UniqueName="Equipment">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Number" HeaderText="Number Needed" UniqueName="Number">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>

                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%--  CONTRACT INFORMATION  --%>
                            <telerik:RadPanelItem Text="Contract Information" Value="ContractInformation">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Contract #
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtContract" runat="server" MaxLength="50" Text='' Width="125px" ToolTip="Enter Contract#" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Start Date
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker RenderMode="Lightweight" ID="rdpStartDate" runat="server" AutoPostBack="true" Visible="true"></telerik:RadDatePicker>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>End Date
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker RenderMode="Lightweight" ID="rdpEndDate" runat="server" AutoPostBack="true" Visible="true"></telerik:RadDatePicker>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Payment Terms
                                            </td>
                                            <td colspan="4">
                                                <telerik:RadTextBox ID="txtPaymentTerms" runat="server" MaxLength="450" Text='' Width="450px" ToolTip="Enter Payment Terms" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Currency
                                            </td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlCurrency" runat="server" DefaultMessage="Select Currency" ToolTip="Select Currency" Visible="true"></telerik:RadDropDownList>
                                            </td>

                                        </tr>

                                    </table>

                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%--  ACCOUNT SUPPORT --%>
                            <telerik:RadPanelItem Text="Account Support" Value="AccountSupport">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Control Branch
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDropDownList ID="rddlControlBranch" runat="server" DefaultMessage="Select Branch" ToolTip="Select Branch" Visible="true"></telerik:RadDropDownList>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>CRR</td>
                                            <td>
                                                <telerik:RadTextBox ID="txtCRR" runat="server" MaxLength="55" Text='' Width="170px" ToolTip="Enter CRR" /></td>
                                        </tr>

                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Default Support User
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtSupportUser" runat="server" MaxLength="55" Text='' Width="170px" ToolTip="Enter Support User" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Default Support Group
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtSupportGroup" runat="server" MaxLength="55" Text='' Width="170px" ToolTip="Enter Support Group" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Office
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtOffice" runat="server" MaxLength="55" Text='' Width="170px" ToolTip="Enter Office" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Group
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtGroup" runat="server" MaxLength="55" Text='' Width="170px" ToolTip="Enter Gorup" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                            <%--  MIGRATION DETAILS --%>
                            <telerik:RadPanelItem Text="Migration Details" Value="Migration" Visible="true">
                                <ContentTemplate>
                                    <table border="0">
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Migration Date
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <telerik:RadDatePicker RenderMode="Lightweight" ID="rdpMigrationDate" runat="server" AutoPostBack="true" Visible="true"></telerik:RadDatePicker>
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10px"></td>
                                            <td>Pre-Migration Solution
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>
                                                <asp:TextBox ID="txtPreMigration" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                                            </td>
                                            <td style="width: 10px"></td>
                                            <td>Post-Migration Solution
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPostMigration" TextMode="multiline" Columns="50" Rows="5" runat="server" />
                                            </td>
                                        </tr>

                                    </table>
                                </ContentTemplate>
                            </telerik:RadPanelItem>

                        </Items>
                    </telerik:RadPanelBar>
                </div>
            </telerik:RadPageView>
            
            <%--  Courier EDI  --%>
            <telerik:RadPageView runat="server" ID="courierEDI">
                <hr />
                <table border="0">
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="width: 140px;text-align:right">Invoice</td>
                        <td></td>
                        <td style="width: 140px; text-align:left">
                            <telerik:RadDropDownList ID="comboBxCourierEDI210" runat="server" Visible="true" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="comboBxCourierEDI210_SelectedIndexChanged">
                                <Items>
                                    <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                                    <telerik:DropDownListItem Value="yes" Text="Yes" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td class="Spacer" ></td>
                        <td style="width: 140px; text-align:right">Shipment Status</td>
                        <td></td>
                        <td style="width: 140px; text-align:left">
                            <telerik:RadDropDownList ID="comboBxCourierEDI214" runat="server" Visible="true" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="comboBxCourierEDI214_SelectedIndexChanged">
                                <Items>
                                    <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                                    <telerik:DropDownListItem Value="yes" Text="Yes"  />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="width: 160px; text-align: right;">
                            <asp:Label ID="Label18" runat="server" Text="If Yes, then:" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 10px; color: red; text-align: right;">
                            <asp:Label ID="Label17" runat="server" Text="*" Visible="false"></asp:Label></td>
                        <td ></td>
                        <td style="width: 10px"></td>
                        <td style="width: 140px;text-align:right">
                            <asp:Label ID="Label20" runat="server" Text="If Yes, then:" Visible="false"></asp:Label>
                        </td>
                       <td style="width: 10px; color: red; text-align: left;">
                            <asp:Label ID="Label19" runat="server" Text="*" Visible="false"></asp:Label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="width: 160px; text-align: right;">
                            <asp:Label ID="lbl210Accounnts" runat="server" Text="Accounts" Visible="true"></asp:Label>
                        </td>
                        <td style="width: 10px; color: red; text-align: right;">
                            <asp:Label ID="lbl210AccounntStar" runat="server" Text="*" Visible="true"></asp:Label></td>
                        <td style="width: 220px; text-align:left">
                            <telerik:RadGrid ID="gridEDI210Accounts" runat="server" AllowPaging="True"
                                AllowSorting="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false"
                                OnNeedDataSource="gridEDI210Accounts_NeedDataSource" OnDeleteCommand="gridEDI210Accounts_DeleteCommand"
                                OnItemDataBound="gridEDI210Accounts_ItemDataBound"   OnItemCommand="gridEDI210Accounts_ItemCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="idEDIAccount" CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AccountNumber" FilterControlAltText="AccountNumber" SortExpression="Team" HeaderText="Account Number" UniqueName="AccountNumber">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <EditColumn UniqueName="EditColumn"></EditColumn>
                                        <FormTemplate>
                                            <table id="Table2" style="padding-top: 2px; width: 100%; margin-left: 5px" border="0">
                                                <tr>
                                                    <td style="text-align: right; width: 100px; vertical-align: top">Account Number</td>
                                                    <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtAccountNum" runat="server" MaxLength="75" Text="" Width="75px"  />
                                                    </td>
                                                    <td style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;"></td>
                                                    <td style="width: 2px;"></td>
                                                    <td align="center">
                                                        <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                        </telerik:RadButton>
                                                        &nbsp;
                                                        <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                            CommandName="Cancel">
                                                        </telerik:RadButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td style="width: 10px"></td>
                        <td style="width: 140px; text-align: right;">
                            <asp:Label ID="lbl214Accounnts" runat="server" Text="Accounts" Visible="true"></asp:Label>
                        </td>
                        <td style="width: 10px; color: red; text-align: right;">
                            <asp:Label ID="lbl214AccounntStar" runat="server" Text="*" Visible="true"></asp:Label></td>
                        <td style="width: 220px; text-align:left">
                            <telerik:RadGrid ID="gridEDI214Accounts" runat="server" AllowPaging="True"
                                AllowSorting="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false"
                                OnNeedDataSource="gridEDI214Accounts_NeedDataSource" OnDeleteCommand="gridEDI214Accounts_DeleteCommand"
                                OnItemDataBound="gridEDI214Accounts_ItemDataBound"   OnItemCommand="gridEDI214Accounts_ItemCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="idEDIAccount" CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AccountNumber" FilterControlAltText="AccountNumber" SortExpression="Team" HeaderText="Account Number" UniqueName="AccountNumber">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <EditColumn UniqueName="EditColumn"></EditColumn>
                                        <FormTemplate>
                                            <table id="Table2" style="padding-top: 2px; width: 100%; margin-left: 5px" border="0">
                                                <tr>
                                                    <td style="text-align: right; width: 100px; vertical-align: top">Account Number</td>
                                                    <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtAccountNum" runat="server" MaxLength="75" Text="" Width="75px"  />
                                                    </td>
                                                    <td style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;"></td>
                                                    <td style="width: 2px;"></td>
                                                    <td align="center">
                                                        <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                        </telerik:RadButton>
                                                        &nbsp;
                                                                            <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                                            </telerik:RadButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lblCombinepayer" runat="server" Text="Combine payer/bill to accounts?" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadDropDownList ID="comboxCombinepayer" runat="server" Visible="true" Width="70px">
                                <Items>
                                    <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                                    <telerik:DropDownListItem Value="yes" Text="Yes" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lblBatchInvoices" runat="server" Text="Batch invoices" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadDropDownList ID="comboBoxBatchInvoices" runat="server" Visible="true" Width="70px">
                                <Items>
                                    <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                                    <telerik:DropDownListItem Value="yes" Text="Yes" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td style="width: 10px"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lbl210InvoiceRecipients" runat="server" Text="Number of Invoice Recipients Requested" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadNumericTextBox RenderMode="Lightweight" MinValue="0" MaxValue="10" runat ="server" ID="txtBxNumberRecipients210" Width="50px" OnTextChanged="txtBxNumberRecipients210_TextChanged" AutoPostBack="true"  ShowSpinButtons="false" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                            <telerik:RadButton ID="btnAdd210" AutoPostBack="false" runat="server" Text="Update"></telerik:RadButton>
                        </td>
                        <td style="width: 10px"></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lbl214InvoiceRecipients" runat="server" Text="Number of Shipment Status Recipients Requested" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadNumericTextBox RenderMode="Lightweight" MinValue="0" MaxValue="10" runat ="server" ID="txtBxNumberRecipients214" Width="50px" OnTextChanged="txtBxNumberRecipients214_TextChanged" AutoPostBack="true"  ShowSpinButtons="false" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                            <telerik:RadButton ID="btnAdd214" AutoPostBack="false" runat="server" Text="Update"></telerik:RadButton>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="4" style="vertical-align:top">
                            <asp:PlaceHolder ID="ph1" runat="server" />
                        </td>
                         <td class="Spacer"></td>
                         <td colspan="4" style="text-align: center;vertical-align:top">
                            <asp:PlaceHolder ID="ph2" runat="server" />
                        </td>
                         <asp:Literal ID="ltlValues" runat="server" />
                         <asp:Literal ID="ltlValues2" runat="server" />
                         <asp:Literal ID="ltlCount" runat="server" Text="0" Visible="false" />
                         <asp:Literal ID="ltlRemoved" runat="server" Visible="false" />
                         <asp:Literal ID="ltlCount2" runat="server" Text="0" Visible="false" />
                         <asp:Literal ID="ltlRemoved2" runat="server" Visible="false" />
                     </tr>
                </table>
            </telerik:RadPageView>

            <%--  Non-Courier EDI  --%>
            <telerik:RadPageView runat="server" ID="noncourierEDI">
                <hr />
                <table border="0">
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="width: 140px;text-align:right">Invoice</td>
                        <td></td>
                        <td style="width: 140px; text-align:left">
                            <telerik:RadDropDownList ID="comboBxNonCourierEDI210" runat="server" Visible="true" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="comboBxNonCourierEDI210_SelectedIndexChanged">
                                <Items>
                                    <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                                    <telerik:DropDownListItem Value="yes" Text="Yes" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td class="SpacerNonCourier" ></td>
                        <td style="width: 140px; text-align:right">Shipment Status</td>
                        <td></td>
                        <td style="width: 140px; text-align:left">
                            <telerik:RadDropDownList ID="comboBxNonCourierEDI214" runat="server" Visible="true" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="comboBxNonCourierEDI214_SelectedIndexChanged">
                                <Items>
                                    <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                                    <telerik:DropDownListItem Value="yes" Text="Yes"  />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td class="SpacerNonCourier" ></td>
                        <td style="width: 170px; text-align:right">PuroPost Standard Invoice</td>
                        <td></td>
                         <td style="width: 140px; text-align:left">
                            <telerik:RadDropDownList ID="comboBxNonCourierPuroPost" runat="server" Visible="true" Width="70px" AutoPostBack="true" OnSelectedIndexChanged="comboBxNonCourierPuroPost_SelectedIndexChanged">
                                <Items>
                                    <telerik:DropDownListItem Value="no" Text="No" Selected="true" />
                                    <telerik:DropDownListItem Value="yes" Text="Yes"  />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="width: 160px; text-align: right;">
                            <asp:Label ID="Label12" runat="server" Text="If Yes, then:" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 10px; color: red; text-align: right;">
                            <asp:Label ID="Label13" runat="server" Text="*" Visible="false"></asp:Label></td>
                        <td ></td>
                        <td style="width: 10px"></td>
                        <td style="width: 140px;text-align:right">
                            <asp:Label ID="Label14" runat="server" Text="If Yes, then:" Visible="false"></asp:Label>
                        </td>
                       <td style="width: 10px; color: red; text-align: left;">
                            <asp:Label ID="Label15" runat="server" Text="*" Visible="false"></asp:Label></td>
                        <td></td>
                        <td></td>
                        <td style="width: 160px; text-align: right;">
                            <asp:Label ID="Label16" runat="server" Text="If Yes, then:" Visible="false"></asp:Label>
                        </td>
                        <td style="width: 10px; color: red; text-align: right;">
                            <asp:Label ID="Label21" runat="server" Text="*" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 10px"></td>
                        <td style="width: 160px; text-align: right;">
                            <asp:Label ID="lblNonCourier210Accounnts" runat="server" Text="Accounts" Visible="true"></asp:Label>
                        </td>
                        <td style="width: 10px; color: red; text-align: right;">
                            <asp:Label ID="lblNonCourier210AccounntStar" runat="server" Text="*" Visible="true"></asp:Label></td>
                        <td style="width: 220px; text-align:left">
                            <telerik:RadGrid ID="gridNonCourierEDI210Accounts" runat="server" AllowPaging="True"
                                AllowSorting="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false"
                                OnNeedDataSource="gridNonCourierEDI210Accounts_NeedDataSource" OnDeleteCommand="gridNonCourierEDI210Accounts_DeleteCommand"
                                OnItemDataBound="gridNonCourierEDI210Accounts_ItemDataBound"   OnItemCommand="gridNonCourierEDI210Accounts_ItemCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="idEDIAccount" CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AccountNumber" FilterControlAltText="AccountNumber" SortExpression="Team" HeaderText="Account Number" UniqueName="AccountNumber">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <EditColumn UniqueName="EditColumn"></EditColumn>
                                        <FormTemplate>
                                            <table id="Table2" style="padding-top: 2px; width: 100%; margin-left: 5px" border="0">
                                                <tr>
                                                    <td style="text-align: right; width: 100px; vertical-align: top">Account Number</td>
                                                    <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtAccountNum" runat="server" MaxLength="75" Text="" Width="75px"  />
                                                    </td>
                                                    <td style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;"></td>
                                                    <td style="width: 2px;"></td>
                                                    <td align="center">
                                                        <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                        </telerik:RadButton>
                                                        &nbsp;
                                                        <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                            CommandName="Cancel">
                                                        </telerik:RadButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td style="width: 10px"></td>
                        <td style="width: 140px; text-align: right;">
                            <asp:Label ID="lblNonCourier214Accounnts" runat="server" Text="Accounts" Visible="true"></asp:Label>
                        </td>
                        <td style="width: 10px; color: red; text-align: right;">
                            <asp:Label ID="lblNonCourier214AccounntStar" runat="server" Text="*" Visible="true"></asp:Label></td>
                        <td style="width: 220px; text-align:left">
                            <telerik:RadGrid ID="gridNonCourierEDI214Accounts" runat="server" AllowPaging="True"
                                AllowSorting="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false"
                                OnNeedDataSource="gridNonCourierEDI214Accounts_NeedDataSource" OnDeleteCommand="gridNonCourierEDI214Accounts_DeleteCommand"
                                OnItemDataBound="gridNonCourierEDI214Accounts_ItemDataBound"   OnItemCommand="gridNonCourierEDI214Accounts_ItemCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="idEDIAccount" CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AccountNumber" FilterControlAltText="AccountNumber" SortExpression="Team" HeaderText="Account Number" UniqueName="AccountNumber">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <EditColumn UniqueName="EditColumn"></EditColumn>
                                        <FormTemplate>
                                            <table id="Table2" style="padding-top: 2px; width: 100%; margin-left: 5px" border="0">
                                                <tr>
                                                    <td style="text-align: right; width: 100px; vertical-align: top">Account Number</td>
                                                    <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtAccountNum" runat="server" MaxLength="75" Text="" Width="75px"  />
                                                    </td>
                                                    <td style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;"></td>
                                                    <td style="width: 2px;"></td>
                                                    <td align="center">
                                                        <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                        </telerik:RadButton>
                                                        &nbsp;
                                                                            <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                                            </telerik:RadButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                        <td></td>
                        <td style="width: 160px; text-align: right;">
                            <asp:Label ID="lblNonCourierPuroPostAccounnts" runat="server" Text="Accounts" Visible="true"></asp:Label>
                        </td>
                        <td style="width: 10px; color: red; text-align: right;">
                            <asp:Label ID="lblNonCourierPuroPostAccounntStar" runat="server" Text="*" Visible="true"></asp:Label></td>
                        <td style="width: 220px; text-align:left">
                            <telerik:RadGrid ID="gridNonCourierPuroPostAccounts" runat="server" AllowPaging="True"
                                AllowSorting="false" AllowFilteringByColumn="false"
                                AllowAutomaticInserts="false" ShowStatusBar="false" AllowAutomaticUpdates="false"
                                OnNeedDataSource="gridNonCourierPuroPostAccounts_NeedDataSource" OnDeleteCommand="gridNonCourierPuroPostAccounts_DeleteCommand"
                                OnItemDataBound="gridNonCourierPuroPostAccounts_ItemDataBound"   OnItemCommand="gridNonCourierPuroPostAccounts_ItemCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="idEDIAccount" CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="AccountNumber" FilterControlAltText="AccountNumber" SortExpression="Team" HeaderText="Account Number" UniqueName="AccountNumber">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false">
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template">
                                        <EditColumn UniqueName="EditColumn"></EditColumn>
                                        <FormTemplate>
                                            <table id="Table2" style="padding-top: 2px; width: 100%; margin-left: 5px" border="0">
                                                <tr>
                                                    <td style="text-align: right; width: 100px; vertical-align: top">Account Number</td>
                                                    <td style="color: red; text-align: left; width: 2px; vertical-align: top">*</td>
                                                    <td>
                                                        <telerik:RadTextBox ID="txtAccountNum" runat="server" MaxLength="75" Text="" Width="75px"  />
                                                    </td>
                                                    <td style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="height: 20px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;"></td>
                                                    <td style="width: 2px;"></td>
                                                    <td align="center">
                                                        <telerik:RadButton ID="btnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                        </telerik:RadButton>
                                                        &nbsp;
                                                                            <telerik:RadButton ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel">
                                                                            </telerik:RadButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lblNonCourier210TestSent" runat="server" Text="How should initial test files be sent?" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadDropDownList ID="comboNonCourier210TestSent" runat="server" Visible="true" Width="100px">
                                <Items>
                                    <telerik:DropDownListItem Value="SFTP" Text="SFTP" Selected="true" />
                                    <telerik:DropDownListItem Value="via Email" Text="via Email" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                        <td></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lblNonCourier214TestSent" runat="server" Text="How should initial test files be sent?" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadDropDownList ID="comboNonCourier214TestSent" runat="server" Visible="true" Width="100px">
                                <Items>
                                    <telerik:DropDownListItem Value="SFTP" Text="SFTP" Selected="true" />
                                    <telerik:DropDownListItem Value="via Email" Text="via Email" />
                                </Items>
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lblNumRecipNonCourier210" runat="server" Text="Number of Invoice Recipients Requested Prod" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadNumericTextBox RenderMode="Lightweight" MinValue="0" MaxValue="10" runat ="server" ID="txtBxNumRecipNonCourier210" Width="50px" OnTextChanged="txtBxNumRecipNonCourier210_TextChanged" AutoPostBack="true"  ShowSpinButtons="false" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                            <telerik:RadButton ID="btnNumRecipNonCourier210" AutoPostBack="false" runat="server" Text="Update"></telerik:RadButton>
                        </td>
                        <td style="width: 10px"></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lblNumRecipNonCourier214" runat="server" Text="Number of Shipment Status Recipients Requested Prod" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadNumericTextBox RenderMode="Lightweight" MinValue="0" MaxValue="10" runat ="server" ID="txtBxNumRecipNonCourier214" Width="50px" OnTextChanged="txtBxNumRecipNonCourier214_TextChanged" AutoPostBack="true"  ShowSpinButtons="false" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                            <telerik:RadButton ID="btnNumRecipNonCourier214" AutoPostBack="false" runat="server" Text="Update"></telerik:RadButton>
                        </td>
                        <td ></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lblNumRecipNonCourierPuroPostStand" runat="server" Text="Number of Invoice Status Recipients Requested" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadNumericTextBox RenderMode="Lightweight" MinValue="0" MaxValue="10" runat ="server" ID="txtBxNumRecipNonCourierPuroPostStand" Width="50px" OnTextChanged="txtBxNumRecipNonCourierPuroPostStand_TextChanged" AutoPostBack="true"  ShowSpinButtons="false" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                            <telerik:RadButton ID="btnNumRecipNonCourierPuroPostStand" AutoPostBack="false" runat="server" Text="Update"></telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="vertical-align: top">
                            <asp:PlaceHolder ID="placeNonCourier210" runat="server" />
                        </td>
                        <td class="SpacerNonCourier"></td>
                        <td colspan="3" style="text-align: center; vertical-align: top">
                            <asp:PlaceHolder ID="placeNonCourier214" runat="server" />
                        </td>
                        <td></td>
                        <td colspan="3" style="text-align: center; vertical-align: top">
                            <asp:PlaceHolder ID="placePuroPostStand" runat="server" />
                        </td>
                        <asp:Literal ID="Literal1" runat="server" />
                        <asp:Literal ID="Literal2" runat="server" />
                        <asp:Literal ID="ltlCountNonCourier210" runat="server" Text="0" Visible="false" />
                        <asp:Literal ID="ltlRemovedNonCourier210" runat="server" Visible="false" />
                        <asp:Literal ID="ltlCountNonCourier214" runat="server" Text="0" Visible="false" />
                        <asp:Literal ID="ltlRemovedNonCourier214" runat="server" Visible="false" />
                        <asp:Literal ID="ltlCountPuroPostStand" runat="server" Text="0" Visible="false" />
                        <asp:Literal ID="ltlRemovedPuroPostStand" runat="server" Visible="false" />
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lblNumRecipNonCourier210Test" runat="server" Text="Number of Invoice Recipients Requested Test" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadNumericTextBox RenderMode="Lightweight" MinValue="0" MaxValue="10" runat="server" ID="txtBxNumRecipNonCourier210Test" Width="50px" OnTextChanged="txtBxNumRecipNonCourier210_TextChanged" AutoPostBack="true" ShowSpinButtons="false" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                            <telerik:RadButton ID="btnNumRecipNonCourier210Test" AutoPostBack="false" runat="server" Text="Update"></telerik:RadButton>
                        </td>
                        <td style="width: 10px"></td>
                        <td style="width: 220px; text-align: right">
                            <asp:Label ID="lblNumRecipNonCourier214Test" runat="server" Text="Number of Shipment Status Recipients Requested Test" Visible="true"></asp:Label>
                        </td>
                        <td></td>
                        <td style="width: 140px; text-align: left">
                            <telerik:RadNumericTextBox RenderMode="Lightweight" MinValue="0" MaxValue="10" runat="server" ID="txtBxNumRecipNonCourier214Test" Width="50px" OnTextChanged="txtBxNumRecipNonCourier214_TextChanged" AutoPostBack="true" ShowSpinButtons="false" NumberFormat-DecimalDigits="0"></telerik:RadNumericTextBox>
                            <telerik:RadButton ID="btnNumRecipNonCourier214Test" AutoPostBack="false" runat="server" Text="Update"></telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="vertical-align: top">
                            <asp:PlaceHolder ID="placeNonCourier210Test" runat="server" />
                        </td>
                        <td class="SpacerNonCourier"></td>
                        <td colspan="3" style="text-align: center; vertical-align: top">
                            <asp:PlaceHolder ID="placeNonCourier214Test" runat="server" />
                        </td>
                        <td></td>
                        <asp:Literal ID="ltlCountNonCourier210Test" runat="server" Text="0" Visible="false" />
                        <asp:Literal ID="ltlRemovedNonCourier210Test" runat="server" Visible="false" />
                        <asp:Literal ID="ltlCountNonCourier214Test" runat="server" Text="0" Visible="false" />
                        <asp:Literal ID="ltlRemovedNonCourier214Test" runat="server" Visible="false" />
                    </tr>
                </table>
            </telerik:RadPageView>

            <%--  NOTES  --%>
            <telerik:RadPageView runat="server" ID="notes">
                <hr />
                <table border="0">
                    <tr>
                        <td style="text-align: right;">Date
                        </td>
                        <td style="color: red;">*
                        </td>
                        <td>
                            <telerik:RadDatePicker RenderMode="Lightweight" ID="dpNoteDate" runat="server" AutoPostBack="true" Enabled="true"></telerik:RadDatePicker>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblInternalTask" runat="server" Text="Task Type" Visible="false"></asp:Label>
                        </td>
                        <td style="color: red;">
                            <asp:Label ID="lblInternalTaskAsk" runat="server" Text="*" Visible="false"></asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <telerik:RadDropDownList ID="rddlInternalType" runat="server" DefaultMessage="Select Task Type" ToolTip="Select Task Type" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="rddlInternalType_IndexChanged"></telerik:RadDropDownList>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblInternalTimeSpent" runat="server" Text="Time Spent in minutes" Visible="false"></asp:Label>
                        </td>
                        <td style="color: red; text-align: left;">
                            <asp:Label ID="lblInternalTimeSpentAsk" runat="server" Text="*" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="txtInternalTimeSpent" runat="server" MaxLength="75" Text="0" Width="100px" ToolTip="Enter Time Spent" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTaskType" runat="server" ValidationGroup="noteInfo" ControlToValidate="rddlInternalType" ErrorMessage="Select Task Type" Style="color: red"></asp:RequiredFieldValidator></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:RequiredFieldValidator ID="rfvTime" runat="server" ValidationGroup="noteInfo" ControlToValidate="txtInternalTimeSpent" ErrorMessage="Enter Time Spent" Style="color: red"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td colspan="8"></td>
                        <td style="height: 20px">
                            <asp:Label ID="lblTimeWarning" Style="color: red;" runat="server" Text="" Visible="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top">Enter Notes
                        </td>
                        <td style="color: red; text-align: left; vertical-align: top">
                            <asp:Label ID="note1ast" runat="server" Text="*" Visible="true"></asp:Label>
                        </td>
                        <td colspan="7">
                            <asp:TextBox ID="txtNotes" TextMode="multiline" Columns="100" Rows="5" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ValidationGroup="noteInfo" ControlToValidate="txtNotes" ErrorMessage="Please enter a note value" Style="color: red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                        <td colspan="7"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top">
                            <asp:Label ID="lblInternalNotes" runat="server" Text="Customer Delay Notes" Visible="false"></asp:Label>
                        </td>
                        <td style="color: red; text-align: left; vertical-align: top">
                            <asp:Label ID="note2ast" runat="server" Text="*" Visible="false"></asp:Label>
                        </td>
                        <td colspan="8">

                            <asp:TextBox ID="txtInternalNotes" TextMode="multiline" Columns="100" Rows="3" runat="server" Visible="false" ForeColor="Red" />
                            <asp:RequiredFieldValidator ID="rfvNotes2" runat="server" ValidationGroup="noteInfo" ControlToValidate="txtInternalNotes" ErrorMessage="Please enter a customer delay note" Style="color: red" Enabled="false"></asp:RequiredFieldValidator>


                        </td>
                    </tr>
                </table>

                <table style="padding-top: 2px; width: 100%;" border="0">
                    <tr>
                        <td>
                            <p style="color: red"></p>
                        </td>
                        <td style="color: blue; width: 20%; font-size: medium; text-align: right;">
                            <telerik:RadButton ID="btnSaveNotes" CausesValidation="true" ValidationGroup="noteInfo" runat="server" Text="Save Notes" OnClick="btnSaveNotes_Click" Visible="false" />
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <p style="color: red"><i>*Required Fields</i></p>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTime" runat="server" Text="Total Time Spent " Visible="false" ForeColor="Blue"></asp:Label><asp:Label ID="lblTotalTime" runat="server" Text="" Visible="false" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                </table>

                <telerik:RadGrid ID="rgNotesGrid" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowFilteringByColumn="true" OnNeedDataSource="rgNotesGrid_NeedDataSource"
                    OnDeleteCommand="rgNotesGrid_DeleteCommand" OnInsertCommand="rgNotesGrid_InsertCommand" OnUpdateCommand="rgNotesGrid_UpdateCommand" OnItemCommand="rgNotesGrid_ItemCommand" OnItemDataBound="rgNotesGrid_ItemDataBound">
                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                    <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
                    </ExportSettings>
                    <MasterTableView AutoGenerateColumns="False" AllowPaging="True" CommandItemDisplay="Top" EditMode="PopUp">
                        <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToWordButton="true" ShowExportToExcelButton="True"></CommandItemSettings>
                        <EditFormSettings UserControlName="EditNote.ascx" EditFormType="WebUserControl" PopUpSettings-Height="550px" PopUpSettings-Width="850px">
                            <FormStyle Height="500px"></FormStyle>
                            <PopUpSettings Modal="true" />
                        </EditFormSettings>
                        <Columns>
                            <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="EditLink">
                                <HeaderStyle Width="36px"></HeaderStyle>
                            </telerik:GridEditCommandColumn>
                            <telerik:GridBoundColumn DataField="idNote" FilterControlAltText="Filter idNote column" HeaderText="idNote" SortExpression="idNote" UniqueName="idNote" Visible="true" Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="idDR" FilterControlAltText="Filter idDR column" HeaderText="idDR" SortExpression="idDR" UniqueName="idDR" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="idTaskType" FilterControlAltText="Filter idTaskType column" HeaderText="idTaskType" SortExpression="idTaskType" UniqueName="idTaskType" Visible="true" Display="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TaskType" FilterControlAltText="Filter TaskType column" HeaderText="Task Type" SortExpression="TaskType" UniqueName="TaskType" Visible="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="noteDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" FilterControlAltText="Filter noteDate column" HeaderText="Note Date" SortExpression="noteDate" UniqueName="noteDate" HeaderStyle-Width="10%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="timeSpent" AllowFiltering="false" FilterControlAltText="Filter timeSpent column" HeaderText="Time Spent" SortExpression="timeSpent" UniqueName="timeSpent" HeaderStyle-Width="5%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Createdby" AllowFiltering="true" FilterControlAltText="Filter Createdby column" HeaderText="Entered By" SortExpression="Createdby" UniqueName="Createdby" HeaderStyle-Width="10%">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="publicNote" AllowFiltering="true" FilterControlAltText="Filter publicNote column" HeaderText="Notes" DataFormatString="<pre>{0}</pre>" SortExpression="publicNote" UniqueName="publicNote">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="privateNote" AllowFiltering="true" ItemStyle-ForeColor="Red" FilterControlAltText="Filter privateNote column" HeaderText="Customer Delay Notes" DataFormatString="<pre>{0}</pre>" SortExpression="privateNote" UniqueName="privateNote" HeaderStyle-Width="20%">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" HeaderText="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false" ConfirmDialogType="RadWindow" ConfirmText="Delete Note?">
                                <HeaderStyle Width="36px"></HeaderStyle>
                            </telerik:GridButtonColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                    </MasterTableView>
                </telerik:RadGrid>
            </telerik:RadPageView>

            <%-- File Uploads --%>
            <telerik:RadPageView runat="server" ID="uploads">
                <hr />
                <table border="0">
                    <tr>
                        <td style="text-align: left">
                            <telerik:RadAsyncUpload RenderMode="Lightweight" runat="server" ID="RadAsyncUpload1" HideFileInput="true"
                                AllowedFileExtensions=".jpeg,.jpg,.png,.doc,.docx,.xls,.xlsx,.pdf,.txt,.gif,.csv,.msg" OnFileUploaded="RadAsyncUpload1_FileUploaded" TargetFolder="~/FileUploads" Localization-Select="Browse"
                                OnClientFileUploaded="onClientFileUploaded" />
                        </td>
                        <td>Upload a Document of type: Gif, JPeg, Png, Doc, Docx, Xls, Xlsx, PDF, txt, msg</td>
                    </tr>
                    <tr>
                        <td colspan="2">

                            <telerik:RadButton ID="btnSaveUpload" Text="Save Files" runat="server" OnClick="btnSaveUpload_Click" Visible="true"></telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <telerik:RadGrid ID="rgUpload" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowFilteringByColumn="true" 
                                OnNeedDataSource="rgUpload_NeedDataSource" OnDeleteCommand="rgUpload_DeleteCommand" 
                                OnInsertCommand="rgUpload_InsertCommand" OnUpdateCommand="rgUpload_UpdateCommand" 
                                OnItemDataBound="rgUpload_ItemDataBound">
                                <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                                <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
                                </ExportSettings>
                                <MasterTableView AutoGenerateColumns="False" AllowPaging="True" CommandItemDisplay="Top" EditMode="PopUp">
                                    <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToWordButton="False" ShowExportToExcelButton="False"></CommandItemSettings>
                                    <EditFormSettings UserControlName="EditFileUpload.ascx" EditFormType="WebUserControl" PopUpSettings-Height="250px" PopUpSettings-Width="550px">
                                        <FormStyle Height="500px"></FormStyle>
                                        <PopUpSettings Modal="true" />
                                    </EditFormSettings>
                                    <Columns>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="EditLink">
                                            <HeaderStyle Width="36px"></HeaderStyle>
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridBoundColumn DataField="idFileUpload" FilterControlAltText="Filter idFileUpload column" HeaderText="idFileUpload" SortExpression="idFileUpload" UniqueName="idFileUpload" Visible="true" Display="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="idRequest" FilterControlAltText="Filter idRequest column" HeaderText="idRequest" SortExpression="idRequest" UniqueName="idRequest" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="UploadDate" DataType="System.DateTime" DataFormatString="{0:MM/dd/yyyy}" FilterControlAltText="Filter UploadDate column" HeaderText="Upload Date" SortExpression="UploadDate" UniqueName="UploadDate" HeaderStyle-Width="10%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Createdby" AllowFiltering="true" FilterControlAltText="Filter Createdby column" HeaderText="Uploaded By" SortExpression="Createdby" UniqueName="Createdby" HeaderStyle-Width="10%">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Description" AllowFiltering="true" FilterControlAltText="Filter Description column" HeaderText="Description" SortExpression="Description" UniqueName="Description">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ActiveFlag" AllowFiltering="true" FilterControlAltText="Filter ActiveFlag column" HeaderText="ActiveFlag" SortExpression="ActiveFlag" UniqueName="ActiveFlag" Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridHyperLinkColumn DataTextField="FilePath" Target="_parent" NavigateUrl="javascript:void(0);"
                                            AllowFiltering="true" FilterControlAltText="Filter FilePath column" HeaderText="File Path" SortExpression="FilePath" UniqueName="FilePath" ItemStyle-ForeColor="Blue">
                                        </telerik:GridHyperLinkColumn>
                                        <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" HeaderText="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false" ConfirmDialogType="RadWindow" ConfirmText="Delete?" Visible="false">
                                            <HeaderStyle Width="36px"></HeaderStyle>
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <PagerStyle AlwaysVisible="True"></PagerStyle>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </telerik:RadPageView>

        </telerik:RadMultiPage>
        <telerik:RadButton RenderMode="Lightweight" ID="btnDebugLoad" runat="server" Text="Customer" OnClick="btnPre_Load_Click" Visible="false"></telerik:RadButton>
        <telerik:RadButton RenderMode="Lightweight" ID="btnDebugLoadContactInfo" runat="server" Text="Contact Info" OnClick="btnDebugLoadContactInfo_Click" Visible="false"></telerik:RadButton>
        <telerik:RadButton RenderMode="Lightweight" ID="btnDebugLoadEDI" runat="server" Text="EDI" OnClick="btnDebugLoadEDI_Click" Visible="false"></telerik:RadButton>
        <telerik:RadButton RenderMode="Lightweight" ID="btnDebugLoadShipping" runat="server" Text="Shipping" OnClick="btnDebugLoadShipping_Click" Visible="false"></telerik:RadButton>
        <asp:TextBox ID="txtBoxMultiDebug" TextMode="multiline" Columns="100" Rows="5" runat="server" Visible="false" />
        <telerik:RadButton RenderMode="Lightweight" ID="btnClearDebug" runat="server" Text="Clear" OnClick="btnClearDebug_Click" Visible="false"></telerik:RadButton>
    </div>

</asp:Content>



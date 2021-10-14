<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CollectionSpecMaintEdit.ascx.cs" Inherits="CollectionSpecMaintEdit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
    Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
    Height="10">
</telerik:RadToolTipManager>
<telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="txtVendor">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtITBA" LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
<telerik:RadWindowManager ID="windowManager" runat="server">
</telerik:RadWindowManager>
<asp:Panel ID="Panel1" runat="server" GroupingText=''>

    <table border="0">
        <tr>
            <td style="height: 10px" colspan="4"></td>
        </tr>
        <tr>
            <td style="width: 10px;"></td>
            <td style="width: 150px;">
                <b>Collection Specialist Maintenance </b>
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblLastUpdated" runat="server" Text='Last Updated by ' Enabled="false" Visible="true">
                </asp:Label><br />
            </td>
            <td>
                <asp:Label ID="LastUpdatedBy" CssClass="alert-link" runat="server" Text='<%# Bind("UpdatedBy") %>' Visible="true"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <asp:Label ID="LastUpdatedOn" CssClass="alert-link" runat="server" Text='<%# Bind("UpdatedOn") %>' Visible="true"></asp:Label><br />
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 70px; color: red; text-align: right">*</td>
            <asp:HiddenField ID="hdnITBAID" runat="server" Value='<%# Bind("idCollectionSpecialist") %>' />
            <td>
                <asp:Label ID="lblITBA" runat="server" CssClass="rdfLabel" Text="Collection Specialst" />
            </td>
            <td>
                <telerik:RadDropDownList ID="rddlEmployee" ValidationGroup="Submitgroup" runat="server" Width="120px" DefaultMessage="Employee" ToolTip="Employee"></telerik:RadDropDownList>
            </td>
            <td>
                <asp:HiddenField ID="hdEmployeeID" runat="server" Value='<%# Bind("idEmployee") %>' />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="rddlEmployee" ErrorMessage="required" ForeColor="Red" ValidationGroup="Submitgroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 70px; color: red; text-align: right">*</td>
            <td>
                <asp:Label ID="lblEmail" runat="server" CssClass="rdfLabel" Text="Email" /></td>
            <td>
                <telerik:RadTextBox ID="txtEmail" runat="server" Text='<%# Bind("email") %>' Width="300px" ToolTip="Enter the Email Address" MaxLength="100">
                </telerik:RadTextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="required" ForeColor="Red" ValidationGroup="Submitgroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 70px; color: red; text-align: right">*</td>
            <td>
                <asp:Label ID="lblLogin" runat="server" CssClass="rdfLabel" Text="Login" /></td>
            <td>
                <telerik:RadTextBox ID="txtLogin" runat="server" Text='<%# Bind("login") %>' Width="300px" ToolTip="Enter the Login" MaxLength="100">
                </telerik:RadTextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="rfv_txtLogin" runat="server" ControlToValidate="txtLogin" ErrorMessage="required" ForeColor="Red" ValidationGroup="Submitgroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 70px; color: red; text-align: right"></td>
            <td colspan="2">Receive Email for New Discovery Requests Submitted by Sales?
                <telerik:RadButton ID="NewReqEmail" runat="server" ToggleType="CheckBox" ButtonType="LinkButton" Checked='<%# Eval("ReceiveNewReqEmail") == DBNull.Value ? true : Convert.ToBoolean(Eval("ReceiveNewReqEmail")) %>' AutoPostBack="false">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />
                    </ToggleStates>
                </telerik:RadButton>

            </td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 30px; color: red; text-align: right"></td>
            <td>Active Flag</td>
            <td>
                <telerik:RadButton ID="ActiveFlag" runat="server" ToggleType="CheckBox" ButtonType="LinkButton" Checked='<%# Eval("ActiveFlag") == DBNull.Value ? true : Convert.ToBoolean(Eval("ActiveFlag")) %>' AutoPostBack="false">
                    <ToggleStates>
                        <telerik:RadButtonToggleState Text="Yes" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                        <telerik:RadButtonToggleState Text="No" PrimaryIconCssClass="rbToggleCheckbox" />
                    </ToggleStates>
                </telerik:RadButton>
            </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                <asp:Button CssClass="btn btn-primary" ValidationGroup="Submitgroup" ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="btn btn-primary" CommandName="Cancel"></asp:Button>
            </td>
        </tr>
    </table>
    <div style="padding-left: 10px">
        <p style="color: red"><i>*Required Fields</i></p>
        <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
        <asp:Label ID="lblSuccessMessage" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
    </div>
    <style>
        .upper {
            text-transform: uppercase;
        }
    </style>
</asp:Panel>



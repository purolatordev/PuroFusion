<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditTaskType.ascx.cs" Inherits="EditTaskType" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
    </telerik:RadToolTipManager>
<telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="txtTaskType">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtTaskType"  LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
  <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
<asp:Panel ID="Panel1" runat="server" GroupingText=''>
    <%--<table>
        <tr>
            <td style="height: 10px"></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 10px;"></td>
            <td style="width: 250px;">
                <b>Task Type Maintenance</b>
            </td>
            <td style="text-align:right">
                     <asp:Label ID="lblLastUpdated" runat="server" Text='Last Updated by '  Enabled="false" Visible="true">
                     </asp:Label><br />
            </td>
            <td >
                      <asp:Label ID="lblLastUpdatedBy" CssClass="alert-link" runat="server" Text='' Visible="true"></asp:Label><br />
            </td>
        </tr>

    </table>--%>
    <table border="0" >
         <tr>
             <td style="height: 10px" colspan="4"></td>
         </tr>
            <tr>
                <td style="width: 10px;"></td>
                <td style="width: 250px;">
                    <b>Task Type Maintenance </b>
      
                </td>
                <td style="text-align:right">
                     <asp:Label ID="lblLastUpdated" runat="server" Text='Last Updated by '  Enabled="false" Visible="true">
                     </asp:Label><br />
                </td>
                <td >
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
            <td style="width: 30px; color:red; text-align:right " ></td>
             <td ><asp:Label ID="Label1" runat="server" CssClass="rdfLabel" Text="ID" />
            </td>
            <td>
                <asp:Label ID="lblTaskTypeID" runat="server" CssClass="rdfLabel" Text='<%# Bind("idTaskType") %>' />
            </td>
        </tr>
        <tr>
            <td style="width: 30px; color:red; text-align:right " >*</td>
            <td ><asp:Label ID="lblTaskType" runat="server" CssClass="rdfLabel" Text="Task Type" />
            </td>
            <td>
                <telerik:RadTextBox ID="txtTaskType" runat="server" Text='<%# Bind("TaskType") %>'  Width="300px" ToolTip="Enter the Task Type" MaxLength="100">
                </telerik:RadTextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTaskType" ErrorMessage="required" ForeColor="Red" ValidationGroup="Submitgroup"></asp:RequiredFieldValidator>
            </td>
        </tr>

         <tr>
            <td style="width: 30px; color:red; text-align:right " >*</td>
            <td ><asp:Label ID="lblOnboardingPhase" runat="server" CssClass="rdfLabel" Text="Onboarding Phase" />
            </td>
            <td>
                <telerik:RadComboBox ID="cbxOnboardingPhase" runat="server" ToolTip="Select Onboarding Phase" EmptyMessage="Select Onboarding Phase" Width="150px" AutoPostBack="true" Visible="true"></telerik:RadComboBox>
                <asp:HiddenField ID="hdnOnboardingPhase" runat="server" Value='<%# Bind("idOnboardingPhase") %>'/>
            </td>
            <td>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="cbxOnboardingPhase" ErrorMessage="required" ForeColor="Red" ValidationGroup="Submitgroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
       
        
        <tr>
            <td style="width: 30px; color:red; text-align:right " ></td>
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
            <td> </td>
            <td> </td>
            <td>
                 <asp:Button CssClass="btn btn-primary" ValidationGroup="Submitgroup" ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                 <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False"  CssClass="btn btn-primary" CommandName="Cancel"></asp:Button>  
                               
                        
            </td>
        </tr>
         
    </table>
    <div style="padding-left:10px">
        <p style="color:red"><i >*Required Fields</i></p>
                <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblSuccessMessage" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
    </div>
    <style>
        .upper{
            text-transform:uppercase;
        }
    </style>
</asp:Panel>


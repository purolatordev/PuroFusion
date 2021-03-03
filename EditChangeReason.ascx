<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditChangeReason.ascx.cs" Inherits="EditChangeReason" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
    </telerik:RadToolTipManager>
<telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="txtChangeReason">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtChangeReason"  LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
  <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
<asp:Panel ID="Panel1" runat="server" GroupingText=''>
   
    <table border="0" >
         <tr>
             <td style="height: 10px" colspan="4"></td>
         </tr>
            <tr>
                <td style="width: 10px;"></td>
                <td style="width: 250px;">
                    <b>Change Reason Maintenance </b>
      
                </td>
                <td style="text-align:right">
                     <asp:Label ID="lblLastUpdated" runat="server" Text='Last Updated by '  Enabled="false" Visible="true">
                     </asp:Label><br />
                </td>
                <td >
                      <asp:Label ID="LastUpdatedBy" CssClass="alert-link" runat="server" Text='<%# Bind("CreatedBy") %>' Visible="true"></asp:Label><br />

                </td>
            </tr>
         <tr>
             <td></td>
             <td></td>
             
             <td>
                    <asp:Label ID="lblLastUpdatedOn" CssClass="alert-link" runat="server" Text='' Visible="true"></asp:Label>
              </td>
              <td >
                      <asp:Label ID="LastUpdatedOn" CssClass="alert-link" runat="server" Text='<%# Bind("CreatedOn") %>' Visible="true"></asp:Label><br />

              </td>
         </tr>
        </table>
    <table>
        
        <tr>
            <td style="width: 30px; color:red; text-align:right " ></td>
             <td ><asp:Label ID="Label1" runat="server" CssClass="rdfLabel" Text="ID" />
            </td>
            <td>
                <asp:Label ID="lblTargetDateID" runat="server" CssClass="rdfLabel" Text='<%# Bind("idTargetDate") %>' />
            </td>
        </tr>
        <tr>
            <td style="width: 30px; color:red; text-align:right " >*</td>
            <td ><asp:Label ID="lblChangeReason" runat="server" CssClass="rdfLabel" Text="Change Reason" />
            </td>
            <td>
                <telerik:RadTextBox ID="txtChangeReason" runat="server" Text='<%# Bind("ChangeReason") %>'  Width="300px" ToolTip="Enter the ChangeReason"  MaxLength="100">
                </telerik:RadTextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtChangeReason" ErrorMessage="required" ForeColor="Red" ValidationGroup="Submitgroup"></asp:RequiredFieldValidator>
            </td>
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


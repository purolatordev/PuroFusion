<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditShippingProduct.ascx.cs" Inherits="EditShippingProduct" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
    </telerik:RadToolTipManager>
<telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rddlService">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rddlService"  LoadingPanelID="RadAjaxLoadingPanel1" />
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
                    <b>Shipping Service Maintenance </b>
      
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
                     <asp:Label ID="Label1" CssClass="alert-link" runat="server" Text='<%# Bind("UpdatedOn") %>' Visible="true"></asp:Label><br />
              </td>
         </tr>
        </table>
    <table>
        
        <tr>
            <td style="width: 30px; color:red; text-align:right " ></td>
             <td ><asp:Label ID="lblShippingProductID" runat="server" CssClass="rdfLabel" Text="ID" />
            </td>
            <td>
                <asp:Label ID="idShippingProduct" runat="server" CssClass="rdfLabel" Text='<%# Bind("idShippingProduct") %>' />
            </td>
        </tr>
        <tr>
            <td style="width: 30px; color:red; text-align:right " >*</td>
            <td ><asp:Label ID="lblshippingProduct" runat="server" CssClass="rdfLabel" Text="Shipping Product" />
            </td>
            <td>
                <telerik:RadTextBox ID="txtShippingProduct" runat="server" Text='<%# Bind("ShippingProduct") %>'  Width="300px" ToolTip="Enter the Shipping Product"  MaxLength="100">
                </telerik:RadTextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtShippingProduct" ErrorMessage="required" ForeColor="Red" ValidationGroup="Submitgroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
         <tr>
            <td style="width: 30px; color:red; text-align:right " >*</td>
            <td ><asp:Label ID="lblshippingSvc" runat="server" CssClass="rdfLabel" Text="Shipping Service Category" />
            </td>
            <td>
               <telerik:RadDropDownList ID="rddlService"  ValidationGroup="Submitgroup" runat="server" Width="120px" DefaultMessage="Proposed Service" ToolTip="Proposed Service" AutoPostBack="true"></telerik:RadDropDownList>
                
            </td>
            <td>
                 <asp:HiddenField ID="hdServieID" runat="server" Value='<%# Bind("idShippingSvc") %>' />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="rddlService" ErrorMessage="required" ForeColor="Red" ValidationGroup="Submitgroup"></asp:RequiredFieldValidator>
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

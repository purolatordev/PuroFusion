<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/LoginMaster.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContentLogin" runat="server">

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

    <h3>
    <asp:Label ID="lblLogin" runat="server" Text="PuroFusion Login "></asp:Label>
</h3>
   <div style="margin: 20px 30px 60px 10px; width:auto;  padding-left:20px">
       <table>
           <tr>
               <td style="color:red; text-align:right">*</td>
               <td> <asp:Label ID="lblUser" runat="server" Text="User Name :"></asp:Label></td>
               <td>
                   <telerik:RadTextBox ID="txtUser" runat="server"></telerik:RadTextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="User Name is Required" ControlToValidate="txtUser" ValidationGroup="LoginVG"></asp:RequiredFieldValidator>
               </td>
           </tr>
           <tr>
               <td style="color:red; text-align:right">*</td>
               <td><asp:Label ID="lblPassword" runat="server" Text="Password :"></asp:Label></td>
               <td>
                   <telerik:RadTextBox ID="txtPasswrd" runat="server" TextMode="Password"></telerik:RadTextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red" ErrorMessage="Password is Required" ControlToValidate="txtPasswrd" ValidationGroup="LoginVG"></asp:RequiredFieldValidator>
               </td>
           </tr>
           <tr>
               <td style="color:red; text-align:right"></td>
               <td>
                   <Telerik:RadButton ID="btnSubmit" runat="server" Text="Login" OnClick="btnSubmit_Click" ValidationGroup="LoginVG" CausesValidation="true" />
               </td>
               <td></td>
           </tr>
           
       </table>
      
   </div>
    
    <h3><asp:Label ID="lblInvalid" runat="server" ForeColor ="Red"></asp:Label></h3>
    
</asp:Content>

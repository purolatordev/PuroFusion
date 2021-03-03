<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="rptTopTenTimeSpent.aspx.cs" Inherits="rptTopTenTimeSpent" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="btnSubmit_Click">
                <UpdatedControls>
                   <telerik:AjaxUpdatedControl ControlID="ReportViewer1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />

                </UpdatedControls>
               
            </telerik:AjaxSetting>
          
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="40" >
    <asp:Image ID="imgLoading" Style="margin-top: 90px" runat="server" ImageUrl="~/Images/Loading640.gif" width="850px"    
        BorderWidth="0px" AlternateText="Loading" />     
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
               <script type="text/javascript">
                   var popUp;
                   function PopUpShowing(sender, eventArgs) {
                       popUp = eventArgs.get_popUp();
                       var gridWidth = sender.get_element().offsetWidth;
                       var gridHeight = sender.get_element().offsetHeight;
                       var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));
                       var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));
                       popUp.style.left = ((gridWidth - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
                       popUp.style.top = ((gridHeight - popUpHeight) / 4 + sender.get_element().offsetTop).toString() + "px";
                   }
                   function confirmCallBackFn(arg) {
                       //rad confirm returns true then trigger onclick event
                       if (arg) {
                           window.location.href = './InvoiceEntry.aspx';
                       } else {
                           window.location.href = './SearchInvoices.aspx';
                       }
                   }
                   Telerik.Web.UI.RadWindowUtils.Localization =
                                               {
                                                   "OK": "Yes",
                                                   "Cancel": "No",
                                               };
                   function onRequestStart(sender, args) {
                       if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                               ) {
                           args.set_enableAjax(false);
                       }
                   }

                   function MyValueChanging(sender, args) {
                       args.set_newValue(args.get_newValue().toUpperCase());
                   }
                   function OpenWin(invoiceId) {

                       window.open('ViewDetails.aspx?InvoiceID=' + invoiceId, 'null', 'scrollbars =1,width=1100,height=700,top=200,left=600');
                       return false;
                   }

                </script>
            </telerik:RadCodeBlock>
     <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
    <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="200px" ShowEvent="OnClick"
          TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Top Ten Customers by ITBA Time Spent</p>">
     </telerik:RadToolTip>
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
    <br />
        <div id="selectedMenu" style="padding-left:20px"><b>Top Ten Customers by Resource Allocation</b>
    <a id="HeaderLink" href="#" onclick="return false;">
       <img src="Images/help-icon16.png" />
    </a>
            <br /><i>Top Ten Customers by Resource Allocation</i>
        </div>
    <div>

    <hr />



       <table border="0">
            <tr>
                <td>Enter Begin Date</td>
                <td><telerik:RadDatePicker  runat="server" ID="dpInvoiceDate1"></telerik:RadDatePicker></td>
                <td>Enter End Date</td>
                <td><telerik:RadDatePicker  runat="server" ID="dpInvoiceDate2"></telerik:RadDatePicker></td>             
                               
           </tr>
            <tr>
                <td>Select Onboarding Specialist</td>
                <td><telerik:RadDropDownList ID="rddlITBA" runat="server" DefaultMessage="Select Onboarding Specialist" ToolTip="Select Onboarding Specialist" Visible="true"></telerik:RadDropDownList></td>
                <td></td>
                <td></td>
               
            </tr>
            <tr>
                 <td>
                    <telerik:RadButton ButtonType="StandardButton" OnClick="btnSubmit_Click" runat="server" ID="btnSubmit" Text="Run Report" ForeColor="Green" Enabled="true" AutoPostBack="true"/>
                </td>    
                <td></td>
                 <td></td>
                 <td></td>
            </tr>
        </table>       
               
          <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="Smaller" Height="550px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" style="margin-right: 0px">
         <LocalReport ReportPath="Report.rdlc">
             <DataSources>
                 <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
             </DataSources>
         </LocalReport>
     </rsweb:ReportViewer>
     <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="PPSTDataSetTableAdapters.ppst_RateTESTTableAdapter"></asp:ObjectDataSource>
           
           
    

       
        <p></p>
       
       

        </div>
</asp:Content>

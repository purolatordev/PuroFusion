<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ITBAMaintenance.aspx.cs" Inherits="ITBAMaintenance" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <link href="styles.css" rel="stylesheet" type="text/css" /> 

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
                        popUp.style.top = ((gridHeight - 375 - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
                    }
                    function onRequestStart(sender, args) {
                        if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                                ) {
                            args.set_enableAjax(false);
                        }
                    }  
                </script>
            </telerik:RadCodeBlock>

    <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
         <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgUsers">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgUsers" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>            
        </AjaxSettings>       
    </telerik:RadAjaxManager>
    <div style="width: 900px">

<telerik:RadAjaxPanel ID="pnlSuccess" runat="server" Visible="false" >
      <div class="alert alert-success" role="alert">
        <asp:Label ID="lblSuccess" Cssclass="alert-link" runat="server" ></asp:Label>
      </div>
     </telerik:RadAjaxPanel>
       <telerik:RadAjaxPanel  ID="pnlInfo" runat="server" Visible="false" >
        <div class="alert alert-info" role="alert">
           <asp:Label id="lblInfo" Cssclass="alert-link" runat="server" ></asp:Label>           
        </div>
       </telerik:RadAjaxPanel>
      <telerik:RadAjaxPanel ID="pnlDanger" runat="server" Visible="false" >
        <div class="alert alert-danger" role="alert">
          <asp:Label ID="lblDanger" runat="server" CssClass="alert-link" ></asp:Label>
        </div>
       </telerik:RadAjaxPanel>
       <telerik:RadAjaxPanel ID="pnlWarning" runat="server" LoadingPanelID="RadAjaxLoadingPanel1" Visible="false">
              <div class="alert alert-warning" role="alert">
                   <asp:Label id="lblWarning" Cssclass="alert-link" runat="server" ></asp:Label>
             </div>
        </telerik:RadAjaxPanel>

       <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="false" />

         <telerik:RadToolTip ID="RadToolTip1" runat="server" Width="200px" ShowEvent="OnClick"
          TargetControlID="HeaderLink1" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the Onboarding Specialst Maintenance Screen to maintain the list of Onboarding Specialsts<p/> ">
          </telerik:RadToolTip>

         <p></p><div><b>Onboarding Specialst Maintenance</b>
                 <a id="HeaderLink1" href="#" onclick="return false;"><img src="Images/help-icon16.png" /></a>
                 <br /><i>Use the Onboarding Specialst Maintenance Screen to maintain the list of  Onboarding Specialsts</i>
                 <hr />
               </div>        

       <%-- begin maintenance--%>

         <telerik:RadGrid ID="rgITBA" runat="server" GridLines="Both" CellSpacing="-1" AllowFilteringByColumn="true" OnNeedDataSource="rgITBA_NeedDataSource" 
             OnInsertCommand="rgITBA_InsertCommand" OnItemDataBound="rgITBA_ItemDataBound" OnUpdateCommand="rgITBA_UpdateCommand">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
            </ExportSettings>
            <ClientSettings>
                <Scrolling  AllowScroll="true" ScrollHeight="450"/>
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" AllowPaging="True" EditMode="PopUp" AllowFilteringByColumn="true" CommandItemDisplay="Top" TableLayout="Fixed" DataKeyNames="idITBA">
                <CommandItemSettings ShowAddNewRecordButton="True" ShowRefreshButton="False" ShowExportToExcelButton="True"></CommandItemSettings>
                <EditFormSettings UserControlName="EditITBA.ascx" EditFormType="WebUserControl"  PopUpSettings-Height="350px" PopUpSettings-Width="450px">
                    <FormStyle Height="500px"></FormStyle>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit" Visible="true">
                    <HeaderStyle Width="36px"></HeaderStyle></telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="idITBA" Visible="false" FilterControlAltText="Filter idITBA column" HeaderText="idITBA" SortExpression="idITBA" UniqueName="idITBA">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ITBAName" FilterControlAltText="Filter ITBAName column" HeaderText="Onboarding Specialst Name" SortExpression="ITBAName" UniqueName="ITBAName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ITBAEmail" FilterControlAltText="Filter ITBAEmail column" HeaderText="Onboarding Specialst Email" SortExpression="ITBAEmail" UniqueName="ITBAEmail">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="idEmployee" Visible="false" FilterControlAltText="Filter idEmployee column" HeaderText="idEmployee" SortExpression="idEmployee" UniqueName="idEmployee">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="UpdatedBy" FilterControlAltText="Filter UpdatedBy column" HeaderText="UpdatedBy" SortExpression="UpdatedBy" UniqueName="UpdatedBy" Visible="false">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="UpdatedOn"  DataType="System.DateTime" FilterControlAltText="Filter UpdatedOn column" HeaderText="UpdatedOn" SortExpression="UpdatedOn" UniqueName="UpdatedOn" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ReceiveNewReqEmail" AllowFiltering="true" FilterControlAltText="Filter ReceiveNewReqEmail column" HeaderText="Receive New Request Emails" SortExpression="ReceiveNewReqEmail" UniqueName="ReceiveNewReqEmail">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="login" FilterControlAltText="Filter login column" HeaderText="login" SortExpression="login" UniqueName="login" Visible="true">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="ActiveFlag" AllowFiltering="true" FilterControlAltText="Filter ActiveFlag column" HeaderText="Active Flag" SortExpression="ActiveFlag" UniqueName="ActiveFlag">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" HeaderText="Remove ITBA" Text="Remove" UniqueName="DeleteLink" ConfirmDialogType="RadWindow"
                    Resizable="false" ConfirmText="Remove From ITBA List?" Visible="false">
                    </telerik:GridButtonColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>
       <%-- end maintenance--%>

   </div>

</asp:Content>


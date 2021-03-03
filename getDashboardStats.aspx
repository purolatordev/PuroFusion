<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="getDashboardStats.aspx.cs" Inherits="getDashboardStats" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
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
                    function assignCallBackFn(arg) {
                    }
                    function OpenWin(requestid) {
                        window.open('View.aspx?requestID=' + requestid, 'null', 'scrollbars =1,width=900,height=800,top=200,left=600');
                        return false;
                    }
                    function OpenWinCY(phase) {
                        window.open('getStatsDetails.aspx?phase=' + phase, 'null', 'scrollbars =1,width=1200,height=600,top=200,left=600');
                        return false;
                    }
                    //function OpenWinCY(phase) {
                    //    window.open('getStatsDetails.aspx?phase=' + phase + '&timeframe=cy', 'null', 'scrollbars =1,width=1200,height=600,top=200,left=600');
                    //    return false;
                    //}
                </script>
            </telerik:RadCodeBlock>

    <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
          <ClientEvents OnRequestStart="onRequestStart" />
         <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgGrid">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgGrid" LoadingPanelID="RadAjaxLoadingPanel1" />
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
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="This report displays the current stats by phase for current and prior year">
          </telerik:RadToolTip>

         <p></p><div><b>Stats Dashboard Report</b>
                 <a id="HeaderLink1" href="#" onclick="return false;"><img src="Images/help-icon16.png" /></a>
                 <hr />
               </div>        

       <%-- begin --%>

        <telerik:RadGrid Width="1100px" Height="350px" ID="rgGrid" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowFilteringByColumn="false" OnNeedDataSource="rgGrid_NeedDataSource" 
            OnItemDataBound="rgGrid_ItemDataBound" PageSize="10" AllowSorting="true" ShowFooter="true">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false" ></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
            </ExportSettings>
            <ClientSettings >
                <Scrolling AllowScroll="True" ScrollHeight="500px"></Scrolling>
                <ClientEvents OnPopUpShowing="PopUpShowing" />
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" AllowPaging="True" AllowFilteringByColumn="false" CommandItemDisplay="Top" TableLayout="Fixed" >
                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="False" ShowExportToExcelButton="True"></CommandItemSettings>                
                <Columns>
                    <telerik:GridBoundColumn DataField="idOnboardingPhase" FilterControlAltText="Filter idOnboardingPhase column" HeaderText="idOnboardingPhase" SortExpression="idOnboardingPhase" UniqueName="idOnboardingPhase" Visible="true" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="OnboardingPhase" FilterControlAltText="Filter OnboardingPhase column" HeaderText="Onboarding Phase" SortExpression="OnboardingPhase" UniqueName="OnboardingPhase">
                    </telerik:GridBoundColumn>
                  
                   
                    <telerik:GridHyperLinkColumn DataTextField="NumberRequests" Target="_parent" NavigateUrl="javascript:void(0);"
                         AllowFiltering="true" FilterControlAltText="Filter NumberRequests column" HeaderText="NumberRequests" SortExpression="NumberRequests" UniqueName="NumberRequests" ItemStyle-ForeColor="Blue">
                    </telerik:GridHyperLinkColumn>

                     <telerik:GridNumericColumn  DataField="ProjectedRevenue" DataType="System.Decimal" DataFormatString="{0:C}" FilterControlAltText="Filter ProjectedRevenue column" HeaderText="Projected Revenue" SortExpression="ProjectedRevenue" UniqueName="ProjectedRevenue" Aggregate="Sum">
                    </telerik:GridNumericColumn>

                   <%--  <telerik:GridHyperLinkColumn DataTextField="NumberRequestsPY" Target="_parent" NavigateUrl="javascript:void(0);"
                         AllowFiltering="true" FilterControlAltText="Filter NumberRequestsPY column" HeaderText="NumberRequestsPY" SortExpression="NumberRequestsPY" UniqueName="NumberRequestsPY" ItemStyle-ForeColor="Blue">
                    </telerik:GridHyperLinkColumn>

                     <telerik:GridNumericColumn  DataField="ProjectedRevenuePY" DataType="System.Decimal" DataFormatString="{0:C}" FilterControlAltText="Filter ProjectedRevenuePY column" HeaderText="ProjectedRevenuePY" SortExpression="ProjectedRevenuePY" UniqueName="ProjectedRevenuePY" Aggregate="Sum">
                    </telerik:GridNumericColumn>--%>

                    

                </Columns>
                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>
                <i>
            Kickoff, Discovery, System Solution Determination, Implementation, and GO-Live shows all requests in the system within those phases.<br />
            Stabiliazation shows all requests that have an Actual Go-Live Date within the current year.<br />
            On Hold and Closed shows those requests that have been created within the current year and subsequently closed or placed on hold.

        </i>
       <%-- end --%>

   </div>
   
</asp:Content>


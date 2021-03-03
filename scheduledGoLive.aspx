<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="scheduledGoLive.aspx.cs" Inherits="scheduledGoLive" %>
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
   <%-- <div style="width: 1100px">--%>
    <div>

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
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="This report displays the upcoming customers who are scheduled to go live.">
          </telerik:RadToolTip>

         <p></p><div><b>Scheduled Go-Live Customers</b>
                 <a id="HeaderLink1" href="#" onclick="return false;"><img src="Images/help-icon16.png" /></a>
                <%-- <br /><i>This report displays the upcoming customers who are scheduled to go live.</i>--%>
                 <hr />
               </div>        

       <%-- begin --%>

        <%-- <telerik:RadGrid ID="rgGrid" runat="server" GridLines="Both" CellSpacing="-1" AllowFilteringByColumn="true" OnNeedDataSource="rgGrid_NeedDataSource" AllowSorting="true" OnItemCreated="rgGrid_ItemCreated">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
            </ExportSettings>
            <ClientSettings>
                <Scrolling  AllowScroll="true" ScrollHeight="450"/>
            </ClientSettings>--%>
        <telerik:RadGrid ID="rgGrid" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowFilteringByColumn="true" OnNeedDataSource="rgGrid_NeedDataSource" 
            OnItemDataBound="rgGrid_ItemDataBound" OnItemCommand="rgGrid_ItemCommand" OnItemCreated="rgGrid_ItemCreated" PageSize="10" AllowSorting="true">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false" ></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
            </ExportSettings>
            <ClientSettings >
                <Scrolling AllowScroll="True" ScrollHeight="500px"></Scrolling>
                <ClientEvents OnPopUpShowing="PopUpShowing" />
            </ClientSettings>
             <HeaderStyle Width="200px"></HeaderStyle>
            <MasterTableView AutoGenerateColumns="False" AllowPaging="True" AllowFilteringByColumn="true" CommandItemDisplay="Top" TableLayout="Fixed" >
                <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="False" ShowExportToExcelButton="True"></CommandItemSettings>                
                <Columns>
                     <telerik:GridBoundColumn DataField="idRequest" FilterControlAltText="Filter isNeidRequestwRequest column" HeaderText="idRequest" SortExpression="idRequest" UniqueName="idRequest" Visible="true" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="TargetGoLive" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" EnableRangeFiltering="true" FilterControlAltText="Filter TargetGoLive column" HeaderText="Target Go-Live" SortExpression="TargetGoLive" UniqueName="TargetGoLive">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn  DataField="CurrentGoLive" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" EnableRangeFiltering="true" FilterControlAltText="Filter CurrentGoLive column" HeaderText="Current Go-Live" SortExpression="CurrentGoLive" UniqueName="CurrentGoLive">
                    </telerik:GridDateTimeColumn>
                     <telerik:GridDateTimeColumn  DataField="ActualGoLive" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" EnableRangeFiltering="true" FilterControlAltText="Filter ActualGoLive column" HeaderText="Actual Go-Live" SortExpression="ActualGoLive" UniqueName="ActualGoLive">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridHyperLinkColumn DataTextField="CustomerName" Target="_parent" NavigateUrl="javascript:void(0);"
                         AllowFiltering="true" FilterControlAltText="Filter CustomerName column" HeaderText="Customer Name" SortExpression="CustomerName" UniqueName="CustomerName" ItemStyle-ForeColor="Blue">
                    </telerik:GridHyperLinkColumn>
                    <%--<telerik:GridBoundColumn DataField="OnboardingPhase" FilterControlAltText="Filter OnboardingPhase column" HeaderText="Onboarding Phase" SortExpression="OnboardingPhase" UniqueName="OnboardingPhase">
                    </telerik:GridBoundColumn>--%>
                    <telerik:GridBoundColumn DataField="OnboardingPhase" AllowFiltering="true" FilterControlAltText="Filter OnboardingPhase column" HeaderText="Onboarding Phase" SortExpression="OnboardingPhase" UniqueName="OnboardingPhase">
                      <FilterTemplate>
                            <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBox1" DataSourceID="SqlDataSource1" DataTextField="OnboardingPhase"
                                DataValueField="OnboardingPhase" Width="100px" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("OnboardingPhase").CurrentFilterValue %>'
                                runat="server" OnClientSelectedIndexChanged="SourceIndexChanged1">
                                <Items>
                                    <telerik:RadComboBoxItem Text="All" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
                                <script type="text/javascript">
                                    function SourceIndexChanged1(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        tableView.filter("OnboardingPhase", args.get_item().get_value(), "EqualTo");
                                    }
                                </script>
                            </telerik:RadScriptBlock>
                              </FilterTemplate>   
                </telerik:GridBoundColumn>
                    <%--<telerik:GridBoundColumn DataField="ShippingChannel" FilterControlAltText="Filter ShippingChannel column" HeaderText="Shipping Channel" SortExpression="ShippingChannel" UniqueName="ShippingChannel">
                    </telerik:GridBoundColumn>--%>
                     <telerik:GridBoundColumn DataField="ShippingChannel" AllowFiltering="true" FilterControlAltText="Filter ShippingChannel column" HeaderText="Shipping Channel" SortExpression="ShippingChannel" UniqueName="ShippingChannel">
                      <FilterTemplate>
                            <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBox2" DataSourceID="SqlDataSource2" DataTextField="ShippingChannel"
                                DataValueField="ShippingChannel" Width="100px" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("ShippingChannel").CurrentFilterValue %>'
                                runat="server" OnClientSelectedIndexChanged="SourceIndexChanged2">
                                <Items>
                                    <telerik:RadComboBoxItem Text="All" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                <script type="text/javascript">
                                    function SourceIndexChanged2(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        tableView.filter("ShippingChannel", args.get_item().get_value(), "EqualTo");
                                    }
                                </script>
                            </telerik:RadScriptBlock>
                              </FilterTemplate>   
                </telerik:GridBoundColumn>

                     <telerik:GridNumericColumn  HeaderStyle-Width="18%" DataField="ProjectedRevenue" DataType="System.Decimal" DataFormatString="{0:C}" FilterControlAltText="Filter ProjectedRevenue column" HeaderText="Projected Revenue" SortExpression="ProjectedRevenue" UniqueName="ProjectedRevenue">
                    </telerik:GridNumericColumn>
                     <telerik:GridBoundColumn DataField="SalesRepName" FilterControlAltText="Filter SalesRepName column" HeaderText="SalesRepName" SortExpression="SalesRepName" UniqueName="SalesRepName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ITBA" AllowFiltering="true" FilterControlAltText="Filter ITBA column" HeaderText="Onboarding Specialist" SortExpression="ITBA" UniqueName="ITBA">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="TimeSpent" AllowFiltering="true" FilterControlAltText="Filter TimeSpent column" HeaderText="Time Recorded" SortExpression="TimeSpent" UniqueName="TimeSpent">
                    </telerik:GridBoundColumn>

                </Columns>
                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>
       <%-- end --%>

   </div>
     <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:PuroTouchDBSQLConnectionString %>"
        ProviderName="System.Data.SqlClient" SelectCommand="select distinct SortValue,OnboardingPhase from tblOnboardingPhase order by SortValue"
        runat="server"></asp:SqlDataSource> 
    <asp:SqlDataSource ID="SqlDataSource2" ConnectionString="<%$ ConnectionStrings:PuroTouchDBSQLConnectionString %>"
        ProviderName="System.Data.SqlClient" SelectCommand="select distinct ShippingChannel from tblShippingChannel order by ShippingChannel"
        runat="server"></asp:SqlDataSource> 
</asp:Content>
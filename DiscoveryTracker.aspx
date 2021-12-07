<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DiscoveryTracker.aspx.cs" Inherits="DiscoveryTracker" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">



    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rgRequests">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="rgRequests" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                    <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                    <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadPersistenceManager ID="RadPersistenceManager1" runat="server">
        <PersistenceSettings>
            <telerik:PersistenceSetting ControlID="rgRequests" />
        </PersistenceSettings>
    </telerik:RadPersistenceManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="40">
        <asp:Image ID="imgLoading" Style="margin-top: 90px" runat="server" ImageUrl="~/Images/Loading640.gif" Height="250px"
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
                popUp.style.top = ((gridHeight - 375 - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
            }
            function OpenWin(requestid) {
                window.open('View.aspx?requestID=' + requestid, 'null', 'scrollbars =1,width=900,height=800,top=200,left=600');
                return false;
            }
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                ) {
                    args.set_enableAjax(false);
                }
            }
            function callBackFn(arg) {
                if (arg == true) {

                }
            }

        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="windowManager" runat="server" Width="500px" Height="900px" Behaviors="Move,Close">
    </telerik:RadWindowManager>
    <telerik:RadToolTip ID="RadToolTip2" runat="server" Width="300px" ShowEvent="OnClick"
        TargetControlID="HeaderLink" IsClientID="true" HideEvent="LeaveToolTip" Position="Center"
        Animation="Resize" ShowDelay="0" RelativeTo="Element"
        Text="Use the Filters to Search on Any Column. </p>Data in the grid can be exported to Excel using the <i>Excel</i> icon at the top right corner of the data grid. ">
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
    <div id="selectedMenu" style="padding-left: 20px">
        <b>Discovery Tracker</b>
        <a id="HeaderLink" href="#" onclick="return false;">
            <img src="Images/help-icon16.png" />
        </a>
        <br />
        <i>The Discovery Trackers Shows all Current Discovery Requests </i>
    </div>
    <br />

    <h5>
        <asp:Label ID="lblGridtitle" runat="server" ForeColor="Blue" Font-Bold="true" Font-Size="Medium" Visible="true" Text="Discovery Tracker List"></asp:Label></h5>
    <div>
        <telerik:RadGrid ID="rgRequests" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowFilteringByColumn="true" OnNeedDataSource="rgRequests_NeedDataSource"
            OnItemDataBound="rgRequests_ItemDataBound" OnItemCommand="rgRequests_ItemCommand" OnDeleteCommand="rgRequests_DeleteCommand" PageSize="10" AllowSorting="true">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
            </ExportSettings>
            <ClientSettings>
                <Scrolling AllowScroll="True" ScrollHeight="500px"></Scrolling>
                <ClientEvents OnPopUpShowing="PopUpShowing" />
            </ClientSettings>
            <HeaderStyle Width="120px"></HeaderStyle>
            <MasterTableView AutoGenerateColumns="False" AllowPaging="True" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage" DataKeyNames="idRequest" Width="100%" TableLayout="Fixed" EditMode="PopUp">
                <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToExcelButton="True"></CommandItemSettings>
                <Columns>
                    <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditRequest" ImageUrl="~/Images/Grid/Edit.gif"
                                CommandArgument='<%# Eval("idRequest") %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                    </telerik:GridTemplateColumn>
                    <%--<telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                        <HeaderStyle Width="36px"></HeaderStyle></telerik:GridEditCommandColumn>--%>
                    <telerik:GridBoundColumn DataField="idRequest" FilterControlAltText="Filter isNeidRequestwRequest column" HeaderText="idRequest" SortExpression="idRequest" UniqueName="idRequest" Visible="true" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RequestType" FilterControlAltText="Filter RequestType column" HeaderText="Request Type" SortExpression="RequestType" UniqueName="RequestType">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="SolutionType" FilterControlAltText="Filter SolutionType column" HeaderText="Solution Type" SortExpression="SolutionType" UniqueName="SolutionType">
                    </telerik:GridBoundColumn>
                    <telerik:GridHyperLinkColumn DataTextField="CustomerName" Target="_parent" NavigateUrl="javascript:void(0);"
                        AllowFiltering="true" FilterControlAltText="Filter CustomerName column" HeaderText="Customer Name" SortExpression="CustomerName" UniqueName="CustomerName" ItemStyle-ForeColor="Blue">
                    </telerik:GridHyperLinkColumn>
                    <telerik:GridBoundColumn DataField="SalesRepName" FilterControlAltText="Filter SalesRepName column" HeaderText="Sales Professional" SortExpression="SalesRepName" UniqueName="SalesRepName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProjectedRevenue" DataType="System.Decimal" DataFormatString="{0:C}" FilterControlAltText="Filter ProjectedRevenue column" HeaderText="Projected Revenue" SortExpression="ProjectedRevenue" UniqueName="ProjectedRevenue">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ITBA" FilterControlAltText="Filter ITBA column" HeaderText="Onboarding Specialist" SortExpression="ITBA" UniqueName="ITBA">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="OnboardingPhase" FilterControlAltText="Filter OnboardingPhase column" HeaderText="Onboarding Phase" SortExpression="OnboardingPhase" UniqueName="OnboardingPhase">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VendorType" FilterControlAltText="Filter VendorType column" HeaderText="Vendor Type" SortExpression="VendorType" UniqueName="VendorType">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VendorName" FilterControlAltText="Filter VendorName column" HeaderText="Vendor Name" SortExpression="VendorName" UniqueName="VendorName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ShippingChannel" FilterControlAltText="Filter ShippingChannel column" HeaderText="Shipping Channel" SortExpression="ShippingChannel" UniqueName="ShippingChannel">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ProposedCustoms" FilterControlAltText="Filter ProposedCustoms column" HeaderText="Customs" SortExpression="ProposedCustoms" UniqueName="ProposedCustoms">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EDISpecialistName" FilterControlAltText="Filter EDISpecialistName column" HeaderText="EDI Specialist" SortExpression="EDISpecialistName" UniqueName="EDISpecialistName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EDIOnboardingPhaseType" FilterControlAltText="Filter EDIOnboardingPhaseType column" HeaderText="EDI Onboarding Phase" SortExpression="EDIOnboardingPhaseType" UniqueName="EDIOnboardingPhaseType">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TotalTimeSpent" FilterControlAltText="Filter TotalTimeSpent column" HeaderText="Time Spent (HH:MM)" SortExpression="TotalTimeSpent" UniqueName="TotalTimeSpent">
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="TargetGoLive" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter TargetGoLive column" HeaderText="Target Go-Live" SortExpression="TargetGoLive" UniqueName="TargetGoLive" EnableTimeIndependentFiltering="true">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="CurrentGoLive" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter CurrentGoLive column" HeaderText="Current Go-Live" SortExpression="CurrentGoLive" UniqueName="CurrentGoLive" EnableTimeIndependentFiltering="true">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="ActualGoLive" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter ActualGoLive column" HeaderText="Acutal Go-Live" SortExpression="ActualGoLive" UniqueName="ActualGoLive" EnableTimeIndependentFiltering="true">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="EDITargetGoLive" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter EDITargetGoLive column" HeaderText="EDI Target Go-Live" SortExpression="EDITargetGoLive" UniqueName="EDITargetGoLive" EnableTimeIndependentFiltering="true">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="EDICurrentGoLive" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter EDICurrentGoLive column" HeaderText="EDI Current Go-Live" SortExpression="EDICurrentGoLive" UniqueName="EDICurrentGoLive" EnableTimeIndependentFiltering="true">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="EDIActualGoLive" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter EDIActualGoLive column" HeaderText="EDI Actual Go-Live" SortExpression="EDIActualGoLive" UniqueName="EDIActualGoLive" EnableTimeIndependentFiltering="true">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="NewRequestYesNo" FilterControlAltText="Filter NewRequestYesNo column" HeaderText="New Request?" SortExpression="NewRequestYesNo" UniqueName="NewRequestYesNo" Visible="false">
                        <HeaderStyle Width="50px"></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="WorldpakYesNo" FilterControlAltText="Filter WorldpakYesNo column" HeaderText="Worldpak?" SortExpression="WorldpakYesNo" UniqueName="WorldpakYesNo" Visible="false">
                        <HeaderStyle Width="50px"></HeaderStyle>
                    </telerik:GridBoundColumn>
                    <telerik:GridDateTimeColumn DataField="CreatedOn" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter CreatedOn column" HeaderText="Submitted Date" SortExpression="CreatedOn" UniqueName="CreatedOn" EnableTimeIndependentFiltering="true">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridDateTimeColumn DataField="UpdatedOn" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter UpdatedOn column" HeaderText="Last Updated" SortExpression="UpdatedOn" UniqueName="UpdatedOn" EnableTimeIndependentFiltering="true">
                    </telerik:GridDateTimeColumn>
                    <telerik:GridBoundColumn DataField="UpdatedBy" FilterControlAltText="Filter UpdatedBy column" HeaderText="Last Updated By" SortExpression="UpdatedBy" UniqueName="UpdatedBy">
                    </telerik:GridBoundColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" HeaderText="Delete" FilterControlAltText="Filter DeleteColumn column" Text="Delete" UniqueName="DeleteLink" Resizable="false" ConfirmDialogType="RadWindow" ConfirmText="Delete Request?">
                        <HeaderStyle Width="50px"></HeaderStyle>
                    </telerik:GridButtonColumn>
                    <%--  <telerik:GridBoundColumn DataField="Source" AllowFiltering="true" FilterControlAltText="Filter Source column" HeaderText="Source" SortExpression="Source" UniqueName="Source">
                         <FilterTemplate>
                            <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxSource" DataSourceID="SqlDataSource1" DataTextField="Source"
                                DataValueField="Source" Width="100px" AppendDataBoundItems="true" SelectedValue='<%# ((GridItem)Container).OwnerTableView.GetColumn("Source").CurrentFilterValue %>'
                                runat="server" OnClientSelectedIndexChanged="SourceIndexChanged">
                                <Items>
                                    <telerik:RadComboBoxItem Text="All" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server">
                                <script type="text/javascript">
                                    function SourceIndexChanged(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        tableView.filter("Source", args.get_item().get_value(), "EqualTo");
                                    }
                                </script>
                            </telerik:RadScriptBlock>
                        </FilterTemplate>
                    </telerik:GridBoundColumn>--%>
                </Columns>

                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>
        <%--  <asp:SqlDataSource ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:RevenueSQLConnectionString %>"
        ProviderName="System.Data.SqlClient" SelectCommand="<%$ appSettings:SourceSQL %>"
        runat="server"></asp:SqlDataSource>--%>
        <br />
    </div>
    <br />

</asp:Content>



<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="rgHomeGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="rgHomeGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="pnlSuccess" />
                        <telerik:AjaxUpdatedControl ControlID="pnlWarning" />
                        <telerik:AjaxUpdatedControl ControlID="pnlDanger" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

            </AjaxSettings>
        </telerik:RadAjaxManager>
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
                $telerik.getViewPortSize = function () {
                    var width = 0;
                    var height = 0;
                    var canvas = document.body;
                    if ((!$telerik.quirksMode && !$telerik.isSafari) ||
                        (Telerik.Web.Browser.chrome && Telerik.Web.Browser.version >= 61)) {
                        canvas = document.documentElement;
                    }

                    if (window.innerWidth) {
                        // Seems there's no completely reliable way to get the viewport size in Gecko, this should be the best one
                        // Check https://bugzilla.mozilla.org/show_bug.cgi?id=189112#c7
                        width = Math.max(document.documentElement.clientWidth, document.body.clientWidth);
                        height = Math.max(document.documentElement.clientHeight, document.body.clientHeight);
                        if (width > window.innerWidth)
                            width = document.documentElement.clientWidth;
                        if (height > window.innerHeight)
                            height = document.documentElement.clientHeight;
                    }
                    else {
                        width = canvas.clientWidth;
                        height = canvas.clientHeight;
                    }
                    width += canvas.scrollLeft;
                    height += canvas.scrollTop;

                    if ($telerik.isMobileSafari) {
                        width += window.pageXOffset;
                        height += window.pageYOffset;
                    }
                    return { width: width - 6, height: height - 6 };
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadWindowManager ID="windowManager" runat="server">
        </telerik:RadWindowManager>
    </div>
    <div style="width: 100%">

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


    <div class="container-fluid">

        <table>
            <tr>
                <td>
                    <div class="left">
                        <telerik:RadHtmlChart runat="server" ID="ColumnChart" Width="500" Height="450" Transitions="true">
                            <PlotArea>
                                <Series>
                                    <telerik:ColumnSeries Name="Legend" Stacked="false" Gap="1.0" Spacing="0.4" DataFieldY="Rev">
                                        <LabelsAppearance DataFormatString="$#,###,###,##0 " Position="OutsideEnd">
                                        </LabelsAppearance>
                                        <TooltipsAppearance DataFormatString="$#,###,###,##0" Color="White">
                                            <ClientTemplate>
                                   #=dataItem.MonthName#
                                            </ClientTemplate>
                                        </TooltipsAppearance>

                                    </telerik:ColumnSeries>

                                </Series>
                                <XAxis AxisCrossingValue="0" DataLabelsField="MonthName" Color="Black" MajorTickType="Outside" MinorTickType="Outside"
                                    Reversed="false">

                                    <LabelsAppearance DataFormatString="$#,###,###,##0" RotationAngle="0" Skip="0" Step="1" Color="Black">
                                        <TextStyle FontSize="9" />
                                    </LabelsAppearance>
                                    <TitleAppearance Position="Center" RotationAngle="0" Text="Go-Live Month">
                                    </TitleAppearance>
                                </XAxis>
                                <YAxis AxisCrossingValue="0" Color="Black" MajorTickSize="1" MajorTickType="Outside"
                                    MinorTickType="None" Reversed="false">
                                    <LabelsAppearance DataFormatString="$#,###,###,##0" RotationAngle="0" Skip="0" Step="1" Color="Black">
                                    </LabelsAppearance>

                                    <TitleAppearance Position="Center" RotationAngle="0" Text="Annualized Projected Revenue">
                                    </TitleAppearance>
                                </YAxis>
                            </PlotArea>
                            <ChartTitle Text="Annualized Revenue by Go-Live Month">
                                <Appearance Align="Center" BackgroundColor="Transparent" Position="Bottom">
                                    <TextStyle Color="#4b6c9e" Bold="true" />
                                </Appearance>
                            </ChartTitle>
                            <Legend>
                                <Appearance Visible="false"></Appearance>
                            </Legend>

                        </telerik:RadHtmlChart>

                    </div>
                </td>
                <td>
                    <div class="right">
                        <telerik:RadHtmlChart runat="server" ID="PieChart1" Width="500" Height="450" Transitions="true">

                            <ChartTitle Text="# Requests by Onboarding Phase">
                                <Appearance Align="Center" BackgroundColor="Transparent" Position="Bottom">
                                    <TextStyle Color="#4b6c9e" Bold="true" />
                                </Appearance>
                            </ChartTitle>

                            <PlotArea>

                                <Series>

                                    <telerik:PieSeries DataFieldY="Amount" NameField="Desc" ExplodeField="IsExploded">

                                        <LabelsAppearance Position="OutsideEnd" DataFormatString="{0:###,###}" Visible="false">
                                        </LabelsAppearance>


                                        <TooltipsAppearance DataFormatString="{0}" Color="White">
                                            <ClientTemplate>

                                   #=dataItem.Desc# (#=dataItem.Amount#)

                                            </ClientTemplate>
                                        </TooltipsAppearance>

                                    </telerik:PieSeries>

                                </Series>

                            </PlotArea>
                            <Legend>
                                <Appearance Position="Right" Width="100">
                                </Appearance>

                            </Legend>
                        </telerik:RadHtmlChart>
                    </div>
                </td>
            </tr>
        </table>




    </div>

    <telerik:RadButton ID="btnNewRequest" CausesValidation="true" runat="server" Text="New Discovery Request" OnClick="btnNewRequest_Click" />





    <table>
        <tr>
            <td>
                <asp:Label ID="lblMGR" runat="server" ForeColor="#4b6c9e" Font-Bold="true" Font-Size="Medium" Visible="false"></asp:Label>
            </td>
            <td>
                <telerik:RadDropDownList ID="rddlITBA" runat="server" DefaultMessage="Select Onboarding Specialist" ToolTip="Select Onboarding Specialist" Visible="false"></telerik:RadDropDownList>
            </td>
            <td>
                <telerik:RadButton ID="btnAssign" CausesValidation="true" runat="server" Text="Assign" OnClick="btnAssign_Click" Visible="false" />
            </td>
            <td></td>
        </tr>
    </table>

    <h5>
        <asp:Label ID="lblGridtitle" runat="server" ForeColor="#4b6c9e" Font-Bold="true" Font-Size="Medium"></asp:Label></h5>
    <div>
        <telerik:RadGrid ID="rgHomeGrid1" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowFilteringByColumn="true" OnNeedDataSource="rgHomeGrid1_NeedDataSource"
            OnItemCommand="rgHomeGrid1_ItemCommand">
            <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
            </ExportSettings>
            <MasterTableView AutoGenerateColumns="False" AllowPaging="True" CommandItemDisplay="Top">
                <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToExcelButton="True"></CommandItemSettings>
                <Columns>
                    <telerik:GridBoundColumn DataField="VendorName" FilterControlAltText="Filter VendorName column" HeaderText="VendorName" SortExpression="VendorName" UniqueName="VendorName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Account" FilterControlAltText="Filter Account column" HeaderText="Account" SortExpression="Account" UniqueName="Account">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ServiceDesc" FilterControlAltText="Filter ServiceDesc column" HeaderText="ServiceDesc" SortExpression="ServiceDesc" UniqueName="ServiceDesc">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="InvoiceAmt" DataFormatString="{0:$#,###,###.00}" DataType="System.Decimal" FilterControlAltText="Filter InvoiceAmt column" HeaderText="InvoiceAmt" SortExpression="InvoiceAmt" UniqueName="InvoiceAmt">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ItemAmountTotal" DataFormatString="{0:$#,###,###.00}" AllowFiltering="false" DataType="System.Decimal" FilterControlAltText="Filter ItemAmountTotal column" HeaderText="ItemAmountTotal" SortExpression="ItemAmountTotal" UniqueName="ItemAmountTotal">
                    </telerik:GridBoundColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <telerik:RadGrid ID="rgRequests" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" AllowFilteringByColumn="true" OnNeedDataSource="rgRequests_NeedDataSource"
        OnItemDataBound="rgRequests_ItemDataBound" OnItemCommand="rgRequests_ItemCommand" PageSize="10" AllowSorting="true">
        <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
        <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
        </ExportSettings>
        <ClientSettings>
            <Scrolling AllowScroll="True" UseStaticHeaders="true" ScrollHeight="400px"></Scrolling>
            <ClientEvents OnPopUpShowing="PopUpShowing" />
        </ClientSettings>
        <HeaderStyle Width="110px"></HeaderStyle>
        <MasterTableView AutoGenerateColumns="False" AllowPaging="True" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage" DataKeyNames="idRequest" TableLayout="Fixed">
            <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="False" ShowExportToExcelButton="True"></CommandItemSettings>
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Edit" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditRequest" ImageUrl="~/Images/Grid/Edit.gif"
                            CommandArgument='<%# Eval("idRequest") %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="36px"></HeaderStyle>
                </telerik:GridTemplateColumn>
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
                <telerik:GridBoundColumn DataField="ProposedCustoms" FilterControlAltText="Filter Customs column" HeaderText="Customs" SortExpression="ProposedCustoms" UniqueName="ProposedCustoms">
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
                <telerik:GridDateTimeColumn DataField="ActualGoLive" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter ActualGoLive column" HeaderText="Actual Go-Live" SortExpression="ActualGoLive" UniqueName="ActualGoLive" EnableTimeIndependentFiltering="true">
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="EDITargetGoLive" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter EDITargetGoLive column" HeaderText="EDI Target Go-Live" SortExpression="EDITargetGoLive" UniqueName="EDITargetGoLive" EnableTimeIndependentFiltering="true">
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="EDICurrentGoLive" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter EDICurrentGoLive column" HeaderText="EDI Current Go-Live" SortExpression="EDICurrentGoLive" UniqueName="EDICurrentGoLive" EnableTimeIndependentFiltering="true">
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="EDIActualGoLive" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter EDIActualGoLive column" HeaderText="EDI Actual Go-Live" SortExpression="EDIActualGoLive" UniqueName="EDIActualGoLive" EnableTimeIndependentFiltering="true">
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="NewRequestYesNo" FilterControlAltText="Filter NewRequestYesNo column" HeaderText="New Relationship?" SortExpression="NewRequestYesNo" UniqueName="NewRequestYesNo" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="CreatedOn" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter CreatedOn column" HeaderText="Submitted Date" SortExpression="CreatedOn" UniqueName="CreatedOn" EnableTimeIndependentFiltering="true">
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="UpdatedOn" Visible="true" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" FilterControlAltText="Filter UpdatedOn column" HeaderText="Last Updated" SortExpression="UpdatedOn" UniqueName="UpdatedOn" EnableTimeIndependentFiltering="true">
                </telerik:GridDateTimeColumn>
                <telerik:GridBoundColumn DataField="UpdatedBy" FilterControlAltText="Filter UpdatedBy column" HeaderText="Last Updated By" SortExpression="UpdatedBy" UniqueName="UpdatedBy">
                </telerik:GridBoundColumn>
            </Columns>
            <PagerStyle AlwaysVisible="True"></PagerStyle>
        </MasterTableView>
    </telerik:RadGrid>
    <br />
    <%-- <style>
        div.left {
            float: left;
            padding-left:initial;
            height: 400px;
            width: 500px;
            padding-top:40px;
        }

        div.right {
            float: right;
            padding-right:30px;
            height: 400px;
            width: 550px;
            padding-top:40px;
        }

        .container {
            height: 500px;
            width: 1200px;
        }
    </style>--%>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ChangeReasonMaintenance.aspx.cs" Inherits="ChangeReasonMaintenance" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
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
                        popUp.style.top = ((gridHeight - 175 - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
                    }
                    function onRequestStart(sender, args) {
                        if (args.get_eventTarget().indexOf("ExportToExcelButton") >= 0
                                ) {
                            args.set_enableAjax(false);
                        }
                    }
                    function assignCallBackFn(arg) {
                    }
                </script>
            </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
    </div>
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
   
 
    <div class="container">
    <div>
       <h5><asp:Label ID="lblGridtitle" runat="server" ForeColor ="#4b6c9e" Font-Bold="true" Font-Size="Medium"></asp:Label></h5>
    <div>
       <telerik:RadGrid ID="rgGrid" runat="server" CellSpacing="-1" GridLines="Both" GroupPanelPosition="Top" ShowFooter="True" 
            OnNeedDataSource="rgGrid_NeedDataSource" AllowFilteringByColumn="True" OnItemCommand="rgGrid_ItemCommand"
             OnUpdateCommand="rgGrid_UpdateCommand"
             AllowSorting="true">
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ClientSettings>
                <ClientEvents OnPopUpShowing="PopUpShowing" />
            </ClientSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True" Excel-Format="ExcelML">
                <Excel Format="ExcelML"></Excel>
            </ExportSettings>
       <MasterTableView  AutoGenerateColumns="False" AllowPaging="True" EditMode="PopUp" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnFirstPage" DataKeyNames="idTargetDate" PageSize="25">
                <CommandItemSettings ShowExportToExcelButton="True" ShowRefreshButton="False" />
                <EditFormSettings UserControlName="EditChangeReason.ascx" EditFormType="WebUserControl"  PopUpSettings-Height="350px" PopUpSettings-Width="500px">
                    <FormStyle Height="500px"></FormStyle>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                        <HeaderStyle Width="36px"></HeaderStyle></telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="idTargetDate" FilterControlAltText="Filter idTargetDate column" HeaderText="idTargetDate" SortExpression="idTargetDate" UniqueName="idTargetDate" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="idRequest" FilterControlAltText="Filter idRequest column" HeaderText="idRequest" SortExpression="idRequest" UniqueName="idRequest" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CustomerName" AllowFiltering="true" FilterControlAltText="Filter CustomerName column" HeaderText="Customer" SortExpression="CustomerName" UniqueName="CustomerName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="TargetDate" DataType="System.DateTime" DataFormatString="{0:M/dd/yyyy}" AllowFiltering="true" FilterControlAltText="Filter TargetDate column" HeaderText="TargetDate" SortExpression="TargetDate" UniqueName="TargetDate">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="ChangeReason" AllowFiltering="true" FilterControlAltText="Filter ChangeReason column" HeaderText="Change Reason" SortExpression="ChangeReason" UniqueName="ChangeReason">
                    </telerik:GridBoundColumn>
                    </Columns>
                <EditItemStyle BackColor="#FFC080" />
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    </div>
  </div>
</asp:Content>


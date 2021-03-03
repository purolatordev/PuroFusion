<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UserSecurity.aspx.cs" Inherits="UserSecurity" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
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
                    function callBackFn(arg) {
                        if (arg == true) {
                            $("#cbxAcctType").css("Display", "None");
                        }
                    }
                    function MyValueChanging(sender, args) {
                        args.set_newValue(args.get_newValue().toUpperCase());
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
             <telerik:AjaxSetting AjaxControlID="cbxUsers">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cbxUesrs" LoadingPanelID="RadAjaxLoadingPanel1" />
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
            <img src="~/Images/check1.jpg" id="check1" runat="server" visible="false"/>
          <asp:Label id="lblInfo" Cssclass="alert-link" runat="server" ></asp:Label>
            <img src="~/Images/check1.jpg" id="check2" runat="server" visible="false"/>
          <asp:Label id="lblInfo2" Cssclass="alert-link" runat="server" ></asp:Label>
            <img src="~/Images/check1.jpg" id="check3" runat="server" visible="false"/>
             <img src="~/Images/redX.jpg" id="uncheck3" runat="server" visible="false"/>
          <asp:Label id="lblInfo3" Cssclass="alert-link" runat="server" ></asp:Label>
            <img src="~/Images/check1.jpg" id="check4" runat="server" visible="false"/>
             <img src="~/Images/redX.jpg" id="uncheck4" runat="server" visible="false"/>
          <asp:Label id="lblInfo4" Cssclass="alert-link" runat="server" ></asp:Label>
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
           Animation="Resize" ShowDelay="0" RelativeTo="Element" Text="Use the User Security Screen to assign Roles to Application Users<p/> ">
          </telerik:RadToolTip>

         <p></p><div><b>PuroFusion User Security</b>
                 <a id="HeaderLink1" href="#" onclick="return false;"><img src="Images/help-icon16.png" /></a>
                 <br /><i>The User Security screen is used to assign roles to application users.</i>
                 <hr />
               </div>

        

       <%-- begin maintenance--%>

         <telerik:RadGrid ID="rgUsers" runat="server" GridLines="Both" CellSpacing="-1" AllowFilteringByColumn="true" OnNeedDataSource="rgUsers_NeedDataSource" 
             OnInsertCommand="rgUsers_InsertCommand" OnUpdateCommand="rgUsers_UpdateCommand" OnItemDataBound="rgUsers_ItemDataBound" OnDeleteCommand="rgUsers_DeleteCommand" >
            <GroupingSettings CollapseAllTooltip="Collapse all groups" CaseSensitive="false"></GroupingSettings>
            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True">
            </ExportSettings>
            <ClientSettings>
                <Scrolling  AllowScroll="true" ScrollHeight="450"/>
            </ClientSettings>
            <MasterTableView AutoGenerateColumns="False" AllowPaging="True" EditMode="PopUp" AllowFilteringByColumn="true" CommandItemDisplay="Top" TableLayout="Fixed" DataKeyNames="idPI_ApplicationUser">
                <CommandItemSettings ShowAddNewRecordButton="True" ShowRefreshButton="False" ShowExportToExcelButton="True"></CommandItemSettings>
                <EditFormSettings UserControlName="EditUser.ascx" EditFormType="WebUserControl"  PopUpSettings-Height="350px" PopUpSettings-Width="450px">
                    <FormStyle Height="500px"></FormStyle>
                    <PopUpSettings Modal="true" />
                </EditFormSettings>
                <Columns>
                    <telerik:GridEditCommandColumn ButtonType="ImageButton" EditImageUrl="~/Images/Grid/Edit.gif" HeaderText="Edit" UniqueName="Edit">
                    <HeaderStyle Width="36px"></HeaderStyle></telerik:GridEditCommandColumn>
                    <telerik:GridBoundColumn DataField="ApplicationName" FilterControlAltText="Filter ApplicationName column" HeaderText="Application Name" SortExpression="ApplicationName" UniqueName="ApplicationName" Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="idPI_ApplicationUser" Visible="false" FilterControlAltText="Filter idPI_ApplicationUser column" HeaderText="idPI_ApplicationUser" SortExpression="idPI_ApplicationUser" UniqueName="idPI_ApplicationUser">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="UserName" FilterControlAltText="Filter UserName column" HeaderText="User Name" SortExpression="UserName" UniqueName="UserName">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="idPI_ApplicationUserRole" Visible="false" FilterControlAltText="Filter idPI_ApplicationUserRole column" HeaderText="idPI_ApplicationUserRole" SortExpression="idPI_ApplicationUserRole" UniqueName="idPI_ApplicationUserRole">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ActiveDirectoryName" FilterControlAltText="Filter ActiveDirectoryName column" HeaderText="Login ID" SortExpression="ActiveDirectoryName" UniqueName="ActiveDirectoryName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RoleName" FilterControlAltText="Filter Role column" HeaderText="Role" SortExpression="RoleName" UniqueName="RoleName">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="role_UpdatedBy" FilterControlAltText="Filter UpdatedBy column" HeaderText="UpdatedBy" SortExpression="UpdatedBy" UniqueName="UpdatedBy" Visible="false">
                    </telerik:GridBoundColumn>
                     <telerik:GridBoundColumn DataField="role_UpdatedOn"  DataType="System.DateTime" FilterControlAltText="Filter UpdatedOn column" HeaderText="UpdatedOn" SortExpression="UpdatedOn" UniqueName="UpdatedOn" Visible="false">
                    </telerik:GridBoundColumn>
                    
                    
                    <telerik:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" FilterControlAltText="Filter DeleteColumn column" HeaderText="Remove Access" Text="Remove" UniqueName="DeleteLink" ConfirmDialogType="RadWindow"
                    Resizable="false" ConfirmText="Remove User Access to PuroFusion?">
                    </telerik:GridButtonColumn>
                </Columns>
                <PagerStyle AlwaysVisible="True"></PagerStyle>
            </MasterTableView>
        </telerik:RadGrid>
       <%-- end maintenance--%>

   </div>

</asp:Content>

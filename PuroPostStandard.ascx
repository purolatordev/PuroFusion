<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PuroPostStandard.ascx.cs" Inherits="PuroPostStandard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
  /*  table,th,td{
        border: 1px solid black;
    }*/
    td.Column1{
        width: 90px;
    }
    td.Column1Span{
        text-align: center;
    }    
    td.Column2{
        text-align: right;
        width: 150px;
    }
    td.Column3Spacer{
        width: 10px;
    }
    td.Column4LeftWidth{
        text-align: left;
        width: 130px;
    }
    td.Column4LeftLargest{
        text-align: right;
    }
</style>
<telerik:RadPanelBar RenderMode="Lightweight" ID="RadPanelBar1" runat="server" Visible="true" Width="100%">
    <Items>
        <telerik:RadPanelItem  Expanded="True">
            <ContentTemplate>
                <table>
                    <tr>
                        <td class="Column1"></td>
                        <td class="Column2">Communication Method</td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadDropDownList ID="comboxCommunicationMethod" runat="server" Visible="true" Width="110px" AutoPostBack="True" OnSelectedIndexChanged="CommunicationMethod_SelectedIndexChanged">
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="Column1Span">If any FTP option is chosen, the following section will appear
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1"></td>
                        <td class="Column2">
                            <asp:Label ID="lblFTPAddress" runat="server" Text="FTP Address" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftLargest">
                            <telerik:RadTextBox ID="textBoxFTPAddress" runat="server" MaxLength="225" Width="225px" ToolTip="Enter FTP Address" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1"></td>
                        <td class="Column2">
                            <asp:Label ID="lblUserName" runat="server" Text="User Name" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadTextBox ID="textBoxUserName" runat="server" MaxLength="100"  Width="110px" ToolTip="Enter User Name" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1"></td>
                        <td class="Column2">
                            <asp:Label ID="lblPassword" runat="server" Text="Password" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadTextBox ID="textBoxPassword" runat="server" MaxLength="100" Width="110px" ToolTip="Enter Password" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1"></td>
                        <td class="Column2">
                            <asp:Label ID="lblFolderPath" runat="server" Text="Folder Path" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftLargest">
                            <telerik:RadTextBox ID="textBoxFolderPath" runat="server" MaxLength="225" Width="225px" ToolTip="Enter Folder Path" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="Column1Span">If email is chosen, the following section will appear
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1"></td>
                        <td class="Column2">
                            <asp:Label ID="lblEmail" runat="server" Text="Email" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftLargest">
                            <telerik:RadTextBox ID="textBoxEmail" runat="server" MaxLength="225" Width="225px" ToolTip="Enter Email" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1"></td>
                        <td class="Column2">
                            <asp:Label ID="Label1" runat="server" Text="Panel Title" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadTextBox ID="textBoxPanelTitle" runat="server" MaxLength="225" Width="150px" ToolTip="Enter Panel title" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" >
                            <telerik:RadButton ID="btnSubmitChanges" CausesValidation="true" runat="server" Text="Save Changes" OnClick="btnSubmit210Changes_Click" AutoPostBack="true" Enabled="true" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </telerik:RadPanelItem>
    </Items>
</telerik:RadPanelBar>

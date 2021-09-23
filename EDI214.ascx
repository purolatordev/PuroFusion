<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EDI214.ascx.cs" Inherits="EDI214" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    /*  table,th,td{
        border: 1px solid black;
    }*/
    td.Column1a {
        width: 75px;
    }

    td.Column1SpanA {
        text-align: left;
    }

    td.Column2A {
        text-align: right;
        width: 145px;
    }

    td.Column3Spacer {
        width: 10px;
    }

    td.Column4LeftWidth {
        text-align: left;
        width: 160px;
    }

    td.Column4LeftLargest {
        text-align: right;
    }
</style>
<telerik:RadPanelBar RenderMode="Lightweight" ID="RadPanelBar1" runat="server" Visible="true" Width="100%">
    <Items>
        <telerik:RadPanelItem Expanded="True">
            <ContentTemplate>
                <table>
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
                        <td class="Column1a"></td>
                        <td class="Column2A">File Format
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadDropDownList ID="comboBxFileFormat214" runat="server" Visible="true" Width="110px" AutoPostBack="True" OnSelectedIndexChanged="FileFormat_SelectedIndexChanged">
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="Column1SpanA">
                            <asp:Label ID="lblEDIIfX12" runat="server" Text="If X12 is chosen, the following section will appear" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblISA" runat="server" Text="ISA" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadTextBox ID="txtBoxISA" runat="server" MaxLength="100" Width="110px" ToolTip="Enter ISA" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblGS" runat="server" Text="GS" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadTextBox ID="txtBoxGS" runat="server" MaxLength="100" Width="110px" ToolTip="Enter GS" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblQualifier" runat="server" Text="Qualifier" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadTextBox ID="txtBoxQualifier" runat="server" MaxLength="100" Width="110px" ToolTip="Enter Qualifier" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">Communication Method</td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadDropDownList ID="comboxCommunicationMethod" runat="server" Visible="true" Width="110px" AutoPostBack="True" OnSelectedIndexChanged="CommunicationMethod_SelectedIndexChanged">
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="Column1SpanA">
                            <asp:Label ID="Label2" runat="server" Text="If any FTP option is chosen, the following section will appear" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblFTPAddress" runat="server" Text="FTP Address" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftLargest">
                            <telerik:RadTextBox ID="textBoxFTPAddress" runat="server" MaxLength="225" Width="225px" ToolTip="Enter FTP Address" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblUserName" runat="server" Text="User Name" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadTextBox ID="textBoxUserName" runat="server" MaxLength="100" Width="110px" ToolTip="Enter User Name" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblPassword" runat="server" Text="Password" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadTextBox ID="textBoxPassword" runat="server" MaxLength="100" Width="110px" ToolTip="Enter Password" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblFolderPath" runat="server" Text="Folder Path" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftLargest">
                            <telerik:RadTextBox ID="textBoxFolderPath" runat="server" MaxLength="225" Width="225px" ToolTip="Enter Folder Path" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="Column1SpanA">
                            <asp:Label ID="Label3" runat="server" Text="If email is chosen, the following section will appear" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblEmail" runat="server" Text="Email" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftLargest">
                            <telerik:RadTextBox ID="textBoxEmail" runat="server" MaxLength="225" Width="225px" ToolTip="Enter Email" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblTriggerMechanism" runat="server" Text="Trigger Mechanism" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadDropDownList ID="comboxTriggerMechanism" runat="server" Visible="true" Width="110px">
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblTiming" runat="server" Text="Timing" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadDropDownList ID="comboxTiming" runat="server" Visible="true" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="Timing_SelectedIndexChanged">
                            </telerik:RadDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="Column1SpanA"></td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">
                            <asp:Label ID="lblTimeofFile" runat="server" Text="Time of File" Visible="true"></asp:Label>
                        </td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadTimePicker ID="timeTimeofFile" runat="server" Width="100px" DateInput-DateFormat="hh:mm"></telerik:RadTimePicker>
                        </td>
                    </tr>
                    <tr>
                        <td class="Column1a"></td>
                        <td class="Column2a">Status Codes</td>
                        <td class="Column3Spacer"></td>
                        <td class="Column4LeftWidth">
                            <telerik:RadComboBox ID="comboxStatusCodes" SelectionMode="Multiple" CheckBoxes="true" runat="server" Visible="true" Width="160px">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <telerik:RadButton ID="btnSubmitChanges" CausesValidation="true" runat="server" Text="Save Changes" OnClick="btnSubmitChanges_Click" AutoPostBack="true" Enabled="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="txtBoxMultiDebug2" TextMode="multiline" Columns="100" Rows="7" runat="server" Visible="true" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </telerik:RadPanelItem>
    </Items>
</telerik:RadPanelBar>

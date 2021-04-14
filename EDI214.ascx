﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EDI214.ascx.cs" Inherits="EDI214" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
  /*  table,th,td{
        border: 1px solid black;
    }*/
    td.Column1a{
        width: 75px;
    }
    td.Column1SpanA{
        text-align: left;
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
<hr />
<table>
    <caption>
        <asp:Label ID="lblHeading" runat="server" Visible="true" ForeColor="Red"></asp:Label></caption>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">File Format
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadDropDownList ID="comboBxFileFormat" runat="server" Visible="true" Width="110px" AutoPostBack="True" OnSelectedIndexChanged="FileFormat_SelectedIndexChanged">
                <Items>
                    <telerik:DropDownListItem Value="CSV" Text="CSV" Selected="true" />
                    <telerik:DropDownListItem Value="Flat" Text="Flat" />
                    <telerik:DropDownListItem Value="X12" Text="X12" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4" class="Column1SpanA">If X12 is chosen, the following section will appear
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">
            <asp:Label ID="lblISA" runat="server" Text="ISA" Visible="true"></asp:Label>
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadTextBox ID="txtBoxISA" runat="server" MaxLength="100" Text='sample isa' Width="110px" ToolTip="Enter ISA" />
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">
            <asp:Label ID="lblGS" runat="server" Text="GS" Visible="true"></asp:Label>
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadTextBox ID="txtBoxGS" runat="server" MaxLength="100" Text='sample GS' Width="110px" ToolTip="Enter GS" />
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">
            <asp:Label ID="lblQualifier" runat="server" Text="Qualifier" Visible="true"></asp:Label>
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadTextBox ID="txtBoxQualifier" runat="server" MaxLength="100" Text='sample Qualifier' Width="110px" ToolTip="Enter Qualifier" />
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">Communication Method</td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadDropDownList ID="comboxCommunicationMethod" runat="server" Visible="true" Width="110px" AutoPostBack="True" OnSelectedIndexChanged="CommunicationMethod_SelectedIndexChanged">
                <Items>
                    <telerik:DropDownListItem Value="Internal FTP" Text="Internal FTP" Selected="true" />
                    <telerik:DropDownListItem Value="External FTP" Text="External FTP" />
                    <telerik:DropDownListItem Value="Internal SFTP" Text="Internal SFTP" />
                    <telerik:DropDownListItem Value="External SFTP" Text="External SFTP" />
                    <telerik:DropDownListItem Value="Email" Text="Email" />
                    <telerik:DropDownListItem Value="AS2" Text="AS2" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4" class="Column1SpanA">If any FTP option is chosen, the following section will appear
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">
            <asp:Label ID="lblFTPAddress" runat="server" Text="FTP Address" Visible="true"></asp:Label>
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftLargest">
            <telerik:RadTextBox ID="textBoxFTPAddress" runat="server" MaxLength="225" Text='sample FTP Address' Width="225px" ToolTip="Enter FTP Address" />
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">
            <asp:Label ID="lblUserName" runat="server" Text="User Name" Visible="true"></asp:Label>
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadTextBox ID="textBoxUserName" runat="server" MaxLength="100" Text='sample User Name' Width="110px" ToolTip="Enter User Name" />
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">
            <asp:Label ID="lblPassword" runat="server" Text="Password" Visible="true"></asp:Label>
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadTextBox ID="textBoxPassword" runat="server" MaxLength="100" Text='sample Password' Width="110px" ToolTip="Enter Password" />
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">
            <asp:Label ID="lblFolderPath" runat="server" Text="Folder Path" Visible="true"></asp:Label>
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftLargest">
            <telerik:RadTextBox ID="textBoxFolderPath" runat="server" MaxLength="225" Text='sample Folder Path' Width="225px" ToolTip="Enter Folder Path" />
        </td>
    </tr>
     <tr>
        <td colspan="4" class="Column1SpanA">If email is chosen, the following section will appear
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">
            <asp:Label ID="lblEmail" runat="server" Text="Email" Visible="true"></asp:Label>
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftLargest">
            <telerik:RadTextBox ID="textBoxEmail" runat="server" MaxLength="225" Text='sample Email' Width="225px" ToolTip="Enter Email" />
        </td>
    </tr>
        <tr>
        <td class="Column1a"></td>
        <td class="Column2">Trigger Mechanism
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadDropDownList ID="comboxTriggerMechanism" runat="server" Visible="true" Width="110px" >
                <Items>
                    <telerik:DropDownListItem Value="Manifest" Text="Manifest" Selected="true" />
                    <telerik:DropDownListItem Value="Invoice" Text="Invoice" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
     <tr>
        <td class="Column1a"></td>
        <td class="Column2">Timing
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadDropDownList ID="comboxTiming" runat="server" Visible="true" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="Timing_SelectedIndexChanged">
                <Items>
                    <telerik:DropDownListItem Value="Once a Day" Text="Once a Day" Selected="true" />
                    <telerik:DropDownListItem Value="Multiple Times a Day" Text="Multiple Times a Day" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="4" class="Column1SpanA">If Once a day, the following section will appear
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">
            <asp:Label ID="lblTimeofFile" runat="server" Text="Time of File" Visible="true"></asp:Label>
        </td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadTextBox ID="textBoxTimeofFile" runat="server" MaxLength="100" Text='sample Time of File' Width="110px" ToolTip="Time of File" />
        </td>
    </tr>
    <tr>
        <td class="Column1a"></td>
        <td class="Column2">Status Codes</td>
        <td class="Column3Spacer"></td>
        <td class="Column4LeftWidth">
            <telerik:RadDropDownList ID="comboxStatusCodes" runat="server" Visible="true" Width="110px" >
                <Items>
                    <telerik:DropDownListItem Value="Status Code 1" Text="Status Code 1" Selected="true" />
                    <telerik:DropDownListItem Value="Status Code 2" Text="Status Code 2" />
                    <telerik:DropDownListItem Value="Status Code 3" Text="Status Code 3" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
</table>

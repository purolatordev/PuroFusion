<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EDI214.ascx.cs" Inherits="EDI214" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<hr />
<table style="text-align: center; vertical-align: top">
    <tr>
        <td style="width: 100px"></td>
        <td>Name:
        </td>
        <td>
            <telerik:RadTextBox ID="txtName" runat="server" Text="UC Text 2" />
        </td>
    </tr>
    <tr>
        <td style="width: 100px"></td>
        <td>Yes No Dropdown:</td>
        <td>
            <telerik:RadDropDownList ID="ddlCountry2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged2" Visible="true" Width="90px">
                <Items>
                    <telerik:DropDownListItem Value="no" Text="No" />
                    <telerik:DropDownListItem Value="yes" Text="Yes" Selected="true" />
                </Items>
            </telerik:RadDropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 100px"></td>
        <td>
            <asp:Label ID="lblTestValue2" runat="server" Text="Test Value:"></asp:Label>
        </td>
        <td>
            <telerik:RadTextBox ID="txtBoxTestValue2" runat="server" Text="Test Value">
            </telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 100px"></td>
        <td>Education:
        </td>
        <td>
            <asp:CheckBoxList ID="cblEducation" runat="server">
                <asp:ListItem Text="BTech" />
                <asp:ListItem Text="MCA" />
                <asp:ListItem Text="MTech" />
            </asp:CheckBoxList>
        </td>
        <td>
            <telerik:RadButton ID="btnfirst" runat="server" Text="UC 2" OnClick="btnfirst_Click" AutoPostBack="true" />
        </td>
    </tr>
</table>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListViewTest1.aspx.cs" Inherits="ListViewTest1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .listview-layout {
            width: 630px;
            border: 1px solid gray;
            border-radius: 10px;
            padding: 10px;
        }

        .listview-item {
            height: 80px;
            width: 80px;
            display: inline-block;
            margin: 10px;
            border: 1px solid black;
            border-radius: 10px;
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <telerik:RadListView ID="RadListView1" runat="server" OnNeedDataSource="RadListView1_NeedDataSource" ItemPlaceholderID="PlaceHolder1">
                <LayoutTemplate>
                    <div class="listview-layout">
                        <h2>Employee List</h2>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="listview-item">
                        <h3><%# Eval("Name") %></h3>
                        <h4><%# Eval("Team") %></h4>
                    </div>
                </ItemTemplate>
            </telerik:RadListView>
        </div>
    </form>
</body>
</html>

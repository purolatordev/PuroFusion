<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TestErrorPage.aspx.cs" Inherits="TestErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

 
  <div>
    <h2>
      Default Page</h2>
    <p>
      Click this button to create an InvalidOperationException.<br />
      Page_Error will catch this and redirect to 
      GenericErrorPage.aspx.<br />
      <asp:Button ID="Submit1" runat="server" 
        CommandArgument="1" OnClick="Submit_Click"
        Text="Button 1" />
    </p>
    <p>
      Click this button to create an ArgumentOutOfRangeException.<br />
      Page_Error will catch this and handle the error.<br />
      <asp:Button ID="Submit2" runat="server" 
        CommandArgument="2" OnClick="Submit_Click"
        Text="Button 2" />
    </p>
    <p>
      Click this button to create a generic Exception.<br />
      Application_Error will catch this and handle the error.<br />
      <asp:Button ID="Submit3" runat="server" 
        CommandArgument="3" OnClick="Submit_Click"
        Text="Button 3" />
    </p>
    <p>
      Click this button to create an HTTP 404 (not found) error.<br />
      Application_Error will catch this 
      and redirect to HttpErrorPage.aspx.<br />
      <asp:Button ID="Submit4" runat="server" 
      CommandArgument="4" OnClick="Submit_Click"
        Text="Button 4" />
    </p>
    <p>
      Click this button to create an HTTP 404 (not found) error.<br />
      Application_Error will catch this 
      but will not take any action on it, and ASP.NET
      will redirect to Http404ErrorPage.aspx. 
      The original exception object will not be
      available.<br />
      <asp:Button ID="Submit5" runat="server" 
        CommandArgument="5" OnClick="Submit_Click"
        Text="Button 5" />
    </p>
    <p>
      Click this button to create an HTTP 400 (invalid url) error.<br />
      Application_Error will catch this 
      but will not take any action on it, and ASP.NET
      will redirect to DefaultRedirectErrorPage.aspx. 
      The original exception object will not
      be available.<br />
      <asp:Button ID="Button1" runat="server" 
        CommandArgument="6" OnClick="Submit_Click"
        Text="Button 6" />
    </p>
  </div>
   
</asp:Content>


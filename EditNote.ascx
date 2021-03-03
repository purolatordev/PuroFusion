<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditNote.ascx.cs" Inherits="EditNote" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" RelativeTo="Element"
                               Position="MiddleRight" AutoTooltipify="true" ContentScrolling="Default" Width="150"
                               Height="10">
</telerik:RadToolTipManager>
<telerik:RadAjaxManagerProxy ID="AjaxManagerProxy1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="rddlInternalTypeEdit">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="rddlInternalTypeEdit"  LoadingPanelID="RadAjaxLoadingPanel1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
<telerik:RadWindowManager ID="windowManager" runat="server">
    </telerik:RadWindowManager>
<asp:Panel ID="Panel1" runat="server" GroupingText=''>

     <table border="0" >
         <tr>
             <td style="height: 10px" colspan="4"></td>
         </tr>
            <tr>
                <td style="width: 10px;"></td>
                <td style="width: 220px;">
                    <b>Edit Note </b>      
                </td>
                
            </tr>      
        </table>

    <table border="0">
              <tr>
                 <td style="text-align:right;" >
                    Date
                </td>
                <td style="color:red;">*</td>
                <td>
                     <telerik:RadDatePicker RenderMode="Lightweight" ID="dpNoteDateEdit" runat="server" AutoPostBack="true" Enabled="true"  SelectedDate='<%# Bind("noteDate") %>'> </telerik:RadDatePicker>                    
                </td>
                <td style="text-align:right;" >
                    <asp:Label ID="lblInternalTaskEdit" runat="server" Text="Task Type" Visible="false"></asp:Label> 
                </td>
                 <td style="color:red;">
                    <asp:Label ID="lblInternalTaskAskEdit" runat="server" Text="*" Visible="false"></asp:Label> 
                </td>
                  <td style="text-align:right;">
                    <telerik:RadDropDownList ID="rddlInternalTypeEdit" runat="server" SelectedValue='<%# Bind("idTaskType") %>' DefaultMessage="Select Task Type" ToolTip="Select Task Type" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="rddlInternalType_IndexChanged"></telerik:RadDropDownList>
                </td>
                 <td style="text-align:right;" >
                     <asp:Label ID="lblInternalTimeSpentEdit" runat="server" Text="Time Spent in minutes" Visible="false"></asp:Label> 
                </td>
                 <td style="color:red;text-align:left;">
                    <asp:Label ID="lblInternalTimeSpentAskEdit" runat="server" Text="*" Visible="false"></asp:Label> 
                </td>
                 <td>
                   <telerik:RadTextBox ID="txtInternalTimeSpentEdit" runat="server" MaxLength="75" Text='<%# Bind("timeSpent") %>'  Width="100px" ToolTip="Enter Time Spent" Visible="false"/>                 
                </td>
            </tr>
             <tr>
                 <td style="text-align:right;" >Note ID</td>
                 <td></td>
                 <td><asp:Label ID="lblNoteID" runat="server" Visible="true" Text='<%# Bind("idNote") %>'></asp:Label> </td>
                 <td></td>
                 <td></td>
                 <td><asp:RequiredFieldValidator ID="rfvTaskTypeEdit" runat="server" ValidationGroup="noteEdit" ControlToValidate="rddlInternalTypeEdit" ErrorMessage="Select Task Type" style="color: red"></asp:RequiredFieldValidator></td>
                 <td></td>
                 <td></td>
                 <td><asp:RequiredFieldValidator ID="rfvTimeEdit" runat="server" ValidationGroup="noteEdit" ControlToValidate="txtInternalTimeSpentEdit" ErrorMessage="Enter Time Spent" style="color: red"></asp:RequiredFieldValidator></td>
             </tr>
            <tr>
                <td colspan="8"></td>
                <td style="height:20px"> <asp:Label ID="lblTimeWarningEdit" style="color:red;" runat="server" Text="" Visible="true"></asp:Label> </td>
            </tr>
            <tr>                
                 <td style="text-align:right;vertical-align:top" >
                    Enter Notes
                </td>
                 <td style="color:red;text-align:left;vertical-align:top">
                     <asp:Label ID="note1ast" runat="server" Text="*" Visible="true"></asp:Label> 
                 </td>
                <td colspan="7">
                   <asp:TextBox id="txtNotesEdit" TextMode="multiline" Columns="100" Rows="15" runat="server"  Text ='<%# Bind("PublicNote") %>'/>
                </td>
            </tr>
            <tr>
                 <td colspan="2"></td>
                 <td colspan="7">
                     <asp:RequiredFieldValidator ID="rfvNotesEdit" runat="server" ValidationGroup="noteEdit" ControlToValidate="txtNotesEdit" ErrorMessage="Please enter a note value" style="color: red"></asp:RequiredFieldValidator>
                 </td>
            </tr>
              <tr>                
                <td style="text-align:right;vertical-align:top">
                    <asp:Label ID="lblInternalNotesEdit" runat="server" Text="Customer Delay Notes" Visible="false"></asp:Label> 
                </td>
                 <td style="color:red;text-align:left;vertical-align:top">
                     <asp:Label ID="note2ast" runat="server" Text="*" Visible="true"></asp:Label> 
                 </td>
                <td colspan="8">
                   <asp:TextBox id="txtInternalNotesEdit" TextMode="multiline" Columns="100" Rows="3" runat="server" Visible="false" ForeColor="Red" Text ='<%# Bind("PrivateNote") %>'/>                    
                   <asp:RequiredFieldValidator ID="rfvNotes2Edit" runat="server" ValidationGroup="noteEdit" ControlToValidate="txtInternalNotesEdit" ErrorMessage="Please enter a note value" style="color: red" Enabled="false"></asp:RequiredFieldValidator>
 
                </td>
            </tr>
        </table>

           <table style="padding-top:2px;width:100%;" border="0" >
           <tr>            
             <td><p style="color:red"></p></td>
             <td style="color:blue;width:20%;font-size:medium;text-align:right;">                 
                <%-- <telerik:RadButton ID="btnSaveNotes" causesValidation ="true" ValidationGroup="noteEdit" runat="server" Text="Save Note" OnClick="btnSaveNotes_Click"/>
                 <telerik:RadButton ID="RadButton1" causesValidation ="true" runat="server" Text="Cancel" CommandName="Cancel"/>          --%>      
                 <asp:Button CssClass="btn btn-primary" ValidationGroup="noteEdit" ID="btnSaveNotes" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                 <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False"  CssClass="btn btn-primary" CommandName="Cancel"></asp:Button>  
             </td>    
 
         </tr>
             
              
        </table>     
    <div style="padding-left:10px">
        <p style="color:red"><i >*Required Fields</i></p>
                <asp:Label ID="lblErrorMessage" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                <asp:Label ID="lblSuccessMessage" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
    </div>
    <style>
        .upper{
            text-transform:uppercase;
        }
    </style>
</asp:Panel>

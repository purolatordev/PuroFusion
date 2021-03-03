<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="View" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Discovery Request</title>
    <style>
        body {
            background-color: 	#ffffff;
        }
       
        table.clientInfo {
            border-collapse: collapse;
            width:inherit;
        }

        th{
            color:red;
        }
         td {
            /*padding: 0.25rem;*/
          
        }
         div.right {
            float:right;
            height: 90px;
            width: 240px;
            font-style:italic;
            font-size:small;
            color:blue;
        }
    
    </style>
</head>
<body style="font-family:Calibri">
        <form id="form1" runat="server">
    <div class="right" >
        <p></p>
             <table>
                  <tr>
                     <td><b>Submitted By: </b></td>
                     <td>
                         <asp:Label ID="lblSubmittedBy" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td><b>Last Updated By: </b></td>
                     <td>
                         <asp:Label ID="lblUpdatedBy" runat="server"></asp:Label>
                     </td>
                 </tr>
                 <tr>
                     <td><b>Last Updated On: </b></td>
                     <td><asp:Label ID="lblUpdatedOn" runat="server"></asp:Label></td>
                 </tr>
             </table>
         </div>

    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="width: 453px">
         <asp:Panel ID="pnlDanger" runat="server" Visible="false">
            <div class="alert alert-danger" role="alert">
                <asp:Label ID="lblDanger" runat="server" CssClass="alert-link"></asp:Label>
            </div>
        </asp:Panel>
        </div>
     <div style="margin: 20px 30px 60px 10px;  padding-left:20px" >

         <img src="../Images/DiscoveryRequest.jpg" />
        
        
        <div style="margin: 20px 100px 60px 10px;">
            <table>
                 <tr>
                     <td>
                         <telerik:RadButton ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" Skin="Web20"  ></telerik:RadButton>
                     </td>
                     <td>
                         <telerik:RadButton ID="btnCancel" runat="server" Text="Cancel" Skin="Web20" OnClick="btnCancel_Click"  ></telerik:RadButton>
                     </td>
                 </tr>
             </table>
            <br />
        </div>

        
       <asp:Panel runat="server" ID="pnlclientInfo" BorderColor="DarkBlue" style="padding-left:5px">
        <div style="text-align:left;width:765px">
             <img src="../Images/divider.png" /> 
        </div>
           
          <asp:Panel runat="server" style="background-color:lightgray !important; -webkit-print-color-adjust: exact;padding-left:5px">
            <table class="clientInfo" style="width:765px">
               
               <tr>                   
                   <td colspan="2" style="color:blue; text-align:left"><p></p><b><asp:Label ID="lblCustomerName" runat="server" ></asp:Label></b><p></p></td>
               </tr>
                <tr>
                    <td style="width:170px"><b>Sales Professional: </b></td>
                    <td>
                        <asp:Label ID="lblSalesProfessional" runat="server"></asp:Label>
                    </td>
                </tr>                 
            </table>          
               <p></p> <hr />
              </asp:Panel>
            
            <asp:Panel runat="server" >
                <table class="clientInfo" border="0">                   
                    <tr><td colspan="5"></td></tr>
                    <tr>
                        <td style="width:175px"><b>Commodity</b></td>
                        <td><asp:Label ID="lblCommodity" runat="server"></asp:Label> </td>
                        <td style="width:50px"></td>
                        <td style="width:130px"><b>Annual Revenue</b></td>
                        <td style="background-color: #ccffcc;text-align: center"> <asp:Label ID="lblAnnualRevenue" runat="server"></asp:Label></td>
                    </tr>
                    <tr><td colspan="5"></td></tr>
                    <tr>
                        <td><b>Address </b></td>
                        <td> <asp:Label ID="lblAdress" runat="server"></asp:Label></td>
                        <td></td>
                        <td></td>
                        <td> </td>
                    </tr>
                    <tr>
                        <td><b></b></td>
                        <td> <asp:Label ID="lblCityStateZip" runat="server"></asp:Label></td>
                        <td></td>
                        <td><b><asp:Label ID="lblWebsitel" runat="server" Text="Website"></asp:Label></b></td>
                        <td> <asp:Label ID="lblWebsite" runat="server"></asp:Label></td>
                    </tr>                     
                    <tr><td colspan="5"></td></tr>
                     <tr>
                        <td><b>Business Contact</b></td>
                        <td> <asp:Label ID="lblBusContactName" runat="server"></asp:Label></td>
                        <td style="width:45px"></td>
                        <td><b>IT Contact</b></td>
                        <td> <asp:Label ID="lblITContactName" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                        <td><b></b></td>
                        <td> <asp:Label ID="lblBusTitle" runat="server"></asp:Label></td>
                        <td style="width:45px"></td>
                        <td><b></b></td>
                        <td> <asp:Label ID="lblITtitle" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b></b></td>
                        <td> <asp:Label ID="lblBusPhone" runat="server"></asp:Label></td>
                        <td style="width:45px"></td>
                        <td><b></b></td>
                        <td> <asp:Label ID="lblITPhone" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                        <td><b></b></td>
                        <td> <asp:Label ID="lblBusEmail" runat="server"></asp:Label></td>
                        <td style="width:45px"></td>
                        <td><b></b></td>
                        <td> <asp:Label ID="lblITEmail" runat="server"></asp:Label></td>
                    </tr>
                     <tr style="height:5px"><td colspan="5"></td></tr>                    
                     <tr>
                        <td style="vertical-align:top"><b>Current Shipping Solution</b></td>
                        <td colspan="4"> <asp:Label ID="lblCurrentSolution" runat="server"></asp:Label></td>                       
                    </tr>                   
                     <tr>
                        <td style="text-align:left; vertical-align:top"><br /><b>Proposed Services</b></td>
                        <td style="text-align:left; vertical-align:top" ><asp:Label ID="lblProposedServices" runat="server"></asp:Label><p></p></td>
                         <td style="width:45px"></td>
                          <td style="vertical-align:top;"><br /><b>Proposed Customs</b></td>
                          <td style="vertical-align:top;"> <br /><asp:Label ID="lblCustoms" runat="server"></asp:Label></td> 
                    </tr>                  
                     
                   
                    
                            <tr>
                        <td ><b><asp:Label ID="lblCallDatesl" runat="server" Text="Potential Call Dates" Visible="false"></asp:Label></b></td>
                        <td colspan="4"> <asp:Label ID="lblCallDates" runat="server" Visible="false"></asp:Label></td>                       
                    </tr>
                    <tr>
                        <td><b><asp:Label ID="lblSalesNotesl" runat="server" Text="Customer Notes" Visible="false"></asp:Label></b></td>
                        <td colspan="4"> <asp:Label ID="lblSalesNotes" runat="server"></asp:Label></td>                       
                    </tr>        
                     <tr><td colspan="5"></td></tr>
                    
            </table>           
            <div style="text-align:left;width:765px">
             <img src="../Content/Images/dividerSmall.png" /> 
            </div>
            <asp:Panel runat="server" style="background-color:lightgray !important; -webkit-print-color-adjust: exact;padding-left:5px">
            <table class="clientInfo" border="0">
               
               <tr>                                 
                   <td colspan="2" style="color:blue;text-align:left"><p></p><b><asp:Label ID="lblITBA" runat="server" ></asp:Label></b><p></p></td>
                   <td colspan="3"><asp:Label ID="lblWPK" runat="server"></asp:Label></td>
               </tr>
                <tr>
                    <td style="width:145px"><b><asp:Label ID="lblTargetGoLivel" runat="server" Text="Target Go-Live: "></asp:Label></b></td>
                    <td><asp:Label ID="lblTargetGoLive" runat="server"></asp:Label></td>     
                    <td colspan="2"> <asp:Label ID="lblOtherType" runat="server"></asp:Label></td>                 
                    <td></td>
                </tr>  
                 <tr>
                    <td style="width:145px"><b><asp:Label ID="lblActualGoLive" runat="server" Text="Actual Go-Live: " Visible="false"></asp:Label></b></td>
                    <td><asp:Label ID="lblGoLiveDate" runat="server"></asp:Label></td>     
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>  
                 <tr>
                    
                     <td><b>Onboarding Phase: </b> </td>
                     <td style="width:230px;text-align:left"><asp:Label ID="lblPhase" runat="server"></asp:Label></td>
                     <td><b> <asp:Label ID="lblShippingChannell" runat="server" Text = "Shipping Channel:"/></b></td>
                     <td style="text-align:left"><asp:Label ID="lblShippingChannel" runat="server"/>  </td>
                     <td></td>
                </tr>  
                 <tr>
                    <td colspan="5"><b><asp:Label ID="lblCustomsNotSupported" Text ="<br>Customs Not Supported" runat="server" Visible="false"></asp:Label></b></td>                    
                </tr>                
                <tr>
                    <td><b><asp:Label ID="lblBrokerName" Text="Customs Broker" runat="server" Visible="false"></asp:Label></b></td>
                     <td style="width:230px;text-align:left"><asp:Label ID="lblCustomsBroker" runat="server"></asp:Label></td>
                     <td colspan="3"><b><asp:Label ID="lblElinkSupported" runat="server"></asp:Label></b></td>                    
                </tr>     
                  <tr>
                   <td><b><asp:Label ID="lblBrokerNumberl" Text="Broker Number" runat="server" Visible="false"></asp:Label></b></td>
                     <td style="width:230px;text-align:left"><asp:Label ID="lblBrokerNumber" runat="server"></asp:Label></td>
                     <td colspan="3"></td>                 
                </tr>     
                 <tr>
                    <td><b><asp:Label ID="lblPars" runat="server"></asp:Label></b></td>
                     <td colspan="4" style="text-align:left"><asp:Label ID="lblParsValue" runat="server"></asp:Label></td>             
                </tr>  
                 <tr>
                    <td><b><asp:Label ID="lblPass" runat="server"></asp:Label></b></td>
                     <td colspan="4" style="text-align:left"><asp:Label ID="lblPassValue" runat="server"></asp:Label></td>             
                </tr>  
            </table> 
             
               <p></p> <hr />
              </asp:Panel>    
             <table class="clientInfo" border="0">
                   <tr>
                        <td style="color:blue; text-align:left"><b><asp:Label ID="lblSolutionSummaryl" runat="server" Text="Solution Summary"></asp:Label></b></td>
                    </tr>
                     <tr>
                        <td > <asp:Label ID="lblSolutionSummary" runat="server"></asp:Label></td>                       
                     </tr>
                
                 </table>

                <table>                   
                   <tr>
                      <td style="text-align:left; vertical-align:top"><br /><b><asp:Label ID="lblProposedProd" runat="server" Text="Proposed Products"></asp:Label></b></td>
                     <td style="width:75px"></td>
                       <td style="text-align:left; vertical-align:top"><br /><b><asp:Label ID="lblequipneeded" runat="server" Text ="Equipment Needed"/></b></td>
                        <td style="width:75px"></td>
                       <td style="text-align:left; vertical-align:top"><br /><b><asp:Label ID="lblEdiLabel" runat="server" Text ="EDI Requests"/></b></td>
                    </tr>  
                  <tr>
                      <td style="text-align:left; vertical-align:top" ><asp:Label ID="lblProducts" runat="server"></asp:Label><p></p></td>
                       <td></td>
                      <td style="text-align:left; vertical-align:top"><asp:Label ID="lblEquipment" runat="server"></asp:Label></td>
                        <td></td>
                      <td style="text-align:left; vertical-align:top"><asp:Label ID="lblEDIRequests" runat="server"></asp:Label></td>
                 </tr>
                 </table>
                <table>
                     <tr>
                      <td colspan="7" style="color:blue; text-align:left; vertical-align:top"><b><asp:Label ID="lblWPKDetails" Text="WorldPak Details" Visible="true" runat="server"></asp:Label></b></td>
                     </tr>
                    <tr>
                        <td style="text-align:left; vertical-align:top"><b><asp:Label ID="lblDataEntrylbl" Text="Data Entry Method" Visible="false" runat="server"/></b></td>
                        <td style="width:25px"></td>
                      <td style="text-align:left; vertical-align:top"><asp:Label ID="lbldataEntry" Text="" Visible="true" runat="server"></asp:Label></td>
                        <td style="width:25px"></td>
                       
                     </tr>
                    <tr>
                        <td style="text-align:left; vertical-align:top"><b><asp:Label ID="lblSandboxlbl" Text="Sandbox Credentials" Visible="false" runat="server"/></b></td>
                        <td style="width:25px"></td>
                        <td style="text-align:left; vertical-align:top"><asp:Label ID="lblSandbox" runat="server" Text =""/></td>
                         <td style="width:25px"></td>
                         <td style="text-align:left; vertical-align:top"><b><asp:Label ID="lblProdCredlbl" Text="Production Credentials" Visible="false" runat="server"/></b></td>
                        <td style="width:25px"></td>
                       <td style="text-align:left; vertical-align:top"><asp:Label ID="lblWPKProd" runat="server" Text =""/></td>
                    </tr>  
                      <tr>
                         <td colspan="7" style="text-align:left; vertical-align:top"><asp:Label ID="lblWPKOther" Text="" Visible="true" runat="server"></asp:Label></td>
                     </tr>
                    <tr>
                         <td colspan="7" style="text-align:left; vertical-align:top"><b><asp:Label ID="lblEWSplitl" Text="East/West Split Details" Visible="false" runat="server"></asp:Label></b></td>
                    </tr>
                     <tr>
                         <td style="text-align:left; vertical-align:top"><b><asp:Label ID="lblewselectl" Text="Account Selected by:" Visible="false" runat="server"/></b></td>
                        <td style="width:25px"></td>
                        <td style="text-align:left; vertical-align:top"><asp:Label ID="lblewselect" runat="server" Text ="" Visible="false"/></td>
                         <td style="width:25px"></td>
                         <td style="text-align:left; vertical-align:top"></td>
                        <td style="width:25px"></td>
                       <td style="text-align:left; vertical-align:top"></td>
                    </tr>
                      <tr>
                         <td colspan="7" style="text-align:left; vertical-align:top"><asp:Label ID="lblewflags" Text="" Visible="false" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                         <td style="text-align:left; vertical-align:top"><b><asp:Label ID="lblewesrtl" Text="East Sort Code:" Visible="false" runat="server"/></b></td>
                        <td style="width:25px"></td>
                        <td style="text-align:left; vertical-align:top"><asp:Label ID="lblewesrt" runat="server" Text ="" Visible="false"/></td>
                         <td style="width:25px"></td>
                         <td style="text-align:left; vertical-align:top"><b><asp:Label ID="lblewwsrtl" Text="West Sort Code:" Visible="false" runat="server"/></b></td>
                        <td style="width:25px"></td>
                       <td style="text-align:left; vertical-align:top"><asp:Label ID="lblewwsrt" runat="server" Text ="" Visible="false"/></td>
                    </tr>
                     <tr>
                         <td style="text-align:left; vertical-align:top"><b><asp:Label ID="lblewsortdetailsl" Text="Sort Details:" Visible="false" runat="server"/></b></td>
                        <td style="width:25px"></td>
                        <td  colspan="5" style="text-align:left; vertical-align:top"><asp:Label ID="lblewsortdetails" runat="server" Text ="" Visible="false"/></td>                        
                    </tr>
                      <tr>
                         <td style="text-align:left; vertical-align:top"><b><asp:Label ID="lblewmissortl" Text="Mis-sort Details:" Visible="false" runat="server"/></b></td>
                        <td style="width:25px"></td>
                        <td  colspan="5" style="text-align:left; vertical-align:top"><asp:Label ID="lblewmissort" runat="server" Text ="" Visible="false"/></td>                        
                    </tr>
                </table>
                <p></p>
               
                   <table border="0">
                    <tr>
                        <td colspan="11" style="color:blue; text-align:left"><b><asp:Label ID="lblBillingInfo" runat="server" Text ="Billing Information" Visible="false"></asp:Label></b></td>                        
                    </tr>
                     <tr>
                        <td style="width:105px"><b><asp:Label ID="lblInvoiceTypel" runat="server" Text ="InvoiceType"  Visible="false"></asp:Label></b></td>
                        <td colspan="2" style="width:75px"><asp:Label ID="lblInvoiceType" runat="server"></asp:Label></td>
                        <td colspan="3"><b><asp:Label ID="lblBillTol" runat="server" Text ="Billto Account"  Visible="false"></asp:Label></b></td>
                        <td colspan="5"><asp:Label ID="lblBillTo" runat="server"></asp:Label></td>                      
                    </tr>
                   <tr>
                       <td colspan="5" style="color:blue; text-align:left"><b><asp:Label ID="lblAcctSupport" runat="server" Text="Account Support"></asp:Label></b></td>
                       <td style="width:25px"></td>
                       <td colspan="5" style="color:blue; text-align:left"><b><asp:Label ID="lblContractInfo" runat="server" Text="Contract Information"></asp:Label></b></td>                       
                   </tr>
                   <tr> 
                        <td style="width:125px"><b><asp:Label ID="lblControlBranchl" runat="server" Text="Control Branch"></asp:Label></b></td>
                        <td style="width:85px"><asp:Label ID="lblControlBranch" runat="server"></asp:Label></td>
                        <td style="width:15px"></td>
                        <td style="width:55px"><b><asp:Label ID="lblCRRl" runat="server" Text="CRR"></asp:Label></b></td>
                        <td ><asp:Label ID="lblCRR" runat="server"></asp:Label></td> 
                        <td style="width:25px"></td>
                        <td ><b><asp:Label ID="lblContractNuml" runat="server" Text="Contract#"></asp:Label></b></td>
                        <td ><asp:Label ID="lblContractNum" runat="server"></asp:Label></td>
                        <td style="width:15px"></td>
                        <td style="width:50px"><b><asp:Label ID="lblConCurl" runat="server" Text="Currency"></asp:Label></b></td>
                        <td style="width:50px"><asp:Label ID="lblConCur" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b><asp:Label ID="lblSupportUserl" runat="server" Text="Support User" /></b></td>
                        <td><asp:Label ID="lblSupportUser" runat="server"></asp:Label></td>
                        <td ></td>
                        <td><b><asp:Label ID="lblOfficel" runat="server" Text="Office"></asp:Label></b></td>
                        <td><asp:Label ID="lblOffice" runat="server"></asp:Label></td>      
                        <td ></td>  
                        <td><b><asp:Label ID="lblConStartl" runat="server" Text="Start Date" /></b></td>
                        <td><asp:Label ID="lblConStart" runat="server"></asp:Label></td>
                        <td ></td>
                        <td><b><asp:Label ID="lblConEndl" runat="server" Text="End Date" /></b></td>
                        <td><asp:Label ID="lblConEnd" runat="server"></asp:Label></td>           
                    </tr>
                    <tr>
                        <td><b><asp:Label ID="lblSupportGroupl" runat="server" Text="Support Group" /></b></td>
                        <td><asp:Label ID="lblSupportGroup" runat="server"></asp:Label></td>     
                        <td ></td>
                        <td><b><asp:Label ID="lblGroupl" runat="server" Text="Group" /></b></td>
                        <td><asp:Label ID="lblGroup" runat="server"></asp:Label></td>    
                        <td ></td>     
                        <td ><b><asp:Label ID="lblPaymentTermsl" runat="server" Text="Payment Terms" /></b></td>
                        <td colspan="4"><asp:Label ID="lblPaymentTerms" runat="server"></asp:Label></td>                                
                    </tr>
                    
                </table>

             <div style="text-align:left;width:765px">
             <img src="../Content/Images/dividerSmall.png" /> 
            </div>
            <asp:Panel runat="server" style="background-color:#f4f6f6  !important; -webkit-print-color-adjust: exact;padding-left:5px">
            <table class="clientInfo" border="0">
                       <tr>
                         <td colspan="7" style="color:red; text-align:left"><p></p><b>Shipping Account Details</b></td>
                     </tr>   
                    
                   <!-- Courier -->
                    <tr>                        
                        <td colspan="7"  style="color:red; text-align:left">                             
                            <asp:Label ID="lblCourierDetail" Text="Courier" runat="server"  Visible="false"></asp:Label>                           
                        </td>
                    </tr>
                    <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierAcctl" Text="Courier Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierAcct" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierContractl" Text="Courier Contract Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierContract" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                    <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierPinl" Text="Pin Prefix" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierPin" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierTransitl" Text="Transit Days" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierTransit" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                  <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierIndl" Text="Induction Address" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierInd" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> </td>
                         <td style="width:15px"></td>
                         <td> </td>                
                   </tr>
                <tr>
                    <td> </td> 
                     <td> </td> 
                     <td colspan="5"><asp:Label ID="lblCourierIndAddress" Text="" runat="server" Visible="false"></asp:Label></td>                
                </tr>
                   <tr>
                       
                       <td> </td> 
                        <td> </td> 
                         <td colspan="5"> <asp:Label ID="lblCourierIndCSZ" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierftpuserl" Text="eManifest FTP UserName" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierftpuser" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td ><asp:Label ID="lblCourierFTPFlag" Text="" runat="server" Visible="false"></asp:Label> </td>
                         <td style="width:15px"></td>
                         <td> </td>                
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierftppwdl" Text="eManifest FTP Password" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierftppwd" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierftpsenderl" Text="eManifest FTP Sender ID" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierftpsender" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierftpuserSandboxl" Text="Sandbox FTP UserName" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierftpuserSandbox" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierftppwdSandboxl" Text="Sandbox FTP Password" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierftppwdSandbox" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                <!-- Courier West -->
                    <tr>                        
                        <td colspan="7"  style="color:red; text-align:left">                             
                            <asp:Label ID="lblCourierDetailWest" Text="Courier West" runat="server"  Visible="false"></asp:Label>                           
                        </td>
                    </tr>
                    <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierAcctlWest" Text="Courier Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierAcctWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierContractlWest" Text="Courier Contract Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierContractWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                    <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierPinlWest" Text="Pin Prefix" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierPinWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierTransitlWest" Text="Transit Days" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierTransitWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                  <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierIndlWest" Text="Induction Address" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierIndWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> </td>
                         <td style="width:15px"></td>
                         <td> </td>                
                   </tr>
                <tr>
                    <td> </td> 
                     <td> </td> 
                     <td colspan="5"><asp:Label ID="lblCourierIndAddressWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                </tr>
                   <tr>
                       
                       <td> </td> 
                        <td> </td> 
                         <td colspan="5"> <asp:Label ID="lblCourierIndCSZWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierftpuserlWest" Text="eManifest FTP UserName" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierftpuserWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td ><asp:Label ID="lblCourierFTPFlagWest" Text="" runat="server" Visible="false"></asp:Label> </td>
                         <td style="width:15px"></td>
                         <td> </td>                
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierftppwdlWest" Text="eManifest FTP Password" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierftppwdWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierftpsenderlWest" Text="eManifest FTP Sender ID" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierftpsenderWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierftpuserSandboxlWest" Text="Sandbox FTP UserName" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierftpuserSandboxWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCourierftppwdSandboxlWest" Text="Sandbox FTP Password" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCourierftppwdSandboxWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>

                <!-- LTL -->
                      <tr>                        
                        <td colspan="7"  style="color:red; text-align:left" >                             
                            <asp:Label ID="lblLTLDetails" Text="LTL" runat="server"  Visible="false"></asp:Label>                           
                        </td>                            
                    </tr>
                  <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblLTLAcctl" Text="LTL Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblLTLAcct" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> </td>
                         <td style="width:15px"></td>
                         <td> </td>                
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblLTLMinl" Text="LTL Min Pro Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblLTLMin" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblLTLMaxl" Text="LTL Max Pro Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblLTLMax" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                  <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblLTLPinPrefixl" Text="LTL PIN Prefix" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblLTLPinPrefix" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold" colspan="2"> <asp:Label ID="lblLTLAutoFlag" Text="" Visible="true" runat="server"></asp:Label></td>
                         <td style="width:15px"></td>            
                    </tr>
                 <!-- LTL West-->
                      <tr>                        
                        <td colspan="7"  style="color:red; text-align:left" >                             
                            <asp:Label ID="lblLTLDetailsWest" Text="LTL West" runat="server"  Visible="false"></asp:Label>                           
                        </td>                            
                    </tr>
                  <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblLTLAcctlWest" Text="LTL Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblLTLAcctWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> </td>
                         <td style="width:15px"></td>
                         <td> </td>                
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblLTLMinlWest" Text="LTL Min Pro Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblLTLMinWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblLTLMaxlWest" Text="LTL Max Pro Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblLTLMaxWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                  <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblLTLPinPrefixlWest" Text="LTL PIN Prefix" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblLTLPinPrefixWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold" colspan="2"> </td>
                         <td style="width:15px"></td>
                     </tr>
                 <!-- CPC -->
                     <tr>                        
                        <td colspan="7"  style="color:red; text-align:left;">                             
                            <asp:Label ID="lblCPCDetails" Text="Cananda Post" runat="server" Visible="false"></asp:Label>                           
                        </td>                            
                    </tr>
                     <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCAcctl" Text="Cananda Post Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCAcct" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCContractl" Text="Cananda Post Contract Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCContract" Text="" runat="server" Visible="false"></asp:Label></td>
                     </tr>
                       <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCSitel" Text="Cananda Post Site Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCSite" Text="" runat="server"></asp:Label></td>
                         <td></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCIndl" Text="Cananda Post Induction Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCInd" Text="" runat="server" Visible="false"></asp:Label></td>
                     </tr>
                      <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCUserl" Text="Cananda Post Username" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCUser" Text="" runat="server" Visible="false"></asp:Label></td>
                        <td></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCPwdl" Text="Cananda Post Password" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCPwd" Text="" runat="server" Visible="false"></asp:Label></td>
                     </tr>
                  <!-- CPC West-->
                     <tr>                        
                        <td colspan="7"  style="color:red; text-align:left;">                             
                            <asp:Label ID="lblCPCDetailsWest" Text="Cananda Post West" runat="server" Visible="false"></asp:Label>                           
                        </td>                            
                    </tr>
                     <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCAcctlWest" Text="Cananda Post Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCAcctWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCContractlWest" Text="Cananda Post Contract Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCContractWest" Text="" runat="server" Visible="false"></asp:Label></td>
                     </tr>
                       <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCSitelWest" Text="Cananda Post Site Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCSiteWest" Text="" runat="server"></asp:Label></td>
                         <td></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCIndlWest" Text="Cananda Post Induction Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCIndWest" Text="" runat="server" Visible="false"></asp:Label></td>
                     </tr>
                      <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCUserlWest" Text="Cananda Post Username" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCUserWest" Text="" runat="server" Visible="false"></asp:Label></td>
                        <td></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblCPCPwdlWest" Text="Cananda Post Password" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblCPCPwdWest" Text="" runat="server" Visible="false"></asp:Label></td>
                     </tr>
                   <!-- PuroPost -->
                    <tr>                        
                        <td colspan="7"  style="color:red; text-align:left">                             
                            <asp:Label ID="lblPuroPostDetails" Text="PuroPost" runat="server"  Visible="false"></asp:Label>                            
                        </td>                            
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblPPSTAcctl" Text="PuroPost Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblPPSTAcct" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblPPSTTransitl" Text="Transit Days" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblPPSTTransit" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                 <tr>
                     <td style="font-weight:bold"> <asp:Label ID="lblPPSTInductionl" Text="Induction Address" runat="server" Visible="false"></asp:Label></td>
                     <td> </td> 
                     <td colspan="5"><asp:Label ID="lblPPSTInduction" Text="" runat="server" Visible="false"></asp:Label></td>                
                </tr>
                 <tr>                       
                     <td> </td> 
                     <td> </td> 
                     <td colspan="5"> <asp:Label ID="lblPPSTIndAddr" Text="" runat="server" Visible="false"></asp:Label></td>                
                  </tr>
                <tr>                       
                     <td> </td> 
                     <td> </td> 
                     <td colspan="5"> <asp:Label ID="lblPPSTIndCSZ" Text="" runat="server" Visible="false"></asp:Label></td>                
                  </tr>
                 <!-- PuroPost West-->
                    <tr>                        
                        <td colspan="7"  style="color:red; text-align:left">                             
                            <asp:Label ID="lblPuroPostDetailsWest" Text="PuroPost West" runat="server"  Visible="false"></asp:Label>                            
                        </td>                            
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblPPSTAcctlWest" Text="PuroPost Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblPPSTAcctWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblPPSTTransitlWest" Text="Transit Days" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblPPSTTransitWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                 <tr>
                     <td style="font-weight:bold"> <asp:Label ID="lblPPSTInductionlWest" Text="Induction Address" runat="server" Visible="false"></asp:Label></td>
                     <td> </td> 
                     <td colspan="5"><asp:Label ID="lblPPSTInductionWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                </tr>
                 <tr>                       
                     <td> </td> 
                     <td> </td> 
                     <td colspan="5"> <asp:Label ID="lblPPSTIndAddrWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                  </tr>
                <tr>                       
                     <td> </td> 
                     <td> </td> 
                     <td colspan="5"> <asp:Label ID="lblPPSTIndCSZWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                  </tr>
                <!-- PuroPost Plus-->
                    <tr>                        
                        <td colspan="7"  style="color:red; text-align:left">                             
                            <asp:Label ID="lblPuroPostPlusDetails" Text="PuroPost Plus" runat="server"  Visible="false"></asp:Label>                            
                        </td>                            
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblPPlusAcctl" Text="PuroPost Plus Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblPPlusAcct" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblPPlusTransitl" Text="Transit Days" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblPPlusTransit" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                 <tr>
                     <td style="font-weight:bold"> <asp:Label ID="lblPPlusInductionl" Text="Induction Address" runat="server" Visible="false"></asp:Label></td>
                     <td> </td> 
                     <td colspan="5"><asp:Label ID="lblPPlusInduction" Text="" runat="server" Visible="false"></asp:Label></td>                
                </tr>
                 <tr>                       
                     <td> </td> 
                     <td> </td> 
                     <td colspan="5"> <asp:Label ID="lblPPlusIndAddr" Text="" runat="server" Visible="false"></asp:Label></td>                
                  </tr>
                <tr>                       
                     <td> </td> 
                     <td> </td> 
                     <td colspan="5"> <asp:Label ID="lblPPlusIndCSZ" Text="" runat="server" Visible="false"></asp:Label></td>                
                  </tr>
                  <!-- PuroPost Plus West-->
                    <tr>                        
                        <td colspan="7"  style="color:red; text-align:left">                             
                            <asp:Label ID="lblPuroPostPlusDetailsWest" Text="PuroPost Plus West" runat="server"  Visible="false"></asp:Label>                            
                        </td>                            
                    </tr>
                 <tr>
                         <td style="font-weight:bold"> <asp:Label ID="lblPPlusAcctlWest" Text="PuroPost Plus Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblPPlusAcctWest" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:50px"></td>
                         <td style="font-weight:bold"> <asp:Label ID="lblPPlusTransitlWest" Text="Transit Days" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblPPlusTransitWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                    </tr>
                 <tr>
                     <td style="font-weight:bold"> <asp:Label ID="lblPPlusInductionlWest" Text="Induction Address" runat="server" Visible="false"></asp:Label></td>
                     <td> </td> 
                     <td colspan="5"><asp:Label ID="lblPPlusInductionWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                </tr>
                 <tr>                       
                     <td> </td> 
                     <td> </td> 
                     <td colspan="5"> <asp:Label ID="lblPPlusIndAddrWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                  </tr>
                <tr>                       
                     <td> </td> 
                     <td> </td> 
                     <td colspan="5"> <asp:Label ID="lblPPlusIndCSZWest" Text="" runat="server" Visible="false"></asp:Label></td>                
                  </tr>
                 <!-- Returns -->
                     <tr>                        
                        <td colspan="7"  style="color:red; text-align:left">                             
                            <asp:Label ID="lblReturndDetals" Text="Returns" runat="server"  Visible="false"></asp:Label>                           
                        </td>                            
                    </tr>
                 <tr>
                 <td style="font-weight:bold"> <asp:Label ID="lblReturnsAcctl" Text="Returns Account Number" runat="server" Visible="false"></asp:Label></td>
                         <td style="width:15px"></td>
                         <td> <asp:Label ID="lblReturnsAcct" Text="" runat="server" Visible="false"></asp:Label></td>
                         <td colspan="4"> <asp:Label ID="lblReturnsFlags" Text="" runat="server" Visible="true"></asp:Label></td>
                </tr>
                <tr>
                     <td style="font-weight:bold"> <asp:Label ID="lblReturnsAddressl" Text="Returns Address" runat="server" Visible="false"></asp:Label></td>
                     <td> </td> 
                     <td colspan="5"><asp:Label ID="lblReturnsAddress" Text="" runat="server" Visible="false"></asp:Label></td>                
                </tr>
                <tr>                       
                     <td> </td> 
                     <td> </td> 
                     <td colspan="5"> <asp:Label ID="lblReturnsCSZ" Text="" runat="server" Visible="false"></asp:Label></td>                
                  </tr>
                 </table>     
                </asp:Panel>
                <!-- Migration -->
                   <table>
                    <tr>
                        <td style="color:blue; text-align:left; font-weight:bold">
                            <asp:Label ID="lblMigration" Text="Migration Details" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            <asp:Label ID="lblMigDate" Text="Migration Date" runat="server"  Visible="false"></asp:Label>
                        </td>
                         <td>
                            <asp:Label ID="lblMigrationDate" Text="" runat="server"  Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPre" Text="Pre-Migration Solution" runat="server"  Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPremigration" Text="" runat="server"  Visible="false"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                         <td>
                            <asp:Label ID="lblPost" Text="Post-Migration Solution" runat="server"  Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPostmigration" Text="" runat="server"  Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>    
                          <hr />  </asp:Panel>
               <!-- Notes -->
                <table>
                       <tr>
                         <td style="color:blue; text-align:left"><p></p><b><asp:Label ID="lblNotesl" runat="server" Text="Notes"></asp:Label></b></td>
                         <td ></td>
                     </tr>   
                     <tr>                        
                        <td colspan="2">                             
                            <asp:Label ID="lblNotes" runat="server"></asp:Label><p></p>                            
                        </td>                            
                    </tr>
                 </table>     

        </asp:Panel>
         <br />
        <%-- <div style="margin: 20px 100px 60px 10px;">
            <table>
                 <tr>
                     <td>
                         <telerik:RadButton ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" Skin="Web20"  ></telerik:RadButton>
                     </td>
                     <td>
                         <telerik:RadButton ID="btnCancel" runat="server" Text="Cancel" Skin="Web20" OnClick="btnCancel_Click"  ></telerik:RadButton>
                     </td>
                 </tr>
             </table>
            <br />
        </div>--%>
         
        </div>
    </div>
    </form>
</body>
</html>

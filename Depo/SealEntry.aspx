<%@ Page Title="Depo | Seal Entry" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="SealEntry.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo | Seal Entry</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>
<i class="glyphicon glyphicon-transfer"></i> Seal Entry 
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="page-container">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-7 pull-md-left main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Entry ID</b>
<asp:TextBox ID="txtEntryID" Style="text-transform: uppercase;text-align:center" ReadOnly="true" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>

<div class="col-md-5 col-xs-12">
<div class="form-group text-label">
<b  >Entry Date</b>
<asp:TextBox ID="txtEntryDate" Style="text-transform:uppercase" TextMode="DateTimeLocal" class="form-control text-label" runat="server"   ></asp:TextBox>
</div>
</div>

</div>
<div class="row">
<div class="col-md-8 col-xs-12">
<div class="form-group text-label">
<b>Shipping Line</b>
<asp:DropDownList ID="ddlShipline" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                                         
</asp:DropDownList> 
</div>
</div>
    
</div>

    <div class="row">
        
<div class="col-md-8 col-xs-12">
<div class="form-group text-label">
<b>Prefix Series</b>
<asp:TextBox ID="txtPrefixSeries" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Series"
runat="server"   ></asp:TextBox>
</div>
</div>
    </div>

<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Seal No From</b>
<asp:TextBox ID="txtSealNoFrom" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidateQty()"  placeholder="From"
runat="server"   ></asp:TextBox>
</div>
</div>
      <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >To</b>
<asp:TextBox ID="txtSealNoTo" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidateQty()"  placeholder="To"
runat="server"   ></asp:TextBox>
</div>
</div>                           
</div>
 
<div class="row">
<div class="col-md-5 col-xs-12">
<div class="form-group text-label">
<b  >Seal Received Date</b>
<asp:TextBox ID="txtSealReceivedDate" Style="text-transform:uppercase" class="form-control text-label" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
</div>
</div>
                                 
</div>
 <div class="row">
     <div class="col-md-8 col-xs-12">
<div class="form-group text-label">
<b  >Remarks</b>
<asp:TextBox ID="txtRemarks" Style="text-transform:uppercase" class="form-control text-label" TextMode="MultiLine" Rows="2" runat="server"></asp:TextBox>
</div>
</div>
 </div>
        <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnSave_Click"  
Text="Save"  OnClientClick="return ValidationSave()" />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="SealEntry.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="SealMasterSummary.aspx" target="_blank"><b style="color:blue">Click here to views list of Seal Entry Summary</b> </a>
</div>
</div>
            <div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="SealReturnIssue.aspx" target="_blank"><b style="color:blue">Click here to views list of Seal Return Issue</b> </a>
</div>
</div>
<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="SealInventory.aspx" target="_blank"><b style="color:blue">Click here to views list of Seal Entry Inventory </b> </a>
</div>
</div>
    <div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="SealFormatSummary.aspx" target="_blank"><b style="color:blue">Click here to views list of Seal Format </b> </a>
     
</div>
</div>     
             <div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
 
    <a href="SealSummaryInventory.aspx" target="_blank"><b style="color:blue">Click here to views list of Seal Summary Inventory</b> </a>
</div>
</div>                  
</div>  
    <div class="col-md-5" style="display:none">
        <asp:Label runat="server" ID="lblfilename"></asp:Label>
        <asp:Label runat="server" ID="lblFilePath"></asp:Label>

    </div>                      
</asp:Panel>
                        
</div>
</div>


         
                      
                    
                   
                         
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<span><i runat="server" id="I3" class="fa fa-5x fa-check-circle-o text-success"></i></span>
<br />
<h4 class="modal-title">
<asp:Label ID="lblSession" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
</center>
</div>
<div class="modal-footer">                   
<a href="SealEntry.aspx" class="btn btn-info btn-block">OK</a>                                
</div>
</div>                    
</ContentTemplate>             
</asp:UpdatePanel>
</div>
</div>
               
</fieldset>

</div>
    <div class="col-md-5 pull-md-right main-content">
        <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
            
<div class="panel-body">
<asp:Panel ID="Panel1" runat="server" Enabled="true">

           <div class="row">
               <div class="col-sm-7 col-xs-12">
        <div class="form-group" style="padding-top:20px">
            <asp:FileUpload runat="server" ID="FileUpload1" />
        </div>
    </div>
    <div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnImport" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnImport_Click"  
Text="Import"  OnClientClick="return ValidationImport()" />
</div>                                
</div>
           </div>
    
    </asp:Panel></div></div>
    </div>
</div>
                               
</div>
                           
                          
                     
                       
                       
</div>
                 

</div>
       
         
</div>
   <script type="text/javascript">
       function ValidationImport() {

           var ddlShipline = document.getElementById('<%= ddlShipline.ClientID%>').value;



    var blResult = Boolean;
    blResult = true;



    if (ddlShipline == 0) {
        document.getElementById('<%= ddlShipline.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

    }

    
    //alert('hi')
    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    return blResult;
}
</script>
<script type="text/javascript">
function ValidationSave() {
                 
    var ddlShipline = document.getElementById('<%= ddlShipline.ClientID%>').value;
    var txtSealNoFrom = document.getElementById('<%= txtSealNoFrom.ClientID%>').value;
    var txtSealNoTo = document.getElementById('<%= txtSealNoTo.ClientID%>').value;
    var txtPrefixSeries = document.getElementById('<%= txtPrefixSeries.ClientID%>').value;
    var txtEntryDate = document.getElementById('<%= txtEntryDate.ClientID%>').value;
    var txtSealReceivedDate = document.getElementById('<%= txtSealReceivedDate.ClientID%>').value;

                   
               

var blResult = Boolean;
blResult = true;
 

                   
if (ddlShipline == 0) {
document.getElementById('<%= ddlShipline.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    if (txtSealNoFrom == "") {
        document.getElementById('<%= txtSealNoFrom.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (txtSealNoTo == "") {
        document.getElementById('<%= txtSealNoTo.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (ddlShipline == 9) {
        if (txtPrefixSeries == "") {
            document.getElementById('<%= txtPrefixSeries.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;
        }
    }
    if (txtEntryDate == "") {
        document.getElementById('<%= txtEntryDate.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (txtSealReceivedDate == "") {
        document.getElementById('<%= txtSealReceivedDate.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>

<script type="text/javascript">
function ValidateQty() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
return event.returnValue;
return event.returnValue = '';
}

function checkEmail(str) {
var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

if (reg.test(emailField.value) == false) {
alert('Invalid Email Address');
return false;
}

return true;
}

function CheckTelephone(tel) {

if (tel.length < 7)
alert("Invalid Telephone number.")
}

function CheckMobile(mob) {
if (mob.length < 10)
alert("Invalid Mobile number.");

}
</script>
</asp:Content>

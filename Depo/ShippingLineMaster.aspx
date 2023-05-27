<%@ Page Title="Depo |Shipping Line Master" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ShippingLineMaster.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo |Shipping Line Master</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>Shipping Line Master
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="page-container" style="margin-left: -25px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
 
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Shipping Line Master
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" >
<b >Line Code</b>
<asp:TextBox ID="txtlinecode" Style="text-transform: uppercase; " class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder=""></asp:TextBox>     
</div>
</div>

</div>

<div class="row">
<div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b  >Line Name</b>
<asp:TextBox ID="txtlineName" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Line Name"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b  >Address</b>
<asp:TextBox ID="txtaddress" Style="text-transform:uppercase" TextMode="MultiLine" Rows="2" class="form-control text-label"  placeholder="Address"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>City</b>
<asp:TextBox ID="txtcity" Style="text-transform: uppercase" class="form-control text-label" placeholder="City"
runat="server"></asp:TextBox>
</div>
</div>

 
</div>

<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Contact Person</b>
<asp:TextBox ID="txtcontactper" Style="text-transform: uppercase" class="form-control text-label" placeholder="Contact Person"
runat="server"></asp:TextBox>
</div>
</div>

<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Person Designation</b>
<asp:TextBox ID="txtperdestinston" Style="text-transform: uppercase" class="form-control text-label" placeholder="Person Designation"
runat="server"></asp:TextBox>
</div>
</div>
<asp:Label ID="lblCHano" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblChaname" Visible="false" runat="server" Text=""></asp:Label>
</div>

<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Contact Number 1</b>
<asp:TextBox ID="txtContactNumber1" Style="text-transform: uppercase" class="form-control text-label" onkeypress="return ValidatePhoneNo()" placeholder="Contact Number 1"
runat="server"></asp:TextBox>
</div>
</div>

<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Contact Number 2</b>
<asp:TextBox ID="txtContactNumber2" Style="text-transform: uppercase" class="form-control text-label" onkeypress="return ValidatePhoneNo()" placeholder="Contact Number 2"
runat="server"></asp:TextBox>
</div>
</div>

</div>

<div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Fax Number</b>
<asp:TextBox ID="txtFaxNumber" Style="text-transform: uppercase" class="form-control text-label" placeholder="Fax Number"
runat="server"></asp:TextBox>
</div>
</div>

<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b>Mobile Number</b>
<asp:TextBox ID="txtMobileNumber" Style="text-transform: uppercase" class="form-control text-label" onkeypress="return ValidatePhoneNo()" placeholder="Mobile Number"
runat="server"></asp:TextBox>
</div>
</div>

</div>
<div class="row">

<div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b>Email ID</b>
<asp:TextBox ID="txtEmailID" Style="text-transform: uppercase" class="form-control text-label" placeholder="Email ID"
runat="server"></asp:TextBox>
</div>
</div>
</div>
 
    <div class="row">
<div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b>Remarks</b>
<asp:TextBox ID="txtremarks" Style="text-transform: uppercase" class="form-control text-label" placeholder="Remarks"
TextMode="MultiLine" runat="server"></asp:TextBox>
</div>
</div>

</div>

    <div class="row">

        <div class="col-md-2 col-xs-12">
<div class="form-group text-label" style="padding-top: 22px;">
<asp:CheckBox ID="chkisInvoice" runat="server" Checked="true" />
<asp:HiddenField ID="hdlocation1" runat="server" Value="0" />
<asp:Label ID="IsActiveLabel1" runat="server" AssociatedControlID="chkisActive" CssClass="inline">Is Offloading Invoice?</asp:Label>
</div>
</div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label" style="padding-top: 22px;">
<asp:CheckBox ID="chkisActive" runat="server" Checked="true" />
<asp:HiddenField ID="hdlocation" runat="server" Value="0" />
<asp:Label ID="IsActiveLabel" runat="server" AssociatedControlID="chkisActive" CssClass="inline">Is Active?</asp:Label>
</div>
</div>
</div>
                               
</asp:Panel>
                        
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
                           
<a href="AccountMaster.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="LineSummary.aspx" target="_blank"><b style="color:blue">Click here to views list of Shipping Line Details </b> </a>
</div>
</div>
                         
</div>
<asp:Label ID="lblAccountID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblAccountName" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblagentname" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         
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
                   
<a href="ShippingLineMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
               
</fieldset>

</div>
</div>
                               
</div>
                           
                          
                     
                       
                       
</div>
                 
</ContentTemplate>
</asp:UpdatePanel>
</div>
       
         
</div>
   
<script type="text/javascript">
function ValidationSave() {
                 
    var txtlinecode = document.getElementById('<%= txtlinecode.ClientID%>').value;
    var txtlineName = document.getElementById('<%= txtlineName.ClientID%>').value;
                   
               

var blResult = Boolean;
blResult = true;
 

                   
if (txtlinecode == "") {
document.getElementById('<%= txtlinecode.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
    if (txtlineName == "") {
        document.getElementById('<%= txtlineName.ClientID%>').style.borderColor = "red";
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
function ValidatePhoneNo() {
    //alert('hii')
    if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 43 || event.keyCode == 32 || event.keyCode == 40 || event.keyCode == 41)
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

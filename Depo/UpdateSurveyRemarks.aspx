<%@ Page Title="Depo |Update Survey Remarks" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="UpdateSurveyRemarks.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo |Update Survey Remarks</title>
       
</head>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>Update Survey Remarks
</h3>           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
<div class="page-container" style="margin-left: -24px; margin-right: -5px; margin-top: -18px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
 
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 
    

    <div class="row">
        <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtcontainerNo" Style="text-transform:uppercase" MaxLength="11"   class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>

        <div class="col-sm-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnShow" class="btn btn-primary btn btn-sm outline " OnClientClick="return ValidationShow()" runat="server" OnClick="btnShow_Click"  
Text="Show"   />
</div>                                                                              
</div>


        
       

    </div>

    <div class="row">
         <div class="col-md-2 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Container ID</b>
<asp:TextBox ID="txtContainerID" Style="text-transform:uppercase"   class="form-control text-label"  placeholder="Container ID"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-sm-2 col-xs-12">                                      
<div class="form-group text-label">
<b>MFG Date</b>                                         
<asp:TextBox ID="txtMFGDate"  placeholder="yyyy-mm-dd" TextMode="Month" format="MM-yyyy"   runat="server"   Class="form-control text-label"></asp:TextBox>
</div>
</div>

         <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Size</b>
<asp:TextBox ID="txtSize" Style="text-transform:uppercase"  ReadOnly="true"   class="form-control text-label"  placeholder="Size"
runat="server"   ></asp:TextBox>
</div>
</div>

          <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >CSC/ASP</b>
<asp:TextBox ID="txtCSCASP" Style="text-transform:uppercase"      class="form-control text-label"  placeholder="CSC/ASP"
runat="server"   ></asp:TextBox>
</div>
</div>
    </div>

    <div class="row">
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Type</b>
    <asp:DropDownList ID="ddlType"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">
 
     </asp:DropDownList>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Pay Load</b>
<asp:TextBox ID="txtPayLoad" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Pay Load"
runat="server"></asp:TextBox>
</div>
</div>
         <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Container Status</b>
<asp:DropDownList ID="ddlcontainerstatus"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="Cargo Worthy">Cargo Worthy</asp:ListItem>
<asp:ListItem Value="Non Cargo Worthy">Non Cargo Worthy</asp:ListItem>                         
</asp:DropDownList> 
</div>
</div>

    </div>

    <div class="row">
         <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Line Name</b>
<asp:TextBox ID="txtlinename" Style="text-transform:uppercase"   ReadOnly="true"  class="form-control text-label"  placeholder="Line Name"
runat="server"   ></asp:TextBox>
</div>
</div>
    </div>
    <div class="row">
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Gross Weight</b>
<asp:TextBox ID="txtgrossweight" Style="text-transform:uppercase"   class="form-control text-label" onchange="GetNetWeight()"  placeholder="Gross Weight"
runat="server"   ></asp:TextBox>
</div>
</div>
        

        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Tare Weight</b>
<asp:TextBox ID="txttareweight" Style="text-transform:uppercase"   class="form-control text-label" onchange="GetNetWeight()"  placeholder="Tare Weight"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Net Weight</b>
<asp:TextBox ID="txtNetWeight" Style="text-transform:uppercase"   class="form-control text-label"  placeholder="Net Weight"
runat="server"   ></asp:TextBox>
</div>
</div>
        
    </div>
     <div class="row">
         <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Damage Remarks</b>
<asp:TextBox ID="txtdamageremarkms" Style="text-transform:uppercase" TextMode="MultiLine" Rows="2"     class="form-control text-label"  placeholder="Damage Remarks"
runat="server"   ></asp:TextBox>
</div>
</div>
         </div>
         <div class="row" style="display:none">
         <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Survey Remarks</b>
<asp:TextBox ID="txtsurveyremarks" Style="text-transform:uppercase" TextMode="MultiLine" class="form-control text-label"  placeholder="Survey Remarks"
runat="server"   ></asp:TextBox>
</div>
</div>
    </div>

     <div class="row" style="display:none">
         <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Booking Remarks</b>
<asp:TextBox ID="txtBookingremarks" Style="text-transform:uppercase"  TextMode="MultiLine"  class="form-control text-label"  placeholder="Booking Remarks"
runat="server"   ></asp:TextBox>
</div>
</div>
    </div>
             <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline " runat="server"  
Text="Save" OnClientClick="return ValidationSave()" OnClick="btnSave_Click"   />
</div>                                
</div>
                       
<div class="col-sm-1" style="padding-left:-0px;">
<div class="form-group" style="padding-top:15px">                    
<a href="UpdateSurveyRemarks.aspx" id="btnclear" runat="server" class="btn btn-primary  btn btn-sm outline ">
Clear
</a>                               
</div>                                                                            
</div>                  
</div>                
</asp:Panel>
                         
</div>
</div>



<asp:Label ID="lblLineId" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblentryid" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         
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
                   
<a href="UpdateSurveyRemarks.aspx" class="btn btn-info btn-block">OK</a>
                                
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
                 
              
</div>
       
         
</div>
   
<script type="text/javascript">
function ValidationSave() {
            
    var txtcontainerNo = document.getElementById('<%= txtcontainerNo.ClientID%>').value;
    var txtdamageremarkms = document.getElementById('<%= txtdamageremarkms.ClientID%>').value;
 
                  


var blResult = Boolean;
blResult = true;
              
if (txtcontainerNo == "") {
document.getElementById('<%= txtcontainerNo.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

    if (txtdamageremarkms == "") {
document.getElementById('<%= txtdamageremarkms.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

                
              
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
    function GetNetWeight() {
        var txtgrossweight = document.getElementById('<%= txtgrossweight.ClientID%>').value;
        var txttareweight = document.getElementById('<%= txttareweight.ClientID%>').value;
        if (((txtgrossweight != 0) || (txtgrossweight != "")) && ((txttareweight != 0) || (txttareweight != ""))) {

            document.getElementById('<%= txtNetWeight.ClientID%>').value = parseFloat(txtgrossweight) - parseFloat(txttareweight);
        }
        
    }
</script>

    <script type="text/javascript">
        function ValidationShow() {

            var txtcontainerNo = document.getElementById('<%= txtcontainerNo.ClientID%>').value;
    




    var blResult = Boolean;
    blResult = true;

    if (txtcontainerNo == "") {
        document.getElementById('<%= txtcontainerNo.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;
}

     

    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    return blResult;
}
</script>

<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy').datepicker({
format: 'yyyy-mm-dd',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,



})

});



</script>  

<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy2').datepicker({
format: 'yyyy-mm-dd',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,



})

});

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

<script type="text/javascript">
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

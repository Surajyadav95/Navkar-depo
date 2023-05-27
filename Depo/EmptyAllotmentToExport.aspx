<%@ Page Title="Depo | Empty Allotment to Export" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EmptyAllotmentToExport.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo | Empty Allotment to Export</title>
       
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>  Empty Allotment to Export 
</h3>
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
 
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-9 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
  

<div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Booking No</b>
<asp:TextBox ID="txtBookingNo" Style="text-transform: uppercase;" ReadOnly="true" OnTextChanged="txtBookingNo_TextChanged" AutoPostBack="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Booking No"></asp:TextBox>     
</div>
</div>
      

     <div class="col-sm-1 col-xs-6">                                    
        <div class="form-group pull-left" style="padding-top:20px; height: 40px; ">
        <asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
        OnClientClick="return emptysearch();">  
        <i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
        </div>                                 
        </div>
</div>

<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtcontainerNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-sm-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnshow" class="btn btn-primary btn btn-sm outline " OnClientClick="return ValidationShow()"  runat="server"  OnClick="btnshow_Click" 
Text="Show"  />
</div>
                       <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />                       
                                      
</div>
</div>

<div class="row">
   <div class="col-sm-3 col-xs-12">                                      
<div class="form-group date text-label">
<b >Allotment Date</b>
<asp:TextBox ID="txtAllotmentDate" Style="border-radius:4px;"    placeholder="dd-MM-yyyy" TextMode="DateTimeLocal" runat="server"  Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>
 
 
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Entry ID</b>
<asp:TextBox ID="txtentryID" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"   placeholder="Entry ID"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Shipping Line</b>
<asp:TextBox ID="txtShippingLine" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"   placeholder="Shipping Line"
runat="server"   ></asp:TextBox>
</div>
</div>
                                 
 
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Size</b>
<asp:TextBox ID="txtsize" Style="text-transform:uppercase" ReadOnly="true"  class="form-control text-label"   placeholder="Size"
runat="server"   ></asp:TextBox>
</div>
</div>
                                 
</div>

    <div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Type</b>
<asp:TextBox ID="txtType" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"   placeholder="Type"
runat="server"   ></asp:TextBox>
</div>
</div>
                                 
 
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >In Date</b>
<asp:TextBox ID="txtindate" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"    
runat="server"   ></asp:TextBox>
</div>
</div>
                                 
</div>
          

   
    <div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >On Account</b>
<asp:DropDownList ID="ddlOnaccount"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList>
</div>
</div>
                                 
</div>
         
     <div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Seal No</b>
<asp:TextBox ID="txtseal" Style="text-transform:uppercase" class="form-control text-label"   placeholder="Seal No"
runat="server"   ></asp:TextBox>
</div>
</div>
                                 
 
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Allotment ID</b>
<asp:TextBox ID="txtAllotmentID" Style="text-transform:uppercase" class="form-control text-label"   placeholder="Allotment ID"
runat="server"   ></asp:TextBox>
</div>
</div>
                                 
</div>         
          

       <div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >POD</b>
<asp:TextBox ID="txtpod" Style="text-transform:uppercase" class="form-control text-label"   placeholder="POD"
runat="server"   ></asp:TextBox>
</div>
</div>
                                 
</div>
                   
    
    <div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Remarks<b>
<asp:TextBox ID="txtRemarks" Style="text-transform:uppercase" TextMode="MultiLine" class="form-control text-label"   placeholder="Remarks"
runat="server"   ></asp:TextBox>
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
                           
<a href="EmptyAllotmentToExport.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
 
                         
</div>           
</asp:Panel>
                        
</div>
</div>



<asp:Label ID="lblbksize" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblbktype" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblbkslid" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         
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
                   
<a href="EmptyAllotmentToExport.aspx" class="btn btn-info btn-block">OK</a>
                                
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



        var blResult = Boolean;
        blResult = true;



        if (txtcontainerNo == "") {
            document.getElementById('<%= txtcontainerNo.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}

        //alert('hi')
    if (blResult == false) {
        alert('Please Enter the Container No');
    }
    return blResult;
}
</script>
      <script type="text/javascript">
          var popup;
          function emptysearch() {

              var url = "EmptySearchBookingExport.aspx"

              popup = window.open(url, "Popup", "width=710,height=555");
              popup.focus();

          }
</script>
<script type="text/javascript">
    function ValidationShow() {
                 
        var txtcontainerNo = document.getElementById('<%= txtcontainerNo.ClientID%>').value;
        var ddlOnaccount = document.getElementById('<%= ddlOnaccount.ClientID%>').value;
                   
               

var blResult = Boolean;
blResult = true;
 

                   
if (txtcontainerNo == "") {
document.getElementById('<%= txtcontainerNo.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
        if (ddlOnaccount == 0) {
            document.getElementById('<%= ddlOnaccount.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;
        }
//alert('hi')
if (blResult == false) {
    alert('Please Enter the Container No');
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

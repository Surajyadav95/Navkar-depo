<%@ Page Title="Depo |ISO Master" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ISOMaster.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo |ISO Master</title>
       
</head>
           <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:300px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>ISO Master
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
ISO Code Master
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" >
<b >ISO ID</b>
<asp:TextBox ID="txtIsoID" placeholder="New" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true" class="form-control text-label form-cascade-control"
runat="server" Text="" ></asp:TextBox>     
</div>
</div>

</div>

<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >ISO Code</b>
<asp:TextBox ID="txtISOCode" Style="text-transform:uppercase" class="form-control text-label"  placeholder="ISO Code"
runat="server"   ></asp:TextBox>
</div>
</div>
 
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Container HT</b>
<asp:TextBox ID="txtcontainerHt" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Container HT"
runat="server"   ></asp:TextBox>
</div>
</div>
 
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b>Container Width</b>
<asp:TextBox ID="txtContainerWidth" Style="text-transform: uppercase" class="form-control text-label" placeholder="Container Width"
runat="server"></asp:TextBox>
</div>
</div>

 
</div>

<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Container Size</b>
<asp:DropDownList ID="ddlContainerSize"   Style="text-transform: uppercase;" runat="server"    class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>     
    <asp:ListItem Value="1">20</asp:ListItem>  
    <asp:ListItem Value="2">40</asp:ListItem>  
    <asp:ListItem Value="3">45</asp:ListItem>   
</asp:DropDownList> 
</div>
</div>

<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Container Type</b>
<asp:DropDownList ID="ddlCOntainerType"   Style="text-transform: uppercase;" runat="server"    class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>      
</asp:DropDownList> 
</div>
</div>
<asp:Label ID="lblCHano" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblChaname" Visible="false" runat="server" Text=""></asp:Label>
 
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b>Tare Weight</b>
<asp:TextBox ID="txtTareWeight" Style="text-transform: uppercase" class="form-control text-label" onkeypress="return ValidatePhoneNo()" placeholder="Tare Weight"
runat="server"></asp:TextBox>
</div>
</div>

 

</div>

    <div class="row">
        <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b>Short Name</b>
<asp:TextBox ID="txtShortName" Style="text-transform: uppercase" class="form-control text-label"  placeholder="Short Name"
runat="server"></asp:TextBox>
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
                       
<div class="col-sm-1" style="padding-left:0px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="ISOMaster.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
<%--<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="AccountSummary.aspx" target="_blank"><b style="color:blue">Click here to views list of accounts head</b> </a>
</div>
</div>--%>
                         
</div>

      <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive  scrolling-table-container">
<asp:GridView ID="GrdISoFill" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"    >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
  
</Columns>

</asp:GridView>
</div>
</div>
</div>
 
</asp:Panel>
                        
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
                   
<a href="ISOMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
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
   
<%--<script type="text/javascript">
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
</script>--%>

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

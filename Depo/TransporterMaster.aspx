<%@ Page Title="Depo |Transporter Master" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="TransporterMaster.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo |Transporter Master</title>
       
</head>
     <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:250px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>Transporter Master
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="page-container" style="margin-left: -35px; margin-right: -5px; margin-top: -18px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Transporter Master
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Transporter ID</b>
<asp:TextBox ID="txtTransporterID" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>

</div>

<div class="row">
<div class="col-md-5 col-xs-12">
<div class="form-group text-label">
<b  >Transporter Name</b>
<asp:TextBox ID="txtTransporterName" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Transporter Name"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Transporter Code</b>
<asp:TextBox ID="txtTransporterCode" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Transporter Code"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-8 col-xs-12">
<div class="form-group text-label">
<b  >Address</b>
<asp:TextBox ID="txtAddress" Style="text-transform:uppercase" TextMode="MultiLine"  class="form-control text-label"  placeholder="Address.."
runat="server"   ></asp:TextBox>
</div>
</div>
</div>

<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Email</b>
<asp:TextBox ID="txtEmail" Style="text-transform:uppercase" class="form-control text-label"   placeholder="Email"
runat="server"   ></asp:TextBox>
</div>
</div>
                                 
 
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Mobile No</b>
<asp:TextBox ID="txtMobile" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidateQty()"   placeholder="Mobile"
runat="server"   ></asp:TextBox>
</div>
</div>
                                 
</div>
 
<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Bank Name</b>
<asp:DropDownList ID="ddlBankNAme"   Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
                               
    
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >IFC Code</b>
<asp:TextBox ID="txtIFCCode" Style="text-transform:uppercase" class="form-control text-label"   placeholder="IFC Code"
runat="server"   ></asp:TextBox>
</div>
</div>
                                 
</div>

    <div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Bank Account No</b>
<asp:TextBox ID="txtAccountNo" Style="text-transform:uppercase" class="form-control text-label" onkeypress="return ValidateQty()"  placeholder="Bank Account No"
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
                       
<div class="col-sm-1" style="padding-left:0px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="TransporterMaster.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
 
                         
</div>        
    
     <div class="row">
        <div class="col-sm-4 col-xs-12 ">
<div class="form-group text-label">
<b  >Search</b>
<asp:TextBox ID="txtsearcht" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>

        <div class="col-sm-1 col-xs-2 pull-left">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnsearch" class="btn btn-primary btn-sm outline  " runat="server" OnClick="btnsearch_Click"  
Text="Search"  />
</div>
                                              
                                      
</div>
                         
</div>       
    
    <div class="row">


<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="true" EmptyDataText="No records found!"  ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
     <asp:TemplateField>
<ItemTemplate>


<a  href='<%# "TransporterMaster.aspx?TransIDEdit=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "Id")).ToString())%>' 
Class='btn btn-info btn-xs outline' 
>Edit</a>
   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="60px" />
</asp:TemplateField>

<%--<asp:BoundField DataField="Id" HeaderText="Entry ID"></asp:BoundField>
 <asp:BoundField DataField="Trailer No" HeaderText="Trailer No"></asp:BoundField>                                                 
<asp:BoundField DataField="Driver Name" HeaderText="Driver Name"></asp:BoundField>
<asp:BoundField DataField="Trailer Type" HeaderText="Trailer Type"></asp:BoundField>
    <asp:BoundField DataField="Trailer_Mov_type" HeaderText="Owned By"></asp:BoundField>
    <asp:BoundField DataField="TransName" HeaderText="Transporter"></asp:BoundField>
    <asp:BoundField DataField="Reg. Date" HeaderText="Reg. Date"></asp:BoundField>
    <asp:BoundField DataField="VehicleType" HeaderText="Vehicle Type"></asp:BoundField>
    <asp:BoundField DataField="Added By" HeaderText="Added By"></asp:BoundField>--%>
     


</Columns>

</asp:GridView>
</div>
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
                   
<a href="TransporterMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
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
 
    var txtTransporterName = document.getElementById('<%= txtTransporterName.ClientID%>').value;
    //alert('hi')
    var txtAddress = document.getElementById('<%= txtAddress.ClientID%>').value;              
    var txtTransporterName = document.getElementById('<%= txtEmail.ClientID%>').value;     
    var txtTransporterCode = document.getElementById('<%= txtTransporterCode.ClientID%>').value;     
 var blResult = Boolean;
blResult = true;
 

                   
if (txtTransporterName == "") {
document.getElementById('<%= txtTransporterName.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}

    if (txtAddress == "") {
        document.getElementById('<%= txtAddress.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
    if (txtEmail == "") {
        document.getElementById('<%= txtEmail.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
    if (txtTransporterCode == "") {
        document.getElementById('<%= txtTransporterCode.ClientID%>').style.borderColor = "red";
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

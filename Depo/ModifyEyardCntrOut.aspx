<%@ Page Title="Depo | Modify Eyard Container Out" Language="VB" EnableEventValidation="false" MasterPageFile="~/Depo/PopUp.master" AutoEventWireup="false" CodeFile="ModifyEyardCntrOut.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
window.opener.document.getElementById("MainContent_btnIndentItem").click();
self.close();
}
</script>
<div class="container" style="background-color: white">

<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Modify Eyard Container Out<small class="pull-left" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>
<div class="row ">
 
<div class="col-sm-4 col-xs-5 ">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtContainerno" Style="text-transform:uppercase;" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-1 col-xs-2 pull-left">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnsearch" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnsearch_Click"  
    OnClientClick="return ValidationShow()"
Text="Show"  />
</div>
                                              
                                      
</div>
</div>

    <div class="row">
         <div class="col-sm-4 col-xs-12">                                      
<div class="form-group text-label">
<b>Out Date</b>                                         
<asp:TextBox ID="txtoutdate"  placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server" OnTextChanged="txtoutdate_TextChanged"  AutoPostBack="true"  Class="form-control text-label"></asp:TextBox>
</div>
</div>
    </div>
    <div class="row" style="display:none">
<div class="col-md-4 col-xs-8">
<div class="form-group text-label">
<b>Movement Type</b>
<asp:DropDownList ID="ddlMovementtype" Style="text-transform: uppercase;"  runat="server" class="form-control " >
           <asp:ListItem Value="">-Select-</asp:ListItem> 
<asp:ListItem Value="Sale Box">Sale Box</asp:ListItem>
<asp:ListItem Value="Offhire">Offhire</asp:ListItem>
    <asp:ListItem Value="Empty Depo">Empty Depo</asp:ListItem>
<asp:ListItem Value="Export PickUp">Export PickUp</asp:ListItem>   
    <asp:ListItem Value="Vessel Repo">Vessel Repo</asp:ListItem>
</asp:DropDownList>
</div>
</div>

</div>
        <div class="row">
<div class="col-sm-4 col-xs-5">
<div class="form-group text-label">
<b  >Booking No</b>
<asp:TextBox ID="txtBooking" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Booking No"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>
    <div class="row">
<div class="col-sm-4 col-xs-5">
<div class="form-group text-label">
<b  >Vehicle No</b>
<asp:TextBox ID="txtVehicle" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Vehicle No"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>


    <div class="row">
<div class="col-sm-6 col-xs-6">
<div class="form-group text-label">
<b  >Transporter Name</b>
<asp:TextBox ID="txtTransporter" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Transporter Name"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>
 
     <div class="row">
<div class="col-sm-6 col-xs-6">
<div class="form-group text-label">
<b  >Shipper Name</b>
<asp:TextBox ID="txtShipper" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Shipper Name"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>



<div class="row">
<div class="col-sm-4 col-xs-5">
<div class="form-group text-label">
<b  >Seal No</b>
<asp:TextBox ID="txtSeal" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Seal No"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>

<div class="row">
<div class="col-sm-6 col-xs-6">
<div class="form-group text-label">
<b  >Location</b>
<asp:TextBox ID="txtLocation" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Location"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>

    <div class="row">
<div class="col-sm-4 col-xs-5">
<div class="form-group text-label">
<b  >POD</b>
<asp:TextBox ID="txtPOD" Style="text-transform:uppercase" class="form-control text-label"  placeholder="POD"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>

    <div class="row">
<div class="col-sm-6 col-xs-6">
<div class="form-group text-label">
<b  >Vessel Name</b>
<%--<asp:TextBox ID="txtVessel" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Vessel Name"
runat="server"   ></asp:TextBox>--%>
    <asp:DropDownList Style="text-transform:uppercase" class="form-control text-label" runat="server" ID="ddlVesselName"></asp:DropDownList>
</div>
</div>

</div>

    <div class="row">
<div class="col-sm-6 col-xs-6">
<div class="form-group text-label">
<b  >Remarks</b>
<asp:TextBox ID="txtRemarks" class="form-control text-label"  placeholder="Remarks"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>

    <asp:Label ID="lblEntryID" Visible="false" runat="server" Text=""></asp:Label>

            <div class="row">
    <div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnUpdate" class="btn btn-primary btn-sm outline  " runat="server" OnClick="btnUpdate_Click"  
Text="Update"  OnClientClick="return ValidationSave()"  />
</div>
                                              
                                      
</div>
                <div class="col-sm-1" style="padding-left:26px;">
<div class="form-group" style="padding-top:18px">                          
<a href="ModifyEyardCntrOut.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a>                               
</div>                                                                               
</div>
                </div>

<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
         
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
                   
<a href="ModifyEyardCntrOut.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
                
</div>
</div>
     
    <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
 
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblquoteApprove"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btncancelyes" data-dismiss="modal" runat="server"  onserverclick="btncancelyes_ServerClick"  aria-hidden="true">
Yes 
</button>
<a href="ModifyEyardCntrOut.aspx" class="btn btn-danger ">No</a>
</div>
</div>
 
</div>
</div>             

</div>
</div>
</div>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtContainerno = document.getElementById('<%= txtContainerno.ClientID%>').value;
    var ddlMovementtype = document.getElementById('<%= ddlMovementtype.ClientID%>').value;
                   

var blResult = Boolean;
blResult = true;
 

if (txtContainerno == "") {
document.getElementById('<%= txtContainerno.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    <%--if (ddlMovementtype == "") {
        document.getElementById('<%= ddlMovementtype.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }--%>
//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
    function ValidationShow() {

        var txtContainerno = document.getElementById('<%= txtContainerno.ClientID%>').value;
        

        var blResult = Boolean;
        blResult = true;


        if (txtContainerno == "") {
            document.getElementById('<%= txtContainerno.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;
        }
        //alert('hi')
        if (blResult == false) {
            alert('Please fill the required fields!');
        }
        return blResult;
    }
</script>
</asp:Content>



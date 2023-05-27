<%@ Page Title="Depo | Cancel Approved Containers" Language="VB" EnableEventValidation="false" MasterPageFile="~/Depo/PopUp.master" AutoEventWireup="false" CodeFile="CancelApprove.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

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
<h1>Cancel Approved Containers<small class="pull-left" style="margin-right:20px"></small></h1>
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
    OnClientClick="return ValidationSave()"
Text="Show"  />
</div>
                                              
                                      
</div>
</div>



    <div class="row">
<div class="col-sm-4 col-xs-5">
<div class="form-group text-label">
<b  >Approve Date</b>
<asp:TextBox ID="txtAppdate" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Estimate Date"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>


    <div class="row">
<div class="col-sm-4 col-xs-5">
<div class="form-group text-label">
<b  >Approve Amt</b>
<asp:TextBox ID="txtAmt" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Approve Amt"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>
 
     <div class="row">
<div class="col-sm-4 col-xs-5">
<div class="form-group text-label">
<b  >CSC/ASP</b>
<asp:TextBox ID="txtCSC" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="CSC/ASP"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>
    
       <div class="row">
<div class="col-sm-4 col-xs-5">
<div class="form-group text-label">
<b  >Description</b>
<asp:TextBox ID="txtDesc" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Description"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>




    <asp:Label ID="lblEntryID" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblEstimat_ID" Visible="false" runat="server" Text=""></asp:Label>

            <div class="row">
    <div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btncancel" class="btn btn-primary btn-sm outline  " runat="server" OnClick="btncancel_Click"  
Text="Cancel"  OnClientClick="return ValidationSave()"  />
</div>
                                              
                                      
</div>
                <div class="col-sm-1" style="padding-left:26px;">
<div class="form-group" style="padding-top:18px">                          
<a href="CancelApprove.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
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
                   
<a href="CancelApprove.aspx" class="btn btn-info btn-block">OK</a>
                                
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
<a href="CancelApprove.aspx" class="btn btn-danger ">No</a>
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



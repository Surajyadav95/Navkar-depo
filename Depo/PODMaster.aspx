<%@ Page Title="Hazira" Language="VB" EnableEventValidation="false" MasterPageFile="PopUp.master" AutoEventWireup="false" CodeFile="PODMaster.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

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
<div class="col-md-12 col-xs-12 pull-left">
<div class="header-lined">
<h1>POD Master  <small class="pull-right" style="margin-right: 20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

</div>

</div>

<div class="row">
    <div class="col-sm-4 col-xs-4">
<div class="form-group text-label">
<b>POD ID</b>
<asp:TextBox ID="txtPortCode" Style="text-transform: uppercase" ReadOnly="true" class="form-control text-label" placeholder="NEW"
runat="server"></asp:TextBox>
</div>
</div>

<div class="col-sm-6 col-xs-6">
<div class="form-group text-label">
<b>POD Name</b>
<asp:TextBox ID="txtPortName" Style="text-transform: uppercase" class="form-control text-label" placeholder="POD Name"
runat="server"></asp:TextBox>
</div>
</div>
    </div>
    <div class="row">
 <div class="col-sm-6 col-xs-6">
<div class="form-group text-label">
<b>Country</b>
<asp:TextBox ID="txtcontry" Style="text-transform: uppercase" class="form-control text-label" placeholder="Country"
runat="server"></asp:TextBox>
</div>
</div>
 
<div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top: 18px">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnSave_Click"
Text="Save" OnClientClick="return ValidationSave()" />
</div>

</div>

</div>

<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>

<div class="row">
               <div class="col-sm-6 col-xs-8">
<div class="form-group text-label">
<b>Search</b>
<asp:TextBox ID="txtSearch" Style="text-transform: uppercase" class="form-control text-label" placeholder="Search By Port Name"
runat="server"></asp:TextBox>
</div>
</div>

<div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top: 18px">
<asp:Button ID="Button1" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="Button1_Click"
Text="Search" />
</div>

</div>      
    </div>
<div class="row">
<div class="col-md-6 col-xs-8 text-label " style="padding-right: 60px;">
<div class="table-responsive scrolling-table-container" style="margin-left: 28px; margin-right: 0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging" AllowPaging="true" PageSize="5">
<PagerStyle BackColor="white" ForeColor="blue" Font-Underline="false" Height="30px" VerticalAlign="Bottom" HorizontalAlign="center" />
<Columns>


<asp:BoundField DataField="PODName" HeaderText="POD Name"></asp:BoundField>

</Columns>

</asp:GridView>
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

<a href="PODMaster.aspx" class="btn btn-info btn-block">OK</a>

</div>
</div>

                           
</div>
</div>


</div>
</div>
</div>
<script type="text/javascript">
function ValidationSave() {

    var txtPortName = document.getElementById('<%= txtPortName.ClientID%>').value;


var blResult = Boolean;
blResult = true;


if (txtPortName == "") {
document.getElementById('<%= txtPortName.ClientID%>').style.borderColor = "red";
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



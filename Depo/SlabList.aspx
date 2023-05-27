<%@ Page Title="Epic" Language="VB" EnableEventValidation="false" MasterPageFile="~/Depo/PopUp.master" AutoEventWireup="false" CodeFile="SlabList.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
    window.opener.document.getElementById("ContentPlaceHolder1_").click();
    self.close();
}
</script>
<div class="container" style="background-color: white">
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<%--<h1>Lot Details<small class="pull-right" style="margin-right:20px"></small></h1>--%>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>   
<asp:Label ID="lblslabID" Visible="false" runat="server" Text=""></asp:Label>                           
 

             

<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>

<div class="row">                  

<div class="col-md-12 col-xs-12 text-label " style="padding-right:60px;">
<div class="table-responsive scrolling-table-container" style="margin-right: 0px;">
<asp:GridView ID="grdLotList" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">

<Columns>
                                 
<asp:BoundField DataField="slabon" HeaderText="Slab On" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="fromslab" HeaderText=" From Slab" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="toslab" HeaderText="To Slab" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
    <asp:BoundField DataField="value" HeaderText="Value" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>

</Columns>

</asp:GridView>
</div>
</div>
</div>                        
             
</div>
</div>
</div>

</asp:Content>



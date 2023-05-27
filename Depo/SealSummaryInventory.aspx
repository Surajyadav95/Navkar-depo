<%@ Page Title="Depo | Seal Inventory" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="SealSummaryInventory.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Seal Inventory</title>
</head>
    <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:500px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Seal Inventory
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body">
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                                                
<div class="row">
    <div class="col-sm-3 col-xs-12">                                      
<div class="form-group date text-label">
<b > As On</b>
<asp:TextBox ID="txtason" Style="border-radius:4px;"    placeholder="dd-MM-yyyy" TextMode="DateTimeLocal" runat="server"  Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>

<div class="col-md-2 col-xs-12" style="display:none" >
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">All</asp:ListItem> 
<asp:ListItem Value="1">Shipping Line</asp:ListItem>
    <asp:ListItem Value="2">Jo Type</asp:ListItem>
</asp:DropDownList>                                               
</div>
</div>

<div class="col-md-4 col-xs-12"  runat="server" id="divShippingLine" style="display:none">
<div class="form-group text-label">
<b>Shipping Line</b>
<asp:DropDownList ID="ddlLineName" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control"></asp:DropDownList>
</div>
</div>

    <div class="col-md-3 col-xs-12"  runat="server" id="divJotype" style="display:none">
<div class="form-group text-label">
<b>Jo Type</b>
<asp:DropDownList ID="ddljoype" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control"></asp:DropDownList>
</div>
</div>
    
    <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
    </div>                                         
                                      
</div>

    <div class="row" style="display:none">
    <div class="col-lg-12 col-xs-12 text-label ">

<div class="col-sm-5 col-xs-12 pull-right">

<div class="col-sm-3 col-xs-12">
<b>20:</b>
<asp:Label runat="server" ID="lbl20" ></asp:Label>
&nbsp</div>
<div class="col-sm-3 col-xs-12">
<b>40:</b>
<asp:Label runat="server" ID="lbl40" ></asp:Label>
&nbsp</div>
<div class="col-sm-3 col-xs-12">
<b>45:</b>
<asp:Label runat="server" ID="lbl45" ></asp:Label>
&nbsp</div>
    <div class="col-sm-3 col-xs-12">
<b>Teus:</b>
<asp:Label runat="server" ID="lblTEUS" ></asp:Label>
&nbsp</div>
    
</div>
    </div>
    </div>
    <br />


  <div class="panel panel-default" style=" padding: 10px; margin: 10px;">
        <div id="Tabs" role="tabpanel">
                <ul class="nav nav-tabs" role="tablist" >
<li   class="active"><a href="#Summary" aria-controls="Summary Details" role="tab" data-toggle="tab"><b>Summary</b></a></li>
                
<li  ><a href="#Container" aria-controls="Vehicle Container" role="tab" data-toggle="tab"><b>Container Wise Summary</b></a></li>
 
            </ul>
            <div class="tab-content" style="padding-top: 20px">
                <div role="tabpanel" class="tab-pane active" id="Summary">
<div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdsummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdsummary_PageIndexChanging"  AllowPaging="true" PageSize="10" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
 
                                                   
<%--<asp:BoundField DataField="Sr.No" HeaderText="Sr No"  HeaderStyle-CssClass="header-center"  ItemStyle-HorizontalAlign="Center"></asp:BoundField>
   <asp:BoundField DataField="Type" HeaderText="Type"  HeaderStyle-CssClass="header-center"  ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="20" HeaderText="20"   HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="40" HeaderText="40"  HeaderStyle-CssClass="header-center"  ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="45" HeaderText="45"   HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="TOTAL" HeaderText="Total"  HeaderStyle-CssClass="header-center"  ItemStyle-HorizontalAlign="Center"></asp:BoundField>
     <asp:BoundField DataField="TEUS" HeaderText="Teus"   HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>--%>

</Columns>

</asp:GridView>
</div>
</div>
</div>
                    </div>
                
                 <div role="tabpanel" class="tab-pane" id="Container" >
                      
                     <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" 
AutoGenerateColumns="true" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
  
</Columns>

</asp:GridView>
</div>
</div>
</div>
       
                     </div>
                </div>
 </div>

  </div>
    <br />
    <div class="row">
        <div class="col-md-2 col-xs-12 pull-right">
                                <div class="form-group pull-right" style="padding-right: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>
    </div>


</div>
</div>   
                                 
                               
</div>
</div>
                          
                     
                       
                       
</div>
                 

</div>
         
</div>
<%-- <script type="text/javascript">
function checkRadioBtn(id) {
var gv = document.getElementById('<%=grdcontainer.ClientID%>');

for (var i = 1; i < gv.rows.length; i++) {
var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

// Check if the id not same
if (radioBtn[0].id != id.id) {
radioBtn[0].checked = false;
}
}
}
</script>--%>
<%--  <script type="text/javascript">
 
function BondExPrint() {
            
var NOCNo1= document.getElementById('<%= txtNOCNo.ClientID%>').value;
             
var url = "../Report_Bond/BondEx_logo_print.aspx?NOCNo=" + NOCNo1;
//alert("hi")
                
window.open(url);

}


</script>--%>
</asp:Content>

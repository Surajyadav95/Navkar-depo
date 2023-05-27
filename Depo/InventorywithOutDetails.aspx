<%@ Page Title="Depo |Inventory with Out Details" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="InventorywithOutDetails.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Inventory with Out Details</title>
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
<i class="glyphicon glyphicon-transfer"></i>Inventory with Out Details
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
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  <div class="col-md-2 col-xs-12" style="padding-right:35px;" >
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria"  runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" AutoPostBack="true"   class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem> 
 
<asp:ListItem Value="1">Line Name</asp:ListItem>
<asp:ListItem Value="2">Size</asp:ListItem>
    <asp:ListItem Value="3">Type</asp:ListItem>
 
</asp:DropDownList>
                                               
</div>

</div>

   

     <div class="col-md-4 col-xs-12" id="divLine" runat="server" style="display:none">
<div class="form-group text-label">
<b  >Line Name</b>
<asp:DropDownList ID="ddlLineName" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>
</div>


     <div class="col-md-3 col-xs-12" id="divSize" runat="server" style="display:none">
<div class="form-group text-label">
<b  >Size</b>
<asp:DropDownList ID="ddlSize" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
 <asp:ListItem Value="0">--Select--</asp:ListItem> 
<asp:ListItem Value="1">20</asp:ListItem>
<asp:ListItem Value="2">40</asp:ListItem>
<asp:ListItem Value="3">45</asp:ListItem>
                              
                                         </asp:DropDownList>
</div>
</div>
     <div class="col-md-4 col-xs-12" id="divType" runat="server" style="display:none">
<div class="form-group text-label">
<b  >Type</b>
<asp:DropDownList ID="ddlType" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>
</div>
     </ContentTemplate>
</asp:UpdatePanel>
    <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
    </div>   
    
     <div class="col-md-2 col-xs-12 pull-left">
                                <div class="form-group pull-left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>                                      
                                      
</div>
                                                   
 
<div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" 
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging" AllowPaging="true" PageSize="10"  >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
 
                                                   
<asp:BoundField DataField="Sr No" HeaderText="Sr No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
   <asp:BoundField DataField="EntryID" HeaderText="Entry ID" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Container No" HeaderText="Container No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Size" HeaderText="Size" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="ISO Code" HeaderText="ISO Code" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

 <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
      <asp:BoundField DataField="In date & Time" HeaderText="In date & Time" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
      <%--<asp:BoundField DataField="Intime" HeaderText="Intime" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>--%>      
      <asp:BoundField DataField="Dwell Days" HeaderText="Dwell Days" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
      <asp:BoundField DataField="Shipping Line" HeaderText="Shipping Line" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      

        <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
     <asp:BoundField DataField="Trailer name" HeaderText="Trailer name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
     <asp:BoundField DataField="Transporter" HeaderText="Transporter" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>                                      
     <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>                                      
     <asp:BoundField DataField="Booking No" HeaderText="Booking No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>                                      
     <asp:BoundField DataField="Condition" HeaderText="Condition" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>                                      
     <asp:BoundField DataField="MFG Date" HeaderText="MFG Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>                                      
     <asp:BoundField DataField="Do Valid Date" HeaderText="Do Valid Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>                                      

     <asp:BoundField DataField="Gross Weight" HeaderText="Gross Weight" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Tare Weight" HeaderText="Tare weight" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Pay Load" HeaderText="Pay Load" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <%--<asp:BoundField DataField="Invoice No" HeaderText="Invoice No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="GST NO" HeaderText="GST NO" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Party Name" HeaderText="Party Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Net Total" HeaderText="Net Total" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="SGST" HeaderText="SGST" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="CGST" HeaderText="CGST" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="IGST" HeaderText="IGST" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Grand Total" HeaderText="Grand Total" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>--%>
    <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
    <br />
    


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

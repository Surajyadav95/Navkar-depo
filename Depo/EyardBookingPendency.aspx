<%@ Page Title="Depo |Booking Movement Tracking" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EyardBookingPendency.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Booking Movement Tracking</title>
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
<i class="glyphicon glyphicon-transfer"></i>Booking Movement Tracking
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
   
       <div class="col-sm-4 col-xs-8">
<div class="form-group text-label">
    <b >Search</b>
<asp:TextBox  ID="txtsearch"    placeholder="Search" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
    <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
    </div>   
    
     <div class="col-md-2 col-xs-12 pull-left">
                                <div class="form-group pull-left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"  
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>                                      
                                      
</div>
                                                   
 

 <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdsummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdsummary_PageIndexChanging"  AllowPaging="true" PageSize="10" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
  
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

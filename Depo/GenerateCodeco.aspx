<%@ Page Title="Depo | Generate Codeco" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="GenerateCodeco.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo |Generate Codeco</title>
</head>
    <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            width:1800px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Generate Codeco
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
      <div class="col-md-6 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
 
    
    
    <div class="col-md-3 col-xs-12"  >
<div class="form-group text-label">
<b  style="margin-left:2px">Shipping Line</b>
<asp:DropDownList ID="ddlShipping" Style="text-transform: uppercase;border-radius:4px; margin-left:2px"  runat="server" class="form-control " >
          <asp:ListItem Value="0">-Select-</asp:ListItem> 
<asp:ListItem Value="2">ECON SHIPPING</asp:ListItem>
<asp:ListItem Value="8">HYUNDAI MERCHANT MARINE LINE</asp:ListItem>
<asp:ListItem Value="17">KMTC LINE</asp:ListItem>  
    <asp:ListItem Value="9">REGIONAL CONTAINER LINES</asp:ListItem>                                           
                                         </asp:DropDownList>
</div>

</div>

        <div class="col-md-2 col-xs-12"  >
<div class="form-group text-label">
<b  >Process</b>
<asp:DropDownList ID="ddlProcess" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
<asp:ListItem Value="0">-Select-</asp:ListItem> 
<asp:ListItem Value="1">In</asp:ListItem>
<asp:ListItem Value="2">Out</asp:ListItem>
<asp:ListItem Value="3">Estimate</asp:ListItem>                                         
                                         </asp:DropDownList>
</div>

</div>
       <div class="col-md-1 col-xs-12 pull-right">
                                <div class="form-group pull-right" style="padding-top: 20px">
                                    <asp:Button ID="btnGenerate" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Generate" ></asp:Button>
                                </div>
                            </div>     
       </div>   
                                              
                                      
</div>
                                           
 


 
<div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdHoldDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover scrolling-table-container"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdHoldDets_PageIndexChanging"  AllowPaging="true" PageSize="10" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
 
                                                   
<asp:BoundField DataField="Sr No" HeaderText="Sr No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="GP No" HeaderText="GP No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="GP Date" HeaderText="GP Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Container No" HeaderText="Container No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Vehicle No" HeaderText="Vehicle No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

<asp:BoundField DataField="Transporter" HeaderText="Transporter" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
 <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
      <asp:BoundField DataField="Added By" HeaderText="Added By" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
     <%-- <asp:BoundField DataField="Hold Reason" HeaderText="Hold Reason" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
      <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
      <asp:BoundField DataField="Hold By" HeaderText="Hold By" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>   --%>   

        

</Columns>

</asp:GridView>



</div>
</div>


      <div class="row">
 <%--      <div class="col-md-1 col-xs-12 pull-right">
                                <div class="form-group pull-right" style="padding-top: 20px">
                                    <asp:Button ID="btnGenerate" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Generate Codeco" ></asp:Button>
                                </div>
                            </div>     
       </div>   --%>   
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

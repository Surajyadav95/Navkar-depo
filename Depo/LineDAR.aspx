<%@ Page Title="Depo | Line DAR" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="LineDAR.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Line DAR</title>
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
<i class="glyphicon glyphicon-transfer"></i>Line DAR
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
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
 
    
    <div class="col-md-4 col-xs-12"  runat="server" style="padding-left:30px;" >
<div class="form-group text-label">
<b  >Line Name</b>
<asp:DropDownList ID="ddlLineName" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>

</div>
    
    <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnShow" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnShow_Click" 
Text="Show"     />
</div>
    </div>                                         
    <div class="col-md-2 col-xs-12 pull-left">
               <div class="form-group pull-left" style="padding-top: 20px">
                                    <asp:Button ID="btnExport"  runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>      
    <div class="col-md-2 col-xs-12 pull-left">
               <div class="form-group pull-left" style="padding-top: 20px">
                                    <asp:Button ID="Button1"  runat="server"
                                        class="btn btn-info btn-sm outline" Text="ZIM Line DAR" ></asp:Button>
                                </div>
                            </div>                             
</div>
                                                   
 


 
<div class="row">
<div class="col-lg-8 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdRegistrationSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdRegistrationSummary_PageIndexChanging"  AllowPaging="true" PageSize="10" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
 
                                                   
<asp:BoundField DataField="Event Name" HeaderText="Event Name" HeaderStyle-CssClass="header-left" ItemStyle-HorizontalAlign="left"></asp:BoundField>
<asp:BoundField DataField="20's" HeaderText="20's" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="40's" HeaderText="40's" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
<asp:BoundField DataField="45's" HeaderText="45's" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>

<asp:BoundField DataField="TEUS" HeaderText="TEU's" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>

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

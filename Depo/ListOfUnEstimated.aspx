<%@ Page Title="Depo | List Of Un Estimated" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ListOfUnEstimated.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo |List Of Un Estimated</title>
</head>
    <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            max-width:1800px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>List Of Un Estimated
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
<div class="col-md-3  col-xs-12">
                                <div class="form-group date text-label">
                                   <b>As On</b>
                                     <asp:TextBox ID="txtdate" Style="text-transform:uppercase" TextMode="DateTimeLocal" class="form-control text-label"   
runat="server"   ></asp:TextBox>
                                </div>


                            </div>


<div class="col-md-4 col-xs-12"  runat="server" >
<div class="form-group text-label">
<b>Shipping Line</b>
<asp:DropDownList ID="ddlLineName" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control"></asp:DropDownList>
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
    <asp:Label runat="server" ID="lblserach" Visible="false" ></asp:Label>
    <div class="row">
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


 
<div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdRegistrationSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover scrolling-table-container"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdRegistrationSummary_PageIndexChanging"  AllowPaging="true" PageSize="10" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
 
                                                   
<asp:BoundField DataField="#" HeaderText="Sr No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Container No" HeaderText="Container No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Size" HeaderText="Size" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Container Type" HeaderText="Container Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

<asp:BoundField DataField="InDate" HeaderText="InDate" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
    <asp:BoundField DataField="SLName" HeaderText="Shipping Line" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
 
    <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
      <asp:BoundField DataField="Dwell Days" HeaderText="Dwell Days" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      

    <asp:BoundField DataField="Added By" HeaderText="Added By" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

</Columns>

</asp:GridView>
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

<%@ Page Title="Depo | Empty Container Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EmptyContainerSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo |Empty Container Summary</title>
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
<i class="glyphicon glyphicon-transfer"></i>Empty Container Summary
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
    <div class="col-sm-6 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
                                                   
<div class="col-md-3 col-xs-12" >
<div class="form-group text-label">
<b >Search</b>
<asp:TextBox ID="txtsearch1" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 
    
    <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
    </div>   
     <div class="col-md-2 col-xs-12 ">
                                <div class="form-group  " style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>                                      
                                      
</div>
                                                   
 


    <div class="row " >
<div class="form-group text-label">
<div class="col-md-5 col-xs-12  ">
<div class="col-sm-3 col-xs-3">
<b>20:</b>
<asp:Label runat="server" ID="lbl20" ></asp:Label>
&nbsp</div>
<div class="col-md-3 col-xs-3">
<b>40:</b>
<asp:Label runat="server" ID="lbl40" ></asp:Label>
&nbsp</div>
<div class="col-md-3 col-xs-3">
<b>45:</b>
<asp:Label runat="server" ID="lbl45" ></asp:Label>
&nbsp</div>
<div class="col-md-3 col-xs-3">
<b>Teus:</b>
<asp:Label runat="server" ID="lblteus" ></asp:Label>
&nbsp</div>
</div>
                                 
</div>
</div>
<div class="row">
    
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdRegistrationSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover  "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdRegistrationSummary_PageIndexChanging"  AllowPaging="true" PageSize="10" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
 <asp:TemplateField>
<ItemTemplate>
    <a  href='<%# "../Depo/Report_Epic/EyardInwoInvoice.aspx?GateInNo=" & (DataBinder.Eval(Container.DataItem, "Code")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Gate Pass</a>
    <a  href='<%# "../Depo/Report_Epic/EyardInPrint.aspx?GateInNo=" & (DataBinder.Eval(Container.DataItem, "Code")).ToString()%>'target="_blank" 
Class='btn btn-info btn-xs outline' 
>Receipt</a>
    </ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="100px" />
</asp:TemplateField>
                                                   
<asp:BoundField DataField="Code" HeaderText="Gate Pass No"  ></asp:BoundField>
    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No"  ></asp:BoundField>
<asp:BoundField DataField="Container No." HeaderText="Container No" ></asp:BoundField>
    <asp:BoundField DataField="Size" HeaderText="Size"  ></asp:BoundField>
<asp:BoundField DataField="In Date And Time" HeaderText="In Date & Time"  ></asp:BoundField>
    <asp:BoundField DataField="Line Name" HeaderText="Line Name"  ></asp:BoundField>

 
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

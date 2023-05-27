<%@ Page Title="Depo | Collection Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="CollectionSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Collection Summary</title>
</head>
    <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:500px;
            overflow:auto
        }
        .nav-tabs>li.active>a, .nav-tabs>li.active>a:focus, .nav-tabs>li.active>a:hover{
            background-color:orange
        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Collection Summary
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
<div class="col-md-7 col-xs-12">                                      
<div class="form-group date text-label">
Date                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>
</div>                                       
</div>   

    
    <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline" runat="server"
Text="Search"     />
</div>
    </div>                                         
                                      
</div>

<div class="row">
<div class="col-sm-12 col-xs-12 text-label ">
<div class="panel panel-default">
<div id="Tabs" role="tabpanel">
    <ul class="nav nav-tabs" role="tablist" style="background-color:#64A5FE">
<li class="active"><a href="#AccountWise" aria-controls="Account Wise Details" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Account Wise Collection Summary</a></li>

<li><a href="#LineWise" aria-controls="Line Wise Details" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Line Wise Collection Summary</a></li>

<li><a href="#ContainerWise" aria-controls="Container Wise Details" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Container Wise Details</a></li>
        </ul>
    <div class="tab-content" style="padding-top:20px">
        <div role="tabpanel" class="tab-pane active" id="AccountWise">
            <asp:UpdatePanel runat="server" ID="AccountUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>              
            <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdAccountWiseCollection" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<Columns>
 
                                                   
<%--<asp:BoundField DataField="Account Head" HeaderText="Account Head" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="left"></asp:BoundField>
<asp:BoundField DataField="T20" HeaderText="T20" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="T40" HeaderText="T40" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="T45" HeaderText="T45" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="Grand Total" HeaderText="Grand Total" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>--%>
</Columns>

</asp:GridView>
</div>
</div>
</div>
                     </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div role="tabpanel" class="tab-pane" id="LineWise">
                        <asp:UpdatePanel runat="server" ID="LineUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>
            <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdLineWiseCollection" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<Columns>
                                                   
<%--<asp:BoundField DataField="Line Name" HeaderText="Line Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="left"></asp:BoundField>
<asp:BoundField DataField="T20" HeaderText="T20" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="T40" HeaderText="T40" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="T45" HeaderText="T45" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>--%>

</Columns>

</asp:GridView>
</div>
</div>
</div>
                     </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div role="tabpanel" class="tab-pane" id="ContainerWise">
             <asp:UpdatePanel runat="server" ID="ContainerUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>
            <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container">
<asp:GridView ID="grdContainerWiseCollection" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="true" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<%--<Columns>
 
<asp:BoundField DataField="Sr No" HeaderText="Sr No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Container No" HeaderText="Container No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Size" HeaderText="Size" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="ISO Code" HeaderText="ISO Code" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

<asp:BoundField DataField="Tare Weight" HeaderText="Tare Weight" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
 <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
      <asp:BoundField DataField="In date & Time" HeaderText="In date & Time" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
      <asp:BoundField DataField="Intime" HeaderText="Intime" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
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
    <asp:BoundField DataField="Tare weight" HeaderText="Tare weight" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

</Columns>--%>

</asp:GridView>
</div>
</div>
</div>
                     </ContentTemplate>
            </asp:UpdatePanel>
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

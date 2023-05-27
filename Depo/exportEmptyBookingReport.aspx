<%@ Page Title="Depo |Empty Booking Report  Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="exportEmptyBookingReport.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo |Empty Booking Report  Summary</title>
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
<i class="glyphicon glyphicon-transfer"></i>Empty Booking Report  Summary
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


    <div class="col-md-5 col-xs-12">                                      
<div class="form-group date text-label"><b>Date</b>
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" style="width: 200px;"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 40px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" style="width: 200px;"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>
    </div>
</div>
        <div class="col-md-2 col-xs-12"  runat="server" style="padding-left:16px;" >
<div class="form-group text-label">
<b  >Search On:</b>
<asp:DropDownList ID="ddlSearchOn" Style="text-transform: uppercase;border-radius:4px" OnSelectedIndexChanged="ddlSearchOn_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control " >     
    <asp:ListItem Value="0">All</asp:ListItem> 
<%--    <asp:ListItem Value="1">Shipping Line</asp:ListItem>
    <asp:ListItem Value="2">Shipper Name</asp:ListItem>--%>
    <asp:ListItem Value="3">Booking No</asp:ListItem>
    <%--<asp:ListItem Value="4">Container No</asp:ListItem>--%>
<%--    <asp:ListItem Value="4">Transporter</asp:ListItem>--%>
</asp:DropDownList>
</div>

</div>

     <div class="col-md-3 col-xs-12" id="divShiplineName" style="display:none"  runat="server" >
<div class="form-group text-label">
<b  >Shipline Name</b>
<asp:DropDownList ID="ddlShipline"  runat="server" class="form-control " >     
</asp:DropDownList>
</div>

</div>

      <div class="col-md-3 col-xs-12" id="divshipper" style="display:none" runat="server" >
<div class="form-group text-label">
<b  >Shipper Name</b>
<asp:TextBox ID="txtshipperName" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control text-label" >                                              
</asp:TextBox>
</div>

</div>

      <div class="col-md-3 col-xs-12" id="divbooking" style="display:none" runat="server" >
<div class="form-group text-label">
<b  >Booking No</b>
<asp:TextBox ID="txtbookingno" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control text-label" >                                              
</asp:TextBox>
</div>

</div>


    
  
     <div class="col-md-3 col-xs-12" id="divtransporter" style="display:none"  runat="server" >
<div class="form-group text-label">
<b  >Transporter</b>
<asp:DropDownList ID="ddltransporter"  runat="server" class="form-control " >     
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
                                      
</div>
                                                   
 


 
<div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdRegistrationSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover scrolling-table-container"
AutoGenerateColumns="false" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdRegistrationSummary_PageIndexChanging"  AllowPaging="true" PageSize="10" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
  <asp:TemplateField HeaderText="iscancel" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:CheckBox ID="chkright" Text="" Checked='<%#Eval("iscancel")%>' runat="server" />
</ItemTemplate>
  <ItemStyle Width="20px" HorizontalAlign="Center" />                                    
</asp:TemplateField>
 
    <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Sr No" HeaderText="Sr No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:TemplateField HeaderText="Booking No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblBookingNo" Text='<%#Eval("Booking No")%>'></asp:Label>
            

        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />  
    </asp:TemplateField>   
    <asp:BoundField DataField="Line Name" HeaderText="Line Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Shipper Name" HeaderText="Shipper Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Do Date" HeaderText="Do Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

<asp:BoundField DataField="Do Validity" HeaderText="Do Validity" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
 <asp:BoundField DataField="POD" HeaderText="POD" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
      <asp:BoundField DataField="No. of 20" HeaderText="No. of 20" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
      <asp:BoundField DataField="No. of 40" HeaderText="No. of 40" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
      <asp:BoundField DataField="No. of 45" HeaderText="No. of 45" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
      <asp:BoundField DataField="Added By" HeaderText="Added By" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>    
         <asp:BoundField DataField="Added On" HeaderText="Added On" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>    
     <asp:BoundField DataField="Transporter" HeaderText="Transporter" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>    
</Columns>

</asp:GridView>
</div>
</div>
</div>
 
    <br />


    <div class="row">
         <div class="col-md-2 col-xs-12 ">
                                <div class="form-group " style="padding-top:0px">
                                    <asp:Button ID="btncancel" runat="server" 
                                        class="btn btn-primary btn-sm outline" Text="Cancel" OnClick="btncancel_Click"   ></asp:Button>
                                </div>
                            </div>
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
    <div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>

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
<%--<asp:Button ID="btntest" runat="server"
class="btn btn-info btn-block" Text="OK" data-dismiss="modal" aria-hidden="true" OnClientClick="populateCalendarTextbox()"></asp:Button>--%>
    <a href="exportEmptyBookingReport.aspx" class="btn btn-info btn-block ">Ok</a>
</div>
</div>

    </ContentTemplate>
</asp:UpdatePanel>
                      
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

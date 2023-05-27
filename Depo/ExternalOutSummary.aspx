<%@ Page Title="Depo |External Empty Out Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ExternalOutSummary.aspx.vb" culture="en-GB" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo |External Empty Out Summary</title>
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
<i class="glyphicon glyphicon-transfer"></i>External Empty Out Summary
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
    <div class="col-sm-5 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" style="width: 195px;"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 40px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" style="width: 195px;"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
                                                   
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  <div class="col-md-2 col-xs-12" style="padding-right:35px;" >
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria"  runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" AutoPostBack="true"   class="form-control text-label">
<asp:ListItem Value="All">All</asp:ListItem> 
 
<asp:ListItem Value="Line Name">Line Name</asp:ListItem>
<asp:ListItem Value="Container No">Container No</asp:ListItem>
     <asp:ListItem Value="Jo Type">Jo Type</asp:ListItem>
</asp:DropDownList>
                                               
</div>

</div>

   
    <div class="col-md-3 col-xs-12"  runat="server" id="divJotype" style="display:none">
<div class="form-group text-label">
<b>Jo Type</b>
<asp:DropDownList ID="ddljoype" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control"></asp:DropDownList>
</div>
</div>

<div class="col-md-4 col-xs-12" id="divLine" runat="server" style="display:none">
<div class="form-group text-label">
<b  >Line Name</b>
<asp:DropDownList ID="ddlLineName" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
</asp:DropDownList>
</div>
</div>

<div class="col-md-4 col-xs-12" id="divCustomer" runat="server" style="display:none">
<div class="form-group text-label">
<b  >Customer</b>
<asp:DropDownList ID="ddlCustomer" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
</asp:DropDownList>
</div>
</div>

   <div class="col-md-4 col-xs-12" id="divContainerNo" runat="server" style="display:none">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox runat="server" ID="txtContainerNo" MaxLength="11" class="form-control " Style="text-transform: uppercase"></asp:TextBox>
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
AutoGenerateColumns="True" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdRegistrationSummary_PageIndexChanging"  AllowPaging="true" PageSize="10" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
     <asp:TemplateField>
<ItemTemplate>
    <a  href='<%# "../Depo/Report_Epic/EyardOutPrintext.aspx?GpNo=" & (DataBinder.Eval(Container.DataItem, "GP No")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Print</a>
    </ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="50px" />
</asp:TemplateField>

<%--<asp:BoundField DataField="Sr No" HeaderText="Sr No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Container No" HeaderText="Container No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="Size" HeaderText="Size" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="ISO Code" HeaderText="ISO Code" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

<asp:BoundField DataField="Tare Weight" HeaderText="Tare Weight" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
    <asp:BoundField DataField="Entry Type" HeaderText="Mode Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
    <asp:BoundField DataField="Jo Type" HeaderText="Jo Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
 <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
      <asp:BoundField DataField="In date & Time" HeaderText="In date & Time" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
      <asp:BoundField DataField="Out Date" HeaderText="Out Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      

      <asp:BoundField DataField="OUT Time" HeaderText="Out time" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
      <asp:BoundField DataField="Dwell Days" HeaderText="Dwell Days" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
      <asp:BoundField DataField="Shipping Line" HeaderText="Shipping Line" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>      
   
     <asp:BoundField DataField="Trailer name" HeaderText="Trailer name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
     <asp:BoundField DataField="Transporter" HeaderText="Transporter" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>                                      
     <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>                                      
    <asp:BoundField DataField="Source" HeaderText="Source" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
     <asp:BoundField DataField="Booking No" HeaderText="Booking No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>                                      
     <asp:BoundField DataField="Seal No" HeaderText="Seal No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>                                      
     <asp:BoundField DataField="Shipper Name" HeaderText="Shipper Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>                                      
     <asp:BoundField DataField="POD" HeaderText="POD" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>                                      
     <asp:BoundField DataField="Vessel Name" HeaderText="Vessel Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>                                      
    <asp:BoundField DataField="ViaNo" HeaderText="Via No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>                                      
     <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>--%>
 
</Columns>

</asp:GridView>
</div>
</div>

</div>
     <div class="col-md-2 col-xs-12 " style="padding-top: 20px">
                                <div class="form-group  " >
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
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

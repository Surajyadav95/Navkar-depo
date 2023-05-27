<%@ Page Title="Depo | Estimate Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="OVMNR_EstimateSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Estimate Summary</title>
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
<i class="glyphicon glyphicon-transfer"></i>Estimate Summary
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
    <div class="col-md-2 col-xs-12"  runat="server" style="padding-left:16px;" >
<div class="form-group text-label">
<b  >Search On:</b>
<asp:DropDownList ID="ddlSearchOn" Style="text-transform: uppercase;border-radius:4px" OnSelectedIndexChanged="ddlSearchOn_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control " >     
<asp:ListItem Value="0">All</asp:ListItem> 
<asp:ListItem Value="2">Container No</asp:ListItem>
</asp:DropDownList>

</div>

</div>

     <div class="col-md-2 col-xs-12" id="divShiplineName" style="display:none"  runat="server" >
<div class="form-group text-label">
<b  >Shipline Name</b>
<asp:DropDownList ID="ddlShipline"  runat="server" class="form-control " >     
  
</asp:DropDownList>
</div>

</div>

    <div class="col-md-3 col-xs-12" id="divContainerNo" style="display:none" runat="server" >
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="TxtContainerNo" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control text-label" >                                              
</asp:TextBox>
</div>

</div>

    <div class="col-md-5 col-xs-12">                                      
<div class="form-group date text-label"><b>Date</b>
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" style="width: 200px;"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 40px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" style="width: 200px;"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>
    </div>
</div>
   <%-- <div class="col-md-2 col-xs-12"  runat="server"  >
<div class="form-group text-label">
<b  >Format Type:</b>
<asp:DropDownList ID="ddlFormat" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >     
<asp:ListItem Value="0">General</asp:ListItem> 
<asp:ListItem Value="1">PAN ASIA LINE</asp:ListItem>
<asp:ListItem Value="2">HYUNDAI MARCHANT MARINE LINE</asp:ListItem>
<asp:ListItem Value="3">ZIM INTEGRATED SHIPPING</asp:ListItem>
    <asp:ListItem Value="4">EVERGREEN SHIPPING AGENCY</asp:ListItem>
</asp:DropDownList>
</div>

</div>  --%>  

    
    <div class="col-md-3 col-xs-12"  >
<div class="form-group pull-right" style=" padding-top:20px">
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
AutoGenerateColumns="false" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
     <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:CheckBox ID="chkright" Text=""  runat="server"/>  <%--OnCheckedChanged="chkright_OnCheckedChanged"--%>
</ItemTemplate>
  <ItemStyle Width="20px" HorizontalAlign="Center" />                                    
</asp:TemplateField>
        <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblContainerNo" Text='<%#Eval("Container No")%>'></asp:Label>
            
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Shipping Line" HeaderStyle-CssClass="header-center" Visible="false">
       
        <ItemTemplate>'
            <asp:Label runat="server" ID="lblShippingLine" Text='<%#Eval("Shipping Line")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Shipping Line" HeaderStyle-CssClass="header-center" Visible="false">
       
        <ItemTemplate>'
            <asp:Label runat="server" ID="lblEntryID" Text='<%#Eval("EntryID")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    

<%--<asp:BoundField DataField="Shipping Line" HeaderText="Shipping Line" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>--%>
<%--<asp:BoundField DataField="Equipment No" HeaderText="Equipment No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>--%>
        <asp:BoundField DataField="Repair Estimate Ref no" HeaderText="Repair Estimate Ref no" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>

    <asp:BoundField DataField="ISOCode" HeaderText="ISOCode" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Depot Code" HeaderText="Depot Code" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Gate In Date" HeaderText="Gate In Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Estimate Date" HeaderText="Estimate Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
<%--<asp:BoundField DataField="Repair Sequence" HeaderText="Repair Sequence" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Damage Location" HeaderText="Damage Location" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Component" HeaderText="Component" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Repair Type" HeaderText="Repair Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Damage Type" HeaderText="Damage Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
<asp:BoundField DataField="Material Type" HeaderText="Material Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
<asp:BoundField DataField="Length" HeaderText="Length" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Width" HeaderText="Width" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Height" HeaderText="Height" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Unit" HeaderText="Unit" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
<asp:BoundField DataField="Hourly Rate" HeaderText="Hourly Rate" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
<asp:BoundField DataField="LabourHours" HeaderText="LabourHours" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Currency" HeaderText="Currency" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Labour Amount" HeaderText="Container No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Material Amount" HeaderText="Material Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>
    <asp:BoundField DataField="Responsible Type" HeaderText="Responsible Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>--%>
         <asp:BoundField DataField="Estimate Amount" Visible ="true"  HeaderText="Estimate Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>


      <asp:TemplateField HeaderText="IsWestim" HeaderStyle-CssClass="header-center" Visible="false">
       
        <ItemTemplate>'
            <asp:Label runat="server" ID="lblIsWestim" Text='<%#Eval("IsWestim")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>


    
    

</Columns>

</asp:GridView>
</div>
</div>
</div>
 
    <br />
    
      

    <div class="row">
          <div class="col-md-2 col-xs-12 pull-right">
                                <div class="form-group pull-right" style="padding-right: 350px">
                                    <asp:Button ID="btnWestim" runat="server"
                                        class="btn btn-info btn-sm outline" Text="WESTIM "></asp:Button>
                                </div>
                            </div>
        <div class="col-md-2 col-xs-12 pull-right">
                                <div class="form-group pull-right" style="padding-right: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>
    

      <%--<div class="row">--%>
         <div class="col-md-2 col-xs-12  pull-right"  style="display:none">
                                <div class="form-group pull-right" style="padding-right: 0px">
                                    <asp:Button ID="ExportToExcel" runat="server"
                                        class="btn btn-success btn-sm outline" Text="Export To Excel " ></asp:Button>
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

<%@ Page Title="Depo | Destim Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="DestimSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Destim Summary</title>
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
<i class="glyphicon glyphicon-transfer"></i>Destim Summary
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
       <asp:ListItem Value="3">Approve Status</asp:ListItem>
</asp:DropDownList>

</div>

</div>
        <div class="col-md-3 col-xs-6" id="divStatus" style="display:none" runat="server" >
<div class="form-group text-label">
<b  >Status</b>
<asp:DropDownList ID="ddlStatus" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
                                                 
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
    
       <div class="col-md-2 col-xs-12"  runat="server" style="padding-left:16px;" >
<div class="form-group text-label">
Based On
<asp:DropDownList ID="ddlBasedOn" runat="server" class="form-control text-label">
<asp:ListItem Value="1">Destim Date</asp:ListItem> 
<asp:ListItem Value="2">Estimate Date</asp:ListItem>

</asp:DropDownList>                                               
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

    
    <div class="col-md-2 col-xs-12"  >
<div class="form-group pull-left" style=" padding-top:20px">
<asp:Button ID="btnShow" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnShow_Click" 
Text="Show"     />
</div>
    </div>                                         
       
      <div class="col-md-2 col-xs-12">
                                <div class="form-group" style="padding-top:20px">
                                    <asp:Button ID="ExportToExcel" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel " ></asp:Button>
                                </div>
                            </div>                                
</div>
                                                   
 


 
<div class="row">
<div class="col-lg-12 col-xs-12 text-label">
<div class="table-responsive">

<asp:GridView ID="grdContainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" 
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
<Columns>  

   <%-- <asp:TemplateField HeaderText="Sr No." HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblSrNo" text='<%#Eval("SR No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>
    <asp:TemplateField HeaderText="Destim Entry ID" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblDestimEntryID" text='<%#Eval("Destim Entry ID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
  <%--  <asp:TemplateField HeaderText="Is Approve" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:CheckBox runat="server" ID="chkIsApprove" AutoPostBack="true" OnCheckedChanged="chkIsApprove_CheckedChanged" Checked='<%#Eval("Is Approve Amount")%>' />
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>
     <asp:TemplateField HeaderText="Destim Entry Date" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblDestimEntryDate" text='<%#Eval("Destim Entry Date")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Estimate NO" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEst_NO" text='<%#Eval("Est_NO")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Estimate Date" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEst_Date" text='<%#Eval("Est_Date")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblContainerNo" text='<%#Eval("ContainerNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Activity" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblActivity" text='<%#Eval("Activity")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>


     <asp:TemplateField HeaderText="Depot Code" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblDepotId" text='<%#Eval("DepotId")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>



    <asp:TemplateField HeaderText="Vendor Code" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblVendorID" text='<%#Eval("VendorID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>


  <%--  <asp:TemplateField HeaderText="QTY" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblQTY" text='<%#Eval("QTY")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>

     <asp:TemplateField HeaderText="Total Amount" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblTotalAmt" text='<%#Eval("Total_Amt")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Approve Status" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblBGMApproveStatus" text='<%#Eval("BGMApproveStatus")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

      <asp:TemplateField HeaderText="Repair Status" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblRepairStatus" text='<%#Eval("Repair Status")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

   <%-- <asp:TemplateField HeaderText="App Man Hours" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txtAppManHours" onchange="return TextChange(this)" CssClass="form-control" Width="50" Text='<%#Eval("App Man Hours")%>'></asp:TextBox>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>


<%--    <asp:TemplateField HeaderText="Man Cost" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblManCost" text='<%#Eval("Man Cost")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Right" />
    </asp:TemplateField>--%>


   <%-- <asp:TemplateField HeaderText="App Man Cost" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txtAppManCost" onchange="return TextChange(this)" CssClass="form-control" style="text-align:right" Width="50" Text='<%#Eval("App Man Cost")%>'></asp:TextBox>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>


  <%--  <asp:TemplateField HeaderText="Material Cost" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblMaterialCost" text='<%#Eval("Material Cost")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Right" />
    </asp:TemplateField>--%>
   <%-- <asp:TemplateField HeaderText="App Material Cost" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txtAppMaterialCost" onchange="return TextChange(this)" CssClass="form-control" style="text-align:right" Width="50" Text='<%#Eval("App Material Cost")%>'></asp:TextBox>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>
<%--<asp:BoundField DataField="Total" HeaderText="Total Estimation Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="text-center"></asp:BoundField>
    
    <asp:TemplateField HeaderText="Total Approved Amount" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
           <asp:TextBox runat="server" ID="txtAppAmount" CssClass="form-control" style="text-align:right" Width="80" Text='<%#Eval("Approve Amount")%>'></asp:TextBox>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Approved Descriptions" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
           <asp:TextBox runat="server" ID="txtAppDescription" Width="200" TextMode="MultiLine" Rows="2" CssClass="form-control" Text='<%#Eval("Approved Descriptions")%>'></asp:TextBox>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="left" />
    </asp:TemplateField>--%>
</Columns>
</asp:GridView>

</div>
</div>
</div>

 
    <br />
    
        <div class="col-md-2 col-xs-12 pull-left" style="display:none">
                                <div class="form-group pull-right" style="padding-left: 40px">
                                    <asp:Button ID="bntRepairDestim" runat="server"
                                        class="btn btn-info btn-sm outline" Text="Repair Destim " ></asp:Button>
                                </div>
                            </div>

    <div class="row" style="display:none">
        <div class="col-md-2 col-xs-12 pull-right">
                                <div class="form-group pull-right" style="padding-right: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>
    

      <%--<div class="row">--%>
       
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

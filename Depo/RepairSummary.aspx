<%@ Page Title="Depo | Repair Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="RepairSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Repair Summary</title>
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
<i class="glyphicon glyphicon-transfer"></i>Repair Summary
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
<asp:ListItem Value="1">IN Date</asp:ListItem> 
<asp:ListItem Value="2">Estimate Date</asp:ListItem>
    <asp:ListItem Value="3">Destim Date</asp:ListItem>

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
                                      
</div>
                                                   
 


 
<div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdContainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" 
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
<Columns>  
     <asp:TemplateField HeaderText=" " HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:CheckBox ID="chkright" Text=""  runat="server"  AutoPostBack="true" OnCheckedChanged="chkright_OnCheckedChanged"/>
</ItemTemplate>
  <ItemStyle Width="20px" HorizontalAlign="Center" />                                    
</asp:TemplateField>
   




     <asp:TemplateField HeaderText="Repair ID" HeaderStyle-CssClass="header-center" Visible="true">
       
        <ItemTemplate>
            <asp:Label runat="server" ID="lblRepairDestimID" Text='<%#Eval("RepairDestimID")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
      <asp:TemplateField HeaderText="Container NO" HeaderStyle-CssClass="header-center" Visible="true">
       
        <ItemTemplate>
            <asp:Label runat="server" ID="lblContainerNo" Text='<%#Eval("ContainerNo")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Repair Date" HeaderStyle-CssClass="header-center" Visible="true">
       
        <ItemTemplate>
            <asp:Label runat="server" ID="lblRepairDate" Text='<%#Eval("RepairDate")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Estimate NO" HeaderStyle-CssClass="header-center" Visible="true">
       
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEstimate_ID" Text='<%#Eval("Estimate_ID")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Estimate Date" HeaderStyle-CssClass="header-center" Visible="true">
       
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEst_Date" Text='<%#Eval("Est_Date")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="In Date" HeaderStyle-CssClass="header-center" Visible="true">
       
        <ItemTemplate>
            <asp:Label runat="server" ID="lblInDate" Text='<%#Eval("InDate")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

   
      <asp:TemplateField HeaderText="Approve Amount" HeaderStyle-CssClass="header-center" Visible="true">
       
        <ItemTemplate>
            <asp:Label runat="server" ID="lblApprove_Amt" Text='<%#Eval("Approve_Amt")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="header-center" Visible="true">
       
        <ItemTemplate>
            <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("Status")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
     <asp:TemplateField HeaderText="IsRepairDestim" HeaderStyle-CssClass="header-center" Visible="false">
       
        <ItemTemplate>
            <asp:Label runat="server" ID="lblIsRepairDestim" Text='<%#Eval("IsRepairDestim")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    
      <asp:TemplateField HeaderText="FileID" HeaderStyle-CssClass="header-center" Visible="false">
       
        <ItemTemplate>
            <asp:Label runat="server" ID="lblFileID" Text='<%#Eval("FileID")%>'></asp:Label>
      </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    
    
    

</Columns>

</asp:GridView>
</div>
</div>
</div>
 
    <br />
    
        <div class="col-md-2 col-xs-12 pull-left">
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
         <div class="col-md-2 col-xs-12 pull-right">
                                <div class="form-group pull-right" style="padding-left: 20px">
                                    <asp:Button ID="ExportToExcel" runat="server"
                                        class="btn btn-success btn-sm outline" Text="ExportToExcel " ></asp:Button>
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

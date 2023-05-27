<%@ Page Title="Depo |Estimate Approved Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EstimateApprovedSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo |Estimate Approved Summary</title>
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
<i class="glyphicon glyphicon-transfer"></i>Estimate Approved Summary
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
<asp:ListItem Value="1">Shipping Line</asp:ListItem>
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
    <div class="col-md-2 col-xs-12"  runat="server"  >
<div class="form-group text-label">
<b  >Format Type:</b>
<asp:DropDownList ID="ddlFormat" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >     
<asp:ListItem Value="0">General</asp:ListItem> 
<asp:ListItem Value="1">PAN ASIA LINE</asp:ListItem>
<asp:ListItem Value="2">HYUNDAI MARCHANT MARINE LINE</asp:ListItem>
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
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdRegistrationSummary_PageIndexChanging"  AllowPaging="true" PageSize="10" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
 <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:CheckBox ID="chkright" Text="" Checked='<%#Eval("Select")%>' runat="server" />
</ItemTemplate>
  <ItemStyle Width="20px" HorizontalAlign="Center" />                                    
</asp:TemplateField>

<asp:BoundField DataField="Sr No." HeaderText="Sr No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>

    <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblContainerNo" Text='<%#Eval("Container No")%>'></asp:Label>
            
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblSize" Text='<%#Eval("Size")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="center" />
    </asp:TemplateField>

<asp:TemplateField HeaderText="Container Type" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblContainerType" Text='<%#Eval("Container Type")%>'></asp:Label>

        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="In Date" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblInDate" Text='<%#Eval("InDate")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Shipping Line" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblShippingLine" Text='<%#Eval("Shipping Line")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Location" visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblLocation" Text='<%#Eval("Location")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

 <asp:BoundField DataField="Estimate Date" HeaderText="Estimate Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>      
      <asp:BoundField DataField="Estimate Amount" HeaderText="Estimate Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>      
      <asp:BoundField DataField="Added By" HeaderText="Added By" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>      
      
    <asp:TemplateField HeaderText="Survey By" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblSurveyBy" Text='<%#Eval("Survey By")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    
     <asp:TemplateField HeaderText="Entry ID" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEntryID" Text='<%#Eval("EntryID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

        <asp:TemplateField HeaderText="Survey Type" Visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblSurveyType" Text='<%#Eval("ServeyType")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Validity" Visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblValidity" Text='<%#Eval("ValidDate")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Consignee" Visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblConsignee" Text='<%#Eval("Consignee")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Gross Weight" Visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblGrossWeight" Text='<%#Eval("GrossWeight")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Tare Weight" Visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblTareWeight" Text='<%#Eval("TareWeight")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>

    <asp:TemplateField HeaderText="MFG Date" Visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblMFGDate" Text='<%#Eval("MFGDate")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>

    <asp:TemplateField HeaderText="Estimate ID" Visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEstimate_ID" Text='<%#Eval("Estimate_ID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>

    <asp:TemplateField HeaderText="Estimation No" Visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEstimationNo" Text='<%#Eval("Estimation No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
    <asp:TemplateField HeaderText="CSCASP" Visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblCSCASP" Text='<%#Eval("CSCASP")%>'></asp:Label>
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

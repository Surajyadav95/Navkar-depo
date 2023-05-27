<%@ Page Title="Depo |List Of Unappoved Containers" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ApproveContainerSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo |List Of Unappoved Containers</title>
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
<i class="glyphicon glyphicon-transfer"></i>List Of Unappoved Containers
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
<asp:ListItem Value="1">Shipping Line</asp:ListItem>
<asp:ListItem Value="2">Container No</asp:ListItem>
</asp:DropDownList>
</div>

</div>

     <div class="col-md-4 col-xs-12" id="divShiplineName" style="display:none"  runat="server" >
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
 

    
    <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnShow" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnShow_Click" 
Text="Show"     />
</div>
    </div>                                         
               
     <div class="col-md-2 col-xs-12 ">
                                <div class="form-group " style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
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
    
        <asp:TemplateField>
        <ItemTemplate>
        <asp:LinkButton ID="lnkApprove"  ControlStyle-CssClass='btn btn-primary btn-xs outline' Text="Approve"                                                         
        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Estimate_ID")%>' runat="server" OnClick="lnkApprove_Click" 
        ></asp:LinkButton>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" Width="100px" />

        </asp:TemplateField>
 <asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:CheckBox ID="chkright" Text="" Checked='<%#Eval("Select")%>' runat="server" />
</ItemTemplate>
  <ItemStyle Width="20px" HorizontalAlign="Center" />                                    
</asp:TemplateField>

<asp:BoundField DataField="#" HeaderText="Sr No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>

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
 

 <asp:BoundField DataField="Estimate Date" HeaderText="Estimate Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField> 
      <asp:TemplateField HeaderText="Estimate Amount" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEstimateAmount" Text='<%#Eval("Estimate Amount")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>     
      <%--<asp:BoundField DataField="Estimate Amount" HeaderText="Estimate Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>--%>      
      <asp:BoundField DataField="In Dwell Days" HeaderText="In Dwell Days" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="center"></asp:BoundField>      
      
    <asp:TemplateField HeaderText="Estimate Dwell Days" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEstimate" Text='<%#Eval("Estimate Dwell Days")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
    
     <asp:TemplateField HeaderText="Entry ID" Visible="false"  HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEntryID" Text='<%#Eval("EntryID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
     
  

    <asp:TemplateField HeaderText="Estimate ID" Visible="false" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblEstimate_ID" Text='<%#Eval("Estimate_ID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>


   
</Columns>

</asp:GridView>
</div>
</div>
</div>
 
    <br />
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
    <div class="row pull-right" style="padding-left:-0px">
        <div class="col-md-7 col-xs-12 ">
        <div class="form-group text-label">
        <b  >Approve Date</b>
        <asp:TextBox ID="txtApprovedate"   placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
        </div>
        </div>

            <div class="col-md-2 col-xs-12 ">
                                <div class="form-group " style="padding-top:20px">
                                    <asp:Button ID="btnMultiApprove" runat="server" 
                                        class="btn btn-info btn-sm outline" Text="Multiple Approve" OnClick="btnMultiApprove_Click"  ></asp:Button>
                                </div>
                            </div>
    </div>

</div>
</div>   
    <div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel3" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
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
                   
<a href="ApproveContainerSummary.aspx" class="btn btn-info btn-block">OK</a>
                                
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

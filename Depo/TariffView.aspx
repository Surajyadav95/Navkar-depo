<%@ Page Title="Depo | Tariff View " Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="TariffView.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Tariff View  </title>
</head>

<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i> Tariff View Details  
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
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b>Tariff ID</b>
<asp:DropDownList ID="ddltraiff"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
            <asp:ListItem Value="0">--Select--</asp:ListItem>  
</asp:DropDownList>
</div>
</div>

  
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b>Invoice Type</b>
<asp:DropDownList ID="ddlbondType"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
        <asp:ListItem Value="0">--Select--</asp:ListItem>
</asp:DropDownList>
</div>
</div>

    <div class="col-md-4 col-xs-12" >
<div class="form-group text-label">
<b >Search</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 

      <div class="col-md-4 col-xs-12" style="display:none" >
<div class="form-group text-label">
<b >Slab ID</b>
<asp:TextBox ID="txtSlabID" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div> 
<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline " runat="server"
OnClick="btnSearch_Click" 
Text="Show" />
</div>                                                                                   
</div>
                                 
</div>
 

                     
<div class="row">
 

<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:5px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
                                                
<asp:BoundField DataField="Tariff ID" HeaderText="Tariff ID"></asp:BoundField>
<asp:BoundField DataField="Account Name" HeaderText="Account Name"></asp:BoundField>
    <asp:BoundField DataField="Delivery Type" HeaderText="Invoice Type"></asp:BoundField>
    <asp:BoundField DataField="Size" HeaderText="Size"></asp:BoundField>
    <asp:BoundField DataField="containertype" HeaderText="Type"></asp:BoundField>

 <asp:TemplateField HeaderText="Slab ID">
<ItemTemplate>
    <asp:LinkButton ID="lnkselect" OnClick="lnkselect_Click"  Text='<%#Eval("Slab ID")%>'   CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Slab ID")%>' runat="server"></asp:LinkButton>
 
</ItemTemplate>
</asp:TemplateField> 
     
<%--<asp:BoundField DataField="Slab ID" HeaderText="Slab ID"></asp:BoundField>--%>
<asp:BoundField DataField="IsSTax" HeaderText="Is STax"></asp:BoundField>
<asp:BoundField DataField="Fixed Amount" HeaderText="Fixed Amount"></asp:BoundField>
<asp:BoundField DataField="Effective From" HeaderText="Effective From"></asp:BoundField>
<asp:BoundField DataField="Effective Upto" HeaderText="Effective Upto"></asp:BoundField>
<asp:BoundField DataField="From" HeaderText="From"></asp:BoundField>
<asp:BoundField DataField="To" HeaderText="To"></asp:BoundField>


    
</Columns>

</asp:GridView>
</div>
</div>
</div>
 
    
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
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
>
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
<asp:Button ID="btntest" runat="server"
class="btn btn-info btn-block" Text="OK" data-dismiss="modal" aria-hidden="true" OnClientClick="populateCalendarTextbox()"></asp:Button>
</div>
</div>

                      
</div>
</div>
</div>

       <script type="text/javascript">
           var popup;
           function OpenSlabID() {

               var txtslab = document.getElementById('<%= txtSlabID.ClientID%>').value;

               var url = "../Depo/SlabList.aspx?SlabID=" + txtslab
               popup = window.open(url, "Popup", "width=800,height=550");
               popup.focus();
             //window.open(url);

         }

</script>
      <%--<script type="text/javascript">
        var popup;
      
        function OpenSlabID() {
         
            var url = "SlabList.aspx"
            //window.open(url);
            popup = window.open(url, "Popup", "width=800,height=550");
            popup.focus();
        }
</script>--%>
</asp:Content>

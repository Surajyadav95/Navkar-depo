<%@ Page Title="Depo |Re-Applicable codeco" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ReApplicablecodeco.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo |Re-Applicable codeco</title>
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
<i class="glyphicon glyphicon-transfer"></i>Re-Applicable codeco
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
<b  >Process:</b>
<asp:DropDownList ID="ddlProcess" Style="text-transform: uppercase;border-radius:4px"   runat="server" class="form-control " >     
<asp:ListItem Value="0">--SELECT--</asp:ListItem> 
<asp:ListItem Value="1">In</asp:ListItem>
<asp:ListItem Value="2">Out</asp:ListItem>
</asp:DropDownList>
</div>

</div>

 
    <div class="col-md-3 col-xs-12"  runat="server" >
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
    
    <div class="col-md-2 col-xs-12  ">
                                <div class="form-group " style="padding-top: 20px">
                                    <asp:Button ID="btnReApplication" runat="server" OnClick="btnReApplication_Click" 
                                        class="btn btn-info btn-sm outline" Text="RE-Applicable" ></asp:Button>
                                </div>
                            </div>                                     
                                      
</div>
                                                   
 


 
<div class="row">
<div class="col-lg-8 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdReApplication" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover  "
AutoGenerateColumns="false" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdRegistrationSummary_PageIndexChanging"  AllowPaging="true" PageSize="10" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
   <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSrNo" runat="server" text='<%#Eval("Sr No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Entry ID" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblEntryID" runat="server" text='<%#Eval("EntryID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSize" runat="server" text='<%#Eval("Size")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblContainerType" runat="server" text='<%#Eval("Type")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Line Name" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblLineName" runat="server" text='<%#Eval("Line Name")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblInDate" runat="server" text='<%#Eval("Date")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
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
                          
                     
     <div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
         
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
                   
<a href="ReApplicablecodeco.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
                
</div>
</div>
     
    <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
 
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblquoteApprove"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btncancelyes" data-dismiss="modal" runat="server"  onserverclick="btncancelyes_ServerClick"  aria-hidden="true">
Yes 
</button>
<a href="ReApplicablecodeco.aspx" class="btn btn-danger ">No</a>
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

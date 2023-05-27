<%@ Page Title="Depo | PD Account Summary" Language="VB" MasterPageFile="../Depo/User.master" AutoEventWireup="false" CodeFile="PDAdjustmentSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Depo | PD Adjustment Summary .</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>PD Adjustment Summary  
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="panel">
<div class="panel-body" >
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
       <div class="col-md-4 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate"  style="width: 150px;" placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label" style="width: 40px;">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="Date" Class="  form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>          

                                    
                                                
<div class="row">
<div class="col-md-4 col-xs-12" >
<div class="form-group text-label">
<b >Search</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search by Assess No or PD Code"
runat="server"   ></asp:TextBox>
</div>
</div> 

<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
                                              
                                      
</div>
                                               
</div>


                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="grdcontainer" />
    </Triggers>
<ContentTemplate>

<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>
    <a  href='<%# "../Depo/Report_Epic/PDAdjustmentPrint.aspx?AssessNo=" & (DataBinder.Eval(Container.DataItem, "AssessNO")).ToString() & "&WorkYear=" & (DataBinder.Eval(Container.DataItem, "AssessYear")).ToString() & "&TransID=" & (DataBinder.Eval(Container.DataItem, "TransNo")).ToString()%>' target="_blank" 
class='btn btn-primary btn-xs outline' 
>Print</a>
<asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Cancel" OnClick="lnkCancel_Click"
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TransNo")%>' runat="server" ></asp:LinkButton>                                                         
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="130px" />
</asp:TemplateField>

<asp:BoundField DataField="TransNo" HeaderText="Trans No" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="TransDate1" HeaderText="Trans Date" HeaderStyle-CssClass="text-center"></asp:BoundField>   
<asp:BoundField DataField="AssessNO" HeaderText="Assess No" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="AssessYear" HeaderText="Assess Year" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="PDCode" HeaderText="PD Code" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="PDAmount" HeaderText="Adjustment Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="TDS" HeaderText="TDS Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-CssClass="text-center"></asp:BoundField>

</Columns>
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

<%@ Page Title="Proforma Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ListofProformaDetails.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Proforma Details </title>
</head>
<style>
.text-center{
text-align:center
}
</style>
<div class="page-container">
<div class="pageheader">
<h3> List of Proforma Details  
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
 <div class="col-sm-7 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
 
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem> 
<asp:ListItem Value="1">Invoice No</asp:ListItem>
<asp:ListItem Value="2">Container No</asp:ListItem> 
<asp:ListItem Value="3">Shipping Line</asp:ListItem>

</asp:DropDownList>
                                               
</div>

</div>
<div class="col-md-2 col-xs-12" style="display:none;"  id="divInvoiceno" runat="server">
<div class="form-group text-label">
<b >Invoice No</b>
<asp:TextBox ID="txtAssessno" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search Assess No"
runat="server"   ></asp:TextBox>
</div>
</div>

    <div class="col-md-3 col-xs-12" style="display:none;"  id="divWorkYear" runat="server">
<div class="form-group text-label">
<b >Work Year</b>
<asp:TextBox ID="TxtWorkYear" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search Work Year"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-4 col-xs-12" style="display:none;"  id="divShipping" runat="server">
<div class="form-group text-label">
  <b>Shipping Line</b>          
<asp:DropDownList ID="ddlShipping"  runat="server"   class="form-control text-label">
</asp:DropDownList>
</div>
</div>
                                                 
<div class="col-md-2 col-xs-12" style="display:none;"  id="divContainerNo" runat="server">
<div class="form-group text-label">
<b >Container No</b>
<asp:TextBox ID="txtContainerNo" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Search Container No"  
runat="server"   ></asp:TextBox>
</div>
</div>                         
<asp:Label ID="lblFD_ID" Visible="false" runat="server" Text=""></asp:Label>

<div class="col-md-1  col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnsearch" runat="server" 
class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click" ></asp:Button>
</div>              
</div>
         <div class="col-md-1  col-xs- 12" style="display:none">
             <asp:TextBox runat="server" ID="strfilename"></asp:TextBox>
         </div>                                 

</div>
                                 

                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:30px;">
<div class="table-responsive scrolling-table-container" style="margin-left:8px;margin-right:0px;">
<asp:GridView ID="grdSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" DataKeyNames="WorkYear" OnPageIndexChanging="OnPageIndexChanging"  AllowPaging="true" PageSize="8" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
<asp:TemplateField>
<ItemTemplate>

<a  href='<%# "../Depo/Report_Epic/HandlingStorageProformaPrint.aspx?InvoiceNo=" & (DataBinder.Eval(Container.DataItem, "InvoiceNo")).ToString() & "&WorkYear=" & (DataBinder.Eval(Container.DataItem, "WorkYear")).ToString() & "&LineID=" & (DataBinder.Eval(Container.DataItem, "lineID")).ToString() & "&InvoiceType=" & (DataBinder.Eval(Container.DataItem, "InvoiceType")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Print</a>

      <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger btn-xs outline'  Text="Cancel" OnClick="lnkCancel_Click" 
                                                            
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "InvoiceNo")%>'   runat="server" 
                                                            ></asp:LinkButton>

    <asp:LinkButton ID="lnkAnnexure" ControlStyle-CssClass='btn btn-info btn-xs outline' Text="Annexure" OnClick="lnkAnnexure_Click" Visible="false"
                                                            
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "InvoiceNo")%>' runat="server" 
                                                            ></asp:LinkButton>
     <asp:LinkButton ID="lnkIsubmit" ControlStyle-CssClass='btn btn-info btn-xs outline' Text="Submit" OnClick="lnkIsubmit_Click"     
                                                            
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "InvoiceNo")%>' runat="server" Class='<%#Eval("Editcss")%>'   
                                                            ></asp:LinkButton>
    
      
</ItemTemplate>
<ItemStyle HorizontalAlign="Left"  />
</asp:TemplateField>
    <asp:BoundField  DataField="AssessNo" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" HeaderText="Assess No"></asp:BoundField>
<asp:TemplateField HeaderText="Invoice No" HeaderStyle-CssClass="text-center">
<ItemTemplate>

    <asp:Label runat="server" ID="lblassessNo" Text='<%#Eval("AssessNo")%>'></asp:Label>
<asp:Label runat="server" ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo")%>'></asp:Label>
    <asp:Label runat="server" ID="lblLineID" Visible="false" Text='<%#Eval("lineID")%>'></asp:Label>
    <asp:Label runat="server" ID="lblInvoiceType" Visible="false" Text='<%#Eval("InvoiceType")%>'></asp:Label>
        <asp:Label runat="server" ID="lblDeliveryType" Visible="false" Text='<%#Eval("DeliveryType")%>'></asp:Label>
</ItemTemplate>

<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Work Year" HeaderStyle-CssClass="text-center">
<ItemTemplate>
<asp:Label runat="server" ID="lblworkyear" Text='<%#Eval("WorkYear")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
<asp:BoundField  DataField="AssessDate" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" HeaderText="Assess Date"></asp:BoundField>
<%--<asp:BoundField DataField="NOCNo" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"  HeaderText="NOC No"  ></asp:BoundField>--%>
<asp:BoundField DataField="GSTName" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="left" HeaderText="Party Name"  ></asp:BoundField>
<asp:BoundField DataField="SLName" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"  HeaderText="Line Name"  ></asp:BoundField>
    <asp:BoundField DataField="Invoice Type" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"  HeaderText="Invoice Type"  ></asp:BoundField>
<asp:BoundField DataField="GrandTotal" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderText="Grand Total"  ></asp:BoundField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
    <%--<asp:Panel ID="pnlPerson" Visible="false" runat="server" font-family="Segoe UI">
       <rsweb:ReportViewer ID="ReportViewer1" Width="1000px" Height="600px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Report_Bond\BondAssessmentPrint.rdlc" >
        </LocalReport>
        </rsweb:ReportViewer>  
        </asp:Panel>--%>                  
</div>
</div>
</div>
</div>
</div>
</div>                              
</div>
        <div class="modal fade control-label" id="myModalforupdate2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="Button1" />
    </Triggers>
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblsession"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info btn-block" id="Button1" data-dismiss="modal" runat="server" onserverclick="btnsearch_Click"  aria-hidden="true">
Ok 
</button>

</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>  

     <script>
         function OpenCancelInvoice() {
             var txtAssessno = document.getElementById('<%= txtAssessno.ClientID%>').value;
             var TxtWorkYear = document.getElementById('<%= TxtWorkYear.ClientID%>').value;

             //var url = "InvoiceCancel.aspx?AssessNo=" + txtAssessno + "&WorkYear=" + TxtWorkYear
            popup = window.open(url, "Popup", "top=100,left=400,width=700,height=215");
            popup.focus();
        }
    </script> 
</asp:Content>

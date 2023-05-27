<%@ Page Title="Depo | Update Invoice Amount" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="InvoiceAmountUpdate.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo | Update Invoice Amount </title>
       
</head>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i> Update Invoice Amount
</h3>           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="col-md-3 pull-md-left sidebar" style="padding-top:12px;">
 
                      <asp:Label runat="server"  Visible="false" Id="lblstatecode"></asp:Label>     
</div>
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-8 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Update Invoice Amount
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
  
<div class="row">
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Work Year</b>
<asp:TextBox ID="txtworkyear" Style="text-transform:uppercase"   class="form-control text-label"  placeholder="Work Year"
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Account Head</b>
<asp:DropDownList ID="ddlAccount"   Style="text-transform: uppercase;" runat="server"    class="form-control text-label">

</asp:DropDownList> 
</div>
</div>
    <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Invoice No</b>
<asp:TextBox ID="txtInvoiceno"  Style="text-transform:uppercase" class="form-control text-label"  placeholder="Invoice No"
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtcontainerno" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>

    <div class="col-sm-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnshow" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnshow_Click" 
Text="Show"  />
</div>
                                              
                                      
</div>
</div>

<div class="row">

</div>

 
<div class="row">
        <div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Invoice Date</b>
<asp:TextBox ID="txtinvdate" TextMode="Date" class="form-control text-label form-cascade-control"
runat="server" Placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
    <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Size</b>
<asp:TextBox ID="txtSize"  Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"    placeholder="Size"
runat="server"   ></asp:TextBox>
</div>
</div>
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Amount</b>
<asp:TextBox ID="txtAmount"  Style="text-transform:uppercase" class="form-control text-label"    placeholder="Amount"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>
     
             <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnSave_Click" 
Text="Save" OnClientClick="return ValidationSave()"   />
</div>
                                              
                                      
</div>
                           
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="InvoiceAmountUpdate.aspx" id="btnclear" runat="server" class="btn btn-primary  btn btn-sm outline ">
Clear
</a> 
                              
</div>                                            
                                      
</div>
 
                         
</div> 
    <div class="row">
        <div class="col-lg-8 text-label" style="padding-right:0px">
        <div id="divtblWOTOtal" runat="server" style="display:none;">                                         
<table forecolor="Black" class="table table-striped table-bordered table-hover" style="border-top:5px solid #7bc144;margin-left:-5px;margin-right:-5px">
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b ">Net Total</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblPercentage" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblTotal" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Discount</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="Label1" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lbldisc" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >CGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblCgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblCGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >SGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblSgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblSGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >IGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblIgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblIGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Grand Total</b></td>
<%--<td style ="width:20%;text-align:right"></td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblAllTotal" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
</table>
</div>
    </div>
        </div>               
</asp:Panel>
                         
</div>
</div>



<asp:Label ID="lblentryid" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lbltaxid" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         
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
                   
<a href="InvoiceAmountUpdate.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
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
<a href="InvoiceAmountUpdate.aspx" class="btn btn-danger ">No</a>
</div>
</div>
 
</div>
</div>         
</fieldset>

</div>
</div>
                               
</div>
                           
                          
                     
                       
                       
</div>
                 
              
</div>
       
         
</div>
   
 
</asp:Content>

<%@ Page Title="Depo | Other Invoice" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EyardOtherAssessment.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo | Other Invoice - Proforma</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>  Other Invoice - Proforma
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-left main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">

            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Invoice No</b>
<asp:TextBox ID="txtinvno" ReadOnly="true"  Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Invoice No."></asp:TextBox>  
       
</div>
</div>

    <div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Invoice Date</b>
<asp:TextBox ID="txtinvdate" TextMode="Date" class="form-control text-label form-cascade-control"
runat="server" Placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
    <div class="col-sm-3 col-xs-12" style="display:none" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Valid Upto Date</b>
<asp:TextBox ID="txtvaliddate" TextMode="Date" class="form-control text-label form-cascade-control"
runat="server" Placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
     <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
        <ContentTemplate>     

                <div class="col-sm-1 col-xs-6" style="display:none">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="LinkButton2" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return BondEx();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
                <div class="col-sm-4 col-xs-12" style="display:none">
<asp:TextBox runat="server" ID="txtassessno" ></asp:TextBox>
<asp:TextBox runat="server" ID="txtworkyear" ></asp:TextBox>
<asp:Label runat="server" ID="lblTariffId"></asp:Label>
        <asp:Label runat="server" ID="lblIsTax"></asp:Label>
        <asp:Label runat="server" ID="lblTaxAmount"></asp:Label>
</div>
            </ContentTemplate></asp:UpdatePanel>

</div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">       
        <ContentTemplate>

       <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
       <asp:Button ID="btnIndentlist" runat="server" Text="Call Button Click1" style="display:none" OnClick="btnIndentlist_Click" />

    <div class="row">
<div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b >GST In Number</b>
<asp:TextBox  ID="txtgstin" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" MaxLength="16" placeholder="Enter GST No" class="form-control text-label">
                                      
</asp:TextBox>
<asp:Label runat="server" ID="lblpartyid" Visible="false"></asp:Label>
</div>
</div>

<div class="col-sm-1 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return gstsearch();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
<div class="col-sm-7 col-xs-12" style="padding-top:20px">
<div class="form-group text-label">
<asp:TextBox  ID="txtgstname" placeholder="GST Name" ReadOnly="true" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
<asp:Label runat="server"  Visible="false" Id="lblstatecode"></asp:Label>
</div>
            <asp:Panel runat="server" Enabled="true">
    <div class="row">
        <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Line Name</b>
<asp:DropDownList ID="ddlcustomer"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                    
</asp:DropDownList> 
</div>
</div>
        <div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >From</b>
<asp:TextBox ID="txtStorageFrom" TextMode="Date" class="form-control text-label form-cascade-control"
runat="server" Placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
           
                <div class="col-sm-3 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Upto</b>
<asp:TextBox ID="txtStorageUpto" TextMode="Date" class="form-control text-label form-cascade-control"
runat="server" Placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
        <div class="col-md-6 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >CHA Name</b>
<asp:DropDownList ID="ddlCHA"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                     
</asp:DropDownList> 
</div>
</div>
    </div>
                </asp:Panel>
            
            </ContentTemplate>
    </asp:UpdatePanel> 
 <div class="row">
     <asp:Panel runat="server" Enabled="true">
     <div class="col-md-6 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Importer Name</b>
<asp:DropDownList ID="ddlimporter"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                     
</asp:DropDownList> 
</div>
</div>
         </asp:Panel>
     <div class="col-sm-3 col-xs-12" style="display:none">
<div class="form-group text-label">
    <b  >BE No</b>
<asp:TextBox  ID="txtbeno" Style="text-transform: uppercase;" runat="server" placeholder="BE No" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
     <div class="col-sm-3 col-xs-12" style="display:none">
<div class="form-group text-label">
    <b  >Bond No</b>
<asp:TextBox  ID="txtbondno" Style="text-transform: uppercase;" runat="server" placeholder="Bond No" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
     
 </div>
     
            <div class="row">
                <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
        <ContentTemplate>
              </ContentTemplate></asp:UpdatePanel> 
                
                <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
        <ContentTemplate>
            </ContentTemplate></asp:UpdatePanel>
                <div class="col-sm-3 col-xs-12" style="display:none" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Insurance From</b>
<asp:TextBox ID="txtInsFrom" TextMode="Date" class="form-control text-label form-cascade-control"
runat="server" Placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
              
                <div class="col-sm-3 col-xs-12" style="display:none" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Insurance Upto</b>
<asp:TextBox ID="txtInsUpto" TextMode="Date" class="form-control text-label form-cascade-control"
runat="server" Placeholder="dd-MM-yyyy"></asp:TextBox>     
</div>
</div>
            </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
       <Triggers>
           <asp:PostBackTrigger ControlID="lnkadd" />
           

       </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Invoice Type</b>
<asp:DropDownList ID="ddlInvoiceType"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
               <%--<asp:ListItem Value="0">--Select--</asp:ListItem>
               <asp:ListItem Value="Other">Other</asp:ListItem>      
               <asp:ListItem Value="Transport">Transport</asp:ListItem>--%>      
          
</asp:DropDownList> 
</div>
</div>

<div class="col-md-3 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Location</b>
<asp:DropDownList ID="ddlLocation"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                     
</asp:DropDownList> 
</div>
</div>

<div class="col-md-3 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Warehouse</b>
<asp:DropDownList ID="ddlwarehouse"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                     
</asp:DropDownList> 
</div>
</div>
                <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Commodity</b>
 
    <asp:DropDownList ID="ddlCommodity"  Style="text-transform: uppercase;" OnTextChanged="ddlCommodity_TextChanged" AutoPostBack="true" runat="server"  class="form-control text-label">
                    
</asp:DropDownList> 
</div>
</div>
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Tax</b>
<asp:DropDownList ID="ddlTax"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                     
</asp:DropDownList> 
</div>
</div>

                          <div class="col-sm-3 col-xs-8" >
<div class="form-group text-label">
<b>Reference Type</b>
<asp:DropDownList  ID="ddlreference"  Style="text-transform: uppercase;" runat="server" class="form-control text-label" >
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="PO">PO</asp:ListItem>
<asp:ListItem Value="Reference No">Reference No</asp:ListItem>
<asp:ListItem Value="Statement No">Statement No</asp:ListItem>                                      
</asp:DropDownList>
</div>
</div>

           <div class="col-sm-3 col-xs-8"   >
<div class="form-group text-label">
    <b>Reference/Statement No</b>
<asp:TextBox  ID="txtReference"   placeholder="Reference NO" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>

            </div>
        <div class="row">
    <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Account Heads</b>
<asp:DropDownList ID="ddlaccntheads" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
</asp:DropDownList> 
</div>
</div>

                <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Container No</b>
<asp:TextBox ID="txtcontainerno" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control" MaxLength="11"
runat="server" Text="" placeholder="Container No"></asp:TextBox>        
          <asp:Label runat="server" ID="lblTaxID" Visible="false"></asp:Label> 
</div>
</div>

              <div class="col-sm-1 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Size</b>
<asp:TextBox ID="txtSize" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control" MaxLength="11"
runat="server" Text="" placeholder="Size"></asp:TextBox>         
</div>
</div>
           
  <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
    <b>Amount</b>
<asp:TextBox  ID="txtamount"  Style="text-transform: uppercase;" runat="server" placeholder="Amount" class="form-control text-label">
</asp:TextBox>
</div>
</div>
    <div class="col-sm-1 col-xs-6">                            
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnkadd" ControlStyle-CssClass='btn btn-info'  runat="server"
OnClientClick="return ValidationAdd();">  
<i class=" fa fa-check"  aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
           
         
</div>    
             </ContentTemplate>
    </asp:UpdatePanel>   
        <div class="col-sm-2 col-xs-12 text-label" style="padding-top:25px">
     <label runat="server"  style="width:0px; margin-left:5px;" >
            <a style="display:block">
 <asp:FileUpload ID="FileUpload1" AllowMultiple="false"  runat="server" ClientIDMode="Static" /></a>
                                       </label>
       </div>
        <div class="col-sm-1 col-xs-12" style="padding-top:21px">
                                <asp:Button ID="btnUpload" class="btn btn-success btn-sm outline"  Text="Import" runat="server" OnClientClick="return ClassChange()"   OnClick="btnUpload_Click"  />  
     <asp:postbacktrigger controlid="btnUpload" xmlns:asp="#unknown"  />
        <b><asp:Label runat="server" ID="lblfilename" Text=""></asp:Label></b>
        </div>
         <div class="col-md-2 col-xs-12" >
<div class="form-group" style="padding-top:22px" >
<asp:LinkButton runat="server" ID="lnkDownloadExcel" OnClick="lnkDownloadExcel_Click"  CssClass="btn btn-info btn-sm" ToolTip="Download Template"><i class="fa fa-download"></i></asp:LinkButton>
</div>
    </div> 
       <div class="row">
         <div class="col-lg-10 text-label">
             <div class="table-responsive scrolling-table-container" style="margin-left:-5px;margin-right:-5px;">
                 <asp:GridView ID="grdcharges" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
                     <Columns>
                         <asp:TemplateField>
    <ItemTemplate>
            <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger' OnClick="lnkCancel_Click"                                                           
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AutoID")%>' runat="server" 
                                                            ><i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton>
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" Width="40px"  />
</asp:TemplateField>
                         <asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblaccntid" runat="server" Visible="false" text='<%#Eval("accountid")%>'></asp:Label>
        <asp:Label ID="lblaccntname" runat="server" text='<%#Eval("accountname")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
                      <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        <asp:Label ID="lblEntryID" runat="server" Visible="false" text='<%#Eval("EntryID")%>'></asp:Label>
        
        <asp:Label ID="lblType" runat="server" Visible="false" text='<%#Eval("Ctypeid")%>'></asp:Label>
        <asp:Label ID="lblContainerNo" runat="server" text='<%#Eval("ContainerNo")%>'></asp:Label>
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center" />
</asp:TemplateField>  
                         
                                   <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
       
        <asp:Label ID="lblSize" runat="server"  text='<%#Eval("Size")%>'></asp:Label>
      
            </ItemTemplate>
    <ItemStyle HorizontalAlign="center" />
</asp:TemplateField>                         
    <asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="header-center">
    <ItemTemplate>
        
        <asp:Label ID="lblntamnt" runat="server" text='<%#Eval("amount")%>'></asp:Label>

            </ItemTemplate>
    <ItemStyle HorizontalAlign="Right"  />
</asp:TemplateField>
                     </Columns>
                 </asp:GridView>
             </div>
         </div>
         </div>
    <div class="row">
        <div class="col-lg-10 text-label" style="padding-right:0px">
        <div id="divtblWOTOtal" runat="server" style="display:none;">                                         
<table forecolor="Black" class="table table-striped table-bordered table-hover" style="border-top:5px solid #7bc144;margin-left:-5px;margin-right:-5px">
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b ">Net Total</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblPercentage" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblTotal" style="margin-left:10px;"></asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Discount</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="Label1" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lbldisc" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >CGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblCgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblCGST" style="margin-left:10px;"></asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >SGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblSgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblSGST" style="margin-left:10px;"></asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">       
<%--<td style ="width:50%;text-align:left"><b >IGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblIgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblIGST" style="margin-left:10px;"></asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Grand Total</b></td>
<%--<td style ="width:20%;text-align:right"></td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblAllTotal" style="margin-left:10px;"></asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
</table>
</div>
    </div>
        </div>
    <div class="row">
       <div class="col-md-10 col-xs-12">
<div class="form-group text-label">
<b  >Remark</b>
<asp:TextBox ID="txtremarks" TextMode="MultiLine" Rows="2" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Remark"
runat="server"   ></asp:TextBox>
</div>
</div>
 </div>   
              
</asp:Panel>
                        
</div>
</div>
</fieldset>

</div>
    

<asp:Label ID="lblAccountID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblAccountName" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblagentname" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         

               

</div>
    
       <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click"  
Text="Save"  OnClientClick="return ValidationSave()" />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="EyardOtherAssessment.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="ListofProformaDetails.aspx" target="_blank"><b style="color:blue">Click here to view Assessment summary</b> </a>
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
                   
<%--<a href="BondOtherAssessment.aspx" class="btn btn-info btn-block">OK</a>--%>
 <button class="btn btn-info btn-block" id="btnoksave" data-dismiss="modal" runat="server" onserverclick="btnoksave_ServerClick" aria-hidden="true">
OK 
</button>
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
        <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblquoteApprove"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenWOPrint()"  aria-hidden="true">
Yes 
</button>
<a href="EyardOtherAssessment.aspx" class="btn btn-danger ">No</a>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div>         

</div>
       
         
</div>
   
<script type="text/javascript">
function ValidationSave() {
                 
    var txtgstin = document.getElementById('<%= txtgstin.ClientID%>').value;
    var ddlcustomer = document.getElementById('<%= ddlcustomer.ClientID%>').value;
    var ddlInvoiceType = document.getElementById('<%= ddlInvoiceType.ClientID%>').value;
    var ddlTax = document.getElementById('<%= ddlTax.ClientID%>').value;
    var txtinvdate = document.getElementById('<%= txtinvdate.ClientID%>').value;
    var txtInsFrom = document.getElementById('<%= txtInsFrom.ClientID%>').value;
    var txtInsUpto = document.getElementById('<%= txtInsUpto.ClientID%>').value;

    document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary disabled");               
               

var blResult = Boolean;
blResult = true;
 

                   
if (txtgstin == "") {
document.getElementById('<%= txtgstin.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}

    if (ddlcustomer == 0) {
        document.getElementById('<%= ddlcustomer.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
    if (ddlInvoiceType == 0) {
        document.getElementById('<%= ddlInvoiceType.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
    if (ddlTax == 0) {
        document.getElementById('<%= ddlTax.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
    if (txtinvdate == "") {
        document.getElementById('<%= txtinvdate.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
//alert('hi')
if (blResult == false) {
    alert('Please fill the required fields!');
    document.getElementById('<%= btnsave.ClientID%>').value = "Save";
    document.getElementById('<%= btnsave.ClientID%>').setAttribute("class", "btn btn-primary");
}
return blResult;
}
</script>
    <script type="text/javascript">
        var popup;
        function OpenWOPrint() {
            var txtassessno = document.getElementById('<%= txtassessno.ClientID%>').value;
    var txtworkyear = document.getElementById('<%= txtworkyear.ClientID%>').value;

            var url = "../Depo/Report_Epic/HandlingStorageProformaPrints.aspx?InvoiceNo=" + txtassessno + "&WorkYear=" + txtworkyear
    window.open(url);

}

</script>
    <script type="text/javascript">
        function ValidationAdd() {
            
            var txtgstin = document.getElementById('<%= txtgstin.ClientID%>').value;
           var ddlaccntheads = document.getElementById('<%= ddlaccntheads.ClientID%>').value;
           var txtamount = document.getElementById('<%= txtamount.ClientID%>').value;

           //document.getElementById('<%= lnkadd.ClientID%>').value = "Please Wait...";
           document.getElementById('<%= lnkadd.ClientID%>').setAttribute("class", "btn btn-info disabled");
           var blResult = Boolean;
           blResult = true;

           if (txtgstin == "") {
               document.getElementById('<%= txtgstin.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }           
           if (ddlaccntheads == 0) {
               document.getElementById('<%= ddlaccntheads.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }
           if (txtamount == 0) {
               document.getElementById('<%= txtamount.ClientID%>').style.borderColor = "red";
               blResult = blResult && false;
           }

           if (blResult == false) {
               alert('Please fill the required fields!');
               //document.getElementById('<%= btnsave.ClientID%>').value = "Save";
               document.getElementById('<%= lnkadd.ClientID%>').setAttribute("class", "btn btn-info");
    }
    return blResult;
}
</script>
    <script>
        function ClassChange() {
            var ddlTax = document.getElementById('<%= ddlTax.ClientID%>').value;
            var ddlaccntheads = document.getElementById('<%= ddlaccntheads.ClientID%>').value;
            var txtamount = document.getElementById('<%= txtamount.ClientID%>').value;

            //document.getElementById('<%= lnkadd.ClientID%>').value = "Please Wait...";
            //document.getElementById('<%= lnkadd.ClientID%>').setAttribute("class", "btn btn-info disabled");
            var blResult = Boolean;
            blResult = true;

            
            if (ddlTax == 0) {
                document.getElementById('<%= ddlTax.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }


           <%-- if (ddlaccntheads == 0) {
                document.getElementById('<%= ddlaccntheads.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            --%>
            if (blResult == false) {
                alert('Please fill the required fields!');
                //document.getElementById('<%= btnsave.ClientID%>').value = "Save";
                document.getElementById('<%= lnkadd.ClientID%>').setAttribute("class", "btn btn-info");
            }
            return blResult;
        }
    </script>
<script type="text/javascript">
function ValidateQty() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
return event.returnValue;
return event.returnValue = '';
}
function ValidateAmount() {
    //alert('hii')
    if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
        return event.returnValue;
    return event.returnValue = '';
}
function checkEmail(str) {
var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

if (reg.test(emailField.value) == false) {
alert('Invalid Email Address');
return false;
}

return true;
}

function CheckTelephone(tel) {

if (tel.length < 7)
alert("Invalid Telephone number.")
}

function CheckMobile(mob) {
if (mob.length < 10)
alert("Invalid Mobile number.");

}
</script>
    <script type="text/javascript">
        var popup;
        function gstsearch() {

            var url = "GSTPartySearch.aspx"

            popup = window.open(url, "Popup", "width=710,height=555");
            popup.focus();

        }
</script>
    <script type="text/javascript">
        var popup;
        function BondEx() {

            var url = "NOCSearchOther.aspx"

            popup = window.open(url, "Popup", "width=710,height=555");
            popup.focus();

        }
</script>
</asp:Content>

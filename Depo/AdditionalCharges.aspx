<%@ Page Title="Depo | Additional Charges" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="AdditionalCharges.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo | Additional Charges</title>
       
</head>
    <style>
        .center{
            text-align:center
        }
    </style>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i> Additional Charges
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

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-6 pull-md main-content" >
<fieldset class="register">
    <asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
</ContentTemplate>
    </asp:UpdatePanel>
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">     
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
<div class="row">
    <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Work Order No.</b>
<asp:TextBox ID="txtwono" ReadOnly="true" Style="text-transform:uppercase" class="form-control text-label"  placeholder="WO No."
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Work Order Date</b>
<asp:TextBox ID="txtwodate" ReadOnly="true" Style="text-transform:uppercase" class="form-control text-label" 
runat="server"   ></asp:TextBox>
</div>
</div>
    </div>
    <div class="row">
        <div class="col-sm-4 col-xs-12" >
<div class="form-group text-label">
<b>Invoice Type</b>
<asp:DropDownList  ID="ddlinvoicetype" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:DropDownList>
</div>
</div>
        <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b>Container No</b>
<asp:TextBox ID="txtContainerNo"  Style="text-transform:uppercase" AutoPostBack="true" OnTextChanged="txtnocno_TextChanged" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>
        </div>
    <div class="row">
<div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b  >Accounting Head</b>
<asp:DropDownList ID="ddlacchead" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                            
</asp:DropDownList> 
    <asp:Label ID="lblgroupid" runat="server" Visible="false"></asp:Label>
</div>
</div>
        <div class="col-sm-3 col-xs-12 text-label" style="padding-top:25px">
     <label runat="server"  style="width:0px; margin-left:10px;" >
            <a style="display:block">
 <asp:FileUpload ID="FileUpload1" AllowMultiple="false"  runat="server" ClientIDMode="Static" /></a>
                                       </label>
       </div>
        <div class="col-sm-1 col-xs-12" style="padding-top:21px">
                                <asp:Button ID="btnUpload" class="btn btn-success btn-sm outline"  Text="Import" runat="server" OnClientClick="return ClassChange()" OnClick="btnUpload_Click"   />  
     <asp:postbacktrigger controlid="btnUpload" xmlns:asp="#unknown"  />
        <b><asp:Label runat="server" ID="lblfilename" Text=""></asp:Label></b>
        </div>
         <div class="col-md-2 col-xs-12" >
<div class="form-group" style="padding-top:22px" >
<asp:LinkButton runat="server" ID="lnkDownloadExcel" OnClick="lnkDownloadExcel_Click" CssClass="btn btn-info btn-sm" ToolTip="Download Template"><i class="fa fa-download"></i></asp:LinkButton>
</div>
    </div> 
        </div>



<div class="row">
    <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b>Amount to Collect</b>
<asp:TextBox ID="txtamtcollect" onkeypress="return ValidateQty();"  Style="text-transform:uppercase" class="form-control text-label"  placeholder="Amount"
runat="server"></asp:TextBox>
</div>
</div>
    <div class="col-sm-2 col-xs-4">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnAdd" class="btn btn-primary " runat="server"  OnClick="btnAdd_Click" 
Text="Add" OnClientClick="return ValidationSave()"   />
</div>                                
</div>
</div>

    <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdOutDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover  "
AutoGenerateColumns="false" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
    <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblContainerNo" runat="server" text='<%#Eval("ContainerNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblAccountName" runat="server" text='<%#Eval("AccountName")%>'></asp:Label>
            <asp:Label ID="lblAccountID" Visible="false" runat="server" text='<%#Eval("AccountID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblAmount" runat="server" text='<%#Eval("Amount")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Invoice Type" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblInvoiceType" runat="server" text='<%#Eval("InvoiceType")%>'></asp:Label>
               <asp:Label ID="lblInvoiceTypeID" runat="server" Visible="false" text='<%#Eval("InvoiceTypeID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
    
</Columns>
</asp:GridView>
</div>
</div>
</div> 

    <div class="col-md-6 col-xs-12">
<div class="form-group text-label" style="padding-top:10px;">
<asp:CheckBox ID="chkisActive" runat="server" Checked="true" />
<asp:hiddenfield ID="hdlocation" runat="server" Value="0" />
<asp:Label ID="IsActiveLabel" runat="server" AssociatedControlID="chkisActive" CssClass="inline"> GST Applicable?</asp:Label>
</div>
</div>

        <div class="row">
<div class="col-md-9 col-xs-12">
<div class="form-group text-label">
<b  >Narration (if any)</b>
<asp:TextBox ID="txtnarration" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Narration"
runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
</div>
</div>
</div>                     
</asp:Panel>
                         
</div>
</div>
            
        

<div class="row">
<div class="col-sm-2 col-xs-4">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary " runat="server" OnClick="btnSave_Click" 
Text="Save" OnClientClick="return ValidationSave()"   />
</div>                                
</div>
                       
<div class="col-sm-1  col-xs-4" >
<div class="form-group" style="padding-top:15px">
                           
<a href="AdditionalCharges.aspx" id="btnclear" runat="server" class="btn btn-primary ">
Clear
</a>                        
</div>                                                                                  
</div>
                         
</div>
<asp:Label ID="lblLoadedID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblagentname" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         
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
                   
<a href="AdditionalCharges.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
               
</fieldset>

</div>
    <div class="col-md-6 pull-md main-content">
        <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
            <div class="panel-heading">
<h3 class="panel-title">
Additional Charges Details 
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
<div class="panel-body">
    <div class="row">
        <div class="col-lg-12 col-xs-12 text-label "  style="padding-right:50px;">
            <div class="table-responsive scrolling-table-container" style="margin-left:5px;margin-right:0px;">
                <asp:GridView ID="grdchargesother" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  >
                    <pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass="btn btn-danger btn-xs" Text="Cancel" OnClick="lnkCancel_Click"                                                            
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "WONO")%>' runat="server" 
                                                            ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Container NO." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:Label ID="lblCOntainerno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ContainerNo")%>'></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Account Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:Label ID="lblaccntname" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AccountName")%>'>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:Label ID="lblamount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount")%>'>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice Type" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="center">
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceType")%>'>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
       
         
</div>
   
<script type="text/javascript">
function ValidationSave() {
            
    var ddlacchead = document.getElementById('<%= ddlacchead.ClientID%>').value;
    var txtContainerNo = document.getElementById('<%= txtContainerNo.ClientID%>').value;

                  


var blResult = Boolean;
blResult = true;
<%--              
if (ddlacchead == 0) {
document.getElementById('<%= ddlacchead.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

    if (txtContainerNo == "") {
document.getElementById('<%= txtContainerNo.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}--%>

                
              
if (blResult == false) {
alert('Please fill the required fields!');
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
function ValidateNumber() {
    //alert('hii')
    if ((event.keyCode > 47 && event.keyCode < 58))
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
function ValidatePhoneNo() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 43 || event.keyCode == 32 || event.keyCode == 40 || event.keyCode == 41)
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
    <script>
        function ClassChange() {
            var ddlacchead = document.getElementById('<%= ddlacchead.ClientID%>').value;
            var txtContainerNo = document.getElementById('<%= txtContainerNo.ClientID%>').value;

                  


            var blResult = Boolean;
            blResult = true;
                         
if (ddlacchead == 0) {
document.getelementbyid('<%= ddlacchead.clientid%>').style.bordercolor = "red";
blresult = blresult && false;
}
 

                
              
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
       
            document.getElementById('<%= btnUpload.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btnUpload.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline disabled");

        }
    </script>
</asp:Content>

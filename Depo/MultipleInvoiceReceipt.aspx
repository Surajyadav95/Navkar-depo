<%@ Page Title="Depo | Multiple Invoice Receipt" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="MultipleInvoiceReceipt.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo | Multiple Invoice Receipt </title>
       
</head>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>Multiple Invoice Receipt
</h3>           
</div>
<style type="text/css">                 
        .center {
            text-align: center;
        }
        .scrolling-table-container1{
            height:130px;
            overflow:auto;
        }
    </style>  
     
         
<div id="page-content">
        
      
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
 
<div class="panel-body">
    
<div class="row">
                                         
<div class="col-md-12 col-xs-12  pull-left main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Multiple Invoice Receipt
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
       
<div class="panel-body">
    
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False" >
                         <Triggers>
                             <asp:Postbacktrigger controlid="ddltds" />                              
                                 <asp:Postbacktrigger  ControlID="ddlmode" />
                                 <asp:Postbacktrigger  ControlID="btnAdd" />
                                 <asp:Postbacktrigger  ControlID="grdinvoices" />
                                 
                            
                         </Triggers>
                        <ContentTemplate>

                    <div class="row">

                    <div class="col-md-2 col-xs-6" >
                    <div class="form-group text-label" style="text-decoration-color:black">
                    <b >Receipt No</b>
                    <asp:TextBox ID="txtReceiptNo" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
                    runat="server" Text="" placeholder="NEW"></asp:TextBox>     
                    </div>
                    </div>

                      
                    <div class="col-md-2 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Receipt Date</b>
                    <asp:TextBox ID="txtReceiptdate" placeholder="dd-MM-yyyy" runat="server" ReadOnly="true" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>                                            
                        <div class="col-md-4 col-xs-6">
                    <div class="form-group text-label">
                    <b>Line Name</b>
                    <asp:DropDownList ID="ddlLineName" Style="text-transform:uppercase;" AutoPostBack="true" runat="server" class="form-control text-label">                               
                    </asp:DropDownList> 
                    </div>
                    </div>
                        <div class="col-md-2 col-xs-6" style="display:none" >
                    <div class="form-group text-label" style="text-decoration-color:black">
                    <b >PD Code</b>
                    <asp:TextBox ID="txtpdcode" Style="text-transform: uppercase; background-color:#e9e9e9" class="form-control text-label form-cascade-control"
                    runat="server" Text="" placeholder="NEW"></asp:TextBox>     
                    </div>
                    </div>
                         <div class="col-md-1 col-xs-12">
                    <div class="form-group pull-left" style="padding-top:20px" >
                    <asp:Button ID="btnsearch" runat="server" 
                    class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click" OnClientClick="return ValidationShow()"/>

                    </div>              
                    </div>
                        <asp:panel runat="server" ID="pnlextrainvoice" Enabled="true">
                         <div class="col-sm-2 col-xs-6">
                    <div class="form-group text-label">
                        <b>Add Invoice</b>
                    <asp:TextBox ID="txtID" Style="text-transform:uppercase;display:none" class="form-control text-label"  placeholder="Enter Invoice No"
                    runat="server" ></asp:TextBox>
                        <asp:TextBox ID="txtinvoiceadd" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Enter Invoice No"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>
                        <div class="col-md-1 col-xs-12">
                    <div class="form-group pull-left" style="padding-top:20px" >
                    <asp:Button id="Button1" runat="server" Text="Add"  
                    class="btn btn-info btn-sm outline" OnClick="Button1_ServerClick"/>

                    </div>              
                    </div>
                            </asp:panel>
                         </div>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="row">
                    <div class="col-lg-12 col-xs-12 text-label " >
                        <asp:Label ID="Label1" Visible="false" runat="server" Text=""></asp:Label>
                    <div class="table-responsive scrolling-table-container1">
                    <asp:GridView ID="grdinvoices" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                    AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
                    <Columns>
                        <asp:TemplateField Visible="true">
                             <ItemTemplate>
                                 <asp:CheckBox ID="chkright" Text="" Checked='<%# Eval("Select")%>' runat="server" />
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice No" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblinvno" runat="server" Text='<%# Eval("InvoiceNo")%>'></asp:Label>
                                 <asp:Label id="lblautoid" Visible="false" runat="server" Text='<%# Eval("AutoTemp")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice Date" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblinvdate" runat="server" Text='<%# Eval("Invoice Date")%>'></asp:Label>
                                 <asp:Label id="lblinvworkyear" Visible="false" runat="server" Text='<%# Eval("AssessYear")%>'></asp:Label>

                             </ItemTemplate>                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Invoice Amount" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblinvAmount" runat="server" Text='<%# Eval("InvoiceAmount")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Credit Amount" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblcreditamt" runat="server" Text='<%# Eval("CreditAmount")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Previous Received Amount" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblprevrecamt" runat="server" Text='<%# Eval("PreviousRecAmount")%>'></asp:Label>
                                 
                             </ItemTemplate>                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Receival Amount" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblreceivalamt" runat="server" Text='<%# Eval("ReceivalAmount")%>'></asp:Label>
                             </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Received Amount" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                 <asp:TextBox runat="server" ID="txtreceivedamt" onkeypress="return ValidateAmount()" AutoPostBack="true" OnTextChanged="txtreceivedamt_TextChanged" Width="100px" Class="form-control" Text='<%# Eval("ReceivedAmount")%>'></asp:TextBox>
                             </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />    
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="TDS" HeaderStyle-CssClass="center" >
                             <ItemTemplate>                                 
                                 <asp:TextBox runat="server" ID="txttdsamt" onkeypress="return ValidateAmount()" AutoPostBack="true" OnTextChanged="txtreceivedamt_TextChanged" Width="100px" Class="form-control" Text='<%# Eval("TDSAmount")%>'></asp:TextBox>
                             </ItemTemplate> 
                             <ItemStyle HorizontalAlign="Right" />                           
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Net Amount Received" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblnetamtrec" runat="server" Text='<%# Eval("NetAmount")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="O/S" HeaderStyle-CssClass="center" >
                             <ItemTemplate>
                                 <asp:Label id="lblOS" runat="server" Text='<%# Eval("OSAmount")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
 
                    </Columns>

                    </asp:GridView>
                    </div>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                            


                            <%-- jjjjjjjjjjjjjjjjjj --%>
                  

                    
                    
                   

                    <div class="row">
                    <div class="col-sm-3 col-xs-6">
                    <div class="form-group text-label">
                    <b>Payment From</b>
                    <asp:DropDownList ID="ddlpayment" Style="text-transform:uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddlpayment_SelectedIndexChanged" runat="server"  class="form-control text-label">  
                             
                    </asp:DropDownList> 

                    </div>
                    </div>

                    <div class="col-sm-6 col-xs-6" style="margin-top:20px;">
                    <div class="form-group text-label">
                   
                    <asp:DropDownList ID="ddlfill" Style="text-transform: uppercase;"   runat="server"  class="form-control text-label">                  
                    </asp:DropDownList> 

                    </div>
                    </div>

                   

                    </div>
                    
                    <div class="row">
                    <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                    <b>Receipt Amount</b>
                    <asp:TextBox ID="txtAmount" text="0" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Receipt Amount"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>

                    <div class="col-sm-3 col-xs-6" style="display:none" >
                    <div class="form-group text-label">
                     <b>TDS</b>
                    <asp:DropDownList ID="ddltds" Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddltds_SelectedIndexChanged" runat="server"  class="form-control text-label">                          
                    </asp:DropDownList> 

                    </div>
                    </div>
                        <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">                        
                    <b> TDS Amount</b>
                    <asp:TextBox ID="txttdsamt" ReadOnly="true" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Amount"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>
                    </div>
                            </ContentTemplate>
                    </asp:UpdatePanel> 
                   <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <div class="row" >
                       <%--  <asp:UpdatePanel ID="UpdatePanel45" runat="server" UpdateMode="Conditional">
                             <Triggers>
                                 <asp:PostBackTrigger  ControlID="ddlmode" />
                                 <asp:PostBackTrigger  ControlID="btnAdd" />
                                 
                             </Triggers>
                        <ContentTemplate>--%>
                        <asp:Panel runat="server" ID="pnlmode" Enabled="true">
                    <div class="col-sm-2 col-xs-6"  >
                    <div class="form-group text-label">
                    <b style="color:blue">Mode</b>
                    <asp:DropDownList ID="ddlmode"  Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChange" runat="server"  class="form-control text-label">                                       
                    </asp:DropDownList> 

                    </div>
                    </div>
                            </asp:Panel>
                    <asp:Label ID="lbltdsamt" Visible="false" runat="server" Text=""></asp:Label>
                     
                    <div class="col-sm-2 col-xs-6" >
                    <div class="form-group text-label">
                         <asp:Label ID="lblAmount" runat="server" visible="false" Text="0"></asp:Label>
                    <b style="color:blue"> Amount</b>
                    <asp:TextBox ID="txtmodeAmount" onkeypress="return ValidateAmount()" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Amount"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                        <asp:Panel runat="server" ID="panelPayment" Enabled="true">
                    <div class="col-sm-2 col-xs-6" >
                    <div class="form-group text-label">
                    <b style="color:blue"> Cheque/DD No.</b>
                    <asp:TextBox ID="txtModeNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Mode No"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>
                             </asp:Panel>
<div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b style="color:blue">Mode Date</b>
                    <asp:TextBox ID="txtmodedate"  runat="server" TextMode="Date" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div> 
                        </div>
                            <div class="row" >
                                <asp:Panel runat="server" ID="panelbank" Enabled="true">
                    <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                    <b style="color:blue">Bank Name</b>
                    <asp:DropDownList ID="ddlbank" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                  
                    </asp:DropDownList> 

                    </div>
                    </div>
                    
                    <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                    <b style="color:blue">Bank Code</b>
                    <asp:TextBox ID="txtbankcod" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Bank Code"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>

                    <div class="col-sm-3 col-xs-12" >
                    <div class="form-group text-label">
                    <b style="color:blue"> Branch Name</b>
                    <asp:DropDownList ID="ddlBranch" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
                    </asp:DropDownList> 

                    </div>
                    </div>
                       </asp:Panel>
                         
                    
                   
                    <div class="col-md-1 col-xs- 12">
                    <div class="form-group pull-left" style="padding-top:20px" >
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"  
                    class="btn btn-primary btn-sm outline" Text="Add" OnClientClick="return ValidationAdd();"  >

                    </asp:Button>
                    </div>              
                    </div>
                                </div>
                                       
                     
                  
                    <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="row">
                    <div class="col-lg-12 col-xs-12 text-label " >
                        <asp:Label ID="lblReceiptNo" Visible="false" runat="server" Text=""></asp:Label>
                    <div class="table-responsive scrolling-table-container">
                    <asp:GridView ID="grdPaymentDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                    AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
                    <Columns>
                        <asp:TemplateField Visible="true">
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkdelete" ControlStyle-CssClass='btn btn-primary btn-xs outline'  Text="Cancel" OnCommand="lnkdelete_Command"
                                 CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "AutoTemp")%>' runat="server"
                                 ></asp:LinkButton>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mode" >
                             <ItemTemplate>
                                 <asp:Label id="lblPaymode" runat="server" Text='<%# Eval("paymode")%>'></asp:Label>
                                 <asp:Label id="lblPayID" Visible="false" runat="server" Text='<%# Eval("Mode")%>'></asp:Label>

                             </ItemTemplate>
                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Amount" >
                             <ItemTemplate>
                                 <asp:Label id="lblPayAmount" runat="server" Text='<%# Eval("AMOUNT")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Mode No.">
                             <ItemTemplate>
                                 <asp:Label id="lblPaymodeNo" runat="server" Text='<%# Eval("Mode_No")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Name">
                             <ItemTemplate>
                                 <asp:Label id="lblBankName" runat="server" Text='<%# Eval("BankName")%>'></asp:Label>
                                 <asp:Label id="lblBankId" Visible="false" runat="server" Text='<%# Eval("BankId")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Bank Code">
                             <ItemTemplate>
                                 <asp:Label id="lblBankCode" runat="server" Text='<%# Eval("BANK_CODE")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch Name" >
                             <ItemTemplate>
                                 <asp:Label id="lblBranchName" runat="server" Text='<%# Eval("BranchName")%>'></asp:Label>
                                 <asp:Label id="lblBranchId" Visible="false" runat="server" Text='<%# Eval("BranchId")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Mode Date" >
                             <ItemTemplate>
                                 <asp:Label id="lblModeDate" runat="server" Text='<%# Eval("MODEDate")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                        
 
                    </Columns>

                    </asp:GridView>
                    </div>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                   

               
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                     <ContentTemplate>
                         <div class="row">
                         <div class="col-md-6 col-xs-12" >
                    <div class="form-group text-label">
                    <b> Ledger Name</b>
                    <asp:DropDownList ID="ddlledger" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
                    </asp:DropDownList> 

                    </div>
                    </div>
                             </div>
                    <div class="row">
                    <div class="col-md-8 col-xs-12">
                    <div class="form-group text-label">
                    <b >Remark (if any)</b>
                    <asp:TextBox ID="txtremarks" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Remark"  
                    TextMode="MultiLine"   runat="server"   ></asp:TextBox>
                    </div>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row">
                    <div class="col-md-4" style="padding-left:16px;">
                    <div class="form-group" style="padding-top:15px">
                    <asp:Button ID="btnSave" OnClientClick="return ValidationSave();" class="btn btn-success btn-sm outline " runat="server"      
                    Text="Save"  />
                   
                    <a href="MultipleInvoiceReceipt.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
                    Clear
                    </a>
                    </div>
                    </div>
                    <div class="col-lg-8" >
                    <div class="form-group pull-right" style="margin-top:18px">
                    <a href="EyardReceiptSummary.aspx" target="_blank"><b style="color:blue">Click here to view Receipt Summary</b> </a>
                    </div>
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
               <%--     <div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
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
                   <button class="btn btn-info btn-block" ID="saveQuoOK" data-dismiss="modal" runat="server"  aria-hidden="true">OK </button>
                   
                                
                    </div>
                    </div>
                    
                    </ContentTemplate>
             
                    </asp:UpdatePanel>
                    </div>
                    </div>

                    <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-sm">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="modal-content">
                    <div class="modal-header">
                    <center>
                    <span><i runat="server" id="I1" class="fa fa-5x fa-check-circle-o text-success"></i></span>
                    <br />
                    <h4 class="modal-title">

                    <asp:Label ID="lblSaveMessage" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
                    </center>
                    </div>
                         <div class="modal-footer">
                             <button class="btn btn-info " ID="btnprint" onserverclick="OnbtnPrint" data-dismiss="modal" runat="server"  aria-hidden="true">
                                 Yes 
                             </button>
                          

                             <a href="BondGenerateReceipt.aspx" class="btn btn-danger ">No</a>
                   
                    </div>
                    
                    </ContentTemplate>
             
                    </asp:UpdatePanel>
                    </div>
                    </div>--%>
                <div class="modal fade control-label" id="myModalforupdate2" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-sm">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="modal-content">
                    <div class="modal-header">
                    <center>
                    <span><i runat="server" id="I2" class="fa fa-5x fa-check-circle-o text-success"></i></span>
                    <br />
                    <h4 class="modal-title">

                    <asp:Label ID="lblsession1" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
                    </center>
                    </div>
                         <div class="modal-footer">
                             <%--<button class="btn btn-info " ID="Button2" onserverclick="OnbtnPrint" data-dismiss="modal" runat="server"  aria-hidden="true">
                                 Yes 
                             </button>--%>
                           <%--  <a href="BondGenerateReceipt.aspx" class="btn btn-info ">Yes</a>--%>

                             <a href="MultipleInvoiceReceipt.aspx" class="btn btn-info btn-block ">OK</a>
                   
                    </div>
                    
                    </ContentTemplate>
             
                    </asp:UpdatePanel>
                    </div>
                    </div>
                  
</div>
    <script type="text/javascript">
    function isNumber(evt, element) {

                var charCode = (evt.which) ? evt.which : event.keyCode

                if (
             // (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57)) {
                    alert("Please enter only numerics")
                    return false;
                }
                else {

                    return true;
                }
    }
        </script>
    <script type="text/javascript">
        function ValidateYear() {
            //alert('hii')
            if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 45)
                return event.returnValue;
            return event.returnValue = '';
        }
        function ValidateNumber() {
            //alert('hii')
            if ((event.keyCode > 47 && event.keyCode < 58))
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
       function ValidationSave()
       {
           var ddlLineName = document.getElementById('<%= ddlLineName.ClientID%>').value;
           var ddlfill = document.getElementById('<%= ddlfill.ClientID%>').value;
           var txtAmount = document.getElementById('<%= txtAmount.ClientID%>').value;
           var ddlledger = document.getElementById('<%= ddlledger.ClientID%>').value;
           
           document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
           document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-success disabled");
           var blResult = Boolean;
           blResult = true;

           document.getElementById('<%= ddlLineName.ClientID%>').style.borderColor = "Gainsboro";
           document.getElementById('<%= ddlfill.ClientID%>').style.borderColor = "Gainsboro";
           document.getElementById('<%= txtAmount.ClientID%>').style.borderColor = "Gainsboro";

           <%--if (ddlcustomer == "0") {
               document.getElementById('<%= ddlcustomer.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }--%>
           if (ddlfill == "0") {
               document.getElementById('<%= ddlfill.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }
           if (txtAmount == "0") {
               document.getElementById('<%= txtAmount.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }
           if (ddlledger == "0") {
               document.getElementById('<%= ddlledger.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }
           if (blResult == false) {
               alert('Please fill the required fields!');
               document.getElementById('<%= btnSave.ClientID%>').value = "Save";
               document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-success");
           }
           return blResult;
        }
</script>

    
   <script type="text/javascript">
        function ValidationShow() {
            var ddlLineName = document.getElementById('<%= ddlLineName.ClientID%>').value;
         //  var ddlfill = document.getElementById('<%= ddlfill.ClientID%>').value;

           var blResult = Boolean;
           blResult = true;

           ddlLineName.getElementById('<%= ddlLineName.ClientID%>').style.borderColor = "Gainsboro";
          // document.getElementById('<%= ddlfill.ClientID%>').style.borderColor = "Gainsboro";

            if (ddlcustomer == 0) {
               document.getElementById('<%= ddlLineName.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }
          

            if (blResult == false) {
                alert('Please fill the required fields!');
              
            }
           return blResult;
       }
</script>
     <script type="text/javascript">
         function ValidationAdd() {
           //  alert("hi")
             var txtAmount = document.getElementById('<%= txtAmount.ClientID%>').value;
             var ddlmode = document.getElementById('<%= ddlmode.ClientID%>').value;
            
             document.getElementById('<%= btnAdd.ClientID%>').value = "Please Wait...";
             document.getElementById('<%= btnAdd.ClientID%>').setAttribute("class", "btn btn-primary disabled");
           var blResult = Boolean;
           blResult = true;

           document.getElementById('<%= txtAmount.ClientID%>').style.borderColor = "Gainsboro";
             document.getElementById('<%= ddlmode.ClientID%>').style.borderColor = "Gainsboro";
          

             if (txtAmount == "" || txtAmount == "0.00" || txtAmount == "0") {
               document.getElementById('<%= txtAmount.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }
             if (ddlmode == 0) {
                 document.getElementById('<%= ddlmode.ClientID%>').style.borderColor = "red"
                 blResult = blResult && false;
             }

             if (blResult == false) {
                 alert('Please fill the required fields!');
                 document.getElementById('<%= btnAdd.ClientID%>').value = "Add";
                 document.getElementById('<%= btnAdd.ClientID%>').setAttribute("class", "btn btn-primary");
             }
            
           return blResult;

       }
</script>
</asp:Content>

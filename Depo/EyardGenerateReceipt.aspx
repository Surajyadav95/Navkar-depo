<%@ Page Title="Depo | Eyard Generate Receipt" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EyardGenerateReceipt.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo | Eyard Generate Receipt</title>
       
</head>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>Eyard Generate Receipt
</h3>           
</div>
<style type="text/css">
        

       

        .modalload1 {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: white;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
            margin-left: -120px;
        }

        .center1 {
            z-index: 1000;
            margin: 30px auto;
            padding: 10px;
            width: 430px;
            background-color: transparent;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center1 img {
                height: 308px;
                width: 308px;
            }

        .center {
            text-align: center;
        }
    </style>  
     
         
<div id="page-content">
        
      
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
 
<div class="panel-body">
    
<div class="row">
                                         
<div class="col-md-9 col-xs-12  pull-left main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Eyard Generate Receipt
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
       
<div class="panel-body">
    
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False" >
                         <Triggers>
                             <asp:asyncPostbacktrigger controlid="ddltds" />
                              
                                 <asp:asyncPostbacktrigger  ControlID="ddlmode" />
                                 <asp:Postbacktrigger  ControlID="btnAdd" />
                                 
                            
                         </Triggers>
                        <ContentTemplate>

                    <div class="row">

                    <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label" style="text-decoration-color:black">
                    <b >Receipt No</b>
                    <asp:TextBox ID="txtReceiptNo" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
                    runat="server" Text="" placeholder="NEW"></asp:TextBox>     
                    </div>
                    </div>

                    <div class="col-md-3 col-xs-6">
                    <div class="form-group text-label">
                    <b  >Last Receipt No</b>
                    <asp:TextBox ID="txtlastreceipt" Style="text-transform: uppercase;" runat="server" ReadOnly="true"  class="form-control text-label"  placeholder="Last Receipt No">                                        
                    </asp:TextBox> 
                    </div>
                    </div>
                      
                    <div class="col-sm-2 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Receipt Date</b>
                    <asp:TextBox ID="txtReceiptdate" placeholder="dd-MM-yyyy" runat="server" ReadOnly="true" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>
          
                   

                    

                         
                         </div>

                    <div class="row">
                         <div class="col-md-2 col-xs-6">
                    <div class="form-group text-label">
                    <b  >Assess Year</b>
                    <asp:TextBox ID="txtWorkYear" placeholder="Assess Year"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                                        
                    </asp:TextBox> 
                    </div>
                    </div>

                     <div class="col-md-3   col-xs-6">
                    <div class="form-group text-label">
                    <b  >Assessment No</b>
                    <asp:TextBox ID="txtAssessment" onkeypress="return isNumber(event)"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label"  placeholder="Assessment No">                                        
                    </asp:TextBox> 
                    </div>
                    </div>

                    <div class="col-md-1 col-xs- 12">
                    <div class="form-group pull-left" style="padding-top:20px" >
                    <asp:Button ID="btnsearch" runat="server" 
                    class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click" OnClientClick="return ValidationShow()"/>

                    </div>              
                    </div>

                    </div>

                    <div class="row" style="display:none">
                    <div class="col-sm-3 col-xs-12">
                    <div class="form-group text-label">                                        
                    <b >NOC No</b>
                    <asp:TextBox ID="txtnoc" Style="text-transform:uppercase" class="form-control text-label"  placeholder="NOC No"
                    runat="server"  ></asp:TextBox>
                    </div>
                    </div>
                    </div>

                    <div class="row" style="display:none">
                    <div class="col-sm-6 col-xs-12">
                    <div class="form-group text-label">                                     
                    <b >Consignee</b>
                    <asp:TextBox ID="txtConsignee" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Consignee"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>
                    
                    <div class="col-sm-6 col-xs-12">
                    <div class="form-group text-label">
                    <b >CHA Code/Name</b>
                    <asp:TextBox ID="txtCHAcode" Style="text-transform:uppercase" class="form-control text-label"  placeholder="CHA Code/Name"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>
                    </div>
                    
                    <div class="row" >
                    <div class="col-sm-12 col-xs-12">
                    <div class="form-group text-label">
                    <b >Assessment Date:  </b>
                    <asp:label ID="lblAsseDate" Style="text-transform:uppercase" class=" text-label"  
                    runat="server"  ></asp:label>   <br />             
                    <b >Line: </b>
                    <asp:label ID="lblLine" Style="text-transform:uppercase" class=" text-label"  
                    runat="server"  ></asp:label><br />
                  
                    <asp:label ID="lbltdsAmtNew" Visible="false" Style="text-transform:uppercase" Text="0" class=" text-label"  
                    runat="server"  ></asp:label>
                    </div>
                    </div>
                    </div>

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

                    <div class="col-sm-3 col-xs-6" style="margin-top:20px;display:none">
                    <div class="form-group text-label">
                    <asp:TextBox ID="txtID" ReadOnly="true" Style="text-transform:uppercase" class="form-control text-label"  placeholder=""
                    runat="server" ></asp:TextBox>
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

                    <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                     <b>TDS</b>
                    <asp:DropDownList ID="ddltds" Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddltds_SelectedIndexChanged" runat="server"  class="form-control text-label"> 
                         
                    </asp:DropDownList> 

                    </div>
                    </div>
                    </div>
                   <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <div class="row" style="background-color:#e6e6ff">
                       <%--  <asp:UpdatePanel ID="UpdatePanel45" runat="server" UpdateMode="Conditional">
                             <Triggers>
                                 <asp:PostBackTrigger  ControlID="ddlmode" />
                                 <asp:PostBackTrigger  ControlID="btnAdd" />
                                 
                             </Triggers>
                        <ContentTemplate>--%>
                        <asp:Panel runat="server" ID="pnlmode" Enabled="true">
                    <div class="col-sm-3 col-xs-6"  >
                    <div class="form-group text-label">
                    <b>Mode</b>
                    <asp:DropDownList ID="ddlmode"  Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChange" runat="server"  class="form-control text-label">                                       
                    </asp:DropDownList> 

                    </div>
                    </div>
                            </asp:Panel>
                    <asp:Label ID="lbltdsamt" Visible="false" runat="server" Text=""></asp:Label>
                     
                    <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                         <asp:Label ID="lblAmount" runat="server" visible="false" Text="0"></asp:Label>
                    <b> Amount</b>
                    <asp:TextBox ID="txtmodeAmount" onkeypress="return isNumber(event)" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Amount"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                        <asp:Panel runat="server" ID="panelPayment" Enabled="true">
                    <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                    <b> Mode No.</b>
                    <asp:TextBox ID="txtModeNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Mode No"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>

                    <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                    <b>Bank Name</b>
                    <asp:DropDownList ID="ddlbank" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                  
                    </asp:DropDownList> 

                    </div>
                    </div>
                    
                    <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                    <b>Bank Code</b>
                    <asp:TextBox ID="txtbankcod" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Bank Code"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>

                    <div class="col-sm-3 col-xs-12" >
                    <div class="form-group text-label">
                    <b> Branch Name</b>
                    <asp:DropDownList ID="ddlBranch" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
                    </asp:DropDownList> 

                    </div>
                    </div>
                       
                         
                    <div class="col-sm-3 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b>Mode Date</b>
                    <asp:TextBox ID="txtmodedate"  runat="server" TextMode="Date" Class="form-control text-label"></asp:TextBox>
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
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>
                    <br />
                    <div class="row" >
                    <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="row">
                    <div class="col-lg-12 text-label "  style="padding-right:50px;">
                        <asp:Label ID="lblReceiptNo" Visible="false" runat="server" Text=""></asp:Label>
                    <div class="table-responsive scrolling-table-container" style="margin-left:12px;margin-right:0px;">
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
                         <asp:TemplateField HeaderText="Mode No." >
                             <ItemTemplate>
                                 <asp:Label id="lblPaymodeNo" runat="server" Text='<%# Eval("Mode_No")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Name" >
                             <ItemTemplate>
                                 <asp:Label id="lblBankName" runat="server" Text='<%# Eval("BankName")%>'></asp:Label>
                                 <asp:Label id="lblBankId" Visible="false" runat="server" Text='<%# Eval("BankId")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Bank Code" >
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
                         <%--<asp:BoundField DataField="paymode" HeaderText="Mode"></asp:BoundField>--%>
                         <%--<asp:BoundField DataField="AMOUNT" ItemStyle-HorizontalAlign="Right" HeaderText="Amount"></asp:BoundField>
                         <asp:BoundField DataField="Mode_No" HeaderText="Mode No."></asp:BoundField>
                         <asp:BoundField DataField="BankName" HeaderText="Bank Name"></asp:BoundField>
                         <asp:BoundField DataField="BANK_CODE" HeaderText="Bank Code"></asp:BoundField>
                         <asp:BoundField DataField="BranchName" HeaderText="Branch Name"></asp:BoundField>
                         <asp:BoundField DataField="MODEDate" HeaderText="Mode Date"></asp:BoundField>--%>
 
                    </Columns>

                    </asp:GridView>
                    </div>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    </div>

               
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                     <ContentTemplate>
                    <div class="row">
                    <div class="col-md-12 col-xs-12">
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
                   
                    <a href="EyardGenerateReceipt.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
                    Clear
                    </a>
                    </div>
                    </div>
                    <div class="col-lg-8" >
                    <div class="form-group pull-right" style="margin-top:18px">
                    <a href="EyardReceiptSummary.aspx" target="_blank"><b style="color:blue">Click here to view Eyard Receipt Summary</b> </a>
                    </div>
                    </div>
                    </div>

                   

</div>
    
               
                         
                   
</fieldset>
    
</div>
                    <div class="col-md-3 col-xs-12 pull-right sidebar" style="padding-top:0px;">
                    <div menuitemname="Client Details" class="panel panel-sidebar" style="height:500px">
                    <div class="panel-heading">
                    <h3 class="panel-title" style="padding-bottom: 0px !important; color:blue"  >
                    <b >Today's Collection </b>   
                    <%-- <i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
                    </h3>
                    </div>   
                    <div class="panel-body"  >
                         <asp:Repeater ID="rptAmount" runat="server" >
                        <ItemTemplate>
                    <div class="row" style="color:blue">
                    <div class="col-md-12 col-xs-12  ">   
                    <div class=" text-label">                                       
                    <b ><%#Eval("Paymode")%></b>&nbsp;&nbsp;<asp:label ID="txtcash" Style="text-transform:uppercase;text-align:right;background-color:#eee;width:130px;border:thick"  ReadOnly="true" class=" text-label pull-right"  placeholder=" "
                    runat="server"  Text='<%#Eval("Amount")%>'  ></asp:label>
                    
                    </div>
                    </div>
                    </div>
                        <br />
                            </ItemTemplate>
                             </asp:Repeater>
                   
                        <br />
                   
                        <br />
                    
                    </div>
                    </div>
                           
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
                   <button class="btn btn-info btn-block" ID="saveQuoOK" data-dismiss="modal" runat="server"  aria-hidden="true">OK </button>
                    <%--<a href="#" class="btn btn-info btn-block">OK</a>--%>
                                
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
                           <%--  <a href="BondGenerateReceipt.aspx" class="btn btn-info ">Yes</a>--%>

                             <a href="EyardGenerateReceipt.aspx" class="btn btn-danger ">No</a>
                   
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
       function ValidationSave()
       {
           var txtAssessment = document.getElementById('<%= txtAssessment.ClientID%>').value;
           var ddlfill = document.getElementById('<%= ddlfill.ClientID%>').value;
           var txtAmount = document.getElementById('<%= txtAmount.ClientID%>').value;
           
           document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
           document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-success disabled");
           var blResult = Boolean;
           blResult = true;

           document.getElementById('<%= txtAssessment.ClientID%>').style.borderColor = "Gainsboro";
           document.getElementById('<%= ddlfill.ClientID%>').style.borderColor = "Gainsboro";
           document.getElementById('<%= txtAmount.ClientID%>').style.borderColor = "Gainsboro";

           if (txtAssessment == "") {
               document.getElementById('<%= txtAssessment.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }
           if (ddlfill == "0") {
               document.getElementById('<%= ddlfill.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }
           if (txtAmount == "0") {
               document.getElementById('<%= txtAmount.ClientID%>').style.borderColor = "red"
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
            var txtAssessment = document.getElementById('<%= txtAssessment.ClientID%>').value;
         //  var ddlfill = document.getElementById('<%= ddlfill.ClientID%>').value;

           var blResult = Boolean;
           blResult = true;

           document.getElementById('<%= txtAssessment.ClientID%>').style.borderColor = "Gainsboro";
          // document.getElementById('<%= ddlfill.ClientID%>').style.borderColor = "Gainsboro";

           if (txtAssessment == "") {
               document.getElementById('<%= txtAssessment.ClientID%>').style.borderColor = "red"
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
            
             document.getElementById('<%= btnAdd.ClientID%>').value = "Please Wait...";
             document.getElementById('<%= btnAdd.ClientID%>').setAttribute("class", "btn btn-primary disabled");
           var blResult = Boolean;
           blResult = true;

           document.getElementById('<%= txtAmount.ClientID%>').style.borderColor = "Gainsboro";
          

             if (txtAmount == "0") {
               document.getElementById('<%= txtAmount.ClientID%>').style.borderColor = "red"
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

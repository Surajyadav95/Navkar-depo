<%@ Page Title="Depo | PD Adjustments" Culture="en-GB" Language="VB" MasterPageFile="~/Depo/User.master" AutoEventWireup="false" CodeFile="PDAdjustment.aspx.vb" Inherits="Bond_PDAdjustment" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo | PD Adjustments </title>
       
</head>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>PD Adjustments
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
                                         
<div class="col-md-9 pull-left main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
PD Adjustments
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
       
<div class="panel-body">
    
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False" >
                         <Triggers>
                             <asp:asyncPostbacktrigger controlid="ddltds" />                            
                                 
                                 <asp:Postbacktrigger  ControlID="btnAdd" />
                                 
                            
                         </Triggers>
                        <ContentTemplate>

                    <div class="row">

                    <div class="col-sm-2 col-xs-6" >
                    <div class="form-group text-label" style="text-decoration-color:black">
                    <b >Trans No.</b>
                    <asp:TextBox ID="txtReceiptNo" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
                    runat="server" Text="" placeholder="NEW"></asp:TextBox>     
                    </div>
                    </div>
                        <div class="col-sm-3 col-xs-6" style="display:none">
                            <asp:TextBox ID="txttransNo" Style="text-transform: uppercase;" ReadOnly="true"  class="form-control text-label form-cascade-control"
                    runat="server" placeholder="NEW"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-xs-6">                                      
                    <div class="form-group text-label">
                    <b >Trans Date</b>
                    <asp:TextBox ID="txtReceiptdate" runat="server" ReadOnly="true" Class="form-control text-label"></asp:TextBox>
                    </div>                        
                    </div>

                    <div class="col-md-3 col-xs-6">
                    <div class="form-group text-label">
                    <b  >Last Receipt No</b>
                    <asp:TextBox ID="txtlastreceipt"   Style="text-transform: uppercase;" runat="server" ReadOnly="true"  class="form-control text-label"  placeholder="Last Receipt No">                                        
                    </asp:TextBox> 
                    </div>
                    </div>
                      
                    
          
                         </div>

                    <div class="row">
                         <div class="col-md-2 col-xs-6">
                    <div class="form-group text-label">
                    <b  >Assess Year</b>
                    <asp:TextBox ID="txtWorkYear" MaxLength="7" onkeypress="return ValidateYear()"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                                        
                    </asp:TextBox> 
                    </div>
                    </div>
                     <div class="col-md-3 col-xs-6">
                    <div class="form-group text-label">
                    <b>Assessment No</b>
                    <asp:TextBox ID="txtAssessment" onkeypress="return ValidateNumber()"  Style="text-transform: uppercase;" runat="server"  class="form-control text-label"  placeholder="Assessment No">                                        
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
                    
                    <div class="row" >
                    <div class="col-sm-12 col-xs-12">
                    <div class="form-group text-label">
                    <b>Valid Upto Date:  </b>
                    <asp:label ID="lblvaliduptoDate" Style="text-transform:uppercase" class=" text-label"  
                    runat="server"  ></asp:label>   <br />             
                    <b>Line Name:  </b>
                    <asp:label ID="lblLineName" Style="text-transform:uppercase" class=" text-label"  
                    runat="server"  ></asp:label><br />
              
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
                        <div class="col-sm-3 col-xs-6">
                    <div class="form-group text-label">
                    <b>TDS Amount</b>
                    <asp:TextBox ID="txttdsamount" onkeypress="return ValidateAmount()" text="0" Style="text-transform:uppercase" class="form-control text-label"  placeholder="TDS Amount"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>
                        <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                    <b>Adjustment Amount</b>
                    <asp:TextBox ID="txtadjustamount" onkeypress="return ValidateAmount()" text="0" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Adjustment Amount"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>
                        </div>
                            <div class="row">
                                <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                     <b>PD Accounts</b>
                    <asp:DropDownList ID="ddlpdaccounts" Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddlpdaccounts_SelectedIndexChanged" runat="server"  class="form-control text-label">                          
                    </asp:DropDownList> 
                    </div>
                    </div>
                        <div class="col-sm-3 col-xs-6" >
                    <div class="form-group text-label">
                    <b>Balance Amount</b>
                    <asp:TextBox ID="txtbalanceamount" text="0" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Balance Amount"
                    runat="server" ></asp:TextBox>
                    </div>
                    </div>
                        
                                
                                 <div class="col-md-1 col-xs- 12" style="display:none">
                    <div class="form-group pull-left" style="padding-top:20px" >
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click"  
                    class="btn btn-primary btn-sm outline" Text="Add" OnClientClick="return ValidationAdd();"  >

                    </asp:Button>
                    </div>              
                    </div>
                            </div>
                        
                    

                    <%--<div class="row" style="background-color:#e6e6ff">--%>

                   
                    </ContentTemplate>
                    </asp:UpdatePanel>
                

                    <div class="row" style="display:none" >
                    <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="grdPaymentDetails" />
                        </Triggers>
                    <ContentTemplate>
                    <div class="row">
                    <div class="col-md-8 col-xs-12 text-label ">
                        
                    <div class="table-responsive scrolling-table-container" style="margin-left:12px;margin-right:0px;">
                    <asp:GridView ID="grdPaymentDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
                    AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  PageSize="6">
                    <Columns>
                        <asp:TemplateField Visible="true">
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkdelete" ControlStyle-CssClass='btn btn-primary btn-xs outline'  Text="Cancel" OnCommand="lnkdelete_Command"
                                 CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "Auto_ID")%>' runat="server"
                                 ></asp:LinkButton>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Balance" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Right"  >
                             <ItemTemplate>
                                 <asp:Label id="lblBankName" runat="server" Text='<%# Eval("BalAmount")%>'></asp:Label>
                                 <asp:Label id="lblBankId" Visible="false" runat="server" Text='<%# Eval("BalAmount")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Adjust Amount" HeaderStyle-CssClass="center" ItemStyle-HorizontalAlign="Right" >
                             <ItemTemplate>
                                 <asp:Label id="lblBankCode" runat="server" Text='<%# Eval("AdjustAmount")%>'></asp:Label>
                             </ItemTemplate>
                            
                        </asp:TemplateField>
                      
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
                    <b >Remarks (if any)</b>
                    <asp:TextBox ID="txtremarks" Style="text-transform:uppercase" class="form-control text-label"    placeholder="Remarks"  
                    TextMode="MultiLine"   runat="server"   ></asp:TextBox>
                    </div>
                    </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
        </div>
                    <div class="row">
                    <div class="col-md-4" style="padding-left:16px;">
                    <div class="form-group" style="padding-top:15px">
                    <asp:Button ID="btnSave" OnClientClick="return ValidationSave();" class="btn btn-success btn-sm outline " runat="server"      
                    Text="Save"  />
                   
                    <a href="PDAdjustment.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
                    Clear
                    </a>
                    </div>
                    </div>
                    <div class="col-lg-8" >
                    <div class="form-group pull-right" style="margin-top:18px">
                    <a href="PDAdjustmentSummary.aspx" target="_blank"><b style="color:blue">Click here to view PD Adjustment Summary</b> </a>
                    </div>
                    </div>
                    </div>

                   

</div>
    
               
                         
                   
</fieldset>
    
</div>
                    <div class="col-md-3 col-xs-3 pull-right sidebar" style="padding-top:0px;">
                    <div menuitemname="Client Details" class="panel panel-sidebar" style="height:500px">
                    <div class="panel-heading">
                    <h3 class="panel-title" style="padding-bottom: 0px !important; color:blue"  >
                    <b >Today's Collection</b>   
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
                   <button class="btn btn-info" id="btnyes" data-dismiss="modal" onclick="OpenWOPrint()" runat="server"  aria-hidden="true">Yes </button>
                    <button class="btn btn-danger" id="btnno" data-dismiss="modal" runat="server" aria-hidden="true">No </button>
                                
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
                             <button class="btn btn-info btn-block " id="btnok" onserverclick="btnok_ServerClick" data-dismiss="modal" runat="server"  aria-hidden="true">
                                 OK 
                             </button>
                           <%--  <a href="BondGenerateReceipt.aspx" class="btn btn-info ">Yes</a>--%>

                             <%--<a href="PDAdjustment.aspx" class="btn btn-info btn-block ">Ok</a>--%>
                   
                    </div>
                    
                    </ContentTemplate>
             
                    </asp:UpdatePanel>
                    </div>
                    </div>
                  
</div>
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
       function ValidationSave() {
           var txtAssessment = document.getElementById('<%= txtAssessment.ClientID%>').value;

           var txtAmount = document.getElementById('<%= txtAmount.ClientID%>').value;

           document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
           document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-success disabled");
           var blResult = Boolean;
           blResult = true;

           document.getElementById('<%= txtAssessment.ClientID%>').style.borderColor = "Gainsboro";

           document.getElementById('<%= txtAmount.ClientID%>').style.borderColor = "Gainsboro";

           if (txtAssessment == "") {
               document.getElementById('<%= txtAssessment.ClientID%>').style.borderColor = "red"
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
        var popup;
        function OpenWOPrint() {
            var txttransNo = document.getElementById('<%= txttransNo.ClientID%>').value;
            var txtAssessment = document.getElementById('<%= txtAssessment.ClientID%>').value;

            var txtworkyear = document.getElementById('<%= txtworkyear.ClientID%>').value;

            var url = "../Depo/Report_Epic/PDAdjustmentPrint.aspx?AssessNo=" + txtAssessment + "&WorkYear=" + txtworkyear + "&TransID=" + txttransNo
            window.open(url);


}

</script>
    
    <script type="text/javascript">
        function ValidationShow() {
            var txtAssessment = document.getElementById('<%= txtAssessment.ClientID%>').value;
            var txtWorkYear = document.getElementById('<%= txtWorkYear.ClientID%>').value;


            var blResult = Boolean;
            blResult = true;

            document.getElementById('<%= txtAssessment.ClientID%>').style.borderColor = "Gainsboro";
            document.getElementById('<%= txtWorkYear.ClientID%>').style.borderColor = "Gainsboro";


            if (txtAssessment == "") {
                document.getElementById('<%= txtAssessment.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }
            if (txtWorkYear == "") {
                document.getElementById('<%= txtWorkYear.ClientID%>').style.borderColor = "red"
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
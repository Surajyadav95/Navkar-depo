<%@ Page Title="Depo | Eyard Receipt Summary" Language="VB" MasterPageFile="~/Depo/User.master" Culture="en-GB" AutoEventWireup="false" CodeFile="EyardReceiptSummary.aspx.vb" Inherits="Bond_BondReceiptSummary" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Bond | Eyard Receipt Summary </title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Eyard Receipt Summary  
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <Triggers>
                           
                                 <asp:Postbacktrigger  ControlID="btnShow" />
                                 <asp:Postbacktrigger  ControlID="ddlSearchList" />
                                 
                            
                         </Triggers>
<ContentTemplate>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body">
<div class="col-md-12 col-xs-12 pull-md-left main-content" >

                 

                                    
                                                
<div class="row">
<div class="col-md-5 col-xs-12">                                      
<div class="form-group date text-label">
Date                                           
<div class=" input-group">
<asp:TextBox type="text" class="form-control" runat="server" TextMode="Date"  id="txtFromDate" name="start"
placeholder="Start Date" required />
<span class="input-group-addon">TO</span>
<asp:TextBox  type="text" TextMode="Date" runat="server" class="form-control" name="end" id="txtToDate"
placeholder="End Date"/>
</div>
</div>                                       
</div>

<div class="col-sm-2 col-xs-12">
<div class="form-group">
 Search Criteria
<div class="input-group">
<asp:DropDownList ID="ddlSearchList" AutoPostBack="true" runat="server" CssClass="form-control">
<asp:ListItem Value="0">All</asp:ListItem>
<asp:ListItem Value="1">Assessment No.</asp:ListItem>
<asp:ListItem Value="2">NOC No.</asp:ListItem>
<asp:ListItem Value="3">Receipt No.</asp:ListItem>
<asp:ListItem Value="4">Receipt Type</asp:ListItem>

</asp:DropDownList>
</div>
</div>
</div>

<div class="col-md-2 col-xs-12" style="display:none;"  id="divReceipt" runat="server">
<div class="form-group text-label">
<b >Receipt No.</b>
<asp:TextBox ID="txtReceiptNo" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Enter Receipt No."
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-2 col-xs-12" style="display:none;"  id="divAssessment" runat="server">
<div class="form-group text-label">
<b >Assessment No.</b>
<asp:TextBox ID="txtAssessment" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Enter Assessment No."
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-2 col-xs-12" style="display:none;"  id="divNoc" runat="server">
<div class="form-group text-label">
<b >NOC No.</b>
<asp:TextBox ID="txtNoc" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Enter NOC No."
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-md-2 col-xs-12" style="display:none;"  id="divreceipttype" runat="server">
<div class="form-group text-label">
<b >Receipt Type</b>
<asp:DropDownList ID="ddlreceipttype" runat="server" CssClass="form-control">
<asp:ListItem Value="0">All</asp:ListItem>
<asp:ListItem Value="S">Single</asp:ListItem>
<asp:ListItem Value="M">Multiple</asp:ListItem>
</asp:DropDownList>
</div>
</div>
<div class="col-md-1 col-xs-12 pull-left" style="margin-top:18px" >   
<div class="form-group">
<asp:Button ID="btnShow" runat="server"
class="btn btn-primary btn-sm outline" Text="Show"></asp:Button>
 </div>
</div>
          <div class="col-md-2 col-xs-12" style="display:none;">
<div class="form-group text-label">

<asp:TextBox ID="txtassessno" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Enter NOC No."
runat="server"   ></asp:TextBox>
    <asp:TextBox ID="txtworkyear" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Enter NOC No."
runat="server"   ></asp:TextBox>
    <asp:TextBox ID="txtReceiptNoforPrint" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Enter NOC No."
runat="server"   ></asp:TextBox>
</div>
</div>                                     
</div>



<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<Triggers>
<asp:Postbacktrigger controlid="grdBondReceipt" />
</Triggers>
<ContentTemplate>
<div class="row">
<div class="col-lg-12 text-label " >
    <asp:Label ID="lblCancel" Visible="false" runat="server" Text=""></asp:Label>
<div class="table-responsive scrolling-table-container" >
<asp:GridView ID="grdBondReceipt" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" DataKeyNames="ReceiptNo" OnPageIndexChanging="OnPageIndexChanging" ShowHeaderWhenEmpty="true"   AllowPaging="true" PageSize="9" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>
    
<asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-primary btn-xs outline' OnClick="OnCancel" text="Cancel" 
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReceiptNo")%>' OnClientClick="return confirm('Are you sure to cancel this receipt?')" runat="server"/>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" />
</asp:TemplateField>

<asp:TemplateField>
<ItemTemplate>
    
<%--<a href='<%# "../Report_Bond/BondReceiptPrintNew.aspx?AssessNo=" & (DataBinder.Eval(Container.DataItem, "AssessNo")).ToString() & "&WorkYear=" & (DataBinder.Eval(Container.DataItem, "WorkYear")).ToString() & "&ReceiptNo=" & (DataBinder.Eval(Container.DataItem, "ReceiptNo")).ToString()%>'target="_blank" 
Class='btn btn-info btn-xs outline' 
>Print</a>--%>
    <asp:LinkButton ID="lnkprint" ControlStyle-CssClass='btn btn-info btn-xs outline' OnClick="lnkprint_Click" text="Print" 
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReceiptNo")%>' runat="server"/>
    <asp:LinkButton ID="lnkmail" ControlStyle-CssClass="btn btn-success btn-xs outline"  Text="Mail" OnClick="lnkmail_Click"
                                                            OnClientClick="return confirm('Are you sure to mail?')"
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ReceiptNo")%>' runat="server"
                                                            ></asp:LinkButton> 
 <asp:Label runat="server" ID="lblRcptType" Visible="false" Text='<%# Eval("Rcttype")%>'></asp:Label>
 <asp:Label runat="server" ID="lblAssessNo" Visible="false" Text='<%# Eval("AssessNo")%>'></asp:Label>
 <asp:Label runat="server" ID="lblWorkYear" Visible="false" Text='<%# Eval("WorkYear")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="Center" Width="130px"  />
</asp:TemplateField>

<asp:BoundField DataField="Srno" HeaderText="Sr No."></asp:BoundField>
<asp:BoundField DataField="ReceiptNo" HeaderText="Receipt No."></asp:BoundField>
<asp:BoundField DataField="ReceiptDate" HeaderText="Receipt Date" ></asp:BoundField>
<asp:BoundField DataField="AssessNo" HeaderText="Assessment No." ></asp:BoundField>
<asp:BoundField DataField="NOCNo" HeaderText="Noc No." ></asp:BoundField>
<asp:BoundField DataField="invoiceType" HeaderText="Invoice Type" ></asp:BoundField>
<asp:BoundField DataField="Pay_From" HeaderText="Pay Name "  ></asp:BoundField>
<asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
<asp:BoundField DataField="CHAName" HeaderText="CHA Name"></asp:BoundField>
<asp:BoundField DataField="agentName" HeaderText="Customer Name"></asp:BoundField>
<asp:BoundField DataField="ReceiptAmount" HeaderText="Receipt Amount" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
<asp:BoundField DataField="UserName" HeaderText="Prepared By"></asp:BoundField>
<asp:BoundField DataField="ReceiptType" HeaderText="Receipt Type"></asp:BoundField>


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
                 
</ContentTemplate>
</asp:UpdatePanel>
     <asp:Panel ID="pnlPerson" Visible="false" runat="server" font-family="Segoe UI">
       <rsweb:ReportViewer ID="ReportViewer1" Width="1000px" Height="600px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
       <LocalReport ReportPath="Hazira\Report_Epic\BondAssessmentPrint.rdlc" >
        </LocalReport>
        </rsweb:ReportViewer>  
        </asp:Panel>
</div>
         
</div>
      
    <div class="modal fade" id="myModal" role="dialog"  aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm" style="width:600px">
                <asp:UpdatePanel ID="upModalCancel" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <Triggers>
                             <asp:Postbacktrigger controlid="btnsave" />
                              
                               
                         </Triggers>
                    <ContentTemplate>
                        
 <div>
                        <div class="modal-content" >
                            <div class="modal-header">
                                <center>
                                   
                                        <asp:Label ID="lblModifyTitle" CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
                              <asp:TextBox ID="txtremarks" Style="text-transform:uppercase" Rows="4" class="form-control text-label"    placeholder="Remarks"  
                    TextMode="MultiLine"   runat="server"   ></asp:TextBox>
                                   
                                </center>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnsave" class="btn btn-success pull-right"  
                                                runat="server" Text="Save" OnClientClick="return ValidationSave();" />
                               <%-- OnClientClick="return confirm('Are you sure to cancel this receipt?')"" --%>
                            </div>
                        </div>
                    </ContentTemplate>
                   <Triggers>
        
                </Triggers>
                </asp:UpdatePanel>
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
                   
                                
                    </div>
                    </div>
                    
                    </ContentTemplate>
             
                    </asp:UpdatePanel>
                    </div>
                    </div>

    <script type="text/javascript">
       function ValidationSave()
       {
           

           var txtremarks = document.getElementById('<%= txtremarks.ClientID%>').value;
          
           var blResult = Boolean;
           blResult = true;

           document.getElementById('<%= txtremarks.ClientID%>').style.borderColor = "Gainsboro";
          
           if (txtremarks == "") {
               document.getElementById('<%= txtremarks.ClientID%>').style.borderColor = "red"
               blResult = blResult && false;
           }

           if (blResult == false) {
               alert('Please fill the required fields!');
              
           }
           return blResult;
        }
</script>
    <script type="text/javascript">
        var popup;
        function OpenWOPrint() {
            var txtassessno = document.getElementById('<%= txtassessno.ClientID%>').value;
            var txtworkyear = document.getElementById('<%= txtworkyear.ClientID%>').value;
            var txtReceiptNoforPrint = document.getElementById('<%= txtReceiptNoforPrint.ClientID%>').value;

            var url = "../Depo/Report_Epic/EyardReceiptPrintNew.aspx?AssessNo=" + txtassessno + "&WorkYear=" + txtworkyear + "&ReceiptNo=" + txtReceiptNoforPrint
            window.open(url);

}

</script>
    <script type="text/javascript">
        var popup;
        function OpenMultiplePrint() {
            
            var txtReceiptNoforPrint = document.getElementById('<%= txtReceiptNoforPrint.ClientID%>').value;

            var url = "../Report_Bond/MultipleInvoice.aspx?ReceiptNo=" + txtReceiptNoforPrint
            window.open(url);

        }

</script>
</asp:Content>

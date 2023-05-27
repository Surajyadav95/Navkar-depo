<%@ Page Title="Depo | Credit Note Summary" Language="VB" MasterPageFile="../Depo/User.master" AutoEventWireup="false"
CodeFile="CreditNoteSummary.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title> Depo | Credit Note Details </title>
</head>
<style>
.text-center{
text-align:center
}
</style>
<div class="page-container">
<div class="pageheader">
<h3> Credit Note Details  
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
 
<div class="col-md-2 col-xs-12" style="margin-right:10px">
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">All</asp:ListItem> 
<asp:ListItem Value="1">Assess No</asp:ListItem>
    <asp:ListItem Value="2">Credit Note No</asp:ListItem>
<asp:ListItem Value="3">GST Name</asp:ListItem> 

</asp:DropDownList>
                                               
</div>

</div>
<div class="col-md-2 col-xs-12" style="display:none;"  id="divassessno" runat="server">
<div class="form-group text-label">
<b >Search Text</b>
<asp:TextBox ID="txtAssessno" Style="text-transform:uppercase" class="form-control text-label "  placeholder="Search Text"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-4 col-xs-12" style="display: none;" id="divGSTParty" runat="server">
                                <div class="form-group text-label">
                                    GST Party
                                    <asp:DropDownList ID="ddlGSTParty" runat="server" Style="text-transform: uppercase" class="form-control text-label">
                                    </asp:DropDownList>
                                </div>
                            </div>                                                    
                             
<asp:Label ID="lblFD_ID" Visible="false" runat="server" Text=""></asp:Label>

<div class="col-md-1  col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnsearch" runat="server" 
class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click" ></asp:Button>
</div>              
</div>
                                          

</div>
                                 

                     
<div class="row">

<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:28px;margin-right:0px;">
<asp:GridView ID="grdSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" DataKeyNames="Work Year" OnPageIndexChanging="OnPageIndexChanging"  AllowPaging="true" PageSize="6" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
<asp:TemplateField>
<ItemTemplate>

<a  href='<%# "../Depo/Report_Epic/CreditNotePrint.aspx?CreditNoteNo=" & (DataBinder.Eval(Container.DataItem, "Credit Note No")).ToString() & "&WorkYear=" & (DataBinder.Eval(Container.DataItem, "Work Year")).ToString()%>'target="_blank" 
Class='btn btn-primary btn-xs outline' 
>Print</a>
    <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Cancel" OnClick="lnkCancel_Click"
                                                            
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Credit Note No")%>' runat="server" 
                                                            ></asp:LinkButton>
                                                          
                                                          
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="130px" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Credit Note No" HeaderStyle-CssClass="text-center">
<ItemTemplate>

<asp:Label runat="server" ID="lblassessNo" Text='<%#Eval("Credit Note No")%>'></asp:Label>

</ItemTemplate>

<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
    <asp:TemplateField HeaderText="Work Year" HeaderStyle-CssClass="text-center">
<ItemTemplate>
<asp:Label runat="server" ID="lblworkyear" Text='<%#Eval("Work Year")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
<asp:BoundField  DataField="Credit Date" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" HeaderText="Credit Date"></asp:BoundField>
<asp:BoundField DataField="Assess No" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"  HeaderText="Assess No"  ></asp:BoundField>
<asp:BoundField DataField="Party Name" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center"  HeaderText="Party Name"  ></asp:BoundField>
<asp:BoundField DataField="Grand Total" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Right" HeaderText="Grand Total"  ></asp:BoundField>
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


      
</asp:Content>

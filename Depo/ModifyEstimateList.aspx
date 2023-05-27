<%@ Page Title="Epic" Language="VB" EnableEventValidation="false" MasterPageFile="~/Depo/PopUp.master" AutoEventWireup="false" CodeFile="ModifyEstimateList.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" >
function callparentfunction() {
 
    window.opener.document.getElementById("ContentPlaceHolder1_btnEstimate").click();

self.close();
}
</script>
<div class="container" style="background-color: white;margin-top:-50px">

<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Estimate List<small class="pull-right" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>
<div class="row ">
         
                         
<div class="col-sm-6 col-xs-6">
<div class="form-group text-label">
<b  >Search</b>
<asp:TextBox ID="txtsearch" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Search"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-2 col-xs-2">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnsearch" class="btn btn-primary " runat="server" OnClick="btnsearch_Click"  
Text="Search"  />
</div>
                                              
                                      
</div>
</div>


<div class="row">
                   

<div class="row">
<div class="col-sm-12 text-label ">
<div class="table-responsive scrolling-table-container" style="margin-left: 0px; margin-right: 0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">

<Columns>

<asp:TemplateField>
<ItemTemplate>

<%--   <a class="btn btn-success btn-xs outline" target="_blank"
href="Indent.aspx?IndentCode=<%# DataBinder.Eval(Container.DataItem, "Code")%>">Open</a>  --%>  
<asp:LinkButton ID="lnkEdit" ControlStyle-CssClass='btn btn-success btn-xs outline' Text="Select"
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ESTIMATE_ID")%>' runat="server"
OnClick="btnSave_Click" ></asp:LinkButton>

</ItemTemplate>
<ItemStyle HorizontalAlign="Center" Width="60px" />
</asp:TemplateField>
                       
<asp:BoundField DataField="#"   HeaderText="#" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="ESTIMATE_ID"   HeaderText="Estimate ID" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Estimate_Date"   HeaderText="Estimate Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="ContainerNo"   HeaderText="Container No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Size"   HeaderText="Size" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="ContainerType"   HeaderText="Container Type" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="SLName"   HeaderText="Shipline" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>

                                                 

</Columns>

</asp:GridView>
</div>
</div>
</div>
                        
</div>

                                     
                 

</div>
</div>
</div>
</asp:Content>



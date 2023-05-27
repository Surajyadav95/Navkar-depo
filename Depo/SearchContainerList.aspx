<%@ Page Title="Epic" Language="VB" EnableEventValidation="false" MasterPageFile="~/Depo/PopUp.master" AutoEventWireup="false" CodeFile="SearchContainerList.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
    window.opener.document.getElementById("ContentPlaceHolder1_btnSearchbyContainer").click();
    self.close();
}
</script>
    <style>
        .text-center{
            text-align:center
        }
        .scrolling-table-container{
            height:300px;
            overflow:auto
        }
    </style>
<div class="container" style="background-color: white">
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Container List <small class="pull-right" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>                                 
<div class="row">
<div class="col-sm-6 col-xs-8">
<div class="form-group text-label">
<b>Search</b>
<asp:TextBox ID="txtSearch" Style="text-transform:uppercase" MaxLength="150" class="form-control text-label"  placeholder="Search by Container No here"
runat="server"   ></asp:TextBox>

</div>
</div>

            
<div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnShow" class="btn btn-primary btn-sm" runat="server" OnClick="btnShow_Click"  
Text="Show"  />
</div>                                                                                  
</div>
</div>

             

<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>

<div class="row">                  

<div class="col-md-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container" style="margin-right:22px">
<asp:GridView ID="grdLotList" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">

<Columns>
<asp:TemplateField>
<ItemTemplate>
    <asp:LinkButton ID="lnkselect" ControlStyle-CssClass='btn btn-primary btn-sm outline' OnClick="btnSave_Click"                                                             
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Container No")%>' runat="server" 
                                                            ><i class="fa fa-check" aria-hidden="true"></i></asp:LinkButton>
<%--<asp:Label runat="server" ID="lblcode" Text='<%#Eval("LOT_NO")%>' Visible="false"></asp:Label>--%>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" Width="60px" />
</asp:TemplateField>                                  
<asp:BoundField DataField="Entry ID" HeaderText="Entry ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="Container No" HeaderText="Container No" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="IN Date" HeaderText="In Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>

</Columns>

</asp:GridView>
</div>
</div>
</div>                        
             
</div>
</div>
</div>

</asp:Content>



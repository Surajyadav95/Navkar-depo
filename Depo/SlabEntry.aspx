<%@ Page Title="Epic" Language="VB" EnableEventValidation="false" MasterPageFile="~/Depo/PopUp.master" AutoEventWireup="false" CodeFile="SlabEntry.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
window.opener.document.getElementById("ContentPlaceHolder1_btnSlab").click();
    self.close();
}
</script>
    <style>
        .text-center{
            text-align:center
        }
        .scrolling-table-container{
            height:200px;
            overflow:auto
        }
    </style>
<div class="container" style="background-color: white;margin-top:-60px">    
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Slab Entry<small class="pull-right" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>                                 
<div class="row">
<div class="col-sm-4 col-xs-4">
<div class="form-group text-label">
<b>Slab ID</b>
<asp:TextBox ID="txtslabid" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="NEW"
runat="server"   ></asp:TextBox>
</div>
</div>
    <div class="col-sm-4 col-xs-4">
<div class="form-group text-label">
<b >Slab Type</b>
<asp:DropDownList  ID="ddlSlabtype" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 
    <asp:ListItem Value="Days">Days</asp:ListItem>
    <asp:ListItem Value="Weight">Weight</asp:ListItem>
    <asp:ListItem Value="Fixed">Fixed</asp:ListItem>
</asp:DropDownList>
</div>
</div> 
<div class="col-sm-4 col-xs-4">
<div class="form-group text-label">
<b >Size</b>
<asp:DropDownList  ID="ddlSize" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 
<asp:ListItem Value="20">20</asp:ListItem>
<asp:ListItem Value="40">40</asp:ListItem>
<asp:ListItem Value="45">45</asp:ListItem>
</asp:DropDownList>
</div>
</div>
</div>  
<div class="row">
<div class="col-sm-4 col-xs-4">
<div class="form-group text-label">
<b>Range From</b>
<asp:TextBox ID="txtFrom" Style="text-transform:uppercase" class="form-control text-label"  placeholder="From"
runat="server"   ></asp:TextBox>
</div>
</div>
<div class="col-sm-4 col-xs-4">
<div class="form-group text-label">
<b>To</b>
<asp:TextBox ID="txtTo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="To"
runat="server"   ></asp:TextBox>
</div>
</div>
</div> 
    <div class="row">
         <div class="col-sm-4 col-xs-4">
<div class="form-group text-label">
<b>Value</b>
<asp:TextBox ID="txtValue" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Value"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnAdd" class="btn btn-primary btn-sm" runat="server" OnClientClick="return ValidationAdd()" OnClick="btnAdd_Click"  
Text="Add"  />
</div>                                                                                  
</div> 
    </div> 
          

<%--<asp:Label ID="lblWHID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblwhname" Visible="false" runat="server" Text=""></asp:Label>--%>

<div class="row">                  

<div class="col-md-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container" style="margin-right:22px">
<asp:GridView ID="grdSlabList" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">

<Columns>
<asp:TemplateField>
<ItemTemplate>
    <asp:LinkButton ID="lnkDelete" ControlStyle-CssClass='btn btn-danger btn-sm outline' OnClick="btnSave_Click"                                                             
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AUTO_ID")%>' runat="server" 
                                                            ><i class="fa fa-remove" aria-hidden="true"></i></asp:LinkButton>
<asp:Label runat="server" ID="lblAutoID" Text='<%#Eval("AUTO_ID")%>' Visible="false"></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Center" Width="60px" />
</asp:TemplateField>                                  
<asp:BoundField DataField="SLAB_ON" HeaderText="Slab On" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="FROM_SLAB" HeaderText="Range From" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="TO_SLAB" HeaderText="To" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>
<asp:BoundField DataField="VALUE" HeaderText="Value" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"></asp:BoundField>



</Columns>

</asp:GridView>
</div>
</div>
</div>                        
        <div class="row">
            <div class="col-sm-1 col-xs-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm" runat="server" OnClick="btnSave_Click"  
Text="Save"  />
</div>                                                                                  
</div> 
        </div>     
</div>
    <div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-xs">
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

<button class="btn btn-info " ID="Button1" data-dismiss="modal" runat="server" onserverclick="btnOk_ServerClick" aria-hidden="true">
                                 Ok 
                             </button>

</div>
</div>

</ContentTemplate>

</asp:UpdatePanel>
</div>
</div>
</div>
</div>
    <script type="text/javascript">
        function ValidationAdd() {
            var txtFrom = document.getElementById('<%= txtFrom.ClientID%>').value;
            var txtTo = document.getElementById('<%= txtTo.ClientID%>').value;
            var txtValue = document.getElementById('<%= txtValue.ClientID%>').value;

            var blResult = Boolean;
            blResult = true;

            if (txtfrom == "") {
                document.getElementById('<%= txtfrom.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtTo == "") {
                document.getElementById('<%= txtTo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtValue == "") {
                document.getElementById('<%= txtValue.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
        }
    </script>
</asp:Content>



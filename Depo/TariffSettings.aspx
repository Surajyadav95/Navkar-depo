<%@ Page Title="Depo | Tariff Settings " Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="TariffSettings.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<style type="text/css">
.center1 {
text-align: center;
}
.scrollable-container{
    height:155px;
    overflow:auto
}
.scrolling-table-container1{
    height:100px;
    overflow:auto
}
</style>
<head>
<title>Depo | Tariff Settings</title>
</head>
<div class="page-container">
<div class="pageheader">

<h3>
<i class="glyphicon glyphicon-transfer"></i>Tariff Settings 
</h3>

</div>

<div id="page-content">



<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">

<div class="col-md-12 pull-left main-content">
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">

<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true">
 
<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b>Tariff ID</b>
<asp:DropDownList ID="ddlTariff" AutoPostBack="true" OnSelectedIndexChanged="ddlTariff_SelectedIndexChanged" Style="text-transform: uppercase;" runat="server" class="form-control text-label">

</asp:DropDownList>
</div>
</div>
       <div class="col-md-6 col-xs-12">                                      
<div class="form-group text-label">
<b>Tariff Description</b>                                           
<asp:TextBox ID="txtTariffDesc" ReadOnly="true" runat="server" Class="form-control text-label"></asp:TextBox>

</div>
</div>                                             
    </div>
        
<div class="row">
<div class="col-md-3 col-xs-12">                                      
<div class="form-group text-label">
<b>Effective From</b>                                           
<asp:TextBox ID="txtfrom" placeholder="dd-MM-yyyy" TextMode="DateTimeLocal"   runat="server"   Class="   form-control text-label"></asp:TextBox>

</div>
</div>

<div class="col-md-3 col-xs-12" ">                                      
<div class="form-group text-label">
<b >Effective UpTo</b>
<asp:TextBox ID="txtUpTo"  placeholder="dd-MM-yyyy" TextMode="DateTimeLocal"  runat="server"   Class="  form-control text-label"></asp:TextBox>
</div>                        
</div>                                                 
</div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div class="row">
        <div class="col-md-3 col-xs-12">
    <div class="form-group">
<b>Invoice Type</b>
<asp:DropDownList ID="ddlInvoiceType" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:DropDownList>        
    </div>
</div>
    </div>
<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Account Head</b>
<asp:DropDownList ID="ddlAccount"   Style="text-transform: uppercase;" runat="server"    class="form-control text-label">

</asp:DropDownList> 
</div>
</div>
<div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Size</b>
<asp:DropDownList  ID="ddlsize" Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddlsize_SelectedIndexChanged" runat="server" class="form-control text-label">   
<asp:ListItem Value="20">20</asp:ListItem>
<asp:ListItem Value="40">40</asp:ListItem>
<asp:ListItem Value="45">45</asp:ListItem>
                                         
</asp:DropDownList>
</div>
</div>
    <div class="col-sm-2 col-xs-12">
        <b >Type</b>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:CheckBox runat="server" ID="chkSelectAll" AutoPostBack="true" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged" />
<div class="form-group scrollable-container" style="border:1px solid gainsboro;border-radius:6px">

<asp:CheckBoxList runat="server" ID="chkType" OnSelectedIndexChanged="chkType_SelectedIndexChanged" AutoPostBack="true">
</asp:CheckBoxList>
</div>
</div>
<div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Charges Based On</b>
<asp:DropDownList  ID="ddlCharges" Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddlCharges_SelectedIndexChanged" runat="server" class="form-control text-label"> 
    <asp:ListItem Value="Fixed">Fixed</asp:ListItem>
    <asp:ListItem Value="Days">Days</asp:ListItem>
    <asp:ListItem Value="Weight">Weight</asp:ListItem>
</asp:DropDownList>
</div>
</div>
    <div class="col-sm-2 col-xs-12" id="divSlab" runat="server" style="display:none">
<div class="form-group text-label">
<b >Slab ID</b>
<asp:DropDownList  ID="ddlSlab" Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddlSlab_SelectedIndexChanged" runat="server" class="form-control text-label"> 
</asp:DropDownList>
</div>
</div>  
    <div class="col-sm-1" style="display:none">
        <asp:Button ID="btnSlab" runat="server" OnClick="btnSlab_Click" />
    </div>
    
     <div class="col-sm-1" id="divSlabAdd" runat="server" style="display:none">
                                     <div class="form-group " style="padding-top:20px;" >
                                         <asp:LinkButton class="btn btn-warning btn-sm outline" ID="lnkAddSlab" OnClientClick="return OpenSlabEntry()" runat="server">Add Slabs</asp:LinkButton>
                                                                                 
                                     </div>
                                 </div>    
        <div class="col-sm-2 col-xs-12" id="divFixedAmount" runat="server" style="display:none">
<div class="form-group text-label">
<b >Fixed Amount</b>
<asp:TextBox ID="txtFixedAmount" Style="text-transform:uppercase" class="form-control text-label" placeholder="Fixed Amount"  
runat="server"></asp:TextBox>
</div>
</div>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="row">
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Free Days</b>
<asp:TextBox ID="txtFreeDays" Style="text-transform:uppercase" class="form-control text-label" placeholder="Free Days"  
runat="server"></asp:TextBox>
</div>
</div>
<div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Tax Code</b>
<asp:DropDownList  ID="ddlTaxCode" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 
</asp:DropDownList>
</div>
</div>   
    <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >From Location</b>
<asp:DropDownList  ID="ddlFromLocation" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 
</asp:DropDownList>
</div>
</div>
    <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >To Location</b>
<asp:DropDownList  ID="ddlToLocation" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 
</asp:DropDownList>
</div>
</div>                               
<div class="col-sm-1 col-xs-12">                                                        
<div class="form-group" style="padding-top: 25px;">
<asp:CheckBox ID="chkIstax" runat="server"  Checked="true"  />
<asp:HiddenField ID="hdltax" runat="server" Value="0" />
<asp:Label ID="TaxLabel" runat="server" AssociatedControlID="chkIstax" CssClass="inline"></asp:Label>
<b>Is Tax?</b>
</div>
</div>

<div class="col-sm-2">
<div class="form-group" style="padding-top: 20px">
<asp:Button ID="Button1" class="btn btn-info btn-sm" runat="server" OnClick="Button1_Click"  
Text="Add" OnClientClick="return Validationsearch()"  />
</div>
</div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <div class="col-sm-5 text-label" runat="server" id="divSlabDets" style="display:none">
        <div class="table-responsive scrolling-table-container1">
<asp:GridView ID="grdSlabDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >

<Columns>
    <asp:BoundField DataField="slabID" HeaderText="Slab ID" />
    <asp:BoundField DataField="Size" HeaderText="Size" />
    <asp:BoundField DataField="slabon" HeaderText="Slab On" />
    <asp:BoundField DataField="fromslab" HeaderText="From" />
    <asp:BoundField DataField="toslab" HeaderText="To" />
    <asp:BoundField DataField="value" HeaderText="Value" />                                               
</Columns>

</asp:GridView>
</div>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
    

<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="row">
<div class="col-lg-12 text-label ">
<div class="table-responsive scrolling-table-container" style="margin-left:5px;margin-right:5px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>

<asp:TemplateField>
<ItemTemplate>                                                     
                                                            
<asp:LinkButton ID="lnkDelete"  ControlStyle-CssClass='btn btn-danger btn-xs outline'                                                         
CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Autotemp")%>' runat="server"  OnClick="lnkDelete_Click" 
><i class="fa fa-remove"></i></asp:LinkButton>

   
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="50px" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Tariff ID" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lbltariffID" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tariffID")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Invoice Type" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblInvoicetype" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "bondtype")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="From" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lbleffectivefrom" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "effectivefrom")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Up To" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lbleffectiveupto" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "effectiveupto")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Account Head" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblAccountHead" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AccountHead")%>'></asp:Label>
<asp:Label ID="lblAccountID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AccountID")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Size" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblsize" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "size")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Type" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblType" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TYPE")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Charges" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblchargesBased" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "chargesBased")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Slab ID" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblSlabID" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SlabID")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
<asp:TemplateField HeaderText="Fixed Amount" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblFixedAmount" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FixedAmount")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Free Days" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblFreeDays" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Free_Days")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Tax Code" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblTaxCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TaxID")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
<asp:TemplateField HeaderText="Is Tax" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>

<asp:Label ID="lblIsTax" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IsTax")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="From" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>
<asp:Label ID="lblFrom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "From")%>'></asp:Label>
    <asp:Label ID="lblFromID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FromID")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="center" />
</asp:TemplateField>
    <asp:TemplateField HeaderText="To" Visible="true"   HeaderStyle-CssClass="center1">
<ItemTemplate>
<asp:Label ID="lblTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "To")%>'></asp:Label>
    <asp:Label ID="lblToID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ToID")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="center" />
</asp:TemplateField>
                                                 
</Columns>

</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>

    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top: 15px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm" runat="server" OnClick="btnSave_Click"
Text="Save" OnClientClick="return Validationseve()" />
</div>


</div>

<div class="col-sm-1" style="padding-left: 14px;">
<div class="form-group" style="padding-top: 15px">

<a href="TariffSettings.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm">Clear
</a>

</div>


</div>


</div>
</asp:Panel>
</div>
</div>

<asp:Label ID="lblcode" Visible="false" runat="server" Text=""></asp:Label>
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

<a href="TariffSettings.aspx" class="btn btn-info btn-block">OK</a>

</div>
</div>

</ContentTemplate>

</asp:UpdatePanel>
</div>
</div>

</fieldset>

</div>
</div>

</div>

</div>


</div>


</div>

<script type="text/javascript">
    function Validationseve() {
        var ddlTariff = document.getElementById('<%= ddlTariff.ClientID%>').value;

        var ddlInvoiceType = document.getElementById('<%= ddlInvoiceType.ClientID%>').value;
        var txtfrom = document.getElementById('<%= txtfrom.ClientID%>').value;
        var txtUpTo = document.getElementById('<%= txtUpTo.ClientID%>').value;

        document.getElementById('<%= btnSave.ClientID%>').value = "Please Wait...";
        document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary btn-sm disabled");
        var blResult = Boolean;
        blResult = true;
        if (txtfrom == "") {
            document.getElementById('<%= txtfrom.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;
        }
        if (txtUpTo == "") {
            document.getElementById('<%= txtUpTo.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;
        }
        if (ddlTariff == 0) {
            document.getElementById('<%= ddlTariff.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;
        }
        if (ddlInvoiceType == 0) {
            document.getElementById('<%= ddlInvoiceType.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;
        }
        if (blResult == false) {
            alert('Please fill the required fields!');
            document.getElementById('<%= btnSave.ClientID%>').value = "Save";
            document.getElementById('<%= btnSave.ClientID%>').setAttribute("class", "btn btn-primary btn-sm");
        }
        return blResult;
    }
</script>     
    
<script type="text/javascript">
function Validationsearch() {
var ddlAccount = document.getElementById('<%= ddlAccount.ClientID%>').value;
    var txtFixedAmount = document.getElementById('<%= txtFixedAmount.ClientID%>').value;
    var txtFreeDays = document.getElementById('<%= txtFreeDays.ClientID%>').value;

    var ddlTariff = document.getElementById('<%= ddlTariff.ClientID%>').value;

    var ddlInvoiceType = document.getElementById('<%= ddlInvoiceType.ClientID%>').value;
    var txtfrom = document.getElementById('<%= txtfrom.ClientID%>').value;
    var txtUpTo = document.getElementById('<%= txtUpTo.ClientID%>').value;


    document.getElementById('<%= Button1.ClientID%>').value = "Please Wait...";
    document.getElementById('<%= Button1.ClientID%>').setAttribute("class", "btn btn-info btn-sm disabled");
var blResult = Boolean;
blResult = true;

if (ddlAccount == 0) {
document.getElementById('<%= ddlAccount.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}

    
    if (txtFreeDays == "") {
        document.getElementById('<%= txtFreeDays.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (txtfrom == "") {
        document.getElementById('<%= txtfrom.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (txtUpTo == "") {
        document.getElementById('<%= txtUpTo.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (ddlTariff == 0) {
document.getElementById('<%= ddlTariff.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    if (ddlInvoiceType == 0) {
        document.getElementById('<%= ddlInvoiceType.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

if (blResult == false) {
    alert('Please fill the required fields!');
    document.getElementById('<%= Button1.ClientID%>').value = "Add";
    document.getElementById('<%= Button1.ClientID%>').setAttribute("class", "btn btn-info btn-sm");
}
return blResult;
}
</script>
    <script>
        function OpenSlabEntry() {
            var url = "SlabEntry.aspx"
            popup = window.open(url, "Popup", "top=100,left=400,width=700,height=550");
            popup.focus();
        }
    </script>
</asp:Content>

<%@ Page Title="Domestic | Job Order" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="ImportJobOrder.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
<head>
<title>Domestic | Job Order</title>       
</head>
    <style>
        .required {
         content:"*";color:red;
         font:bold;
         font-size: small; 
         padding-bottom:-10px;
         vertical-align:middle;
    }
    </style>
<div class="page-container">
<div class="pageheader">            
<h3>
<i class="glyphicon glyphicon-transfer"></i>  Job Order 
</h3>           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">
<div class="row">                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
           
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-sm-4 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >JO No</b>
<asp:TextBox ID="txtJONo" Style="text-transform: uppercase;" ReadOnly  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >JO Date</b>
<asp:TextBox ID="txtJODate" Style="text-transform:uppercase" class="form-control text-label" TextMode="DateTimeLocal"
runat="server"   ></asp:TextBox>
</div>
</div>
   <asp:Panel runat="server" ID="pnlJOType" Enabled="true">
    <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >JO Type</b>
<asp:DropDownList ID="ddlJOType" Style="text-transform: uppercase;" runat="server"  class="form-control text-label" onchange="JOTypeChange();">
     <asp:ListItem Value="0">--Select--</asp:ListItem>
    <asp:ListItem Value="1">Cargo</asp:ListItem>
    <asp:ListItem Value="2">Container</asp:ListItem>               
</asp:DropDownList> 
</div>
</div>
       </asp:Panel>
</div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional"><ContentTemplate>
<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >IGM No</b>
<asp:TextBox ID="txtIGMNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="IGM No" AutoPostBack="true" OnTextChanged="txtIGMNo_TextChanged"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Item No</b>
    <asp:TextBox ID="txtItemNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Item No" AutoPostBack="true" OnTextChanged="txtItemNo_TextChanged"
runat="server"   ></asp:TextBox>
    <asp:Label runat="server" ID="lblJONo" Visible="false"></asp:Label>
</div>
</div>
    
</div>

<div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >Customer</b>
<asp:DropDownList ID="ddlCustomer" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
                                 

<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
    <%--<span class="required">*</span>--%>
<b  >CHA</b>
<asp:DropDownList ID="ddlCHA" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
                                

<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
    <%--<span class="required">*</span>--%>
<b  >Importer</b>
<asp:DropDownList ID="ddlImporter" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
         <div class="Col-md-2" style="display:none">
             <asp:TextBox runat="server" ID="txtOpenContainerNo"></asp:TextBox>
         </div>                       
</div>  
        </ContentTemplate></asp:UpdatePanel> 
    <div class="row">
        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional"><ContentTemplate>
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >BE No</b>
    <asp:TextBox ID="txtBENo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="BE No"
runat="server"   ></asp:TextBox>
</div>
</div>
            </ContentTemplate></asp:UpdatePanel>
       <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >BE Date</b>
    <asp:TextBox ID="txtBEDate" Style="text-transform:uppercase" class="form-control text-label" TextMode="DateTimeLocal"
runat="server"   ></asp:TextBox>
</div>
</div>                         
        <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional"><ContentTemplate>
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >BE Adress</b>

    <asp:TextBox ID="txtBEAdress" Style="text-transform:uppercase" class="form-control text-label" TextMode="MultiLine" Rows="2"  placeholder="BE Adress"
runat="server"   ></asp:TextBox>
</div>
</div>
         </ContentTemplate></asp:UpdatePanel>                       
</div>  
    <div class="row">
<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >PO No</b>
<asp:TextBox ID="txtPONo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="PO No"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Lot No</b>
    <asp:TextBox ID="txtLotNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Lot No"
runat="server"   ></asp:TextBox>

</div>
</div>
        <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >Ref JO No/BL No</b>
    <asp:TextBox ID="txtRefJONo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Ref JO No"
runat="server"   ></asp:TextBox>

</div>
</div>
</div> 
    <div class="row">
        <div class="col-md-8 col-xs-12">
<div class="form-group text-label">
<b  >Remarks</b>
    <asp:TextBox ID="txtRemarks" Style="text-transform:uppercase" class="form-control text-label" MaxLength="150"  placeholder="Remarks" TextMode="MultiLine" Rows="2"
runat="server"   ></asp:TextBox>

</div>
</div>
    </div>                  
</asp:Panel>
                        
</div>
    </div>
    <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
    <div class="panel-body">
<asp:Panel ID="Panel1" runat="server" Enabled="true">
    <div class="row">
<div class="col-md-2 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >Container No</b>
<asp:TextBox ID="txtContainerNo" MaxLength="11" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-sm-1 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return gstsearch();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
<div class="col-md-1 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >Size</b>
<asp:DropDownList ID="ddlSize" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >Cargo Type</b>
<asp:DropDownList ID="ddlCargoType" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >Container Type</b>
<asp:DropDownList ID="ddlContainerType" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
    <%--<span class="required">*</span>--%>
<b  >ISO Code</b>
<asp:DropDownList ID="ddlISOCode" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
        </div>
       <div class="row">
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >From</b>
<asp:DropDownList ID="ddlFrom" Style="text-transform: uppercase;" runat="server"  class="form-control text-label" onchange="FromChange();">
                                         
</asp:DropDownList> 
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >To</b>
<asp:DropDownList ID="ddlTo" Style="text-transform: uppercase;" runat="server"  class="form-control text-label" onchange="ToChange();">
                                         
</asp:DropDownList> 
</div>
</div>

        <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Line</b>
<asp:DropDownList ID="ddlLine" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >PKGS</b>
<asp:TextBox ID="txtPackages" Style="text-transform:uppercase" class="form-control text-label"  placeholder="pkgs"
runat="server"   ></asp:TextBox>
</div>
</div>
           </div>
           <div class="row">
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >Pkgs Type</b>
<asp:DropDownList ID="ddlpkgsType" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
                                         
</asp:DropDownList> 
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >Weight</b>
<asp:TextBox ID="txtWeight" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Weight"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Class</b>
<asp:TextBox ID="txtClass" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Class"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >UN No</b>
<asp:TextBox ID="txtUNNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="UN No"
runat="server"   ></asp:TextBox>
</div>
</div>
            </div>
        <div class="row">
            
        <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Do Valid Date</b>
<asp:TextBox ID="txtDoValidDate" Style="text-transform:uppercase" class="form-control text-label"  TextMode="DateTimeLocal"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-5 col-xs-12">
<div class="form-group text-label">
    <span class="required">*</span>
<b  >Cargo Description</b>
<asp:TextBox ID="txtCargoDescription" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Cargo Description"
runat="server"   ></asp:TextBox>
</div>
</div>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
        </ContentTemplate>
</asp:UpdatePanel>--%>
            <div class="col-md-1 col-xs-12">
                    <div class="form-group pull-left" style="padding-top:20px" >
                    <asp:Button id="btnAddClick" runat="server" Text="Add"  
                    class="btn btn-info btn-sm outline" OnClientClick="return btnAddClick();" OnClick="btnAddClick_Click"/>

                    </div>              
                    </div>


</div>
    <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
    <div class="row">
     
<div class="col-lg-12 text-label " >
<div class="table-responsive scrolling-table-container">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" EnableModelValidation="true">
<Columns>


<asp:TemplateField HeaderText="Container No" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblContainerNo" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ContainerNo")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>



<asp:TemplateField HeaderText="Size" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblSize" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Size")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Cargo Type" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblType" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Type")%>'></asp:Label>
<asp:Label ID="lblTypeID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TypeID")%>'></asp:Label>


</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="Container Type" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblCType" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CType")%>'></asp:Label>
<asp:Label ID="lblCTypeID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CTypeID")%>'></asp:Label>


</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="ISO Code" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblISOCOde" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ISOCOde")%>'></asp:Label>
<asp:Label ID="lblISOCodeID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ISOCodeID")%>'></asp:Label>


</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
<asp:TemplateField HeaderText="From" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblFrom" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "From")%>'></asp:Label>
<asp:Label ID="lblFromID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FromID")%>'></asp:Label>


</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="To" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblTo" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "To")%>'></asp:Label>
<asp:Label ID="lblToID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ToID")%>'></asp:Label>


</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>

<asp:TemplateField HeaderText="Line" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblLine" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Line")%>'></asp:Label>
<asp:Label ID="lblLineID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LineID")%>'></asp:Label>


</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
              <asp:TemplateField HeaderText="Packages" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblPackages" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Pkgs")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField> 
    <asp:TemplateField HeaderText="Package Type" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblPkgsType" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PkgsType")%>'></asp:Label>
<asp:Label ID="lblPkgstypeid" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PkgsTypeID")%>'></asp:Label>


</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField> 
    <asp:TemplateField HeaderText="Weight" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblWeight" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Weight")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>                                 
                      <asp:TemplateField HeaderText="Class" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>

<asp:Label ID="lblClass" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Class")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="center" />

</asp:TemplateField>
    <asp:TemplateField HeaderText="UN No" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>
<asp:Label ID="lblUNNo" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UNNo")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="center" />
</asp:TemplateField>  
    <asp:TemplateField HeaderText="Do Valid Date" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>
<asp:Label ID="lblDoValidDate" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DoValidDate")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="center" />
</asp:TemplateField> 
    <asp:TemplateField HeaderText="Cargo Description" Visible="true"   HeaderStyle-CssClass="center1"   >
<ItemTemplate>
<asp:Label ID="lblCargoDescription" AutoPostBack="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CargoDescription")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="center" />
</asp:TemplateField>                               
</Columns>
    <RowStyle Height="30px" />
</asp:GridView>
</div>
</div>
</div>
</asp:Panel>
        </div>
</div>


<div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm" runat="server" OnClick="btnSave_Click"  
Text="Save"  OnClientClick="return ValidationSave()" />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
                           
<a href="DomesticJobOrder.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
<%--<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="DomesticJobOrder.aspx" target="_blank"><b style="color:blue">Click here to view Account Summary</b> </a>
</div>
</div>--%>
                         
</div>
<asp:Label ID="lblAccountID" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblAccountName" Visible="false" runat="server" Text=""></asp:Label>
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
                   
<a href="DomesticJobOrder.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
        <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblPrintQue"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenSlipPrint()"  aria-hidden="true">
Yes 
</button>
<a href="DomesticLoadedIn.aspx" class="btn btn-danger ">No</a>
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
function ValidationSave() {
                 
    var ddlJOType = document.getElementById('<%= ddlJOType.ClientID%>').value;
    var ddlCustomer = document.getElementById('<%= ddlCustomer.ClientID%>').value;
    
    var txtRefJONo = document.getElementById('<%= txtRefJONo.ClientID%>').value;

                   
               

var blResult = Boolean;
blResult = true;

if (ddlJOType == 0) {
    document.getElementById('<%= ddlJOType.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}
if (ddlCustomer == 0) {
    document.getElementById('<%= ddlCustomer.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}
 (txtRefJONo == "") {
document.getElementById('<%= txtRefJONo.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
 
//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
    <script>
        function JOTypeChange() {
            
            var ddlJOType = document.getElementById('<%= ddlJOType.ClientID%>').value;

            if (ddlJOType == 1) {
                document.getElementById('<%= txtContainerNo.ClientID%>').value = document.getElementById('<%= txtOpenContainerNo.ClientID%>').value;
                document.getElementById('<%= txtContainerNo.ClientID%>').disabled = true;
            }
            else if (ddlJOType == 2) {
                document.getElementById('<%= txtContainerNo.ClientID%>').value = "";
                document.getElementById('<%= txtContainerNo.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%= txtContainerNo.ClientID%>').value = "";
                document.getElementById('<%= txtContainerNo.ClientID%>').disabled = false;
            }
        }
        function FromChange() {
            var ddlFrom = document.getElementById('<%= ddlFrom.ClientID%>').value;
            var ddlTo = document.getElementById('<%= ddlTo.ClientID%>').value;

            if (ddlFrom != 0) {
                if (ddlFrom == ddlTo) {
                    document.getElementById('<%= ddlFrom.ClientID%>').value = 0;
                    alert('From and To locations should be different!');
                    return;
                }
            }
        }
        function ToChange() {
            var ddlFrom = document.getElementById('<%= ddlFrom.ClientID%>').value;
            var ddlTo = document.getElementById('<%= ddlTo.ClientID%>').value;

            if (ddlTo != 0) {
                if (ddlTo == ddlFrom) {
                    document.getElementById('<%= ddlTo.ClientID%>').value = 0;
                    alert('From and To locations should be different!');
                    return;
                }
            }
        }
        function btnAddClick() {
            var txtContainerNo = document.getElementById('<%= txtContainerNo.ClientID%>').value;
            var ddlCargoType = document.getElementById('<%= ddlCargoType.ClientID%>').value;
            var ddlFrom = document.getElementById('<%= ddlFrom.ClientID%>').value;
            var ddlTo = document.getElementById('<%= ddlTo.ClientID%>').value;
            var txtPackages = document.getElementById('<%= txtPackages.ClientID%>').value;
            var txtWeight = document.getElementById('<%= txtWeight.ClientID%>').value;
            var txtClass = document.getElementById('<%= txtClass.ClientID%>').value;
            var txtUNNo = document.getElementById('<%= txtUNNo.ClientID%>').value;
            var txtCargoDescription = document.getElementById('<%= txtCargoDescription.ClientID%>').value;
            var ddlJOType = document.getElementById('<%= ddlJOType.ClientID%>').value;
            var ddlpkgsType = document.getElementById('<%= ddlpkgsType.ClientID%>').value;

            var blResult = Boolean;
            blResult = true;

            if (txtContainerNo == "") {
                document.getElementById('<%= txtContainerNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlCargoType == 0) {
                document.getElementById('<%= ddlCargoType.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlFrom == 0) {
                document.getElementById('<%= ddlFrom.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlTo == 0) {
                document.getElementById('<%= ddlTo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if ((ddlJOType != 1) && (ddlJOType != 0)) {
          
            }
            if (txtPackages == "") {
                document.getElementById('<%= txtPackages.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlpkgsType == 0) {
                document.getElementById('<%= ddlpkgsType.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtWeight == "") {
                document.getElementById('<%= txtWeight.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlCargoType == 2) {
                if (txtClass == "") {
                    document.getElementById('<%= txtClass.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;
                }
                if (txtUNNo == "") {
                    document.getElementById('<%= txtUNNo.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;
                }
            }
            if (txtCargoDescription == "") {
                document.getElementById('<%= txtCargoDescription.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
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
        var popup;
        function gstsearch() {
            var txtIGMNo = document.getElementById('<%= txtIGMNo.ClientID%>').value;
            var txtItemNo = document.getElementById('<%= txtItemNo.ClientID%>').value;

            var url = "ContainerListforDomesticJobOrder.aspx?IGMNo=" + txtIGMNo + "&ItemNo=" + txtItemNo

            popup = window.open(url, "Popup", "width=710,height=555");
            popup.focus();

        }
</script>
</asp:Content>

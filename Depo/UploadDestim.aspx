<%@ Page Title="Depo | Upload Destim" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="UploadDestim.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo | Approve Container</title>
       
</head>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i> Upload Destim
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
        <asp:PostBackTrigger ControlID="btnUpload" />
           <asp:PostBackTrigger  ControlID="Button1" />
           <asp:PostBackTrigger  ControlID="lnkDownloadExcel" />

    </Triggers>
<ContentTemplate>

<div class="row">
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label" style="text-decoration-color:black">
<b >Container No</b>
<asp:TextBox ID="txtContainerNo" MaxLength="11" Style="text-transform: uppercase;" AutoPostBack="true" OnTextChanged="txtContainerNo_TextChanged" class="form-control text-label form-cascade-control"
runat="server" placeholder="Enter Container No"></asp:TextBox>     
</div>
</div>

    <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Estimate Date</b>
<asp:TextBox ID="txtEstDate" class="form-control text-label" TextMode="DateTimeLocal" runat="server" ReadOnly="true" ></asp:TextBox>
</div>
</div>


    <div class="col-md-2 col-xs-12">
        <div class="form-group" style=" padding-top:20px">
<asp:Button  ID="btnShow" class="btn btn-primary btn btn-sm outline  "  runat="server" Text="Show" OnClick="btnShow_Click" />
    </div>
        </div>
    <asp:Label ID="Lblfile" runat="server" style="display:none" Text="" ForeColor="red"></asp:Label>
    <div class="col-sm-4 col-xs-12  pull-right" style="padding-top:5px">
 <div class="col-sm-6 col-xs-12 text-label" style="padding-top:12px">
     <label runat="server"  style="width:0px; margin-left:5px;" >
            <a style="display:block">
 <asp:FileUpload ID="FileUpload1" AllowMultiple="false"  runat="server" ClientIDMode="Static" /></a>
                                       </label>
       </div>
    <div class="col-sm-2 col-xs-12" style="padding-top:5px">
                                <asp:Button ID="btnUpload" class="btn btn-success btn-sm outline"  Text="Import" runat="server" OnClientClick="return ClassChange()" onclick="Upload_Click"    />  
     <asp:postbacktrigger controlid="btnUpload" xmlns:asp="#unknown"  />
        <b><asp:Label runat="server" ID="lblfilename" Text=""></asp:Label></b>
        </div>

     <div class="col-sm-2 col-xs-12 text-label" style="padding-top:5px;display:none" >
                                 <div class="form-group " >
                            <asp:Button ID="Button1" class="btn btn-primary btn-sm outline" runat="server"  CommandName="MoveNext" OnClick="Button1_Click" 
                                Text="Clear"  onclientclick="return confirm('Are you sure to Clear?')"  />
                        </div>         
                                    </div>
        <div class="col-md-2 col-xs-12" >
<div class="form-group" style="padding-top:5px;display:none">
<asp:LinkButton runat="server" ID="lnkDownloadExcel" CssClass="btn btn-info btn-sm" ToolTip="Download Template"><i class="fa fa-download"></i></asp:LinkButton>
</div>
    </div> 
    <asp:HiddenField ID="hffile" runat="server" value="" />
                               <asp:HiddenField ID="hdExist" runat="server" value="0" />
        </div>

</div>
        
    <asp:Panel runat="server" Enabled="false">
<div class="row">
<div class="col-md-3 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Size</b>
<asp:TextBox ID="txtSize" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Size"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-3 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Container Type</b>
<asp:TextBox ID="txtCType" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Container type"
runat="server"></asp:TextBox>
</div>
</div>
</div>
<div class="col-md-3 col-xs-12" style="display:none">
<asp:TextBox ID="txtEntryID" class="form-control text-label" runat="server"></asp:TextBox>
<asp:TextBox ID="txtEstimateID" class="form-control text-label" runat="server"></asp:TextBox>
</div>
<div class="row">
<div class="col-md-3 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >In Date</b>
<asp:TextBox ID="txtInDate" class="form-control text-label" runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-md-3 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Estimate Date</b>
<asp:TextBox ID="txtEstimateDate" class="form-control text-label" runat="server"   ></asp:TextBox>
</div>
</div>
                                 
</div>
 
<div class="row">
<div class="col-md-6 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Shipping Line</b>
<asp:TextBox ID="txtShippingLine" class="form-control text-label" placeholder="Shipping Line" runat="server"   ></asp:TextBox>
</div>
</div>                                 
</div>

</asp:Panel>

    
    <div class="row">
        <div class="col-md-2 col-xs-12" >
<div class="form-group text-label">
<b  >Activity</b>
<asp:TextBox ID="txtActivity" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Activity" ReadOnly="true"
runat="server"></asp:TextBox>
</div>
</div>

        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Depot Code</b>
<asp:TextBox ID="txtDepotCode" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Depot Code" ReadOnly="true"
runat="server"></asp:TextBox>
</div>
</div>

        <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Vendor Code</b>
<asp:TextBox ID="txtVendorCode" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Vendor Code" ReadOnly="true"
runat="server"></asp:TextBox>
</div>
</div>

                <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Estimate No</b>
<asp:TextBox ID="txtEstNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Estimate No" ReadOnly="true"
runat="server"></asp:TextBox>
</div>
</div>
          <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Total Amount</b>
<asp:TextBox ID="txtTotAmt" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Total Amount" ReadOnly="true"
runat="server"></asp:TextBox>
</div>
</div>
   <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Status</b>
<asp:TextBox ID="txtStatus" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Status" ReadOnly="true"
runat="server"></asp:TextBox>
</div>
</div>
    </div>
    <div class="row">

    </div>
<div class="row">
<div class="col-lg-12 col-xs-12 text-label">
<div class="table-responsive">

<asp:GridView ID="grdContainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" 
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
<Columns>  

   <%-- <asp:TemplateField HeaderText="Sr No." HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblSrNo" text='<%#Eval("SR No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>
    <asp:TemplateField HeaderText="Damage Location" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblDamageLocation" text='<%#Eval("DamageLocation")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Left" />
    </asp:TemplateField>
  <%--  <asp:TemplateField HeaderText="Is Approve" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:CheckBox runat="server" ID="chkIsApprove" AutoPostBack="true" OnCheckedChanged="chkIsApprove_CheckedChanged" Checked='<%#Eval("Is Approve Amount")%>' />
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>
     <asp:TemplateField HeaderText="Component" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblComponent" text='<%#Eval("Component")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Damage Type" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblDamageType" text='<%#Eval("DamageType")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Repair Type" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblRepairType" text='<%#Eval("RepairType")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Unit" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblUnit" text='<%#Eval("Unit")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="ManHours" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblManHours" text='<%#Eval("ManHours")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>


     <asp:TemplateField HeaderText="MatCost" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblMatCost" text='<%#Eval("MatCost")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>



    <asp:TemplateField HeaderText="ManCost" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblManCost" text='<%#Eval("ManCost")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>


    <asp:TemplateField HeaderText="QTY" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblQTY" text='<%#Eval("QTY")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

     <asp:TemplateField HeaderText="Total Amount" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblTotalAmt" text='<%#Eval("Total_Amt")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Approve Status" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblBGMApproveStatus" text='<%#Eval("BGMApproveStatus")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>

   <%-- <asp:TemplateField HeaderText="App Man Hours" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txtAppManHours" onchange="return TextChange(this)" CssClass="form-control" Width="50" Text='<%#Eval("App Man Hours")%>'></asp:TextBox>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>


<%--    <asp:TemplateField HeaderText="Man Cost" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblManCost" text='<%#Eval("Man Cost")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Right" />
    </asp:TemplateField>--%>


   <%-- <asp:TemplateField HeaderText="App Man Cost" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txtAppManCost" onchange="return TextChange(this)" CssClass="form-control" style="text-align:right" Width="50" Text='<%#Eval("App Man Cost")%>'></asp:TextBox>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>


  <%--  <asp:TemplateField HeaderText="Material Cost" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:Label runat="server" ID="lblMaterialCost" text='<%#Eval("Material Cost")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Right" />
    </asp:TemplateField>--%>
   <%-- <asp:TemplateField HeaderText="App Material Cost" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
            <asp:TextBox runat="server" ID="txtAppMaterialCost" onchange="return TextChange(this)" CssClass="form-control" style="text-align:right" Width="50" Text='<%#Eval("App Material Cost")%>'></asp:TextBox>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>--%>
<%--<asp:BoundField DataField="Total" HeaderText="Total Estimation Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="text-center"></asp:BoundField>
    
    <asp:TemplateField HeaderText="Total Approved Amount" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
           <asp:TextBox runat="server" ID="txtAppAmount" CssClass="form-control" style="text-align:right" Width="80" Text='<%#Eval("Approve Amount")%>'></asp:TextBox>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="Approved Descriptions" HeaderStyle-CssClass="text-center">
        <ItemTemplate>
           <asp:TextBox runat="server" ID="txtAppDescription" Width="200" TextMode="MultiLine" Rows="2" CssClass="form-control" Text='<%#Eval("Approved Descriptions")%>'></asp:TextBox>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="left" />
    </asp:TemplateField>--%>
</Columns>
</asp:GridView>

</div>
</div>
</div>


    </ContentTemplate>
</asp:UpdatePanel>
    <div class="row">
<div class="col-md-3 col-xs-12" id="divUpdateRepairDate" runat="server">
<div class="form-group text-label">
<b  >Repair Date</b>
    <asp:TextBox ID="txtRepairDate" class="form-control text-label" TextMode="DateTimeLocal" runat="server"   ></asp:TextBox>

<asp:TextBox ID="txtApproveDate" class="form-control text-label" TextMode="DateTimeLocal" runat="server" visible="false" ></asp:TextBox>
</div>
</div>

          <div class="col-md-2 col-xs-12"  id="divUpdateRepair" runat="server">
        <div class="form-group" style=" padding-top:20px">
<asp:Button  ID="btnUpdateRepair" class="btn btn-primary btn btn-sm outline  "  runat="server" Text="Update Repair" OnClick="btnUpdateRepair_Click" />
    </div>
        </div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label" style="display:none">
<b  >Approved By</b>
<asp:TextBox ID="txtApprovedBy" MaxLength="75" placeholder="Approved By" class="form-control text-label" runat="server"></asp:TextBox>
</div>
</div>
                                 
</div>
    <div class="row">
        <div class="col-md-6 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Approve Remarks</b>
<asp:TextBox ID="txtApproveRemarks" MaxLength="125" class="form-control text-label" placeholder="Remarks" TextMode="MultiLine" Rows="2" runat="server"   ></asp:TextBox>
</div>
</div>
    </div>
    <div class="row">
        <asp:Label ID="lblFileID" Visible="false" runat="server" ></asp:Label>
    </div>
<div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px;display:none">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnSave_Click"  
Text="Save"  OnClientClick="return ValidationSave()" />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:14px;display:none">
<div class="form-group" style="padding-top:15px">
                           
<a href="UploadDestim.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm">
Clear
</a> 
                              
</div>
                                              
                                      
</div>
                         
</div> 
                               
</asp:Panel>
                        
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
                   
<a href="UploadDestim.aspx" class="btn btn-info btn-block">OK</a>
                                
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
    <script>
        function ClassChange() {
            document.getElementById('<%= btnUpload.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btnUpload.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline disabled");
        }
    </script>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtContainerNo = document.getElementById('<%= txtContainerNo.ClientID%>').value;
    var txtApproveDate = document.getElementById('<%= txtApproveDate.ClientID%>').value;
    var txtApprovedBy = document.getElementById('<%= txtApprovedBy.ClientID%>').value;
    var txtApproveRemarks = document.getElementById('<%= txtApproveRemarks.ClientID%>').value;
                   
               

var blResult = Boolean;
blResult = true;
 

                   
<%--if (txtContainerNo == "") {
document.getElementById('<%= txtContainerNo.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
    if (txtApprovedBy == "") {
        document.getElementById('<%= txtApprovedBy.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }--%>
//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
    <script>
        function TextChange(obj) {
            var row = obj.parentNode.parentNode;
            var sum = 0;
            var multiply = 0;
            sum = (row.cells[8].getElementsByTagName("input")[0].value);              
            multiply = (row.cells[4].getElementsByTagName("input")[0].value * row.cells[6].getElementsByTagName("input")[0].value);
            row.cells[10].getElementsByTagName("input")[0].value = parseFloat(multiply) + parseFloat(sum);
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
</asp:Content>

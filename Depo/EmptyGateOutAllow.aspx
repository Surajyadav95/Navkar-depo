<%@ Page Title="Depo |Empty Eyard Gate Out Allow" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EmptyGateOutAllow.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo |Empty Eyard Gate Out Allow</title>
       
</head>
      <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:100px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>Empty Eyard Gate Out Allow
</h3>           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -24px; margin-right: -5px; margin-top: -18px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
 
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Empty Eyard Gate Out Allow
                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
            
<div class="panel-body">
                         
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
 
    <div class="row"  >
        <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
            <ContentTemplate>
                          <div class="col-md-2 col-xs-12">
        <div class="form-group text-label">
        <b  >Gate Out Allow ID</b>
        <asp:TextBox ID="txtgateOutAllow" ReadOnly="true" Style="text-transform:uppercase" class="form-control text-label"  placeholder="NEW"
        runat="server"   ></asp:TextBox>
        </div>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
         <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >Gate Out Allow Date</b>
        <asp:TextBox ID="txtgateOutAllowDate"   placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
        </div>
        </div>

         <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >Do Validity</b>
        <asp:TextBox ID="txtdovalidity"   placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
        </div>
        </div>
  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>

                <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >Booking No</b>
        <asp:TextBox ID="txtbookingNo" Style="text-transform:uppercase" OnTextChanged="txtbookingNo_TextChanged"  AutoPostBack="true" class="form-control text-label"  placeholder="Booking No"
        runat="server"   ></asp:TextBox>
        </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
        
        <div class="col-sm-1 col-xs-6">                                    
        <div class="form-group pull-left" style="padding-top:20px; height: 40px; ">
        <asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
        OnClientClick="return emptysearch();">  
        <i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
        </div>                                 
        </div>
    </div>

     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" Enabled="true"> 
    <div class="row">    
        <div class="col-md-2 col-xs-12"  >
<div class="form-group text-label">
<b  >POD</b>
<asp:DropDownList ID="ddlpod"    Style="text-transform: uppercase;"   runat="server"    class="form-control text-label">                                            
</asp:DropDownList> 
</div>
</div>
          <div class="col-md-4 col-xs-12"  >
<div class="form-group text-label">
<b  >Line Name</b>
<asp:DropDownList ID="ddllinename"   Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddllinename_SelectedIndexChanged"  runat="server" class="form-control text-label">                                            
</asp:DropDownList> 

</div>
</div>
         <div class="col-md-1 col-xs-12" style="padding-top:20px">
        <div class="form-group text-label">
        <asp:TextBox ID="txtlineId" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"   
        runat="server"></asp:TextBox>
        </div>
        </div>   
         <div class="col-md-4 col-xs-12"  >
<div class="form-group text-label">
<b  >Vessel Name</b>
<asp:DropDownList ID="ddlvesselName"   Style="text-transform: uppercase;" AutoPostBack="true" OnSelectedIndexChanged="ddlvesselName_SelectedIndexChanged"   runat="server" class="form-control text-label">                                            
</asp:DropDownList> 

</div>
</div>   
              <div class="col-md-1 col-xs-12" style="padding-top:20px">
        <div class="form-group text-label">
        <asp:TextBox ID="txtvesselid" Style="text-transform:uppercase"  ReadOnly="true" class="form-control text-label"   
        runat="server"></asp:TextBox>
        </div>
        </div>         
    </div>

    <div class="row">
          <div class="col-md-3 col-xs-12"  >
<div class="form-group text-label">
<b  >Locations</b>
<asp:DropDownList ID="ddllocations"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">                                            
</asp:DropDownList> 
</div>
</div>
          <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Shipper Name</b>
<asp:TextBox ID="txtshippername" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Shipper Name"
runat="server"   ></asp:TextBox>
</div>
</div>
         <div class="col-md-3 col-xs-12"  >
<div class="form-group text-label">
<b  >Transporter</b>
<asp:TextBox ID="txttransport" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Transporter Name"
runat="server"   ></asp:TextBox>
</div>
</div>


       


    </div>
    <div class="row">
         <div class="col-md-3 col-xs-12"  >
<div class="form-group text-label">
<b  >Ports</b>
<asp:DropDownList ID="ddlports"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">
                                            
</asp:DropDownList> 
</div>
</div>
        <div class="col-md-2 col-xs-12"  >
<div class="form-group text-label">
<b  >Seal No</b>
<asp:TextBox ID="txtSealNo" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Seal No"
runat="server"   ></asp:TextBox>
</div>

</div>
         <div class="col-md-2 col-xs-12"  >
<div class="form-group text-label">
<b  >JO Reference</b>
<asp:TextBox ID="txtJOReference" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="JO Reference"
runat="server"   ></asp:TextBox>
</div>

</div>
    </div>
        </asp:Panel>
    <div class="row">
        <div class="col-md-2 col-xs-8">
<div class="form-group text-label">
<b  >Movement Type</b>
<asp:DropDownList ID="ddlMovementtype" Style="text-transform: uppercase;"  runat="server" class="form-control " >
           <asp:ListItem Value="">-Select-</asp:ListItem> 
<asp:ListItem Value="Sale Box">Sale Box</asp:ListItem>
<asp:ListItem Value="Offhire">Offhire</asp:ListItem>
    <asp:ListItem Value="Empty Depo">Empty Depo</asp:ListItem>
<asp:ListItem Value="Party PickUp">Party PickUp</asp:ListItem>   
    <asp:ListItem Value="Vessel Repo">Vessel Repo</asp:ListItem>                                      
                                         </asp:DropDownList>
</div>

</div>
    </div>
    <div class="row">
        <div class="col-md-1 col-xs-12">
<div class="form-group text-label">
<b  >Total20's</b>
<asp:TextBox ID="txttotal20" Style="text-transform:uppercase"    class="form-control text-label"  placeholder="20's"
runat="server"   ></asp:TextBox>
</div>
</div>

        <div class="col-md-2 col-xs-12"  >
<div class="form-group text-label">
<b  >20's Type</b>
<asp:DropDownList ID="ddl20stype"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">
                                            
</asp:DropDownList> 
</div>
</div>

        <div class="col-md-1 col-xs-12">
<div class="form-group text-label">
<b  >Total40's</b>
<asp:TextBox ID="txttotal40" Style="text-transform:uppercase"    class="form-control text-label"  placeholder="40's"
runat="server"   ></asp:TextBox>
</div>
</div>

         <div class="col-md-2 col-xs-12"  >
<div class="form-group text-label">
<b  >40's Type</b>
<asp:DropDownList ID="ddl40type"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">
                                            
</asp:DropDownList> 
</div>
</div>

         <div class="col-md-1 col-xs-12">
<div class="form-group text-label">
<b  >Total45's</b>
<asp:TextBox ID="txttotal45" Style="text-transform:uppercase"    class="form-control text-label"  placeholder="45's"
runat="server"   ></asp:TextBox>
</div>
</div>
  
            <div class="col-md-2 col-xs-12"  >
<div class="form-group text-label">
<b  >45's Type</b>
<asp:DropDownList ID="ddl45type"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">
                                            
</asp:DropDownList> 
</div>
</div>
        <div class="col-sm-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnAdd" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnAdd_Click"  
Text="Add"    />
</div>
                                              
                                      
</div>
    </div>

     <div class="row">
            
    <div class="col-sm-8 text-label" runat="server"  >
        <div class="table-responsive scrolling-table-container1">
<asp:GridView ID="grdOutAllow" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >

<Columns>

<asp:TemplateField>
                                                    <ItemTemplate>
                                                               
                                                            
                                                        <asp:LinkButton ID="lnkDelete"  ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Delete"                                                         
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AutoIdTemp")%>' runat="server" OnClick="lnkDelete_Click"
                                                            ></asp:LinkButton>

   
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="20px" />
                                                </asp:TemplateField>
    <asp:TemplateField>
<ItemTemplate>
                                       
           <asp:LinkButton ID="lnkEdit"  ControlStyle-CssClass="btn btn-info btn-xs outline" Text="Edit"                                                         
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AutoIdTemp")%>' runat="server" OnClick="lnkEdit_Click1"
                                                            ></asp:LinkButton>

                   
<%--<a  href='<%# "EmptyGateOutAllow.aspx?AutoIdTempEdit=" & Server.UrlEncode(Encrypt(DataBinder.Eval(Container.DataItem, "AutoIdTemp")).ToString())%>'
Class='btn btn-info btn-xs outline' 
>Edit</a>--%>
                                                         
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" Width="60px" />
</asp:TemplateField>
    
     <asp:TemplateField HeaderText="20's" HeaderStyle-CssClass="center-header">
        <ItemTemplate>

       <asp:Label ID="lblC20s"   runat="server" text='<%#Eval("C20s")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField> 

     <asp:TemplateField HeaderText="Type Of 20's" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblType20" Visible="false"  runat="server" text='<%#Eval("TypeID_20")%>'></asp:Label>
             <asp:Label ID="lbltpe20"   runat="server" text='<%#Eval("CType ID20")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField> 

    <asp:TemplateField HeaderText="40's" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblC40s"   runat="server" text='<%#Eval("C40s")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
    
     <asp:TemplateField HeaderText="Type of 40's" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblType40"  Visible="false"  runat="server" text='<%#Eval("TypeID_40")%>'></asp:Label>
             <asp:Label ID="lbltpe40"   runat="server" text='<%#Eval("Ctype ID40")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField> 

     <asp:TemplateField HeaderText="45's" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblC45s"   runat="server" text='<%#Eval("C45s")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField> 

    <asp:TemplateField HeaderText="Type of 45's" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblType45" Visible="false" runat="server" text='<%#Eval("TypeID_45")%>'></asp:Label>
             <asp:Label ID="lbltpe45"   runat="server" text='<%#Eval("Ctype ID45")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField> 
 
     
    
                                               
</Columns>

</asp:GridView>
</div>
    </div>
 </div>
    <div class="row">
         <div class="col-md-6 col-xs-12">
        <div class="form-group text-label">
        <b  >Booking Remarks</b>
        <asp:TextBox ID="txtbookingRemarks" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Booking Remarks"
        runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
        </div>
        </div>
    </div>
    <div class="row" style="display:none">
         <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Vehicle No</b>
<asp:TextBox ID="txtvehicleno" Style="text-transform:uppercase"    class="form-control text-label"  placeholder="Vehicle No"
runat="server"   ></asp:TextBox>
</div>
</div>

         <div class="col-md-2 col-xs-12"  >
<div class="form-group text-label">
<b  >No Of Container </b>
<asp:DropDownList ID="ddlNoofcontainer"   Style="text-transform: uppercase;"  runat="server"    class="form-control text-label">
           <asp:ListItem Value="1">1</asp:ListItem>
<asp:ListItem Value="2">2</asp:ListItem>                                 
</asp:DropDownList> 
</div>
</div>
           <div class="col-sm-1">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnaddvehi" class="btn btn-primary btn btn-sm outline " runat="server"  
Text="Add"   OnClick="btnaddvehi_Click"   />
</div>
                                              
                                      
</div>

    </div>
      <div class="row">
    <div class="col-sm-6 text-label" runat="server" style="display:none"  >
        <div class="table-responsive scrolling-table-container">
<asp:GridView ID="grdVehicler" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >

<Columns>
    <asp:TemplateField HeaderText="Vehicle No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblVehicleNo"   runat="server" text='<%#Eval("Vehicle No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>    
    <%--<asp:BoundField DataField="Vehicle No" HeaderText="Vehicle No" />--%>

    <asp:TemplateField HeaderText="No Of Container" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblNoofContainer"   runat="server" text='<%#Eval("NoofContainer")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>    

<%--    <asp:BoundField DataField="NoofContainer" HeaderText="No Of Container" />--%>
                         
     <asp:TemplateField HeaderText="IsOut" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblIsOut"   runat="server" text='<%#Eval("IsOut")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>                     
</Columns>

</asp:GridView>
</div>
    </div>
          
    <div class="col-sm-4 pull-right" style="padding-top:0px;">
<div class="form-group">
<a href="exportEmptyBookingReport.aspx" target="_blank"><b style="color:blue">Click here to view Empty Booking Report</b> </a>
    <br />
    <a href="EyardBookingPendency.aspx" target="_blank"><b style="color:blue">Click here to view Booking Movement Traking</b> </a>
    <br />
  
</div>
</div>
          <b></b>
           <div class="col-sm-4 pull-right" style="padding-top:0px;">
<div class="form-group">

</div>
</div>
           <div class="col-sm-4 pull-right" style="padding-top:0px;">
<div class="form-group">

</div>
</div>
 </div>
    </ContentTemplate>
</asp:UpdatePanel>
             <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline " runat="server"  
Text="Save" OnClientClick="return ValidationSave()" OnClick="btnSave_Click"   />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:-0px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="UpdateSurveyRemarks.aspx" id="btnclear" runat="server" class="btn btn-primary  btn btn-sm outline ">
Clear
</a> 
                              
</div>                                            
                                      
</div>
 
                         
</div>                
</asp:Panel>
                         
</div>
</div>



<asp:Label ID="lblDONo" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblentryid" Visible="false" runat="server" Text=""></asp:Label>
             
                      
                    
                   
                         
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
                   
<a href="EmptyGateOutAllow.aspx" class="btn btn-info btn-block">OK</a>
                                
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
       var popup;
       function emptysearch() {

           var url = "EmptySearchBooking.aspx"

           popup = window.open(url, "Popup", "width=710,height=555");
           popup.focus();

       }
</script>
 
<script type="text/javascript">
    function ValidationSave() {
        

        var txtbookingNo = document.getElementById('<%= txtbookingNo.ClientID%>').value;
        var ddllocations = document.getElementById('<%= ddllocations.ClientID%>').value;
        var ddlpod = document.getElementById('<%= ddlpod.ClientID%>').value;
        var txtshippername = document.getElementById('<%= txtshippername.ClientID%>').value;
        var txttransport = document.getElementById('<%= txttransport.ClientID%>').value;
        var ddlMovementtype = document.getElementById('<%= ddlMovementtype.ClientID%>').value;
        
            var blResult = Boolean;
            blResult = true;
            //alert('hi')
            if (txtbookingNo == "") {
                document.getElementById('<%= txtbookingNo.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

            }
        if (ddllocations == "0") {
            document.getElementById('<%= ddllocations.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }

        if (ddlMovementtype == "") {
            document.getElementById('<%= ddlMovementtype.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }
        if (ddlpod == "0") {
            document.getElementById('<%= ddlpod.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }
        if (txtshippername == "") {
            document.getElementById('<%= txtshippername.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }
        if (txttransport == "") {
            document.getElementById('<%= txttransport.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }
       <%-- if (ddllocations == "0") {
        document.getElementById('<%= ddllocations.ClientID%>').style.borderColor = "red";
             blResult = blResult && false;

         }
        if (ddllinename == "0") {
             document.getElementById('<%= ddllinename.ClientID%>').style.borderColor = "red";
          blResult = blResult && false;

      }

        if (ddltransporter == "0") {
          document.getElementById('<%= ddltransporter.ClientID%>').style.borderColor = "red";
          blResult = blResult && false;

      }--%>
     

    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    return blResult;
}
</script>
     
    <script type="text/javascript">
        function LineChange() {           
            var ddllinename = document.getElementById('<%= ddllinename.ClientID%>').value;                 

            if (ddllinename != 0) {
                document.getElementById('<%= txtlineId.ClientID%>').value = ddllinename;
            }
        
    }
</script>



<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy').datepicker({
format: 'yyyy-mm-dd',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,



})

});



</script>  

<script type="text/javascript">

$(document).ready(function () {

//alert('hi')
$('.dummy2').datepicker({
format: 'yyyy-mm-dd',
todayHighlight: true,
autoclose: true,
allowInputToggle: true,



})

});

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
function ValidatePhoneNo() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 43 || event.keyCode == 32 || event.keyCode == 40 || event.keyCode == 41)
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

<%@ Page Title="Depo |Gate Out Empty Yard Container" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EyardOut.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo |Gate Out Empty Yard Container</title>
       
</head>
       <style>
           .nav-tabs > li.active > a, .nav-tabs > li.active > a:focus, .nav-tabs > li.active > a:hover {
               background-color: orange;
           }
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:700px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>Gate Out Empty Yard Container
</h3>
           
</div>
       
<div id="page-content">
   
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -35px; margin-right: -5px; margin-top: -25px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>

<div class="panel-body">

<div class="row">
                                         
<div class="col-md-8 pull-md-left main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">

        
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
<div class="col-sm-2 col-xs-8" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Gate Pass No</b>
<asp:TextBox ID="txtGatePassNo" placeholder="New" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" ></asp:TextBox>     
</div>
</div>

 <div class="col-md-4 col-xs-8">                                      
<div class="form-group date text-label"><b>Gate Pass Date</b>
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtGatePassDate" placeholder="mm-dd-yyyy" style="  border-radius:4px "  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>


</div>
    </div>
</div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="col-sm-4 col-xs-8">
<div class="form-group text-label">
    <b >Booking No</b>
<asp:TextBox  ID="txtBookingNo"  AutoPostBack="true"  placeholder="Booking No" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
        </ContentTemplate>
    </asp:UpdatePanel>                       
</div>
     
    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
        <ContentTemplate>

    <div class="row">
       
                 <div class="col-md-4 col-xs-8">
<div class="form-group text-label">
<b  >Movement By</b>
<asp:DropDownList ID="ddlMovementBy" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
             
      <asp:ListItem Value="0">-Select-</asp:ListItem> 
<asp:ListItem Value="1">Depot</asp:ListItem>
<asp:ListItem Value="2">Party</asp:ListItem>                                     
                                         </asp:DropDownList>
</div>

</div>
  <div class="col-md-4 col-xs-8">
<div class="form-group text-label">
<b  >Movement Type</b>
<asp:DropDownList ID="ddlMovementtype" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
           <asp:ListItem Value="0">-Select-</asp:ListItem> 
<asp:ListItem Value="1">Sale Box</asp:ListItem>
<asp:ListItem Value="2">Offhire</asp:ListItem>
    <asp:ListItem Value="3">Empty Depo</asp:ListItem>
<asp:ListItem Value="4">Export PickUp</asp:ListItem>                                      
                                         </asp:DropDownList>
</div>

</div>
        <div class="col-md-4 col-xs-8">
<div class="form-group text-label">
<b  >Truck No</b>
<asp:DropDownList ID="ddlTruckNo" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>

</div>
               <div class="col-md-4 col-xs-8">
<div class="form-group text-label">
<b  >Transporter</b>
<asp:DropDownList ID="ddlTransporter" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>
   </div>
           <div class="col-sm-3 col-xs-8" >
        <div class="form-group text-label">
        <b>Entry Type</b>
        <asp:DropDownList  ID="ddlEntrytype"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">    
        <asp:ListItem Value="0">--Select--</asp:ListItem> 
        <asp:ListItem Value="1">Port Return</asp:ListItem>
        <asp:ListItem Value="2">ICD/CFS</asp:ListItem> 
        <asp:ListItem Value="3">Factory Return/Consignee</asp:ListItem>     
        <asp:ListItem Value="4">Export Return</asp:ListItem>   
        <asp:ListItem Value="5">Party Return</asp:ListItem>   
        <asp:ListItem Value="6">Dock</asp:ListItem>                               
        </asp:DropDownList>
        </div>
        </div>
</div>
     
    <div class="row">
         <div class="col-sm-4 col-xs-8" >
<div class="form-group text-label">
    <b>Container No</b> 
<asp:TextBox  ID="txtContainer" AutoPostBack="true"  placeholder="Container No" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
               <div class="col-sm-3 col-xs-8" Style="padding-top:24px"  >
<div class="form-group text-label">
    <b>Entry ID</b> 
  <asp:Label ID="lblEntryID"  Visible="true" runat="server"  Text=" "></asp:Label>
</div>
</div>
      
        </div>


 
         <div class="row">

                          <div class="col-sm-2 col-xs-8" >
<div class="form-group text-label">
    <b>Type</b>
<asp:TextBox  ID="txtType"   placeholder="Type" Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
                      <div class="col-sm-2 col-xs-8" >
<div class="form-group text-label">
    <b>Size</b> 
<asp:TextBox  ID="TxtSize"   placeholder="Size" Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
                     <div class="col-sm-3 col-xs-8" Style="padding-top:24px"  >
<div class="form-group text-label">
    <b>ISO Code</b> 
<asp:Label ID="lblisocode" Visible="false" runat="server" Text=""></asp:Label>
</div>
</div>
 

             
              </div>
    <div class="row">
         
<div class="col-lg-8 text-label">
<div class="table-responsive " style="margin-left:-5px;margin-right:-5px;">
<asp:GridView ID="grdDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="true" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
<Columns>

<asp:BoundField DataField="20'" HeaderText="20"></asp:BoundField>
<asp:BoundField DataField="40'" HeaderText="40"></asp:BoundField>
<asp:BoundField DataField="45'" HeaderText="45"></asp:BoundField>                                               

</Columns>
</asp:GridView>
</div>

 
</div>
 
    </div>
     <div class="row">
  <div class="col-sm-6 col-xs-12" >
<div class="form-group text-label">
    <b>Line Name</b> 
<asp:TextBox  ID="txtLineName"   placeholder="Line Name" Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
                   <div class="col-sm-4 col-xs-12" >
        <div class="form-group text-label">
        <b>GST Party Name</b>
        <asp:TextBox  ID="txtgstpartyname"   placeholder="GST Party Name" ReadOnly="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

        <div class="col-sm-1 col-xs-6">                                    
        <div class="form-group pull-left" style="padding-top:20px; height: 40px; ">
        <asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
        OnClientClick="return emptysearch();">  
        <i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
        </div>                                 
        </div>
         </div>

       


    <div class="row">
                      <div class="col-sm-6 col-xs-8" >
<div class="form-group text-label">
    <b>Shipper Name</b>
<asp:TextBox  ID="txtShipperName"   placeholder="Shipper Name" Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
               <div class="col-sm-2 col-xs-12" style="display:none">
        <asp:TextBox runat="server" ID="txtLotNo"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtAutoID"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtEyardOutPrint"></asp:TextBox>

    </div>
       <div class="col-md-6 col-xs-8">
<div class="form-group text-label">
<b  >Vessel Name</b>
<asp:DropDownList ID="ddlVesselName" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>
   </div>

                  </div>

    <div class="row">
          <div class="col-sm-4 col-xs-8" >
<div class="form-group text-label">
    <b>Seal No</b>
<asp:TextBox  ID="txtSealNo"   placeholder="Seal No" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>

         <div class="col-sm-4 col-xs-8" >
<div class="form-group text-label">
    <b>Location</b>
<asp:TextBox  ID="txtLocation"   placeholder="Location" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>

          <div class="col-sm-4 col-xs-8" >
<div class="form-group text-label">
    <b>POD No</b>
<asp:TextBox  ID="txtPODNo"   placeholder="POD No" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
        </div>

          <div class="row">

               <div class="col-md-4 col-xs-8">
<div class="form-group text-label">
<b  >Out Status</b>
<asp:DropDownList ID="ddlOutStatus" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
          <asp:ListItem Value="0">-Select-</asp:ListItem> 
<asp:ListItem Value="1">DM</asp:ListItem>
<asp:ListItem Value="2">AV</asp:ListItem>                                         
                                         </asp:DropDownList>
</div>

</div>
              <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
        <asp:Label ID="lblgstid" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblTaxID" Visible="false" runat="server" Text=""></asp:Label>
                      <div class="col-sm-7 col-xs-8" >
<div class="form-group text-label">
    <b>Remarks</b>
<asp:TextBox  ID="txtRemarks"   placeholder="Remarks"  runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
              <div class="col-sm-1 col-xs-6">   
 <div class="form-group pull-left" style="padding-top:20px; height: 40px;">
<asp:Button ID="btnAdd" class="btn btn-primary btn btn-sm outline "   runat="server" OnClientClick="return ValidationAdd()"
Text="Add"/>
</div>
    </div>  
         </div>
    </ContentTemplate></asp:UpdatePanel>
 

<div class="row">
<div class="col-sm-12 col-xs-12 text-label ">
<div class="panel panel-default">
<div id="Tabs" role="tabpanel">
    <ul class="nav nav-tabs" role="tablist" style="background-color:#64A5FE">
        <li class="active"><a href="#HoldDetails" aria-controls="Hold Details" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Container Details</a></li>
<li ><a href="#ChargesDetails" aria-controls="Charges Details" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Invoice Details</a></li>



 
        </ul>
    <div class="tab-content" style="padding-top:20px">
        <div role="tabpanel" class="tab-pane active" id="HoldDetails">
                 <asp:UpdatePanel runat="server" ID="HoldUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>              
            <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdHoldDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<Columns>
     <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblContainerNo" runat="server" text='<%#Eval("ContainerNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

    <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSize" runat="server" text='<%#Eval("Size")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

    
    <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblType" runat="server" text='<%#Eval("ContainerType")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

      <asp:TemplateField HeaderText="ISO Code" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblISOCode" runat="server" text='<%#Eval("IsocodeID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

     <asp:TemplateField HeaderText="Line Name" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblLineName" runat="server" text='<%#Eval("LineName")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

       <asp:TemplateField HeaderText="Booking/BL No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblBooking" runat="server" text='<%#Eval("Booking_No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

     <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblRemarks" runat="server" text='<%#Eval("Remarks")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

       <asp:TemplateField HeaderText="VesselName" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblVesselName" runat="server" text='<%#Eval("VesselName")%>'></asp:Label>
             <asp:Label ID="lblVesselID" Visible="false" runat="server" text='<%#Eval("Vessel_ID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

      <asp:TemplateField HeaderText="POD No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblPOD" runat="server" text='<%#Eval("POD_No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

    <asp:TemplateField HeaderText="Seal No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSeal" runat="server" text='<%#Eval("Seal_No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

    <asp:TemplateField HeaderText="Entry ID" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblEntry" runat="server" text='<%#Eval("Entry_ID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

    <asp:TemplateField HeaderText="Out Status" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblStatus" runat="server" text='<%#Eval("Out_Status")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

      <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lbllocation" runat="server" text='<%#Eval("location")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
      <asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblAccountName"  runat="server" text='<%#Eval("AccountName")%>'></asp:Label>
             <asp:Label ID="lblAccountId" Visible="false" runat="server" text='<%#Eval("AccountID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

               <asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblNetAmounts"   runat="server" text='<%#Eval("NetAmount")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

 


</Columns>

</asp:GridView>
</div>
</div>
</div>
                     </ContentTemplate>
            </asp:UpdatePanel>        
        </div>
        <div role="tabpanel" class="tab-pane " id="ChargesDetails">
                      <div class="row">
                  
           <div class="col-md-3 col-xs-6">
        <div class="form-group text-label" style="padding-top:20px;">
        <asp:CheckBox ID="chkisActive" runat="server"/>
        <asp:hiddenfield ID="hdlocation" runat="server" Value="0" />
        <asp:Label ID="IsActiveLabel" runat="server" AssociatedControlID="chkisActive" CssClass="inline">Invoice Required ?</asp:Label>
        </div>
        </div>
                      </div>
            <div class="row">
      

          
              <div class="col-sm-3 col-xs-12" style="padding-top:-20px;" >
        <div class="form-group text-label">
            <b>Total Amount</b>
        <asp:TextBox  ID="txttotalamount"    Style="text-transform: uppercase;" ReadOnly="true" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

          
              <div class="col-sm-3 col-xs-12" style="padding-top:-20px;" >
        <div class="form-group text-label">
            <b>SGST</b>
        <asp:TextBox  ID="txtsgst"  Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

          
              <div class="col-sm-3 col-xs-12" style="padding-top:-20px;" >
        <div class="form-group text-label">
            <b>CGST</b>
        <asp:TextBox  ID="txtcgst"   Style="text-transform: uppercase;" ReadOnly="true" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

          
              <div class="col-sm-3 col-xs-12" style="padding-top:-20px;" >
        <div class="form-group text-label">
            <b>IGST</b>
        <asp:TextBox  ID="txtigst"   Style="text-transform: uppercase;" ReadOnly="true" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

          
              <div class="col-sm-3 col-xs-12" style="padding-top:-25px;" >
        <div class="form-group text-label">
            <b  >Grand Total</b>
        <asp:TextBox  ID="txtgrandtotal"  Style="text-transform: uppercase;"  ReadOnly="true" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
 
    </div>
                     <div class="row">
         <div class="col-md-3 col-xs-8">
        <div class="form-group text-label">
        <b  >Mode</b>
        <asp:DropDownList ID="ddlmode" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >                                               
        </asp:DropDownList>
        </div>
        </div>

        
              <div class="col-sm-2 col-xs-8" style="padding-top:-25px;" >
        <div class="form-group text-label">
            <b  >Mode No</b>
        <asp:TextBox  ID="txtmodeno"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

         <div class="col-sm-2 col-xs-12" style="padding-top:-25px;" >
        <div class="form-group text-label">
            <b>Amount</b>
        <asp:TextBox  ID="txtamount"  Style="text-transform: uppercase;" onkeypress="return ValidatePhoneNo()"  runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

          <div class="col-md-5 col-xs-12">
        <div class="form-group text-label">
        <b  >Bank Name</b>
        <asp:DropDownList ID="ddlbankname" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >                                               
        </asp:DropDownList>
        </div>
        </div>

        
           <div class="col-md-4 col-xs-12">
        <div class="form-group text-label">
        <b >Mode Date</b>
      <asp:TextBox ID="txtmodate"   placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
        </div>
        </div>

          <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <asp:Button ID="btnmode" class="btn btn-primary  btn-sm outline" OnClientClick="return ValidationMode()" runat="server"  OnClick="btnmode_Click"
        Text="Add" />
        </div>                                  
        </div>
    </div>
      
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="row">
        <div class="col-lg-12 col-xs-12 text-label " >
        <div class="table-responsive " style="margin-left:10px;margin-right:0px;">
        <asp:GridView ID="grdMode" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
        AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
        <pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
        <Columns>
        <asp:TemplateField HeaderText="Mode" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
       <asp:Label ID="lblmode" runat="server" text='<%#Eval("paymode")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Mode No" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
       <asp:Label ID="lblmodeno" runat="server" text='<%#Eval("Mode_No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
       <asp:Label ID="lblamount" runat="server" text='<%#Eval("Amount")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Bank Name" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
       <asp:Label ID="lblbankname" runat="server" text='<%#Eval("bankname")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="Mode Date" HeaderStyle-CssClass="header-center">
        <ItemTemplate>
       <asp:Label ID="lblmodedate" runat="server" text='<%#Eval("Mode_Date")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
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
    </div>
    </div>
    </div>
     <div class="col-sm-1 col-xs-3">   
 <div class="form-group pull-left" style="padding-top:5px; height: 40px;">
<asp:Button ID="btnSave" class="btn btn-primary btn btn-sm outline "   runat="server"  OnClientClick="return Validationsave()" OnClick="btnSave_Click"
Text="Save"/>
</div>
    </div>  
      <div class="col-sm-1 col-xs-3">   
 <div class="form-group pull-left" style="padding-top:5px; height: 40px;">
<a href="EyardOut.aspx" id="btnclear" runat="server" class="btn btn-primary btn btn-sm outline " >
  Clear
    </a>
</div>
    </div>



     </asp:Panel>                      
</div>
                
</div>

<asp:Label ID="txtForwarderLine" Visible="false" runat="server" Text=""></asp:Label>
    <asp:Label ID="lbllineid" Visible="false" runat="server" Text=""></asp:Label>
       <asp:Label ID="lbltype" Visible="false" runat="server" Text=""></asp:Label>
<asp:Label ID="lblcusto" Visible="false" runat="server" Text=""></asp:Label>
             <asp:Label ID="lblstatecode" Visible="false" runat="server" Text=""></asp:Label>
                      
                    
                   
                         
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
     <button class="btn btn-info btn-block" id="SaveOk" data-dismiss="modal" runat="server" onserverclick="SaveOk_ServerClick">OK</button>                
<%--<a href="GRNEntry.aspx" class="btn btn-info btn-block">OK</a>--%>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
          <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel6" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
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
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenEyardOutPrint()"  aria-hidden="true">
Yes 
</button>
<a href="EyardOut.aspx" class="btn btn-danger ">No</a>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div> 
               
</fieldset>
    </div>
      <div class="col-md-4 col-xs-12 pull-md-right sidebar" >

<div menuitemname="Client Details" class="panel panel-sidebar" style="height:700px">
   
<div class="panel-body">
<asp:UpdatePanel ID="upModalSave1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>
<div class="row">
    <div class="col-sm-10 col-xs-12">                                      
<div class="form-group date text-label">
Date
                                           
<div class="input-group input-append date input-daterange" id="datePicker1">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
    </div>
 <div class="row">    
     <div class="col-sm-10 col-xs-12">                                      
<div class="form-group date text-label">
To                                           
<div class="input-group input-append date input-daterange" id="datePicker2">
<asp:TextBox ID="txttodate" placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>

</div>                                       
</div>
     </div>
     <div class="row"> 
    <div class="col-sm-8 col-xs-12">
<div class="form-group text-label">
    <b >Search</b>
<asp:TextBox  ID="txtsearch" placeholder="Search" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
          <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline  " runat="server"
Text="Search"     />
</div>
    </div>
         </div>
     
</div>
                                         
<div class="row text-label">
&nbsp;&nbsp; <b><asp:Label ID="lblname" runat="server" ForeColor="Blue" text="Container Out Summary"></asp:Label></b>
<div class="text-label pull-right" style="padding-right:5px">
<b><asp:Label ID="lblchargescount" Visible="false" runat="server" ForeColor="Blue" text="Count:"></asp:Label></b>
<asp:Label ID="LBLNO"  runat="server" ForeColor="Black" text=""></asp:Label>
</div>
                              
<br /><br />
<div class="col-lg-12 text-label">
<div class="table-responsive scrolling-table-container" style="margin-left:-5px;margin-right:-5px;height:300px;">
<asp:GridView ID="grdconrainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
<Columns>

<asp:BoundField DataField="Code" HeaderText="Code"  ></asp:BoundField>
<asp:BoundField DataField="Container No." HeaderText="Container No" ></asp:BoundField>
    <asp:BoundField DataField="Size" HeaderText="Size"  ></asp:BoundField>
<asp:BoundField DataField="Out Date And Time" HeaderText="Out Date & Time"  ></asp:BoundField>                                             

</Columns>
</asp:GridView>
</div>

 
</div>
</div>
    
</ContentTemplate>
</asp:UpdatePanel>  
                        
</div>
                      
</div>
<div class="row" style="padding-top:14px;">
<asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">

<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
          

<br /><br /><br />
        
</div>
</div>
</div>
    
    
</div>
                               
</div>
    

</div>
       
         
</div>

    <script type="text/javascript">
        function ValidationAdd() {

            var ddlVesselName = document.getElementById('<%= ddlVesselName.ClientID%>').value;
            var ddlOutStatus = document.getElementById('<%= ddlOutStatus.ClientID%>').value;
        var ddlTransporter = document.getElementById('<%= ddlTransporter.ClientID%>').value;
        var ddlTruckNo = document.getElementById('<%= ddlTruckNo.ClientID%>').value;


        var blResult = Boolean;
        blResult = true;



        if (ddlVesselName == 0) {
            document.getElementById('<%= ddlVesselName.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}
            if (ddlOutStatus == 0) {
        document.getElementById('<%= ddlOutStatus.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }

        if (ddlTransporter == 0) {
            document.getElementById('<%= ddlTransporter.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }

        if (ddlTruckNo == 0) {
            document.getElementById('<%= ddlTruckNo.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }



        //alert('hi')
        if (blResult == false) {
            alert('Please fill the required fields!');
        }
        return blResult;
    }
</script>
   
<script type="text/javascript">
    function Validationsave() {
                 
    var ddlMovementBy = document.getElementById('<%= ddlMovementBy.ClientID%>').value;
        var ddlMovementtype = document.getElementById('<%= ddlMovementtype.ClientID%>').value;
        var ddlTransporter = document.getElementById('<%= ddlTransporter.ClientID%>').value;
        var ddlTruckNo = document.getElementById('<%= ddlTruckNo.ClientID%>').value;
               

var blResult = Boolean;
blResult = true;
 

                   
if (ddlMovementBy == 0) {
document.getElementById('<%= ddlMovementBy.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
        if (ddlMovementtype == 0) {
        document.getElementById('<%= ddlMovementtype.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

        }

        if (ddlTransporter == 0) {
            document.getElementById('<%= ddlTransporter.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }

        if (ddlTruckNo == 0) {
            document.getElementById('<%= ddlTruckNo.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;

        }


 
//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>

     

     
    

    <script type="text/javascript">
        var popup;
        function OpenEyardOutPrint() {

            var txtEyardOutPrint = document.getElementById('<%= txtEyardOutPrint.ClientID%>').value;

            var url = "../Depo/Report_Epic/EyardOutPrint.aspx?GpNo=" + txtEyardOutPrint

            window.open(url);

        }

</script>
    <script type="text/javascript">
        var popup;
        function emptysearch() {

            var url = "EmptyOutSearch.aspx"

            popup = window.open(url, "Popup", "width=710,height=555");
            popup.focus();

        }
</script>
</asp:Content>

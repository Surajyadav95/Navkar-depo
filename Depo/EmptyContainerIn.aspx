<%@ Page Title="Depo |Empty Container In" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EmptyContainerIn.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">       
    <head>
<title>Depo | Empty Container In</title>  
          
</head>

     <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:500px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i>Empty Container In
</h3>           
</div>    
<div id="page-content">      
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
<div class="page-container" style="margin-left: -38px; margin-right: -5px; margin-top: -10px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="panel-body">
<div class="row">                                      
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
<div class="panel-heading">
<h3 class="panel-title">
Empty Container In              
</h3>
</div>           
<div class="panel-body">
     <div class="row">
        <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >CFS Code</b>
        <asp:TextBox ID="txtCfscode" ReadOnly="true" Style="text-transform:uppercase" class="form-control text-label"  placeholder="NEW"
        runat="server"   ></asp:TextBox>
        </div>
        </div>

        <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >In Date-Time</b>
        <asp:TextBox ID="txtindatetime"   placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
        </div>
        </div>

        <div class="col-sm-3 col-xs-12" >
        <div class="form-group text-label">
        <b>Entry Type</b>
        <asp:DropDownList  ID="ddlEntrytype"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">    
        <asp:ListItem Value="0">--Select--</asp:ListItem> 
        <asp:ListItem Value="1">Port Return</asp:ListItem>
        <asp:ListItem Value="2">CFS</asp:ListItem> 
        <asp:ListItem Value="3">Factory Return/Consignee</asp:ListItem>     
        <asp:ListItem Value="4">Export Return</asp:ListItem>   
        <asp:ListItem Value="5">Party Return</asp:ListItem>   
        <asp:ListItem Value="6">Dock</asp:ListItem>                               
        </asp:DropDownList>
        </div>
        </div>

           <div class="col-sm-3 col-xs-12" >
        <div class="form-group text-label">
        <b>Movement By</b>
        <asp:DropDownList  ID="ddlmovementby"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">    
               <asp:ListItem Value="0">--Select--</asp:ListItem> 
        <asp:ListItem Value="1">Depot</asp:ListItem>
        <asp:ListItem Value="2">Party</asp:ListItem>                                  
        </asp:DropDownList>
        </div>
        </div>

        <div class="col-sm-3 col-xs-12" >
        <div class="form-group text-label">
        <b>Truck No</b>
        <asp:TextBox  ID="txttruckNo"   placeholder="Truck No" Style="text-transform: uppercase;" OnTextChanged="txttruckNo_TextChanged" AutoPostBack="true" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

          <div class="col-sm-6 col-xs-12" >
        <div class="form-group text-label">
        <b>Transporter</b>
        <asp:DropDownList  ID="ddlTransporter"  Style="text-transform: uppercase;" Visible="false" runat="server" class="form-control text-label">                                     
        </asp:DropDownList>
            <asp:TextBox  ID="txtTransporter"   placeholder="Transporter" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
         <div class="col-sm-3 col-xs-12" style="display:none">
        <div class="form-group text-label">
        <b>Shipper Booking No</b>
        <asp:TextBox  ID="txtBkNo"   placeholder="Shipper Booking No" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
   </div>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
    <div class="row">
        <div class="col-sm-3 col-xs-12" >
        <div class="form-group text-label">
        <b>Container No</b>
        <asp:TextBox  ID="txtcontainerNo"   placeholder="Container NO" MaxLength="11"  OnTextChanged="txtcontainerNo_TextChanged" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                   
        </asp:TextBox>
        </div>
        </div>

         
         <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>Size</b>
        <asp:DropDownList  ID="ddlSize"  Style="text-transform: uppercase;" runat="server" class="form-control text-label" AutoPostBack="true" OnSelectedIndexChanged="ddlSize_SelectedIndexChanged">  
            <asp:ListItem Value="0">--Select--</asp:ListItem> 
        <asp:ListItem Value="20">20</asp:ListItem>
        <asp:ListItem Value="40">40</asp:ListItem> 
        <asp:ListItem Value="45">45</asp:ListItem>                                    
        </asp:DropDownList>
        </div>
        </div>

          <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>Type</b>
        <asp:DropDownList  ID="ddlType"    Style="text-transform: uppercase;"    runat="server" class="form-control text-label" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">                                     
        </asp:DropDownList>
        </div>
        </div>
         <div class="col-sm-2 col-xs-12" style="display:none">
        <asp:TextBox runat="server" ID="txtLotNo"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtAutoID"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtEyardInPrint"></asp:TextBox>
             <asp:TextBox runat="server" ID="txtEyardRecipt"></asp:TextBox>

    </div>

        <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>ISO Code</b>
        <asp:DropDownList  ID="ddlISOCode"    Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                     
        </asp:DropDownList>
        </div>
        </div>
           

         <div class="col-sm-3 col-xs-12" >
        <div class="form-group text-label">
        <b>Tare Weight</b>
        <asp:TextBox  ID="txtTareWeight"   placeholder="Tare Weight" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
        </div>
             </ContentTemplate>
                </asp:UpdatePanel>
    

    <div class="row">
        <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >Shipping Line</b>
        <asp:DropDownList ID="ddlshipline" Style="text-transform: uppercase;border-radius:4px" OnSelectedIndexChanged="ddlshipline_SelectedIndexChanged"  AutoPostBack="true" runat="server"  class="form-control " >                                               
        </asp:DropDownList>
        </div>
        </div>
        <div class="col-sm-3 col-xs-12" id="divCusmer" runat="server"  style="display:none"  >
        <div class="form-group text-label">
        <b>Line Customer</b>
        <asp:DropDownList  ID="ddlLineCustomer"   OnSelectedIndexChanged="ddlLineCustomer_SelectedIndexChanged"  AutoPostBack="true"  runat="server"  class="form-control text-label">                                    
        </asp:DropDownList>
         
        </div>
        </div>
          <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>Status Type</b>
        <asp:DropDownList  ID="ddlstatusType"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                     
        </asp:DropDownList>
        </div>
        </div>

         <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>Gross Wt</b>
        <asp:TextBox  ID="txtgross"   placeholder="Gross" OnTextChanged="txtgross_TextChanged" AutoPostBack="true" onkeypress="return ValidatePhoneNo()"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

        <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>CC Wt</b>
        <asp:TextBox  ID="txtCCWt"   placeholder="CC Wt" onkeypress="return ValidatePhoneNo()"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
        <div class="col-sm-3 col-xs-12" >
        <div class="form-group text-label">
        <b>CSC/ASP</b>
        <asp:TextBox  ID="txtCSCASP"   placeholder="CSC/ASP" Style="text-transform: uppercase;" runat="server" class="form-control">                                    
        </asp:TextBox>
        </div>
        </div>
             <div class="col-sm-3 col-xs-12"   >
        <div class="form-group text-label">
        <b>Shipper/Consignee</b>
        <asp:TextBox  ID="txtShippConsignee"   placeholder="Shipper/Consignee" OnTextChanged="txtShippConsignee_TextChanged" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" class="form-control">                                    
        </asp:TextBox>
        </div>
        </div>

           <div class="col-md-2 col-xs-12">
        <div class="form-group text-label">
        <b  >Condition</b>
        <asp:DropDownList ID="ddlCondition" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >                                               
        </asp:DropDownList>
        </div>
        </div>
           <div class="col-md-2 col-xs-12">
        <div class="form-group text-label">
        <b  >MFG Date</b>
        <asp:TextBox ID="txtMFGDate"   placeholder="yyyy-mm-dd" TextMode="Month" format="MM-yyyy"  runat="server"   Class="form-control text-label"></asp:TextBox>
        </div>
        </div>

         
        
        <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>Surveyor Name</b>
        <asp:TextBox  ID="txtsurveryEir"   placeholder="Surveryer Name" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
    </div>

    <div class="row">

        
      

    </div>

    <div class="row">
      

        <asp:Label ID="lblgstid" Visible="false" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblTaxID" Visible="false" runat="server" Text=""></asp:Label>



        <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>Grade</b>
        <asp:TextBox  ID="txtGrade"   placeholder="Grade" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

         <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>Location</b>
        <asp:TextBox  ID="txtlocation"   placeholder="Location" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
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
         <div class="col-sm-3 col-xs-12" >
        <div class="form-group text-label">
        <b>GST No</b>
        <asp:TextBox  ID="txtGSTNo"   placeholder="GST Number" ReadOnly="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
    </div>

    <div class="row">
   

        <div class="col-md-4 col-xs-12">
        <div class="form-group text-label">
        <b  >Remarks</b>
        <asp:TextBox ID="txtRemarks" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Remarks"
        runat="server" TextMode="MultiLine" Rows="1"></asp:TextBox>
        </div>
        </div>

          <div class="col-md-4 col-xs-12">
        <div class="form-group text-label">
        <b  >Damage Remarks</b>
        <asp:TextBox ID="txtdamageremarks" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Damage Remarks"
        runat="server" TextMode="MultiLine" Rows="1"></asp:TextBox>
        </div>
        </div>

           <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >Do Valid Date</b>
        <asp:TextBox ID="txtDovaliddate"   placeholder="yyyy-mm-dd " TextMode="Date"  runat="server"   Class="form-control text-label"></asp:TextBox>
        </div>
        </div>

         <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >Additional Remarks</b>
        <asp:DropDownList ID="ddlAdditionalRemarks" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >                                               
        </asp:DropDownList>
        </div>
        </div>

        <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <asp:Button ID="btnAdd" class="btn btn-primary  btn-sm outline"  OnClientClick="return ValidationAdd()"  runat="server" OnClick="btnAdd_Click"
        Text="Add" />
        </div>                                  
        </div>
   </div>

    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
<ContentTemplate>
        <div class="row">
        <div class="col-lg-12 col-xs-12 text-label " >
        <div class="table-responsive  " style="margin-left:10px;margin-right:0px;">
        <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
        AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
        <pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
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
            <asp:Label ID="lbltypeid" Visible="false" runat="server" text='<%#Eval("CONTAINERTYPEID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="ISO Code" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblISOCode" runat="server" text='<%#Eval("ISOCODE")%>'></asp:Label>
             <asp:Label ID="lblisoid" Visible="false" runat="server" text='<%#Eval("ISOCODEID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="Tare Weight" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblTareWeight" runat="server" text='<%#Eval("TareWeight")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Status Type" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblStatusType" runat="server" text='<%#Eval("StatusType")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

            <asp:TemplateField HeaderText="Gross Wt" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblGrossWt" runat="server" text='<%#Eval("GROSSWEIGHT")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="CC Wt" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblCCWt" runat="server" text='<%#Eval("CC_WT")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="Shipping Line" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblShippingLine" runat="server" text='<%#Eval("SLName")%>'></asp:Label>
            <asp:Label ID="lblLineid" Visible="false" runat="server" text='<%#Eval("LINEID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="Consignee" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblConsignee" runat="server" text='<%#Eval("Consignee")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="Condition" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblCondition" runat="server" text='<%#Eval("Condition")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="MFG Date" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblMFGDate" runat="server" text='<%#Eval("MFGDate")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="BK No" Visible="false" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblBKNo" runat="server" text='<%#Eval("BKNo")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
            <asp:TemplateField HeaderText="CSC/ASP" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblCSCASP" runat="server" text='<%#Eval("CSCASP")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
              <asp:TemplateField HeaderText="Surveryer Name" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSurveryEIR" runat="server" text='<%#Eval("SURVEYEIR")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Grade" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblGrade" runat="server" text='<%#Eval("Grade")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblLocation" runat="server" text='<%#Eval("Location")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="GST Party Name" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblPartyName" runat="server" text='<%#Eval("PARTYNAME")%>'></asp:Label>
            <asp:Label ID="Label1" Visible="false" runat="server" text='<%#Eval("PARTYNAME")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblRemarks" runat="server" text='<%#Eval("Remarks")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Damage Remarks" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDamageRemarks" runat="server" text='<%#Eval("damageRemarks")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Do Valid Date" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDoValidDate" runat="server" text='<%#Eval("ValidDate")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="Additional Remarks" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblAdditionalRemarks" runat="server" text='<%#Eval("Additional_Remarks")%>'></asp:Label>
            <asp:Label ID="lblAddid" Visible="false" runat="server" text='<%#Eval("ID")%>'></asp:Label>
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
    <asp:GridView ID="grdAccounts" runat="server" Visible="false" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
        AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
        <Columns>
            <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblContainerNo" runat="server" text='<%#Eval("ContainerNo")%>'></asp:Label>
       <asp:Label ID="lblSize" runat="server" text='<%#Eval("Size")%>'></asp:Label>
       <asp:Label ID="lbltareWeight" runat="server" text='<%#Eval("TareWeight")%>'></asp:Label>
             <asp:Label ID="lblType" runat="server" text='<%#Eval("TypeID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Account Id" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblAccountID" runat="server" text='<%#Eval("AccountID")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
            <asp:TemplateField HeaderText="NetAmount" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblNetAmount" runat="server" text='<%#Eval("NetAmount")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="20px" HorizontalAlign="Center" /> 
        </asp:TemplateField>
        </Columns>
    </asp:GridView>

   
    </ContentTemplate>
        </asp:UpdatePanel>
      <div class="row">
          <asp:Panel ID="Panel2" runat="server" Enabled="true">
                 <div class="col-md-2 col-xs-6">
        <div class="form-group text-label" style="padding-top:20px;">
        <asp:CheckBox ID="chkisActive" runat="server" AutoPostBack="true" OnCheckedChanged="chkisActive_CheckedChanged" />
        <asp:hiddenfield ID="hdlocation" runat="server" Value="0" />
        <asp:Label ID="IsActiveLabel" runat="server" AssociatedControlID="chkisActive" CssClass="inline">Invoice Required ?</asp:Label>
        </div>
        </div>
              </asp:Panel>
     

          
              <div class="col-sm-2 col-xs-12" style="padding-top:-20px;" >
        <div class="form-group text-label">
            <b>Total Amount</b>
        <asp:TextBox  ID="txttotalamount"    Style="text-transform: uppercase;" ReadOnly="true" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

          
              <div class="col-sm-2 col-xs-12" style="padding-top:-20px;" >
        <div class="form-group text-label">
            <b>SGST</b>
        <asp:TextBox  ID="txtsgst"  Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

          
              <div class="col-sm-2 col-xs-12" style="padding-top:-20px;" >
        <div class="form-group text-label">
            <b>CGST</b>
        <asp:TextBox  ID="txtcgst"   Style="text-transform: uppercase;" ReadOnly="true" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

          
              <div class="col-sm-2 col-xs-12" style="padding-top:-20px;" >
        <div class="form-group text-label">
            <b>IGST</b>
        <asp:TextBox  ID="txtigst"   Style="text-transform: uppercase;" ReadOnly="true" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

          
              <div class="col-sm-2 col-xs-12" style="padding-top:-25px;" >
        <div class="form-group text-label">
            <b  >Grand Total</b>
        <asp:TextBox  ID="txtgrandtotal"  Style="text-transform: uppercase;"  ReadOnly="true" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
 
    </div>
       
    <asp:Label runat="server"  Visible="false" Id="lblstatecode"></asp:Label>
    <div class="row">
         <div class="col-md-2 col-xs-12">
        <div class="form-group text-label">
        <b  >Mode</b>
        <asp:DropDownList ID="ddlmode" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >                                               
        </asp:DropDownList>
        </div>
        </div>

        
              <div class="col-sm-2 col-xs-12" style="padding-top:-25px;" >
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

        
           <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b >Mode Date</b>
      <asp:TextBox ID="txtmodate"   placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
        </div>
        </div>

          <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <asp:Button ID="btnmode" class="btn btn-primary  btn-sm outline" OnClientClick="return ValidationMode()" runat="server" OnClick="btnmode_Click"
        Text="Add" />
        </div>                                  
        </div>
    </div>
      <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
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
       <asp:Label ID="lblModeID" runat="server" Visible="false" text='<%#Eval("paymodeID")%>'></asp:Label>
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
       <asp:Label ID="lblBankID" Visible="false" runat="server" text='<%#Eval("banknameID")%>'></asp:Label>

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

      <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <asp:Button ID="btnsave" class="btn btn-primary  btn-sm outline" OnClientClick="return Validationsave()" runat="server"  OnClick="btnsave_Click" 
        Text="Save" />
        </div>                                  
        </div>

    <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <asp:Button ID="btnclear" class="btn btn-primary  btn-sm outline" runat="server"  
        Text="Clear" />
        </div>                                  
        </div>

    <div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="EmptyContainerSummary.aspx" target="_blank"><b style="color:blue">Click here to views list of Empty Container Details</b> </a>
</div>
</div>

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
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenEyardInPrint()"  aria-hidden="true">
Yes 
</button>
<a href="EmptyContainerIn.aspx" class="btn btn-danger ">No</a>
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

           var url = "EmptySearch.aspx"

           popup = window.open(url, "Popup", "width=710,height=555");
           popup.focus();

       }
</script>
    <script type="text/javascript">


        function showView() {

            $("#divView").show();
            $("#diventry").hide();


        }
        function ValidationAdd() {

            var txtcontainerNo = document.getElementById('<%= txtcontainerNo.ClientID%>').value;
            var ddlstatusType = document.getElementById('<%= ddlstatusType.ClientID%>').value;
            var ddlCondition = document.getElementById('<%= ddlCondition.ClientID%>').value;
            var ddlSize = document.getElementById('<%= ddlSize.ClientID%>').value;
            var ddlISOCode = document.getElementById('<%= ddlISOCode.ClientID%>').value;
            var txtlocation = document.getElementById('<%= txtlocation.ClientID%>').value;
            var txtTareWeight = document.getElementById('<%=txtTareWeight.ClientID%>').value;
            var txtgross = document.getElementById('<%=txtgross.ClientID%>').value;
            var ddlshipline = document.getElementById('<%=ddlshipline.ClientID%>').value;
            var txtShippConsignee = document.getElementById('<%= txtShippConsignee.ClientID%>').value;
            var txtMFGDate = document.getElementById('<%= txtMFGDate.ClientID%>').value;
            var txtGrade = document.getElementById('<%= txtGrade.ClientID%>').value;
            var txtsurveryEir = document.getElementById('<%= txtsurveryEir.ClientID%>').value;
            var ddlType = document.getElementById('<%= ddlType.ClientID%>').value;
            var txtdamageremarks = document.getElementById('<%= txtdamageremarks.ClientID%>').value;
    var blResult = Boolean;
    blResult = true;
 //alert('hi')
    if (txtcontainerNo == "") {
        document.getElementById('<%= txtcontainerNo.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

    }

            if (txtdamageremarks == "") {
                document.getElementById('<%= txtdamageremarks.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }

         if (ddlstatusType == "0") {
        document.getElementById('<%= ddlstatusType.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

         }
            if (ddlType == "0") {
                document.getElementById('<%= ddlType.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }

      if (ddlCondition == "0") {
        document.getElementById('<%= ddlCondition.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}

      if (ddlSize == "0") {
        document.getElementById('<%= ddlSize.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}
      if (ddlISOCode == "0") {
        document.getElementById('<%= ddlISOCode.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}
     if (txtlocation == "") {
                document.getElementById('<%= txtlocation.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
 }
   if (txtTareWeight == "") {
                document.getElementById('<%= txtTareWeight.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
 }
       if (txtgross == "") {
                document.getElementById('<%= txtgross.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
 }
    if (ddlshipline == "0") {
                document.getElementById('<%= ddlshipline.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
}
            if (txtShippConsignee == "") {
                document.getElementById('<%= txtShippConsignee.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }

            if (txtGrade == "") {
                document.getElementById('<%= txtGrade.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtsurveryEir == "") {
                document.getElementById('<%= txtsurveryEir.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    return blResult;
}
</script>

    <script type="text/javascript">
        function ValidationMode() {

            var ddlmode = document.getElementById('<%= ddlmode.ClientID%>').value;
            var txtmodeno = document.getElementById('<%= txtmodeno.ClientID%>').value;
            var txtamount = document.getElementById('<%= txtamount.ClientID%>').value;
            var ddlbankname = document.getElementById('<%= ddlbankname.ClientID%>').value;
    var blResult = Boolean;
            blResult = true;



            //alert('hi')
            if (ddlmode != "1") {
                if (txtmodeno == "") {
                    document.getElementById('<%= txtmodeno.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;

                }
            }
            


            if (ddlmode == "0") {
        document.getElementById('<%= ddlmode.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }


            if (txtamount == "") {
                document.getElementById('<%= txtamount.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }

            if (ddlmode != "1"){
            if (ddlbankname == "0") {
                document.getElementById('<%= ddlbankname.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }
            }
             

            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
        }

</script>

    <script type="text/javascript">
        function Validationsave() {

            var txtTransporter = document.getElementById('<%= txtTransporter.ClientID%>').value;
            var txttruckNo = document.getElementById('<%= txttruckNo.ClientID%>').value;
            var ddlEntrytype = document.getElementById('<%= ddlEntrytype.ClientID%>').value;
            var ddlmovementby = document.getElementById('<%= ddlmovementby.ClientID%>').value;
            

            var blResult = Boolean;
            blResult = true;
    //alert('hi')
            if (txtTransporter == "") {
                document.getElementById('<%= txtTransporter.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txttruckNo == "") {
                document.getElementById('<%= txttruckNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            
            if (ddlEntrytype == "0") {
                document.getElementById('<%= ddlEntrytype.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }
            if (ddlmovementby == "0") {
                document.getElementById('<%= ddlmovementby.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }

            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
        }

</script>
    <script type="text/javascript">
        function ValidatePhoneNo() {
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
        function OpenEyardInPrint() {

            var txtEyardInPrint = document.getElementById('<%= txtEyardInPrint.ClientID%>').value;
            var txtEyardRecipt = document.getElementById('<%= txtEyardRecipt.ClientID%>').value;

            var url = "../Depo/Report_Epic/EyardInwoInvoice.aspx?GateInNo=" + txtEyardInPrint
            window.open(url);
            var url1 = "../Depo/Report_Epic/EyardInPrint.aspx?GateInNo=" + txtEyardRecipt
            window.open(url1);
            

        }

</script>
</asp:Content>

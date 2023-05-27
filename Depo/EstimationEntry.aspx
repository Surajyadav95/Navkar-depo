<%@ Page Title="Depo | Generate Estimates" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EstimationEntry.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>
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
<i class="glyphicon glyphicon-transfer"></i>Generate Estimates
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
     <div class="row">
        <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >Estimate No</b>
        <asp:TextBox ID="txtEstimateNo" ReadOnly="true" Style="text-transform:uppercase" class="form-control text-label"  placeholder="NEW"
        runat="server"   ></asp:TextBox>
        </div>
        </div>

        <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >Estimate Date</b>
        <asp:TextBox ID="txtEstimateDate" TextMode="DateTimeLocal" style="text-transform:uppercase" runat="server"  Class="form-control text-label"></asp:TextBox>
        </div>
        </div>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

<ContentTemplate>

        <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>Container No</b>
        <asp:TextBox  ID="txtContainerNo" placeholder="Container No" MaxLength="11" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                   
        </asp:TextBox>
        </div>
        </div>
    <div class="col-sm-2 col-xs-12" style="display:none" >
        <div class="form-group text-label">
        <b>Entry ID</b>
        <asp:TextBox  ID="txtEntryID" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                   
        </asp:TextBox>
        </div>
        </div>
                 <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <asp:Button ID="btnShow" class="btn btn-primary btn-sm outline"  OnClientClick="return ValidationShow()"  runat="server" OnClick="btnShow_Click"
        Text="Show" />
        </div>                                  
        </div>
    <div class="col-sm-1 col-xs-6">                                    
        <div class="form-group pull-left" style="padding-top:20px;">
        <asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary btn-sm'  runat="server"
        OnClientClick="return ContainerSearchList();">  
        <i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
        </div>                                 
        </div>
        </ContentTemplate>
</asp:UpdatePanel>
   </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">

<ContentTemplate>
        <div class="row">
    <asp:Panel runat="server" Enabled="false">


         <div class="col-sm-1 col-xs-12" >
        <div class="form-group text-label">
        <b>Size</b>
        <asp:TextBox  ID="txtSize"   placeholder="Size" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

        <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>Type</b>
        <asp:TextBox  ID="txtCType"   placeholder="Container Type" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
        
        <div class="col-sm-4 col-xs-12" >
        <div class="form-group text-label">
        <b>Shipping Line</b>
        <asp:DropDownList  ID="ddlShippingLine" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                     
        </asp:DropDownList>
        </div>
        </div>
           
            

        </asp:Panel>
               
          <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>Survey Type</b>
        <asp:DropDownList  ID="ddlSurveyType"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                     
        </asp:DropDownList>
        </div>
        </div>

         <div class="col-sm-3 col-xs-12" >
        <div class="form-group text-label">
        <b>Manual Estimate No</b>
        <asp:TextBox  ID="txtManualEstimateNo" onkeypress="return ValidateNo();"  placeholder="Manual Estimate No" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
             </div>
   
    </ContentTemplate>
</asp:UpdatePanel>
     <div class="row">
           <div class="col-sm-3 col-xs-12" >
        <div class="form-group text-label">
        <b>CSC/ASP</b>
        <asp:TextBox  ID="txtCSCASP"   placeholder="CSC/ASP"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
         <div class="col-sm-4 col-xs-12" >
        <div class="form-group text-label">
        <b>Damage Remarks</b>
        <asp:TextBox  ID="txtDMGRemarks" TextMode="MultiLine" Rows="1" placeholder="Damage Remarks"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>


          <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        <b>MF Date</b>
        <asp:TextBox  ID="txtMFDate" TextMode="Month" format="MM-yyyy"  placeholder="" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">

<ContentTemplate>
           <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >Survey By</b>
       <asp:TextBox  ID="txtSurveyBy"   placeholder="Survey By" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>           
    </ContentTemplate>
</asp:UpdatePanel>
    </div>
      </div>
      </div>
    <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
         
<div class="panel-body">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">

<ContentTemplate>
    <div class="row">
        <asp:Label ID="lblgstid" Visible="false" runat="server" Text=""></asp:Label>
          <div class="col-sm-6 col-xs-12" >
        <div class="form-group text-label">
        Description
        <asp:TextBox  ID="txtDescription" MaxLength="150"  placeholder="" TextMode="MultiLine" Rows="2" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
        <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        Estimate Type
        <asp:DropDownList  ID="ddlEstimateType"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                     
        </asp:DropDownList>
        </div>
        </div>
         <div class="col-sm-2 col-xs-12" style="display:none">
        <div class="form-group text-label">
        <b>Location</b>
        <asp:TextBox  ID="txtLocation" MaxLength="50"  placeholder="" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
        </div>

    <div class="row">
        <div class="col-sm-1 col-xs-12" >
        <div class="form-group text-label">
        Comp Code
        <asp:TextBox  ID="txtComponentCode" MaxLength="3" Visible="false" placeholder="Component Code" OnTextChanged="btnEstimateCodes_Click" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
            <asp:DropDownList runat="server" ID="ddlComponentCode" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="btnEstimateCodes_Click"></asp:DropDownList>
            <asp:Label runat="server" ID="lblComponentCode" Font-Bold="true"></asp:Label>
        </div>
        </div>
        <div class="col-sm-1 col-xs-12" >
        <div class="form-group text-label">
        Loc Code
        <asp:TextBox  ID="txtCodeLocation" Visible="false" MaxLength="4"  placeholder="Location Code" OnTextChanged="txtCodeLocation_TextChanged" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
            <asp:DropDownList runat="server" ID="ddlLocationCode" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="txtCodeLocation_TextChanged"></asp:DropDownList>
            <asp:Label runat="server" ID="lblCodeLocation" Font-Bold="true"></asp:Label>
        </div>
        </div>
        <div class="col-sm-1 col-xs-12" >
        <div class="form-group text-label">
        Dmg Code
        <asp:TextBox  ID="txtDamageCode" MaxLength="2" Visible="false"  placeholder="Damage Code" OnTextChanged="txtDamageCode_TextChanged" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
            <asp:DropDownList runat="server" ID="ddlDamageCode" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="txtDamageCode_TextChanged"></asp:DropDownList>

            <asp:Label runat="server" ID="lblDamageCode" Font-Bold="true"></asp:Label>
        </div>
        </div>
        <div class="col-sm-1 col-xs-12" >
        <div class="form-group text-label">
        Repair Code
        <asp:TextBox  ID="txtRepairCode" MaxLength="2" Visible="false"  placeholder="Repair Code" OnTextChanged="txtRepairCode_TextChanged" AutoPostBack="true" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
            <asp:DropDownList runat="server" ID="ddlRepairCode" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="txtRepairCode_TextChanged"></asp:DropDownList>

            <asp:Label runat="server" ID="lblRepairCode" Font-Bold="true"></asp:Label>
        </div>
        </div>
         <div class="col-sm-1 col-xs-12" >
        <div class="form-group text-label">
        Mat Type
        <asp:TextBox ID="txtMaterial" MaxLength="2" Visible="false" Style="text-transform:uppercase" OnTextChanged="txtMaterial_TextChanged" AutoPostBack="true" class="form-control text-label"  placeholder="Material Type"
        runat="server"></asp:TextBox>
            <asp:DropDownList runat="server" ID="ddlMaterialType" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="txtMaterial_TextChanged"></asp:DropDownList>

            <asp:Label runat="server" ID="lblMaterialType" Font-Bold="true"></asp:Label>
        </div>
        </div>
        
  
        <div class="col-md-1 col-xs-12">
        <div class="form-group text-label">
        Qty
        <asp:TextBox ID="txtDescSize" MaxLength="50" Style="text-transform:uppercase" class="form-control text-label" AutoPostBack="true" OnTextChanged="txtDescSize_TextChanged"  placeholder="Qty"
        runat="server"></asp:TextBox>
        </div>
        </div>
        <div class="col-sm-1 col-xs-12" >
        <div class="form-group text-label">
        Length
        <asp:TextBox  ID="txtLength" onkeypress="return ValidatePhoneNo();"  placeholder="Length" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>

              <div class="col-sm-1 col-xs-12" >
        <div class="form-group text-label">
        Width
        <asp:TextBox  ID="txtWidth" onkeypress="return ValidatePhoneNo();"  placeholder="Width" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
        <div class="col-sm-2 col-xs-12" >
        <div class="form-group text-label">
        Part Code
        <asp:TextBox  ID="txtPartCode"  placeholder="Part Code" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                    
        </asp:TextBox>
        </div>
        </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">

<ContentTemplate>
        <div class="col-md-1 col-xs-12">
        <div class="form-group text-label">
        Man Hours
        <asp:TextBox ID="txtManHours" onkeypress="return ValidatePhoneNo();" AutoPostBack="true" OnTextChanged="txtManHours_TextChanged" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Hrs"
        runat="server"></asp:TextBox>
        </div>
        </div>

           <div class="col-md-1 col-xs-12">
        <div class="form-group text-label">
        Man Cost
        <asp:TextBox ID="txtManCost" Style="text-align:right" AutoPostBack="true" OnTextChanged="txtManCost_TextChanged" onkeypress="return ValidatePhoneNo();"  placeholder="₹" runat="server"  Class="form-control text-label"></asp:TextBox>
        </div>
        </div>
        <div class="col-md-2 col-xs-12">
        <div class="form-group text-label">
        Material Cost
        <asp:TextBox ID="txtMaterialCost" AutoPostBack="true" OnTextChanged="txtMaterialCost_TextChanged" onkeypress="return ValidatePhoneNo();" Style="text-align:right" placeholder="₹" runat="server"  Class="form-control text-label"></asp:TextBox>
        </div>
        </div>
        <asp:Panel runat="server" Enabled="false">
        <div class="col-md-2 col-xs-12">
        <div class="form-group text-label">
        Total
        <asp:TextBox ID="txtTotal" Style="text-align:right" placeholder="Total Cost ₹" runat="server"  Class="form-control text-label"></asp:TextBox>
        </div>
        </div>
            </asp:Panel>
        </ContentTemplate></asp:UpdatePanel>

        <div class="col-md-3 col-xs-12">
        <div class="form-group text-label" style="padding-top:23px">
        
        <asp:FileUpload runat="server" ID="FileUpload" />
        </div>
        </div>
        <%--<asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">

<ContentTemplate>
     </ContentTemplate>
</asp:UpdatePanel>--%>
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
        <asp:GridView ID="grdEstimateDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
        AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
        
        <Columns>
            <asp:TemplateField>
        <ItemTemplate>
       <asp:LinkButton ID="lnkCancel" ControlStyle-CssClass='btn btn-danger btn-xs' OnClick="lnkCancel_Click"                                                             
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AUTO_ID")%>' runat="server" 
                                                            ><i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton>

<asp:LinkButton ID="lnkEdit" ControlStyle-CssClass='btn btn-info btn-xs' OnClick="lnkEdit_Click"                                                             
                                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AUTO_ID")%>' runat="server" 
                                                            ><i class="fa fa-pencil" aria-hidden="true"></i></asp:LinkButton>
        
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" Width="100px" /> 
        </asp:TemplateField>
            <asp:BoundField HeaderText="#" DataField="#" HeaderStyle-CssClass="center-header" ItemStyle-HorizontalAlign="Center" />
        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDescription" runat="server" text='<%#Eval("DESCRIPTION")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Location Physical" Visible="false" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblLocationPhysical" runat="server" text='<%#Eval("LOCATION_PHYSICAL")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Component Code" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblComponentCode" runat="server" text='<%#Eval("COMPONENT_CODE")%>'></asp:Label>            
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Location Code" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblCodeLocation" runat="server" text='<%#Eval("CODE_LOCATION")%>'></asp:Label>
             
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Damage Code" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDamageCode" runat="server" text='<%#Eval("DAMAGE_CODE")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Repair Code" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblRepairCode" runat="server" text='<%#Eval("REPAIR_CODE")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Material Type" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblMaterialType" runat="server" text='<%#Eval("Material_Type")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
             

              <asp:TemplateField HeaderText="Estimate Type" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblEstimateType" runat="server" text='<%#Eval("ESTIMATE_TYPE")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblDescSize" runat="server" text='<%#Eval("DESC_SIZE")%>'></asp:Label>
            
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Length" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblLength" runat="server" text='<%#Eval("LENGTH")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="Width" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblWidth" runat="server" text='<%#Eval("WIDTH")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>                   
            <asp:TemplateField HeaderText="Part Code" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblPartCode" runat="server" text='<%#Eval("PARTCODE")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>
              <asp:TemplateField HeaderText="Man Hours" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblManHours" runat="server" text='<%#Eval("MAN_HOURS")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="Man Cost" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblManCost" runat="server" text='<%#Eval("MAN_COST")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

              <asp:TemplateField HeaderText="Material Cost" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblMaterialCost" runat="server" text='<%#Eval("MATERIAL_COST")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblTotal" runat="server" text='<%#Eval("TOTAL")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Image" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblImage" runat="server" text='<%#Eval("FILENAME")%>'></asp:Label>
       <%--<asp:Image ID="lblImage" runat="server" />--%>
        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" /> 
        </asp:TemplateField>           
       
        </Columns>
        </asp:GridView>
        </div>
        </div>
        </div>
    <div class="row">
        <asp:Panel runat="server" Enabled="false">
            <div class="col-md-4"></div>
        <div class="col-md-2 col-xs-12">
        <div class="form-group text-label">
        <b  >Total Man Hours</b>
        <asp:TextBox ID="txtTotManHrs" Style="text-transform:uppercase;text-align:right" class="form-control text-label"  placeholder=""
        runat="server"></asp:TextBox>
        </div>
        </div>

           <div class="col-md-2 col-xs-12">
        <div class="form-group text-label">
        <b  >Total Man Cost</b>
        <asp:TextBox ID="txtTotManCost" Style="text-align:right"   placeholder="" runat="server"  Class="form-control text-label"></asp:TextBox>
        </div>
        </div>
        <div class="col-md-2 col-xs-12">
        <div class="form-group text-label">
        <b  >Total Material Cost</b>
        <asp:TextBox ID="txtTotMatCost" Style="text-align:right" placeholder="" runat="server"  Class="form-control text-label"></asp:TextBox>
        </div>
        </div>
        
        <div class="col-md-2 col-xs-12">
        <div class="form-group text-label">
        <b  >Total</b>
        <asp:TextBox ID="txtTot" Style="text-align:right" placeholder="" runat="server"  Class="form-control text-label"></asp:TextBox>
        </div>
        </div>
            </asp:Panel>
    </div>
    <asp:Button ID="btnComponentCode" runat="server" style="display:none" OnClick="btnEstimateCodes_Click" />
    <asp:Button ID="btnCodeLocation" runat="server" style="display:none" OnClick="btnEstimateCodes_Click" />
    <asp:Button ID="btn" runat="server" style="display:none" OnClick="btnEstimateCodes_Click" />
    <asp:Button ID="Button3" runat="server" style="display:none" OnClick="btnEstimateCodes_Click" />
    <asp:Button ID="Button4" runat="server" style="display:none" OnClick="btnEstimateCodes_Click" />

   </ContentTemplate>
        </asp:UpdatePanel>

    <asp:Label runat="server"  Visible="false" Id="lblstatecode"></asp:Label>
 

  
      <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
      <asp:Button ID="btnEstimate" runat="server" Text="Call Button Click1" style="display:none" OnClick="btnEstimate_Click" />

    
      <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <asp:Button ID="btnsave" class="btn btn-primary  btn-sm outline" OnClientClick="return Validationsave()" runat="server"  OnClick="btnsave_Click" 
        Text="Save" />
        </div>                                  
        </div>
        <div class="col-sm-1 col-xs-6">                                    
        <div class="form-group pull-left" style="padding-top:20px;">
        <asp:LinkButton ID="LinkButton1" ControlStyle-CssClass='btn btn-primary btn-sm' Text="Modify"  runat="server"
        OnClientClick="return ModifyList();">  
       </asp:LinkButton>
        </div>                                 
        </div>
    <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <a href="EstimationEntry.aspx" class="btn btn-primary  btn-sm outline" runat="server"  
         >Clear</a>
        </div>                                  
        </div>

    <div class="row" id="divattach" runat="server" style="cursor:pointer">
                           <div class="col-sm-6 col-xs-12 pull-right">            
                          <asp:Label ID="Lblfile" runat="server" style="display:none" Text="" ForeColor="red"></asp:Label>

  <asp:UpdatePanel ID="UpdatePanel" runat="server">
   <ContentTemplate>
<div class="row" id="div1" runat="server" style="display:block">

 <div class="col-sm-6 col-xs-12 text-label">
     <label runat="server"  style="width:0px; margin-left:10px;" >
            <a style="display:block">
 <asp:FileUpload ID="FileUpload1" AllowMultiple="false"  runat="server" ClientIDMode="Static" /></a>
                                       </label>
       </div>
    <div class="col-sm-2 col-xs-12" style="padding-top:5px">
                                <asp:Button ID="btnUpload" class="btn btn-success btn-sm outline"  Text="Import" runat="server" OnClientClick="return ClassChange()" onclick="btnUpload_Click"    />  
     <asp:postbacktrigger controlid="btnUpload" xmlns:asp="#unknown"  />
        </div>
     
   

     <div class="col-sm-2 col-xs-12 text-label" style="padding-top:5px" >
                                 <div class="form-group " >
                            <asp:Button ID="btnClear" class="btn btn-primary btn-sm outline" runat="server"  CommandName="MoveNext" OnClick="btnClear_Click"
                                Text="Clear"  onclientclick="return confirm('Are you sure to Clear?')"  />
                                    
                        </div>
                                              
                                    </div>
    <div class="col-md-2 col-xs-12" >
<div class="form-group" style="padding-top:5px" >
<asp:LinkButton runat="server" ID="lnkDownloadExcel" OnClick="lnkDownloadExcel_Click" CssClass="btn btn-info btn-sm" ToolTip="Download Template"><i class="fa fa-download"></i></asp:LinkButton>
</div>
    </div> 
</div>
       </ContentTemplate> 
	  <Triggers>
        <asp:PostBackTrigger ControlID="btnUpload" />
           <asp:PostBackTrigger  ControlID="btnClear" />
           <asp:PostBackTrigger  ControlID="lnkDownloadExcel" />

    </Triggers>
                                </asp:UpdatePanel> 
    <asp:HiddenField ID="hffile" runat="server" value="" />
                               <asp:HiddenField ID="hdExist" runat="server" value="0" />
<asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
       <div class="row">
            <div class="col-sm-5 col-xs-12 text-label" style="margin-left:10px">
          <b><asp:Label runat="server" ID="lblfilename" Text=""></asp:Label></b> 
       </div>
           </div>
<div class="row text-label">
                                <div class="col-lg-12 " style="padding-right:50px;margin-left:-10px">
                                    <div class="table-responsive " style="margin-left:12px;margin-right:0px;">

<asp:GridView ID="GridView1" runat="server"  OnRowDataBound="OnRowDataBound"  ForeColor="Black" CssClass="table table-striped table-bordered table-hover">

</asp:GridView>    
</div>
                                    </div>
    </div>
       </ContentTemplate>
    </asp:UpdatePanel>

                                    
                               </div> 
                                           </div>

    <%--<div class="col-sm-5 pull-right" style="padding-top:25px;">
<div class="form-group">
<a href="EmptyContainerSummary.aspx" target="_blank"><b style="color:blue">Click here to views list of Empty Container Details</b> </a>
</div>
</div>--%>

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
                   
<a href="EstimationEntry.aspx" class="btn btn-info btn-block">OK</a>
                                
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
       function ContainerSearchList() {

           var url = "ContainerListforEstimates.aspx"

           popup = window.open(url, "Popup", "width=710,height=555");
           popup.focus();

       }
</script>
    <script>
        function ClassChange() {
            document.getElementById('<%= btnUpload.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btnUpload.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline disabled");
        }
    </script>
     <script type="text/javascript">
         var popup;
         function ModifyList() {

             var url = "ModifyEstimateList.aspx"

             popup = window.open(url, "Popup", "top=100,left=450,width=710,height=555");
             popup.focus();

         }
</script>
    <script type="text/javascript">
        function ValidationAdd() {

            var txtcontainerNo = document.getElementById('<%= txtcontainerNo.ClientID%>').value;
            var txtDescription = document.getElementById('<%= txtDescription.ClientID%>').value;
            var txtManHours = document.getElementById('<%= txtManHours.ClientID%>').value;
            var txtManCost = document.getElementById('<%= txtManCost.ClientID%>').value;
            var txtMaterialCost = document.getElementById('<%= txtMaterialCost.ClientID%>').value;            
   
    var blResult = Boolean;
    blResult = true;
 //alert('hi')
    if (txtcontainerNo == "") {
        document.getElementById('<%= txtcontainerNo.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}
            if (txtDescription == "") {
        document.getElementById('<%= txtDescription.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}
            <%--if (txtManHours == "") {
        document.getElementById('<%= txtManHours.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}

            if (txtManCost == "") {
        document.getElementById('<%= txtManCost.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}
            if (txtMaterialCost == "") {
        document.getElementById('<%= txtMaterialCost.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

}--%>
     
    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    return blResult;
}
</script>

    <script type="text/javascript">
        function Validationsave() {

            var txtEstimateDate = document.getElementById('<%= txtEstimateDate.ClientID%>').value;
            var txtContainerNo = document.getElementById('<%= txtContainerNo.ClientID%>').value;
            var txtSize = document.getElementById('<%= txtSize.ClientID%>').value;
            var ddlShippingLine = document.getElementById('<%= ddlShippingLine.ClientID%>').value;
            
    var blResult = Boolean;
            blResult = true;


            if (txtEstimateDate == "") {
                document.getElementById('<%= txtEstimateDate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }
            if (txtContainerNo == "") {
                document.getElementById('<%= txtContainerNo.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }


            if (ddlShippingLine == 0) {
        document.getElementById('<%= txtContainerNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }

            if (txtSize == "") {
                document.getElementById('<%= txtContainerNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }

            if (blResult == false) {
                alert('Please enter valid container no!');
            }
            return blResult;
        }

</script>
    <script type="text/javascript">
        function ValidationShow() {

            var txtContainerNo = document.getElementById('<%= txtContainerNo.ClientID%>').value;          
       
            var blResult = Boolean;
            blResult = true;

            if (txtContainerNo == "") {
                document.getElementById('<%= txtContainerNo.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }
 

            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
        }

</script>
    <script type="text/javascript">
        function Total() {                  
            var first_number = document.getElementById('<%= txtManCost.ClientID%>').value;

            var second_number = document.getElementById('<%= txtMaterialCost.ClientID%>').value;
            alert(first_number)
            alert(second_number)

            var result = first_number + second_number;
            alert(result)

            document.getElementById('<%= txtTotal.ClientID%>').value = result;
        }
    </script>
    <script type="text/javascript">
        function ValidatePhoneNo() {
            //alert('hii')
            if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
                return event.returnValue;
            return event.returnValue = '';
        }
        function ValidateNo() {
            //alert('hii')
            if (event.keyCode > 47 && event.keyCode < 58)
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
    <script>
        function ComponentCode() {
            document.getElementById('<%= btnComponentCode.ClientID%>').click();            
        }

    </script>
</asp:Content>

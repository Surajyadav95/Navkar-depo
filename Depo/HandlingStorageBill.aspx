<%@ Page Title="Handling Storage Bill" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="HandlingStorageBill.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Handling Storage Bill</title>    
</head>
       <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container1{
            max-height:220px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">            
<h3>
<i class="glyphicon glyphicon-transfer"></i>Handling Storage Bill 
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
<div class="col-sm-3 col-xs-8" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Invoice No</b>
<asp:TextBox ID="txtInvoiceNo" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="NEW"></asp:TextBox>     
</div>
</div>
<div class="col-md-4  col-xs-8">                                      
<div class="form-group text-label">
<b>Invoice Date</b>                                          
<asp:TextBox ID="txtInvoiceDate"  placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
</div>
</div>
    </div>
     <asp:Button ID="btnIndentItem1" runat="server" Text="CallButtonClick"  style="display:none" OnClick="btnIndentItem1_Click" />
    <asp:Button ID="btnProforma" runat="server" Text="Call Button Click Proforma" style="display:none" OnClick="btnProforma_Click" />
     <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="row">
    <div class="col-md-3 col-xs-8">
<div class="form-group text-label">
<b  >Criteria</b>
<asp:DropDownList ID="ddlEXTCriteria" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control" >
</asp:DropDownList>
</div>

</div>
            <div class="col-sm-4 col-xs-8">
<div class="form-group text-label">
<b >Proforma No</b>
<asp:TextBox  ID="txtProformaNo" readonly="true" Style="text-transform: uppercase;" AutoPostBack="true" OnTextChanged="txtProformaNo_TextChanged" placeholder="Proforma No" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
<asp:Label runat="server" ID="Label1" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="Label2" Visible="false"></asp:Label>
</div>
</div>
 
<div class="col-sm-1 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px; ">
<asp:LinkButton ID="lnkProforma" ControlStyle-CssClass='btn btn-primary btn-sm'  runat="server"
OnClientClick="return proformasearch();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
        </div>
    
   

   
    <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
    


<div class="row">
<div class="col-sm-4 col-xs-8">
<div class="form-group text-label">
<b >GST In Number</b>
<asp:TextBox  ID="txtGstINNumber"  Style="text-transform: uppercase;" placeholder="GST Number" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
<asp:Label runat="server" ID="lblpartyid" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblGSTID" Visible="false"></asp:Label>
</div>
</div>
 
<div class="col-sm-1 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px; ">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary btn-sm'  runat="server"
OnClientClick="return gstsearch();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
<div class="col-sm-6 col-xs-8" style="padding-top:20px">
<div class="form-group text-label">
<asp:TextBox  ID="txtgstname" ReadOnly="true" AutoPostBack="false" placeholder="GST Name" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:TextBox>
</div>
</div>
<asp:Label runat="server"  Visible="false" Id="lblstatecode"></asp:Label>
</div>
    <div class="row">
        <div class="col-md-6 col-xs-8">
<div class="form-group text-label">
<b  >Shipping Line</b>
<asp:DropDownList ID="ddlshipline" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " AutoPostBack="true" OnSelectedIndexChanged="ddlshipline_SelectedIndexChanged" >
</asp:DropDownList>
</div>

</div>
        <div class="col-sm-4 col-xs-8" >
<div class="form-group text-label">
    <b>PO No</b>
<asp:TextBox  ID="txtPONo" placeholder="PO No" Style="text-transform: uppercase;" runat="server" class="form-control text-label">                                
</asp:TextBox>
</div>
</div>
        <div class="col-sm-2 col-xs-12" >
<div class="form-group text-label">
    <b>Via No</b>
<asp:TextBox  ID="txtViaNo" placeholder="Via No" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
</asp:TextBox>
</div>
</div>
        
        </div>
    <div class="row">
        <div class="col-sm-4 col-xs-8" >
<div class="form-group text-label">
    <b>Container No</b>
<asp:TextBox  ID="txtcontainer" MaxLength="11"  placeholder="Container NO" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
        <div class="col-sm-2 col-xs-6">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px; ">
<asp:LinkButton ID="lnkAdd" ControlStyle-CssClass='btn btn-primary btn-sm'  runat="server" OnClick="lnkAdd_Click"
OnClientClick="return ValidationAdd();">  
<i class="fa fa-check" aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
        <div class="col-md-5 col-xs-8">
<div class="form-group text-label">
<b  >Vessels</b>
<asp:DropDownList ID="ddlVessels" Style="text-transform: uppercase;border-radius:4px" Visible="false" runat="server" class="form-control " AutoPostBack="true" >
</asp:DropDownList>
    <asp:TextBox runat="server" ID="txtVessels" TextMode="MultiLine" Rows="2" CssClass="form-control" style="text-transform:uppercase" ReadOnly="true"></asp:TextBox>
    <asp:TextBox runat="server" ID="txtVesselID" TextMode="MultiLine" Rows="2" CssClass="form-control" style="text-transform:uppercase" Visible="false"></asp:TextBox>

</div>

</div>
        <div class="col-sm-1 col-xs-4">                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px; ">
<asp:LinkButton ID="lnkVessel" ControlStyle-CssClass='btn btn-info btn-sm'  runat="server"
OnClientClick="return vesselsearch();"> 
<i class="fa fa-search" aria-hidden="true"></i> </asp:LinkButton>
</div>                                  
</div>
    </div>
    <div class="row"> 
        <div class="col-sm-6 col-xs-8" >
<div class="form-group text-label">
<b>Invoice Type</b>
<asp:DropDownList  ID="ddlinvoicetype" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label" onchange="return InvoiceTypeChange();">
                                      
</asp:DropDownList>
</div>
</div>


            <div class="col-sm-3 col-xs-8" >
<div class="form-group text-label">
<b>Reference Type</b>
<asp:DropDownList  ID="ddlreference"  Style="text-transform: uppercase;" runat="server" class="form-control text-label" >
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="Reference No">Reference No</asp:ListItem>
<asp:ListItem Value="Statement No">Statement No</asp:ListItem>                                      
</asp:DropDownList>
</div>
</div>

           <div class="col-sm-3 col-xs-8"   >
<div class="form-group text-label">
    <b>Reference/Statement No</b>
<asp:TextBox  ID="txtReference"   placeholder="Reference NO" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>

          



    
    <div class="col-md-2" style="display:none">
        <asp:TextBox runat="server" ID="txtAssessNoPrint"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtWorkYearPrint"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtLineIDPrint"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtInvoiceTypePrint"></asp:TextBox>
        <asp:Label runat="server" ID="lblDiscPer"></asp:Label>
    </div>
   
         
<div class="col-md-4 col-xs-8" runat="server" id="divBasedOnDate" style="display:block">
<div class="form-group text-label">
<b  >Based On Date</b>
<asp:DropDownList ID="ddlBasedOnDate" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
<asp:ListItem Value="0">--Select--</asp:ListItem>
<asp:ListItem Value="Repaired Date">Repaired Date</asp:ListItem>
<asp:ListItem Value="Approved Date">Approved Date</asp:ListItem>
<asp:ListItem Value="Out Date">Out Date</asp:ListItem>

</asp:DropDownList>
</div>
</div>
         </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-4 col-xs-8" runat="server" id="divFromDate">                                      
<div class="form-group text-label">
<b >From Date</b>
<asp:TextBox ID="txtFromDate"  placeholder="yyyy-mm-dd" TextMode="DateTimeLocal"  runat="server"   Class="  form-control text-label"></asp:TextBox>

</div>                        
</div>
        <div class="col-md-4 col-xs-8" ">                                      
<div class="form-group text-label">
<b >To Date</b>
<asp:TextBox ID="txtBilledToDate"  placeholder="yyyy-mm-dd" TextMode="DateTimeLocal"  runat="server"   Class="  form-control text-label"></asp:TextBox>
</div>                        
</div>
        

          <div class="col-md-1 col-xs-8"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnShow" class="btn btn-primary btn btn-sm outline" OnClientClick="return ValidationSave();" OnClick="btnShow_Click" runat="server"
Text="Show"     />
</div>
    </div>   
        
         </div>
 
 
  <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
<ContentTemplate>
<div class="row">
    <div class="col-sm-3 col-xs-12">
        <div class="form-group">
            <asp:CheckBox runat="server" ID="chkSelectAll" AutoPostBack="true" Text="Select All" OnCheckedChanged="chkSelectAll_CheckedChanged" />
        </div>
    </div>
    <div class="form-group text-label pull-right" >
<div class="col-sm-10 col-xs-12"  >
<div class="col-sm-3 col-xs-3">
<b>20:</b>
<asp:Label runat="server" ID="lblA" ></asp:Label>
&nbsp</div>
<div class="col-sm-3 col-xs-3">
<b>40:</b>
<asp:Label runat="server" ID="lblB" ></asp:Label>
&nbsp</div>
<div class="col-sm-3 col-xs-3">
<b>45:</b>
<asp:Label runat="server" ID="lblC" ></asp:Label>
&nbsp</div>
    <div class="col-sm-3 col-xs-3">
<b>TEUS:</b>
<asp:Label runat="server" ID="lblTeus" ></asp:Label>
&nbsp</div>
</div>
                                 
</div>
<div class="col-lg-12 col-xs-12 text-label " >
<div class="table-responsive scrolling-table-container1" >
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  >
<Columns>
<asp:TemplateField HeaderText="Select" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:CheckBox ID="chkright" Text="" Checked='<%#Eval("Select")%>' runat="server" AutoPostBack="true" OnCheckedChanged="chkright_CheckedChanged" />
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

    <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblSrNo" runat="server" text='<%#Eval("Sr No")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

    <asp:TemplateField HeaderText="Entry ID" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblEntryID" runat="server" text='<%#Eval("Entry ID")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

    <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblContainerN" runat="server" text='<%#Eval("Container No")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

    <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblContainerType" runat="server" text='<%#Eval("Container Type")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

     <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblSize" runat="server" text='<%#Eval("Size")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

     <asp:TemplateField HeaderText="In Date" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblInDate" runat="server" text='<%#Eval("In Date")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

     <asp:TemplateField HeaderText="Out Date" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblOutDate" runat="server" text='<%#Eval("Out Date")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

     <asp:TemplateField HeaderText="Total Days" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblTotalDays" runat="server" text='<%#Eval("Total Days")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

     <asp:TemplateField HeaderText="Vehicle No" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblVehicleNo" runat="server" text='<%#Eval("Vehicle No")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

    <asp:TemplateField HeaderText="From" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblFromLocation" runat="server" text='<%#Eval("From")%>'></asp:Label>
<asp:Label ID="lblFromID" Visible="false" runat="server" text='<%#Eval("FromID")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

    <asp:TemplateField HeaderText="To" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblToLocation" runat="server" text='<%#Eval("To")%>'></asp:Label>
    <asp:Label ID="lblToID" Visible="false" runat="server" text='<%#Eval("ToID")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>

<%--<asp:BoundField DataField="Sr No" HeaderText="Sr No"></asp:BoundField>--%>
<%--<asp:BoundField DataField="Container No." HeaderText="Container No"></asp:BoundField>--%>
<%--<asp:BoundField DataField="Container Type" HeaderText="Type"></asp:BoundField>--%>

 <%--<asp:BoundField DataField="Size" HeaderText="Size"></asp:BoundField>--%>
<%--<asp:BoundField DataField="In Date" HeaderText="In Date"></asp:BoundField>--%>
<%--<asp:BoundField DataField="Out Date" HeaderText="Out Date"></asp:BoundField>--%>     
    <%--<asp:BoundField DataField="Total Days" HeaderText="Total Days"></asp:BoundField>--%>
<%--<asp:BoundField DataField="Vehicle No" HeaderText="Vehicle No"></asp:BoundField>--%>                  
</Columns>

</asp:GridView>
</div>
</div>
</div>
   </ContentTemplate>
      </asp:UpdatePanel>
          
<div class="row">
    <asp:UpdatePanel runat="server" ID="UpdateTariff" UpdateMode="Conditional">
        <ContentTemplate>

<div class="col-sm-4 col-xs-12" >
<div class="form-group text-label">
<b>Tariff  </b>
<asp:DropDownList  ID="ddltariff" AutoPostBack="false" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:DropDownList>
</div>
</div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
<asp:label ID="lblArea" Visible="false"  AutoPostBack="true" runat="server" ></asp:label>
<div class="col-lg-2 text-label" style="padding-top:20px" >
<asp:Button ID="btnCalculate" data-layout="center" data-type="confirm" visible="true" 
class="btn btn-success btn-sm outline" runat="server" Text="Calculate" OnClick="btnCalculate_Click"  OnClientClick=" return ValidationCalcu()" />
</div>
    <div class="col-md-2 col-xs-12" >
    <div class="form-group text-label" style="padding-top: 0px;">
        <b>Tax</b>
<asp:DropDownList  ID="ddlTaxID" Style="text-transform: uppercase;" runat="server" class="form-control text-label"> 

</asp:DropDownList>
</div>
        </div>

    
</div>
     <div class="row">
         <div class="col-md-12 col-xs-12" runat="server">                                      
<div class="form-group text-label">
<b >Remarks</b>
<asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="2"  placeholder="remarks" runat="server" Class="form-control text-label"></asp:TextBox>

</div>                        
</div>
     </div>
    <div class="row">
        <div class="col-lg-6">
<div class="form-group" style="margin-top:8px">
<b><asp:linkButton ID="lnkdetails" runat="server" Text="Click here to view Invoice details" ForeColor="Blue" Font-Size="15px"
OnClientClick="return OpenInvoicelist();"></asp:linkButton></b>&nbsp;&nbsp;&nbsp;<b style="color:blue;font-size:15px;font-family:'Segoe UI'">(F9)</b>
</div>
</div>
        
    </div>
     </asp:Panel>                      
</div>                    
</div>           
     <div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
<ContentTemplate>
<div class="modal-content">
<div class="modal-header">
<center>
<span><i runat="server" id="I1" class="fa fa-5x fa-check-circle-o text-success"></i></span>
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
<button class="btn btn-info " id="btnprint" data-dismiss="modal" runat="server" onclick="OpenInvoicePrint()"  aria-hidden="true">
Yes 
</button>
<a href="HandlingStorageBill.aspx" class="btn btn-danger ">No</a>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</div> 
               
</fieldset>
    </div>
    <div class="col-md-4 col-xs-12 pull-md-right sidebar" >

<div menuitemname="Client Details" class="panel panel-sidebar" style="height:722px">
   
<div class="panel-body">
<asp:UpdatePanel ID="upModalSave1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>

                                         
<div class="row text-label">
&nbsp;&nbsp; <b><asp:Label ID="lblname" runat="server" ForeColor="Blue" text="Charges to be collected"></asp:Label></b>
<div class="text-label pull-right" style="padding-right:5px">
<b><asp:Label ID="lblchargescount" Visible="false" runat="server" ForeColor="Blue" text="Count:"></asp:Label></b>
<asp:Label ID="LBLNO"  runat="server" ForeColor="Black" text=""></asp:Label>
</div>
                              
<br /><br />
<div class="col-lg-12 text-label">
<div class="table-responsive scrolling-table-container" style="margin-left:-5px;margin-right:-5px;height:372px;">
<asp:GridView ID="rptIndentLIst" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
<Columns>
<asp:TemplateField Visible="false">
<ItemTemplate>
<asp:HiddenField ID="hfEntryid" runat="server" Value='<%#Eval("AccountID")%>' />
<asp:Label ID="lblaccntid" runat="server" text='<%#Eval("AccountID")%>'></asp:Label>
<asp:CheckBox id="chkindent"  runat="server" Visible="false"/>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left"  />
</asp:TemplateField>
                                               
<asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="header-center">
<ItemTemplate> 
<asp:Label ID="lblaccntname" runat="server" text='<%#Eval("AccountName")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="header-center">
<ItemTemplate> 
<asp:Label ID="lblContainerNo" runat="server" text='<%#Eval("Containerno")%>'></asp:Label>
<asp:Label ID="lblSize" Visible="false" runat="server" text='<%#Eval("Size")%>'></asp:Label>

</ItemTemplate>
<ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>                                    
<asp:TemplateField HeaderText="Net Amount" HeaderStyle-CssClass="header-center">
<ItemTemplate>
<asp:Label ID="lblntamnt" runat="server" text='<%#Eval("NetAmount")%>'></asp:Label>
</ItemTemplate>
<ItemStyle HorizontalAlign="Right"  />
</asp:TemplateField>
</Columns>
</asp:GridView>
</div>
<div id="divtblWOTOtal" runat="server" style="display:none;" >                                         
<table forecolor="Black" class="table table-striped table-bordered table-hover" style="border-top:5px solid #7bc144;margin-left:-5px;">
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b ">Net Total</b></td>
<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblPercentage" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblTotal" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered" style="display:none">
       
<td style ="width:69%;text-align:left"><b >Discount</b></td>

<%--<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="Label1" style="margin-left:10px;"> </asp:Label>&nbsp;%</td>--%>
<td style ="width:30%;text-align:right"><asp:textbox ID="txtdiscount" AutoPostBack="true" class="form-control" OnTextChanged="txtdiscount_TextChanged"   Width="150px"  runat="server"  text='<%#Eval("")%>'></asp:textbox>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
    <tr  class="table-bordered">
       
<%--<td style ="width:69%;text-align:left"><b >SGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblSgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblSGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr class="table-bordered">
       
<%--<td style ="width:69%;text-align:left"><b >CGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblCgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblCGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>

<tr  class="table-bordered">
       
<%--<td style ="width:69%;text-align:left"><b >IGST</b></td>--%>
<td style ="width:20%;text-align:right"><asp:Label runat="server" ID="lblIgstPer" style="margin-left:10px;"> </asp:Label>&nbsp;</td>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblIGST" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
<tr  class="table-bordered">
       
<td style ="width:69%;text-align:left"><b >Grand Total</b></td>
<%--<td style ="width:20%;text-align:right"></td>--%>
<td style ="width:30%;text-align:right"><asp:Label runat="server" ID="lblAllTotal" style="margin-left:10px;"> </asp:Label>&nbsp;<i class="fa fa-rupee"></i></td>
</tr>
</table>
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
          
<div class="col-lg-12" style="margin-left:6px;">
<a href="HandlingStorageBill.aspx" class="btn btn-primary btn-sm outline pull-left"
runat="server">Clear</a>
                                 
<asp:Button ID="btnsave" data-layout="center" Visible="true"  data-type="success" ValidationGroup="Groupsubmit" class="btn btn-success btn-sm outline pull-right "
runat="server" Text="Save"  OnClientClick="return ValidationSave1()"  />
</div>
<br /><br /><br />
        
</div>
</div>
</div>
    
    
</div>
                               
</div>
      

</div>
       
         
</div>
    <script type="text/javascript">
        function InvoiceTypeChange() {
            var ddlinvoicetype = document.getElementById('<%= ddlinvoicetype.ClientID%>').value;
            if ((ddlinvoicetype == 6) || (ddlinvoicetype == 7)) {
                document.getElementById('<%= divBasedOnDate.ClientID%>').style.display = "block";
                document.getElementById('<%= ddlBasedOnDate.ClientID%>').value = 0;
                document.getElementById('<%= divFromDate.ClientID%>').style.display = "block";
            }
            else if (ddlinvoicetype == 4) {
                document.getElementById('<%= divFromDate.ClientID%>').style.display = "none";  
                document.getElementById('<%= divBasedOnDate.ClientID%>').style.display = "none";
                
            }
            else
            {
                document.getElementById('<%= divFromDate.ClientID%>').style.display = "block";
                document.getElementById('<%= divBasedOnDate.ClientID%>').style.display = "none";
            }
        }
    </script>
    <script type="text/javascript">
        var popup;
        function OpenInvoicelist() {
            
            var url = "ListofInvoiceDetails.aspx"
                

window.open(url);

        }
        
    </script>
    <script  type="text/javascript">
        if (window.captureEvents) {
            window.captureEvents(Event.KeyUp);
            window.onkeyup = executeCode;
        }
        else if (window.attachEvent) {
            document.attachEvent('onkeyup', executeCode);
        }

        function executeCode(evt) {
            if (evt == null) {
                evt = window.event;
            }
            var theKey = parseInt(evt.keyCode, 10);
            switch (theKey) {
                case 120:  // End
                    document.getElementById("<%=lnkdetails.ClientID%>").click();
                         //document.getElementById('ctl00_ContentPlaceHolder1_btnHome').click(); 

                     break;
                         //case 36:  // F8
                         //    document.getElementById('btnreset').click();
                         //    break;
                         //case 120:  // F9
                         //    // document.getElementById('Button1').click();

                         //    break;
                         //case 87: //w
                         //    if (window.event.altKey)
                         //        document.getElementById('buttonid').click();
                         //    break;
             }
             evt.returnValue = false;
             return false;
         }

</script>
   <script type="text/javascript">
       var popup;
       function OpenInvoicePrint() {

           var txtAssessNoPrint = document.getElementById('<%= txtAssessNoPrint.ClientID%>').value;
           var txtWorkYearPrint = document.getElementById('<%= txtWorkYearPrint.ClientID%>').value;
           var txtLineIDPrint = document.getElementById('<%= txtLineIDPrint.ClientID%>').value;
           var txtInvoiceTypePrint = document.getElementById('<%= txtInvoiceTypePrint.ClientID%>').value;


           var url = "../Depo/Report_Epic/HandlingStoragePrint.aspx?InvoiceNo=" + txtAssessNoPrint + "&WorkYear=" + txtWorkYearPrint + "&LineID=" + txtLineIDPrint + "&InvoiceType=" + txtInvoiceTypePrint

            window.open(url);
        }

</script>
    <script type="text/javascript">
        function ValidationCalcu() {

            var ddlinvoicetype = document.getElementById('<%= ddlinvoicetype.ClientID%>').value;
            var txtGstINNumber = document.getElementById('<%= txtGstINNumber.ClientID%>').value;
            var ddltariff = document.getElementById('<%= ddltariff.ClientID%>').value;
            document.getElementById('<%= btnCalculate.ClientID%>').value = "Please Wait...";

            document.getElementById('<%= btnCalculate.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline disabled");
    var blResult = Boolean;
    blResult = true;

    if (ddlinvoicetype == 0) {
        document.getElementById('<%= ddlinvoicetype.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }

            if (ddltariff == 0) {
        document.getElementById('<%= ddltariff.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
            }
            if (txtGstINNumber == "") {
                document.getElementById('<%= txtGstINNumber.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }

    //alert('hi')
    if (blResult == false) {
        alert('Please fill the required fields!');
        document.getElementById('<%= btnCalculate.ClientID%>').value = "Calculate";
        document.getElementById('<%= btnCalculate.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline");
    }
    return blResult;
}
</script>

<script type="text/javascript">
function ValidationSave() {
    
    var ddlinvoicetype = document.getElementById('<%= ddlinvoicetype.ClientID%>').value;
    var ddlshipline = document.getElementById('<%= ddlshipline.ClientID%>').value;
    var ddlBasedOnDate = document.getElementById('<%= ddlBasedOnDate.ClientID%>').value;
    <%--var ddlVessels = document.getElementById('<%= ddlVessels.ClientID%>').value;--%>
    var txtProformaNo = document.getElementById('<%= txtProformaNo.ClientID%>').value;
               
//    document.getElementById('<%= btnShow.ClientID%>').value = "Please Wait...";

//    document.getElementById('<%= btnShow.ClientID%>').setAttribute("class", "btn btn-primary btn btn-sm outline disabled");
//var blResult = Boolean;
//blResult = true;
// 
    
                   
if (ddlinvoicetype == 0) {
document.getElementById('<%= ddlinvoicetype.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
    if (ddlshipline == 0) {
        document.getElementById('<%= ddlshipline.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;
    }
    if (ddlinvoicetype == 5) {
        if (ddlBasedOnDate == 0) {
            document.getElementById('<%= ddlBasedOnDate.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;
        }
    }
    if (ddlinvoicetype == 3) {
        if (txtProformaNo == "") {
            document.getElementById('<%= txtProformaNo.ClientID%>').style.borderColor = "red";
            blResult = blResult && false;
        }
    }
//alert('hi')
if (blResult == false) {
    alert('Please fill the required fields!');
    document.getElementById('<%= btnShow.ClientID%>').value = "Show";

    document.getElementById('<%= btnShow.ClientID%>').setAttribute("class", "btn btn-primary btn btn-sm outline");
}
return blResult;
}
</script>
    <script type="text/javascript">
        function ValidationSave1() {

            var result = confirm('Do you  want to save ?')
            var blResult = Boolean;
            blResult = true;

            if (result == true) {
                document.getElementById('<%= btnsave.ClientID%>').value = "Please Wait...";

                document.getElementById('<%= btnsave.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline pull-right disabled");
            }
            else {
                blResult = blResult && false;
            }
            return blResult;
           
}
</script>
        <script type="text/javascript">
            function ValidationAdd() {

                var ddlinvoicetype = document.getElementById('<%= ddlinvoicetype.ClientID%>').value;
                var ddlshipline = document.getElementById('<%= ddlshipline.ClientID%>').value;


                var blResult = Boolean;
                blResult = true;

                document.getElementById('<%= lnkAdd.ClientID%>').setAttribute("class", "btn btn-primary btn-sm disabled");

                if (ddlinvoicetype == 0) {
                    document.getElementById('<%= ddlinvoicetype.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;
                }
                if (ddlshipline == 0) {
                    document.getElementById('<%= ddlshipline.ClientID%>').style.borderColor = "red";
                    blResult = blResult && false;
                }
                if (blResult == false) {
                    alert('Please fill the required fields!');
                    document.getElementById('<%= lnkAdd.ClientID%>').setAttribute("class", "btn btn-primary btn-sm");
                }
            return blResult;

        }
</script>
    
<script type="text/javascript">
    var popup;
    function gstsearch() {
        var url = "GSTPartySearch.aspx"

        popup = window.open(url, "Popup", "width=710,height=555");
        popup.focus();
    }
    function proformasearch() {
        var url = "ProformaSearch.aspx"

        popup = window.open(url, "Popup", "width=710,height=555");
        popup.focus();
    }
</script>

    
    <script type="text/javascript">
        var popup;
        function vesselsearch() {

            var txtFromDate = document.getElementById('<%= txtFromDate.ClientID%>').value;
            var txtBilledToDate = document.getElementById('<%= txtBilledToDate.ClientID%>').value;
            var ddlshipline = document.getElementById('<%= ddlshipline.ClientID%>').value;

            var blResult = Boolean;
            blResult = true;

            if (txtFromDate == "") {
                document.getElementById('<%= txtFromDate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (txtBilledToDate == "") {
                document.getElementById('<%= txtBilledToDate.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (ddlshipline == 0) {
                document.getElementById('<%= ddlshipline.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            else {
                var url = "VesselList.aspx?FromDate=" + txtFromDate + "&ToDate=" + txtBilledToDate + "&Line=" + ddlshipline

                popup = window.open(url, "Popup", "width=710,height=555");
                popup.focus();
            }
        }
</script>


    
</asp:Content>

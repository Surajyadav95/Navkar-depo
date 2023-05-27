<%@ Page Title="Depo |Modify Category Wise GST Party" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="WiseModifGSTParty.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">  
 
       
<head>
<title>Depo |  Modify Category Wise GST Party</title>
       
</head>
<div class="page-container">
<div class="pageheader">           
<h3>
<i class="glyphicon glyphicon-transfer"></i> Modify Category Wise GST Party
</h3>           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
</ContentTemplate>
</asp:UpdatePanel>
<div class="page-container" style="margin-left: -15px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
 
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">

            
<div class="panel-body">
                         
 <div class="row">
     <div class="col-sm-3 col-xs-12">                                      
<div class="form-group text-label">
<b >Date of Modification</b>
<asp:TextBox ID="txtdateofModi"  placeholder="DD-MM-yyyy"   runat="server" TextMode="Date" Class="form-control text-label"></asp:TextBox>
</div>                        
</div>


 </div>
    <div class="row">
             <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Category</b>
<asp:DropDownList ID="ddlCategory" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">
    <asp:ListItem Value="0">--Select--</asp:ListItem>
    
    <asp:ListItem Value="Empty Yard">Empty Yard</asp:ListItem>  
                                        
</asp:DropDownList> 
</div>
</div>
    </div>

    <div class="row">

                     <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Work Year</b>
<asp:TextBox ID="txtWorkYear" placeholder="Work Year" Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                                        
</asp:TextBox> 
</div>
</div>
          <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click1" />

          <div class="col-md-2 col-xs-12">
<div class="form-group text-label">
<b  >Assessment No</b>
<asp:TextBox ID="txtAssessment"   Style="text-transform: uppercase;" runat="server"  class="form-control text-label"  placeholder="Assessment No">                                        
</asp:TextBox> 
</div>
</div>

 

        <div class="col-md-1 col-xs- 12">
<div class="form-group pull-left" style="padding-top:20px" >
<asp:Button ID="btnsearch" runat="server" 
class="btn btn-primary btn-sm outline" Text="Show" OnClick="btnsearch_Click" 
    OnClientClick="return ValidationAdd()">

</asp:Button>
</div>              
</div>
    </div>

    <div class="row">
         <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >GST In</b>
<asp:TextBox ID="txtGstIn" placeholder="GST Number"  Style="text-transform: uppercase;" runat="server" ReadOnly="true"  class="form-control text-label">                                        
</asp:TextBox> 
</div>
</div>
    </div>

    <div class="row">
  <div class="col-sm-3 col-xs-12">                                      
   <div class="form-group text-label">
  <b >Assessment Date</b>
   <asp:TextBox ID="txtAssessmentDate"  placeholder="dd-MM-yyyy"  runat="server" ReadOnly="true"   Class="form-control text-label"></asp:TextBox>
  </div>                        
   </div>
    </div>
  
    <div class="row">
              <div class="col-md-4 col-xs-12">
<div class="form-group text-label">
<b  >Party Name</b>
<asp:TextBox ID="txtPartyName" placeholder="Party Name"    Style="text-transform: uppercase;" runat="server" ReadOnly="true"  class="form-control text-label">                                        
</asp:TextBox> 
</div>
</div>
           <div class="col-md-5 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Party Name</b>
<asp:DropDownList ID="ddlpartyName"   Style="text-transform: uppercase;" runat="server"  class="form-control text-label">                                        
</asp:DropDownList> 
    <asp:Label runat="server" ID="lblstatecode" Visible="false"></asp:Label>
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
        <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
<b >GST Address</b>
<asp:TextBox ID="txGSTAddress" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"    placeholder="GST Address"  
TextMode="MultiLine"    runat="server"   ></asp:TextBox>
</div>
</div>
             <asp:Label ID="lblgst" Visible="false" runat="server" Text=""></asp:Label>       
    </div>
    <div class="row">
        <div class="col-md-6 col-xs-12">
<div class="form-group text-label" style="padding-top:22px;">
<asp:CheckBox ID="chkgstapplicable" runat="server" Checked="true" />
<asp:hiddenfield ID="Hiddenfield1" runat="server" Value="0" />
<asp:Label ID="Label1" runat="server" AssociatedControlID="chkgstapplicable" CssClass="inline"> Is Tax Applicable?</asp:Label>
</div>
</div>

      </div>          
    <div class="row">
<div class="col-sm-1">
<div class="form-group" style="padding-top:15px">
<asp:Button ID="btnSave" class="btn btn-primary btn-sm outline" runat="server" OnClick="btnSave_Click"
Text="Update"  OnClientClick="return ValidationSave()" />
</div>
                                              
                                      
</div>
                       
<div class="col-sm-1" style="padding-left:14px;">
<div class="form-group" style="padding-top:15px">
                           
<a href="WiseModifGSTParty.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline">
Clear
</a> 
                              
</div>
                                  
                                      
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
                   
<a href="WiseModifGSTParty.aspx" class="btn btn-info btn-block">OK</a>
                                
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
function ValidationAdd() {
    var txtAssessment = document.getElementById('<%= txtAssessment.ClientID%>').value;
    var txtWorkYear = document.getElementById('<%= txtWorkYear.ClientID%>').value;
    var ddlCategory = document.getElementById('<%= ddlCategory.ClientID%>').value;



var blResult = Boolean;
blResult = true;

if (txtAssessment == "") {
document.getElementById('<%= txtAssessment.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}
 
    if (ddlCategory == 0) {
document.getElementById('<%= ddlCategory.ClientID%>').style.borderColor = "red";
blResult = blResult && false;
}



if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>
          

 
     <script type="text/javascript">
         var popup;
         function emptysearch() {

             var url = "EmptySearchParty.aspx"

             popup = window.open(url, "Popup", "width=710,height=555");
             popup.focus();

         }
</script>
 
</asp:Content>
  
<%@ Page Title="Depo | Home" Language="VB" MasterPageFile="~/Depo/User.Master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="RA_asd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--Page Title-->
<!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
<form id="form1" >
<div class="pageheader">
<h3>
<i class="fa fa-home"></i>Home
</h3>
<div class="breadcrumb-wrapper">
<span class="label">You are here:</span>
<ol class="breadcrumb">
<li><a href="Home.aspx">Home </a></li>
<li class="active">Home </li>
</ol>
</div>
</div>
<!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
<!--End page title-->
<!--Page content-->
<!--===================================================-->
<div id="page-content">
<!--Widget-4 -->
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
 
<div class="row">

<div class="col-md-4 col-xs-6 text-label" style="width:280px">
                  
<div class="col-md-12 col-xs-12 panel " style="height:108px;display: inline-block;background-color:rgb(84, 181, 224);margin-right:32px;padding:initial;border-color:rgb(231, 84, 90);border-radius:4px">
<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:108px; background-color:rgb(41, 162, 217);border-radius:4px"  >
</div>                                 
<div class="col-md-9 col-xs-8 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Unestimated 
                               
<br />
<span class="pull-left" style="color:white;font-size:11px;margin-top:-1px;">20's</span>
<asp:label class="pull-right" runat="server" id="lblUnest20" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-1px;margin-right:8px" >0</asp:label>
    
    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-8px;">40's</span>
<asp:label class="pull-right" runat="server" id="lblUnest40" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-8px;margin-right:8px" >0</asp:label>

    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-14px;">45's</span>
<asp:label class="pull-right" runat="server" id="lblUnest45" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-14px;margin-right:8px" >0</asp:label>

<br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-20px;">Teu's</span>
<asp:label class="pull-right" runat="server" id="lblUnestTeus" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-20px;margin-right:8px" >0</asp:label>
</div>
               
<br />
            
</div>
           
</div> 
<div class="col-md-4 col-xs-6 text-label" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:108px;display: inline-block;background-color:rgb(144, 198, 87);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);border-radius:4px">
<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:108px; background-color:rgb(118, 175, 60);border-radius:4px">

</div>
                                 

<div class="col-md-9 col-xs-8 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Empty-IN


 <br />
<span class="pull-left" style="color:white;font-size:11px;margin-top:-1px;">20's</span>
<asp:label class="pull-right" runat="server" id="lblIn20" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-1px;margin-right:8px" >0</asp:label>
    
    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-8px;">40's</span>
<asp:label class="pull-right" runat="server" id="lblIn40" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-8px;margin-right:8px" >0</asp:label>

    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-14px;">45's</span>
<asp:label class="pull-right" runat="server" id="lblIn45" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-14px;margin-right:8px" >0</asp:label>

<br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-20px;">Teu's</span>
<asp:label class="pull-right" runat="server" id="lblInTeus" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-20px;margin-right:8px" >0</asp:label>                            
<br /><br />
</div>
            
<br />
            
</div>
           
</div> 
<div class="col-md-4 col-xs-6 text-label" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:108px;display: inline-block;background-color:rgb(249, 169, 74);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);border-radius:4px">
<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:108px; background-color:rgb(249, 149, 25);border-radius:4px"></div>
                                 

<div class="col-md-9 col-xs-8 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Empty-Out
   <br />
<span class="pull-left" style="color:white;font-size:11px;margin-top:-1px;">20's</span>
<asp:label class="pull-right" runat="server" id="lblOut20" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-1px;margin-right:8px" >0</asp:label>
    
    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-8px;">40's</span>
<asp:label class="pull-right" runat="server" id="lblOut40" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-8px;margin-right:8px" >0</asp:label>

    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-14px;">45's</span>
<asp:label class="pull-right" runat="server" id="lblOut45" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-14px;margin-right:8px" >0</asp:label>

<br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-20px;">Teu's</span>
<asp:label class="pull-right" runat="server" id="lblOutTeus" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-20px;margin-right:8px" >0</asp:label>                               
<br /><br />
</div>
      
<br />
            
</div>
           
</div> 
<div class="col-md-4 col-xs-6 text-label" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:108px;display: inline-block;background-color:rgb(229, 86, 88);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);border-radius:4px">
<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:108px; background-color:rgb(225, 43, 42);border-radius:4px"></div>
                                 

<div class="col-md-9 col-xs-8 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Inventory

 <br />
<span class="pull-left" style="color:white;font-size:11px;margin-top:-1px;">20's</span>
<asp:label class="pull-right" runat="server" id="lblInv20" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-1px;margin-right:8px" >0</asp:label>
    
    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-8px;">40's</span>
<asp:label class="pull-right" runat="server" id="lblInv40" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-8px;margin-right:8px" >0</asp:label>

    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-14px;">45's</span>
<asp:label class="pull-right" runat="server" id="lblInv45" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-14px;margin-right:8px" >0</asp:label>

<br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-20px;">Teu's</span>
<asp:label class="pull-right" runat="server" id="lblInvTeus" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-20px;margin-right:8px" >0</asp:label>  
<br />

<br /><br />
</div>
     
<br />
            
</div>
           
</div> 


        <div class="col-md-4 col-xs-6 text-label" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:108px;display: inline-block;background-color:rgb(84, 181, 224);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);border-radius:4px">
<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:108px; background-color:rgb(41, 162, 217);border-radius:4px"></div>
                                 


<div class="col-md-9 col-xs-8 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Estimated
<br />
<span class="pull-left" style="color:white;font-size:11px;margin-top:-1px;">20's</span>
<asp:label class="pull-right" runat="server" id="lblest20" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-1px;margin-right:8px" >0</asp:label>
    
    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-8px;">40's</span>
<asp:label class="pull-right" runat="server" id="lblest40" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-8px;margin-right:8px" >0</asp:label>

    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-14px;">45's</span>
<asp:label class="pull-right" runat="server" id="lblest45" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-14px;margin-right:8px" >0</asp:label>

<br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-20px;">Teu's</span>
<asp:label class="pull-right" runat="server" id="lblestTeus" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-20px;margin-right:8px" >0</asp:label>                                 
<br /><br />
</div>
      
<br />
            
</div>
           
</div>

    <div class="col-md-4 col-xs-6 text-label" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:108px;display: inline-block;background-color:rgb(144, 198, 87);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);border-radius:4px">
<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:108px; background-color:rgb(118, 175, 60);border-radius:4px"></div>
                                 

<div class="col-md-9 col-xs-8 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Approved
 <br />
<span class="pull-left" style="color:white;font-size:11px;margin-top:-1px;">20's</span>
<asp:label class="pull-right" runat="server" id="lblApp20" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-1px;margin-right:8px" >0</asp:label>
    
    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-8px;">40's</span>
<asp:label class="pull-right" runat="server" id="lblApp40" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-8px;margin-right:8px" >0</asp:label>

    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-14px;">45's</span>
<asp:label class="pull-right" runat="server" id="lblApp45" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-14px;margin-right:8px" >0</asp:label>

<br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-20px;">Teu's</span>
<asp:label class="pull-right" runat="server" id="lblAppTeus" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-20px;margin-right:8px" >0</asp:label>                             
<br /><br />
</div>
      
<br />
            
</div>
           
</div>
    <div class="col-md-4 col-xs-6 text-label" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:108px;display: inline-block;background-color:rgb(249, 169, 74);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);border-radius:4px">
<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:108px; background-color:rgb(249, 149, 25);border-radius:4px"></div>
                                 


<div class="col-md-9 col-xs-8 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Repaired
  <br />
<span class="pull-left" style="color:white;font-size:11px;margin-top:-1px;">20's</span>
<asp:label class="pull-right" runat="server" id="lblRep20" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-1px;margin-right:8px" >0</asp:label>
    
    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-8px;">40's</span>
<asp:label class="pull-right" runat="server" id="lblRep40" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-8px;margin-right:8px" >0</asp:label>

    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-14px;">45's</span>
<asp:label class="pull-right" runat="server" id="lblRep45" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-14px;margin-right:8px" >0</asp:label>

<br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-20px;">Teu's</span>
<asp:label class="pull-right" runat="server" id="lblRepTeus" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-20px;margin-right:8px" >0</asp:label>                                
<br /><br />
</div>
      
<br />
            
</div>
           
</div>
    <div class="col-md-4 col-xs-6 text-label" style="width:280px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:108px;display: inline-block;background-color:rgb(229, 86, 88);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);border-radius:4px">
<div class="col-md-3 col-xs-4 pull-left"style="top:-0px; height:108px; background-color:rgb(225, 43, 42);border-radius:4px"></div>
                                 


<div class="col-md-9 col-xs-8 pull-left" style="margin-top:5px;text-align:left;color:white;font-size:large" >
Hold
 <br />
<span class="pull-left" style="color:white;font-size:11px;margin-top:-1px;">20's</span>
<asp:label class="pull-right" runat="server" id="lblHold20" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-1px;margin-right:8px" >0</asp:label>
    
    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-8px;">40's</span>
<asp:label class="pull-right" runat="server" id="lblHold40" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-8px;margin-right:8px" >0</asp:label>

    <br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-14px;">45's</span>
<asp:label class="pull-right" runat="server" id="lblHold45" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-14px;margin-right:8px" >0</asp:label>

<br />
    <span class="pull-left" style="color:white;font-size:11px;margin-top:-20px;">Teu's</span>
<asp:label class="pull-right" runat="server" id="lblHoldTeus" ToolTip="Last 30 days" style="color:white;font-size:11px;margin-top:-20px;margin-right:8px" >0</asp:label>                                 
<br /><br />
</div>
      
<br />
            
</div>
           
</div>

    <asp:label ID="lblfrom" Visible="false" runat="server" Text=""></asp:label>
    <asp:label ID="lblto" Visible="false" runat="server" Text=""></asp:label>
            


</div>
<div class="row">
</div>
<div class="row">
</div>
        
</div>
<!--===================================================-->
<!--End page content-->
<script src="../js/jQuery.min.js" type="text/javascript"></script>
<script type="text/javascript">
$(document).ready(function () {
$('[data-toggle="tooltip"]').tooltip();
});

       
</script>
</form>
</asp:Content>


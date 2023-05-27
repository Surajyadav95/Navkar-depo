<%@ Page Title="Depo | Admin Amendments" Language="VB" MasterPageFile="User.master" AutoEventWireup="false" CodeFile="Ammendment.aspx.vb" Inherits="RA_asd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<!--Page Title-->
<!--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~-->
<form id="form1" >
<div class="pageheader">
<h3>
<i class="fa fa-home"></i>Admin Amendments
</h3>
<div class="breadcrumb-wrapper">
 
<ol class="breadcrumb">
 
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
        <%--<b style="font-size:25px;margin-left:12px;color:blue">Empty Yard</b>
        <b style="font-size:25px;margin-left:143px;color:blue">Generate Codeco</b>--%>
        
        </div>
 
<div class="row">

<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelNOC" style="width:275px;vertical-align:middle;">            
<div class="col-md-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(84, 181, 224);margin-right:32px;padding:initial;border-color:rgb(231, 84, 90)">
                                 
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
<a href="EyardCntrModification.aspx" style="color:white;" onclick='return pop("EyardCntrModification.aspx",900,550,70,300);'> Change Container Status
</a>
                               
<br /><br />
</div>             
<br />           
</div>
           
</div> 


    <div class="col-md-4 col-xs-6 text-label" runat="server" id="div1" style="width:275px;display:none">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(254, 106, 0);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
                                 
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
 
<a href="GenerateCodeco.aspx"  style="color:white" target="_blank"> Generate Codeco
</a>                               
<br /><br />
</div>
            
<br />
            
</div>
           
</div> 

 

    


   
<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelBondIN" style="width:275px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(249, 169, 74);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
                                 
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
 
<a href="CancelGateIn.aspx"  style="color:white" onclick='return pop("CancelGateIn.aspx",900,550,70,300);'> Cancel Gate In
</a>                               
<br /><br />
</div>
            
<br />
            
</div>
           
</div> 
            <div class="col-md-4 col-xs-6 text-label" runat="server" id="div2" style="width:275px;display:none">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(232, 34, 54);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
                                 
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
 
<a href="ReApplicablecodeco.aspx"  style="color:white" target="_blank"> Re-Application For Codeco
</a>                               
<br /><br />
</div>
            
<br />
            
</div>
           
</div> 

 
<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelBondEx" style="width:275px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(229, 86, 88);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
                                 

<div class="col-md-12 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
 
<a href="CancelGateOut.aspx"  style="color:white" onclick='return pop("CancelGateOut.aspx",900,550,70,300);'>  Cancel Gate out
</a>                               
<br /><br />
</div>
      
<br />
            
</div>
           
</div> 
 
<div class="col-md-4 col-xs-6 text-label" runat="server" id="divCancelGatePass" style="width:275px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(0, 148, 254);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
                                 
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >
    <a href="ModifyEyardCntrOut.aspx"  style="color:white" onclick='return pop("ModifyEyardCntrOut.aspx",900,550,70,300);'>Modify Gate Out
</a>  
<br />

<br /><br />
</div>
     
<br />
            
</div>
           
</div> 
    </div> 
<div class="row">

<div class="col-md-3 col-xs-6 text-label" runat="server" id="divModifyNOC"  >
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(0, 189, 242);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:12px;text-align:left;color:white;font-size:large" >

         <a href="WiseModifGSTParty.aspx"  style="color:white" target="_blank">Modify GST Party Invoice
</a>                         
<br /><br />
</div>
      
<br />
            
</div>
           
</div>
 
<div class="col-md-5 col-xs-6 text-label" runat="server" id="divModifyBondIn" style="width:275px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(229, 86, 88);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
 
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:-12px;text-align:left;color:white;font-size:large" >

<br />
 <a href="CancelEstimation.aspx"  style="color:white" onclick='return pop("CancelEstimation.aspx",900,550,70,300);'>Cancel Estimation
</a>     

<br /><br />
</div>
                
<br />
            
</div>
           
</div>

        
 
<div class="col-md-5 col-xs-6 text-label" runat="server" id="divModifyBondEx" style="width:275px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(192, 37, 84);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
                              
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:-12px;text-align:left;color:white;font-size:large" >

<br />
 <a href="CancelApprove.aspx"  style="color:white" onclick='return pop("CancelApprove.aspx",900,550,70,300);'>Cancel Approve
</a>
                  
<br /><br />
</div>
       
<br />
            
</div>
           
</div>
 
<div class="col-md-5 col-xs-6 text-label" runat="server" id="div6" style="width:275px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(41, 162, 217);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
                              
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:-12px;text-align:left;color:white;font-size:large" >

<br />
 <a href="CancelRepair.aspx"  style="color:white" onclick='return pop("CancelRepair.aspx",900,550,70,300);'>Cancel Repair
</a>
                  
<br /><br />
</div>
       
<br />
            
</div>
           
</div>

    
 <div class="col-md-5 col-xs-6 text-label" runat="server" id="div3" style="width:275px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(192, 37, 84);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
                              
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:-12px;text-align:left;color:white;font-size:large" >

<br />
 <a href="DPDContainerGateIn.aspx"  style="color:white" onclick='return pop("DPDContainerGateIn.aspx",900,550,70,300);'>DPD Container Gate In
</a>
                  
<br /><br />
</div>
       
<br />
            
</div>
           
</div>
     <div class="col-md-5 col-xs-6 text-label" runat="server" id="div4" style="width:275px">
                  
<div class="col-sm-12 col-xs-12 panel " style="height:50px;display: inline-block;background-color:rgb(192, 37, 84);margin-right:32px;padding:initial;border-color:rgb(241, 240, 248);">
                              
<div class="col-md-12 col-xs-8 pull-right" style="margin-top:-12px;text-align:left;color:white;font-size:large" >

<br />
 <a href="ChangeSizeType.aspx"  style="color:white" onclick='return pop("ChangeSizeType.aspx",900,550,70,300);'>Change Size Type
</a>
                  
<br /><br />
</div>
       
<br />
            
</div>
           
</div>
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


<%@ Page Title="Depo |Hold Entry" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EmpHold.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo |Hold Entry</title>
       
</head>
       <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:200px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>Hold Entry 
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
              <div class="col-sm-4 col-xs-8" >
<div class="form-group text-label">
    <b>Container No</b>
<asp:TextBox  ID="txtcontainer"   placeholder="Container NO" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>

          <div class="col-md-1 col-xs-8"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnShow" class="btn btn-primary btn btn-sm outline " OnClientClick=" return ValidationSave()"  runat="server"
Text="Show"     />
</div>
    </div>   
        
      </div>

<div class="row">
<div class="col-sm-3 col-xs-8" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Entry ID</b>
<asp:TextBox ID="txtEntryId" placeholder="Entry ID" Style="text-transform: uppercase; background-color:#e9e9e9" ReadOnly="true"  class="form-control text-label form-cascade-control"
runat="server" Text="" ></asp:TextBox>     
</div>
</div>

</div>
 

<%--    <asp:Button ID="btnIndentItem" runat="server" Text="Call Button Click" style="display:none" OnClick="btnIndentItem_Click" />
    <div class="row">

 
<div class="col-sm-1 col-xs-6">
                                     
<div class="form-group pull-left" style="padding-top:20px; height: 40px; ">
<asp:LinkButton ID="lnksearch" ControlStyle-CssClass='btn btn-primary'  runat="server"
OnClientClick="return gstsearch();">  
<i class=" fa fa-search"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>

<asp:Label runat="server"  Visible="false" Id="lblstatecode"></asp:Label>
</div>--%>


    <div class="row">
         <div class="col-md-6 col-xs-8">
<div class="form-group text-label">
<b  >Hold Reasons</b>
<asp:DropDownList ID="ddlHoldReason" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>

</div>


</div>
    <div class="row">
                      <div class="col-sm-6 col-xs-8" >
<div class="form-group text-label">
    <b>Line Name</b>
<asp:TextBox  ID="txtLineName"   placeholder="Line Name" Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
        </div>

         <div class="row">
                      <div class="col-sm-2 col-xs-8" >
<div class="form-group text-label">
    <b>Size</b> 
<asp:TextBox  ID="TxtSize"   placeholder="Size" Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>

             <div class="col-sm-2 col-xs-8" >
<div class="form-group text-label">
    <b>Type</b>
<asp:TextBox  ID="txtType"   placeholder="Type" Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
                                   <div class="col-sm-2 col-xs-8" >
<div class="form-group text-label">
    <b>Via No</b>
<asp:TextBox  ID="txtViano"   placeholder="via No" Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
        </div>


    <div class="row">
                      <div class="col-sm-6 col-xs-8" >
<div class="form-group text-label">
    <b>Customer Name</b>
<asp:TextBox  ID="txtCustomerName"   placeholder="Customer Name" Style="text-transform: uppercase;" runat="server" ReadOnly="true" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
        </div>

    <div class="row">
    <div class="col-md-5 col-xs-12">                                      
<div class="form-group date text-label"><b>In Date</b>
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="TxtInDate" placeholder="mm-dd-yyyy" style="width: 200px;"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>


</div>
    </div>
</div>
        </div>

        <div class="row">
           <div class="col-md-5 col-xs-12">                                      
<div class="form-group date text-label"><b>Hold Date</b>
                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtHoldDate" placeholder="mm-dd-yyyy" style="width: 200px;"  runat="server" TextMode="DateTimeLocal" ReadOnly="true" Class="form-control text-label"></asp:TextBox>


</div>
    </div>
</div>
        </div>

        <div class="row">
                      <div class="col-sm-6 col-xs-8" >
<div class="form-group text-label">
    <b>Remarks</b>
<asp:TextBox  ID="txtRemarks"   placeholder="Remarks"  runat="server" TextMode="MultiLine" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
         </div>
    <div class="row">
                             <div class="col-md-3">
<div class="form-group"  >
<asp:Button ID="btnHold" class="btn btn-primary btn btn-sm outline " OnClientClick=" return ValidationHold()"  runat="server"
Text="Put On Hold"     />
</div>
    </div>   
          <div class="col-md-3"  style="margin-left:-60px" >
<div class="form-group"  >
<asp:Button ID="btnClearHold" class="btn btn-primary btn btn-sm outline " OnClientClick=" return ValidationClearHold()"  runat="server"
Text="Clear From Hold"     />
</div>
    </div>  
        <div class="col-md-3"  style="margin-left:-40px" >
<div class="form-group"  >
<asp:Button ID="btnClear" class="btn btn-primary btn btn-sm outline "   runat="server"
Text="Clear"/>
</div>
    </div>   
     
        </div>
 
  <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
<ContentTemplate>
<div class="row">
    <div class="form-group text-label pull-right" >

                                 
</div>
<div class="col-lg-12 col-xs-12 text-label " >
<div class="table-responsive " >
<asp:GridView ID="GrdHoldDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"  >
<Columns>


    <%-- <asp:TemplateField HeaderText="Vehicle No" HeaderStyle-CssClass="center-header">
<ItemTemplate>
<asp:Label ID="lblVehicleNo" runat="server" text='<%#Eval("Vehicle No")%>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="20px" HorizontalAlign="Center" /> 
</asp:TemplateField>--%>

<asp:BoundField DataField="Hold On" HeaderText="Hold On"></asp:BoundField>
<asp:BoundField DataField="Hold By" HeaderText="Hold By"></asp:BoundField>
<asp:BoundField DataField="Released On" HeaderText="Released On"></asp:BoundField>

 <asp:BoundField DataField="Released By" HeaderText="Released By"></asp:BoundField>
<asp:BoundField DataField="Hold Remark" HeaderText="Hold Remark"></asp:BoundField>
<asp:BoundField DataField="Released Remark" HeaderText="Released Remark"></asp:BoundField>     
                
</Columns>

</asp:GridView>
</div>
</div>
</div>
    </ContentTemplate>
      </asp:UpdatePanel>
          

  
 
     </asp:Panel>                      
</div>
 
                               

   
                        
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
                   
<a href="EmpHold.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
               
</fieldset>
    </div>
    <div class="col-md-4 col-xs-12 pull-md-right sidebar" >

<div menuitemname="Client Details" class="panel panel-sidebar" style="height:400px">
   
<div class="panel-body">
<asp:UpdatePanel ID="upModalSave1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
<ContentTemplate>

                                         
<div class="row text-label">
&nbsp;&nbsp; <b><asp:Label ID="lblname" runat="server" ForeColor="Blue" text="All Container On Hold"></asp:Label></b>
<div class="text-label pull-right" style="padding-right:5px">
<b><asp:Label ID="lblchargescount" Visible="false" runat="server" ForeColor="Blue" text="Count:"></asp:Label></b>
<asp:Label ID="LBLNO"  runat="server" ForeColor="Black" text=""></asp:Label>
</div>
                              
<br /><br />
<div class="col-lg-12 text-label">
<div class="table-responsive scrolling-table-container" style="margin-left:-5px;margin-right:-5px;height:300px;">
<asp:GridView ID="grdDets" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover "
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">
<Columns>

<asp:BoundField DataField="Entry ID" HeaderText="Entry ID"></asp:BoundField>
<asp:BoundField DataField="Container No" HeaderText="Container No"></asp:BoundField>
<asp:BoundField DataField="Hold Reason" HeaderText="Hold Reason"></asp:BoundField>                                               

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
function ValidationSave() {
                 
    var txtcontainer = document.getElementById('<%= txtcontainer.ClientID%>').value;
                   
               

var blResult = Boolean;
blResult = true;
 

                   
if (txtcontainer == "") {
document.getElementById('<%= txtcontainer.ClientID%>').style.borderColor = "red";
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
        function ValidationHold() {
            var ddlHoldReason = document.getElementById('<%= ddlHoldReason.ClientID%>').value;
            var txtRemarks = document.getElementById('<%= txtRemarks.ClientID%>').value;
            


    var blResult = Boolean;
    blResult = true;



    if (ddlHoldReason == 0) {
        document.getElementById('<%= ddlHoldReason.ClientID%>').style.borderColor = "red";
    blResult = blResult && false;

    }
            if (txtRemarks == "") {
                document.getElementById('<%= txtRemarks.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }

           

    //alert('hi')
    if (blResult == false) {
        alert('Please fill the required fields!');
    }
    else {
        var result = confirm('Sure to put this container on hold ?')
        if (result == true) {

        }
        else {
            blResult = blResult && false;
        }
    }
    return blResult;
}
</script>

    <script type="text/javascript">
        function ValidationClearHold() {
            var ddlHoldReason = document.getElementById('<%= ddlHoldReason.ClientID%>').value;
            var txtRemarks = document.getElementById('<%= txtRemarks.ClientID%>').value;



            var blResult = Boolean;
            blResult = true;



            
    if (txtRemarks == "") {
        document.getElementById('<%= txtRemarks.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;

            }



            //alert('hi')
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            else {
                var result = confirm('Sure to clear this container from hold ?')
                if (result == true) {

                }
                else {
                    blResult = blResult && false;
                }
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
</script>
</asp:Content>

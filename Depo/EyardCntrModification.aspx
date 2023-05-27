<%@ Page Title="Depo | Modify Empty yard Container" Language="VB" EnableEventValidation="false" MasterPageFile="~/Depo/PopUp.master" AutoEventWireup="false" CodeFile="EyardCntrModification.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
window.opener.document.getElementById("MainContent_btnIndentItem").click();
self.close();
}
</script>
<div class="container" style="background-color: white">

<div class="panel-body">
<div class="form-group">
<div class="col-md-12 col-xs-12 pull-left" >
<div class="header-lined">
<h1>Modify Empty yard Container<small class="pull-left" style="margin-right:20px"></small></h1>
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                           
</div>

</div>
<div class="row ">
 
<div class="col-sm-4 col-xs-12 ">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtContainerno" Style="text-transform:uppercase;" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>

<div class="col-sm-1 col-xs-2 pull-left">
<div class="form-group" style="padding-top:18px">
<asp:Button ID="btnsearch" class="btn btn-primary btn-sm outline " runat="server" OnClick="btnsearch_Click"  
    OnClientClick="return ValidationSave()"
Text="Show"  />
</div>
                                              
                                      
</div>
</div>



    <div class="row">
<div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b  >Entry ID</b>
<asp:TextBox ID="txtEntry" Style="text-transform:uppercase" ReadOnly="true" class="form-control text-label"  placeholder="Entry ID"
runat="server"   ></asp:TextBox>
</div>
</div>

 <div class="col-sm-3 col-xs-12">                                      
<div class="form-group text-label">
<b>MFG Date</b>                                         
<asp:TextBox ID="txtMFGDate"  placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
</div>
</div>
</div>


    <div class="row">

        <div class="col-sm-4 col-xs-12">                                      
<div class="form-group text-label">
<b>In Date</b>                                         
<asp:TextBox ID="txtIndate" TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
</div>
</div>

        <div class="col-sm-3 col-xs-12">                                      
<div class="form-group text-label">
<b>Do Valid Date</b>
<asp:TextBox ID="txtDoValid"  placeholder="yyyy-mm-dd " TextMode="DateTimeLocal"  runat="server"   Class="form-control text-label"></asp:TextBox>
</div>
</div>

        </div>
        <div class="row">
        <div class="col-sm-3 col-xs-12"  runat="server" >
<div class="form-group text-label">
<b  >ISO Code</b>
<asp:DropDownList ID="ddlISOCode" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>

</div>

            <div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b  >Survery EIR No</b>
<asp:TextBox ID="txtEIR" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Survery EIR No"
runat="server"   ></asp:TextBox>
</div>
</div>
 </div>


<div class="row">
    <div class="col-sm-3 col-xs-12"  runat="server" >
<div class="form-group text-label">
<b  >Container Type</b>
<asp:DropDownList ID="ddlCtrType" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>

</div>
    <div class="col-sm-3 col-xs-12"  runat="server" >
<div class="form-group text-label">
<b  >Status Type</b>
<asp:DropDownList ID="ddlStatus" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
            <asp:ListItem Value="0">-Select-</asp:ListItem> 
<asp:ListItem Value="1">AV</asp:ListItem>
<asp:ListItem Value="2">AE</asp:ListItem>                                         
                                         </asp:DropDownList>
</div>

</div>



    </div>

 
    <div class="row">
    <div class="col-sm-3 col-xs-12"  runat="server" >
<div class="form-group text-label">
<b  >Size</b>
<asp:DropDownList ID="ddlSize" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >

    <asp:ListItem Value="0">-Select-</asp:ListItem> 
<asp:ListItem Value="1">20</asp:ListItem>
<asp:ListItem Value="2">40</asp:ListItem>
<asp:ListItem Value="3">45</asp:ListItem>
                                                 
</asp:DropDownList>
</div>

</div>
    <div class="col-sm-3 col-xs-12"  runat="server" >
<div class="form-group text-label">
<b  >Condition</b>
<asp:DropDownList ID="ddlCondition" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>

</div>



    </div>

    <div class="row">    
        <div class="col-sm-4 col-xs-12"  runat="server" >
<div class="form-group text-label">
<b  >Shipline Name</b>
<asp:DropDownList ID="ddlShipline" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
                                                 
                                         </asp:DropDownList>
</div>

</div>
        </div>
 
     <div class="row">
<div class="col-sm-8 col-xs-12">
<div class="form-group text-label">
<b  >Customer Name</b>
<asp:TextBox ID="txtCustomer" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Customer Name"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>



<div class="row">
<div class="col-sm-8 col-xs-12">
<div class="form-group text-label">
<b  >Transporter</b>
<asp:TextBox ID="txtTransporter" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Transporter Name"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>

<div class="row">
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Process type</b>
<asp:TextBox ID="txtProcess" Style="text-transform:uppercase" ReadOnly="True" class="form-control text-label"  placeholder="Process type"
runat="server"   ></asp:TextBox>
</div>
</div>


    <div class="col-sm-6 col-xs-12" >
<div class="form-group text-label" runat="server">
<b  >CC Weight</b>
<asp:TextBox ID="txtWeight" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="CC Weight"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>

    <div class="row">
<div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b  >Tare Weight</b>
<asp:TextBox ID="txtTare" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Tare Weight"
runat="server"   ></asp:TextBox>
</div>
</div>


    <div class="col-sm-6 col-xs-12" >
<div class="form-group text-label" runat="server">
<b  >Booking No</b>
<asp:TextBox ID="txtBookingNo" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Booking No"
runat="server"   ></asp:TextBox>
</div>
</div>

</div>

    <div class="row">
<div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b  >Carrying Capacity</b>
<asp:TextBox ID="txtCarring" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Carrying Capacity"
runat="server"   ></asp:TextBox>
</div>
</div>


    <div class="col-sm-5 col-xs-12" style="display:none" >
<div class="form-group text-label" runat="server">
<b  >Location</b>
<asp:TextBox ID="txtLocation" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Location"
runat="server"   ></asp:TextBox>
</div>
</div>
</div>

    <div class="row">
  <div class="col-sm-4 col-xs-12"  runat="server" >
<div class="form-group text-label">
<b  >Movement Type</b>
<asp:DropDownList ID="ddlMovement" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control " >
    <asp:ListItem Value="">-Select-</asp:ListItem> 
<asp:ListItem Value="Depot">Depot</asp:ListItem>
<asp:ListItem Value="Party">Party</asp:ListItem>
 </asp:DropDownList>
</div>

</div>

            <div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b  >Remarks</b>
<asp:TextBox ID="txtRemarks" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Remarks"
runat="server"   ></asp:TextBox>
</div>
</div>
 </div>
    <div class="row">

        <div class="col-sm-4 col-xs-12" style="display:none">
<div class="form-group text-label">
<b  >Truck No</b>
<asp:TextBox ID="txtTruck" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Truck No"
runat="server"   ></asp:TextBox>
</div>
</div>


    <div class="col-sm-5 col-xs-12" >
<div class="form-group text-label" runat="server">
<b  >Damage Remarks</b>
<asp:TextBox ID="txtDamage" Style="text-transform:uppercase"  class="form-control text-label"  placeholder="Damage Remarks"
runat="server"   ></asp:TextBox>
</div>
</div>
        </div>

    <div class="row">
            <div class="col-sm-4 col-xs-12" >
<div class="form-group text-label" runat="server">
<b  >New Container No</b>
<asp:TextBox ID="txtNewContainerNo" Style="text-transform:uppercase" OnTextChanged="txtNewContainerNo_TextChanged" AutoPostBack="true" MaxLength="11"  class="form-control text-label"  placeholder="New Container No"
runat="server"   ></asp:TextBox>

    <asp:TextBox ID="txtOldDetails" Style="text-transform:uppercase;display:none"  class="form-control text-label"  placeholder="New Container No"
runat="server"   ></asp:TextBox>
</div>
</div>
        </div>

</div>

    <asp:Label ID="lblEntryID" Visible="false" runat="server" Text=""></asp:Label>

            <div class="row">
    <div class="col-sm-1 col-xs-1">
<div class="form-group" >
<asp:Button ID="btnUpdate" class="btn btn-primary btn-sm outline  " runat="server" OnClick="btnUpdate_Click"  
Text="Update"  OnClientClick="return ValidationUpdate()"  />
</div>
                                              
                                      
</div>
                <div class="col-sm-1" style="padding-left:26px;">
<div class="form-group" >                          
<a href="EyardCntrModification.aspx" id="btnclear" runat="server" class="btn btn-primary btn-sm outline ">
Clear
</a>                               
</div>                                                                               
</div>
                </div>

<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
         
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
                   
<a href="EyardCntrModification.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
                
</div>
</div>
     
    <div class="modal fade control-label" id="myModalforupdate1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
 
<div class="modal-content">
<div class="modal-header">
<center>
<h4 class="modal-title">
<asp:Label ID="lblquoteApprove"  CssClass="control-label" Font-Bold="true" runat="server" Text=""></asp:Label>
</h4>
</center>
</div>
<div class="modal-footer">
<button class="btn btn-info " id="btncancelyes" data-dismiss="modal" runat="server"   aria-hidden="true">
Yes 
</button>
<a href="EyardCntrModification.aspx" class="btn btn-danger ">No</a>
</div>
</div>
 
</div>
</div>             

</div>
</div>
</div>
<script type="text/javascript">
function ValidationSave() {
                 
    var txtContainerno = document.getElementById('<%= txtContainerno.ClientID%>').value;


    var blResult = Boolean;
    blResult = true;


    if (txtContainerno == "") {
        document.getElementById('<%= txtContainerno.ClientID%>').style.borderColor = "red";
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
         function ValidationUpdate() {
             var txtContainerno = document.getElementById('<%= txtContainerno.ClientID%>').value;
           
            var blResult = Boolean;
            blResult = true;



            if (txtContainerno == 0) {
                document.getElementById('<%= txtContainerno.ClientID%>').style.borderColor = "red";
        blResult = blResult && false;

    }
   
            //alert('hi')
            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            else {
                var result = confirm('Are you sure to Update ?')
                if (result == true) {

                }
                else {
                    blResult = blResult && false;
                }
            }
            return blResult;
        }
</script>
</asp:Content>



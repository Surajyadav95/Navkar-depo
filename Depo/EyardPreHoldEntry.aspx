<%@ Page Title="Depo |Pre-Hold Entry" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EyardPreHoldEntry.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo |Pre-Hold Entry</title>
       
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

<i class="glyphicon glyphicon-transfer"></i>Pre-Hold Entry 
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
              <div class="col-sm-4 col-xs-12" >
<div class="form-group text-label">
    <b>Container No</b>
<asp:TextBox  ID="txtcontainer" MaxLength="11"  placeholder="Container No" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>  
      <div class="col-sm-1 col-xs-12" style="padding-top:25px">
          <b>OR</b>
      </div>
       <div class="col-sm-4 col-xs-12" >
<div class="form-group text-label">
    <b>Booking No</b>
<asp:TextBox  ID="txtBookingNo"   placeholder="Booking No" Style="text-transform: uppercase;" runat="server" class="form-control text-label">
                                      
</asp:TextBox>
</div>
</div>
      </div>


    <div class="row">
         <div class="col-md-6 col-xs-8">
<div class="form-group text-label">
<b  >Hold Reasons</b>
<asp:DropDownList ID="ddlHoldReason" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
</asp:DropDownList>
</div>

</div>
<div class="col-sm-3 col-xs-12" >
        <div class="form-group text-label">
        <b>Hold Type</b>
        <asp:DropDownList  ID="ddlHoldtype"  Style="text-transform: uppercase;" runat="server" class="form-control text-label">    
        <asp:ListItem Value="0">--Select--</asp:ListItem> 
        <asp:ListItem Value="1">IN</asp:ListItem>
        <asp:ListItem Value="2">Out</asp:ListItem> 
                                  
        </asp:DropDownList>
        </div>
        </div>

</div>

        <div class="row">
           <div class="col-md-4 col-xs-12">                                      
<div class="form-group date text-label">
    <b>Hold Date</b>

<asp:TextBox ID="txtHoldDate" placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>

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
   <div class="col-md-1">
<div class="form-group"  >
<asp:Button ID="btnHold" class="btn btn-primary btn btn-sm outline " OnClientClick=" return ValidationHold()"  runat="server"
Text="Hold"     />
</div>
    </div>   
          <div class="col-md-2" >
<div class="form-group"  >
<asp:Button ID="btnClearHold" class="btn btn-primary btn btn-sm outline " OnClientClick=" return ValidationClearHold()"  runat="server"
Text="Release"     />
</div>
    </div>  
        <div class="col-md-1"  style="margin-left:-40px" >
<div class="form-group"  >
<asp:Button ID="btnClear" class="btn btn-primary btn btn-sm outline "   runat="server"
Text="Clear"/>
</div>
    </div>   
     
        </div>
       

  
 
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
                   
<a href="EyardPreHoldEntry.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
               
</fieldset>
    </div>
    <div class="col-md-4 col-xs-12 pull-md-right sidebar" >

<div menuitemname="Client Details" class="panel panel-sidebar" style="height:373px">
   
<div class="panel-body">
<asp:UpdatePanel ID="upModalSave1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUpload" />
           
    </Triggers>
<ContentTemplate>
    <div class="row">
        <div class="form-group">
            <b><asp:Label ID="lblname" runat="server" ForeColor="Blue" text="Upload"></asp:Label></b>                             

        </div>
    </div>
<div class="row text-label">
    <div class="col-md-6 col-xs-12">
<div class="form-group text-label">
    <b>Process</b>
<asp:DropDownList ID="ddlHoldorRelease" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
    <asp:ListItem Value="0">--Select--</asp:ListItem>
    <asp:ListItem Value="Hold">Hold</asp:ListItem>
    <asp:ListItem Value="Release">Release</asp:ListItem>

</asp:DropDownList>
</div>

</div>

</div>
    <div class="row text-label">
        <div class="col-md-12 col-xs-12">
<div class="form-group text-label">
<b  >Hold Reasons</b>
<asp:DropDownList ID="ddlHoldReasonImport" Style="text-transform: uppercase;border-radius:4px"  runat="server" class="form-control " >
</asp:DropDownList>
</div>

</div>
    </div>
    
    <div class="row">
     <asp:Label ID="Lblfile" runat="server" style="display:none" Text="" ForeColor="red"></asp:Label>

 <div class="col-sm-9 col-xs-12 text-label" style="padding-top:12px">
     <label runat="server"  style="width:0px; margin-left:10px;" >
            <a style="display:block">
 <asp:FileUpload ID="FileUpload1" AllowMultiple="false"  runat="server" ClientIDMode="Static" /></a>
                                       </label>
       </div>
        
    <div class="col-sm-3 col-xs-12" style="padding-top:5px">
                                <asp:Button ID="btnUpload" class="btn btn-success btn-sm outline"  Text="Import" runat="server" OnClientClick="return ClassChange()" onclick="btnUpload_Click"    />  
     <asp:postbacktrigger controlid="btnUpload" xmlns:asp="#unknown"  />
        <b><asp:Label runat="server" ID="lblfilename" Text=""></asp:Label></b>
        </div>

    

    <asp:HiddenField ID="hffile" runat="server" value="" />
                               <asp:HiddenField ID="hdExist" runat="server" value="0" />
        </div>
</ContentTemplate>
</asp:UpdatePanel>  
    <br />
               <div class="col-md-2 pull-right" >
<div class="form-group"  >
<asp:LinkButton runat="server" ID="lnkDownloadExcel" OnClick="lnkDownloadExcel_Click" CssClass="btn btn-info btn-sm" ToolTip="Download Template"><i class="fa fa-download"></i></asp:LinkButton>
</div>
    </div>         
</div>
                      
</div>

</div>
</div>
    
    
</div>
                               
</div>
      

</div>
       
         
</div>
   
    <script>
        function ClassChange() {
            var ddlHoldorRelease = document.getElementById('<%= ddlHoldorRelease.ClientID%>').value;
            var ddlHoldReasonImport = document.getElementById('<%= ddlHoldReasonImport.ClientID%>').value;

            var blResult = Boolean;
            blResult = true;

            document.getElementById('<%= btnUpload.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btnUpload.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline disabled");

            if (ddlHoldorRelease == 0) {
                document.getElementById('<%= ddlHoldorRelease.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }

            if (ddlHoldReasonImport == 0) {
                document.getElementById('<%= ddlHoldReasonImport.ClientID%>').style.borderColor = "red";
                blResult = blResult && false;
            }
            if (blResult == false) {
                document.getElementById('<%= btnUpload.ClientID%>').value = "Import";
                document.getElementById('<%= btnUpload.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline");
                alert('Please fill the required fields!');
            }
            return blResult;
        }
    </script>
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

<%@ Page Title="Depo | Mty Pre Planning" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="MtyPrePlannig.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Mty Pre Planning .</title>
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Mty Pre Planning
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     

<div class="panel-body" >
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
    <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
        <div class="panel-heading">
<h3 class="panel-title">
Upload Planning Details                
<%--<i class="fa fa-chevron-up panel-minimise pull-right"></i>--%>
</h3>
</div>
        <div class="panel-body">
            <div class="row">        
<asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional"> 
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUpload" />           
    </Triggers>
<ContentTemplate>

     
     <asp:Label ID="Lblfile" runat="server" style="display:none" Text="" ForeColor="red"></asp:Label>

 <div class="col-md-5 col-xs-12 text-label" style="padding-top:12px">
     <label runat="server"  style="width:0px; margin-left:10px;" >
            <a style="display:block">
 <asp:FileUpload ID="FileUpload1" AllowMultiple="false"  runat="server" ClientIDMode="Static" /></a>
                                       </label>
       </div>
        
    <div class="col-md-1 col-xs-12" style="padding-top:5px">
                                <asp:Button ID="btnUpload" class="btn btn-success btn-sm outline"  Text="Import" runat="server" OnClientClick="return ClassChange()" onclick="btnUpload_Click"    />  
     <asp:postbacktrigger controlid="btnUpload" xmlns:asp="#unknown"  />
        <b><asp:Label runat="server" ID="lblfilename" Text=""></asp:Label></b>
        </div>

    

    <asp:HiddenField ID="hffile" runat="server" value="" />
                               <asp:HiddenField ID="hdExist" runat="server" value="0" />
                                                 

</ContentTemplate>
</asp:UpdatePanel>
    <div class="col-md-2 col-xs-12" >
<div class="form-group" style="padding-top:5px" >
<asp:LinkButton runat="server" ID="lnkDownloadExcel" OnClick="lnkDownloadExcel_Click" CssClass="btn btn-info btn-sm" ToolTip="Download Template"><i class="fa fa-download"></i></asp:LinkButton>
</div>
    </div> 
           </div> 
        </div>
    </div>
         <div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
             <div class="panel-heading">
<h3 class="panel-title">
Empty Planning Summary
</h3>
</div>
             <div class="panel-body">
                 <div class="row">
<div class="col-md-6 col-xs-12">                                      
<div class="form-group date text-label">
Date                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy"  runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>
</div>                                       
</div>
        <div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b  >Container No</b>
<asp:TextBox ID="txtContainerNo" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Container No"
runat="server"   ></asp:TextBox>
</div>
</div>
        <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
    </div> 
    </div>
                 <div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="row">
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="true" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grdcontainer_PageIndexChanging"  AllowPaging="true" PageSize="7" >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>


</Columns>

</asp:GridView>
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
                   
<a href="MtyPrePlannig.aspx" class="btn btn-info btn-block">OK</a>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>
</div>
         
</div>
    <script>
        function ClassChange() {
            

            var blResult = Boolean;
            blResult = true;

            document.getElementById('<%= btnUpload.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btnUpload.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline disabled");

            
            if (blResult == false) {
                document.getElementById('<%= btnUpload.ClientID%>').value = "Import";
                document.getElementById('<%= btnUpload.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline");
                alert('Please fill the required fields!');
            }
            return blResult;
        }
    </script>
<%-- <script type="text/javascript">
function checkRadioBtn(id) {
var gv = document.getElementById('<%=grdcontainer.ClientID%>');

for (var i = 1; i < gv.rows.length; i++) {
var radioBtn = gv.rows[i].cells[0].getElementsByTagName("input");

// Check if the id not same
if (radioBtn[0].id != id.id) {
radioBtn[0].checked = false;
}
}
}
</script>--%>
<%--  <script type="text/javascript">
 
function BondExPrint() {
            
var NOCNo1= document.getElementById('<%= txtNOCNo.ClientID%>').value;
             
var url = "../Report_Bond/BondEx_logo_print.aspx?NOCNo=" + NOCNo1;
//alert("hi")
                
window.open(url);

}


</script>--%>
</asp:Content>

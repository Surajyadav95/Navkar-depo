<%@ Page Title="Depo | Estimate Repair Entry" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="EstimateRepairEntry.aspx.vb" Inherits="Summary_BCYMovement" Culture="en-GB" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Estimate Repair Entry</title>
</head>
    <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            max-height:380px;
            overflow:auto
        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Estimate Repair Entry
</h3>
           
</div>
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body">
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" />
                <asp:PostBackTrigger ControlID="btnExport" />

            </Triggers>
            <ContentTemplate>

                                                  
<div class="row">  

<div class="col-md-2 col-xs-12" >
<div class="form-group text-label">
Search On
<asp:DropDownList ID="ddlcriteria" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">All</asp:ListItem> 
<asp:ListItem Value="1">Shipping Line</asp:ListItem>
<asp:ListItem Value="2">Container No</asp:ListItem>

</asp:DropDownList>                                               
</div>
</div>

<div class="col-md-4 col-xs-12"  runat="server" id="divShippingLine" style="display:none">
<div class="form-group text-label">
<b>Shipping Line</b>
<asp:DropDownList ID="ddlLineName" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control"></asp:DropDownList>
</div>
</div>
    <div class="col-md-4 col-xs-12"  runat="server" id="divContainer" style="display:none">
<div class="form-group text-label">
<b>Container No</b>
<asp:TextBox runat="server" MaxLength="11" ID="txtContainerNo" CssClass="form-control" Style="text-transform: uppercase;" ></asp:TextBox>
</div>
</div>
    <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline  " runat="server"
OnClick="btnSave_Click" 
Text="Search"     />
</div>
    </div> 
    <div class="col-md-2 col-xs-12 ">
                                <div class="form-group " style="padding-top: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
                                </div>
                            </div>                                        
                                      
</div>
                 </ContentTemplate>
        </asp:UpdatePanel> 
    <div class="row">
    <div class="col-lg-12 col-xs-12 text-label ">

<div class="col-sm-5 col-xs-12 pull-right">

<div class="col-sm-3 col-xs-12">
<b>20:</b>
<asp:Label runat="server" ID="lbl20" ></asp:Label>
&nbsp</div>
<div class="col-sm-3 col-xs-12">
<b>40:</b>
<asp:Label runat="server" ID="lbl40" ></asp:Label>
&nbsp</div>
<div class="col-sm-3 col-xs-12">
<b>45:</b>
<asp:Label runat="server" ID="lbl45" ></asp:Label>
&nbsp</div>
    <div class="col-sm-3 col-xs-12">
<b>Teus:</b>
<asp:Label runat="server" ID="lblTEUS" ></asp:Label>
&nbsp</div>
    
</div>
    </div>
    </div>
    <br />


 
<div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive scrolling-table-container">
<asp:GridView ID="grdRegistrationSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true">

<Columns>
 <asp:TemplateField>
     <ItemTemplate>
         <asp:CheckBox runat="server" ID="chkSelect" Checked="false" />
     </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" />
 </asp:TemplateField>
         <asp:TemplateField >
             <ItemTemplate>
                <asp:LinkButton ID="lnkRepair" ControlStyle-CssClass='btn btn-danger btn-xs outline' Text="Repair" OnClick="lnkRepair_Click"                                                            
                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EntryID")%>' runat="server" 
                ></asp:LinkButton>
                <asp:Label runat="server" ID="lblEntryID" Visible="false" Text='<%#Eval("EntryID")%>'></asp:Label>
                <asp:Label runat="server" ID="lblEstimateID" Visible="false" Text='<%#Eval("Estimate_ID")%>'></asp:Label>

             </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" />
         </asp:TemplateField>                                          
<asp:BoundField DataField="#" HeaderText="#" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:TemplateField HeaderText="Container No" HeaderStyle-CssClass="header-center">
             <ItemTemplate>
                <asp:Label runat="server" ID="lblContainerNo" Text='<%#Eval("Container No")%>'></asp:Label>

             </ItemTemplate>
     <ItemStyle HorizontalAlign="Center" />
         </asp:TemplateField>
    <asp:BoundField DataField="Size" HeaderText="Size" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
<asp:BoundField DataField="Container Type" HeaderText="Container Type" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
    <asp:BoundField DataField="In Date" HeaderText="In Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>

<asp:BoundField DataField="Shipping Line" HeaderText="Shipping Line" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Left"></asp:BoundField>
 <asp:BoundField DataField="Estimate Date" HeaderText="Estimate Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
      <asp:BoundField DataField="Estimate Amount" HeaderText="Estimate Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Right"></asp:BoundField>      
      <asp:BoundField DataField="Approved Date" HeaderText="Approved Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
      <asp:BoundField DataField="Approved Amount" HeaderText="Approved Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Right"></asp:BoundField>      
      <asp:BoundField DataField="In Dwell Days" HeaderText="In Dwell Days" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      

        <asp:BoundField DataField="Approved Dwell Days" HeaderText="Approved Dwell Days" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center"></asp:BoundField>      
     
</Columns>

</asp:GridView>
</div>
</div>
</div>
 
    <br />
    <div class="row">

        <asp:Label ID="Lblfile" runat="server" style="display:none" Text="" ForeColor="red"></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
   <ContentTemplate>
    <div class="col-sm-5 col-xs-12">
 <div class="col-sm-6 col-xs-12 text-label" style="padding-top:25px">
     <label runat="server"  style="width:0px; margin-left:10px;" >
            <a style="display:block">
 <asp:FileUpload ID="FileUpload1" AllowMultiple="false"  runat="server" ClientIDMode="Static" /></a>
                                       </label>
       </div>
    <div class="col-sm-2 col-xs-12" style="padding-top:20px">
                                <asp:Button ID="btnUpload" class="btn btn-success btn-sm outline"  Text="Import" runat="server" OnClientClick="return ClassChange()" onclick="btnUpload_Click"    />  
     <asp:postbacktrigger controlid="btnUpload" xmlns:asp="#unknown"  />
        <b><asp:Label runat="server" ID="lblfilename" Text=""></asp:Label></b>
        </div>

     <div class="col-sm-2 col-xs-12 text-label" style="padding-top:20px" >
                                 <div class="form-group " >
                            <asp:Button ID="Button1" class="btn btn-primary btn-sm outline" runat="server"  CommandName="MoveNext" OnClick="Button1_Click" 
                                Text="Clear"  onclientclick="return confirm('Are you sure to Clear?')"  />
                        </div>         
                                    </div>
        <div class="col-md-2 col-xs-12" >
<div class="form-group" style="padding-top:20px" >
<asp:LinkButton runat="server" ID="lnkDownloadExcel" OnClick="lnkDownloadExcel_Click" CssClass="btn btn-info btn-sm" ToolTip="Download Template"><i class="fa fa-download"></i></asp:LinkButton>
</div>
    </div> 
    <asp:HiddenField ID="hffile" runat="server" value="" />
                               <asp:HiddenField ID="hdExist" runat="server" value="0" />
        </div>
       </ContentTemplate>
             <Triggers>
        <asp:PostBackTrigger ControlID="btnUpload" />
           <asp:PostBackTrigger  ControlID="Button1" />
           <asp:PostBackTrigger  ControlID="lnkDownloadExcel" />

    </Triggers>

        </asp:UpdatePanel>
        <div class="col-md-2"></div>
        <div class="col-md-3 col-xs-12">
        <div class="form-group text-label">
        <b  >Repair Date</b>
        <asp:TextBox ID="txtRepairDate" TextMode="DateTimeLocal" style="text-transform:uppercase" runat="server"  Class="form-control text-label"></asp:TextBox>
        </div>
        </div>
        <div class="col-md-2 col-xs-12 ">
                                <div class="form-group" style="padding-top: 20px">
                                    <asp:Button ID="btnMultipleRepair" runat="server"
                                        class="btn btn-info btn-sm outline" Text="Update Repair" ></asp:Button>
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
                 <button class="btn btn-info btn-block" id="SaveOk" data-dismiss="modal" runat="server" onserverclick="SaveOk_ServerClick">OK</button>     
<%--<a href="EstimationEntry.aspx" class="btn btn-info btn-block">OK</a>--%>
                                
</div>
</div>
                    
</ContentTemplate>
             
</asp:UpdatePanel>
</div>
</div>                     
                     
                       
                       
</div>
                 

</div>
         
</div>
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
    <script>
        function ClassChange() {
            document.getElementById('<%= btnUpload.ClientID%>').value = "Please Wait...";
            document.getElementById('<%= btnUpload.ClientID%>').setAttribute("class", "btn btn-success btn-sm outline disabled");
        }
    </script>
</asp:Content>

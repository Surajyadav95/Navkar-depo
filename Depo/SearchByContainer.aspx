<%@ Page Title="Depo | Search By Container" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="SearchByContainer.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <head>
<title>Depo | Search By Container</title>
       
</head>
    <style>
        .nav-tabs>li.active>a, .nav-tabs>li.active>a:focus, .nav-tabs>li.active>a:hover{
            background-color:orange
        }
        .header-center{
            text-align:center
        }
    </style>
<div class="page-container">
<div class="pageheader">
            
<h3>

<i class="glyphicon glyphicon-transfer"></i>  Search By Container
</h3>
           
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<ContentTemplate>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-12 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
           
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 
<div class="row">
<div class="col-sm-2 col-xs-12" >
<div class="form-group text-label" style="text-decoration-color:black">
<b >Container No</b>
<asp:TextBox ID="txtContainerNo" AutoPostBack="true" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Container No" MaxLength="11"></asp:TextBox>     
</div>
</div>
    <div class="col-sm-1  col-xs-12" >
<div class="form-group" style="padding-top:20px;" >
<asp:LinkButton class="btn btn-primary btn-sm outline" ID="lnkSearch" runat="server" OnClientClick="return OpenContainerList();">
<i class="fa fa-search"></i></asp:LinkButton>
    <asp:Button ID="btnSearchbyContainer" runat="server" Text="Call Button Item Click" style="display:none" OnClick="btnSearchbyContainer_Click" />                                                                               
</div>
</div>
    
    <div class="col-sm-1 col-xs-12" >
<div class="form-group text-label">
    <b>Entry ID</b>
<asp:DropDownList runat="server" ID="ddlEntryID" CssClass="form-control"></asp:DropDownList>   
</div>
</div>
    <div class="col-sm-1  col-xs-12" >
<div class="form-group" style="padding-top:20px;" >
    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-info btn-sm" Text="Search" OnClick="btnSearch_Click" />                                                                               
</div>
</div>
    <asp:Panel runat="server" Enabled="false">
    <div class="col-sm-1 col-xs-12">
<div class="form-group text-label">
<b >Size</b>
<asp:TextBox ID="txtSize" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Size"></asp:TextBox>     
</div>
</div>
    <div class="col-sm-1 col-xs-12">
<div class="form-group text-label">
<b >Type</b>
<asp:TextBox ID="txtCType" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Type"></asp:TextBox>     
</div>
</div>

        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Tare Weight</b>
<asp:TextBox ID="txtTareWeight" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Tare Weight"></asp:TextBox>     
</div>
</div>
         <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Gross Weight</b>
<asp:TextBox ID="txtGrossWeight" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Gross Weight"></asp:TextBox>     
</div>
</div>
        </asp:Panel>
    </div>
    <asp:Panel runat="server" Enabled="false">
    <div class="row">
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >In Date & Time</b>
<asp:TextBox ID="txtInDateTime" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="In Date & Time"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >ISO Code</b>
<asp:TextBox ID="txtISOCode" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="ISO Code"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >CC Wt</b>
<asp:TextBox ID="txtCCWt" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="CC Weight"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Truck No</b>
<asp:TextBox ID="txtTruckNo" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Truck No"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Transporter</b>
<asp:TextBox ID="txtTransporter" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Transporter Name"></asp:TextBox>     
</div>
</div>
    </div>
   
    <div class="row">
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Booking No</b>
<asp:TextBox ID="txtBookingNo" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Booking No"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Location</b>
<asp:TextBox ID="txtLocation" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Location"></asp:TextBox>     
</div>
</div>
    
        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Condition</b>
<asp:TextBox ID="txtCondition" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Condition"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Survey EIR No</b>
<asp:TextBox ID="txtSurvyeEIRNo" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Survey EIR No"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-1 col-xs-12">
<div class="form-group text-label">
<b >Status</b>
<asp:TextBox ID="txtStatus" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Status"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-2 col-xs-12">
<div class="form-group text-label">
<b >Entry Type</b>
<asp:TextBox ID="txtentryType" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Entry Type"></asp:TextBox>     
</div>
</div>
    </div> 
    
    <div class="row">
        <div class="col-md-7 col-xs-12">
<div class="form-group text-label">
<b  >Shipping Line</b>
    <asp:TextBox ID="txtShippingLine" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Shipping Line"></asp:TextBox> 
</div>
</div>
   
        <div class="col-md-5 col-xs-12">
<div class="form-group text-label">
<b  >Shipper Name</b>
<asp:TextBox ID="txtShipperName" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Shipper Name"></asp:TextBox> 
</div>
</div>
    </div>
    <div class="row">
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >M.F.G. Date</b>
<asp:TextBox ID="txtMFGDate" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="MFG Date"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b >Damage Remark</b>
<asp:TextBox ID="txtDamageRemark" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Damage Remark"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-1"></div>
        <div class="col-sm-3 col-xs-12" id="divHoldImage" runat="server" style="display:none">
            <img src="../img/on-hold1.png" width="150" height="77" />
        </div>
    </div> 
    <div class="row">
        <div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b >Remark</b>
<asp:TextBox ID="txtRemark" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Remark"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Generated By</b>
<asp:TextBox ID="txtGeneratedBy" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Generated By"></asp:TextBox>     
</div>
</div>
    </div> 
<div class="row">
<div class="col-sm-12 col-xs-12 text-label ">
<div class="panel panel-default">
<div id="Tabs" role="tabpanel">
    <ul class="nav nav-tabs" role="tablist" style="background-color:#64A5FE">
<li class="active"><a href="#ChargesDetails" aria-controls="Charges Details" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Charges Details</a></li>

<li><a href="#HoldDetails" aria-controls="Hold Details" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Hold Details</a></li>

<li><a href="#AmmendmentLog" aria-controls="Ammendment Log" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Ammendment Log</a></li>

        <li><a href="#EstimateDetails" aria-controls="Estimate Details" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Estimate Details</a></li>

        <li><a href="#ApproveDetails" aria-controls="Approve Details" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Approve Details</a></li>

        <li><a href="#RepairDetails" aria-controls="Repair Details" role="tab" data-toggle="tab" style="color:white;border-radius:20px">Repair Details</a></li>
        </ul>
    <div class="tab-content" style="padding-top:20px">
        <div role="tabpanel" class="tab-pane active" id="ChargesDetails">
            <asp:UpdatePanel runat="server" ID="ChargesUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>              
            <div class="row">
<div class="col-lg-10 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdChargesDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<Columns>
 
                                                   
<asp:BoundField DataField="Assess No" HeaderText="Assess No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="left"></asp:BoundField>
<asp:BoundField DataField="Assess Date" HeaderText="Assess Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="Receipt No" HeaderText="Receipt No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Receipt Date" HeaderText="Receipt Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="Party Id" HeaderText="Party Id" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="Party Name" HeaderText="Party Name" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="Prepared By" HeaderText="Prepared By" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>

</Columns>

</asp:GridView>
</div>
</div>
</div>
                     </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div role="tabpanel" class="tab-pane" id="HoldDetails">
                 <asp:UpdatePanel runat="server" ID="HoldUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>              
            <div class="row">
<div class="col-lg-10 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdHoldDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<Columns>
 
                                                   
<asp:BoundField DataField="Hold Reason" HeaderText="Hold Reason" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="left"></asp:BoundField>
<asp:BoundField DataField="Hold By" HeaderText="Hold By" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="Hold Date" HeaderText="Hold Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Cleared By" HeaderText="Cleared By" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="Cleared On" HeaderText="Cleared On" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    

</Columns>

</asp:GridView>
</div>
</div>
</div>
                     </ContentTemplate>
            </asp:UpdatePanel>        
        </div>
        <div role="tabpanel" class="tab-pane" id="AmmendmentLog">
             <asp:UpdatePanel runat="server" ID="AmmendmentUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>              
            <div class="row">
<div class="col-lg-8 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdAmmendmentLog" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<Columns>
 
                                                   
<asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="left"></asp:BoundField>
<asp:BoundField DataField="Modified By" HeaderText="Modified By" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
    <asp:BoundField DataField="Modified On" HeaderText="Modified On" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>

    

</Columns>

</asp:GridView>
</div>
</div>
</div>
                     </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div role="tabpanel" class="tab-pane" id="EstimateDetails">
             <asp:UpdatePanel runat="server" ID="EstimateUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>              
            <div class="row">
<div class="col-lg-10 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdEstimateDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<Columns>
 
                                                   
<asp:BoundField DataField="Sr.No" HeaderText="Sr.No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="left"></asp:BoundField>
<asp:BoundField DataField="Descriptions" HeaderText="Descriptions" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Man Hours" HeaderText="Man Hours" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Man Cost" HeaderText="Man Cost" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Material Cost" HeaderText="Material Cost" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Estimate Amount" HeaderText="Estimate Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
   

</Columns>

</asp:GridView>
</div>
</div>
</div>
                     </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div role="tabpanel" class="tab-pane" id="ApproveDetails">
              <asp:UpdatePanel runat="server" ID="ApproveUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>              
            <div class="row">
<div class="col-lg-10 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdApprovedDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<Columns>
 
                                                   
<asp:BoundField DataField="Sr.No" HeaderText="Sr.No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="left"></asp:BoundField>
<asp:BoundField DataField="Descriptions" HeaderText="Descriptions" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Man Hours" HeaderText="Man Hours" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Man Cost" HeaderText="Man Cost" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Material Cost" HeaderText="Material Cost" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Approved Amount" HeaderText="Approved Amount" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="UserName" HeaderText="UserName" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>

   

</Columns>

</asp:GridView>
</div>
</div>
</div>
                     </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div role="tabpanel" class="tab-pane" id="RepairDetails">
             <asp:UpdatePanel runat="server" ID="RepairUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>              
            <div class="row">
<div class="col-lg-12 col-xs-12 text-label ">
<div class="table-responsive">
<asp:GridView ID="grdRepairDetails" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true" >
<Columns>
 
                                                   
<asp:BoundField DataField="Sr.No" HeaderText="Sr.No" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="left"></asp:BoundField>
<asp:BoundField DataField="Descriptions" HeaderText="Descriptions" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Man Hours" HeaderText="Man Hours" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Man Cost" HeaderText="Man Cost" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Material Cost" HeaderText="Material Cost" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Repaired Date" HeaderText="Repaired Date" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Repaired By" HeaderText="Repaired By" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>
<asp:BoundField DataField="Repaired On" HeaderText="Repaired On" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="right"></asp:BoundField>


   

</Columns>

</asp:GridView>
</div>
</div>
</div>
                     </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </div>
    </div>
    </div>
    </div>   
              <div class="row">
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Survey Type</b>
<asp:TextBox ID="txtSurveyType" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Survey Type"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >CSC/ASP</b>
<asp:TextBox ID="txtCSCASP" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="CSC/ASP"></asp:TextBox>     
</div>
</div>
    </div> 
    <div class="row">
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Estimate Date</b>
<asp:TextBox ID="txtEstimateDate" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Estimate Date"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Repair Date</b>
<asp:TextBox ID="txtRepairDate" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Repair Date"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Approve Date</b>
<asp:TextBox ID="txtApproveDate" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Approve Date"></asp:TextBox>     
</div>
</div>
    </div>  
    <div class="row">
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Out Date & Time</b>
<asp:TextBox ID="txtOutDate" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Out Date & Time"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Truck No</b>
<asp:TextBox ID="txtTruckNoOut" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Truck No"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Transporter</b>
<asp:TextBox ID="txtTransporterOut" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Transporter"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Out Shipper</b>
<asp:TextBox ID="txtOutShipper" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Out Shipper"></asp:TextBox>     
</div>
</div>
    </div>  
             <div class="col-sm-2 col-xs-12" style="display:none">
        <asp:TextBox runat="server" ID="txtLotNo"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtgatepass"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtEyardInPrint"></asp:TextBox>
             <asp:TextBox runat="server" ID="txtEyardRecipt"></asp:TextBox>

    </div>
    <div class="row">
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Booking No</b>
<asp:TextBox ID="txtBookingNoOut" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Booking No"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Seal No</b>
<asp:TextBox ID="txtSealNo" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Seal No"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Location</b>
<asp:TextBox ID="txtLocationOut" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Location"></asp:TextBox>     
</div>
</div>
    </div>   
     <div class="row">
        <div class="col-sm-6 col-xs-12">
<div class="form-group text-label">
<b >Remark</b>
<asp:TextBox ID="txtRemarkOut" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Remark"></asp:TextBox>     
</div>
</div>
        <div class="col-sm-3 col-xs-12">
<div class="form-group text-label">
<b >Generated By</b>
<asp:TextBox ID="txtGeneratedByOut" Style="text-transform: uppercase;" class="form-control text-label form-cascade-control"
runat="server" Text="" placeholder="Generated By"></asp:TextBox>     
</div>
</div>

    
    </div>    

        
        </asp:Panel> 
    <div class="row">
     <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <asp:Button ID="btngate" class="btn btn-primary  btn-sm outline"  OnClientClick="return OpenEyardInPrint()"  runat="server"  
        Text="Gate Pass" />
        </div>                                  
        </div>

      <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <asp:Button ID="Button1" class="btn btn-primary  btn-sm outline"  OnClientClick="return OpenEyardPrint()"  runat="server"  
        Text="Receipt" />
        </div>                                  
        </div>

      <div class="col-sm-1">
        <div class="form-group" style="padding-top:20px">
        <asp:Button ID="Button2" class="btn btn-primary  btn-sm outline" OnClientClick="return OpenGatePassPrint()"  OnClick="Button2_Click"  runat="server"  
        Text="Gate Pass Out" />
        </div>                                  
        </div>
        </div>                      
</asp:Panel>                        
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
                   
<a href="AccountMaster.aspx" class="btn btn-info btn-block">OK</a>
                                
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
                 
</ContentTemplate>
</asp:UpdatePanel>
</div>
       
         
</div>
   
<%--<script type="text/javascript">
function ValidationSave() {
                 
var txtAccountName = document.getElementById('<%= txtAccountName.ClientID%>').value;
                   
               

var blResult = Boolean;
blResult = true;
 

                   
if (txtAccountName == "") {
document.getElementById('<%= txtAccountName.ClientID%>').style.borderColor = "red";
blResult = blResult && false;

}
 
//alert('hi')
if (blResult == false) {
alert('Please fill the required fields!');
}
return blResult;
}
</script>--%>

      <script type="text/javascript">
          var popup;
          function OpenEyardInPrint() {

              var txtEyardInPrint = document.getElementById('<%= txtEyardInPrint.ClientID%>').value;
            var txtEyardRecipt = document.getElementById('<%= txtEyardRecipt.ClientID%>').value;

            var url = "../Depo/Report_Epic/EyardInwoInvoice.aspx?GateInNo=" + txtEyardInPrint
            window.open(url);
            //var url1 = "../Depo/Report_Epic/EyardInPrint.aspx?GateInNo=" + txtEyardRecipt
            //window.open(url1);


        }

</script>

     <script type="text/javascript">
         var popup;
         function OpenEyardPrint() {

             var txtEyardInPrint = document.getElementById('<%= txtEyardInPrint.ClientID%>').value;
              var txtEyardRecipt = document.getElementById('<%= txtEyardRecipt.ClientID%>').value;

              //var url = "../Depo/Report_Epic/EyardInwoInvoice.aspx?GateInNo=" + txtEyardInPrint
              //window.open(url);
              var url1 = "../Depo/Report_Epic/EyardInPrint.aspx?GateInNo=" + txtEyardRecipt
              window.open(url1);


          }

</script>

         <script type="text/javascript">
             var popup;
             function OpenGatePassPrint() {

                 var txtgatepass = document.getElementById('<%= txtgatepass.ClientID%>').value;
             //var txtEyardRecipt = document.getElementById('<%= txtEyardRecipt.ClientID%>').value;

             //var url = "../Depo/Report_Epic/EyardInwoInvoice.aspx?GateInNo=" + txtEyardInPrint
             //window.open(url);
                 var url1 = "../Depo/Report_Epic/EyardOutPrint.aspx?GpNo=" + txtgatepass
             window.open(url1);


         }

</script>

    <script type="text/javascript">
        var popup;
        function OpenContainerList() {
            var url = "SearchContainerList.aspx"
            popup = window.open(url, "Popup", "width=700,height=550");
            popup.focus();
        }
</script>
<script type="text/javascript">
function ValidateQty() {
//alert('hii')
if ((event.keyCode > 47 && event.keyCode < 58) || event.keyCode == 46)
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
</asp:Content>

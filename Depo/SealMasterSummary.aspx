<%@ Page Title="Depo | Seal Entry Summary" Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="SealMasterSummary.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <head>
<title>Depo | Seal Entry Summary</title>
</head>
    <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            height:500px;
            overflow:auto
        }
        .nav-tabs>li.active>a, .nav-tabs>li.active>a:focus, .nav-tabs>li.active>a:hover{
            background-color:orange
        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Seal Entry Summary
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
                                                
<div class="row">
<div class="col-sm-7 col-xs-12"   >                                      
<div class="form-group date text-label">
Date                                           
<div class="input-group input-append date input-daterange" id="datePicker">
<asp:TextBox ID="txtfromDate" placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
<div class="input-group-addon text-label">To</div>
<asp:TextBox ID="txttoDate"  placeholder="mm-dd-yyyy" runat="server" TextMode="DateTimeLocal" Class="form-control text-label"></asp:TextBox>
</div>
</div>                                       
</div>   

<div class="col-md-2 col-xs-12" style="display:none" >
<div class="form-group text-label">
Search Criteria
<asp:DropDownList ID="ddlcriteria" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcriteria_SelectedIndexChanged" class="form-control text-label">
<asp:ListItem Value="0">All</asp:ListItem> 
<asp:ListItem Value="1">Shipping Line</asp:ListItem>
    <asp:ListItem Value="2">Jo Type</asp:ListItem>
</asp:DropDownList>                                               
</div>
</div>

<div class="col-md-4 col-xs-12"  runat="server" id="divShippingLine">
<div class="form-group text-label">
<b>Shipping Line</b>
<asp:DropDownList ID="ddlLineName" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control"></asp:DropDownList>
</div>
</div>

    <div class="col-md-3 col-xs-12"  runat="server" id="divJotype" style="display:none">
<div class="form-group text-label">
<b>Jo Type</b>
<asp:DropDownList ID="ddljoype" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control"></asp:DropDownList>
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

    <div class="row" style="display:none">
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
<div class="col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container" style="margin-left:10px;margin-right:0px;">
<asp:GridView ID="grdRegistrationSummary" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
AutoGenerateColumns="true" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
<asp:TemplateField>
    <HeaderTemplate>
      <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />
    </HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="chkright" Text="" Checked='<%#Eval("Check")%>' onclick = "Check_Click(this)" runat="server" />
</ItemTemplate>
  <ItemStyle Width="20px" HorizontalAlign="Center" />                                    
</asp:TemplateField>

      <asp:TemplateField HeaderText="Invoice No" Visible="false" HeaderStyle-CssClass="text-center">
<ItemTemplate>

<asp:Label runat="server" ID="lblassessNo" Visible="false" Text='<%#Eval("Seal No")%>'></asp:Label>
   
</ItemTemplate>

<ItemStyle HorizontalAlign="center"  />
</asp:TemplateField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
 
 
 
    <br />

    <div class="row">
        <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btncancel" class="btn btn-danger btn btn-sm outline  " runat="server"
 OnClick="btncancel_Click"
Text="Cancel"     />
</div>
    </div> 
        <div class="col-md-2 col-xs-12 pull-right">
                                <div class="form-group pull-right" style="padding-right: 20px">
                                    <asp:Button ID="btnExport" runat="server"
                                        class="btn btn-warning btn-sm outline" Text="Export To Excel" ></asp:Button>
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
                   
<a href="SealMasterSummary.aspx" class="btn btn-info btn-block">OK</a>
                                
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
     <script type = "text/javascript">

         function checkAll(objRef) {

             var GridView = objRef.parentNode.parentNode.parentNode;

             var inputList = GridView.getElementsByTagName("input");

             for (var i = 0; i < inputList.length; i++) {

                 //Get the Cell To find out ColumnIndex

                 var row = inputList[i].parentNode.parentNode;

                 if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                     if (objRef.checked) {

                         //If the header checkbox is checked

                         //check all checkboxes

                         //and highlight all rows

                         //row.style.backgroundColor = "aqua";

                         inputList[i].checked = true;

                     }

                     else {

                         //If the header checkbox is checked

                         //uncheck all checkboxes

                         //and change rowcolor back to original

                         //if(row.rowIndex % 2 == 0)

                         //{

                         //   //Alternating Row Color

                         //   //row.style.backgroundColor = "#C2D69B";

                         //}

                         //else

                         //{

                         //   row.style.backgroundColor = "white";

                         //}

                         inputList[i].checked = false;

                     }

                 }

             }

         }

</script> 
    <script type = "text/javascript">

        function Check_Click(objRef) {

            //Get the Row based on checkbox

            var row = objRef.parentNode.parentNode;

            //Get the reference of GridView

            var GridView = row.parentNode;



            //Get all input elements in Gridview

            var inputList = GridView.getElementsByTagName("input");



            for (var i = 0; i < inputList.length; i++) {

                //The First element is the Header Checkbox

                var headerCheckBox = inputList[0];



                //Based on all or none checkboxes

                //are checked check/uncheck Header Checkbox

                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;

                    }

                }

            }

            headerCheckBox.checked = checked;
        }

</script>
</asp:Content>

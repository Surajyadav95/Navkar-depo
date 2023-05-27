<%@ Page Title="Depo |Approve Tariff" Language="VB" MasterPageFile="../Depo/User.master" AutoEventWireup="false"
CodeFile="ApproveTariff.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<head>
<title>Depo |Approve Tariff</title>
</head>
      <style>
        .header-center{
            text-align:center
        }
        .scrolling-table-container{
            max-height:500px;
            overflow:auto

             

        }
    </style>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i>Approve Tariff Details  
</h3>
           
</div>
     
<div id="page-content">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
                     
<div class="panel">
<div class="panel-body">
<div class="col-md-12 col-xs-12 pull-md-left main-content" >
<div class="row">
                 
<asp:UpdatePanel ID="updatepanel2" runat="server" UpdateMode="Conditional"> 
<ContentTemplate>
                                    
                                                
<div class="row">
<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b>Tariff ID</b>
<asp:DropDownList ID="ddltraiff"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>  
</asp:DropDownList>
</div>
</div>

<div class="col-md-3 col-xs-12">
<div class="form-group text-label">
<b>Invoice Type</b>
<asp:DropDownList ID="ddlbondType"   Style="text-transform: uppercase;" runat="server" class="form-control text-label">
<asp:ListItem Value="0">--Select--</asp:ListItem>

</asp:DropDownList>
</div>
</div>

<div class="col-sm-1" style="padding-left:16px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline " runat="server"
OnClick="btnSearch_Click" 
Text="Show"     />
</div>                                                                                   
</div>

<div class="col-sm-1 col-xs-12" style="display:none;">
                                     
<div class="form-group" style="padding-top:20px">
<asp:LinkButton ID="lnkUpdate" ControlStyle-CssClass='btn btn-info'  runat="server"
OnClientClick="return Container()" >  
<i class=" fa fa-check"     aria-hidden="true"></i> </asp:LinkButton>
</div>
                                  
</div>
                                               
</div>


</ContentTemplate>
</asp:UpdatePanel>
                     
<div class="row">
<asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="Conditional">
<ContentTemplate>

<div class="row">
<div class=" col-md-12 col-lg-12 text-label "  style="padding-right:50px;">
<div class="table-responsive scrolling-table-container " style="margin-left:5    px;margin-right:0px;">
<asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover scrolling-table-container"
AutoGenerateColumns="False" EmptyDataText="No records found!" ShowHeaderWhenEmpty="true"   >
<pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 
<Columns>
 
<asp:TemplateField>
    <HeaderTemplate>
      <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />
    </HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="chkright" Text="" Checked='<%#Eval("IsApproved")%>' onclick = "Check_Click(this)" runat="server" />
</ItemTemplate>
  <ItemStyle Width="20px" HorizontalAlign="Center" />                                    
</asp:TemplateField>
    <asp:TemplateField HeaderText="Tariff ID" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
            <asp:Label runat="server" ID="lbltariffid" Text='<%#Eval("tariffID")%>'></asp:Label>
            <asp:Label runat="server" ID="lblentryid" Visible="false" Text='<%#Eval("entryID")%>'></asp:Label>

        </ItemTemplate>
        <ItemStyle HorizontalAlign="Center" />  
    </asp:TemplateField> 
                                                
 
<asp:BoundField DataField="deliverytype" HeaderText="Invoice Type"></asp:BoundField>
<asp:BoundField DataField="AccountName" HeaderText="Account Name"></asp:BoundField>
    <asp:BoundField DataField="deliverytype" HeaderText="Invoice Type"></asp:BoundField>
    <asp:BoundField DataField="containersize" HeaderText="Size"></asp:BoundField>
<asp:BoundField DataField="slabID" HeaderText="Slab ID"></asp:BoundField>
<asp:BoundField DataField="IsSTax" HeaderText="Is STax"></asp:BoundField>
<asp:BoundField DataField="fixedamt" HeaderText="Fixed Amount"></asp:BoundField>
<asp:BoundField DataField="effectivefrom" HeaderText="Effective From"></asp:BoundField>
<asp:BoundField DataField="effectiveupto" HeaderText="Effective Upto"></asp:BoundField>
</Columns>

</asp:GridView>
</div>
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
</div>

<div class="col-sm-2 pull-right" style="padding-right:0px;">
<div class="form-group" style="padding-top:20px">
<asp:Button ID="btnApprove" class="btn btn-primary btn btn-sm outline " runat="server" OnClick="btnApprove_Click"
Text="Approve Tariff"/>
</div>
                                              
                                      
</div>


</div>
</div>   
                                 
                               
</div>
</div>
                          
                     
                       
                       
</div>
                 
</ContentTemplate>
</asp:UpdatePanel>
</div>
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-sm">
<asp:UpdatePanel ID="UpdatePanel5" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
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
<%--<asp:Button ID="btntest" runat="server"
class="btn btn-info btn-block" Text="OK" data-dismiss="modal" aria-hidden="true" OnClientClick="populateCalendarTextbox()"></asp:Button>--%>
    <a href="ApproveTariff.aspx" class="btn btn-info btn-block ">Ok</a>
</div>
</div>

    </ContentTemplate>
</asp:UpdatePanel>
                      
</div>
</div>
</div>
  <script type = "text/javascript">

function checkAll(objRef)

{

    var GridView = objRef.parentNode.parentNode.parentNode;

    var inputList = GridView.getElementsByTagName("input");

    for (var i=0;i<inputList.length;i++)

    {

        //Get the Cell To find out ColumnIndex

        var row = inputList[i].parentNode.parentNode;

        if(inputList[i].type == "checkbox"  && objRef != inputList[i])

        {

            if (objRef.checked)

            {

                //If the header checkbox is checked

                //check all checkboxes

                //and highlight all rows

                //row.style.backgroundColor = "aqua";

                inputList[i].checked=true;

            }

            else

            {

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

                inputList[i].checked=false;

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

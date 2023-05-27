<%@ Page Title="Depo | " Language="VB" MasterPageFile="User.master" AutoEventWireup="false"
CodeFile="SealReturnIssue.aspx.vb" Inherits="Summary_BCYMovement" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
       
<head>
<title>Depo |Seal Return </title>
       
</head>
<div class="page-container">
<div class="pageheader">
<h3>
<i class="glyphicon glyphicon-transfer"></i> Seal Return /Issue Entry
</h3>
</div>
       
<div id="page-content">
        
       
       
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
 
<div class="page-container" style="margin-left: -5px; margin-right: -5px; margin-top: -15px;">
<asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
<div class="panel-body">

<div class="row">
                                         
<div class="col-md-9 pull-md-right main-content" >
<fieldset class="register">
<div menuitemname="Client Details" class="panel panel-sidebar panel-sidebar">
            
<div class="panel-body">
<asp:Panel ID="Panel2" runat="server" Enabled="true"> 

<div class="row">
  <div class="col-md-4 col-xs-12"  runat="server" id="divShippingLine">
<div class="form-group text-label">
<b>Shipping Line</b>
<asp:DropDownList ID="ddlLineName" Style="text-transform: uppercase;border-radius:4px" runat="server" class="form-control"></asp:DropDownList>
</div>
</div>
     <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnSearch" class="btn btn-primary btn btn-sm outline  " runat="server" OnClick="btnSearch_Click"
Text="Show"     />
</div>
    </div> 
</div>
      <div class="row">
        <div class="col-lg-6 col-xs-12 text-label " >
        <div class="table-responsive  scrolling-table-container " style="margin-left:10px;margin-right:0px;">
        <asp:GridView ID="grdcontainer" runat="server" ForeColor="Black" CssClass="table table-striped table-bordered table-hover"
        AutoGenerateColumns="false" EmptyDataText="No records found!"   >
        <pagerstyle backcolor="white" ForeColor="blue" Font-Underline="false" height="30px" verticalalign="Bottom" horizontalalign="center"/> 

        <Columns>

            <asp:TemplateField>
    <HeaderTemplate>
      <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />
    </HeaderTemplate>
<ItemTemplate>
<asp:CheckBox ID="chkright" Text="" Checked='<%#Eval("Check")%>' onclick = "Check_Click(this)" runat="server" />
</ItemTemplate>
  <ItemStyle Width="5px" HorizontalAlign="Center" />                                    
</asp:TemplateField>

     <asp:TemplateField HeaderText="Sr No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblsrno" runat="server" text='<%#Eval("Sr No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="5px" HorizontalAlign="left" /> 
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Seal No" HeaderStyle-CssClass="center-header">
        <ItemTemplate>
       <asp:Label ID="lblSealNo" runat="server" text='<%#Eval("Seal No")%>'></asp:Label>
        </ItemTemplate>
        <ItemStyle Width="5px" HorizontalAlign="left" /> 
        </asp:TemplateField>
             
       
        </Columns>
        </asp:GridView>
        </div>
        </div>
        </div>
     <div class="col-md-1 col-xs-12"  >
<div class="form-group" style="padding-top:20px ">
<asp:Button ID="btnsave" class="btn btn-primary btn btn-sm outline  " runat="server"  OnClick="btnsave_Click"
Text="Save"     />
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
                   
<a href="SealReturnIssue.aspx" class="btn btn-info btn-block">OK</a>
                                
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
 
</div>
       
         
</div>
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

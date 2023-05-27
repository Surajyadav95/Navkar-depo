<%@ Page Title="Epic" Language="VB" EnableEventValidation="false" MasterPageFile="~/Depo/PopUp.master" AutoEventWireup="false" CodeFile="InvoiceCancel.aspx.vb" Inherits="Account_ItemList" Culture="en-GB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
function callparentfunction() {
//alert("hiii");
//  alert(window.opener.document.getElementById("_btnIndentItem"));
    window.opener.document.getElementById("ContentPlaceHolder1_btnsearch").click();
    self.close();
}
</script>
    <style>
        .text-center{
            text-align:center
        }
        .scrolling-table-container{
            height:200px;
            overflow:auto
        }
    </style>
<div class="container" style="background-color: white;margin-top:-60px">    
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="panel-body">
<div class="form-group">
    <div class="row">
        <div class="col-xs-6">
            <b>Invoice No:</b>&nbsp;<asp:Label runat="server" ID="lblInvoiceNo" BackColor="Yellow"></asp:Label>
        </div>
        <div class="col-xs-6 ">
            <div class="form-group pull-right">
            <b>Work Year:</b>&nbsp;<asp:Label runat="server" ID="lblWorkYear" BackColor="Yellow"></asp:Label>
            </div>
        </div>

    </div>                                
    <div class="row">
         <div class="col-sm-4 col-xs-12">
<div class="form-group text-label">
<b>Cancel Remarks</b>
<asp:TextBox ID="txtRemarks" Style="text-transform:uppercase" class="form-control text-label"  placeholder="Remarks"
runat="server" TextMode="MultiLine" rows="3"></asp:TextBox>
</div>
</div>

    </div>      
<div class="row">
<div class="col-sm-12 col-xs-12">
<div class="col-sm-1 col-xs-1">
    
<asp:Button ID="btnClose" class="btn btn-primary btn-sm "  runat="server" OnClientClick="return callparentfunction()"
Text="Close"  />                                                                                 
</div> 
<div class="col-sm-1 col-xs-1 pull-right" style="padding-right:10px">
<asp:Button ID="btnSave" class="btn btn-danger btn-sm " runat="server" OnClientClick="return ValidationCancel()" OnClick="btnSave_Click"  
Text="Cancel"  />                                                                                 
</div> 
</div> 
</div>     
</div>
<div class="modal fade control-label" id="myModalforupdate" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
<div class="modal-dialog modal-xs">
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

<button class="btn btn-info " ID="Button1" data-dismiss="modal" runat="server" onclick="callparentfunction()" aria-hidden="true">
                                Ok 
                            </button>

</div>
</div>

</ContentTemplate>

</asp:UpdatePanel>
</div>
</div>
</div>
</div>
    <script type="text/javascript">
        function ValidationAdd() {
          

            var blResult = Boolean;
            blResult = true;

            if (blResult == false) {
                alert('Please fill the required fields!');
            }
            return blResult;
        }
    </script>
    <script type="text/javascript">
        function ValidationCancel() {
            var txtRemarks = document.getElementById('<%= txtRemarks.ClientID%>').value;

            var result = confirm('Are you sure to cancel this Invoice?')
            var blResult = Boolean;
            blResult = true;

            if (result == true) {
                if (txtRemarks == "") {
                    document.getElementById('<%= txtRemarks.ClientID%>').style.borderColor = "red";
                    alert('Please enter cancel remark!');
                    blResult = blResult && false;
                }
            }
            else {
                blResult = blResult && false;
            }

            return blResult;

        }
</script>
</asp:Content>



<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>Depo | Login</title>
<link rel="shortcut icon" href="../img/Phoenix-logo-Bird.png" type="image/png">
<!--STYLESHEET-->
<!--=================================================-->
<!--Bootstrap Stylesheet [ REQUIRED ]-->
<link href="../css/bootstrap.min.css" rel="stylesheet">
<!--Jasmine Stylesheet [ REQUIRED ]-->
<link href="../css/style.css" rel="stylesheet">
<!--Font Awesome [ OPTIONAL ]-->
<link href="../plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet">
<!--Switchery [ OPTIONAL ]-->
<link href="../plugins/switchery/switchery.min.css" rel="stylesheet">
<!--Bootstrap Select [ OPTIONAL ]-->
<link href="../plugins/bootstrap-select/bootstrap-select.min.css" rel="stylesheet">
<!--Demo [ DEMONSTRATION ]-->
<link href="../css/demo/jasmine.css" rel="stylesheet">
<!--SCRIPT-->
<!--=================================================-->
<!--Page Load Progress Bar [ OPTIONAL ]-->
<link href="../plugins/pace/pace.min.css" rel="stylesheet">
<script type="text/javascript">
function Validate() {
if (document.getElementById("TxtUserid").value == "") {

}
}
</script>
<%--   style="background-image: url(img/bgC.jpg); background-repeat:repeat-y ;"--%>
</head>
<body>
<%--<div style="background-image: url(img/33.jpg); background-repeat:repeat;">--%>
<div style="background-color: 231,231,231;">
<center>
<div id="container" style="width: 40%;" class="effect mainnav-lg navbar-fixed mainnav-fixed">
<header id="navbar">
<div id="navbar-container" class="boxed">
<!--Brand logo & name-->
<!--================================-->
<div class="navbar-header">
<a href="" class="navbar-brand">
<i class="fa fa-cube brand-icon"></i>
<div class="brand-title">
<span class="brand-text">TRACKER</span>
</div>
</a>
</div>
                   
</div>
</header>
<div class="boxed" >
<!--CONTENT CONTAINER-->
<!--===================================================-->
<div style="padding-left: 0px !important;">
<!--Page content-->
<!--===================================================-->
<div id="page-content" style="padding-top:100px; width:90%;">
<div class="row">
<div class="eq-height">
<div class="col-sm-10  eq-box-sm">
<div class="panel">
<div class="panel-heading">
<h3 style="padding-right: 60%; color: #006f92;">
LOGIN</h3>
</div>
<!--Horizontal Form-->
<!--===================================================-->
<form class="form-horizontal" action="#" id="registrationForm" runat="server">
<asp:Panel ID="panel" runat="server" DefaultButton="BtnSubmit">
<div class="panel-body" style="padding-left:1px;">
<div class="form-group">
<label class="col-sm-4 control-label" for="demo-hor-inputpass">
&nbsp;User ID</label>
<div class="col-sm-6 ">
<asp:TextBox runat="server" ID="username" placeholder="User ID" CssClass="form-control"></asp:TextBox>
</div>    
</div>
<div class="form-group">
<label class="col-sm-4 control-label" for="demo-hor-inputpass">
Password</label>
<div class="col-sm-6">
<asp:TextBox runat="server" ID="password" type="password" placeholder="Password"
name="password" CssClass="form-control"></asp:TextBox>

<asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
</div>

</div>

     <div class="form-group"">
                            <asp:CheckBox ID="chkRememberMe" runat="server" />
                              <asp:hiddenfield ID="hdlocation" runat="server" Value="0" />
                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="chkRememberMe" CssClass="inline">Remember me</asp:Label>
                        </div>
</div>
<div class="panel-footer text-lg-left col-lg-4  " style="width: 50%; float: left;">
<%-- &nbsp; <a class="btn-link text-semibold" href="#">Forgotten password?</a>--%>
<br />
                                                
</div>
                                               
</div>
<div class="panel-footer text-lg-right " style="width: 50%; padding-right: 20%; float: right">
<asp:Button ID="BtnSubmit" CssClass="btn btn-info btn-sm outline" runat="server" Text="Log In" OnClientClick="return Validate();">
</asp:Button>
</div>
</asp:Panel>

                                            
</form>
<!--===================================================-->
<!--End Horizontal Form-->
</div>
</div>
</div>
</div>
</div>
<!--===================================================-->
<!--End page content-->
</div>
<!--===================================================-->
<!--END CONTENT CONTAINER-->
<!--MAIN NAVIGATION-->
</div>
<!-- FOOTER -->
<!--===================================================-->
<footer id="footer" style="position: fixed; padding-left: 0px !important; left: 10px;
text-align: left; background-color: transparent;">
               
<!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
<!-- Remove the class name "show-fixed" and "hide-fixed" to make the content always appears. -->
<!-- ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ -->
<label class="text-semibold" style="color: #6666FF">Best viewed in resolution 1366 X 768</label>

</footer>
<!--===================================================-->
<!-- END FOOTER -->
<!-- SCROLL TOP BUTTON -->
<!--===================================================-->
<!--===================================================-->
</div>
</center>
</div>
<!--===================================================-->
<!-- END OF CONTAINER -->
<!--JAVASCRIPT-->
<!--=================================================-->
   
   
</body>
</html>

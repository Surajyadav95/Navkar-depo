<%@ Page Title="" Language="VB" AutoEventWireup="false"
    CodeFile="SessionExpired.aspx.vb" Inherits="Common_SessionExpired" %>


    <html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <link rel="shortcut icon" href="img/favicon.ico" />
    <!--STYLESHEET-->
    <!--=================================================-->
    <!--Roboto Slab Font [ OPTIONAL ] -->
    <link href="https://fonts.googleapis.com/css?family=Roboto+Slab:300,400,700|Roboto:300,400,700"
        rel="stylesheet">
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
    <!--Bootstrap Validator [ OPTIONAL ]-->
    <link href="../plugins/bootstrap-validator/bootstrapValidator.min.css" rel="stylesheet">
    <!--ricksaw.js [ OPTIONAL ]-->
    <link href="../plugins/jquery-ricksaw-chart/css/rickshaw.css" rel="stylesheet">
    <!--jVector Map [ OPTIONAL ]-->
    <link href="../plugins/jvectormap/jquery-jvectormap.css" rel="stylesheet">
    <!--Demo [ DEMONSTRATION ]-->
    <link href="../css/demo/jquery-steps.min.css" rel="stylesheet">
    <!--Demo [ DEMONSTRATION ]-->
    <link href="../css/demo/jasmine.css" rel="stylesheet">
    <!--SCRIPT-->
    <!--=================================================-->
    <!--Page Load Progress Bar [ OPTIONAL ]-->
    <link href="../plugins/pace/pace.min.css" rel="stylesheet">
     <style type="text/css">
            .header-center
            {
                text-align: center;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <header id="navbar">
                <div id="navbar-container" class="boxed">
                    <!--Brand logo & name-->
                    <!--================================-->
                    <div class="navbar-header">
                        <a class="navbar-brand">
                            <i class="fa fa-cube brand-icon"></i>
                            <div class="brand-title">
                                <span class="brand-text">Bond</span>
                            </div>
                        </a>
                    </div>
                   
                </div>
          

              
                      </header>
    </div>
    <br />
    <br /><br />
    <div class="boxed">
    <br />
    <br />
    <br />
    <br />
    <div id="page-content" style="margin-left: -7px; margin-right: 0px;">
        <div id="div1" runat="server" class="col-md-4 col-sm-12 col-xs-12">
        </div>
        <div id="div_ImportTile" runat="server" class="col-md-4 col-sm-12 col-xs-12">
            <div class="panel">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-xs-3">
                            <i class="fa fa-clock-o fa-4x text-danger"></i>
                        </div>
                        <div class="col-md-10 col-sm-10 col-xs-9">
                            <h2 class="mar-no">
                                <span class="counter center-block"><a class="text-danger center-block">Session Expired</a></span></h2>
                            <p>
                            </p>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-xs-3">
                        </div>
                        <div class="col-md-10 col-sm-10 col-xs-9">
                            <h4 class="mar-no">
                                <span class="counter"><a class="text-danger">Your session has expired. You will be redirected
                                    to the Login page.</a></span></h4>
                            <br />
                            <a class="text-danger"></a><a type="button" class="btn btn-success   btn-sm outline btn-xs"
                                href="../Login.aspx" target="_blank">Login</a>
                            <p>
                            </p>
                        </div>
                    </div>
                    <p>
                        <%-- <a class="text-info" href="../Summary/ImportMovement.aspx">Open Import Movement</a>--%>
                    </p>
                </div>
            </div>
        </div>
    </div>

        </div>
    <script src="../js/jquery-2.1.1.min.js"></script>
    <!--BootstrapJS [ RECOMMENDED ]-->
    <script src="../js/bootstrap.min.js"></script>
    <!--Fast Click [ OPTIONAL ]-->
         <script src="../js/demo/form-component.js" type="text/javascript"></script>
    <script src="../js/demo/mail-compose.js" type="text/javascript"></script>
    </form> 
</body>
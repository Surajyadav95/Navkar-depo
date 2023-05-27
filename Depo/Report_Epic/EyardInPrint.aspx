<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EyardInPrint.aspx.vb" Inherits="Report_Estimation_Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtigm"  Style="text-transform: uppercase;display:none " class="form-control text-label form-cascade-control"
                                                runat="server" Text="2178833"></asp:TextBox>
    <asp:TextBox ID="txtitem"  Style="text-transform: uppercase;display:none " class="form-control text-label form-cascade-control"
                                                runat="server" Text="360"></asp:TextBox>
     <asp:TextBox ID="txtvaldate"  Style="text-transform: uppercase; display:none" class="form-control text-label form-cascade-control"
                                                runat="server" Text=""></asp:TextBox>
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" Width="1000px" Height="600px" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="16pt">
        <LocalReport ReportPath="Epic\Report_Epic\EyardInPrint.rdlc" >
        </LocalReport>
        </rsweb:ReportViewer>
    </div>
    </form>
        <asp:Label ID="lblError" runat="server" Text="" ForeColor="red"></asp:Label>
    
   
</body>
</html>

<%@ Page Language="VB" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    Protected Sub Page_Load(sender As Object, ec As EventArgs)
        Using cn As New SqlConnection("Server=dbcapps;database=DBCCFS;uid=sa;pwd=Password@123")
            'change as needed
            Using sr As New StreamReader(Request.InputStream, Encoding.UTF8)
                Response.ContentType = "text/plain"
                Dim c As String
                c = Request.QueryString("query")
                'for debugging with the browser
                'you can set the query by adding the query parameter  For ex: http://127.0.0.1/test.aspx?query=select * from table1
                If c Is Nothing Then
                    c = sr.ReadToEnd()
                End If
                Try
                    Dim cmd As New SqlCommand(c, cn)
                    cn.Open()
                    Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                    Dim list As New List(Of Dictionary(Of String, Object))()
                    While rdr.Read()
                        Dim d As New Dictionary(Of String, Object)(rdr.FieldCount)
                        For i As Integer = 0 To rdr.FieldCount - 1
                            d(rdr.GetName(i)) = rdr.GetValue(i)
                        Next
                        list.Add(d)
                    End While
                    Dim j As New JavaScriptSerializer()

                    Response.Write(j.Serialize(list.ToArray()))
                Catch e As Exception
                    Response.TrySkipIisCustomErrors = True
                    Response.StatusCode = 500
                    Response.Write("Error occurred. Query=" & c & vbLf)

                    Response.Write(e.ToString())
                End Try
                Response.[End]()
            End Using
        End Using
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>


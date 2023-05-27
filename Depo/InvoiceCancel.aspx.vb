Imports System.Data
Imports System.IO
Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim WHID, WHIDView As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblInvoiceNo.Text = Request.QueryString("AssessNo")
            lblWorkYear.Text = Request.QueryString("WorkYear")
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_CANCEL_INVOICE '" & Trim(lblInvoiceNo.Text & "") & "','" & Trim(lblWorkYear.Text & "") & "','" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "'," & Session("UserId_DepoCFS") & ""
            db.sub_ExecuteNonQuery(strSql)
            lblInvoiceNo.Text = ""
            lblWorkYear.Text = ""
            txtRemarks.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invoice cancelled successfully');", True)
            Dim page As String = "index.aspx"
            ClientScript.RegisterStartupScript(page.GetType(), "OpenList", "<script>callparentfunction(); </script>")
        Catch ex As Exception
            'lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

End Class

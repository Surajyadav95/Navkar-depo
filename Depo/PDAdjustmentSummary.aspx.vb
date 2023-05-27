Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now.AddDays(-7)).ToString("yyyy-MM-dd")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-dd")

            btnSave_Click(sender, e)
        End If
    End Sub

    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_PDAdjustment_Summary '" & Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy 00:00:00") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy 23:59:00") & "','" & Trim(txtsearch.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            up_grid.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim TransNo As String = lnkcancel.CommandArgument

            strSql = ""
            strSql += "update Eyard_PDAdjustments set IsCancel=1,CancelledBy='" & Session("UserId_DepoCFS") & "',CancelledOn=GETDATE() where TransNo='" & TransNo & "' "
            strSql += "update Eyard_assessM set Status='P' where AssessNo=(select AssessNo from Bond_PDAdjustments where TransNo='" & TransNo & "') and WorkYear=(select AssessYear from Bond_PDAdjustments where TransNo='" & TransNo & "')"
            db.sub_ExecuteNonQuery(strSql)
            btnSave_Click(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

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
            FillDropdown()
            btnsearch_Click(sender, e)
        End If
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_CREDIT_NOTE_SUMMARY '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00") & "',"
            strSql += "'" & Trim(ddlcriteria.SelectedValue & "") & "','" & Trim(txtAssessno.Text & "") & "','" & Trim(ddlGSTParty.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdSummary.DataSource = dt
            grdSummary.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdSummary.PageIndex = e.NewPageIndex
        Me.btnsearch_Click(sender, e)
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function

    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            txtAssessno.Text = ""            
            ddlGSTParty.SelectedValue = 0
            If ddlcriteria.SelectedValue = 0 Then
                divassessno.Attributes.Add("style", "display:none")
                divGSTParty.Attributes.Add("style", "display:none")
               
            ElseIf ddlcriteria.SelectedValue = 1 Then
                divassessno.Attributes.Add("style", "display:block")
                divGSTParty.Attributes.Add("style", "display:none")
                
            ElseIf ddlcriteria.SelectedValue = 2 Then
                divassessno.Attributes.Add("style", "display:block")
                divGSTParty.Attributes.Add("style", "display:none")
               
            ElseIf ddlcriteria.SelectedValue = 3 Then
                divassessno.Attributes.Add("style", "display:none")
                divGSTParty.Attributes.Add("style", "display:block")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim AssessNo As String = lnkcancel.CommandArgument
            Dim WorkYear As String = grdSummary.DataKeys(row.RowIndex).Value.ToString()
            Dim str As String = ""
            strSql = ""
            strSql += "UPDATE CreditNoteM set iscancel=1,cancelledby='" & Session("UserId_DepoCFS") & "',CancelledOn=getdate()  where CreditNoteNo='" & AssessNo & "' and WorkYear='" & WorkYear & "'"
            db.sub_ExecuteNonQuery(strSql)
            btnsearch_Click(sender, e)
            lblsession.Text = "Assessment Cancelled Successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub FillDropdown()
        Try
            strSql = ""
            strSql += "USP_CREDIT_NOTE_FILL_DROPDOWN"
            dt = db.sub_GetDatatable(strSql)
            ddlGSTParty.DataSource = dt
            ddlGSTParty.DataTextField = "GSTName"
            ddlGSTParty.DataValueField = "GSTID"
            ddlGSTParty.DataBind()
            ddlGSTParty.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports System.Configuration

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt10 As DataTable
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            btnShow_Click(sender, e)

        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
     
    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            
            strSql = ""
            strSql = " exec Sp_Show_Container_Re_Applicable '" & Trim(ddlProcess.SelectedItem.Text) & "','" & Trim(TxtContainerNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdReApplication.DataSource = dt
            grdReApplication.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdRegistrationSummary_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdReApplication.PageIndex = e.NewPageIndex
        Me.btnShow_Click(sender, e)
    End Sub
 Protected Sub btnReApplication_Click(sender As Object, e As EventArgs)
        Try
            lblquoteApprove.Text = "Are you sure to Cancel ?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btncancelyes_ServerClick(sender As Object, e As EventArgs)
        Try
            For Each row As GridViewRow In grdReApplication.Rows
                If ddlProcess.SelectedItem.Text = "In" Then
                    strSql = ""
                    strSql = "Update EYard_In set IsAutoMailedline=0 where Entryid='" & Val(CType(row.FindControl("lblEntryID"), Label).Text & "") & "' and ContainerNo='" & TxtContainerNo.Text & "'"
                    db.sub_ExecuteNonQuery(strSql)
                    db.AmmendmentLog("Update EYard_In set IsAutoMailedline=0 where Entryid='" & Val(CType(row.FindControl("lblEntryID"), Label).Text & "") & "' and ContainerNo='" & TxtContainerNo.Text & "'", Session("UserId_DepoCFS"))

                ElseIf ddlProcess.SelectedItem.Text = "Out" Then
                    strSql = ""
                    strSql = "Update EYardEmptyOut set IsAutoMailedline=0 where Entryid='" & Val(CType(row.FindControl("lblEntryID"), Label).Text & "") & "' and ContainerNo='" & TxtContainerNo.Text & "'"
                    db.sub_ExecuteNonQuery(strSql)
                    db.AmmendmentLog("Update EYardEmptyOut set IsAutoMailedline=0 where Entryid='" & Val(CType(row.FindControl("lblEntryID"), Label).Text & "") & "' and ContainerNo='" & TxtContainerNo.Text & "'", Session("UserId_DepoCFS"))
                End If
            Next
            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            'UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

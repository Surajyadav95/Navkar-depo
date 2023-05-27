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
            Filldropdown()
            btnSearch_Click(sender, e)
        End If
    End Sub
    Public Sub grid()
        strSql = ""
        strSql += ""
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_TARIFF_VIEW_MASTER"
            ds = db.sub_GetDataSets(strSql)
            ddltraiff.DataSource = ds.Tables(0)
            ddltraiff.DataTextField = "tariffID"
            ddltraiff.DataValueField = "entryID"
            ddltraiff.DataBind()
            ddltraiff.Items.Insert(0, New ListItem("--Select--", 0))

            strSql = ""
            strSql += "USP_TARIFF_INVOICE_TYPE_VIEW"
            ds = db.sub_GetDataSets(strSql)
            ddlbondType.DataSource = ds.Tables(0)
            ddlbondType.DataTextField = "InvoiceType"
            ddlbondType.DataValueField = "ID"
            ddlbondType.DataBind()
            ddlbondType.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function   
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSearch_Click(sender, e)
    End Sub
    Protected Sub btnApprove_Click(sender As Object, e As EventArgs)
        Try                     
            For Each row As GridViewRow In grdcontainer.Rows
                Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                'Dim IsTax As String
                'If (chkright.Checked = True) Then
                '    IsApproved = "Yes"
                'Else
                '    IsApproved = "No"
                'End If
                strSql = ""
                strSql += "USP_insert_IsApprovr_update_eyard'" & Trim(chkright.Checked & "") & "','" & Session("UserId_DepoCFS") & "','" & Trim(CType(row.FindControl("lblentryid"), Label).Text) & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next
            btnSearch_Click(sender, e)
            lblSession.Text = "Record updated successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " usp_Approve_tariff_eyard '" & Trim(ddltraiff.SelectedItem.Text & "") & "','" & Trim(ddlbondType.SelectedItem.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

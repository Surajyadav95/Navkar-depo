Imports System.Data
Imports System.IO
Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not (Request.QueryString("FromDate") = "" And Request.QueryString("ToDate") = "" And Request.QueryString("Line") = "") Then
                getItemList()
            End If
        End If
    End Sub
    Protected Sub getItemList()
        Try
            dt = db.sub_GetDatatable("USP_VESSEL_LIST_FOR_INVOICE '" & Trim(txtItemCode.Text & "") & "'," & Session("UserId_DepoCFS") & ",'" & Convert.ToDateTime(Request.QueryString("FromDate")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Request.QueryString("ToDate")).ToString("yyyy-MM-dd HH:mm") & "','" & Request.QueryString("Line") & "'")
            grdVesselList.DataSource = dt
            grdVesselList.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click()
        Try
            getItemList()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub OpenList(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dblVesselCheck As Double = 0
            For Each row In grdVesselList.Rows
                Dim chkitem1 As CheckBox = CType(row.FindControl("chkitem"), CheckBox)
                If chkitem1.Checked = True Then
                    'strSql = ""
                    'strSql += "USP_VALIDATION_FOR_VESSEL_ADDING '" & Trim(CType(row.FindControl("lblcode"), Label).Text & "") & "'," & Session("UserId_DepoCFS") & ""
                    'dt = db.sub_GetDatatable(strSql)
                    dblVesselCheck += 1
                    Exit For
                End If
            Next
            If (dblVesselCheck = 0) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Select atleast one Vessel Name');", True)
                Exit Sub
            End If
            For Each row In grdVesselList.Rows
                Dim chkitem As CheckBox = CType(row.FindControl("chkitem"), CheckBox)
                If chkitem.Checked = True Then
                    strSql = ""
                    strSql = "USP_INSERT_INTO_VESSEL_REPO_INVOICE '" & Trim(CType(row.FindControl("lblcode"), Label).Text & "") & "'," & Session("UserId_DepoCFS") & ""
                    db.sub_ExecuteNonQuery(strSql)
                End If
            Next
            Dim page As String = "index.aspx"
            ClientScript.RegisterStartupScript(page.GetType(), "OpenList", "<script>callparentfunction(); </script>")

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

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
            db.sub_ExecuteNonQuery("delete from TEMP_SLABS where USER_ID=" & Session("UserId_DepoCFS") & "")
            FillGrid()
            ddlSlabtype.Focus()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dblCount As Double = 0
            For Each row In grdSlabList.Rows
                dblCount += 1
            Next
            If dblCount = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Add slab details first..');", True)
                Exit Sub
            End If
            db.sub_ExecuteNonQuery("USP_INSERT_INTO_EYARD_SLABS " & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("delete from TEMP_SLABS where USER_ID=" & Session("UserId_DepoCFS") & "")
            ddlSlabtype.SelectedValue = "Days"
            txtFrom.Text = ""
            txtTo.Text = ""
            txtValue.Text = ""
            FillGrid()
            Dim page As String = "index.aspx"
            ClientScript.RegisterStartupScript(page.GetType(), "OpenList", "<script>callparentfunction(); </script>")
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim dblCount As Double = 0
            For Each row In grdSlabList.Rows
                dblCount += 1
            Next
            If dblCount > 0 Then
                strSql = ""
                strSql += "SELECT * FROM TEMP_SLABS WHERE IS_CANCEL=0 AND USER_ID=" & Session("UserId_DepoCFS") & " AND SLAB_ON='" & Trim(ddlSlabtype.SelectedItem.Text) & "'"
                dt = db.sub_GetDatatable(strSql)
                If Not dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Slab Type must be same');", True)
                    Exit Sub
                End If
            End If

            strSql = ""
            strSql += "SELECT * FROM TEMP_SLABS WHERE IS_CANCEL=0 AND USER_ID=" & Session("UserId_DepoCFS") & " AND SLAB_ON='" & Trim(ddlSlabtype.SelectedItem.Text) & "' AND FROM_SLAB='" & Trim(txtFrom.Text & "") & "' AND TO_SLAB='" & Trim(txtTo.Text & "") & "' AND SIZE=" & ddlSize.SelectedValue & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Slab ID already present');", True)
                Exit Sub
            End If
            strSql = ""
            strSql += "insert into TEMP_SLABS(SLAB_ON,FROM_SLAB,TO_SLAB,VALUE,USER_ID,SIZE) VALUES('" & Trim(ddlSlabtype.SelectedItem.Text) & "','" & Trim(txtFrom.Text & "") & "','" & Trim(txtTo.Text & "") & "','" & Trim(txtValue.Text & "") & "'," & Session("UserId_DepoCFS") & "," & ddlSize.SelectedValue & ")"
            db.sub_ExecuteNonQuery(strSql)
            ddlSlabtype.SelectedValue = "Days"
            txtFrom.Text = ""
            txtTo.Text = ""
            txtValue.Text = ""
            ddlSize.SelectedValue = "20"
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub FillGrid()
        Try
            strSql = ""
            strSql += "select * from TEMP_SLABS WHERE IS_CANCEL=0 AND USER_ID=" & Session("UserId_DepoCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            grdSlabList.DataSource = dt
            grdSlabList.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnOk_ServerClick(sender As Object, e As EventArgs)
        Try
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub FillDropdown()
        Try
            strSql = ""
            strSql += "select * from slab_type_m where is_fixed=1"
            dt = db.sub_GetDatatable(strSql)
            ddlSize.DataSource = dt
            ddlSize.DataTextField = "SLAB_TYPE"
            ddlSize.DataValueField = "ID"
            ddlSize.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

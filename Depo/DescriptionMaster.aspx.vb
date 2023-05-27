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
            getItemList()

        End If
    End Sub

    Protected Sub getItemList()
        Try
            strSql = ""
            strSql = "SELECT TOP(100) DescriptionID AS [ID], DescriptionName As [Description] from Description_M ORDER BY DescriptionID DESC"
            dt = db.sub_GetDatatable(strSql)
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            'up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim Intactive As Integer
            Dim dt As New DataTable
            Dim dt1 As New DataTable

            strSql = ""
            strSql = " SELECT DescriptionName FROM Description_M  where DescriptionName='" & Trim(txtDescriptiontype.Text) & "'  "
            dt1 = db.sub_GetDatatable(strSql)

            If dt1.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Description name is already exists. cannot proceed');", True)
                'MsgBox("Description name is already exists. cannot proceed", vbCritical)
                txtDescriptiontype.Focus()
                Exit Sub
            End If

           
                strSql = ""
                strSql = " SELECT isnull(MAX(DescriptionID ),0)FROM Description_M  "
                dt1 = db.sub_GetDatatable(strSql)

                If dt1.Rows.Count > 0 Then
                    txtID.Text = Val(dt1.Rows(0)(0)) + 1
                Else
                    txtID.Text = 1
                End If

                If chkisActive.Checked = True Then
                    Intactive = 1
                Else
                    Intactive = 0
                End If

                strSql = ""
                strSql = ""
                strSql += "SP_DescriptionMaster '" & Trim(txtID.Text) & "','" & Trim(txtDescriptiontype.Text) & "','" & Session("UserId_DepoCFS") & "' , '" & Format(Now, "yyyy-MM-dd HH:mm") & "', '" & Intactive & "' "
                db.sub_ExecuteNonQuery(strSql)
            lblSession.Text = "Record Saved successfully "
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                'UpdatePanel3.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.getItemList()
    End Sub
End Class

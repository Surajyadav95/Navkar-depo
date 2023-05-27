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
            strSql += "select * from LocationMaster"
            If Not Trim(txtSearch.Text & "") = "" Then
                strSql += " where locationName like '%" & Trim(txtSearch.Text & "") & "%'"
            End If
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
         

            strSql = ""
            strSql += "select * from LocationMaster where locationName='" & Replace(Trim(txtLocationName.Text & ""), "'", "''") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtLocationName.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "alert('Location Name already exists .');", True)
                Exit Sub
            End If

            strSql = ""
            strSql += "insert into LocationMaster(locationName) Values('" & Replace(Trim(txtLocationName.Text & ""), "'", "''") & "')"
            dt = db.sub_GetDatatable(strSql)
            lblSession.Text = "Record Saved successfully"
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            getItemList()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

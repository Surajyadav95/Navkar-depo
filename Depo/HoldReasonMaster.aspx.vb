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
            strSql = "Select HoldReasonID [ID] ,HoldReason [Reason], IsActive from  Eyard_HoldReasons "
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
            Dim Count As Integer = 0
            Dim Intactive As Integer
            Dim dt As New DataTable
            Dim dt1 As New DataTable

            strSql = ""
            strSql = " select  HoldReason from  Eyard_HoldReasons  WHERE HoldReason='" & Trim(txtHoldReason.Text) & "'"
            dt1 = db.sub_GetDatatable(strSql)

            If dt1.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Hold reason already exists.');", True)
                'MsgBox("Description name is already exists. cannot proceed", vbCritical)
                txtHoldReason.Focus()
                Exit Sub
            End If


            strSql = ""
            strSql = "SELECT isnull(MAX(cast (HoldReasonID as bigint)),0) as HoldReasonID FROM Eyard_HoldReasons"
            dt1 = db.sub_GetDatatable(strSql)
            dt = db.sub_GetDatatable(strSql)
            Count = Val(dt.Rows(0)("HoldReasonID"))
            If Count = 0 Then
                txtID.Text = (Count + 1)
            Else
                txtID.Text = (Count + 1)
            End If

            If chkisActive.Checked = True Then
                Intactive = 1
            Else
                Intactive = 0
            End If

            strSql = ""
            strSql = ""
            strSql += " EXEC  SP_save_holdreason " & txtID.Text & ",'" & txtHoldReason.Text & "','" & chkisActive.Checked & "'," & Session("UserId_DepoCFS") & ",'" & Format(Now, "yyyy-MMM-dd HH:mm:ss") & "'"
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

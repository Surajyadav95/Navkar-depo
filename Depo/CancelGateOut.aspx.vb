Imports System.Data
Imports System.IO
Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim WHID, WHIDView As String
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            txtContainerno.Focus()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function  
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
           
            strSql = ""
            strSql = "select ContainerNo ,entryID  from EYardEmptyOut where containerNo ='" & txtContainerno.Text & "' AND Iscancel=0  order by entryID desc "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblEntryID.Text = dt.Rows(0)("entryID")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Container not in CFS');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()
                lblEntryID.Text = ""
                Exit Sub
            End If

            strSql = ""
            strSql = "Sp_GateoutShow '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtOutdate.Text = Trim(dt1.Rows(0)("outdate") & "")
                txtSize.Text = Trim(dt1.Rows(0)("Size") & "")
                txtType.Text = Trim(dt1.Rows(0)("containertype") & "")


            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()

                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Control_Clear(sender As Object, e As EventArgs)
        Try
            txtContainerno.Text = ""
            txtSize.Text = ""
            txtType.Text = ""
            txtOutdate.Text = ""

          
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btncancel_Click(sender As Object, e As EventArgs)
        Try
           
            lblquoteApprove.Text = "Are you sure to Cancel ?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btncancelyes_ServerClick(sender As Object, e As EventArgs)
        strSql = ""
        strSql = "Exec  USP_Cance_Eyard_OUt '" & Trim(txtContainerno.Text) & "', '" & Val(lblEntryID.Text) & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd") & "','" & Session("UserId_DepoCFS") & "'"
        dt3 = db.sub_GetDatatable(strSql)
        lblSession.Text = "Container cancelled successfully"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        Control_Clear(sender, e)
    End Sub
  
End Class

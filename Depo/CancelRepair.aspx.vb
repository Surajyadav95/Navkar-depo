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
            strSql = "select top 1 entryid, containerno, outdate,  RepairedDate from eyard_stock where ContainerNo ='" & Trim(txtContainerno.Text) & "' order by entryid desc"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("Outdate") <> "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container has out from empty depot. Cannot proceed');", True)
                    txtContainerno.Focus()
                    Exit Sub
                End If
                lblEntryID.Text = dt.Rows(0)("entryID")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Container not in CFS');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()
                lblEntryID.Text = ""
                Exit Sub
            End If

            strSql = ""
            strSql = "SP_ShowRepair '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtRepairdate.Text = Trim(dt1.Rows(0)("RepairedDate") & "")
                txtCSC.Text = Trim(dt1.Rows(0)("CSCASP") & "")
                txtDesc.Text = Trim(dt1.Rows(0)("Descriptions") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()

                Exit Sub
            End If
            txtContainerno_TextChanged(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Control_Clear(sender As Object, e As EventArgs)
        Try
            txtContainerno.Text = ""
            txtRepairdate.Text = ""
            txtAmt.Text = ""
            txtCSC.Text = ""
            txtDesc.Text = ""
            lblEntryID.Text = ""
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
        strSql = ""
        strSql = "sp_Cancel_Repair '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "','" & Trim(lblEstimat_ID.Text) & "' "
        dt2 = db.sub_GetDatatable(strSql)
        lblSession.Text = "Container cancelled successfully"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        Control_Clear(sender, e)
    End Sub

    Protected Sub txtContainerno_TextChanged(sender As Object, e As EventArgs) Handles txtContainerno.TextChanged
        strSql = ""
        strSql = "select EntryID,Estimate_ID from Estimate_M where ContainerNo ='" & Trim(txtContainerno.Text) & "'"
        dt3 = db.sub_GetDatatable(strSql)
        If dt3.Rows.Count > 0 Then
            lblEntryID.Text = dt3.Rows(0)("entryID")
            lblEstimat_ID.Text = dt3.Rows(0)("Estimate_ID")
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found');", True)
            Control_Clear(sender, e)
            txtContainerno.Focus()
            Exit Sub
        End If
    End Sub
End Class

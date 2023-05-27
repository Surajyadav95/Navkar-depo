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
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container is out from empty depot. Cannot proceed');", True)
                    Control_Clear(sender, e)
                    txtContainerno.Focus()
                    Exit Sub
                End If
                If Trim(dt.Rows(0)("RepairedDate") & "") <> "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container is repaired. Cannot proceed');", True)
                    Control_Clear(sender, e)
                    txtContainerno.Focus()
                    Exit Sub
                End If

                lblEntryID.Text = dt.Rows(0)("entryID")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Container is not in CFS');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()
                lblEntryID.Text = ""
                Exit Sub
            End If

            strSql = ""
            strSql = "SP_ShowApprove '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtAppdate.Text = Trim(dt1.Rows(0)("ApprovedOn") & "")
                txtAmt.Text = Trim(dt1.Rows(0)("ApprovedAmt") & "")
                txtCSC.Text = Trim(dt1.Rows(0)("CSCASP") & "")
                txtDesc.Text = Trim(dt1.Rows(0)("Descriptions") & "")
                lblEstimat_ID.Text = Trim(dt1.Rows(0)("Estimate_ID") & "")

            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Container No Already Approved ');", True)
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
            txtAppdate.Text = ""
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
        strSql = "sp_Cancel_Approved '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "','" & Trim(lblEstimat_ID.Text) & "' "
        dt2 = db.sub_GetDatatable(strSql)
        lblSession.Text = "Container Approved  successfully"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        Control_Clear(sender, e)
    End Sub
End Class

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
            FillDropDown()
            txtContainerno.Focus()
        End If
    End Sub
    Protected Sub FillDropDown()
        Try
            
            strSql = ""
            strSql = "select ContainerTypeID,ContainerType from ContainerType  "
            ds = db.sub_GetDataSets(strSql)
            ddlCtrType.DataSource = ds.Tables(0)
            ddlCtrType.DataTextField = "ContainerType"
            ddlCtrType.DataValueField = "ContainerTypeID"
            ddlCtrType.DataBind()
            ddlCtrType.Items.Insert(0, New ListItem("-Select-", 0))

          
        Catch ex As Exception

        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function  
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            If Trim(txtContainerno.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No cannot be left blank');", True)
                txtContainerno.Focus()

                Exit Sub
            End If
            strSql = ""
            strSql = "select ContainerNo, EntryID from EYard_in  where ContainerNo='" & Trim(txtContainerno.Text & "") & "' and IsCancel=0 Order By EntryID Desc"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblEntryID.Text = dt.Rows(0)("entryID")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Container is not in CFS');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()
                lblEntryID.Text = ""
                Exit Sub
            End If

            strSql = ""
            strSql = "Sp_EyardInShow '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then

                ddlSize.SelectedItem.Text = Trim(dt1.Rows(0)("Size") & "")
                ddlCtrType.SelectedValue = Trim(dt1.Rows(0)("ContainerTypeID") & "")
              
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
            
            lblEntryID.Text = ""

          
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btncancel_Click(sender As Object, e As EventArgs)
        Try
            If Trim(txtContainerno.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No cannot be left blank');", True)
                txtContainerno.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql = "select * from Estimate_M where containerno = '" & Trim(txtContainerno.Text) & "' and entryid = '" & Trim(lblEntryID.Text) & "' and iscancel=0 "
            dt2 = db.sub_GetDatatable(strSql)
            If dt2.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Estimation entry has done. Cannot proceed');", True)
                txtContainerno.Focus()
                Exit Sub
            Else

            End If

            lblquoteApprove.Text = "Are you sure to Update ?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btncancelyes_ServerClick(sender As Object, e As EventArgs)
        strSql = ""
        strSql = "Exec  USP_Update_Size_Eyard_In '" & Trim(txtContainerno.Text) & "', '" & Val(lblEntryID.Text) & "' ,  '" & Trim(ddlCtrType.SelectedValue & "") & "','" & Trim(ddlSize.SelectedItem.Text & "") & "','" & Session("UserId_DepoCFS") & "','" & Format(Now, "yyyy-MM-dd HH:mm") & "'  "
        dt3 = db.sub_GetDatatable(strSql)
        lblSession.Text = "Record Updated Successfully"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        Control_Clear(sender, e)
    End Sub
End Class

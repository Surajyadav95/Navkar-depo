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
            txtnewindate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtContainerno.Focus()
            Filldropdown()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql = "USP_EMPTY_CONTAINER_FILLDROP_DOWN"

            ds = db.sub_GetDataSets(strSql)
            

            ddlCondition.DataSource = ds.Tables(5)
            ddlCondition.DataTextField = "Name"
            ddlCondition.DataValueField = "ID"
            ddlCondition.DataBind()
            ddlCondition.Items.Insert(0, New ListItem("--Select--", 0))

             

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
           
            strSQL = ""
            strSql = "select ContainerNo ,entryID  from Eyard_in where containerNo ='" & txtContainerno.Text & "' and iscancel=0 and status='P' "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblEntryID.Text = dt.Rows(0)("entryID")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid Container Number');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()
                lblEntryID.Text = ""
                Exit Sub
            End If

            strSql = ""
            strSql = "usp_dpd_change'" & Trim(txtContainerno.Text & "") & "','" & Trim(lblEntryID.Text & "") & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtsize.Text = Trim(dt1.Rows(0)("Size") & "")
                txtlinename.Text = Trim(dt1.Rows(0)("SLName") & "")
                txttype.Text = Trim(dt1.Rows(0)("ContainerType") & "")
                ddlCondition.SelectedItem.Text = Trim(dt1.Rows(0)("Condition") & "")
           
                txtIndate.Text = Convert.ToDateTime(Trim(dt1.Rows(0)("InDate"))).ToString("yyyy-MM-ddTHH:mm")
                'txtRemarks.Text = Trim(dt1.Rows(0)("remark") & "")

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
            'txtContainerno.Text = ""
            'txtTransporter.Text = ""
            'txtSeal.Text = ""
            'txtLocation.Text = ""
            'txtVessel.Text = ""
            'txtPOD.Text = ""
            'txtShipper.Text = ""
            'lblEntryID.Text = ""
            'txtVehicle.Text = ""

          
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Try
            If Trim(txtContainerno.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No cannot be left blank');", True)
                txtContainerno.Focus()
                Exit Sub
            End If
            lblquoteApprove.Text = "Are you sure to update ?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btncancelyes_ServerClick(sender As Object, e As EventArgs)
        strSql = ""
        strSql = " usp_update_DPD_change '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "','" & Convert.ToDateTime(Trim(txtnewindate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtRemarks.Text) & "','" & Trim(ddlCondition.SelectedItem.Text & "") & "'"
        dt3 = db.sub_GetDatatable(strSql)
        Call db.AmmendmentLog(" " & Trim(txtContainerno.Text) & " DPD In details'" & Trim(lblEntryID.Text) & "'in EYardIn ", Session("UserId_DepoCFS"))
        lblSession.Text = "Record Updated Successfully"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        Control_Clear(sender, e)
    End Sub

    Protected Sub txtContainerno_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_create_Check_DPDIn '" & Trim(txtContainerno.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' " + dt.Rows(0)("msg") + ".');", True)
                txtContainerno.Text = ""
                Exit Sub

            Else
               
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class

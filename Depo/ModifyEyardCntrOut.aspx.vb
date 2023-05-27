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
            strSql = ""
            strSql += "select VesselID,VesselName from vessels"
            dt = db.sub_GetDatatable(strSql)
            ddlVesselName.DataSource = dt
            ddlVesselName.DataValueField = "VesselID"
            ddlVesselName.DataTextField = "VesselName"
            ddlVesselName.DataBind()
            ddlVesselName.Items.Insert(0, New ListItem("--Select--", 0))
            txtContainerno.Focus()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function  
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
           
            strSQL = ""
            strSql = "select top 1 ContainerNo ,entryID  from EYardEmptyOut where containerNo ='" & txtContainerno.Text & "' and iscancel=0 order by outdate desc"
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
            strSql = "usp_Depo_Modify_OutDate'" & Trim(txtContainerno.Text & "") & "','" & Trim(lblEntryID.Text & "") & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtTransporter.Text = Trim(dt1.Rows(0)("Transporter") & "")
                txtSeal.Text = Trim(dt1.Rows(0)("SealNo") & "")
                txtLocation.Text = Trim(dt1.Rows(0)("Location") & "")
                ddlVesselName.SelectedValue = Trim(dt1.Rows(0)("VesselName") & "")
                txtPOD.Text = Trim(dt1.Rows(0)("PortName") & "")
                txtShipper.Text = Trim(dt1.Rows(0)("ShipperName") & "")
                txtVehicle.Text = Trim(dt1.Rows(0)("VehicleNo") & "")
                txtoutdate.Text = Convert.ToDateTime(Trim(dt1.Rows(0)("OutDate"))).ToString("yyyy-MM-ddTHH:mm")
                txtRemarks.Text = Trim(dt1.Rows(0)("remark") & "")
                txtBooking.Text = Trim(dt1.Rows(0)("bookingno") & "")
                Dim strMovType As String = Trim(dt1.Rows(0)("Movtype") & "")
                ddlMovementtype.SelectedValue = strMovType
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
            txtTransporter.Text = ""
            txtSeal.Text = ""
            txtLocation.Text = ""
            ddlVesselName.SelectedValue = 0
            txtPOD.Text = ""
            txtShipper.Text = ""
            lblEntryID.Text = ""
            txtVehicle.Text = ""
            ddlMovementtype.SelectedValue = ""
          
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
        If Trim(txtSeal.Text) <> "" Then


            strSql = ""
            strSql = " select * from Import_SealMaster where  PrefixNo ='" & Trim(txtSeal.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then

            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified Seal No not found in stock. Cannot proceed.');", True)
                txtSeal.Text = ""
                txtSeal.Focus()
                Exit Sub
            End If
        End If
        strSql = ""
        strSql = " sp_UpdateEYardEmptyOut '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "','" & Trim(txtTransporter.Text) & "','" & Trim(txtLocation.Text) & "' ,"
        strSql += " '" & Trim(txtSeal.Text) & "','" & Trim(txtPOD.Text) & "','" & Val(ddlVesselName.SelectedValue) & "','" & Trim(txtShipper.Text) & "','" & Trim(txtVehicle.Text) & "',' " & Convert.ToDateTime(Trim(txtoutdate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtRemarks.Text & "") & "','" & Trim(txtBooking.Text & "") & "','" & Trim(ddlMovementtype.SelectedValue & "") & "'"
        dt3 = db.sub_GetDatatable(strSql)
        Call db.AmmendmentLog(" " & Trim(txtContainerno.Text) & " modify out details'" & Trim(lblEntryID.Text) & "'in eyardemptyout ", Session("UserId_DepoCFS"))
        lblSession.Text = "Record Updated Successfully"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        Control_Clear(sender, e)
    End Sub

    Protected Sub txtoutdate_TextChanged(sender As Object, e As EventArgs)
        Try
            If Not Convert.ToDateTime(Trim(txtoutdate.Text & "")).ToString("yyyy-MM-dd HH:mm") < Convert.ToDateTime(Trim(txtoutdate.Text & "")).ToString("yyyy-MM-dd HH:mm") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Outdate Cannot be greater than today date. Cannot proceed.');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

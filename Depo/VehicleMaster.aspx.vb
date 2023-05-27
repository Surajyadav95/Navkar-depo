Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim TrailerId As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserIDPRE_Bond") Is Nothing Then
        '    Session("UserIDPRE_Bond") = Request.Cookies("UserIDPRE_Bond").Value
        '    'Session("Dept") = Request.Cookies("Dept").Value
        '    Session("UserNamePRE_Bond") = Request.Cookies("UserNamePRE_Bond").Value
        '    ''Session("PROFILEURL") = Request.Cookies("PROFILEURL").Value
        '    'Session("Location") = Request.Cookies("Location").Value
        '    ''Session("LOcationId") = Request.Cookies("LOcationId").Value
        '    'Session("ID") = Response.Cookies("ID").Value
        '    'Session("CompID") = Response.Cookies("CompID").Value
        '    'Session("Workyear") = Response.Cookies("Workyear").Value
        'End If
        If Not IsPostBack Then

            txtRegdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            btnsearch_Click(sender, e)
            Filldropdown()
            If Not (Request.QueryString("trailerIDEdit") = "") Then
                TrailerId = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("trailerIDEdit")))
                strSql = ""
                strSql = "USP_Edit_trailer '" & TrailerId & "','" & Session("UserId_DepoCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    LbltrailerID.Text = Trim(dt.Rows(0)("trailerid") & "")
                    txtRegNo.Text = Trim(dt.Rows(0)("trailerid") & "")
                    txtTrailerNo.Text = Trim(dt.Rows(0)("trailername") & "")
                    ddlOwnedBy.SelectedValue = Trim(dt.Rows(0)("OwnedBy") & "")
                    ddlDriverName.SelectedValue = Trim(dt.Rows(0)("DriverID") & "")
                    ddltransporter.SelectedValue = Trim(dt.Rows(0)("Transporter") & "")
                    ddlvehicletype.SelectedValue = Trim(dt.Rows(0)("VehicleType") & "")
                    ddltrailertype.SelectedValue = Trim(dt.Rows(0)("trailerType") & "")
                    txtRegdate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("RegDate") & "")).ToString("yyyy-MM-ddTHH:mm")
                    chkisActive.Checked = Trim(dt.Rows(0)("isactive") & "")
                   
                End If

                btnSave.Text = "Update"
            End If

        End If
    End Sub
 Public Sub grid()
        strSql = ""
        strSql += "USP_Get_Fuel_Entry_Dets'" & Trim(txtsearchm.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("usp_trailers_dropdown")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlOwnedBy.DataSource = ds.Tables(0)
                ddlOwnedBy.DataTextField = "Trailer_Mov_type"
                ddlOwnedBy.DataValueField = "ID"
                ddlOwnedBy.DataBind()
                ddlOwnedBy.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                ddlDriverName.DataSource = ds.Tables(1)
                ddlDriverName.DataTextField = "drivername"
                ddlDriverName.DataValueField = "driverid"
                ddlDriverName.DataBind()
                ddlDriverName.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                ddltransporter.DataSource = ds.Tables(2)
                ddltransporter.DataTextField = "TransName"
                ddltransporter.DataValueField = "TransID"
                ddltransporter.DataBind()
                ddltransporter.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                ddlvehicletype.DataSource = ds.Tables(3)
                ddlvehicletype.DataTextField = "VehicleType"
                ddlvehicletype.DataValueField = "VehicleTypeID"
                ddlvehicletype.DataBind()
                ddlvehicletype.Items.Insert(0, New ListItem("--Select--", 0))
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
     Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If (btnSave.Text = "Update") Then
                strSql = ""
                strSql += "USP_UPDATE_TRAILERS'" & Trim(LbltrailerID.Text & "") & "','" & Convert.ToDateTime(Trim(txtRegdate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtTrailerNo.Text & "") & "','" & Trim(ddlOwnedBy.SelectedValue & "") & "',"
                strSql += "'" & Trim(ddlDriverName.SelectedValue & "") & "','" & Trim(ddlDriverName.SelectedValue & "") & "','" & Trim(ddltransporter.SelectedValue & "") & "',"
                strSql += "'" & Trim(ddlvehicletype.SelectedValue & "") & "','" & Trim(ddltrailertype.SelectedValue & "") & "','" & Trim(chkisActive.Checked & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record Updated successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            Else

                strSql = ""
                strSql = "select * from trailers where trailername ='" & Trim(txtTrailerNo.Text & "") & "' "
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    txtTrailerNo.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Trailer name already exists .');", True)
                    txtTrailerNo.Focus()
                End If

                strSql = ""
                strSql = " SELECT ISNULL( MAX(trailerid),0) FROM trailers with(xlock) "
                Dim dt1 As New DataTable
                dt1 = db.sub_GetDatatable(strSql)
                txtRegNo.Text = Val(dt1.Rows(0)(0)) + 1

                strSql = ""
                strSql += "USP_INSERT_TRAILERS'" & Convert.ToDateTime(Trim(txtRegdate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtTrailerNo.Text & "") & "','" & Trim(ddlOwnedBy.SelectedValue & "") & "',"
                strSql += "'" & Trim(ddlDriverName.SelectedValue & "") & "','" & Trim(ddlDriverName.SelectedValue & "") & "','" & Trim(ddltransporter.SelectedValue & "") & "',"
                strSql += "'" & Trim(ddlvehicletype.SelectedValue & "") & "','" & Trim(ddltrailertype.SelectedValue & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_DepoCFS") & "'"
                dt = db.sub_GetDatatable(strSql)

                strSql = ""
                strSql = "INSERT INTO driverTruckAllotment(vehicleNo,trailerID,driverid,allotmentdate,drivername,addedby,addedon)"
                strSql += " values('" & Trim(txtTrailerNo.Text & "") & "','" & Trim(txtRegNo.Text) & "','" & Trim(ddlDriverName.SelectedValue & "") & "','" & Convert.ToDateTime(Trim(txtRegdate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlDriverName.SelectedItem.Text & "") & "'," & Session("UserId_DepoCFS") & ",getdate()) "
                db.sub_ExecuteNonQuery(strSql)
                lblSession.Text = "Record saved successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "SP_FillTrailerMasterDets'" & Trim(txtsearchm.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

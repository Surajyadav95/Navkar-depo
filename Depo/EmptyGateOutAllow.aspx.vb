Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt10 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim TariffID, TariffIDView As String
    Dim AutoTempe, GSTIDView As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserId_DepoCFS") Is Nothing Then
        '    Session("UserId_DepoCFS") = Request.Cookies("UserIDPRE_Bond").Value
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
            db.sub_ExecuteNonQuery("delete from Temp_Eyard_Allow_Size where USER_ID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("delete from Team_Eyard_Out_Vehicle where USERID=" & Session("UserId_DepoCFS") & "")
            txtgateOutAllowDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtdovalidity.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            grid()
            grid1()
            Filldropdown()
            txtbookingNo.Focus()
    
            'Panel1.Enabled = False
            'Panel3.Enabled = False
            'Panel4.Enabled = False
            'Panel5.Enabled = False
        End If

    End Sub
    Protected Sub Filldropdown()
        Try

          

            ds = db.sub_GetDataSets("usp_gate_allow_fill")
            If (ds.Tables(5).Rows.Count > 0) Then
                ddlpod.DataSource = ds.Tables(5)
                ddlpod.DataTextField = "PODName"
                ddlpod.DataValueField = "PODID"
                ddlpod.DataBind()
                ddlpod.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                ddllinename.DataSource = ds.Tables(1)
                ddllinename.DataTextField = "SLName"
                ddllinename.DataValueField = "slid"
                ddllinename.DataBind()
                ddllinename.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                ddllocations.DataSource = ds.Tables(2)
                ddllocations.DataTextField = "location"
                ddllocations.DataValueField = "LocationID"
                ddllocations.DataBind()
                ddllocations.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            'If (ds.Tables(3).Rows.Count > 0) Then
            '    ddltransporter.DataSource = ds.Tables(3)
            '    ddltransporter.DataTextField = "TransName"
            '    ddltransporter.DataValueField = "TransID"
            '    ddltransporter.DataBind()
            '    ddltransporter.Items.Insert(0, New ListItem("--Select--", 0))
            'End If

            If (ds.Tables(4).Rows.Count > 0) Then
                ddl20stype.DataSource = ds.Tables(4)
                ddl20stype.DataTextField = "ContainerType"
                ddl20stype.DataValueField = "ContainerTypeID"
                ddl20stype.DataBind()
                ddl20stype.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(4).Rows.Count > 0) Then
                ddl40type.DataSource = ds.Tables(4)
                ddl40type.DataTextField = "ContainerType"
                ddl40type.DataValueField = "ContainerTypeID"
                ddl40type.DataBind()
                ddl40type.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(4).Rows.Count > 0) Then
                ddl45type.DataSource = ds.Tables(4)
                ddl45type.DataTextField = "ContainerType"
                ddl45type.DataValueField = "ContainerTypeID"
                ddl45type.DataBind()
                ddl45type.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(6).Rows.Count > 0) Then
                ddlvesselName.DataSource = ds.Tables(6)
                ddlvesselName.DataTextField = "VesselName"
                ddlvesselName.DataValueField = "VesselID"
                ddlvesselName.DataBind()
                ddlvesselName.Items.Insert(0, New ListItem("--Select--", 0))
            End If


            If (ds.Tables(7).Rows.Count > 0) Then
                ddlports.DataSource = ds.Tables(7)
                ddlports.DataTextField = "PortName"
                ddlports.DataValueField = "portID"
                ddlports.DataBind()
                ddlports.Items.Insert(0, New ListItem("--Select--", 0))
            End If


        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub grid()
        strSql = ""
        dt = db.sub_GetDatatable("usp_temp_allow_fill '" & Session("UserId_DepoCFS") & "'")
        grdOutAllow.DataSource = dt
        grdOutAllow.DataBind()
        UpdatePanel2.Update()
    End Sub

    Public Sub grid1()
        dt4 = db.sub_GetDatatable("usp_vehicle_fill '" & Session("UserId_DepoCFS") & "'")
        grdVehicler.DataSource = dt4
        grdVehicler.DataBind()
        UpdatePanel2.Update()
    End Sub
    Protected Sub txtbookingNo_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "SELECT BookingNo FROM Eyard_Gate_Out_Allow_M WHERE BookingNo='" & Trim(txtbookingNo.Text & "") & "' and IsCancel =0"
            dt8 = db.sub_GetDatatable(strSql)
            If (dt8.Rows.Count > 0) Then
                txtbookingNo.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('booking No Already Exists .');", True)
                txtbookingNo.Focus()
                Exit Sub
            End If
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim strTableName As String = "", StrCol As String = "", strsub As String
            Dim intTick As Integer = 0
            If (Val(txttotal20.Text & "") <> 0) And Val(ddl20stype.SelectedValue & "") = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Select Type Of 20s');", True)
                'MsgBox("Please Select Type Of 20's")
                ddl20stype.Focus()
                Exit Sub
            End If
            If (Val(txttotal40.Text & "") <> 0) And Trim(ddl40type.SelectedValue & "") = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Select Type Of 40s');", True)
                'MsgBox("Please Select Type Of 40's")
                ddl40type.Focus()
                Exit Sub
            End If
            If (Val(txttotal45.Text & "") <> 0) And Trim(ddl45type.SelectedValue & "") = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Select Type Of 45s');", True)
                'MsgBox("Please Select Type Of 45's")
                ddl45type.Focus()
                Exit Sub
            End If
            If (Val(txttotal20.Text & "") = 0) And Val(ddl20stype.SelectedValue & "") <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter Qty Of 20s');", True)
                'MsgBox("Please Enter Qty Of 20's")
                txttotal20.Focus()
                Exit Sub
            End If
            If (Val(txttotal40.Text & "") = 0) And Val(ddl40type.SelectedValue & "") <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter Qty Of 40s');", True)
                'MsgBox("Please Enter Qty Of 40's")
                txttotal40.Focus()
                Exit Sub
            End If
            If (Val(txttotal45.Text & "") = 0) And Val(ddl45type.SelectedValue & "") <> 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter Qty Of 45s');", True)
                'MsgBox("Please Enter Qty Of 45's")
                txttotal45.Focus()
                Exit Sub
            End If
            If Trim(txttotal20.Text & "") = Trim(txttotal40.Text & "") And (Val(txttotal45.Text & "") = 0) And (Val(txttotal20.Text & "") = 0) And Trim(txttotal20.Text & "") = Trim(txttotal45.Text & "") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter atleast 1 field Qty');", True)
                'MsgBox("Please Enter atleast 1  feild Qty ")
                txttotal20.Focus()
                Exit Sub
            End If
            Dim strtype20 As String = ""
            Dim strtype40 As String = ""
            Dim strtype45 As String = ""
            If Trim(ddl20stype.SelectedValue & "") <> 0 Then
                strtype20 = Trim(ddl20stype.SelectedItem.Text & "")
            End If
            If Trim(ddl40type.SelectedValue & "") <> 0 Then
                strtype40 = Trim(ddl40type.SelectedItem.Text & "")
            End If
            If Trim(ddl45type.SelectedValue & "") <> 0 Then
                strtype45 = Trim(ddl45type.SelectedItem.Text & "")
            End If

            strSql = ""
            strSql = "USP_INSERT_TEMP_EYARD_ALLOW_SIZE'" & Trim(txttotal20.Text & "") & "','" & Trim(ddl20stype.SelectedValue & "") & "','" & Trim(txttotal40.Text & "") & "',"
            strSql += "'" & Trim(ddl40type.SelectedValue & "") & "','" & Trim(txttotal45.Text & "") & "','" & Trim(ddl45type.SelectedValue & "") & "','" & Session("UserId_DepoCFS") & "'"
            dt3 = db.sub_GetDatatable(strSql)
            grid()
            'Container_In(sender, e)
            Dim intContainer As Integer = 0, intNoofContainer As Integer = 0
            For Each row As GridViewRow In grdOutAllow.Rows
                intContainer += Val(CType(row.FindControl("lblC20s"), Label).Text & "")
                intContainer += Val(CType(row.FindControl("lblC40s"), Label).Text & "")
                intContainer += Val(CType(row.FindControl("lblC45s"), Label).Text & "")
            Next
            For Each row As GridViewRow In grdVehicler.Rows

                intNoofContainer += Val(CType(row.FindControl("lblNoofContainer"), Label).Text & "")
            Next
            If intContainer < intNoofContainer Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Maximum number of Container exceed. First Remove vehicles.');", True)
                'MsgBox("Maximum number of Container exceed. First Remove vehicles.")

                Exit Sub
            End If
            txttotal20.Text = ""
            txttotal40.Text = ""
            txttotal45.Text = ""
            ddl20stype.SelectedValue = 0
            ddl40type.SelectedValue = 0
            ddl45type.SelectedValue = 0
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnaddvehi_Click(sender As Object, e As EventArgs)
        Try
            If Trim(txtvehicleno.Text) = "" Then
                MsgBox("Please Enter Vehicle No")
                txtvehicleno.Focus()
                Exit Sub
            End If
            If ddlNoofcontainer.SelectedIndex = -1 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Select No of Container ');", True)
                'MsgBox("Please Select No of Container ")
                ddlNoofcontainer.Focus()
                Exit Sub
            End If

            Dim intContainer As Integer = 0, intNoofContainer As Integer = 0
            For Each row As GridViewRow In grdOutAllow.Rows

                intContainer += Val(CType(row.FindControl("lblC20s"), Label).Text & "")
                intContainer += Val(CType(row.FindControl("lblC40s"), Label).Text & "")
                intContainer += Val(CType(row.FindControl("lblC45s"), Label).Text & "")
            Next

            For Each row As GridViewRow In grdVehicler.Rows

                intNoofContainer += Val(CType(row.FindControl("lblNoofContainer"), Label).Text & "")
            Next
            intNoofContainer = intNoofContainer + ddlNoofcontainer.Text
            If intContainer < intNoofContainer Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Maximum number of Container exceed'.);", True)
                'MsgBox("Maximum number of Container exceed.")

                Exit Sub
            End If

            strSql = ""
            strSql = "USP_INSERT_TEAM_EYARD_OUT_VEHICLE'" & Trim(txtvehicleno.Text & "") & "','" & Trim(ddlNoofcontainer.SelectedValue & "") & "','" & Session("UserId_DepoCFS") & "'"
            dt5 = db.sub_GetDatatable(strSql)
            grid1()
            txtvehicleno.Text = ""
            ddlNoofcontainer.SelectedValue = 1
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim intContainer As Integer = 0, intNoofContainer As Integer = 0
            For Each row As GridViewRow In grdOutAllow.Rows
                intContainer += Val(CType(row.FindControl("lblC20s"), Label).Text & "")
                intContainer += Val(CType(row.FindControl("lblC40s"), Label).Text & "")
                intContainer += Val(CType(row.FindControl("lblC45s"), Label).Text & "")
            Next
            For Each row As GridViewRow In grdVehicler.Rows
                intNoofContainer += Val(CType(row.FindControl("lblNoofContainer"), Label).Text & "")
            Next
            'intNoofContainer = intNoofContainer + ddlNoofcontainer.Text
            If intContainer < intNoofContainer Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "alert('maximum number of container exceed');", True)
                'msgbox("maximum number of container exceed.")
                Exit Sub
            End If
            Dim blnGrid As Boolean = False
            For Each row In grdOutAllow.Rows
                blnGrid = True
                Exit For
            Next
            If blnGrid = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "alert('No details to save');", True)
                Exit Sub
            End If
            Dim IntBillNo As Integer
            Dim tot20 As Integer = 0
            Dim tot40 As Integer = 0
            Dim tot45 As Integer = 0
            Dim strAllID As String = ""
            strAllID = txtgateOutAllow.Text
            If txtgateOutAllow.Text = "" Then
                strSql = ""
                strSql = "Select IsNull(MAX (GateAllowID ),0) from Eyard_Gate_Out_Allow_M WITH(XLOCK)  where IsCancel=0  "
                dt6 = db.sub_GetDatatable(strSql)
                If dt6.Rows.Count > 0 Then
                    IntBillNo = dt6.Rows(0)(0) + 1
                Else
                    IntBillNo = 1
                End If
            Else
                IntBillNo = txtgateOutAllow.Text
            End If
            For Each row As GridViewRow In grdOutAllow.Rows
                tot20 = tot20 + Val(CType(row.FindControl("lblC20s"), Label).Text & "")
                tot40 = tot40 + Val(CType(row.FindControl("lblC40s"), Label).Text & "")
                tot45 = tot45 + Val(CType(row.FindControl("lblC45s"), Label).Text & "")
            Next
            strSql = ""
            strSql = "delete from Eyard_Gate_Out_Allow_D where GateAllowID =" & IntBillNo & ""
            db.sub_ExecuteNonQuery(strSql)
            strSql = ""
            strSql = "delete from Eyard_Gate_Out_Vehicle_D where AllowID=" & txtgateOutAllow.Text & " and isout=0"
            db.sub_ExecuteNonQuery(strSql)

            strSql = ""
            strSql = "USP_INSERT_EYARD_GATE_OUT_ALLOW_M '" & IntBillNo & "','" & Convert.ToDateTime(Trim(txtgateOutAllowDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtbookingNo.Text & "") & "','" & Trim(ddllinename.SelectedValue & "") & "','" & Trim(txtlineId.Text & "") & "','" & Trim(ddllinename.SelectedItem.Text & "") & "',"
            strSql += "'" & tot20 & "','" & tot40 & "','" & tot45 & "','" & Session("UserId_DepoCFS") & "','" & Trim(lblDONo.Text & "") & "','" & Convert.ToDateTime(Trim(txtdovalidity.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtshippername.Text & "") & "','" & Trim(ddllocations.SelectedItem.Text) & "',"
            strSql += "'" & Trim(ddlpod.SelectedItem.Text & "") & "','" & Trim(txttransport.Text) & "','" & Trim(txtbookingRemarks.Text & "") & "','" & Trim(ddlvesselName.SelectedValue & "") & "','" & Trim(ddlports.SelectedValue & "") & "','" & Trim(txtSealNo.Text & "") & "','" & Trim(ddlMovementtype.SelectedValue & "") & "','" & Trim(txtJOReference.Text & "") & "'"
            dt7 = db.sub_GetDatatable(strSql)

            txtgateOutAllow.Text = IntBillNo

            Clear()
            txtgateOutAllow.Text = ""
            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddllinename_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "SELECT said FROM shiplines WHERE slid=" & ddllinename.SelectedValue & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtlineId.Text = Trim(dt.Rows(0)("said") & "")
            End If
            UpdatePanel2.Update()
            'ddllocations.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub Clear()
        Try
            db.sub_ExecuteNonQuery("delete from Temp_Eyard_Allow_Size where USER_ID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("delete from Team_Eyard_Out_Vehicle where USERID=" & Session("UserId_DepoCFS") & "")
            txtgateOutAllowDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtdovalidity.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            grid()
            grid1()
            txtbookingNo.Text = ""
            Filldropdown()
            txtlineId.Text = ""
            txtshippername.Text = ""
            txtvehicleno.Text = ""
            ddlNoofcontainer.SelectedValue = 1
            txttotal20.Text = ""
            txttotal40.Text = ""
            txttotal45.Text = ""
            ddl20stype.SelectedValue = 0
            ddl40type.SelectedValue = 0
            ddl45type.SelectedValue = 0
            txtbookingNo.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select *  from Temp_Empty_Booking_search where userid='" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtbookingNo.Text = Trim(dt.Rows(0)("BookingNo") & "")

            End If
            Call btnIndentshow(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Container_In(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_size_type '" & txtgateOutAllow.Text & "'"
            db.sub_ExecuteNonQuery(strSql)
            db.sub_ExecuteNonQuery("usp_insert_temp_size_type '" & Session("UserId_DepoCFS") & "','" & Trim(txtgateOutAllow.Text & "") & "'")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentshow(sender As Object, e As EventArgs)
        strSql = ""
        strSql = "usp_bookin_serch_Fill'" & Trim(txtbookingNo.Text & "") & "','" & Session("UserId_DepoCFS") & "'"
        dt1 = db.sub_GetDatatable(strSql)
        If dt1.Rows.Count > 0 Then
            txtgateOutAllow.Text = Trim(dt1.Rows(0)("GateAllowID") & "")
            lblDONo.Text = Trim(dt1.Rows(0)("DONo") & "")
            ddllocations.SelectedValue = Trim(dt1.Rows(0)("LOCATIONID") & "")
            txtshippername.Text = Trim(dt1.Rows(0)("shipperName") & "")
            txtlineId.Text = Trim(dt1.Rows(0)("lineID") & "")
            ddllinename.SelectedValue = Trim(dt1.Rows(0)("lineID") & "")
            ddlpod.SelectedValue = Trim(dt1.Rows(0)("PODID") & "")
            txttransport.Text = Trim(dt1.Rows(0)("Transporter") & "")
            txtgateOutAllowDate.Text = Convert.ToDateTime(Trim(dt1.Rows(0)("AllowDate") & "")).ToString("yyyy-MM-ddTHH:mm")
            txtdovalidity.Text = Convert.ToDateTime(Trim(dt1.Rows(0)("DoValidity") & "")).ToString("yyyy-MM-ddTHH:mm")
            ddlvesselName.SelectedValue = Val(dt1.Rows(0)("VesselID") & "")
            txtvesselid.Text = Val(dt1.Rows(0)("VesselID") & "")
            ddlports.SelectedValue = Val(dt1.Rows(0)("Ports") & "")
            txtSealNo.Text = Trim(dt1.Rows(0)("SealNo") & "")
            ddlMovementtype.SelectedValue = Trim(dt1.Rows(0)("Movement_Type") & "")
            grid()
            Panel1.Enabled = False
            strSql = "EXEC SP_GetVehicleDets '" & Trim(txtbookingNo.Text & "") & "'"
            dt2 = db.sub_GetDatatable(strSql)
            grdVehicler.DataSource = dt2
            grdVehicler.DataBind()


        Else
            txtgateOutAllow.Text = ""
            lblDONo.Text = ""
            ddllocations.SelectedValue = 0
            txtshippername.Text = ""
            txtlineId.Text = ""
            ddllinename.SelectedValue = 0
            ddlpod.SelectedValue = 0
            txttransport.Text = ""
            txtgateOutAllowDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtdovalidity.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            grid()
            grid1()
            Panel1.Enabled = True
        End If
        UpdatePanel5.Update()
        UpdatePanel2.Update()
        ddlpod.Focus()
    End Sub

    Protected Sub ddlvesselName_SelectedIndexChanged(sender As Object, e As EventArgs)
        strSql = ""
        strSql += "SELECT ViaNo FROM Movement WHERE VesselID=" & ddlvesselName.SelectedValue & ""
        dt = db.sub_GetDatatable(strSql)
        If dt.Rows.Count > 0 Then
            txtvesselid.Text = Trim(dt.Rows(0)("ViaNo") & "")
        End If
        UpdatePanel2.Update()
        ddllocations.Focus()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try

       
        Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
        Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
        Dim AutoID As String = lnkRemove.CommandArgument
            dt = db.sub_GetDatatable("USP_Delete_Gate_Out_allow '" & AutoID & "','" & Session("UserId_DepoCFS") & "'")
            grid()
        If (dt.Rows.Count > 0) Then
            End If
            'up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
      
    End Sub

    Protected Sub lnkEdit_Click1(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            
            strSql = ""
            strSql = "USP_Edit_Gate_Out_allow'" & AutoID & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txttotal20.Text = Trim(dt.Rows(0)("Total20") & "")
                ddl20stype.SelectedValue = Trim(dt.Rows(0)("TypeID_20") & "")
                txttotal40.Text = Trim(dt.Rows(0)("Total40") & "")
                ddl40type.SelectedValue = Val(dt.Rows(0)("TypeID_40") & "")
                txttotal45.Text = Trim(dt.Rows(0)("Total45") & "")
                ddl45type.SelectedValue = Trim(dt.Rows(0)("TypeID_45") & "")
            End If
            grid()

        Catch ex As Exception

        End Try
    End Sub
End Class

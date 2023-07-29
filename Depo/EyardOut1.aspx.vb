Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11, dt12, dt13, dt14 As DataTable
    Dim db As New dbOperation_Depo
    Dim dbamt As New dbAmountInWords
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim strcontainerstatus As Boolean
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim dblNetAmount As Double
    Dim dbltotalamount As Double = 0

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
            db.sub_ExecuteNonQuery("delete from Temp_Empty_Out where USERID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("delete from TEMP_MODE_CONTAINER where USERID=" & Session("UserId_DepoCFS") & "")
            txtGatePassDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            'txtmodate.Text = Convert.ToDateTime(Now).ToString("dd-MM-yyyy")
            Filldropdown()
            grid()
            grid2()
            txtBookingNo.Focus()

        End If

    End Sub
    Protected Sub grid2()
        Try
            Dim dt As DataTable
            dt = db.sub_GetDatatable("usp_mode_fill_container '" & Session("UserId_DepoCFS") & "'")
            grdMode.DataSource = dt
            grdMode.DataBind()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_FILL_DROPDOWN_EYARD_OUT"
            ds = db.sub_GetDataSets(strSql)
            ddlTruckNo.DataSource = ds.Tables(0)
            ddlTruckNo.DataTextField = "Vehicle No"
            ddlTruckNo.DataValueField = "EntryID"
            ddlTruckNo.DataBind()
            ddlTruckNo.Items.Insert(0, New ListItem("--Select--", 0))

            ddlTransporter.DataSource = ds.Tables(1)
            ddlTransporter.DataTextField = "Name"
            ddlTransporter.DataValueField = "ID"
            ddlTransporter.DataBind()
            ddlTransporter.Items.Insert(0, New ListItem("--Select--", 0))

            ddlVesselName.DataSource = ds.Tables(2)
            ddlVesselName.DataTextField = "VesselName"
            ddlVesselName.DataValueField = "VesselID"
            ddlVesselName.DataBind()
            ddlVesselName.Items.Insert(0, New ListItem("--Select--", 0))

            ddlmode.DataSource = ds.Tables(3)
            ddlmode.DataTextField = "Name"
            ddlmode.DataValueField = "ID"
            ddlmode.DataBind()
            ddlmode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlbankname.DataSource = ds.Tables(4)
            ddlbankname.DataTextField = "bankname"
            ddlbankname.DataValueField = "bankID"
            ddlbankname.DataBind()
            ddlbankname.Items.Insert(0, New ListItem("--Select--", 0))

            ddllocation.DataSource = ds.Tables(5)
            ddllocation.DataTextField = "Location"
            ddllocation.DataValueField = "LocationID"
            ddllocation.DataBind()
            ddllocation.Items.Insert(0, New ListItem("--Select--", 0))


        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grid()
        Try
            dt7 = db.sub_GetDatatable("USP_TEMP_GRID_EMPTY_OUT '" & Session("UserId_DepoCFS") & "'")
            GrdHoldDetails.DataSource = dt7
            grdHoldDetails.DataBind()

            Dim dblamount As Double = 0
            For i As Integer = 0 To dt7.Rows.Count - 1
                dblamount = dblamount + Val(dt7.Rows(i)("NetAmount") & "")
            Next
            If dt7.Rows.Count > 0 Then
                lnksearch.Visible = False
            Else
                lnksearch.Visible = True
            End If
            txttotalamount.Text = dblamount
            txtsgst.Text = Format(dblamount * (dblSGST / 100), "0.00")
            txtcgst.Text = Format(dblamount * (dblCGST / 100), "0.00")
            txtigst.Text = Format(dblamount * (dblIGST / 100), "0.00")
            txtgrandtotal.Text = Val(txttotalamount.Text) + Val(txtsgst.Text) + Val(txtcgst.Text) + Val(txtigst.Text)
            txtgrandtotal.Text = Math.Round(Val(txtgrandtotal.Text))
            lblTaxID.Text = dbltaxgroupid
            HoldUpdatePanel.Update()
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            Dim dtSizeCheck As DataTable
            Dim dtCount As DataTable
            Dim straccountname As String = ""
            Dim countrunning As Integer = 0
            Dim Size As Integer = 0
            countrunning = 1
            For Each row As GridViewRow In grdHoldDetails.Rows
                countrunning = countrunning + 1
                Size = Val(CType(row.FindControl("lblSize"), Label).Text & "")
                strSql = ""
                strSql = "exec  USP_EXP_IN_Allow_Validation_new '" & Trim(txtBookingNo.Text & "") & "','" & Size & "','" & countrunning & "'"
                dtCount = db.sub_GetDatatable(strSql)
                If dtCount.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' " + dtCount.Rows(0)("msg") + ".');", True)
                    txtContainer.Text = ""
                    lblEntryID.Text = ""
                    txtType.Text = ""
                    TxtSize.Text = ""
                    lblisocode.Text = ""

                    txtContainer.Focus()
                    Exit Sub
                End If

            Next
            If (lbllineid.Text <> "371") Then
                If Len(Trim(txtContainer.Text)) <> "11" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid container No');", True)
                    txtContainer.Text = ""
                    lblEntryID.Text = ""
                    txtType.Text = ""
                    TxtSize.Text = ""
                    lblisocode.Text = ""
                    txtContainer.Focus()
                    Exit Sub
                End If
            End If
            
            If Trim(TxtSize.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter container No again');", True)
                txtContainer.Text = ""
                lblEntryID.Text = ""
                txtType.Text = ""
                TxtSize.Text = ""
                lblisocode.Text = ""
                txtContainer.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql = "USP_EYARD_CTR_HOLD '" & Trim(txtContainer.Text) & " '"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified Container no is on hold again hold reasons  ');", True)
                txtContainer.Text = ""
                lblEntryID.Text = ""
                txtType.Text = ""
                TxtSize.Text = ""
                lblisocode.Text = ""
                txtContainer.Focus()
                Exit Sub
            End If



            strSql = ""
            strSql = "USP_EYARD_BK_HOLD '" & Trim(txtBookingNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified Booking no is on hold again hold reasons');", True)
                txtContainer.Text = ""
                lblEntryID.Text = ""
                txtType.Text = ""
                TxtSize.Text = ""
                lblisocode.Text = ""

                txtContainer.Focus()
                Exit Sub
            End If
            If Trim(txtType.Text & "") <> "FR" Then
                If grdHoldDetails.Rows.Count >= 2 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container more than 2 is Not allowed! ');", True)
                    txtContainer.Text = ""
                    lblEntryID.Text = ""
                    txtType.Text = ""
                    TxtSize.Text = ""
                    lblisocode.Text = ""
                    Exit Sub
                End If
            End If
            If Trim(txtBookingNo.Text & "") <> "" Then

                strSql = ""
                strSql = "exec  sp_check_gatepassbookingNo '" & Trim(txtBookingNo.Text & "") & "','" & Trim(txtType.Text & "") & "'"
                dtSizeCheck = db.sub_GetDatatable(strSql)
                Dim blnSizeCheck As Boolean = False
                If dtSizeCheck.Rows.Count > 0 Then
                    For i = 0 To dtSizeCheck.Rows.Count - 1
                        If (Val(dtSizeCheck.Rows(i)("20'")) > 0 And TxtSize.Text = "20" And Trim(dtSizeCheck.Rows(i)("Type 20")) = Trim(txtType.Text & "")) Then
                            blnSizeCheck = True
                            Exit For
                        End If
                        If (Val(dtSizeCheck.Rows(i)("40'")) > 0 And TxtSize.Text = "40" And Trim(dtSizeCheck.Rows(i)("Type 40")) = Trim(txtType.Text & "")) Then
                            blnSizeCheck = True
                            Exit For
                        End If
                        If (Val(dtSizeCheck.Rows(i)("45'")) > 0 And TxtSize.Text = "45" And Trim(dtSizeCheck.Rows(i)("Type 45")) = Trim(txtType.Text & "")) Then
                            blnSizeCheck = True
                            Exit For
                        End If
                    Next
                    If blnSizeCheck = False Then

                        'End If
                        'If (dtSizeCheck.Rows(0)("20'") = 0 And TxtSize.Text = "20") Or (dtSizeCheck.Rows(0)("40'") = 0 And TxtSize.Text = "40") Or (dtSizeCheck.Rows(0)("45'") = 0 And TxtSize.Text = "45") Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Size of the container is not there against this Booking no! ');", True)
                        txtContainer.Text = ""
                        lblEntryID.Text = ""
                        txtType.Text = ""
                        TxtSize.Text = ""
                        lblisocode.Text = ""
                        txtContainer.Focus()
                        Exit Sub
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Size of the container is not there against this Booking no! ');", True)
                    Exit Sub
                End If
            End If

            strSql = ""
            strSql = "select OutDate,containerNo,EntryId FROM EYardEmptyOut Where ContainerNo='" & Trim(txtContainer.Text) & "' AND EntryId = " & Val(lblEntryID.Text) & " and iscancel=0"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                If Trim(dt.Rows(0)("OutDate") & "") <> "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified container is all ready out');", True)
                    txtContainer.Text = ""
                    lblEntryID.Text = ""
                    txtType.Text = ""
                    TxtSize.Text = ""
                    lblisocode.Text = ""
                    'MsgBox("specified container is all ready out", vbCritical)
                    txtContainer.Focus()
                    Exit Sub
                End If
            End If



            strSql = ""
            strSql = "select Accountname  from eyard_accountmaster where  accountid=6"
            dt5 = db.sub_GetDatatable(strSql)
            If dt5.Rows.Count > 0 Then
                straccountname = Trim(dt5.Rows(0)("Accountname") & "")
            End If
            dbltotalamount = 0
            txtsgst.Text = 0
            txtcgst.Text = 0
            txtigst.Text = 0
            txtgrandtotal.Text = 0

            Call tarriffset()
            Call Sub_SGTRate()
            strSql = ""
            strSql = "USP_INSERT_TEMP_EMPTY_OUT'" & Trim(txtContainer.Text & "") & "','" & Trim(TxtSize.Text & "") & "','" & Trim(txtType.Text & "") & "','" & Trim(lblisocode.Text & "") & "','" & Trim(lbllineid.Text & "") & "','" & Trim(txtBookingNo.Text & "") & "',"
            strSql += "'" & Trim(ddllocation.SelectedItem.Text & "") & "','" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "','" & Trim(ddlVesselName.SelectedValue & "") & "','" & Trim(txtPODNo.Text & "") & "','" & Trim(txtSealNo.Text & "") & "',"
            strSql += "'" & Trim(lblEntryID.Text & "") & "','" & Trim(ddlOutStatus.SelectedItem.Text & "") & "','" & "6" & "','" & dbltotalamount & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtgstpartyname.Text) & "'"
            dt6 = db.sub_GetDatatable(strSql)
            grid()
            Dim lINE As String = lbllineid.Text
            txtContainer.Text = ""
            TxtSize.Text = ""
            txtType.Text = ""
            lblEntryID.Text = ""
            lblisocode.Text = ""
            'txtLineName.Text = ""
            'txtBookingNo.Text = ""
            'txtLocation.Text = ""
            If Trim(lbllineid.Text <> "572") Then
                txtRemarks.Text = ""
            End If

            'ddlVesselName.SelectedValue = 0
            'txtPODNo.Text = ""
            txtSealNo.Text = ""
            ddlOutStatus.SelectedValue = 0

            'strSql = ""
            'strSql = "exec  sp_check_gatepassbookingNoTemp '" & Trim(txtBookingNo.Text & "") & "',''"
            'dtSizeCheck = db.sub_GetDatatable(strSql)

            '    grdDets.Columns.Clear()
            '    grdDets.DataSource = dtSizeCheck
            '    grdDets.DataBind()


            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtContainer_TextChanged(sender As Object, e As EventArgs) Handles txtContainer.TextChanged
        Try

            If Trim(txtBookingNo.Text & "") = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter booking no. first! ');", True)

                Exit Sub
            End If
            Dim blnMode As Boolean = False
            For Each row In grdDets.Rows
                blnMode = True
            Next
            If blnMode = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Booking details not added');", True)
                Exit Sub
            End If
            'dt.Clear()
            strSql = ""
            strSql = "exec sp_getContainerApprStatus '" & Trim(txtContainer.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            'If Not dt.Rows.Count > 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container is not Approved yet. Does not allow for Gate Out');", True)
            '    txtContainer.Text = ""
            '    Exit Sub
            'End If

            If Trim(txtContainer.Text) <> "" Then
                If Len(Trim(txtContainer.Text & "")) = 11 Then
                    For i As Integer = 0 To grdHoldDetails.Rows.Count - 1
                        'If Trim(GrdHoldDetails.Rows(i).Cells(1).Value & "") = Trim(txtContainer.Text & "") Then
                        '    MsgBox("Container no. is already entered. ", vbInformation)
                        '    txtContainer.Focus()
                        '    Exit Sub
                        'End If
                    Next

                    Dim dt4 As New DataTable
                    strSql = ""
                    strSql = "exec get_sp_container_fetch_for_Out '" & Trim(txtContainer.Text & "") & "'"

                    dt4 = db.sub_GetDatatable(strSql)
                    If dt4.Rows.Count > 0 Then

                        If Trim(dt4.Rows(0)("LineID") & "") <> Val(lblBookingLineID.Text & "") Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container is not found agaist this booking no. ');", True)
                            'MsgBox("Container is not found agaist this booking no. ", vbInformation)

                            'txtContainer.Text = Trim(dt4.Rows(0)("ContainerNo") & "")
                            txtContainer.Text = ""
                            lblEntryID.Text = ""
                            txtType.Text = ""
                            TxtSize.Text = ""
                            lblisocode.Text = ""
                            txtContainer.Focus()
                            Exit Sub

                        End If
                        'Call db.Sub_ContianerHOLDStatus(Trim(txtContainer.Text & ""), Trim(dt.Rows(0)("ENTRYID") & ""))
                        If strcontainerstatus = True Then
                            'MsgBox(StrProcessin, vbInformation)
                            ' txtContainerNo.Focus()

                            'txtContainer.Text = Trim(dt4.Rows(0)("ContainerNo") & "")
                            txtContainer.Text = ""
                            lblEntryID.Text = ""
                            txtType.Text = ""
                            TxtSize.Text = ""
                            lblisocode.Text = ""
                            txtContainer.Focus()
                            Exit Sub
                        End If
                        lblEntryID.Text = Trim(dt4.Rows(0)("ENTRYID") & "")
                        TxtSize.Text = Trim(dt4.Rows(0)("Size") & "")
                        lblisocode.Text = Trim(dt4.Rows(0)("Isocode") & "")
                        txtType.Text = Trim(dt4.Rows(0)("Type") & "")
                        lbltype.Text = Trim(dt4.Rows(0)("ContainerTypeID") & "")
                        txtForwarderLine.Text = Trim(dt4.Rows(0)("LineI") & "")
                        ' txtcustomer.Text = Trim(dt.Rows(0)("consignee") & "")
                        lbllineid.Text = Trim(dt4.Rows(0)("LineID") & "")
                        lblcusto.Text = Trim(dt4.Rows(0)("AgentId") & "")
                        If Trim(txtRemarks.Text) = "" Then
                            txtRemarks.Text = Trim(dt4.Rows(0)("damageRemarks") & "")
                        End If

                        Dim intdays As Integer = 0
                        'dt.Clear()
                        strSql = ""
                        strSql = "Select DATEDIFF(day,INDate,GetDate()) [Day] from EYard_In Where EntryID=" & Val(lblEntryID.Text) & " and ContainerNo='" & Trim(txtContainer.Text) & "'"
                        dt = db.sub_GetDatatable(strSql)
                        If dt.Rows.Count > 0 Then
                            intdays = dt.Rows(0)("Day")
                        End If
                        'dt.Clear()
                        strSql = ""
                        strSql = "exec USP_Ctr_List_Above_Days '" & Trim(lbllineid.Text & "") & "'," & Val(intdays) & ""
                        dt = db.sub_GetDatatable(strSql)
                        If dt.Rows.Count > 0 Then
                            'pnlConList.Visible = True
                            'dgContainerlist.Visible = True
                            'dgContainerlist.DataSource = Nothing
                            'dgContainerlist.DataSource = dt
                            'dgContainerlist.Columns("Container No").Width = 100
                            'dgContainerlist.Columns("InDate").Width = 120
                            'dgContainerlist.Columns("Dwell Days").Width = 100
                        End If
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container Not gate in,Please recheck');", True)
                        txtContainer.Text = ""
                        lblEntryID.Text = ""
                        txtType.Text = ""
                        TxtSize.Text = ""
                        lblisocode.Text = ""
                        'MsgBox("" & txtContainer.Text & " Container Not gate in,Please recheck", vbCritical)
                        txtContainer.Focus()

                        Exit Sub
                    End If
                End If
            End If
            ddlVesselName.Focus()
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim blnMode As Boolean = False
            For Each row In grdHoldDetails.Rows
                blnMode = True
            Next
            If blnMode = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container details not added');", True)
                Exit Sub
            End If
            Dim Workyear As String = ""
            If Now.Month < 4 Then
                Workyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
            ElseIf Now.Month >= 4 Then
                Workyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
            End If

            Dim strWorkYear As String = Workyear
            Dim intEntryID As Integer = 0, intTrailerID As Integer = 0, strTrailerNo As String = "", intgateinid As Integer = 0

            strSql = "SELECT isnull(MAX(GpNo),0)+1 as EntryID FROM EYardEmptyOut WITH (XLOCK)"
            dt8 = db.sub_GetDatatable(strSql)
            If dt8.Rows.Count > 0 Then
                intEntryID = dt8.Rows(0)("EntryID")
            Else
                intEntryID = 1
            End If
            intTrailerID = 0
            For Each row As GridViewRow In grdHoldDetails.Rows
                strSql = ""
                strSql += "sp_SaveEYardEmptyOut'" & intEntryID & "','" & Convert.ToDateTime(Trim(txtGatePassDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlMovementBy.SelectedItem.Text & "") & "','" & Trim(ddlMovementtype.SelectedItem.Text & "") & "',"
                strSql += "'" & Val(intTrailerID) & "','" & Trim(txtTruckNo.Text & "") & "','" & Trim(txtTransporter.Text & "") & "','" & Val(CType(row.FindControl("lblEntry"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblSize"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblISOCode"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblType"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblLineName"), Label).Text & "") & "','" & Trim(txtShipperName.Text & "") & "','" & Replace(Trim(CType(row.FindControl("lblRemarks"), Label).Text & ""), "'", "''") & "','" & Trim(CType(row.FindControl("lblBooking"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblSeal"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblVesselID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblPOD"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lbllocation"), Label).Text & "") & "',"
                strSql += "'" & Session("UserId_DepoCFS") & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(lblcusto.Text & "") & "','" & Trim(lbllineid.Text & "") & "','','" & Trim(CType(row.FindControl("lblStatus"), Label).Text & "") & "','" & Trim(txtports.Text & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
                txtEyardOutPrint.Text = Trim(dt8.Rows(0)("EntryID") & "")
            Next
            'If chkisActive.Checked = True Then
            '    Dim StrlineID As String = Trim(lbllineid.Text & "")
            '    Dim intmaxAssessno As Integer = 0, intmaxReceiptno As Integer = 0, strInvoiceNo As String = ""
            '    Dim dtassess As New DataTable

            '    Dim strInWord As String = ""
            '    strInWord = dbamt.RupeesConvert(Val(txtgrandtotal.Text))
            '    strSql = ""
            '    strSql = "exec SP_Save_eyard_assessM  " & intmaxAssessno & ",'" & strWorkYear & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlEntrytype.SelectedItem.Text & "") & "','" & Val(lbllineid.Text) & "','" & Trim(txtShipperName.Text & "") & "','" & Session("UserId_DepoCFS") & "','" & Format(Now, "yyyy-MM-dd HH:mm") & "','" & Val(txttotalamount.Text & "") & "','" & Val(txtgrandtotal.Text & "") & "','" & strInWord & "','" & Trim(intgateinid & "") & "', " & Val(txtsgst.Text) & ", " & Val(txtcgst.Text) & ", " & Val(txtigst.Text) & ", " & Val(lblgstid.Text) & ","
            '    strSql += "1,'" & Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyy-MM-dd HH:mm") & "'"
            '    dt = db.sub_GetDatatable(strSql)
            '    If dt.Rows.Count > 0 Then
            '        intmaxAssessno = Val(dt.Rows(0)("ASSESS_NO"))
            '        strInvoiceNo = Trim(dt.Rows(0)("INVOICE_NO"))
            '    End If
            '    For Each row As GridViewRow In grdHoldDetails.Rows
            '        strSql = ""
            '        strSql += "EXEC  SP_Save_eyard_assessD '" & intmaxAssessno & "','" & strWorkYear & "','E',"
            '        strSql += "'" & intEntryID & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblSize"), Label).Text & "") & "','" & Val(lblgstid.Text) & "',"
            '        strSql += "'" & Val(CType(row.FindControl("lblAccountId"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblNetAmounts"), Label).Text & "") & "',"
            '        strSql += "'" & Val(CType(row.FindControl("lblTareWeight"), Label).Text & "") & "','0','','','" & Val(txtsgst.Text) & "', '" & Val(txtcgst.Text) & "', '" & Val(txtigst.Text) & "','" & strInvoiceNo & "'," & Val(lblTaxID.Text) & ""
            '        db.sub_ExecuteNonQuery(strSql)
            '    Next
            '    strSql = ""
            '    strSql = "select isnull(max(ReceiptNo),0) ReceiptNo  from Eyard_Receipt where workyear='" & strWorkYear & "'"
            '    dtassess = db.sub_GetDatatable(strSql)
            '    If dtassess.Rows.Count > 0 Then
            '        intmaxReceiptno = Val(dtassess.Rows(0)("ReceiptNo") & "") + 1
            '    End If
            '    strSql = ""
            '    strSql = "EXEC  SP_Save_eyard_Receipt  '" & intmaxReceiptno & "','" & strWorkYear & "','M','" & Format(Now, "yyyy-MM-dd HH:mm") & "','" & intmaxAssessno & "','" & strWorkYear & "'"
            '    strSql += " ,'" & Trim(txtgrandtotal.Text & "") & "','" & Trim(txtgrandtotal.Text & "") & "','','','','','" & strInWord & "',0,0,'','" & Session("UserId_DepoCFS") & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(intgateinid & "") & "'"
            '    db.sub_ExecuteNonQuery(strSql)
            '    For Each row As GridViewRow In grdMode.Rows
            '        Dim strDate As String = ""

            '        strSql = ""
            '        strSql += "EXEC  SP_Save_eyard_Receipt_mode  '" & intmaxReceiptno & "','" & strWorkYear & "','" & Trim(CType(row.FindControl("lblmode"), Label).Text & "") & "',"
            '        strSql += "'" & Val(CType(row.FindControl("lblmodeno"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblamount"), Label).Text & "") & "',"
            '        strSql += "'" & Trim(CType(row.FindControl("lblbankname"), Label).Text & "") & "',"
            '        If Trim(CType(row.FindControl("lblmodedate"), Label).Text & "") = "" Then
            '            strSql += "NULL,"
            '        Else
            '            strDate = Convert.ToDateTime(Trim(CType(row.FindControl("lblmodedate"), Label).Text & "")).ToString("yyyy-MM-dd")
            '            strSql += "'" & Convert.ToDateTime(Trim(CType(row.FindControl("lblmodedate"), Label).Text & "")).ToString("yyyy-MM-dd") & "',"
            '        End If
            '        strSql += "'','',''"
            '        db.sub_ExecuteNonQuery(strSql)
            '    Next
            'End If
            Call Clear()
            lblSession.Text = "Record Saved successfully "

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtBookingNo_TextChanged(sender As Object, e As EventArgs) Handles txtBookingNo.TextChanged
        Try

            If Trim(txtBookingNo.Text) <> "" Then
                Dim dt As New DataTable
                strSql = ""
                strSql = "USP_EYARD_OUT_BOOKINGNO_CHANGE '" & Trim(txtBookingNo.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    txtShipperName.Text = Trim(dt.Rows(0)("shipperName") & "")
                    txtLineName.Text = Trim(dt.Rows(0)("LineName") & "")
                    ddllocation.SelectedItem.Text = Trim(dt.Rows(0)("Destination") & "")
                    txtTransporter.Text = Trim(dt.Rows(0)("TransName") & "")
                    txtPODNo.Text = Trim(dt.Rows(0)("POD") & "")
                    ddlVesselName.SelectedValue = Trim(dt.Rows(0)("VesselID") & "")
                    txtports.Text = Trim(dt.Rows(0)("portname") & "")
                    txtSealNo.Text = Trim(dt.Rows(0)("SealNo") & "")
                    ddlMovementtype.SelectedValue = Trim(dt.Rows(0)("Movement_Type") & "")
                    lblBookingLineID.Text = Val(dt.Rows(0)("lineID") & "")
                    txtRemarks.Text = Trim(dt.Rows(0)("Booking_Remarks") & "")
                    ddlMovementBy.Focus()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Booking No Not Found! ');", True)
                    txtBookingNo.Text = ""
                    txtBookingNo.Focus()
                    Exit Sub
                End If
                Dim dtSizeCheck As DataTable
                strSql = ""
                strSql = "exec  sp_check_gatepassbookingNo '" & Trim(txtBookingNo.Text & "") & "',''"
                dtSizeCheck = db.sub_GetDatatable(strSql)
                If dtSizeCheck.Rows.Count > 0 Then
                    grdDets.Columns.Clear()
                    grdDets.DataSource = dtSizeCheck
                    grdDets.DataBind()
                End If
            End If
            UpdatePanel1.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub SaveOk_ServerClick(sender As Object, e As EventArgs)
        Try
            lblPrintQue.Text = "Do you wish to print ?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub tarriffset()
        Try
            Dim dt As New DataTable, dtstatus As New DataTable
            Dim strgrptype As String = ""
            Dim subqry As String = ""
            Dim strtrailertype As String = ""
            Dim strsize As Integer = 0
            Dim strctype As String = ""
            Dim intslid As Integer = 0
            Dim strdeltype As String = ""
            Dim straccountid As Integer = 0
            Dim dt6 As New DataTable

            strsize = Trim(TxtSize.Text & "")
            strctype = Trim(lbltype.Text & "")
            intslid = Trim(lbllineid.Text & "")
            straccountid = 6
            If Trim(ddlEntrytype.SelectedItem.Text) = "Party Return" Then
                strdeltype = "Party Return"
            Else
                strdeltype = "Offloading"
            End If

            strSql = ""
            strSql = "exec  SP_GET_EmptyContainerIN_tariffset '" & Trim(straccountid) & "','" & Trim(strctype & "") & "','" & Trim(strsize & "") & "','" & Trim(intslid & "") & "','" & Trim(strdeltype) & "'"
            dt6 = db.sub_GetDatatable(strSql)
            If dt6.Rows.Count > 0 Then
                dbltotalamount = Val(dt6.Rows(0)("Amount") & "")
            Else
                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Tariff Not Found! ');", True)
                'Exit Sub
            End If

        Catch ex As Exception
            'lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Sub_SGTRate()
        Try
            Dim compid As String = ""
            strSql = ""
            strSql += "select Tinnumber from settings"
            dt9 = db.sub_GetDatatable(strSql)
            If dt9.Rows.Count > 0 Then
                compid = Trim(dt9.Rows(0)(0))
            End If
            strSql = ""
            strSql += "USP_GST_Cal"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                dblSGST = Val(dt1.Rows(0)("SGST"))
                dblCGST = Val(dt1.Rows(0)("CGST"))
                dblIGST = Val(dt1.Rows(0)("IGST"))
                dbltaxgroupid = Trim(dt1.Rows(0)("settingsID") & "")
                strSGSTPer = "SGST " & dblSGST & "%"
                StrCGSTPEr = "CGST " & dblCGST & "%"
                StrIGSTPer = "IGST " & dblIGST & "%"
                If lblstatecode.Text = compid Then
                    dblIGST = 0
                    StrIGSTPer = "IGST " & dblIGST & "%"
                Else
                    dblSGST = 0
                    dblCGST = 0
                    strSGSTPer = "SGST " & dblSGST & "%"
                    StrCGSTPEr = "CGST " & dblCGST & "%"
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnmode_Click(sender As Object, e As EventArgs)
        Try
            If Trim(ddlmode.SelectedItem.Text & "") = "" Or Trim(ddlmode.SelectedItem.Text & "") = "--Select--" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Mode can't be left blank.');", True)
                ddlmode.Focus()
                Exit Sub

            End If
            If Trim(txtmodeno.Text & "") = "" And Trim(ddlmode.SelectedItem.Text & "") <> "Cash (+)" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Mode No can't be left blank.');", True)
                txtmodeno.Focus()
                Exit Sub
            End If
            If Trim(ddlbankname.SelectedValue & "") = "0" And Trim(ddlmode.SelectedItem.Text & "") <> "Cash (+)" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Bank Name can't be left blank.');", True)
                ddlbankname.Focus()
                Exit Sub
            End If
            If Val(txtamount.Text) = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Amount can't be left blank.');", True)

                txtamount.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql += "USP_INSERT_TEMP_MODE_CONTAINER'" & Trim(ddlmode.SelectedValue & "") & "','" & Trim(txtmodeno.Text & "") & "','" & Trim(txtamount.Text & "") & "','" & Trim(ddlbankname.SelectedValue & "") & "',"
            strSql += "'" & Trim(txtmodate.Text & "") & "','" & Session("UserId_DepoCFS") & "'"
            dt3 = db.sub_GetDatatable(strSql)
            grid2()
            ddlmode.SelectedValue = 0
            txtmodeno.Text = ""
            txtamount.Text = ""
            ddlbankname.SelectedValue = 0
            ddlmode.Focus()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select TECS.*,PGM.state_Code from Temp_Empty_Container_search TECS INNER JOIN PARTY_GST_M PGM ON TECS.GSTID=PGM.GSTID where userid='" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtgstpartyname.Text = Trim(dt.Rows(0)("PARTY_NAME") & "")
                lblgstid.Text = Trim(dt.Rows(0)("GSTID") & "")
                lblstatecode.Text = Trim(dt.Rows(0)("state_Code") & "")
            End If
            txtSealNo.Focus()
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            db.sub_ExecuteNonQuery("delete from Temp_Empty_Out where USERID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("delete from TEMP_MODE_CONTAINER where USERID=" & Session("UserId_DepoCFS") & "")
            txtGatePassDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            'txtmodate.Text = Convert.ToDateTime(Now).ToString("dd-MM-yyyy")
            Filldropdown()
            grid()
            grid2()
            txtBookingNo.Text = ""
            ddlMovementtype.SelectedValue = ""
            ddlMovementBy.SelectedValue = 0
            txtTruckNo.Text = ""
            txtTransporter.Text = ""
            lblisocode.Text = ""
            ddlEntrytype.SelectedValue = 0
            txtContainer.Text = ""
            lblEntryID.Text = ""
            txtType.Text = ""
            TxtSize.Text = ""
            txtLineName.Text = ""
            txtShipperName.Text = ""
            txtgstpartyname.Text = ""
            lblgstid.Text = ""
            txtSealNo.Text = ""
            ddllocation.SelectedItem.Text = ""
            txtPODNo.Text = ""
            ddlOutStatus.SelectedValue = 0
            txtRemarks.Text = ""
            txttotalamount.Text = ""
            chkisActive.Checked = False
            txtsgst.Text = ""
            txtcgst.Text = ""
            txtigst.Text = ""
            txtgrandtotal.Text = ""

            grdDets.Columns.Clear()
            grdDets.Visible = False
            UpdatePanel1.Update()
            txtBookingNo.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtTruckNo_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "exec  Usp_gettrailern '" & Trim(txtTruckNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtTransporter.Text = Trim(dt.Rows(0)("TransName") & "")

            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub


    Protected Sub txtSealNo_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "exec  usp_seal_vildtions_Out '" & Trim(txtSealNo.Text & "") & "','" & Trim(lbllineid.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If Trim(dt.Rows(0)("msg") & "") <> "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' " + dt.Rows(0)("msg") + ".');", True)
                txtSealNo.Text = ""
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

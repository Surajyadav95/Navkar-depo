Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11, dt12, dt13, dt14 As DataTable
    Dim db As New dbOperation_Depo
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim dblNetAmount As Double
    Dim strContNumbers As String
    Dim dblNetAmount_IND As Double
    Dim dblSTaxOnAmount As Double
    Dim strAccountID As String = ""
    Dim intGrossWeight As Double
    Dim dbamt As New dbAmountInWords
    Dim blnSTax As Boolean
    Dim dblGroup1Amt As Double
    Dim dblGroup2Amt As Double
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode
    Dim strcontainerstatus As Boolean
    Dim dbltotalamount As Double = 0
    Dim StrProcessin As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txttodate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            db.sub_ExecuteNonQuery("delete from Temp_Empty_Out where USERID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("delete from TEMP_MODE_CONTAINER where USERID=" & Session("UserId_DepoCFS") & "")
            txtGatePassDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtmodate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Filldropdown()
            grid()
            grid1()
            grid2()

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
            strSql = "  select trailerid [EntryID],trailername [Vehicle No] from trailers where trailerName<>''  " '--order by Name
            strSql += " UNION "
            strSql += " select EntryID,VehicleNo [Vehicle No] from Eyard_Gate_Out_Vehicle_D where VehicleNo<>'' order by EntryID"
            ds = db.sub_GetDataSets(strSql)
            ddlTruckNo.DataSource = ds.Tables(0)
            ddlTruckNo.DataTextField = "Vehicle No"
            ddlTruckNo.DataValueField = "EntryID"
            ddlTruckNo.DataBind()
            ddlTruckNo.Items.Insert(0, New ListItem("--Select--", 0))


            strSql = ""
            strSql = " select 0 as ID, '' as [Name] from trailers union SELECT TransID ID ,TransName AS Name  FROM Transporter WHERE TransName<>'' ORDER BY Name "
            ds = db.sub_GetDataSets(strSql)
            ddlTransporter.DataSource = ds.Tables(0)
            ddlTransporter.DataTextField = "Name"
            ddlTransporter.DataValueField = "ID"
            ddlTransporter.DataBind()
            ddlTransporter.Items.Insert(0, New ListItem("--Select--", 0))

            strSql = ""
            strSql = "select VesselID=0 ,VesselName=' '  union all select VesselID ,VesselName  from vessels  order by VesselName "
            ds = db.sub_GetDataSets(strSql)
            ddlVesselName.DataSource = ds.Tables(0)
            ddlVesselName.DataTextField = "VesselName"
            ddlVesselName.DataValueField = "VesselID"
            ddlVesselName.DataBind()
            ddlVesselName.Items.Insert(0, New ListItem("--Select--", 0))

            strSql = ""
            strSql = " select paymodeID as ID ,paymode [Name] from payment_modes  "
            ds = db.sub_GetDataSets(strSql)
            ddlmode.DataSource = ds.Tables(0)
            ddlmode.DataTextField = "Name"
            ddlmode.DataValueField = "ID"
            ddlmode.DataBind()
            ddlmode.Items.Insert(0, New ListItem("--Select--", 0))

            strSql = ""
            strSql = " select bankID ,bankname   from import_banks     where bankname<>'' order by bankname  "
            ds = db.sub_GetDataSets(strSql)
            ddlbankname.DataSource = ds.Tables(0)
            ddlbankname.DataTextField = "bankname"
            ddlbankname.DataValueField = "bankID"
            ddlbankname.DataBind()
            ddlbankname.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grid()
        Try
            dt7 = db.sub_GetDatatable("USP_TEMP_GRID_EMPTY_OUT '" & Session("UserId_DepoCFS") & "'")
            GrdHoldDetails.DataSource = dt7
            GrdHoldDetails.DataBind()
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


        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub grid1()
        Try
            strSql = ""
            strSql += " sp_GetEmptyoutDets '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttodate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
            strSql += "'" & Trim(txtsearch.Text & "") & "'"
            dt10 = db.sub_GetDatatable(strSql)
            grdconrainer.DataSource = dt10
            grdconrainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtBookingNo_TextChanged(sender As Object, e As EventArgs) Handles txtBookingNo.TextChanged
        If Trim(txtBookingNo.Text) <> "" Then
            Dim dt As New DataTable
            strSql = ""
            strSql = "select Top (1) LineName ,shipperName  ,Transporter ,Destination,POD,GateAllowID  from Eyard_Gate_Out_Allow_M G  where BookingNo ='" & Trim(txtBookingNo.Text & "") & "' and IsCancel =0 "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtShipperName.Text = Trim(dt.Rows(0)("shipperName") & "")
                txtLineName.Text = Trim(dt.Rows(0)("LineName") & "")
                txtLocation.Text = Trim(dt.Rows(0)("Destination") & "")
                ddlTransporter.SelectedItem.Text = Trim(dt.Rows(0)("Transporter") & "")
                txtPODNo.Text = Trim(dt.Rows(0)("POD") & "")
                ddlMovementtype.Focus()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Booking No Not Found! ');", True)
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
        UpdatePanel5.Update()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim straccountname As String = ""
        If Trim(txtType.Text & "") <> "FR" Then
            If grdHoldDetails.Rows.Count >= 2 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container is more then 2 is Not allowed! ');", True)
                Exit Sub
            End If
        End If
        If Trim(txtBookingNo.Text & "") <> "" Then
            Dim dtSizeCheck As DataTable
            strSql = ""
            strSql = "exec  sp_check_gatepassbookingNo '" & Trim(txtBookingNo.Text & "") & "','" & Trim(txtType.Text & "") & "'"
            dtSizeCheck = db.sub_GetDatatable(strSql)
            If dtSizeCheck.Rows.Count > 0 Then
                If (dtSizeCheck.Rows(0)("20'") = 0 And TxtSize.Text = "20") Or (dtSizeCheck.Rows(0)("40'") = 0 And TxtSize.Text = "40") Or (dtSizeCheck.Rows(0)("45'") = 0 And TxtSize.Text = "45") Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Size of the container  is not there against this Booking no! ');", True)
                    Exit Sub

                End If

            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Size of the container  is not there against this Booking no! ');", True)
                Exit Sub
            End If
        End If


        strSql = ""
        strSql = "select OutDate,containerNo,EntryId FROM EYardEmptyOut Where ContainerNo='" & Trim(txtContainer.Text) & "' AND EntryId = " & Val(lblEntryID.Text) & " and iscancel=0"
        dt = db.sub_GetDatatable(strSql)
        If dt.Rows.Count > 0 Then
            If Trim(dt.Rows(0)("OutDate") & "") <> "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('specified container is all ready out');", True)
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
        strSql = "USP_INSERT_TEMP_EMPTY_OUT'" & Trim(txtContainer.Text & "") & "','" & Trim(TxtSize.Text & "") & "','" & Trim(txtType.Text) & "','" & Trim(lblisocode.Text & "") & "','" & Trim(lbllineid.Text & "") & "','" & Trim(txtBookingNo.Text & "") & "',"
        strSql += "'" & Trim(txtLocation.Text & "") & "','" & Trim(txtRemarks.Text & "") & "','" & Trim(ddlVesselName.SelectedValue & "") & "','" & Trim(txtPODNo.Text & "") & "','" & Trim(txtSealNo.Text & "") & "',"
        strSql += "'" & Trim(lblEntryID.Text & "") & "','" & Trim(ddlOutStatus.SelectedItem.Text & "") & "','" & "6" & "','" & dbltotalamount & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtgstpartyname.Text) & "'"
        dt6 = db.sub_GetDatatable(strSql)
        grid()
        txtContainer.Text = ""
        TxtSize.Text = ""
        txtLineName.Text = ""
        txtBookingNo.Text = ""
        txtLocation.Text = ""
        txtRemarks.Text = ""
        ddlVesselName.SelectedValue = 0
        txtPODNo.Text = ""
        txtSealNo.Text = ""
        ddlOutStatus.SelectedItem.Text = ""
    End Sub

    Protected Sub txtContainer_TextChanged(sender As Object, e As EventArgs) Handles txtContainer.TextChanged
        If Trim(txtBookingNo.Text & "") = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter booking no. first! ');", True)

            Exit Sub
        End If


        strSql = ""
        strSql = "exec sp_getContainerApprStatus '" & Trim(txtContainer.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        If dt.Rows.Count > 0 Then
            If Trim(dt.Rows(0)("ApprovedDate") & "") Is DBNull.Value.ToString Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container is not Approved yet. Does not allow for Gate Out');", True)

                txtContainer.Text = ""
                Exit Sub
            End If
        End If

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

                    If Trim(dt4.Rows(0)("LineI") & "") <> Trim(txtShipperName.Text & "") Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container is not found agaist this booking no. ');", True)
                        txtContainer.Text = ""
                        txtContainer.Text = Trim(dt4.Rows(0)("ContainerNo") & "")
                        txtContainer.Focus()
                        Exit Sub

                    End If
                    If strcontainerstatus = True Then
                        txtContainer.Text = ""
                        txtContainer.Text = Trim(dt4.Rows(0)("ContainerNo") & "")
                        txtContainer.Focus()
                        Exit Sub
                    End If
                    lblEntryID.Text = Trim(dt4.Rows(0)("ENTRYID") & "")
                    TxtSize.Text = Trim(dt4.Rows(0)("Size") & "")
                    lblisocode.Text = Trim(dt4.Rows(0)("Isocode") & "")
                    txtType.Text = Trim(dt4.Rows(0)("Type") & "")
                    lbltype.Text = Trim(dt4.Rows(0)("ContainerTypeID") & "")
                    txtForwarderLine.Text = Trim(dt4.Rows(0)("LineI") & "")
                    lbllineid.Text = Trim(dt4.Rows(0)("LineID") & "")
                    lblcusto.Text = Trim(dt4.Rows(0)("AgentId") & "")

                    Dim intdays As Integer = 0

                    strSql = ""
                    strSql = "Select DATEDIFF(day,INDate,GetDate()) [Day] from EYard_In Where EntryID=" & Val(lblEntryID.Text) & " and ContainerNo='" & Trim(txtContainer.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If dt.Rows.Count > 0 Then
                        intdays = dt.Rows(0)("Day")
                    End If

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
                    txtContainer.Focus()
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            Dim Workyear As String = ""
            If Now.Month < 4 Then
                Workyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
            ElseIf Now.Month >= 4 Then
                Workyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
            End If

            Dim strWorkYear As String = Workyear
            Dim intEntryID As Integer = 0, intTrailerID As Integer = 0, strTrailerNo As String = "", intgateinid As Integer = 0

            strSql = "SELECT isnull(MAX(GpNo),0)+1 as EntryID FROM EYardEmptyOut"
            dt8 = db.sub_GetDatatable(strSql)
            If dt8.Rows.Count > 0 Then
                intEntryID = dt8.Rows(0)("EntryID")
            Else
                intEntryID = 1
            End If
            intTrailerID = 0
            For Each row As GridViewRow In grdHoldDetails.Rows
                strSql = ""
                strSql += "sp_SaveEYardEmptyOut'" & intEntryID & "','" & Convert.ToDateTime(Trim(txtGatePassDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlMovementBy.SelectedItem.Text & "") & "','" & Trim(ddlMovementtype.SelectedItem.Text & "") & "',"
                strSql += "'" & Val(intTrailerID) & Trim(ddlTruckNo.SelectedItem.Text & "") & "','" & Trim(ddlTransporter.SelectedItem.Text & "") & "','" & Val(CType(row.FindControl("lblEntry"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblSize"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblISOCode"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblLineName"), Label).Text & "") & "','" & Trim(txtShipperName.Text & "") & "','" & Trim(CType(row.FindControl("lblRemarks"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblBooking"), Label).Text & "") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblSeal"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblVesselID"), Label).Text & "") & "',''," & Trim(CType(row.FindControl("lbllocation"), Label).Text & "") & "',"
                strSql += "'" & Session("UserId_DepoCFS") & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm") & "','" & Trim(lbllineid.Text & "") & "','" & Trim(lblcusto.Text & "") & "',''," & Trim(CType(row.FindControl("lblStatus"), Label).Text & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
                txtEyardOutPrint.Text = Trim(dt8.Rows(0)("EntryID") & "")
            Next
            If chkisActive.Checked = True Then
                Dim StrlineID As String = Trim(lbllineid.Text & "")
                Dim intmaxAssessno As Integer = 0, intmaxReceiptno As Integer = 0, strInvoiceNo As String = ""
                Dim dtassess As New DataTable

                Dim strInWord As String
                strInWord = dbamt.RupeesConvert(Val(txtgrandtotal.Text))
                strSql = ""
                strSql = "exec SP_Save_eyard_assessM  " & intmaxAssessno & ",'" & strWorkYear & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlEntrytype.SelectedItem.Text & "") & "','" & Val(lbllineid.Text) & "','" & Trim(txtShipperName.Text & "") & "','" & Session("UserId_DepoCFS") & "','" & Format(Now, "yyyy-MMM-dd HH:mm") & "','" & Val(txttotalamount.Text & "") & "','" & Val(txtgrandtotal.Text & "") & "','" & strInWord & "','" & Trim(intgateinid & "") & "', " & Val(txtsgst.Text) & ", " & Val(txtcgst.Text) & ", " & Val(txtigst.Text) & ", " & Val(lblgstid.Text) & ","
                strSql += "1,'" & Convert.ToDateTime(txtGatePassDate.Text).ToString("yyyy-MM-dd HH:mm") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    intmaxAssessno = Val(dt.Rows(0)("ASSESS_NO"))
                    strInvoiceNo = Trim(dt.Rows(0)("INVOICE_NO"))
                End If
                For Each row As GridViewRow In grdHoldDetails.Rows
                    strSql = ""
                    strSql += "EXEC  SP_Save_eyard_assessD '" & intmaxAssessno & "','" & strWorkYear & "','E',"
                    strSql += "'" & intEntryID & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblSize"), Label).Text & "") & "','" & Val(lblgstid.Text) & "',"
                    strSql += "'" & Val(CType(row.FindControl("lblAccountId"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblNetAmounts"), Label).Text & "") & "',"
                    strSql += "'" & Val(CType(row.FindControl("lblTareWeight"), Label).Text & "") & "','0','','','" & Val(txtsgst.Text) & "', '" & Val(txtcgst.Text) & "', '" & Val(txtigst.Text) & "','" & strInvoiceNo & "'," & Val(lblTaxID.Text) & ""
                    db.sub_ExecuteNonQuery(strSql)
                Next
                strSql = ""
                strSql = "select isnull(max(ReceiptNo),0) ReceiptNo  from Eyard_Receipt where workyear='" & strWorkYear & "'"
                dtassess = db.sub_GetDatatable(strSql)
                If dtassess.Rows.Count > 0 Then
                    intmaxReceiptno = Val(dtassess.Rows(0)("ReceiptNo") & "") + 1
                End If
                strSql = ""
                strSql = "EXEC  SP_Save_eyard_Receipt  '" & intmaxReceiptno & "','" & strWorkYear & "','M','" & Format(Now, "yyyy-MMM-dd HH:mm") & "','" & intmaxAssessno & "','" & strWorkYear & "'"
                strSql += " ,'" & Trim(txtgrandtotal.Text & "") & "','" & Trim(txtgrandtotal.Text & "") & "','','','','','" & strInWord & "',0,0,'','" & Session("UserId_DepoCFS") & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(intgateinid & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
                For Each row As GridViewRow In grdMode.Rows
                    strSql = ""
                    strSql += "EXEC  SP_Save_eyard_Receipt_mode  '" & intmaxReceiptno & "','" & strWorkYear & "','" & Trim(CType(row.FindControl("lblmode"), Label).Text & "") & "',"
                    strSql += "'" & Val(CType(row.FindControl("lblmodeno"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblamount"), Label).Text & "") & "',"
                    strSql += "'" & Trim(CType(row.FindControl("lblbankname"), Label).Text & "") & "','" & Convert.ToDateTime(CType(row.FindControl("lblmodedate"), Label).Text.ToString()).ToString("yyyy-MM-dd") & "','','',''"
                    db.sub_ExecuteNonQuery(strSql)
                Next
            End If
            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
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
        Dim dt As New DataTable, dtstatus As New DataTable
        Dim strgrptype As String = ""
        Dim subqry As String = ""
        Dim strtrailertype As String = ""
        Dim strsize As Integer = 0
        Dim strctype As Integer = 0
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

        strSQL = ""
        strSQL = "exec  SP_GET_EmptyContainerIN_tariffset '" & Trim(straccountid) & "','" & Trim(strctype & "") & "','" & Trim(strsize & "") & "','" & Trim(intslid & "") & "','" & Trim(strdeltype) & "'"
        dt6 = db.sub_GetDatatable(strSql)
        If dt6.Rows.Count > 0 Then
            dbltotalamount = Val(dt6.Rows(0)("Amount") & "")
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Tariff Not Found! ');", True)
            Exit Sub
        End If
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
            strSql += "'" & Convert.ToDateTime(Trim(txtmodate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_DepoCFS") & "'"
            dt3 = db.sub_GetDatatable(strSql)
            grid2()
            ddlmode.SelectedValue = 0
            txtmodeno.Text = ""
            txtamount.Text = ""
            ddlbankname.SelectedValue = 0

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
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

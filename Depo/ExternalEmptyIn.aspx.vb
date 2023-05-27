Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports System.Configuration
Imports System.Data.OleDb
Imports System.Globalization

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt9, dt10, dt11 As DataTable
    Dim db As New dbOperation_Depo
    Dim dbamt As New dbAmountInWords
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim ds As DataSet
    Dim TariffID, TariffIDView As String
    Dim strCategory As String
    Dim strCategoryDetails As String
    Dim strCategorySPName As String
    Dim ed As New clsEncodeDecode
    Dim dbltotalamount As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("delete from Temp_FACILITATION_CHARGES where USERID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("delete from TEMP_EMPTY_CONTAINER_IN where USERID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("delete from TEMP_MODE_CONTAINER where USERID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("delete from Temp_Train_Container_search where USERID=" & Session("UserId_DepoCFS") & "")
            txtindatetime.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtMFGDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM")
            txtDovaliddate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtmodate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")

            sub_CreateTable()
            grid()
            grid1()
            Filldropdown()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql = "USP_EMPTY_CONTAINER_FILLDROP_DOWN"

            ds = db.sub_GetDataSets(strSql)
            ddlTransporter.DataSource = ds.Tables(0)
            ddlTransporter.DataTextField = "TransName"
            ddlTransporter.DataValueField = "TransID"
            ddlTransporter.DataBind()
            ddlTransporter.Items.Insert(0, New ListItem("--Select--", 0))

            ddlType.DataSource = ds.Tables(1)
            ddlType.DataTextField = "ContainerType"
            ddlType.DataValueField = "ContainerTypeID"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("--Select--", 0))

            ddlISOCode.DataSource = ds.Tables(2)
            ddlISOCode.DataTextField = "ISOCode"
            ddlISOCode.DataValueField = "ISOID"
            ddlISOCode.DataBind()
            ddlISOCode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlstatusType.DataSource = ds.Tables(3)
            ddlstatusType.DataTextField = "Name"
            ddlstatusType.DataValueField = "ID"
            ddlstatusType.DataBind()
            ddlstatusType.Items.Insert(0, New ListItem("--Select--", 0))


            ddlshipline.DataSource = ds.Tables(4)
            ddlshipline.DataTextField = "SLName"
            ddlshipline.DataValueField = "SLID"
            ddlshipline.DataBind()
            ddlshipline.Items.Insert(0, New ListItem("--Select--", 0))


            ddlCondition.DataSource = ds.Tables(5)
            ddlCondition.DataTextField = "Name"
            ddlCondition.DataValueField = "ID"
            ddlCondition.DataBind()
            ddlCondition.Items.Insert(0, New ListItem("--Select--", 0))

            ddlmode.DataSource = ds.Tables(6)
            ddlmode.DataTextField = "Name"
            ddlmode.DataValueField = "ID"
            ddlmode.DataBind()
            ddlmode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlbankname.DataSource = ds.Tables(7)
            ddlbankname.DataTextField = "bankname"
            ddlbankname.DataValueField = "bankID"
            ddlbankname.DataBind()
            ddlbankname.Items.Insert(0, New ListItem("--Select--", 0))

            ddlAdditionalRemarks.DataSource = ds.Tables(8)
            ddlAdditionalRemarks.DataTextField = "Additional_Remarks"
            ddlAdditionalRemarks.DataValueField = "ID"
            ddlAdditionalRemarks.DataBind()
            ddlAdditionalRemarks.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable
        Dim dtDepoChargesCnt As New DataTable
        Dim dtDepoReceipt As New DataTable

        dtDepoContainer.Columns.Clear()
        dtDepoChargesCnt.Columns.Clear()
        dtDepoReceipt.Columns.Clear()

        Session("table_DepoContainer") = ""
        Session("table_DepoChargesContainer") = ""
        Session("table_DepoReceiptContainer") = ""
        dtDepoContainer.Columns.Add("Check")
        dtDepoContainer.Columns.Add("ContainerNo")
        dtDepoContainer.Columns.Add("Size")
        dtDepoContainer.Columns.Add("ContainerType")
        dtDepoContainer.Columns.Add("CONTAINERTYPEID")
        dtDepoContainer.Columns.Add("ISOCODE")
        dtDepoContainer.Columns.Add("ISOCODEID")
        dtDepoContainer.Columns.Add("TareWeight")
        dtDepoContainer.Columns.Add("StatusType")
        dtDepoContainer.Columns.Add("GROSSWEIGHT")
        dtDepoContainer.Columns.Add("CC_WT")
        dtDepoContainer.Columns.Add("SLName")
        dtDepoContainer.Columns.Add("LINEID")
        dtDepoContainer.Columns.Add("Consignee")
        dtDepoContainer.Columns.Add("Condition")
        dtDepoContainer.Columns.Add("MFGDate")
        dtDepoContainer.Columns.Add("BKNo")
        dtDepoContainer.Columns.Add("CSCASP")
        dtDepoContainer.Columns.Add("SURVEYEIR")
        dtDepoContainer.Columns.Add("Grade")
        dtDepoContainer.Columns.Add("Location")
        dtDepoContainer.Columns.Add("PARTYNAME")
        dtDepoContainer.Columns.Add("Remarks")
        dtDepoContainer.Columns.Add("damageRemarks")
        dtDepoContainer.Columns.Add("ValidDate")
        dtDepoContainer.Columns.Add("AccountName")
        dtDepoContainer.Columns.Add("AccountID")
        dtDepoContainer.Columns.Add("NetAmount")
        dtDepoContainer.Columns.Add("Additional_Remarks")
        dtDepoContainer.Columns.Add("ID")

        dtDepoChargesCnt.Columns.Add("ContainerNo")
        dtDepoChargesCnt.Columns.Add("Size")
        dtDepoChargesCnt.Columns.Add("TareWeight")
        dtDepoChargesCnt.Columns.Add("TypeID")
        dtDepoChargesCnt.Columns.Add("AccountID")
        dtDepoChargesCnt.Columns.Add("NetAmount")

        dtDepoReceipt.Columns.Add("paymode")
        dtDepoReceipt.Columns.Add("paymodeID")
        dtDepoReceipt.Columns.Add("Mode_No")
        dtDepoReceipt.Columns.Add("Amount")
        dtDepoReceipt.Columns.Add("bankname")
        dtDepoReceipt.Columns.Add("banknameID")
        dtDepoReceipt.Columns.Add("Mode_Date")

        Dim dtRow2 As DataRow = dtDepoContainer.NewRow
        Dim dtRow3 As DataRow = dtDepoChargesCnt.NewRow
        Dim dtRow4 As DataRow = dtDepoReceipt.NewRow

        grdcontainer.DataSource = Nothing
        grdcontainer.DataSource = dtDepoContainer
        grdcontainer.DataBind()
        grdAccounts.DataSource = Nothing
        grdAccounts.DataSource = dtDepoChargesCnt
        grdAccounts.DataBind()
        grdMode.DataSource = Nothing
        grdMode.DataSource = dtDepoReceipt
        grdMode.DataBind()
        Session("table_DepoContainer") = dtDepoContainer
        Session("table_DepoChargesContainer") = dtDepoChargesCnt
        Session("table_DepoReceiptContainer") = dtDepoReceipt

    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select TECS.*,PGM.state_Code,pgm.GSTIn_uniqID from Temp_Empty_Container_search TECS INNER JOIN PARTY_GST_M PGM ON TECS.GSTID=PGM.GSTID where userid='" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtgstpartyname.Text = Trim(dt.Rows(0)("PARTY_NAME") & "")
                lblgstid.Text = Trim(dt.Rows(0)("GSTID") & "")
                lblstatecode.Text = Trim(dt.Rows(0)("state_Code") & "")
                txtGSTNo.Text = Trim(dt.Rows(0)("GSTIn_uniqID") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grid()
        Try
            'dt4 = db.sub_GetDatatable("usp_grid_container_fill '" & Session("UserId_DepoCFS") & "'")
            'grdcontainer.DataSource = dt4
            'grdcontainer.DataBind()
            Dim dblamount As Double = 0, blnmode As Boolean = False
            For Each row In grdcontainer.Rows
                blnmode = True
                dblamount = dblamount + Val(CType(row.FindControl("lblNetAmounts"), Label).Text & "")
            Next
            If Not Val(lblgstid.Text) = 0 Then
                lnksearch.Visible = False
            Else
                lnksearch.Visible = True
            End If
            txttotalamount.Text = dblamount
            UpdatePanel4.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Calculation()
        Dim dblamount As Double = 0
        Call Sub_SGTRate()
        dblamount = Val(txttotalamount.Text)
        txtsgst.Text = Format(dblamount * (dblSGST / 100), "0.00")
        txtcgst.Text = Format(dblamount * (dblCGST / 100), "0.00")
        txtigst.Text = Format(dblamount * (dblIGST / 100), "0.00")
        txtgrandtotal.Text = Val(txttotalamount.Text) + Val(txtsgst.Text) + Val(txtcgst.Text) + Val(txtigst.Text)
        txtgrandtotal.Text = Math.Round(Val(txtgrandtotal.Text))
        lblTaxID.Text = dbltaxgroupid
        ddlmode.SelectedValue = 1
        txtamount.Text = Val(txtgrandtotal.Text)
        UpdatePanel4.Update()
    End Sub
    'Protected Sub ddlISOCode_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        strSql = "select [Weight],CtrSize [Size], (SELECT ContainerTypeID FROM ContainerType C where C.ContainerType=I.I_ContainerType  ) as [TypeID] from ISOCodes I where ISOCode=" & Trim(ddlISOCode.SelectedItem.Text & "") & ""
    '        dt = db.sub_GetDatatable(strSql)
    '        If dt.Rows.Count > 0 Then
    '            If Not dt.Rows(0)("Weight").ToString Is DBNull.Value.ToString Then
    '                txtTareWeight.Text = Trim(dt.Rows(0)("Weight") & "")
    '                ddlSize.SelectedValue = Trim(dt.Rows(0)("Size") & "")
    '                If Not dt.Rows(0)("TypeID").ToString Is DBNull.Value.ToString Then
    '                    ddlType.SelectedValue = Trim(dt.Rows(0)("TypeID") & "")
    '                End If

    '            End If
    '        Else
    '            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Selected ISO Code was Not found');", True)
    '            txtTareWeight.Text = ""
    '            'ddlSize.SelectedValue = 0
    '            'ddlType.SelectedValue = 0
    '            'ddlISOCode.SelectedValue = 0
    '            'Exit Sub
    '        End If
    '        UpdatePanel5.Update()
    '        ddlshipline.Focus()
    '    Catch ex As Exception
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    End Try
    'End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            Dim dt5 As New DataTable
            Dim straccountname As String = ""
            strSql = ""
            strSql += "SELECT ContainerNo FROM External_MTY_In WHERE ContainerNo='" & Trim(txtcontainerNo.Text) & "' AND STATUS='P' AND ISCANCEL=0"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtcontainerNo.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No Already Gate in .');", True)
                txtcontainerNo.Focus()
                Exit Sub
            End If
            'dt4 = db.sub_GetDatatable("usp_grid_container_fill '" & Session("UserId_DepoCFS") & "'")
            'If dt4.Rows.Count > 0 Then
            '    If dt4.Rows.Count >= 2 Then
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No more containers are valid');", True)
            '        txtcontainerNo.Focus()
            '        Exit Sub
            '    Else
            '        If Val(dt4.Rows(0)("SIZE")) = 40 Or Val(dt4.Rows(0)("SIZE")) = 45 Then
            '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Size Invalid');", True)
            '            txtcontainerNo.Focus()
            '            Exit Sub
            '        End If
            '    End If
            'End If

            If Convert.ToDateTime(Trim(txtDovaliddate.Text)).ToString("yyyyMMdd") < Convert.ToDateTime(Trim(txtindatetime.Text)).ToString("yyyyMMdd") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('DO Validity date must be greater than indate');", True)
                'txtDovaliddate.Focus()
                'Exit Sub
            End If
            Dim dtDepoContainer As New DataTable
            Dim intRows As Integer = 0

            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            If dtDepoContainer.Rows.Count > 1 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No more containers are valid');", True)
                txtcontainerNo.Focus()
                Exit Sub
            End If
            If dtDepoContainer.Rows.Count > 0 Then
                If Val(dtDepoContainer.Rows(0)("Size")) = 40 Or Val(dtDepoContainer.Rows(0)("Size")) = 45 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Size Invalid');", True)
                    txtcontainerNo.Focus()
                    Exit Sub
                End If
                If Val(dtDepoContainer.Rows(0)("Size")) = 20 And (Val(ddlSize.SelectedItem.Text) = 40 Or Val(ddlSize.SelectedItem.Text) = 45) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Size Invalid');", True)
                    txtcontainerNo.Focus()
                    Exit Sub
                End If
            End If
            'strSql = ""
            'strSql = "select Accountname  from eyard_accountmaster where accountid=3"
            'dt5 = db.sub_GetDatatable(strSql)
            'If dt5.Rows.Count > 0 Then
            '    straccountname = Trim(dt5.Rows(0)("Accountname") & "")
            'End If
            'dbltotalamount = 0
            'txtsgst.Text = 0
            'txtcgst.Text = 0
            'txtigst.Text = 0
            'txtgrandtotal.Text = 0

            'Call tarriffset()
            'Call Sub_SGTRate()

            'strSql = ""
            'strSql += " USP_INSERT_TEMP_EMPTY_CONTAINER_IN '" & Trim(txtcontainerNo.Text & "") & "','" & Trim(ddlSize.SelectedItem.Text & "") & "','" & Trim(ddlType.SelectedValue & "") & "',"
            'strSql += "'" & Trim(ddlISOCode.SelectedValue & "") & "','" & Trim(txtTareWeight.Text & "") & "','" & Trim(ddlstatusType.SelectedItem.Text & "") & "','" & Trim(txtgross.Text & "") & "',"
            'strSql += "'" & Trim(txtCCWt.Text & "") & "','" & Trim(ddlshipline.SelectedValue & "") & "','" & Trim(txtShippConsignee.Text & "") & "','" & Trim(ddlCondition.SelectedItem.Text & "") & "',"
            'strSql += "'" & Convert.ToDateTime(Trim(txtMFGDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtBkNo.Text & "") & "','" & Trim(txtsurveryEir.Text & "") & "',"
            'strSql += "'" & Trim(txtGrade.Text & "") & "','" & Trim(txtlocation.Text & "") & "','" & Trim(txtgstpartyname.Text & "") & "','" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "','" & Replace(Trim(txtdamageremarks.Text & ""), "'", "''") & "',"
            'strSql += "'" & Convert.ToDateTime(Trim(txtDovaliddate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & "6" & "','" & dbltotalamount & "','" & Session("UserId_DepoCFS") & "','" & Replace(Trim(txtCSCASP.Text & ""), "'", "''") & "'"
            'dt1 = db.sub_GetDatatable(strSql)


            intRows = dtDepoContainer.Rows.Count
            Dim dtRow As DataRow = dtDepoContainer.NewRow
            dtRow.Item("Check") = "1"
            dtRow.Item("ContainerNo") = Trim(txtcontainerNo.Text.ToUpper() & "")
            dtRow.Item("Size") = Trim(ddlSize.SelectedItem.Text & "")
            dtRow.Item("ContainerType") = Trim(ddlType.SelectedItem.Text & "")
            dtRow.Item("CONTAINERTYPEID") = Val(ddlType.SelectedValue & "")
            dtRow.Item("ISOCODE") = Trim(ddlISOCode.SelectedItem.Text & "")
            dtRow.Item("ISOCODEID") = Val(ddlISOCode.SelectedValue & "")
            dtRow.Item("TareWeight") = Trim(txtTareWeight.Text & "")
            dtRow.Item("StatusType") = Trim(ddlstatusType.SelectedItem.Text & "")
            dtRow.Item("GROSSWEIGHT") = Trim(txtgross.Text & "")
            dtRow.Item("CC_WT") = Trim(txtCCWt.Text & "")
            dtRow.Item("SLName") = Trim(ddlshipline.SelectedItem.Text & "")
            dtRow.Item("LINEID") = Val(ddlshipline.SelectedValue & "")
            dtRow.Item("Consignee") = Trim(txtShippConsignee.Text & "")
            dtRow.Item("Condition") = Trim(ddlCondition.SelectedItem.Text & "")
            dtRow.Item("MFGDate") = Convert.ToDateTime(Trim(txtMFGDate.Text & "")).ToString("yyyy-MM-dd HH:mm")
            dtRow.Item("BKNo") = Trim(txtBkNo.Text & "")
            dtRow.Item("CSCASP") = Replace(Trim(txtCSCASP.Text & ""), "'", "''")
            dtRow.Item("SURVEYEIR") = Trim(txtsurveryEir.Text & "")
            dtRow.Item("Grade") = Trim(txtGrade.Text.ToUpper() & "")
            dtRow.Item("Location") = Trim(txtlocation.Text.ToUpper() & "")
            dtRow.Item("PARTYNAME") = Trim(txtgstpartyname.Text & "")
            dtRow.Item("Remarks") = Replace(Trim(txtRemarks.Text & ""), "'", "''")
            dtRow.Item("damageRemarks") = Replace(Trim(txtdamageremarks.Text & ""), "'", "''")
            dtRow.Item("ValidDate") = Convert.ToDateTime(Trim(txtDovaliddate.Text & "")).ToString("yyyy-MM-dd HH:mm")
            dtRow.Item("AccountName") = straccountname
            dtRow.Item("AccountID") = 3
            dtRow.Item("NetAmount") = Val(dbltotalamount)
            dtRow.Item("Additional_Remarks") = Trim(ddlAdditionalRemarks.SelectedItem.Text & "")
            dtRow.Item("ID") = Trim(ddlAdditionalRemarks.SelectedValue & "")

            dtDepoContainer.Rows.Add(dtRow)
            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDepoContainer
            grdcontainer.DataBind()

            Session("table_DepoContainer") = dtDepoContainer

            grid()
            txtcontainerNo.Text = ""
            ddlSize.SelectedValue = "0"
            ddlType.SelectedValue = "0"
            ddlISOCode.SelectedValue = "0"
            ddlstatusType.SelectedValue = "0"
            txtTareWeight.Text = ""
            txtgross.Text = ""
            txtCCWt.Text = ""
            ddlshipline.SelectedValue = "0"
            txtShippConsignee.Text = ""
            ddlCondition.SelectedValue = "0"
            txtBkNo.Text = ""
            txtsurveryEir.Text = ""
            txtGrade.Text = ""
            txtlocation.Text = ""
            'txtgstpartyname.Text = ""
            txtRemarks.Text = ""
            txtdamageremarks.Text = ""
            txtCSCASP.Text = ""
            txtsurveryEir.Text = ""
            chkisActive.Checked = False
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnmode_Click(sender As Object, e As EventArgs)
        Try
            'If Trim(ddlmode.SelectedItem.Text & "") = "" Or Trim(ddlmode.SelectedItem.Text & "") = "--Select--" Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Mode can't be left blank.');", True)
            '    ddlmode.Focus()
            '    Exit Sub

            'End If
            'If Trim(txtmodeno.Text & "") = "" And Trim(ddlmode.SelectedItem.Text & "") <> "Cash (+)" Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Mode No can't be left blank.');", True)
            '    txtmodeno.Focus()
            '    Exit Sub
            'End If
            'If Trim(ddlbankname.SelectedValue & "") = "0" And Trim(ddlmode.SelectedItem.Text & "") <> "Cash (+)" Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Bank Name can't be left blank.');", True)

            '    ddlbankname.Focus()
            '    Exit Sub
            'End If
            'If Val(txtamount.Text) = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Amount can't be left blank.');", True)

            '    txtamount.Focus()
            '    Exit Sub
            'End If

            'strSql = ""
            'strSql += "USP_INSERT_TEMP_MODE_CONTAINER'" & Trim(ddlmode.SelectedValue & "") & "','" & Trim(txtmodeno.Text & "") & "','" & Trim(txtamount.Text & "") & "','" & Trim(ddlbankname.SelectedValue & "") & "',"
            'strSql += "'" & Convert.ToDateTime(Trim(txtmodate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_DepoCFS") & "'"
            'dt3 = db.sub_GetDatatable(strSql)

            'Dim dtDepoReceipt As New DataTable
            'Dim intRows As Integer = 0

            'dtDepoReceipt = DirectCast(Session("table_DepoReceiptContainer"), DataTable)
            'intRows = dtDepoReceipt.Rows.Count
            'Dim dtRow As DataRow = dtDepoReceipt.NewRow

            'dtRow.Item("paymode") = Trim(ddlmode.SelectedItem.Text & "")
            'dtRow.Item("paymodeID") = Val(ddlmode.SelectedValue & "")
            'dtRow.Item("Mode_No") = Trim(txtmodeno.Text & "")
            'dtRow.Item("Amount") = Trim(txtamount.Text & "")
            'If Trim(ddlbankname.SelectedValue) = "0" Then
            '    dtRow.Item("bankname") = ""
            'Else
            '    dtRow.Item("bankname") = Trim(ddlbankname.SelectedItem.Text & "")
            'End If

            'dtRow.Item("banknameID") = Trim(ddlbankname.SelectedValue & "")
            'dtRow.Item("Mode_Date") = Convert.ToDateTime(Trim(txtmodate.Text & "")).ToString("yyyy-MM-dd HH:mm")

            'dtDepoReceipt.Rows.Add(dtRow)
            'grdMode.DataSource = Nothing
            'grdMode.DataSource = dtDepoReceipt
            'grdMode.DataBind()

            'Session("table_DepoReceiptContainer") = dtDepoReceipt
            ''grid1()
            'ddlmode.SelectedValue = 0
            'txtmodeno.Text = ""
            'txtamount.Text = ""
            'ddlbankname.SelectedValue = "0"
            'UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grid1()
        Try
            'Dim dt As DataTable
            'dt = db.sub_GetDatatable("usp_mode_fill_container '" & Session("UserId_DepoCFS") & "'")
            'grdMode.DataSource = dt
            'grdMode.DataBind()
            'UpdatePanel2.Update()
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
        Dim dt6 As New DataSet

        strsize = Trim(ddlSize.SelectedItem.Text & "")
        strctype = Trim(ddlType.SelectedValue & "")
        intslid = Trim(ddlshipline.SelectedValue & "")
        'straccountid = 3
        'dbltotalamount = Val(txtamount.Text & "")
        If Trim(ddlEntrytype.SelectedItem.Text) = "Party Return" Then
            strdeltype = "OffLoading Invoice"
            'strdeltype = "Party Return"
        Else
            strdeltype = "OffLoading Invoice"
        End If

        strSql = ""
        strSql = "exec  SP_GET_EmptyContainerIN_tariffset_NEW1 '" & Trim(straccountid) & "','" & Trim(strctype & "") & "','" & Trim(strsize & "") & "','" & Trim(intslid & "") & "','" & Trim(strdeltype) & "','" & Trim(txtcontainerNo.Text & "") & "','" & Trim(txtTareWeight.Text & "") & "','" & Session("UserId_DepoCFS") & "'"
        dt6 = db.sub_GetDataSets(strSql)

        If dt6.Tables(0).Rows.Count > 0 Then
            If Trim(ddlEntrytype.SelectedItem.Text) = "Factory Return/Consignee" Then
                dbltotalamount += Val(dt6.Tables(0).Rows(0)("Amount") & "") * 2
            Else
                dbltotalamount += Val(dt6.Tables(0).Rows(0)("Amount") & "")
            End If

        Else
            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Tariff Not Found! ');", True)
            'Exit Sub
        End If
        If dt6.Tables(1).Rows.Count > 0 Then
            Dim dtDepoChargesCnt As New DataTable
            Dim intRows As Integer = 0

            dtDepoChargesCnt = DirectCast(Session("table_DepoChargesContainer"), DataTable)
            intRows = dtDepoChargesCnt.Rows.Count
            For i = 0 To dt6.Tables(1).Rows.Count - 1
                Dim dtRow As DataRow = dtDepoChargesCnt.NewRow
                dtRow.Item("ContainerNo") = Trim(dt6.Tables(1).Rows(i)("ContainerNo") & "")
                dtRow.Item("Size") = Val(dt6.Tables(1).Rows(i)("Size") & "")
                dtRow.Item("TareWeight") = Trim(dt6.Tables(1).Rows(i)("TareWeight") & "")
                dtRow.Item("TypeID") = Val(dt6.Tables(1).Rows(i)("TypeID") & "")
                dtRow.Item("AccountID") = Val(dt6.Tables(1).Rows(i)("AccountID") & "")
                If Trim(ddlEntrytype.SelectedItem.Text) = "Factory Return/Consignee" Then
                    dtRow.Item("NetAmount") = Val(dt6.Tables(1).Rows(i)("NetAmount") & "") * 2
                Else
                    dtRow.Item("NetAmount") = Val(dt6.Tables(1).Rows(i)("NetAmount") & "")
                End If
                dtDepoChargesCnt.Rows.Add(dtRow)
            Next
            grdAccounts.DataSource = Nothing
            grdAccounts.DataSource = dtDepoChargesCnt
            grdAccounts.DataBind()
            UpdatePanel4.Update()
            Session("table_DepoChargesContainer") = dtDepoChargesCnt
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
    Protected Sub btnsave_Click(sender As Object, e As EventArgs)
        Try
            Dim Workyear As String = "", blMode As Boolean = False, blContainerDets As Boolean = False
            If Now.Month < 4 Then
                Workyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
            ElseIf Now.Month >= 4 Then
                Workyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
            End If
            If chkisActive.Checked = True And Val(lblgstid.Text) = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select GST Name first! ');", True)
                txtgstpartyname.Focus()
                Exit Sub
            End If
            For Each row As GridViewRow In grdMode.Rows
                blMode = True
                Exit For
            Next
            For Each row As GridViewRow In grdcontainer.Rows
                blContainerDets = True
                Exit For
            Next
            If blContainerDets = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please add container details first! ');", True)
                txtcontainerNo.Focus()
                Exit Sub
            End If
            If chkisActive.Checked = True And blMode = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please add receipt details first! ');", True)
                ddlmode.Focus()
                Exit Sub
            End If

            For Each row As GridViewRow In grdcontainer.Rows
                Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)

                If (chkright.Checked = True) Then
                    If CType(row.FindControl("ddlLineID"), DropDownList).SelectedValue = 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter Line Name ');", True)
                        CType(row.FindControl("ddlLineID"), DropDownList).Focus()
                        Exit Sub
                    End If
                End If
                


            Next
            Dim strWorkYear As String = Workyear
            Dim dt7 As New DataTable
            Dim intEntryID As Integer = 0, intTrailerID As Integer = 0, strTrailerNo As String = "", intgateinid As Integer = 0
            Dim dt As New DataTable
            

            'Sub_SGTRate()
            'Dim strInWord As String
            'strInWord = dbamt.RupeesConvert(Val(txtgrandtotal.Text))
            'If Not chkisActive.Checked = True Then
            '    txttotalamount.Text = 0
            '    txtsgst.Text = 0
            '    txtcgst.Text = 0
            '    txtigst.Text = 0
            '    txtgrandtotal.Text = 0
            'End If
            'If Not Val(txtgrandtotal.Text) > 0 Then
            '    strInWord = "Zero Only"
            'End If
            'Dim dtDepoContainer As New DataTable, dtDepoReceipt As New DataTable, dtDepoChargesCnt As New DataTable
            'dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            'dtDepoReceipt = DirectCast(Session("table_DepoReceiptContainer"), DataTable)
            'dtDepoChargesCnt = DirectCast(Session("table_DepoChargesContainer"), DataTable)


            ''strSql = ""
            ''strSql += "USP_INSERT_EYARD_IN_NEW '" & Convert.ToDateTime(Trim(txtindatetime.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
            ''strSql += "'" & Trim(ddlEntrytype.SelectedItem.Text & "") & "','" & Trim(ddlmovementby.SelectedItem.Text & "") & "','" & Trim(txttruckNo.Text & "") & "',"
            ''strSql += "'" & Replace(Trim(txtTransporter.Text & ""), "'", "''") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_DepoCFS") & "',"
            ''strSql += "'" & strWorkYear & "',2,'" & Val(txttotalamount.Text & "") & "','" & Val(txtgrandtotal.Text & "") & "','" & strInWord & "',"
            ''strSql += "'" & Val(dblSGST) & "','" & Val(dblCGST) & "','" & Val(dblIGST) & "'," & Val(lblgstid.Text) & ",'" & dbltaxgroupid & "'"
            'Dim param As New SqlParameter()
            'Dim param1 As New SqlParameter()
            'Dim param2 As New SqlParameter()

            'Dim cmd As New SqlCommand()
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.AddWithValue("@InDate", Convert.ToDateTime(Trim(txtindatetime.Text & "")).ToString("yyyy-MM-dd HH:mm"))
            'cmd.Parameters.AddWithValue("@ENTRYTYPE", Trim(ddlEntrytype.SelectedItem.Text & ""))
            'cmd.Parameters.AddWithValue("@MOVTYPE", Trim(ddlmovementby.SelectedItem.Text & ""))
            'cmd.Parameters.AddWithValue("@TRUCKNO", Trim(txttruckNo.Text & ""))
            'cmd.Parameters.AddWithValue("@TRANSPORTER", Replace(Trim(txtTransporter.Text & ""), "'", "''"))
            'cmd.Parameters.AddWithValue("@ISINVOICEGEN", Trim(chkisActive.Checked & ""))
            'cmd.Parameters.AddWithValue("@ADDEDBY", Session("UserId_DepoCFS"))
            'cmd.Parameters.AddWithValue("@Workyear", strWorkYear)
            'cmd.Parameters.AddWithValue("@InvoiceType", 2)
            'cmd.Parameters.AddWithValue("@nettotal", Val(txttotalamount.Text & ""))
            'cmd.Parameters.AddWithValue("@grandtotal", Val(txtgrandtotal.Text & ""))
            'cmd.Parameters.AddWithValue("@totInWord", strInWord)
            'cmd.Parameters.AddWithValue("@SGSTPER", Val(dblSGST))
            'cmd.Parameters.AddWithValue("@CGSTPER", Val(dblCGST))
            'cmd.Parameters.AddWithValue("@igstPER", Val(dblIGST))
            'cmd.Parameters.AddWithValue("@PartyId", Val(lblgstid.Text))
            'cmd.Parameters.AddWithValue("@taxgroupid", dbltaxgroupid)

            'param.ParameterName = "@PT_EmptyInContainerDets"
            'param.Value = dtDepoContainer
            'param.TypeName = "PT_EmptyInContainerDets"
            'param.SqlDbType = SqlDbType.Structured
            'cmd.Parameters.Add(param)

            'param1.ParameterName = "@PT_EmptyInContainerReceiptDets"
            'param1.Value = dtDepoReceipt
            'param1.TypeName = "PT_EmptyInContainerReceiptDets"
            'param1.SqlDbType = SqlDbType.Structured
            'cmd.Parameters.Add(param1)

            'param2.ParameterName = "@PT_EmptyInFascilationDets"
            'param2.Value = dtDepoChargesCnt
            'param2.TypeName = "PT_EmptyInFascilationDets"
            'param2.SqlDbType = SqlDbType.Structured
            'cmd.Parameters.Add(param2)

            'Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
            'Dim con As New SqlConnection(strConnString)
            'cmd.CommandText = "USP_INSERT_EYARD_IN_NEW"
            'cmd.Connection = con
            'Dim da As New SqlDataAdapter()
            'da.SelectCommand = cmd
            'Dim dtEmptyIn As New DataTable
            'Try
            '    con.Open()
            '    da.Fill(dtEmptyIn)
            'Catch ex As Exception
            '    Response.Write(ex.Message)
            'Finally
            '    con.Close()
            '    con.Dispose()
            'End Try

            If Trim(ddlExternalLocation.SelectedValue) = "1" Then
                strSql = "SELECT isnull(MAX(entryID),0)+1 as EntryID, isnull(MAX(GateInNo),0)+1 as GateInNo FROM EYard_In WITH (XLOCK)"
                dt7 = db.sub_GetDatatable(strSql)
                If dt7.Rows.Count > 0 Then
                    intEntryID = dt7.Rows(0)("EntryID")
                    intgateinid = dt7.Rows(0)("GateInNo")
                Else
                    intEntryID = 1
                    intgateinid = 1
                End If
                For Each row As GridViewRow In grdcontainer.Rows

                    Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                    If (chkright.Checked = True) Then
                        strSql = ""
                        strSql += "USP_INSERT_EyarDIn_MTY_In'" & intEntryID & "','" & intgateinid & "','" & Convert.ToDateTime(Trim(txtindatetime.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlEntrytype.SelectedItem.Text & "") & "','" & Trim(ddlmovementby.SelectedItem.Text & "") & "',"
                        strSql += "'" & Trim(txttruckNo.Text & "") & "','" & Replace(Trim(txtTransporter.Text & ""), "'", "''") & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "',"
                        strSql += "'" & Val(CType(row.FindControl("lblSize"), Label).Text & "") & "','" & Val(CType(row.FindControl("lbltypeid"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblisoid"), Label).Text & "") & "',"
                        strSql += "'" & Val(CType(row.FindControl("lblTareWeight"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblStatusType"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblGrossWt"), Label).Text & "") & "',"
                        strSql += "'" & Val(CType(row.FindControl("ddlLineID"), DropDownList).SelectedValue & "") & "','" & Trim(CType(row.FindControl("lblConsignee"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblCondition"), Label).Text & "") & "',"
                        strSql += "'" & Convert.ToDateTime(CType(row.FindControl("lblMFGDate"), Label).Text.ToString()).ToString("yyyy-MM-dd") & "','" & Trim(CType(row.FindControl("lblBKNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSurveryEIR"), Label).Text & "") & "',"
                        strSql += "'" & Trim(CType(row.FindControl("lblGrade"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLocation"), Label).Text & "") & "','" & Replace(Trim(CType(row.FindControl("lblRemarks"), Label).Text & ""), "'", "''") & "',"
                        strSql += "'" & Replace(Trim(CType(row.FindControl("lblRemarks"), Label).Text & ""), "'", "''") & "','" & Convert.ToDateTime(CType(row.FindControl("txtDoValidDates"), TextBox).Text.ToString()).ToString("yyyy-MM-dd") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_DepoCFS") & "','" & Replace(Trim(CType(row.FindControl("lblCSCASP"), Label).Text & ""), "'", "''") & "','" & Trim(txtTrainNo.Text & "") & "'"
                        db.sub_ExecuteNonQuery(strSql)

                        intEntryID += 1
                        intgateinid += 1
                    End If



                Next
            End If
            If Trim(ddlExternalLocation.SelectedValue) = "2" Then
                strSql = "SELECT isnull(MAX(entryID),0)+1 as EntryID, isnull(MAX(GateInNo),0)+1 as GateInNo FROM External_MTY_In WITH (XLOCK)"
                dt7 = db.sub_GetDatatable(strSql)
                If dt7.Rows.Count > 0 Then
                    intEntryID = dt7.Rows(0)("EntryID")
                    intgateinid = dt7.Rows(0)("GateInNo")
                Else
                    intEntryID = 1
                    intgateinid = 1
                End If
                For Each row As GridViewRow In grdcontainer.Rows

                    Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                    If (chkright.Checked = True) Then
                        strSql = ""
                        strSql += "USP_INSERT_External_MTY_In'" & intEntryID & "','" & intgateinid & "','" & Convert.ToDateTime(Trim(txtindatetime.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlEntrytype.SelectedItem.Text & "") & "','" & Trim(ddlmovementby.SelectedItem.Text & "") & "',"
                        strSql += "'" & Trim(txttruckNo.Text & "") & "','" & Replace(Trim(txtTransporter.Text & ""), "'", "''") & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "',"
                        strSql += "'" & Val(CType(row.FindControl("lblSize"), Label).Text & "") & "','" & Val(CType(row.FindControl("lbltypeid"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblisoid"), Label).Text & "") & "',"
                        strSql += "'" & Val(CType(row.FindControl("lblTareWeight"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblStatusType"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblGrossWt"), Label).Text & "") & "',"
                        strSql += "'" & Val(CType(row.FindControl("ddlLineID"), DropDownList).SelectedValue & "") & "','" & Trim(CType(row.FindControl("lblConsignee"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblCondition"), Label).Text & "") & "',"
                        strSql += "'" & Convert.ToDateTime(CType(row.FindControl("lblMFGDate"), Label).Text.ToString()).ToString("yyyy-MM-dd") & "','" & Trim(CType(row.FindControl("lblBKNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSurveryEIR"), Label).Text & "") & "',"
                        strSql += "'" & Trim(CType(row.FindControl("lblGrade"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblLocation"), Label).Text & "") & "','" & Replace(Trim(CType(row.FindControl("lblRemarks"), Label).Text & ""), "'", "''") & "',"
                        strSql += "'" & Replace(Trim(CType(row.FindControl("lblDamageRemarks"), Label).Text & ""), "'", "''") & "','" & Convert.ToDateTime(CType(row.FindControl("txtDoValidDates"), TextBox).Text.ToString()).ToString("yyyy-MM-dd") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_DepoCFS") & "','" & Replace(Trim(CType(row.FindControl("lblCSCASP"), Label).Text & ""), "'", "''") & "','" & Trim(txtTrainNo.Text & "") & "'"
                        db.sub_ExecuteNonQuery(strSql)

                        intEntryID += 1
                        intgateinid += 1
                    End If



                Next
            End If
          

            'Dim StrlineID As String = Trim(ddlshipline.SelectedValue & "")
            'Dim intmaxAssessno As Integer = 0, intmaxReceiptno As Integer = 0, strInvoiceNo As String = ""
            'Dim dtassess As New DataTable
            'strSql = ""
            'strSql = "select isnull(max(assessNo),0) assessNo  from eyard_assessM where workyear='" & strWorkYear & "'"
            'dtassess = db.sub_GetDatatable(strSql)
            'If dtassess.Rows.Count > 0 Then
            '    intmaxAssessno = Val(dtassess.Rows(0)("assessNo") & "") + 1
            'End If
            'strSql = ""
            'strSql = "exec SP_Save_eyard_assessM  " & intmaxAssessno & ",'" & strWorkYear & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlEntrytype.SelectedItem.Text & "") & "','" & Val(ddlshipline.SelectedValue) & "','" & Replace(Trim(txtShippConsignee.Text & ""), "'", "''") & "','" & Session("UserId_DepoCFS") & "','" & Format(Now, "yyyy-MMM-dd HH:mm") & "','" & Val(txttotalamount.Text & "") & "','" & Val(txtgrandtotal.Text & "") & "','" & strInWord & "','" & Trim(intgateinid & "") & "', " & Val(txtsgst.Text) & ", " & Val(txtcgst.Text) & ", " & Val(txtigst.Text) & ", " & Val(lblgstid.Text) & ","
            'strSql += "2,'" & Convert.ToDateTime(txtDovaliddate.Text).ToString("yyyy-MM-dd HH:mm") & "'"
            'dt = db.sub_GetDatatable(strSql)
            'If dt.Rows.Count > 0 Then
            '    intmaxAssessno = Val(dt.Rows(0)("ASSESS_NO"))
            '    strInvoiceNo = Trim(dt.Rows(0)("INVOICE_NO"))
            'End If
            'For Each row As GridViewRow In grdcontainer.Rows
            'If chkisActive.Checked = True Then
            '    For Each row1 In grdAccounts.Rows
            '        strSql = ""
            '        strSql += "EXEC  SP_Save_eyard_assessD '" & intmaxAssessno & "','" & strWorkYear & "','E',"
            '        strSql += "'" & intEntryID & "','" & Trim(CType(row1.FindControl("lblContainerNo"), Label).Text & "") & "','" & Val(CType(row1.FindControl("lblSize"), Label).Text & "") & "','" & Val(CType(row1.FindControl("lblType"), Label).Text & "") & "',"
            '        strSql += "'" & Val(CType(row1.FindControl("lblAccountID"), Label).Text & "") & "','" & Val(CType(row1.FindControl("lblNetAmount"), Label).Text & "") & "',"
            '        strSql += "'" & Val(CType(row1.FindControl("lbltareWeight"), Label).Text & "") & "','0','','','" & (Val(CType(row1.FindControl("lblNetAmount"), Label).Text & "") * (dblSGST / 100)) & "', '" & (Val(CType(row1.FindControl("lblNetAmount"), Label).Text & "") * (dblCGST / 100)) & "', '" & (Val(CType(row1.FindControl("lblNetAmount"), Label).Text & "") * (dblIGST / 100)) & "','" & strInvoiceNo & "'," & Val(lblTaxID.Text) & ""
            '        db.sub_ExecuteNonQuery(strSql)
            '    Next
            'Else
            '    For Each row1 In grdAccounts.Rows
            '        strSql = ""
            '        strSql += "EXEC  SP_Save_eyard_assessD '" & intmaxAssessno & "','" & strWorkYear & "','E',"
            '        strSql += "'" & intEntryID & "','" & Trim(CType(row1.FindControl("lblContainerNo"), Label).Text & "") & "','" & Val(CType(row1.FindControl("lblSize"), Label).Text & "") & "','" & Val(CType(row1.FindControl("lblType"), Label).Text & "") & "',"
            '        strSql += "'" & Val(CType(row1.FindControl("lblAccountID"), Label).Text & "") & "','0',"
            '        strSql += "'" & Val(CType(row1.FindControl("lbltareWeight"), Label).Text & "") & "','0','','','0', '0', '0','" & strInvoiceNo & "'," & Val(lblTaxID.Text) & ""
            '        db.sub_ExecuteNonQuery(strSql)
            '    Next
            'End If
            'Next
            'If blMode = True Then
            '    strSql = ""
            '    strSql = "select isnull(max(ReceiptNo),0) ReceiptNo  from Eyard_Receipt With (XLOCK) where workyear='" & strWorkYear & "'"
            '    dtassess = db.sub_GetDatatable(strSql)
            '    If dtassess.Rows.Count > 0 Then
            '        intmaxReceiptno = Val(dtassess.Rows(0)("ReceiptNo") & "") + 1
            '    End If
            '    strSql = ""
            '    strSql = "EXEC  SP_Save_eyard_Receipt  '" & intmaxReceiptno & "','" & strWorkYear & "','M','" & Format(Now, "yyyy-MMM-dd HH:mm") & "','" & intmaxAssessno & "','" & strWorkYear & "'"
            '    strSql += " ,'" & Trim(txtgrandtotal.Text & "") & "','" & Trim(txtgrandtotal.Text & "") & "','','','','','" & strInWord & "',0,0,'','" & Session("UserId_DepoCFS") & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(intgateinid & "") & "'"
            '    db.sub_ExecuteNonQuery(strSql)
            '    For Each row As GridViewRow In grdMode.Rows
            '        strSql = ""
            '        strSql += "EXEC  SP_Save_eyard_Receipt_mode  '" & intmaxReceiptno & "','" & strWorkYear & "','" & Trim(CType(row.FindControl("lblmode"), Label).Text & "") & "',"
            '        strSql += "'" & Val(CType(row.FindControl("lblamount"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblmodeno"), Label).Text & "") & "',"
            '        strSql += "'" & Trim(CType(row.FindControl("lblbankname"), Label).Text & "") & "','" & Convert.ToDateTime(CType(row.FindControl("lblmodedate"), Label).Text.ToString()).ToString("yyyy-MM-dd") & "','','',''"
            '        db.sub_ExecuteNonQuery(strSql)
            '    Next
            'End If
            'txtEyardInPrint.Text = Trim(dt7.Rows(0)("GateInNo") & "")
            'txtEyardRecipt.Text = Trim(dt7.Rows(0)("GateInNo") & "")
            subClear()
            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    'Protected Sub SaveOk_ServerClick(sender As Object, e As EventArgs)
    '    Try
    '        lblPrintQue.Text = "Do you wish to print ?"
    '        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
    '        UpdatePanel6.Update()
    '    Catch ex As Exception
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    End Try
    'End Sub
    Private Sub subClear()
        ddlEntrytype.SelectedValue = 0
        ddlmovementby.SelectedValue = 0
        txttruckNo.Text = ""
        txtamount.Text = ""
        txtsgst.Text = ""
        txtcgst.Text = ""
        txtigst.Text = ""
        txtgrandtotal.Text = ""
        chkisActive.Checked = False
        txtTransporter.Text = ""
        txtgstpartyname.Text = ""
        txtGSTNo.Text = ""
        lblgstid.Text = ""
        lblstatecode.Text = ""
        txtindatetime.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
        txtMFGDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM")
        txtDovaliddate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
        txtmodate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
        db.sub_ExecuteNonQuery("delete from TEMP_EMPTY_CONTAINER_IN where USERID=" & Session("UserId_DepoCFS") & "")
        db.sub_ExecuteNonQuery("delete from TEMP_MODE_CONTAINER where USERID=" & Session("UserId_DepoCFS") & "")
        db.sub_ExecuteNonQuery("delete from Temp_FACILITATION_CHARGES where USERID=" & Session("UserId_DepoCFS") & "")
        sub_CreateTable()
        grid()
        grid1()
        txtcontainerNo.Text = ""
        ddlSize.SelectedValue = "0"
        ddlType.SelectedValue = "0"
        ddlISOCode.SelectedValue = "0"
        ddlstatusType.SelectedValue = "0"
        txtTareWeight.Text = ""
        txtgross.Text = ""
        txtCCWt.Text = ""
        ddlshipline.SelectedValue = "0"
        txtShippConsignee.Text = ""
        ddlCondition.SelectedValue = "0"
        txtBkNo.Text = ""
        txtsurveryEir.Text = ""
        txtGrade.Text = ""
        txtlocation.Text = ""
        'txtgstpartyname.Text = ""
        txtRemarks.Text = ""
        txtdamageremarks.Text = ""
        txtCSCASP.Text = ""
        txtsurveryEir.Text = ""
        chkisActive.Checked = False


    End Sub
    Protected Sub ddlSize_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If Not (Val(ddlSize.SelectedValue) = 0 And Val(ddlType.SelectedValue) = 0) Then
                strSql = ""
                strSql += "USP_ISO_CODES_FROM_SIZE_N_TYPE " & Val(ddlSize.SelectedValue) & "," & Val(ddlType.SelectedValue) & ""
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ddlISOCode.SelectedValue = Val(dt.Rows(0)("ISOID"))
                End If
            End If
            ddlType.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If Not (Val(ddlSize.SelectedValue) = 0 And Val(ddlType.SelectedValue) = 0) Then
                strSql = ""
                strSql += "USP_ISO_CODES_FROM_SIZE_N_TYPE " & Val(ddlSize.SelectedValue) & "," & Val(ddlType.SelectedValue) & ""
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ddlISOCode.SelectedValue = Val(dt.Rows(0)("ISOID"))
                End If
            End If
            ddlISOCode.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtgross_TextChanged(sender As Object, e As EventArgs)
        Try
            txtCCWt.Text = txtgross.Text - txtTareWeight.Text
            txtCSCASP.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtcontainerNo_TextChanged(sender As Object, e As EventArgs)
        Try
            If Not Trim(txtcontainerNo.Text & "") = "" Then
                strSql = ""
                strSql += "USP_CHECK_CONTAINER_DIGIT '" & Trim(txtcontainerNo.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If Trim(dt.Rows(0)(0)) = "false" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter correct container no! ');", True)
                    txtcontainerNo.Text = ""
                    txtcontainerNo.Focus()
                    Exit Sub
                Else
                    ddlSize.Focus()
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub chkisActive_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chkisActive.Checked = True And Val(lblgstid.Text) = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select GST Name first! ');", True)
                txtgstpartyname.Focus()
                lnksearch.Visible = True
                chkisActive.Checked = False
                Exit Sub
            End If
            If chkisActive.Checked = True Then
                Calculation()
            Else
                txtsgst.Text = 0
                txtcgst.Text = 0
                txtigst.Text = 0
                txtgrandtotal.Text = 0
                lblTaxID.Text = 0
                ddlmode.SelectedValue = 0
                txtamount.Text = ""
            End If
            chkisActive.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txttruckNo_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "exec  Usp_gettrailern '" & Trim(txttruckNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtTransporter.Text = Trim(dt.Rows(0)("TransName") & "")

            End If
            txtTransporter.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "SELECT TrainNo  FROM External_MTY_In where DATEDIFF (d, addedon , GETDATE ())>=4 and TrainNo='" & Trim(txtTrainNo.Text) & "' AND STATUS='P' AND ISCANCEL=0"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtTrainNo.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Train No Already Gate in .');", True)
                txtTrainNo.Focus()
                Exit Sub
            End If



            strSql = ""
            strSql = "exec  USP_Train_No_fill '" & Trim(txtTrainNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try
            If (e.Row.RowType = DataControlRowType.DataRow) Then
                Dim ddlLineID As DropDownList = CType(e.Row.FindControl("ddlLineID"), DropDownList)
                strSql = ""
                strSql = "select SLID ,SLName from ShipLines "
                dt = db.sub_GetDatatable(strSql)
                ddlLineID.DataSource = dt
                ddlLineID.DataTextField = "SLName"
                ddlLineID.DataValueField = "SLID"
                ddlLineID.DataBind()
                ddlLineID.Items.Insert(0, New ListItem("Select", 0))
                Dim Line As String = CType(e.Row.FindControl("lblLineid"), Label).Text
                If Not (Line = "") Then
                    ddlLineID.Items.FindByValue(Line).Selected = True
                End If
            End If

           
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnTrainSearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select Top (1) TrainNO from Temp_Train_Container_search  where userid='" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtTrainNo.Text = Trim(dt.Rows(0)("TrainNO") & "")
                
            End If
            Call btnShow_Click(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            strSql = ""
            strSql = "exec  USP_Train_No_fill '" & Trim(txtTrainNo.Text & "") & "'"
            dt11 = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt11
            grdcontainer.DataBind()

            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt11, "Container Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt11.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(8)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("A5:" & Excelno & "5").Merge()
                        .Range("A6:" & Excelno & "6").Merge()
                        .Range("A7:" & Excelno & "7").Merge()
                        .Range("A8:" & Excelno & "8").Merge()


                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
                        .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
                        .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(7).Height = 20
                        '.Cell(6, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        '.Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray


                        .Cell(7, 1).Value = "Container Details"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(2, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(4, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(5, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(5, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(7, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(7, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(7, 1).Style.Font.FontSize = 17
                        .Cell(7, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(1, 1).Style.Font.FontSize = 20
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=ContainerSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        'getdatewiseWO()
                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No record found!');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

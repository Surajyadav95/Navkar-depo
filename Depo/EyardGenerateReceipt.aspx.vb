Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim strId As String
    Dim strname As String
    Dim strcolname As String
    Dim dblServiceTax As String
    Dim dblEDUCESS As String
    Dim dblHIGHCESS As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim TariffID, TariffIDView As String
    Dim ed As New clsEncodeDecode
    Dim dblReceiptNo As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Convert.ToInt32(Session("UserId_DepoCFS")) = 0 Then
            Response.Redirect("~/Depo/Login.aspx")
            Exit Sub
        End If
        txtmodeAmount.BorderColor = System.Drawing.Color.Gainsboro
        txtModeNo.BorderColor = System.Drawing.Color.Gainsboro
        ddlbank.BorderColor = System.Drawing.Color.Gainsboro
        txtbankcod.BorderColor = System.Drawing.Color.Gainsboro
        ddlBranch.BorderColor = System.Drawing.Color.Gainsboro
        txtmodedate.BorderColor = System.Drawing.Color.Gainsboro
        txtAmount.BorderColor = System.Drawing.Color.Gainsboro

        If Not IsPostBack Then
            grid()
            Filldropdown()
            getLastReceiptNo()
            db.sub_ExecuteNonQuery("USP_DeleteFrom_Temp " & Session("UserId_DepoCFS") & "")
            txtReceiptdate.Text = Convert.ToDateTime(Now).ToString("dd-MM-yyyy")
            txtmodedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")

            txtWorkYear.Text = Session("Workyear")
            ddlpayment_SelectedIndexChanged(sender, e)
            FillTodayCllection()
            txtAssessment.Focus()
        End If

    End Sub

    Public Sub FillTodayCllection()
        Try
            ds = db.sub_GetDataSets("USP_CurrentDate_Collection_eyard")
            rptAmount.DataSource = ds.Tables(0)
            rptAmount.DataBind()
            'If (ds.Tables(0).Rows.Count > 0) Then
            '    txtcash.Text = ds.Tables(0).Rows(0)(0)
            'End If
            'If (ds.Tables(1).Rows.Count > 0) Then
            '    txtcheque.Text = ds.Tables(1).Rows(0)(0)
            'End If
            'If (ds.Tables(2).Rows.Count > 0) Then
            '    txtdd.Text = ds.Tables(2).Rows(0)(0)
            'End If
            'If (ds.Tables(3).Rows.Count > 0) Then
            '    txtpo.Text = ds.Tables(3).Rows(0)(0)
            'End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub grid()
        strSql = ""
        strSql += ""
        dt = db.sub_GetDatatable(strSql)
        grdPaymentDetails.DataSource = dt
        grdPaymentDetails.DataBind()
    End Sub
    Public Sub getLastReceiptNo()
        dt = db.sub_GetDatatable("USP_get_Last_Receipt_NO_eyard")
        txtlastreceipt.Text = dt.Rows(0)("ReceiptNo")
    End Sub
    Protected Sub Filldropdown()
        Try

            ds = db.sub_GetDataSets("USP_receipt_fill_eyard")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlbank.DataSource = ds.Tables(0)
                ddlbank.DataTextField = "bankname"
                ddlbank.DataValueField = "bankID"
                ddlbank.DataBind()
                ddlbank.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            'If (ds.Tables(1).Rows.Count > 0) Then
            '    ddlPdbalance.DataSource = ds.Tables(1)
            '    ddlPdbalance.DataTextField = "Balance"
            '    ddlPdbalance.DataValueField = "PDCode"
            '    ddlPdbalance.DataBind()
            '    ddlPdbalance.Items.Insert(0, New ListItem("--Select--", 0))
            'End If

            ds = db.sub_GetDataSets("USP_Get_Payment_Mode")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlmode.DataSource = ds.Tables(0)
                ddlmode.DataTextField = "Paymode"
                ddlmode.DataValueField = "PaymodeID"
                ddlmode.DataBind()
                ddlmode.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                ddlBranch.DataSource = ds.Tables(1)
                ddlBranch.DataTextField = "BranchName"
                ddlBranch.DataValueField = "BranchID"
                ddlBranch.DataBind()
                ddlBranch.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(2).Rows.Count > 0) Then
                ddlpayment.DataSource = ds.Tables(2)
                ddlpayment.DataTextField = "Pay_From"
                ddlpayment.DataValueField = "Pay_FRom_ID"
                ddlpayment.DataBind()
                '  ddlpayment.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            If (ds.Tables(3).Rows.Count > 0) Then
                ddltds.DataSource = ds.Tables(3)
                ddltds.DataTextField = "Percentage"
                ddltds.DataValueField = "TDS_PER_ID"
                ddltds.DataBind()
                ddltds.Items.Insert(0, New ListItem("--Select--", 0))
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try

            strSql = ""
            strSql = "USP_check_IsReceipt_eyard '" & Trim(txtAssessment.Text & "") & "','" & Trim(txtWorkYear.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Key", "alert('Receipt already generated for specified assessment no.!');", True)

                Exit Sub
            End If
            Dim IsFoundAssesment As Integer = ds.Tables(1).Rows(0)("CntAssemnent")
            If (IsFoundAssesment = 0) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "Key", "alert('Specified Invoice no. is not found.');", True)
                txtnoc.Text = ""
                txtCHAcode.Text = ""
                txtConsignee.Text = ""
                txtAmount.Text = ""
                
                lblAsseDate.Text = ""
                ddlfill.SelectedValue = 0
                ddltds.SelectedValue = 0
                txtmodedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
                txtremarks.Text = ""
                db.sub_ExecuteNonQuery("USP_DeleteFrom_Temp " & Session("UserId_DepoCFS") & "")
                'grdPaymentDetails.DataSource = Nothing
                Call grid()
                grdPaymentDetails.DataBind()
                getLastReceiptNo()
                UpdatePanel2.Update()
                up_grid.Update()
                Exit Sub
            End If

            strSql = ""
            strSql = "usp_receipt_check_eyard '" & Trim(txtAssessment.Text & "") & "','" & Trim(txtWorkYear.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                'txtnoc.Text = Trim(ds.Tables(0).Rows(0)("NOCNo") & "")
                'txtCHAcode.Text = Trim(ds.Tables(0).Rows(0)("CHAName") & "")
                'txtConsignee.Text = Trim(ds.Tables(0).Rows(0)("ImporterName") & "")
                txtAmount.Text = Trim(ds.Tables(0).Rows(0)("GrandTotal") & "")
                lblLine.Text = Trim(ds.Tables(0).Rows(0)("SlName") & "")
                lblAsseDate.Text = Trim(ds.Tables(0).Rows(0)("AssDate") & "")
                ddlfill.SelectedValue = Val(ds.Tables(0).Rows(0)("lineID"))
            End If

            If (ds.Tables(1).Rows.Count > 0) Then
                txtlastreceipt.Text = Trim(ds.Tables(1).Rows(0)("Receipt") & "")
            End If
            txtmodedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            UpdatePanel2.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try

            If (txtAmount.Text = "0.00") Then
                ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Please fill Amount');", True)
                txtAmount.BorderColor = System.Drawing.Color.Red
                btnAdd.Text = "Add"
                btnAdd.CssClass = "btn btn-primary btn-sm outline"
                Exit Sub
            End If

            If (ddlmode.SelectedValue = 0) Then
                ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Please select mode of payment');", True)
                ddlmode.BorderColor = System.Drawing.Color.Red
                btnAdd.Text = "Add"
                btnAdd.CssClass = "btn btn-primary btn-sm outline"
                Exit Sub
            End If

            If (ddlmode.SelectedItem.Text = "Cash (+)" Or ddlmode.SelectedItem.Text = "Credit (-)" Or ddlmode.SelectedItem.Text = "TDS") Then
                ' txtmodedate.Text = ""
                If (txtmodeAmount.Text = "" Or txtmodeAmount.Text = "0.00") Then
                    ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Please fill amount');", True)
                    txtmodeAmount.BorderColor = System.Drawing.Color.Red
                    btnAdd.Text = "Add"
                    btnAdd.CssClass = "btn btn-primary btn-sm outline"
                    Exit Sub
                End If
                'If (txtmodedate.Text = "") Then
                '    ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Please fill Mode date');", True)
                '    txtmodedate.BorderColor = System.Drawing.Color.Red
                '    btnAdd.Text = "Add"
                '    btnAdd.CssClass = "btn btn-primary btn-sm outline"
                '    Exit Sub
                'End If
            Else
                If (txtmodeAmount.Text = "" Or txtmodeAmount.Text = "0.00") Then
                    ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Please fill amount');", True)
                    txtmodeAmount.BorderColor = System.Drawing.Color.Red
                    btnAdd.Text = "Add"
                    btnAdd.CssClass = "btn btn-primary btn-sm outline"
                    Exit Sub
                End If
                If (txtModeNo.Text = "") Then
                    ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Please fill mode No.');", True)
                    txtModeNo.BorderColor = System.Drawing.Color.Red
                    btnAdd.Text = "Add"
                    btnAdd.CssClass = "btn btn-primary btn-sm outline"
                    Exit Sub
                End If
                If (ddlbank.SelectedValue = "0") Then
                    ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Please select bank name');", True)
                    ddlbank.BorderColor = System.Drawing.Color.Red
                    btnAdd.Text = "Add"
                    btnAdd.CssClass = "btn btn-primary btn-sm outline"
                    Exit Sub
                End If

                If (ddlBranch.SelectedValue = "0") Then
                    ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Please select branch name');", True)
                    ddlBranch.BorderColor = System.Drawing.Color.Red
                    'btnAdd.Text = "Add"
                    'btnAdd.CssClass = "btn btn-primary btn-sm outline"
                    Exit Sub
                End If
                If (txtmodedate.Text = "") Then
                    ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Please fill mode date');", True)
                    txtmodedate.BorderColor = System.Drawing.Color.Red
                    btnAdd.Text = "Add"
                    btnAdd.CssClass = "btn btn-primary btn-sm outline"
                    Exit Sub
                End If
            End If

            Dim Calculation = 0
            Dim IsTDS As String
            For Each row As GridViewRow In grdPaymentDetails.Rows

                Calculation = Val(CType(row.FindControl("lblPayAmount"), Label).Text.ToString()) + Calculation
                If (Trim(CType(row.FindControl("lblPaymode"), Label).Text.ToString()) = "TDS") Then
                    IsTDS = "TDS"
                End If
            Next



            Dim AmountValidation As Double = txtAmount.Text - (txtmodeAmount.Text + Calculation)
            If (AmountValidation < 0) Then
                ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Amount specifications seems to be wrong. Please check!');", True)
                txtmodeAmount.BorderColor = System.Drawing.Color.Red
                Exit Sub
            End If


            btnAdd.Text = "Please wait..."
            btnAdd.CssClass = "btn btn-primary btn-sm outline disabled"
            UpdatePanel2.Update()

            pnlmode.Enabled = True

            strSql = ""
            strSql = "USP_INSERT_TEMP_RECEIPT_T"
            strSql += "'" & Trim(ddlmode.SelectedValue & "") & "','" & Trim(txtmodeAmount.Text & "") & "','" & Trim(ddlbank.SelectedValue & "") & "',"
            strSql += "'" & Trim(txtbankcod.Text & "") & "','" & Trim(ddlBranch.SelectedValue & "") & "',"
            strSql += "'" & Convert.ToDateTime(Trim(txtmodedate.Text & "")).ToString("yyyy-MM-dd") & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtModeNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                If (dt.Rows(0)(0) <> "Inserted") Then
                    btnAdd.Text = "Add"
                    btnAdd.CssClass = "btn btn-primary btn-sm outline"
                    ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('" & dt.Rows(0)(0) & "');", True)

                    Exit Sub
                End If
            End If

            ddlmode.SelectedValue = 0
            txtmodeAmount.Text = ""
            txtModeNo.Text = ""
            ddlbank.SelectedValue = 0
            txtbankcod.Text = ""
            ddlBranch.SelectedValue = 0
            txtmodedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            ddlmode.Focus()
            btnAdd.Text = "Add"
            btnAdd.CssClass = "btn btn-primary btn-sm outline"
            Receipt(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Receipt(sender As Object, e As EventArgs)
        Try
            'Dim dt As DataTable
            ds = db.sub_GetDataSets("usp_select_tmp_Receipt '" & Session("UserId_DepoCFS") & "'")
            grdPaymentDetails.DataSource = ds.Tables(0)
            grdPaymentDetails.DataBind()
            lblAmount.Text = ds.Tables(1).Rows(0)(0)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlMode_SelectedIndexChange(sender As Object, e As EventArgs)
        Try
            If (txtmodeAmount.Text = "0.00" Or txtmodeAmount.Text = "") Then
                Dim CalculationAmt = 0
                For Each row As GridViewRow In grdPaymentDetails.Rows

                    CalculationAmt = Val(CType(row.FindControl("lblPayAmount"), Label).Text.ToString()) + CalculationAmt
                Next
                Dim dbla As Decimal = txtAmount.Text - CalculationAmt
                '  Dim r As Double = Math.Round(dbla, 0)
                txtmodeAmount.Text = dbla.ToString("0.00")
                'Dim dbla As Decimal = txtAmount.Text - lblAmount.Text
                'Dim r As Double = Math.Round(dbla, 0)
                'txtmodeAmount.Text = r.ToString("0.00")
            End If

            If (ddlmode.SelectedItem.Text = "Cash (+)" Or ddlmode.SelectedItem.Text = "Credit (-)" Or ddlmode.SelectedItem.Text = "TDS") Then
                txtModeNo.Text = ""
                ddlbank.SelectedValue = 0
                txtbankcod.Text = ""
                ddlBranch.SelectedValue = 0
                panelPayment.Enabled = False
            Else
                panelPayment.Enabled = True
            End If
            txtmodedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlpayment_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try

            If Trim(ddlpayment.SelectedItem.Text) = "ShipLines" Then
                strname = "ShipLines"
                strId = "SLID"
                strcolname = "SLName"
                FillName()
                'ElseIf ddlpayment.SelectedItem.Text = "Line" Then
                '    strname = "ShipLines"
                '    strId = "SLID"
                '    strcolname = "SLName"
                '    FillName()
                'ElseIf ddlpayment.SelectedItem.Text = "IMPORTER" Then
                '    strname = "IMPORTER"
                '    strId = "ImporterID"
                '    strcolname = "ImporterName"
                '    FillName()
                'ElseIf ddlpayment.SelectedItem.Text = "CUSTOMOER" Then
                '    strname = "agent"
                '    strId = "agentID"
                '    strcolname = "agentName"
                '    FillName()
            End If
            txtmodedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkdelete_Command(sender As Object, e As CommandEventArgs)
        Try
            Dim AutoTemp As String = e.CommandArgument

            Dim strSQL As String = ""
            strSQL = "USP_Delete_Paymode " & AutoTemp & ""
            db.sub_ExecuteNonQuery(strSQL)
            Receipt(sender, e)
            lblSession.Text = "Record cancelled successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Private Sub FillName()
        txtID.Text = ""
        strSql = ""
        strSql = "USP_cmb_receipt_search_New_eyard '" & Trim(ddlpayment.SelectedItem.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        If (dt.Rows.Count > 0) Then
            ddlfill.DataSource = dt
            ddlfill.DataTextField = "strcolname"
            ddlfill.DataValueField = "strID"
            ddlfill.DataBind()
            ddlfill.Items.Insert(0, New ListItem("--Select--", 0))
        End If
    End Sub
    Protected Sub ddlfill_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If Trim(ddlfill.SelectedItem.Text) <> "" Then

                strcolname = "SLID"
                Filltext()
                'ElseIf Trim(ddlfill.SelectedItem.Text) <> "" Then

                '    strcolname = "SaID"
                '    Filltext()
                'ElseIf Trim(ddlfill.SelectedItem.Text) <> "" Then

                '    strcolname = "agentCode"
                '    Filltext()
                'ElseIf Trim(ddlfill.SelectedItem.Text) <> "" Then

                '    strcolname = "CodeID"
                '    Filltext()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Filltext()
        Try
            strSql = ""
            strSql = "usp_text_fill_New '" & Trim(ddlfill.SelectedValue & "") & "','" & Trim(ddlpayment.SelectedItem.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                txtID.Text = Trim(dt.Rows(0)("strcolname") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try

    End Sub
    Protected Sub ddltds_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            pnlmode.Enabled = True
            If (ddltds.SelectedValue <> 0) Then
                ddlmode.SelectedValue = 7
                panelPayment.Enabled = False
                ' UpdatePanel4.Update()
                ' ddlMode_SelectedIndexChange(sender, e)
            Else
                ddlmode.SelectedValue = 0
            End If
            Dim IsTDS As String = ""
            For Each row As GridViewRow In grdPaymentDetails.Rows

                If (Trim(CType(row.FindControl("lblPaymode"), Label).Text.ToString()) = "TDS") Then
                    IsTDS = "TDS"
                End If
            Next
            If (ddltds.SelectedValue <> 0) Then
                If (IsTDS = "") Then
                    pnlmode.Enabled = False                   
                End If
            End If

            Dim IntGrntAmt As Double
            Dim IntotherAmt As Double
            Dim intITdsAmt As Double
            Dim IntIITDSAmt As Double
            Dim dblStaxAmt As Double
            Dim dblEcessAmt As Double
            Dim dblHcessAmt As Double
            Dim dblnetamount As Double
            IntGrntAmt = 0
            IntotherAmt = 0
            intITdsAmt = 0
            IntIITDSAmt = 0
            dblStaxAmt = 0
            dblEcessAmt = 0
            dblHcessAmt = 0
            dblnetamount = 0

            If txtAmount.Text <> "" Then
                strSql = ""
                strSql = "USP_Get_Net_Amount_eyard  " & Trim(txtAssessment.Text) & "," & ddltds.SelectedValue & ",'" & Trim(txtWorkYear.Text) & "' "
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    Dim dbla1 As Decimal = Format(dt.Rows(0)("TDSAmount"), "0.00")
                    Dim r1 As Double = Math.Round(dbla1, 0)
                    txtmodeAmount.Text = r1.ToString("0.00")

                    ' txtmodeAmount.Text = Format(dt.Rows(0)("TDSAmount"), "0.00")
                    '(Convert.ToDouble(Trim(dt.Rows(0)("TDSAmount"))).ToString("0.00"))
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks"))
                    lbltdsAmtNew.Text = r1.ToString("0.00")
                    'If dt.Rows(0)("NetAmount") = True Then
                    '    IntGrntAmt = 0
                    'Else
                    '    IntGrntAmt = Trim(dt.Rows(0)("NetAmount"))
                    'End If
                    'dblStaxAmt = Val(IntGrntAmt) * dblServiceTax / 100
                    'dblEcessAmt = Val(dblStaxAmt * dblEDUCESS / 100)
                    'dblHcessAmt = Val(dblStaxAmt * dblHIGHCESS / 100)
                    'IntotherAmt = Val(txtAmount.Text) - IntGrntAmt - dblHcessAmt - dblEcessAmt - dblStaxAmt
                    'IntGrntAmt = IntGrntAmt + dblHcessAmt + dblEcessAmt + dblStaxAmt
                End If
                txtmodedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
                UpdatePanel2.Update()
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub


    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try

            Dim ReceiptAmount As Double = txtAmount.Text - lblAmount.Text

            Dim year As Integer = Convert.ToDateTime(Trim(txtReceiptdate.Text & "")).ToString("yy")
            Dim inword As Double = txtAmount.Text - lbltdsAmtNew.Text
            If Not (ReceiptAmount = 0) Then
                ScriptManager.RegisterStartupScript(btnSave, btnSave.GetType, "Key", "alert('Amount specifications seems to be wrong. Please check!');", True)
                btnSave.Text = "Save"
                btnSave.CssClass = "btn btn-success btn-sm outline"
                Exit Sub
            End If
            ' Thread.Sleep(10000)
            dt = db.sub_GetDatatable("USP_get_Max_RevisionNo_eyard  '" & txtWorkYear.Text & "'")

            If dt.Rows(0)(0) = 0 Then
                dblReceiptNo = year & Strings.Right("000000" & 1, 6) 'Right$(String(6, "0") & Val(Mid(0, 3)) + 1, 6)
            Else
                dblReceiptNo = Val(dt.Rows(0)(0) + 1)
            End If

            Dim strInWord As String
            strInWord = AmountInWords(Val(txtAmount.Text))
            strSql = ""
            strSql += "SP_Save_eyard_Receipt '" & dblReceiptNo & "','" & Session("Workyear") & "','M','" & Convert.ToDateTime(Trim(txtReceiptdate.Text & "")).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & Trim(txtAssessment.Text) & "','" & txtWorkYear.Text & "',"
            strSql += "'" & Trim(txtAmount.Text - lbltdsAmtNew.Text) & "','" & Trim(txtAmount.Text - lbltdsAmtNew.Text) & "','','','','','" & strInWord & "',0,0,'" & Trim(txtremarks.Text) & "','" & Session("UserId_DepoCFS") & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "',''"
         
            dt = db.sub_GetDatatable(strSql)

            For Each row As GridViewRow In grdPaymentDetails.Rows
                strSql = ""
                strSql += "USP_Insert_Into_eyard_Receipt_Modes  " & dblReceiptNo & ",'" & Session("Workyear") & "',"
                strSql += "'" & CType(row.FindControl("lblPayAmount"), Label).Text.ToString() & "','" & CType(row.FindControl("lblPaymodeNo"), Label).Text.ToString() & "',"
                strSql += "'" & CType(row.FindControl("lblBankCode"), Label).Text.ToString() & "',"
                strSql += "'" & Convert.ToDateTime(CType(row.FindControl("lblModeDate"), Label).Text.ToString()) & "',"
                strSql += "" & CType(row.FindControl("lblPayID"), Label).Text.ToString() & "," & Val(CType(row.FindControl("lblBankId"), Label).Text.ToString()) & ","
                strSql += "" & Val(CType(row.FindControl("lblBranchId"), Label).Text.ToString()) & ""

                db.sub_ExecuteNonQuery(strSql)
            Next
            btnSave.Text = "Save"
            btnSave.CssClass = "btn btn-success btn-sm outline"
            lblReceiptNo.Text = dblReceiptNo
            lblSaveMessage.Text = "Receipt no.: " & dblReceiptNo & " generated successfully.<br/> Do you wish to print the same?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel4.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub OnbtnPrint()
        Try

            Dim url As String = "../Depo/Report_Epic/EyardReceiptPrintNew.aspx?AssessNo=" & txtAssessment.Text & "&WorkYear=" & txtWorkYear.Text & "&ReceiptNo=" & lblReceiptNo.Text & ""
            ClearForm()
            Dim s As String = "window.open('" & url + "', 'popup_window', '');"
            'ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "script", s, True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub
    Public Sub ClearForm()
        Try
            txtnoc.Text = ""
            txtCHAcode.Text = ""
            txtConsignee.Text = ""
            txtAmount.Text = ""
           
            lblAsseDate.Text = ""
            ddlfill.SelectedValue = 0
            ddltds.SelectedValue = 0
            txtAssessment.Text = ""
            txtmodedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtremarks.Text = ""
            db.sub_ExecuteNonQuery("USP_DeleteFrom_Temp " & Session("UserId_DepoCFS") & "")
            'grdPaymentDetails.DataSource = Nothing
            Call grid()
            grdPaymentDetails.DataBind()
            getLastReceiptNo()
            UpdatePanel2.Update()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub
    Public Function AmountInWords(ByVal nAmount As String, Optional ByVal wAmount _
                 As String = vbNullString, Optional ByVal nSet As Object = Nothing) As String
        'Let's make sure entered value is numeric
        If Not IsNumeric(nAmount) Then Return "Please enter numeric values only."

        Dim tempDecValue As String = String.Empty : If InStr(nAmount, ".") Then _
            tempDecValue = nAmount.Substring(nAmount.IndexOf("."))
        nAmount = Replace(nAmount, tempDecValue, String.Empty)

        Try
            Dim intAmount As Long = nAmount
            If intAmount > 0 Then
                nSet = IIf((intAmount.ToString.Trim.Length / 3) _
                    > (CLng(intAmount.ToString.Trim.Length / 3)), _
                  CLng(intAmount.ToString.Trim.Length / 3) + 1, _
                    CLng(intAmount.ToString.Trim.Length / 3))
                Dim eAmount As Long = Microsoft.VisualBasic.Left(intAmount.ToString.Trim, _
                  (intAmount.ToString.Trim.Length - ((nSet - 1) * 3)))
                Dim multiplier As Long = 10 ^ (((nSet - 1) * 3))

                Dim Ones() As String = _
                {"", "One", "Two", "Three", _
                  "Four", "Five", _
                  "Six", "Seven", "Eight", "Nine"}
                Dim Teens() As String = {"", _
                "Eleven", "Twelve", "Thirteen", _
                  "Fourteen", "Fifteen", _
                  "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
                Dim Tens() As String = {"", "Ten", _
                "Twenty", "Thirty", _
                  "Forty", "Fifty", "Sixty", _
                  "Seventy", "Eighty", "Ninety"}
                Dim HMBT() As String = {"", "", _
                "Thousand", "Million", _
                  "Billion", "Trillion", _
                  "Quadrillion", "Quintillion"}

                intAmount = eAmount

                Dim nHundred As Integer = intAmount \ 100 : intAmount = intAmount Mod 100
                Dim nTen As Integer = intAmount \ 10 : intAmount = intAmount Mod 10
                Dim nOne As Integer = intAmount \ 1

                If nHundred > 0 Then wAmount = wAmount & _
                Ones(nHundred) & " Hundred " 'This is for hundreds                
                If nTen > 0 Then 'This is for tens and teens
                    If nTen = 1 And nOne > 0 Then 'This is for teens 
                        wAmount = wAmount & Teens(nOne) & " "
                    Else 'This is for tens, 10 to 90
                        wAmount = wAmount & Tens(nTen) & IIf(nOne > 0, "-", " ")
                        If nOne > 0 Then wAmount = wAmount & Ones(nOne) & " "
                    End If
                Else 'This is for ones, 1 to 9
                    If nOne > 0 Then wAmount = wAmount & Ones(nOne) & " "
                End If
                wAmount = wAmount & HMBT(nSet) & " "
                wAmount = AmountInWords(CStr(CLng(nAmount) - _
                  (eAmount * multiplier)).Trim & tempDecValue, wAmount, nSet - 1)
            Else
                If Val(nAmount) = 0 Then nAmount = nAmount & _
                tempDecValue : tempDecValue = String.Empty
                If (Math.Round(Val(nAmount), 2) * 100) > 0 Then wAmount = _
                  Trim(AmountInWords(CStr(Math.Round(Val(nAmount), 2) * 100), _
                  wAmount.Trim, 1)) & " Cents"

                ' wAmount.Trim & " Pesos And ", 1)) & " Cents"
            End If
        Catch ex As Exception
            'MessageBox.Show("Error Encountered: " & ex.Message, _
            '  "Convert Numbers To Words", _
            '  MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Return "!#ERROR_ENCOUNTERED"
        End Try

        'Trap null values
        If IsNothing(wAmount) = True Then wAmount = String.Empty Else wAmount = _
          IIf(InStr(wAmount.Trim.ToLower, ""), _
          wAmount.Trim, wAmount.Trim & " ")

        'Display the result
        Return wAmount
    End Function
End Class

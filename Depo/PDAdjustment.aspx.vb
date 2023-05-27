Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Partial Class Bond_PDAdjustment
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
        'txtmodeAmount.BorderColor = System.Drawing.Color.Gainsboro
        'txtModeNo.BorderColor = System.Drawing.Color.Gainsboro
        'ddlbank.BorderColor = System.Drawing.Color.Gainsboro
        'txtbankcod.BorderColor = System.Drawing.Color.Gainsboro
        'ddlBranch.BorderColor = System.Drawing.Color.Gainsboro
        'txtmodedate.BorderColor = System.Drawing.Color.Gainsboro
        txtAmount.BorderColor = System.Drawing.Color.Gainsboro

        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete from temp_pdadjustments Where UserID=" & Session("UserId_DepoCFS") & "")
            FillGrid()
            Filldropdown()
            getLastReceiptNo()
            'db.sub_ExecuteNonQuery("USP_DeleteFrom_Temp " & Session("UserId_DepoCFS") & "")
            txtReceiptdate.Text = Convert.ToDateTime(Now).ToString("dd-MM-yyyy")
            'txtmodedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtWorkYear.Text = Session("Workyear")

            FillTodayCllection()
            txtAssessment.Focus()
        End If

    End Sub
    Public Sub FillTodayCllection()
        Try
            ds = db.sub_GetDataSets("USP_CurrentDate_Collection_pd_adjustment")
            rptAmount.DataSource = ds.Tables(0)
            rptAmount.DataBind()           
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub getLastReceiptNo()
        dt = db.sub_GetDatatable("USP_get_Last_Receipt_NO")
        txtlastreceipt.Text = dt.Rows(0)("ReceiptNo")
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("USP_PDAdjustments_Fill_Dropdown")

            ddltds.DataSource = ds.Tables(0)
            ddltds.DataTextField = "Percentage"
            ddltds.DataValueField = "TDS_PER_ID"
            ddltds.DataBind()
            ddltds.Items.Insert(0, New ListItem("--Select--", 0))

            ddlpdaccounts.DataSource = ds.Tables(1)
            ddlpdaccounts.DataTextField = "PDCode"
            ddlpdaccounts.DataValueField = "entryid"
            ddlpdaccounts.DataBind()
            ddlpdaccounts.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            ClearForm()
            Dim dblTotalReceived As Double = 0
            strSql = ""
            strSql += "USP_PDAdjustment_fetch_details_assessno '" & Trim(txtAssessment.Text & "") & "','" & Trim(txtWorkYear.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(3).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Adjustment already done against this assess no');", True)
                txtAssessment.Text = ""
                txtAssessment.Focus()
                Exit Sub
            End If
            If Not ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No assessment found.');", True)
                txtAssessment.Text = ""
                txtAssessment.Focus()
                Exit Sub
            End If

            If ds.Tables(0).Rows.Count > 0 Then
                lblvaliduptoDate.Text = Trim(ds.Tables(0).Rows(0)("ValidUptoDate") & "")
                lblLineName.Text = Trim(ds.Tables(0).Rows(0)("SlName") & "")

                txtAmount.Text = Trim(ds.Tables(0).Rows(0)("GrandTotal") & "")
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                dblTotalReceived = Trim(ds.Tables(1).Rows(0)(0) & "")
            End If
            If ds.Tables(2).Rows.Count > 0 Then
                dblTotalReceived += Trim(ds.Tables(2).Rows(0)(0) & "")
            End If
            'txtbalanceamount.Text = Val(txtAmount.Text) - dblTotalReceived
            txtadjustamount.Text = Val(txtAmount.Text)
            ddltds.Focus()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim dblAdjustment As Double = 0
            strSql = ""
            strSql += "USP_PDAdjustment_add_button '" & Trim(ddlpdaccounts.SelectedItem.Text) & "'"
            ds = db.sub_GetDataSets(strSql)
            dblAdjustment = Val(txtAmount.Text)
            strSql = ""
            strSql += "USP_PDAdjustments_insert_into_temp_pd '" & txtbalanceamount.Text & "','" & txtadjustamount.Text & "','" & Session("UserId_DepoCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            FillGrid()
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
            'lblAmount.Text = ds.Tables(1).Rows(0)(0)
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
            lblSession.Text = "Record Cancelled Successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddltds_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
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
                strSql = "USP_PDAdjustments_tds_change  " & Trim(txtAssessment.Text) & ",'" & Trim(txtWorkYear.Text) & "' "
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    IntGrntAmt = Val(dt.Rows(0)("NetAmount"))
                    IntotherAmt = Val(txtAmount.Text) - IntGrntAmt
                End If
                If Trim(txtAmount.Text & "") <> "" And Trim(ddltds.SelectedValue) <> 1 Then
                    If Trim(ddltds.SelectedValue) = 2 Then
                        intITdsAmt = IntGrntAmt * 10 / 100
                        IntIITDSAmt = IntotherAmt * 2 / 100
                        txttdsamount.Text = Math.Round(intITdsAmt + IntIITDSAmt)
                        'txtadjustamount.Text = ""
                        'txtadjustamount.Text = Val(txtAmount.Text) - Val(txttdsamount.Text)
                        btnAdd.Focus()
                    Else
                        txttdsamount.Text = ""
                        txtadjustamount.Text = Val(txtAmount.Text)
                        btnAdd.Focus()
                    End If
                End If
                If txtAmount.Text <> "" And Trim(ddltds.SelectedValue) <> 2 Then
                    If Trim(ddltds.SelectedValue) = 1 Then
                        txttdsamount.Text = Math.Round(Val(txtAmount.Text) * 2 / 100)
                        'txtadjustamount.Text = ""
                        'txtadjustamount.Text = Val(txtAmount.Text) - Val(txttdsamount.Text)
                        btnAdd.Focus()
                    Else
                        txttdsamount.Text = ""
                        txtadjustamount.Text = Val(txtAmount.Text)
                        btnAdd.Focus()
                    End If
                End If
                If txtAmount.Text <> "" And Trim(ddltds.SelectedValue) <> 2 And Trim(ddltds.SelectedValue) <> 1 Then
                    If Trim(ddltds.SelectedValue) = 3 Then
                        txttdsamount.Text = Math.Round(Val(txtAmount.Text) * 10 / 100)
                        'txtadjustamount.Text = ""
                        'txtadjustamount.Text = Val(txtAmount.Text) - Val(txttdsamount.Text)
                        btnAdd.Focus()
                    Else
                        txttdsamount.Text = ""
                        txtadjustamount.Text = Val(txtAmount.Text)
                        btnAdd.Focus()
                    End If
                End If
                UpdatePanel2.Update()
            End If
            ddltds.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim year As Integer = Convert.ToDateTime(Trim(txtReceiptdate.Text & "")).ToString("yy")

            strSql = ""
            strSql += "USP_PDAdjustment_insert_into_bond_pdadjustment '" & Convert.ToDateTime(Trim(txtReceiptdate.Text & "")).ToString("yyyy-MM-dd") & "',"
            strSql += "'" & Trim(txtAssessment.Text) & "','" & txtWorkYear.Text & "','" & Trim(txtadjustamount.Text & "") & "',"
            strSql += "'" & Trim(ddlpdaccounts.SelectedItem.Text) & "','" & Trim(txttdsamount.Text) & "','" & Replace(Trim(txtremarks.Text), "'", "''") & "',"
            strSql += "'" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)

            btnAdd.Text = "Save"
            btnAdd.CssClass = "btn btn-success btn-sm outline"
            txttransNo.Text = Trim(dt.Rows(0)(0))
            ClearForm()
            lblSaveMessage.Text = "Record saved successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel4.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub OnbtnPrint()
        Try
            Dim url As String = "../Depo/Report_Eoic/EyardReceiptPrintNew.aspx?AssessNo=" & txtAssessment.Text & "&WorkYear=" & txtWorkYear.Text & ""
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
            lblvaliduptoDate.Text = ""
            lblLineName.Text = ""

            txtAmount.Text = ""
            ddltds.SelectedValue = 0

            txttdsamount.Text = ""
            txtadjustamount.Text = ""
            ddlpdaccounts.SelectedValue = 0
            txtbalanceamount.Text = ""            
            txtremarks.Text = ""            
            getLastReceiptNo()
            FillTodayCllection()
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
    Public Sub FillGrid()
        Try
            strSql = ""
            strSql += "select * from temp_pdadjustments where userid='" & Session("UserId_DepoCFS") & "' and iscancel=0"
            dt = db.sub_GetDatatable(strSql)
            grdPaymentDetails.DataSource = dt
            grdPaymentDetails.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlpdaccounts_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "sp_GetOpenBal_importNew '" & Convert.ToDateTime(Now.AddDays(1)).ToString("dd-MMM-yyyy") & "','" & Trim(ddlpdaccounts.SelectedItem.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            txtbalanceamount.Text = Trim(dt.Rows(0)(0))
            UpdatePanel2.Update()
            ddlpdaccounts.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnok_ServerClick(sender As Object, e As EventArgs)
        Try
            lblSession.Text = "Do you wish to print the adjustment?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
            txtAssessment.Text = ""
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class
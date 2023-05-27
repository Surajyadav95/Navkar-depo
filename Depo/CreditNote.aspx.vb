Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt9 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim dblGroup1Amt As Double

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
            db.sub_ExecuteNonQuery("Delete from temp_credit_note Where UserID=" & Session("UserId_DepoCFS") & "")
            Dim strWorkyear As String = ""
            If Now.Month < 4 Then
                strWorkyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
            ElseIf Now.Month >= 4 Then
                strWorkyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
            End If
            txtworkyear.Text = strWorkyear
            txtcreditnotedate.Text = Convert.ToDateTime(Now).ToString("dd-MM-yyyy")
            FillGrid()
            txtinvno.Focus()
        End If

    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim strWorkyear As String = ""
            If Now.Month < 4 Then
                strWorkyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
            ElseIf Now.Month >= 4 Then
                strWorkyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
            End If
            Dim dblSumSGSTAmt As Double = 0, dblSumNetAmtTotal As Double = 0, dblSumCGSTAmt As Double = 0, dblSumIGSTAmt As Double = 0, dblgrandtotal As Double = 0
            Dim Count As Double = 0, dblassessno As Double = 0
            strSql = ""
            strSql += "Select * from temp_credit_note where UserID='" & Session("UserId_DepoCFS") & "' and IsCancel=0"
            dt1 = db.sub_GetDatatable(strSql)
            If Not dt1.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No charges applied.');", True)
                Exit Sub
            End If
            For Each row In grdcharges.Rows
                If CType(row.FindControl("txtcreditamt"), TextBox).Text = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter credit amount');", True)
                    CType(row.FindControl("txtcreditamt"), TextBox).Focus()
                    Exit Sub
                End If
            Next
            For Each row In grdcharges.Rows
                Dim Creditamt1 As Double = 0
                Dim netamt As Double = Val(CType(row.FindControl("lblntamnt"), Label).Text & "")
                Dim creditamt As Double = Val(CType(row.FindControl("txtcreditamt"), TextBox).Text & "")
                Dim accntid As Double = Val(CType(row.FindControl("lblaccntid"), Label).Text & "")
                strSql = ""
                strSql += "select ISNULL(creditamount,0) as CreditAmt from temp_credit_note where accountid='" & accntid & "' and UserId='" & Session("UserId_DepoCFS") & "' and IsCancel=0"
                dt = db.sub_GetDatatable(strSql)
                Creditamt1 = Val(dt.Rows(0)("CreditAmt"))
                If creditamt + Creditamt1 > netamt Then
                    ScriptManager.RegisterStartupScript(Me.Page(), Page.GetType, "Key", "alert('Credit amount should not be greater than net amount.');", True)
                    CType(row.FindControl("txtcreditamt"), TextBox).Text = ""
                    CType(row.FindControl("txtcreditamt"), TextBox).Focus()
                    Exit Sub
                End If
            Next
            If Trim(txtcreditamount.Text & "") = "0" Or Trim(txtcreditamount.Text & "") = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid credit amount');", True)
                grdcharges.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql += "SELECT isnull(MAX(CreditNoteNo),0) FROM CreditNoteM WITH(XLOCK) WHERE WorkYear='" & strWorkyear & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows(0)(0) = 0 Then
                dblassessno = Convert.ToDateTime(txtcreditnotedate.Text).ToString("yy") & Strings.Right("000000" & 1, 6)
            Else
                dblassessno = Val(dt.Rows(0)(0)) + 1
            End If
            Call Sub_SGTRate()
            For Each row In grdcharges.Rows
                strSql = ""
                strSql += "USP_insert_into_creditnoted '" & dblassessno & "','" & Trim(strWorkyear & "") & "','" & Val(CType(row.FindControl("lblaccntid"), Label).Text) & "',"
                strSql += "'" & Val(CType(row.FindControl("lblntamnt"), Label).Text) & "','" & Val(CType(row.FindControl("txtcreditamt"), TextBox).Text) & "',"
                strSql += "'" & Format(Val(CType(row.FindControl("txtcreditamt"), TextBox).Text) * (dblSGST / 100), "0.00") & "','" & Format(Val(CType(row.FindControl("txtcreditamt"), TextBox).Text) * (dblCGST / 100), "0.00") & "',"
                strSql += "'" & Format(Val(CType(row.FindControl("txtcreditamt"), TextBox).Text) * (dblIGST / 100), "0.00") & "','" & dbltaxgroupid & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next
            strSql = ""
            strSql += "get_sum_charges_creditnote '" & dblassessno & "','" & strWorkyear & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                dblSumSGSTAmt = Val(dt.Rows(0)("SGST"))
                dblSumCGSTAmt = Val(dt.Rows(0)("CGST"))
                dblSumIGSTAmt = Val(dt.Rows(0)("IGST"))
                dblSumNetAmtTotal = Val(dt.Rows(0)("Amount"))
                dblgrandtotal = Val(dblSumSGSTAmt) + Val(dblSumCGSTAmt) + Val(dblSumIGSTAmt) + Val(dblSumNetAmtTotal)
            End If
            strSql = ""
            strSql += "USP_insert_into_creditnotem '" & dblassessno & "','" & strWorkyear & "','" & Convert.ToDateTime(txtcreditnotedate.Text).ToString("yyyy-MM-dd") & "','" & Trim(txtinvno.Text) & "',"
            strSql += "'" & Trim(txtworkyear.Text) & "','" & Trim(lblpartyid.Text) & "','" & Trim(txtgrandtotal.Text) & "','" & Trim(txtcredittotal.Text) & "','" & dblSumSGSTAmt & "','" & dblSumCGSTAmt & "',"
            strSql += "'" & dblSumIGSTAmt & "','" & dblgrandtotal & "','" & Replace(Trim(txtremarks.Text & ""), "'", "''") & "','" & Session("UserId_DepoCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            txtcreditno.Text = dblassessno
            txtcredityear.Text = strWorkyear
            Clear()
            txtinvno.Text = ""
            txtworkyear.Text = strWorkyear
            txtinvno.Focus()
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            lblSession.Text = "Record saved successfully for Credit Note " & dblassessno & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtinvno_TextChanged(sender As Object, e As EventArgs) Handles txtinvno.TextChanged
        Try
            Clear()          
            strSql = ""
            strSql += "USP_fetch_details_for_credit_note '" & Trim(txtinvno.Text & "") & "','" & Trim(txtworkyear.Text & "") & "','" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                'If Not Trim(dt.Rows(0)("CreditNoteNo") & "") = 0 Then
                '    lblsession1.Text = "Credit Note No: " & Trim(dt.Rows(0)("CreditNoteNo") & "") & " already generated for this assessment.Are you want to proceed further?"
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
                '    'UpdatePanel2.Update()
                '    Exit Sub
                'End If
                txtgstname.Text = Trim(dt.Rows(0)("GSTName") & "")
                txtaddress.Text = Trim(dt.Rows(0)("GSTAddress") & "")
                lblstatecode.Text = Trim(dt.Rows(0)("state_Code") & "")
                txtnettotal.Text = Trim(dt.Rows(0)("NetTotal") & "")
                txtsgst.Text = Val(dt.Rows(0)("SGST"))
                txtcgst.Text = Val(dt.Rows(0)("CGST"))
                txtigst.Text = Val(dt.Rows(0)("IGST"))
                txtgrandtotal.Text = Val(dt.Rows(0)("GrandTotal"))
                lblpartyid.Text = Trim(dt.Rows(0)("PartyID") & "")
            Else
                ScriptManager.RegisterStartupScript(Me.Page(), Page.GetType, "key", "alert('no records found');", True)
                txtinvno.Text = ""
                Exit Sub
            End If
            FillGrid()
            grdcharges.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub Clear()
        Try
            txtcreditnoteNo.Text = ""
            txtgstname.Text = ""
            txtaddress.Text = ""
            txtnettotal.Text = ""
            txtsgst.Text = ""
            txtcgst.Text = ""
            txtigst.Text = ""
            txtgrandtotal.Text = ""
            txtcreditamount.Text = ""
            txtsgstcredit.Text = ""
            txtcgstcredit.Text = ""
            txtcredittotal.Text = ""
            txtremarks.Text = ""
            lblstatecode.Text = ""
            db.sub_ExecuteNonQuery("Delete from temp_credit_note Where UserID=" & Session("UserId_DepoCFS") & "")                        
            FillGrid()
            txtinvno.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub FillGrid()
        Try
            strSql = ""
            strSql += "USP_fetch_from_temp_credit_note '" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdcharges.DataSource = dt
            grdcharges.DataBind()           
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
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
    Private Sub sub_CalcTotals()
        Try
            Dim dbltotal As Double = 0, dblvalSGST As Double = 0, dbltotalsgst As Double = 0, dbltotalcgst As Double = 0, dbltotaligst As Double = 0, dblvalCGST As Double = 0, dblvalIGST As Double = 0, dbldisc As Double = 0, dblalltotal As Double = 0
            For Each row In grdcharges.Rows
                Dim CreditAmt As Double = Val(CType(row.FindControl("txtcreditamt"), TextBox).Text & "")
                dblGroup1Amt += CreditAmt
            Next
            dbltotal = dblGroup1Amt
            dbltotalcgst = Format(dblGroup1Amt * (dblSGST / 100), "0.00")
            dbltotalsgst = Format(dblGroup1Amt * (dblCGST / 100), "0.00")
            dbltotaligst = Format(dblGroup1Amt * (dblIGST / 100), "0.00")
            strSql = ""
            strSql += "select round(" & dbltotalcgst & ",0) as totalcgst,round(" & dbltotalsgst & ",0) as totalsgst,round(" & dbltotaligst & ",0) as totaligst"
            dt = db.sub_GetDatatable(strSql)
            dblalltotal = dbltotal - dbldisc + Val(dt.Rows(0)("totalsgst")) + Val(dt.Rows(0)("totalcgst")) + Val(dt.Rows(0)("totaligst"))
            txtcreditamount.Text = dbltotal
            txtsgstcredit.Text = Val(dt.Rows(0)("totalsgst"))
            txtcgstcredit.Text = Val(dt.Rows(0)("totalcgst"))
            txtigstcredit.Text = Val(dt.Rows(0)("totaligst"))
            txtcredittotal.Text = dblalltotal
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtcreditamt_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim grdcharges As GridViewRow = CType(CType(sender, TextBox).NamingContainer, GridViewRow)
            Dim netamt As Double = Val(CType(grdcharges.FindControl("lblntamnt"), Label).Text & "")
            Dim creditamt As Double = Val(CType(grdcharges.FindControl("txtcreditamt"), TextBox).Text & "")
            Dim accntid As Double = Val(CType(grdcharges.FindControl("lblaccntid"), Label).Text & "")
            
            Dim Creditamt1 As Double = 0
            strSql = ""
            strSql += "select ISNULL(creditamount,0) as CreditAmt from temp_credit_note where accountid='" & accntid & "' and UserId='" & Session("UserId_DepoCFS") & "' and IsCancel=0"
            dt = db.sub_GetDatatable(strSql)
            Creditamt1 = Val(dt.Rows(0)("CreditAmt"))
            If creditamt + Creditamt1 > netamt Then
                ScriptManager.RegisterStartupScript(Me.Page(), Page.GetType, "Key", "alert('Credit amount should not be greater than net amount.');", True)
                CType(grdcharges.FindControl("txtcreditamt"), TextBox).Text = ""
                CType(grdcharges.FindControl("txtcreditamt"), TextBox).Focus()
                Exit Sub
            End If
            Sub_SGTRate()
            sub_CalcTotals()
            CType(grdcharges.FindControl("txtcreditamt"), TextBox).Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Button1_ServerClick(sender As Object, e As EventArgs)
        Clear()
        strSql = ""
        strSql += "USP_fetch_details_for_credit_note '" & Trim(txtinvno.Text & "") & "','" & Trim(txtworkyear.Text & "") & "','" & Session("UserId_DepoCFS") & "'"
        dt = db.sub_GetDatatable(strSql)
        If dt.Rows.Count > 0 Then
            txtgstname.Text = Trim(dt.Rows(0)("GSTName") & "")
            txtaddress.Text = Trim(dt.Rows(0)("GSTAddress") & "")
            lblstatecode.Text = Trim(dt.Rows(0)("state_Code") & "")
            txtnettotal.Text = Trim(dt.Rows(0)("NetTotal") & "")
            txtsgst.Text = Val(dt.Rows(0)("SGST"))
            txtcgst.Text = Val(dt.Rows(0)("CGST"))
            txtigst.Text = Val(dt.Rows(0)("IGST"))
            txtgrandtotal.Text = Val(dt.Rows(0)("GrandTotal"))
            lblpartyid.Text = Trim(dt.Rows(0)("PartyID") & "")
        Else
            ScriptManager.RegisterStartupScript(Me.Page(), Page.GetType, "Key", "alert('No records found');", True)
            txtinvno.Text = ""
            Exit Sub
        End If
        FillGrid()
        grdcharges.Focus()
    End Sub

    Protected Sub Button3_ServerClick(sender As Object, e As EventArgs)
        Try
            Label1.Text = "Do you wish to print Credit Note?"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            UpdatePanel4.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

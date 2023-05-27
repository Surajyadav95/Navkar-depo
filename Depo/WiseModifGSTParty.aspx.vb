Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt9 As DataTable
    Dim db As New dbOperation_Depo
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim ds As DataSet
    Dim TariffID, TariffIDView As String
    Dim strCategory As String
    Dim strCategoryDetails As String
    Dim strCategorySPName As String
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            txtdateofModi.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtWorkYear.Text = Session("Workyear")
            'txtAssessmentDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            Filldropdown()
        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("USP_PARTY_SEARCH_EMPTY_YARD")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlpartyName.DataSource = ds.Tables(0)
                ddlpartyName.DataTextField = "GSTName"
                ddlpartyName.DataValueField = "GSTID"
                ddlpartyName.DataBind()
                ddlpartyName.Items.Insert(0, New ListItem("--Select--", 0))
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function

    'Protected Sub ddlpartyName_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    Try
    '        strSql = ""
    '        strSql = "usp_modify_fill '" & Trim(ddlpartyName.SelectedItem.Text & "") & "','" & Session("UserID") & "'"
    '        dt = db.sub_GetDatatable(strSql)
    '        If dt.Rows.Count > 0 Then
    '            lblgst.Text = Trim(dt.Rows(0)("GSTIn_uniqID") & "")
    '            txGSTAddress.Text = Trim(dt.Rows(0)("GSTAddress") & "")
    '            lblstatecode.Text = Trim(dt.Rows(0)("state_Code") & "")
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    End Try
    'End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "usp_search_modify'" & Trim(txtAssessment.Text & "") & "','" & Session("Workyear") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtGstIn.Text = Trim(dt.Rows(0)("GSTIn_uniqID") & "")
                ddlpartyName.SelectedValue = Trim(dt.Rows(0)("PartyId") & "")
                txtPartyName.Text = Trim(dt.Rows(0)("GSTName") & "")
                txGSTAddress.Text = Trim(dt.Rows(0)("GSTAddress") & "")
                'txtoldpartyid.Text = Trim(dt.Rows(0)("PartyId") & "")
                txtAssessmentDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Assess Date") & "")).ToString("dd-MM-yyyy")
                'If Val(dt.Rows(0)("gst")) = 0 Then
                '    chkgstapplicable.Checked = False
                'Else
                '    chkgstapplicable.Checked = True
                'End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dblmaxcrno As Double
            Dim dblSGST1 As Double = 0
            Dim dblIGST1 As Double = 0
            Dim dblCGST1 As Double = 0
            Dim strSQL As String
            Dim dbldiscamt As Double = 0
            Sub_SGTRate()
            If chkgstapplicable.Checked = False Then
                strSGSTPer = "SGST" & " @ " & 0 & "%"
                StrCGSTPEr = "CGST" & " @ " & 0 & "%"
                StrIGSTPer = "IGST" & " @ " & 0 & "%"
                If Not dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified invoice is without GST. Are you sure to update GST Party?');", True)
                    Exit Sub
                End If
            End If
            strCategory = ""
            strCategoryDetails = ""
            strCategorySPName = ""
            If ddlCategory.SelectedValue = "Empty Yard" Then
                strCategory = "EYARD_ASSESSM"
                strCategoryDetails = "EYARD_ASSESSD"
                strCategorySPName = "get_sum_charges_EmptyYard_New"
            End If
            Dim dtdays As New DataTable
            Dim intdays As Integer

            strSQL = "get_Sp_nettotal_new '" & strCategory & "','" & Trim(txtAssessment.Text) & "','" & Trim(txtWorkYear.Text) & "'"
            dt = db.sub_GetDatatable(strSQL)
            If dt.Rows.Count > 0 Then
                If Not (Val(ddlpartyName.SelectedValue)) = 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        dblCGST1 = (Val(dt.Rows(i)("Net Amount")) - Val(dt.Rows(i)("Discount Amount"))) * dblCGST / 100
                        dblSGST1 = (Val(dt.Rows(i)("Net Amount")) - Val(dt.Rows(i)("Discount Amount"))) * dblSGST / 100
                        dblIGST1 = (Val(dt.Rows(i)("Net Amount")) - Val(dt.Rows(i)("Discount Amount"))) * dblIGST / 100

                        dblCGST1 = Format(dblCGST1, "0.00")
                        dblIGST1 = Format(dblIGST1, "0.00")
                        dblSGST1 = Format(dblSGST1, "0.00")

                        strSQL = "exec  get_Sp_UpdateTaxAssessD '" & Trim(dt.Rows(i)("Name") & "") & "','" & Trim(txtAssessment.Text) & "',"
                        strSQL += " '" & Trim(txtWorkYear.Text) & "','" & Trim(dt.Rows(i)("First") & "") & "','" & Trim(dt.Rows(i)("Second") & "") & "',"
                        strSQL += " '" & Trim(dt.Rows(i)("Account ID") & "") & "','" & dblCGST1 & "','" & dblSGST1 & "','" & dblIGST1 & "'"
                        db.sub_ExecuteNonQuery(strSQL)
                    Next
                    'Else
                    '    For i As Integer = 0 To dt.Rows.Count - 1
                    '        dblIGST = (Val(dt.Rows(i)("Net Amount")) - Val(dt.Rows(i)("Discount Amount"))) * dblIGST / 100
                    '        dblCGST = Format(dblCGST, "0.00")
                    '        dblIGST = Format(dblIGST, "0.00")
                    '        dblSGST = Format(dblSGST, "0.00")

                    '        strSQL = "exec  get_Sp_UpdateTaxAssessD '" & Trim(dt.Rows(i)("Name") & "") & "','" & Trim(txtAssessment.Text) & "',"
                    '        strSQL += " '" & Trim(txtWorkYear.Text) & "','" & Trim(dt.Rows(i)("First") & "") & "','" & Trim(dt.Rows(i)("Second") & "") & "',"
                    '        strSQL += " '" & Trim(dt.Rows(i)("Account ID") & "") & "','" & dblCGST & "','" & dblSGST & "','" & dblIGST & "'"
                    '        dt = db.sub_GetDatatable(strSQL)
                    '    Next
                End If
            End If

            Dim dtdiscamt As New DataTable
            strSQL = ""
            strSQL = "select * from " & strCategory & " where InvoiceNo='" & Trim(txtAssessment.Text) & "' and workyear='" & Trim(txtWorkYear.Text) & "'"
            dtdiscamt = db.sub_GetDatatable(strSQL)
            If dtdiscamt.Rows.Count > 0 Then
                dbldiscamt = Val(dtdiscamt.Rows(0)("NetTotal1") & "")
            End If

            Dim dtfill As New DataTable
            strSQL = ""
            strSQL = "exec " & strCategorySPName & " '" & Trim(txtAssessment.Text & "") & "','" & Trim(Trim(txtWorkYear.Text) & "") & "'"
            dtfill = db.sub_GetDatatable(strSQL)
            If dtfill.Rows.Count > 0 Then
                strSQL = ""
                strSQL = "update " & strCategory & "  set  Grandtotal=" & Val(Val(dtfill.Rows(0)("SGST")) + Val(dtfill.Rows(0)("CGST")) + Val(dtfill.Rows(0)("IGST")) + Val(dtfill.Rows(0)("Amount")) - Val(dbldiscamt)) & ",  PartyId='" & Trim(lblgst.Text & "") & "' ,SGST=" & Val(dtfill.Rows(0)("SGST") & "") & " ,CGST=" & Val(dtfill.Rows(0)("CGST") & "") & " ,IGST=" & Val(dtfill.Rows(0)("IGST") & "") & " "
                strSQL += " where InvoiceNo ='" & Trim(txtAssessment.Text) & "' AND WorkYear = '" & Trim(txtWorkYear.Text) & "'"
                dt = db.sub_GetDatatable(strSQL)
            End If
            Dim strQuery As String
            strQuery = ""
            If chkgstapplicable.Checked = False Then
                strQuery = "Exec USP_Inser_Modify_GST_Inv '" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd") & "','" & Trim(ddlCategory.SelectedValue) & "', '" & Trim(txtAssessment.Text) & "','" & Trim(txtWorkYear.Text) & "','" & Trim(txtGstIn.Text) & "', '" & Trim(ddlpartyName.SelectedValue) & "', NULL, " & Session("UserId_DepoCFS") & " "
            Else
                strQuery = "Exec USP_Inser_Modify_GST_Inv '" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd") & "','" & Trim(ddlCategory.SelectedValue) & "', '" & Trim(txtAssessment.Text) & "','" & Trim(txtWorkYear.Text) & "','" & Trim(txtGstIn.Text) & "', '" & Trim(ddlpartyName.SelectedValue) & "', '" & Convert.ToDateTime(Trim(txtAssessmentDate.Text & "")).ToString("yyyy-MM-dd ") & "', " & Session("UserId_DepoCFS") & " "
            End If
            db.sub_ExecuteNonQuery(strSQL)
            lblSession.Text = "Record updated successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
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
    

    Protected Sub btnIndentItem_Click1(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "usp_modify_fill_Party '" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtPartyName.Text = Trim(dt.Rows(0)("PARTY_NAME") & "")
                lblgst.Text = Trim(dt.Rows(0)("GSTID") & "")
                txGSTAddress.Text = Trim(dt.Rows(0)("GSTAddress") & "")
                lblstatecode.Text = Trim(dt.Rows(0)("state_Code") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

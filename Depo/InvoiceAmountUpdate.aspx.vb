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
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim dblGroup1Amt As Double
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserID") Is Nothing Then
        '    Session("UserID") = Request.Cookies("UserIDPRE_Bond").Value
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
            Filldropdown()
            txtinvdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtworkyear.Text = "2020-21"
        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("USP_FILL_DROPDOWN_TARIFF_SETTINGS")
             
            ddlAccount.DataSource = ds.Tables(1)
            ddlAccount.DataTextField = "AccountName"
            ddlAccount.DataValueField = "AccountID"
            ddlAccount.DataBind()
            ddlAccount.Items.Insert(0, New ListItem("--Select--", 0))

            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Trim(txtcontainerno.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No cannot be left blank');", True)
                txtcontainerno.Focus()
                Exit Sub
            End If
            lblquoteApprove.Text = "Are you sure to update ?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btncancelyes_ServerClick(sender As Object, e As EventArgs)
        Try
            Dim dblSumSGSTAmt As Double = 0, dblSumNetAmtTotal As Double = 0, dblSumCGSTAmt As Double = 0, dblSumIGSTAmt As Double = 0, dblgrandtotal As Double = 0
            Call Sub_SGTRate()
            strSql = ""
            strSql = "usp_update_AssessD_Amount'" & Trim(txtAmount.Text & "") & "','" & Val(dblCGST) & "','" & Val(dblSGST) & "','" & Val(dblIGST) & "','" & Trim(txtInvoiceno.Text & "") & "','" & Trim(txtworkyear.Text & "") & "','" & Trim(txtcontainerno.Text & "") & "','" & Trim(lblentryid.Text & "") & "','" & Trim(ddlAccount.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)

            strSql = ""
            strSql += "get_sum_charges_Eyard_MODIFY '" & Trim(txtInvoiceno.Text & "") & "','" & Trim(txtworkyear.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                dblSumSGSTAmt = Val(dt.Rows(0)("SGST"))
                dblSumCGSTAmt = Val(dt.Rows(0)("CGST"))
                dblSumIGSTAmt = Val(dt.Rows(0)("IGST"))
                dblSumNetAmtTotal = Val(dt.Rows(0)("Amount"))
                dblgrandtotal = Val(dblSumSGSTAmt) + Val(dblSumCGSTAmt) + Val(dblSumIGSTAmt) + Val(dblSumNetAmtTotal)
            End If
            strSql = ""
            strSql += "USP_update_into_Eyard_assessm '" & dblSumSGSTAmt & "','" & dblSumCGSTAmt & "','" & dblSumIGSTAmt & "','" & dblSumNetAmtTotal & "','" & dblgrandtotal & "',"
            strSql += "'" & Trim(txtInvoiceno.Text & "") & "','" & Trim(txtworkyear.Text & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            Call db.AmmendmentLog(" " & Trim(txtInvoiceno.Text & "") & "' Invoice No " & Trim(txtcontainerno.Text) & "Invoice details'" & Trim(lblentryid.Text) & "'in EyardAssessM ", Session("UserId_DepoCFS"))
            lblSession.Text = "Assessment updated successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Sub_SGTRate()
        Try
            Dim compid As String = ""
            strSql = ""
            strSql += "select Tinnumber from settings"
            dt9 = db.sub_GetDatatable(strSql)
            If dt9.Rows.Count > 0 Then
                compid = Trim(dt9.Rows(0)(0))
            End If
            strSql = ""
            strSql += "USP_GST_Casl'" & Val(lbltaxid.Text) & "'"
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
            dbltotal = dblGroup1Amt
            dbltotalcgst = Format(dblGroup1Amt * (dblSGST / 100), "0.00")
            dbltotalsgst = Format(dblGroup1Amt * (dblCGST / 100), "0.00")
            dbltotaligst = Format(dblGroup1Amt * (dblIGST / 100), "0.00")
            strSql = ""
            strSql += "select CEILING(cast(" & dbltotalcgst & " as float)) as totalcgst,CEILING(cast(" & dbltotalsgst & "as float)) as totalsgst,CEILING(cast(" & dbltotaligst & " as float)) as totaligst"
            dt = db.sub_GetDatatable(strSql)
            dblalltotal = dbltotal - dbldisc + Val(dt.Rows(0)("totalsgst")) + Val(dt.Rows(0)("totalcgst")) + Val(dt.Rows(0)("totaligst"))
            lblTotal.Text = dbltotal
            lbldisc.Text = dbldisc
            lblSgstPer.Text = strSGSTPer
            lblCgstPer.Text = StrCGSTPEr
            lblIgstPer.Text = StrIGSTPer
            lblCGST.Text = Val(dt.Rows(0)("totalcgst"))
            lblSGST.Text = Val(dt.Rows(0)("totalsgst"))
            lblIGST.Text = Val(dt.Rows(0)("totaligst"))
            lblAllTotal.Text = dblalltotal
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnshow_Click(sender As Object, e As EventArgs)
        Try
            If Trim(ddlAccount.SelectedValue) = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Account Head cannot be left blank');", True)
                ddlAccount.Focus()
                Exit Sub
            End If

            If Trim(txtInvoiceno.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invoice No cannot be left blank');", True)
                txtInvoiceno.Focus()
                Exit Sub
            End If

            If Trim(txtcontainerno.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No cannot be left blank');", True)
                txtcontainerno.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql = "usp_update_Container_Amount'" & Trim(txtworkyear.Text & "") & "','" & Trim(txtInvoiceno.Text & "") & "','" & Trim(txtcontainerno.Text & "") & "','" & Trim(ddlAccount.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count() > 0) Then
                txtInvoiceno.Text = Trim(dt.Rows(0)("InvoiceNo"))
                lblentryid.Text = Trim(dt.Rows(0)("entryid"))
                lbltaxid.Text = Trim(dt.Rows(0)("Taxgroupid"))
                txtAmount.Text = Trim(dt.Rows(0)("netAmount"))
                txtSize.Text = Trim(dt.Rows(0)("Csize"))
                lblstatecode.Text = Trim(dt.Rows(0)("state_Code"))
                txtinvdate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("AssessDate"))).ToString("yyyy-MM-dd")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

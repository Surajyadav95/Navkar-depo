Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel
'Imports Outlook = Microsoft.Office.Interop.Outlook
Partial Class Bond_BondReceiptSummary
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds, dsReceipt As DataSet
    Dim ed As New clsEncodeDecode
    Dim dtSPID As New DataTable
    Dim dtTempI As New DataTable
    Dim strDesc As String = ""
    Dim strtable As String = ""
    Dim strText As String = ""
    Dim strMailTo As String = ""
    Dim strCcIDs As String = ""
    Dim strToIDs As String = ""
    Dim strSubject As String = ""
    Dim strBodyText As String = ""
    Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing

    Dim streamids As String() = Nothing

    Dim mimeType As String = Nothing

    Dim encoding As String = Nothing

    Dim extension As String = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Convert.ToInt32(Session("UserId_DepoCFS")) = 0 Then
            Response.Redirect("~/Depo/Login.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            txtFromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtToDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            btnShow_Click(sender, e)
        End If
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            strSql = ""
            strSql += "USP_Get_Receipt_Summary_eyard '" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyy-MM-dd 00:00:00") & "','" & Convert.ToDateTime(Trim(txtToDate.Text & "")).ToString("yyyy-MM-dd 23:59:00 ") & "',"
            strSql += "" & ddlSearchList.SelectedValue & ",'" & Trim(txtReceiptNo.Text) & "','" & Trim(txtAssessment.Text) & "','" & Trim(txtNoc.Text) & "','" & Trim(ddlreceipttype.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdBondReceipt.DataSource = dt
            grdBondReceipt.DataBind()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlSearchList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSearchList.SelectedIndexChanged
        Try
            txtReceiptNo.Text = ""
            txtNoc.Text = ""
            txtAssessment.Text = ""
            ddlreceipttype.SelectedValue = 0
            If (ddlSearchList.SelectedValue = 0) Then
                divReceipt.Attributes.Add("Style", "display:none")
                divAssessment.Attributes.Add("Style", "display:none")
                divNoc.Attributes.Add("Style", "display:none")
                divreceipttype.Attributes.Add("Style", "display:none")

            ElseIf (ddlSearchList.SelectedValue = 1) Then
                divReceipt.Attributes.Add("Style", "display:none")
                divAssessment.Attributes.Add("Style", "display:block")
                divNoc.Attributes.Add("Style", "display:none")
                divreceipttype.Attributes.Add("Style", "display:none")

            ElseIf (ddlSearchList.SelectedValue = 2) Then
                divReceipt.Attributes.Add("Style", "display:none")
                divAssessment.Attributes.Add("Style", "display:none")
                divNoc.Attributes.Add("Style", "display:block")
                divreceipttype.Attributes.Add("Style", "display:none")

            ElseIf (ddlSearchList.SelectedValue = 3) Then
                divReceipt.Attributes.Add("Style", "display:block")
                divAssessment.Attributes.Add("Style", "display:none")
                divNoc.Attributes.Add("Style", "display:none")
                divreceipttype.Attributes.Add("Style", "display:none")
            ElseIf (ddlSearchList.SelectedValue = 4) Then
                divReceipt.Attributes.Add("Style", "display:none")
                divAssessment.Attributes.Add("Style", "display:none")
                divNoc.Attributes.Add("Style", "display:none")
                divreceipttype.Attributes.Add("Style", "display:block")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Public Sub OnCancel(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblCancel.Text = ""
            Dim lnkbtn As LinkButton = TryCast(sender, LinkButton)
            Dim gvrow As GridViewRow = TryCast(lnkbtn.NamingContainer, GridViewRow)
            Dim ReceiptNo As String = grdBondReceipt.DataKeys(gvrow.RowIndex).Value.ToString()
            lblCancel.Text = ReceiptNo
            lblModifyTitle.Text = "Enter Cancel reason for Receipt No. " & ReceiptNo & " "
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", True)
            upModalCancel.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            If (txtremarks.Text = "") Then
                ScriptManager.RegisterStartupScript(btnsave, btnsave.GetType, "Key", "alert('Please fill the required fields!');", True)
                txtremarks.BorderColor = System.Drawing.Color.Red
                Exit Sub
            End If

            Dim strAddress As String = ""
            strAddress = Replace(Trim(txtremarks.Text), "'", "''")
            strSql = ""
            strSql += "USP_Cancel_Receipt_eyard " & lblCancel.Text & ",'" & strAddress & "','" & Session("UserId_DepoCFS") & "'"
            db.sub_ExecuteNonQuery(strSql)
            txtremarks.Text = ""
            upModalCancel.Update()
            btnShow_Click(sender, e)
            'lblSession.Text = "Record Cancelled Successfully"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            'UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)

        Me.btnShow_Click(sender, e)
    End Sub

    Protected Sub lnkprint_Click(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            Dim ReceiptNo As String = grdBondReceipt.DataKeys(gr.RowIndex).Value.ToString()
            txtassessno.Text = Trim(CType(gr.FindControl("lblAssessNo"), Label).Text)
            txtworkyear.Text = Trim(CType(gr.FindControl("lblWorkYear"), Label).Text)
            txtReceiptNoforPrint.Text = ReceiptNo
            If Trim(CType(gr.FindControl("lblRcptType"), Label).Text) = "M" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:OpenMultiplePrint();", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:OpenWOPrint();", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkmail_Click(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            Dim ReceiptNo As String = grdBondReceipt.DataKeys(gr.RowIndex).Value.ToString()
            txtassessno.Text = Trim(CType(gr.FindControl("lblAssessNo"), Label).Text)
            txtworkyear.Text = Trim(CType(gr.FindControl("lblWorkYear"), Label).Text)
            If Trim(CType(gr.FindControl("lblRcptType"), Label).Text) = "M" Then
                MultipleMail(ReceiptNo)
            Else
                SingleMail(ReceiptNo)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub SingleMail(ReceiptNo1 As String)
        Try
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/BondReceiptPrintDetails.rdlc")

            Dim assessno As String = txtassessno.Text
            Dim workyear As String = txtworkyear.Text
            Dim ReceiptNo As Integer = ReceiptNo1
            ''assessno = "182"
            ''workyear = "2018-19"
            strSql = ""
            strSql = "USP_get_Payment_Details '" & assessno & "','" & workyear & "'," & ReceiptNo & ""
            dsReceipt = db.sub_GetDataSets(strSql)

            strSql = ""
            strSql += "USP_NOC_Assessment_Print_details '" & assessno & "','" & workyear & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then

                dt = ds.Tables(1)
                Dim Con_name As String = dt.Rows(0)("con_Name")
                Dim addressI As String = dt.Rows(0)("AddressI")
                Dim addressII As String = Trim(dt.Rows(0)("AddressII") & "")
                Dim BankName As String = dt.Rows(0)("BankName")
                Dim AccountNo As String = dt.Rows(0)("AccountNo")
                Dim BranchName As String = dt.Rows(0)("BranchName")
                Dim IFSCCode As String = dt.Rows(0)("IFSCCode")
                Dim GSTINComp As String = dt.Rows(0)("GSTIN")
                Dim CINNo As String = dt.Rows(0)("CINNo")
                Dim PANNo As String = dt.Rows(0)("PANNo")
                Dim ConFor As String = dt.Rows(0)("Con_For")
                Dim NoteI As String = dt.Rows(0)("NoteI")
                Dim NoteII As String = dt.Rows(0)("NoteII")
                Dim Swiftcode As String = dt.Rows(0)("Swiftcode")
                Dim MICROCode As String = dt.Rows(0)("MICROCode")

                dt1 = ds.Tables(0)

                Dim Assessnoprint As String = Trim(dt1.Rows(0)("AssessNo") & "")
                Dim assessdate As String = Trim(dt1.Rows(0)("AssessDate") & "")
                Dim GSTNAME As String = Trim(dt1.Rows(0)("GSTName") & "")
                Dim GSTADD As String = Trim(dt1.Rows(0)("GSTAddress") & "")
                Dim GSTIN As String = Trim(dt1.Rows(0)("GSTIn_uniqID") & "")
                Dim STATE As String = Trim(dt1.Rows(0)("State") & "")
                Dim STATECODE As String = Trim(dt1.Rows(0)("state_Code") & "")
                Dim CHAName As String = Trim(dt1.Rows(0)("CHAName") & "")
                Dim ImporterName As String = Trim(dt1.Rows(0)("ImporterName") & "")
                Dim NOCNo As String = Trim(dt1.Rows(0)("NOCNo") & "")
                Dim ValidUptoDate As String = Trim(dt1.Rows(0)("ValidUptoDate") & "")
                Dim IGMNo As String = Trim(dt1.Rows(0)("IGMNo") & "")
                Dim NOCDate As String = Trim(dt1.Rows(0)("NOCDate") & "")
                Dim BOENo As String = Trim(dt1.Rows(0)("BOENo") & "")
                Dim BOEDate As String = Trim(dt1.Rows(0)("BOEDate") & "")
                Dim Area As String = Trim(dt1.Rows(0)("Area") & "")
                Dim Qty As String = Trim(dt1.Rows(0)("Qty") & "")
                Dim Value As String = Trim(dt1.Rows(0)("Value") & "")
                Dim Duty As String = Trim(dt1.Rows(0)("duty") & "")

                Dim Weight As String = Trim(dt1.Rows(0)("Weight") & "")
                Dim UserName As String = Trim(dt1.Rows(0)("UserName") & "")
                Dim Today As String = Trim(dt1.Rows(0)("Today") & "")
                Dim SGST As String = Val(dt1.Rows(0)("sgst"))
                Dim CGST As String = Val(dt1.Rows(0)("cgst"))
                Dim IGST As String = Val(dt1.Rows(0)("igst"))
                Dim NetTotal As String = dt1.Rows(0)("NetTotal")
                Dim GrandTotal As String = dt1.Rows(0)("GrandTotal")
                Dim amtinword As String = dt1.Rows(0)("amtinword")

                Dim CargoType As String = dt1.Rows(0)("cargotype")
                Dim CargoDescrp As String = dt1.Rows(0)("CargoDescrp")
                Dim T20 As String = dt1.Rows(0)("T20")
                Dim T40 As String = dt1.Rows(0)("T40")
                Dim DeliveryType As String = dt1.Rows(0)("DeliveryType")
                Dim BondNo As String = Trim(dt1.Rows(0)("BondNo") & "")
                Dim BondDate As String = Trim(dt1.Rows(0)("BondDate") & "")
                Dim EXBOENo As String = Trim(dt1.Rows(0)("ExBoeNo") & "")
                Dim EXBOEDate As String = Trim(dt1.Rows(0)("ExBoedate") & "")

                Dim LastAssessNo As String = Trim(dt1.Rows(0)("LastAssessNo") & "")
                Dim LastAssessNo1 As String
                If LastAssessNo = "0" Then
                    LastAssessNo1 = ""
                Else
                    LastAssessNo1 = "Previous Invoice No : " + LastAssessNo
                End If

                Dim NetAmount As String = dsReceipt.Tables(1).Rows(0)("NetAmount")
                Dim TDS As String = dsReceipt.Tables(1).Rows(0)("TDS")
                Dim ReceiptAmount As String = dsReceipt.Tables(1).Rows(0)("ReceiptAmount")
                Dim Inwords As String = dsReceipt.Tables(1).Rows(0)("Inwords")
                Dim Remarks As String = dsReceipt.Tables(1).Rows(0)("Remarks")
                Dim Receipt As String = dsReceipt.Tables(1).Rows(0)("ReceiptNo")
                Dim ReceiptDate As String = dsReceipt.Tables(1).Rows(0)("ReceiptDate")

                dt2 = ds.Tables(2)
                Dim datasource As New ReportDataSource("DataSet1", dt2)
                dt3 = ds.Tables(3)
                Dim datasource1 As New ReportDataSource("DataSet2", dt3)
                Dim datasource2 As New ReportDataSource("BondReceiptModes", dsReceipt.Tables(0))

                ReportViewer1.LocalReport.DataSources.Clear()
                ReportViewer1.LocalReport.DataSources.Add(datasource)
                ReportViewer1.LocalReport.DataSources.Add(datasource1)
                ReportViewer1.LocalReport.DataSources.Add(datasource2)

                Dim p1 As New ReportParameter("Con_Name", Con_name)
                Dim p2 As New ReportParameter("AddressI", addressI)
                Dim p3 As New ReportParameter("AddressII", addressII)
                Dim p4 As New ReportParameter("BankName", BankName)
                Dim p5 As New ReportParameter("AccountNo", AccountNo)
                Dim p6 As New ReportParameter("BranchName", BranchName)
                Dim p7 As New ReportParameter("IFSCCOde", IFSCCode)
                Dim p8 As New ReportParameter("GSTINComp", GSTINComp)
                Dim p9 As New ReportParameter("CINNo", CINNo)
                Dim p10 As New ReportParameter("PANNo", PANNo)

                Dim p11 As New ReportParameter("AssessNo", Assessnoprint)
                Dim p12 As New ReportParameter("AssessDate", assessdate)
                Dim p13 As New ReportParameter("GSTNAME", GSTNAME)
                Dim p14 As New ReportParameter("GST_address", GSTADD)
                Dim p15 As New ReportParameter("GSTIN", GSTIN)
                Dim p16 As New ReportParameter("STATE", STATE)
                Dim p17 As New ReportParameter("STATECODE", STATECODE)
                Dim p18 As New ReportParameter("CHAName", CHAName)
                Dim p19 As New ReportParameter("ImporterName", ImporterName)
                Dim p20 As New ReportParameter("NOCNo", NOCNo)
                Dim p21 As New ReportParameter("ValidUptoDate", ValidUptoDate)
                Dim p22 As New ReportParameter("IGMNo", IGMNo)
                Dim p23 As New ReportParameter("NOCDate", NOCDate)
                Dim p24 As New ReportParameter("BOENo", BOENo)
                Dim p25 As New ReportParameter("Value", Value)
                Dim p26 As New ReportParameter("BOEDate", BOEDate)
                Dim p27 As New ReportParameter("Area", Area)
                Dim p28 As New ReportParameter("Qty", Qty)
                Dim p29 As New ReportParameter("Weight", Weight)
                Dim p30 As New ReportParameter("UserName", UserName)
                Dim p31 As New ReportParameter("Today", Today)
                Dim p32 As New ReportParameter("SGST", SGST)
                Dim p33 As New ReportParameter("CGST", CGST)
                Dim p34 As New ReportParameter("IGST", IGST)
                Dim p35 As New ReportParameter("NetTotal", NetTotal)
                Dim p36 As New ReportParameter("GrandTotal", GrandTotal)
                Dim p37 As New ReportParameter("amtinword", amtinword)
                Dim p38 As New ReportParameter("ConFor", ConFor)

                Dim p39 As New ReportParameter("NoteI", NoteI)
                Dim p40 As New ReportParameter("NoteII", NoteII)
                Dim p41 As New ReportParameter("Duty", Duty)
                Dim p42 As New ReportParameter("CargoType", CargoType)
                Dim p43 As New ReportParameter("CargoDescrp", CargoDescrp)
                Dim p44 As New ReportParameter("T20", T20)
                Dim p45 As New ReportParameter("T40", T40)
                Dim p46 As New ReportParameter("DeliveryType", DeliveryType)
                Dim p47 As New ReportParameter("BondNo", BondNo)
                Dim p48 As New ReportParameter("BondDate", BondDate)
                Dim p49 As New ReportParameter("EXBOENo", EXBOENo)
                Dim p50 As New ReportParameter("EXBOEDate", EXBOEDate)

                Dim p51 As New ReportParameter("Swiftcode", Swiftcode)
                Dim p52 As New ReportParameter("MICROCode", MICROCode)

                Dim p53 As New ReportParameter("LastAssessNo1", LastAssessNo1)

                Dim p54 As New ReportParameter("NetAmount", NetAmount)
                Dim p55 As New ReportParameter("TDS", TDS)
                Dim p56 As New ReportParameter("ReceiptAmount", ReceiptAmount)
                Dim p57 As New ReportParameter("Inwords", Inwords)
                Dim p58 As New ReportParameter("Remarks", Remarks)
                Dim p59 As New ReportParameter("Receipt", Receipt)
                Dim p60 As New ReportParameter("ReceiptDate", ReceiptDate)

                Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15,
                                                                                  p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29,
                                                                                  p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44,
                                                                                  p45, p46, p47, p48, p49, p50, p51, p52, p53,
                                                                                  p54, p55, p56, p57, p58, p59, p60})


                ''''''''''Mail Body'''''''''''''



                'strSql = ""
                'strSql = "USP_getElement '" & lblticketno.Text & "'"
                'dtTempI = db.sub_GetDatatable(strSql)
                'strDesc = ""



                'For ispID As Integer = 0 To dtSPID.Rows.Count - 1
                strSql = ""
                strSql = "USP_MAIL_INVOICE_EMAILID  '" & Trim(assessno) & "','" & Trim(workyear) & "'"
                ds = db.sub_GetDataSets(strSql)

                Dim dtTemp As New DataTable
                strText = ""
                strText = "<font face='Calibri' color='black' size=3>A Receipt, &lt; " & ReceiptNo & " &gt; has been generated.</font> <br>"

                strtable += "<html><body>"
                strtable += "<table align='left' cellpadding='0' cellspacing='0' bordercolor='black' Style='width: 100%; height: auto;'>  "
                strtable += "<tr bgcolor='white'><font face='Calibri' color='black' size='3'>"
                strtable += "<td style='padding-left: 10px; padding-bottom: 05px; padding-top: 05px; padding-right: 20px;'>Greetings, <br><br>" & strText & "<br>"
                strtable += "<table align='left' cellpadding='0' cellspacing='0' bordercolor='black' Style='width: 100%; height: auto;'>  "
                strtable = strtable & "<font face='Calibri' color='black' size=3>Please find attachment. </font> "
                strtable += "<br>"
                strtable += "<font face='Calibri' color='black' size=3>"

                strtable += "<br>"
                strtable += "<font face='Calibri' color='black' size=3>Best Regards,<br>"
                ' strtable += "[" & Trim(dtSPID.Rows(ispID)("Person Name")) & "]</font>"
                strtable += "&lt; Bond &gt; </font>"

                strtable += "</table>"
                strtable += "</body></html>"
                strtable += "</table>"
                'If dtSPID.Rows.Count > 0 Then

                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        strMailTo = Trim(ds.Tables(0).Rows(i)("Email")) + ";" + strMailTo
                    Next
                    strToIDs = strMailTo

                End If
                'strToIDs = "durga@phoenixkreations.com;"
                strSubject = "Bond: Invoice [" & ReceiptNo & "] "
                strBodyText = strtable & "<br><br>"
                'End If
                If (strToIDs = ";;") Then
                    Exit Sub
                End If
                ' Exit Sub
                strCcIDs = ""
                ' am.Sub_mail(strToIDs, strCcIDs, strSubject, strBodyText)

                'Next

                ''''''Mail Connection''''''''''

                Dim strmaildomain As String = ""
                'Dim strCcIDs As String = ""
                Dim intPortNo As Integer
                Dim strFrom As String = ""
                Dim strMailPswrd As String = ""
                Dim mm As New MailMessage
                Dim varSplitTo() As String
                '    Dim userid As String = "4"
                strmaildomain = "mail.phoenixkreations.in"
                intPortNo = 25
                strFrom = "aampleERP@phoenixkreations.in"
                strMailPswrd = "kanshu0909"
                Dim mailAddress As New MailAddress(strFrom, "Bond")
                If Trim(strToIDs) <> "" Then
                    varSplitTo = Split(strToIDs, ";")
                    For Each Toid As String In varSplitTo
                        If Trim(Toid) <> "" And Len(Trim(Toid)) >= 5 Then
                            If InStr(Toid, "@") > 0 Then
                                mm.To.Add(Trim(Toid))
                            End If
                        End If
                    Next
                End If
                mm.Bcc.Add("aampleERP@phoenixkreations.in")
                mm.Subject = strSubject
                mm.Body = strBodyText
                mm.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "BondSingleReceiptPrint.pdf")))
                mm.IsBodyHtml = True

                mm.From = mailAddress
                Dim smtpClient As New SmtpClient(strmaildomain, intPortNo)
                Dim credentials As New System.Net.NetworkCredential(strFrom, strMailPswrd)
                smtpClient.EnableSsl = False
                smtpClient.UseDefaultCredentials = False
                smtpClient.Credentials = credentials
                smtpClient.Send(mm)
            End If
            'Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16})

            'Dim strfilename As String = ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "BondSingleReceiptPrint.pdf")
            'Dim oApp As Outlook.Application = New Outlook.Application()
            'Dim mail As Outlook.MailItem = CType(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
            'mail.To = ""
            ''mail.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "BondAssessmentPrint.pdf")))
            'mail.Attachments.Add(strfilename, Outlook.OlAttachmentType.olByValue, 1, "BondSingleReceiptPrint")
            'mail.Display(True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub MultipleMail(ReceiptNo1 As String)
        Try
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report_Bond/MultipleInvoicePrint.rdlc")

            Dim ReceiptNo As String = ReceiptNo1

            'GPNO = "1"
            ds = db.sub_GetDataSets("USP_MULTIPLEINVOICE_PRINT " & ReceiptNo & "")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(2))
            Dim datasource1 As New ReportDataSource("DataSet2", ds.Tables(3))

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)
            ReportViewer1.LocalReport.DataSources.Add(datasource1)
            If ds.Tables(1).Rows.Count > 0 Then

                Dim con_Name As String = Trim(ds.Tables(0).Rows(0)("con_Name") & "")
                Dim AddressI As String = Trim(ds.Tables(0).Rows(0)("AddressI") & "")
                Dim AddressII As String = Trim(ds.Tables(0).Rows(0)("AddressII") & "")
                Dim Con_For As String = Trim(ds.Tables(0).Rows(0)("Con_For") & "")
                Dim ServiceTaxNo As String = Trim(ds.Tables(0).Rows(0)("ServiceTaxNo") & "")
                Dim PANNo As String = Trim(ds.Tables(0).Rows(0)("PANNo") & "")

                Dim ReceiptDate As String = Trim(ds.Tables(1).Rows(0)("ReceiptDate") & "")
                Dim Customer As String = Trim(ds.Tables(1).Rows(0)("Customer") & "")
                Dim InWords As String = Trim(ds.Tables(1).Rows(0)("InWords") & "")
                Dim Remarks As String = Trim(ds.Tables(1).Rows(0)("Remarks") & "")
                Dim NetAmount As String = Trim(ds.Tables(1).Rows(0)("NetAmount") & "")
                Dim TDSAmount As String = Trim(ds.Tables(1).Rows(0)("TDSAmount") & "")
                Dim ReceivedAmount As String = Trim(ds.Tables(1).Rows(0)("ReceivedAmount") & "")

                Dim Username As String = Trim(ds.Tables(1).Rows(0)("UserName") & "")
                Dim Today As String = Trim(ds.Tables(1).Rows(0)("Today") & "")

                Dim p1 As New ReportParameter("con_Name", con_Name)
                Dim p2 As New ReportParameter("AddressI", AddressI)
                Dim p3 As New ReportParameter("AddressII", AddressII)
                Dim p4 As New ReportParameter("Con_For", Con_For)

                Dim p5 As New ReportParameter("ReceiptNo", ReceiptNo)
                Dim p6 As New ReportParameter("ReceiptDate", ReceiptDate)
                Dim p7 As New ReportParameter("Customer", Customer)
                Dim p8 As New ReportParameter("InWords", InWords)
                Dim p9 As New ReportParameter("Remarks", Remarks)

                Dim p10 As New ReportParameter("Username", Username)
                Dim p11 As New ReportParameter("Today", Today)

                Dim p12 As New ReportParameter("NetAmount", NetAmount)
                Dim p13 As New ReportParameter("TDSAmount", TDSAmount)
                Dim p14 As New ReportParameter("ReceivedAmount", ReceivedAmount)
                Dim p15 As New ReportParameter("ServiceTaxNo", ServiceTaxNo)
                Dim p16 As New ReportParameter("PANNo", PANNo)


                'Dim p18 As New ReportParameter("AddressII", AddressII)

                Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16})
                'Dim strfilename As String = ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "BondMultipleReceiptPrint.pdf")
                'Dim oApp As Outlook.Application = New Outlook.Application()
                'Dim mail As Outlook.MailItem = CType(oApp.CreateItem(Outlook.OlItemType.olMailItem), Outlook.MailItem)
                'mail.To = ""
                ''mail.Attachments.Add(New Attachment(ExportReportToPDF(Server.MapPath("~/Report_Bond/"), "BondAssessmentPrint.pdf")))
                'mail.Attachments.Add(strfilename, Outlook.OlAttachmentType.olByValue, 1, "BondMultipleReceiptPrint")
                'mail.Display(True)

                
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Function ExportReportToPDF(path As String, reportName As String) As String

        Dim bytes As Byte() = ReportViewer1.LocalReport.Render("PDF", Nothing, mimeType, encoding, extension, streamids, warnings)
        Dim filename As String = path & reportName
        Using fs = New System.IO.FileStream(filename, System.IO.FileMode.Create)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
        End Using

        Return filename
    End Function
End Class

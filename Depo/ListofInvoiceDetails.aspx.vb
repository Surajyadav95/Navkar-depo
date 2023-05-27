Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
'Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel
Imports Microsoft.Reporting.WebForms
Imports ClosedXML.Excel


Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt10 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
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

    Dim deviceInfo As String

    Dim bytes As Byte()

    Dim lr As New Microsoft.Reporting.WebForms.LocalReport
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Filldropdown()
            If Not Request.QueryString("InvoiceNo") = "" Or Not Request.QueryString("WorkYear") = "" Or Not Request.QueryString("LineID") = "" Or Not Request.QueryString("InvoiceType") = "" Then

            End If
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            btnsearch_Click(sender, e)
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_LineName"
            ds = db.sub_GetDataSets(strSql)
            ddlShipping.DataSource = ds.Tables(0)
            ddlShipping.DataTextField = "SLName"
            ddlShipping.DataValueField = "SLID"
            ddlShipping.DataBind()
            ddlShipping.Items.Insert(0, New ListItem("All", 0))

            ds = db.sub_GetDataSets(strSql)
            ddlinvoicetype.DataSource = ds.Tables(3)
            ddlinvoicetype.DataTextField = "InvoiceType"
            ddlinvoicetype.DataValueField = "ID"
            ddlinvoicetype.DataBind()
            ddlinvoicetype.Items.Insert(0, New ListItem("All", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_Handling_Assessment_summary '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00") & "',"
            strSql += "'" & Trim(ddlcriteria.SelectedValue & "") & "','" & Trim(txtAssessno.Text & "") & "','" & Trim(txtContainerNo.Text & "") & "','" & Trim(ddlShipping.SelectedValue) & "','" & Trim(ddlinvoicetype.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdSummary.DataSource = dt
            grdSummary.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdSummary.DataSource = dt
        grdSummary.PageIndex = e.NewPageIndex
        Me.btnsearch_Click(sender, e)
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            txtAssessno.Text = ""
            'txtnocno.Text = ""
            txtContainerNo.Text = ""
            'ddlinvoicetype.SelectedValue = 0
            If ddlcriteria.SelectedValue = 0 Then
                divInvoiceno.Attributes.Add("style", "display:none")
                divShipping.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:none")

            ElseIf ddlcriteria.SelectedValue = 1 Then
                divInvoiceno.Attributes.Add("style", "display:block")
                divShipping.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:none")
                divinvoice.Attributes.Add("style", "display:none")

            ElseIf ddlcriteria.SelectedValue = 2 Then
                divInvoiceno.Attributes.Add("style", "display:none")
                divShipping.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:block")
                divinvoice.Attributes.Add("style", "display:none")

            ElseIf ddlcriteria.SelectedValue = 3 Then
                divInvoiceno.Attributes.Add("style", "display:none")
                divShipping.Attributes.Add("style", "display:block")
                divContainerNo.Attributes.Add("style", "display:none")
                divinvoice.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 4 Then
                divInvoiceno.Attributes.Add("style", "display:none")
                divinvoice.Attributes.Add("style", "display:block")
                divContainerNo.Attributes.Add("style", "display:none")
                divShipping.Attributes.Add("style", "display:none")
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim AssessNo As String = lnkcancel.CommandArgument
            Dim WorkYear As String = grdSummary.DataKeys(row.RowIndex).Value.ToString()
            Dim str As String = ""
            txtAssessno.Text = AssessNo
            TxtWorkYear.Text = WorkYear
            Dim dtACk As New DataTable
            strSql = ""
            strSql = "USP_Validation_ACK_Status '" & AssessNo & "' "
            dtACk = db.sub_GetDatatable(strSql)
            If dtACk.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' " + dtACk.Rows(0)("msg") + ".');", True)
                Exit Sub
            End If
            strSql = ""
            strSql += "UPDATE eyard_assessM set iscancel=1,cancelledby='" & Session("UserID") & "',CancelledOn=getdate() where InvoiceNo='" & AssessNo & "' and WorkYear='" & WorkYear & "'"
            db.sub_ExecuteNonQuery(strSql)
            btnsearch_Click(sender, e)
            lblsession.Text = "Assessment Cancelled Successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            UpdatePanel6.Update()
            ClientScript.RegisterStartupScript(Page.GetType(), "OpenList", "<script>OpenCancelInvoice(); </script>")

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkAnnexure_Click(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            Dim lnkAnnexure As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkAnnexure.Parent.Parent, GridViewRow)
            Dim AssessNo As String = lnkAnnexure.CommandArgument
            Dim WorkYear As String = grdSummary.DataKeys(row.RowIndex).Value.ToString()
            Dim LineID As String = Trim(CType(gr.FindControl("lblLineID"), Label).Text)
            Dim InvoiceType As String = Trim(CType(gr.FindControl("lblInvoiceType"), Label).Text)
            Dim dblCount As Double = 0
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            
            strSql = ""
            strSql += "USP_INVOICE_WISE_ANNEXURE_SEPARATE '" & AssessNo & "','" & WorkYear & "','" & InvoiceType & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(ds.Tables(0), "Annexure")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(ds.Tables(0).Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(2)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(ds.Tables(1).Rows(0)("InvoiceType") & "") + " for " + Trim(ds.Tables(1).Rows(0)("Month") & "")
                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 20
                        .Row(2).Height = 20
                        .Cell(1, 1).Style.Font.FontSize = 14
                        .Cell(2, 1).Style.Font.FontSize = 14
                        For i = 1 To ds.Tables(0).Columns.Count
                            .Cell(ds.Tables(0).Rows.Count + 3, i).Style.Fill.BackgroundColor = XLColor.Yellow
                        Next
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=Annexure(" & AssessNo & ").xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            strSql = ""
            strSql += " USP_Handling_Assessment_summary '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00") & "',"
            strSql += "'" & Trim(ddlcriteria.SelectedValue & "") & "','" & Trim(txtAssessno.Text & "") & "','" & Trim(txtContainerNo.Text & "") & "','" & Trim(ddlShipping.SelectedValue) & "','" & Trim(ddlinvoicetype.SelectedValue & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            dt.Columns.Remove("lineID")
            dt.Columns.Remove("InvoiceType")
            dt.Columns.Remove("WorkYear")
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Invoice Details" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(12)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("A5:" & Excelno & "5").Merge()
                        .Range("A6:" & Excelno & "6").Merge()
                        .Range("A7:" & Excelno & "7").Merge()
                        .Range("A8:" & Excelno & "8").Merge()
                        .Range("A9:" & Excelno & "9").Merge()
                        .Range("A10:" & Excelno & "10").Merge()
                        .Range("A11:" & Excelno & "11").Merge()
                        .Range("A12:" & Excelno & "12").Merge()

                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
                        .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
                        .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(6).Height = 20
                        .Row(10).Height = 20
                        '.Cell(10, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(10, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(11, 1).Value = "Invoice Details"
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
                        .Cell(6, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(6, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(6, 1).Style.Font.FontSize = 17
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Cell(11, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(11, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(11, 1).Style.Font.FontSize = 17
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=InvoiceDetails" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
    Protected Sub btnGeneratePDF_Click(sender As Object, e As EventArgs)
        Try

            For Each row In grdSummary.Rows
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
                If (Request.QueryString("InvoiceType")) = "3" Then
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Depo/Report_Epic/HandlingEmptyRepoPrint.rdlc")
                Else
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Depo/Report_Epic/HandlingStoragePrint.rdlc")
                End If
                Dim InvoiceNo As String = Request.QueryString("InvoiceNo")
                Dim WorkYear As String = Request.QueryString("WorkYear")
                ds = db.sub_GetDataSets("USP_HANDLING_STORAGE_INVOICE_PRINT '" & Trim(CType(row.FindControl("lblassessNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblworkyear"), Label).Text) & "'")

                Dim datasource As New ReportDataSource("DataSet1", ds.Tables(2))
                Dim datasource1 As New ReportDataSource("DataSet2", ds.Tables(3))
                Dim datasource2 As New ReportDataSource("DataSet3", ds.Tables(4))

                ReportViewer1.LocalReport.DataSources.Clear()
                ReportViewer1.LocalReport.DataSources.Add(datasource)
                ReportViewer1.LocalReport.DataSources.Add(datasource1)
                ReportViewer1.LocalReport.DataSources.Add(datasource2)

                Dim InvoiceDate As String = Trim(ds.Tables(0).Rows(0)("InvoiceDate") & "")
                Dim GSTName As String = Trim(ds.Tables(0).Rows(0)("GSTName") & "")
                Dim GSTAddress As String = Trim(ds.Tables(0).Rows(0)("GSTAddress") & "")
                Dim GSTIn_uniqID As String = Trim(ds.Tables(0).Rows(0)("GSTIn_uniqID") & "")
                Dim State As String = Trim(ds.Tables(0).Rows(0)("State") & "")
                Dim state_Code As String = Trim(ds.Tables(0).Rows(0)("state_Code") & "")
                Dim SLName As String = Trim(ds.Tables(0).Rows(0)("SLName") & "")
                Dim UserName As String = Trim(ds.Tables(0).Rows(0)("UserName") & "")
                Dim Today As String = Trim(ds.Tables(0).Rows(0)("Today") & "")
                Dim AmountInWords As String = Trim(ds.Tables(0).Rows(0)("AmountInWords") & "")
                Dim NetTotal As String = Trim(ds.Tables(0).Rows(0)("NetTotal") & "")
                Dim CGST As String = Trim(ds.Tables(0).Rows(0)("CGST") & "")
                Dim SGST As String = Trim(ds.Tables(0).Rows(0)("SGST") & "")
                Dim IGST As String = Trim(ds.Tables(0).Rows(0)("IGST") & "")
                Dim GrandTotal As String = Trim(ds.Tables(0).Rows(0)("GrandTotal") & "")
                Dim TaxNote As String = Trim(ds.Tables(0).Rows(0)("Note") & "")

                Dim ForMonth As String = Trim(ds.Tables(0).Rows(0)("ForMonth") & "")
                Dim VesselName As String = Trim(ds.Tables(0).Rows(0)("VesselName") & "")
                Dim C20 As String = Trim(ds.Tables(0).Rows(0)("C20") & "")
                Dim C40 As String = Trim(ds.Tables(0).Rows(0)("C40") & "")
                Dim CLON20 As String = Trim(ds.Tables(0).Rows(0)("CLON20") & "")
                Dim CLON40 As String = Trim(ds.Tables(0).Rows(0)("CLON40") & "")
                Dim CLOF20 As String = Trim(ds.Tables(0).Rows(0)("CLOF20") & "")
                Dim CLOF40 As String = Trim(ds.Tables(0).Rows(0)("CLOF40") & "")
                Dim InvoiceType As String = Trim(ds.Tables(0).Rows(0)("InvoiceType") & "")
                Dim PONo As String = Trim(ds.Tables(0).Rows(0)("PONo") & "")
                Dim ViaNo As String = Trim(ds.Tables(0).Rows(0)("ViaNo") & "")
                Dim Remarks As String = Trim(ds.Tables(0).Rows(0)("Remarks") & "")
                Dim MovedFrom As String = Trim(ds.Tables(0).Rows(0)("MovedFrom") & "")
                Dim Vessels As String = Trim(ds.Tables(0).Rows(0)("Vessels") & "")
                Dim DiscAmt As String = Trim(ds.Tables(0).Rows(0)("DiscAmt") & "")


                Dim CON_NAME As String = Trim(ds.Tables(1).Rows(0)("CON_NAME") & "")
                Dim CON_NAMEI As String = Trim(ds.Tables(1).Rows(0)("CON_NAMEI") & "")
                Dim ADDRESSI As String = Trim(ds.Tables(1).Rows(0)("ADDRESSI") & "")
                Dim ADDRESSII As String = Trim(ds.Tables(1).Rows(0)("ADDRESSII") & "")
                Dim CON_DETS As String = Trim(ds.Tables(1).Rows(0)("CINNO") & "")
                Dim CON_FOR As String = Trim(ds.Tables(1).Rows(0)("CON_FOR") & "")
                Dim BankName As String = Trim(ds.Tables(1).Rows(0)("BankName") & "")
                Dim AccountNo As String = Trim(ds.Tables(1).Rows(0)("AccountNo") & "")
                Dim BranchName As String = Trim(ds.Tables(1).Rows(0)("BranchName") & "")
                Dim IFSCCode As String = Trim(ds.Tables(1).Rows(0)("IFSCCode") & "")
                Dim GSTIN As String = Trim(ds.Tables(1).Rows(0)("GSTIN") & "")
                Dim PANNO As String = Trim(ds.Tables(1).Rows(0)("PANNO") & "")
                Dim NoteVI As String = Trim(ds.Tables(1).Rows(0)("NoteVI") & "")

                Dim p1 As New ReportParameter("InvoiceDate", InvoiceDate)
                Dim p2 As New ReportParameter("GSTName", GSTName)
                Dim p3 As New ReportParameter("GSTAddress", GSTAddress)
                Dim p4 As New ReportParameter("GSTIn_uniqID", GSTIn_uniqID)
                Dim p5 As New ReportParameter("State", State)
                Dim p6 As New ReportParameter("state_Code", state_Code)
                Dim p7 As New ReportParameter("SLName", SLName)

                Dim p8 As New ReportParameter("con_Name", CON_NAME)
                Dim p9 As New ReportParameter("AddressI", ADDRESSI)
                Dim p10 As New ReportParameter("CON_NAMEI", CON_NAMEI)
                Dim p11 As New ReportParameter("ADDRESSII", ADDRESSII)

                Dim p12 As New ReportParameter("CON_DETS", CON_DETS)
                Dim p13 As New ReportParameter("CON_FOR", CON_FOR)

                Dim p14 As New ReportParameter("BankName", BankName)
                Dim p15 As New ReportParameter("AccountNo", AccountNo)
                Dim p16 As New ReportParameter("BranchName", BranchName)
                Dim p17 As New ReportParameter("IFSCCode", IFSCCode)

                Dim p18 As New ReportParameter("UserName", UserName)
                Dim p19 As New ReportParameter("Today", Today)

                Dim p20 As New ReportParameter("AmountInWords", AmountInWords)
                Dim p21 As New ReportParameter("NetTotal", NetTotal)
                Dim p22 As New ReportParameter("CGST", CGST)
                Dim p23 As New ReportParameter("SGST", SGST)
                Dim p24 As New ReportParameter("IGST", IGST)
                Dim p25 As New ReportParameter("GrandTotal", GrandTotal)
                Dim p26 As New ReportParameter("InvoiceNo", InvoiceNo)

                Dim p27 As New ReportParameter("ForMonth", ForMonth)
                Dim p28 As New ReportParameter("VesselName", VesselName)
                Dim p29 As New ReportParameter("C20", C20)
                Dim p30 As New ReportParameter("C40", C40)
                Dim p31 As New ReportParameter("CLON20", CLON20)
                Dim p32 As New ReportParameter("CLON40", CLON40)
                Dim p33 As New ReportParameter("CLOF20", CLOF20)
                Dim p34 As New ReportParameter("CLOF40", CLOF40)
                Dim p35 As New ReportParameter("InvoiceType", InvoiceType)
                Dim p38 As New ReportParameter("GSTINCOMP", GSTIN)
                Dim p36 As New ReportParameter("PONo", PONo)
                Dim p37 As New ReportParameter("ViaNo", ViaNo)
                Dim p39 As New ReportParameter("PANNO", PANNO)
                Dim p40 As New ReportParameter("NoteVI", NoteVI)
                Dim p41 As New ReportParameter("Remarks", Remarks)
                Dim p42 As New ReportParameter("TaxNote", TaxNote)
                Dim p43 As New ReportParameter("MovedFrom", MovedFrom)
                Dim p44 As New ReportParameter("Vessels", Vessels)
                Dim p45 As New ReportParameter("DiscAmt", DiscAmt)



                Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45})

                deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"

                bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

                Response.ClearContent()

                Response.ClearHeaders()

                Response.ContentType = "application/pdf"

                Response.BinaryWrite(bytes)
                File.WriteAllBytes("D:\trackerweb\Navkar-Depo\Depo_PDF\AssessMentPrint_" & Val(CType(row.FindControl("lblassessNo"), Label).Text) & ".pdf", bytes)
                'Response.Flush()

                'Response.Close()
            Next
            'btnsearch_Click(sender, e)
            'lblsession.Text = "Assessment Cancelled Successfully"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            'UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

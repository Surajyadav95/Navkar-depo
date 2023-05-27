

Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO

Partial Class Report_Estimation_Default
    Inherits System.Web.UI.Page
    Dim db As New dbOperation_Depo

    Dim strSql, strSql1 As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9 As DataTable
    ' Dim db As New dbOperation
    Dim ds, ds1, ds2, ds3 As DataSet
    Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing
    Dim streamids As String() = Nothing

    Dim mimeType As String = Nothing

    Dim encoding As String = Nothing

    Dim extension As String = Nothing

    Dim deviceInfo As String

    Dim bytes As Byte()

    Dim lr As New Microsoft.Reporting.WebForms.LocalReport
    Private Sub PrintData(ByVal strTINo As String)
        Try
            btnExport_Click()
        Catch ex As Exception
            MsgBox("Error in procedure: " & System.Reflection.MethodBase.GetCurrentMethod.Name & vbCrLf & ex.Message.ToString)
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not Request.QueryString("InvoiceNo") = "" Or Not Request.QueryString("WorkYear") = "" Or Not Request.QueryString("LineID") = "" Or Not Request.QueryString("InvoiceType") = "" Then
                LoadReport()
            End If
        End If
    End Sub
    Protected Sub btnExport_Click()
        Response.ContentType = "application/pdf"
        Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf")
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim sw As New StringWriter()
        Dim hw As New HtmlTextWriter(sw)
        '  pnlPerson.RenderControl(hw)
        Dim sr As New StringReader(sw.ToString())

        Response.Write(hw)
        Response.End()
    End Sub
    Private Sub LoadReport()
        Try
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
            If (Request.QueryString("InvoiceType")) = "3" Then
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Depo/Report_Epic/HandlingEmptyRepoPrint.rdlc")
            Else
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Depo/Report_Epic/HandlingStoragePrint.rdlc")
            End If

            Dim InvoiceNo As String = Request.QueryString("InvoiceNo")
            Dim WorkYear As String = Request.QueryString("WorkYear")

            ds = db.sub_GetDataSets("USP_HANDLING_STORAGE_INVOICE_PRINT_Proforma_M '" & InvoiceNo & "','" & WorkYear & "'")

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
            Dim SignedQRcode As String = Trim(ds.Tables(0).Rows(0)("SignedQRcode") & "")
            Dim AckNo As String = Trim(ds.Tables(0).Rows(0)("AckNo") & "")
            Dim Irn As String = Trim(ds.Tables(0).Rows(0)("Irn") & "")
            Dim Ackdt As String = Trim(ds.Tables(0).Rows(0)("Ackdt") & "")
            Dim Reference_Type As String = Trim(ds.Tables(0).Rows(0)("Reference_Type") & "")
            Dim ReferenceNo As String = Trim(ds.Tables(0).Rows(0)("ReferenceNo") & "")

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
            Dim p46 As New ReportParameter("SignedQRcode", SignedQRcode)
            Dim p47 As New ReportParameter("AckNo", AckNo)
            Dim p48 As New ReportParameter("Irn", Irn)
            Dim p49 As New ReportParameter("Ackdt", Ackdt)
            Dim p50 As New ReportParameter("Reference_Type", Reference_Type)
            Dim p51 As New ReportParameter("ReferenceNo", ReferenceNo)

            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30, p31, p32, p33, p34, p35, p36, p37, p38, p39, p40, p41, p42, p43, p44, p45, p46, p47, p48, p49, p50, p51})

            deviceInfo = "<DeviceInfo><SimplePageHeaders>True</SimplePageHeaders></DeviceInfo>"

            bytes = ReportViewer1.LocalReport.Render("PDF", deviceInfo, mimeType, encoding, extension, streamids, warnings)

            Response.ClearContent()

            Response.ClearHeaders()

            Response.ContentType = "application/pdf"

            Response.BinaryWrite(bytes)

            Response.Flush()

            Response.Close()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub


End Class

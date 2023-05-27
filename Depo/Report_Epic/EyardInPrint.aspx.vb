

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
            If Not Request.QueryString("GateInNo") = "" Then
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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Depo/Report_Epic/EyardInPrint.rdlc")

           

            Dim GateInNo As String = Request.QueryString("GateInNo")


            ds = db.sub_GetDataSets("USP_EMPTY_EYARD_IN_PRINT '" & GateInNo & "'")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(2))
            Dim datasource1 As New ReportDataSource("DataSet2", ds.Tables(3))

            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)
            ReportViewer1.LocalReport.DataSources.Add(datasource1)

            Dim InDate As String = Trim(ds.Tables(0).Rows(0)("InDate") & "")
            Dim TruckNo As String = Trim(ds.Tables(0).Rows(0)("TruckNo") & "")
            Dim InvoiceNo As String = Trim(ds.Tables(0).Rows(0)("InvoiceNo") & "")
            Dim Transporter As String = Trim(ds.Tables(0).Rows(0)("Transporter") & "")
            Dim GSTName As String = Trim(ds.Tables(0).Rows(0)("GSTName") & "")
            Dim Consignee As String = Trim(ds.Tables(0).Rows(0)("Consignee") & "")
            Dim GSTAddress As String = Trim(ds.Tables(0).Rows(0)("GSTAddress") & "")
            Dim GSTIn_uniqID As String = Trim(ds.Tables(0).Rows(0)("GSTIn_uniqID") & "")
            Dim AmountInWords As String = Trim(ds.Tables(0).Rows(0)("AmountInWords") & "")
            Dim UserName As String = Trim(ds.Tables(0).Rows(0)("UserName") & "")
            Dim SignedQRcode As String = Trim(ds.Tables(0).Rows(0)("SignedQRcode") & "")
            Dim AckNo As String = Trim(ds.Tables(0).Rows(0)("AckNo") & "")
            Dim Irn As String = Trim(ds.Tables(0).Rows(0)("Irn") & "")
            Dim Ackdt As String = Trim(ds.Tables(0).Rows(0)("Ackdt") & "")

            Dim CON_NAME As String = Trim(ds.Tables(1).Rows(0)("CON_NAME") & "")
            Dim CON_NAMEI As String = Trim(ds.Tables(1).Rows(0)("CON_NAMEI") & "")
            Dim ADDRESSI As String = Trim(ds.Tables(1).Rows(0)("ADDRESSI") & "")
            Dim PANNo As String = Trim(ds.Tables(1).Rows(0)("PANNo") & "")
            Dim GSTIN As String = Trim(ds.Tables(1).Rows(0)("GSTIN") & "")
                     

            Dim p1 As New ReportParameter("GateInNo", GateInNo)
            Dim p2 As New ReportParameter("InDate", InDate)
            Dim p3 As New ReportParameter("TruckNo", TruckNo)
            Dim p4 As New ReportParameter("InvoiceNo", InvoiceNo)
            Dim p5 As New ReportParameter("Transporter", Transporter)
            Dim p6 As New ReportParameter("GSTName", GSTName)
            Dim p7 As New ReportParameter("Consignee", Consignee)
            Dim p8 As New ReportParameter("GSTAddress", GSTAddress)
            Dim p9 As New ReportParameter("GSTIn_uniqID", GSTIn_uniqID)
            Dim p10 As New ReportParameter("AmountInWords", AmountInWords)
            Dim p11 As New ReportParameter("UserName", UserName)

            Dim p12 As New ReportParameter("CON_NAME", CON_NAME)
            Dim p13 As New ReportParameter("CON_NAMEI", CON_NAMEI)
            Dim p14 As New ReportParameter("AddressI", ADDRESSI)
            Dim p15 As New ReportParameter("PANNo", PANNo)
            Dim p16 As New ReportParameter("GSTIN", GSTIN)
            Dim p17 As New ReportParameter("SignedQRcode", SignedQRcode)
            Dim p18 As New ReportParameter("AckNo", AckNo)
            Dim p19 As New ReportParameter("Irn", Irn)
            Dim p20 As New ReportParameter("Ackdt", Ackdt)

            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20})

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



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
            If Not Request.QueryString("InvoiceNo") = "" Or Not Request.QueryString("WorkYear") = "" Or Not Request.QueryString("InvoiceType") = "" Then
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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Depo/Report_Epic/VehiclePrint.rdlc")

           

            Dim InvoiceNo As String = Request.QueryString("InvoiceNo")
            Dim WorkYear As String = Request.QueryString("WorkYear")
            Dim InvoiceType As String = Request.QueryString("InvoiceType")



            ds = db.sub_GetDataSets("USP_vehicle_Invoice '" & InvoiceNo & "','" & WorkYear & "','" & InvoiceType & "'")

            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(1))


            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)
            'ReportViewer1.LocalReport.DataSources.Add(datasource1)


            Dim InvoiceTypes As String = Trim(ds.Tables(0).Rows(0)("InvoiceTypes") & "")
            Dim AssessDate As String = Trim(ds.Tables(0).Rows(0)("AssessDate") & "")
            Dim TransportName As String = Trim(ds.Tables(0).Rows(0)("TransportName") & "")
            Dim VehicleNo As String = Trim(ds.Tables(0).Rows(0)("VehicleNo") & "")
            Dim AmountInWords As String = Trim(ds.Tables(0).Rows(0)("AmountInWords") & "")
            Dim UserName As String = Trim(ds.Tables(0).Rows(0)("UserName") & "")
          
             
            Dim CON_NAME As String = Trim(ds.Tables(2).Rows(0)("CON_NAME") & "")
            Dim ADDRESSI As String = Trim(ds.Tables(2).Rows(0)("ADDRESSI") & "")
            Dim ADDRESSII As String = Trim(ds.Tables(2).Rows(0)("ADDRESSII") & "")
       
                     

            Dim p1 As New ReportParameter("InvoiceTypes", InvoiceTypes)
            Dim p2 As New ReportParameter("AssessDate", AssessDate)
            Dim p3 As New ReportParameter("TransportName", TransportName)
            Dim p4 As New ReportParameter("VehicleNo", VehicleNo)
            Dim p5 As New ReportParameter("AmountInWords", AmountInWords)
            Dim p6 As New ReportParameter("UserName", UserName)
      

            Dim p7 As New ReportParameter("CON_NAME", CON_NAME)
            Dim p8 As New ReportParameter("ADDRESSI", ADDRESSI)
            Dim p9 As New ReportParameter("ADDRESSII", ADDRESSII)
          

            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9})

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

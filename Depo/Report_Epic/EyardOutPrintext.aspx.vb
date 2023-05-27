﻿

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
            If Not Request.QueryString("GpNo") = "" Then
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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Depo/Report_Epic/EyardOutPrintext.rdlc")

           

            Dim GpNo As String = Request.QueryString("GpNo")


            ds = db.sub_GetDataSets("usp_eyard_out_print_external '" & GpNo & "'")


            Dim datasource As New ReportDataSource("DataSet1", ds.Tables(2))


            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)
            'ReportViewer1.LocalReport.DataSources.Add(datasource1)

            Dim OutDate As String = Trim(ds.Tables(0).Rows(0)("OutDate") & "")
            Dim GpNo1 As String = Trim(ds.Tables(0).Rows(0)("GatepassNo") & "")
            Dim VehicleNo As String = Trim(ds.Tables(0).Rows(0)("VehicleNo") & "")
            Dim SealNo As String = Trim(ds.Tables(0).Rows(0)("SealNo") & "")
            Dim remark As String = Trim(ds.Tables(0).Rows(0)("remark") & "")
            Dim bookingno As String = Trim(ds.Tables(0).Rows(0)("bookingno") & "")
            Dim UserName As String = Trim(ds.Tables(0).Rows(0)("UserName") & "")
            Dim PortName As String = Trim(ds.Tables(0).Rows(0)("PortName") & "")
            Dim shippername As String = Trim(ds.Tables(0).Rows(0)("shippername") & "")
            Dim Ports As String = Trim(ds.Tables(0).Rows(0)("Ports") & "")
             
            Dim CON_NAME As String = Trim(ds.Tables(1).Rows(0)("CON_NAME") & "")
            Dim ADDRESSI As String = Trim(ds.Tables(1).Rows(0)("ADDRESSI") & "")
            Dim ADDRESSII As String = Trim(ds.Tables(1).Rows(0)("ADDRESSII") & "")
            Dim Location As String = Trim(ds.Tables(0).Rows(0)("Location") & "")
            Dim transporter As String = Trim(ds.Tables(0).Rows(0)("transporter") & "")
            Dim GSTIN As String = Trim(ds.Tables(1).Rows(0)("GSTIN") & "")

            Dim p1 As New ReportParameter("GpNo1", GpNo1)
            Dim p2 As New ReportParameter("OutDate", OutDate)
            Dim p3 As New ReportParameter("VehicleNo", VehicleNo)
            Dim p4 As New ReportParameter("SealNo", SealNo)
            Dim p5 As New ReportParameter("remark", remark)
            Dim p6 As New ReportParameter("bookingno", bookingno)
            Dim p7 As New ReportParameter("UserName", UserName)
      

            Dim p8 As New ReportParameter("CON_NAME", CON_NAME)
            Dim p9 As New ReportParameter("ADDRESSI", ADDRESSI)
            Dim p10 As New ReportParameter("ADDRESSII", ADDRESSII)
            Dim p11 As New ReportParameter("Location", Location)
            Dim p12 As New ReportParameter("transporter", transporter)
            Dim p13 As New ReportParameter("PortName", PortName)
            Dim p14 As New ReportParameter("shippername", shippername)
            Dim p15 As New ReportParameter("Ports", Ports)
            Dim p16 As New ReportParameter("GSTIN", GSTIN)

            Me.ReportViewer1.LocalReport.SetParameters(New ReportParameter() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16})

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

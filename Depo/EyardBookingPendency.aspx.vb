Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports System.Configuration

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt10 As DataTable
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            txtason.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")

            btnSave_Click(sender, e)
           
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
 
     
     
     
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Trim(txtsearch.Text) <> "" Then


                Dim strSql As String
                Dim dt As New DataTable
                strSql = ""
                strSql += " Get_Sp_SearchEyardBookingBalances '" & Trim(txtsearch.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                grdsummary.DataSource = dt
                grdsummary.DataBind()
            Else
                strSql = ""
                strSql += " Get_Sp_EyardBookingBalance '" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                grdsummary.DataSource = dt1
                grdsummary.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdsummary_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdsummary.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
 
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
       
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            'If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
            '    Exit Sub
         
            If Trim(txtsearch.Text) <> "" Then
                Dim strSql As String
                Dim dt As New DataTable
                strSql = ""
                strSql += " Get_Sp_SearchEyardBookingBalance '" & Trim(txtsearch.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                grdsummary.DataSource = dt
                grdsummary.DataBind()
            Else
                strSql = ""
                strSql += " Get_Sp_EyardBookingBalance '" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                grdsummary.DataSource = dt1
                grdsummary.DataBind()
                Call fill_Summary()
            End If
                strSql = ""
                strSql += "Select * from con_details"
                dt10 = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Summary" & Trim(txtsearch.Text & "") & "")

                        With wb.Worksheets(0)
                            Dim Excelno As String = ""
                            Excelno = db.GetExcelColumnName(dt.Columns.Count)
                            .Range("A1:" & Excelno & "1").InsertRowsAbove(9)
                            .Range("A1:" & Excelno & "1").Merge()
                            .Range("A2:" & Excelno & "2").Merge()
                            .Range("A3:" & Excelno & "3").Merge()
                            .Range("A4:" & Excelno & "4").Merge()
                            .Range("A5:" & Excelno & "5").Merge()
                            .Range("A6:" & Excelno & "6").Merge()
                            .Range("A7:" & Excelno & "7").Merge()
                            .Range("A8:" & Excelno & "8").Merge()
                            .Range("A9:" & Excelno & "9").Merge()

                            .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                            .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
                            .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
                            .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")

                            .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                            .Row(1).Height = 30
                            .Row(7).Height = 20
                            .Cell(6, 1).Value = "As On: " + Convert.ToDateTime(txtason.Text).ToString("dd MMM yyyy")
                            .Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray

                            .Cell(8, 1).Value = "Summary"
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
                            .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                            .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                            .Cell(8, 1).Style.Font.FontSize = 17
                            .Cell(8, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                            .Cell(1, 1).Style.Font.FontSize = 20
                            .Column(2).Width = 30
                            .Column(3).Width = 10
                            .Column(4).Width = 10
                            .Column(5).Width = 10
                            .Column(6).Width = 10
                            .Column(7).Width = 10
                        End With
                      
                        Response.Clear()
                        Response.Buffer = True
                        Response.Charset = ""
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                        Response.AddHeader("content-disposition", "attachment;filename=BookingMovementTraking" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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

    Private Sub fill_Summary()
        strSql = ""
        strSql += "Select * from con_details"
        dt10 = db.sub_GetDatatable(strSql)
        If (dt1.Rows.Count > 0) Then
            Using wb As New XLWorkbook()

                wb.Worksheets.Add(dt1, "Booking Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")

                With wb.Worksheets(0)
                    Dim Excelno As String = ""
                    Excelno = db.GetExcelColumnName(dt1.Columns.Count)
                    .Range("A1:" & Excelno & "1").InsertRowsAbove(9)
                    .Range("A1:" & Excelno & "1").Merge()
                    .Range("A2:" & Excelno & "2").Merge()
                    .Range("A3:" & Excelno & "3").Merge()
                    .Range("A4:" & Excelno & "4").Merge()
                    .Range("A5:" & Excelno & "5").Merge()
                    .Range("A6:" & Excelno & "6").Merge()
                    .Range("A7:" & Excelno & "7").Merge()
                    .Range("A8:" & Excelno & "8").Merge()
                    .Range("A9:" & Excelno & "9").Merge()

                    .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                    .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
                    .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
                    .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")

                    .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                    .Row(1).Height = 30
                    .Row(7).Height = 20
                    .Cell(6, 1).Value = "As On: " + Convert.ToDateTime(txtason.Text).ToString("dd MMM yyyy")
                    .Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray


                    .Cell(8, 1).Value = "Booking Wise Summary"
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
                    .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                    .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                    .Cell(8, 1).Style.Font.FontSize = 17
                    .Cell(8, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                    .Cell(1, 1).Style.Font.FontSize = 20
                    .Column(27).Width = 50
                    '.Column(8).DataType = XLCellValues.DateTime
                    Dim intRow As Integer = 10

                End With
                Response.Clear()
                Response.Buffer = True
                Response.Charset = ""
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment;filename=BookingMovementTraking" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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

    End Sub

End Class

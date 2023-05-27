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
    Dim dt, dt1, dt10, dt2 As DataTable
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            txtason.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            Filldropdown()
            ''btnSave_Click(sender, e)
           
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Private Sub fill_carting_details()
        Dim strSearchText As String = ""
        If ddlcriteria.SelectedValue = 1 Then
            strSearchText = ddlLineName.SelectedValue
        ElseIf ddlcriteria.SelectedValue = 2 Then
            strSearchText = ddlSize.SelectedItem.Text
        ElseIf ddlcriteria.SelectedValue = 3 Then
            strSearchText = ddlType.SelectedValue
        End If
        Dim strSql As String
        Dim dt As New DataTable
        strSql = ""
        strSql += " SP_Container_Wise_Summary_NEW '" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyyMMddHHmm") & "','" & Trim(ddlcriteria.SelectedItem.Text & "") & "',"
        strSql += "'" & strSearchText & "'"
        dt = db.sub_GetDatatable(strSql)
        grdsummary.DataSource = dt
        grdsummary.DataBind()
    End Sub
    Private Sub fill_Container_Summary()
        Dim strSearchText As String = ""
        If ddlcriteria.SelectedValue = 1 Then
            strSearchText = ddlLineName.SelectedValue
        ElseIf ddlcriteria.SelectedValue = 2 Then
            strSearchText = ddlSize.SelectedItem.Text
        ElseIf ddlcriteria.SelectedValue = 3 Then
            strSearchText = ddlType.SelectedValue
        End If
        Dim strSql As String
        Dim dt As New DataTable
        strSql = ""
        strSql += " Get_Sp_EyardInventory_NEW '" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyyMMddHHmm") & "','" & Trim(ddlcriteria.SelectedItem.Text & "") & "',"
        strSql += "'" & strSearchText & "','" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
        dt1 = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt1
        grdcontainer.DataBind()



    End Sub

  
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_YARD_INVENTROY_SEARCH"
            ds = db.sub_GetDataSets(strSql)
            ddlLineName.DataSource = ds.Tables(0)
            ddlLineName.DataTextField = "SLName"
            ddlLineName.DataValueField = "SLID"
            ddlLineName.DataBind()
            ddlLineName.Items.Insert(0, New ListItem("All", 0))

            ds = db.sub_GetDataSets(strSql)
            ddlType.DataSource = ds.Tables(1)
            ddlType.DataTextField = "ContainerType"
            ddlType.DataValueField = "ContainerTypeID"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("All", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddlLineName.SelectedValue = 0
            If ddlcriteria.SelectedValue = 0 Then
                divLine.Attributes.Add("style", "display:none")
                divSize.Attributes.Add("style", "display:none")
                divType.Attributes.Add("style", "display:none")
            
            ElseIf ddlcriteria.SelectedValue = 1 Then
                divLine.Attributes.Add("style", "display:block")
                divSize.Attributes.Add("style", "display:none")
                divType.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 2 Then
                divSize.Attributes.Add("style", "display:block")

                divLine.Attributes.Add("style", "display:none")
                divType.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 3 Then
                divType.Attributes.Add("style", "display:block")
                divSize.Attributes.Add("style", "display:none")

                divLine.Attributes.Add("style", "display:none")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim strSearchText As String = ""
            If ddlcriteria.SelectedValue = 1 Then
                strSearchText = ddlLineName.SelectedValue
            ElseIf ddlcriteria.SelectedValue = 2 Then
                strSearchText = ddlSize.SelectedItem.Text
            ElseIf ddlcriteria.SelectedValue = 3 Then
                strSearchText = ddlType.SelectedValue
            End If

            fill_carting_details()
            fill_Container_Summary()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdsummary_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdsummary.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
 
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
 
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            'If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
            '    Exit Sub
            'End If
            Dim strSearchText As String = ""
            If ddlcriteria.SelectedValue = 1 Then
                strSearchText = ddlLineName.SelectedValue
            ElseIf ddlcriteria.SelectedValue = 2 Then
                strSearchText = ddlSize.SelectedValue
            ElseIf ddlcriteria.SelectedValue = 3 Then
                strSearchText = ddlType.SelectedValue
            End If
            Dim strSql As String
            Dim dt As New DataTable
            strSql = ""
            strSql += " SP_Container_Wise_Summary_NEW '" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyyMMddHHmmss") & "','" & Trim(ddlcriteria.SelectedItem.Text & "") & "',"
            strSql += "'" & strSearchText & "'"
            dt = db.sub_GetDatatable(strSql)

            strSql = ""
            strSql += " Get_Sp_EyardInventory_NEW '" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyyMMddHHmmss") & "','" & Trim(ddlcriteria.SelectedItem.Text & "") & "',"
            strSql += "'" & strSearchText & "','" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyy-MM-dd HH:mm:ss") & "'"
            dt1 = db.sub_GetDatatable(strSql)
            dt1.Columns.Remove("EntryID")
            dt1.Columns.Remove("Out Date")
            'dt1.Columns.Remove("Customer Name")
            dt1.Columns.Remove("Trailer name")
            dt1.Columns.Remove("Transporter")
            ''dt1.Columns.Remove("Location")
            dt1.Columns.Remove("Booking No")
            dt1.Columns.Remove("Do Valid Date")

            strSql = ""
            strSql += " Get_Sp_EyardInventory_NEW_DPD '" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyyMMddHHmmss") & "','" & Trim(ddlcriteria.SelectedItem.Text & "") & "',"
            strSql += "'" & strSearchText & "','" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyy-MM-dd HH:mm:ss") & "'"
            dt2 = db.sub_GetDatatable(strSql)
            dt2.Columns.Remove("EntryID")
            dt2.Columns.Remove("Out Date")
            dt2.Columns.Remove("Customer Name")
            dt2.Columns.Remove("Trailer name")
            dt2.Columns.Remove("Transporter")
            dt2.Columns.Remove("Location")
            dt2.Columns.Remove("Booking No")
            dt2.Columns.Remove("Do Valid Date")

            dt2.Columns.Remove("Labour Cost")
            dt2.Columns.Remove("Material Cost")
            dt2.Columns.Remove("Cleaning")
            dt2.Columns.Remove("Estimate Amount")
            dt2.Columns.Remove("Approved Amount")
            dt2.Columns.Remove("Approval Date")
            dt2.Columns.Remove("Repair Date")
            dt2.Columns.Remove("Hold Reason")

            dt2.Columns.Remove("Condition")
            dt2.Columns.Remove("MFG Date")
            dt2.Columns.Remove("Gross Weight")
            dt2.Columns.Remove("Tare Weight")
            dt2.Columns.Remove("Pay Load")
            dt2.Columns.Remove("GRADE")
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    wb.Worksheets.Add(dt1, "Container Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    wb.Worksheets.Add(dt2, "DPD Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
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
                        If ddlcriteria.SelectedValue = 0 Then
                            .Cell(7, 1).Value = "All"
                        ElseIf ddlcriteria.SelectedValue = 1 Then
                            .Cell(7, 1).Value = ddlcriteria.SelectedItem.Text + ": " + ddlLineName.SelectedItem.Text
                        End If

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
                    With wb.Worksheets(1)
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
                        If ddlcriteria.SelectedValue = 0 Then
                            .Cell(7, 1).Value = "All"
                        ElseIf ddlcriteria.SelectedValue = 1 Then
                            .Cell(7, 1).Value = ddlcriteria.SelectedItem.Text + ": " + ddlLineName.SelectedItem.Text
                        End If

                        .Cell(8, 1).Value = "Container Wise Summary"
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
                        .Column(24).Width = 30
                        '.Column(8).DataType = XLCellValues.DateTime
                        Dim intRow As Integer = 10
                        For i = 0 To dt1.Rows.Count - 1
                            intRow += 1
                            If Not Trim(dt1.Rows(i)("Hold Reason") & "") = "" Then
                                For j = 1 To dt1.Columns.Count
                                    .Cell(intRow, j).Style.Fill.BackgroundColor = XLColor.Yellow
                                Next
                            End If
                            For j = 1 To dt1.Columns.Count
                                .Cell(intRow, 9).DataType = XLCellValues.DateTime
                            Next
                        Next                        
                    End With

                    With wb.Worksheets(2)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt2.Columns.Count)
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
                        If ddlcriteria.SelectedValue = 0 Then
                            .Cell(7, 1).Value = "All"
                        ElseIf ddlcriteria.SelectedValue = 1 Then
                            .Cell(7, 1).Value = ddlcriteria.SelectedItem.Text + ": " + ddlLineName.SelectedItem.Text
                        End If

                        .Cell(8, 1).Value = "DPD Summary"
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
                        For i = 0 To dt1.Rows.Count - 1
                            intRow += 1
                            If Not Trim(dt1.Rows(i)("Hold Reason") & "") = "" Then
                                For j = 1 To dt1.Columns.Count
                                    .Cell(intRow, j).Style.Fill.BackgroundColor = XLColor.Yellow
                                Next
                            End If

                            .Cell(intRow, 9).DataType = XLCellValues.DateTime

                        Next
                    End With

                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=YardInventory" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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

End Class

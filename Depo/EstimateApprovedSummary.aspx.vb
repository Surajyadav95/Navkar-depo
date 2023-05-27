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
            db.sub_ExecuteNonQuery("Delete From Temp_Est_Report Where User_ID=" & Session("UserId_DepoCFS") & "")

            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            ddlSearchOn.Text = "All"
            Filldropdown()
            btnShow_Click(sender, e)

        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_LineName"
            ds = db.sub_GetDataSets(strSql)
            ddlShipline.DataSource = ds.Tables(0)
            ddlShipline.DataTextField = "SLName"
            ddlShipline.DataValueField = "SLID"
            ddlShipline.DataBind()
            ddlShipline.Items.Insert(0, New ListItem("All", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try

            strSql = ""
            strSql += " USP_Get_Est_Approved_Dets '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlSearchOn.SelectedItem.Text) & "',"
            If Trim(ddlSearchOn.SelectedItem.Text) = "Shipping Line" Then
                strSql += "'" & Trim(ddlShipline.SelectedItem.Text) & "'"
            ElseIf Trim(ddlSearchOn.SelectedItem.Text) = "Container No" Then
                strSql += "'" & Trim(TxtContainerNo.Text) & "'"
            Else
                strSql += "''"
            End If
            dt = db.sub_GetDatatable(strSql)
            grdRegistrationSummary.DataSource = dt
            grdRegistrationSummary.DataBind()
            'up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdRegistrationSummary_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdRegistrationSummary.PageIndex = e.NewPageIndex
        Me.btnShow_Click(sender, e)
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        db.sub_ExecuteNonQuery("Delete From Temp_Est_Report Where User_ID=" & Session("UserId_DepoCFS") & "")

        If Trim(ddlFormat.SelectedItem.Text) = "General" Then
            Call General()
        End If
        If Trim(ddlFormat.SelectedItem.Text) = "PAN ASIA LINE" Then
            Call PAN()
        End If
        If Trim(ddlFormat.SelectedItem.Text) = "HYUNDAI MARCHANT MARINE LINE" Then
            Call Hyundai()
        End If

    End Sub
    Protected Sub ddlSearchOn_SelectedIndexChanged(sender As Object, e As EventArgs)

        If (ddlSearchOn.SelectedValue = "1") Then
            divShiplineName.Attributes.Add("style", "display:block")
            TxtContainerNo.Text = ""
        Else
            divShiplineName.Attributes.Add("style", "display:None")
        End If
        If (ddlSearchOn.SelectedValue = "2") Then
            divContainerNo.Attributes.Add("style", "display:block")
            ddlShipline.SelectedValue = 0

        Else
            divContainerNo.Attributes.Add("style", "display:None")
        End If
    End Sub

    Private Sub General()
        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            Dim strSearchText As String = ""
            Dim dt2 As New DataTable


            strSql = ""
                strSql += "Select * from con_details"
                dt10 = db.sub_GetDatatable(strSql)
            If (dt10.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    For Each row In grdRegistrationSummary.Rows
                        strSql = ""
                        strSql += " SP_GetEstimationCostDetails '" & Trim(CType(row.FindControl("lblEstimate_ID"), Label).Text) & "'"
                        dt = db.sub_GetDatatable(strSql)
                        dt.Columns.Remove("Location")
                        dt.Columns.Remove("Comp Code")
                        dt.Columns.Remove("Dmg Code")
                        dt.Columns.Remove("Repair Code")
                        dt.Columns.Remove("Loc Code")
                        dt.Columns.Remove("Length")
                        dt.Columns.Remove("Width")
                        dt.Columns.Remove("Qty")
                        dt.Columns.Remove("Total Approved Amount")




                        Exit For
                    Next

                    wb.Worksheets.Add("Estimate Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(8)
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
                        .Cell(6, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray


                        .Cell(8, 1).Value = "Estimate Summary"
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
                        .Cell(7, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(7, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Range("A10:" & Excelno & "10").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Range("A10:" & Excelno & "10").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Font.FontSize = 17
                        .Cell(8, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(1, 1).Style.Font.FontSize = 20
                        Dim introw As Integer = 9
                        For Each row In grdRegistrationSummary.Rows
                            If CType(row.FindControl("chkright"), CheckBox).Checked = True Then
                                'strSql = ""
                                'strSql += " SP_GetEstimationCostDetails '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                                ''strSql += "'" & Val(dblLineName.SelectedValue) & "'"
                                'dt = db.sub_GetDatatable(strSql)
                                .Row(introw + 1).InsertRowsBelow(2)
                                introw += 3
                                '.Cell(introw, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                                .Cell(introw, 1).Value = "Client :"
                                .Column(1).Width = 15
                                .Column(2).Width = 50
                                .Column(3).Width = 15
                                .Column(4).Width = 15
                                .Column(5).Width = 15
                                .Column(6).Width = 15
                                .Column(7).Width = 15
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblShippingLine"), Label).Text)
                                .Cell(introw, 5).Value = "Location:"
                                .Cell(introw, 6).Value = Trim(CType(row.FindControl("lblLocation"), Label).Text)
                                .Row(introw).InsertRowsBelow(1)
                                introw += 1
                                .Cell(introw, 1).Value = "Consignee:"
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblConsignee"), Label).Text)
                                .Cell(introw, 5).Value = "Survey Type:"
                                .Cell(introw, 6).Value = Trim(CType(row.FindControl("lblSurveyType"), Label).Text)
                                .Row(introw).InsertRowsBelow(1)
                                introw += 1
                                .Cell(introw, 1).Value = "Validity:"
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblValidity"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Row(introw).InsertRowsBelow(1)
                                introw += 1

                                .Cell(introw, 1).Value = "Container No:"
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 2).Value = "Size :"
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 3).Value = "In Date:"
                                .Cell(introw, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 4).Value = "Gross Wt:"
                                .Cell(introw, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 5).Value = "Tare Wt:"
                                .Cell(introw, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 6).Value = "Mfg. Date:"
                                .Cell(introw, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Row(introw).InsertRowsBelow(1)
                                introw += 1
                                .Cell(introw, 1).Value = Trim(CType(row.FindControl("lblContainerNo"), Label).Text)
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblSize"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 2).DataType = XLCellValues.Text

                                .Cell(introw, 3).Value = Trim(CType(row.FindControl("lblInDate"), Label).Text)
                                .Cell(introw, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 4).Value = Trim(CType(row.FindControl("lblGrossWeight"), Label).Text)
                                .Cell(introw, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 5).Value = Trim(CType(row.FindControl("lblTareWeight"), Label).Text)
                                .Cell(introw, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 6).Value = Trim(CType(row.FindControl("lblMFGDate"), Label).Text)
                                .Cell(introw, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                                .Cell(introw, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                introw += 1
                                Dim dt1 As New DataTable
                                strSql = ""
                                strSql += " SP_GetEstimationCostDetails '" & Trim(CType(row.FindControl("lblEstimate_ID"), Label).Text) & "'"
                                dt1 = db.sub_GetDatatable(strSql)
                                dt1.Columns.Remove("Location")
                                dt1.Columns.Remove("Comp Code")
                                dt1.Columns.Remove("Dmg Code")
                                dt1.Columns.Remove("Repair Code")
                                dt1.Columns.Remove("Loc Code")
                                dt1.Columns.Remove("Length")
                                dt1.Columns.Remove("Width")
                                dt1.Columns.Remove("Qty")
                                dt1.Columns.Remove("Total Approved Amount")

                                'Dim i As Integer = 0
                                wb.Worksheet(1).Cell(introw, 1).InsertTable(dt1)
                                strSql = ""
                                strSql = "select SGST, CGST, IGST, (SGST+CGST+IGST+Est_Amount) as [Grand Total] from Estimate_M"
                                strSql += " Where ContainerNo='" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "' and EntryId='" & Trim(CType(row.FindControl("lblEntryId"), Label).Text) & "'"
                                dt2 = db.sub_GetDatatable(strSql)
                                introw += dt1.Rows.Count + 1
                                .Row(introw).InsertRowsBelow(1)
                                'introw += 1
                                .Cell(introw, 5).Value = "SGST"
                                .Cell(introw, 6).Value = dt2.Rows(0)("SGST")
                                introw += 1
                                .Cell(introw, 5).Value = "CGST"
                                .Cell(introw, 6).Value = dt2.Rows(0)("CGST")
                                introw += 1
                                .Cell(introw, 5).Value = "IGST"
                                .Cell(introw, 6).Value = dt2.Rows(0)("IGST")
                                introw += 1
                                .Cell(introw, 5).Value = "Grand Total"
                                .Cell(introw, 5).Style.Fill.BackgroundColor = XLColor.Yellow
                                .Cell(introw, 6).Value = dt2.Rows(0)("Grand Total")
                                .Cell(introw, 6).Style.Fill.BackgroundColor = XLColor.Yellow

                                '.Cell(introw, 5).Style.Fill.BackgroundColor = XLColor.Yellow
                                '.Cell(introw, 6).Style.Fill.BackgroundColor = XLColor.Yellow

                            End If


                        Next
                    End With

                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=EstimateSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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

    Private Sub Hyundai()
        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            Dim strSearchText As String = ""
            Dim dt2 As New DataTable
            Dim dt3 As New DataTable
            Dim dt4 As New DataTable
            Dim dt5 As New DataTable


            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)

            'strSql = ""
            'strSql = "SP_Estimate_Report '" & Trim(dt3.Rows(0)("Container No")) & "','" & Trim(dt3.Rows(0)("Entry Id")) & "'"
            'dt3 = db.sub_GetDatatable(strSql)


            If (dt10.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    For Each row In grdRegistrationSummary.Rows
                        strSql = ""
                        strSql = "SP_Estimate_Report '" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblEntryId"), Label).Text) & "'"
                        dt3 = db.sub_GetDatatable(strSql)

                        strSql = ""
                        strSql = "USP_INSERT_TEMP_EST_REPORT '" & Trim(dt3.Rows(0)("Container No")) & "','" & Trim(dt3.Rows(0)("Size\Type")) & "','" & Trim(dt3.Rows(0)("In Date")) & "','" & Trim(dt3.Rows(0)("Washing")) & "',"
                        strSql += "'" & Trim(dt3.Rows(0)("Damage")) & "','" & Trim(dt3.Rows(0)("Tax")) & "','" & Trim(dt3.Rows(0)("Total")) & "','" & Trim(dt3.Rows(0)("Estimation No")) & "','" & Trim(dt3.Rows(0)("Remarks")) & "','" & Session("UserId_DepoCFS") & "'"
                        dt4 = db.sub_GetDatatable(strSql)


                    Next

                    strSql = ""
                    strSql += "sELECT Row_Number() Over (Order By ContainerNo desc) as [Sr No], ContainerNo, Size_Type, InDate, Washing, Damage, Tax, Total, Estimation_No, Remarks FROM Temp_Est_report WHERE User_Id=" & Session("UserId_DepoCFS") & ""
                    dt5 = db.sub_GetDatatable(strSql)


                    wb.Worksheets.Add(dt5, "Covering" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    wb.Worksheets.Add("Estimate Details" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")

                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        'dt1.Columns(5).ColumnMapping = MappingType.Hidden

                        Excelno = db.GetExcelColumnName(dt3.Columns.Count)
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
                        .Cell(6, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray

                        .Cell(8, 1).Value = "Estimation Report"
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
                        .Cell(7, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(7, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Range("A10:" & Excelno & "10").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Range("A10:" & Excelno & "10").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Font.FontSize = 17
                        .Cell(8, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Column(2).Width = 30
                        .Column(3).Width = 10
                        .Column(4).Width = 20
                        .Column(5).Width = 10
                        .Column(6).Width = 10
                        .Column(7).Width = 10
                        .Column(10).Width = 30
                    End With

                    With wb.Worksheets(1)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(8)
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
                        .Cell(6, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray


                        .Cell(8, 1).Value = "Estimate Details"
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
                        .Cell(7, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(7, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Range("A10:" & Excelno & "10").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Range("A10:" & Excelno & "10").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Font.FontSize = 17
                        .Cell(8, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(1, 1).Style.Font.FontSize = 20
                        Dim introw As Integer = 9
                        For Each row In grdRegistrationSummary.Rows
                            If CType(row.FindControl("chkright"), CheckBox).Checked = True Then
                                .Row(introw + 1).InsertRowsBelow(2)
                                introw += 3
                                '.Cell(introw, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                                .Cell(introw, 1).Value = "Shipping Line :"
                                .Column(1).Width = 15
                                .Column(2).Width = 50
                                .Column(3).Width = 15
                                .Column(4).Width = 15
                                .Column(5).Width = 15
                                .Column(6).Width = 15
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblShippingLine"), Label).Text)

                                introw += 1
                                .Cell(introw, 1).Value = "Container No:"
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblContainerNo"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center


                                .Cell(introw, 3).Value = "Size:"
                                .Cell(introw, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 4).Value = Trim(CType(row.FindControl("lblSize"), Label).Text)
                                .Cell(introw, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                .Cell(introw, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 5).Value = "Estimation No:"
                                .Cell(introw, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 6).Value = Trim(CType(row.FindControl("lblEstimationNo"), Label).Text)
                                .Cell(introw, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                introw += 1

                                .Cell(introw, 1).Value = "In Date:"
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblInDate"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center


                                .Cell(introw, 3).Value = "Gross Wt:"
                                .Cell(introw, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 4).Value = Trim(CType(row.FindControl("lblGrossWeight"), Label).Text)
                                .Cell(introw, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                .Cell(introw, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 5).Value = "CSCASP:"
                                .Cell(introw, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 6).Value = Trim(CType(row.FindControl("lblCSCASP"), Label).Text)
                                .Cell(introw, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                introw += 1
                                .Cell(introw, 1).Value = "Mfg. Date:"
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblMFGDate"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 3).Value = "Tare Wt:"
                                .Cell(introw, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 4).Value = Trim(CType(row.FindControl("lblTareWeight"), Label).Text)
                                .Cell(introw, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right
                                .Cell(introw, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 5).Value = "Validity:"
                                .Cell(introw, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 6).Value = Trim(CType(row.FindControl("lblValidity"), Label).Text)
                                .Cell(introw, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                introw += 1

                                .Cell(introw, 1).Value = "Location:"
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblLocation"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                                .Cell(introw, 3).Value = "Survey Type:"
                                .Cell(introw, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 4).Value = Trim(CType(row.FindControl("lblSurveyType"), Label).Text)
                                .Cell(introw, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 5).Value = "LAB Rate: "
                                .Cell(introw, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                introw += 1
                                .Cell(introw, 3).Value = "Survey By:"
                                .Cell(introw, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 4).Value = Trim(CType(row.FindControl("lblSurveyBy"), Label).Text)
                                .Cell(introw, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                                .Cell(introw, 3).Value = "Currency:"
                                .Cell(introw, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 4).Value = "INR"
                                .Cell(introw, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left

                                .Row(introw).InsertRowsBelow(1)


                                introw += 1
                                Dim dt1 As New DataTable
                                strSql = ""
                                strSql += " SP_GetEstimationCostDetails '" & Trim(CType(row.FindControl("lblEstimate_ID"), Label).Text) & "'"
                                dt1 = db.sub_GetDatatable(strSql)
                                dt.Columns.Remove("Location")
                                dt.Columns.Remove("Comp Code")
                                dt.Columns.Remove("Dmg Code")
                                dt.Columns.Remove("Repair Code")
                                dt.Columns.Remove("Loc Code")
                                dt.Columns.Remove("Length")
                                dt.Columns.Remove("Width")
                                dt.Columns.Remove("Qty")
                                dt.Columns.Remove("Total Approved Amount")

                                'Dim i As Integer = 0
                                wb.Worksheet(2).Cell(introw, 1).InsertTable(dt1)
                                strSql = ""
                                strSql = "select SGST, CGST, IGST, (SGST+CGST+IGST+Est_Amount) as [Grand Total] from Estimate_M"
                                strSql += " Where ContainerNo='" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "' and EntryId='" & Trim(CType(row.FindControl("lblEntryId"), Label).Text) & "'"
                                dt2 = db.sub_GetDatatable(strSql)
                                introw += dt1.Rows.Count + 1
                                .Row(introw).InsertRowsBelow(1)
                                'introw += 1
                                .Cell(introw, 5).Value = "SGST"
                                .Cell(introw, 6).Value = dt2.Rows(0)("SGST")
                                introw += 1
                                .Cell(introw, 5).Value = "CGST"
                                .Cell(introw, 6).Value = dt2.Rows(0)("CGST")
                                introw += 1
                                .Cell(introw, 5).Value = "IGST"
                                .Cell(introw, 6).Value = dt2.Rows(0)("IGST")
                                introw += 1
                                .Cell(introw, 5).Value = "Grand Total"
                                .Cell(introw, 5).Style.Fill.BackgroundColor = XLColor.Yellow
                                .Cell(introw, 6).Value = dt2.Rows(0)("Grand Total")
                                .Cell(introw, 6).Style.Fill.BackgroundColor = XLColor.Yellow



                            End If


                        Next
                    End With



                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=EstimateSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
    Private Sub PAN()
        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            Dim strSearchText As String = ""
            Dim dt2 As New DataTable



            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt10.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    For Each row In grdRegistrationSummary.Rows
                        strSql = ""
                        strSql += " SP_GetEstimationCostDetails '" & Trim(CType(row.FindControl("lblEstimate_ID"), Label).Text) & "'"
                        dt = db.sub_GetDatatable(strSql)
                        dt.Columns.Remove("Total Approved Amount")
                        Exit For
                    Next




                    wb.Worksheets.Add("Estimate Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(8)
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
                        .Cell(6, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(8, 1).Value = "Estimate Summary"
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
                        .Cell(7, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(7, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Range("A10:" & Excelno & "10").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Range("A10:" & Excelno & "10").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Font.FontSize = 17
                        .Cell(8, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(1, 1).Style.Font.FontSize = 20
                        Dim introw As Integer = 9
                        For Each row In grdRegistrationSummary.Rows
                            Dim dblManHrs As Double = 0, dblhrsCost As Double = 0, dblMtlCost As Double = 0, dblTotalCost As Double = 0
                            If CType(row.FindControl("chkright"), CheckBox).Checked = True Then
                                .Row(introw + 1).InsertRowsBelow(2)
                                introw += 3
                                .Cell(introw, 1).Value = "Estimate No :"
                                .Cell(introw, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                                .Column(1).Width = 15
                                .Column(2).Width = 20
                                .Column(3).Width = 50
                                .Column(4).Width = 15
                                .Column(5).Width = 15
                                .Column(6).Width = 15
                                .Column(7).Width = 15
                                .Column(8).Width = 30
                                .Column(9).Width = 15
                                .Column(10).Width = 15
                                .Column(11).Width = 15
                                .Column(12).Width = 15
                                .Column(13).Width = 15
                                .Column(14).Width = 15
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblEstimationNo"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 2).Style.Fill.BackgroundColor = XLColor.LightPink
                                introw += 1

                                .Cell(introw, 1).Value = "Container No:"
                                .Cell(introw, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblContainerNo"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 2).Style.Fill.BackgroundColor = XLColor.LightPink
                                introw += 1
                                .Cell(introw, 1).Value = "Size :"
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblSize"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 2).Style.Fill.BackgroundColor = XLColor.LightPink
                                .Cell(introw, 2).DataType = XLCellValues.Text
                                introw += 1

                                .Cell(introw, 1).Value = "Container Type :"
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblContainerType"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 2).Style.Fill.BackgroundColor = XLColor.LightPink

                                introw += 1

                                .Cell(introw, 1).Value = "In Date:"
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblInDate"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 2).Style.Fill.BackgroundColor = XLColor.LightPink

                                introw += 1

                                .Cell(introw, 1).Value = "CSCASP:"
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblCSCASP"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 2).Style.Fill.BackgroundColor = XLColor.LightPink
                                introw += 1

                                .Cell(introw, 1).Value = "MFG Date:"
                                .Cell(introw, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                                .Cell(introw, 2).Value = Trim(CType(row.FindControl("lblMFGDate"), Label).Text)
                                .Cell(introw, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                                .Cell(introw, 2).Style.Fill.BackgroundColor = XLColor.LightPink
                                introw += 1
                                .Row(introw).InsertRowsBelow(3)
                                introw += 1
                                Dim dt1 As New DataTable
                                strSql = ""
                                strSql += " SP_GetEstimationCostDetails '" & Trim(CType(row.FindControl("lblEstimate_ID"), Label).Text) & "'"
                                dt1 = db.sub_GetDatatable(strSql)
                                dt1.Columns.Remove("Total Approved Amount")

                                For i As Integer = 0 To dt1.Rows.Count - 1
                                    dblManHrs += Format(dt1.Rows(i)("Hrs."), "0.00")
                                    dblhrsCost += Format(dt1.Rows(i)("Hrs. Cost"), "0.00")
                                    dblMtlCost += Format(dt1.Rows(i)("Mtl. Cost"), "0.00")
                                    dblTotalCost += Format(dt1.Rows(i)("Total Cost"), "0.00")
                                Next

                                'Dim i As Integer = 0
                                wb.Worksheet(1).Cell(introw, 1).InsertTable(dt1)
                                strSql = ""
                                strSql = "SP_Calculate_Tax '" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblEntryId"), Label).Text) & "'"
                                dt2 = db.sub_GetDatatable(strSql)
                                introw += dt1.Rows.Count + 1
                                .Row(introw).InsertRowsBelow(2)
                                .Cell(introw, 3).Value = "Total"
                                .Cell(introw, 3).Style.Fill.BackgroundColor = XLColor.Yellow
                                .Cell(introw, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                                .Cell(introw, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                                .Cell(introw, 4).Value = dblManHrs
                                .Cell(introw, 4).Style.Fill.BackgroundColor = XLColor.Yellow
                                .Cell(introw, 5).Value = dblhrsCost
                                .Cell(introw, 5).Style.Fill.BackgroundColor = XLColor.Yellow
                                .Cell(introw, 6).Value = dblMtlCost
                                .Cell(introw, 6).Style.Fill.BackgroundColor = XLColor.Yellow
                                .Cell(introw, 7).Value = dblTotalCost
                                .Cell(introw, 7).Style.Fill.BackgroundColor = XLColor.Yellow

                                introw += 1
                                .Row(introw).InsertRowsBelow(1)
                                introw += 1

                                .Cell(introw, 3).Value = "CLEANING CONSIGNEE(CC)"
                                .Cell(introw, 3).Style.Fill.BackgroundColor = XLColor.LightBlue
                                .Cell(introw, 4).Value = dt2.Rows(0)("Washing")
                                .Cell(introw, 4).Style.Fill.BackgroundColor = XLColor.Tan

                                introw += 1
                                .Cell(introw, 3).Value = "DAMAGE CONSIGNEE(DC)"
                                .Cell(introw, 3).Style.Fill.BackgroundColor = XLColor.LightBlue

                                .Cell(introw, 4).Value = dt2.Rows(0)("Damage")
                                .Cell(introw, 4).Style.Fill.BackgroundColor = XLColor.Tan

                                introw += 1
                                .Cell(introw, 3).Value = "Total"
                                .Cell(introw, 3).Style.Fill.BackgroundColor = XLColor.LightBlue

                                .Cell(introw, 4).Value = dt2.Rows(0)("Damage") + dt2.Rows(0)("Washing")
                                .Cell(introw, 4).Style.Fill.BackgroundColor = XLColor.Tan

                                introw += 1
                                .Cell(introw, 3).Value = "GST"
                                .Cell(introw, 3).Style.Fill.BackgroundColor = XLColor.LightBlue

                                .Cell(introw, 4).Value = dt2.Rows(0)("Tax")
                                .Cell(introw, 4).Style.Fill.BackgroundColor = XLColor.Tan

                                introw += 1
                                .Cell(introw, 3).Value = "Grand Total"
                                .Cell(introw, 3).Style.Fill.BackgroundColor = XLColor.Yellow

                                .Cell(introw, 4).Value = dt2.Rows(0)("Grand Total")
                                .Cell(introw, 4).Style.Fill.BackgroundColor = XLColor.Yellow


                            End If


                        Next
                    End With

                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=EstimateSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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

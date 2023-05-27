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
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10 As DataTable
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
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
            ddlLineName.DataSource = ds.Tables(0)
            ddlLineName.DataTextField = "SLName"
            ddlLineName.DataValueField = "SLID"
            ddlLineName.DataBind()
            ddlLineName.Items.Insert(0, New ListItem("All", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            If ddlLineName.SelectedItem.Text = "All" Then
                strSql = ""
                strSql += " Get_sp_EmptyYardALLDAR '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
                'strSql += "'" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
            Else
                strSql = ""
                strSql += " Get_sp_EmptyYardDAR '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                strSql += "'" & Val(ddlLineName.SelectedValue) & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
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

        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            Dim strSearchText As String = ""
            Dim Rowindex As Integer = 0

            strSql = ""
            strSql += " Get_Sp_EyardInDAR '" & Val(ddlLineName.SelectedValue) & "','" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
            'strSql += "'" & Val(ddlLineName.SelectedValue) & "'"
            dt1 = db.sub_GetDatatable(strSql)
            dt1.Columns.Remove("Source")
            dt1.Columns.Remove("Blno")
            dt1.Columns.Remove("Size")
            dt1.Columns.Remove("ContainerType")
            dt1.Columns.Remove("Voy")
            dt1.Columns.Remove("SLName")
            dt1.Columns.Remove("Agenti")
            dt1.Columns.Remove("GrossWt")
            dt1.Columns.Remove("TareWeight")
            dt1.Columns.Remove("Received")
            dt1.Columns.Remove("PayLoad")
            dt1.Columns.Remove("Damage")
            dt1.Columns.Remove("COMMODITY")
            dt1.Columns.Remove("MFG Date")
            dt1.Columns.Remove("FROM")
            dt1.Columns.Remove("REF. NO.")
            dt1.Columns.Remove("OnLine Status")
            dt1.Columns.Remove("condition")
            dt1.Columns.Remove("CSC Details")
            dt1.Columns.Remove("DO No.")
            dt1.Columns.Remove("Instruction Given By")
            dt1.Columns.Remove("LONGSTAY DAYS")
            dt1.Columns.Remove("PTI DATE")
            dt1.Columns.Remove("DO Validity")
            strSql = ""
            strSql += " Get_Sp_EyardOUTDAR '" & Val(ddlLineName.SelectedValue) & "','" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
            'strSql += "'" & Val(ddlLineName.SelectedValue) & "'"
            dt2 = db.sub_GetDatatable(strSql)
            dt2.Columns.Remove("Size")
            dt2.Columns.Remove("ContainerType")
            dt2.Columns.Remove("Dwelldays")
            dt2.Columns.Remove("ApprovalAmount")
            dt2.Columns.Remove("Agenti")
            dt2.Columns.Remove("TareWeight")
            dt2.Columns.Remove("GROSSWT")
            dt2.Columns.Remove("RefNo")
            dt2.Columns.Remove("PortName")
            dt2.Columns.Remove("SLName")
            dt2.Columns.Remove("Payload")
            dt2.Columns.Remove("carryingcapacity")
            dt2.Columns.Remove("Destination")
            dt2.Columns.Remove("FPD")
            dt2.Columns.Remove("RepairDT")
            dt2.Columns.Remove("CRONo")
            dt2.Columns.Remove("FORMNO")
            dt2.Columns.Remove("STATUS")
            dt2.Columns.Remove("condition")
            dt2.Columns.Remove("Cargo")
            dt2.Columns.Remove("OnLine Status")
            dt2.Columns.Remove("Source")
            dt2.Columns.Remove("Do No")
            dt2.Columns.Remove("Instruction Given By")
            dt2.Columns.Remove("Huleir Page Reveived")
            dt2.Columns.Remove("customseal")
            dt2.Columns.Remove("Wagon")
            dt2.Columns.Remove("POL")
            dt2.Columns.Remove("TruckIn Time")
            dt2.Columns.Remove("Out Temperature")
            dt2.Columns.Remove("Reefer Type")
            dt2.Columns.Remove("Vent Seal No")
            dt2.Columns.Remove("EOA No")
            dt2.Columns.Remove("Voyage")
            dt2.Columns.Remove("Remarks")
            dt2.Columns.Remove("Ref No")


            strSql = ""
            strSql += " Get_Sp_EyardInventoryDAR '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Val(ddlLineName.SelectedValue) & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
            'strSql += "'" & Val(ddlLineName.SelectedValue) & "'"
            dt3 = db.sub_GetDatatable(strSql)
            dt3.Columns.Remove("estimateDate")
            dt3.Columns.Remove("ApprovedDate")
            'dt3.Columns.Remove("ApprovedAmt")
            'dt3.Columns.Remove("RepairedDate")
            dt3.Columns.Remove("Dwelldays")
            dt3.Columns.Remove("Remarks")
            dt3.Columns.Remove("Location")
            dt3.Columns.Remove("InYard")
            dt3.Columns.Remove("Size")
            dt3.Columns.Remove("ContainerType")
            dt3.Columns.Remove("AvDate")
            dt3.Columns.Remove("Agenti")
            dt3.Columns.Remove("OFFHIRE")
            dt3.Columns.Remove("Grossswt")
            dt3.Columns.Remove("Activity")
            dt3.Columns.Remove("PLSInstuction")
            dt3.Columns.Remove("TAREWt")
            'dt3.Columns.Remove("WashingAmt")
            dt3.Columns.Remove("Estimate_ID")
            dt3.Columns.Remove("PayLoad")
            dt3.Columns.Remove("MFG Date")
            dt3.Columns.Remove("Condition")
            dt3.Columns.Remove("Amount")
            dt3.Columns.Remove("Date")
            dt3.Columns.Remove("Finalstatus")
            dt3.Columns.Remove("OnlineStatus")
            dt3.Columns.Remove("Out Date")
            dt3.Columns.Remove("Booking Remarks")
            dt3.Columns.Remove("Seal Status")
            dt3.Columns.Remove("Grade")

            strSql = ""
            strSql += " SP_Stock_Dar_KMTC '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
            strSql += "'" & Val(ddlLineName.SelectedValue) & "'"
            dt4 = db.sub_GetDatatable(strSql)

            strSql = ""
            strSql += " USP_Inventory_Summary_STOCK '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Val(ddlLineName.SelectedValue) & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
            'strSql += "'" & Val(ddlLineName.SelectedValue) & "'"
            dt5 = db.sub_GetDatatable(strSql)

            strSql = ""
            strSql += " SP_GateLog_KMTC_DAR '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
            strSql += "'" & Val(ddlLineName.SelectedValue) & "'"
            dt6 = db.sub_GetDatatable(strSql)

            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt10.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt1, "IN" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    wb.Worksheets.Add(dt2, "Out" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    wb.Worksheets.Add(dt3, "Dry Inv" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    wb.Worksheets.Add(dt4, "Stock" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'wb.Worksheets.Add(dt5, "Stock Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    wb.Worksheets.Add(dt6, "Gate Log" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        'dt1.Columns(5).ColumnMapping = MappingType.Hidden

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
                        .Cell(6, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        If ddlLineName.SelectedValue = 0 Then
                            .Cell(7, 1).Value = "Line Name: " + "All"
                        ElseIf ddlLineName.SelectedValue > 0 Then
                            .Cell(7, 1).Value = "Line Name: " + ddlLineName.SelectedItem.Text
                        End If

                        .Cell(8, 1).Value = "Gate In"
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
                        .Column(4).Width = 10
                        .Column(5).Width = 10
                        .Column(6).Width = 10
                        .Column(7).Width = 10
                    End With

                    With wb.Worksheets(1)
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
                        .Cell(6, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        If ddlLineName.SelectedValue = 0 Then
                            .Cell(7, 1).Value = "Line Name: " + "All"
                        ElseIf ddlLineName.SelectedValue > 0 Then
                            .Cell(7, 1).Value = "Line Name: " + ddlLineName.SelectedItem.Text
                        End If

                        .Cell(8, 1).Value = "Gate Out"
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
                        .Column(4).Width = 10
                        .Column(5).Width = 10
                        .Column(6).Width = 10
                        .Column(7).Width = 10
                    End With

                    With wb.Worksheets(2)
                        Dim Excelno As String = ""
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
                        If ddlLineName.SelectedValue = 0 Then
                            .Cell(7, 1).Value = "Line Name: " + "All"
                        ElseIf ddlLineName.SelectedValue > 0 Then
                            .Cell(7, 1).Value = "Line Name: " + ddlLineName.SelectedItem.Text
                        End If

                        .Cell(8, 1).Value = "Inventory"
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
                        .Column(4).Width = 10
                        .Column(5).Width = 10
                        .Column(6).Width = 10
                        .Column(7).Width = 10
                    End With

                    With wb.Worksheets(3)
                        Dim Excelno As String = ""
                        Dim i As Integer = 0

                        Rowindex = dt4.Rows.Count
                        i = Rowindex + 5

                        wb.Worksheet(4).Cell(i, 1).InsertTable(dt5)
                        Excelno = db.GetExcelColumnName(dt4.Columns.Count)
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
                        If ddlLineName.SelectedValue = 0 Then
                            .Cell(7, 1).Value = "Line Name: " + "All"
                        ElseIf ddlLineName.SelectedValue > 0 Then
                            .Cell(7, 1).Value = "Line Name: " + ddlLineName.SelectedItem.Text
                        End If

                        .Cell(8, 1).Value = "Stock"
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
                        .Column(2).Width = 10
                        .Column(3).Width = 10
                        .Column(4).Width = 10
                        .Column(5).Width = 10
                        .Column(6).Width = 10
                        .Column(7).Width = 10
                    End With

                    With wb.Worksheets(4)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt6.Columns.Count)
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
                        If ddlLineName.SelectedValue = 0 Then
                            .Cell(7, 1).Value = "Line Name: " + "All"
                        ElseIf ddlLineName.SelectedValue > 0 Then
                            .Cell(7, 1).Value = "Line Name: " + ddlLineName.SelectedItem.Text
                        End If

                        .Cell(8, 1).Value = "Gate Log"
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
                        .Column(4).Width = 10
                        .Column(5).Width = 10
                        .Column(6).Width = 10
                        .Column(7).Width = 10
                    End With


                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=LineDAR" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            strSql = ""
            strSql += " USP_ZIM_STOCK_COUNT_REPORT '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyyMMddHHmmss") & "','" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyyMMddHHmmss") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
            dt = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += " USP_ZIM_INVENTORY_REPORT '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyyMMddHHmmss") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm:ss") & "'"
            dt1 = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += " USP_ZIM_IN_DETAILS '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
            dt2 = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += " USP_ZIM_OUT_DETAILS '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "'"
            dt3 = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt10.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "STOCK")
                    wb.Worksheets.Add(dt1, "INVENTORY")
                    wb.Worksheets.Add(dt2, "IN")
                    wb.Worksheets.Add(dt3, "OUT")

                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        'dt1.Columns(5).ColumnMapping = MappingType.Hidden

                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(5)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("A5:" & Excelno & "5").Merge()

                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Cell(4, 1).Value = "Stock As On: " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(4, 1).Style.Fill.BackgroundColor = XLColor.LightGray

                        '.Cell(3, 1).Value = "TOTAL STOCK"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(2, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(4, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                        .Range("A6:" & Excelno & "6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Range("A6:" & Excelno & "6").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Font.FontSize = 20
                        '.Column(2).Width = 30
                        Dim r As Double = 6 + dt.Rows.Count
                        .Range("A" & r & ":" & Excelno & "" & r & "").Style.Fill.BackgroundColor = XLColor.Yellow
                    End With
                    With wb.Worksheets(1)
                        Dim Excelno As String = ""
                        'dt1.Columns(5).ColumnMapping = MappingType.Hidden

                        Excelno = db.GetExcelColumnName(dt1.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(4)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()

                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        '.Cell(2, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        '.Cell(4, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(3, 1).Style.Fill.BackgroundColor = XLColor.LightGray

                        .Cell(3, 1).Value = "TOTAL STOCK"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                        .Range("A5:" & Excelno & "5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Range("A5:" & Excelno & "5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Font.FontSize = 20
                        '.Column(2).Width = 30
                    End With
                    With wb.Worksheets(2)
                        Dim Excelno As String = ""
                        'dt1.Columns(5).ColumnMapping = MappingType.Hidden

                        Excelno = db.GetExcelColumnName(dt2.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(4)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()

                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        '.Cell(2, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        '.Cell(4, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(3, 1).Style.Fill.BackgroundColor = XLColor.LightGray

                        .Cell(3, 1).Value = "GATE IN MOVEMENTS"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                        .Range("A5:" & Excelno & "5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Range("A5:" & Excelno & "5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Font.FontSize = 20
                        '.Column(2).Width = 30
                    End With
                    With wb.Worksheets(3)
                        Dim Excelno As String = ""
                        'dt1.Columns(5).ColumnMapping = MappingType.Hidden

                        Excelno = db.GetExcelColumnName(dt3.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(4)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()

                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        '.Cell(2, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        '.Cell(4, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(3, 1).Style.Fill.BackgroundColor = XLColor.LightGray

                        .Cell(3, 1).Value = "GATE OUT MOVEMENTS"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

                        .Range("A5:" & Excelno & "5").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Range("A5:" & Excelno & "5").Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(1, 1).Style.Font.FontSize = 20
                        '.Column(2).Width = 30
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=ZIM DAR cum Stock Dated " & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("ddMMyyyy") & ".xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        'getdatewiseWO()
                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            End If
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

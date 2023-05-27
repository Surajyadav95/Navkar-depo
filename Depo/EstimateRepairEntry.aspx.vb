Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports System.Configuration
Imports System.Data.OleDb
Imports System.Globalization

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt10 As DataTable
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete from Temp_Repair_Containers_Import Where userID=" & Session("UserId_DepoCFS") & "")
            Filldropdown()
            txtRepairDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            btnSave_Click(sender, e)
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
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dbl20 As Double = 0, dbl40 As Double = 0, dbl45 As Double = 0
            strSql = ""
            strSql += " USP_ESTIMATE_REPAIR_LIST '" & Trim(ddlcriteria.SelectedValue & "") & "',"
            If ddlcriteria.SelectedValue = 1 Then
                strSql += "'" & Trim(ddlLineName.SelectedValue) & "'"
            ElseIf ddlcriteria.SelectedValue = 2 Then
                strSql += "'" & Trim(txtContainerNo.Text & "") & "'"
            Else
                strSql += "''"
            End If

            dt = db.sub_GetDatatable(strSql)
            grdRegistrationSummary.DataSource = dt
            grdRegistrationSummary.DataBind()
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("Size") = "20" Then
                    dbl20 += 1
                ElseIf dt.Rows(i)("Size") = "40" Then
                    dbl40 += 1
                ElseIf dt.Rows(i)("Size") = "45" Then
                    dbl45 += 1
                End If
            Next
            lbl20.Text = dbl20
            lbl40.Text = dbl40
            lbl45.Text = dbl45
            lblTEUS.Text = Val(dbl20) + Val(dbl40) * 2 + Val(dbl45) * 2
            'up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try

            Dim Name As String = ""
            If ddlcriteria.SelectedValue = 1 Then
                Name = Val(ddlLineName.SelectedValue)
            Else
                Name = ""
            End If

            strSql = ""
            strSql += " USP_ESTIMATE_REPAIR_LIST '" & Trim(ddlcriteria.SelectedValue & "") & "',"
            If ddlcriteria.SelectedValue = 1 Then
                strSql += "'" & Trim(ddlLineName.SelectedValue) & "'"
            ElseIf ddlcriteria.SelectedValue = 2 Then
                strSql += "'" & Trim(txtContainerNo.Text & "") & "'"
            Else
                strSql += "''"
            End If
            dt = db.sub_GetDatatable(strSql)
            dt.Columns.Remove("EntryID")
            dt.Columns.Remove("Estimate_ID")

            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Repair Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(8)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("A5:" & Excelno & "5").Merge()
                        .Range("A6:" & Excelno & "6").Merge()
                        .Range("A7:" & Excelno & "7").Merge()
                        .Range("A8:" & Excelno & "8").Merge()


                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
                        .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
                        .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(7).Height = 20
                        '.Cell(6, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        '.Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        If ddlcriteria.SelectedValue = 0 Then
                            .Cell(6, 1).Value = "All"
                        ElseIf ddlcriteria.SelectedValue = 1 Then
                            .Cell(6, 1).Value = ddlcriteria.SelectedItem.Text + ": " + ddlLineName.SelectedItem.Text
                        ElseIf ddlcriteria.SelectedValue = 2 Then
                            .Cell(6, 1).Value = ddlcriteria.SelectedItem.Text + ": " + txtContainerNo.Text
                        End If

                        .Cell(7, 1).Value = "Repair Details"
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
                        .Cell(7, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(7, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(7, 1).Style.Font.FontSize = 17
                        .Cell(7, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(1, 1).Style.Font.FontSize = 20
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=RepairSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddlLineName.SelectedValue = 0
            txtContainerNo.Text = ""
            If ddlcriteria.SelectedValue = 0 Then
                divShippingLine.Attributes.Add("style", "display:none")
                divContainer.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 1 Then
                divShippingLine.Attributes.Add("style", "display:block")
                divContainer.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 2 Then
                divShippingLine.Attributes.Add("style", "display:none")
                divContainer.Attributes.Add("style", "display:block")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkRepair_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim EntryID As String = lnkcancel.CommandArgument
        
            strSql = ""
            strSql += "SP_Update_Repaired '" & Convert.ToDateTime(Trim(txtRepairDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "',"
            strSql += "" & Session("UserId_DepoCFS") & ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & EntryID & "'"
            db.sub_ExecuteNonQuery(strSql)
            btnSave_Click(sender, e)
            lblSession.Text = "Repaired status updated successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnMultipleRepair_Click(sender As Object, e As EventArgs) Handles btnMultipleRepair.Click
        Try
            For Each row In grdRegistrationSummary.Rows
                Dim chkright As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                If chkright.Checked = True Then
                    strSql = ""
                    strSql += "SP_Update_Repaired '" & Convert.ToDateTime(Trim(txtRepairDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "',"
                    strSql += "" & Session("UserId_DepoCFS") & ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblEntryID"), Label).Text & "") & "'"
                    db.sub_ExecuteNonQuery(strSql)
                End If
            Next

            strSql = ""
            strSql = " Update EYard_stock set RepairedDate ='" & Convert.ToDateTime(Trim(txtRepairDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "' where ContainerNo='" & Trim(txtContainerNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)

            btnSave_Click(sender, e)
            lblSession.Text = "Repaired status updated successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub SaveOk_ServerClick(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("../Depo/EstimateRepairEntry.aspx")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            If FileUpload1.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)

                Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
                lblfilename.Text = "File Name: " + FileName

                Dim FilePath As String = Server.MapPath(FolderPath + FileName)
                If Not ((Extension = ".xls") Or (Extension = ".xlsx")) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Only .xls or .xlsx files are required!');", True)
                    btnUpload.Text = "Import"
                    btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
                    Exit Sub
                End If
                FileUpload1.SaveAs(FilePath)

                Import_To_Grid(FilePath, Extension, sender, e)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please choose file!');", True)
                btnUpload.Text = "Import"
                btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
                Exit Sub
            End If
        Catch ex As Exception
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String, sender As Object, e As EventArgs)
        Try
            Dim conStr As String = ""

            If (Extension = ".xlsx" Or Extension = ".xls") Then
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid File selection please import .csv file');", True)

                ' Exit Sub

                ' csvfile()
                Dim storedProc As String = String.Empty
                storedProc = "spx_ImportFromExcel07"
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString").ConnectionString()
                ' XlsxFile(FilePath, Extension)
                'Exit Sub
                conStr = String.Format(conStr, FilePath, "Yes")
                ' conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & FilePath & ";Extended Properties='Excel 8.0;HDR=YES'"
                Dim connExcel As New OleDbConnection(conStr)

                Dim cmdExcel As New OleDbCommand()

                Dim oda As New OleDbDataAdapter()

                Dim dt As New DataTable()

                cmdExcel.Connection = connExcel

                'Get the name of First Sheet

                connExcel.Open()

                Dim dtExcelSchema As DataTable

                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

                Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()

                connExcel.Close()

                'Read Data from First Sheet

                connExcel.Open()

                cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"

                oda.SelectCommand = cmdExcel

                oda.Fill(dt)

                connExcel.Close()
                File.Delete(FilePath)
                XlsxFile(dt, sender, e)
            End If
            If (Extension = ".csv") Then
                'csvfile()
                Exit Sub
            End If
        Catch ex As Exception
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub XlsxFile(dt As DataTable, sender As Object, e As EventArgs)
        Try
            Dim Rdate As String = ""
            Dim ContainerNo As String = ""
            Dim Vendor As String = ""
            Dim Remarks As String = ""
            Dim strContainer As String = ""
            Dim strContainer1 As String = ""
            Dim strContainer2 As String = ""
            Dim strContainer3 As String = ""
            Dim strContainer4 As String = ""
            Dim strmessage As String = ""
            Dim formats() As String = {"dd-MM-yyyy", "yyyy-MM-dd", "dd/MM/yyyy", "yyyy/MM/dd", "dd-MM-yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "dd/MM/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "dd-MM-yyyy HH:mm:ss tt", "yyyy-MM-dd HH:mm:ss tt", "dd/MM/yyyy HH:mm:ss tt", "yyyy/MM/dd HH:mm:ss tt", "dd-MM-yyyy hh:mm:ss tt", "yyyy-MM-dd hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt", "yyyy/MM/dd hh:mm:ss tt"}
            If (dt.Rows.Count > 0) Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Trim(dt.Rows(i).ItemArray(0).ToString()) <> "" Then
                        If Len(Trim(dt.Rows(i).ItemArray(0).ToString())) <> 11 Then
                            If strContainer = "" Then
                                strContainer = Trim(dt.Rows(i).ItemArray(0).ToString())
                            Else
                                strContainer += "," + Trim(dt.Rows(i).ItemArray(0).ToString())
                            End If
                        End If
                        Dim dtdate As DateTime
                        If DateTime.TryParseExact(Trim(dt.Rows(i).ItemArray(1).ToString()), formats, New CultureInfo("en-GB"), DateTimeStyles.None, dtdate) Then
                        Else
                            If strContainer1 = "" Then
                                strContainer1 = Trim(dt.Rows(i).ItemArray(0).ToString())
                            Else
                                If Not InStr(strContainer1, Trim(dt.Rows(i).ItemArray(0).ToString())) > 0 Then
                                    strContainer1 += "," + Trim(dt.Rows(i).ItemArray(0).ToString())
                                End If
                            End If
                        End If
                        strSql = ""
                        strSql += "select * from estimate_m where containerno='" & Trim(dt.Rows(i).ItemArray(0).ToString()) & "' and Iscancel=0"
                        dt1 = db.sub_GetDatatable(strSql)
                        If Not dt1.Rows.Count > 0 Then
                            If strContainer2 = "" Then
                                strContainer2 = Trim(dt.Rows(i).ItemArray(0).ToString())
                            Else
                                If Not InStr(strContainer2, Trim(dt.Rows(i).ItemArray(0).ToString())) > 0 Then
                                    strContainer2 += "," + Trim(dt.Rows(i).ItemArray(0).ToString())
                                End If
                            End If
                        End If
                        strSql = ""
                        strSql += "select * from Vendor_m where Name='" & Trim(dt.Rows(i).ItemArray(2).ToString()) & "'"
                        dt1 = db.sub_GetDatatable(strSql)
                        If Not dt1.Rows.Count > 0 Then
                            If strContainer3 = "" Then
                                strContainer3 = Trim(dt.Rows(i).ItemArray(0).ToString())
                            Else
                                If Not InStr(strContainer3, Trim(dt.Rows(i).ItemArray(0).ToString())) > 0 Then
                                    strContainer3 += "," + Trim(dt.Rows(i).ItemArray(0).ToString())
                                End If
                            End If
                        End If
                    End If
                Next
                If Not (strContainer = "" And strContainer1 = "" And strContainer2 = "" And strContainer3 = "") Then
                    If Not strContainer = "" Then
                        strmessage += "Invalid container no length for " & strContainer & "\n"
                    End If
                    If Not strContainer1 = "" Then
                        strmessage += "Invalid date format for " & strContainer1 & "\n"
                    End If
                    If Not strContainer2 = "" Then
                        strmessage += "No records in estimate found for " & strContainer2 & "\n"
                    End If
                    If Not strContainer3 = "" Then
                        strmessage += "Vendor not found for " & strContainer3 & "\n"
                    End If
                    strmessage += "Please correct records and import again."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" & strmessage & "');", True)
                    Clear()
                    btnUpload.Text = "Import"
                    btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
                    Exit Sub
                End If
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Trim(dt.Rows(i).ItemArray(0).ToString()) <> "" Then
                        ContainerNo = dt.Rows(i).ItemArray(0).ToString()
                        Rdate = dt.Rows(i).ItemArray(1).ToString()
                        Vendor = dt.Rows(i).ItemArray(2).ToString()
                        Remarks = dt.Rows(i).ItemArray(3).ToString()

                        strSql = ""
                        strSql += "USP_INSERT_INTO_TEMP_REPAIR_CONTAINERS_IMPORT '" & Trim(ContainerNo) & "','" & Convert.ToDateTime(Trim(Rdate)).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(Vendor) & "','" & Replace(Trim(Remarks), "'", "''") & "','" & Session("UserId_DepoCFS") & "'"
                        db.sub_ExecuteNonQuery(strSql)

                        ContainerNo = ""
                        Rdate = ""
                        Vendor = ""
                        Remarks = ""
                    End If
                Next
            End If

            strSql = ""
            strSql += "USP_UPDATE_REPAIRED_DETAILS_IMPORT " & Session("UserId_DepoCFS") & ""
            db.sub_ExecuteNonQuery(strSql)
            Clear()
            btnSave_Click(sender, e)
            lblSession.Text = "Record successfully imported "
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Clear()
        lblfilename.Text = ""
        Lblfile.Text = ""
        db.sub_ExecuteNonQuery("Delete from Temp_Repair_Containers_Import Where userID=" & Session("UserId_DepoCFS") & "")
    End Sub

    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select '' [Container No],'' [Repaired Date (yyyy-MM-dd)],'' Vendor,'' Remarks"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Repair Container")
                    With wb.Worksheets(0)
                        .Column(2).Style.DateFormat.Format = "yyyy-MM-dd"
                    End With
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=RepairContainerTemplate.xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
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

Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel
Imports System.Globalization

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt10 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserIDPRE_Bond") Is Nothing Then
        '    Session("UserIDPRE_Bond") = Request.Cookies("UserIDPRE_Bond").Value
        '    'Session("Dept") = Request.Cookies("Dept").Value
        '    Session("UserNamePRE_Bond") = Request.Cookies("UserNamePRE_Bond").Value
        '    ''Session("PROFILEURL") = Request.Cookies("PROFILEURL").Value
        '    'Session("Location") = Request.Cookies("Location").Value
        '    ''Session("LOcationId") = Request.Cookies("LOcationId").Value
        '    'Session("ID") = Response.Cookies("ID").Value
        '    'Session("CompID") = Response.Cookies("CompID").Value
        '    'Session("Workyear") = Response.Cookies("Workyear").Value
        'End If
        If Not IsPostBack Then            
            sub_CreateTable()
        End If
    End Sub
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable

        dtDepoContainer.Columns.Clear()

        Session("table_DepoContainer") = ""

        dtDepoContainer.Columns.Add("ContainerNo")
        dtDepoContainer.Columns.Add("SizeType")
        dtDepoContainer.Columns.Add("TotalRep")
        dtDepoContainer.Columns.Add("LineLAB")
        dtDepoContainer.Columns.Add("LineMat")
        dtDepoContainer.Columns.Add("LineTotal")
        dtDepoContainer.Columns.Add("DD")
        dtDepoContainer.Columns.Add("Remark")
        dtDepoContainer.Columns.Add("Remarks")


        Dim dtRow2 As DataRow = dtDepoContainer.NewRow

        grdOutDets.DataSource = Nothing
        grdOutDets.DataSource = dtDepoContainer
        grdOutDets.DataBind()
        Session("table_DepoContainer") = dtDepoContainer

    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dtDepoContainer As New DataTable
            Dim intRows As Integer = 0
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)

            If Not dtDepoContainer.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Import Containers first');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If
            Dim param As New SqlParameter()
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@UserID", Session("UserId_DepoCFS"))

            param.ParameterName = "@PT_MNRESTIMATEDOWNLOADLTotal"
            param.Value = dtDepoContainer
            param.TypeName = "PT_MNRESTIMATEDOWNLOADLTotal"
            param.SqlDbType = SqlDbType.Structured
            cmd.Parameters.Add(param)
            Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
            Dim con As New SqlConnection(strConnString)
            cmd.CommandText = "USP_SAVE_ESTIMATES_DOWNLOAD_Total"
            cmd.Connection = con
            Dim da As New SqlDataAdapter()
            da.SelectCommand = cmd
            Dim dtMNR As New DataTable

            Try
                con.Open()
                da.Fill(dtMNR)
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                con.Close()
                con.Dispose()
            End Try

            If dtMNR.Rows.Count > 0 Then
                If dtMNR.Rows(0)(0) > 0 Then
                    Control_Clear()
                    btnSave.Text = "Save"
                    btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                    lblSession.Text = "Record saved successfully "
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                    UpdatePanel3.Update()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not saved successfully');", True)
                    btnSave.Text = "Save"
                    btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                    Exit Sub
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not saved successfully');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
        End Try
    End Sub
    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select '' [Container No],'' [Size/Type],'' [Total Rep],'' [Line LAB],'' [Line Mat],'' [Line Total],'' [DD],'' [Remark],'' [Remarks]"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Upload Cost MNR")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'With wb.Worksheets(0)
                    '    .Column(3).Style.DateFormat.Format = "yyyy-MM-dd"
                    '    .Column(9).Style.DateFormat.Format = "yyyy-MM-dd"
                    '    .Column(10).Style.DateFormat.Format = "yyyy-MM-dd"
                    'End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=UploadCostMNR.xlsx")
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
    Private Sub Upload(sender As Object, e As EventArgs, FilePath As String)
        Try
            Dim intRows As Integer = 0
            Dim dtDepoContainer As New DataTable
            Dim strContainer As String = ""
            Dim strContainer1 As String = ""
            Dim strContainer2 As String = ""
            Dim strContainer3 As String = ""
            Dim formats() As String = {"dd-MM-yyyy", "yyyy-MM-dd", "dd/MM/yyyy", "yyyy/MM/dd", "dd-MM-yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "dd/MM/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "dd-MM-yyyy HH:mm:ss tt", "yyyy-MM-dd HH:mm:ss tt", "dd/MM/yyyy HH:mm:ss tt", "yyyy/MM/dd HH:mm:ss tt", "dd-MM-yyyy hh:mm:ss tt", "yyyy-MM-dd hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt", "yyyy/MM/dd hh:mm:ss tt"}
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            Dim int20 As Integer = 0, int40 As Integer = 0, int45 As Integer = 0, intTues As Integer = 0
            If FileUpload1.HasFile Then
                'Dim filePath As String = FileUpload1.FileName
                Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
                lblfilename.Text = "File Name: " + FileName
                'Dim FilePath As String = Server.MapPath(FolderPath + FileName)
                Using workBook As New XLWorkbook(FilePath)
                    Dim workSheet As IXLWorksheet = workBook.Worksheet(1)
                    Dim firstRow As Boolean = True
                    For Each row As IXLRow In workSheet.Rows()
                        If Not Trim(row.Cell(1).Value.ToString()) = "" Then
                            If Not firstRow Then
                                'strSql = ""
                                'strSql += "USP_VALIDATION_FOR_MNR_DOWNLOAD '" & Trim(row.Cell(1).Value.ToString()) & "'"
                                'ds = db.sub_GetDataSets(strSql)
                                'If Not ds.Tables(0).Rows.Count > 0 Then
                                '    If strContainer1 = "" Then
                                '        strContainer1 = Trim(row.Cell(1).Value.ToString())
                                '    Else
                                '        If Not InStr(strContainer1, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                '            strContainer1 += "," + Trim(row.Cell(1).Value.ToString())
                                '        End If
                                '    End If
                                '    GoTo lblnext
                                'End If
                                'If ds.Tables(1).Rows.Count > 0 Then
                                '    If strContainer3 = "" Then
                                '        strContainer3 = Trim(row.Cell(1).Value.ToString())
                                '    Else
                                '        If Not InStr(strContainer3, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                '            strContainer3 += "," + Trim(row.Cell(1).Value.ToString())
                                '        End If
                                '    End If
                                '    GoTo lblnext
                                'End If
                                'If Len(Trim(row.Cell(1).Value.ToString() & "")) <> "11" Then
                                '    If strContainer1 = "" Then
                                '        strContainer1 = Trim(row.Cell(1).Value.ToString())
                                '    Else
                                '        If Not InStr(strContainer1, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                '            strContainer1 += "," + Trim(row.Cell(1).Value.ToString())
                                '        End If
                                '    End If
                                '    GoTo lblnext
                                'End If
                                'Dim dtdate As DateTime
                                'If DateTime.TryParseExact(Trim(row.Cell(3).Value.ToString()), formats, New CultureInfo("en-GB"), DateTimeStyles.None, dtdate) Then
                                'Else
                                '    If strContainer2 = "" Then
                                '        strContainer2 = Trim(row.Cell(1).Value.ToString())
                                '    Else
                                '        If Not InStr(strContainer2, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                '            strContainer2 += "," + Trim(row.Cell(1).Value.ToString())
                                '        End If
                                '    End If
                                '    GoTo lblnext
                                'End If
                                'If Not Trim(row.Cell(9).Value.ToString()) = "" Then
                                '    If DateTime.TryParseExact(Trim(row.Cell(9).Value.ToString()), formats, New CultureInfo("en-GB"), DateTimeStyles.None, dtdate) Then
                                '    Else
                                '        If strContainer2 = "" Then
                                '            strContainer2 = Trim(row.Cell(1).Value.ToString())
                                '        Else
                                '            If Not InStr(strContainer2, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                '                strContainer2 += "," + Trim(row.Cell(1).Value.ToString())
                                '            End If
                                '        End If
                                '        GoTo lblnext
                                '    End If
                                'End If
                                'If Not Trim(row.Cell(9).Value.ToString()) = "" Then
                                '    If DateTime.TryParseExact(Trim(row.Cell(10).Value.ToString()), formats, New CultureInfo("en-GB"), DateTimeStyles.None, dtdate) Then
                                '    Else
                                '        If strContainer2 = "" Then
                                '            strContainer2 = Trim(row.Cell(1).Value.ToString())
                                '        Else
                                '            If Not InStr(strContainer2, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                '                strContainer2 += "," + Trim(row.Cell(1).Value.ToString())
                                '            End If
                                '        End If
                                '        GoTo lblnext
                                '    End If
                                'End If
                                Dim dtRow As DataRow = dtDepoContainer.NewRow
                                dtRow.Item("ContainerNo") = row.Cell(1).Value.ToString()
                                dtRow.Item("SizeType") = row.Cell(2).Value.ToString()
                                dtRow.Item("TotalRep") = row.Cell(3).Value.ToString()
                                dtRow.Item("LineLAB") = row.Cell(4).Value.ToString()
                                dtRow.Item("LineMat") = row.Cell(5).Value.ToString()
                                dtRow.Item("LineTotal") = row.Cell(6).Value.ToString()
                                dtRow.Item("DD") = row.Cell(7).Value.ToString()
                                dtRow.Item("Remark") = row.Cell(8).Value.ToString()
                                dtRow.Item("Remarks") = row.Cell(9).Value.ToString()
                                'If Trim(row.Cell(9).Value.ToString()) = "" Then
                                '    dtRow.Item("ApprovedDate") = ""
                                'Else
                                '    dtRow.Item("ApprovedDate") = Convert.ToDateTime(row.Cell(9).Value).ToString("yyyy-MM-dd")
                                'End If
                                'If Trim(row.Cell(10).Value.ToString()) = "" Then
                                '    dtRow.Item("RepairedDate") = ""
                                'Else
                                '    dtRow.Item("RepairedDate") = Convert.ToDateTime(row.Cell(10).Value).ToString("yyyy-MM-dd")
                                'End If
                                dtDepoContainer.Rows.Add(dtRow)
                            Else
                                firstRow = False
                            End If
                        End If
lblnext:
                    Next
                End Using
                File.Delete(FilePath)
            End If
            'If Not (strContainer1 = "" And strContainer2 = "" And strContainer3 = "") Then
            '    If Not strContainer1 = "" Then
            '        strContainer += "Container not found or length not match for " & strContainer1 & "\n"
            '    End If
            '    If Not strContainer2 = "" Then
            '        strContainer += "Invalid date format for " & strContainer2 & "\n"
            '    End If
            '    If Not strContainer3 = "" Then
            '        strContainer += "Estimation already done for " & strContainer3 & "\n"
            '    End If
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" & strContainer & "');", True)
            'End If

            grdOutDets.DataSource = Nothing
            grdOutDets.DataSource = dtDepoContainer
            grdOutDets.DataBind()
            For j = 0 To dtDepoContainer.Rows.Count - 1
                If InStr(Trim(dtDepoContainer.Rows(j)("SizeType")), "20") > 0 Then
                    int20 = int20 + 1
                ElseIf InStr(Trim(dtDepoContainer.Rows(j)("SizeType")), "40") Then
                    int40 = int40 + 1
                ElseIf InStr(Trim(dtDepoContainer.Rows(j)("SizeType")), "45") Then
                    int45 = int45 + 1
                End If
            Next
            intTues = int20 + int40 + int45
            lbl20.Text = int20
            lbl40.Text = int40
            lbl45.Text = int45
            lblTEUS.Text = intTues

            'Dim LabourTotal As Double = 0
            'For I = 0 To dtDepoContainer.Rows.Count - 1
            '    If Trim(dtDepoContainer.Rows(I)("LabourAmount")) Then
            '        LabourTotal += Trim(dtDepoContainer.Rows(I)("LabourAmount"))
            '    End If
            'Next
            'lblLabour.Text = LabourTotal

            'Dim MaterialTotal As Double = 0
            'For I = 0 To dtDepoContainer.Rows.Count - 1
            '    If Trim(dtDepoContainer.Rows(I)("MaterialAmount")) Then
            '        MaterialTotal += Trim(dtDepoContainer.Rows(I)("MaterialAmount"))
            '    End If
            'Next
            'lblMaterial.Text = MaterialTotal

            'Dim CleaningTotal As Double = 0
            'For I = 0 To dtDepoContainer.Rows.Count - 1
            '    If Trim(dtDepoContainer.Rows(I)("CleaningAmount")) Then
            '        CleaningTotal += Trim(dtDepoContainer.Rows(I)("CleaningAmount"))
            '    End If
            'Next
            'lblCleaning.Text = CleaningTotal
            Session("table_DepoContainer") = dtDepoContainer
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
                Upload(sender, e, FilePath)
                'Import_To_Grid(FilePath, Extension)
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

    Protected Sub XlsxFile(dt As DataTable)
        Try
            Dim BookingNo As String = ""
            Dim ContainerNo As String = ""
            Dim WagonNo As String = ""
            Dim POD As String = ""

            Dim strContainer As String = ""
            Dim strContainer1 As String = ""
            If (dt.Rows.Count > 0) Then
                For i As Integer = 0 To dt.Rows.Count - 1

                    If Trim(dt.Rows(i).ItemArray(0).ToString()) <> "" Then
                        strSql = ""
                        strSql += "USP_VALIDATION_OUT_BY_RAIL '" & Trim(dt.Rows(i).ItemArray(0).ToString()) & "','" & Trim(dt.Rows(i).ItemArray(2).ToString()) & "'"
                        ds = db.sub_GetDataSets(strSql)
                        If ds.Tables(0).Rows.Count > 0 And ds.Tables(1).Rows.Count > 0 Then
                            If Val(ds.Tables(0).Rows(0)("LineID") & "") <> Val(ds.Tables(1).Rows(0)("LineID")) Then
                                If strContainer1 = "" Then
                                    strContainer1 = Trim(dt.Rows(i).ItemArray(0).ToString())
                                Else
                                    If Not InStr(strContainer1, Trim(dt.Rows(i).ItemArray(0).ToString())) > 0 Then
                                        strContainer1 += "," + Trim(dt.Rows(i).ItemArray(0).ToString())
                                    End If
                                End If
                            End If
                        Else
                            If strContainer1 = "" Then
                                strContainer1 = Trim(dt.Rows(i).ItemArray(0).ToString())
                            Else
                                If Not InStr(strContainer1, Trim(dt.Rows(i).ItemArray(0).ToString())) > 0 Then
                                    strContainer1 += "," + Trim(dt.Rows(i).ItemArray(0).ToString())
                                End If
                            End If
                        End If
                    End If
                Next
                If Not strContainer1 = "" Then
                    If Not strContainer1 = "" Then
                        strContainer += "Line Name not Matching with booking entry for " & strContainer1 & "\n"
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" & strContainer & "');", True)
                    CLear()
                    btnUpload.Text = "Import"
                    btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
                    Exit Sub
                End If


                For i As Integer = 0 To dt.Rows.Count - 1
                    If Trim(dt.Rows(i).ItemArray(0).ToString()) <> "" Then
                        strSql = ""
                        strSql += "select * from Temp_Out_by_Rail WHERE UserID='" & Session("UserId_DepoCFS") & "' and ContainerNo='" & dt.Rows(i).ItemArray(0).ToString() & "'"
                        dt1 = db.sub_GetDatatable(strSql)
                        If dt1.Rows.Count > 0 Then
                            GoTo NextRecord
                        End If
                        ContainerNo = dt.Rows(i).ItemArray(0).ToString()
                        WagonNo = dt.Rows(i).ItemArray(1).ToString()
                        BookingNo = dt.Rows(i).ItemArray(2).ToString()
                        POD = dt.Rows(i).ItemArray(3).ToString()
                        strSql = ""
                        strSql += "USP_INSERT_TEMP_OUT_BY_RAIL '" & Trim(ContainerNo) & "','" & Trim(BookingNo) & "','" & Trim(WagonNo) & "','" & Trim(POD) & "','" & Session("UserId_DepoCFS") & "'"
                        db.sub_ExecuteNonQuery(strSql)

                        BookingNo = ""
                        ContainerNo = ""
                        WagonNo = ""
                        POD = ""
                    End If
NextRecord:
                Next
            End If
            FillGrid()
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record imported successfully');", True)
        Catch ex As Exception
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub CLear()
        db.sub_ExecuteNonQuery("delete from Temp_Out_by_Rail where UserID=" & Session("UserId_DepoCFS") & "")
    End Sub
    Private Sub FillGrid()
        Try
            Dim int20 As Integer = 0, int40 As Integer = 0, int45 As Integer = 0, intTues As Integer = 0
            strSql = ""
            strSql += "USP_FILL_GRID_OUT_BY_RAIL " & Session("UserId_DepoCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            grdOutDets.DataSource = dt
            grdOutDets.DataBind()
            For j = 0 To dt.Rows.Count - 1
                If Trim(dt.Rows(j)("Size") = "20") Then
                    int20 = int20 + 1
                ElseIf Trim(dt.Rows(j)("Size") = "40") Then
                    int40 = int40 + 1
                ElseIf Trim(dt.Rows(j)("Size") = "45") Then
                    int45 = int45 + 1
                End If
            Next
            intTues = int20 + (2 * int40) + (2 * int45)
            lbl20.Text = int20
            lbl40.Text = int40
            lbl45.Text = int45
            lblTEUS.Text = intTues
        Catch ex As Exception
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Control_Clear()
        sub_CreateTable()
        btnUpload.Text = "Save"
        btnUpload.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
        btnUpload.Text = "Import"
        btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim dtDepoContainer As New DataTable
            dtDepoContainer = Session("table_DepoContainer")

            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dtDepoContainer.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dtDepoContainer, "MNR Estimate" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dtDepoContainer.Columns.Count)
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

                        .Cell(7, 1).Value = "MNR Estimate"
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
                    Response.AddHeader("content-disposition", "attachment;filename=MNREstimate" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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

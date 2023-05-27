Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.IO
Imports ClosedXML.Excel

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
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
            db.sub_ExecuteNonQuery("delete from Temp_Out_by_Rail where UserID=" & Session("UserId_DepoCFS") & "")
            txtOutDateTime.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            FillGrid()
            Filldropdown()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try


        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Val(lblTEUS.Text) > 90 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Only 90 Teus can Gate In in one time again train no. Cannot proceed!');", True)
                txtTrainNo.Focus()
                Exit Sub
            End If
            Dim strContainer As String = ""
            Dim strContainer1 As String = ""
            Dim strContainer2 As String = ""
            Dim dblGPNo As Double = 0

            For Each row In grdOutDets.Rows
                strSql = ""
                strSql += "USP_VALIDATION_SAVE_OUT_BY_RAIL '" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblEntryID"), Label).Text & "") & "'"
                ds = db.sub_GetDataSets(strSql)
                If ds.Tables(1).Rows.Count > 0 Then
                    If strContainer1 = "" Then
                        strContainer1 = Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "")
                    Else
                        If Not InStr(strContainer1, Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "")) > 0 Then
                            strContainer1 += "," + Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "")
                        End If
                    End If
                End If
                If ds.Tables(2).Rows.Count > 0 Then
                    If strContainer1 = "" Then
                        strContainer1 = Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "")
                    Else
                        If Not InStr(strContainer1, Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "")) > 0 Then
                            strContainer1 += "," + Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "")
                        End If
                    End If
                End If
                If ds.Tables(3).Rows.Count > 0 Then
                    If strContainer2 = "" Then
                        strContainer2 = Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "")
                    Else
                        If Not InStr(strContainer2, Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "")) > 0 Then
                            strContainer2 += "," + Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "")
                        End If
                    End If
                End If
            Next
            If Not (strContainer1 = "" And strContainer2 = "") Then
                If Not strContainer1 = "" Then
                    strContainer += "Containers on hold -" & strContainer1 & "\n"
                End If
                If Not strContainer2 = "" Then
                    strContainer += "Containers already gated out -" & strContainer2 & ""
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" & strContainer & "');", True)
                btnUpload.Text = "Save"
                btnUpload.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_EYARD_SAVE_OUT_BY_RAIL '" & Convert.ToDateTime(Trim(txtOutDateTime.Text)).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtTrainNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)(0) > 0 Then
                    Control_Clear()
                    btnUpload.Text = "Save"
                    btnUpload.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                    lblSession.Text = "Record saved successfully "
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                    UpdatePanel3.Update()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not saved successfully');", True)
                    btnUpload.Text = "Save"
                    btnUpload.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                    Exit Sub
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not saved successfully');", True)
                btnUpload.Text = "Save"
                btnUpload.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
            btnUpload.Text = "Save"
            btnUpload.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
        End Try
    End Sub
    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select '' [Container No],'' [Wagon No],'' [Booking No],'' [POD]"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Out by Rail Template")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")

                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=OutByRailTemplate.xlsx")
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
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            CLear()
            FillGrid()
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
                XlsxFile(FilePath)
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
    'Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String)
    '    Try
    '        Dim conStr As String = ""

    '        If (Extension = ".xlsx" Or Extension = ".xls") Then
    '            ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid File selection please import .csv file');", True)

    '            ' Exit Sub

    '            ' csvfile()
    '            Dim storedProc As String = String.Empty
    '            storedProc = "spx_ImportFromExcel07"
    '            conStr = ConfigurationManager.ConnectionStrings("Excel07ConString").ConnectionString()
    '            ' XlsxFile(FilePath, Extension)
    '            'Exit Sub
    '            conStr = String.Format(conStr, FilePath, "Yes")
    '            ' conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & FilePath & ";Extended Properties='Excel 8.0;HDR=YES'"
    '            Dim connExcel As New OleDbConnection(conStr)

    '            Dim cmdExcel As New OleDbCommand()

    '            Dim oda As New OleDbDataAdapter()

    '            Dim dt As New DataTable()

    '            cmdExcel.Connection = connExcel

    '            'Get the name of First Sheet

    '            connExcel.Open()

    '            Dim dtExcelSchema As DataTable

    '            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

    '            Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()

    '            connExcel.Close()

    '            'Read Data from First Sheet

    '            connExcel.Open()

    '            cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"

    '            oda.SelectCommand = cmdExcel

    '            oda.Fill(dt)

    '            connExcel.Close()
    '            File.Delete(FilePath)
    '            XlsxFile(dt)
    '        End If
    '        If (Extension = ".csv") Then
    '            'csvfile()
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        btnUpload.Text = "Import"
    '        btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    End Try
    'End Sub
    Protected Sub XlsxFile(FilePath As String)
        Try
            Dim BookingNo As String = ""
            Dim ContainerNo As String = ""
            Dim WagonNo As String = ""
            Dim POD As String = ""

            Dim strContainer As String = ""
            Dim strContainer1 As String = ""
            Using workBook As New XLWorkbook(FilePath)
                Dim workSheet As IXLWorksheet = workBook.Worksheet(1)
                Dim firstRow As Boolean = True
                For Each row As IXLRow In workSheet.Rows()
                    If Not firstRow Then
                        If Trim(row.Cell(1).Value.ToString()) <> "" Then
                            strSql = ""
                            strSql += "USP_VALIDATION_OUT_BY_RAIL '" & Trim(row.Cell(1).Value.ToString()) & "','" & Trim(row.Cell(3).Value.ToString()) & "'"
                            ds = db.sub_GetDataSets(strSql)
                            If ds.Tables(0).Rows.Count > 0 And ds.Tables(1).Rows.Count > 0 Then
                                If Val(ds.Tables(0).Rows(0)("LineID") & "") <> Val(ds.Tables(1).Rows(0)("LineID")) Then
                                    If strContainer1 = "" Then
                                        strContainer1 = Trim(row.Cell(1).Value.ToString())
                                    Else
                                        If Not InStr(strContainer1, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                            strContainer1 += "," + Trim(row.Cell(1).Value.ToString())
                                        End If
                                    End If
                                End If
                            Else
                                If strContainer1 = "" Then
                                    strContainer1 = Trim(row.Cell(1).Value.ToString())
                                Else
                                    If Not InStr(strContainer1, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                        strContainer1 += "," + Trim(row.Cell(1).Value.ToString())
                                    End If
                                End If
                            End If
                        End If
                    Else
                        firstRow = False
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
                firstRow = True
                For Each row As IXLRow In workSheet.Rows()
                    If Not firstRow Then
                        If Trim(row.Cell(1).Value.ToString()) <> "" Then
                            strSql = ""
                            strSql += "select * from Temp_Out_by_Rail WHERE UserID='" & Session("UserId_DepoCFS") & "' and ContainerNo='" & Trim(row.Cell(1).Value.ToString()) & "'"
                            dt1 = db.sub_GetDatatable(strSql)
                            If dt1.Rows.Count > 0 Then
                                GoTo NextRecord
                            End If
                            ContainerNo = Trim(row.Cell(1).Value.ToString())
                            WagonNo = Trim(row.Cell(2).Value.ToString())
                            BookingNo = Trim(row.Cell(3).Value.ToString())
                            POD = Trim(row.Cell(4).Value.ToString())
                            strSql = ""
                            strSql += "USP_INSERT_TEMP_OUT_BY_RAIL '" & Trim(ContainerNo) & "','" & Trim(BookingNo) & "','" & Trim(WagonNo) & "','" & Trim(POD) & "','" & Session("UserId_DepoCFS") & "'"
                            db.sub_ExecuteNonQuery(strSql)

                            BookingNo = ""
                            ContainerNo = ""
                            WagonNo = ""
                            POD = ""
                        End If
NextRecord:
                    Else
                        firstRow = False
                    End If
                Next
            End Using
            File.Delete(FilePath)
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
        FillGrid()
        db.sub_ExecuteNonQuery("delete from Temp_Out_by_Rail where UserID=" & Session("UserId_DepoCFS") & "")
        txtOutDateTime.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
        txtTrainNo.Text = ""
        btnUpload.Text = "Save"
        btnUpload.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
        btnUpload.Text = "Import"
        btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
    End Sub
End Class

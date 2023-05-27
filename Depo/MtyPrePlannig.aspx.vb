Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.IO
Imports ClosedXML.Excel
Imports System.Globalization

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete from Temp_MTY_Pre_Planning_Import Where userID=" & Session("UserId_DepoCFS") & "")
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            btnSave_Click(sender, e)
        End If
    End Sub
    Public Sub Fillgrid()
        strSql = ""
        strSql += " USP_MTY_PRE_PLANNING_SUMMARY '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtContainerNo.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
        up_grid.Update()
    End Sub

    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Fillgrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdcontainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdcontainer.DataSource = dt
        grdcontainer.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
    End Sub

    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            Try
                strSql = ""
                strSql += "Select '' [Container No],'' [POD],'' [POL],'' [Vessel Name],'' [Cut of Date & Time],'' [Voyage No]"
                dt = db.sub_GetDatatable(strSql)

                If (dt.Rows.Count > 0) Then
                    Using wb As New XLWorkbook()
                        wb.Worksheets.Add(dt, "MTY Pre Planning")
                        'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                        Response.Clear()
                        Response.Buffer = True
                        Response.Charset = ""
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                        Response.AddHeader("content-disposition", "attachment;filename=MTYPrePlanningTemplate.xlsx")
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
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            Try
                If FileUpload1.HasFile Then
                    Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                    Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)

                    Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
                    'lblfilename.Text = "File Name: " + FileName


                    Dim FilePath As String = Server.MapPath(FolderPath + FileName)
                    If Not ((Extension = ".xls") Or (Extension = ".xlsx")) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Only .xls or .xlsx files are required!');", True)
                        btnUpload.Text = "Import"
                        btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
                        Exit Sub
                    End If
                    FileUpload1.SaveAs(FilePath)

                    Import_To_Grid(FilePath, Extension)
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
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String)
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
                XlsxFile(dt)
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
    Protected Sub XlsxFile(dt As DataTable)
        Try
            Dim ContainerNo As String = ""
            Dim POD As String = ""
            Dim POL As String = ""
            Dim VesselName As String = ""
            Dim CutDateTime As String = ""
            Dim VoyageNo As String = ""
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
                        If DateTime.TryParseExact(Trim(dt.Rows(i).ItemArray(4).ToString()), formats, New CultureInfo("en-GB"), DateTimeStyles.None, dtdate) Then
                        Else
                            If strContainer1 = "" Then
                                strContainer1 = Trim(dt.Rows(i).ItemArray(4).ToString())
                            Else
                                If Not InStr(strContainer1, Trim(dt.Rows(i).ItemArray(4).ToString())) > 0 Then
                                    strContainer1 += "," + Trim(dt.Rows(i).ItemArray(4).ToString())
                                End If
                            End If
                        End If
                        strSql = ""
                        strSql += "USP_VALIDATION_FOR_MTY_PRE_PLANNING '" & Trim(dt.Rows(i).ItemArray(0).ToString()) & "'"
                        ds = db.sub_GetDataSets(strSql)
                        If Not ds.Tables(0).Rows.Count > 0 Then
                            If strContainer2 = "" Then
                                strContainer2 = Trim(dt.Rows(i).ItemArray(0).ToString())
                            Else
                                If Not InStr(strContainer2, Trim(dt.Rows(i).ItemArray(0).ToString())) > 0 Then
                                    strContainer2 += "," + Trim(dt.Rows(i).ItemArray(0).ToString())
                                End If
                            End If
                        End If
                        If Trim(ds.Tables(0).Rows(0)("Status")) = "O" Then
                            If strContainer3 = "" Then
                                strContainer3 = Trim(dt.Rows(i).ItemArray(0).ToString())
                            Else
                                If Not InStr(strContainer3, Trim(dt.Rows(i).ItemArray(0).ToString())) > 0 Then
                                    strContainer3 += "," + Trim(dt.Rows(i).ItemArray(0).ToString())
                                End If
                            End If
                        End If                        
                        If (ds.Tables(1).Rows.Count > 0 Or ds.Tables(2).Rows.Count > 0) Then
                            If strContainer4 = "" Then
                                strContainer4 = Trim(dt.Rows(i).ItemArray(0).ToString())
                            Else
                                If Not InStr(strContainer4, Trim(dt.Rows(i).ItemArray(0).ToString())) > 0 Then
                                    strContainer4 += "," + Trim(dt.Rows(i).ItemArray(0).ToString())
                                End If
                            End If
                        End If
                    End If
                Next
                If Not (strContainer = "" And strContainer1 = "" And strContainer2 = "" And strContainer3 = "" And strContainer4 = "") Then
                    If Not strContainer = "" Then
                        strmessage += "Invalid container no length for " & strContainer & "\n"
                    End If
                    If Not strContainer1 = "" Then
                        strmessage += "Invalid date format for " & strContainer1 & "\n"
                    End If
                    If Not strContainer2 = "" Then
                        strmessage += "No records found for " & strContainer2 & "\n"
                    End If
                    If Not strContainer3 = "" Then
                        strmessage += "Already Out -" & strContainer3 & "\n"
                    End If
                    If Not strContainer4 = "" Then
                        strmessage += "Containers are on hold -" & strContainer4 & "\n"
                    End If
                    strmessage += "Please correct records and import again."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" & strmessage & "');", True)
                    btnUpload.Text = "Import"
                    btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
                    Exit Sub
                End If
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Trim(dt.Rows(i).ItemArray(0).ToString()) <> "" Then
                        ContainerNo = dt.Rows(i).ItemArray(0).ToString()
                        POD = dt.Rows(i).ItemArray(1).ToString()
                        POL = dt.Rows(i).ItemArray(2).ToString()
                        VesselName = dt.Rows(i).ItemArray(3).ToString()
                        CutDateTime = dt.Rows(i).ItemArray(4).ToString()
                        VoyageNo = dt.Rows(i).ItemArray(5).ToString()

                        strSql = ""
                        strSql += "USP_INSERT_TEMP_MTY_PRE_PLANNING_IMPORT '" & Trim(ContainerNo) & "','" & Trim(POD) & "','" & Trim(POL) & "','" & Trim(VesselName) & "','" & Convert.ToDateTime(Trim(CutDateTime & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                        strSql += "'" & Trim(VoyageNo) & "','" & Session("UserId_DepoCFS") & "'"
                        db.sub_ExecuteNonQuery(strSql)

                        CutDateTime = ""
                        ContainerNo = ""
                        POD = ""
                        POL = ""
                        VoyageNo = ""
                        VesselName = ""

                    End If
                Next
            End If
            strSql = ""
            strSql += "USP_INSERT_INTO_EYARD_PRE_PLANNING " & Session("UserId_DepoCFS") & ""
            db.sub_ExecuteNonQuery(strSql)
            db.sub_ExecuteNonQuery("Delete from Temp_MTY_Pre_Planning_Import Where userID=" & Session("UserId_DepoCFS") & "")
            Fillgrid()
            lblSession.Text = "Record successfully imported "
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

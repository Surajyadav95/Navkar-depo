Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
'Imports System.Data.OleDb

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11, dt12, dt13, dt14 As DataTable
    Dim db As New dbOperation_Depo
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim dblNetAmount As Double
    Dim strContNumbers As String
    Dim dblNetAmount_IND As Double
    Dim dblSTaxOnAmount As Double
    Dim strAccountID As String = ""
    Dim intGrossWeight As Double
    Dim blnSTax As Boolean
    Dim dblGroup1Amt As Double
    Dim dblGroup2Amt As Double
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete from Temp_Pre_Hold_Import Where userID=" & Session("UserId_DepoCFS") & "")

            txtHoldDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Filldropdown()
        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_HoldReason"
            ds = db.sub_GetDataSets(strSql)
            ddlHoldReason.DataSource = ds.Tables(0)
            ddlHoldReason.DataTextField = "HoldReason"
            ddlHoldReason.DataValueField = "HoldReasonID"
            ddlHoldReason.DataBind()
            ddlHoldReason.Items.Insert(0, New ListItem("--Select--", 0))

            ddlHoldReasonImport.DataSource = ds.Tables(0)
            ddlHoldReasonImport.DataTextField = "HoldReason"
            ddlHoldReasonImport.DataValueField = "HoldReasonID"
            ddlHoldReasonImport.DataBind()
            ddlHoldReasonImport.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnHold_Click(sender As Object, e As EventArgs) Handles btnHold.Click
        Try
            If Trim(txtcontainer.Text) <> "" Or Trim(txtBookingNo.Text) <> "" Then
                If Not Trim(txtcontainer.Text) = "" Then
                    strSql = ""
                    strSql += "select * from Eyard_holds where ContainerNo='" & Trim(txtcontainer.Text) & "' and IsCancel=0 and HoldStatus='H' and HoldReasonID='" & Val(ddlHoldReason.SelectedValue) & "'"
                    dt3 = db.sub_GetDatatable(strSql)
                    If dt3.Rows.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Container is already on hold');", True)
                        txtcontainer.Focus()
                        Exit Sub
                    End If
                End If
                If Not Trim(txtBookingNo.Text) = "" Then
                    strSql = ""
                    strSql += "select * from Eyard_holds where BookingNo='" & Trim(txtBookingNo.Text) & "' and IsCancel=0 and HoldStatus='H' and HoldReasonID='" & Val(ddlHoldReason.SelectedValue) & "'"
                    dt3 = db.sub_GetDatatable(strSql)
                    If dt3.Rows.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Booking no is already on hold');", True)
                        txtBookingNo.Focus()
                        Exit Sub
                    End If
                End If
                strSql = ""
                strSql = "SP_Save_ext_Pre_Holds '0', '" & Trim(txtcontainer.Text) & "','" & Val(ddlHoldReason.SelectedValue) & "','" & Replace(Trim(txtRemarks.Text), "'", "''") & "','H','" & Session("UserId_DepoCFS") & "','" & Trim(txtBookingNo.Text) & "','" & Trim(ddlHoldtype.SelectedValue & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
                Clear()
                lblSession.Text = "Hold Details updated successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                txtcontainer.Focus()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnClearHold_Click(sender As Object, e As EventArgs) Handles btnClearHold.Click
        Try
            If Trim(txtcontainer.Text) <> "" Or Trim(txtBookingNo.Text) <> "" Then
                If Not Trim(txtcontainer.Text) = "" Then
                    strSql = ""
                    strSql += "select * from Eyard_holds where ContainerNo='" & Trim(txtcontainer.Text) & "' and IsCancel=0 and HoldStatus='H' and HoldReasonID='" & Val(ddlHoldReason.SelectedValue) & "'"
                    dt3 = db.sub_GetDatatable(strSql)
                    If Not dt3.Rows.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Container is not on hold or already released. Cannot Proceed');", True)
                        txtcontainer.Focus()
                        Exit Sub
                    End If
                End If
                If Not Trim(txtBookingNo.Text) = "" Then
                    strSql = ""
                    strSql += "select * from Eyard_holds where BookingNo='" & Trim(txtBookingNo.Text) & "' and IsCancel=0 and HoldStatus='H' and HoldReasonID='" & Val(ddlHoldReason.SelectedValue) & "'"
                    dt3 = db.sub_GetDatatable(strSql)
                    If Not dt3.Rows.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Booking no is not on hold or already released. Cannot Proceed');", True)
                        txtBookingNo.Focus()
                        Exit Sub
                    End If
                End If
                If Not Trim(txtBookingNo.Text) = "" Then
                    strSql = "UPDATE Eyard_holds SET Releasedremarks='" & Replace(Trim(txtRemarks.Text), "'", "''") & "', HoldStatus='C', ClearedBy=" & Session("UserId_DepoCFS") & ", ClearedOn='" & Format(Now, "dd-MMM-yyyy HH:mm:ss") & "',HoldType = '" & Trim(ddlHoldtype.SelectedValue & "") & "' WHERE"
                    strSql += " BookingNo='" & Trim(txtBookingNo.Text) & "' and ID=(SELECT MAX(ID) FROM Eyard_holds WHERE BookingNo='" & Trim(txtBookingNo.Text) & "'"
                    strSql += " AND HoldStatus='H' AND HoldReasonID=" & Val(ddlHoldReason.SelectedValue) & ")"
                    dt4 = db.sub_GetDatatable(strSql)
                End If
                If Not Trim(txtcontainer.Text) = "" Then
                    strSql = "UPDATE Eyard_holds SET Releasedremarks='" & Replace(Trim(txtRemarks.Text), "'", "''") & "', HoldStatus='C', ClearedBy=" & Session("UserId_DepoCFS") & ", ClearedOn='" & Format(Now, "dd-MMM-yyyy HH:mm:ss") & "',HoldType = '" & Trim(ddlHoldtype.SelectedValue & "") & "' WHERE"
                    strSql += " ContainerNo='" & Trim(txtcontainer.Text) & "' and ID=(SELECT MAX(ID) FROM Eyard_holds WHERE ContainerNo='" & Trim(txtcontainer.Text) & "'"
                    strSql += " AND HoldStatus='H' AND HoldReasonID=" & Val(ddlHoldReason.SelectedValue) & ")"
                    dt4 = db.sub_GetDatatable(strSql)
                End If
                Clear()
                lblSession.Text = "Hold releases successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                txtcontainer.Focus()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clear()
    End Sub
    Public Sub Clear()
        Try
            txtcontainer.Text = ""
            txtBookingNo.Text = ""
            txtRemarks.Text = ""
            txtHoldDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            ddlHoldReason.SelectedValue = 0
            ddlHoldReasonImport.SelectedValue = 0
            ddlHoldorRelease.SelectedValue = 0
            lblfilename.Text = ""
            Lblfile.Text = ""
            db.sub_ExecuteNonQuery("Delete from Temp_Pre_Hold_Import Where userID=" & Session("UserId_DepoCFS") & "")
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
                'Import_To_Grid(FilePath, Extension)
                XlsxFile(FilePath)
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
    '            'File.Delete(FilePath)
    '            'XlsxFile(FilePath)
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
            Dim HRemarks As String = ""

            Dim ContainerNo As String = ""
            Dim strContainer As String = ""
            Dim strContainer1 As String = ""

            Using workBook As New XLWorkbook(FilePath)
                Dim workSheet As IXLWorksheet = workBook.Worksheet(1)
                Dim firstRow As Boolean = True
                For Each row As IXLRow In workSheet.Rows()
                    If Not firstRow Then
                        ContainerNo = Trim(row.Cell(1).Value.ToString())
                        BookingNo = Trim(row.Cell(2).Value.ToString())
                        HRemarks = Trim(row.Cell(3).Value.ToString())

                        strSql = ""
                        strSql += "USP_INSERT_INTO_TEMP_PRE_HOLD_IMPORT '" & Trim(ContainerNo) & "','" & Trim(BookingNo) & "','" & Trim(ddlHoldorRelease.SelectedValue) & "','" & Val(ddlHoldReasonImport.SelectedValue) & "','" & Session("UserId_DepoCFS") & "','" & Replace(Trim(HRemarks), "'", "''") & "'"
                        db.sub_ExecuteNonQuery(strSql)

                        BookingNo = ""
                        ContainerNo = ""
                        HRemarks = ""
                    Else
                        firstRow = False
                    End If
                Next
            End Using
            File.Delete(FilePath)
            strSql = ""
            strSql += "USP_IMPORT_INTO_EYARD_HOLDS '" & Session("UserId_DepoCFS") & "','" & Trim(ddlHoldtype.SelectedValue & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            Clear()
            lblSession.Text = "Record successfully imported "
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select '' [Container No],'' [Booking No],'' [Remarks]"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Pre Hold Template")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")

                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=PreHoldTemplate.xlsx")
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

Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports DataAccessLayer
Imports System.IO
Imports System.Data.OleDb
 
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
            Filldropdown()
            txtEntryDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtSealReceivedDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_FILLDROPDOWN_SEAL_ENTRY"
            dt = db.sub_GetDatatable(strSql)
            ddlShipline.DataSource = dt
            ddlShipline.DataTextField = "SLName"
            ddlShipline.DataValueField = "SLID"
            ddlShipline.DataBind()
            ddlShipline.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            
            Dim StrSealNo As String = ""
            For i = Val(txtSealNoFrom.Text) To Val(txtSealNoTo.Text)
                If Trim(txtPrefixSeries.Text) = "" Then
                    StrSealNo = Trim(i)
                Else
                    StrSealNo = Trim(txtPrefixSeries.Text) + Trim(i)
                End If
                strSql = ""
                strSql = "SELECT * FROM import_sealMaster WHERE PrefixNo = '" & StrSealNo & "'   and SLId ='" & Trim(ddlShipline.SelectedValue) & "' AND Iscancel = 0  "
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This seal no already added. Cannot Proceed');", True)
                    txtSealNoFrom.Focus()
                    Exit Sub
                End If
                strSql = ""
                strSql = " SP_SealEntryMaster  '" & Trim(i) & "','" & Convert.ToDateTime(Trim(txtEntryDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                strSql += " '" & Convert.ToDateTime(Trim(txtSealReceivedDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_DepoCFS") & "','" & Val(ddlShipline.SelectedValue) & "'"
                strSql += " ,0,'" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "', '" & StrSealNo & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next
            Clear()
            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub Clear()
        Try
            txtEntryDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtSealReceivedDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            ddlShipline.SelectedValue = 0
            txtPrefixSeries.Text = ""
            txtSealNoFrom.Text = ""
            txtSealNoTo.Text = ""
            txtRemarks.Text = ""

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnImport_Click(sender As Object, e As EventArgs)
        'Try
        '    If FileUpload1.HasFile Then

        '        Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)

        '        Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)

        '        Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
        '        lblfilename.Text = "File Name: " + FileName

        '        Dim FilePath As String = Server.MapPath("~/Depo/Files/" + FileName)

        '        ' Dim FilePath As String = Server.MapPath(FolderPath + FileName)
        '        lblFilePath.Text = FilePath
        '        FileUpload1.SaveAs(FilePath)
        '        ' GridView1.DataSource = Nothing

        '        Import_To_Grid(FilePath, Extension)
        '        IO.File.Delete(FilePath)
        '        lblSession.Text = "Record Saved successfully "
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
        '        UpdatePanel3.Update()
        '    Else
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please choose file!');", True)
        '        Exit Sub
        '    End If
        'Catch ex As Exception
        '    lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        'End Try
        Try

            'FillGrid()
            If FileUpload1.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)

                Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")
                lblfilename.Text = "File Name: " + FileName


                Dim FilePath As String = Server.MapPath(FolderPath + FileName)
                If Not ((Extension = ".xls") Or (Extension = ".xlsx")) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Only .xls or .xlsx files are required!');", True)
                    btnImport.Text = "Import"
                    btnImport.Attributes.Add("Class", "btn btn-success btn-sm outline")
                    Exit Sub
                End If
                FileUpload1.SaveAs(FilePath)
                Import_To_Grid(FilePath)
                'Import_To_Grid(FilePath, Extension)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please choose file!');", True)
                btnImport.Text = "Import"
                btnImport.Attributes.Add("Class", "btn btn-success btn-sm outline")
                Exit Sub
            End If
        Catch ex As Exception
            btnImport.Text = "Import"
            btnImport.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Import_To_Grid(FilePath As String)
        Try
            Dim BookingNo As String = ""
            Dim ContainerNo As String = ""
            Dim WagonNo As String = ""
            Dim POD As String = ""
            Dim Name As String = "", StrSealNo As String = ""

            Dim strContainer As String = ""
            Dim strContainer1 As String = ""
            Using workBook As New XLWorkbook(FilePath)
                Dim workSheet As IXLWorksheet = workBook.Worksheet(1)
                Dim firstRow As Boolean = True
                firstRow = True
                For Each row As IXLRow In workSheet.Rows()
                    If Not firstRow Then
                        If Trim(row.Cell(1).Value.ToString()) <> "" Then

                            Name = Trim(row.Cell(1).Value.ToString())
                           

                            If Trim(txtPrefixSeries.Text) = "" Then
                                StrSealNo = Trim(Name)
                            Else
                                StrSealNo = Trim(txtPrefixSeries.Text) + Trim(Name)
                            End If
                            strSql = ""
                            strSql = "SELECT * FROM import_sealMaster WHERE PrefixNo = '" & StrSealNo & "'   and SLId ='" & Trim(ddlShipline.SelectedValue) & "' AND Iscancel = 0 and Status ='P'"
                            dt = db.sub_GetDatatable(strSql)
                            If dt.Rows.Count > 0 Then
                                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This seal no already added. Cannot Proceed');", True)
                                txtSealNoFrom.Focus()
                                Exit Sub
                            End If
                            strSql = ""
                            strSql = " SP_SealEntryMaster  '" & Trim(Name) & "','" & Convert.ToDateTime(Trim(txtEntryDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                            strSql += " '" & Convert.ToDateTime(Trim(txtSealReceivedDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_DepoCFS") & "','" & Val(ddlShipline.SelectedValue) & "'"
                            strSql += " ,0,'" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "', '" & StrSealNo & "'"
                            db.sub_ExecuteNonQuery(strSql)
                            Name = ""

                        End If
NextRecord:
                    Else
                        firstRow = False
                    End If
                Next
            End Using
            File.Delete(FilePath)

            btnImport.Text = "Import"
            btnImport.Attributes.Add("Class", "btn btn-success btn-sm outline")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record imported successfully');", True)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub XlsxFile(dt As DataTable)
        Try
            Dim Name As String = "", StrSealNo As String = ""

            If (dt.Rows.Count > 0) Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Name = ""
                    If Not Trim(dt.Rows(i)(1).ToString).ToString Is DBNull.Value.ToString Then
                        Name += Convert.ToString(Trim(dt.Rows(i).ItemArray(1))) + "<br/> "
                    Else
                        Name = ""
                    End If
                Next
                If (Name <> "") Then
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This file is not containing In or Out Time for employee... \n" & Name & "');", True)
                    'Exit Sub
                    'lblError.Text = "This file is not containing In or Out Time for employee... <br/>" & Name & ""
                End If
            End If
            Name = ""

            If (dt.Rows.Count > 0) Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Name = dt.Rows(i).ItemArray(1).ToString()
                    If Trim(txtPrefixSeries.Text) = "" Then
                        StrSealNo = Trim(Name)
                    Else
                        StrSealNo = Trim(txtPrefixSeries.Text) + Trim(Name)
                    End If
                    strSql = ""
                    strSql = " SP_SealEntryMaster  '" & Trim(Name) & "','" & Convert.ToDateTime(Trim(txtEntryDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                    strSql += " '" & Convert.ToDateTime(Trim(txtSealReceivedDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_DepoCFS") & "','" & Val(ddlShipline.SelectedValue) & "'"
                    strSql += " ,0,'" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "', '" & StrSealNo & "'"
                    db.sub_ExecuteNonQuery(strSql)
                    Name = ""
                Next
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

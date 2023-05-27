Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb
Imports System.Globalization
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
            db.sub_ExecuteNonQuery("Delete from Temp_Approve_Container_Import Where userID=" & Session("UserId_DepoCFS") & "")

            txtApproveDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            strSql = ""
            dt = db.sub_GetDatatable(strSql)
            grdContainer.DataSource = dt
            grdContainer.DataBind()
        End If
    End Sub
    Protected Sub FillGrid(ByVal blISApp As Boolean)
        Try
            Dim dblentryid As Double = 0
            strSql = ""
            strSql += "USP_APPROVE_CONTAINER_TEXT_CHANGE '" & Trim(txtContainerNo.Text & "") & "'"
            'ds = db.sub_GetDataSets(strSql)
            dt = db.sub_GetDatatable(strSql)
            'If ds.Tables(0).Rows.Count > 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Approved entry already done dated on " & Convert.ToDateTime(ds.Tables(0).Rows(0)("approvedon")).ToString("dd-MM-yyyy") & " updated by " & (ds.Tables(0).Rows(0)("username")) & ". Cannot proceed');", True)
            '    Clear()
            '    txtContainerNo.Text = ""
            '    txtContainerNo.Focus()
            '    'Exit Sub
            'End If
            'If ds.Tables(1).Rows.Count > 0 Then
            '    dblentryid = Val(ds.Tables(1).Rows(0)(0) & "")
            'Else
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid container no. Cannot proceed');", True)
            '    Clear()
            '    txtContainerNo.Text = ""
            '    txtContainerNo.Focus()
            '    Exit Sub
            'End If
            'strSql = ""
            'If blISApp = True Then
            '    strSql = " Exec SP_ApproveAmt_Flag '" & txtContainerNo.Text & "' ," & dblentryid & " "
            'ElseIf blISApp = False Then
            '    strSql = " Exec SP_ApproveAmt '" & txtContainerNo.Text & "' ," & dblentryid & ""
            'End If
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtSize.Text = Trim(dt.Rows(0)("Size") & "")
                txtCType.Text = Trim(dt.Rows(0)("Container Type") & "")
                txtShippingLine.Text = Trim(dt.Rows(0)("Shipping Line") & "")
                txtInDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("InDate") & "")).ToString("dd-MM-yyyy HH:mm")
                'txtEstimateDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Estimate Date") & "")).ToString("dd-MM-yyyy HH:mm")
                'txtApprovedBy.Text = Trim(dt.Rows(0)("Approved by") & "")
                'txtApproveRemarks.Text = Trim(dt.Rows(0)("Approved_remarks") & "")
                txtEntryID.Text = Trim(dt.Rows(0)("EntryID") & "")
                ''txtEstimateID.Text = Trim(dt.Rows(0)("Estimate_ID") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records found');", True)
                Clear()
                txtContainerNo.Text = ""
                txtContainerNo.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql += "SP_GetEstiateDetailsAppr '" & Trim(txtEstimateID.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdContainer.DataSource = dt
            grdContainer.DataBind()
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dblAmount As Double = 0
            For Each row In grdContainer.Rows
                If CType(row.FindControl("chkIsApprove"), CheckBox).Checked = True Then
                    dblAmount += Trim(CType(row.FindControl("txtAppAmount"), TextBox).Text & "")
                Else
                    dblAmount += 0
                End If
            Next
            'If dblAmount = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records selected for approval');", True)
            '    Exit Sub
            'End If
            strSql = ""
            strSql += "USP_APPROVE_CONTAINER_UPDATE '" & dblAmount & "'," & Session("UserId_DepoCFS") & ",'" & Replace(Trim(txtApprovedBy.Text & ""), "'", "''") & "',"
            strSql += "'" & Replace(Trim(txtApproveRemarks.Text & ""), "'", "''") & "','" & Trim(txtEstimateID.Text & "") & "','" & Convert.ToDateTime(txtApproveDate.Text & "").ToString("yyyy-MM-dd HH:mm") & "',"
            strSql += "'" & Trim(txtContainerNo.Text & "") & "','" & Trim(txtEntryID.Text & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            For Each row In grdContainer.Rows
                If CType(row.FindControl("chkIsApprove"), CheckBox).Checked = True Then
                    strSql = ""
                    strSql = "update Estimate_D set isuploaded=0,  AppAmount='" & Trim(CType(row.FindControl("txtAppAmount"), TextBox).Text & "") & "', Approved_Desc='" & Replace(Trim(CType(row.FindControl("txtAppDescription"), TextBox).Text & ""), "'", "''") & "', "
                    strSql += "AppManHours='" & Trim(CType(row.FindControl("txtAppManHours"), TextBox).Text & "") & "',AppMatCost='" & Trim(CType(row.FindControl("txtAppMaterialCost"), TextBox).Text & "") & "',AppManCost='" & Trim(CType(row.FindControl("txtAppManCost"), TextBox).Text & "") & "' where Estimate_ID='" & Trim(txtEstimateID.Text) & "' and SrNo='" & Trim(CType(row.FindControl("lblSrNo"), Label).Text & "") & "'"
                    db.sub_ExecuteNonQuery(strSql)
                End If
            Next
            Clear()
            lblSession.Text = "Approved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtContainerNo_TextChanged(sender As Object, e As EventArgs)
        Try
            Clear()
            FillGrid(False)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            txtSize.Text = ""
            txtCType.Text = ""
            txtInDate.Text = ""
            txtEstimateDate.Text = ""
            txtApproveDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtShippingLine.Text = ""
            strSql = ""
            dt = db.sub_GetDatatable(strSql)
            grdContainer.DataSource = dt
            grdContainer.DataBind()
            txtApprovedBy.Text = ""
            txtApproveRemarks.Text = ""
            txtEntryID.Text = ""
            txtEstimateID.Text = ""
            lblfilename.Text = ""
            Lblfile.Text = ""
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            db.sub_ExecuteNonQuery("Delete from Temp_Approve_Container_Import Where userID=" & Session("UserId_DepoCFS") & "")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub chkIsApprove_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, CheckBox).NamingContainer, GridViewRow)
            If CType(gr.FindControl("chkIsApprove"), CheckBox).Checked = True Then
                CType(gr.FindControl("txtAppManHours"), TextBox).Text = Trim(CType(gr.FindControl("lblManhours"), Label).Text)
                CType(gr.FindControl("txtAppManCost"), TextBox).Text = Trim(CType(gr.FindControl("lblManCost"), Label).Text)
                CType(gr.FindControl("txtAppMaterialCost"), TextBox).Text = Trim(CType(gr.FindControl("lblMaterialCost"), Label).Text)
                CType(gr.FindControl("txtAppAmount"), TextBox).Text = (Val(CType(gr.FindControl("lblManhours"), Label).Text) * Val(CType(gr.FindControl("lblManCost"), Label).Text)) + Val(CType(gr.FindControl("lblMaterialCost"), Label).Text)
                CType(gr.FindControl("txtAppDescription"), TextBox).Text = Trim(CType(gr.FindControl("lblDescription"), Label).Text)
            Else
                CType(gr.FindControl("txtAppManHours"), TextBox).Text = 0
                CType(gr.FindControl("txtAppManCost"), TextBox).Text = 0
                CType(gr.FindControl("txtAppMaterialCost"), TextBox).Text = 0
                CType(gr.FindControl("txtAppAmount"), TextBox).Text = 0
                CType(gr.FindControl("txtAppDescription"), TextBox).Text = ""
            End If
            UpdatePanel1.Update()
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
            Dim Adate As String = ""
            Dim ContainerNo As String = ""
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
                    End If
                Next
                If Not (strContainer = "" And strContainer1 = "" And strContainer2 = "") Then
                    If Not strContainer = "" Then
                        strmessage += "Invalid container no length for " & strContainer & "\n"
                    End If
                    If Not strContainer1 = "" Then
                        strmessage += "Invalid date format for " & strContainer1 & "\n"
                    End If
                    If Not strContainer2 = "" Then
                        strmessage += "No records in estimate found for " & strContainer2 & "\n"
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
                        Adate = dt.Rows(i).ItemArray(1).ToString()

                        strSql = ""
                        strSql += "USP_INSERT_INTO_TEMP_APPROVE_CONTAINER_IMPORT '" & Trim(ContainerNo) & "','" & Convert.ToDateTime(Adate).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_DepoCFS") & "'"
                        db.sub_ExecuteNonQuery(strSql)

                        Adate = ""
                        ContainerNo = ""
                    End If
                Next
            End If
            strSql = ""
            strSql += "USP_UPDATE_APPROVE_CONTAINER_DETAILS_IMPORT " & Session("UserId_DepoCFS") & ""
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
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Clear()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select '' [Container No],'' [Approved Date (yyyy-MM-dd)]"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Estimation Approve")
                    With wb.Worksheets(0)
                        .Column(2).Style.DateFormat.Format = "yyyy-MM-dd"
                    End With
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=ApproveEstimateTemplate.xlsx")
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

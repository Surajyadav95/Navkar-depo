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
    Dim dt, dt1, dt2, dt3, dt4 As DataTable
    Dim db As New dbOperation_Depo
    Dim dbO As New dbOperation
    Dim ds, ds1 As DataSet
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
        'If Session("UserName") Is Nothing Then
        '    Session("UserID") = Request.Cookies("UserID").Value
        '    Session("UserName") = Request.Cookies("UserName").Value
        'End If
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete from Temp_Approve_Container_Import Where userID=" & Session("UserId_DepoCFS") & "")

            txtApproveDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtRepairDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            strSql = ""
            dt = db.sub_GetDatatable(strSql)
            grdContainer.DataSource = dt
            grdContainer.DataBind()
        End If
    End Sub
    Protected Sub FillGrid()
        Try
            strSql = ""
            strSql += "USP_GetDestim_Datails '" & Val(lblFileID.Text) & "'"
            ds1 = db.sub_GetDataSets(strSql)
            dt1 = ds1.Tables(0)
            txtContainerNo.Text = dt1.Rows(0)("ContainerNo")
            'txtEstDate.Text = Convert.ToDateTime(dt1.Rows(0)("Est_Date") & "").ToString("dd-MM-yyyy HH:mm")
            txtEstDate.Text = Convert.ToDateTime(dt1.Rows(0)("Est_Date")).ToString("yyyy-MM-ddTHH:mm")

            txtActivity.Text = dt1.Rows(0)("Activity")
            txtDepotCode.Text = dt1.Rows(0)("DepotId")
            txtVendorCode.Text = dt1.Rows(0)("VendorID")
            txtEstNo.Text = dt1.Rows(0)("Est_NO")
            txtTotAmt.Text = dt1.Rows(0)("APPROVE_AMT")
            txtStatus.Text = dt1.Rows(0)("Status")

            grdContainer.DataSource = ds1.Tables(1)
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
            If dblAmount = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No records selected for approval');", True)
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_APPROVE_CONTAINER_UPDATE '" & dblAmount & "'," & Session("UserID") & ",'" & Replace(Trim(txtApprovedBy.Text & ""), "'", "''") & "',"
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
            'FillGrid(False)
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
            db.sub_ExecuteNonQuery("Delete from Temp_Approve_Container_Import Where userID=" & Session("UserID") & "")
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
                        strSql += "USP_INSERT_INTO_TEMP_APPROVE_CONTAINER_IMPORT '" & Trim(ContainerNo) & "','" & Convert.ToDateTime(Adate).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserID") & "'"
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

    

    Protected Sub Upload_Click(sender As Object, e As EventArgs)
        Try
            If FileUpload1.HasFile Then
                Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
                Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)

                Dim FolderPath As String = ConfigurationManager.AppSettings("FolderPath")

                lblfilename.Text = "File Name: " + FileName


                Dim FilePath As String = Server.MapPath(FolderPath + FileName)
                If Not ((Extension = ".EDI") Or (Extension = ".edi")) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Only .EDI or .edi files are required!');", True)
                    btnUpload.Text = "Import"
                    btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
                    Exit Sub
                End If
                FileUpload1.SaveAs(FilePath)
                divUpdateRepair.Attributes.Add("style", "display:block")
                divUpdateRepairDate.Attributes.Add("style", "display:block")
                Upload(sender, e, FilePath, FileName)

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
    'Private Sub Upload(ByVal FilePath As String, ByVal Extension As String)
    Private Sub Upload(sender As Object, e As EventArgs, FilePath As String, strFileName As String)

        Try
            'Dim strConnString As String = "", strFileName As String = "", stringReader As String = ""
            Dim fileReader As System.IO.StreamReader
            Dim DestimFile As String = ""
            'strFileName = Application.StartupPath & "\ChennaiDepot.DSN"
            'fileReader = My.Computer.FileSystem.OpenTextFileReader(strFileName)

            'StringReader = ""
            'While Not fileReader.EndOfStream
            '    strConnString = strConnString & fileReader.ReadLine()
            'End While

            fileReader = My.Computer.FileSystem.OpenTextFileReader(FilePath)
            Dim int20 As Integer = 0
            Dim intNotfound As Integer = 0
            Dim intSuspense As Integer = 0
            Dim FILE_NAME As String = strFileName
            Dim strFILEID As Double = 0




            Dim Strtxtline As String
            Dim strLINE As String
            Dim strSEQNO As Double = 0
            Dim objReader As New System.IO.StreamReader(FilePath)
            Do While objReader.Peek() <> -1
                strSEQNO = strSEQNO + 1
                strLINE = objReader.ReadLine()
    
              


                strLINE = strLINE.Replace(System.Environment.NewLine, "'")
                DestimFile = DestimFile & strLINE
            Loop


            Dim StrLength As Integer = 0
            Dim IndexOfBreck As Integer = 0
            Dim IndexOfBreck2 As Integer = 0

            Dim StartPoint As Integer = 1
            Dim StrLine1 As String = ""
            StrLength = DestimFile.Length()

            Dim FILE_ID As Integer = 0
            Dim SEQNO As Integer = 1

            strSql = ""
            strSql += "Select isnull(MAX(FILE_ID),0)+1 As FILE_ID from DESTIM_FILEDATA "
            dt1 = db.sub_GetDatatable(strSql)
            FILE_ID = dt1.Rows(0)("FILE_ID")

            Dim a = Split(DestimFile, "'")
            For Each x In a
                StrLine1 = (x & "''")
                If StrLine1.Equals("'") = False Then
                    StrLine1 = StrLine1.TrimStart()
                    strSql = ""

                    strSql += "InsertInto_DESTIM_FILEDATA '" & FILE_ID & "','" & SEQNO & "','" & StrLine1 & "'"
                    db.sub_ExecuteNonQuery(strSql)
                    SEQNO = SEQNO + 1
                End If
            Next
            ''dbO.sub_ConnectDB()

            ''Dim sqlCommandSting As String
            ''sqlCommandSting = "USP_INSERT_INTO_TEMP_GENERATE_ESTIMATES "
            ''Dim cmd As New SqlCommand(sqlCommandSting, dbO.conn)
            ''cmd.CommandType = CommandType.StoredProcedure
            ''cmd.Parameters.AddWithValue("@FILEID", FILE_ID)
            ''cmd.Parameters.AddWithValue("@USERID", Session("UserId_DepoCFS"))

            ''cmd.Connection = dbO.conn
            ''Try
            ''    'dbO.conn.Open()
            ''    'cmd.ExecuteNonQuery()
            ''    If dbO.conn.State = ConnectionState.Closed Then dbO.conn.Open()
            ''    Dim da As New SqlDataAdapter
            ''    da.SelectCommand = cmd
            ''    ' da.Fill(ds1)
            ''Catch ex As Exception
            ''    Response.Write(ex.Message)
            ''Finally
            ''    dbO.conn.Close()
            ''    dbO.conn.Dispose()
            ''End Try
           
            strSql = ""
            strSql += "USP_READ_DESTIME_FILE '" & FILE_ID & "','" & Session("UserId_DepoCFS") & "'"
            ds1 = db.sub_GetDataSets(strSql)
            lblFileID.Text = FILE_ID
            FillGrid()
            'grdContainer.DataSource = ds1.Tables(1)
            'grdContainer.DataBind()
            'UpdatePanel1.Update()


          
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            If txtContainerNo.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter Container No. First');", True)

            Else
                'btnUpdateRepair.Style.Add("display", "block")

                strSql = ""
                strSql += "USP_Show_GetDestim_Datails '" & Trim(txtContainerNo.Text & "") & "'"
                ds1 = db.sub_GetDataSets(strSql)
                dt2 = ds1.Tables(1)

                dt1 = ds1.Tables(0)
                txtContainerNo.Text = dt1.Rows(0)("ContainerNo")
                lblFileID.Text = dt1.Rows(0)("FileID")
                'txtEstDate.Text = Convert.ToDateTime(dt1.Rows(0)("Est_Date") & "").ToString("dd-MM-yyyy HH:mm")
                txtEstDate.Text = Convert.ToDateTime(dt1.Rows(0)("Est_Date")).ToString("yyyy-MM-ddTHH:mm")

                txtActivity.Text = dt1.Rows(0)("Activity")
                txtDepotCode.Text = dt1.Rows(0)("DepotId")
                txtVendorCode.Text = dt1.Rows(0)("VendorID")
                txtEstNo.Text = dt1.Rows(0)("Est_NO")
                txtTotAmt.Text = dt1.Rows(0)("APPROVE_AMT")
                txtStatus.Text = dt1.Rows(0)("Status")
              

                If dt1.Rows(0)("IsRepair") = 1 Then
                    'btnUpdateRepair.Style.Add("display", "block")
                    'btnUpdateRepair.Style.Add("display", "none")
                    divUpdateRepair.Attributes.Add("style", "display:None")
                    divUpdateRepairDate.Attributes.Add("style", "display:None")
                    'btnUpdateRepair.Style.Add("visible", "false")
                    'btnUpdateRepair.Enabled = False

                End If
                '                btnUpdateRepair.Style.Add("Display","none"); // for hide
                'Button1.Style.Add("Display","block"); // for show 
                grdContainer.DataSource = dt2
                grdContainer.DataBind()
                UpdatePanel1.Update()
            End If


        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub

    Protected Sub btnUpdateRepair_Click(sender As Object, e As EventArgs)
        Try
            ''dbO.sub_ConnectDB()

            ''Dim sqlCommandSting As String
            ''sqlCommandSting = "USP_InsertIntoRepair "
            ''Dim cmd As New SqlCommand(sqlCommandSting, dbO.conn)
            ''cmd.CommandType = CommandType.StoredProcedure
            ''cmd.Parameters.AddWithValue("@ContainerNo", txtContainerNo.Text & "")
            ''cmd.Parameters.AddWithValue("@FILEID", lblFileID.Text & "")
            ''cmd.Parameters.AddWithValue("@RepairDate", Convert.ToDateTime(txtRepairDate.Text & "").ToString("yyyy-MM-dd HH:mm") & "")
            ''cmd.Parameters.AddWithValue("@USERID", Session("UserId_DepoCFS"))

            ''cmd.Connection = dbO.conn
            ''Try
            ''    'dbO.conn.Open()
            ''    'cmd.ExecuteNonQuery()
            ''    If dbO.conn.State = ConnectionState.Closed Then dbO.conn.Open()
            ''    Dim da As New SqlDataAdapter
            ''    da.SelectCommand = cmd
            ''    da.Fill(dt3)
            ''Catch ex As Exception
            ''    Response.Write(ex.Message)
            ''Finally
            ''    dbO.conn.Close()
            ''    dbO.conn.Dispose()
            ''End Try
            strSql = ""
            strSql += "Select * from RepairDestim_M Where FileID='" & Trim(lblFileID.Text & "") & "'"
            dt4 = db.sub_GetDatatable(strSql)
            If (dt4.Rows.Count > 0) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Repair Entry Already Updated');", True)
                Exit Sub
            End If


            strSql = ""
            strSql += "USP_InsertIntoRepair '" & Trim(txtContainerNo.Text & "") & "','" & Trim(lblFileID.Text & "") & "','" & Convert.ToDateTime(txtRepairDate.Text & "").ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserID") & "'"
            dt3 = db.sub_GetDatatable(strSql)
            If dt3.Rows.Count > 0 Then
                btnUpdateRepair.Style.Add("display", "none")
                lblSession.Text = "Record Saved successfully "
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            Else
                lblSession.Text = "Repair Not Saved successfully ,Please Try Again"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            End If
            btnShow_Click(sender, e)
        Catch ex As Exception

        End Try
    End Sub

End Class

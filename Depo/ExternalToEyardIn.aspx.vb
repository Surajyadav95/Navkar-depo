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
            txtAllotmentDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Call sub_CreateTable()
        End If
    End Sub
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable

        dtDepoContainer.Columns.Clear()

        Session("table_DepoContainer") = ""

        dtDepoContainer.Columns.Add("ContainerNo")
        dtDepoContainer.Columns.Add("Size")
        dtDepoContainer.Columns.Add("Type")
        dtDepoContainer.Columns.Add("ISOCode")
        dtDepoContainer.Columns.Add("LineName")
        dtDepoContainer.Columns.Add("Location")
        dtDepoContainer.Columns.Add("PortName")
        dtDepoContainer.Columns.Add("EntryID")
        dtDepoContainer.Columns.Add("LineID")


        Dim dtRow2 As DataRow = dtDepoContainer.NewRow

        grdOutDets.DataSource = Nothing
        grdOutDets.DataSource = dtDepoContainer
        grdOutDets.DataBind()
        Session("table_DepoContainer") = dtDepoContainer

    End Sub
    Protected Sub Filldropdown()
        Try           
            strSql = ""
            strSql = "SELECT DISTINCT agid, agname FROM customer where isactive=1 order by agname asc"
            dt = db.sub_GetDatatable(strSql)
            ddlOnaccount.DataSource = dt
            ddlOnaccount.DataTextField = "agname"
            ddlOnaccount.DataValueField = "agid"
            ddlOnaccount.DataBind()
            ddlOnaccount.Items.Insert(0, New ListItem("All", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_EYARD_BK_HOLD '" & Trim(txtBookingNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified Booking no is on hold again hold reasons');", True)
                'MsgBox("Specified Booking no is on hold again hold reasons " & Trim(dt.Rows(0)("HoldReason") & "") & " and remarks  " & Trim(dt.Rows(0)("Remarks") & "") & ". Cannot proceed", vbCritical)
                txtcontainerNo.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql = "USP_EYARD_CTR_HOLD '" & Trim(txtcontainerNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified Container no is on hold again hold reasons ');", True)
                'MsgBox("Specified Container no is on hold again hold reasons " & Trim(dt.Rows(0)("HoldReason") & "") & " and remarks  " & Trim(dt.Rows(0)("Remarks") & "") & ". Cannot proceed", vbCritical)
                txtcontainerNo.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql = "usp_check_exp_do_allotment_Validations_NEW '" & Trim(txtBookingNo.Text) & "','" & Val(txtAllotmentID.Text) & "', '" & Trim(txtcontainerNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ' txttareWt.Text = Trim(dt.Rows(0)("TareWeight") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified container line mismatch. Cannot proceed');", True)
                'MsgBox("Specified container siz/type/ line mismatch. Cannot proceed", vbCritical)
                txtcontainerNo.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql = "SELECT TOP 1 EntryID FROM exp_IN WHERE ContainerNo='" & Trim(txtcontainerNo.Text) & "' And Status='P' AND IsCancel=0 ORDER BY EntryID DESC"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Container is already IN. please go to administrator for editing if any!');", True)
                'MsgBox("This Container is already IN. please go to administrator for editing if any!")
                Exit Sub
            End If

            strSql = ""
            strSql += "USP_ALLOTMENT_MTY_TO_EXPORT_NEW '" & Trim(txtBookingNo.Text) & "', '" & Trim(txtcontainerNo.Text) & "'," & Val(txtentryID.Text) & ", " & Val(ddlOnaccount.SelectedValue) & ", '" & Trim(txtseal.Text) & "','" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "', '" & Val(txtAllotmentID.Text) & "', '" & Session("UserId_DepoCFS") & "',  '" & Convert.ToDateTime(Trim(txtindate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtpod.Text) & "' "
            db.sub_ExecuteNonQuery(strSql)
            lblSession.Text = "Record Updated successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select *  from Temp_Empty_Booking_Export where userid='" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtbookingNo.Text = Trim(dt.Rows(0)("BookingNo") & "")

            End If
            Call txtBookingNo_TextChanged(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnshow_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "exec get_sp_container_fetch_for_Out '" & Trim(txtcontainerNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtentryID.Text = Trim(dt.Rows(0)("ENTRYID") & "")
                txtsize.Text = Trim(dt.Rows(0)("Size") & "")
                txtindate.Text = Trim(dt.Rows(0)("indate") & "")
                txtType.Text = Trim(dt.Rows(0)("Type") & "")
                txtShippingLine.Text = Trim(dt.Rows(0)("LineI") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container Not gate in,Please recheck');", True)
                'MsgBox("" & txtcontainerNo.Text & " Container Not gate in,Please recheck", vbCritical)
                txtcontainerNo.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtBookingNo_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "exec usp_booking_Export_search '" & Trim(txtBookingNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                'txtBookingNo.Text = Trim(dt.Rows(0)("ENTRYID") & "")
                ddlOnaccount.SelectedValue = Val(dt.Rows(0)("agencyID") & "")
                'lblbksize.Text = Trim(dt.Rows(0)("size") & "")
                'lblbktype.Text = Trim(dt.Rows(0)("containertypeid") & "")
                lblbkslid.Text = Trim(dt.Rows(0)("SLID") & "")
                txtpod.Text = Trim(dt.Rows(0)("POD") & "")
                txtAllotmentID.Text = Trim(dt.Rows(0)("Gate Allow ID") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select '' [Container No],'' [Size],'' [Type],'' [ISO Code],'' [LineName],'' [Location]"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "External To Eyard In")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        .Column(3).Style.DateFormat.Format = "yyyy-MM-dd"
                        .Column(9).Style.DateFormat.Format = "yyyy-MM-dd"
                        .Column(10).Style.DateFormat.Format = "yyyy-MM-dd"
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=ExternalToEyardIn.xlsx")
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
    Private Sub Upload(sender As Object, e As EventArgs, FilePath As String)
        Try
            Dim intRows As Integer = 0
            Dim dtDepoContainer As New DataTable
            Dim strContainer As String = ""
            Dim strContainer1 As String = ""
            Dim strContainer2 As String = ""
            Dim strContainer3 As String = ""
            Dim strContainer4 As String = ""
            Dim strContainer5 As String = ""
            Dim strContainer6 As String = ""
            Dim strContainer7 As String = ""
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
                    'For Each row As IXLRow In workSheet.Rows()
                    '    If Not Trim(row.Cell(2).Value.ToString()) = "" Then
                    '        If Not firstRow Then
                    '            intRows += 1
                    '        Else
                    '            firstRow = False
                    '        End If
                    '    End If
                    'Next
                    'firstRow = True
                    For Each row As IXLRow In workSheet.Rows()
                        Dim dblDuplicate As Double = 0, dblWeight As Double = 0
                        If Not Trim(row.Cell(1).Value.ToString()) = "" Then
                            If Not firstRow Then
                                'For i = 0 To dtDepoContainer.Rows.Count - 1
                                '    'If Trim(dtDepoContainer.Rows(i)("WagonNo")) = Trim(row.Cell(1).Value.ToString()) Then
                                '    '    dblDuplicate += 1
                                '    'End If
                                '    If Trim(dtDepoContainer.Rows(i)("WagonNo")) = Trim(row.Cell(1).Value.ToString()) Then
                                '        dblWeight += Val(dtDepoContainer.Rows(i)("StuffWeight"))
                                '    End If
                                'Next
                                'dblWeight += Val(row.Cell(3).Value)
                                ''If dblDuplicate > 0 Then
                                ''    GoTo lblnext
                                ''End If
                                'strSql = ""
                                'strSql += "USP_VALIDATION_COIL_IN_WAGON_UPLOAD '" & Trim(row.Cell(1).Value.ToString()) & "','" & Trim(row.Cell(2).Value.ToString()) & "',"
                                'strSql += "'" & Trim(row.Cell(9).Value.ToString()) & "','" & Trim(row.Cell(10).Value.ToString()) & "','" & Trim(row.Cell(3).Value.ToString()) & "'"
                                'ds = db.sub_GetDataSets(strSql)
                                'If Not ds.Tables(0).Rows.Count > 0 Then
                                '    If strContainer1 = "" Then
                                '        strContainer1 = Trim(row.Cell(2).Value.ToString())
                                '    Else
                                '        If Not InStr(strContainer1, Trim(row.Cell(2).Value.ToString())) > 0 Then
                                '            strContainer1 += "," + Trim(row.Cell(2).Value.ToString())
                                '        End If
                                '    End If
                                'End If
                                'If Not ds.Tables(1).Rows.Count > 0 Then
                                '    If strContainer2 = "" Then
                                '        strContainer2 = Trim(row.Cell(9).Value.ToString())
                                '    Else
                                '        If Not InStr(strContainer2, Trim(row.Cell(9).Value.ToString())) > 0 Then
                                '            strContainer2 += "," + Trim(row.Cell(9).Value.ToString())
                                '        End If
                                '    End If
                                'End If
                                'If Not ds.Tables(2).Rows.Count > 0 Then
                                '    If strContainer3 = "" Then
                                '        strContainer3 = Trim(row.Cell(10).Value.ToString())
                                '    Else
                                '        If Not InStr(strContainer3, Trim(row.Cell(10).Value.ToString())) > 0 Then
                                '            strContainer3 += "," + Trim(row.Cell(10).Value.ToString())
                                '        End If
                                '    End If
                                'End If
                                'If Not ds.Tables(3).Rows.Count > 0 Then
                                '    If strContainer4 = "" Then
                                '        strContainer4 = Trim(row.Cell(3).Value.ToString())
                                '    Else
                                '        If Not InStr(strContainer4, Trim(row.Cell(3).Value.ToString())) > 0 Then
                                '            strContainer4 += "," + Trim(row.Cell(3).Value.ToString())
                                '        End If
                                '    End If
                                'End If
                                'Dim dtdate As DateTime
                                ''If DateTime.TryParseExact(Trim(row.Cell(15).Value.ToString()), formats, New CultureInfo("en-GB"), DateTimeStyles.None, dtdate) Then
                                ''    If (Convert.ToDateTime(Trim(row.Cell(15).Value)).ToString("yyyyMMdd")) < Convert.ToDateTime(Now).ToString("yyyyMMdd") Then
                                ''        If strContainer7 = "" Then
                                ''            strContainer7 = Trim(row.Cell(1).Value.ToString())
                                ''        Else
                                ''            If Not InStr(strContainer7, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                ''                strContainer7 += "," + Trim(row.Cell(1).Value.ToString())
                                ''            End If
                                ''        End If
                                ''        GoTo lblnext
                                ''    End If

                                ''Else
                                ''    If strContainer5 = "" Then
                                ''        strContainer5 = Trim(row.Cell(1).Value.ToString())
                                ''    Else
                                ''        If Not InStr(strContainer5, Trim(row.Cell(1).Value.ToString())) > 0 Then
                                ''            strContainer5 += "," + Trim(row.Cell(1).Value.ToString())
                                ''        End If
                                ''    End If
                                ''    GoTo lblnext
                                ''End If
                                'If ds.Tables(0).Rows.Count > 0 Then
                                '    If dblWeight > Val(ds.Tables(0).Rows(0)("Capacity")) Then
                                '        If strContainer6 = "" Then
                                '            strContainer6 = Trim(row.Cell(7).Value.ToString())
                                '        Else
                                '            If Not InStr(strContainer6, Trim(row.Cell(7).Value.ToString())) > 0 Then
                                '                strContainer6 += "," + Trim(row.Cell(7).Value.ToString())
                                '            End If
                                '        End If
                                '        GoTo lblnext
                                '    End If

                                'End If
                                'If Not ds.Tables(0).Rows.Count > 0 Or Not ds.Tables(1).Rows.Count > 0 Or Not ds.Tables(2).Rows.Count > 0 Or Not ds.Tables(3).Rows.Count > 0 Then
                                '    GoTo lblnext
                                'End If
                                Dim dtRow As DataRow = dtDepoContainer.NewRow

                                dtRow.Item("ContainerNo") = row.Cell(1).Value.ToString()
                                dtRow.Item("Size") = row.Cell(2).Value.ToString()
                                dtRow.Item("Type") = row.Cell(3).Value.ToString()
                                dtRow.Item("ISOCode") = row.Cell(4).Value.ToString()
                                dtRow.Item("LineName") = row.Cell(5).Value.ToString()
                                dtRow.Item("Location") = row.Cell(6).Value.ToString()
                                'dtRow.Item("Type") = row.Cell(3).Value.ToString()
                                'dtRow.Item("ISOCode") = row.Cell(4).Value.ToString()



                                dtDepoContainer.Rows.Add(dtRow)
                            Else
                                firstRow = False
                            End If
                        End If
lblnext:
                    Next
                    'If Not (strContainer1 = "" And strContainer2 = "" And strContainer3 = "" And strContainer4 = "" And strContainer5 = "" And strContainer6 = "" And strContainer7 = "") Then
                    '    If Not strContainer1 = "" Then
                    '        strContainer += "Train and Wagon not matching for -" & strContainer1 & "\n"
                    '    End If
                    '    If Not strContainer2 = "" Then
                    '        strContainer += "Customer not found -" & strContainer2 & "\n"
                    '    End If
                    '    If Not strContainer3 = "" Then
                    '        strContainer += "Commodity not found -" & strContainer3 & "\n"
                    '    End If
                    '    If Not strContainer4 = "" Then
                    '        strContainer += "Source Location not found -" & strContainer4 & "\n"
                    '    End If
                    '    If Not strContainer5 = "" Then
                    '        strContainer += "Validity Date Invalid for -" & strContainer5 & ""
                    '    End If
                    '    If Not strContainer6 = "" Then
                    '        strContainer += "Wagon Capacity exceeded for -" & strContainer6 & ""
                    '    End If
                    '    If Not strContainer7 = "" Then
                    '        strContainer += "Validity Date must be greater than today for -" & strContainer7 & ""
                    '    End If
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" & strContainer & "');", True)
                    'End If
                End Using
                File.Delete(FilePath)
            End If

            grdOutDets.DataSource = Nothing
            grdOutDets.DataSource = dtDepoContainer
            grdOutDets.DataBind()
            Session("table_DepoContainer") = dtDepoContainer
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim dtDepoContainer As New DataTable
            Dim intRows As Integer = 0

            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            intRows = dtDepoContainer.Rows.Count
            Dim dtRow As DataRow = dtDepoContainer.NewRow
            dtDepoContainer.Columns.Add("ContainerNo")
            dtDepoContainer.Columns.Add("Size")
            dtDepoContainer.Columns.Add("Type")
            dtDepoContainer.Columns.Add("ISOCode")
            dtDepoContainer.Columns.Add("LineName")
            dtDepoContainer.Columns.Add("Location")
            dtDepoContainer.Columns.Add("PortName")
            dtDepoContainer.Columns.Add("EntryID")
            dtDepoContainer.Columns.Add("LineID")

            'dtRow.Item("ContainerNo") = txtStuffedContainerNo.Text
            '' dtRow.Item("Size") = lblSEntryID.Text



            'dtRow.Item("WareHouse_ID") = ddllocation.SelectedValue
            'dtRow.Item("StuffQty") = txtStuffedQty.Text
            'dtRow.Item("StuffWt") = txtStuffedWt.Text
            'dtRow.Item("ShortPkgs") = txtShortPkgs.Text
            'dtRow.Item("ExcessPkgs") = txtExcessPkgs.Text

            'dtRow.Item("DCA_No") = txtDCAJoNo.Text
            'dtRow.Item("IGM_No") = txtIgmNo.Text
            'dtRow.Item("Item_No") = txtItemNo.Text
            'dtRow.Item("Seal_No") = txtSealNo.Text
            'dtRow.Item("Cargo_Desc") = txtCargoDesc.Text
            'dtRow.Item("Area") = txtArea.Text
            'dtRow.Item("NoLabours") = txtNoLabours.Text
            'If ddlVendorName.SelectedValue = 0 Then
            '    dtRow.Item("VendorName") = ""
            'Else
            '    dtRow.Item("VendorName") = ddlVendorName.SelectedItem.Text
            'End If
            'dtRow.Item("VendorID") = ddlVendorName.SelectedValue
            'If ddlEquipmentType.SelectedValue = 0 Then
            '    dtRow.Item("EquipmentType") = ""
            'Else
            '    dtRow.Item("EquipmentType") = ddlEquipmentType.SelectedItem.Text
            'End If
            'dtRow.Item("EquipmentTypeID") = ddlEquipmentType.SelectedValue

            'dtDomContainer.Rows.Add(dtRow)

            'grdOutDets.DataSource = Nothing
            'grdOutDets.DataSource = dtDomContainer
            'grdOutDets.DataBind()

            'Session("table_DomesticContainerStuffing") = dtDomContainer

            'txtStuffedContainerNo.Text = ""
            'lblSEntryID.Text = ""
            'ddllocation.SelectedValue = 0
            'txtStuffedQty.Text = ""
            'txtStuffedWt.Text = ""
            'txtShortPkgs.Text = ""
            'txtExcessPkgs.Text = ""
            'txtDCAJoNo.Text = ""
            'txtIgmNo.Text = ""
            'txtItemNo.Text = ""
            'txtCargoDesc.Text = ""
            'txtSealNo.Text = ""
            'txtArea.Text = ""
            'txtNoLabours.Text = ""
            'ddlVendorName.SelectedValue = 0
            'ddlEquipmentType.SelectedValue = 0

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

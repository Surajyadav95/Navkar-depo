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
    Dim dt, dt1, dt3, dt4, dt10, dt5 As DataTable
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserName") Is Nothing Then
        '    Session("UserID") = Request.Cookies("UserID").Value
        '    Session("UserName") = Request.Cookies("UserName").Value
        'End If
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("Delete From Temp_Est_Report Where User_ID=" & Session("UserId_DepoCFS") & "")

            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            ddlSearchOn.Text = "All"
            Filldropdown()
            btnShow_Click(sender, e)

        End If
      


        'strSql = ""
        'strSql += "select IsWestim from  estimate_m where IsWestim= 1 "
        'dt5 = db.sub_GetDatatable(strSql)
        'If dt5.Rows.Count > 0 Then


        '    aty = Trim(dt5.Rows(0)("IsWestim") & "")


        'End If

    End Sub
  
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_LineName"
            ds = db.sub_GetDataSets(strSql)
            ddlShipline.DataSource = ds.Tables(0)
            ddlShipline.DataTextField = "SLName"
            ddlShipline.DataValueField = "SLID"
            ddlShipline.DataBind()
            ddlShipline.Items.Insert(0, New ListItem("All", 0))
            strSql = ""
            strSql += "USP_ApproveStatus"
            ds = db.sub_GetDataSets(strSql)
            ddlStatus.DataSource = ds.Tables(0)
            ddlStatus.DataTextField = "BGMStatus"
            ddlStatus.DataValueField = "BGMCode"
            ddlStatus.DataBind()
            ddlStatus.Items.Insert(0, New ListItem("All", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try


            strSql = ""
            strSql += "USP_GetRepairSummary'" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & ddlBasedOn.SelectedValue & "','" & Trim(ddlSearchOn.SelectedItem.Text) & "',"

            If Trim(ddlSearchOn.SelectedItem.Text) = "Container No" Then
                strSql += "'" & Trim(TxtContainerNo.Text) & "'"
            ElseIf Trim(ddlSearchOn.SelectedItem.Text) = "Approve Status" Then
                strSql += "'" & Trim(ddlStatus.SelectedValue) & "'"
            Else
                strSql += "''"
            End If

            dt = db.sub_GetDatatable(strSql)

            grdContainer.DataSource = dt
            grdContainer.DataBind()

            For Each row In grdContainer.Rows

                If CType(row.findcontrol("lblIsRepairDestim"), Label).Text = 1 Then
                    CType(row.findcontrol("chkright"), CheckBox).Enabled = False
                End If
            Next
            'up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdContainer_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdContainer.PageIndex = e.NewPageIndex

        Me.btnShow_Click(sender, e)

    End Sub
    
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(btnShow, btnShow.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            Dim from As String = Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd")
            Dim strsql As String
            '------GetEstimationDetailsExport
            strsql = ""
            strsql += "USP_GetRepairSummary'" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & ddlBasedOn.SelectedValue & "','" & Trim(ddlSearchOn.SelectedItem.Text) & "',"

            If Trim(ddlSearchOn.SelectedItem.Text) = "Container No" Then
                strsql += "'" & Trim(TxtContainerNo.Text) & "'"
            ElseIf Trim(ddlSearchOn.SelectedItem.Text) = "Approve Status" Then
                strsql += "'" & Trim(ddlStatus.SelectedValue) & "'"
            Else
                strsql += "''"
            End If
            dt = db.sub_GetDatatable(strsql)
            If (dt.Rows.Count > 0) Then
                'Dim intCount As Integer = dt.Rows.Count
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Estimate Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        For k = 1 To dt.Columns.Count
                            .Cell(1, k).Style.Fill.BackgroundColor = XLColor.Yellow
                            .Cell(1, k).Style.Font.FontColor = XLColor.DarkBrown
                        Next
                        For i = 2 To dt.Rows.Count + 1
                            '.Cell(i, 21).Style.Fill.BackgroundColor = XLColor.LightGreen
                            .Cell(i, 22).Style.Fill.BackgroundColor = XLColor.LightGray
                            .Cell(i, 23).Style.Fill.BackgroundColor = XLColor.LightGray
                            .Cell(i, 24).Style.Fill.BackgroundColor = XLColor.LightGray
                            .Cell(i, 25).Style.Fill.BackgroundColor = XLColor.LightGray
                            .Cell(i, 26).Style.Fill.BackgroundColor = XLColor.LightGray
                            .Cell(i, 27).Style.Fill.BackgroundColor = XLColor.LightGray
                            .Cell(i, 28).Style.Fill.BackgroundColor = XLColor.LightGray
                            If .Cell(i, 1).Value = "" Then
                                .Cell(i, 4).Value = ""
                                .Cell(i, 6).Value = ""
                                .Cell(i, 12).Value = ""
                                .Cell(i, 14).Value = ""
                                .Cell(i, 15).Value = ""
                                .Cell(i, 16).Value = ""
                                .Cell(i, 17).Value = ""
                                .Cell(i, 18).Value = ""
                                .Cell(i, 19).Value = ""
                                .Cell(i, 20).Value = ""
                                .Cell(i, 21).Value = ""
                                .Cell(i, 22).Value = ""
                                .Cell(i, 23).Value = ""
                                .Cell(i, 24).Value = ""
                                .Cell(i, 25).Value = ""
                                .Cell(i, 26).Value = ""
                                .Cell(i, 27).Value = ""
                            End If
                        Next
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=EstimateSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyhhmm") & ".xlsx")
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

        'db.sub_ExecuteNonQuery("Delete From Temp_Est_Report Where User_ID=" & Session("UserId_DepoCFS") & "")

        'If Trim(ddlFormat.SelectedItem.Text) = "General" Then
        '    Call General()
        'End If
        'If Trim(ddlFormat.SelectedItem.Text) = "PAN ASIA LINE" Then
        '    Call PAN()
        'End If
        'If Trim(ddlFormat.SelectedItem.Text) = "HYUNDAI MARCHANT MARINE LINE" Then
        '    Call Hyundai()
        'End If
        'If Trim(ddlFormat.SelectedItem.Text) = "ZIM INTEGRATED SHIPPING" Then
        '    Call ZIM()
        'End If
        'If Trim(ddlFormat.SelectedItem.Text) = "EVERGREEN SHIPPING AGENCY" Then
        '    Call EverGreen()
        'End If
    End Sub
    Protected Sub ExportToExcel_Click(sender As Object, e As EventArgs) Handles ExportToExcel.Click
        Try
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(btnShow, btnShow.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If
            strSql = ""
            strSql += "SP_USP_GetEstimationDetails_export'" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlSearchOn.SelectedItem.Text) & "',"
            If Trim(ddlSearchOn.SelectedItem.Text) = "Shipping Line" Then
                strSql += "'" & Trim(ddlShipline.SelectedItem.Text) & "'"
            ElseIf Trim(ddlSearchOn.SelectedItem.Text) = "Container No" Then
                strSql += "'" & Trim(TxtContainerNo.Text) & "'"
            Else
                strSql += "''"
            End If
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                'Dim intCount As Integer = dt.Rows.Count
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Estimate Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        '.Range("A1:" & Excelno & "1").InsertRowsAbove(1)







                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=EstimateSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyhhmm") & ".xlsx")
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


    Protected Sub ddlSearchOn_SelectedIndexChanged(sender As Object, e As EventArgs)

        If (ddlSearchOn.SelectedValue = "1") Then
            divShiplineName.Attributes.Add("style", "display:block")
            TxtContainerNo.Text = ""
            divShiplineName.Attributes.Add("style", "display:None")
            divStatus.Attributes.Add("style", "display:none")
        End If
        If (ddlSearchOn.SelectedValue = "2") Then
            divContainerNo.Attributes.Add("style", "display:block")

            ddlShipline.SelectedValue = 0

        Else
            divContainerNo.Attributes.Add("style", "display:None")
            divStatus.Attributes.Add("style", "display:none")
        End If

        If (ddlSearchOn.SelectedValue = "3") Then
            divStatus.Attributes.Add("style", "display:block")

            ddlShipline.SelectedValue = 0

        Else
            'divContainerNo.Attributes.Add("style", "display:None")
            divStatus.Attributes.Add("style", "display:none")
        End If
    End Sub
    Protected Sub btnRepairDestim_Click(sender As Object, e As EventArgs) Handles bntRepairDestim.Click
        RepairDestim_Click()
        btnShow_Click(sender, e)

    End Sub

    Protected Sub RepairDestim_Click()
        Try
            Dim filename As String
            Dim dblChkCount As Double = 0
            Dim EdiNo As Integer = 0

            Dim strSEC As String
            For Each row In grdContainer.Rows

                If CType(row.findcontrol("lblIsRepairDestim"), Label).Text = 1 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Repair Destim Is Already Generared');", True)

                End If
                If CType(row.findcontrol("chkright"), CheckBox).Checked = True Then
                    dblChkCount += 1


                End If
            Next
            If dblChkCount = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select atleast one Container No.');", True)
                Exit Sub
            End If
            Dim Firstline As Integer = 1
            Dim LastLine As Integer = dblChkCount

            ' For Each row In grdSummary.Rows
            For Each row In grdContainer.Rows
                If CType(row.findcontrol("chkright"), CheckBox).Checked = True Then


                    strSql = ""
                    strSql = "exec USP_RepairDestim'" & Trim(("MSC") & "") & "','" & Trim(CType(row.FindControl("lblFileID"), Label).Text) & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "'"

                    dt3 = db.sub_GetDatatable(strSql)
                    Dim EdiNoNew As String = ""
                    'If grdContainer.Rows.Count = 0 Then
                    If Firstline = 1 Then

                        EdiNo = Convert.ToString(dt3.Rows(0)("EdiNo"))
                        EdiNoNew = EdiNo.ToString()
                    End If



                    If dt3.Rows.Count > 0 Then
                        For i = 0 To dt3.Rows.Count - 1
                            'If (dt3.Rows(i)("CONT_DATA").ToString().Contains("UNZ+2+")).ToString() = "UNZ+2+" Then
                            If (dt3.Rows(i)("CONT_DATA").ToString().Contains("UNZ+")) Then

                                strSEC = strSEC & "UNZ+" & LastLine & "+" & EdiNoNew & "'" & vbCrLf
                            Else
                                strSEC = strSEC & Trim(dt3.Rows(i)("CONT_DATA") & "") & vbCrLf
                            End If



                        Next

                    End If


                    Firstline = Firstline + 1
                    LastLine = dblChkCount

                    strSql = ""
                    strSql += "USP_Update_RepairDestim '" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblFileID"), Label).Text) & "'"
                    dt4 = db.sub_GetDatatable(strSql)



                End If
            Next


            filename = "RepairDestim_FILE_" & Convert.ToDateTime(Now).ToString("yyyyMMddHHmmss") & ".EDI"
            Dim strfilePath As String = ""
            strfilePath = Server.MapPath("~/RepairDestimFiles/")
            strfilePath += filename

            Dim writeFileXML As System.IO.TextWriter = New StreamWriter(strfilePath)
            writeFileXML.WriteLine(strSEC)
            writeFileXML.Flush()
            writeFileXML.Close()
            writeFileXML = Nothing
            Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + Path.GetFileName(strfilePath)))
            Response.WriteFile(strfilePath)

            Response.End()


            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "OpenList", "javascript:ShowAlert();", True)

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

  
   
End Class
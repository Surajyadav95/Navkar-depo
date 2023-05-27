﻿Imports System.Drawing
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
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try

            strSql = ""
            strSql += "USP_ShowtEstimationDetails'" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlSearchOn.SelectedItem.Text) & "',"
            If Trim(ddlSearchOn.SelectedItem.Text) = "Shipping Line" Then
                strSql += "'" & Trim(ddlShipline.SelectedItem.Text) & "'"
            ElseIf Trim(ddlSearchOn.SelectedItem.Text) = "Container No" Then
                strSql += "'" & Trim(TxtContainerNo.Text) & "'"
            Else
                strSql += "''"
            End If

            dt = db.sub_GetDatatable(strSql)

            'dt.Columns.Remove("Shipping Line")
            grdRegistrationSummary.DataSource = dt
            grdRegistrationSummary.DataBind()

            For Each row In grdRegistrationSummary.Rows

                'If CType(row.findcontrol("lblIsWestim"), Label).Text = 1 Then
                '    For Each row1 In grdRegistrationSummary.Rows
                '        CType(row1.findcontrol("chkright"), CheckBox).Checked = False
                '        CType(row1.findcontrol("chkright"), CheckBox).Enabled = False

                '    Next
                'End If

                If CType(row.findcontrol("lblIsWestim"), Label).Text = 1 Then
                    CType(row.findcontrol("chkright"), CheckBox).Checked = False

                    CType(row.findcontrol("chkright"), CheckBox).Enabled = False

                End If
            Next
            'up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdRegistrationSummary_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdRegistrationSummary.PageIndex = e.NewPageIndex

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
            strsql += "GetEstimationDetailsExport'" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlSearchOn.SelectedItem.Text) & "',"
            If Trim(ddlSearchOn.SelectedItem.Text) = "Shipping Line" Then
                strsql += "'" & Trim(ddlShipline.SelectedItem.Text) & "'"
            ElseIf Trim(ddlSearchOn.SelectedItem.Text) = "Container No" Then
                strsql += "'" & Trim(TxtContainerNo.Text) & "'"
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
                    Response.AddHeader("content-disposition", "attachment;filename=EstimateSummary_" & Convert.ToDateTime(Now).ToString("ddMMyyyyhhmm") & ".xlsx")
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
        Else
            divShiplineName.Attributes.Add("style", "display:None")
        End If
        If (ddlSearchOn.SelectedValue = "2") Then
            divContainerNo.Attributes.Add("style", "display:block")
            ddlShipline.SelectedValue = 0

        Else
            divContainerNo.Attributes.Add("style", "display:None")
        End If
    End Sub

    Protected Sub btnWestim_Click(sender As Object, e As EventArgs) Handles btnWestim.Click
                    strSql = ""
                    strSql += "UPDATE OVMNR_Estimate_M set IsWestim =1 where Containerno ='" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "'"
                    dt4 = db.sub_GetDatatable(strSql)
            
            Dim CHkCount As Integer = 0

            For Each row In grdRegistrationSummary.Rows
                        If CType(row1.findcontrol("chkright"), CheckBox).Checked = False Then
                            CType(row1.findcontrol("chkright"), CheckBox).Enabled = False
                        End If


                    Next
                        If CType(row1.findcontrol("lblIsWestim"), Label).Text = 0 Then
                            CType(row1.findcontrol("chkright"), CheckBox).Enabled = True
                        End If


                    Next
                End If

        Catch ex As Exception

        End Try
    End Sub

            Dim CHkCount As Integer = 0

            For Each row In grdRegistrationSummary.Rows
                        If CType(row1.findcontrol("chkright"), CheckBox).Checked = False Then
                            CType(row1.findcontrol("chkright"), CheckBox).Enabled = False
                        End If


                    Next
                        If CType(row1.findcontrol("lblIsWestim"), Label).Text = 0 Then
                            CType(row1.findcontrol("chkright"), CheckBox).Enabled = True
                        End If


                    Next
                End If

        Catch ex As Exception

        End Try
    End Sub
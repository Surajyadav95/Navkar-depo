Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports ClosedXML.Excel
Imports System.IO
Imports System.Configuration
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7 As DataTable
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim db As New dbOperation_Depo
    Dim strattachfile As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txttoDate.Text = Convert.ToDateTime(Now.AddDays(1)).ToString("yyyy-MM-ddTHH:mm")
            'Filldropdown()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    'Protected Sub Filldropdown()
    '    Try
    '        strSql = ""
    '        strSql = "select SLID,SLName from ShipLines"
    '        ds = db.sub_GetDataSets(strSql)
    '        ddlShipping.DataSource = ds.Tables(0)
    '        ddlShipping.DataTextField = "SLName"
    '        ddlShipping.DataValueField = "SLID"
    '        ddlShipping.DataBind()
    '        ddlShipping.Items.Insert(0, New ListItem("-Select-", 0))
    '    Catch ex As Exception
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    End Try
    'End Sub
    'Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim dbl20 As Double = 0, dbl40 As Double = 0, dbl45 As Double = 0, dblTEUS As Double = 0
    '        strSql = ""
    '        strSql += " GET_SP_ext_holdreport '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Val(ddlShipping.SelectedValue) & "'"
    '        dt = db.sub_GetDatatable(strSql)
    '        grdHoldDets.DataSource = dt
    '        grdHoldDets.DataBind()
    '        Dim intcontainerA As Double = 0, intcontainerB As Double = 0, intcontainerC As Double = 0, intcontainerD As Double = 0

    '        For i As Integer = 0 To dt.Rows.Count - 1

    '            If dt.Rows(i)("Size") = "20" Then
    '                dbl20 = i + 1
    '            End If
    '            If dt.Rows(i)("Size") = "40" Then
    '                dbl40 = i + 1
    '            End If

    '            If dt.Rows(i)("Size") = "45" Then
    '                dbl45 = i + 1
    '            End If

    '        Next
    '        dblTEUS = dbl20 + 2 * dbl40 + 2 * dbl45

    '    Catch ex As Exception
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    End Try
    'End Sub
    Protected Sub grdHoldDets_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdHoldDets.PageIndex = e.NewPageIndex
        'Me.btnSearch_Click(sender, e)
    End Sub

    'Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
    '    'Try
    '    '    If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd")) Then
    '    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
    '    '        Exit Sub
    '    '    End If
    '    '    Dim strSearchText As String = ""

    '    '    strSql = ""
    '    '    strSql += " GET_SP_ext_holdreport '" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Val(ddlHoldReason.SelectedValue) & "'"
    '    '    dt = db.sub_GetDatatable(strSql)
    '    '    strSql = ""
    '    '    strSql += "Select * from con_details"
    '    '    dt10 = db.sub_GetDatatable(strSql)
    '    '    If (dt.Rows.Count > 0) Then
    '    '        Using wb As New XLWorkbook()
    '    '            wb.Worksheets.Add(dt, "Hold Report" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
    '    '            'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
    '    '            With wb.Worksheets(0)
    '    '                Dim Excelno As String = ""
    '    '                Excelno = db.GetExcelColumnName(dt.Columns.Count)
    '    '                .Range("A1:" & Excelno & "1").InsertRowsAbove(12)
    '    '                .Range("A1:" & Excelno & "1").Merge()
    '    '                .Range("A2:" & Excelno & "2").Merge()
    '    '                .Range("A3:" & Excelno & "3").Merge()
    '    '                .Range("A4:" & Excelno & "4").Merge()
    '    '                .Range("A5:" & Excelno & "5").Merge()
    '    '                .Range("A6:" & Excelno & "6").Merge()
    '    '                .Range("A7:" & Excelno & "7").Merge()
    '    '                .Range("A8:" & Excelno & "8").Merge()
    '    '                .Range("A9:" & Excelno & "9").Merge()
    '    '                .Range("A10:" & Excelno & "10").Merge()
    '    '                .Range("A11:" & Excelno & "11").Merge()
    '    '                .Range("A12:" & Excelno & "12").Merge()

    '    '                .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
    '    '                .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
    '    '                .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
    '    '                .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")
    '    '                .Cell(5, 1).Value = Trim(dt10.Rows(0)("AddressIII") & "")
    '    '                .Cell(6, 1).Value = Trim(dt10.Rows(0)("AddressIV") & "")
    '    '                .Cell(8, 1).Value = Trim(dt10.Rows(0)("Con_Dets") & "")

    '    '                .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
    '    '                .Row(1).Height = 30
    '    '                .Row(6).Height = 20
    '    '                .Row(10).Height = 20
    '    '                .Cell(10, 1).Value = "As On: " + Convert.ToDateTime(txtason.Text).ToString("dd MMM yyyy")
    '    '                .Cell(10, 1).Style.Fill.BackgroundColor = XLColor.LightGray
    '    '                .Cell(11, 1).Value = "Hold Report"
    '    '                .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
    '    '                .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '    '                .Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
    '    '                .Cell(2, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '    '                .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
    '    '                .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '    '                .Cell(4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
    '    '                .Cell(4, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '    '                .Cell(5, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
    '    '                .Cell(5, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '    '                .Cell(6, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
    '    '                .Cell(6, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '    '                .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
    '    '                .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
    '    '                .Cell(6, 1).Style.Font.FontSize = 17
    '    '                .Cell(1, 1).Style.Font.FontSize = 20
    '    '                .Cell(11, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
    '    '                .Cell(11, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

    '    '                .Cell(10, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
    '    '                .Cell(10, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center

    '    '                .Cell(11, 1).Style.Font.FontSize = 17
    '    '                .Column(2).Width = 15

    '    '            End With
    '    '            Response.Clear()
    '    '            Response.Buffer = True
    '    '            Response.Charset = ""
    '    '            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    '    '            Response.AddHeader("content-disposition", "attachment;filename=HoldReport" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
    '    '            Using MyMemoryStream As New MemoryStream()
    '    '                wb.SaveAs(MyMemoryStream)
    '    '                MyMemoryStream.WriteTo(Response.OutputStream)
    '    '                'getdatewiseWO()
    '    '                Response.Flush()
    '    '                Response.End()
    '    '            End Using
    '    '        End Using
    '    '    Else
    '    '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No record found!');", True)
    '    '    End If
    '    'Catch ex As Exception
    '    '    lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    'End Try
    'End Sub

    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If Val(ddlShipping.SelectedValue) = 8 Then
            If Trim(ddlProcess.SelectedItem.Text) = "In" Then
                Call sub_GenerateEDEMPTYIN_HMM()
            ElseIf Trim(ddlProcess.SelectedItem.Text) = "Out" Then
                Call sub_GenerateEDEMPTYOut_HMM()

            ElseIf Val(ddlShipping.SelectedValue) = 9 Then
                If Trim(ddlProcess.SelectedItem.Text) = "In" Then
                    sub_GenerateRCLIN()
                ElseIf Trim(ddlProcess.SelectedItem.Text) = "Out" Then
                    sub_GenerateRCLOUTYOut()
                End If


            ElseIf Val(ddlShipping.SelectedValue) = 17 Then
                If Trim(ddlProcess.SelectedItem.Text) = "In" Then
                    sub_GenerateKMTCIN()
                ElseIf Trim(ddlProcess.SelectedItem.Text) = "Out" Then
                    sub_GenerateKMTCOUTYOut()
                End If
            ElseIf Val(ddlShipping.SelectedValue) = 2 Then
                If Trim(ddlProcess.SelectedItem.Text) = "In" Then
                    sub_GenerateECONIN()
                ElseIf Trim(ddlProcess.SelectedItem.Text) = "Out" Then
                    sub_GenerateECONOUTYOut()
                End If
            End If
        End If
    End Sub
    Protected Sub sub_GenerateECONIN()

    End Sub
    Protected Sub sub_GenerateECONOUTYOut()

    End Sub
    Protected Sub sub_GenerateKMTCIN()
        Try
            'Dim intfilenum As Integer
            Dim filename As String
            Dim dblRunningNo As String
            Dim strFST As String
            Dim strFST1 As String
            Dim strContainerNo As String
            Dim strDateandTime As String
            Dim dblTotalContainer As Integer
            Dim strISOCode As String
            Dim Strcontainers As String
            strDateandTime = Format(Now, "yyMMdd:HHmm")
            Dim strSEC As String

            Dim strsql As String
            Dim dt As DataTable

            strsql = ""
            strsql = "select  max(runningno)as runningno from Empty_EDI  "
            dt = db.sub_GetDatatable(strsql)

            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("runningno")) = True Then
                    dblRunningNo = 1
                Else
                    dblRunningNo = Val(dt.Rows(0)(0)) + 1
                End If
                'filename = "GATEIN_EPKINNSA_HMM_" & Format(Now, "ddMMyyyyHHmm")
                strsql = ""
                strsql = "exec SP_Save_Empty_EDI '" & dblRunningNo & "','" & Trim(filename) & "'"
                dt1 = db.sub_GetDatatable(strsql)
            End If


            strsql = ""
            strsql = "exec USP_Codeco_Applicable_In  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt2 = db.sub_GetDatatable(strsql)
            If dt2.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found!');", True)
                Exit Sub
            End If

            dt.Clear()
            'strsql = ""
            'strsql = "select * from Empty_EDI"

            'dt6 = db.sub_GetDatatable(strsql)
            'If dt.Rows.Count > 0 Then
            '    'dt.Rows(0)("runningno") = dblRunningNo
            '    'sub_ExecuteNonQuery(strsql)
            'Else

            'End If


            strFST1 = "UNA:+.? '" & vbCrLf
            strFST1 = strFST1 & "UNB+UNOA:1+EPICGLOBLE+KMTC+" & Trim(strDateandTime) & "+" & Trim(dblRunningNo) & "" & Trim("'") & "" & vbCrLf
            dblTotalContainer = 1

            strsql = ""
            strsql = "exec USP_Codeco_Applicable_In  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt3 = db.sub_GetDatatable(strsql)

            For i As Integer = 0 To dt3.Rows.Count - 1
                strContainerNo = ""
                strISOCode = ""
                strFST = "UNH+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "+" & Trim("CODECO:D:95B:UN:ITG14'") & "" & vbCrLf
                strFST = strFST & "BGM+34+10-1001+9'" & vbCrLf
                strFST = strFST & "TDT+20++1++KMTC:172:20+++:103::NA'" & vbCrLf
                strFST = strFST & "NAD+MS+EPICGLOBLE:160:87'" & vbCrLf
                strFST = strFST & "NAD+MR+KMTC:160:87'" & vbCrLf
                strFST = strFST & "NAD+CF+KMTC:160:87'" & vbCrLf
                strContainerNo = Trim(dt3.Rows(i)("containerno"))


                strsql = "SELECT * FROM ISOCODES where Isoid=" & Val(dt3.Rows(i)("isocodeid")) & ""
                dt4 = db.sub_GetDatatable(strsql)
                If dt4.Rows.Count > 0 Then
                    strISOCode = Trim(dt4.Rows(0)("isocode"))
                End If
                strFST = strFST & "EQD+CN+" & Trim(strContainerNo) & "+" & Trim(strISOCode) & "" & Trim(":102:5++3+4'") & "" & vbCrLf
                'strFST = strFST & "TMD+2'" & vbCrLf
                strFST = strFST & "DTM+7:" & Format(dt3.Rows(i)("InDate"), "yyyyMMddHHmm") & ":" & Trim("203'") & "" & vbCrLf
                strFST = strFST & "LOC+165+INNSA:139:6+INNASMKKLS:STO:ZZZ'" & vbCrLf
                strFST = strFST & "MEA+AAE+T+KGM:" & (dt3.Rows(i)("tareweight")) & "" & "'" & vbCrLf
                strFST = strFST & "SEL+NA+CA+1'" & vbCrLf
                strFST = strFST & "TDT+1++3+31++++" & Trim(dt3.Rows(i)("truckno")) & "" & "'" & vbCrLf
                strFST = strFST & "CNT+16:20'" & vbCrLf
                strFST = strFST & "UNT+15+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "" & "'" & vbCrLf
                If Trim(dblTotalContainer) = 1 Then
                    strSEC = strSEC & strFST1 & strFST
                Else
                    strSEC = strSEC & strFST
                End If

                strsql = ""
                strsql = "UPDATE Eyard_In SET IsAutoMailedline=1 WHERE EntryID='" & dt3.Rows(i)("EntryID") & "' and ContainerNo='" & dt3.Rows(i)("ContainerNo") & "'"

                dt5 = db.sub_GetDatatable(strsql)

                dblTotalContainer = dblTotalContainer + 1
                'objRSCount.MoveNext()
                If Trim(Strcontainers) = "" Then
                    Strcontainers = strContainerNo
                Else
                    Strcontainers = Strcontainers & ", " & strContainerNo
                End If
            Next

            strSEC = strSEC & "UNZ+" & Trim(dblTotalContainer) - 1 & "+" & Trim(dblRunningNo) & "" & "'" & vbCrLf
            If strSEC <> "" Then
                strSEC = Mid(strSEC, 1, Len(strSEC) - 2)
            End If

            filename = "GATE_IN_KMTC_INNSA_" & Convert.ToDateTime(Now).ToString("yyyyMMddHHmm") & ".EDI"
            Dim strfilePath As String = ""
            strfilePath = Server.MapPath("~/XMLFiles/")
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

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub sub_GenerateKMTCOUTYOut()
        Try
            'Dim intfilenum As Integer
            Dim filename As String
            Dim dblRunningNo As String
            Dim strFST As String
            Dim strFST1 As String
            Dim strContainerNo As String
            Dim strDateandTime As String
            Dim dblTotalContainer As Integer
            Dim strISOCode As String
            Dim Strcontainers As String
            strDateandTime = Format(Now, "yyMMdd:HHmm")
            Dim strSEC As String

            Dim strsql As String
            Dim dt As DataTable

            strsql = ""
            strsql = "select  max(runningno)as runningno from Empty_EDI  "
            dt = db.sub_GetDatatable(strsql)

            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("runningno")) = True Then
                    dblRunningNo = 1
                Else
                    dblRunningNo = Val(dt.Rows(0)(0)) + 1
                End If
                'filename = "GATEOUT_EPK_HMM_" & Format(Now, "ddMMyyyyHHmm")
                strsql = ""
                strsql = "exec SP_Save_Empty_EDI '" & dblRunningNo & "','" & Trim(filename) & "'"
                dt1 = db.sub_GetDatatable(strsql)
            End If


            strsql = ""
            strsql = "exec USP_Codeco_Applicable_OUt_nEW  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt2 = db.sub_GetDatatable(strsql)
            If dt2.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found!');", True)
                Exit Sub
            End If
            dt.Clear()
            Strcontainers = ""
            strFST1 = "UNA:+.? '" & vbCrLf
            strFST1 = strFST1 & "UNB+UNOA:1+EPICGLOBLE+KMTC+" & Trim(strDateandTime) & "+" & Trim(dblRunningNo) & "" & Trim("'") & "" & vbCrLf
            dblTotalContainer = 1



            strsql = ""
            strsql = "exec USP_Codeco_Applicable_OUt_nEW  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt3 = db.sub_GetDatatable(strsql)
            For i As Integer = 0 To dt3.Rows.Count - 1
                strContainerNo = ""
                strISOCode = ""
                strFST = "UNH+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "+" & Trim("CODECO:D:95B:UN:ITG14'") & "" & vbCrLf
                strFST = strFST & "BGM+36+11-1001+9'" & vbCrLf
                strFST = strFST & "TDT+20++1++" & Trim((dt3.Rows(i)("transporter"))) & "" & Trim(":172:87+++") & Trim((dt3.Rows(i)("transporter"))) & ":146::" & Trim((dt3.Rows(i)("transporter"))) & "'" & vbCrLf
                strFST = strFST & "NAD+MS+EPICGLOBLE:160:87'" & vbCrLf
                strFST = strFST & "NAD+MR+KMTC:160:87'" & vbCrLf
                strFST = strFST & "NAD+CF+KMTC:160:87'" & vbCrLf
                strContainerNo = Trim(dt3.Rows(i)("containerno"))

                strsql = ""
                strsql = "SELECT * FROM ISOCODES where Isoid=" & Val(dt3.Rows(i)("isocodeid")) & ""
                dt4 = db.sub_GetDatatable(strsql)
                If dt4.Rows.Count > 0 Then
                    strISOCode = Trim(dt4.Rows(0)("isocode"))
                End If
                strFST = strFST & "EQD+CN+" & Trim(strContainerNo) & "+" & Trim(strISOCode) & "" & Trim(":102:5++2+4'") & "" & vbCrLf
                strFST = strFST & "RFF+BN:" & "" & Trim((dt3.Rows(i)("bookingno"))) & "" & "'" & vbCrLf
                ' strFST = strFST & "TMD+4'" & vbCrLf
                strFST = strFST & "DTM+7:" & Format(dt3.Rows(i)("Outdate"), "yyyyMMddHHmm") & ":" & Trim("203'") & "" & vbCrLf
                strFST = strFST & "LOC+165+INNSA:139:6+INNASMKKLS:STO:ZZZ'" & vbCrLf
                strFST = strFST & "MEA+AAE+T+KGM:" & (dt3.Rows(i)("tareweight")) & "" & "'" & vbCrLf
                strFST = strFST & "SEL++CA+1'" & vbCrLf
                'strFST = strFST & "TDT+1++3+31++++" & Trim(objRScount.Fields("truckno")) & "" & "'" & vbCrLf
                strFST = strFST & "TDT+1++3+3+++++" & Trim((dt3.Rows(i)("transporter"))) & "" & ":146:ZZZ'" & vbCrLf
                strFST = strFST & "CNT+16:50'" & vbCrLf
                strFST = strFST & "UNT+15+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "" & "'" & vbCrLf
                If Trim(dblTotalContainer) = 1 Then
                    strSEC = strSEC & strFST1 & strFST
                Else
                    strSEC = strSEC & strFST
                End If

                strsql = ""
                strsql = "UPDATE eyardemptyout SET IsAutoMailedline=1 WHERE EntryID='" & dt3.Rows(i)("EntryID") & "' and ContainerNo='" & dt3.Rows(i)("ContainerNo") & "'"

                dt5 = db.sub_GetDatatable(strsql)

                dblTotalContainer = dblTotalContainer + 1

                If Trim(Strcontainers) = "" Then
                    Strcontainers = strContainerNo
                Else
                    Strcontainers = Strcontainers & ", " & strContainerNo
                End If

                ' objRSCount.MoveNext()
            Next

            strSEC = strSEC & "UNZ+" & Trim(dblTotalContainer) - 1 & "+" & Trim(dblRunningNo) & "" & "'" & vbCrLf
            If strSEC <> "" Then
                strSEC = Mid(strSEC, 1, Len(strSEC) - 2)
            End If

            filename = "GATE_OUT_KMTC_INNSA_" & Convert.ToDateTime(Now).ToString("yyyyMMddHHmm") & ".EDI"
            Dim strfilePath As String = ""
            strfilePath = Server.MapPath("~/XMLFiles/")
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

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub sub_GenerateRCLIN()
        Try
            'Dim intfilenum As Integer
            Dim filename As String
            Dim dblRunningNo As String
            Dim strFST As String
            Dim strFST1 As String
            Dim strContainerNo As String
            Dim strDateandTime As String
            Dim dblTotalContainer As Integer
            Dim strISOCode As String
            Dim Strcontainers As String
            strDateandTime = Format(Now, "yyMMdd:HHmm")
            Dim strSEC As String

            Dim strsql As String
            Dim dt As DataTable

            strsql = ""
            strsql = "select  max(runningno)as runningno from Empty_EDI  "
            dt = db.sub_GetDatatable(strsql)

            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("runningno")) = True Then
                    dblRunningNo = 1
                Else
                    dblRunningNo = Val(dt.Rows(0)(0)) + 1
                End If
                'filename = "GATEIN_EPKINNSA_HMM_" & Format(Now, "ddMMyyyyHHmm")
                strsql = ""
                strsql = "exec SP_Save_Empty_EDI '" & dblRunningNo & "','" & Trim(filename) & "'"
                dt1 = db.sub_GetDatatable(strsql)
            End If


            strsql = ""
            strsql = "exec USP_Codeco_Applicable_In  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt2 = db.sub_GetDatatable(strsql)
            If dt2.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found!');", True)
                Exit Sub
            End If

            strFST1 = "UNA:+.? '" & vbCrLf
            strFST1 = strFST1 & "UNB+UNOA:1+INEGS+RCL+" & Trim(strDateandTime) & "+" & Trim(dblRunningNo) & "" & Trim("'") & "" & vbCrLf
            dblTotalContainer = 1

            strsql = ""
            strsql = "exec USP_Codeco_Applicable_In  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt3 = db.sub_GetDatatable(strsql)

            For i As Integer = 0 To dt3.Rows.Count - 1
                strContainerNo = ""
                strISOCode = ""
                strFST = "UNH+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "+" & Trim("CODECO:D:95B:UN:ITG14'") & "" & vbCrLf
                strFST = strFST & "BGM+34+10-1001+9'" & vbCrLf
                strFST = strFST & "TDT+20++1++" & Trim((dt3.Rows(i)("transporter"))) & "" & Trim(":172:87+++") & Trim((dt3.Rows(i)("transporter"))) & ":146::" & Trim((dt3.Rows(i)("transporter"))) & "'" & vbCrLf
                strFST = strFST & "LOC+11+INEGS:139:6'" & vbCrLf
                strFST = strFST & "NAD+MS+INEGS:160:87'" & vbCrLf
                strContainerNo = Trim(dt3.Rows(i)("containerno"))


                strsql = "SELECT * FROM ISOCODES where Isoid=" & Val(dt3.Rows(i)("isocodeid")) & ""
                dt4 = db.sub_GetDatatable(strsql)
                If dt4.Rows.Count > 0 Then
                    strISOCode = Trim(dt4.Rows(0)("isocode"))
                End If
                strFST = strFST & "EQD+CN+" & Trim(strContainerNo) & "+" & Trim(strISOCode) & "" & Trim(":102:5++3+4'") & "" & vbCrLf
                strFST = strFST & "RFF+BN:" & "" & Trim((dt3.Rows(i)("BKNo"))) & "" & "'" & vbCrLf
                strFST = strFST & "DTM+7:" & Format(dt3.Rows(i)("InDate"), "yyyyMMddHHmm") & ":" & Trim("203'") & "" & vbCrLf
                strFST = strFST & "LOC+165+INEGS:139:6+:STO:ZZZ'" & vbCrLf
                strFST = strFST & "MEA+AAE+T+KGM:" & (dt3.Rows(i)("tareweight")) & "" & "'" & vbCrLf
                '-------------------for damage flag
                If Trim(dt3.Rows(i)("statustype")) = "AE" Then
                    strFST = strFST & "FTX+DAR++5'" & vbCrLf
                    strFST = strFST & "DAM+1+DD'" & vbCrLf
                End If

                strFST = strFST & "TDT+1++3+++++" & Trim((dt3.Rows(i)("transporter"))) & "" & ":146:ZZZ'" & vbCrLf
                strFST = strFST & "CNT+16:1'" & vbCrLf
                If Trim(dt3.Rows(i)("statustype")) = "AE" Then
                    strFST = strFST & "UNT+15+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "" & "'" & vbCrLf
                Else
                    strFST = strFST & "UNT+13+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "" & "'" & vbCrLf
                End If

                If Trim(dblTotalContainer) = 1 Then
                    strSEC = strSEC & strFST1 & strFST
                Else
                    strSEC = strSEC & strFST
                End If

                strsql = ""
                strsql = "UPDATE Eyard_In SET IsAutoMailedline=1 WHERE EntryID='" & dt3.Rows(i)("EntryID") & "' and ContainerNo='" & dt3.Rows(i)("ContainerNo") & "'"

                dt5 = db.sub_GetDatatable(strsql)

                If Trim(Strcontainers) = "" Then
                    Strcontainers = strContainerNo
                Else
                    Strcontainers = Strcontainers & ", " & strContainerNo
                End If


            Next

            strSEC = strSEC & "UNZ+" & Trim(dblTotalContainer) - 1 & "+" & Trim(dblRunningNo) & "" & "'" & vbCrLf
            If strSEC <> "" Then
                strSEC = Mid(strSEC, 1, Len(strSEC) - 2)
            End If

            filename = "GATE_IN_RCL_INEGS_" & Convert.ToDateTime(Now).ToString("yyyyMMddHHmm") & ".EDI"
            Dim strfilePath As String = ""
            strfilePath = Server.MapPath("~/XMLFiles/")
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

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub sub_GenerateRCLOUTYOut()
        Try
            'Dim intfilenum As Integer
            Dim filename As String
            Dim dblRunningNo As String
            Dim strFST As String
            Dim strFST1 As String
            Dim strContainerNo As String
            Dim strDateandTime As String
            Dim dblTotalContainer As Integer
            Dim strISOCode As String
            Dim Strcontainers As String
            strDateandTime = Format(Now, "yyMMdd:HHmm")
            Dim strSEC As String

            Dim strsql As String
            Dim dt As DataTable

            strsql = ""
            strsql = "select  max(runningno)as runningno from Empty_EDI  "
            dt = db.sub_GetDatatable(strsql)

            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("runningno")) = True Then
                    dblRunningNo = 1
                Else
                    dblRunningNo = Val(dt.Rows(0)(0)) + 1
                End If
                'filename = "GATEOUT_EPK_HMM_" & Format(Now, "ddMMyyyyHHmm")
                strsql = ""
                strsql = "exec SP_Save_Empty_EDI '" & dblRunningNo & "','" & Trim(filename) & "'"
                dt1 = db.sub_GetDatatable(strsql)
            End If


            strsql = ""
            strsql = "exec USP_Codeco_Applicable_OUt_nEW  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt2 = db.sub_GetDatatable(strsql)
            If dt2.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found!');", True)
                Exit Sub
            End If
            dt.Clear()
            Strcontainers = ""
            strFST1 = "UNA:+.? '" & vbCrLf
            strFST1 = strFST1 & "UNB+UNOA:1+INEGS+RCL+" & Trim(strDateandTime) & "+" & Trim(dblRunningNo) & "" & Trim("'") & "" & vbCrLf
            dblTotalContainer = 1



            strsql = ""
            strsql = "exec USP_Codeco_Applicable_OUt_nEW  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt3 = db.sub_GetDatatable(strsql)
            For i As Integer = 0 To dt3.Rows.Count - 1
                strContainerNo = ""
                strISOCode = ""
                strFST = "UNH+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "+" & Trim("CODECO:D:95B:UN:ITG14'") & "" & vbCrLf
                strFST = strFST & "BGM+36+11-1001+9'" & vbCrLf
                strFST = strFST & "TDT+20++1++" & Trim((dt3.Rows(i)("transporter"))) & "" & Trim(":172:87+++") & Trim((dt3.Rows(i)("transporter"))) & ":146::" & Trim((dt3.Rows(i)("transporter"))) & "'" & vbCrLf
                strFST = strFST & "NAD+MS+INEGS:160:87'" & vbCrLf
                strContainerNo = Trim(dt3.Rows(i)("containerno"))

                strsql = ""
                strsql = "SELECT * FROM ISOCODES where Isoid=" & Val(dt3.Rows(i)("isocodeid")) & ""
                dt4 = db.sub_GetDatatable(strsql)
                If dt4.Rows.Count > 0 Then
                    strISOCode = Trim(dt4.Rows(0)("isocode"))
                End If
                strFST = strFST & "EQD+CN+" & Trim(strContainerNo) & "+" & Trim(strISOCode) & "" & Trim(":102:5++2+4'") & "" & vbCrLf
                strFST = strFST & "RFF+BN:" & "" & Trim((dt3.Rows(i)("bookingno"))) & "" & "'" & vbCrLf
                strFST = strFST & "DTM+7:" & Format(dt3.Rows(i)("Outdate"), "yyyyMMddHHmm") & ":" & Trim("203'") & "" & vbCrLf
                strFST = strFST & "LOC+165+INEGS:139:6+:STO:ZZZ'" & vbCrLf
                strFST = strFST & "MEA+AAE+T+KGM:" & (dt3.Rows(i)("tareweight")) & "" & "'" & vbCrLf
                strFST = strFST & "SEL+RCL1+AB'" & vbCrLf
                ' strFST = strFST & "TDT+1++3+3+++++" & Trim((dt3.Rows(i)("transporter"))) & "" & ":146:ZZZ'" & vbCrLf
                strFST = strFST & "TDT+1++3+++++" & Trim((dt3.Rows(i)("transporter"))) & "" & ":146:ZZZ'" & vbCrLf
                strFST = strFST & "CNT+16:1'" & vbCrLf
                strFST = strFST & "UNT+13+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "" & "'" & vbCrLf
                If Trim(dblTotalContainer) = 1 Then
                    strSEC = strSEC & strFST1 & strFST
                Else
                    strSEC = strSEC & strFST
                End If

                strsql = ""
                strsql = "UPDATE eyardemptyout SET IsAutoMailedline=1 WHERE EntryID='" & dt3.Rows(i)("EntryID") & "' and ContainerNo='" & dt3.Rows(i)("ContainerNo") & "'"

                dt5 = db.sub_GetDatatable(strsql)

                dblTotalContainer = dblTotalContainer + 1

                If Trim(Strcontainers) = "" Then
                    Strcontainers = strContainerNo
                Else
                    Strcontainers = Strcontainers & ", " & strContainerNo
                End If

            Next

            strSEC = strSEC & "UNZ+" & Trim(dblTotalContainer) - 1 & "+" & Trim(dblRunningNo) & "" & "'" & vbCrLf
            If strSEC <> "" Then
                strSEC = Mid(strSEC, 1, Len(strSEC) - 2)
            End If

            filename = "GATE_OUT_RCL_INEGS_" & Convert.ToDateTime(Now).ToString("yyyyMMddHHmm") & ".EDI"
            Dim strfilePath As String = ""
            strfilePath = Server.MapPath("~/XMLFiles/")
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

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub sub_GenerateEDEMPTYIN_HMM()
        Try
            'Dim intfilenum As Integer
            Dim filename As String
            Dim dblRunningNo As String
            Dim strFST As String
            Dim strFST1 As String
            Dim strContainerNo As String
            Dim strDateandTime As String
            Dim dblTotalContainer As Integer
            Dim strISOCode As String
            Dim Strcontainers As String
            strDateandTime = Format(Now, "yyMMdd:HHmm")
            Dim strSEC As String

            Dim strsql As String
            Dim dt As DataTable

            strsql = ""
            strsql = "select  max(runningno)as runningno from Empty_EDI  "
            dt = db.sub_GetDatatable(strsql)

            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("runningno")) = True Then
                    dblRunningNo = 1
                Else
                    dblRunningNo = Val(dt.Rows(0)(0)) + 1
                End If
                filename = "GATEIN_EPKINNSA_HMM_" & Format(Now, "ddMMyyyyHHmm")
                strsql = ""
                strsql = "exec SP_Save_Empty_EDI '" & dblRunningNo & "','" & Trim(filename) & "'"
                dt1 = db.sub_GetDatatable(strsql)
            End If


            strsql = ""
            strsql = "exec USP_Codeco_Applicable_In  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt2 = db.sub_GetDatatable(strsql)
            If dt2.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found!');", True)
                Exit Sub
            End If
            Strcontainers = ""
            strFST1 = "UNA:+.? '" & vbCrLf
            strFST1 = strFST1 & "UNB+UNOA:1+EPKINNSA+HMM+" & Trim(strDateandTime) & "+" & Trim(dblRunningNo) & "" & Trim("'") & "" & vbCrLf
            dblTotalContainer = 1

            Dim dt3 As New DataTable
            strsql = ""
            strsql = "exec USP_Codeco_Applicable_In  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt3 = db.sub_GetDatatable(strsql)
            For i As Integer = 0 To dt3.Rows.Count - 1
                strContainerNo = ""
                strISOCode = ""
                strFST = "UNH+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "+" & Trim("CODECO:D:95B:UN:ITG14'") & "" & vbCrLf
                strFST = strFST & "BGM+34+10-1001+9'" & vbCrLf
                strFST = strFST & "TDT+20++1++:172:20+++:::'" & vbCrLf
                strFST = strFST & "NAD+CF+EPKINNSA:172:20'" & vbCrLf
                strContainerNo = Trim(dt3.Rows(i)("containerno"))

                Dim dt4 As New DataTable
                strsql = "SELECT * FROM ISOCODES where Isoid=" & Val(dt3.Rows(i)("isocodeid")) & ""
                dt4 = db.sub_GetDatatable(strsql)
                If dt4.Rows.Count > 0 Then
                    strISOCode = Trim(dt4.Rows(0)("isocode"))
                End If
                strFST = strFST & "EQD+CN+" & Trim(strContainerNo) & "+" & Trim(strISOCode) & "" & Trim(":102:5++2+4'") & "" & vbCrLf
                strFST = strFST & "RFF+BN:'" & vbCrLf
                strFST = strFST & "TMD+4'" & vbCrLf
                strFST = strFST & "DTM+7:" & Format(dt3.Rows(i)("InDate"), "yyyyMMddHHmm") & ":" & Trim("203'") & "" & vbCrLf
                strFST = strFST & "LOC+165+INNSA:139:6+EPKINNSA:STO:ZZZ'" & vbCrLf
                strFST = strFST & "MEA+AAE+T+KGM:" & (dt3.Rows(i)("tareweight")) & "" & "'" & vbCrLf
                strFST = strFST & "SEL+NA+CA+1'" & vbCrLf
                strFST = strFST & "TDT+1++3+31++++" & Trim(dt3.Rows(i)("truckno")) & "" & "'" & vbCrLf
                strFST = strFST & "CNT+1:1'" & vbCrLf
                strFST = strFST & "UNT+15+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "" & "'" & vbCrLf
                If Trim(dblTotalContainer) = 1 Then
                    strSEC = strSEC & strFST1 & strFST
                Else
                    strSEC = strSEC & strFST
                End If

                strsql = ""
                strsql = "UPDATE Eyard_In SET IsAutoMailedline=1 WHERE EntryID='" & dt3.Rows(i)("EntryID") & "' and ContainerNo='" & dt3.Rows(i)("ContainerNo") & "'"

                dt5 = db.sub_GetDatatable(strsql)

                If Trim(Strcontainers) = "" Then
                    Strcontainers = strContainerNo
                Else
                    Strcontainers = Strcontainers & ", " & strContainerNo
                End If

                dblTotalContainer = dblTotalContainer + 1
            Next

            strSEC = strSEC & "UNZ+" & Trim(dblTotalContainer) - 1 & "+" & Trim(dblRunningNo) & "" & "'" & vbCrLf

            If strSEC <> "" Then
                strSEC = Mid(strSEC, 1, Len(strSEC) - 2)
            End If

            filename = "GATEIN_EPK_HMM_" & Convert.ToDateTime(Now).ToString("yyyyMMddHHmm") & ".txt"
            Dim strfilePath As String = ""
            strfilePath = Server.MapPath("~/XMLFiles/")
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

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub sub_GenerateEDEMPTYOut_HMM()
       Try
            'Dim intfilenum As Integer
            Dim filename As String
            Dim dblRunningNo As String
            Dim strFST As String
            Dim strFST1 As String
            Dim strContainerNo As String
            Dim strDateandTime As String
            Dim dblTotalContainer As Integer
            Dim strISOCode As String
            Dim Strcontainers As String
            strDateandTime = Format(Now, "yyMMdd:HHmm")
            Dim strSEC As String

            Dim strsql As String
            Dim dt As DataTable

            strsql = ""
            strsql = "select  max(runningno)as runningno from Empty_EDI  "
            dt = db.sub_GetDatatable(strsql)

            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("runningno")) = True Then
                    dblRunningNo = 1
                Else
                    dblRunningNo = Val(dt.Rows(0)(0)) + 1
                End If
                filename = "GATEOUT_EPK_HMM_" & Format(Now, "ddMMyyyyHHmm")
                strsql = ""
                strsql = "exec SP_Save_Empty_EDI '" & dblRunningNo & "','" & Trim(filename) & "'"
                dt1 = db.sub_GetDatatable(strsql)
            End If


            strsql = ""
            strsql = "exec USP_Codeco_Applicable_OUt_nEW  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt2 = db.sub_GetDatatable(strsql)
            If dt2.Rows.Count = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found!');", True)
                Exit Sub
            End If
            dt.Clear()
            Strcontainers = ""
            strFST1 = "UNA:+.? '" & vbCrLf
            strFST1 = strFST1 & "UNB+UNOA:1+EPKINNSA+HMM+" & Trim(strDateandTime) & "+" & Trim(dblRunningNo) & "" & Trim("'") & "" & vbCrLf
            dblTotalContainer = 1



            strsql = ""
            strsql = "exec USP_Codeco_Applicable_OUt_nEW  '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MMM-dd") & "' , '" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MMM-dd") & "',  " & Val(ddlShipping.SelectedValue) & " "
            dt3 = db.sub_GetDatatable(strsql)
            For i As Integer = 0 To dt3.Rows.Count - 1
                strContainerNo = ""
                strISOCode = ""
                strFST = "UNH+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "+" & Trim("CODECO:D:95B:UN:ITG14'") & "" & vbCrLf
                strFST = strFST & "BGM+36+11-1001+9'" & vbCrLf
                strFST = strFST & "TDT+20++1++:172:20+++:::'" & vbCrLf
                strFST = strFST & "NAD+CF+EPKINNSA:172:20'" & vbCrLf
                strContainerNo = Trim(dt3.Rows(i)("containerno"))

                strsql = ""
                strsql = "SELECT * FROM ISOCODES where Isoid=" & Val(dt3.Rows(i)("isocodeid")) & ""
                dt4 = db.sub_GetDatatable(strsql)
                If dt4.Rows.Count > 0 Then
                    strISOCode = Trim(dt4.Rows(0)("isocode"))
                End If
                strFST = strFST & "EQD+CN+" & Trim(strContainerNo) & "+" & Trim(strISOCode) & "" & Trim(":102:5++2+4'") & "" & vbCrLf
                strFST = strFST & "RFF+BN:" & "" & Trim((dt3.Rows(i)("bookingno"))) & "" & "'" & vbCrLf
                strFST = strFST & "TMD+4'" & vbCrLf
                strFST = strFST & "DTM+7:" & Format(dt3.Rows(i)("Outdate"), "yyyyMMddHHmm") & ":" & Trim("203'") & "" & vbCrLf
                strFST = strFST & "LOC+165+INNSA:139:6+EPKINNSA:STO:ZZZ'" & vbCrLf
                strFST = strFST & "MEA+AAE+T+KGM:" & (dt3.Rows(i)("tareweight")) & "" & "'" & vbCrLf
                strFST = strFST & "SEL++CA+1'" & vbCrLf
                strFST = strFST & "TDT+1++3+31+ANY:172:87+++:146'" & vbCrLf
                strFST = strFST & "CNT+1:1'" & vbCrLf
                strFST = strFST & "UNT+14+" & Trim(dblRunningNo) & "" & Trim("A") & "" & Trim(dblTotalContainer) & "" & "'" & vbCrLf
                If Trim(dblTotalContainer) = 1 Then
                    strSEC = strSEC & strFST1 & strFST
                Else
                    strSEC = strSEC & strFST
                End If

                strsql = ""
                strsql = "UPDATE eyardemptyout SET IsAutoMailedline=1 WHERE EntryID='" & dt3.Rows(i)("EntryID") & "' and ContainerNo='" & dt3.Rows(i)("ContainerNo") & "'"

                dt5 = db.sub_GetDatatable(strsql)

                dblTotalContainer = dblTotalContainer + 1

                If Trim(Strcontainers) = "" Then
                    Strcontainers = strContainerNo
                Else
                    Strcontainers = Strcontainers & ", " & strContainerNo
                End If

            Next

            strSEC = strSEC & "UNZ+" & Trim(dblTotalContainer) - 1 & "+" & Trim(dblRunningNo) & "" & "'" & vbCrLf
            If strSEC <> "" Then
                strSEC = Mid(strSEC, 1, Len(strSEC) - 2)
            End If

            filename = "GATEOUT_EPK_HMM_" & Convert.ToDateTime(Now).ToString("yyyyMMddHHmm") & ".txt"
            Dim strfilePath As String = ""
            strfilePath = Server.MapPath("~/XMLFiles/")
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

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

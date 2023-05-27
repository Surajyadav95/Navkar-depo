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
    Dim dt, dt1, dt10 As DataTable
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txtason.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            Filldropdown()

        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_LineName"
            ds = db.sub_GetDataSets(strSql)
            ddlLineName.DataSource = ds.Tables(0)
            ddlLineName.DataTextField = "SLName"
            ddlLineName.DataValueField = "SLID"
            ddlLineName.DataBind()
            ddlLineName.Items.Insert(0, New ListItem("All", 0))

            ddljoype.DataSource = ds.Tables(2)
            ddljoype.DataTextField = "Jo_Type"
            ddljoype.DataValueField = "Jo_Type_ID"
            ddljoype.DataBind()
            ddljoype.Items.Insert(0, New ListItem("All", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim Name As String = "", dbl20 As Double = 0, dbl40 As Double = 0, dbl45 As Double = 0
            If ddlcriteria.SelectedValue = 1 Then
                Name = Val(ddlLineName.SelectedValue)
            Else
                Name = Val(ddljoype.SelectedValue)
            End If
            strSql = ""
            strSql += "SP_SealEntryInventoryS '" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlLineName.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdRegistrationSummary.DataSource = dt
            grdRegistrationSummary.DataBind()
            'For i = 0 To dt.Rows.Count - 1
            '    If dt.Rows(i)("Size") = "20" Then
            '        dbl20 += 1
            '    ElseIf dt.Rows(i)("Size") = "40" Then
            '        dbl40 += 1
            '    ElseIf dt.Rows(i)("Size") = "45" Then
            '        dbl45 += 1
            '    End If
            'Next
            'lbl20.Text = dbl20
            'lbl40.Text = dbl40
            'lbl45.Text = dbl45
            'lblTEUS.Text = Val(dbl20) + Val(dbl40) * 2 + Val(dbl45) * 2
            'up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grdRegistrationSummary_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdRegistrationSummary.PageIndex = e.NewPageIndex
        Me.btnSave_Click(sender, e)
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try

            strSql = ""
            strSql += "SP_SealEntryInventoryS '" & Convert.ToDateTime(Trim(txtason.Text & "")).ToString("yyyyMMddHHmmss") & "','" & Trim(ddlLineName.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
           
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Seal Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(9)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("A5:" & Excelno & "5").Merge()
                        .Range("A6:" & Excelno & "6").Merge()
                        .Range("A7:" & Excelno & "7").Merge()
                        .Range("A8:" & Excelno & "8").Merge()
                        .Range("A9:" & Excelno & "9").Merge()

                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
                        .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
                        .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(7).Height = 20
                        '.Cell(6, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(6, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        If ddlcriteria.SelectedValue = 0 Then
                            .Cell(7, 1).Value = "All"
                        ElseIf ddlcriteria.SelectedValue = 1 Then
                            .Cell(7, 1).Value = ddlcriteria.SelectedItem.Text + ": " + ddlLineName.SelectedItem.Text
                        End If

                        .Cell(8, 1).Value = "Seal Details"
                        .Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(2, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(2, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(4, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(4, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(5, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(5, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Font.FontSize = 17
                        .Cell(8, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Column(26).Width = 50
                     

                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=SealSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        'getdatewiseWO()
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
    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddlLineName.SelectedValue = 0
            If ddlcriteria.SelectedValue = 0 Then
                divShippingLine.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 1 Then
                divShippingLine.Attributes.Add("style", "display:block")
            End If

            If ddlcriteria.SelectedValue = 0 Then
                divJotype.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = 2 Then
                divJotype.Attributes.Add("style", "display:block")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

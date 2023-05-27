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
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            Filldropdown()
            btnSave_Click(sender, e)
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "usp_fill_out_cmb"
            ds = db.sub_GetDataSets(strSql)
            ddlLineName.DataSource = ds.Tables(0)
            ddlLineName.DataTextField = "SLName"
            ddlLineName.DataValueField = "SLID"
            ddlLineName.DataBind()
            ddlLineName.Items.Insert(0, New ListItem("All", 0))

            ddlCustomer.DataSource = ds.Tables(1)
            ddlCustomer.DataTextField = "SLName"
            ddlCustomer.DataValueField = "SLID"
            ddlCustomer.DataBind()
            ddlCustomer.Items.Insert(0, New ListItem("All", 0))



            ddljoype.DataSource = ds.Tables(2)
            ddljoype.DataTextField = "Jo_Type"
            ddljoype.DataValueField = "Jo_Type_ID"
            ddljoype.DataBind()
            ddljoype.Items.Insert(0, New ListItem("All", 0))


        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            'ddlLineName.SelectedValue = 0
            If ddlcriteria.SelectedValue = "All" Then
                divLine.Attributes.Add("style", "display:none")
                divCustomer.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:none")
                divJotype.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = "Line Name" Then
                divLine.Attributes.Add("style", "display:block")
                divCustomer.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:none")
                divJotype.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = "Customer" Then
                divCustomer.Attributes.Add("style", "display:block")
                divLine.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:none")
                divJotype.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = "Container No" Then
                divCustomer.Attributes.Add("style", "display:none")
                divLine.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:block")
                divJotype.Attributes.Add("style", "display:none")
            ElseIf ddlcriteria.SelectedValue = "Jo Type" Then
                divCustomer.Attributes.Add("style", "display:none")
                divLine.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:none")
                divJotype.Attributes.Add("style", "display:block")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try

            Dim strSearchText As String = ""
            If ddlcriteria.SelectedValue = "Line Name" Then
                strSearchText = ddlLineName.SelectedValue
            ElseIf ddlcriteria.SelectedValue = "Customer" Then
                strSearchText = ddlCustomer.SelectedItem.Text
            ElseIf ddlcriteria.SelectedValue = "Container No" Then
                strSearchText = Trim(txtContainerNo.Text)
            ElseIf ddlcriteria.SelectedValue = "Jo Type" Then
                strSearchText = Trim(ddljoype.SelectedItem.Text)

            End If

            strSql = ""
            strSql += " Get_Sp_EyardOutDetails '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlcriteria.SelectedItem.Text & "") & "',"
            strSql += "'" & strSearchText & "'"
            dt1 = db.sub_GetDatatable(strSql)
            grdRegistrationSummary.DataSource = dt1
            grdRegistrationSummary.DataBind()

            Dim dbl20 As Double = 0, dbl40 As Double = 0, dbl45 As Double = 0, dblTEUS As Double = 0
            For i As Integer = 0 To dt1.Rows.Count - 1
                If dt1.Rows(i)("Size") = 20 Then
                    dbl20 = dbl20 + 1
                End If
                If dt1.Rows(i)("Size") = 40 Then
                    dbl40 = dbl40 + 1
                End If
                If dt1.Rows(i)("Size") = 45 Then
                    dbl45 = dbl45 + 1
                End If
            Next
            dblTEUS = dbl20 + (dbl40 * 2) + (dbl45 * 2)
            lbl20.Text = dbl20
            lbl40.Text = dbl40
            lbl45.Text = dbl45
            lblteus.Text = dblTEUS
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
            If (Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd") > Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid date selection');", True)
                Exit Sub
            End If

            Dim strSearchText As String = ""
            If ddlcriteria.SelectedValue = "Line Name" Then
                strSearchText = ddlLineName.SelectedValue
            ElseIf ddlcriteria.SelectedValue = "Customer" Then
                strSearchText = ddlCustomer.SelectedItem.Text
            End If
            strSql = ""
            strSql += " Get_Sp_EyardOutDetails '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ddlcriteria.SelectedItem.Text & "") & "',"
            strSql += "'" & strSearchText & "'"
            dt = db.sub_GetDatatable(strSql)
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Gate Out Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(dt.Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(12)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Range("A3:" & Excelno & "3").Merge()
                        .Range("A4:" & Excelno & "4").Merge()
                        .Range("A5:" & Excelno & "5").Merge()
                        .Range("A6:" & Excelno & "6").Merge()
                        .Range("A7:" & Excelno & "7").Merge()
                        .Range("A8:" & Excelno & "8").Merge()
                        .Range("A9:" & Excelno & "9").Merge()
                        .Range("A10:" & Excelno & "10").Merge()
                        .Range("A11:" & Excelno & "11").Merge()
                        .Range("A12:" & Excelno & "12").Merge()

                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(dt10.Rows(0)("con_NameI") & "")
                        .Cell(3, 1).Value = Trim(dt10.Rows(0)("AddressI") & "")
                        .Cell(4, 1).Value = Trim(dt10.Rows(0)("AddressII") & "")

                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 30
                        .Row(6).Height = 20
                        .Row(10).Height = 20
                        .Cell(10, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(10, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(11, 1).Value = "Gate Out Summary"
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
                        .Cell(6, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(6, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(8, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(8, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(6, 1).Style.Font.FontSize = 17
                        .Cell(1, 1).Style.Font.FontSize = 20
                        .Cell(11, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center
                        .Cell(11, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center
                        .Cell(11, 1).Style.Font.FontSize = 17
                        Dim intRow As Integer = 13
                        For i = 0 To dt.Rows.Count - 1
                            intRow += 1
                            .Cell(intRow, 12).DataType = XLCellValues.DateTime
                            .Cell(intRow, 13).DataType = XLCellValues.DateTime
                        Next
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=GateOutSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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

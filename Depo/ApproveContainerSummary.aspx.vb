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
    Dim dt, dt1, dt5, dt10 As DataTable
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'txtApprovedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtApprovedate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            ddlSearchOn.Text = "All"
            Filldropdown()
            btnShow_Click(sender, e)

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
            strSql += " GetContainerforApproval '" & Trim(ddlSearchOn.SelectedItem.Text) & "',"
            If Trim(ddlSearchOn.SelectedItem.Text) = "Shipping Line" Then
                strSql += "'" & Trim(ddlShipline.SelectedItem.Text) & "'"
            ElseIf Trim(ddlSearchOn.SelectedItem.Text) = "Container No" Then
                strSql += "'" & Trim(TxtContainerNo.Text) & "'"
            Else
                strSql += "''"
            End If
            dt = db.sub_GetDatatable(strSql)
            grdRegistrationSummary.DataSource = dt
            grdRegistrationSummary.DataBind()

            Dim dbl20 As Double = 0, dbl40 As Double = 0, dbl45 As Double = 0, dblTEUS As Double = 0
            For i As Integer = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("Size") = 20 Then
                    dbl20 = dbl20 + 1
                End If
                If dt.Rows(i)("Size") = 40 Then
                    dbl40 = dbl40 + 1
                End If
                If dt.Rows(i)("Size") = 45 Then
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
        Me.btnShow_Click(sender, e)
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

    Protected Sub lnkApprove_Click(sender As Object, e As EventArgs)
        Try
            Call btnMultiApprove_Click(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnMultiApprove_Click(sender As Object, e As EventArgs)
        Try
            Dim intestimate_ID As Double, intamount As Double = 0
            For Each row As GridViewRow In grdRegistrationSummary.Rows
                 

                    Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                    strSql = ""
                    strSql = "usp_Estimate_Approve_Summary'" & Trim(CType(row.FindControl("lblEstimate_ID"), Label).Text) & "','" & Trim(CType(row.FindControl("lblEstimateAmount"), Label).Text) & "','" & Session("UserId_DepoCFS") & "'"
                    db.sub_ExecuteNonQuery(strSql)

                    strSql = ""
                    strSql = "Select * from Estimate_D Where Estimate_ID='" & Val(CType(row.FindControl("lblEstimate_ID"), Label).Text) & "'"
                    dt5 = db.sub_GetDatatable(strSql)
                    If dt5.Rows.Count > 0 Then
                        For i = 0 To dt5.Rows.Count - 1
                            intamount = 0
                            intamount = Val(dt5.Rows(i)("Amount"))
                            strSql = ""
                            strSql = "update Estimate_D set isuploaded=0, AppAmount='" & Trim(intamount) & "'"
                            strSql += " where Estimate_ID='" & Val(CType(row.FindControl("lblEstimate_ID"), Label).Text) & "' and SrNo='" & dt5.Rows(i)("SrNo") & "'"
                            db.sub_ExecuteNonQuery(strSql)
                        Next
                    End If
                    strSql = ""
                    strSql = "Update EYard_stock SET isuploaded=0, ApprovedDate='" & Convert.ToDateTime(Trim(txtApprovedate.Text & "")).ToString("yyyy-MM-dd") & "'  Where ContainerNo='" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "' and EntryID='" & Trim(CType(row.FindControl("lblEntryID"), Label).Text) & "'"
                    db.sub_ExecuteNonQuery(strSql)

            Next
            lblSession.Text = "Approved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
             
            Dim strSearchText As String = ""

            strSql = ""
            strSql += " GetContainerforApproval '" & Trim(ddlSearchOn.SelectedItem.Text) & "',"
            If Trim(ddlSearchOn.SelectedItem.Text) = "Shipping Line" Then
                strSql += "'" & Trim(ddlShipline.SelectedItem.Text) & "'"
            ElseIf Trim(ddlSearchOn.SelectedItem.Text) = "Container No" Then
                strSql += "'" & Trim(TxtContainerNo.Text) & "'"
            End If
            dt = db.sub_GetDatatable(strSql)
            dt.Columns.Remove("Select")
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Unappoved Containers" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
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
                        '.Cell(10, 1).Value = "From: " + Convert.ToDateTime(txtfromDate.Text).ToString("dd MMM yyyy") + " to " + Convert.ToDateTime(txttoDate.Text).ToString("dd MMM yyyy")
                        .Cell(10, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(11, 1).Value = "UnappovedContainers"
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
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=UnappovedContainers" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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

Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
'Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.Mime
Imports System.Threading
Imports System.ComponentModel
Imports ClosedXML.Excel


Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt10 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode

    Dim dtSPID As New DataTable
    Dim dtTempI As New DataTable
    Dim strDesc As String = ""
    Dim strtable As String = ""
    Dim strText As String = ""
    Dim strMailTo As String = ""
    Dim strCcIDs As String = ""
    Dim strToIDs As String = ""
    Dim strSubject As String = ""
    Dim strBodyText As String = ""
    'Dim warnings As Microsoft.Reporting.WebForms.Warning() = Nothing

    'Dim streamids As String() = Nothing

    'Dim mimeType As String = Nothing

    'Dim encoding As String = Nothing

    'Dim extension As String = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Filldropdown()
            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")
            btnsearch_Click(sender, e)
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_LineName"
            ds = db.sub_GetDataSets(strSql)
            ddlShipping.DataSource = ds.Tables(0)
            ddlShipping.DataTextField = "SLName"
            ddlShipping.DataValueField = "SLID"
            ddlShipping.DataBind()
            ddlShipping.Items.Insert(0, New ListItem("All", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_Vehicle_Assessment_summary '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00") & "',"
            strSql += "'" & Trim(ddlcriteria.SelectedValue & "") & "','" & Trim(txtAssessno.Text & "") & "','" & Trim(txtContainerNo.Text & "") & "','" & Trim(ddlShipping.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdSummary.DataSource = dt
            grdSummary.DataBind()
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        grdSummary.DataSource = dt
        grdSummary.PageIndex = e.NewPageIndex
        Me.btnsearch_Click(sender, e)
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub ddlcriteria_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            txtAssessno.Text = ""
            'txtnocno.Text = ""
            txtContainerNo.Text = ""
            'ddlinvoicetype.SelectedValue = 0
            If ddlcriteria.SelectedValue = 0 Then
                divInvoiceno.Attributes.Add("style", "display:none")
                divShipping.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:none")

            ElseIf ddlcriteria.SelectedValue = 1 Then
                divInvoiceno.Attributes.Add("style", "display:block")
                divShipping.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:none")

            ElseIf ddlcriteria.SelectedValue = 2 Then
                divInvoiceno.Attributes.Add("style", "display:none")
                divShipping.Attributes.Add("style", "display:none")
                divContainerNo.Attributes.Add("style", "display:block")

            ElseIf ddlcriteria.SelectedValue = 3 Then
                divInvoiceno.Attributes.Add("style", "display:none")
                divShipping.Attributes.Add("style", "display:block")
                divContainerNo.Attributes.Add("style", "display:none")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim AssessNo As String = lnkcancel.CommandArgument
            Dim WorkYear As String = grdSummary.DataKeys(row.RowIndex).Value.ToString()
            Dim str As String = ""
            txtAssessno.Text = AssessNo
            TxtWorkYear.Text = WorkYear
            'strSql = ""
            'strSql += "UPDATE eyard_assessM set iscancel=1,cancelledby='" & Session("UserID") & "',CancelledOn=getdate() where InvoiceNo='" & AssessNo & "' and WorkYear='" & WorkYear & "'"
            'db.sub_ExecuteNonQuery(strSql)
            'btnsearch_Click(sender, e)
            'lblsession.Text = "Assessment Cancelled Successfully"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate2", "$('#myModalforupdate2').modal();", True)
            'UpdatePanel6.Update()
            ClientScript.RegisterStartupScript(Page.GetType(), "OpenList", "<script>OpenCancelInvoice(); </script>")

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkAnnexure_Click(sender As Object, e As EventArgs)
        Try
            Dim gr As GridViewRow = CType(CType(sender, LinkButton).NamingContainer, GridViewRow)
            Dim lnkAnnexure As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkAnnexure.Parent.Parent, GridViewRow)
            Dim AssessNo As String = lnkAnnexure.CommandArgument
            Dim WorkYear As String = grdSummary.DataKeys(row.RowIndex).Value.ToString()
            Dim LineID As String = Trim(CType(gr.FindControl("lblLineID"), Label).Text)
            Dim InvoiceType As String = Trim(CType(gr.FindControl("lblInvoiceType"), Label).Text)
            Dim dblCount As Double = 0
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            
            strSql = ""
            strSql += "USP_INVOICE_WISE_ANNEXURE_SEPARATE '" & AssessNo & "','" & WorkYear & "','" & InvoiceType & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(ds.Tables(0), "Annexure")
                    With wb.Worksheets(0)
                        Dim Excelno As String = ""
                        Excelno = db.GetExcelColumnName(ds.Tables(0).Columns.Count)
                        .Range("A1:" & Excelno & "1").InsertRowsAbove(2)
                        .Range("A1:" & Excelno & "1").Merge()
                        .Range("A2:" & Excelno & "2").Merge()
                        .Cell(1, 1).Value = Trim(dt10.Rows(0)("con_Name") & "")
                        .Cell(2, 1).Value = Trim(ds.Tables(1).Rows(0)("InvoiceType") & "") + " for " + Trim(ds.Tables(1).Rows(0)("Month") & "")
                        .Cell(1, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Cell(2, 1).Style.Fill.BackgroundColor = XLColor.LightGray
                        .Row(1).Height = 20
                        .Row(2).Height = 20
                        .Cell(1, 1).Style.Font.FontSize = 14
                        .Cell(2, 1).Style.Font.FontSize = 14
                        For i = 1 To ds.Tables(0).Columns.Count
                            .Cell(ds.Tables(0).Rows.Count + 3, i).Style.Fill.BackgroundColor = XLColor.Yellow
                        Next
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=Annexure(" & AssessNo & ").xlsx")
                    Using MyMemoryStream As New MemoryStream()
                        wb.SaveAs(MyMemoryStream)
                        MyMemoryStream.WriteTo(Response.OutputStream)
                        Response.Flush()
                        Response.End()
                    End Using
                End Using
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try

         strSql = ""
            strSql += " USP_Vehicle_Assessment_summary '" & Convert.ToDateTime(txtfromDate.Text).ToString("yyyy-MM-dd 00:00:00 ") & "','" & Convert.ToDateTime(txttoDate.Text).ToString("yyyy-MM-dd 23:59:00") & "',"
            strSql += "'" & Trim(ddlcriteria.SelectedValue & "") & "','" & Trim(txtAssessno.Text & "") & "','" & Trim(txtContainerNo.Text & "") & "','" & Trim(ddlShipping.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            grdSummary.DataSource = dt
            grdSummary.DataBind()
            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Praking Charges" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
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
                        .Cell(11, 1).Value = "PrakingCharges"
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
                    Response.AddHeader("content-disposition", "attachment;filename=PrakingCharges" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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

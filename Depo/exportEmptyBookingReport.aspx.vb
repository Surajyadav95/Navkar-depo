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
    Dim dt, dt1, dt10 As DataTable
    Dim ds As DataSet
    Dim ed As New clsEncodeDecode
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then


            txtfromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txttoDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
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

            ds = db.sub_GetDataSets(strSql)
            ddltransporter.DataSource = ds.Tables(1)
            ddltransporter.DataTextField = "TransName"
            ddltransporter.DataValueField = "TransID"
            ddltransporter.DataBind()
            ddltransporter.Items.Insert(0, New ListItem("All", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            Dim strsearch As String = ""
            If ddlSearchOn.SelectedItem.Text = "All" Then
            ElseIf ddlSearchOn.Text = "Line Name" Then
                strsearch = Trim(ddlShipline.SelectedValue & "")
            ElseIf ddlSearchOn.SelectedItem.Text = "Shipper Name" Then
                strsearch = Trim(txtbookingno.Text & "")
            ElseIf ddlSearchOn.SelectedItem.Text = "Booking No" Then
                strsearch = Trim(txtbookingno.Text & "")
            ElseIf ddlSearchOn.SelectedItem.Text = "Transporter" Then
                strsearch = Trim(ddltransporter.SelectedItem.Text & "")
            End If
            strSql = ""
            strSql += " Sp_eyard_BookingReport '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlSearchOn.SelectedItem.Text) & "',"
            strSql += "'" & strsearch & "'"
            dt = db.sub_GetDatatable(strSql)
            grdRegistrationSummary.DataSource = dt
            grdRegistrationSummary.DataBind()
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
            Dim strsearch As String = ""
            If ddlSearchOn.SelectedItem.Text = "All" Then
            ElseIf ddlSearchOn.Text = "Line Name" Then
                strsearch = Trim(ddlShipline.SelectedValue & "")
            ElseIf ddlSearchOn.SelectedItem.Text = "Shipper Name" Then
                strsearch = Trim(txtbookingno.Text & "")
            ElseIf ddlSearchOn.SelectedItem.Text = "Booking No" Then
                strsearch = Trim(txtbookingno.Text & "")
            ElseIf ddlSearchOn.SelectedItem.Text = "Transporter" Then
                strsearch = Trim(ddltransporter.SelectedItem.Text & "")
            End If
            strSql = ""
            strSql += " Sp_EyardContainerBookingReport '" & Convert.ToDateTime(Trim(txtfromDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(txttoDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(ddlSearchOn.SelectedItem.Text) & "',"
            strSql += "'" & strsearch & "'"
            dt = db.sub_GetDatatable(strSql)

            strSql = ""
            strSql += "Select * from con_details"
            dt10 = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Container Summary" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
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

                        

                        .Cell(8, 1).Value = "Container Booking Summary"
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
                    End With
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=ContainerBookingSummary" & Convert.ToDateTime(Now).ToString("ddMMyyyyHHmm") & ".xlsx")
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
    Protected Sub ddlSearchOn_SelectedIndexChanged(sender As Object, e As EventArgs)

        If (ddlSearchOn.SelectedValue = "1") Then
            divShiplineName.Attributes.Add("style", "display:block")
            txtshipperName.Text = ""
            txtbookingno.Text = ""

        Else
            divShiplineName.Attributes.Add("style", "display:None")
        End If
        If (ddlSearchOn.SelectedValue = "2") Then
            divshipper.Attributes.Add("style", "display:block")
            ddlShipline.SelectedValue = 0

        Else
            divshipper.Attributes.Add("style", "display:None")
        End If
        If (ddlSearchOn.SelectedValue = "3") Then
            divbooking.Attributes.Add("style", "display:block")
            ddlShipline.SelectedValue = 0

        Else
            divbooking.Attributes.Add("style", "display:None")
        End If

        If (ddlSearchOn.SelectedValue = "4") Then
            divtransporter.Attributes.Add("style", "display:block")
            ddlShipline.SelectedValue = 0
            'txtsearch.Text = ""
        Else
            divtransporter.Attributes.Add("style", "display:None")
        End If

      
    End Sub

    Protected Sub btncancel_Click(sender As Object, e As EventArgs)
        Try
            For Each row As GridViewRow In grdRegistrationSummary.Rows
                Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                If chkright.Checked = True Then

                    strSql = ""
                    strSql = "Select count(*) as Count from eyardemptyout where bookingno='" & Trim(CType(row.FindControl("lblBookingNo"), Label).Text) & "' and iscancel=0 "
                    dt = db.sub_GetDatatable(strSql)
                    ''"Select count(*) from eyardemptyout where bookingno='" & Trim(CType(row.FindControl("lblBookingNo"), Label).Text) & "' and iscancel=0") > 0 Then
                    If dt.Rows(0)("Count") > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Booking No Already out.');", True)
                        Exit Sub

                    Else
                        strSql = ""
                        strSql = "UPDATE Eyard_Gate_Out_Allow_M  set IsCancel =1 ,CancelledBy =" & Session("UserId_DepoCFS") & ",CancelledOn = GETDATE ()  where BookingNo ='" & Trim(CType(row.FindControl("lblBookingNo"), Label).Text) & "' "
                        db.sub_ExecuteNonQuery(strSql)

                    End If
                End If
            Next
            btnShow_Click(sender, e)
            lblSession.Text = "Booking No Cancelled Successfully"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

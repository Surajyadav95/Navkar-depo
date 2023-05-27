Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports System.Data.OleDb
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports ClosedXML.Excel

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim TariffID, TariffIDView As String
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserId_DepoCFS") Is Nothing Then
        '    Session("UserId_DepoCFS") = Request.Cookies("UserIDPRE_Bond").Value
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
            txtwodate.Text = Convert.ToDateTime(Now).ToString("dd-MM-yyyy")
            sub_CreateTable()
            Filldropdown()
            txtContainerNo.Focus()
        End If

    End Sub
    Private Sub sub_CreateTable()
        Dim dtDepoContainer As New DataTable

        dtDepoContainer.Columns.Clear()

        Session("table_DepoContainer") = ""

        dtDepoContainer.Columns.Add("ContainerNo")
        dtDepoContainer.Columns.Add("AccountName")
        dtDepoContainer.Columns.Add("Amount")
        dtDepoContainer.Columns.Add("InvoiceType")
        dtDepoContainer.Columns.Add("InvoiceTypeID")
        dtDepoContainer.Columns.Add("AccountID")
        

        Dim dtRow2 As DataRow = dtDepoContainer.NewRow

        grdOutDets.DataSource = Nothing
        grdOutDets.DataSource = dtDepoContainer
        grdOutDets.DataBind()
        Session("table_DepoContainer") = dtDepoContainer

    End Sub
    Protected Sub Filldropdown()
        Try

            ds = db.sub_GetDataSets("USP_fill_details_accountndetails_Depo")

            ddlacchead.DataSource = ds.Tables(0)
            ddlacchead.DataTextField = "AccountName"
            ddlacchead.DataValueField = "AccountID"
            ddlacchead.DataBind()
            ddlacchead.Items.Insert(0, New ListItem("--Select--", 0))

            grdchargesother.DataSource = ds.Tables(1)
            grdchargesother.DataBind()

            ddlinvoicetype.DataSource = ds.Tables(2)
            ddlinvoicetype.DataTextField = "InvoiceType"
            ddlinvoicetype.DataValueField = "ID"
            ddlinvoicetype.DataBind()
            ddlinvoicetype.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
        
            For Each row As GridViewRow In grdOutDets.Rows
                strSql = ""
                strSql += "USP_validation_additional_save_charges_Depo '" & Trim(txtContainerNo.Text) & "','" & ddlacchead.SelectedValue & "','" & Val(lblLoadedID.Text) & "'"
                ds = db.sub_GetDataSets(strSql)
                If ds.Tables(1).Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('An entry for this record already exists. Cannot save.!');", True)
                    Exit Sub
                End If
                If ds.Tables(0).Rows.Count > 0 Then
                    If Val(ds.Tables(0).Rows(0)(0)) = 0 Then
                        txtwono.Text = 1
                    Else
                        txtwono.Text = Val(ds.Tables(0).Rows(0)(0)) + 1
                    End If
                End If

                strSql = ""
                strSql += "USP_GET_CONTAINER_ID_INVOICETYPE_WISE '" & Val(ddlinvoicetype.SelectedValue) & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If Not dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No " + Trim(CType(row.FindControl("lblContainerNo"), Label).Text) + " not found!');", True)
                    txtContainerNo.Text = ""
                    txtContainerNo.Focus()
                    Exit Sub
                Else
                    lblLoadedID.Text = Val(dt.Rows(0)("EntryID"))
                End If

                strSql = ""
                strSql += "USP_insert_into_Depo_wocharges '" & Trim(txtwono.Text) & "','" & Convert.ToDateTime(txtwodate.Text).ToString("yyyy-MM-dd") & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(ddlacchead.SelectedValue) & "','" & Trim(CType(row.FindControl("lblAmount"), Label).Text & "") & "',"
                strSql += "'" & Trim(txtnarration.Text) & "','" & chkisActive.Checked & "','" & Session("UserId_DepoCFS") & "','" & Trim(lblLoadedID.Text & "") & "','" & Val(ddlinvoicetype.SelectedValue & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
            Next
            Clear()
            txtContainerNo.Text = ""
            ddlinvoicetype.SelectedValue = 0
            lblSession.Text = "Record saved successfully!"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim WONO As String = lnkcancel.CommandArgument
            strSql = ""
            strSql += "UPDATE Eyard_WOCharges  SET IsCancel=1, CancelledBy=" & Session("UserId_DepoCFS") & ", CancelledOn=getdate()"
            strSql += " WHERE WONo=" & WONO & ""
            db.sub_ExecuteNonQuery(strSql)
            lblSession.Text = "Record cancelled successfully!"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtnocno_TextChanged(sender As Object, e As EventArgs)
        Try
            Clear()
            strSql = ""
            strSql += "USP_GET_CONTAINER_ID_INVOICETYPE_WISE '" & Val(ddlinvoicetype.SelectedValue) & "','" & Trim(txtContainerNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If Not dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No not found!');", True)
                txtContainerNo.Text = ""
                txtContainerNo.Focus()
                Exit Sub
            Else
                lblLoadedID.Text = Val(dt.Rows(0)("EntryID"))
            End If
            ddlacchead.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    

    Public Sub Clear()
        Try
            txtwono.Text = ""
            txtwodate.Text = Convert.ToDateTime(Now).ToString("dd-MM-yyyy")            
            ddlacchead.SelectedValue = 0
            txtamtcollect.Text = ""
            chkisActive.Checked = True
            txtnarration.Text = ""

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select  '' [Container No],'' Amount"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Additional Import")
                    With wb.Worksheets(0)
                        .Column(1).Style.DateFormat.Format = "yyyy-MM-dd"
                    End With
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=AdditionalTemplate.xlsx")
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
                    For Each row As IXLRow In workSheet.Rows()
                        If Not Trim(row.Cell(1).Value.ToString()) = "" Then
                            If Not firstRow Then

                                Dim dtRow As DataRow = dtDepoContainer.NewRow
                                dtRow.Item("ContainerNo") = row.Cell(1).Value.ToString()
                                dtRow.Item("AccountName") = Trim(ddlacchead.SelectedItem.Text)
                                dtRow.Item("Amount") = row.Cell(2).Value.ToString()
                                dtRow.Item("InvoiceType") = Trim(ddlinvoicetype.SelectedItem.Text)
                                dtRow.Item("InvoiceTypeID") = Trim(ddlinvoicetype.SelectedValue)
                                dtRow.Item("AccountID") = Trim(ddlinvoicetype.SelectedValue)


                                dtDepoContainer.Rows.Add(dtRow)
                                grdOutDets.DataSource = Nothing
                                grdOutDets.DataSource = dtDepoContainer
                                grdOutDets.DataBind()
                            Else
                                firstRow = False
                            End If
                        End If
lblnext:
                    Next
                End Using
                File.Delete(FilePath)
            End If
           
       
            Session("table_DepoContainer") = dtDepoContainer
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim dtDepoContainer As New DataTable
            dtDepoContainer = DirectCast(Session("table_DepoContainer"), DataTable)
            Dim dtRow As DataRow = dtDepoContainer.NewRow
            dtRow.Item("ContainerNo") = Trim(txtContainerNo.Text)
            dtRow.Item("AccountName") = Trim(ddlacchead.SelectedItem.Text)
            dtRow.Item("Amount") = txtamtcollect.Text
            dtRow.Item("InvoiceType") = Trim(ddlinvoicetype.SelectedItem.Text)
            dtRow.Item("InvoiceTypeID") = Trim(ddlinvoicetype.SelectedValue)
            dtRow.Item("AccountID") = Trim(ddlinvoicetype.SelectedValue)
             
            dtDepoContainer.Rows.Add(dtRow)
            grdOutDets.DataSource = Nothing
            grdOutDets.DataSource = dtDepoContainer
            grdOutDets.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

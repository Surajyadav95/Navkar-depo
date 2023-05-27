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
    Dim dt, dt1, dt2, dt3, dt4, dt9 As DataTable
    Dim db As New dbOperation_Depo
    Dim dbamt As New dbAmountInWords
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim ds, ds1 As DataSet
    Dim TariffID, TariffIDView As String
    Dim strCategory As String
    Dim strCategoryDetails As String
    Dim strCategorySPName As String
    Dim ed As New clsEncodeDecode
    Dim dbltotalamount As Double = 0
    Dim csvPath As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("delete from TEMP_GENERATE_ESTIMATES where USER_ID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Modify_Estimate Where User_ID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Estimate_Import Where userID=" & Session("UserId_DepoCFS") & "")

            txtEstimateDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtMFDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM")

            FillGrid()
            Filldropdown()
            txtContainerNo.Focus()
        End If
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql = "USP_FILL_DROPDOWN_ESTIMATE_ENTRY"

            ds = db.sub_GetDataSets(strSql)

            ddlSurveyType.DataSource = ds.Tables(0)
            ddlSurveyType.DataTextField = "Type"
            ddlSurveyType.DataValueField = "ID"
            ddlSurveyType.DataBind()
            ddlSurveyType.Items.Insert(0, New ListItem("--Select--", 0))

            'ddlMaterialType.DataSource = ds.Tables(1)
            'ddlMaterialType.DataTextField = "Material_Type"
            'ddlMaterialType.DataValueField = "Material_Type_ID"
            'ddlMaterialType.DataBind()
            'ddlMaterialType.Items.Insert(0, New ListItem("--Select--", 0))

            ddlEstimateType.DataSource = ds.Tables(2)
            ddlEstimateType.DataTextField = "Type"
            ddlEstimateType.DataValueField = "Type"
            ddlEstimateType.DataBind()
            ddlEstimateType.Items.Insert(0, New ListItem("--Select--", 0))

            ddlShippingLine.DataSource = ds.Tables(3)
            ddlShippingLine.DataTextField = "SLName"
            ddlShippingLine.DataValueField = "SLID"
            ddlShippingLine.DataBind()
            ddlShippingLine.Items.Insert(0, New ListItem("--Select--", 0))
            strSql = ""
            strSql += "USP_MNR_CODE_DESCRIPTION"
            ds1 = db.sub_GetDataSets(strSql)
            ddlComponentCode.DataSource = ds1.Tables(0)
            ddlComponentCode.DataTextField = "COMPCODE"
            ddlComponentCode.DataTextField = "COMPCODE"
            ddlComponentCode.DataBind()
            ddlComponentCode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlLocationCode.DataSource = ds1.Tables(1)
            ddlLocationCode.DataTextField = "LOCATIONID"
            ddlLocationCode.DataTextField = "LOCATIONID"
            ddlLocationCode.DataBind()
            ddlLocationCode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlDamageCode.DataSource = ds1.Tables(2)
            ddlDamageCode.DataTextField = "DAMAGECODE"
            ddlDamageCode.DataTextField = "DAMAGECODE"
            ddlDamageCode.DataBind()
            ddlDamageCode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlRepairCode.DataSource = ds1.Tables(3)
            ddlRepairCode.DataTextField = "REPAIRCODE"
            ddlRepairCode.DataTextField = "REPAIRCODE"
            ddlRepairCode.DataBind()
            ddlRepairCode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlMaterialType.DataSource = ds1.Tables(4)
            ddlMaterialType.DataTextField = "Material_code"
            ddlMaterialType.DataTextField = "Material_code"
            ddlMaterialType.DataBind()
            ddlMaterialType.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "SELECT TOP 1 * FROM TEMP_CONTAINER_ESTIMATE_SEARCH where USER_ID=" & Session("UserId_DepoCFS") & " ORDER BY ADDED_ON DESC"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtContainerNo.Text = Trim(dt.Rows(0)("CONTAINER_NO") & "")
                UpdatePanel1.Update()
                btnShow_Click(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub FillGrid()
        Try
            Dim dblTotManHrs As Double = 0, dblTotManCost As Double = 0, dblTotMatCost As Double = 0, dblTotal As Double = 0
            dt4 = db.sub_GetDatatable("USP_FETCH_TEMP_TO_GRID_ESTIMATE " & Session("UserId_DepoCFS") & ",'" & Trim(txtContainerNo.Text & "") & "'")
            grdEstimateDetails.DataSource = dt4
            grdEstimateDetails.DataBind()
            For i = 0 To dt4.Rows.Count - 1
                dblTotManHrs += Val(dt4.Rows(i)("MAN_HOURS"))
                dblTotManCost += Val(dt4.Rows(i)("MAN_COST"))
                dblTotMatCost += Val(dt4.Rows(i)("MATERIAL_COST"))
                dblTotal += Val(dt4.Rows(i)("TOTAL"))
            Next
            txtTotManHrs.Text = dblTotManHrs
            txtTotManCost.Text = dblTotManCost
            txtTotMatCost.Text = dblTotMatCost
            txtTot.Text = dblTotal
            UpdatePanel4.Update()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim filePath As String = FileUpload.PostedFile.FileName
            Dim filename As String = Path.GetFileName(filePath)
            Dim ext As String = Path.GetExtension(filename)
            Dim contenttype As String = String.Empty
            'Set the contenttype based on File Extension
            Select Case ext
                Case ".doc"
                    contenttype = "application/vnd.ms-word"
                    Exit Select
                Case ".docx"
                    contenttype = "application/vnd.ms-word"
                    Exit Select
                Case ".xls"
                    contenttype = "application/vnd.ms-excel"
                    Exit Select
                Case ".xlsx"
                    contenttype = "application/vnd.ms-excel"
                    Exit Select
                Case ".jpg"
                    contenttype = "image/jpg"
                    Exit Select
                Case ".png"
                    contenttype = "image/png"
                    Exit Select
                Case ".gif"
                    contenttype = "image/gif"
                    Exit Select
                Case ".pdf"
                    contenttype = "application/pdf"
                    Exit Select
            End Select
            If contenttype <> String.Empty Then
                Dim fs As Stream = FileUpload.PostedFile.InputStream
                Dim br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(fs.Length)

                Dim strQuery As String = "USP_INSERT_INTO_TEMP_GENERATE_ESTIMATES @ContainerNo, @Description, @Location,@ComponentCode,@CodeLocation,@DamageCode,@RepairCode,@Length,@Width,"
                strQuery += " @DescSize,@MaterialType,@EstimateType,@ManHrs,@ManCost,@MaterialCost,@total,@Data,@UserID,@ContentType,@Name,@PARTCODE"
                Dim cmd As New SqlCommand(strQuery)
                cmd.Parameters.Add("@ContainerNo", SqlDbType.VarChar).Value = Trim(txtContainerNo.Text.ToUpper() & "")
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Trim(Replace(Trim(txtDescription.Text.ToUpper() & ""), "'", "''") & "")
                cmd.Parameters.Add("@Location", SqlDbType.VarChar).Value = Trim(Replace(Trim(txtLocation.Text & ""), "'", "''") & "")
                cmd.Parameters.Add("@ComponentCode", SqlDbType.VarChar).Value = Trim(Replace(Trim(ddlComponentCode.SelectedValue.ToUpper() & ""), "'", "''") & "")
                cmd.Parameters.Add("@CodeLocation", SqlDbType.VarChar).Value = Trim(Replace(Trim(ddlLocationCode.SelectedValue.ToUpper() & ""), "'", "''") & "")
                cmd.Parameters.Add("@DamageCode", SqlDbType.VarChar).Value = Trim(Replace(Trim(ddlDamageCode.SelectedValue.ToUpper() & ""), "'", "''") & "")
                cmd.Parameters.Add("@RepairCode", SqlDbType.VarChar).Value = Trim(Replace(Trim(ddlRepairCode.SelectedValue.ToUpper() & ""), "'", "''") & "")
                cmd.Parameters.Add("@Length", SqlDbType.VarChar).Value = Trim(txtLength.Text & "")
                cmd.Parameters.Add("@Width", SqlDbType.VarChar).Value = Trim(txtWidth.Text & "")
                cmd.Parameters.Add("@DescSize", SqlDbType.VarChar).Value = Trim(Replace(Trim(txtDescSize.Text & ""), "'", "''") & "")
                cmd.Parameters.Add("@MaterialType", SqlDbType.Int).Value = Trim(ddlMaterialType.SelectedValue.ToUpper() & "")
                cmd.Parameters.Add("@EstimateType", SqlDbType.VarChar).Value = Trim(ddlEstimateType.SelectedValue & "")
                cmd.Parameters.Add("@ManHrs", SqlDbType.VarChar).Value = Trim(txtManHours.Text & "")
                cmd.Parameters.Add("@ManCost", SqlDbType.VarChar).Value = Trim(txtManCost.Text & "")
                cmd.Parameters.Add("@MaterialCost", SqlDbType.VarChar).Value = Trim(txtMaterialCost.Text & "")
                cmd.Parameters.Add("@total", SqlDbType.VarChar).Value = Trim(txtTotal.Text & "")
                cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session("UserId_DepoCFS")
                cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = contenttype
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename
                cmd.Parameters.Add("@PARTCODE", SqlDbType.VarChar).Value = Trim(txtPartCode.Text & "")
                'strSql = ""
                'strSql += "USP_INSERT_INTO_TEMP_GENERATE_ESTIMATES '" & Trim(txtContainerNo.Text & "") & "','" & Replace(Trim(txtDescription.Text & ""), "'", "''") & "','" & Replace(Trim(txtLocation.Text & ""), "'", "''") & "',"
                'strSql += "'" & Replace(Trim(txtComponentCode.Text & ""), "'", "''") & "','" & Replace(Trim(txtCodeLocation.Text & ""), "'", "''") & "','" & Replace(Trim(txtDamageCode.Text & ""), "'", "''") & "',"
                'strSql += "'" & Replace(Trim(txtRepairCode.Text & ""), "'", "''") & "','" & Trim(txtLength.Text & "") & "','" & Trim(txtWidth.Text & "") & "','" & Replace(Trim(txtDescSize.Text & ""), "'", "''") & "',"
                'strSql += "" & Val(ddlMaterialType.SelectedValue & "") & ",'" & Trim(ddlEstimateType.SelectedValue & "") & "','" & Trim(txtManHours.Text & "") & "','" & Trim(txtManCost.Text & "") & "','" & Trim(txtMaterialCost.Text & "") & "',"
                'strSql += "'" & Trim(txtTotal.Text & "") & "','" & Replace(System.Text.Encoding.Unicode.GetString(bytes), "'", "''") & "'," & Session("UserId_DepoCFS") & ",'" & contenttype & "','" & filename & "'"
                'db.sub_ExecuteNonQuery(strSql)
                Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
                Dim con As New SqlConnection(strConnString)
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                Try
                    con.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    con.Close()
                    con.Dispose()
                End Try
            Else
                strSql = ""
                strSql += "USP_INSERT_INTO_TEMP_GENERATE_ESTIMATES '" & Trim(txtContainerNo.Text.ToUpper() & "") & "','" & Replace(Trim(txtDescription.Text.ToUpper() & ""), "'", "''") & "','" & Replace(Trim(txtLocation.Text & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(ddlComponentCode.SelectedValue.ToUpper() & ""), "'", "''") & "','" & Replace(Trim(ddlLocationCode.SelectedValue.ToUpper() & ""), "'", "''") & "','" & Replace(Trim(ddlDamageCode.SelectedValue.ToUpper() & ""), "'", "''") & "',"
                strSql += "'" & Replace(Trim(ddlRepairCode.SelectedValue.ToUpper() & ""), "'", "''") & "','" & Trim(txtLength.Text.ToUpper() & "") & "','" & Trim(txtWidth.Text & "") & "','" & Replace(Trim(txtDescSize.Text & ""), "'", "''") & "',"
                strSql += "'" & Trim(ddlMaterialType.SelectedValue.ToUpper() & "") & "','" & Trim(ddlEstimateType.SelectedValue & "") & "','" & Trim(txtManHours.Text & "") & "','" & Trim(txtManCost.Text & "") & "','" & Trim(txtMaterialCost.Text & "") & "',"
                strSql += "'" & Trim(txtTotal.Text & "") & "',NULL," & Session("UserId_DepoCFS") & ",'" & contenttype & "','" & filename & "','" & Trim(txtPartCode.Text & "") & "'"
                db.sub_ExecuteNonQuery(strSql)
            End If
            FillGrid()
            txtDescription.Text = ""
            txtLocation.Text = ""
            ddlComponentCode.SelectedValue = "0"
            ddlLocationCode.SelectedValue = "0"
            ddlDamageCode.SelectedValue = "0"
            ddlRepairCode.SelectedValue = "0"
            ddlMaterialType.SelectedValue = "0"
            txtLength.Text = ""
            txtWidth.Text = ""
            txtDescSize.Text = ""
            ddlEstimateType.SelectedValue = 0
            txtManHours.Text = ""
            txtManCost.Text = ""
            txtMaterialCost.Text = ""
            txtTotal.Text = ""
            lblComponentCode.Text = ""
            lblCodeLocation.Text = ""
            lblDamageCode.Text = ""
            lblRepairCode.Text = ""
            lblMaterialType.Text = ""
            txtPartCode.Text = ""
            txtDescription.Focus()
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Sub_SGTRate()
        Try
            Dim compid As String = ""
            strSql = ""
            strSql += "select Tinnumber from settings"
            dt9 = db.sub_GetDatatable(strSql)
            If dt9.Rows.Count > 0 Then
                compid = Trim(dt9.Rows(0)(0))
            End If
            strSql = ""
            strSql += "USP_GST_Cal"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                dblSGST = Val(dt1.Rows(0)("SGST"))
                dblCGST = Val(dt1.Rows(0)("CGST"))
                dblIGST = Val(dt1.Rows(0)("IGST"))
                dbltaxgroupid = Trim(dt1.Rows(0)("settingsID") & "")
                strSGSTPer = "SGST " & dblSGST & "%"
                StrCGSTPEr = "CGST " & dblCGST & "%"
                StrIGSTPer = "IGST " & dblIGST & "%"
                If lblstatecode.Text = compid Then
                    dblIGST = 0
                    StrIGSTPer = "IGST " & dblIGST & "%"
                Else
                    dblSGST = 0
                    dblCGST = 0
                    strSGSTPer = "SGST " & dblSGST & "%"
                    StrCGSTPEr = "CGST " & dblCGST & "%"
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsave_Click(sender As Object, e As EventArgs)
        Try
            Dim dblCount As Double = 0, intAmount As Double = 0
            For Each row As GridViewRow In grdEstimateDetails.Rows
                dblCount += 1
                intAmount += Val(CType(row.FindControl("lblTotal"), Label).Text) '+ Val(CType(row.FindControl("lblMaterialCost"), Label).Text)
            Next
            If dblCount = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please add description details first');", True)
                Exit Sub
            End If
            lblstatecode.Text = "24"
            Sub_SGTRate()
            strSql = ""
            strSql += "USP_INSERT_ESTIMATION_DETAILS '" & Convert.ToDateTime(Trim(txtEstimateDate.Text)).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtContainerNo.Text.ToUpper() & "") & "',"
            strSql += "'" & Val(ddlSurveyType.SelectedValue) & "','" & Replace(Trim(txtCSCASP.Text & ""), "'", "''") & "','" & Val(intAmount) & "','" & Val(intAmount * dblCGST / 100) & "',"
            strSql += "'" & Val(intAmount * dblSGST / 100) & "','" & Val(intAmount * dblIGST / 100) & "','" & Val(txtEntryID.Text) & "',"
            strSql += "'" & Replace(Trim(txtSurveyBy.Text & ""), "'", "''") & "','" & Val(txtManualEstimateNo.Text & "") & "',"
            If Not txtMFDate.Text = "" Then
                strSql += "'" & Convert.ToDateTime(Trim(txtMFDate.Text)).ToString("yyyy-MM-dd HH:mm") & "'," & Session("UserId_DepoCFS") & ",'" & Trim(txtEstimateNo.Text & "") & "'"
            Else
                strSql += "''," & Session("UserId_DepoCFS") & ",'" & Trim(txtEstimateNo.Text & "") & "'"
            End If
            db.sub_ExecuteNonQuery(strSql)
            Clear()
            txtEntryID.Text = ""
            ddlShippingLine.SelectedValue = 0
            txtSize.Text = ""
            txtCType.Text = ""
            txtContainerNo.Text = ""
            txtEstimateDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            txtEntryID.Text = ""
            ddlShippingLine.SelectedValue = 0
            txtSize.Text = ""
            txtCType.Text = ""
            Clear()
            strSql = ""
            strSql += "USP_CONTAINER_NO_SHOW_DETAILS_ESTIMATE '" & Trim(txtContainerNo.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Cannot proceed. Estimate has been generated against Container No " & Trim(txtContainerNo.Text & "") & " where Estimate Date=" & Trim(ds.Tables(1).Rows(0)("EstimateDate") & "") & " created by " & Trim(ds.Tables(0).Rows(0)("UserName") & "") & " ');", True)
                txtContainerNo.Focus()
            Else
                txtEntryID.Text = Trim(ds.Tables(1).Rows(0)("EntryID") & "")
                ddlShippingLine.SelectedValue = Trim(ds.Tables(1).Rows(0)("SLID") & "")
                txtSize.Text = Trim(ds.Tables(1).Rows(0)("Size") & "")
                txtCType.Text = Trim(ds.Tables(1).Rows(0)("Container Type") & "")
                If Not Trim(ds.Tables(1).Rows(0)("MFGDate") & "") = "" Then
                    txtMFDate.Text = Convert.ToDateTime(Trim(ds.Tables(1).Rows(0)("MFGDate") & "")).ToString("yyyy-MM")
                End If
                txtCSCASP.Text = Trim(ds.Tables(1).Rows(0)("CSCASP") & "")
                txtSurveyBy.Text = Trim(ds.Tables(1).Rows(0)("SurveyEIRID") & "")
                txtDMGRemarks.Text = Trim(ds.Tables(1).Rows(0)("damageRemarks") & "")

                ddlSurveyType.Focus()
            End If
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtManHours_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtDescSize.Text) <> 0 Then
                txtTotal.Text = Val(Val(txtManCost.Text) * Val(txtManHours.Text) + Val(txtMaterialCost.Text)) * Val(txtDescSize.Text)
            Else
                txtTotal.Text = Val(txtManCost.Text) * Val(txtManHours.Text) + Val(txtMaterialCost.Text)
            End If
            txtManCost.Focus()
            UpdatePanel8.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtManCost_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtDescSize.Text) <> 0 Then
                txtTotal.Text = Val(Val(txtManCost.Text) * Val(txtManHours.Text) + Val(txtMaterialCost.Text)) * Val(txtDescSize.Text)
            Else
                txtTotal.Text = Val(txtManCost.Text) * Val(txtManHours.Text) + Val(txtMaterialCost.Text)
            End If
            txtMaterialCost.Focus()
            UpdatePanel8.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtMaterialCost_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtDescSize.Text) <> 0 Then
                txtTotal.Text = Val(Val(txtManCost.Text) * Val(txtManHours.Text) + Val(txtMaterialCost.Text)) * Val(txtDescSize.Text)
            Else
                txtTotal.Text = Val(txtManCost.Text) * Val(txtManHours.Text) + Val(txtMaterialCost.Text)
            End If
            FileUpload.Focus()
            UpdatePanel8.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim Auto_Id As String = lnkRemove.CommandArgument
            strSql = ""
            strSql += "UPDATE TEMP_GENERATE_ESTIMATES set IS_CANCEL=1 where AUTO_ID=" & Auto_Id & ""
            db.sub_ExecuteNonQuery(strSql)
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Clear()
        Try
            btnsave.Text = "Save"
            btnShow.Visible = True
            lnksearch.Visible = True
            txtContainerNo.ReadOnly = False
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblfilename.Text = ""
            Lblfile.Text = ""
            db.sub_ExecuteNonQuery("delete from TEMP_GENERATE_ESTIMATES where USER_ID=" & Session("UserId_DepoCFS") & "")
            txtMFDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM")
            ddlSurveyType.SelectedValue = 0
            txtManualEstimateNo.Text = ""
            txtCSCASP.Text = ""
            txtSurveyBy.Text = ""
            txtDescription.Text = ""
            txtLocation.Text = ""
            ddlComponentCode.SelectedValue = "0"
            ddlLocationCode.SelectedValue = "0"
            ddlDamageCode.SelectedValue = "0"
            ddlRepairCode.SelectedValue = "0"
            ddlMaterialType.SelectedValue = "0"
            txtLength.Text = ""
            txtWidth.Text = ""
            txtDescSize.Text = ""
            ddlEstimateType.SelectedValue = 0
            txtManHours.Text = ""
            txtManCost.Text = ""
            txtMaterialCost.Text = ""
            txtTotal.Text = ""
            lblComponentCode.Text = ""
            lblCodeLocation.Text = ""
            lblDamageCode.Text = ""
            lblRepairCode.Text = ""
            lblMaterialType.Text = ""
            txtPartCode.Text = ""
            UpdatePanel6.Update()
            FillGrid()
            db.sub_ExecuteNonQuery("Delete from Temp_Estimate_Import Where userID=" & Session("UserId_DepoCFS") & "")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnEstimate_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_ESTIMATE_MODIFY_DETAILS " & Session("UserId_DepoCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                btnsave.Text = "Update"
                btnShow.Visible = False
                lnksearch.Visible = False

                txtContainerNo.ReadOnly = True
                txtContainerNo.Text = dt.Rows(0)("ContainerNo")
                txtManualEstimateNo.Text = Trim(dt.Rows(0)("running_est_no") & "")
                txtSize.Text = dt.Rows(0)("Size")
                ddlShippingLine.Text = dt.Rows(0)("SLName")
                txtEntryID.Text = dt.Rows(0)("EntryID")
                txtCType.Text = dt.Rows(0)("ContainerType")
                ddlSurveyType.SelectedValue = dt.Rows(0)("ID")
                txtCSCASP.Text = Trim(dt.Rows(0)("CSCASP") & "")
                txtSurveyBy.Text = Trim(dt.Rows(0)("surveyby") & "")
                txtEstimateDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("Est_Date") & "")).ToString("yyyy-MM-ddTHH:mm")
                If Not Trim(dt.Rows(0)("MFGDate") & "") = "" Then
                    txtMFDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("MFGDate") & "")).ToString("yyyy-MM")
                End If
                txtEstimateNo.Text = Trim(dt.Rows(0)("Estimate_ID") & "")
                FillGrid()
            Else
                Clear()
            End If
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnEstimateCodes_Click(sender As Object, e As EventArgs)
        Try
            lblComponentCode.Text = ""
            If Not Trim(ddlComponentCode.SelectedValue & "") = "0" Then
                strSql = ""
                strSql += "SELECT COMPDESC FROM COMPONANT_M WHERE COMPCODE='" & Trim(ddlComponentCode.SelectedValue & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    lblComponentCode.Text = Trim(dt.Rows(0)("COMPDESC"))
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Wrong Component Code');", True)
                    ddlComponentCode.SelectedValue = "0"
                    ddlComponentCode.Focus()
                    lblComponentCode.Text = ""
                    Exit Sub
                End If
            End If
            ddlComponentCode.Focus()
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtCodeLocation_TextChanged(sender As Object, e As EventArgs)
        Try
            lblCodeLocation.Text = ""
            If Not Trim(ddlLocationCode.SelectedValue & "") = "0" Then
                strSql = ""
                strSql += "SELECT LOCATIONDES FROM RLOCATION_M WHERE LOCATIONID='" & Trim(ddlLocationCode.SelectedValue & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    lblCodeLocation.Text = Trim(dt.Rows(0)("LOCATIONDES"))
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Wrong Location Code');", True)
                    ddlLocationCode.SelectedValue = "0"
                    ddlLocationCode.Focus()
                    lblCodeLocation.Text = ""
                    Exit Sub
                End If
                ddlLocationCode.Focus()
                UpdatePanel6.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtDamageCode_TextChanged(sender As Object, e As EventArgs)
        Try
            lblDamageCode.Text = ""
            If Not Trim(ddlDamageCode.SelectedValue & "") = "0" Then
                strSql = ""
                strSql += "SELECT DAMAGEDESC FROM DAMAGE_M WHERE DAMAGECODE='" & Trim(ddlDamageCode.SelectedValue & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    lblDamageCode.Text = Trim(dt.Rows(0)("DAMAGEDESC"))
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Wrong Damage Code');", True)
                    ddlDamageCode.SelectedValue = "0"
                    ddlDamageCode.Focus()
                    lblDamageCode.Text = ""
                    Exit Sub
                End If
            End If
            ddlDamageCode.Focus()
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtRepairCode_TextChanged(sender As Object, e As EventArgs)
        Try
            lblRepairCode.Text = ""
            If Not Trim(ddlRepairCode.SelectedValue & "") = "0" Then
                strSql = ""
                strSql += "SELECT REPAIRDESC FROM REPAIR_M WHERE REPAIRCODE='" & Trim(ddlRepairCode.SelectedValue & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    lblRepairCode.Text = Trim(dt.Rows(0)("REPAIRDESC"))
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Wrong Repair Code');", True)
                    ddlRepairCode.SelectedValue = "0"
                    ddlRepairCode.Focus()
                    lblRepairCode.Text = ""
                    Exit Sub
                End If
            End If
            ddlRepairCode.Focus()
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtMaterial_TextChanged(sender As Object, e As EventArgs)
        Try
            lblMaterialType.Text = ""
            If Not Trim(ddlMaterialType.SelectedValue & "") = "0" Then
                strSql = ""
                strSql += "SELECT Material_Type FROM MaterialType WHERE Material_code='" & Trim(ddlMaterialType.SelectedValue & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    lblMaterialType.Text = Trim(dt.Rows(0)("Material_Type"))
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Wrong Material Type Code');", True)
                    ddlMaterialType.SelectedValue = "0"
                    ddlMaterialType.Focus()
                    lblMaterialType.Text = ""
                End If
            End If
            ddlMaterialType.Focus()
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            ' GridView1.Rows(0).Visible = False
            ' GridView1.Columns(9).Visible = False
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                e.Row.Cells(9).Visible = False

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

                Import_To_Grid(FilePath, Extension)
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


    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String)
        Try

            Dim conStr As String = ""

            ' Select Case Extension
            'If (Extension = ".xls") Then
            ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid File selection please import .csv file');", True)
            ' Exit Sub
            ' conStr = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString()

            'End If

            If (Extension = ".xlsx" Or Extension = ".xls") Then
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid File selection please import .csv file');", True)

                ' Exit Sub

                ' csvfile()
                Dim storedProc As String = String.Empty
                storedProc = "spx_ImportFromExcel07"
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString").ConnectionString()
                ' XlsxFile(FilePath, Extension)
                'Exit Sub
                conStr = String.Format(conStr, FilePath, "Yes")
                ' conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & FilePath & ";Extended Properties='Excel 8.0;HDR=YES'"
                Dim connExcel As New OleDbConnection(conStr)

                Dim cmdExcel As New OleDbCommand()

                Dim oda As New OleDbDataAdapter()

                Dim dt As New DataTable()

                cmdExcel.Connection = connExcel

                'Get the name of First Sheet

                connExcel.Open()

                Dim dtExcelSchema As DataTable

                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

                Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()

                connExcel.Close()

                'Read Data from First Sheet

                connExcel.Open()

                cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"

                oda.SelectCommand = cmdExcel

                oda.Fill(dt)

                connExcel.Close()
                File.Delete(FilePath)
                XlsxFile(dt)
            End If
            If (Extension = ".csv") Then
                'csvfile()
                Exit Sub
            End If
        Catch ex As Exception
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub XlsxFile(dt As DataTable)
        Try
            Dim Edate As String = ""
            Dim ContainerNo As String = ""
            Dim Size As String = ""
            Dim CSCASP As String = ""
            Dim Survey As String = ""
            Dim Description As String = ""
            Dim EstimateType As String = ""
            Dim Component As String = ""
            Dim Repair As String = ""
            Dim Damage As String = ""
            Dim Location As String = ""
            Dim Material As String = ""
            Dim PartCode As String = ""
            Dim Width As String = ""
            Dim Length As String = ""
            Dim Qty As String = ""
            Dim ManHrs As String = ""
            Dim LabourCost As String = ""
            Dim MaterialCost As String = ""
            Dim Total As String = ""
            Dim strContainer As String = ""
            Dim strContainer1 As String = ""
            Dim strContainer2 As String = ""
            Dim strContainer3 As String = ""
            Dim strContainer4 As String = ""
            Dim strmessage As String = ""
            Dim formats() As String = {"dd-MM-yyyy", "yyyy-MM-dd", "dd/MM/yyyy", "yyyy/MM/dd", "dd-MM-yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "dd/MM/yyyy HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "dd-MM-yyyy HH:mm:ss tt", "yyyy-MM-dd HH:mm:ss tt", "dd/MM/yyyy HH:mm:ss tt", "yyyy/MM/dd HH:mm:ss tt", "dd-MM-yyyy hh:mm:ss tt", "yyyy-MM-dd hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt", "yyyy/MM/dd hh:mm:ss tt"}
            If (dt.Rows.Count > 0) Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Trim(dt.Rows(i).ItemArray(1).ToString()) <> "" Then
                        If Len(Trim(dt.Rows(i).ItemArray(1).ToString())) <> 11 Then
                            If strContainer = "" Then
                                strContainer = Trim(dt.Rows(i).ItemArray(1).ToString())
                            Else
                                strContainer += "," + Trim(dt.Rows(i).ItemArray(1).ToString())
                            End If
                        End If
                        Dim dtdate As DateTime
                        If DateTime.TryParseExact(Trim(dt.Rows(i).ItemArray(0).ToString()), formats, New CultureInfo("en-GB"), DateTimeStyles.None, dtdate) Then
                        Else
                            If strContainer1 = "" Then
                                strContainer1 = Trim(dt.Rows(i).ItemArray(1).ToString())
                            Else
                                If Not InStr(strContainer1, Trim(dt.Rows(i).ItemArray(1).ToString())) > 0 Then
                                    strContainer1 += "," + Trim(dt.Rows(i).ItemArray(1).ToString())
                                End If
                            End If
                        End If

                        If Not (Trim(dt.Rows(i).ItemArray(4).ToString().ToUpper) = "CARGO WORTHY" Or Trim(dt.Rows(i).ItemArray(4).ToString().ToUpper) = "IICL") Then
                            If strContainer2 = "" Then
                                strContainer2 = Trim(dt.Rows(i).ItemArray(1).ToString())
                            Else
                                If Not InStr(strContainer2, Trim(dt.Rows(i).ItemArray(1).ToString())) > 0 Then
                                    strContainer2 += "," + Trim(dt.Rows(i).ItemArray(1).ToString())
                                End If
                            End If
                        End If
                        If Not (Trim(dt.Rows(i).ItemArray(6).ToString().ToUpper) = "R" Or Trim(dt.Rows(i).ItemArray(6).ToString().ToUpper) = "W") Then
                            If strContainer3 = "" Then
                                strContainer3 = Trim(dt.Rows(i).ItemArray(1).ToString())
                            Else
                                If Not InStr(strContainer3, Trim(dt.Rows(i).ItemArray(1).ToString())) > 0 Then
                                    strContainer3 += "," + Trim(dt.Rows(i).ItemArray(1).ToString())
                                End If
                            End If
                        End If
                        strSql = ""
                        strSql += "select * from eyard_in where containerno='" & Trim(dt.Rows(i).ItemArray(1).ToString()) & "' and Iscancel=0"
                        dt1 = db.sub_GetDatatable(strSql)
                        If Not dt1.Rows.Count > 0 Then
                            If strContainer4 = "" Then
                                strContainer4 = Trim(dt.Rows(i).ItemArray(1).ToString())
                            Else
                                If Not InStr(strContainer4, Trim(dt.Rows(i).ItemArray(1).ToString())) > 0 Then
                                    strContainer4 += "," + Trim(dt.Rows(i).ItemArray(1).ToString())
                                End If
                            End If
                        End If
                    End If
                Next
                If Not (strContainer = "" And strContainer1 = "" And strContainer2 = "" And strContainer3 = "" And strContainer4 = "") Then
                    If Not strContainer = "" Then
                        strmessage += "Invalid container no length for " & strContainer & "\n"
                    End If
                    If Not strContainer1 = "" Then
                        strmessage += "Invalid date format for " & strContainer1 & "\n"
                    End If
                    If Not strContainer2 = "" Then
                        strmessage += "Invalid servey type for " & strContainer2 & "\n"
                    End If
                    If Not strContainer3 = "" Then
                        strmessage += "Invalid estimate type for " & strContainer3 & "\n"
                    End If
                    If Not strContainer4 = "" Then
                        strmessage += "No records found for " & strContainer4 & "\n"
                    End If
                    strmessage += "Please correct records and import again."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" & strmessage & "');", True)
                    Clear()
                    btnUpload.Text = "Import"
                    btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
                    Exit Sub
                End If
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Trim(dt.Rows(i).ItemArray(1).ToString()) <> "" Then
                        Edate = dt.Rows(i).ItemArray(0).ToString()
                        ContainerNo = dt.Rows(i).ItemArray(1).ToString()
                        Size = dt.Rows(i).ItemArray(2).ToString()
                        CSCASP = dt.Rows(i).ItemArray(3).ToString()
                        Survey = dt.Rows(i).ItemArray(4).ToString()
                        Description = dt.Rows(i).ItemArray(5).ToString()
                        EstimateType = dt.Rows(i).ItemArray(6).ToString()
                        Component = dt.Rows(i).ItemArray(7).ToString()
                        Repair = dt.Rows(i).ItemArray(8).ToString()
                        Damage = dt.Rows(i).ItemArray(9).ToString()
                        Location = dt.Rows(i).ItemArray(10).ToString()
                        Material = dt.Rows(i).ItemArray(11).ToString()
                        PartCode = dt.Rows(i).ItemArray(12).ToString()
                        Width = dt.Rows(i).ItemArray(13).ToString()
                        Length = dt.Rows(i).ItemArray(14).ToString()
                        Qty = dt.Rows(i).ItemArray(15).ToString()
                        ManHrs = dt.Rows(i).ItemArray(16).ToString()
                        LabourCost = dt.Rows(i).ItemArray(17).ToString()
                        MaterialCost = dt.Rows(i).ItemArray(18).ToString()
                        Total = dt.Rows(i).ItemArray(19).ToString()

                        strSql = ""
                        strSql += "USP_INSERT_TEMP_ESTIMATE_IMPORT '" & Convert.ToDateTime(Trim(Edate & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(ContainerNo) & "','" & Trim(CSCASP) & "','" & Trim(Survey) & "','" & Replace(Trim(Description), "'", "''") & "','" & Trim(EstimateType) & "',"
                        strSql += "'" & Trim(Component) & "','" & Trim(Repair) & "','" & Trim(Damage) & "','" & Trim(Location) & "','" & Trim(Material) & "','" & Trim(Width) & "','" & Trim(Length) & "',"
                        strSql += "'" & Trim(Qty) & "','" & Trim(ManHrs) & "','" & Trim(LabourCost) & "','" & Trim(MaterialCost) & "','" & Trim(Total) & "','" & Session("UserId_DepoCFS") & "','" & Trim(PartCode) & "'"
                        db.sub_ExecuteNonQuery(strSql)

                        Edate = ""
                        ContainerNo = ""
                        CSCASP = ""
                        Survey = ""
                        Description = ""
                        EstimateType = ""
                        Component = ""
                        Repair = ""
                        Damage = ""
                        Location = ""
                        Material = ""
                        PartCode = ""
                        Width = ""
                        Length = ""
                        Qty = ""
                        ManHrs = ""
                        LabourCost = ""
                        MaterialCost = ""
                        Total = ""
                    End If
                Next
            End If

            strSql = ""
            strSql += "USP_IMPORT_INTO_ESTIMATE_M " & Session("UserId_DepoCFS") & ""
            db.sub_ExecuteNonQuery(strSql)
            Clear()
            lblSession.Text = "Record successfully imported "
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()

        Catch ex As Exception
            btnUpload.Text = "Import"
            btnUpload.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtDescSize_TextChanged(sender As Object, e As EventArgs)
        Try
            If Val(txtDescSize.Text) <> 0 Then
                txtTotal.Text = Val(Val(txtManCost.Text) * Val(txtManHours.Text) + Val(txtMaterialCost.Text)) * Val(txtDescSize.Text)
            Else
                txtTotal.Text = Val(txtManCost.Text) * Val(txtManHours.Text) + Val(txtMaterialCost.Text)
            End If
            txtLength.Focus()
            UpdatePanel8.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs)
        Try
            Clear()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim Auto_Id As String = lnkRemove.CommandArgument

            strSql = ""
            strSql += "USP_EDIT_DESCRIPTION_WISE_ESTIMATE '" & Auto_Id & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtDescription.Text = Trim(dt.Rows(0)("DESCRIPTION") & "")
                ddlEstimateType.SelectedValue = Trim(dt.Rows(0)("ESTIMATE_TYPE") & "")
                ddlComponentCode.SelectedValue = Trim(dt.Rows(0)("COMPONENT_CODE") & "")
                ddlLocationCode.SelectedValue = Trim(dt.Rows(0)("CODE_LOCATION") & "")
                ddlDamageCode.SelectedValue = Trim(dt.Rows(0)("DAMAGE_CODE") & "")
                ddlRepairCode.SelectedValue = Trim(dt.Rows(0)("REPAIR_CODE") & "")
                ddlMaterialType.SelectedValue = Trim(dt.Rows(0)("Material_Type") & "")
                txtDescSize.Text = Trim(dt.Rows(0)("DESC_SIZE") & "")
                txtLength.Text = Trim(dt.Rows(0)("LENGTH") & "")
                txtWidth.Text = Trim(dt.Rows(0)("WIDTH") & "")
                txtPartCode.Text = Trim(dt.Rows(0)("PARTCODE") & "")
                txtManHours.Text = Trim(dt.Rows(0)("MAN_HOURS") & "")
                txtManCost.Text = Trim(dt.Rows(0)("MAN_COST") & "")
                txtMaterialCost.Text = Trim(dt.Rows(0)("MATERIAL_COST") & "")
                txtTotal.Text = Trim(dt.Rows(0)("TOTAL") & "")
            End If
            FillGrid()
            UpdatePanel6.Update()
            UpdatePanel8.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select '' [Estimate Date (yyyy-MM-dd)],'' [Container No],'' Size,'' [CSCASP],'' [Survey Type (Cargo Worthy / IICL)],'' [Description],'' [Estimate Type (W/R)],'' [Component Code],'' [Repair Code],'' [Damage Code],'' [Location Code],'' [Material Type],'' [Part Code],'' [Width],'' [Length],'' [Qty],'' [Man Hours],'' [Labour Cost],'' [Material Cost],'' [Total Amount]"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Estimation Import")
                    With wb.Worksheets(0)
                        .Column(1).Style.DateFormat.Format = "yyyy-MM-dd"
                    End With
                    'wb.Worksheets.Add(dt, "Noc Register1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=EstimateTemplate.xlsx")
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

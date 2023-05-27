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
Imports System.Web.Services

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt9 As DataTable
    Dim db As New dbOperation_Depo
    Dim dbO As New dbOperation
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
    Dim arrayfile As New ArrayList

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserName") Is Nothing Then
        '    Session("UserId_DepoCFS") = Request.Cookies("UserId_DepoCFS").Value
        '    Session("UserName") = Request.Cookies("UserName_DepoCFS").Value
        'End If
        If Not IsPostBack Then
            db.sub_ExecuteNonQuery("delete from TEMP_GENERATE_ESTIMATES_OVMNR where USERID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Modify_Estimate Where User_ID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Estimate_Import Where userID=" & Session("UserId_DepoCFS") & "")

            txtEstimateDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtMFDate.Text = ""
            ddlMoveType.SelectedValue = 0
            'FillGrid()
            Filldropdown()
            txtContainerNo.Focus()
        End If
    End Sub


    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql = "USP_FILL_DROPDOWN_ESTIMATE_ENTRY_OVMNR"

            ds = db.sub_GetDataSets(strSql)

            ddlSurveyType.DataSource = ds.Tables(0)
            ddlSurveyType.DataTextField = "Type"
            ddlSurveyType.DataValueField = "ID"
            ddlSurveyType.DataBind()
            'ddlSurveyType.Items.Insert(0, New ListItem("--Select--", 0))

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

            'ddlLocationCode.DataSource = ds1.Tables(1)
            'ddlLocationCode.DataTextField = "LOCATIONID"
            'ddlLocationCode.DataTextField = "LOCATIONID"
            'ddlLocationCode.DataBind()
            'ddlLocationCode.Items.Insert(0, New ListItem("--Select--", 0))

            'ddlDamageCode.DataSource = ds1.Tables(2)
            'ddlDamageCode.DataTextField = "DAMAGECODE"
            'ddlDamageCode.DataTextField = "DAMAGECODE"
            'ddlDamageCode.DataBind()
            'ddlDamageCode.Items.Insert(0, New ListItem("--Select--", 0))

            'ddlMaterialType.DataSource = ds1.Tables(4)
            'ddlMaterialType.DataTextField = "Material_code"
            'ddlMaterialType.DataTextField = "Material_code"
            'ddlMaterialType.DataBind()
            'ddlMaterialType.Items.Insert(0, New ListItem("--Select--", 0))

            ddlArea.DataSource = ds.Tables(4)
            ddlArea.DataTextField = "Area"
            ddlArea.DataValueField = "ID"
            ddlArea.DataBind()
            ddlArea.Items.Insert(0, New ListItem("--Select--", 0))

            ddlLocation.DataSource = ds.Tables(5)
            ddlLocation.DataTextField = "Location"
            ddlLocation.DataValueField = "ID"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem("--Select--", 0))

            ddlTypeOfRepair.DataSource = ds.Tables(6)
            ddlTypeOfRepair.DataTextField = "TypeOfRepair"
            ddlTypeOfRepair.DataValueField = "ID"
            ddlTypeOfRepair.DataBind()
            ddlTypeOfRepair.Items.Insert(0, New ListItem("--Select--", 0))

            ddlDescriptionOfRepair.DataSource = ds.Tables(7)
            ddlDescriptionOfRepair.DataTextField = "DescriptionOfRepair"
            ddlDescriptionOfRepair.DataValueField = "ID"
            ddlDescriptionOfRepair.DataBind()
            ddlDescriptionOfRepair.Items.Insert(0, New ListItem("--Select--", 0))

            ddlRepairCode.DataSource = ds.Tables(8)
            ddlRepairCode.DataTextField = "RepairCode"
            ddlRepairCode.DataValueField = "ID"
            ddlRepairCode.DataBind()
            ddlRepairCode.Items.Insert(0, New ListItem("--Select--", 0))



            ddlActivity.DataSource = ds.Tables(9)
            ddlActivity.DataTextField = "Activity"
            ddlActivity.DataValueField = "Activity"
            ddlActivity.DataBind()
            ddlActivity.Items.Insert(0, New ListItem("--Select--", 0))

            ddlparty.DataSource = ds.Tables(10)
            ddlparty.DataTextField = "Party"
            ddlparty.DataValueField = "Party"
            ddlparty.DataBind()
            ddlparty.Items.Insert(0, New ListItem("--Select--", 0))

            ddlVendorcode.DataSource = ds.Tables(11)
            ddlVendorcode.DataTextField = "VendorCode"
            ddlVendorcode.DataValueField = "VendorCode"
            ddlVendorcode.DataBind()
            'ddlVendorcode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlDepotCode.DataSource = ds.Tables(12)
            ddlDepotCode.DataTextField = "DepotCode"
            ddlDepotCode.DataValueField = "DepotCode"
            ddlDepotCode.DataBind()
            'ddlDepotCode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlUnit.DataSource = ds.Tables(13)
            ddlUnit.DataTextField = "Unit"
            ddlUnit.DataValueField = "Unit"
            ddlUnit.DataBind()
            ddlUnit.Items.Insert(0, New ListItem("--Select--", 0))

            ddlDimension.DataSource = ds.Tables(14)
            ddlDimension.DataTextField = "Dimensions"
            ddlDimension.DataValueField = "ID"
            ddlDimension.DataBind()
            ddlDimension.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub FillGrid()
        Try
            Dim dblTotManHrs As Double = 0, dblTotManCost As Double = 0, dblTotMatCost As Double = 0, dblTotal As Double = 0
            dt4 = db.sub_GetDatatable("USP_FETCH_TEMP_TO_GRID_ESTIMATE_OVMNR " & Session("UserId_DepoCFS") & ",'" & Trim(txtContainerNo.Text & "") & "'")
            grdEstimateDetails.DataSource = dt4
            grdEstimateDetails.DataBind()
            For i = 0 To dt4.Rows.Count - 1
                dblTotManHrs += Val(dt4.Rows(i)("ManHrs"))
                dblTotManCost += Val(dt4.Rows(i)("ManCost"))
                dblTotMatCost += Val(dt4.Rows(i)("MaterialCost"))
                dblTotal += Val(dt4.Rows(i)("Total"))
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

    Protected Sub textQty_Selected_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtRepairCode.Text <> "" Then
                If textQty.Text <> "" Then
                    textTotal.Text = Val(lblTotalAmt.Text) * Val(textQty.Text)

                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please add Repair code  first');", True)
                Exit Sub

            End If
        Catch ex As Exception

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
    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim dtBind As New DataTable
            If txtManHours.Text = "" Then
                txtManHours.Text = 0
            End If
            If txtManCost.Text = "" Then
                txtManCost.Text = 0
            End If
            If txtMaterialCost.Text = "" Then
                txtMaterialCost.Text = 0
            End If
            If txtTotal.Text = "" Then
                txtTotal.Text = 0
            End If

            dtBind = db.InsertEstimationEntry(Trim(txtComponent.Text & ""), Trim(txtDamageLocation.Text & ""), Trim(txtDamageType.Text & ""), Trim(txtMaterialtype.Text & ""), Trim(txtRepairType.Text & ""),
                                               Trim(txtRepairDescription.Text & ""), Trim(ddlActivity.SelectedValue & ""), Trim(ddlparty.SelectedValue & ""), Trim(txtRepairCode.Text), Trim(ddlDimension.SelectedItem.Text), Val(txtLength.Text), Val(txtWidth.Text), Trim(ddlUnit.SelectedItem.Text & ""), Val(textQty.Text),
            Val(txtManHrs.Text & ""), Val(textManCost.Text & ""), Val(textMaterialCost.Text & ""), Val(textTotal.Text & ""),
                                                Session("UserId_DepoCFS"), Trim(txtContainerNo.Text & ""), Trim(ddlEstimateType.Text))
            FillGrid()


            txtComponent.Text = ""
            txtDamageLocation.Text = ""
            txtDamageType.Text = ""
            txtMaterialtype.Text = ""
            txtRepairType.Text = ""
            txtRepairDescription.Text = ""
            ddlActivity.SelectedValue = 0
            ddlparty.SelectedValue = 0
            txtRepairCode.Text = ""
            txtDimension.Text = ""
            ddlDimension.SelectedValue = 0
            txtLength.Text = ""
            txtWidth.Text = ""
            txtWidth.Text = ""
            ddlUnit.SelectedValue = 0
            textQty.Text = ""

            txtDescription.Text = ""
            ddlLocation.SelectedValue = 0
            ddlEstimateType.SelectedValue = 0
            ddlTypeOfRepair.SelectedValue = 0
            txtManHours.Text = ""
            txtManCost.Text = ""
            ddlArea.SelectedValue = 0
            ddlRepairCode.SelectedValue = 0
            txtQty.Text = ""
            ddlDescriptionOfRepair.SelectedValue = 0
            txtMaterialCost.Text = ""
            txtTotal.Text = ""
            txtDescription.Text = ""

            txtManHrs.Text = ""
            textManCost.Text = ""
            textMaterialCost.Text = ""
            textTotal.Text = ""

            txtDescription.Focus()
            'UpdatePanel6.Update()
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
            strSql += "USP_GST_Cal '1'"
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


            strSql = ""
            strSql += "USP_CONTAINER_NO_SHOW_DETAILS_ESTIMATE_OVMNR '" & Trim(txtContainerNo.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                Clear()
                Exit Sub
            End If
            Dim dblCount As Double = 0, intAmount As Double = 0
            For Each row As GridViewRow In grdEstimateDetails.Rows
                dblCount += 1
                intAmount += Val(CType(row.FindControl("lblTotal"), Label).Text) '+ Val(CType(row.FindControl("lblMaterialCost"), Label).Text)
            Next
            If dblCount = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please add description details first');", True)
                Exit Sub
            End If
            'lblstatecode.Text = "27"

            strSql = ""
            strSql += "select Tinnumber from settings"
            dt9 = db.sub_GetDatatable(strSql)
            If dt9.Rows.Count > 0 Then
                lblstatecode.Text = Trim(dt9.Rows(0)(0))
            End If
            Sub_SGTRate()
            strSql = ""
            strSql += "USP_INSERT_ESTIMATION_DETAILS_OVMNR '" & Convert.ToDateTime(Trim(txtEstimateDate.Text)).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtContainerNo.Text.ToUpper() & "") & "',"
            strSql += "'" & Val(ddlSurveyType.SelectedValue) & "','" & Replace(Trim(txtCSCASP.Text & ""), "'", "''") & "','" & Val(intAmount) & "','" & Val(intAmount * dblCGST / 100) & "',"
            strSql += "'" & Val(intAmount * dblSGST / 100) & "','" & Val(intAmount * dblIGST / 100) & "','" & Trim(ddlVendorcode.SelectedValue) & "','" & Trim(ddlDepotCode.SelectedValue) & "','" & Val(txtEntryID.Text) & "',"
            strSql += "'" & Replace(Trim(txtSurveyBy.Text & ""), "'", "''") & "','" & Val(txtManualEstimateNo.Text & "") & "',"
            If Not txtMFDate.Text = "" Then
                strSql += "'" & Trim(txtMFDate.Text) & "'," & Session("UserId_DepoCFS") & ",'" & Trim(txtEstimateNo.Text & "") & "'"
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
            strSql += "USP_CONTAINER_NO_SHOW_DETAILS_ESTIMATE_OVMNR '" & Trim(txtContainerNo.Text & "") & "'"
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
                    'txtMFDate.Text = Convert.ToDateTime(Trim(ds.Tables(1).Rows(0)("MFGDate") & "")).ToString("yyyy")
                    txtMFDate.Text = Convert.ToDateTime(ds.Tables(1).Rows(0)("MFGDate")).ToString("MMM-yyyy")
                End If
                txtCSCASP.Text = Trim(ds.Tables(1).Rows(0)("CSCASP") & "")
                txtSurveyBy.Text = Trim(ds.Tables(1).Rows(0)("SurveyEIRID") & "")
                txtDMGRemarks.Text = Trim(ds.Tables(1).Rows(0)("damageRemarks") & "")
                If Trim(ds.Tables(1).Rows(0)("MoveType") & "") = "MTIN" Then
                    ddlMoveType.SelectedValue = 1
                    ddlparty.SelectedValue = "User"
                ElseIf Trim(ds.Tables(1).Rows(0)("MoveType") & "") = "MIR" Then
                    ddlMoveType.SelectedValue = 2
                    ddlparty.SelectedValue = "Owner"

                ElseIf Trim(ds.Tables(1).Rows(0)("MoveType") & "") = "DSTUFF" Then
                    ddlMoveType.SelectedValue = 3
                ElseIf Trim(ds.Tables(1).Rows(0)("MoveType") & "") = "Enblock" Then
                    ddlMoveType.SelectedValue = 4
                End If
                'ddlSurveyType.Focus()
            End If
            UpdatePanel2.Update()
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
            strSql += "UPDATE TEMP_GENERATE_ESTIMATES_OVMNR set ISCANCEL=1 where AUTO_ID=" & Auto_Id & ""
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
            lblfilename.Text = ""
            Lblfile.Text = ""
            txtQty.Text = ""
            ddlTypeOfRepair.SelectedValue = 0
            db.sub_ExecuteNonQuery("delete from TEMP_GENERATE_ESTIMATES_OVMNR where USER_ID=" & Session("UserId_DepoCFS") & "")
            txtMFDate.Text = ""
            ddlSurveyType.SelectedValue = 1
            txtManualEstimateNo.Text = ""
            txtCSCASP.Text = ""
            txtSurveyBy.Text = ""
            txtDescription.Text = ""
            ddlLocation.SelectedValue = 0
            ddlEstimateType.SelectedValue = 0
            ddlDescriptionOfRepair.SelectedValue = 0
            txtManHours.Text = ""
            txtManCost.Text = ""
            txtMaterialCost.Text = ""
            txtTotal.Text = ""
            'UpdatePanel6.Update()
            FillGrid()
            db.sub_ExecuteNonQuery("Delete from Temp_Estimate_Import Where userID=" & Session("UserId_DepoCFS") & "")
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlArea_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        strSql = ""
        strSql = "USP_FillTariffLocation " & ddlArea.SelectedValue & ""
        dt = db.sub_GetDatatable(strSql)
        ddlLocation.DataSource = dt
        ddlLocation.DataTextField = "Location"
        ddlLocation.DataValueField = "ID"
        ddlLocation.DataBind()
        ddlLocation.Items.Insert(0, New ListItem("--Select--", 0))
    End Sub
    Protected Sub ddlLocation_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        strSql = ""
        strSql = "USP_FillTypeOfRepair " & ddlLocation.SelectedValue & ""
        dt = db.sub_GetDatatable(strSql)
        ddlTypeOfRepair.DataSource = dt
        ddlTypeOfRepair.DataTextField = "TypeOfRepair"
        ddlTypeOfRepair.DataValueField = "ID"
        ddlTypeOfRepair.DataBind()
        ddlTypeOfRepair.Items.Insert(0, New ListItem("--Select--", 0))
    End Sub
    Protected Sub ddlTypeOfRepair_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        strSql = ""
        strSql = "USP_FillDescriptionOfRepair " & ddlTypeOfRepair.SelectedValue & ""
        dt = db.sub_GetDatatable(strSql)
        ddlDescriptionOfRepair.DataSource = dt
        ddlDescriptionOfRepair.DataTextField = "DescriptionOfRepair"
        ddlDescriptionOfRepair.DataValueField = "ID"
        ddlDescriptionOfRepair.DataBind()
        ddlDescriptionOfRepair.Items.Insert(0, New ListItem("--Select--", 0))
    End Sub
    Protected Sub ddlDescriptionOfRepair_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        strSql = ""
        strSql = "USP_FillRepairCode " & ddlDescriptionOfRepair.SelectedValue & ""
        dt = db.sub_GetDatatable(strSql)
        ddlRepairCode.DataSource = dt
        ddlRepairCode.DataTextField = "RepairCode"
        ddlRepairCode.DataValueField = "ID"
        ddlRepairCode.DataBind()
        ddlRepairCode.Items.Insert(0, New ListItem("--Select--", 0))
    End Sub
    Protected Sub ddlRepairCode_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        If txtManHours.Text = "" Then
            txtManHours.Text = 0
        End If
        If txtManCost.Text = "" Then
            txtManCost.Text = 0
        End If
        If txtMaterialCost.Text = "" Then
            txtMaterialCost.Text = 0
        End If
        If txtTotal.Text = "" Then
            txtTotal.Text = 0
        End If
        strSql = ""
        strSql = "USP_GetEstimateTariffData '" & ddlRepairCode.SelectedItem.Text & "','" & ddlDescriptionOfRepair.SelectedItem.Text & "'"
        dt = db.sub_GetDatatable(strSql)
        If dt.Rows.Count > 0 Then
            txtManHours.Text = dt.Rows(0)("ManHrs")
            txtManCost.Text = dt.Rows(0)("ManCost")
            txtMaterialCost.Text = dt.Rows(0)("MaterialCost")
            txtTotal.Text = dt.Rows(0)("TotalCost")
        End If
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

    Protected Sub btnClear_Click(sender As Object, e As EventArgs)
        Try
            Clear()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    'Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
    '    Try
    '        Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
    '        Dim row As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
    '        Dim Auto_Id As String = lnkRemove.CommandArgument

    '        strSql = ""
    '        strSql += "USP_EDIT_DESCRIPTION_WISE_ESTIMATE '" & Auto_Id & "'"
    '        dt = db.sub_GetDatatable(strSql)
    '        If dt.Rows.Count > 0 Then
    '            txtDescription.Text = Trim(dt.Rows(0)("DESCRIPTION") & "")
    '            ddlEstimateType.SelectedValue = Trim(dt.Rows(0)("ESTIMATE_TYPE") & "")
    '            txtManHours.Text = Trim(dt.Rows(0)("MAN_HOURS") & "")
    '            txtManCost.Text = Trim(dt.Rows(0)("MAN_COST") & "")
    '            txtMaterialCost.Text = Trim(dt.Rows(0)("MATERIAL_COST") & "")
    '            txtTotal.Text = Trim(dt.Rows(0)("TOTAL") & "")
    '        End If
    '        FillGrid()
    '        UpdatePanel6.Update()
    '        UpdatePanel8.Update()
    '    Catch ex As Exception
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
    '    End Try
    'End Sub

    Protected Sub txtRepairCode_TextChanged(sender As Object, e As EventArgs)
        Try
            'strSql = ""
            'strSql += "Usp_getRepairCodeDetails '" & Trim(txtRepairCode.Text & "") & "'"
            'dt2 = db.sub_GetDatatable(strSql)
            'If dt2.Rows.Count > 0 Then
            '    txtDimension.Text = Trim(dt2.Rows(0)("KeyValue"))

            '    txtManHrs.Text = Val(dt2.Rows(0)("ManHrs"))
            '    textManCost.Text = Val(dt2.Rows(0)("ManCost"))
            '    textMaterialCost.Text = Val(dt2.Rows(0)("MaterialCost"))
            '    textTotal.Text = Val(dt2.Rows(0)("TotalCost"))

            '    lblTotalAmt.Text = Val(dt2.Rows(0)("TotalCost"))
            'Else
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Repair Type Is Not Valid ');", True)
            '    Exit Sub
            'End If

            strSql = ""
            strSql += "USP_GetRepairCodeDetailsForEstimate '" & Trim(txtRepairCode.Text & "") & "'"
            dt2 = db.sub_GetDatatable(strSql)
            If dt2.Rows.Count > 0 Then
                txtDamageLocation.Text = dt2.Rows(0)("Damage_Location")
                txtComponent.Text = dt2.Rows(0)("Component")
                txtDamageType.Text = dt2.Rows(0)("Damage_Type")
                txtRepairType.Text = dt2.Rows(0)("Repair_Type")
                txtMaterialtype.Text = dt2.Rows(0)("Material_Type")
                ddlActivity.SelectedValue = dt2.Rows(0)("Activity")

                If dt2.Rows(0)("Dimension") = "LN*W" Then
                    ddlDimension.SelectedValue = 1

                ElseIf dt2.Rows(0)("Dimension") = "LN" Then
                    ddlDimension.SelectedValue = 2
                ElseIf dt2.Rows(0)("Dimension") = "Q" Then
                    ddlDimension.SelectedValue = 3

                End If

                txtLength.Text = dt2.Rows(0)("Length")
                txtWidth.Text = dt2.Rows(0)("Width")
                ddlUnit.SelectedValue = dt2.Rows(0)("Unit")
                txtManHrs.Text = dt2.Rows(0)("Labour_Hours")
                textManCost.Text = dt2.Rows(0)("LabourCost")
                textMaterialCost.Text = dt2.Rows(0)("Material_Cost")

                textTotal.Text = (Val(textManCost.Text) + Val(textMaterialCost.Text))
                lblTotalAmt.Text = (Val(textManCost.Text) + Val(textMaterialCost.Text))

                strSql = ""
                strSql += "USP_CONTAINER_NO_SHOW_DETAILS_ESTIMATE_OVMNR '" & Trim(txtContainerNo.Text & "") & "'"
                ds = db.sub_GetDataSets(strSql)
                If Trim(ds.Tables(1).Rows(0)("MoveType") & "") = "MTIN" Then
                    ddlparty.SelectedValue = "User"
                ElseIf Trim(ds.Tables(1).Rows(0)("MoveType") & "") = "MIR" Then
                    ddlparty.SelectedValue = "Owner"

                ElseIf Trim(ds.Tables(1).Rows(0)("MoveType") & "") = "DSTUFF" Then
                ElseIf Trim(ds.Tables(1).Rows(0)("MoveType") & "") = "Enblock" Then
                End If

                'txtDimension.Text = Trim(dt2.Rows(0)("KeyValue"))

                'txtManHrs.Text = Val(dt2.Rows(0)("ManHrs"))
                'textManCost.Text = Val(dt2.Rows(0)("ManCost"))
                'textMaterialCost.Text = Val(dt2.Rows(0)("MaterialCost"))
                'textTotal.Text = Val(dt2.Rows(0)("TotalCost"))

                'lblTotalAmt.Text = Val(dt2.Rows(0)("TotalCost"))
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Repair Code Is Not Valid ');", True)
                Exit Sub
            End If


        Catch ex As Exception

        End Try
    End Sub

    'Public Function GetComponentDetails(term As String)
    '    arrayfile.Clear()
    '    Dim col As New AutoCompleteType
    '    'strSql = ""
    '    'strSql = "Usp_getComponentDetails '" & Trim(TextBox1.Text & "") & "'"
    '    'dt2 = db.sub_GetDatatable(strSql)
    '    If dt2.Rows.Count > 0 Then
    '        For i = 0 To dt2.Rows.Count - 1
    '            arrayfile.Add(dt2.Rows(i)("Code").ToString())

    '        Next
    '        'TextBox1.Aut()
    '    End If

    '    Return arrayfile
    'End Function

    <WebMethod()>
    Public Shared Function getComponents() As List(Of Object)
        'Dim constr As String = ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
        Dim Dt10 As New DataTable
        Dim strSql1 As String = ""
        'strSql1 = "Usp_getComponentDetails '" & Trim(TextBox1.Text & "") & "'"
        'Dt10 = db.sub_GetDatatable(Dt10)

        Dim Components As List(Of Object) = New List(Of Object)()
        If Dt10.Rows.Count > 0 Then
            For i = 0 To Dt10.Rows.Count - 1
                Components.Add(New With {.Components = Dt10.Rows(i)("Code").ToString()})

            Next

        End If
        Return Components
    End Function
    <WebMethod()>
    Public Shared Function SearchCustomers(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Using conn As SqlConnection = New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
            Using cmd As SqlCommand = New SqlCommand()
                'Using cmd As StoredProcedure = New StoredProcedure()
                cmd.CommandText = "Usp_getComponentDetails @ComponentCode "
                cmd.Parameters.AddWithValue("@ComponentCode", prefixText)
                cmd.Connection = conn
                conn.Open()
                Dim customers As List(Of String) = New List(Of String)()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(sdr("Name").ToString())
                    End While
                End Using
                conn.Close()

                Return customers
            End Using
        End Using
    End Function
    <WebMethod()>
    Public Shared Function SearchDmgLocation(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Using conn As SqlConnection = New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
            Using cmd As SqlCommand = New SqlCommand()
                'Using cmd As StoredProcedure = New StoredProcedure()
                cmd.CommandText = "Usp_getDMGLocation @DMGLocation "
                cmd.Parameters.AddWithValue("@DMGLocation", prefixText)
                cmd.Connection = conn
                conn.Open()
                Dim customers As List(Of String) = New List(Of String)()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(sdr("Description").ToString())
                    End While
                End Using
                conn.Close()

                Return customers
            End Using
        End Using
    End Function

    <WebMethod()>
    Public Shared Function SearchDamageType(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Using conn As SqlConnection = New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
            Using cmd As SqlCommand = New SqlCommand()
                'Using cmd As StoredProcedure = New StoredProcedure()
                cmd.CommandText = "Usp_getSearchDamageType @Code "
                cmd.Parameters.AddWithValue("@Code", prefixText)
                cmd.Connection = conn
                conn.Open()
                Dim customers As List(Of String) = New List(Of String)()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(sdr("Name").ToString())
                    End While
                End Using
                conn.Close()

                Return customers
            End Using
        End Using
    End Function
    Protected Sub SearchDmgLocation1()

        Dim D As Integer = 0
    End Sub
    <WebMethod()>
    Public Shared Function SearchMaterialtype(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Using conn As SqlConnection = New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
            Using cmd As SqlCommand = New SqlCommand()
                'Using cmd As StoredProcedure = New StoredProcedure()
                cmd.CommandText = "Usp_getSearchMaterial_Type @Code "
                cmd.Parameters.AddWithValue("@Code", prefixText)
                cmd.Connection = conn
                conn.Open()
                Dim customers As List(Of String) = New List(Of String)()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(sdr("Name").ToString())
                    End While
                End Using
                conn.Close()

                Return customers
            End Using
        End Using
    End Function

    <WebMethod()>
    Public Shared Function SearchRepairDescription(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Using conn As SqlConnection = New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
            Using cmd As SqlCommand = New SqlCommand()
                'Using cmd As StoredProcedure = New StoredProcedure()
                cmd.CommandText = "Usp_getSearchRepairDescription @Code "
                cmd.Parameters.AddWithValue("@Code", prefixText)
                cmd.Connection = conn
                conn.Open()
                Dim customers As List(Of String) = New List(Of String)()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(sdr("Name").ToString())
                    End While
                End Using
                conn.Close()

                Return customers
            End Using
        End Using
    End Function

    <WebMethod()>
    Public Shared Function SearchRepairCode(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Using conn As SqlConnection = New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
            Using cmd As SqlCommand = New SqlCommand()
                'Using cmd As StoredProcedure = New StoredProcedure()
                cmd.CommandText = "Usp_getSearchEstimate_Tariff @Code "
                cmd.Parameters.AddWithValue("@Code", prefixText)
                cmd.Connection = conn
                conn.Open()
                Dim customers As List(Of String) = New List(Of String)()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        customers.Add(sdr("Repair_Code").ToString())
                    End While
                End Using
                conn.Close()

                Return customers
            End Using
        End Using
    End Function

    Protected Sub txtComponent_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_Validate_ComponentCode '" & Trim(txtComponent.Text) & "'"
            dt3 = db.sub_GetDatatable(strSql)
            If dt3.Rows.Count > 0 Then
                txtComponent.Text = Trim(dt3.Rows(0)("Code"))
                txtDamageType.Focus()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Component Code Is Not Valid ');", True)
                txtComponent.Text = ""
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub
    '<System.Web.Services.WebMethod>
    '<WebMethod()>
    Protected Sub txtDamageLocation_TextChanged()
        Try
            strSql = ""
            strSql += "USP_Validate_DAMAGE_LOCATION '" & Trim(txtDamageLocation.Text) & "'"
            dt3 = db.sub_GetDatatable(strSql)
            If dt3.Rows.Count > 0 Then
                txtDamageLocation.Text = Trim(dt3.Rows(0)("Code"))
                txtComponent.Focus()

            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Damage Location Code Is Not Valid ');", True)
                txtDamageLocation.Text = ""

                Exit Sub
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub txtDamageType_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_Validate_Damage_Type '" & Trim(txtDamageType.Text) & "'"
            dt3 = db.sub_GetDatatable(strSql)
            If dt3.Rows.Count > 0 Then
                txtDamageType.Text = Trim(dt3.Rows(0)("Code"))
                txtRepairType.Focus()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Damage Type Is Not Valid ');", True)
                txtDamageType.Text = ""
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub txtMaterialtype_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_Validate_Material_Type '" & Trim(txtMaterialtype.Text) & "'"
            dt3 = db.sub_GetDatatable(strSql)
            If dt3.Rows.Count > 0 Then
                txtMaterialtype.Text = Trim(dt3.Rows(0)("Code"))
                txtRepairDescription.Focus()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Material Type Is Not Valid ');", True)
                txtMaterialtype.Text = ""
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub



    Protected Sub txtRepairType_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += " USP_Validate_Repair_Type '" & Trim(txtRepairType.Text) & "'"
            dt3 = db.sub_GetDatatable(strSql)
            If dt3.Rows.Count > 0 Then
                txtRepairType.Text = Trim(dt3.Rows(0)("Code"))
                txtMaterialtype.Focus()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Repair Type Is Not Valid ');", True)
                txtRepairType.Text = ""
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    '<System.Web.Services.WebMethod>
    <WebMethod()>
    Public Function txtDamageLocation_TextChanged1(ByVal name As String) As String
        Try
            strSql = ""
            strSql += "USP_Validate_DAMAGE_LOCATION '" & Trim(txtDamageLocation.Text) & "'"
            dt3 = db.sub_GetDatatable(strSql)
            If dt3.Rows.Count > 0 Then
                txtDamageLocation.Text = Trim(dt3.Rows(0)("Code"))
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Damage Location Code Is Not Valid ');", True)
                txtDamageLocation.Text = ""


                Exit Function
            End If
        Catch ex As Exception

        End Try


    End Function
End Class

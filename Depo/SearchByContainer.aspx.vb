Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserIDPRE_Bond") Is Nothing Then
        '    Session("UserIDPRE_Bond") = Request.Cookies("UserIDPRE_Bond").Value
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
            db.sub_ExecuteNonQuery("delete from TEMP_SEARCH_CONTAINER where USER_ID=" & Session("UserId_DepoCFS") & "")
            'FillGrid()
            txtContainerNo.Focus()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSearchbyContainer_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from TEMP_SEARCH_CONTAINER where USER_ID=" & Session("UserId_DepoCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtContainerNo.Text = Trim(dt.Rows(0)("CONTAINER_NO") & "")
                UpdatePanel1.Update()
            End If
            txtContainerNo_TextChanged(sender, e)
            ddlEntryID.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Get_Sp_EyardCtrSeacrh '" & txtContainerNo.Text & "','" & ddlEntryID.SelectedValue & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtSize.Text = Trim(dt.Rows(0)("Size") & "")
                txtCType.Text = Trim(dt.Rows(0)("Type") & "")
                txtTareWeight.Text = Trim(dt.Rows(0)("TareWeight") & "")
                txtGrossWeight.Text = Trim(dt.Rows(0)("grossWeight") & "")
                txtInDateTime.Text = Trim(dt.Rows(0)("Indate") & "")
                txtISOCode.Text = Trim(dt.Rows(0)("ISOCode") & "")
                txtCCWt.Text = Trim(dt.Rows(0)("carryingcapacity") & "")
                txtTruckNo.Text = Trim(dt.Rows(0)("TruckNo") & "")
                txtTransporter.Text = Trim(dt.Rows(0)("Trans In") & "")
                txtBookingNo.Text = Trim(dt.Rows(0)("bkno") & "")
                txtLocation.Text = Trim(dt.Rows(0)("Location") & "")
                txtCondition.Text = Trim(dt.Rows(0)("condition") & "")
                txtSurvyeEIRNo.Text = Trim(dt.Rows(0)("surveyEIRID") & "")
                txtStatus.Text = Trim(dt.Rows(0)("status") & "")
                txtShippingLine.Text = Trim(dt.Rows(0)("SLName") & "")
                txtShipperName.Text = Trim(dt.Rows(0)("shippername") & "")
                txtOutShipper.Text = Trim(dt.Rows(0)("OutShipper") & "")

                txtMFGDate.Text = Trim(dt.Rows(0)("mfgdate") & "")
                txtDamageRemark.Text = Trim(dt.Rows(0)("damageremarks") & "")
                txtRemark.Text = Trim(dt.Rows(0)("Remarks") & "")
                txtGeneratedBy.Text = Trim(dt.Rows(0)("username") & "")

                txtOutDate.Text = Trim(dt.Rows(0)("OutDate") & "")
                txtTruckNoOut.Text = Trim(dt.Rows(0)("VehicleNo") & "")
                txtTransporterOut.Text = Trim(dt.Rows(0)("Trans Out") & "")
                txtBookingNoOut.Text = Trim(dt.Rows(0)("bookingno") & "")
                txtSealNo.Text = Trim(dt.Rows(0)("SealNo") & "")
                txtLocationOut.Text = Trim(dt.Rows(0)("Location Out") & "")
                txtGeneratedByOut.Text = Trim(dt.Rows(0)("gpGenerate") & "")
                txtRemarkOut.Text = Trim(dt.Rows(0)("Remarks Out") & "")
                txtentryType.Text = Trim(dt.Rows(0)("Entry Type") & "")
                txtEyardInPrint.Text = Trim(dt.Rows(0)("GateInNo") & "")
                txtEyardRecipt.Text = Trim(dt.Rows(0)("GateInNo") & "")
                txtgatepass.Text = Trim(dt.Rows(0)("GpNo") & "")
            End If
            strSql = ""
            strSql += "USP_SEARCH_CONTAINER_DETAILS '" & txtContainerNo.Text & "','" & ddlEntryID.SelectedValue & "'"
            ds = db.sub_GetDataSets(strSql)
            If ds.Tables(0).Rows.Count > 0 Then
                txtSurveyType.Text = (ds.Tables(0).Rows(0)("type"))
                txtCSCASP.Text = (ds.Tables(0).Rows(0)("cscasp"))
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                txtEstimateDate.Text = Trim(ds.Tables(1).Rows(0)("est_date") & "")
                txtRepairDate.Text = Trim(ds.Tables(1).Rows(0)("repaireddate") & "")
                txtApproveDate.Text = Trim(ds.Tables(1).Rows(0)("approvedon") & "")
            End If
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtContainerNo_TextChanged(sender As Object, e As EventArgs) Handles txtContainerNo.TextChanged
        Try
            Clear()
            strSql = ""
            strSql += "SELECT *,EntryID  FROM EYard_In  WHERE ContainerNo='" & Trim(txtContainerNo.Text) & "' and iscancel=0 ORDER BY InDate DESC"
            dt = db.sub_GetDatatable(strSql)
            ddlEntryID.DataSource = dt
            ddlEntryID.DataTextField = "EntryID"
            ddlEntryID.DataValueField = "EntryID"
            ddlEntryID.DataBind()
            If Not dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No not found');", True)
                txtContainerNo.Focus()
                Exit Sub
            End If
            ddlEntryID.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub FillGrid()
        Try
            strSql = ""
            strSql += "USP_SHOW_CHARGES_DETAILS '" & txtContainerNo.Text & "','" & ddlEntryID.SelectedValue & "'"
            dt = db.sub_GetDatatable(strSql)
            grdChargesDetails.DataSource = dt
            grdChargesDetails.DataBind()
            ChargesUpdatePanel.Update()

            strSql = ""
            strSql += "sp_YardHolddet '" & ddlEntryID.SelectedValue & "','" & txtContainerNo.Text & "'"
            dt = db.sub_GetDatatable(strSql)
            grdHoldDetails.DataSource = dt
            grdHoldDetails.DataBind()
            HoldUpdatePanel.Update()

            strSql = ""
            strSql += "SELECT  * FROM Eyard_holds H left JOIN eyard_holdreasons R ON H.HoldReasonID=R.HoldReasonID WHERE   ContainerNo='" & Trim(txtContainerNo.Text) & "' AND HoldStatus='H' AND IsCancel=0"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                divHoldImage.Attributes.Add("style", "display:block")
            Else
                divHoldImage.Attributes.Add("style", "display:none")

            End If

            strSql = ""
            strSql += "sp_container_changes_History '" & ddlEntryID.SelectedValue & "','" & txtContainerNo.Text & "'"
            dt = db.sub_GetDatatable(strSql)
            grdAmmendmentLog.DataSource = dt
            grdAmmendmentLog.DataBind()
            AmmendmentUpdatePanel.Update()

            strSql = ""
            strSql += "SP_Show_Estimate_Details '" & ddlEntryID.SelectedValue & "','" & txtContainerNo.Text & "'"
            dt = db.sub_GetDatatable(strSql)
            grdEstimateDetails.DataSource = dt
            grdEstimateDetails.DataBind()
            EstimateUpdatePanel.Update()

            strSql = ""
            strSql += "SP_Show_Approve_Details '" & ddlEntryID.SelectedValue & "','" & txtContainerNo.Text & "'"
            dt = db.sub_GetDatatable(strSql)
            grdApprovedDetails.DataSource = dt
            grdApprovedDetails.DataBind()
            ApproveUpdatePanel.Update()

            strSql = ""
            strSql += "SP_Show_Repair_Details '" & ddlEntryID.SelectedValue & "','" & txtContainerNo.Text & "'"
            dt = db.sub_GetDatatable(strSql)
            grdRepairDetails.DataSource = dt
            grdRepairDetails.DataBind()
            RepairUpdatePanel.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            db.sub_ExecuteNonQuery("delete from TEMP_SEARCH_CONTAINER where USER_ID=" & Session("UserId_DepoCFS") & "")
            txtSize.Text = ""
            txtCType.Text = ""
            txtTareWeight.Text = ""
            txtGrossWeight.Text = ""
            txtInDateTime.Text = ""
            txtISOCode.Text = ""
            txtCCWt.Text = ""
            txtTruckNo.Text = ""
            txtTransporter.Text = ""
            txtBookingNo.Text = ""
            txtLocation.Text = ""
            txtCondition.Text = ""
            txtSurvyeEIRNo.Text = ""
            txtStatus.Text = ""
            txtShippingLine.Text = ""
            txtShipperName.Text = ""
            txtOutShipper.Text = ""
            txtMFGDate.Text = ""
            txtDamageRemark.Text = ""
            txtRemark.Text = ""
            txtGeneratedBy.Text = ""
            divHoldImage.Attributes.Add("style", "display:none")

            txtOutDate.Text = ""
            txtTruckNoOut.Text = ""
            txtTransporterOut.Text = ""
            txtBookingNoOut.Text = ""
            txtSealNo.Text = ""
            txtLocationOut.Text = ""
            txtGeneratedByOut.Text = ""
            txtRemarkOut.Text = ""
            txtSurveyType.Text = ""
            txtCSCASP.Text = ""           
            txtEstimateDate.Text = ""
            txtRepairDate.Text = ""
            txtApproveDate.Text = ""
            strSql = ""
            dt = db.sub_GetDatatable(strSql)
            ddlEntryID.DataSource = dt
            ddlEntryID.DataBind()
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "SELECT TOP 1  * FROM EYardEmptyOut  WHERE GpNo=" & Val(txtgatepass.Text) & " AND ContainerNo='" & Trim(txtContainerNo.Text) & "' AND IsCancel=0"
            dt = db.sub_GetDatatable(strSql)
            If Not dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No not Gate Out');", True)
                txtContainerNo.Focus()
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

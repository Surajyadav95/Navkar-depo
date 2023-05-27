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
            Call Clear_Controls()
            Filldropdown()
            txtMFGDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM")
            ddlcontainerstatus.SelectedItem.Text = "--SELECT--"
        End If

    End Sub
     
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "get_sp_eyard_status"
            ds = db.sub_GetDataSets(strSql)
            ddlType.DataSource = ds.Tables(1)
            ddlType.DataTextField = "ContainerType"
            ddlType.DataValueField = "ContainerTypeID"
            ddlType.DataBind()
            ddlType.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            If txtcontainerNo.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter Container No);", True)
                txtcontainerNo.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql = "exec sp_containerFetchForSurvey '" & Trim(txtcontainerNo.Text & "") & "'"

            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblentryid.Text = Trim(dt.Rows(0)("Entry ID").ToString & "")
                txtSize.Text = Trim(dt.Rows(0)("Size").ToString & "")
                txtdamageremarkms.Text = Trim(dt.Rows(0)("Damage Remarks").ToString & "")
                ddlType.SelectedValue = Val(dt.Rows(0)("ContainerTypeID").ToString & "")
                txtMFGDate.Text = Convert.ToDateTime(dt.Rows(0)("MFG Date")).ToString("yyyy-MM")
                txtlinename.Text = Trim(dt.Rows(0)("Line Name").ToString & "")
                txttareweight.Text = Val(dt.Rows(0)("Tare Weight") & "")
                txtgrossweight.Text = Val(dt.Rows(0)("Gross Weight").ToString & "")
                txtNetWeight.Text = Val(dt.Rows(0)("Gross Weight").ToString & "") - Val(dt.Rows(0)("Tare Weight").ToString & "")
                lblLineId.Text = Trim(dt.Rows(0)("Line ID").ToString & "")
                ddlcontainerstatus.SelectedValue = Trim(dt.Rows(0)("Status").ToString & "")
                txtsurveyremarks.Text = Trim(dt.Rows(0)("Survey Remarks").ToString & "")
                txtBookingremarks.Text = Trim(dt.Rows(0)("BookingRemarks").ToString & "")
                txtCSCASP.Text = Trim(dt.Rows(0)("CSCASP").ToString & "")
                txtPayLoad.Text = Trim(dt.Rows(0)("PayLoad").ToString & "")

                txtdamageremarkms.Focus()
            Else
                txtSize.Text = ""
                lblentryid.Text = ""
                txtdamageremarkms.Text = ""
                'txtType.Text = ""
                txtlinename.Text = ""
                txttareweight.Text = ""
                txtgrossweight.Text = ""
                txtNetWeight.Text = ""
                txtMFGDate.Text = ""
                lblLineId.Text = ""
                txtsurveyremarks.Text = ""
                txtPayLoad.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('his Container not in CFS);", True)
                txtcontainerNo.Focus()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dt1 As New DataTable
            strSql = ""
            strSql = "exec  sp_eyard_Survey_Remarks '" & Trim(lblentryid.Text) & "','" & Trim(txtcontainerNo.Text) & "','" & Trim(txtSize.Text) & "','" & Trim(Replace(txtdamageremarkms.Text, "'", "''") & "") & "',"
            strSql += " '" & Trim(txttareweight.Text) & "','" & Trim(txtNetWeight.Text) & "','" & Trim(txtgrossweight.Text) & "','" & (ddlcontainerstatus.SelectedItem.Text) & "','" & Trim(lblLineId.Text & "") & "','" & Convert.ToDateTime(Trim(txtMFGDate.Text & "")).ToString("yyyy-MM-dd") & "'," & Session("UserId_DepoCFS") & ",'" & Trim(txtBookingremarks.Text) & "','" & Trim(txtCSCASP.Text & "") & "','" & Trim(txtPayLoad.Text & "") & "','" & Trim(ddlType.SelectedValue & "") & "'"
            dt1 = db.sub_GetDatatable(strSql)
            lblSession.Text = "Record Saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            Call Clear_Controls()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub Clear_Controls()
        'txtType.SelectedValue = 0
        lblentryid.Text = ""
        txtsurveyremarks.Text = ""
        txttareweight.Text = ""
        txtNetWeight.Text = ""
        txtgrossweight.Text = ""
        txtContainerNo.Text = ""
        txtsize.Text = ""
        txtdamageremarkms.Text = ""
        txtmfgdate.Text = ""
        txtlinename.Text = ""
        txtBookingremarks.Text = ""
        txtPayLoad.Text = ""
        txtContainerNo.Focus()
    End Sub
End Class

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
            txtfrom.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtTo.Text = Convert.ToDateTime(Now).AddYears(1).ToString("yyyy-MM-dd")

            Filldropdown()

            If Not (Request.QueryString("TariffIDEdit") = "") Then
                TariffID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("TariffIDEdit")))
                strSql = ""
                strSql = "USP_Edit_Tariff_Master_eyard '" & TariffID & "','" & Session("UserId_DepoCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblID.Visible = True
                    lblID.Text = Trim(dt.Rows(0)("slID") & "")
                    txttariffId.Text = Trim(dt.Rows(0)("TariffID") & "")
                    ddlLineName.SelectedValue = Trim(dt.Rows(0)("slID"))
                    txtDescription.Text = Trim(dt.Rows(0)("TariffDescription"))
                    txtDiscount.Text = Trim(dt.Rows(0)("discPercent"))
                    txtfrom.Text = Convert.ToDateTime(Trim(dt.Rows(0)("EffectiveFrom"))).ToString("yyyy-MM-dd")
                    txtTo.Text = Convert.ToDateTime(Trim(dt.Rows(0)("EffectiveUpto"))).ToString("yyyy-MM-dd")
                    ''txtfrom.Text = Trim(dt.Rows(0)("EffectiveFrom"))
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive"))
                End If

                Panel2.Enabled = True

                btnSave.Text = "Update"
            End If
            If Not (Request.QueryString("TariffIDView") = "") Then
                TariffID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("TariffIDView")))
                strSql = ""
                strSql = "USP_Edit_Tariff_Master_eyard '" & TariffID & "','" & Session("UserId_DepoCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblID.Text = Trim(dt.Rows(0)("slID") & "")
                    txttariffId.Text = Trim(dt.Rows(0)("TariffID") & "")
                    ddlLineName.SelectedValue = Trim(dt.Rows(0)("slID"))
                    txtDescription.Text = Trim(dt.Rows(0)("TariffDescription"))
                    txtDiscount.Text = Trim(dt.Rows(0)("discPercent"))
                    txtfrom.Text = Convert.ToDateTime(Trim(dt.Rows(0)("EffectiveFrom"))).ToString("yyyy-MM-dd")
                    txtTo.Text = Convert.ToDateTime(Trim(dt.Rows(0)("EffectiveUpto"))).ToString("yyyy-MM-dd")
                    ''txtfrom.Text = Trim(dt.Rows(0)("EffectiveFrom"))
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive"))
                End If

                Panel2.Enabled = False

                btnSave.Text = "View"
                btnSave.Visible = False
                btnclear.Visible = False
            End If
        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_tariff_List_get_eyard")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If
            ds = db.sub_GetDataSets("USP_Fill_Drop_Tariff_eyard")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlLineName.DataSource = ds.Tables(0)
                ddlLineName.DataTextField = "SLName"
                ddlLineName.DataValueField = "SLID"
                ddlLineName.DataBind()
                ddlLineName.Items.Insert(0, New ListItem("--Select--", 0))
            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If (btnSave.Text = "Update") Then
                strSql = ""
                strSql += "usp_Insert_tariff_master_eyard'" & Trim(ddlLineName.SelectedValue & "") & "','" & Trim(txttariffId.Text & "") & "','" & Trim(txtDescription.Text & "") & "',"
                strSql += "'" & Convert.ToDateTime(Trim(txtfrom.Text & "")).ToString("yyyyMMdd") & "','" & Convert.ToDateTime(Trim(txtTo.Text & "")).ToString("yyyyMMdd") & "',"
                strSql += "'" & Trim(txtDiscount.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_DepoCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record updated successfully  "
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            Else
                If Trim(ddlLineName.SelectedValue) = "--SELECT--" Then
                    MsgBox("Please select customer Name", vbCritical)
                    ddlLineName.Focus()
                    Exit Sub
                End If

                strSql = ""
                strSql = "SELECT * FROM Eyard_tariffmaster WHERE SLID='" & Val(ddlLineName.SelectedValue) & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    ddlLineName.Text = "0"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Customer name already exists .');", True)
                    Exit Sub
                End If
                strSql = ""
                strSql += "usp_Insert_tariff_master_eyard'" & Trim(ddlLineName.SelectedValue & "") & "','" & Trim(txttariffId.Text & "") & "','" & Trim(txtDescription.Text & "") & "',"
                strSql += "'" & Convert.ToDateTime(Trim(txtfrom.Text & "")).ToString("yyyyMMdd") & "','" & Convert.ToDateTime(Trim(txtTo.Text & "")).ToString("yyyyMMdd") & "',"
                strSql += "'" & Trim(txtDiscount.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_DepoCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully  "
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlLineName_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "SELECT  slID  FROM ShipLines  where SlName='" & Trim(ddlLineName.SelectedItem.Text) & "' "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblID.Text = dt.Rows(0)("slID")
                divjono.Attributes.Add("style", "display:block")
            Else
                lblID.Text = ""
                divjono.Attributes.Add("style", "display:none")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txttariffId_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "select  Convert(VARCHAR(50),Convert(Datetime, EffectiveFrom),105) EffectiveF,Convert(VARCHAR(50),Convert(Datetime, EffectiveUpto),105) EffectiveT,* from Eyard_tariffmaster where  TariffID ='" & txttariffId.Text & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtDescription.Text = Trim(dt.Rows(0)("TariffDescription"))
                txtDiscount.Text = Trim(dt.Rows(0)("discPercent"))
                txtfrom.Text = Convert.ToDateTime(Trim(dt.Rows(0)("EffectiveF"))).ToString("yyyy-MM-dd")
                txtTo.Text = Convert.ToDateTime(Trim(dt.Rows(0)("EffectiveT"))).ToString("yyyy-MM-dd")

                If Val(dt.Rows(0)("IsActive")) = True Then
                    chkisActive.Checked = True
                Else
                    chkisActive.Checked = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

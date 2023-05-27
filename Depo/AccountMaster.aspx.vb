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
            Filldropdown()
            If Not (Request.QueryString("AccountIDEdit") = "") Then
                AccountID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("AccountIDEdit")))
                strSql = ""
                strSql = "USP_Epic_Edit_Account '" & AccountID & "','" & Session("UserId_DepoCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblAccountID.Text = Trim(dt.Rows(0)("AccountID") & "")
                    txtAccountID.Text = Trim(dt.Rows(0)("AccountID") & "")
                    lblAccountName.Text = Trim(dt.Rows(0)("AccountName") & "")
                    txtAccountName.Text = Trim(dt.Rows(0)("AccountName") & "")
                    txtTally.Text = Trim(dt.Rows(0)("TallyName") & "")
                    ddlGroup.SelectedValue = Trim(dt.Rows(0)("GroupID") & "")
                    txtHsn.Text = Trim(dt.Rows(0)("HSNCode") & "")
                    txtDisplay.Text = Trim(dt.Rows(0)("DisplayPriority") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")
                    txtChargesDescription.Text = Trim(dt.Rows(0)("HSNChargesDescription") & "")

                End If
                Panel2.Enabled = True
                btnSave.Text = "Update"

            End If
            If Not (Request.QueryString("AccountIDView") = "") Then
                AccountID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("AccountIDView")))
                strSql = ""
                strSql = "USP_Epic_Edit_Account '" & AccountID & "','" & Session("UserId_DepoCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    lblAccountID.Text = Trim(dt.Rows(0)("AccountID") & "")
                    txtAccountID.Text = Trim(dt.Rows(0)("AccountID") & "")
                    lblAccountName.Text = Trim(dt.Rows(0)("AccountName") & "")
                    txtAccountName.Text = Trim(dt.Rows(0)("AccountName") & "")
                    txtTally.Text = Trim(dt.Rows(0)("TallyName") & "")
                    ddlGroup.SelectedValue = Trim(dt.Rows(0)("GroupID") & "")
                    txtHsn.Text = Trim(dt.Rows(0)("HSNCode") & "")
                    txtDisplay.Text = Trim(dt.Rows(0)("DisplayPriority") & "")
                    chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")
                    txtChargesDescription.Text = Trim(dt.Rows(0)("HSNChargesDescription") & "")
                End If

                Panel2.Enabled = False
                chkisActive.Enabled = False
                btnSave.Text = "View"
                btnSave.Visible = False
                btnclear.Visible = False
            End If

        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_Epic_Account_Name_recipt_list")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If

            ds = db.sub_GetDataSets("use_fill_group_eyard")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlGroup.DataSource = ds.Tables(0)
                ddlGroup.DataTextField = "GroupName"
                ddlGroup.DataValueField = "GroupID"
                ddlGroup.DataBind()
                ddlGroup.Items.Insert(0, New ListItem("--Select--", 0))
            End If

            'If (ds.Tables(1).Rows.Count > 0) Then
            '    ddlDisplay.DataSource = ds.Tables(1)
            '    ddlDisplay.DataTextField = "DisplayPriority"
            '    ddlDisplay.DataValueField = "GroupID"
            '    ddlDisplay.DataBind()
            '    ddlDisplay.Items.Insert(0, New ListItem("--Select--", 0))
            'End If

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

                If Trim(txtAccountName.Text) <> Trim(lblAccountName.Text) Then
                    strSql = ""
                    strSql += "SELECT Count(*) FROM Eyard_accountmaster WHERE AccountName='" & Trim(txtAccountName.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (dt.Rows(0)(0) >= 1) Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Account Name Already Exists .');", True)
                        txtAccountName.Focus()
                        Exit Sub
                    End If
                End If

                strSql = ""
                strSql += "USP_Update_eyardAccount '" & Trim(lblAccountID.Text & "") & "','" & Replace(Trim(txtAccountName.Text & ""), "'", "''") & "','" & Replace(Trim(txtTally.Text & ""), "'", "''") & "','" & Trim(ddlGroup.SelectedValue & "") & "',"
                strSql += "'" & Trim(txtHsn.Text & "") & "','" & Trim(txtDisplay.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtChargesDescription.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record Updated for AccountID " & lblAccountID.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            Else

                strSql = ""
                strSql += "USP_eyard_account_Name '" & Replace(Trim(txtAccountName.Text & ""), "'", "''") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    txtAccountName.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Account name Already Exists .');", True)
                    txtAccountName.Focus()
                    Exit Sub
                End If

                strSql = ""
                strSql += "USP_eyard_account_Master'" & Replace(Trim(txtAccountName.Text & ""), "'", "''") & "','" & Replace(Trim(txtTally.Text & ""), "'", "''") & "','" & Trim(ddlGroup.SelectedValue & "") & "',"
                strSql += "'" & Trim(txtHsn.Text & "") & "','" & Trim(txtDisplay.Text & "") & "','" & Trim(chkisActive.Checked & "") & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtChargesDescription.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record Saved successfully Account ID " & dt.Rows(0)("AccountID") & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim strId As String
    Dim strname As String
    Dim strcolname As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim GSTID, GSTIDView As String
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
            txtdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            Filldropdown()
            Call btnSearch_Click(sender, e)
            If Not (Request.QueryString("GSTIDEdit") = "") Then
                GSTID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("GSTIDEdit")))
                strSql = ""
                strSql = "USP_Edit_GST '" & GSTID & "','" & Session("UserId_DepoCFS") & "'"
                dt = db.sub_GetDatatable(strSql)
                If (dt.Rows.Count > 0) Then
                    'lblID.Text = Trim(ds.Tables(0).Rows(0)("GSTID") & "")
                    txtID.Text = Trim(dt.Rows(0)("GSTID") & "")
                    txtname.Text = Trim(dt.Rows(0)("GSTName") & "")
                    txtGstUniqu.Text = Trim(dt.Rows(0)("GSTIn_uniqID") & "")
                    txtEmail.Text = Trim(dt.Rows(0)("EmailID") & "")
                    txtcontact.Text = Trim(dt.Rows(0)("MobNo") & "")
                    ddlState.SelectedItem.Text = Trim(dt.Rows(0)("State") & "")
                    txtstateco.Text = Trim(dt.Rows(0)("state_Code") & "")
                    txtAddress.Text = Trim(dt.Rows(0)("GSTAddress") & "")
                    ddlParty.SelectedValue = Trim(dt.Rows(0)("Partytype") & "")
                    txtPincode.Text = Trim(dt.Rows(0)("PINCODE") & "")
                    txtpanno.Text = Trim(dt.Rows(0)("panno") & "")
                    ddltypeof.SelectedItem.Text = Trim(dt.Rows(0)("TyepReg") & "")
                    If Trim(dt.Rows(0)("RegDate") & "") = "" Then
                        txtdate.Text = ""
                    Else
                        txtdate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("RegDate") & "")).ToString("yyyy-MM-dd")
                    End If
                    ddltypeof_SelectedIndexChanged(sender, e)
                    ddlParty_SelectedIndexChanged(sender, e)


                    'ddlname.SelectedItem.Text = Trim(dt.Rows(0)("PartyType") & "")
                    'If ddlParty.SelectedItem.Text <> "--SELECT--" And ddlParty.SelectedItem.Text <> "Consignee" And ddlParty.SelectedItem.Text <> "NA" Then
                    '    If dt.Rows.Count > 0 Then
                    '        '        ddlname.SelectedItem.Text = Trim(dt.Rows(0)(strId) & "")
                    '    End If
                    'End If

                End If
                Panel1.Enabled = True
                Panel2.Enabled = True
                Panel3.Enabled = True
                btnSave.Text = "Update"
            End If
            If Not (Request.QueryString("GSTIDView") = "") Then
                GSTID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("GSTIDView")))
                strSql = ""
                strSql = "USP_Edit_GST '" & GSTID & "','" & Session("UserId_DepoCFS") & "'"
                ds = db.sub_GetDataSets(strSql)
                If (ds.Tables(0).Rows.Count > 0) Then
                    lblID.Text = Trim(ds.Tables(0).Rows(0)("GSTID") & "")
                    txtID.Text = Trim(ds.Tables(0).Rows(0)("GSTID") & "")
                    txtname.Text = Trim(ds.Tables(0).Rows(0)("GSTName") & "")
                    txtGstUniqu.Text = Trim(ds.Tables(0).Rows(0)("GSTIn_uniqID") & "")
                    txtEmail.Text = Trim(ds.Tables(0).Rows(0)("EmailID") & "")
                    txtcontact.Text = Trim(ds.Tables(0).Rows(0)("MobNo") & "")
                    ddlState.SelectedValue = Trim(ds.Tables(0).Rows(0)("State_Code") & "")
                    txtstateco.Text = Trim(ds.Tables(0).Rows(0)("state_Code") & "")
                    txtAddress.Text = Trim(ds.Tables(0).Rows(0)("GSTAddress") & "")
                    ddlParty.SelectedItem.Text = Trim(ds.Tables(0).Rows(0)("Partytype") & "")
                    txtPincode.Text = Trim(dt.Rows(0)("PINCODE") & "")
                    txtpanno.Text = Trim(ds.Tables(0).Rows(0)("panno") & "")
                    ddltypeof.SelectedItem.Text = Trim(ds.Tables(0).Rows(0)("TyepReg") & "")
                    If Trim(ds.Tables(0).Rows(0)("RegDate") & "") = "" Then
                        txtdate.Text = ""
                    Else
                        txtdate.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("RegDate") & "")).ToString("yyyy-MM-dd")
                    End If

                   
                    'txtdate.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("RegDate") & "")).ToString("yyyy-MM-dd")
                    strId = Trim(ds.Tables(0).Rows(0)("PartyType") & "")
                    If ddlParty.SelectedItem.Text <> "--SELECT--" And ddlParty.SelectedItem.Text <> "Consignee" And ddlParty.SelectedItem.Text <> "NA" Then
                        If (ds.Tables(1).Rows.Count > 0) Then
                            ddlname.SelectedValue = Trim(ds.Tables(1).Rows(0)(strId) & "")
                        End If
                    End If
                    ddlParty_SelectedIndexChanged(sender, e)
                    ddltypeof_SelectedIndexChanged(sender, e)
                End If
                Panel1.Enabled = False
                Panel2.Enabled = False
                Panel3.Enabled = False
                btnSave.Text = "View"
                btnSave.Visible = False
                btnclear.Visible = False
            End If
        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            dt = db.sub_GetDatatable("USP_Get_GST_List")
            If dt.Rows.Count > 0 Then
                rptnoLIst.DataSource = dt
                rptnoLIst.DataBind()
            End If
            ddlParty.Items.Insert(0, New ListItem("--Select--", 0))
            ddltypeof.Items.Insert(0, New ListItem("--Select--", 0))
            ddlname.Items.Insert(0, New ListItem("--Select--", 0))

            ds = db.sub_GetDataSets("USP_Fiil_Party_GST")
            If (ds.Tables(0).Rows.Count > 0) Then
                ddlState.DataSource = ds.Tables(0)
                ddlState.DataTextField = "State"
                ddlState.DataValueField = "State_Code"
                ddlState.DataBind()
                ddlState.Items.Insert(0, New ListItem("--Select--", 0))
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
            strSql = ""
            strSql = "USP_Validate_PIN_Code'" & Trim(ddlState.SelectedItem.Text & "") & "','" & Trim(txtPincode.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('" + dt.Rows(0)("message") + "');", True)
                txtpanno.Text = ""
                txtpanno.Focus()
                Exit Sub
            End If
            If (btnSave.Text = "Update") Then
                Dim intReg As Integer
                If ddltypeof.SelectedItem.Text = "Registered" Then
                    intReg = 1
                Else
                    intReg = 0
                End If
                strSql = ""
                strSql += "USP_Save_GSTDetails'" & Trim(txtID.Text & "") & "','" & Trim(ddltypeof.SelectedItem.Text & "") & "','" & Convert.ToDateTime(Trim(txtdate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtGstUniqu.Text & "") & "',"
                strSql += "'" & Trim(txtpanno.Text & "") & "','" & Trim(txtname.Text & "") & "','" & Trim(txtAddress.Text & "") & "','" & Trim(ddlState.SelectedItem.Text & "") & "','" & Trim(txtstateco.Text & "") & "',"
                strSql += "'" & Trim(ddlParty.SelectedItem.Text & "") & "','" & Trim(ddlname.SelectedValue & "") & "','" & Trim(txtEmail.Text & "") & "','" & Trim(txtcontact.Text & "") & "','" & intReg & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtPincode.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record updated successfully for GST ID " & lblID.Text & ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            Else

                Dim strSQL As String
                If Trim(ddltypeof.SelectedItem.Text) = "--SELECT--" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select type of registered.');", True)
                    ddltypeof.Focus()
                    Exit Sub
                End If
                strSQL = ""
                strSQL += "select * from Party_GST_M where GSTName='" & Replace(Trim(txtname.Text & ""), "'", "''") & "' and GSTAddress='" & Replace(Trim(txtAddress.Text & ""), "'", "''") & "'"
                dt = db.sub_GetDatatable(strSQL)
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Same name with address found.Cannot proceed...');", True)
                    txtname.Focus()
                    Exit Sub
                End If
                Dim intReg As Integer
                If ddltypeof.SelectedItem.Text = "Registered" Then
                    intReg = 1
                Else
                    intReg = 0
                End If
                Dim dblGSTID As Integer
                Dim dtID As New DataTable
                strSQL = ""
                strSQL += "select Max(GSTID) as GSTID from Party_GST_M "
                dtID = db.sub_GetDatatable(strSQL)
                dblGSTID = dtID.Rows(0)("GSTID")
                lblID.Text = dblGSTID + 1

                strSQL = ""
                strSQL += "USP_Save_GSTDetails'" & Trim(lblID.Text) & "','" & Trim(ddltypeof.SelectedItem.Text & "") & "','" & Convert.ToDateTime(Trim(txtdate.Text & "")).ToString("yyyy-MM-dd") & "','" & Trim(txtGstUniqu.Text & "") & "',"
                strSQL += "'" & Trim(txtpanno.Text & "") & "','" & Trim(txtname.Text & "") & "','" & Trim(txtAddress.Text & "") & "','" & Trim(ddlState.SelectedItem.Text & "") & "','" & Trim(txtstateco.Text & "") & "',"
                strSQL += "'" & Trim(ddlParty.SelectedItem.Text & "") & "','" & Trim(ddlname.SelectedValue & "") & "','" & Trim(txtEmail.Text & "") & "','" & Trim(txtcontact.Text & "") & "','" & intReg & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtPincode.Text & "") & "'"
                dt = db.sub_GetDatatable(strSQL)
                lblSession.Text = "Record saved successfully "
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try

    End Sub
    Protected Sub txtUniqueid_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim left2 As String
            left2 = txtGstUniqu.Text.Substring(0, 2)
            strSql = ""
            strSql += "Sp_stateFilter '" & left2 & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddlState.SelectedValue = Trim(dt.Rows(0)("State_Code") & "")
                txtstateco.Text = Trim(dt.Rows(0)("state_Code") & "")
            End If

            strSql = ""
            strSql = "SELECT  GSTIn_uniqID,Partytype FROM Party_GST_M where GSTIn_uniqID='" & Trim(txtGstUniqu.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblSession1.Text = "This GSTIN is already exists, Do you want add another Address for this GSTIN? "
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
                'UpdatePanel1.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnok1_ServerClick(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_PARTY_GST_MASTER'" & Trim(txtGstUniqu.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                'lblID.Text = Trim(ds.Tables(0).Rows(0)("GSTID") & "")
                txtname.Text = Trim(ds.Tables(0).Rows(0)("GSTName") & "")
                txtEmail.Text = Trim(ds.Tables(0).Rows(0)("EmailID") & "")
                txtcontact.Text = Trim(ds.Tables(0).Rows(0)("MobNo") & "")
                ddlState.SelectedValue = Trim(ds.Tables(0).Rows(0)("State_Code") & "")
                txtstateco.Text = Trim(ds.Tables(0).Rows(0)("state_Code") & "")
                txtAddress.Text = Trim(ds.Tables(0).Rows(0)("GSTAddress") & "")
                ddlParty.SelectedItem.Text = Trim(ds.Tables(0).Rows(0)("Partytype") & "")
                ddlParty_SelectedIndexChanged(sender, e)
                txtpanno.Text = Trim(ds.Tables(0).Rows(0)("panno") & "")
                ddltypeof.SelectedItem.Text = Trim(ds.Tables(0).Rows(0)("TyepReg") & "")
                ddltypeof_SelectedIndexChanged(sender, e)
                txtdate.Text = Trim(ds.Tables(0).Rows(0)("RegDate") & "")
                strId = Trim(ds.Tables(0).Rows(0)("PartyType") & "")
                If ddlParty.SelectedItem.Text <> "--SELECT--" And ddlParty.SelectedItem.Text <> "Consignee" And ddlParty.SelectedItem.Text <> "NA" Then
                    If (ds.Tables(1).Rows.Count > 0) Then

                        ddlname.SelectedValue = Trim(ds.Tables(1).Rows(0)(strId) & "")
                    End If
                End If

            End If
            UpdatePanel4.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If (ddlParty.SelectedItem.Text = "Customer") Then
                divName.Attributes.Add("style", "display:block")

            Else
                divName.Attributes.Add("style", "display:None")
            End If

            If ddlParty.SelectedItem.Text = "--SELECT--" Or ddlParty.SelectedItem.Text = "Consignee" Or ddlParty.SelectedItem.Text = "NA" Then
                strname = ""
                Panel1.Enabled = False

            ElseIf ddlParty.SelectedItem.Text = "CHA" Then
                strname = "CHA"
                strId = "CHAID"
                strcolname = "CHAName"
                Panel1.Enabled = True
                FillName()

            ElseIf ddlParty.SelectedItem.Text = "Line" Then
                strname = "ShipLines"
                strId = "SLID"
                strcolname = "SLName"
                Panel1.Enabled = True
                FillName()
            ElseIf ddlParty.SelectedItem.Text = "Customer" Then
                strname = "imp_agents"
                strId = "agentID"
                strcolname = "agentName"
                Panel1.Enabled = True
                FillName()

            ElseIf ddlParty.SelectedItem.Text = "Exporter" Then
                strname = "SHIPPER"
                strId = "ShipperID"
                strcolname = "ShipperName"
                Panel1.Enabled = True
                FillName()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub FillName()
        strSql = ""
        strSql = "SP_FillCombo '" & Trim(ddlParty.SelectedItem.Text & "") & "'"
        dt = db.sub_GetDatatable(strSql)
        If (dt.Rows.Count > 0) Then
            ddlname.DataSource = dt
            ddlname.DataTextField = "ConDesc"
            ddlname.DataValueField = "ConID"
            ddlname.DataBind()
            ddlname.Items.Insert(0, New ListItem("--Select--", 0))
        End If
    End Sub
    Protected Sub ddltypeof_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If Trim(ddltypeof.SelectedItem.Text & "") = "Registered" Or Trim(ddltypeof.SelectedItem.Text & "") = "SEZ Registered" Then
                txtGstUniqu.ReadOnly = False

                Panel3.Enabled = False

            ElseIf ddltypeof.SelectedItem.Text = "UnRegistered" Then
                txtGstUniqu.ReadOnly = True
                txtGstUniqu.Text = "UnRegistered"
                Panel3.Enabled = True

            ElseIf ddltypeof.SelectedItem.Text = "--SELECT--" Then
                txtGstUniqu.ReadOnly = False

            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim dt2 As New DataTable
            strSql = ""
            strSql = "USP_GST_PARTY '" & Trim(txtSearch.Text) & "'"
            dt2 = db.sub_GetDatatable(strSql)
            GrdGSTPartySearch.DataSource = dt2
            GrdGSTPartySearch.DataBind()

        Catch ex As Exception

        End Try
    End Sub

End Class

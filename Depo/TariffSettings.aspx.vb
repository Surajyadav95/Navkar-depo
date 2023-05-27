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
            db.sub_ExecuteNonQuery("Delete from Temp_Charges_eyard Where UniqueID=" & Session("UserId_DepoCFS") & "")
            Filldropdown()
            Add(sender, e)
            'If Not (Request.QueryString("TariffIDView") = "") Then
            '    TariffID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("TariffIDView")))
            '    strSql = ""
            '    strSql = "use_view_cancel '" & TariffID & "','" & Session("UserId_DepoCFS") & "'"
            '    dt = db.sub_GetDatatable(strSql)

            '    If (dt.Rows.Count > 0) Then
            '        ddlTariff.SelectedItem.Text = Trim(dt.Rows(0)("tariffID") & "")
            '        ddlInvoiceType.SelectedItem.Text = Trim(dt.Rows(0)("bondtype") & "")
            '        txtfrom.Text = Trim(dt.Rows(0)("effectivefrom") & "")
            '        txtUpTo.Text = Trim(dt.Rows(0)("effectiveupto") & "")
            '        ddlAccount.SelectedItem.Text = Trim(dt.Rows(0)("accountID") & "")
            '        ddlsize.SelectedItem.Text = Trim(dt.Rows(0)("Size") & "")
            '        ddlCharges.SelectedItem.Text = Trim(dt.Rows(0)("SorF") & "")
            '        txtFixedAmount.Text = Trim(dt.Rows(0)("fixedamt") & "")

            '        'Dim IsTax As String
            '        'If dt.Rows(0)("IsSTax") = True Then
            '        '    IsTax = True
            '        'Else
            '        '    IsTax = False
            '        'End If

            '        'Dim OnDelivereed As String
            '        'If dt.Rows(0)("ConsiderArea") = True Then
            '        '    OnDelivereed = True
            '        'Else
            '        '    OnDelivereed = False
            '        'End If
            '    End If
            '    Panel2.Enabled = False
            '    btnSave.Visible = False
            '    btnclear.Visible = False
            'End If
            ddlCharges_SelectedIndexChanged(sender, e)            
        End If

    End Sub
    Public Sub grid()
        strSql = ""
        strSql += ""
        dt = db.sub_GetDatatable(strSql)
        grdcontainer.DataSource = dt
        grdcontainer.DataBind()
    End Sub
    Protected Sub Filldropdown()
        Try
            ds = db.sub_GetDataSets("USP_FILL_DROPDOWN_TARIFF_SETTINGS")

            ddlTariff.DataSource = ds.Tables(0)
            ddlTariff.DataTextField = "TariffID"
            ddlTariff.DataValueField = "ENTRYID"
            ddlTariff.DataBind()
            ddlTariff.Items.Insert(0, New ListItem("--Select--", 0))

            ddlAccount.DataSource = ds.Tables(1)
            ddlAccount.DataTextField = "AccountName"
            ddlAccount.DataValueField = "AccountID"
            ddlAccount.DataBind()
            ddlAccount.Items.Insert(0, New ListItem("--Select--", 0))

            ddlInvoiceType.DataSource = ds.Tables(2)
            ddlInvoiceType.DataTextField = "InvoiceType"
            ddlInvoiceType.DataValueField = "ID"
            ddlInvoiceType.DataBind()
            ddlInvoiceType.Items.Insert(0, New ListItem("--Select--", 0))

            chkType.DataSource = ds.Tables(3)
            chkType.DataTextField = "Type"
            chkType.DataValueField = "ContainerTypeID"
            chkType.DataBind()

            ddlTaxCode.DataSource = ds.Tables(4)
            ddlTaxCode.DataTextField = "taxname"
            ddlTaxCode.DataValueField = "SettingsID"
            ddlTaxCode.DataBind()
            ddlTaxCode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlFromLocation.DataSource = ds.Tables(5)
            ddlFromLocation.DataTextField = "location"
            ddlFromLocation.DataValueField = "locationid"
            ddlFromLocation.DataBind()
            ddlFromLocation.Items.Insert(0, New ListItem("--Select--", 0))

            ddlToLocation.DataSource = ds.Tables(5)
            ddlToLocation.DataTextField = "location"
            ddlToLocation.DataValueField = "locationid"
            ddlToLocation.DataBind()
            ddlToLocation.Items.Insert(0, New ListItem("--Select--", 0))

            ddlSlab.Items.Insert(0, New ListItem("--Select--", 0))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim dblCount As Double = 0
            For Each row In grdcontainer.Rows
                strSql = ""
                strSql += "USP_VALIDATION_FOR_TARIFF_SETTINGS '" & Trim(ddlTariff.SelectedItem.Text & "") & "'," & Trim(CType(row.FindControl("lblsize"), Label).Text & "") & ","
                strSql += "'" & Trim(CType(row.FindControl("lblInvoicetype"), Label).Text & "") & "'," & Trim(CType(row.FindControl("lblAccountID"), Label).Text & "") & ""
                strSql += ",'" & Val(CType(row.FindControl("lblFromID"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblToID"), Label).Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Tariff already set for this Tariff ID');", True)
                    CType(row.FindControl("lnkDelete"), LinkButton).Focus()
                    Exit Sub
                End If
                dblCount += 1
            Next
            If dblCount = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please add tariff details first');", True)
                Exit Sub
            End If

            strSql = ""
            strSql += "usp_insert_into_eyard_tariff"
            strSql += " " & Session("UserId_DepoCFS") & ""
            dt = db.sub_GetDatatable(strSql)

            Button1.Text = "Save"
            Button1.Attributes.Add("Class", "btn btn-primary btn-sm")
            Clear(sender, e)
            lblSession.Text = "Record saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()

        Catch ex As Exception
            Button1.Text = "Save"
            Button1.Attributes.Add("Class", "btn btn-primary")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Try
            Dim IsTax As String, dblTypeCount As Double = 0
            For i = 0 To chkType.Items.Count - 1
                If chkType.Items(i).Selected = True Then
                    dblTypeCount += 1
                End If
            Next
            If dblTypeCount = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select atleast one Container Type');", True)
                chkType.Focus()
                Exit Sub
            End If
            If ddlCharges.SelectedValue = "Fixed" Then
                If txtFixedAmount.Text = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter Fixed Amount');", True)
                    txtFixedAmount.Focus()
                    Exit Sub
                End If
            Else
                If ddlSlab.SelectedValue = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select slab ID first');", True)
                    ddlSlab.Focus()
                    Exit Sub
                End If
            End If
            For i = 0 To chkType.Items.Count - 1
                If chkType.Items(i).Selected = True Then
                    strSql = ""
                    strSql += "use_temp_traiff_setting_eyard "
                    strSql += "'" & Trim(ddlTariff.SelectedItem.Text & "") & "','" & Trim(ddlInvoiceType.SelectedItem.Text) & "','" & Convert.ToDateTime(Trim(txtfrom.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txtUpTo.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                    strSql += "'" & Trim(ddlAccount.SelectedValue & "") & "','" & Trim(ddlsize.SelectedValue & "") & "','" & Trim(ddlCharges.SelectedValue & "") & "',"
                    strSql += "'" & Trim(txtFixedAmount.Text & "") & "','" & chkIstax.Checked & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtFreeDays.Text & "") & "','" & Trim(chkType.Items(i).Text) & "'," & Val(ddlTaxCode.SelectedValue) & ",'" & ddlSlab.SelectedValue & "'"
                    strSql += ",'" & Val(ddlFromLocation.SelectedValue) & "','" & Val(ddlToLocation.SelectedValue) & "'"
                    db.sub_ExecuteNonQuery(strSql)
                End If                
            Next
            
            Add(sender, e)
            ddlAccount.SelectedValue = "0"
            ddlsize.SelectedValue = "20"
            ddlCharges.SelectedValue = "Fixed"
            txtFixedAmount.Text = ""
            txtFreeDays.Text = ""
            ddlTaxCode.SelectedValue = 0
            ddlSlab.SelectedValue = 0
            ddlFromLocation.SelectedValue = 0
            ddlToLocation.SelectedValue = 0
            For i = 0 To chkType.Items.Count - 1
                chkType.Items(i).Selected = False                
            Next
            ddlCharges_SelectedIndexChanged(sender, e)
            divSlabDets.Attributes.Add("style", "display:none")
            UpdatePanel1.Update()
            chkIstax.Checked = True
            up_grid.Update()
            Button1.Text = "Add"
            Button1.Attributes.Add("Class", "btn btn-info btn-sm")
        Catch ex As Exception
            Button1.Text = "Add"
            Button1.Attributes.Add("Class", "btn btn-info btn-sm")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Add(sender As Object, e As EventArgs)
        Try
            strSql = "USP_SELECT_FROM_TEMP_CHARGES_eyard " & Session("UserId_DepoCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkRemove As LinkButton = DirectCast(sender, LinkButton)
            Dim grdContainer As GridViewRow = DirectCast(lnkRemove.Parent.Parent, GridViewRow)
            Dim AutoID As String = lnkRemove.CommandArgument
            dt = db.sub_GetDatatable("USP_Delete_temp_eyard_traiff '" & AutoID & "'")
            Add(sender, e)          

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlTariff_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "USP_TARIFF_DETAILS_ONCHANGE_eyard '" & Trim(ddlTariff.SelectedItem.Text & "") & "'"
            ds = db.sub_GetDataSets(strSql)
            If (ds.Tables(0).Rows.Count > 0) Then
                txtfrom.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("EffectiveFrom") & "")).ToString("yyyy-MM-ddTHH:mm")
                txtUpTo.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("EffectiveUpto") & "")).ToString("yyyy-MM-ddTHH:mm")
            End If
            If ds.Tables(1).Rows.Count > 0 Then
                txtTariffDesc.Text = Trim(ds.Tables(1).Rows(0)("Description") & "")
            End If
            'UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Clear(sender As Object, e As EventArgs)
        Try
            db.sub_ExecuteNonQuery("Delete from Temp_Charges_eyard Where UniqueID=" & Session("UserId_DepoCFS") & "")
            Add(sender, e)
            For i = 0 To chkType.Items.Count - 1
                chkType.Items(i).Selected = False
            Next            
            ddltariff.SelectedValue = 0
            txtfrom.Text = ""
            txtUpTo.Text = ""
            txtTariffDesc.Text = ""
            ddlInvoiceType.SelectedValue = 0
            txtFreeDays.Text = ""
            ddlAccount.SelectedValue = "0"
            ddlsize.SelectedValue = "20"
            ddlCharges.SelectedValue = "Fixed"
            txtFixedAmount.Text = ""
            chkIstax.Checked = True
            divSlabDets.Attributes.Add("style", "display:none")
            ddlCharges_SelectedIndexChanged(sender, e)
            up_grid.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlCharges_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try            
            txtFixedAmount.Text = ""
            If Not ddlCharges.SelectedValue = "Fixed" Then
                strSql = ""
                strSql += "select distinct slabID FROM eyard_slabs WHERE SlabOn='" & Trim(ddlCharges.SelectedItem.Text) & "' AND Size=" & ddlsize.SelectedValue & " order by slabID "
                dt = db.sub_GetDatatable(strSql)
                ddlSlab.DataSource = dt
                ddlSlab.DataTextField = "slabID"
                ddlSlab.DataValueField = "slabID"
                ddlSlab.DataBind()
                ddlSlab.Items.Insert(0, New ListItem("--Select--", 0))
                divSlab.Attributes.Add("style", "display:block")
                divSlabAdd.Attributes.Add("style", "display:block")
                divFixedAmount.Attributes.Add("style", "display:none")
                ddlSlab_SelectedIndexChanged(sender, e)
                UpdatePanel1.Update()

            Else                
                divSlab.Attributes.Add("style", "display:none")
                divSlabAdd.Attributes.Add("style", "display:none")
                divSlabDets.Attributes.Add("style", "display:none")
                divFixedAmount.Attributes.Add("style", "display:block")
                UpdatePanel1.Update()

            End If
            ddlCharges.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnSlab_Click(sender As Object, e As EventArgs)
        Try
            ddlCharges_SelectedIndexChanged(sender, e)
            ddlSlab.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlSlab_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If ddlSlab.SelectedValue = 0 Then
                divSlabDets.Attributes.Add("style", "display:none")
                UpdatePanel1.Update()
            Else
                divSlabDets.Attributes.Add("style", "display:block")
                strSql = ""
                strSql += "select * FROM eyard_slabs WHERE SlabID=" & ddlSlab.SelectedValue & " and Size=" & ddlsize.SelectedValue & " order by FromSlab"
                dt = db.sub_GetDatatable(strSql)
                grdSlabDets.DataSource = dt
                grdSlabDets.DataBind()
                UpdatePanel1.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlsize_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            ddlCharges_SelectedIndexChanged(sender, e)
            ddlCharges.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chkSelectAll.Checked = True Then
                For i = 0 To chkType.Items.Count - 1
                    chkType.Items(i).Selected = True
                Next
            Else
                For i = 0 To chkType.Items.Count - 1
                    chkType.Items(i).Selected = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub chkType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim dblchkcount As Double = 0
            For i = 0 To chkType.Items.Count - 1
                If chkType.Items(i).Selected = True Then
                    dblchkcount += 1
                End If
            Next
            If dblchkcount = chkType.Items.Count Then
                chkSelectAll.Checked = True
            Else
                chkSelectAll.Checked = False
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

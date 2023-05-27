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
    Dim dt, dt1, dt9 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim dblGroup1Amt As Double
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
            db.sub_ExecuteNonQuery("Delete from Temp_Other_Assessment_Depo Where UserID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_GST_Search_Depo Where UserID=" & Session("UserId_DepoCFS") & "")
            txtvaliddate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtinvdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")

            Filldropdown()
            FillGrid()
            ddlCommodity_TextChanged(sender, e)
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_Fill_inv_list_Depo"
            ds = db.sub_GetDataSets(strSql)
            ddlcustomer.DataSource = ds.Tables(0)
            ddlcustomer.DataTextField = "agentName"
            ddlcustomer.DataValueField = "agentID"
            ddlcustomer.DataBind()
            ddlcustomer.Items.Insert(0, New ListItem("--Select--", 0))
            ddlimporter.DataSource = ds.Tables(1)
            ddlimporter.DataTextField = "ImporterName"
            ddlimporter.DataValueField = "ImporterID"
            ddlimporter.DataBind()
            ddlimporter.Items.Insert(0, New ListItem("--Select--", 0))
            ddlCHA.DataSource = ds.Tables(2)
            ddlCHA.DataTextField = "CHAName"
            ddlCHA.DataValueField = "CHAID"
            ddlCHA.DataBind()
            ddlCHA.Items.Insert(0, New ListItem("--Select--", 0))
            ddlaccntheads.DataSource = ds.Tables(4)
            ddlaccntheads.DataTextField = "AccountName"
            ddlaccntheads.DataValueField = "AccountID"
            ddlaccntheads.DataBind()
            ddlaccntheads.Items.Insert(0, New ListItem("--Select--", 0))
            ddlwarehouse.DataSource = ds.Tables(5)
            ddlwarehouse.DataTextField = "Warehouse_code"
            ddlwarehouse.DataValueField = "Warehouse_code"
            ddlwarehouse.DataBind()
            ddlwarehouse.Items.Insert(0, New ListItem("--Select--", 0))
            ddlTax.DataSource = ds.Tables(7)
            ddlTax.DataTextField = "Tax"
            ddlTax.DataValueField = "settingsID"
            ddlTax.DataBind()
            ddlTax.Items.Insert(0, New ListItem("--Select--", 0))

            ddlInvoiceType.DataSource = ds.Tables(8)
            ddlInvoiceType.DataTextField = "InvoiceType"
            ddlInvoiceType.DataValueField = "ID"
            ddlInvoiceType.DataBind()
            ddlInvoiceType.Items.Insert(0, New ListItem("--Select--", 0))

            strSql = ""
            strSql += "USP_LOCATION_CUSTOMER_WISE '" & lblTariffId.Text & "','Depo'"
            dt = db.sub_GetDatatable(strSql)
            ddlLocation.DataSource = dt
            ddlLocation.DataTextField = "Location"
            ddlLocation.DataValueField = "LocationID"
            ddlLocation.DataBind()

            strSql = ""
            strSql += "USP_Fill_Noc_list_DOMESTIC"
            ds = db.sub_GetDataSets(strSql)
            ddlCommodity.DataSource = ds.Tables(8)
            ddlCommodity.DataTextField = "Commodity_Group_Name"
            ddlCommodity.DataValueField = "Commodity_Group_ID"
            ddlCommodity.DataBind()
            ddlLocation.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_GST_Search_Depo where userid='" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                If dt.Rows.Count > 0 Then
                    txtgstin.Text = Trim(dt.Rows(0)("GSTNo") & "")
                    txtgstname.Text = Trim(dt.Rows(0)("GSTName") & "")
                    lblstatecode.Text = Val(dt.Rows(0)("Statecode"))
                    lblpartyid.Text = Trim(dt.Rows(0)("Gstid") & "")
                    ddlcustomer.Focus()
                    Call Sub_SGTRate()
                    FillGrid()                    

                Else
                    txtgstin.Text = ""
                    txtgstname.Text = ""
                    lblstatecode.Text = ""
                    txtgstin.Focus()
                    lblpartyid.Text = ""
                    Exit Sub
                End If
            End If
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
            strSql += "USP_GST_Cal_Domestic_other " & Val(ddlTax.SelectedValue) & ""
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
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim intid As Integer = 0, AccountID As String = ""
            Dim strinvoiceNo As String = ""
            If txtStorageFrom.Text <> "" And txtStorageUpto.Text <> "" Then
                If Convert.ToDateTime(txtStorageFrom.Text).ToString("yyyyMMdd") > Convert.ToDateTime(txtStorageUpto.Text).ToString("yyyyMMdd") Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid date');", True)
                    txtStorageUpto.Focus()
                    btnSave.Text = "Save"
                    btnSave.Attributes.Add("Class", "btn btn-primary")
                    Exit Sub
                End If
            End If
            'If txtInsFrom.Text <> "" And txtInsUpto.Text <> "" Then
            '    If Convert.ToDateTime(txtInsFrom.Text).ToString("yyyyMMdd") > Convert.ToDateTime(txtInsUpto.Text).ToString("yyyyMMdd") Then
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid date');", True)
            '        txtInsUpto.Focus()
            '        btnSave.Text = "Save"
            '        btnSave.Attributes.Add("Class", "btn btn-primary")
            '        Exit Sub
            '    End If
            'End If

            Dim strWorkyear As String = ""
            Dim InvoiceDate As Date = Trim(txtinvdate.Text)
            If InvoiceDate.Month < 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") - 1 & "-" & Format(InvoiceDate, "yy")
            ElseIf InvoiceDate.Month >= 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") & "-" & Format(InvoiceDate, "yy") + 1
            End If
            Dim count As Double = 0, dblassessno As Double = 0
            Dim dblSumSGSTAmt As Double = 0, dblSumNetAmtTotal As Double = 0, dblSumCGSTAmt As Double = 0, dblSumIGSTAmt As Double = 0, dblgrandtotal As Double = 0

            For Each row In grdcharges.Rows
                count += 1
                Exit For
            Next
            If Not count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No details selected for assessment');", True)
                btnSave.Text = "Save"
                btnSave.Attributes.Add("Class", "btn btn-primary")
                Exit Sub
            End If
            'strSql = ""
            'strSql += "select * from NOC where NOCNo='" & Trim(txtnocno.Text & "") & "' and iscancel=0"
            'dt1 = db.sub_GetDatatable(strSql)
            'If Not dt1.Rows.Count > 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('NOC No not found. Please enter valid NOC No.');", True)
            '    txtnocno.Focus()
            '    btnSave.Text = "Save"
            '    btnSave.Attributes.Add("Class", "btn btn-primary")
            '    Exit Sub
            'End If
            Call Sub_SGTRate()

            'strSql = ""
            'strSql += "SELECT isnull(MAX(AssessNo),0) FROM Proforma_M WITH(XLOCK) WHERE WorkYear='" & strWorkyear & "' "
            'dt = db.sub_GetDatatable(strSql)
            'If dt.Rows(0)(0) = 0 Then
            '    dblassessno = 1
            'Else
            '    dblassessno = Val(dt.Rows(0)(0)) + 1
            'End If
            strSql = ""
            strSql = "USP_INSERT_INTO_EYARD_ASSESSM_OTHER_Proforma_M '" & Trim(strWorkyear) & "','" & Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd HH:mm") & "',"
            If txtStorageUpto.Text = "" Then
                strSql += "NULL,"
            Else
                strSql += "'" & Convert.ToDateTime(txtStorageUpto.Text).ToString("yyyy-MM-dd HH:mm") & "',"
            End If
            strSql += "" & Val(ddlcustomer.SelectedValue) & ",'" & Val(lblTotal.Text) & "','" & Val(lblAllTotal.Text) & "'," & Session("UserId_DepoCFS") & "," '" & Trim(RupeesConvert(Val(lblAllTotal.Text))) & "',"
            strSql += "'" & Val(lblSGST.Text) & "','" & Val(lblCGST.Text) & "','" & Trim(lblIGST.Text) & "','" & Val(lblpartyid.Text) & "'," & Val(ddlInvoiceType.SelectedValue) & ",'" & Replace(Trim(txtremarks.Text), "'", "''") & "','" & Trim(ddlreference.SelectedValue & "") & "','" & Trim(txtReference.Text & "") & "','" & Trim(lblTaxID.Text & "") & "'"

            dt = db.sub_GetDatatable(strSql)
            intid = Val(dt.Rows(0)("ASSESS_NO"))
            strinvoiceNo = Trim(dt.Rows(0)("INVOICE_NO"))

            For Each row In grdcharges.Rows
                strSql = ""
                strSql += "USP_insert_into_eyard_assessd_other_assessment_Proforma_D " & Val(intid) & ",'" & strinvoiceNo & "','" & strWorkyear & "',"
                strSql += "'" & Trim(CType(row.FindControl("lblEntryID"), Label).Text) & "','" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text) & "','" & Trim(CType(row.FindControl("lblaccntid"), Label).Text) & "','" & Trim(CType(row.FindControl("lblntamnt"), Label).Text) & "','" & Trim(CType(row.FindControl("lblType"), Label).Text) & "','" & Trim(CType(row.FindControl("lblSize"), Label).Text) & "',"
                strSql += "'" & Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblSGST / 100) & "','" & Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblCGST / 100) & "', '" & Val(CType(row.FindControl("lblntamnt"), Label).Text) * (dblIGST / 100) & "'," & dbltaxgroupid & ",0"
                db.sub_ExecuteNonQuery(strSql)
            Next
            Clear()
            txtinvdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            txtcontainerno.Text = ""
            txtassessno.Text = strinvoiceNo
            txtworkyear.Text = strWorkyear
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            lblSession.Text = "Record saved successfully for Assess NO " & dblassessno & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            btnSave.Text = "Save"
            btnSave.Attributes.Add("Class", "btn btn-primary")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub FillGrid()
        Try
            strSql = ""
            strSql += "USP_fill_grid_other_assessment_Depo '" & Session("UserId_DepoCFS") & "'"
            ds = db.sub_GetDataSets(strSql)
            grdcharges.DataSource = ds.Tables(0)
            grdcharges.DataBind()
            If ds.Tables(0).Rows.Count > 0 Then
                divtblWOTOtal.Attributes.Add("style", "display:block")
            Else
                divtblWOTOtal.Attributes.Add("style", "display:none")
            End If
            If Val(ds.Tables(1).Rows(0)(0)) <> 0 Then
                dblGroup1Amt = Trim(ds.Tables(1).Rows(0)(0))
                sub_CalcTotals()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CalcTotals()
        Try
            Dim dbltotal As Double = 0, dblvalSGST As Double = 0, dbltotalsgst As Double = 0, dbltotalcgst As Double = 0, dbltotaligst As Double = 0, dblvalCGST As Double = 0, dblvalIGST As Double = 0, dbldisc As Double = 0, dblalltotal As Double = 0, dblTaxAmount As Double = 0
            'dblTaxAmount = Val(lblTaxAmount.Text)
            dbltotal = dblGroup1Amt            
            dbltotalcgst = Format(dbltotal * (dblSGST / 100), "0.00")
            dbltotalsgst = Format(dbltotal * (dblCGST / 100), "0.00")
            dbltotaligst = Format(dbltotal * (dblIGST / 100), "0.00")
            strSql = ""
            strSql += "select round(" & dbltotalcgst & ",2) as totalcgst,round(" & dbltotalsgst & ",2) as totalsgst,round(" & dbltotaligst & ",2) as totaligst"
            dt = db.sub_GetDatatable(strSql)
            dblalltotal = dbltotal - dbldisc + Val(dt.Rows(0)("totalsgst")) + Val(dt.Rows(0)("totalcgst")) + Val(dt.Rows(0)("totaligst"))
            lblTotal.Text = dbltotal
            lbldisc.Text = dbldisc
            lblSgstPer.Text = strSGSTPer
            lblCgstPer.Text = StrCGSTPEr
            lblIgstPer.Text = StrIGSTPer
            lblCGST.Text = Val(dt.Rows(0)("totalcgst"))
            lblSGST.Text = Val(dt.Rows(0)("totalsgst"))
            lblIGST.Text = Val(dt.Rows(0)("totaligst"))
            strSql = ""
            strSql += "select round(" & dblalltotal & ",0) as GrandTotal "
            dt1 = db.sub_GetDatatable(strSql)
            lblAllTotal.Text = Val(dt1.Rows(0)("GrandTotal"))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkadd_Click(sender As Object, e As EventArgs) Handles lnkadd.Click
        Try
            Dim dblAmount As String
            dblAmount = Val(lblTaxAmount.Text)
            If ddlInvoiceType.SelectedValue = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select Invoice type first');", True)
                ddlInvoiceType.Focus()
                Exit Sub
            End If
            If ddlInvoiceType.SelectedValue = "Transport" Then
                If ddlLocation.SelectedValue = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please select Location for Transport invoice');", True)
                    ddlLocation.Focus()
                    Exit Sub
                End If
            End If
            If Not Trim(txtcontainerno.Text) = "" Then
                If Len(Trim(txtcontainerno.Text)) <> 11 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter valid container no');", True)
                    lnkadd.Attributes.Add("Class", "btn btn-info")
                    txtcontainerno.Focus()
                    Exit Sub
                End If
                'strSql = ""
                'strSql += "select * from eyard_stock where containerno='" & Trim(txtcontainerno.Text & "") & "'"
                'dt = db.sub_GetDatatable(strSql)
                'If Not dt.Rows.Count > 0 Then
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No not found.');", True)
                '    lnkadd.Attributes.Add("Class", "btn btn-info")
                '    txtcontainerno.Text = ""
                '    txtcontainerno.Focus()
                '    Exit Sub
                'End If
            End If            
            strSql = ""
            strSql += "select * from Temp_Other_Assessment_Depo where IsCancel=0 and AccountID='" & Trim(ddlaccntheads.SelectedValue) & "' and ContainerNo='" & Trim(txtcontainerno.Text) & "' and UserID='" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('These charges are already applied.');", True)
                lnkadd.Attributes.Add("Class", "btn btn-info")
                ddlaccntheads.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql += "USP_insert_into_temp_other_assessment_Depo '" & Trim(ddlaccntheads.SelectedValue) & "','" & Trim(txtamount.Text & "") & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtcontainerno.Text) & "','" & Trim(txtSize.Text & "") & "'"
            db.sub_ExecuteNonQuery(strSql)
            dblAmount += Trim(txtamount.Text)
            lblTaxAmount.Text = dblAmount
            Call Sub_SGTRate()
            FillGrid()
            ddlaccntheads.SelectedValue = 0
            txtamount.Text = ""
            txtcontainerno.Text = ""
            lnkadd.Attributes.Add("Class", "btn btn-info")
            ddlaccntheads.Focus()
        Catch ex As Exception
            lnkadd.Attributes.Add("Class", "btn btn-info")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try
            Dim lnkcancel As LinkButton = DirectCast(sender, LinkButton)
            Dim row As GridViewRow = DirectCast(lnkcancel.Parent.Parent, GridViewRow)
            Dim AccountId As String = lnkcancel.CommandArgument
            strSql = ""
            strSql += "Update Temp_Other_Assessment_Depo set IsCancel=1 where AutoID='" & AccountId & "'"
            db.sub_ExecuteNonQuery(strSql)
            Sub_SGTRate()
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            txtgstin.Text = ""
            txtgstname.Text = ""
            ddlcustomer.SelectedValue = 0
            ddlCHA.SelectedValue = 0
            ddlimporter.SelectedValue = 0
            txtbeno.Text = ""
            txtbondno.Text = ""
            ddlaccntheads.SelectedValue = 0
            txtamount.Text = ""
            txtremarks.Text = ""
            txtStorageFrom.Text = ""
            txtStorageUpto.Text = ""
            ddlTax.SelectedValue = 0
            ddlLocation.SelectedValue = 0
            ddlInvoiceType.SelectedValue = 0
            txtcontainerno.Text = ""
            txtvaliddate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            'txtinvdate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-dd")
            db.sub_ExecuteNonQuery("Delete from Temp_Other_Assessment_Depo Where UserID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_GST_Search_Depo Where UserID=" & Session("UserId_DepoCFS") & "")
            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnoksave_ServerClick(sender As Object, e As EventArgs)
        Try
            lblquoteApprove.Text = "Do you wish to print Invoice?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel5.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub txtcontainerno_TextChanged(sender As Object, e As EventArgs)
        Try
            Clear()
            strSql = ""
            strSql += "USP_NOC_TEXT_CHANGED_OTHER_ASSESSMENT '" & Trim(txtcontainerno.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddlcustomer.SelectedValue = Trim(dt.Rows(0)("CustID") & "")
                ddlCHA.SelectedValue = Trim(dt.Rows(0)("CHAID") & "")
                ddlimporter.SelectedValue = Trim(dt.Rows(0)("ImporterId") & "")
                txtbeno.Text = Trim(dt.Rows(0)("BOENo") & "")
                txtbondno.Text = Trim(dt.Rows(0)("BondNo") & "")
                ddlwarehouse.SelectedValue = Trim(dt.Rows(0)("wr_code") & "")
                txtStorageFrom.Text = Convert.ToDateTime(Trim(dt.Rows(0)("NOCDate"))).ToString("yyyy-MM-dd")
                txtInsFrom.Text = Convert.ToDateTime(Trim(dt.Rows(0)("InsuDate"))).ToString("yyyy-MM-dd")

                strSql = ""
                strSql += "select top 1 TariffID from bond_tariffmaster where custid=" & Val(ddlcustomer.SelectedValue) & " order by AddedOn desc"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    lblTariffId.Text = Trim(dt.Rows(0)("TariffID") & "")
                End If
                strSql = ""
                strSql += "USP_LOCATION_CUSTOMER_WISE '" & lblTariffId.Text & "','Bond'"
                dt = db.sub_GetDatatable(strSql)
                ddlLocation.DataSource = dt
                ddlLocation.DataTextField = "Location"
                ddlLocation.DataValueField = "LocationID"
                ddlLocation.DataBind()
                ddlLocation.Items.Insert(0, New ListItem("--Select--", 0))
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('NOC No not found');", True)
                txtcontainerno.Text = ""
                txtcontainerno.Focus()
                Exit Sub
            End If
            txtgstin.Focus()
            UpdatePanel1.Update()
            UpdatePanel4.Update()
            'UpdatePanel6.Update()
            'UpdatePanel7.Update()
            UpdatePanel2.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnIndentlist_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_Other_Search_NOC where USERID=" & Session("UserId_DepoCFS") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtcontainerno.Text = Trim(dt.Rows(0)("NOCNO") & "")
                txtcontainerno_TextChanged(sender, e)
            End If
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub ddlaccntheads_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim dblAmount As String
            dblAmount = Val(lblTaxAmount.Text)
            
            'strSql = ""
            'strSql += "USP_FETCH_OTHER_ASSESSMENT_AMOUNT '" & Trim(ddlInvoiceType.SelectedValue & "") & "','" & Val(ddlaccntheads.SelectedValue & "") & "','" & Val(ddlLocation.SelectedValue & "") & "','" & Trim(lblTariffId.Text & "") & "'"
            'dt = db.sub_GetDatatable(strSql)
            'If dt.Rows.Count > 0 Then
            '    txtamount.Text = Val(dt.Rows(0)("fixedamt"))
            '    If dt.Rows(0)("isSTax") = True Then
            '        dblAmount += Val(dt.Rows(0)("fixedamt"))
            '        lblIsTax.Text = 1
            '    Else
            '        lblIsTax.Text = 0
            '    End If
            'End If
            'lblTaxAmount.Text = dblAmount
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkDownloadExcel_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select  '' [Container No],'' [Size],'' Amount,'' as [Account Name]"
            dt = db.sub_GetDatatable(strSql)

            If (dt.Rows.Count > 0) Then
                Using wb As New XLWorkbook()
                    wb.Worksheets.Add(dt, "Other Invoice")

                    With wb.Worksheets(0)
                        .Column(1).Style.DateFormat.Format = "yyyy-MM-dd"
                    End With
                  
                    wb.Worksheets.Add(dt, "Other Invoice1" & Convert.ToDateTime(Now).ToString("dd-MM-yyyy") & "")
                   
                    Response.Clear()
                    Response.Buffer = True
                    Response.Charset = ""
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment;filename=OtherInvoice.xlsx")
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
            Dim dblAmount As String = ""
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

                                strSql = ""
                                strSql += " Select AccountID,AccountName from eyard_accountmaster where IsActive=1  and   AccountName='" & row.Cell(4).Value.ToString() & "'"
                                dt1 = db.sub_GetDatatable(strSql)

                                If dt1.Rows.Count > 0 Then

                                Else
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Name is InVailid  " + row.Cell(4).Value.ToString() + "');", True)
                                    Exit Sub
                                End If



                                strSql = ""
                                strSql += "USP_insert_into_temp_other_assessment_Depo '" & Trim(dt1.Rows(0)("AccountID") & "") & "','" & row.Cell(3).Value.ToString() & "','" & Session("UserId_DepoCFS") & "','" & row.Cell(1).Value.ToString() & "','" & row.Cell(2).Value.ToString() & "'"
                                db.sub_ExecuteNonQuery(strSql)
                                dblAmount += row.Cell(3).Value.ToString()
                                lblTaxAmount.Text = dblAmount
                                Call Sub_SGTRate()

                            Else
                                firstRow = False
                            End If
                        End If
lblnext:
                    Next
                End Using
                File.Delete(FilePath)
            End If

            FillGrid()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlCommodity_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = " select * from Commodity_Group_M where Commodity_Group_ID=" & Val(ddlCommodity.SelectedValue) & " "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddlTax.SelectedValue = Val(dt.Rows(0)("settingsID") & "")
                lblTaxID.Text = Val(dt.Rows(0)("TaxGroupID") & "")
            Else
                ddlTax.SelectedValue = Val(dt.Rows(0)("settingsID") & "")
                lblTaxID.Text = Val(dt.Rows(0)("TaxGroupID") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

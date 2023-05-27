Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11, dt12, dt13, dt14 As DataTable
    Dim db As New dbOperation_Depo
    Dim strSGSTPer As String = "", StrCGSTPEr As String = "", StrIGSTPer As String = ""
    Dim dblSGST As Double = 0, dblCGST As Double = 0, dblIGST As Double = 0
    Dim dbltaxgroupid As Integer = 0
    Dim dblNetAmount As Double
    Dim strContNumbers As String
    Dim dblNetAmount_IND As Double
    Dim dblSTaxOnAmount As Double
    Dim strAccountID As String = ""
    Dim intGrossWeight As Double
    Dim blnSTax As Boolean
    Dim dblGroup1Amt As Double
    Dim dblGroup2Amt As Double
    Dim dblDiscAmount As Double
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
            db.sub_ExecuteNonQuery("Delete From temp_bill_emptyD Where UserID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_GST_Search_Depo Where UserID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from TEMP_INVOICE_CONTAINERS Where USER_ID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Vessel_Repo_Invoice Where UserID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Proforma_Search Where UserID=" & Session("UserId_DepoCFS") & "")

            txtInvoiceDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtFromDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT00:00")
            txtBilledToDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddT23:59")

            Filldropdown()
            btnShow_Click(sender, e)
            GRID()
        End If

    End Sub
    Protected Sub GRID()
        Try
            strSql = ""
            strSql = ""
            dt = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt
            grdcontainer.DataBind()
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub grid1()
        strSql = ""
        strSql += "USP_FILL_TEMP_BILL_D " & Session("UserId_DepoCFS") & ""
        dt = db.sub_GetDatatable(strSql)
        If dt.Rows.Count > 0 Then
            lblchargescount.Visible = True
            LBLNO.Text = dt.Rows.Count
            LBLNO.Visible = True
            divtblWOTOtal.Attributes.Add("style", "display:block")
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
        Else
            lblchargescount.Visible = False
            LBLNO.Text = dt.Rows.Count
            LBLNO.Visible = False
            divtblWOTOtal.Attributes.Add("style", "display:none")
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No charges found!');", True)
        End If
        UpdatePanel1.Update()
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql = "USP_INVOICE_FILL_STORAGE_BILL"
            ds = db.sub_GetDataSets(strSql)
            ddlinvoicetype.DataSource = ds.Tables(0)
            ddlinvoicetype.DataTextField = "InvoiceType"
            ddlinvoicetype.DataValueField = "ID"
            ddlinvoicetype.DataBind()
            ddlinvoicetype.Items.Insert(0, New ListItem("--Select--", 0))

            ddlshipline.DataSource = ds.Tables(1)
            ddlshipline.DataTextField = "SLName"
            ddlshipline.DataValueField = "SLID"
            ddlshipline.DataBind()
            ddlshipline.Items.Insert(0, New ListItem("--Select--", 0))

            ddltariff.DataSource = ds.Tables(2)
            ddltariff.DataTextField = "tariffID"
            ddltariff.DataValueField = "entryID"
            ddltariff.DataBind()
            ddltariff.Items.Insert(0, New ListItem("--Select--", 0))

            ddlTaxID.DataSource = ds.Tables(3)
            ddlTaxID.DataTextField = "TAX"
            ddlTaxID.DataValueField = "SETTINGSID"
            ddlTaxID.DataBind()
            ddlTaxID.Items.Insert(0, New ListItem("", 0))
            ddlTaxID.SelectedValue = 10

            ddlEXTCriteria.DataSource = ds.Tables(4)
            ddlEXTCriteria.DataTextField = "EXT_Type"
            ddlEXTCriteria.DataValueField = "EXT_Type"
            ddlEXTCriteria.DataBind()

            strSql = ""
            strSql = "USP_VESSELS_LINE_WISE " & Val(ddlshipline.SelectedValue) & ""
            dt = db.sub_GetDatatable(strSql)
            ddlVessels.DataSource = dt
            ddlVessels.DataTextField = "VesselName"
            ddlVessels.DataValueField = "VesselID"
            ddlVessels.DataBind()
            ddlVessels.Items.Insert(0, New ListItem("All", 0))
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
                txtGstINNumber.Text = Trim(dt.Rows(0)("GSTNo") & "")
                txtgstname.Text = Trim(dt.Rows(0)("GSTName") & "")
                lblstatecode.Text = Val(dt.Rows(0)("Statecode"))
                lblpartyid.Text = Trim(dt.Rows(0)("Gstid") & "")
                'ddltariff.SelectedValue = Trim(dt.Rows(0)("TariffID") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnShow_Click(sender As Object, e As EventArgs)
        Try
            ClearAmount()
            Dim dbl20 As Double, dbl40 As Double, dbl45 As Double, strContainer As String
            db.sub_ExecuteNonQuery("Delete from TEMP_INVOICE_CONTAINERS Where USER_ID=" & Session("UserId_DepoCFS") & "")

            If ddlinvoicetype.SelectedItem.Text = "Storage" Then
                strSql = ""
                strSql = "GETContainerDetails_Storage '" & Trim(ddlshipline.SelectedValue & "") & "','" & Trim(txtcontainer.Text & "") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyyMMddHHmm") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Session("UserId_DepoCFS") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                grdcontainer.DataSource = dt1
                grdcontainer.DataBind()
                UpdatePanel1.Update()
                For i = 0 To dt1.Rows.Count - 1
                    strContainer = Trim(dt1.Rows(i)("Size"))
                    If InStr(strContainer, "20") > 0 Then
                        dbl20 += 1
                    ElseIf InStr(strContainer, "40") > 0 Then
                        dbl40 += 1
                    ElseIf InStr(strContainer, "45") > 0 Then
                        dbl45 += 1
                    End If
                Next
                lblA.Text = dbl20
                lblB.Text = dbl40
                lblC.Text = dbl45
                lblTeus.Text = dbl20 + Val(dbl40) * 2 + Val(dbl45) * 2
            End If
            If ddlinvoicetype.SelectedItem.Text = "LIFT ON HEAD" Then
                strSql = ""
                strSql = "SP_GetInContainer_LiftOnHaed'" & Trim(ddlshipline.SelectedValue & "") & "','" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtcontainer.Text & "") & "','" & Session("UserId_DepoCFS") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                grdcontainer.DataSource = dt1
                grdcontainer.DataBind()
                UpdatePanel1.Update()
                For i = 0 To dt1.Rows.Count - 1
                    strContainer = Trim(dt1.Rows(i)("Size"))
                    If InStr(strContainer, "20") > 0 Then
                        dbl20 += 1
                    ElseIf InStr(strContainer, "40") > 0 Then
                        dbl40 += 1
                    ElseIf InStr(strContainer, "45") > 0 Then
                        dbl45 += 1
                    End If
                Next
                lblA.Text = dbl20
                lblB.Text = dbl40
                lblC.Text = dbl45
                lblTeus.Text = dbl20 + Val(dbl40) * 2 + Val(dbl45) * 2
            End If
            If ddlinvoicetype.SelectedItem.Text = "LIFT OFF HEAD" Then
                strSql = ""
                strSql = " SP_GetLiftOnLiftHaed'" & Trim(ddlshipline.SelectedValue & "") & "','" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyy-MM-dd HH:mm:ss") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd HH:mm:ss") & "','" & Trim(txtcontainer.Text & "") & "','" & Session("UserId_DepoCFS") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                grdcontainer.DataSource = dt1
                grdcontainer.DataBind()
                UpdatePanel1.Update()
                For i = 0 To dt1.Rows.Count - 1
                    strContainer = Trim(dt1.Rows(i)("Size"))
                    If InStr(strContainer, "20") > 0 Then
                        dbl20 += 1
                    ElseIf InStr(strContainer, "40") > 0 Then
                        dbl40 += 1
                    ElseIf InStr(strContainer, "45") > 0 Then
                        dbl45 += 1
                    End If
                Next
                lblA.Text = dbl20
                lblB.Text = dbl40
                lblC.Text = dbl45
                lblTeus.Text = dbl20 + Val(dbl40) * 2 + Val(dbl45) * 2
            End If
            If ddlinvoicetype.SelectedItem.Text = "Empty Repo" Then
                strSql = ""
                strSql = "SP_GetOutContainerDetails_EmptyRepo'" & Trim(ddlshipline.SelectedValue & "") & "','" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtcontainer.Text & "") & "','" & Session("UserId_DepoCFS") & "','" & Trim(txtVesselID.Text & "") & "','" & Trim(ddlEXTCriteria.SelectedItem.Text & "") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                grdcontainer.DataSource = dt1
                grdcontainer.DataBind()
                UpdatePanel1.Update()
                For i = 0 To dt1.Rows.Count - 1
                    strContainer = Trim(dt1.Rows(i)("Size"))
                    If InStr(strContainer, "20") > 0 Then
                        dbl20 += 1
                    ElseIf InStr(strContainer, "40") > 0 Then
                        dbl40 += 1
                    ElseIf InStr(strContainer, "45") > 0 Then
                        dbl45 += 1
                    End If
                Next
                lblA.Text = dbl20
                lblB.Text = dbl40
                lblC.Text = dbl45
                lblTeus.Text = dbl20 + Val(dbl40) * 2 + Val(dbl45) * 2
            End If
            If ddlinvoicetype.SelectedItem.Text = "Lift On/Lift Off" Then
                strSql = ""
                strSql = " SP_GetLiftOnLiftOff_LOLO'" & Trim(ddlshipline.SelectedValue & "") & "','" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyy-MM-dd HH:mm:ss") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd HH:mm:ss") & "','" & Trim(txtcontainer.Text & "") & "','" & Session("UserId_DepoCFS") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd HH:mm:ss") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                grdcontainer.DataSource = dt1
                grdcontainer.DataBind()
                UpdatePanel1.Update()
                For i = 0 To dt1.Rows.Count - 1
                    strContainer = Trim(dt1.Rows(i)("Size"))
                    If InStr(strContainer, "20") > 0 Then
                        dbl20 += 1
                    ElseIf InStr(strContainer, "40") > 0 Then
                        dbl40 += 1
                    ElseIf InStr(strContainer, "45") > 0 Then
                        dbl45 += 1
                    End If
                Next
                lblA.Text = dbl20
                lblB.Text = dbl40
                lblC.Text = dbl45
                lblTeus.Text = dbl20 + Val(dbl40) * 2 + Val(dbl45) * 2
            End If
            If ddlinvoicetype.SelectedItem.Text = "Empty Container Repair" Then
                strSql = ""
                strSql = "GETContainerDetails_MNR '" & Trim(ddlshipline.SelectedValue & "") & "','" & Trim(txtcontainer.Text & "") & "',"
                If ddlBasedOnDate.SelectedValue = "Out Date" Then
                    strSql += "'" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyyMMddHHmm") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyyMMddHHmm") & "',"
                Else
                    strSql += "'" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                End If
                strSql += "'" & Trim(ddlBasedOnDate.SelectedValue) & "','" & Session("UserId_DepoCFS") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                grdcontainer.DataSource = dt1
                grdcontainer.DataBind()
                UpdatePanel1.Update()
                For i = 0 To dt1.Rows.Count - 1
                    strContainer = Trim(dt1.Rows(i)("Size"))
                    If InStr(strContainer, "20") > 0 Then
                        dbl20 += 1
                    ElseIf InStr(strContainer, "40") > 0 Then
                        dbl40 += 1
                    ElseIf InStr(strContainer, "45") > 0 Then
                        dbl45 += 1
                    End If
                Next
                lblA.Text = dbl20
                lblB.Text = dbl40
                lblC.Text = dbl45
                lblTeus.Text = dbl20 + Val(dbl40) * 2 + Val(dbl45) * 2
            End If
            If ddlinvoicetype.SelectedItem.Text = "Bundling" Then
                strSql = ""
                strSql = "SP_GetOutContainerDetails_Bundling'" & Trim(ddlshipline.SelectedValue & "") & "','" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtcontainer.Text & "") & "','" & Session("UserId_DepoCFS") & "','" & Trim(ddlVessels.SelectedItem.Text & "") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                grdcontainer.DataSource = dt1
                grdcontainer.DataBind()
                UpdatePanel1.Update()
                For i = 0 To dt1.Rows.Count - 1
                    strContainer = Trim(dt1.Rows(i)("Size"))
                    If InStr(strContainer, "20") > 0 Then
                        dbl20 += 1
                    ElseIf InStr(strContainer, "40") > 0 Then
                        dbl40 += 1
                    ElseIf InStr(strContainer, "45") > 0 Then
                        dbl45 += 1
                    End If
                Next
                lblA.Text = dbl20
                lblB.Text = dbl40
                lblC.Text = dbl45
                lblTeus.Text = dbl20 + Val(dbl40) * 2 + Val(dbl45) * 2
            End If
            If ddlinvoicetype.SelectedItem.Text = "Washing" Then
                strSql = ""
                strSql = "GETContainerDetails_MNR_Washing '" & Trim(ddlshipline.SelectedValue & "") & "','" & Trim(txtcontainer.Text & "") & "',"
                If ddlBasedOnDate.SelectedValue = "Out Date" Then
                    strSql += "'" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyyMMddHHmm") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyyMMddHHmm") & "',"
                Else
                    strSql += "'" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                End If
                strSql += "'" & Trim(ddlBasedOnDate.SelectedValue) & "','" & Session("UserId_DepoCFS") & "'"
                dt1 = db.sub_GetDatatable(strSql)
                grdcontainer.DataSource = dt1
                grdcontainer.DataBind()
                UpdatePanel1.Update()
                For i = 0 To dt1.Rows.Count - 1
                    strContainer = Trim(dt1.Rows(i)("Size"))
                    If InStr(strContainer, "20") > 0 Then
                        dbl20 += 1
                    ElseIf InStr(strContainer, "40") > 0 Then
                        dbl40 += 1
                    ElseIf InStr(strContainer, "45") > 0 Then
                        dbl45 += 1
                    End If
                Next
                lblA.Text = dbl20
                lblB.Text = dbl40
                lblC.Text = dbl45
                lblTeus.Text = dbl20 + Val(dbl40) * 2 + Val(dbl45) * 2
            End If
            btnShow.Text = "Show"
            btnShow.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
            If (ddlinvoicetype.SelectedValue = 6 Or ddlinvoicetype.SelectedValue = 6) Then
                divBasedOnDate.Attributes.Add("style", "display:block")
                divFromDate.Attributes.Add("style", "display:block")
            ElseIf ddlinvoicetype.SelectedValue = 4 Then
                divFromDate.Attributes.Add("style", "display:none")
                divBasedOnDate.Attributes.Add("style", "display:none")
            Else
                divFromDate.Attributes.Add("style", "display:block")
                divBasedOnDate.Attributes.Add("style", "display:none")
            End If
            chkright_CheckedChanged(sender, e)
        Catch ex As Exception
            btnShow.Text = "Show"
            btnShow.Attributes.Add("Class", "btn btn-primary btn btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs)
        Dim blnContainerFound As Boolean = False
        Try
            ClearAmount()
            For Each row As GridViewRow In grdcontainer.Rows
                Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                If chkright.Checked = True Then
                    blnContainerFound = True
                    Exit For
                End If
            Next
            If blnContainerFound = False Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('No details selected for assessment.');", True)
                Exit Sub
            End If
            'db.sub_ExecuteNonQuery("Delete From temp_bill_emptyD Where UserID=" & Session("UserId_DepoCFS") & "")
            Call Sub_SGTRate()
            Dim strWorkyear As String = ""
            Dim InvoiceDate As Date = Trim(txtInvoiceDate.Text)
            If InvoiceDate.Month < 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") - 1 & "-" & Format(InvoiceDate, "yy")
            ElseIf InvoiceDate.Month >= 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") & "-" & Format(InvoiceDate, "yy") + 1
            End If
            'If Not (Trim(ddlinvoicetype.SelectedItem.Text & "") = "Empty Container Repair" Or Trim(ddlinvoicetype.SelectedItem.Text & "") = "Washing") Then
            '    For Each row As GridViewRow In grdcontainer.Rows
            '        Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
            '        If chkright.Checked = True Then
            '            strSql = ""
            '            strSql = "USP_CALCULATION_HENDING_STORAGE '" & Trim(ddltariff.SelectedItem.Text & "") & "','" & Convert.ToDateTime(Trim(txtInvoiceDate.Text & "")).ToString("yyyyMMdd") & "','" & Val(CType(row.FindControl("lblSize"), Label).Text) & "','" & Trim(ddlinvoicetype.SelectedItem.Text & "") & "','" & Trim(CType(row.FindControl("lblContainerType"), Label).Text) & "'"
            '            dt4 = db.sub_GetDatatable(strSql)
            '            If Not dt4.Rows.Count > 0 Then
            '                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Tariff Not Found');", True)
            '                Exit Sub
            '            End If
            '        End If
            '    Next
            'End If
            'Dim dtDiscPer As New DataTable
            'strSql = ""
            'strSql += ""
            'dtDiscPer = db.sub_GetDatatable(strSql)
            For Each row As GridViewRow In grdcontainer.Rows
                Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                If chkright.Checked = True Then
                    If Not (Trim(ddlinvoicetype.SelectedItem.Text & "") = "Empty Container Repair" Or Trim(ddlinvoicetype.SelectedItem.Text & "") = "Washing") Then
                        strSql = ""
                        strSql = "USP_CALCULATION_STORAGE'" & Trim(ddltariff.SelectedItem.Text & "") & "','" & Convert.ToDateTime(Trim(txtInvoiceDate.Text & "")).ToString("yyyyMMdd") & "','" & Val(CType(row.FindControl("lblSize"), Label).Text) & "','" & Trim(ddlinvoicetype.SelectedItem.Text & "") & "','" & Trim(CType(row.FindControl("lblFromID"), Label).Text) & "','" & Trim(CType(row.FindControl("lblToID"), Label).Text) & "'"
                        dt5 = db.sub_GetDatatable(strSql)
                        'If Not dt5.Rows.Count > 0 Then
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container No " + Trim(CType(row.FindControl("lblContainerN"), Label).Text) + " And To Size " + Trim(CType(row.FindControl("lblSize"), Label).Text) + " And From " + Trim(CType(row.FindControl("lblFromLocation"), Label).Text) + " And To " + Trim(CType(row.FindControl("lblToLocation"), Label).Text) + " tariff Not Added.');", True)
                        '    Exit Sub
                        'End If
                        If dt5.Rows.Count > 0 Then
                            For j As Integer = 0 To dt5.Rows.Count - 1
                                dblNetAmount_IND = 0
                                strSql = ""
                                strSql = " SELECT DISTINCT AccountName FROM Eyard_accountmaster WHERE AccountID=" & Val(dt5.Rows(j)("AccountID")) & " and IsActive=1"
                                dt6 = db.sub_GetDatatable(strSql)
                                If dt6.Rows.Count > 0 Then

                                    Call sub_fetchcharges_IND(Trim(CType(row.FindControl("lblContainerN"), Label).Text), Trim(CType(row.FindControl("lblContainerType"), Label).Text), Val(CType(row.FindControl("lblSize"), Label).Text), Val(dt5.Rows(j)("AccountID")), 0, 0, 0, Trim(dt5.Rows(j)("deliverytype")), 0, Val(CType(row.FindControl("lblEntryID"), Label).Text), Val(CType(row.FindControl("lblTotalDays"), Label).Text), Trim(CType(row.FindControl("lblOutDate"), Label).Text), Trim(CType(row.FindControl("lblInDate"), Label).Text), Trim(CType(row.FindControl("lblFromID"), Label).Text), Trim(CType(row.FindControl("lblToID"), Label).Text))
                                    If Val(dblNetAmount_IND) > 0 Then
                                        Dim dblnetamountnew As Double = 0

                                        dblnetamountnew = dblNetAmount_IND
                                        'If Trim(ddlinvoicetype.SelectedItem.Text & "") = "Lift On/Lift Off" Then
                                        '    If Trim(CType(row.FindControl("lblOutDate"), Label).Text) <> "" Then
                                        '        '  dblGroup1Amt = dblGroup1Amt * 2
                                        '        ' dblnetamountnew = Val(dblnetamountnew) * 2
                                        '        ' dblGroup1Amt = dblGroup1Amt + dblnetamountnew
                                        '        'dblNetAmount_IND = dblNetAmount_IND * 2
                                        '    End If
                                        'Else
                                        dblGroup1Amt = dblGroup1Amt + dblNetAmount_IND
                                        'End If
                                        strSql = ""
                                        strSql += "USP_Insert_temp_bill_emptyD '0' ,'" & strWorkyear & "','" & Val(CType(row.FindControl("lblEntryID"), Label).Text) & "' , "
                                        strSql += " '" & Trim(CType(row.FindControl("lblContainerN"), Label).Text) & "', '" & Val(CType(row.FindControl("lblSize"), Label).Text) & "', '','" & Trim(CType(row.FindControl("lblVehicleNo"), Label).Text) & "','','', "
                                        strSql += " '','','','','','','" & Val(dt5.Rows(j)("AccountID")) & "', "
                                        strSql += "'" & dblnetamountnew & "','" & Format(dblnetamountnew * (dblSGST / 100), "0.00") & "','" & Format(dblnetamountnew * (dblCGST / 100), "0.00") & "', "
                                        strSql += " '" & Format(dblnetamountnew * (dblIGST / 100), "0.00") & "', "
                                        strSql += " '" & Session("UserId_DepoCFS") & "','" & Val(dt5.Rows(j)("TaxID") & "") & "','" & Trim(CType(row.FindControl("lblContainerType"), Label).Text) & "'"
                                        db.sub_ExecuteNonQuery(strSql)
                                    End If
                                End If
                            Next
                        End If
                    Else
                        Call sub_fetchcharges_IND(Trim(CType(row.FindControl("lblContainerN"), Label).Text), Trim(CType(row.FindControl("lblContainerType"), Label).Text), Val(CType(row.FindControl("lblSize"), Label).Text), 5, 0, 0, 0, Trim(ddlinvoicetype.SelectedItem.Text), 0, Val(CType(row.FindControl("lblEntryID"), Label).Text), Val(CType(row.FindControl("lblTotalDays"), Label).Text), Trim(CType(row.FindControl("lblOutDate"), Label).Text), Trim(CType(row.FindControl("lblInDate"), Label).Text), Trim(CType(row.FindControl("lblFromID"), Label).Text), Trim(CType(row.FindControl("lblToID"), Label).Text))
                        If Val(dblNetAmount_IND) > 0 Then
                            dblGroup1Amt = dblGroup1Amt + dblNetAmount_IND
                            strSql = ""
                            strSql += "USP_Insert_temp_bill_emptyD '0' ,'" & strWorkyear & "','" & Val(CType(row.FindControl("lblEntryID"), Label).Text) & "' , "
                            strSql += " '" & Trim(CType(row.FindControl("lblContainerN"), Label).Text) & "', '" & Val(CType(row.FindControl("lblSize"), Label).Text) & "', '','" & Trim(CType(row.FindControl("lblVehicleNo"), Label).Text) & "','','', "
                            strSql += " '','','','','','','5', "
                            strSql += "'" & dblNetAmount_IND & "','" & Format(dblNetAmount_IND * (dblSGST / 100), "0.00") & "','" & Format(dblNetAmount_IND * (dblCGST / 100), "0.00") & "', "
                            strSql += " '" & Format(dblNetAmount_IND * (dblIGST / 100), "0.00") & "', "
                            strSql += " '" & Session("UserId_DepoCFS") & "','10','" & Trim(CType(row.FindControl("lblContainerType"), Label).Text) & "'"
                            db.sub_ExecuteNonQuery(strSql)
                        End If
                        Call sub_fetchcharges_IND(Trim(CType(row.FindControl("lblContainerN"), Label).Text), Trim(CType(row.FindControl("lblContainerType"), Label).Text), Val(CType(row.FindControl("lblSize"), Label).Text), 6, 0, 0, 0, Trim(ddlinvoicetype.SelectedItem.Text), 0, Val(CType(row.FindControl("lblEntryID"), Label).Text), Val(CType(row.FindControl("lblTotalDays"), Label).Text), Trim(CType(row.FindControl("lblOutDate"), Label).Text), Trim(CType(row.FindControl("lblInDate"), Label).Text), Trim(CType(row.FindControl("lblFromID"), Label).Text), Trim(CType(row.FindControl("lblToID"), Label).Text))
                        If Val(dblNetAmount_IND) > 0 Then
                            dblGroup1Amt = dblGroup1Amt + dblNetAmount_IND
                            strSql = ""
                            strSql += "USP_Insert_temp_bill_emptyD '0' ,'" & strWorkyear & "','" & Val(CType(row.FindControl("lblEntryID"), Label).Text) & "' , "
                            strSql += " '" & Trim(CType(row.FindControl("lblContainerN"), Label).Text) & "', '" & Val(CType(row.FindControl("lblSize"), Label).Text) & "', '','" & Trim(CType(row.FindControl("lblVehicleNo"), Label).Text) & "','','', "
                            strSql += " '','','','','','','6', "
                            strSql += "'" & dblNetAmount_IND & "','" & Format(dblNetAmount_IND * (dblSGST / 100), "0.00") & "','" & Format(dblNetAmount_IND * (dblCGST / 100), "0.00") & "', "
                            strSql += " '" & Format(dblNetAmount_IND * (dblIGST / 100), "0.00") & "', "
                            strSql += " '" & Session("UserId_DepoCFS") & "','10','" & Trim(CType(row.FindControl("lblContainerType"), Label).Text) & "'"
                            db.sub_ExecuteNonQuery(strSql)
                        End If
                        Call sub_fetchcharges_IND(Trim(CType(row.FindControl("lblContainerN"), Label).Text), Trim(CType(row.FindControl("lblContainerType"), Label).Text), Val(CType(row.FindControl("lblSize"), Label).Text), 7, 0, 0, 0, Trim(ddlinvoicetype.SelectedItem.Text), 0, Val(CType(row.FindControl("lblEntryID"), Label).Text), Val(CType(row.FindControl("lblTotalDays"), Label).Text), Trim(CType(row.FindControl("lblOutDate"), Label).Text), Trim(CType(row.FindControl("lblInDate"), Label).Text), Trim(CType(row.FindControl("lblFromID"), Label).Text), Trim(CType(row.FindControl("lblToID"), Label).Text))
                        If Val(dblNetAmount_IND) > 0 Then
                            dblGroup1Amt = dblGroup1Amt + dblNetAmount_IND
                            strSql = ""
                            strSql += "USP_Insert_temp_bill_emptyD '0' ,'" & strWorkyear & "','" & Val(CType(row.FindControl("lblEntryID"), Label).Text) & "' , "
                            strSql += " '" & Trim(CType(row.FindControl("lblContainerN"), Label).Text) & "', '" & Val(CType(row.FindControl("lblSize"), Label).Text) & "', '','" & Trim(CType(row.FindControl("lblVehicleNo"), Label).Text) & "','','', "
                            strSql += " '','','','','','','7', "
                            strSql += "'" & dblNetAmount_IND & "','" & Format(dblNetAmount_IND * (dblSGST / 100), "0.00") & "','" & Format(dblNetAmount_IND * (dblCGST / 100), "0.00") & "', "
                            strSql += " '" & Format(dblNetAmount_IND * (dblIGST / 100), "0.00") & "', "
                            strSql += " '" & Session("UserId_DepoCFS") & "','10','" & Trim(CType(row.FindControl("lblContainerType"), Label).Text) & "'"
                            db.sub_ExecuteNonQuery(strSql)
                        End If
                    End If
                End If
            Next
            Dim blnAccountFound As Boolean = False
            For Each row As GridViewRow In grdcontainer.Rows
                Dim dtAccountFound As New DataTable
                Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                If chkright.Checked = True Then
                    strSql = ""
                    strSql = "  select accountid, SUM(amount) as amount,ContainerNo,IsStax   FROM eyard_wocharges WHERE ContainerNo='" & Trim(CType(row.FindControl("lblContainerN"), Label).Text & "") & "' AND  "
                    strSql += "  ReceiptNo=0 and IsCancel=0  GROUP BY accountID, Containerno,IsStax "
                    dt5 = db.sub_GetDatatable(strSql)
                    If dt5.Rows.Count > 0 Then
                        For j As Integer = 0 To dt5.Rows.Count - 1
                            strSql = ""
                            strSql += "select * from temp_bill_emptyD where accountid='" & Val(dt5.Rows(j)("accountid")) & "' and UserId='" & Session("UserId_DepoCFS") & "'and Containerno='" & Trim(CType(row.FindControl("lblContainerN"), Label).Text) & "'"
                            dtAccountFound = db.sub_GetDatatable(strSql)
                            If dtAccountFound.Rows.Count > 0 Then
                                blnAccountFound = True
                            End If
                            dblNetAmount_IND = 0
                            If blnAccountFound = False Then
                                If dt5.Rows(j)("amount") <> 0 Then
                                    If Val(dt5.Rows(j)("Amount")) > 0 Then
                                        dblNetAmount_IND = Val(dt5.Rows(j)("Amount"))
                                        strSql = ""
                                        strSql = " SELECT DISTINCT AccountName FROM Eyard_accountmaster WHERE AccountID=" & Val(dt5.Rows(j)("AccountID")) & " and IsActive=1"
                                        dt6 = db.sub_GetDatatable(strSql)
                                        If dt6.Rows.Count > 0 Then
                                            'Call sub_fetchcharges_IND(Val(CType(row.FindControl("lblContainerN"), Label).Text), Val(CType(row.FindControl("lblContainerType"), Label).Text), Val(CType(row.FindControl("lblSize"), Label).Text), Val(dt5.Rows(j)("AccountID")), 0, 0, 0, Trim(dt5.Rows(j)("deliverytype")), 0, Val(CType(row.FindControl("lblEntryID"), Label).Text), Val(CType(row.FindControl("lblTotalDays"), Label).Text), Val(CType(row.FindControl("lblOutDate"), Label).Text))
                                            If Val(dblNetAmount_IND) > 0 Then
                                                dblGroup1Amt = dblGroup1Amt + dblNetAmount_IND
                                                strSql = ""
                                                strSql += "USP_Insert_temp_bill_emptyD '0' ,'" & strWorkyear & "','" & Val(CType(row.FindControl("lblEntryID"), Label).Text) & "' , "
                                                strSql += " '" & Trim(CType(row.FindControl("lblContainerN"), Label).Text) & "', '" & Val(CType(row.FindControl("lblSize"), Label).Text) & "', '','" & Trim(CType(row.FindControl("lblVehicleNo"), Label).Text) & "','','', "
                                                strSql += " '','','','','','','" & Val(dt5.Rows(j)("AccountID")) & "', "
                                                strSql += "'" & dblNetAmount_IND & "','" & Format(dblNetAmount_IND * (dblSGST / 100), "0.00") & "','" & Format(dblNetAmount_IND * (dblCGST / 100), "0.00") & " ', "
                                                strSql += "'" & Format(dblNetAmount_IND * (dblIGST / 100), "0.00") & "', "
                                                strSql += "'" & Session("UserId_DepoCFS") & "','" & Val(ddlTaxID.SelectedValue) & "','" & Trim(CType(row.FindControl("lblContainerType"), Label).Text) & "'"
                                                db.sub_ExecuteNonQuery(strSql)
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            Next

            'strSql = ""
            'strSql = "USP_FILL_TEMP_BILL_D '" & Session("UserId_DepoCFS") & "'"
            'dt10 = db.sub_GetDatatable(strSql)
            'rptIndentLIst.DataSource = dt10
            'rptIndentLIst.DataBind()
            'strSql = ""
            'strSql = "get_sum_charges_BillEmpty_TMT '" & Trim(strWorkyear) & "','" & Session("UserId_DepoCFS") & " '  "
            'dt11 = db.sub_GetDatatable(strSql)
            'If dt11.Rows.Count > 0 Then
            '    lblSGST.Text = Val(dt11.Rows(0)("SGST"))
            '    lblCGST.Text = Val(dt11.Rows(0)("CGST"))
            '    lblIGST.Text = Val(dt11.Rows(0)("IGST"))
            '    dblNetAmount = Val(dt11.Rows(0)("Amount"))
            '    lblAllTotal.Text = Val(dt11.Rows(0)("Amount")) + Val(dt11.Rows(0)("SGST")) + Val(dt11.Rows(0)("CGST")) + Val(dt11.Rows(0)("IGST"))
            '    dblGroup1Amt = Val(lblAllTotal.Text)
            'End If
            Call grid1()
            Call sub_CalcTotals()
            btnCalculate.Text = "Calculate"
            btnCalculate.Attributes.Add("Class", "btn btn-success btn-sm outline")
        Catch ex As Exception
            btnCalculate.Text = "Calculate"
            btnCalculate.Attributes.Add("Class", "btn btn-success btn-sm outline")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Sub_SGTRate()
        Try
            Dim compid As String = ""
            strSql = ""
            strSql += "select Tinnumber from settings"
            dt9 = db.sub_GetDatatable(strSql)
            If dt9.Rows.Count > 0 Then
                compid = Trim(dt9.Rows(0)(0))
            End If
            strSql = ""
            strSql += "USP_GST_Cal_invoice " & Val(ddlTaxID.SelectedValue) & ""
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
    Protected Sub sub_fetchcharges_IND(ByVal ContainerNo As String, ByVal strContainerType As String, ByVal intContainerSize As Integer, ByVal strAccountID As String, ByVal dblWeight As Double, ByVal Strlocationfrom As String, ByVal Strlocationto As String, ByVal activity As String, ByVal ConDesc As String, ByVal movementno As String, ByVal TotalDays As String, strOutDate As String, strInDate As String, ByVal FromID As Integer, ByVal ToID As Integer)
        Dim strAccountName As String = ""
        Dim dblPercentage As Double
        Dim dblSQM As Double
        Dim dblAmount As Double
        Dim dblPaidAmount As Double
        Try
            dblNetAmount = 0
            dblNetAmount_IND = 0
            Dim dsFetch As New DataSet()
            strSql = ""
            strSql += "USP_ACCOUNT_HENDLING_STORAGE'" & Trim(ddltariff.SelectedItem.Text & "") & "','" & Convert.ToDateTime(Trim(txtInvoiceDate.Text & "")).ToString("yyyyMMdd") & "','" & Val(intContainerSize) & "',"
            strSql += "'" & activity & "','" & strAccountID & "','" & Trim(ContainerNo & "") & "','" & movementno & "','" & strContainerType & "','" & Val(ddlshipline.SelectedValue) & "','" & FromID & "','" & ToID & "','" & Trim(ddlEXTCriteria.SelectedItem.Text) & "','" & Trim(ddlinvoicetype.SelectedValue & "") & "',"
            strSql += "'" & Convert.ToDateTime(Trim(txtFromDate.Text & "")).ToString("yyyy-MM-dd") & "','" & Convert.ToDateTime(Trim(txtBilledToDate.Text & "")).ToString("yyyy-MM-dd") & "'"
            dsFetch = db.sub_GetDataSets(strSql)

            If dsFetch.Tables(0).Rows.Count > 0 Then
                strAccountName = Trim(dsFetch.Tables(0).Rows(0)("AccountName"))
            Else
                strAccountName = ""
            End If
            If (Trim(ddlinvoicetype.SelectedItem.Text & "") = "Empty Container Repair" Or Trim(ddlinvoicetype.SelectedItem.Text & "") = "Washing") Then
                If dsFetch.Tables(3).Rows.Count > 0 Then
                    dblAmount = Val(dsFetch.Tables(3).Rows(0)("ApprovedAmt"))
                End If
                dblNetAmount = dblNetAmount + dblAmount
                dblPaidAmount = 0

                If dsFetch.Tables(2).Rows.Count > 0 Then
                    dblPaidAmount = dblPaidAmount + Val(dsFetch.Tables(2).Rows(0)("NetAmount"))
                End If
                dblNetAmount = dblNetAmount - dblPaidAmount
                dblNetAmount_IND = dblNetAmount
                If blnSTax = True Then
                    dblSTaxOnAmount = dblNetAmount
                End If
                Exit Sub
            End If
            If dsFetch.Tables(1).Rows.Count > 0 Then
                TotalDays = TotalDays - Val(dsFetch.Tables(1).Rows(0)("TotalFreeDays"))
            End If
            If TotalDays < 0 Then
                TotalDays = 0
            End If
            If dsFetch.Tables(1).Rows.Count > 0 Then
                If dsFetch.Tables(1).Rows(0)("IsSTax") = True Then
                    blnSTax = True
                Else
                    blnSTax = False
                End If
                If dsFetch.Tables(1).Rows(0)("SorF") = "F" Then
                    If strAccountID = 12 Then
                        If Not strInDate = "" Then
                            If Not Convert.ToDateTime(strInDate).ToString("yyyyMMddHHMM") < Convert.ToDateTime(txtFromDate.Text).ToString("yyyyMMddHHmm") Then
                                dblAmount = Val(dsFetch.Tables(1).Rows(0)("FixedAmt"))
                            End If
                        End If
                    ElseIf strAccountID = 13 Then
                        If Not strOutDate = "" Then
                            If Not Convert.ToDateTime(strOutDate).ToString("yyyyMMddHHMM") > Convert.ToDateTime(txtBilledToDate.Text).ToString("yyyyMMddHHmm") Then
                                dblAmount = Val(dsFetch.Tables(1).Rows(0)("FixedAmt"))
                            End If
                        End If
                    Else
                        dblAmount = Val(dsFetch.Tables(1).Rows(0)("FixedAmt"))
                    End If
                Else
                    dblAmount = slab_CalcAmount(dsFetch.Tables(1).Rows(0)("SlabID"), TotalDays, dblPercentage, dblWeight, Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm"), dsFetch.Tables(1).Rows(0)("TotalFreeDays"))
                End If

                'If strOutDate = "" And ddlinvoicetype.SelectedItem.Text = "Lift On/Lift Off" Then
                '    dblAmount = 0
                'End If
                If dsFetch.Tables(4).Rows.Count > 0 Then
                    dblAmount = dblAmount + Val(dsFetch.Tables(4).Rows(0)("Amount"))
                End If
                If dsFetch.Tables(1).Rows.Count > 0 And Not (Trim(ddlinvoicetype.SelectedItem.Text & "") = "Empty Container Repair" Or Trim(ddlinvoicetype.SelectedItem.Text & "") = "Washing") Then
                    dblDiscAmount += Val(Val(dsFetch.Tables(1).Rows(0)("DiscAmt")) / 100) * dblAmount
                    dblAmount = dblAmount - Val(Val(dsFetch.Tables(1).Rows(0)("DiscAmt")) / 100) * dblAmount
                End If

                dblNetAmount = dblNetAmount + dblAmount
                dblPaidAmount = 0

                If dsFetch.Tables(2).Rows.Count > 0 Then
                    dblPaidAmount = dblPaidAmount + Val(dsFetch.Tables(2).Rows(0)("NetAmount"))
                End If

                dblNetAmount = dblNetAmount - dblPaidAmount
                dblNetAmount_IND = dblNetAmount
                If blnSTax = True Then
                    dblSTaxOnAmount = dblNetAmount
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Function slab_CalcAmount(ByVal slabID As Integer, ByVal DaysValue As Double, ByVal percentage As Double, ByVal Weight As Double, ByVal indate As Date, ByVal Freedays As String)
        Dim dblSlabAmount As Double
        Dim dtValidDate As Date
        Dim intValidCounter As Integer = 0
        Dim dblAddSecs As Double = 0
        Dim dblActualHrs As Double = 0
        Dim dblHrs As Double = 0
        Dim dblLeftHrs As Double = 0
        Dim dblAmt As Double = 0
        Dim dblTotDays As Double = 0
        Dim dblWeekValue As Double = 0

        dblSlabAmount = 0
        Dim strSlab As String = ""
        Dim dtSlab As New DataTable
        strSql = ""
        strSql = "SELECT * FROM eyard_slabs WHERE SlabID=" & slabID & " ORDER By FromSlab"
        dtSlab = db.sub_GetDatatable(strSql)
        If dtSlab.Rows.Count > 0 Then
            If dtSlab.Rows(0)("slabON") = "Days" Then
                For i = 0 To dtSlab.Rows.Count - 1
                    If Val(dtSlab.Rows(i)("FromSlab")) <= DaysValue Then
                        If Val(dtSlab.Rows(i)("ToSlab")) < DaysValue Then
                            dblSlabAmount = dblSlabAmount + (Val(dtSlab.Rows(i)("ToSlab")) - Val(dtSlab.Rows(i)("FromSlab")) + 1) * Val(dtSlab.Rows(i)("Value"))
                        Else
                            dblSlabAmount = dblSlabAmount + (Val(DaysValue) - Val(dtSlab.Rows(i)("FromSlab")) + 1) * Val(dtSlab.Rows(i)("Value"))
                        End If
                    End If
                Next
                slab_CalcAmount = slab_CalcAmount + dblSlabAmount
            ElseIf dtSlab.Rows(0)("slabON") = "Weeks" Then
                If DaysValue Mod 7 = 0 Then
                    dblWeekValue = DaysValue / 7
                Else
                    dblWeekValue = Int(DaysValue / 7) + 1
                End If
                For i = 0 To dtSlab.Rows.Count - 1
                    If Val(dtSlab.Rows(i)("FromSlab")) <= dblWeekValue Then
                        If Val(dtSlab.Rows(i)("ToSlab")) < dblWeekValue Then
                            dblSlabAmount = dblSlabAmount + Val(dtSlab.Rows(i)("ToSlab")) - Val(dtSlab.Rows(i)("FromSlab")) + 1 * Val(dtSlab.Rows(i)("Value"))
                        Else
                            dblSlabAmount = dblSlabAmount + Val(DaysValue) - Val(dtSlab.Rows(i)("FromSlab")) + 1 * Val(dtSlab.Rows(i)("Value"))
                        End If
                    End If
                Next
                slab_CalcAmount = slab_CalcAmount + dblSlabAmount
            ElseIf dtSlab.Rows(0)("slabON") = "Percentage" Then
                Dim strImpSlab As String = ""
                Dim dtImpSlab As New DataTable
                strImpSlab = "SELECT * FROM eyard_slabs WHERE SlabID=" & slabID & " and " & percentage & " BETWEEN FromSlab and ToSlab ORDER BY FromSlab"
                dtImpSlab = db.sub_GetDatatable(strImpSlab)
                If dtImpSlab.Rows.Count > 0 Then
                    dblSlabAmount = Val(dtImpSlab.Rows(0)("Value"))
                End If
                slab_CalcAmount = slab_CalcAmount + dblSlabAmount
            ElseIf dtSlab.Rows(0)("slabON") = "Weight" Then
                Dim strImpSlab As String = ""
                Dim dtImpSlab As New DataTable
                strImpSlab = "SELECT * FROM eyard_slabs WHERE SlabID=" & slabID & " and " & Weight & " BETWEEN FromSlab and ToSlab ORDER BY FromSlab"
                dtImpSlab = db.sub_GetDatatable(strImpSlab)
                If dtImpSlab.Rows.Count > 0 Then
                    dblSlabAmount = Val(dtImpSlab.Rows(0)("Value"))
                End If

                slab_CalcAmount = slab_CalcAmount + dblSlabAmount
            ElseIf dtSlab.Rows(0)("slabON") = "Weight" Then
                dtValidDate = Format(indate, "dd-MMM-yyyy HH:mm")

                '359 hard coded for 6 hours slab
                '  dtpvalidupto.Value = Format(DateAdd("s", 359 * 60, Now), "dd-MMM-yyyy HH:mm")

                intValidCounter = 0
            End If
        End If
    End Function
    Private Sub sub_CalcTotals()
        Try
            Dim dbltotal As Double = 0, dblvalSGST As Double = 0, dbltotalsgst As Double = 0, dbltotalcgst As Double = 0, dbltotaligst As Double = 0, dblvalCGST As Double = 0, dblvalIGST As Double = 0, dbldisc As Double = 0, dblalltotal As Double = 0, dblnetamountnew As Double
            dbltotal = dblGroup1Amt
            dbldisc = Val(txtdiscount.Text)
            dbltotalcgst = Format((dblGroup1Amt - dbldisc) * (dblSGST / 100), "0.00")
            dbltotalsgst = Format((dblGroup1Amt - dbldisc) * (dblCGST / 100), "0.00")
            dbltotaligst = Format((dblGroup1Amt - dbldisc) * (dblIGST / 100), "0.00")
            strSql = ""
            strSql += "select CEILING(cast(" & dbltotalcgst & " as float)) as totalcgst,CEILING(cast(" & dbltotalsgst & "as float)) as totalsgst,CEILING(cast(" & dbltotaligst & " as float)) as totaligst"
            dt = db.sub_GetDatatable(strSql)
            dblalltotal = dbltotal - dbldisc + Val(dt.Rows(0)("totalsgst")) + Val(dt.Rows(0)("totalcgst")) + Val(dt.Rows(0)("totaligst"))
            lblTotal.Text = dbltotal

            lblSgstPer.Text = strSGSTPer
            lblCgstPer.Text = StrCGSTPEr
            lblIgstPer.Text = StrIGSTPer
            lblCGST.Text = Val(dt.Rows(0)("totalcgst"))
            lblSGST.Text = Val(dt.Rows(0)("totalsgst"))
            lblIGST.Text = Val(dt.Rows(0)("totaligst"))
            lblAllTotal.Text = dblalltotal
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            Dim intid As Integer = 0, AccountID As String = ""
            Dim strinvoiceNo As String = ""
            If Val(lblAllTotal.Text) = 0 Or Trim(lblAllTotal.Text) = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Grand total cannot be zero. Cannot proceed');", True)
                Exit Sub
            End If
            Dim strWorkyear As String = ""
            Dim InvoiceDate As Date = Trim(txtInvoiceDate.Text)
            If InvoiceDate.Month < 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") - 1 & "-" & Format(InvoiceDate, "yy")
            ElseIf InvoiceDate.Month >= 4 Then
                strWorkyear = Format(InvoiceDate, "yyyy") & "-" & Format(InvoiceDate, "yy") + 1
            End If
            Dim Con_ID As Integer = 0
            strSql = ""
            strSql += "SELECT * FROM Other_Company where LineID='" & Trim(ddlshipline.SelectedValue) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                Con_ID = Trim(dt1.Rows(0)("Con_ID") & "")
            Else
                Con_ID = "1"
            End If
            'strSql = ""
            'strSql += "SELECT Isnull(Max(AssessNo),0) as [AssessNo] FROM Eyard_AssessM where workyear='" & strWorkyear & "'"
            'dt = db.sub_GetDatatable(strSql)
            'If dt.Rows(0)("AssessNo") = 0 Then
            '    intid = 1
            'Else
            '    intid = Val(dt.Rows(0)(0)) + 1
            'End If
            'strinvoiceNo = "EPIC/" & Strings.Right(strWorkyear, 5) & "/0" & intid
            strSql = "USP_INSERT_INTO_EYARD_ASSESSM_WEB '" & Trim(strWorkyear) & "','" & Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyy-MM-dd HH:mm") & "',"
            strSql += "'" & Convert.ToDateTime(txtBilledToDate.Text).ToString("yyyy-MM-dd HH:mm") & "'," & Val(ddlshipline.SelectedValue) & ","
            strSql += "'" & Val(lblTotal.Text) & "','" & Val(lblAllTotal.Text) & "'," & Session("UserId_DepoCFS") & ",'" & Trim(ddltariff.SelectedItem.Text) & "'," '" & Trim(RupeesConvert(Val(lblAllTotal.Text))) & "',"
            strSql += "'" & Val(lblSGST.Text) & "','" & Val(lblCGST.Text) & "','" & Trim(lblIGST.Text) & "','" & Val(lblpartyid.Text) & "'," & Val(ddlinvoicetype.SelectedValue) & ",'" & Convert.ToDateTime(txtFromDate.Text).ToString("yyyy-MM-dd HH:mm") & "'"
            strSql += ",'" & Trim(txtVessels.Text) & "','" & Trim(txtPONo.Text & "") & "','" & Trim(txtViaNo.Text & "") & "','" & Convert.ToDateTime(txtBilledToDate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtVesselID.Text) & "','" & Replace(Trim(txtRemarks.Text), "'", "''") & "','" & Trim(ddlEXTCriteria.SelectedItem.Text) & "','" & Trim(txtProformaNo.Text & "") & "'"
            strSql += ",'" & Val(ddlTaxID.SelectedValue) & "','" & Trim(ddlreference.SelectedValue & "") & "','" & Trim(txtReference.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            intid = Val(dt.Rows(0)("ASSESS_NO"))
            strinvoiceNo = Trim(dt.Rows(0)("INVOICE_NO"))
            '" & Trim(ddlinvoicetype.SelectedItem.Text) & "','','" & Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") & "','0',
            For Each row In grdcontainer.Rows
                Dim chkright As CheckBox = DirectCast(row.FindControl("chkright"), CheckBox)
                If chkright.Checked = True Then
                    'If ddlinvoicetype.SelectedItem.Text = "Lift On/Lift Off" And Trim(CType(row.FindControl("lblOutDate"), Label).Text) = "" Then
                    '    GoTo lblNext
                    'End If
                    strSql = ""
                    strSql += "SELECT  * FROM eyard_tariffdetails WHERE TariffID='" & Trim(ddltariff.SelectedItem.Text) & "' and "
                    strSql += " '" & Convert.ToDateTime(txtInvoiceDate.Text).ToString("yyyyMMdd") & "' BETWEEN EffectiveFrom and EffectiveUpto  AND IsApproved=1 and iscancel=0 AND "
                    strSql += "deliverytype='" & Trim(ddlinvoicetype.SelectedItem.Text & "") & "' " ' and ContainerType='" & Trim(grdcontainers.Rows(i).Cells("Type").Value) & "'" 'AND locfrom='" & Trim(grdcontainers.Rows(k).Cells("Location(From)").Value) & "' and locTo='" & Trim(grdcontainers.Rows(k).Cells("Location(To)").Value) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If (Trim(ddlinvoicetype.SelectedItem.Text & "") = "Empty Container Repair" Or Trim(ddlinvoicetype.SelectedItem.Text & "") = "Washing") Then
                        AccountID = 9
                    Else
                        If Not dt.Rows.Count > 0 Then
                            GoTo lblNext
                        End If
                        AccountID = Trim(dt.Rows(0)("AccountID") & "")
                    End If

                    Dim dtctype As New DataTable
                    strSql = ""
                    strSql += "select ContainerTypeID from ContainerType where ContainerType='" & Trim(CType(row.FindControl("lblContainerType"), Label).Text) & "'"
                    dtctype = db.sub_GetDatatable(strSql)

                    strSql = ""
                    strSql = "Exec SP_GetBilledDays '" & Trim(CType(row.FindControl("lblContainerN"), Label).Text) & "','" & Trim(CType(row.FindControl("lblSize"), Label).Text) & "','" & Val(dtctype.Rows(0)(0)) & "','" & AccountID & "'"
                    dt1 = db.sub_GetDatatable(strSql)
                    Dim billedDays As Integer = 0
                    If dt1.Rows.Count > 0 Then
                        billedDays = Trim(dt1.Rows(0)("Billed Days"))
                    Else
                        billedDays = 0
                    End If
                    strSql = ""
                    strSql += "select * from temp_bill_emptyd where containerno='" & Trim(CType(row.FindControl("lblContainerN"), Label).Text) & "' and size='" & Trim(CType(row.FindControl("lblSize"), Label).Text) & "' and USERID=" & Session("UserId_DepoCFS") & " "
                    dt2 = db.sub_GetDatatable(strSql)
                    If dt2.Rows.Count > 0 Then
                        For i = 0 To dt2.Rows.Count - 1
                            strSql = ""
                            strSql += "USP_INSERT_INTO_EYARD_ASSESSD_WEB " & Val(intid) & ",'" & strinvoiceNo & "','" & strWorkyear & "',"
                            strSql += " '" & Trim(dt2.Rows(i)("movementno")) & "','" & Trim(dt2.Rows(i)("Containerno")) & "','" & Trim(dt2.Rows(i)("AccountID")) & "','" & Trim(dt2.Rows(i)("NetAmount")) & "','" & Trim(dt2.Rows(i)("CONTAINERTYPE")) & "','" & Trim(dt2.Rows(i)("size")) & "',"
                            strSql += "'" & Val(dt2.Rows(i)("d_sgst")) & "','" & Val(dt2.Rows(i)("d_cgst")) & "', '" & Val(dt2.Rows(i)("d_igst")) & "'," & Val(dt2.Rows(i)("taxgroupID")) & "," & billedDays & "," & Val(ddltariff.SelectedValue & "") & ""
                            db.sub_ExecuteNonQuery(strSql)
                        Next
                    End If
                End If
lblNext:
            Next
            txtAssessNoPrint.Text = strinvoiceNo
            txtWorkYearPrint.Text = strWorkyear
            txtLineIDPrint.Text = Val(ddlshipline.SelectedValue)
            txtInvoiceTypePrint.Text = Val(ddlinvoicetype.SelectedValue)
            Clear()
            btnsave.Text = "Save"
            btnsave.Attributes.Add("Class", "btn btn-success btn-sm outline pull-right")
            lblSession.Text = "Record Saved successfully for Invoice No </br>" & strinvoiceNo & ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel4.Update()
        Catch ex As Exception
            btnsave.Text = "Save"
            btnsave.Attributes.Add("Class", "btn btn-success btn-sm outline pull-right")
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Sub Clear()
        Try
            db.sub_ExecuteNonQuery("Delete From temp_bill_emptyD Where UserID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from temp_gst_search Where UserID=" & Session("UserId_DepoCFS") & "")
            db.sub_ExecuteNonQuery("Delete from Temp_Vessel_Repo_Invoice Where UserID=" & Session("UserId_DepoCFS") & "")
            txtInvoiceDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtBilledToDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Filldropdown()
            GRID()
            txtcontainer.Text = ""
            txtGstINNumber.Text = ""
            txtgstname.Text = ""
            lblTotal.Text = ""
            lblCGST.Text = ""
            lblSGST.Text = ""
            lblIGST.Text = ""
            lblAllTotal.Text = ""
            StrCGSTPEr = ""
            StrIGSTPer = ""
            StrIGSTPer = ""
            lblchargescount.Visible = False
            LBLNO.Text = ""
            LBLNO.Visible = False
            divtblWOTOtal.Attributes.Add("style", "display:none")
            lblA.Text = ""
            lblB.Text = ""
            lblC.Text = ""
            lblTeus.Text = ""
            txtProformaNo.Text = ""
            txtVessels.Text = ""
            txtVesselID.Text = ""
            txtReference.Text = ""
            'txtStatement.Text = ""
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub SaveOk_ServerClick(sender As Object, e As EventArgs)
        Try
            lblPrintQue.Text = "Do you wish to print Invoice?"
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModalforupdate1", "$('#myModalforupdate1').modal();", True)
            UpdatePanel6.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlBasedOnDate_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            'If txtcontainer.Text = "" Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Enter Container no first');", True)
            '    txtcontainer.Focus()
            '    Exit Sub
            'End If
            'strSql = ""
            'strSql += "USP_BILLED_TO_DATE_BASED_ON '" & Trim(txtcontainer.Text & "") & "'"
            'ds = db.sub_GetDataSets(strSql)

            'If ddlBasedOnDate.SelectedValue = "0" Then
            '    txtBilledToDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            'ElseIf ddlBasedOnDate.SelectedValue = "Repaired Date" Then
            '    If ds.Tables(0).Rows.Count > 0 Then
            '        txtBilledToDate.Text = Convert.ToDateTime(ds.Tables(0).Rows(0)("REPAIREDDATE")).ToString("yyyy-MM-ddTHH:mm")
            '    End If
            'ElseIf ddlBasedOnDate.SelectedValue = "Approved Date" Then
            '    If ds.Tables(0).Rows.Count > 0 Then
            '        txtBilledToDate.Text = Convert.ToDateTime(ds.Tables(0).Rows(0)("APPROVEDON")).ToString("yyyy-MM-ddTHH:mm")
            '    End If
            'ElseIf ddlBasedOnDate.SelectedValue = "Out Date" Then
            '    If ds.Tables(1).Rows.Count > 0 Then
            '        txtBilledToDate.Text = Convert.ToDateTime(ds.Tables(1).Rows(0)("OutDate")).ToString("yyyy-MM-ddTHH:mm")
            '    End If
            'End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chkSelectAll.Checked = True Then
                For Each row In grdcontainer.Rows
                    DirectCast(row.FindControl("chkright"), CheckBox).Checked = True
                Next
            Else
                For Each row In grdcontainer.Rows
                    DirectCast(row.FindControl("chkright"), CheckBox).Checked = False
                Next
            End If
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub chkright_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dblchkcount As Double = 0
            For Each row In grdcontainer.Rows
                If CType(row.FindControl("chkright"), CheckBox).Checked = True Then
                    dblchkcount += 1
                End If
            Next
            If dblchkcount = grdcontainer.Rows.Count Then
                chkSelectAll.Checked = True
            Else
                chkSelectAll.Checked = False
            End If
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub ddlshipline_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If ddlshipline.SelectedValue <> 0 Then
                strSql = ""
                strSql += "USP_TARRIFF_ID_LINE_WISE " & Val(ddlshipline.SelectedValue & "") & ""
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ddltariff.SelectedValue = Val(dt.Rows(0)("EntryID") & "")
                End If
                strSql = ""
                strSql = "USP_VESSELS_LINE_WISE " & Val(ddlshipline.SelectedValue) & ""
                dt = db.sub_GetDatatable(strSql)
                ddlVessels.DataSource = dt
                ddlVessels.DataTextField = "VesselName"
                ddlVessels.DataValueField = "VesselID"
                ddlVessels.DataBind()
                ddlVessels.Items.Insert(0, New ListItem("All", 0))
            Else
                ddltariff.SelectedValue = 0
            End If
            UpdateTariff.Update()
            txtcontainer.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Try
            Dim dbl20 As Double, dbl40 As Double, dbl45 As Double, strContainer As String
            If txtcontainer.Text <> "" And Len(Trim(txtcontainer.Text & "")) = "11" Then
                strSql = ""
                strSql += "USP_INSERT_INTO_TEMP_INVOICE_CONTAINERS '" & Trim(txtcontainer.Text & "") & "'," & Val(ddlshipline.SelectedValue) & "," & Session("UserId_DepoCFS") & ""
                dt = db.sub_GetDatatable(strSql)
                grdcontainer.DataSource = dt
                grdcontainer.DataBind()

                For i = 0 To dt.Rows.Count - 1
                    strContainer = Trim(dt.Rows(i)("Size"))
                    If InStr(strContainer, "20") > 0 Then
                        dbl20 += 1
                    ElseIf InStr(strContainer, "40") > 0 Then
                        dbl40 += 1
                    ElseIf InStr(strContainer, "45") > 0 Then
                        dbl45 += 1
                    End If
                Next
                lblA.Text = dbl20
                lblB.Text = dbl40
                lblC.Text = dbl45
                lblTeus.Text = dbl20 + Val(dbl40) * 2 + Val(dbl45) * 2
            End If
            chkright_CheckedChanged(sender, e)
            UpdatePanel1.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub ClearAmount()
        Try
            db.sub_ExecuteNonQuery("Delete From temp_bill_emptyD Where UserID=" & Session("UserId_DepoCFS") & "")
            strSql = ""
            strSql = ""
            dt = db.sub_GetDatatable(strSql)
            rptIndentLIst.DataSource = dt
            rptIndentLIst.DataBind()
            lblchargescount.Visible = False
            LBLNO.Text = dt.Rows.Count
            LBLNO.Visible = False
            divtblWOTOtal.Attributes.Add("style", "display:none")
            lblTotal.Text = ""
            txtdiscount.Text = ""
            lblSgstPer.Text = ""
            lblCgstPer.Text = ""
            lblIgstPer.Text = ""
            lblCGST.Text = ""
            lblSGST.Text = ""
            lblIGST.Text = ""
            lblAllTotal.Text = ""

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnIndentItem1_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "USP_FETCH_FROM_TEMP_VESSEL_REPO_INVOICE '" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            txtVessels.Text = Trim(dt.Rows(0)("VesselName"))
            txtVesselID.Text = Trim(dt.Rows(0)("VesselID"))

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnProforma_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "Select top 1 * from Temp_Proforma_Search WHERE USERID='" & Session("UserId_DepoCFS") & "' order by autoid desc"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtProformaNo.Text = Trim(dt.Rows(0)("ProformaNo") & "")
                txtProformaNo_TextChanged(sender, e)
            End If
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtProformaNo_TextChanged(sender As Object, e As EventArgs)
        Try
            If Trim(txtProformaNo.Text) <> "" Then
                ClearAmount()
                Dim dbl20 As Double, dbl40 As Double, dbl45 As Double, strContainer As String
                db.sub_ExecuteNonQuery("Delete from TEMP_INVOICE_CONTAINERS Where USER_ID=" & Session("UserId_DepoCFS") & "")
                strSql = ""
                strSql += "USP_GET_PROFORMA_DETAILS_FOR_FINAL '" & Trim(txtProformaNo.Text & "") & "','" & Session("UserId_DepoCFS") & "'"
                ds = db.sub_GetDataSets(strSql)
                If ds.Tables(0).Rows.Count > 0 Then
                    ddlEXTCriteria.SelectedValue = Trim(ds.Tables(0).Rows(0)("EXT_Type"))
                    txtGstINNumber.Text = Trim(ds.Tables(0).Rows(0)("GSTIn_uniqID"))
                    txtgstname.Text = Trim(ds.Tables(0).Rows(0)("GSTName"))
                    lblstatecode.Text = Trim(ds.Tables(0).Rows(0)("state_Code"))
                    lblpartyid.Text = Trim(ds.Tables(0).Rows(0)("GSTID"))
                    ddlshipline.SelectedValue = Val(ds.Tables(0).Rows(0)("lineID"))
                    txtPONo.Text = Trim(ds.Tables(0).Rows(0)("PONo"))
                    txtViaNo.Text = Trim(ds.Tables(0).Rows(0)("ViaNo"))
                    txtVesselID.Text = Trim(ds.Tables(0).Rows(0)("VesselID"))
                    txtVessels.Text = Trim(ds.Tables(0).Rows(0)("VesselName"))
                    ddlinvoicetype.SelectedValue = Val(ds.Tables(0).Rows(0)("InvoiceType"))
                    txtFromDate.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("FromDate"))).ToString("yyyy-MM-ddTHH:mm")
                    txtBilledToDate.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("Todate"))).ToString("yyyy-MM-ddTHH:mm")
                    ddltariff.SelectedValue = Val(ds.Tables(0).Rows(0)("TariffID"))
                    ddlTaxID.SelectedValue = Val(ds.Tables(0).Rows(0)("TaxID"))
                    txtRemarks.Text = Trim(ds.Tables(0).Rows(0)("Remarks"))
                    dblGroup1Amt = Val(ds.Tables(0).Rows(0)("NetTotal"))
                End If
                grdcontainer.DataSource = ds.Tables(1)
                grdcontainer.DataBind()
                UpdatePanel1.Update()
                For i = 0 To ds.Tables(1).Rows.Count - 1
                    strContainer = Trim(ds.Tables(1).Rows(i)("Size"))
                    If InStr(strContainer, "20") > 0 Then
                        dbl20 += 1
                    ElseIf InStr(strContainer, "40") > 0 Then
                        dbl40 += 1
                    ElseIf InStr(strContainer, "45") > 0 Then
                        dbl45 += 1
                    End If
                Next
                lblA.Text = dbl20
                lblB.Text = dbl40
                lblC.Text = dbl45
                lblTeus.Text = dbl20 + Val(dbl40) * 2 + Val(dbl45) * 2
                grid1()
                Sub_SGTRate()
                sub_CalcTotals()
            End If
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub


    Protected Sub txtdiscount_TextChanged(sender As Object, e As EventArgs)
        Try
            Sub_SGTRate()
            Call sub_CalcTotals1()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CalcTotals1()
        Try
            Dim dbltotal As Double = 0, dblvalSGST As Double = 0, dbltotalsgst As Double = 0, dbltotalcgst As Double = 0, dbltotaligst As Double = 0, dblvalCGST As Double = 0, dblvalIGST As Double = 0, dbldisc As Double = 0, dblalltotal As Double = 0
            dblGroup1Amt = lblTotal.Text
            dbltotal = dblGroup1Amt
            dbldisc = Val(txtdiscount.Text)
            dbltotalcgst = Format((dblGroup1Amt - dbldisc) * (dblSGST / 100), "0.00")
            dbltotalsgst = Format((dblGroup1Amt - dbldisc) * (dblCGST / 100), "0.00")
            dbltotaligst = Format((dblGroup1Amt - dbldisc) * (dblIGST / 100), "0.00")
            strSql = ""
            strSql += "select CEILING(cast(" & dbltotalcgst & " as float)) as totalcgst,CEILING(cast(" & dbltotalsgst & "as float)) as totalsgst,CEILING(cast(" & dbltotaligst & " as float)) as totaligst"
            dt = db.sub_GetDatatable(strSql)
            dblalltotal = dbltotal - dbldisc + Val(dt.Rows(0)("totalsgst")) + Val(dt.Rows(0)("totalcgst")) + Val(dt.Rows(0)("totaligst"))
            lblTotal.Text = dbltotal

            lblSgstPer.Text = strSGSTPer
            lblCgstPer.Text = StrCGSTPEr
            lblIgstPer.Text = StrIGSTPer
            lblCGST.Text = Val(dt.Rows(0)("totalcgst"))
            lblSGST.Text = Val(dt.Rows(0)("totalsgst"))
            lblIGST.Text = Val(dt.Rows(0)("totaligst"))
            lblAllTotal.Text = dblalltotal
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

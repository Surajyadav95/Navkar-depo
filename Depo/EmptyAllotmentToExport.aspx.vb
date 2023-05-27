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
            txtAllotmentDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
        End If
    End Sub
    Protected Sub Filldropdown()
        Try           
            strSql = ""
            strSql = "SELECT DISTINCT agid, agname FROM customer where isactive=1 order by agname asc"
            dt = db.sub_GetDatatable(strSql)
            ddlOnaccount.DataSource = dt
            ddlOnaccount.DataTextField = "agname"
            ddlOnaccount.DataValueField = "agid"
            ddlOnaccount.DataBind()
            ddlOnaccount.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            Dim countrunning As Integer = 0
            countrunning = countrunning + 1
            Dim dtCount As DataTable
            strSql = ""
            strSql = "exec  USP_EXP_IN_Allow_Validation_new '" & Trim(txtBookingNo.Text & "") & "','" & Trim(txtsize.Text) & "','" & countrunning & "'"
            dtCount = db.sub_GetDatatable(strSql)
            If dtCount.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert(' " + dtCount.Rows(0)("msg") + ".');", True)
                txtBookingNo.Text = ""
           

                txtBookingNo.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql = "USP_EYARD_BK_HOLD '" & Trim(txtBookingNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified Booking no is on hold again hold reasons');", True)
                'MsgBox("Specified Booking no is on hold again hold reasons " & Trim(dt.Rows(0)("HoldReason") & "") & " and remarks  " & Trim(dt.Rows(0)("Remarks") & "") & ". Cannot proceed", vbCritical)
                txtcontainerNo.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql = "USP_EYARD_CTR_HOLD '" & Trim(txtcontainerNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified Container no is on hold again hold reasons ');", True)
                'MsgBox("Specified Container no is on hold again hold reasons " & Trim(dt.Rows(0)("HoldReason") & "") & " and remarks  " & Trim(dt.Rows(0)("Remarks") & "") & ". Cannot proceed", vbCritical)
                txtcontainerNo.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql = "usp_check_exp_do_allotment_Validations_NEW '" & Trim(txtBookingNo.Text) & "','" & Val(txtAllotmentID.Text) & "', '" & Trim(txtcontainerNo.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ' txttareWt.Text = Trim(dt.Rows(0)("TareWeight") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified container line mismatch. Cannot proceed');", True)
                'MsgBox("Specified container siz/type/ line mismatch. Cannot proceed", vbCritical)
                txtcontainerNo.Focus()
                Exit Sub
            End If
            strSql = ""
            strSql = "SELECT TOP 1 EntryID FROM exp_IN WHERE ContainerNo='" & Trim(txtcontainerNo.Text) & "' And Status='P' AND IsCancel=0 ORDER BY EntryID DESC"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Container is already IN. please go to administrator for editing if any!');", True)
                'MsgBox("This Container is already IN. please go to administrator for editing if any!")
                Exit Sub
            End If

            strSql = ""
            strSql += "USP_ALLOTMENT_MTY_TO_EXPORT_NEW '" & Trim(txtBookingNo.Text) & "', '" & Trim(txtcontainerNo.Text) & "'," & Val(txtentryID.Text) & ", " & Val(ddlOnaccount.SelectedValue) & ", '" & Trim(txtseal.Text) & "','" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "', '" & Val(txtAllotmentID.Text) & "', '" & Session("UserId_DepoCFS") & "',  '" & Convert.ToDateTime(Trim(txtindate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "','" & Trim(txtpod.Text) & "' "
            db.sub_ExecuteNonQuery(strSql)
            lblSession.Text = "Record Updated successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select *  from Temp_Empty_Booking_Export where userid='" & Session("UserId_DepoCFS") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtbookingNo.Text = Trim(dt.Rows(0)("BookingNo") & "")

            End If
            Call txtBookingNo_TextChanged(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnshow_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "exec get_sp_container_fetch_for_Out '" & Trim(txtcontainerNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtentryID.Text = Trim(dt.Rows(0)("ENTRYID") & "")
                txtsize.Text = Trim(dt.Rows(0)("Size") & "")
                txtindate.Text = Trim(dt.Rows(0)("indate") & "")
                txtType.Text = Trim(dt.Rows(0)("Type") & "")
                txtShippingLine.Text = Trim(dt.Rows(0)("LineI") & "")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container Not gate in,Please recheck');", True)
                'MsgBox("" & txtcontainerNo.Text & " Container Not gate in,Please recheck", vbCritical)
                txtcontainerNo.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtBookingNo_TextChanged(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "exec usp_booking_Export_search '" & Trim(txtBookingNo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                'txtBookingNo.Text = Trim(dt.Rows(0)("ENTRYID") & "")
                ddlOnaccount.SelectedValue = Val(dt.Rows(0)("agencyID") & "")
                'lblbksize.Text = Trim(dt.Rows(0)("size") & "")
                'lblbktype.Text = Trim(dt.Rows(0)("containertypeid") & "")
                lblbkslid.Text = Trim(dt.Rows(0)("SLID") & "")
                txtpod.Text = Trim(dt.Rows(0)("POD") & "")
                txtAllotmentID.Text = Trim(dt.Rows(0)("Gate Allow ID") & "")
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

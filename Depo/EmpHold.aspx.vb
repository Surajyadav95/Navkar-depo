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
    Dim ds As DataSet
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
       
        If Not IsPostBack Then
            txtHoldDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            Filldropdown()
            grid()
            gridHolddetas()
        End If

    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_HoldReason"
            ds = db.sub_GetDataSets(strSql)
            ddlHoldReason.DataSource = ds.Tables(0)
            ddlHoldReason.DataTextField = "HoldReason"
            ddlHoldReason.DataValueField = "HoldReasonID"
            ddlHoldReason.DataBind()
            ddlHoldReason.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub grid()
        Try
            strSql = ""
            strSql += ""
            dt = db.sub_GetDatatable(strSql)
            GrdHoldDetails.DataSource = dt
            GrdHoldDetails.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Dim dt As New DataTable
        Dim strsql As String

        strsql = ""
        strsql = "sp_Get_ext_Cont_LastJODetails '" & Trim(txtcontainer.Text) & "'"
        dt = db.sub_GetDatatable(strsql)
        If dt.Rows.Count > 0 Then
            txtEntryId.Text = Trim(dt.Rows(0)("EntryID") & "")
            txtLineName.Text = Trim(dt.Rows(0)("SLName") & "")
            If Val(dt.Rows(0)("HoldReasonID") & "") = 0 Then
            Else
                ddlHoldReason.SelectedValue = Val(dt.Rows(0)("HoldReasonID") & "")
            End If
            TxtSize.Text = Trim(dt.Rows(0)("Size") & "")
            txtType.Text = Trim(dt.Rows(0)("ContainerType") & "")
            txtViano.Text = Trim(dt.Rows(0)("ViaNo") & "")
            txtCustomerName.Text = Trim(dt.Rows(0)("Consignee") & "")
            TxtInDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("InDate") & "")).ToString("yyyy-MM-ddTHH:mm")
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid container no');", True)
            Exit Sub


        End If
        ddlHoldReason.Focus()

        'Dim dt1 As New DataTable
        strsql = ""
        strsql = "get_sp_Eyard_Hold_Status '" & Trim(txtcontainer.Text) & "','" & Trim(txtEntryId.Text) & "'"
        dt1 = db.sub_GetDatatable(strsql)
        GrdHoldDetails.DataSource = dt1
        GrdHoldDetails.DataBind()




    End Sub
    Private Sub gridHolddetas()
        strSql = ""
        strSql = "Select EntryID as [Entry ID], ContainerNo as [Container No], HoldReason [Hold Reason] from Eyard_Holds HD, Eyard_HoldReasons HR Where HD.HoldReasonID=HR.HoldReasonID and HoldStatus='H'"
        dt2 = db.sub_GetDatatable(strSql)
        grdDets.DataSource = dt2
        grdDets.DataBind()

    End Sub

    Protected Sub btnHold_Click(sender As Object, e As EventArgs) Handles btnHold.Click
        Try
            Dim intRowCounter As Integer
            Dim strprocesses As String
            Dim IntCount As Integer
            strSql = ""
            strSql += "SELECT * FROM Eyard_holds WHERE HoldStatus='H' AND ContainerNo='" & Trim(txtcontainer.Text) & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container no already on hold');", True)
                Exit Sub
            End If
            If Trim(txtEntryId.Text) <> "" And Trim(TxtSize.Text) <> "" Then
                strSql = ""
                strSql = "SP_Save_ext_Holds '" & Trim(txtEntryId.Text) & "', '" & Trim(txtcontainer.Text) & "','" & Val(ddlHoldReason.SelectedValue) & "','','" & Trim(txtRemarks.Text) & "','H','" & Session("UserId_DepoCFS") & "'"
                dt3 = db.sub_GetDatatable(strSql)
                lblSession.Text = "Hold  Details update successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                txtcontainer.Focus()
                gridHolddetas()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnClearHold_Click(sender As Object, e As EventArgs) Handles btnClearHold.Click
        Try
            strSql = ""
            strSql += "SELECT * FROM Eyard_holds WHERE HoldStatus='H' AND ContainerNo='" & Trim(txtcontainer.Text) & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If Not dt1.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Container no not on hold');", True)
                Exit Sub
            End If
            If Trim(txtEntryId.Text) <> "" And Trim(TxtSize.Text) <> "" Then
                strSql = "UPDATE Eyard_holds SET Releasedremarks='" & Trim(txtRemarks.Text) & "', HoldStatus='C', ClearedBy=" & Session("UserId_DepoCFS") & ", ClearedOn='" & Format(Now, "dd-MMM-yyyy HH:mm:ss") & "' WHERE "
                strSql += " ContainerNo='" & Trim(txtcontainer.Text) & "'"
                strSql += " AND HoldStatus='H'"
                dt4 = db.sub_GetDatatable(strSql)
                strSql = ""
                strSql += "update EYard_In set Is_Expired=0 where ContainerNo='" & Trim(txtcontainer.Text) & "' and EntryID=" & Val(txtEntryId.Text) & ""
                db.sub_ExecuteNonQuery(strSql)
                lblSession.Text = "Hold  clear successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                txtcontainer.Focus()
                gridHolddetas()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub CLear()
        txtEntryId.Text = ""
        ddlHoldReason.SelectedValue = 0
        txtLineName.Text = ""
        TxtSize.Text = ""
        txtType.Text = ""
        txtViano.Text = ""
        TxtInDate.Text = ""
        txtRemarks.Text = ""
        grid()
        gridHolddetas()
    End Sub
    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtcontainer.Text = ""
        txtEntryId.Text = ""
        ddlHoldReason.SelectedValue = 0
        txtLineName.Text = ""
        TxtSize.Text = ""
        txtType.Text = ""
        txtViano.Text = ""
        TxtInDate.Text = ""
        txtRemarks.Text = ""
        grid()
        gridHolddetas()
        txtHoldDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")

    End Sub
End Class

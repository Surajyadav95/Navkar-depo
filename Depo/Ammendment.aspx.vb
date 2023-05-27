Imports System.Data.SqlClient
Imports System.Data

Partial Class RA_asd
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'fillDashboard()
            fillclosed()
            checkRights()
        End If
    End Sub
    Private Sub checkRights()
        Try
            Dim strConn As String = ""
            Dim conn As SqlConnection
            Dim cmd As New SqlCommand
            strConn = ConfigurationManager.ConnectionStrings("SqlConnString").ConnectionString
            If conn Is Nothing OrElse conn.ConnectionString = Nothing Then
                conn = New System.Data.SqlClient.SqlConnection(strConn)
            End If
            If conn.State = System.Data.ConnectionState.Closed Then
                Try
                    conn.Open()
                Catch Ex As Exception
                    conn = New System.Data.SqlClient.SqlConnection(strConn)
                    conn.Open()
                End Try
            End If

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter
            Dim strSQL = ""
            strSQL = "Select menuid,MenuDesc,isnull((Select isaccess from UserRights where MenuID=md.MenuID and UserID='" & Convert.ToInt32(Session("UserId_DepoCFS")) & "'),0) as IsAccess from MenuDetails md where menuid IN (69,70,71,72,73,74,75)"

            cmd.CommandTimeout = 120
            cmd.CommandText = strSQL
            cmd.Connection = conn
            If conn.State = ConnectionState.Closed Then conn.Open()
            da.SelectCommand = cmd
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If Trim(dt.Rows(i)("MenuID")) = "69" Then
                        divCancelNOC.Visible = Convert.ToBoolean(Val(dt.Rows(i)("IsAccess")))
                    ElseIf Trim(dt.Rows(i)("MenuID")) = "70" Then
                        divCancelBondIN.Visible = Convert.ToBoolean(Val(dt.Rows(i)("IsAccess")))
                    ElseIf Trim(dt.Rows(i)("MenuID")) = "71" Then
                        divCancelBondEx.Visible = Convert.ToBoolean(Val(dt.Rows(i)("IsAccess")))
                    ElseIf Trim(dt.Rows(i)("MenuID")) = "72" Then
                        divCancelGatePass.Visible = Convert.ToBoolean(Val(dt.Rows(i)("IsAccess")))
                    ElseIf Trim(dt.Rows(i)("MenuID")) = "73" Then
                        divModifyNOC.Visible = Convert.ToBoolean(Val(dt.Rows(i)("IsAccess")))
                    ElseIf Trim(dt.Rows(i)("MenuID")) = "74" Then
                        divModifyBondIn.Visible = Convert.ToBoolean(Val(dt.Rows(i)("IsAccess")))
                    ElseIf Trim(dt.Rows(i)("MenuID")) = "75" Then
                        divModifyBondEx.Visible = Convert.ToBoolean(Val(dt.Rows(i)("IsAccess")))
                    End If
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub
    'Protected Sub fillDashboard()
    '    Try
    '        ds = db.sub_GetDataSets("GET_Sp_BOND_CFS_SummaryDtels")
    '        If (ds.Tables(0).Rows.Count > 0) Then
    '            'lblreg.Text = ds.Tables(0).Rows(0)(1)
    '            'lblbondIn.Text = ds.Tables(0).Rows(1)(1)
    '            'lblbondex.Text = ds.Tables(0).Rows(2)(1)
    '            'lbllive.Text = ds.Tables(0).Rows(3)(1)
    '            'lblEx.Text = ds.Tables(0).Rows(4)(1)
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

    '    End Try
    'End Sub
    Protected Sub fillclosed()
        Try
            dt = db.sub_GetDatatable("SP_Bond_Space_Status")
            If dt.Rows.Count > 0 Then
                'lblopen.Text = Trim(dt.Rows(0)("Tool") & "")
                ''lblopenbond.Text = Trim(dt.Rows(0)("Activity") & "")
                'lblclosed.Text = Trim(dt.Rows(1)("Tool") & "")
                'lblclosebond.Text = Trim(dt.Rows(1)("Activity") & "")

            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub
End Class

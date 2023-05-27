Imports System.Data.SqlClient
Imports System.Data

Partial Class RA_asd
    Inherits System.Web.UI.Page
    Dim ds As DataSet
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillDashboard()
            'fillclosed()
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
            strSQL = " SELECT *  FROM UserRights INNER JOIN MenuDetails ON UserRights.MenuID=MenuDetails.MenuID "
            strSQL += " where MenuDetails.menuid IN (209, 305, 517, 203) and UserID='" & Convert.ToInt32(Session("UserId_DepoCFS")) & "'"

            cmd.CommandTimeout = 120
            cmd.CommandText = strSQL
            cmd.Connection = conn
            If conn.State = ConnectionState.Closed Then conn.Open()
            da.SelectCommand = cmd
            da.Fill(dt)
          

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub fillDashboard()
        Try
            Dim dt As New DataTable
            Dim strsql As String
            lblto.Text = Now
            lblfrom.Text = Now.AddDays(-Now.Day + 1)
            strsql = ""
            strsql = "Get_Sp_Dashbord '" & Convert.ToDateTime(lblfrom.Text).ToString("yyyy-MM-dd 08:00:00") & "','" & Convert.ToDateTime(lblto.Text).ToString("yyyy-MM-dd 07:59:59") & "'"
            ds = db.sub_GetDataSets(strsql)
            If (ds.Tables(0).Rows.Count > 0) Then
                lblUnest20.Text = ds.Tables(0).Rows(1)("20's")
                lblUnest40.Text = ds.Tables(0).Rows(1)("40's")
                lblUnest45.Text = ds.Tables(0).Rows(1)("45's")
                lblUnestTeus.Text = ds.Tables(0).Rows(1)("TEUS")

                lblIn20.Text = ds.Tables(0).Rows(2)("20's")
                lblIn40.Text = ds.Tables(0).Rows(2)("40's")
                lblIn45.Text = ds.Tables(0).Rows(2)("45's")
                lblInTeus.Text = ds.Tables(0).Rows(2)("TEUS")

                lblOut20.Text = ds.Tables(0).Rows(3)("20's")
                lblOut40.Text = ds.Tables(0).Rows(3)("40's")
                lblOut45.Text = ds.Tables(0).Rows(3)("45's")
                lblOutTeus.Text = ds.Tables(0).Rows(3)("TEUS")

                lblInv20.Text = ds.Tables(0).Rows(4)("20's")
                lblInv40.Text = ds.Tables(0).Rows(4)("40's")
                lblInv45.Text = ds.Tables(0).Rows(4)("45's")
                lblInvTeus.Text = ds.Tables(0).Rows(4)("TEUS")

                lblest20.Text = ds.Tables(0).Rows(0)("20's")
                lblest40.Text = ds.Tables(0).Rows(0)("40's")
                lblest45.Text = ds.Tables(0).Rows(0)("45's")
                lblestTeus.Text = ds.Tables(0).Rows(0)("TEUS")

                lblApp20.Text = ds.Tables(0).Rows(6)("20's")
                lblApp40.Text = ds.Tables(0).Rows(6)("40's")
                lblApp45.Text = ds.Tables(0).Rows(6)("45's")
                lblAppTeus.Text = ds.Tables(0).Rows(6)("TEUS")

                lblRep20.Text = ds.Tables(0).Rows(5)("20's")
                lblRep40.Text = ds.Tables(0).Rows(5)("40's")
                lblRep45.Text = ds.Tables(0).Rows(5)("45's")
                lblRepTeus.Text = ds.Tables(0).Rows(5)("TEUS")

                lblHold20.Text = ds.Tables(0).Rows(7)("20's")
                lblHold40.Text = ds.Tables(0).Rows(7)("40's")
                lblHold45.Text = ds.Tables(0).Rows(7)("45's")
                lblHoldTeus.Text = ds.Tables(0).Rows(7)("TEUS")

            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub
    Protected Sub fillclosed()
        Try
            dt = db.sub_GetDatatable("SP_Bond_Space_Status")
            If dt.Rows.Count > 0 Then
                'lblopen.Text = Trim(dt.Rows(0)("Tool") & "")
                'lblopenbond.Text = Trim(dt.Rows(0)("Activity") & "")
                'lblclosed.Text = Trim(dt.Rows(1)("Tool") & "")
                'lblclosebond.Text = Trim(dt.Rows(1)("Activity") & "")

            End If

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString

        End Try
    End Sub
End Class

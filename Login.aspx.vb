Imports System.Data.SqlClient
Imports HRRecruitment.MyConnection
Imports System.Data

Partial Class Login
    Inherits System.Web.UI.Page
    Dim strSQL As String
    Dim db As New dbOperation_Depo
    Dim dt As DataTable

    Protected Sub BtnSubmit_Click(sender As Object, e As EventArgs) Handles BtnSubmit.Click
        Try
            initDB()

            'Dim db As New Class1
            'Dim ObjDS As New DataSet
            'Dim strSQL As String = ""
            'strSQL = "GET_UserLoginDetails '" & username.Text & "',  '" & password.Text & "'"
            'ObjDS = db.sub_GetDataSets(strSQL)

            'If ObjDS.Tables(0).Rows.Count > 0 Then
            '    Session("UserId_DepoCFS") = ObjDS.Tables(0).Rows(0)("UserID")
            '    Session("UserName_DepoCFS") = ObjDS.Tables(0).Rows(0)("UserName")
            '    Response.Redirect("RA/Home.aspx")
            'Else
            '    Label2.Text = "Invalid User Name or Password"
            'End If


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Session("UserId_DepoCFS") = 0
        Session("UserName_DepoCFS") = ""
        lblErrorMsg.Text = ""
        Dim strWorkyear As String = ""
        If Now.Month < 4 Then
            strWorkyear = Format(Now, "yyyy") - 1 & "-" & Format(Now, "yy")
        ElseIf Now.Month >= 4 Then
            strWorkyear = Format(Now, "yyyy") & "-" & Format(Now, "yy") + 1
        End If
        Session("Workyear") = strWorkyear
        strSQL = ""
        strSQL += "select Tinnumber from settings"
        dt = db.sub_GetDatatable(strSQL)
        If dt.rows.count > 0 Then
            Session("CompID") = dt.Rows(0)(0)
        End If
        If ((Not (Request.Cookies("EMPID_DepoCFS")) Is Nothing) _
              AndAlso (Not (Request.Cookies("UserPass_DepoCFS")) Is Nothing)) Then
            username.Text = Request.Cookies("EMPID_DepoCFS").Value
            password.Attributes("value") = Request.Cookies("UserPass_DepoCFS").Value
            If username.Text <> "" Then
                chkRememberMe.Checked = True
            End If

            'DivLocation.Attributes.Add("style", "display: none")
        End If
        username.Focus()
    End Sub
    Public Sub initDB()
        Dim strConn As String = ""
        Dim conn As SqlConnection
        Dim cmd As New SqlCommand
        lblErrorMsg.Text = ""
        Try
            strConn = ConfigurationManager.ConnectionStrings("SqlConnString_Depo").ConnectionString
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
            If cmd Is Nothing Then
                cmd = New System.Data.SqlClient.SqlCommand()
                cmd.CommandType = System.Data.CommandType.Text
                cmd.Connection = conn
            End If

            rememberMe()
            Dim dt As New DataSet
            Dim da As New SqlDataAdapter
            cmd.CommandTimeout = 120
            cmd.CommandText = " GET_UserLoginDetails_Web '" & Trim(username.Text & "") & "','" & Trim(password.Text & "") & "'"
            cmd.Connection = conn
            If conn.State = ConnectionState.Closed Then conn.Open()
            da.SelectCommand = cmd
            da.Fill(dt)
            If dt.Tables(0).Rows.Count > 0 Then
                Session("UserId_DepoCFS") = dt.Tables(0).Rows(0)("UserID")
                Session("UserName_DepoCFS") = dt.Tables(0).Rows(0)("UserName")
                Session("UserPass_DepoCFS") = dt.Tables(0).Rows(0)("UserPass")
                Session("EMPID_DepoCFS") = dt.Tables(0).Rows(0)("EMPID")
                Response.Cookies("EMPID_DepoCFS").Value = Session("EMPID_DepoCFS")
                Response.Cookies("UserPass_DepoCFS").Value = Session("UserPass_DepoCFS")
                Response.Redirect("~/Depo/Home.aspx")
            Else
                lblErrorMsg.Text = "Invalid User Name or Password"
            End If

        Catch ex As Exception
            lblErrorMsg.Text = "Invalid User Name or Password"
        End Try
        ' strConn = "Data Source=ASHRITA-PC;Initial Catalog=HRMS;Integrated Security=True;"

    End Sub
    Private Sub rememberMe()
        If chkRememberMe.Checked Then
            Response.Cookies("EMPID_DepoCFS").Expires = DateTime.Now.AddDays(30)
            Response.Cookies("UserPass_DepoCFS").Expires = DateTime.Now.AddDays(30)
        Else
            Response.Cookies("EMPID_DepoCFS").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("UserPass_DepoCFS").Expires = DateTime.Now.AddDays(-1)
        End If
        Response.Cookies("EMPID_DepoCFS").Value = username.Text.Trim
        Response.Cookies("UserPass_DepoCFS").Value = password.Text.Trim
    End Sub
    Protected Sub password_TextChanged(sender As Object, e As System.EventArgs) Handles password.TextChanged
        lblErrorMsg.Text = ""
    End Sub

    Protected Sub username_TextChanged(sender As Object, e As System.EventArgs) Handles username.TextChanged
        lblErrorMsg.Text = ""
    End Sub
End Class

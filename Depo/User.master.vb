Imports System.Data.SqlClient
Imports System.Data

Partial Class RA_RA
    Inherits System.Web.UI.MasterPage
    Dim db As New dbOperation_Depo
    Dim strSQL As String
    Dim dt, dt1 As New DataTable


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Convert.ToInt32(Session("UserId_DepoCFS")) = 0 Then
            Response.Redirect("~/Depo/Login.aspx")
            Exit Sub
        End If

        LblUserName.Text = Convert.ToString(Session("UserName_DepoCFS"))
        lblUserID.Text = Convert.ToInt32(Session("UserId_DepoCFS"))

        Call checkRights()

    End Sub

    Private Sub checkRights()
        Try
            Dim strConn As String = ""
            Dim conn As SqlConnection
            Dim cmd As New SqlCommand
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

            Dim dt As New DataTable
            Dim da As New SqlDataAdapter
            strSQL = ""
            strSQL += "USP_FETCHING_MENU_GROUPS_Depo " & Convert.ToInt32(Session("UserId_DepoCFS")) & ""
            dt = db.sub_GetDatatable(strSQL)
            If dt.Rows.Count > 0 Then
                rptgroup.DataSource = dt
                rptgroup.DataBind()
            End If


            '==========Hide Main menu if all sub menu are not visible==============
            'If ul_Scanning.Visible = False Then
            '    div_MISReport.Visible = False
            'End If

            'If ul_Import.Visible = False Then
            '    div_Import.Visible = False
            'End If

            'If ul_Export.Visible = False Then
            '    div_Export.Visible = False
            'End If

            'If ul_BCY.Visible = False Then
            '    div_BCY.Visible = False
            'End If
            '==========END Hide Main menu if all sub menu are not visible==============

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub rptgroup_ItemDataBound(sender As Object, e As RepeaterItemEventArgs)
        Try
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim rptmaster As Repeater = TryCast(e.Item.FindControl("rptmaster"), Repeater)
                Dim hfmenudeptid As HiddenField = TryCast(e.Item.FindControl("hfmenudeptid"), HiddenField)
                strSql = ""
                strSQL += "USP_GET_MENU_DETAILS_GROUP_WISE_Depo " & Val(hfmenudeptid.Value) & "," & Convert.ToInt32(Session("UserId_DepoCFS")) & ""
                dt1 = db.sub_GetDatatable(strSQL)
                rptmaster.DataSource = dt1
                rptmaster.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class


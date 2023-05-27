Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Collections
Imports HRRecruitment.Resource.DataAccessLayer

Namespace dbClasses
    Public Class dbConnect

        Public conn As System.Data.SqlClient.SqlConnection
        Public cmd As New SqlCommand
        Public Sub sub_ConnectDB()
            Try
                Dim strConn As String = ""

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
                    Finally
                        If conn.State = ConnectionState.Open Then conn.Close()
                    End Try
                End If
                If cmd Is Nothing Then
                    cmd = New System.Data.SqlClient.SqlCommand()
                    cmd.CommandType = System.Data.CommandType.Text
                    cmd.Connection = conn
                End If

            Catch ex As Exception
                Throw ex
                If conn.State = ConnectionState.Open Then conn.Close()
                '  MsgBox("Error in procedure: " & System.Reflection.MethodBase.GetCurrentMethod.Name & vbCrLf & ex.Message.ToString)
            End Try
        End Sub

        Public Function sub_GetDatatable(ByVal strSQL As String) As DataTable
            Dim dt As New DataTable
            Try
                If conn Is Nothing OrElse conn.ConnectionString = Nothing Then
                    sub_ConnectDB()
                End If
                If conn.State = ConnectionState.Closed Then conn.Open()
                Dim cmd As New SqlCommand(strSQL, conn)
                Dim da As New SqlDataAdapter
                da.SelectCommand = cmd
                da.Fill(dt)
                If conn.State = ConnectionState.Open Then conn.Close()

            Catch ex As Exception
                'If conn.State = ConnectionState.Open Then conn.Close()
                'MsgBox("Error in procedure: " & System.Reflection.MethodBase.GetCurrentMethod.Name & vbCrLf & ex.Message.ToString)
            End Try

            Return dt
        End Function
        Public Function sub_GetDataSets(ByVal strSQL As String) As DataSet
            Dim ds As New DataSet
            Try
                If conn Is Nothing OrElse conn.ConnectionString = Nothing Then
                    sub_ConnectDB()
                End If
                If conn.State = ConnectionState.Closed Then conn.Open()
                Dim cmd As New SqlCommand
                Dim da As New SqlDataAdapter

                cmd.CommandTimeout = 120
                cmd.CommandText = strSQL
                cmd.Connection = conn
                If conn.State = ConnectionState.Closed Then conn.Open()
                da.SelectCommand = cmd
                da.Fill(ds)
                If conn.State = ConnectionState.Closed Then conn.Open()
            Catch ex As Exception
                'If conn.State = ConnectionState.Open Then conn.Close()
                '  MsgBox("Error in procedure: " & System.Reflection.MethodBase.GetCurrentMethod.Name & vbCrLf & ex.Message.ToString)
            End Try

            Return ds
        End Function
    End Class
End Namespace

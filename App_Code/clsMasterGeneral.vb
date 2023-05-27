Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class clsMasterGeneral
    Public Function UpdateData(ByVal strQuery As String)
        Return (clsCommon.ExecuteNonQuery(ConfigurationManager.AppSettings("ConString"), CommandType.Text, strQuery))
    End Function
    Public Function ExecuteData(ByVal strQuery As String) As Integer
        Return (clsCommon.ExecuteNonQuery(ConfigurationManager.AppSettings("ConString"), CommandType.Text, strQuery))
    End Function
    Public Function GetDetails_datareadder(ByVal strSQL As String) As SqlDataReader
        Return (clsCommon.ExecuteReader(ConfigurationManager.AppSettings("ConString"), CommandType.Text, strSQL))
    End Function

    Public Function GetDetails_dataset(ByVal strSQl As String) As DataSet
        Return (clsCommon.ExecuteDataset(ConfigurationManager.AppSettings("ConString"), CommandType.Text, strSQl))
    End Function
    Public Function GetDetails_dataTable(ByVal strSQl As String) As DataTable
        Return (clsCommon.ExecuteDataDatble(ConfigurationManager.AppSettings("ConString"), CommandType.Text, strSQl))
    End Function
    Public Function ExecuteDataWithCmd(ByVal strSQL As String, ByVal cmd As SqlCommand) As Integer
        Dim strConn As String = ConfigurationManager.AppSettings("ConString")
        Dim clsConnection As New SqlConnection(strConn)
        Dim i As Integer = 0
        Try
            cmd.CommandText = strSQL
            cmd.CommandType = System.Data.CommandType.Text
            cmd.Connection = clsConnection
            If clsConnection.State = ConnectionState.Closed Then clsConnection.Open()
            i = cmd.ExecuteNonQuery()
            clsConnection.Close()
        Catch ex As Exception
            clsConnection.Close()
            Return ex.Message
        End Try
        Return i
    End Function
    Public Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable
        'Dim strConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("conString").ConnectionString
        'Dim con As New SqlConnection(strConnString)
        Dim strConn As String = ConfigurationManager.AppSettings("ConString")
        Dim sda As New SqlDataAdapter
        Dim clsConnection As New SqlConnection(strConn)
        Try
            If clsConnection.State = ConnectionState.Closed Then
                clsConnection.Open()
            End If
            cmd.CommandTimeout = 180
            cmd.CommandType = CommandType.Text
            cmd.Connection = clsConnection

            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            'Response.Write(ex.Message)
            Return Nothing
        Finally
            clsConnection.Close()
            sda.Dispose()
            clsConnection.Dispose()
        End Try
    End Function

End Class

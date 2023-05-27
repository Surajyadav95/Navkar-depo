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
Imports SelandiaERP.Resource.DataAccessLayer
Imports HRRecruitment.Resource.DataAccessLayer

Namespace MyConnection

    Public Class dbAccess
        ''' Global connection object
        Public Shared connCount As Integer
        ''' Global Transaction object
        Public conn As System.Data.SqlClient.SqlConnection
        ''' Global command object
        Public trans As System.Data.SqlClient.SqlTransaction
        ''' Private variable - Connection string
        Public cmd As System.Data.SqlClient.SqlCommand
        Private strConn As String
        '''
        ' TODO: Add constructor logic here
        '
        Public Sub New()
        End Sub


        '''/Initialise and Open the connection     
        Public Sub initDB()

            strConn = ConfigurationManager.AppSettings("SqlConnString")
            If conn Is Nothing OrElse conn.ConnectionString = Nothing Then
                conn = New System.Data.SqlClient.SqlConnection(strConn)
                System.Math.Max(System.Threading.Interlocked.Increment(connCount), connCount - 1)
            End If
            If conn.State = System.Data.ConnectionState.Closed Then
                Try
                    conn.Open()
                Catch
                    conn = New System.Data.SqlClient.SqlConnection(strConn)
                    conn.Open()
                    System.Math.Max(System.Threading.Interlocked.Increment(connCount), connCount - 1)
                End Try
            End If
            If cmd Is Nothing Then
                cmd = New System.Data.SqlClient.SqlCommand()
                cmd.CommandType = System.Data.CommandType.Text
                cmd.Connection = conn
            End If
        End Sub


        ''' close database and perform cleanup   
        Public Sub closeDB()
            Try
                If Not (cmd Is Nothing) Then
                    cmd.Dispose()
                End If
            Catch generatedExceptionName As Exception
            End Try
            If Not (conn Is Nothing) Then
                If conn.State = System.Data.ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                System.Math.Max(System.Threading.Interlocked.Decrement(connCount), connCount + 1)
            End If

        End Sub

        Public Function getDS(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As DataSet
            Dim dsObj As New DataSet()
            Dim cmdObj As New SqlCommand()
            'Try
            If conn Is Nothing Then
                Me.initDB()
            End If
            If conn.State = ConnectionState.Closed Then
                Me.initDB()
            End If

            cmdObj = New SqlCommand()
            cmdObj.CommandType = CommandType.StoredProcedure
            cmdObj.CommandText = strCommandText
            cmdObj.Connection = conn

            Me.fnBindParams(cmdObj, cmdParameters)

            Dim dapObj As New SqlDataAdapter(cmdObj)
            dapObj.Fill(dsObj)
            'Catch ex As Exception
            '    ex.Message.ToString()
            '    Return dsObj
            'End Try

            Return dsObj
        End Function


        Public Function getDA(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As SqlDataAdapter
            Dim cmdObj As New SqlCommand()
            If conn.State = ConnectionState.Closed Then
                Me.initDB()
            End If

            cmdObj = New SqlCommand()
            cmdObj.CommandType = CommandType.StoredProcedure
            cmdObj.CommandText = strCommandText
            cmdObj.Connection = conn

            Me.fnBindParams(cmdObj, cmdParameters)

            Dim dapObj As New SqlDataAdapter(cmdObj)

            Return dapObj
        End Function
#Region " IsRecordExists"
        ''' <summary>
        ''' Checks for Record exists or not
        ''' Created By: Jojo George
        ''' </summary>

        Public Function IsRecordExists(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As Boolean
            Dim IsExist As Boolean = False
            Dim intCheck As Integer = 0
            Dim cmdObj As New SqlCommand()
            Try
                Me.initDB()

                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.StoredProcedure
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = conn
                Me.fnBindParams(cmdObj, cmdParameters)
                intCheck = Convert.ToInt32(cmdObj.ExecuteScalar())
                If intCheck <> 0 Then

                    IsExist = True
                End If
            Catch Ex As Exception
                Ex.Message.ToString()
            End Try
            Return IsExist

        End Function
#End Region

        Public Sub fnBindParams(ByVal sqlCommObj As SqlCommand, ByVal ParamArray cmdParameterObj As SqlParameter())
            If sqlCommObj Is Nothing Then
                Throw New ArgumentNullException()
            End If
            If cmdParameterObj Is Nothing Then
                Throw New ArgumentNullException()
            End If
            Try
                'Attaching parameters to command object
                For Each p As SqlParameter In cmdParameterObj
                    If Not (p Is Nothing) Then
                        If ((p.Direction = ParameterDirection.InputOutput) OrElse (p.Direction = ParameterDirection.Input)) And (p.Value = Nothing) Then
                            If p.DbType = DbType.Date Or p.DbType = DbType.DateTime Or p.DbType = DbType.DateTime2 Or p.DbType = DbType.DateTimeOffset Then
                                p.Value = DBNull.Value
                            ElseIf p.DbType = DbType.AnsiString Or p.DbType = DbType.AnsiStringFixedLength Or p.DbType = DbType.String Or p.DbType = DbType.StringFixedLength Then
                                p.Value = DBNull.Value
                            Else
                                p.Value = 0
                            End If
                        End If
                        sqlCommObj.Parameters.Add(p)
                    End If
                Next
            Catch Ex As Exception
                Ex.Message.ToString()
            End Try
        End Sub


        Public Function getDS(ByVal strCommandText As String) As DataSet
            Dim dsObj As New DataSet()
            Dim cmdObj As New SqlCommand()
            'Try
            Me.initDB()

            cmdObj = New SqlCommand()
            cmdObj.CommandType = CommandType.StoredProcedure
            cmdObj.CommandText = strCommandText
            cmdObj.Connection = conn

            Dim dapObj As New SqlDataAdapter(cmdObj)

            dapObj.Fill(dsObj)
            'Catch Ex As Exception
            '    Ex.Message.ToString()
            'End Try

            Return dsObj
        End Function

        Public Function getDT(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As DataTable
            Dim dtObj As New DataTable()
            Dim cmdObj As New SqlCommand()
            Try
                If conn.State = ConnectionState.Closed Then
                    Me.initDB()
                End If

                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.StoredProcedure
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = conn

                Me.fnBindParams(cmdObj, cmdParameters)

                Dim dapObj As New SqlDataAdapter(cmdObj)
                dapObj.Fill(dtObj)
            Catch ex As Exception
                ex.Message.ToString()
                Return dtObj
            End Try

            Return dtObj
        End Function


        Public Function getDT(ByVal strCommandText As String) As DataTable
            Dim dtObj As New DataTable()
            Dim cmdObj As New SqlCommand()
            Try
                Me.initDB()

                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.StoredProcedure
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = conn

                Dim dapObj As New SqlDataAdapter(cmdObj)

                dapObj.Fill(dtObj)
            Catch Ex As Exception
                Ex.Message.ToString()
            End Try

            Return dtObj
        End Function




        Public Sub BeginTrans()
            trans = conn.BeginTransaction()
            cmd.Transaction = trans
        End Sub

        Public Sub CommitTrans()
            trans.Commit()
        End Sub

        Public Sub RollbackTrans()
            trans.Rollback()
        End Sub

        Public Sub BindCombo(ByVal drpList As DropDownList, ByVal ds As DataSet)
            drpList.DataSource = ds.Tables(0)
            drpList.DataValueField = ds.Tables(0).Columns(0).ToString()
            drpList.DataTextField = ds.Tables(0).Columns(1).ToString()
            drpList.DataBind()
        End Sub

        Public Sub BindCombo(ByVal drpList As DropDownList, ByVal strCommandText As String)
            Dim ds As New DataSet()
            ds = getDS(strCommandText)

            BindCombo(drpList, ds)
        End Sub

        Public Sub BindCombo(ByVal drpList As DropDownList, ByVal strCommandText As String, ByVal ParamArray cmdParameterObj As SqlParameter())
            Dim ds As New DataSet()
            ds = getDS(strCommandText, cmdParameterObj)

            BindCombo(drpList, ds)
        End Sub


        Public Function getScalar(ByVal strCommandText As String) As Object
            Try
                Me.initDB()

                Dim cmd As New SqlCommand()
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = strCommandText
                cmd.Parameters.Clear()
                If cmd.ExecuteScalar() <> Nothing Then
                    Return cmd.ExecuteScalar()
                Else
                    Return Nothing
                End If
            Catch Ex As Exception
                Ex.Message.ToString()
                Return Nothing
            End Try
        End Function


        Public Function getScalar(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As Object
            Try
                If conn Is Nothing Then
                    initDB()
                End If
                Dim cmd As New SqlCommand()
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = strCommandText

                cmd.Parameters.Clear()
                Me.fnBindParams(cmd, cmdParameters)

                If cmd.ExecuteScalar() <> Nothing Then
                    Return cmd.ExecuteScalar()
                Else
                    Return Nothing
                End If
            Catch Ex As Exception
                Ex.Message.ToString()
                Return Nothing
            End Try
        End Function


        Public Function getScalar(ByVal strCommandText As String, ByVal IsSp As Boolean) As Object
            If Not (cmd Is Nothing) Then
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = strCommandText
                cmd.Parameters.Clear()
                If cmd.ExecuteScalar() <> Nothing Then
                    Return cmd.ExecuteScalar()
                Else
                    Return Nothing
                End If
            Else
                cmd = New SqlCommand()
                cmd.Connection = conn
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandText = strCommandText
                cmd.Parameters.Clear()
                If cmd.ExecuteScalar() <> Nothing Then
                    Return cmd.ExecuteScalar()
                Else
                    Return Nothing
                End If
            End If
        End Function


        Public Function getDSTrans(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As DataSet
            Dim TR As SqlTransaction = Nothing
            Dim dsObj As New DataSet()
            Dim cmdObj As New SqlCommand()
            Try
                If conn.State = ConnectionState.Closed Then
                    Me.initDB()
                End If

                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.StoredProcedure
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = conn

                TR = cmdObj.Connection.BeginTransaction()
                cmdObj.Transaction = TR

                Me.fnBindParams(cmdObj, cmdParameters)

                Dim dapObj As New SqlDataAdapter(cmdObj)
                dapObj.Fill(dsObj)

                TR.Commit()
            Catch ex As Exception
                TR.Rollback()
                ex.Message.ToString()
                Return dsObj
            End Try

            Return dsObj
        End Function

        Public Function getDR(ByVal strCommandText As String) As System.Data.SqlClient.SqlDataReader
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = strCommandText
            cmd.Parameters.Clear()
            Dim DR As System.Data.SqlClient.SqlDataReader
            DR = cmd.ExecuteReader()
            Return DR
        End Function


        Public Function getDR(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As System.Data.SqlClient.SqlDataReader
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = strCommandText

            'If cmd.Connection.State = ConnectionState.Closed Then
            '    cmd.Connection.Open()
            'End If

            cmd.Parameters.Clear()
            Me.fnBindParams(cmd, cmdParameters)

            Dim DR As System.Data.SqlClient.SqlDataReader
            DR = cmd.ExecuteReader()
            Return DR
        End Function

        Public Function ExceptionLog(ByVal UserId As Integer, ByVal PageName As String, ByVal Exception As String, ByVal FunctionName As String, ByVal Description As String) As Integer
            Try
                Return (SQLDatabaseOperations.ExecuteNonQuery(ConfigurationManager.AppSettings("SqlConnString"), CommandType.StoredProcedure, "SP_ExceptionLog", UserId, PageName, Exception, FunctionName, Description))
            Catch ex As Exception
            Finally

            End Try
        End Function

        Public Function ExecuteQueryText(ByVal strCommandText As String) As Integer
            Try
                Me.initDB()
                Dim cmd As New SqlCommand()
                cmd.Connection = conn
                cmd.CommandType = CommandType.Text
                cmd.CommandText = strCommandText
                cmd.Parameters.Clear()
                If cmd.ExecuteNonQuery() <> Nothing Then
                    cmd.ExecuteNonQuery()
                    Return 1
                Else
                    Return 0
                End If
            Catch Ex As Exception
                Ex.Message.ToString()
                Return Nothing
            End Try
        End Function
        '    CREATE PROCEDURE  udsp_DeleteGymLocator
        '@LOCATIONID
        'AS
        'BEGIN
        '    DELETE FROM [mstGymLocator] WHERE LOCATIONID = @LOCATIONID
        'END
        'GO
    End Class
End Namespace



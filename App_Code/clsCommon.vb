Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsCommon
    Public Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                ByVal commandType As Data.CommandType, _
                                                ByVal commandText As String, _
                                                ByVal ParamArray parameterValues() As Object) As DataSet

        Dim commandParameters As SqlParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = GetSpParameterSet(connectionString, commandText, False)

            'assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues)
        End If
        'create a command and prepare it for execution
        Dim GstCon As New SqlConnection(connectionString)
        Dim GstCommand As New SqlCommand
        Dim GstDS As New DataSet
        Dim GstDA As SqlDataAdapter
        GstCommand.CommandTimeout = 60000

        PrepareCommand(GstCommand, GstCon, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters)

        'create the DataAdapter & DataSet
        GstDA = New SqlDataAdapter(GstCommand)

        'fill the DataSet using default values for DataTable names, etc.
        GstDA.Fill(GstDS)

        'detach the SqlParameters from the command object, so they can be used again
        GstCommand.Parameters.Clear()

        'return the dataset
        Return GstDS

    End Function 'ExecuteDataset
    Public Shared Function ExecuteDataDatble(ByVal connectionString As String, _
                                                ByVal commandType As Data.CommandType, _
                                                ByVal commandText As String, _
                                                ByVal ParamArray parameterValues() As Object) As DataTable

        Dim commandParameters As SqlParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = GetSpParameterSet(connectionString, commandText, False)

            'assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues)
        End If
        'create a command and prepare it for execution
        Dim GstCon As New SqlConnection(connectionString)
        Dim GstCommand As New SqlCommand
        Dim GstDt As New DataTable
        Dim GstDA As SqlDataAdapter
        GstCommand.CommandTimeout = 60000

        PrepareCommand(GstCommand, GstCon, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters)

        'create the DataAdapter & DataSet
        GstDA = New SqlDataAdapter(GstCommand)

        'fill the DataSet using default values for DataTable names, etc.
        GstDA.Fill(GstDt)

        'detach the SqlParameters from the command object, so they can be used again
        GstCommand.Parameters.Clear()

        'return the dataset
        Return GstDt

    End Function 'ExecuteDataset
    Public Shared Function ExecuteReader(ByVal connectionString As String, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal ParamArray parameterValues() As Object) As Object
        Dim commandParameters As SqlParameter()


        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = GetSpParameterSet(connectionString, commandText, False)

            'assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues)
        End If

        'create a command and prepare it for execution
        Dim gstCon As New SqlConnection(connectionString)
        Dim gstCommand As New SqlCommand
        Dim gstReader As SqlDataReader


        PrepareCommand(gstCommand, gstCon, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters)

        'execute the command & return the results
        gstReader = gstCommand.ExecuteReader()

        'detach the SqlParameters from the command object, so they can be used again
        gstCommand.Parameters.Clear()

        Return gstReader

    End Function 'ExecuteReader
    Public Shared Function GetSpParameterSet(ByVal connectionString As String, _
                                                              ByVal spName As String, _
                                                              ByVal includeReturnValueParameter As Boolean) As SqlParameter()

        Dim cachedParameters() As SqlParameter
        Dim hashKey As String

        hashKey = connectionString + ":" + spName + IIf(includeReturnValueParameter = True, ":include ReturnValue Parameter", "")

        cachedParameters = CType(paramCache(hashKey), SqlParameter())

        If (cachedParameters Is Nothing) Then
            paramCache(hashKey) = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter)
            cachedParameters = CType(paramCache(hashKey), SqlParameter())

        End If

        Return CloneParameters(cachedParameters)

    End Function 'GetSpParameterSet
    Private Shared Sub AssignParameterValues(ByVal commandParameters() As SqlParameter, ByVal parameterValues() As Object)
        Dim i As Short
        Dim j As Short

        If (commandParameters Is Nothing) And (parameterValues Is Nothing) Then
            'do nothing if we get no data
            Return
        End If

        ' we must have the same number of values as we pave parameters to put them in
        If commandParameters.Length <> parameterValues.Length Then
            Throw New ArgumentException("Parameter count does not match Parameter Value count.")
        End If

        'value array
        j = commandParameters.Length - 1
        For i = 0 To j
            commandParameters(i).Value = parameterValues(i)
        Next

    End Sub 'AssignParameterValues
    Private Shared Sub PrepareCommand(ByVal command As SqlCommand, _
                                       ByVal connection As SqlConnection, _
                                       ByVal transaction As SqlTransaction, _
                                       ByVal commandType As CommandType, _
                                       ByVal commandText As String, _
                                       ByVal commandParameters() As SqlParameter)

        'if the provided connection is not open, we will open it
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If

        'associate the connection with the command
        command.Connection = connection

        'set the command text (stored procedure name or SQL statement)
        command.CommandText = commandText

        'if we were provided a transaction, assign it.
        If Not (transaction Is Nothing) Then
            command.Transaction = transaction
        End If

        'set the command type
        command.CommandType = commandType

        'attach the command parameters if they are provided
        If Not (commandParameters Is Nothing) Then
            AttachParameters(command, commandParameters)
        End If

        Return
    End Sub 'PrepareCommand
    Private Shared Sub AttachParameters(ByVal command As SqlCommand, ByVal commandParameters() As SqlParameter)
        Dim p As SqlParameter
        For Each p In commandParameters
            'check for derived output value with no value assigned
            If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                p.Value = Nothing
            End If
            command.Parameters.Add(p)
        Next p
    End Sub 'AttachParameters
    Private Shared paramCache As Hashtable = Hashtable.Synchronized(New Hashtable)

    '*********************************************************************
    '
    ' Execute a SqlCommand (that returns no resultset) against the specified SqlConnection 
    ' using the provided parameters.
    ' 
    ' e.g.:  
    '  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
    ' 
    ' param name="connection" a valid SqlConnection 
    ' param name="commandType" the CommandType (stored procedure, text, etc.) 
    ' param name="commandText" the stored procedure name or T-SQL command 
    ' param name="commandParameters" an array of SqlParamters used to execute the command 
    ' returns an int representing the number of rows affected by the command
    '
    '*********************************************************************
    Public Overloads Shared Function ExecuteNonQuery(ByVal connectionString As String, _
                                                    ByVal commandType As CommandType, _
                                                    ByVal commandText As String, _
                                                    ByVal ParamArray parameterValues() As Object) As Integer

        Dim commandParameters As SqlParameter()

        'if we receive parameter values, we need to figure out where they go
        If Not (parameterValues Is Nothing) And parameterValues.Length > 0 Then
            'pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)

            commandParameters = GetSpParameterSet(connectionString, commandText, False)

            'assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues)

        End If
        'create a command and prepare it for execution
        Dim GstCon As New SqlConnection(connectionString)
        Dim GstCommand As New SqlCommand
        Dim GstDS As New DataSet
        Dim GstDA As SqlDataAdapter
        Dim retval As Integer

        PrepareCommand(GstCommand, GstCon, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters)

        'finally, execute the command.
        retval = GstCommand.ExecuteNonQuery()

        'detach the SqlParameters from the command object, so they can be used again
        GstCommand.Parameters.Clear()
        Return retval
    End Function 'ExecuteNonQuery
    Private Shared Function DiscoverSpParameterSet(ByVal connectionString As String, _
                                                          ByVal spName As String, _
                                                          ByVal includeReturnValueParameter As Boolean, _
                                                          ByVal ParamArray parameterValues() As Object) As SqlParameter()

        Dim cn As New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand(spName, cn)
        Dim discoveredParameters() As SqlParameter

        Try
            cn.Open()
            cmd.CommandType = CommandType.StoredProcedure
            SqlCommandBuilder.DeriveParameters(cmd)
            If Not includeReturnValueParameter Then
                cmd.Parameters.RemoveAt(0)
            End If

            discoveredParameters = New SqlParameter(cmd.Parameters.Count - 1) {}
            cmd.Parameters.CopyTo(discoveredParameters, 0)
        Finally
            cmd.Dispose()
            cn.Dispose()

        End Try

        Return discoveredParameters

    End Function 'DiscoverSpParameterSet
    'deep copy of cached SqlParameter array
    Private Shared Function CloneParameters(ByVal originalParameters() As SqlParameter) As SqlParameter()

        Dim i As Short
        Dim j As Short = originalParameters.Length - 1
        Dim clonedParameters(j) As SqlParameter

        For i = 0 To j
            clonedParameters(i) = CType(CType(originalParameters(i), ICloneable).Clone, SqlParameter)
        Next

        Return clonedParameters
    End Function 'CloneParameters
End Class

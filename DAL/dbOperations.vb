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
Namespace MyConnection
    Public Class dbOperations
        Public Shared connCount As Integer
        Public conn As System.Data.SqlClient.SqlConnection
        Public trans As System.Data.SqlClient.SqlTransaction
        Public cmd As System.Data.SqlClient.SqlCommand
        Private strConn As String

        Public Sub initDB()
            ' strConn = "Data Source=ASHRITA-PC;Initial Catalog=HRMS;Integrated Security=True;"
            strConn = ConfigurationManager.ConnectionStrings("SqlConnString").ConnectionString
            If conn Is Nothing OrElse conn.ConnectionString = Nothing Then
                conn = New System.Data.SqlClient.SqlConnection(strConn)
                System.Math.Max(System.Threading.Interlocked.Increment(connCount), connCount - 1)
            End If
            If conn.State = System.Data.ConnectionState.Closed Then
                Try
                    conn.Open()
                Catch Ex As Exception
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

        Public Function geturl(ByVal strCommandText As String, ByVal strh1tag As String) As String
            Dim cmd As New SqlCommand()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = strCommandText
            cmd.Connection = conn
            Dim h1tag As New SqlParameter("@h1tag", SqlDbType.VarChar)
            h1tag.Value = strh1tag
            cmd.Parameters.Add(h1tag)
            Dim result As String = Convert.ToString(cmd.ExecuteScalar())
            Return result
        End Function

        Public Function M_AddEditDelete(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As Integer
            Dim TR As SqlTransaction = Nothing
            Dim cmdObj As New SqlCommand()
            Try
                'if (conn.State == ConnectionState.Closed)
                Me.initDB()

                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.StoredProcedure
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = conn
                TR = cmdObj.Connection.BeginTransaction()
                cmdObj.Transaction = TR
                Me.fnBindParams(cmdObj, cmdParameters)

                Dim iRow As Integer = cmdObj.ExecuteNonQuery()

                TR.Commit()

                Return iRow
            Catch ex As Exception
                TR.Rollback()
                ex.Message.ToString()
                Return 0
            Finally
                cmdObj.Dispose()
                Me.closeDB()
            End Try
        End Function

        Public Function M_InsertMultipleRecords(ByVal strCommandText As String, ByVal objDT As DataTable, ByVal ParamArray cmdParameters As SqlParameter()) As DataTable
            Dim TR As SqlTransaction = Nothing
            Dim cmdObj As New SqlCommand()
            Dim objSDA As New SqlDataAdapter()
            Try
                'if (conn.State == ConnectionState.Closed)
                Me.initDB()

                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.StoredProcedure
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = conn
                TR = cmdObj.Connection.BeginTransaction()
                cmdObj.Transaction = TR
                Me.fnBindParams(cmdObj, cmdParameters)

                objSDA.InsertCommand = cmdObj
                objSDA.Update(objDT)

                TR.Commit()

                Return objDT
            Catch ex As Exception
                TR.Rollback()
                ex.Message.ToString()
                Return Nothing
            Finally

                cmdObj.Dispose()
                Me.closeDB()
            End Try
        End Function

        Public Function M_AddEditDelete(ByVal strCommandText As String) As Integer
            Dim TR As SqlTransaction = Nothing
            Dim cmdObj As New SqlCommand()
            Try
                'if (conn.State == ConnectionState.Closed)
                Me.initDB()

                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.Text
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = conn
                TR = cmdObj.Connection.BeginTransaction()
                cmdObj.Transaction = TR

                Dim iRow As Integer = cmdObj.ExecuteNonQuery()

                TR.Commit()

                Return iRow
            Catch ex As Exception
                TR.Rollback()
                ex.Message.ToString()
                Return 0
            Finally

                cmdObj.Dispose()
                Me.closeDB()
            End Try
        End Function

        Public Function getDS(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As DataSet
            Dim dsObj As New DataSet()
            Dim cmdObj As New SqlCommand()
            Try
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
                dapObj.SelectCommand.CommandTimeout = 0
                dapObj.Fill(dsObj)
            Catch ex As Exception
                ex.Message.ToString()
                Return dsObj
            End Try

            Return dsObj
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

        Public Sub Fill_DROPDOWN_LIST(ByVal DDL As DropDownList, ByVal SpName As String, ByVal intLanguageColumnNo As Integer)
            Dim ds As DataSet = Me.getDS(SpName)

            If Not (ds Is Nothing) Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        'if (ds.Tables[0].Rows[0][intLanguageColumnNo].ToString().Length > 0)
                        '{
                        DDL.ClearSelection()
                        DDL.Items.Clear()
                        'DataRow dr = ds.Tables[0].NewRow();
                        'dr[0] = "0";
                        'dr[1] = "--Select--";
                        'ds.Tables[0].Rows.InsertAt(dr, 0);

                        DDL.DataSource = ds.Tables(0)
                        DDL.DataTextField = ds.Tables(0).Columns(intLanguageColumnNo).ToString()
                        DDL.DataValueField = ds.Tables(0).Columns(0).ToString()
                        '}
                        'else
                        '{
                        '    DDL.Items.Clear();
                        '    DDL.Items.Add(new ListItem("--Select--","0"));                                                 
                        '} 
                        DDL.DataBind()
                    End If
                End If
            End If
        End Sub

        Public Sub Fill_DROPDOWN_LIST_WithPrm(ByVal DDL As DropDownList, ByVal intLanguageColumnNo As Integer, ByVal SpName As String, ByVal ParamArray cmdParameterObj As SqlParameter())
            Dim ds As DataSet = Me.getDS(SpName, cmdParameterObj)

            If Not (ds Is Nothing) Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        'if (ds.Tables[0].Rows[0][intLanguageColumnNo].ToString().Length > 0)
                        '{
                        DDL.ClearSelection()
                        DDL.Items.Clear()
                        'DataRow dr = ds.Tables[0].NewRow();
                        'dr[0] = "0";
                        'dr[1] = "--Select--";
                        'ds.Tables[0].Rows.InsertAt(dr, 0);

                        DDL.DataSource = ds.Tables(0)
                        DDL.DataTextField = ds.Tables(0).Columns(intLanguageColumnNo).ToString()
                        DDL.DataValueField = ds.Tables(0).Columns(0).ToString()
                        '}
                        'else
                        '{
                        '    DDL.Items.Clear();
                        '    DDL.Items.Add(new ListItem("--Select--","0"));                                                 
                        '} 
                        DDL.DataBind()
                    End If
                End If
            End If
        End Sub

        Public Sub Fill_ListBox(ByVal LST As ListBox, ByVal SpName As String, ByVal intLanguageColumnNo As Integer)
            Dim ds As DataSet = Me.getDS(SpName)

            If Not (ds Is Nothing) Then
                If ds.Tables.Count > 0 Then
                    LST.Items.Clear()
                    If ds.Tables(0).Rows.Count > 0 Then
                        'if (ds.Tables[0].Rows[0][intLanguageColumnNo].ToString().Length > 0)
                        '{
                        'LST.ClearSelection();
                        'LST.Items.Clear();
                        '''/DataRow dr = ds.Tables[0].NewRow();
                        '''/dr[0] = "0";
                        '''/dr[1] = "--Select--";
                        '''/ds.Tables[0].Rows.InsertAt(dr, 0);

                        LST.DataSource = ds.Tables(0)
                        LST.DataTextField = ds.Tables(0).Columns(intLanguageColumnNo).ToString()
                        LST.DataValueField = ds.Tables(0).Columns(0).ToString()
                        '}
                        'else
                        '{

                        ' LST.Items.Add(new ListItem(ds.Tables[0].Rows[intLanguageColumnNo].ToString(), "0"));                                                 
                        '} 
                        LST.DataBind()
                    End If
                End If
            End If
        End Sub

        Public Sub Fill_ListBox_WithPrm(ByVal LST As ListBox, ByVal intLanguageColumnNo As Integer, ByVal SpName As String, ByVal ParamArray cmdParameterObj As SqlParameter())
            Dim ds As DataSet = Me.getDS(SpName, cmdParameterObj)

            If Not (ds Is Nothing) Then
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        'if (ds.Tables[0].Rows[0][intLanguageColumnNo].ToString().Length > 0)
                        '{
                        LST.ClearSelection()
                        LST.Items.Clear()
                        'DataRow dr = ds.Tables[0].NewRow();
                        'dr[0] = "0";
                        'dr[1] = "--Select--";
                        'ds.Tables[0].Rows.InsertAt(dr, 0);

                        LST.DataSource = ds.Tables(0)
                        LST.DataTextField = ds.Tables(0).Columns(intLanguageColumnNo).ToString()
                        LST.DataValueField = ds.Tables(0).Columns(0).ToString()
                        '}
                        'else
                        '{
                        '    LST.Items.Clear();
                        '    LST.Items.Add(new ListItem("--Select--","0"));                                                 
                        '} 
                        LST.DataBind()
                    End If
                End If
            End If
        End Sub

        Public Shared Function DeleteFile(ByVal FilePathWithoutVirtualPath As String) As Boolean
            Dim strUploadDel As String = HttpContext.Current.Server.MapPath("upload/" + FilePathWithoutVirtualPath)
            Dim fiExistFile As New FileInfo(strUploadDel)

            If fiExistFile.Exists Then
                'strUploadDel = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["VirtualFolder"].ToString() + "\\" + FilePathWithoutVirtualPath);
                Dim fiFile As New FileInfo(strUploadDel)
                fiFile.Delete()

                Return True
            End If
            Return False
        End Function

        Public Function BindDatalist(ByVal DataList1 As DataList, ByVal CurrIndex As Integer, ByVal PageIndex As Integer, ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As String

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

                Me.fnBindParams(cmdObj, cmdParameters)

                Dim dapObj As New SqlDataAdapter(cmdObj)


                dapObj.Fill(dsObj, CurrIndex, PageIndex, "mstSiteUser")
                DataList1.DataSource = dsObj
                DataList1.DataBind()

                Return dsObj.Tables(0).Rows(0)(0).ToString()
            Catch ex As Exception
                ex.Message.ToString()
                Return "fs"
            End Try
        End Function


        Public Sub BindYear(ByVal drpYearCombo As DropDownList, ByVal drpMonthCombo As DropDownList)
            Dim lt As ListItem = New ListItem
            Dim i As Integer = DateTime.Now.Year
            Do While (i >= 2013)
                lt = New ListItem
                lt.Text = i.ToString
                lt.Value = i.ToString
                drpYearCombo.Items.Add(lt)
                i = (i - 1)
            Loop

            drpYearCombo.Items.Insert(0, "--Select--")
            drpYearCombo.Items.Item(0).Value = 0

            Dim ltmonth As ListItem = New ListItem
            Dim month As Date = Convert.ToDateTime("1/1/2000")
            For j = 0 To 11
                Dim NextMont As DateTime = month.AddMonths(j)
                ltmonth = New ListItem
                ltmonth.Text = NextMont.ToString("MMM")
                ltmonth.Value = NextMont.Month.ToString()
                drpMonthCombo.Items.Add(ltmonth)
            Next
            drpMonthCombo.Items.Insert(0, "--Select--")
            drpMonthCombo.Items.Item(0).Value = 0
        End Sub


#Region " File Upload"
        Public Function F_FileUpload(ByVal fup As FileUpload) As StringBuilder
            Dim strUpload As New StringBuilder()
            Try
                Dim strFileName As String = fup.FileName

                strUpload.Append(HttpContext.Current.Server.MapPath("upload/" + strFileName))

                If fup.FileName.Length > 0 Then
                    Dim fiFile As New FileInfo(strUpload.ToString())
                    If fiFile.Exists Then
                        '"File already exists, please rename the file.";
                        Dim strExtFile As String = Path.GetExtension(strFileName)
                        strFileName = strFileName.Substring(0, strFileName.LastIndexOf("."c))
                        strFileName = strFileName + DateTime.Now.ToString("d/M/yy H:m:s") + DateTime.Now.Millisecond.ToString()
                        strFileName = strFileName.Replace(":", "").Replace("/", "").Replace("\", "").Replace(" ", "")
                        strFileName = strFileName + strExtFile
                    End If
                    strUpload.Remove(0, strUpload.Length)
                    strUpload.Append(HttpContext.Current.Server.MapPath("upload/" + strFileName))

                    If strUpload.Length > 0 Then
                        fup.SaveAs(strUpload.ToString())
                        strUpload.Remove(0, strUpload.Length)
                        strUpload.Append("\upload\" + strFileName)
                    End If
                Else
                    strUpload.Remove(0, strUpload.Length)
                End If

                Return strUpload
            Catch Ex As Exception
                Return strUpload
            End Try
        End Function

        Public Function FileUpload(ByVal fup As FileUpload, ByVal UploadFolder As String) As StringBuilder
            Dim strUpload As New StringBuilder()
            Try
                Dim strFileName As String = fup.FileName
                strUpload.Append(HttpContext.Current.Server.MapPath(UploadFolder & "/" + strFileName))
                If fup.FileName.Length > 0 Then
                    Dim fiFile As New FileInfo(strUpload.ToString())
                    If fiFile.Exists Then
                        '"File already exists, please rename the file.";
                        Dim strExtFile As String = Path.GetExtension(strFileName)
                        strFileName = strFileName.Substring(0, strFileName.LastIndexOf("."c))
                        strFileName = strFileName + DateTime.Now.ToString("d/M/yy H:m:s") + DateTime.Now.Millisecond.ToString()
                        strFileName = strFileName.Replace(":", "").Replace("/", "").Replace("\", "").Replace(" ", "")
                        strFileName = strFileName + strExtFile
                    End If
                    strUpload.Remove(0, strUpload.Length)
                    strUpload.Append(HttpContext.Current.Server.MapPath(UploadFolder & "/" + strFileName))

                    If strUpload.Length > 0 Then
                        fup.SaveAs(strUpload.ToString())
                        strUpload.Remove(0, strUpload.Length)
                        strUpload.Append("\" & UploadFolder & "\" + strFileName)
                    End If
                Else
                    strUpload.Remove(0, strUpload.Length)
                End If

                Return strUpload
            Catch Ex As Exception
                Return strUpload
            End Try
        End Function
#End Region

        Public Function ExceptionLog(ByVal UserId As Integer, ByVal PageName As String, ByVal Exception As String, ByVal FunctionName As String, ByVal Description As String) As Integer
            Try
                Return (SQLDatabaseOperations.ExecuteNonQuery(ConfigurationManager.AppSettings("SqlConnString"), CommandType.StoredProcedure, "SP_ExceptionLog", UserId, PageName, Exception, FunctionName, Description))
            Catch ex As Exception
            Finally

            End Try
        End Function


        ' #Region
        'Sachin- 17-june-2014
        Public Function M_AddWithOutPutParameter(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As Integer
            Dim TR As SqlTransaction = Nothing
            Dim cmdObj As New SqlCommand()
            Try
                'if (conn.State == ConnectionState.Closed)
                Me.initDB()
                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.StoredProcedure
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = conn
                TR = cmdObj.Connection.BeginTransaction()
                cmdObj.Transaction = TR
                Me.fnBindParams(cmdObj, cmdParameters)
                Dim retValParam As New SqlParameter("@ID", Data.SqlDbType.BigInt)
                retValParam.Direction = Data.ParameterDirection.Output
                cmdObj.Parameters.Add(retValParam)
                Dim iRow As Integer = cmdObj.ExecuteNonQuery()
                TR.Commit()
                iRow = cmdObj.Parameters("@ID").Value.ToString()
                Return iRow
            Catch ex As Exception
                TR.Rollback()
                ex.Message.ToString()
                Return 0
            Finally
                cmdObj.Dispose()
                Me.closeDB()
            End Try
        End Function

        Public Function getDSByQuery(ByVal strCommandText As String) As DataSet
            Dim dsObj As New DataSet()
            Dim cmdObj As New SqlCommand()
            Try
                Me.initDB()
                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.Text
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = conn
                Dim dapObj As New SqlDataAdapter(cmdObj)
                dapObj.Fill(dsObj)
                Me.closeDB()
            Catch Ex As Exception
                Ex.Message.ToString()
            End Try

            Return dsObj
        End Function
        '#End Region

        Public Function sub_GetDatatable(ByVal strSQL As String) As DataTable
            Dim dt As New DataTable
            Try
                If conn Is Nothing Then
                    Me.initDB()
                End If
                If conn.State = ConnectionState.Closed Then conn.Open()
                Dim cmd As New SqlCommand(strSQL, conn)
                Dim da As New SqlDataAdapter
                cmd.CommandTimeout = 120
                da.SelectCommand = cmd
                da.Fill(dt)
                If conn.State = ConnectionState.Open Then conn.Close()

            Catch ex As Exception

            End Try

            Return dt
        End Function
        Public Function sub_GetDataSet(ByVal strSQL As String) As DataSet
            Dim ds As New DataSet
            Try
                If conn Is Nothing Then
                    Me.initDB()
                End If
                If conn.State = ConnectionState.Closed Then conn.Open()
                Dim cmd As New SqlCommand(strSQL, conn)
                Dim da As New SqlDataAdapter
                cmd.CommandTimeout = 120
                da.SelectCommand = cmd
                da.Fill(ds)
                If conn.State = ConnectionState.Open Then conn.Close()

            Catch ex As Exception

            End Try

            Return ds
        End Function
        'Public Function sub_GetDatatable(ByVal strSQL As String) As DataTable
        '    If conn Is Nothing Then
        '        Me.initDB()
        '    End If

        '    Dim dt As New DataTable
        '    If conn.State = ConnectionState.Closed Then conn.Open()
        '    Dim cmd As New SqlCommand(strSQL, conn)
        '    Using sdr As SqlDataReader = cmd.ExecuteReader()
        '        'Create a new DataSet.
        '        Dim dsCustomers As New DataSet()
        '        dsCustomers.Tables.Add()

        '        'Load DataReader into the DataTable.
        '        dsCustomers.Tables(0).Load(sdr)
        '        dt = dsCustomers.Tables(0)
        '    End Using
        '    conn.Close()
        '    Return dt
        'End Function

    End Class
End Namespace


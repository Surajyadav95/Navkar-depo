Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports HRRecruitment.MyConnection

Namespace MyTransaction
    Public Class AddEditDelete
#Region " ADD EDIT DELETE"

        Public Sub M_AddEditDelete(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter())
            Dim TR As SqlTransaction = Nothing
            Dim db As New dbAccess()
            Dim cmdObj As New SqlCommand()
            Try
                db.initDB()
                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.StoredProcedure
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = db.conn
                TR = cmdObj.Connection.BeginTransaction()
                cmdObj.Transaction = TR
                db.fnBindParams(cmdObj, cmdParameters)

                Dim iRow As Integer = cmdObj.ExecuteNonQuery()
                TR.Commit()

            Catch ex As Exception
                TR.Rollback()
                ex.Message.ToString()
                Throw ex
            Finally

                cmdObj.Dispose()
                db.closeDB()
            End Try
        End Sub
#End Region

#Region " Get Rating"

        Public Function M_GetRating(ByVal strCommandText As String, ByVal ParamArray cmdParameters As SqlParameter()) As Double
            Dim iGrossRate As Double = 0
            Dim db As New dbAccess()
            Dim cmdObj As New SqlCommand()
            Try
                db.initDB()
                cmdObj = New SqlCommand()
                cmdObj.CommandType = CommandType.StoredProcedure
                cmdObj.CommandText = strCommandText
                cmdObj.Connection = db.conn
                db.fnBindParams(cmdObj, cmdParameters)
                iGrossRate = Convert.ToDouble(cmdObj.ExecuteScalar())

                Return iGrossRate
            Catch ex As Exception
                ex.Message.ToString()
                Return -1
            Finally

                cmdObj.Dispose()
                db.closeDB()
            End Try
        End Function
#End Region

    End Class
End Namespace


Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsSalesPerson
    Dim strSql As String
    Dim ObjMasterGeneral As New clsMasterGeneral
    'Public Function getProjectDetails(ByVal salespersonID As Integer) As DataTable
    '    Dim dtTemp As DataTable = Nothing
    '    Try
    '        'strSql = ""
    '        'strSql += " select ProjectCode,ProjectName,''ContactPerson,''ContactNo , AddedBy from Master_Projects where "
    '        'strSql += " ProjectCode<>'' and ProjectCode is not null and projectName<>'' and projectcode IN (select projectcode from SalesPersonProjects where salespersonID=" & salespersonID & ")   order by ProjectName "

    '        'dtTemp = ObjMasterGeneral.GetDetails_dataset(strSql).Tables(0)
    '    Catch ex As Exception

    '    End Try

    '    'Return dtTemp

    'End Function
    Public Function SaveMessage() As String

        Return "Record Saved Successfully."
    End Function

    Public Function UpdateMessage() As String

        Return "Record Updated Successfully."
    End Function
    Public Function NoRecordMessage() As String

        Return " No Records Found. "
    End Function

    Public Function InvalidDate() As String

        Return " Invalid Date selection. "
    End Function
    Public Function DeleteRecordMessage() As String

        Return " Record Deleted Successfully."
    End Function
    Public Function DeleteFileMessage() As String

        Return " File Deleted Successfully."
    End Function
End Class

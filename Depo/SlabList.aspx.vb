Imports System.Data
Imports System.IO
Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim SlabID, WHIDView As String
    Dim ed As New clsEncodeDecode
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'db.sub_ExecuteNonQuery("delete from TEMP_BTT_BACK where USER_ID=" & Session("UserId_DepoCFS") & "")
            getItemList()
        End If
    End Sub
    Protected Sub getItemList()
        Try
            Dim SlabID As String = 0
            strSql = ""
            strSql += "USP_EYARD_SLAB_ENTRY '" & Request.QueryString("SlabID") & "'"
            dt = db.sub_GetDatatable(strSql)
            grdLotList.DataSource = dt
            grdLotList.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function
     
    
End Class

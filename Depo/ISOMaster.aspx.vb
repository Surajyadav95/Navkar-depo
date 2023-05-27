Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim Count As Integer = 0
    Dim checkValue As Integer = 0
    Dim SLID As String = ""
    Dim AccountID, AccountIDView As String
    Dim ed As New clsEncodeDecode


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("UserIDPRE_Bond") Is Nothing Then
        '    Session("UserIDPRE_Bond") = Request.Cookies("UserIDPRE_Bond").Value
        '    'Session("Dept") = Request.Cookies("Dept").Value
        '    Session("UserNamePRE_Bond") = Request.Cookies("UserNamePRE_Bond").Value
        '    ''Session("PROFILEURL") = Request.Cookies("PROFILEURL").Value
        '    'Session("Location") = Request.Cookies("Location").Value
        '    ''Session("LOcationId") = Request.Cookies("LOcationId").Value
        '    'Session("ID") = Response.Cookies("ID").Value
        '    'Session("CompID") = Response.Cookies("CompID").Value
        '    'Session("Workyear") = Response.Cookies("Workyear").Value
        'End If
        If Not IsPostBack Then
            Filldropdown()
            fill_grid()
        End If
    End Sub
    Private Sub fill_grid()
        Try
            Dim dt As New DataTable
            Dim strSQL As String
            strSQL = ""
            strSQL = "SELECT ISOID AS [ISO ID],ISOCode as [ISO Code],ContainerHT as [Container HT], ContainerType as [Type],CtrSize as [Size],CtrWidth as [Width],"
            strSQL += " Weight ,ShortName as [Short Name] FROM ISOCodes  ORDER BY [ISO ID] DESC"
            dt1 = db.sub_GetDatatable(strSQL)
            GrdISoFill.DataSource = dt1
            GrdISoFill.DataBind()

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql = "SELECT ContainerTypeID , ContainerType  FROM ContainerType"
            dt = db.sub_GetDatatable(strSql)
            If (dt.Rows.Count > 0) Then
                ddlCOntainerType.DataSource = dt
                ddlCOntainerType.DataTextField = "ContainerType"
                ddlCOntainerType.DataValueField = "ContainerTypeID"
                ddlCOntainerType.DataBind()
                ddlCOntainerType.Items.Insert(0, New ListItem("--Select--", 0))
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql = "SELECT ISOCode FROM ISOCodes WHERE ISOCode = '" & Trim(txtISOCode.Text) & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This ISO Code is Already exist');", True)
                txtISOCode.Focus()
                Exit Sub
            End If

            strSql = ""
            strSql = " SELECT ISNULL(MAX(ISOID ),0) FROM ISOCodes "
            dt = db.sub_GetDatatable(strSql)
            txtIsoID.Text = Val(dt.Rows(0)(0)) + 1
            strSql = ""
            strSql = "SP_ISOMaster'" & Trim(txtIsoID.Text) & "','" & Trim(txtcontainerHt.Text) & "','" & Trim(ddlContainerSize.SelectedItem.Text) & "','" & Trim(ddlCOntainerType.SelectedItem.Text) & "'"
            strSql += " ,'" & Trim(txtContainerWidth.Text) & "','" & Trim(txtISOCode.Text) & "','" & Trim(txtShortName.Text) & "','" & Trim(txtTareWeight.Text) & "', '" & Session("UserId_DepoCFS") & "', '" & Format(Now, "yyyy-MM-dd HH:mm") & "'"
            db.sub_ExecuteNonQuery(strSql)
            lblSession.Text = "Record saved successfully "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            UpdatePanel3.Update()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

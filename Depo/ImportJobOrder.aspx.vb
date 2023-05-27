Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient
Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1 As DataTable
    Dim db As New dbOperation_Domestic
    Dim ds As DataSet
    Dim JONO, AccountIDView As String
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
            Call sub_CreateTable()
            txtJODate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtBEDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtDoValidDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")

            If Not (Request.QueryString("JONoEdit") = "") Then
                JONO = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("JONoEdit")))
                strSql = ""
                strSql += "USP_MODIFY_JONO " & JONO & ""
                ds = db.sub_GetDataSets(strSql)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtJONo.Text = Trim(ds.Tables(0).Rows(0)("JONO"))
                    txtJODate.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("JODATE"))).ToString("yyyy-MM-ddTHH:mm")
                    ddlJOType.SelectedValue = Val(ds.Tables(0).Rows(0)("JOTYPE"))
                    txtIGMNo.Text = Trim(ds.Tables(0).Rows(0)("IGMNO"))
                    txtItemNo.Text = Trim(ds.Tables(0).Rows(0)("ITEMNO"))
                    ddlCustomer.SelectedValue = Val(ds.Tables(0).Rows(0)("CUSTOMERID"))
                    ddlCHA.SelectedValue = Val(ds.Tables(0).Rows(0)("CHA"))
                    ddlImporter.SelectedValue = Val(ds.Tables(0).Rows(0)("IMPORTER"))
                    txtBENo.Text = Trim(ds.Tables(0).Rows(0)("BENO"))
                    If Not Trim(ds.Tables(0).Rows(0)("BEDATE")) = "" Then
                        txtBEDate.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("BEDATE")) & "").ToString("yyyy-MM-ddTHH:mm")
                    Else
                        txtBEDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")

                    End If
                    txtBEAdress.Text = Trim(ds.Tables(0).Rows(0)("BEADDRESS"))
                    txtPONo.Text = Trim(ds.Tables(0).Rows(0)("PONO"))
                    txtLotNo.Text = Trim(ds.Tables(0).Rows(0)("LOTNO"))
                    txtRefJONo.Text = Trim(ds.Tables(0).Rows(0)("REFJONO"))
                    txtRemarks.Text = Trim(ds.Tables(0).Rows(0)("REMARKS"))                    
                End If
                If ds.Tables(1).Rows.Count > 0 Then
                    FillGrid(ds.Tables(1))
                End If
                btnSave.Text = "Update"
                pnlJOType.Enabled = False
            End If
            If Not (Request.QueryString("JONoView") = "") Then
                JONO = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("JONoView")))
                strSql = ""
                strSql += "USP_MODIFY_JONO " & JONO & ""
                ds = db.sub_GetDataSets(strSql)
                If ds.Tables(0).Rows.Count > 0 Then
                    txtJONo.Text = Trim(ds.Tables(0).Rows(0)("JONO"))
                    txtJODate.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("JODATE"))).ToString("yyyy-MM-ddTHH:mm")
                    ddlJOType.SelectedValue = Val(ds.Tables(0).Rows(0)("JOTYPE"))
                    txtIGMNo.Text = Trim(ds.Tables(0).Rows(0)("IGMNO"))
                    txtItemNo.Text = Trim(ds.Tables(0).Rows(0)("ITEMNO"))
                    ddlCustomer.SelectedValue = Val(ds.Tables(0).Rows(0)("CUSTOMERID"))
                    ddlCHA.SelectedValue = Val(ds.Tables(0).Rows(0)("CHA"))
                    ddlImporter.SelectedValue = Val(ds.Tables(0).Rows(0)("IMPORTER"))
                    txtBENo.Text = Trim(ds.Tables(0).Rows(0)("BENO"))
                    If Not Trim(ds.Tables(0).Rows(0)("BEDATE")) = "" Then
                        txtBEDate.Text = Convert.ToDateTime(Trim(ds.Tables(0).Rows(0)("BEDATE")) & "").ToString("yyyy-MM-ddTHH:mm")
                    Else
                        txtBEDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")

                    End If
                    txtBEAdress.Text = Trim(ds.Tables(0).Rows(0)("BEADDRESS"))
                    txtPONo.Text = Trim(ds.Tables(0).Rows(0)("PONO"))
                    txtLotNo.Text = Trim(ds.Tables(0).Rows(0)("LOTNO"))
                    txtRefJONo.Text = Trim(ds.Tables(0).Rows(0)("REFJONO"))
                    txtRemarks.Text = Trim(ds.Tables(0).Rows(0)("REMARKS"))
                End If
                If ds.Tables(1).Rows.Count > 0 Then
                    FillGrid(ds.Tables(1))
                End If
                Panel1.Enabled = False
                Panel2.Enabled = False
                btnSave.Visible = False
            End If
        End If
    End Sub
    Private Sub FillGrid(dt As DataTable)
        Try
            Dim dtDomContainer As New DataTable
            Dim intRows As Integer = 0

            dtDomContainer = DirectCast(Session("table_DomesticContainer"), DataTable)
            intRows = dtDomContainer.Rows.Count
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dtRow As DataRow = dtDomContainer.NewRow

                dtRow.Item("ContainerNo") = Trim(dt.Rows(i)("CONTAINERNO") & "")
                dtRow.Item("Size") = Val(dt.Rows(i)("SIZE") & "")
                dtRow.Item("Type") = Trim(dt.Rows(i)("Cargotype") & "")
                dtRow.Item("TypeID") = Val(dt.Rows(i)("CARGOTYPEID") & "")
                dtRow.Item("CType") = Trim(dt.Rows(i)("CONTAINERTYPE") & "")
                dtRow.Item("CTypeID") = Val(dt.Rows(i)("CONTAINERTYPEID") & "")
                dtRow.Item("From") = Trim(dt.Rows(i)("FROMLOCATION") & "")
                dtRow.Item("FromID") = Val(dt.Rows(i)("FROMLOCATIONID") & "")
                dtRow.Item("To") = Trim(dt.Rows(i)("TOLOCATION") & "")
                dtRow.Item("ToID") = Val(dt.Rows(i)("TOLOCATIONID") & "")
                If Val(dt.Rows(0)("LINEID") & "") = 0 Then
                    dtRow.Item("Line") = ""
                Else
                    dtRow.Item("Line") = Trim(dt.Rows(i)("SLName") & "")
                End If

                dtRow.Item("LineID") = Val(dt.Rows(i)("LINEID") & "")
                dtRow.Item("Pkgs") = Trim(dt.Rows(i)("PKGS") & "")
                dtRow.Item("PkgsType") = Trim(dt.Rows(i)("Package") & "")
                dtRow.Item("PkgsTypeID") = Val(dt.Rows(i)("PKGSTYPE") & "")
                dtRow.Item("Weight") = Trim(dt.Rows(i)("WEIGHT") & "")
                dtRow.Item("Class") = Trim(dt.Rows(i)("CONTAINERNO") & "")
                dtRow.Item("UNNo") = Trim(dt.Rows(i)("UNNo") & "")
                dtRow.Item("DoValidDate") = Convert.ToDateTime(Trim(dt.Rows(i)("DOVALIDDATE") & "")).ToString("dd-MM-yyyy HH:mm")
                dtRow.Item("CargoDescription") = Trim(dt.Rows(i)("CARGODESCRIPTION") & "")
                dtRow.Item("ISOCodeID") = Trim(dt.Rows(i)("ISOCodeID") & "")
                dtRow.Item("ISOCOde") = Val(dt.Rows(i)("ISOCODE") & "")

                dtDomContainer.Rows.Add(dtRow)
            Next
            

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()

            Session("table_DomesticContainer") = dtDomContainer
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Private Sub sub_CreateTable()
        Try

            Dim dtDomContainer As New DataTable
            dtDomContainer.Columns.Clear()
            Session("table_DomesticContainer") = ""

            'dtDomContainer.Rows.Count(-1)

            dtDomContainer.Columns.Add("ContainerNo")
            dtDomContainer.Columns.Add("Size")
            dtDomContainer.Columns.Add("Type")
            dtDomContainer.Columns.Add("TypeID")
            dtDomContainer.Columns.Add("CType")
            dtDomContainer.Columns.Add("CTypeID")
            dtDomContainer.Columns.Add("ISOCodeID")
            dtDomContainer.Columns.Add("ISOCOde")
            dtDomContainer.Columns.Add("From")
            dtDomContainer.Columns.Add("FromID")
            dtDomContainer.Columns.Add("To")
            dtDomContainer.Columns.Add("ToID")
            dtDomContainer.Columns.Add("Line")
            dtDomContainer.Columns.Add("LineID")
            dtDomContainer.Columns.Add("Pkgs")
            dtDomContainer.Columns.Add("PkgsType")
            dtDomContainer.Columns.Add("PkgsTypeID")
            dtDomContainer.Columns.Add("Weight")
            dtDomContainer.Columns.Add("Class")
            dtDomContainer.Columns.Add("UNNo")
            dtDomContainer.Columns.Add("DoValidDate")
            dtDomContainer.Columns.Add("CargoDescription")


            Dim dtRow2 As DataRow = dtDomContainer.NewRow

            'dtRow2.Item("ProductName") = ""
            'dtRow2.Item("Qty") = ""
            'dtRow2.Item("Unit") = ""
            'dtRow2.Item("Rate") = ""
            'dtRow2.Item("Amount") = ""
            'dtRow2.Item("vatper") = ""
            'dtRow2.Item("staxper") = ""
            'dtRow2.Item("staxamount") = ""
            'dtRow2.Item("cstper") = ""
            'dtRow2.Item("cstamount") = ""
            'dtRow2.Item("TotalAmount") = ""
            'dtDomContainer.Rows.Add(dtRow2)

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()
            Session("table_DomesticContainer") = dtDomContainer


            If dtDomContainer.Rows.Count > 0 Then
                ' lblError.Text = dtDomContainer.Rows(0)("Designation")
            End If

        Catch ex As Exception
            '  lblError.Text = ex.Message.ToString
        End Try
    End Sub
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql += "USP_DOMESTIC_FILL_DROPDOWN_JOB_ORDER"
            ds = db.sub_GetDataSets(strSql)
            ddlCustomer.DataSource = ds.Tables(0)
            ddlCustomer.DataTextField = "agentName"
            ddlCustomer.DataValueField = "agentID"
            ddlCustomer.DataBind()
            ddlCustomer.Items.Insert(0, New ListItem("--Select--", 0))

            ddlImporter.DataSource = ds.Tables(1)
            ddlImporter.DataTextField = "ImporterName"
            ddlImporter.DataValueField = "ImporterID"
            ddlImporter.DataBind()
            ddlImporter.Items.Insert(0, New ListItem("--Select--", 0))

            ddlCHA.DataSource = ds.Tables(2)
            ddlCHA.DataTextField = "CHAName"
            ddlCHA.DataValueField = "CHAID"
            ddlCHA.DataBind()
            ddlCHA.Items.Insert(0, New ListItem("--Select--", 0))

            ddlSize.DataSource = ds.Tables(3)
            ddlSize.DataTextField = "containersize"
            ddlSize.DataValueField = "containersize"
            ddlSize.DataBind()

            ddlCargoType.DataSource = ds.Tables(4)
            ddlCargoType.DataTextField = "cargotype"
            ddlCargoType.DataValueField = "cargotypeid"
            ddlCargoType.DataBind()
            ddlCargoType.Items.Insert(0, New ListItem("--Select--", 0))

            ddlFrom.DataSource = ds.Tables(5)
            ddlFrom.DataTextField = "location"
            ddlFrom.DataValueField = "locationid"
            ddlFrom.DataBind()
            ddlFrom.Items.Insert(0, New ListItem("--Select--", 0))

            ddlTo.DataSource = ds.Tables(5)
            ddlTo.DataTextField = "location"
            ddlTo.DataValueField = "locationid"
            ddlTo.DataBind()
            ddlTo.Items.Insert(0, New ListItem("--Select--", 0))

            ddlLine.DataSource = ds.Tables(6)
            ddlLine.DataTextField = "slname"
            ddlLine.DataValueField = "slid"
            ddlLine.DataBind()
            ddlLine.Items.Insert(0, New ListItem("--Select--", 0))

            ddlISOCode.DataSource = ds.Tables(7)
            ddlISOCode.DataTextField = "ISOCODE"
            ddlISOCode.DataValueField = "ISOID"
            ddlISOCode.DataBind()
            ddlISOCode.Items.Insert(0, New ListItem("--Select--", 0))

            ddlpkgsType.DataSource = ds.Tables(8)
            ddlpkgsType.DataTextField = "package"
            ddlpkgsType.DataValueField = "codeid"
            ddlpkgsType.DataBind()
            ddlpkgsType.Items.Insert(0, New ListItem("--Select--", 0))

            txtOpenContainerNo.Text = Trim(ds.Tables(9).Rows(0)(0) & "")

            ddlContainerType.DataSource = ds.Tables(10)
            ddlContainerType.DataTextField = "ContainerType"
            ddlContainerType.DataValueField = "ContainerTypeID"
            ddlContainerType.DataBind()
            ddlContainerType.Items.Insert(0, New ListItem("--Select--", 0))
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String

        Return ed.Encrypt(clearText)
    End Function
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If Not grdcontainer.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Please fill container details first!');", True)
                txtContainerNo.Focus()
                Exit Sub
            End If
            If btnSave.Text = "Save" Then
                strSql = ""
                strSql += "USP_INSERT_INTO_DOMESTIC_JOBORDERM '" & Convert.ToDateTime(txtJODate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Replace(Trim(txtIGMNo.Text & ""), "'", "''") & "','" & Replace(Trim(txtItemNo.Text & ""), "'", "''") & "',"
                strSql += "" & Val(ddlCustomer.SelectedValue) & "," & Val(ddlCHA.SelectedValue) & "," & Val(ddlImporter.SelectedValue) & ",'" & Replace(Trim(txtBENo.Text & ""), "'", "''") & "',"
                If txtBEDate.Text = "" Then
                    strSql += "NULL,"
                Else
                    strSql += "'" & Convert.ToDateTime(txtBEDate.Text).ToString("yyyy-MM-dd HH:mm") & "',"
                End If
                strSql += "'" & Replace(Trim(txtBEAdress.Text & ""), "'", "''") & "','" & Replace(Trim(txtPONo.Text & ""), "'", "''") & "','" & Replace(Trim(txtLotNo.Text & ""), "'", "''") & "','" & Replace(Trim(txtRefJONo.Text & ""), "'", "''") & "','" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "'," & Session("UserID") & ",'" & Trim(ddlJOType.SelectedItem.Text) & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    Dim strJONo As String = Val(dt.Rows(0)(0))
                    'Dim strConn As String = ""
                    'Dim conn As SqlConnection
                    'Dim cmd As New SqlCommand
                    'strConn = ConfigurationManager.ConnectionStrings("SqlConnString_Domestic").ConnectionString
                    'If conn Is Nothing OrElse conn.ConnectionString = Nothing Then
                    '    conn = New System.Data.SqlClient.SqlConnection(strConn)
                    'End If
                    'If conn.State = System.Data.ConnectionState.Closed Then
                    '    Try
                    '        conn.Open()
                    '    Catch Ex As Exception
                    '        conn = New System.Data.SqlClient.SqlConnection(strConn)
                    '        conn.Open()
                    '    End Try
                    'End If
                    'cmd = New SqlCommand("USP_INSERT_INTO_DOMESTIC_JOBORDERD", conn)
                    'cmd.CommandType = CommandType.StoredProcedure
                    'Dim sqlParam As SqlParameter
                    'sqlParam = cmd.Parameters.AddWithValue("@JONO", strJONo)
                    'sqlParam = cmd.Parameters.AddWithValue("@PT_DOMESTICJOBORDERD", Session("table_DomesticContainer"))
                    'sqlParam.SqlDbType = SqlDbType.Structured
                    'cmd.ExecuteNonQuery()
                    'conn.Close()
                    strSql = ""
                    strSql += "DELETE FROM DOMESTIC_JOBORDERD WHERE JONO='" & strJONo & "'"
                    db.sub_ExecuteNonQuery(strSql)
                    For Each row In grdcontainer.Rows
                        strSql = ""
                        strSql += "USP_INSERT_INTO_DOMESTIC_JOBORDERD " & strJONo & ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSize"), Label).Text & "") & "',"
                        strSql += "'" & Trim(CType(row.FindControl("lblTypeID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblFromID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblToID"), Label).Text & "") & "',"
                        strSql += "'" & Trim(CType(row.FindControl("lblLineID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblPackages"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblWeight"), Label).Text & "") & "',"
                        strSql += "'" & Trim(CType(row.FindControl("lblClass"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblUNNo"), Label).Text & "") & "',"
                        If Trim(CType(row.FindControl("lblDoValidDate"), Label).Text & "") = "" Then
                            strSql += "NULL,"
                        Else
                            strSql += "'" & Convert.ToDateTime(Trim(CType(row.FindControl("lblDoValidDate"), Label).Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                        End If
                        strSql += "'" & Trim(CType(row.FindControl("lblCargoDescription"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblISOCodeID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblPkgstypeid"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblCTypeID"), Label).Text & "") & "'"
                        db.sub_ExecuteNonQuery(strSql)
                    Next
                    Clear()
                    lblSession.Text = "Record saved successfully for JO No " & strJONo & ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                    UpdatePanel3.Update()
                End If
            Else
                strSql = ""
                strSql += "USP_UPDATE_DOMESTIC_JOBORDERM " & Val(txtJONo.Text) & ",'" & Convert.ToDateTime(txtJODate.Text).ToString("yyyy-MM-dd HH:mm") & "','" & Replace(Trim(txtIGMNo.Text & ""), "'", "''") & "','" & Replace(Trim(txtItemNo.Text & ""), "'", "''") & "',"
                strSql += "" & Val(ddlCustomer.SelectedValue) & "," & Val(ddlCHA.SelectedValue) & "," & Val(ddlImporter.SelectedValue) & ",'" & Replace(Trim(txtBENo.Text & ""), "'", "''") & "',"
                If txtBEDate.Text = "" Then
                    strSql += "NULL,"
                Else
                    strSql += "'" & Convert.ToDateTime(txtBEDate.Text).ToString("yyyy-MM-dd HH:mm") & "',"
                End If
                strSql += "'" & Replace(Trim(txtBEAdress.Text & ""), "'", "''") & "','" & Replace(Trim(txtPONo.Text & ""), "'", "''") & "','" & Replace(Trim(txtLotNo.Text & ""), "'", "''") & "','" & Replace(Trim(txtRefJONo.Text & ""), "'", "''") & "','" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "'," & Session("UserID") & ",'" & Trim(ddlJOType.SelectedItem.Text) & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    Dim strJONo As String = Val(txtJONo.Text)
                    'Dim strConn As String = ""
                    'Dim conn As SqlConnection
                    'Dim cmd As New SqlCommand
                    'strConn = ConfigurationManager.ConnectionStrings("SqlConnString_Domestic").ConnectionString
                    'If conn Is Nothing OrElse conn.ConnectionString = Nothing Then
                    '    conn = New System.Data.SqlClient.SqlConnection(strConn)
                    'End If
                    'If conn.State = System.Data.ConnectionState.Closed Then
                    '    Try
                    '        conn.Open()
                    '    Catch Ex As Exception
                    '        conn = New System.Data.SqlClient.SqlConnection(strConn)
                    '        conn.Open()
                    '    End Try
                    'End If
                    'cmd = New SqlCommand("USP_INSERT_INTO_DOMESTIC_JOBORDERD", conn)
                    'cmd.CommandType = CommandType.StoredProcedure
                    'Dim sqlParam As SqlParameter
                    'sqlParam = cmd.Parameters.AddWithValue("@JONO", strJONo)
                    'sqlParam = cmd.Parameters.AddWithValue("@PT_DOMESTICJOBORDERD", Session("table_DomesticContainer"))
                    'sqlParam.SqlDbType = SqlDbType.Structured
                    'cmd.ExecuteNonQuery()
                    'conn.Close()
                    strSql = ""
                    strSql += "DELETE FROM DOMESTIC_JOBORDERD WHERE JONO='" & strJONo & "'"
                    db.sub_ExecuteNonQuery(strSql)
                    For Each row In grdcontainer.Rows
                        strSql = ""
                        strSql += "USP_INSERT_INTO_DOMESTIC_JOBORDERD " & strJONo & ",'" & Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblSize"), Label).Text & "") & "',"
                        strSql += "'" & Trim(CType(row.FindControl("lblTypeID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblFromID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblToID"), Label).Text & "") & "',"
                        strSql += "'" & Trim(CType(row.FindControl("lblLineID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblPackages"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblWeight"), Label).Text & "") & "',"
                        strSql += "'" & Trim(CType(row.FindControl("lblClass"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblUNNo"), Label).Text & "") & "',"
                        If Trim(CType(row.FindControl("lblDoValidDate"), Label).Text & "") = "" Then
                            strSql += "NULL,"
                        Else
                            strSql += "'" & Convert.ToDateTime(Trim(CType(row.FindControl("lblDoValidDate"), Label).Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
                        End If
                        strSql += "'" & Trim(CType(row.FindControl("lblCargoDescription"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblISOCodeID"), Label).Text & "") & "','" & Trim(CType(row.FindControl("lblPkgstypeid"), Label).Text & "") & "','" & Val(CType(row.FindControl("lblCTypeID"), Label).Text & "") & "'"
                        db.sub_ExecuteNonQuery(strSql)
                    Next
                    Clear()
                    lblSession.Text = "Record updated successfully for JO No " & strJONo & ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                    UpdatePanel3.Update()
                End If
            End If
            
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnAddClick_Click(sender As Object, e As EventArgs)
        Try            
            If ddlJOType.SelectedValue = 1 Then
                If grdcontainer.Rows.Count > 1 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Only one container in this job order type. Cannot Proceed!');", True)
                    txtContainerNo.Focus()
                    Exit Sub
                End If
                txtContainerNo.Text = Trim(txtOpenContainerNo.Text & "")
            End If
            For Each row In grdcontainer.Rows
                If txtContainerNo.Text = Trim(CType(row.FindControl("lblContainerNo"), Label).Text & "") Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('Container No already their. Cannot Proceed!');", True)
                    txtContainerNo.Focus()
                    Exit Sub
                End If
            Next
            Dim dtDomContainer As New DataTable
            Dim intRows As Integer = 0

            dtDomContainer = DirectCast(Session("table_DomesticContainer"), DataTable)
            intRows = dtDomContainer.Rows.Count
            Dim dtRow As DataRow = dtDomContainer.NewRow


            dtRow.Item("ContainerNo") = txtContainerNo.Text.ToUpper()
            dtRow.Item("Size") = ddlSize.SelectedValue
            dtRow.Item("Type") = ddlCargoType.SelectedItem.Text
            dtRow.Item("TypeID") = ddlCargoType.SelectedValue
            dtRow.Item("CType") = ddlContainerType.SelectedItem.Text
            dtRow.Item("CTypeID") = ddlContainerType.SelectedValue
            dtRow.Item("From") = ddlFrom.SelectedItem.Text
            dtRow.Item("FromID") = ddlFrom.SelectedValue
            dtRow.Item("To") = ddlTo.SelectedItem.Text
            dtRow.Item("ToID") = ddlTo.SelectedValue
            If ddlLine.SelectedValue = 0 Then
                dtRow.Item("Line") = ""
            Else
                dtRow.Item("Line") = ddlLine.SelectedItem.Text
            End If

            dtRow.Item("LineID") = ddlLine.SelectedValue
            dtRow.Item("Pkgs") = txtPackages.Text
            dtRow.Item("PkgsType") = Trim(ddlpkgsType.SelectedItem.Text & "")
            dtRow.Item("PkgsTypeID") = Val(ddlpkgsType.SelectedValue & "")
            dtRow.Item("Weight") = txtWeight.Text
            dtRow.Item("Class") = txtClass.Text
            dtRow.Item("UNNo") = txtUNNo.Text
            dtRow.Item("DoValidDate") = Convert.ToDateTime(txtDoValidDate.Text).ToString("dd-MM-yyyy HH:mm")
            dtRow.Item("CargoDescription") = txtCargoDescription.Text
            dtRow.Item("ISOCodeID") = Val(ddlISOCode.SelectedValue)
            dtRow.Item("ISOCOde") = Trim(ddlISOCode.SelectedItem.Text & "")

            dtDomContainer.Rows.Add(dtRow)

            grdcontainer.DataSource = Nothing
            grdcontainer.DataSource = dtDomContainer
            grdcontainer.DataBind()

            Session("table_DomesticContainer") = dtDomContainer

            If ddlJOType.SelectedValue = 1 Then
                txtContainerNo.Text = ""
                txtContainerNo.ReadOnly = True
            Else
                txtContainerNo.Text = ""
            End If
            ddlSize.SelectedValue = "20"
            ddlCargoType.SelectedValue = 0
            ddlFrom.SelectedValue = 0
            ddlTo.SelectedValue = 0
            ddlLine.SelectedValue = 0
            ddlContainerType.SelectedValue = 0
            txtPackages.Text = ""
            txtWeight.Text = ""
            txtClass.Text = ""
            txtUNNo.Text = ""
            txtDoValidDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtCargoDescription.Text = ""
            ddlISOCode.SelectedValue = 0
            txtContainerNo.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtIGMNo_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtIGMNo.Text <> "" And txtItemNo.Text <> "" Then
                strSql = ""
                strSql += "USP_IGM_ITEM_TEXT_CHANGED '" & Trim(txtIGMNo.Text & "") & "','" & Trim(txtItemNo.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ddlCustomer.SelectedValue = Val(dt.Rows(0)("AgentID"))
                    ddlCHA.SelectedValue = Val(dt.Rows(0)("CHAID"))
                    ddlImporter.SelectedValue = Val(dt.Rows(0)("Importerid"))
                    txtBENo.Text = Trim(dt.Rows(0)("BOENo"))
                    txtBEDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("BOEDate"))).ToString("yyyy-MM-ddTHH:mm")
                    txtBEAdress.Text = Trim(dt.Rows(0)("Con_IGMAddress"))
                End If
            End If
            UpdatePanel1.Update()
            UpdatePanel2.Update()
            UpdatePanel4.Update()
            txtItemNo.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtItemNo_TextChanged(sender As Object, e As EventArgs)
        Try
            If txtIGMNo.Text <> "" And txtItemNo.Text <> "" Then
                strSql = ""
                strSql += "USP_IGM_ITEM_TEXT_CHANGED '" & Trim(txtIGMNo.Text & "") & "','" & Trim(txtItemNo.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ddlCustomer.SelectedValue = Val(dt.Rows(0)("AgentID"))
                    ddlCHA.SelectedValue = Val(dt.Rows(0)("CHAID"))
                    ddlImporter.SelectedValue = Val(dt.Rows(0)("Importerid"))
                    txtBENo.Text = Trim(dt.Rows(0)("BOENo"))
                    txtBEDate.Text = Convert.ToDateTime(Trim(dt.Rows(0)("BOEDate"))).ToString("yyyy-MM-ddTHH:mm")
                    txtBEAdress.Text = Trim(dt.Rows(0)("Con_IGMAddress"))
                End If
            End If
            UpdatePanel1.Update()
            UpdatePanel2.Update()
            UpdatePanel4.Update()
            ddlCustomer.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Public Sub Clear()
        Try
            Call sub_CreateTable()
            txtJODate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtBEDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtDoValidDate.Text = Convert.ToDateTime(Now).ToString("yyyy-MM-ddTHH:mm")
            txtIGMNo.Text = ""
            txtItemNo.Text = ""
            ddlCustomer.SelectedValue = 0
            ddlCHA.SelectedValue = 0
            ddlImporter.SelectedValue = 0
            txtBENo.Text = ""
            txtBEAdress.Text = ""
            txtPONo.Text = ""
            txtLotNo.Text = ""
            txtRefJONo.Text = ""
            txtRemarks.Text = ""
            ddlJOType.SelectedValue = 0
            pnlJOType.Enabled = True
            txtJONo.Text = ""
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub btnIndentItem_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "select * from Temp_Containers_Domestic_JobOrder Where UserID=" & Session("UserID") & ""
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                txtContainerNo.Text = Trim(dt.Rows(0)("ContainerNo") & "")
                lblJONo.Text = Trim(dt.Rows(0)("JONO") & "")
            End If
            strSql = ""
            strSql += "USP_CONTAINER_DETAILS_FOR_JOB_ORDER '" & Trim(txtContainerNo.Text & "") & "','" & Trim(lblJONo.Text & "") & "'"
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                ddlSize.SelectedValue = Trim(dt.Rows(0)("Size") & "")
                ddlCargoType.SelectedValue = Trim(dt.Rows(0)("ContainerTypeID") & "")
                ddlContainerType.SelectedValue = Trim(dt.Rows(0)("containertype") & "")
                ddlISOCode.SelectedValue = Trim(dt.Rows(0)("ISOCode") & "")
                ddlLine.SelectedValue = Trim(dt.Rows(0)("SLID") & "")
                txtPackages.Text = Trim(dt.Rows(0)("IGM_Qty") & "")
                txtWeight.Text = Trim(dt.Rows(0)("IGM_GrossWt") & "")
            End If
            ddlSize.Focus()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

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
    Dim ShipLineID, ShipLineIDView As String
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
            If Not (Request.QueryString("ShipLineIDEdit") = "") Then
                ShipLineID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("ShipLineIDEdit")))
                strSql = ""
                strSql = "SELECT * FROM ShipLines WHERE SLID='" & ShipLineID & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    txtlinecode.Text = Trim(dt.Rows(0)("SaID") & "")
                    txtlineName.Text = Trim(dt.Rows(0)("SLName") & "")
                    lblAccountName.Text = Trim(dt.Rows(0)("SLName") & "")
                    txtaddress.Text = Trim(dt.Rows(0)("SLAddressI") & "")
                    txtcity.Text = Trim(dt.Rows(0)("SLCity") & "")
                    txtcontactper.Text = Trim(dt.Rows(0)("SLAuthPerson") & "")
                    txtperdestinston.Text = Trim(dt.Rows(0)("SLAuthPersonDesig") & "")
                    txtContactNumber1.Text = Trim(dt.Rows(0)("SLTelI") & "")
                    txtContactNumber2.Text = Trim(dt.Rows(0)("SLTelII") & "")
                    txtFaxNumber.Text = Trim(dt.Rows(0)("SLFax") & "")
                    txtMobileNumber.Text = Trim(dt.Rows(0)("SLAuthPersonCell") & "")
                    txtEmailID.Text = Trim(dt.Rows(0)("SLEmail") & "")
                    txtremarks.Text = Trim(dt.Rows(0)("Remarks") & "")
                    If dt.Rows(0)("SLIsActive") = True Then
                        chkisActive.Checked = True
                    Else
                        chkisActive.Checked = False
                    End If
                    If dt.Rows(0)("IsContract") = True Then
                        chkisInvoice.Checked = True
                    Else
                        chkisInvoice.Checked = False
                    End If



                    txtlineName.Focus()
                End If
                Panel2.Enabled = True
                btnSave.Text = "Update"

            End If
            'If Not (Request.QueryString("ShipLineIDView") = "") Then
            '    ShipLineID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("ShipLineIDView")))
            '    strSql = ""
            '    strSql = "USP_Epic_Edit_Account '" & ShipLineID & "','" & Session("UserId_DepoCFS") & "'"
            '    dt = db.sub_GetDatatable(strSql)
            '    If (dt.Rows.Count > 0) Then
            '        lblAccountID.Text = Trim(dt.Rows(0)("AccountID") & "")
            '        txtAccountID.Text = Trim(dt.Rows(0)("AccountID") & "")
            '        lblAccountName.Text = Trim(dt.Rows(0)("AccountName") & "")
            '        txtAccountName.Text = Trim(dt.Rows(0)("AccountName") & "")
            '        txtTally.Text = Trim(dt.Rows(0)("TallyName") & "")
            '        ddlGroup.SelectedValue = Trim(dt.Rows(0)("GroupID") & "")
            '        txtHsn.Text = Trim(dt.Rows(0)("HSNCode") & "")
            '        ddlDisplay.SelectedValue = Trim(dt.Rows(0)("DisplayPriority") & "")
            '        chkisActive.Checked = Trim(dt.Rows(0)("IsActive") & "")

            '    End If

            '    Panel2.Enabled = False
            '    chkisActive.Enabled = False
            '    btnSave.Text = "View"
            '    btnSave.Visible = False
            '    btnclear.Visible = False
            'End If
        End If
    End Sub
    Protected Sub Filldropdown()
        Try

          

        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Public Function Encrypt(clearText As String) As String



        Return ed.Encrypt(clearText)
    End Function

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Try
            If (btnSave.Text = "Update") Then
                If Trim(txtlineName.Text) <> Trim(lblAccountName.Text) Then
                    strSql = "SELECT SLName FROM ShipLines WHERE SLName='" & Trim(txtlineName.Text) & "'"
                    dt = db.sub_GetDatatable(strSql)
                    If dt.Rows.Count > 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Shipping Line Name Already Exists.');", True)
                        'MsgBox("Shipping Line Name Already Exists.")
                        txtlineName.Focus()
                        Exit Sub
                    End If

                End If

                strSql = ""
                strSql = "Exec SP_SaveShippingLineDetails '" & SLID & "','" & txtlinecode.Text & "','" & txtlineName.Text & "','" & txtaddress.Text & "','" & txtcity.Text & "','" & txtcontactper.Text & "','" & txtperdestinston.Text & "','" & txtContactNumber1.Text & "',"
                strSql += " '" & txtContactNumber2.Text & "','" & txtFaxNumber.Text & "','" & txtMobileNumber.Text & "','" & txtEmailID.Text & "','" & txtremarks.Text & "'," & checkValue & "," & Session("UserId_DepoCFS") & "," & Session("UserId_DepoCFS") & "," & chkisInvoice.Checked & ""
                db.sub_ExecuteNonQuery(strSql)
                lblSession.Text = "Record Updated successfully "
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            Else
                strSql = ""
                strSql = "SELECT MAX(cast (SLID as bigint)) as SLID FROM ShipLines"
                dt = db.sub_GetDatatable(strSql)
                Count = Val(dt.Rows(0)("SLID"))
                If Count = 0 Then
                    SLID = (Count + 1)
                Else
                    SLID = (Count + 1)
                End If

                strSql = "SELECT SLName FROM ShipLines WHERE SLName='" & Trim(txtlineName.Text) & "'"
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Shipping Line Name Already Exists.');", True)
                    'MsgBox("Shipping Line Name Already Exists.")
                    txtlineName.Focus()
                    Exit Sub
                End If

                If chkisActive.Checked = True Then
                    checkValue = 1
                Else
                    checkValue = 0
                End If
                strSql = ""
                strSql = "Exec SP_SaveShippingLineDetails '" & SLID & "','" & txtlinecode.Text & "','" & txtlineName.Text & "','" & txtaddress.Text & "','" & txtcity.Text & "','" & txtcontactper.Text & "','" & txtperdestinston.Text & "','" & txtContactNumber1.Text & "',"
                strSql += " '" & txtContactNumber2.Text & "','" & txtFaxNumber.Text & "','" & txtMobileNumber.Text & "','" & txtEmailID.Text & "','" & txtremarks.Text & "'," & checkValue & "," & Session("UserId_DepoCFS") & "," & Session("UserId_DepoCFS") & "," & chkisInvoice.Checked & ""
                db.sub_ExecuteNonQuery(strSql)
                lblSession.Text = "Record Saved successfully "
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
                UpdatePanel3.Update()
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

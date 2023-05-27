Imports System.Drawing
Imports System.Data
Imports HRRecruitment.MyConnection
Imports System.Data.SqlClient

Partial Class Summary_BCYMovement
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3, dt4, dt5 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim TransID As String
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
            btnsearch_Click(sender, e)
            If Not (Request.QueryString("TransIDEdit") = "") Then
                TransID = ed.Decrypt(HttpUtility.UrlDecode(Request.QueryString("TransIDEdit")))
                strSql = "SELECT * FROM Transporter WHERE TransID=" & TransID & ""
                dt4 = db.sub_GetDatatable(strSql)
                If dt4.Rows.Count > 0 Then
                    txtTransporterID.Text = Trim(dt4.Rows(0)("transID") & "")
                    txtTransporterName.Text = Trim(dt4.Rows(0)("TransName") & "")
                    txtAddress.Text = Trim(dt4.Rows(0)("Address") & "")
                    txtEmail.Text = Trim(dt4.Rows(0)("EmailID") & "")

                    txtMobile.Text = Trim(dt4.Rows(0)("MobileNo") & "")
                    ddlBankNAme.SelectedItem.Text = Trim(dt4.Rows(0)("BankName") & "")
                    txtAccountNo.Text = Trim(dt4.Rows(0)("BankAccountNo") & "")
                    txtIFCCode.Text = Trim(dt4.Rows(0)("IFSCcode") & "")
                    txtTransporterCode.Text = Trim(dt4.Rows(0)("Transcode") & "")
                End If



                btnSave.Text = "Update"
            End If

        End If

    End Sub
   
    Protected Sub Filldropdown()
        Try
            strSql = ""
            strSql = " SELECT bankID ,bankname  FROM import_banks"
            ds = db.sub_GetDataSets(strSql)
            ddlBankNAme.DataSource = ds.Tables(0)
            ddlBankNAme.DataTextField = "bankname"
            ddlBankNAme.DataValueField = "bankID"
            ddlBankNAme.DataBind()
            ddlBankNAme.Items.Insert(0, New ListItem("--Select--", 0))
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
                strSql = ""
                strSql = "UPDATE Transporter SET  TransName ='" & txtTransporterName.Text & "' ,EmailID='" & Trim(txtEmail.Text) & "' ,MobileNo='" & Trim(txtMobile.Text) & "',"
                strSql += "Address='" & Trim(txtAddress.Text) & "',BankName='" & Trim(ddlBankNAme.SelectedItem.Text) & "',IFSCcode='" & Trim(txtIFCCode.Text) & "',BankAccountNo='" & Trim(txtAccountNo.Text) & "' ,transCode='" & Trim(txtTransporterCode.Text & "") & "' where TransID=" & Trim(txtTransporterID.Text & "") & " "
                dt5 = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record Updated successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            Else
                strSql = ""
                strSql = " SELECT TransID FROM TRANSPORTER where TransName='" & Trim(txtTransporterName.Text & "") & "' "
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Transporter Name is already Exist');", True)
                    txtTransporterName.Focus()
                    Exit Sub
                End If
                strSql = ""
                strSql = " SELECT TransID FROM TRANSPORTER where Transcode='" & Trim(txtTransporterCode.Text & "") & "' "
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('This Transporter Code is already Exist');", True)
                    txtTransporterCode.Focus()
                    Exit Sub
                End If
                strSql = ""
                strSql = " SELECT ISNULL(MAX(TransID ),0) FROM TRANSPORTER"
                Dim dt1 As New DataTable
                dt1 = db.sub_GetDatatable(strSql)
                txtTransporterID.Text = Val(dt1.Rows(0)(0)) + 1
                strSql = ""
                strSql = "INSERT INTO TRANSPORTER (TransName,EmailID,MobileNo,Address,BankName,IFSCcode,BankAccountNo,transCode) "
                strSql += "values ('" & txtTransporterName.Text & "','" & Trim(txtEmail.Text) & "','" & Trim(txtMobile.Text) & "','" & Trim(txtAddress.Text) & "'"
                strSql += " ,'" & Trim(ddlBankNAme.SelectedItem.Text) & "','" & Trim(txtIFCCode.Text) & "','" & Trim(txtAccountNo.Text) & "','" & Trim(txtTransporterCode.Text) & "')"
                dt2 = db.sub_GetDatatable(strSql)
                lblSession.Text = "Record saved successfully"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
            strSql = ""
            strSql += "SELECT TransID AS [ID], TransName AS [Transporter Name] ,Address,BankName as [Bank Name],EmailID as [Email ID] FROM Transporter WHERE TransName like'" & txtsearcht.Text & "%'ORDER BY TransID DESC"
            dt3 = db.sub_GetDatatable(strSql)
            grdcontainer.DataSource = dt3
            grdcontainer.DataBind()
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class

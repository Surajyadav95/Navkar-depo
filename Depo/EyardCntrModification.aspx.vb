Imports System.Data
Imports System.IO
Partial Class Account_ItemList
    Inherits System.Web.UI.Page
    Dim strSql As String
    Dim dt, dt1, dt2, dt3 As DataTable
    Dim db As New dbOperation_Depo
    Dim ds As DataSet
    Dim WHID, WHIDView As String
    Dim ed As New clsEncodeDecode
    Dim stroldDetails As String = "", strNewDetails As String = "", strchanges As String = ""
    Dim Strqry As String = ""
    Public strServerDateM As Date
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillDropDown()
            txtContainerno.Focus()
        End If
    End Sub
    Public Function Encrypt(clearText As String) As String
        Return ed.Encrypt(clearText)
    End Function  
    Protected Sub btnsearch_Click(sender As Object, e As EventArgs)
        Try
           
            strSQL = ""
            strSql = "SELECT EntryID  FROM EYard_In WHERE ContainerNo ='" & Trim(txtContainerno.Text) & "' and  status ='P' and iscancel=0 order by indate desc "
            dt = db.sub_GetDatatable(strSql)
            If dt.Rows.Count > 0 Then
                lblEntryID.Text = dt.Rows(0)("entryID")
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Invalid Container Number');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()
                lblEntryID.Text = ""
                Exit Sub
            End If

            Dim intRowCounter As Integer = 0
            Dim intCounter As Integer = 0
            Dim dtRStemp As New DataTable
            Dim dtRSInDets As New DataTable
            Dim dtRSJODets As New DataTable
            Dim dtRSDelDets As New DataTable
          
            strSQL = ""
            strSQL = "exec sp_Eyardmodification '" & Trim(txtContainerno.Text) & "','" & Trim(lblEntryID.Text) & "'"
            dt1 = db.sub_GetDatatable(strSql)
            If dt1.Rows.Count > 0 Then
                txtEntry.Text = Trim(lblEntryID.Text)
                txtMFGDate.Text = Convert.ToDateTime(Trim(dt1.Rows(0)("MFGDate"))).ToString("yyyy-MM-ddTHH:mm")
                txtIndate.Text = Convert.ToDateTime(Trim(dt1.Rows(0)("InDate"))).ToString("yyyy-MM-ddTHH:mm")
                If Trim(dt1.Rows(0)("ValidDate")) <> "" Then
                    txtDoValid.Text = Convert.ToDateTime(Trim(dt1.Rows(0)("ValidDate"))).ToString("yyyy-MM-ddTHH:mm")
                End If
                ddlISOCode.SelectedValue = Trim(dt1.Rows(0)("ISOCode") & "")
                txtEIR.Text = Trim(dt1.Rows(0)("SurveyEIRID") & "")
                ddlCtrType.SelectedValue = Trim(dt1.Rows(0)("ContainerType") & "")
                ddlStatus.SelectedItem.Text = Trim(dt1.Rows(0)("statusType") & "")
                ddlSize.SelectedItem.Text = Trim(dt1.Rows(0)("Size") & "")
                ddlCondition.SelectedItem.Text = Trim(dt1.Rows(0)("Condition") & "")
                ddlShipline.SelectedValue = Trim(dt1.Rows(0)("SLName") & "")
                txtCustomer.Text = Trim(dt1.Rows(0)("consignee") & "")
                txtTransporter.Text = Trim(dt1.Rows(0)("Transporter") & "")
                txtProcess.Text = Trim(dt1.Rows(0)("ProcessType") & "")
                txtWeight.Text = Trim(dt1.Rows(0)("GrossWeight") & "")
                txtTare.Text = Trim(dt1.Rows(0)("TareWeight") & "")
                txtBookingNo.Text = Trim(dt1.Rows(0)("BKNo") & "")
                txtCarring.Text = Trim(dt1.Rows(0)("carryingcapacity") & "")
                txtLocation.Text = Trim(dt1.Rows(0)("location") & "")
                ddlMovement.SelectedValue = Trim(dt1.Rows(0)("Movtype") & "")
                txtRemarks.Text = Trim(dt1.Rows(0)("Remarks") & "")
                txtTruck.Text = Trim(dt1.Rows(0)("TruckNo") & "")
                txtDamage.Text = Trim(dt1.Rows(0)("damageRemarks") & "")
                txtNewContainerNo.Text = Trim(txtContainerno.Text)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Record not found');", True)
                Control_Clear(sender, e)
                txtContainerno.Focus()
                Exit Sub
            End If
            Dim intTrailerID As Integer = 0, strTrailerNo As String = ""
            intTrailerID = 0
            strTrailerNo = Trim(txtTruck.Text)
            stroldDetails = ""
            Strqry = ""
            Strqry = "containerNo =" & Trim(txtContainerno.Text) & ", EntryID=" & Trim(txtEntry.Text) & ", Indate=" & Convert.ToDateTime(Trim(txtIndate.Text & "")).ToString("yyyy-MM-dd HH:mm") & ","
            Strqry += "ISOCode=" & Trim(ddlISOCode.SelectedItem.Text) & ",ContainerType=" & ddlCtrType.SelectedItem.Text & ", ProcessType = " & Trim(txtProcess.Text) & ", TareWt = " & Trim(txtTare.Text) & ","
            Strqry += "size=" & Trim(ddlSize.SelectedItem.Text) & ",shippingLine = " & Trim(ddlShipline.SelectedItem.Text) & ", Customer = " & Trim(txtCustomer.Text) & ", Movement = " & Trim(ddlMovement.SelectedValue) & ","
            Strqry += " TruckNo = " & Trim(strTrailerNo) & " ,Transporter = " & Trim(txtTransporter.Text) & ", NewContainerno = " & Trim(txtNewContainerNo.Text) & ",carryingcapacity =" & Trim(txtCarring.Text & "") & ",consignee =" & Trim(txtCustomer.Text & "") & ""
            stroldDetails = Strqry
            txtOldDetails.Text = Strqry
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
    Protected Sub FillDropDown()
        Try
            strSql = ""
            strSql = "select ISOID,ISOCode from ISOCodes"
            ds = db.sub_GetDataSets(strSql)
            ddlISOCode.DataSource = ds.Tables(0)
            ddlISOCode.DataTextField = "ISOCode"
            ddlISOCode.DataValueField = "ISOID"
            ddlISOCode.DataBind()
            ddlISOCode.Items.Insert(0, New ListItem("-Select-", 0))

            strSql = ""
            strSql = "select ContainerTypeID,ContainerType from ContainerType"
            ds = db.sub_GetDataSets(strSql)
            ddlCtrType.DataSource = ds.Tables(0)
            ddlCtrType.DataTextField = "ContainerType"
            ddlCtrType.DataValueField = "ContainerTypeID"
            ddlCtrType.DataBind()
            ddlCtrType.Items.Insert(0, New ListItem("-Select-", 0))

            strSql = ""
            strSql = "select SLID,SLName from ShipLines"
            ds = db.sub_GetDataSets(strSql)
            ddlShipline.DataSource = ds.Tables(0)
            ddlShipline.DataTextField = "SLName"
            ddlShipline.DataValueField = "SLID"
            ddlShipline.DataBind()
            ddlShipline.Items.Insert(0, New ListItem("-Select-", 0))

            strSql = ""
            strSql = "select ID , Name from Condition "
            ds = db.sub_GetDataSets(strSql)
            ddlCondition.DataSource = ds.Tables(0)
            ddlCondition.DataTextField = "Name"
            ddlCondition.DataValueField = "ID"
            ddlCondition.DataBind()
            ddlCondition.Items.Insert(0, New ListItem("-Select-", 0))


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Control_Clear(sender As Object, e As EventArgs)
        Try
          
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Try
            stroldDetails = Trim(txtOldDetails.Text & "")
            Dim dblUpNo As Double
            Dim strholdremarks As String
            Dim dtMax As New DataTable
            If Trim(txtContainerno.Text) <> Trim(txtNewContainerNo.Text) Then
                strSql = ""
                strSql = "SELECT top 1 * FROM Eyard_In where containerno='" & Trim(txtNewContainerNo.Text) & "' and iscancel=0 and Status='P' "
                dt = db.sub_GetDatatable(strSql)
                If dt.Rows.Count > 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Specified new container already in Yard. Cannot proceed');", True)
                    txtContainerno.Focus()
                    Exit Sub
                End If
            End If
            strholdremarks = "direct m or  by container modification"
            If Trim(txtContainerno.Text) = "" Or Len(Trim(txtContainerno.Text & "")) <> 11 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter Valid ContainerNo!');", True)
                txtContainerno.Focus()
                Exit Sub
            End If
            If Trim(txtNewContainerNo.Text) = "" Or Len(Trim(txtNewContainerNo.Text & "")) <> 11 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please Enter Valid New ContainerNo!');", True)
                txtContainerno.Focus()
                Exit Sub
            End If
            If Convert.ToDateTime(Trim(txtIndate.Text & "")).ToString("yyyy-MM-dd HH:mm") > Convert.ToDateTime(Now).ToString("yyyy-MM-dd HH:mm") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Indate cannot be greater than server date. cannot proceed');", True)
                txtIndate.Focus()
                Exit Sub
            End If

            'If Trim(txtTruck.Text & "") = "" Then
            '    MsgBox("Trailor No can't be left blank.", vbCritical)
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Trailor No can't be left blank');", True)
            '    txtTruck.Focus()
            '    Exit Sub
            'End If

            dblUpNo = 0
            dtMax.Clear()
            Strqry = "SELECT MAX(RunningNo ) as ID FROM ammendmentlog "
            dtMax = db.sub_GetDatatable(Strqry)
            If Not dtMax.Rows(0)("ID").ToString Is DBNull.Value.ToString Then
                dblUpNo = dtMax.Rows(0)("ID") + 1
            Else
                dblUpNo = 1
            End If
            Dim flag As Boolean = False

            Dim intTrailerID As Integer = 0, strTrailerNo As String = ""
            strTrailerNo = Trim(txtTruck.Text)

            Strqry = ""
            Strqry = "UPDATE EYard_In SET isuploaded=0,Size='" & Trim(ddlSize.SelectedItem.Text) & "', InDate='" & Convert.ToDateTime(Trim(txtIndate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',"
            Strqry += " IsocodeID='" & Trim(ddlISOCode.SelectedValue) & "',ContainerTypeID='" & Val(ddlCtrType.SelectedValue) & " ', Transporter = '" & Replace(Trim(txtTransporter.Text.ToUpper()), "'", "''") & "' ,consignee ='" & Replace(Trim(txtCustomer.Text & ""), "'", "''") & "', "
            Strqry += " MFGDate='" & Convert.ToDateTime(Trim(txtMFGDate.Text & "")).ToString("yyyy-MM-dd HH:mm") & "',SurveyEIRID='" & Replace(Trim(txtEIR.Text), "'", "''") & " ', statusType = '" & Trim(ddlStatus.SelectedItem.Text) & "' ,Condition ='" & Trim(ddlCondition.SelectedItem.Text & "") & "', "
            Strqry += " GrossWeight='" & Trim(txtWeight.Text) & "',BKNo='" & Trim(txtBookingNo.Text) & " ', location = '" & Trim(txtLocation.Text) & "' ,damageRemarks ='" & Replace((txtDamage.Text & ""), "'", "''") & "' ,Remarks ='" & Replace(Trim(txtRemarks.Text & ""), "'", "''") & "', "
            Strqry += "Lineid='" & Val(ddlShipline.SelectedValue) & "',ProcessType='" & Trim(txtProcess.Text) & "',TareWeight='" & Replace(Trim(txtTare.Text), "'", "''") & "',TrailerID='" & intTrailerID & "',TruckNo='" & Trim(strTrailerNo) & "',Movtype='" & Trim(ddlMovement.SelectedValue) & "',carryingcapacity ='" & Trim(txtCarring.Text & "") & "'"
            Strqry += " where containerno='" & Trim(txtContainerno.Text) & "' and entryid='" & Trim(lblEntryID.Text) & "'"
            dt = db.sub_GetDatatable(Strqry)

            strSql = ""
            strSql += "UPDATE EYard_stock SET Size='" & Trim(ddlSize.SelectedItem.Text) & "',ContainerTypeID='" & Val(ddlCtrType.SelectedValue) & "',LineId='" & Val(ddlShipline.SelectedValue) & "',EmptyIn='" & Convert.ToDateTime(Trim(txtIndate.Text & "")).ToString("yyyyMMddHHmmss") & "' where containerno='" & Trim(txtContainerno.Text) & "' AND EntryID='" & Trim(lblEntryID.Text) & "'"
            db.sub_ExecuteNonQuery(strSql)

            Dim EstimateDate As String = "", ApprovalDate As String = "", RepairedDate As String = ""
            Dim dtstock As New DataTable
            strSql = ""
            strSql += "select EstimateDate,RepairedDate,ApprovedDate from EYard_stock where containerno='" & Trim(txtContainerno.Text) & "' and entryid='" & Trim(lblEntryID.Text) & "'"
            dtstock = db.sub_GetDatatable(strSql)
            If dtstock.Rows.Count > 0 Then
                EstimateDate = Trim(dtstock.Rows(0)("EstimateDate") & "")
                RepairedDate = Trim(dtstock.Rows(0)("RepairedDate") & "")
                ApprovalDate = Trim(dtstock.Rows(0)("ApprovedDate") & "")
            End If
            If Trim(txtContainerno.Text) <> Trim(txtNewContainerNo.Text) Then
                Strqry = ""
                Strqry = "UPDATE EYard_In SET ContainerNo='" & Trim(txtNewContainerNo.Text.ToUpper()) & "' where containerno='" & Trim(txtContainerno.Text) & "' and entryid='" & Trim(lblEntryID.Text) & "'"
                dt = db.sub_GetDatatable(Strqry)

                Strqry = ""
                Strqry = "delete from EYard_stock  where containerno='" & Trim(txtContainerno.Text) & "' and entryid='" & Trim(lblEntryID.Text) & "'"
                dt = db.sub_GetDatatable(Strqry)

                Strqry = ""
                Strqry = "UPDATE EYard_assessd SET isuploaded=0, ContainerNo='" & Trim(txtNewContainerNo.Text.ToUpper()) & "' where containerno='" & Trim(txtContainerno.Text) & "' and entryid='" & Trim(lblEntryID.Text) & "'"
                dt = db.sub_GetDatatable(Strqry)

                Strqry = ""
                Strqry = "UPDATE estimate_m SET ContainerNo='" & Trim(txtNewContainerNo.Text.ToUpper()) & "' where containerno='" & Trim(txtContainerno.Text) & "' and entryid='" & Trim(lblEntryID.Text) & "'"
                dt = db.sub_GetDatatable(Strqry)

                Strqry = ""
                Strqry += "insert into EYard_stock(ProcessType,EntryID,ContainerNo,Size,ContainerTypeID,LineId,EmptyIn,OutDate,EstimateDate,RepairedDate,ApprovedDate,isuploaded)"
                Strqry += "Values('E','" & Trim(lblEntryID.Text) & "','" & Trim(txtNewContainerNo.Text.ToUpper()) & "','" & Trim(ddlSize.SelectedItem.Text) & "','" & Val(ddlCtrType.SelectedValue) & "','" & Val(ddlShipline.SelectedValue) & "','" & Convert.ToDateTime(Trim(txtIndate.Text & "")).ToString("yyyyMMddHHmmss") & "','',"
                If Trim(EstimateDate) = "" Then
                    Strqry += "NULL,"
                Else
                    Strqry += "'" & Trim(EstimateDate) & "',"
                End If
                If Trim(RepairedDate) = "" Then
                    Strqry += "NULL,"
                Else
                    Strqry += "'" & Trim(RepairedDate) & "',"
                End If
                If Trim(ApprovalDate) = "" Then
                    Strqry += "NULL,"
                Else
                    Strqry += "'" & Trim(ApprovalDate) & "',"
                End If
                Strqry += "0)"
                db.sub_ExecuteNonQuery(Strqry)
            End If

            strNewDetails = ""
            Strqry = ""
            Strqry = " containerNo =" & Trim(txtContainerno.Text) & ", EntryID=" & Trim(txtEntry.Text) & ", Indate=" & Convert.ToDateTime(Trim(txtIndate.Text & "")).ToString("yyyy-MM-dd HH:mm") & ","
            Strqry += " ISOCode=" & Trim(ddlISOCode.SelectedItem.Text) & ",ContainerType='" & Trim(ddlCtrType.SelectedItem.Text) & ", ProcessType = " & Trim(txtProcess.Text) & ", TareWt = " & Trim(txtTare.Text) & ","
            Strqry += " size=" & Trim(ddlSize.SelectedItem.Text) & ",shippingLine =" & Trim(ddlShipline.SelectedItem.Text) & ", Customer = " & Trim(txtCustomer.Text) & ", Movement = " & Trim(ddlMovement.SelectedValue) & ","
            Strqry += " TruckNo = " & Trim(strTrailerNo) & " ,Transporter = " & Trim(txtTransporter.Text) & ", NewContainerno = " & Trim(txtNewContainerNo.Text) & ",carryingcapacity =" & Trim(txtCarring.Text & "") & ",consignee =" & Trim(txtCustomer.Text & "") & ""
            strNewDetails = Strqry

            Dim dtdel As New DataTable
            If strNewDetails <> stroldDetails Then
                If strNewDetails <> stroldDetails Then
                    strSql = ""
                    strSql = " Delete from Eyard_changes_container_Log where containerno ='" & Trim(txtContainerno.Text & "") & "' and entryid ='" & Trim(txtEntry.Text & "") & "'"
                    dtdel = db.sub_GetDatatable(strSql)
                    Strqry = ""
                    Strqry = "Insert into Eyard_changes_container_Log (entryID,ContainerNo,OldDetails,NewDetails,ModifiedOn ,ModifiedBy)"
                    Strqry += " values (" & Trim(txtEntry.Text & "") & ",'" & Trim(txtContainerno.Text.ToUpper() & "") & "','" & Replace(stroldDetails, "'", "''") & "','" & Replace(strNewDetails, "'", "''") & "',getdate()," & Session("UserId_DepoCFS") & ") "
                    dtdel = db.sub_GetDatatable(Strqry)
                End If


                Dim dtold As New DataTable
                Dim dtnewd As New DataTable
                Strqry = ""
                Strqry = "SELECT Item FROM dbo.SplitString((select top(1)OldDetails from Eyard_changes_container_Log  where ContainerNo ='" & Trim(txtContainerno.Text & "") & "' and entryID ='" & Trim(txtEntry.Text & "") & "' ), ',')"
                dtold = db.sub_GetDatatable(Strqry)

                Strqry = ""
                Strqry = "SELECT Item FROM dbo.SplitString((select top(1)NewDetails from Eyard_changes_container_Log  where ContainerNo ='" & Trim(txtContainerno.Text & "") & "' and entryID ='" & Trim(txtEntry.Text & "") & "' ), ',')"
                dtnewd = db.sub_GetDatatable(Strqry)
                strchanges = ""
                For i As Integer = 0 To dtold.Rows.Count - 1
                    If Trim(dtold.Rows(i)(0) & "") <> Trim(dtnewd.Rows(i)(0) & "") Then
                        strchanges = "Empty Yard  : Changes from " & Trim(dtold.Rows(i)(0) & "") & "  replace to " & Trim(dtnewd.Rows(i)(0) & "") & "  for  Container No =" & Trim(txtContainerno.Text) & " and Entry ID =" & Trim(lblEntryID.Text) & ""
                        Call db.AmmendmentLog(strchanges, Session("UserId_DepoCFS"))
                    End If

                Next
            End If
            lblSession.Text = "Record Updated Successfully"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myModalforupdate", "$('#myModalforupdate').modal();", True)
            Control_Clear(sender, e)
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub

    Protected Sub txtNewContainerNo_TextChanged(sender As Object, e As EventArgs)
        Try
            If Not Trim(txtNewContainerNo.Text & "") = "" Then
                strSql = ""
                strSql += "USP_CHECK_CONTAINER_DIGIT '" & Trim(txtNewContainerNo.Text & "") & "'"
                dt = db.sub_GetDatatable(strSql)
                If Trim(dt.Rows(0)(0)) = "false" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "Key", "alert('Please enter correct container no! ');", True)
                    txtNewContainerNo.Text = ""
                    txtNewContainerNo.Focus()
                    Exit Sub
                Else
                    txtNewContainerNo.Focus()
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error in procedure: " & Reflection.MethodBase.GetCurrentMethod.Name & ". " & ex.Message.ToString
        End Try
    End Sub
End Class


Partial Class Summary_PopUp
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Convert.ToInt32(Session("UserId_DepoCFS")) = 0 Then
            Response.Redirect("../Depo/Common/SessionExpired.aspx")
      
        End If

    End Sub
End Class


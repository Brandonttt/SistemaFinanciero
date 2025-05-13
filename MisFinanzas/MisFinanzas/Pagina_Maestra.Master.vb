Imports Microsoft.VisualBasic.ApplicationServices

Public Class Pagina_Maestra
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            ' Mostrar enlace de administración solo si es admin
            If Page.User.Identity.Name = "admin" Then
                liAdminUsuarios.Visible = True
            Else
                liAdminUsuarios.Visible = False
            End If
        Else
            Response.Redirect("Login.aspx")
        End If

    End Sub

    Private Sub btCerrarSesion_Click(sender As Object, e As EventArgs) Handles btCerrarSesion.Click
        FormsAuthentication.SignOut()
        Session.Abandon()
        Response.Redirect("LogIn.aspx")
    End Sub

End Class
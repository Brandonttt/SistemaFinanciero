Public Class LogIn
    Inherits System.Web.UI.Page

    Protected Sub btIniciar_Click(sender As Object, e As EventArgs) Handles btIniciar.Click


        Dim username As String = txtUser.Text
        Dim password As String = txtPassword.Text

        If txtUser.Text = "" Then
            lblError.Text = "Usuario o contraseña incorrectos."
            MuestraMensaje(Me, "Error", "Datos incorrectos", "error")
        Else
            ' Validar usuario (puedes conectarte a una base de datos o usar un método estático)
            If ValidateUser(username, password) Then
                ' Crear el ticket de autenticación
                Dim authTicket As New FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), False, "UserRole")

                ' Encriptar el ticket
                Dim encryptedTicket As String = FormsAuthentication.Encrypt(authTicket)

                ' Crear una cookie con el ticket
                Dim authCookie As New HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                Response.Cookies.Add(authCookie)

                ' Redirigir al usuario
                Response.Redirect(FormsAuthentication.GetRedirectUrl(username, False))
            Else
                lblError.Text = "Usuario o contraseña incorrectos."
                MuestraMensaje(Me, "Error", "Datos incorrectos", "error")
            End If
        End If

    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblError.Text = ""
    End Sub

    Private Function ValidateUser(username As String, password As String) As Boolean
        ' Validación simple (reemplaza con lógica real, como una consulta a la base de datos)
        Conexiones.AbrirConexion()
        Conexiones.Cnn.Open()
        Dim flag As Boolean = False
        Dim da As New SqlClient.SqlDataAdapter("select * from Usuario where ID_Usuario='" & txtUser.Text & "' and Contra = '" & txtPassword.Text & "'", Conexiones.Cnn)
        Dim ds As New DataSet
        da.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            flag = True
        End If
        Conexiones.Cnn.Close()
        Return flag
    End Function

End Class
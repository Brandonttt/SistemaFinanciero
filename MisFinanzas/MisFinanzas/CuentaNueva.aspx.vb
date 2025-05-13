Public Class CuentaNueva
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblError.Text = ""
    End Sub

    Protected Sub btIniciar_Click(sender As Object, e As EventArgs) Handles btIniciar.Click
        Try
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            If txtUser.Text = "" Or txtPassword.Text = "" Or txtNombre.Text = "" Or txtApellido.Text = "" Or txtEmail.Text = "" Then
                MuestraMensaje(Me, "Error", "Por favor, llena todos los datos.", "error")
            Else
                Dim query As String = "INSERT INTO Usuario (ID_Usuario, Contra, Nombre, Apellido, Email, MontoMinimo, Saldo) VALUES (@Usuario, @Password, @Nombre, @Apellido, @Email, 0, 0)"
                Dim cmd As New SqlClient.SqlCommand(query, Conexiones.Cnn)

                cmd.Parameters.AddWithValue("@Usuario", txtUser.Text)
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text)
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text)
                cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text)
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    CreateCookies()
                    MuestraMensaje(Me, "Cuenta creada correctamente", "Ahora inicia sesión", "success")
                    Response.Redirect("~/LogIn.aspx")
                Else
                    lblError.Text = "Error al crear cuenta, intente de nuevo."
                    MuestraMensaje(Me, "Error", "No se pudo crear tu cuenta", "error")
                End If
            End If

        Catch ex As SqlClient.SqlException
            ' Verificar si el error es de clave duplicada (usuario ya existe)
            If ex.Number = 2627 Or ex.Number = 2601 Then
                lblError.Text = "El nombre de usuario o correo electrónico ya está registrado. Intenta con otro."
                MuestraMensaje(Me, "Error", "Usuario o correo electrónico duplicado", "error")
            Else
                ' Otros errores generales
                lblError.Text = "Error: " & ex.Message
                MuestraMensaje(Me, "Error", "Ocurrió un error al crear la cuenta", "error")
            End If
        Finally
            Conexiones.Cnn.Close()
        End Try
    End Sub


    Private Sub CreateCookies()
        Dim FechaHora As String = Now.AddMinutes(3)

        If Request.Cookies("EmpleadoASP") Is Nothing Then

            Dim aCookie As New HttpCookie("EmpleadoASP")

            aCookie.Value = "Activa"

            aCookie.Expires = FechaHora

            Response.Cookies.Add(aCookie)

        Else

            Dim cookie As HttpCookie = HttpContext.Current.Request.Cookies("EmpleadoASP")

            cookie.Value = "Activa"

            cookie.Expires = FechaHora

            Response.Cookies.Add(cookie)

        End If



    End Sub
End Class
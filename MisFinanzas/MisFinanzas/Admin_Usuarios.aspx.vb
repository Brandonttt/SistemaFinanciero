Imports System.Data.SqlClient

Public Class Admin_Usuarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Verificar si el usuario es admin
        If Not Page.User.Identity.Name = "admin" Then
            Response.Redirect("~/Index.aspx")
        End If

        If Not IsPostBack Then
            CargarUsuarios()
        End If
    End Sub

    Private Sub CargarUsuarios()
        Try
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            ' Consulta SQL para obtener todos los usuarios excepto el admin
            Dim query As String = "SELECT ID_Usuario, Nombre, Apellido, Email, Saldo FROM Usuario WHERE ID_Usuario <> 'admin'"
            Dim cmd As New SqlCommand(query, Conexiones.Cnn)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()

            adapter.Fill(dt)
            gvUsuarios.DataSource = dt
            gvUsuarios.DataBind()

        Catch ex As Exception
            MuestraMensaje(Me, "Error", "Error al cargar los usuarios: " & ex.Message, "error")
        Finally
            If Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try
    End Sub

    Protected Sub gvUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim userId As String = e.CommandArgument.ToString()

        Select Case e.CommandName
            Case "EliminarUsuario"
                EliminarUsuario(userId)
            Case "ModificarUsuario"
                Session("ModoEdicion") = True
                CargarUsuarioParaEditar(userId)
        End Select
    End Sub

    Private Sub CargarUsuarioParaEditar(userId As String)
        Try
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            Dim query As String = "SELECT ID_Usuario, Nombre, Apellido, Email, Saldo FROM Usuario WHERE ID_Usuario = @UserId"
            Dim cmd As New SqlCommand(query, Conexiones.Cnn)
            cmd.Parameters.AddWithValue("@UserId", userId)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                ' Establecer modo edición antes de configurar los controles
                Session("usuarioEditando") = userId

                ' Configurar controles
                txtUsuario.Text = reader("ID_Usuario").ToString()
                txtUsuario.Enabled = False ' No permitir cambiar el ID de usuario
                txtNombre.Text = reader("Nombre").ToString()
                txtApellido.Text = reader("Apellido").ToString()
                txtEmail.Text = reader("Email").ToString()
                txtSaldo.Text = Convert.ToDecimal(reader("Saldo")).ToString()
                txtPassword.Text = "" ' No mostrar la contraseña actual

                ' Actualizar UI para modo edición
                lblFormTitle.Text = "Modificar Usuario"
                btnGuardar.Text = "Actualizar Usuario"
                btnCancelar.Visible = True
            End If

            reader.Close()

        Catch ex As Exception
            MuestraMensaje(Me, "Error", "Error al cargar el usuario: " & ex.Message, "error")
        Finally
            If Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try
    End Sub

    Private Sub EliminarUsuario(userId As String)
        Try
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            ' Primero eliminar los movimientos asociados al usuario
            Dim queryMovimientos As String = "DELETE FROM Movimiento WHERE ID_Usuario = @UserId"
            Dim cmdMovimientos As New SqlCommand(queryMovimientos, Conexiones.Cnn)
            cmdMovimientos.Parameters.AddWithValue("@UserId", userId)
            cmdMovimientos.ExecuteNonQuery()

            ' Luego eliminar el usuario
            Dim queryUsuario As String = "DELETE FROM Usuario WHERE ID_Usuario = @UserId"
            Dim cmdUsuario As New SqlCommand(queryUsuario, Conexiones.Cnn)
            cmdUsuario.Parameters.AddWithValue("@UserId", userId)
            cmdUsuario.ExecuteNonQuery()

            MuestraMensaje(Me, "Éxito", "Usuario eliminado correctamente", "success")
            CargarUsuarios()

        Catch ex As Exception
            MuestraMensaje(Me, "Error", "Error al eliminar el usuario: " & ex.Message, "error")
        Finally
            If Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Session("ModoEdicion") IsNot Nothing AndAlso CBool(Session("ModoEdicion")) Then
            ActualizarUsuario()
            Session("ModoEdicion") = False
        Else
            CrearNuevoUsuario()
        End If
    End Sub

    Private Sub CrearNuevoUsuario()
        Try
            If String.IsNullOrEmpty(txtUsuario.Text) OrElse String.IsNullOrEmpty(txtPassword.Text) Then
                MuestraMensaje(Me, "Error", "Usuario y contraseña son obligatorios", "error")
                Return
            End If

            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            ' Verificar si el usuario ya existe
            Dim queryVerificar As String = "SELECT COUNT(*) FROM Usuario WHERE ID_Usuario = @UserId"
            Dim cmdVerificar As New SqlCommand(queryVerificar, Conexiones.Cnn)
            cmdVerificar.Parameters.AddWithValue("@UserId", txtUsuario.Text)

            If Convert.ToInt32(cmdVerificar.ExecuteScalar()) > 0 Then
                MuestraMensaje(Me, "Error", "El usuario ya existe", "error")
                Return
            End If

            ' Insertar nuevo usuario
            Dim query As String = "INSERT INTO Usuario (ID_Usuario, Contra, Nombre, Apellido, Email, MontoMinimo, Saldo) " &
                                "VALUES (@UserId, @Password, @Nombre, @Apellido, @Email, 0, @Saldo)"
            Dim cmd As New SqlCommand(query, Conexiones.Cnn)

            cmd.Parameters.AddWithValue("@UserId", txtUsuario.Text)
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text)
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text)
            cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text)
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text)
            cmd.Parameters.AddWithValue("@Saldo", Convert.ToDecimal(If(String.IsNullOrEmpty(txtSaldo.Text), "0", txtSaldo.Text)))

            cmd.ExecuteNonQuery()

            MuestraMensaje(Me, "Éxito", "Usuario creado correctamente", "success")
            LimpiarFormulario()
            CargarUsuarios()

        Catch ex As Exception
            MuestraMensaje(Me, "Error", "Error al crear el usuario: " & ex.Message, "error")
        Finally
            If Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try
    End Sub

    Private Sub ActualizarUsuario()
        Try
            If Session("usuarioEditando") = "" Then
                MuestraMensaje(Me, "Error", "No se encontró el usuario a editar", "error")
                Return
            End If

            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            ' Primero verificar si el email está duplicado (excluyendo el usuario actual)
            Dim queryVerificarEmail As String = "SELECT COUNT(*) FROM Usuario WHERE Email = @Email AND ID_Usuario <> @UserId"
            Dim cmdVerificarEmail As New SqlCommand(queryVerificarEmail, Conexiones.Cnn)
            cmdVerificarEmail.Parameters.AddWithValue("@Email", txtEmail.Text)
            cmdVerificarEmail.Parameters.AddWithValue("@UserId", Session("usuarioEditando"))

            If Convert.ToInt32(cmdVerificarEmail.ExecuteScalar()) > 0 Then
                MuestraMensaje(Me, "Error", "El email ya está registrado con otro usuario", "error")
                Return
            End If

            ' Si pasa la validación, proceder con la actualización
            Dim query As String = "UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, " &
                                "Email = @Email, Saldo = @Saldo"

            ' Agregar actualización de contraseña solo si se proporcionó una nueva
            If Not String.IsNullOrEmpty(txtPassword.Text) Then
                query &= ", Contra = @Password"
            End If

            query &= " WHERE ID_Usuario = @UserId"

            Dim cmd As New SqlCommand(query, Conexiones.Cnn)

            cmd.Parameters.AddWithValue("@UserId", Session("usuarioEditando"))
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text)
            cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text)
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text)
            cmd.Parameters.AddWithValue("@Saldo", Convert.ToDecimal(If(String.IsNullOrEmpty(txtSaldo.Text), "0", txtSaldo.Text)))

            If Not String.IsNullOrEmpty(txtPassword.Text) Then
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text)
            End If

            cmd.ExecuteNonQuery()

            MuestraMensaje(Me, "Éxito", "Usuario actualizado correctamente", "success")
            LimpiarFormulario()
            CargarUsuarios()

        Catch ex As Exception
            MuestraMensaje(Me, "Error", "Error al actualizar el usuario: " & ex.Message, "error")
        Finally
            If Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        LimpiarFormulario()
    End Sub

    Private Sub LimpiarFormulario()
        ' Restablecer modo
        Session("ModoEdicion") = False
        Session("usuarioEditando") = String.Empty

        ' Limpiar controles
        txtUsuario.Text = ""
        txtUsuario.Enabled = True
        txtPassword.Text = ""
        txtNombre.Text = ""
        txtApellido.Text = ""
        txtEmail.Text = ""
        txtSaldo.Text = "0"

        ' Restablecer UI
        lblFormTitle.Text = "Crear Nuevo Usuario"
        btnGuardar.Text = "Guardar Usuario"
        btnCancelar.Visible = False
    End Sub

End Class
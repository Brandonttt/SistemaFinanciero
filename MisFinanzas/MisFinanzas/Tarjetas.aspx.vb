Imports System.Data.SqlClient

Public Class Tarjetas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarTarjetas()
        End If
    End Sub

    Private Sub CargarTarjetas()
        Try
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            Dim query As String = "SELECT ID_Tarjeta, Nombre, LimiteCredito, DiaCorte, DiaPago, PagoMinimo " &
                                "FROM Tarjetas WHERE ID_Usuario = @ID_Usuario"

            Dim cmd As New SqlCommand(query, Conexiones.Cnn)
            cmd.Parameters.AddWithValue("@ID_Usuario", Page.User.Identity.Name)

            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            gvTarjetas.DataSource = dt
            gvTarjetas.DataBind()

        Catch ex As Exception
            MuestraMensaje(Me, "Error", "Error al cargar las tarjetas: " & ex.Message, "error")
        Finally
            If Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try
    End Sub

    Protected Sub btnGuardarTarjeta_Click(sender As Object, e As EventArgs) Handles btnGuardarTarjeta.Click
        Try
            If ValidarDatos() Then
                Conexiones.AbrirConexion()
                Conexiones.Cnn.Open()

                Dim query As String = "INSERT INTO Tarjetas (ID_Usuario, Nombre, LimiteCredito, DiaCorte, DiaPago, PagoMinimo) " &
                                    "VALUES (@ID_Usuario, @Nombre, @LimiteCredito, @DiaCorte, @DiaPago, @PagoMinimo)"

                Dim cmd As New SqlCommand(query, Conexiones.Cnn)
                cmd.Parameters.AddWithValue("@ID_Usuario", Page.User.Identity.Name)
                cmd.Parameters.AddWithValue("@Nombre", txtNombreTarjeta.Text.Trim())
                cmd.Parameters.AddWithValue("@LimiteCredito", Decimal.Parse(txtLimiteCredito.Text))
                cmd.Parameters.AddWithValue("@DiaCorte", Integer.Parse(txtDiaCorte.Text))
                cmd.Parameters.AddWithValue("@DiaPago", Integer.Parse(txtDiaPago.Text))
                cmd.Parameters.AddWithValue("@PagoMinimo", Decimal.Parse(txtPagoMinimo.Text))

                cmd.ExecuteNonQuery()

                LimpiarCampos()
                CargarTarjetas()
                MuestraMensaje(Me, "Éxito", "Tarjeta registrada correctamente", "success")
            End If

        Catch ex As Exception
            MuestraMensaje(Me, "Error", "Error al guardar la tarjeta: " & ex.Message, "error")
        Finally
            If Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try
    End Sub

    Private Function ValidarDatos() As Boolean
        If String.IsNullOrEmpty(txtNombreTarjeta.Text.Trim()) Then
            MuestraMensaje(Me, "Error", "El nombre de la tarjeta es requerido", "error")
            Return False
        End If

        Dim limiteCredito As Decimal
        If Not Decimal.TryParse(txtLimiteCredito.Text, limiteCredito) OrElse limiteCredito <= 0 Then
            MuestraMensaje(Me, "Error", "El límite de crédito debe ser un número mayor a 0", "error")
            Return False
        End If

        Dim diaCorte As Integer
        If Not Integer.TryParse(txtDiaCorte.Text, diaCorte) OrElse diaCorte < 1 OrElse diaCorte > 31 Then
            MuestraMensaje(Me, "Error", "El día de corte debe ser un número entre 1 y 31", "error")
            Return False
        End If

        Dim diaPago As Integer
        If Not Integer.TryParse(txtDiaPago.Text, diaPago) OrElse diaPago < 1 OrElse diaPago > 31 Then
            MuestraMensaje(Me, "Error", "El día de pago debe ser un número entre 1 y 31", "error")
            Return False
        End If

        Dim pagoMinimo As Decimal
        If Not Decimal.TryParse(txtPagoMinimo.Text, pagoMinimo) OrElse pagoMinimo < 0 Then
            MuestraMensaje(Me, "Error", "El pago mínimo debe ser un número mayor o igual a 0", "error")
            Return False
        End If

        Return True
    End Function

    Private Sub LimpiarCampos()
        txtNombreTarjeta.Text = String.Empty
        txtLimiteCredito.Text = String.Empty
        txtDiaCorte.Text = String.Empty
        txtDiaPago.Text = String.Empty
        txtPagoMinimo.Text = String.Empty
    End Sub

    Protected Sub gvTarjetas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTarjetas.RowCommand
        Dim idTarjeta As Integer = Convert.ToInt32(e.CommandArgument)

        Select Case e.CommandName
            Case "Eliminar"
                EliminarTarjeta(idTarjeta)
            Case "Editar"
                ' Implementar lógica de edición
                Response.Redirect($"EditarTarjeta.aspx?id={idTarjeta}")
        End Select
    End Sub

    Private Sub EliminarTarjeta(idTarjeta As Integer)
        Try
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            Dim query As String = "DELETE FROM Tarjetas WHERE ID_Tarjeta = @ID_Tarjeta AND ID_Usuario = @ID_Usuario"
            Dim cmd As New SqlCommand(query, Conexiones.Cnn)
            cmd.Parameters.AddWithValue("@ID_Tarjeta", idTarjeta)
            cmd.Parameters.AddWithValue("@ID_Usuario", Page.User.Identity.Name)

            Dim resultado As Integer = cmd.ExecuteNonQuery()

            If resultado > 0 Then
                CargarTarjetas()
                MuestraMensaje(Me, "Éxito", "Tarjeta eliminada correctamente", "success")
            Else
                MuestraMensaje(Me, "Error", "No se pudo eliminar la tarjeta", "error")
            End If

        Catch ex As Exception
            MuestraMensaje(Me, "Error", "Error al eliminar la tarjeta: " & ex.Message, "error")
        Finally
            If Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try
    End Sub
End Class

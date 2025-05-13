Imports System.Data.SqlClient

Public Class Configuracion_Usuario
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Conexiones.AbrirConexion()
        Conexiones.Cnn.Open()
        If Not IsPostBack Then
            Dim user_ As String = Page.User.Identity.Name
            Dim da As New SqlClient.SqlDataAdapter("SELECT MontoMinimo, Saldo FROM Usuario WHERE ID_Usuario = '" & user_ & "'", Conexiones.Cnn)
            Dim ds As New DataSet
            da.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                'lblMonto.Text = "Monto mínimo actual: " & ds.Tables(0).Rows(0)("MontoMinimo").ToString()
                'lblSaldo.Text = "Saldo actual: " & ds.Tables(0).Rows(0)("Saldo").ToString()
                txtMontoMinimo.Text = ds.Tables(0).Rows(0)("MontoMinimo").ToString()
                txtSaldo.Text = ds.Tables(0).Rows(0)("Saldo").ToString()
            End If
            Conexiones.Cnn.Close()
        End If

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim saldoNuevo As Decimal
        Dim montoNuevo As Decimal
        If Not String.IsNullOrWhiteSpace(txtSaldo.Text) AndAlso Decimal.TryParse(txtSaldo.Text, saldoNuevo) AndAlso Not String.IsNullOrWhiteSpace(txtMontoMinimo.Text) AndAlso Decimal.TryParse(txtMontoMinimo.Text, montoNuevo) Then
            ' Abre la conexión
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            Dim user_ As String = Page.User.Identity.Name

            ' Usa SqlCommand en lugar de SqlDataAdapter
            Dim cmd As New SqlClient.SqlCommand("UPDATE Usuario SET Saldo = @Saldo, MontoMinimo = @MontoMinimo WHERE ID_Usuario = @ID_Usuario", Conexiones.Cnn)

            ' Agregar los parámetros para evitar inyección SQL
            cmd.Parameters.AddWithValue("@MontoMinimo", montoNuevo)
            cmd.Parameters.AddWithValue("@Saldo", saldoNuevo)
            cmd.Parameters.AddWithValue("@ID_Usuario", user_)

            ' Ejecuta el comando
            Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()

            ' Verifica si se actualizó correctamente
            If filasAfectadas > 0 Then
                MuestraMensaje(Me.Page, "Valores actualizados", "El monto mínimo se ha actualizado a " & montoNuevo.ToString("C") & "<br>El saldo se ha actualizado a " & saldoNuevo.ToString("C"), "success")
            Else
                MuestraMensaje(Me.Page, "Error al actualizar", "Hubo un problema al actualizar los valores.", "error")
            End If

            ' Cierra la conexión
            Conexiones.Cnn.Close()
        Else
            ' Mensaje si el valor no es un número válido o el campo está vacío
            MuestraMensaje(Me.Page, "Valor no válido", "Por favor, ingresa un monto válido.", "error")
        End If
    End Sub
End Class
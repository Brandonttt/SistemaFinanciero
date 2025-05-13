Imports System.Data.SqlClient

Public Class Inversiones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            PoblarCategorias()
            'PoblarPlazos()


        End If
    End Sub

    Private Sub PoblarCategorias()
        Dim query As String = "SELECT ID_Categoria, Nombre FROM Categoria_Inversion"

        Try
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            Dim command As New SqlCommand(query, Conexiones.Cnn)
            Dim reader As SqlDataReader = command.ExecuteReader()

            dropList.DataSource = reader
            dropList.DataTextField = "Nombre"
            dropList.DataValueField = "ID_Categoria"
            dropList.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write("<script>alert('Error al cargar las categorías: " & ex.Message & "');</script>")
        Finally
            If Conexiones.Cnn IsNot Nothing AndAlso Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try

        dropList.Items.Insert(0, New ListItem("-- Selecciona una categoría --", "0"))
    End Sub

    ' Método para poblar los plazos según la categoría seleccionada
    Private Sub PoblarPlazos(idCategoria As Integer)
        Dim query As String = "SELECT ID_Plazo, Descripcion FROM Plazo_Inversion WHERE ID_Categoria = @ID_Categoria"

        Try
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            Dim command As New SqlCommand(query, Conexiones.Cnn)
            command.Parameters.AddWithValue("@ID_Categoria", idCategoria)

            Dim reader As SqlDataReader = command.ExecuteReader()

            DropDownList1.DataSource = reader
            DropDownList1.DataTextField = "Descripcion"
            DropDownList1.DataValueField = "ID_Plazo"
            DropDownList1.DataBind()

            reader.Close()
        Catch ex As Exception
            Response.Write("<script>alert('Error al cargar los plazos: " & ex.Message & "');</script>")
        Finally
            If Conexiones.Cnn IsNot Nothing AndAlso Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try

        DropDownList1.Items.Insert(0, New ListItem("-- Selecciona un plazo --", "0"))
    End Sub

    ' Evento que se ejecuta al cambiar la categoría seleccionada
    Protected Sub dropList_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim selectedCategoryId As Integer = Convert.ToInt32(dropList.SelectedValue)

        If selectedCategoryId > 0 Then
            PoblarPlazos(selectedCategoryId)
        Else
            DropDownList1.Items.Clear()
            DropDownList1.Items.Insert(0, New ListItem("-- Selecciona un plazo --", "0"))
        End If
    End Sub
    Protected Sub CalcularTotalInversiones(ByVal sender As Object, ByVal e As EventArgs)
        Try
            lblResultados.Text = "" ' Limpiar cualquier mensaje previo

            ' Validar entrada de datos
            If String.IsNullOrEmpty(txtMonto.Text) OrElse Not IsNumeric(txtMonto.Text) Then
                lblResultados.Text = "<div class='alert alert-danger'>Por favor, ingrese un monto válido.</div>"
                Return
            End If

            If DropDownList1.SelectedValue = "0" Then
                lblResultados.Text = "<div class='alert alert-danger'>Por favor, seleccione un plazo válido.</div>"
                Return
            End If

            Dim monto As Decimal = Convert.ToDecimal(txtMonto.Text)
            Dim idPlazo As Integer = Convert.ToInt32(DropDownList1.SelectedValue)

            ' Verificar conexión a la base de datos
            If Conexiones.Cnn.State <> ConnectionState.Open Then Conexiones.Cnn.Open()

            ' Consulta para obtener la duración y tasa de interés
            Dim cmd As New SqlCommand("SELECT duracionMeses, InteresAnual FROM Plazo_Inversion WHERE ID_Plazo = @ID_Plazo", Conexiones.Cnn)
            cmd.Parameters.AddWithValue("@ID_Plazo", idPlazo)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.HasRows Then
                reader.Read()
                Dim duracionMeses As Integer = Convert.ToInt32(reader("duracionMeses"))
                Dim tasa As Decimal = Convert.ToDecimal(reader("InteresAnual"))

                ' Calcular intereses
                Dim interesBruto As Decimal = monto * (tasa / 100) * (duracionMeses / 12)
                Dim isr As Decimal = interesBruto * 0.05D ' 20% de ISR
                Dim interesNeto As Decimal = interesBruto - isr
                Dim montoFinal As Decimal = monto + interesNeto

                ' Mostrar desglose detallado
                lblResultados.Text = String.Format(
                "<div class='card p-3'>" &
                "<h4 class='text-primary'>Resultados</h4>" &
                "<p><strong>Monto invertido:</strong> {0:C}</p>" &
                "<p><strong>Duración:</strong> {1} meses</p>" &
                "<p><strong>Tasa anual:</strong> {2:P2}</p>" &
                "<p><strong>Interés bruto:</strong> {3:C}</p>" &
                "<p><strong>ISR :</strong> {4:C}</p>" &
                "<p><strong>Interés neto:</strong> {5:C}</p>" &
                "<h5 class='text-success'><strong>Monto final:</strong> {6:C}</h5>" &
                "</div>",
                monto, duracionMeses, tasa / 100, interesBruto, isr, interesNeto, montoFinal
            )
            Else
                lblResultados.Text = "<div class='alert alert-warning'>No se encontraron datos para el plazo seleccionado.</div>"
            End If

            reader.Close()
            Conexiones.Cnn.Close()

        Catch ex As Exception
            lblResultados.Text = "<div class='alert alert-danger'>Error durante el cálculo: " & ex.Message & "</div>"
        End Try
    End Sub
    Protected Sub GuardarInversion(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Validar entrada de datos
            If String.IsNullOrEmpty(txtMonto.Text) OrElse Not IsNumeric(txtMonto.Text) Then
                lblResultados.Text = "<div class='alert alert-danger'>Por favor, ingrese un monto válido.</div>"
                Return
            End If

            If DropDownList1.SelectedValue = "0" Then
                lblResultados.Text = "<div class='alert alert-danger'>Por favor, seleccione un plazo válido.</div>"
                Return
            End If

            ' Obtener los valores de los campos
            Dim monto As Decimal = Convert.ToDecimal(txtMonto.Text)
            Dim idCategoria As Integer = Convert.ToInt32(dropList.SelectedValue)
            Dim idPlazo As Integer = Convert.ToInt32(DropDownList1.SelectedValue)

            ' Verificar conexión a la base de datos
            If Conexiones.Cnn.State <> ConnectionState.Open Then Conexiones.Cnn.Open()

            ' Consulta para insertar los datos en la tabla Inversiones
            Dim query As String = "INSERT INTO Inversiones (ID_Usuario, Monto, ID_Categoria, ID_Plazo) " &
                              "VALUES (@ID_Usuario, @Monto, @ID_Categoria, @ID_Plazo)"

            Dim cmd As New SqlCommand(query, Conexiones.Cnn)
            cmd.Parameters.AddWithValue("@ID_Usuario", Page.User.Identity.Name)
            cmd.Parameters.AddWithValue("@Monto", monto)
            cmd.Parameters.AddWithValue("@ID_Categoria", idCategoria)
            cmd.Parameters.AddWithValue("@ID_Plazo", idPlazo)

            ' Ejecutar la consulta
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            ' Validar si se insertó correctamente
            If rowsAffected > 0 Then
                MuestraMensaje(Me, "Inversión realizada", "Tu inversión ha sido agregada.", "success")
            Else
                MuestraMensaje(Me, "Error", "No se pudo agregar tu inversión", "error")
            End If

        Catch ex As Exception
            lblResultados.Text = "<div class='alert alert-danger'>Error al guardar la inversión: " & ex.Message & "</div>"
        Finally
            ' Cerrar la conexión
            If Conexiones.Cnn IsNot Nothing AndAlso Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try
    End Sub

End Class
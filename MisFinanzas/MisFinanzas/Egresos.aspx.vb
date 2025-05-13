Imports System.Data.SqlClient

Public Class Egresos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCategorias()
        End If
    End Sub
    Private Sub CargarCategorias()
        Dim query As String = "SELECT ID_Categoria, Nombre FROM Categoria_Movimiento WHERE ClaseTipoMovimiento = 0"
        Conexiones.AbrirConexion()
        Conexiones.Cnn.Open()

        Dim command As New SqlCommand(query, Conexiones.Cnn)
        Dim reader As SqlDataReader = command.ExecuteReader()

        ' Configurar DropDownList
        dropList.DataSource = reader
        dropList.DataTextField = "Nombre" ' Lo que se mostrará en el dropdown
        dropList.DataValueField = "ID_Categoria" ' Valor asociado al elemento
        dropList.DataBind()

        reader.Close()
        Conexiones.Cnn.Close()

        ' Agregar un elemento por defecto al inicio
        dropList.Items.Insert(0, New ListItem("-- Selecciona una categoría --", "0"))
    End Sub

    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Try
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            Dim query As String = "INSERT INTO Movimiento (ID_Categoria, ID_Deudas, ID_Usuario, Nombre, Cantidad, Fecha) VALUES (@ID_Categoria, NULL, @ID_Usuario, @Nombre, @Cantidad, @Fecha)"
            Dim cmd As New SqlClient.SqlCommand(query, Conexiones.Cnn)

            cmd.Parameters.AddWithValue("@ID_Usuario", Page.User.Identity.Name)
            cmd.Parameters.AddWithValue("@ID_Categoria", dropList.SelectedValue)
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text)
            cmd.Parameters.AddWithValue("@Cantidad", txtCantidad.Text)
            cmd.Parameters.AddWithValue("@Fecha", txtFecha.Text)

            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

            If rowsAffected > 0 Then
                MuestraMensaje(Me, "Egreso agregado", "Tu egreso ha sido agregado.", "success")
            Else
                MuestraMensaje(Me, "Error", "No se pudo agregar tu egreso", "error")
            End If
        Catch ex As SqlClient.SqlException
            MuestraMensaje(Me, "Error", "Ocurrió un error al agregar el egreso" & ex.ToString(), "error")
        Finally
            Conexiones.Cnn.Close()
        End Try
    End Sub
End Class


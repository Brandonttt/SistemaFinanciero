Imports System.Data.SqlClient

Public Class Index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()
            Dim negativo, positivo As Double
            Dim user_ As String = Page.User.Identity.Name
            Dim dCalculoPositivo As New SqlClient.SqlDataAdapter("SELECT SUM(Cantidad) AS Positivo FROM Movimiento M INNER JOIN Categoria_Movimiento C ON C.ID_Categoria = M.ID_Categoria WHERE M.ID_Usuario = '" & user_ & "' AND C.ClaseTipoMovimiento = 1", Conexiones.Cnn)
            Dim dC As New DataSet
            dCalculoPositivo.Fill(dC)
            If dC.Tables(0).Rows.Count > 0 Then
                positivo = dC.Tables(0).Rows(0)("Positivo").ToString()
            End If
            Dim dCalculoNegativo As New SqlClient.SqlDataAdapter("SELECT SUM(Cantidad) AS Negativo FROM Movimiento M INNER JOIN Categoria_Movimiento C ON C.ID_Categoria = M.ID_Categoria WHERE M.ID_Usuario = '" & user_ & "' AND C.ClaseTipoMovimiento = 0", Conexiones.Cnn)
            Dim dCN As New DataSet
            dCalculoNegativo.Fill(dCN)
            If dCN.Tables(0).Rows.Count > 0 Then
                negativo = dCN.Tables(0).Rows(0)("Negativo").ToString()
            End If
            Dim total As Double
            total = positivo - negativo
            Dim query As String = "UPDATE Usuario SET Saldo = @Saldo WHERE ID_Usuario = @ID_Usuario;"
            Dim cmd As New SqlClient.SqlCommand(query, Conexiones.Cnn)
            cmd.Parameters.AddWithValue("@ID_Usuario", Page.User.Identity.Name)
            cmd.Parameters.AddWithValue("@Saldo", total)
            cmd.ExecuteNonQuery()

            Dim da As New SqlClient.SqlDataAdapter("SELECT Nombre, Saldo FROM Usuario WHERE ID_Usuario = '" & user_ & "'", Conexiones.Cnn)
            Dim ds As New DataSet
            da.Fill(ds)
            If ds.Tables(0).Rows.Count > 0 Then
                lblBienvenida.Text = "Bienvenid@ " & ds.Tables(0).Rows(0)("Nombre").ToString()
                lblSaldo.Text = "Saldo: $" & ds.Tables(0).Rows(0)("Saldo").ToString()
            End If

            Conexiones.Cnn.Close()
        Catch ex As Exception

        End Try

        If Not IsPostBack Then
            MostrarMovimientos()
            MostrarInversiones()
        End If

    End Sub

    Private Sub MostrarMovimientos()
        Try
            ' Abrir conexión
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            ' Obtener el usuario actual
            Dim usuarioActual As String = Page.User.Identity.Name

            ' Crear comando SQL para obtener movimientos en los últimos 15 días con INNER JOIN
            Dim query As String = "SELECT M.Nombre AS MovimientoNombre, M.Cantidad, M.Fecha, C.Nombre AS CategoriaNombre, C.ClaseTipoMovimiento " &
                                  "FROM Movimiento M " &
                                  "INNER JOIN Categoria_Movimiento C ON M.ID_Categoria = C.ID_Categoria " &
                                  "WHERE M.ID_Usuario = @ID_Usuario AND M.Fecha >= DATEADD(DAY, -15, GETDATE())" &
                                  "ORDER BY M.Fecha DESC;"
            Dim cmd As New SqlCommand(query, Conexiones.Cnn)
            cmd.Parameters.AddWithValue("@ID_Usuario", usuarioActual)

            ' Ejecutar el comando
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            tbMovimientos.Rows.Clear()

            ' Agregar clases Bootstrap y estilos personalizados a la tabla
            tbMovimientos.CssClass = "table table-striped table-bordered table-hover financial-table"

            ' Crear una fila de encabezado
            Dim headerRow As New TableRow()

            ' Configurar las celdas de encabezado con estilos personalizados
            Dim headerCell1 As New TableCell() With {.Text = "Nombre", .CssClass = "text-center"}
            headerCell1.Style.Add("font-size", "16px") ' Aumentar tamaño del texto
            headerCell1.Style.Add("font-weight", "bold") ' Aplicar negritas
            headerRow.Cells.Add(headerCell1)

            Dim headerCell2 As New TableCell() With {.Text = "Categoría", .CssClass = "text-center"}
            headerCell2.Style.Add("font-size", "16px") ' Aumentar tamaño del texto
            headerCell2.Style.Add("font-weight", "bold") ' Aplicar negritas
            headerRow.Cells.Add(headerCell2)

            Dim headerCell3 As New TableCell() With {.Text = "Cantidad", .CssClass = "text-center"}
            headerCell3.Style.Add("font-size", "16px") ' Aumentar tamaño del texto
            headerCell3.Style.Add("font-weight", "bold") ' Aplicar negritas
            headerRow.Cells.Add(headerCell3)

            Dim headerCell4 As New TableCell() With {.Text = "Fecha", .CssClass = "text-center"}
            headerCell4.Style.Add("font-size", "16px") ' Aumentar tamaño del texto
            headerCell4.Style.Add("font-weight", "bold") ' Aplicar negritas
            headerRow.Cells.Add(headerCell4)

            Dim headerCell5 As New TableCell() With {.Text = "Tipo", .CssClass = "text-center"}
            headerCell5.Style.Add("font-size", "16px") ' Aumentar tamaño del texto
            headerCell5.Style.Add("font-weight", "bold") ' Aplicar negritas
            headerRow.Cells.Add(headerCell5)

            ' Agregar la fila de encabezado a la tabla
            tbMovimientos.Rows.Add(headerRow)



            ' Llenar la tabla con los datos
            While reader.Read()
                Dim row As New TableRow()

                ' Determinar el tipo de movimiento y asignar clase CSS
                Dim tipoMovimiento As String = If(Convert.ToInt32(reader("ClaseTipoMovimiento")) = 1, "Ingreso", "Egreso")
                Dim cssClass As String = If(tipoMovimiento = "Ingreso", "ingreso", "egreso")

                ' Agregar celdas a la fila
                row.Cells.Add(New TableCell() With {.Text = reader("MovimientoNombre").ToString()})
                row.Cells.Add(New TableCell() With {.Text = reader("CategoriaNombre").ToString()})
                row.Cells.Add(New TableCell() With {.Text = Convert.ToDecimal(reader("Cantidad")).ToString("C2"), .CssClass = cssClass})
                row.Cells.Add(New TableCell() With {.Text = Convert.ToDateTime(reader("Fecha")).ToString("yyyy-MM-dd")})
                row.Cells.Add(New TableCell() With {.Text = tipoMovimiento, .CssClass = cssClass})

                tbMovimientos.Rows.Add(row)
            End While


            ' Cerrar el lector
            reader.Close()
        Catch ex As Exception
            ' Manejar errores (opcional)
            Response.Write("<script>alert('Error: " & ex.Message & "');</script>")
        Finally
            ' Cerrar la conexión
            If Conexiones.Cnn IsNot Nothing AndAlso Conexiones.Cnn.State = ConnectionState.Open Then
                Conexiones.Cnn.Close()
            End If
        End Try
    End Sub



    Private Sub MostrarInversiones()
        Try
            ' Abrir conexión
            Conexiones.AbrirConexion()
            Conexiones.Cnn.Open()

            ' Obtener el usuario actual
            Dim usuarioActual As String = Page.User.Identity.Name

            ' Crear comando SQL para obtener las inversiones
            Dim query As String = "SELECT I.Monto, CI.Nombre AS Categoria, PI.Descripcion AS Plazo " &
                                  "FROM Inversiones I " &
                                  "INNER JOIN Categoria_Inversion CI ON I.ID_Categoria = CI.ID_Categoria " &
                                  "INNER JOIN Plazo_Inversion PI ON I.ID_Plazo = PI.ID_Plazo " &
                                  "WHERE I.ID_Usuario = @ID_Usuario"
            Dim cmd As New SqlCommand(query, Conexiones.Cnn)
            cmd.Parameters.AddWithValue("@ID_Usuario", usuarioActual)

            ' Ejecutar el comando
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            tbInversiones.Rows.Clear()

            ' Agregar clases Bootstrap y estilos personalizados a la tabla
            tbInversiones.CssClass = "table table-striped table-bordered table-hover investment-table"

            ' Crear una fila de encabezado
            Dim headerRow As New TableRow()

            ' Configurar las celdas de encabezado con estilos personalizados
            Dim headerCell1 As New TableCell() With {.Text = "Monto", .CssClass = "text-center"}
            headerCell1.Style.Add("font-size", "16px")
            headerCell1.Style.Add("font-weight", "bold")
            headerRow.Cells.Add(headerCell1)

            Dim headerCell2 As New TableCell() With {.Text = "Categoría", .CssClass = "text-center"}
            headerCell2.Style.Add("font-size", "16px")
            headerCell2.Style.Add("font-weight", "bold")
            headerRow.Cells.Add(headerCell2)

            Dim headerCell3 As New TableCell() With {.Text = "Plazo", .CssClass = "text-center"}
            headerCell3.Style.Add("font-size", "16px")
            headerCell3.Style.Add("font-weight", "bold")
            headerRow.Cells.Add(headerCell3)



            ' Agregar la fila de encabezado a la tabla
            tbInversiones.Rows.Add(headerRow)

            ' Llenar la tabla con los datos
            While reader.Read()
                Dim row As New TableRow()

                row.Cells.Add(New TableCell() With {.Text = String.Format("{0:C}", reader("Monto"))})
                row.Cells.Add(New TableCell() With {.Text = reader("Categoria").ToString()})
                row.Cells.Add(New TableCell() With {.Text = reader("Plazo").ToString()})


                ' Agregar la fila a la tabla
                tbInversiones.Rows.Add(row)
            End While

            reader.Close()

        Catch ex As Exception
            Console.WriteLine($"Error al mostrar las inversiones: {ex.Message}")
        Finally
            Conexiones.Cnn.Close()
        End Try
    End Sub

End Class
Public Class Reportes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'graficaIngresosEgresos("Prueba", Me.Page)
    End Sub

    Private Sub btnGenerarGrafica_Click(sender As Object, e As EventArgs) Handles btnGenerarGrafica.Click
        Dim fechaInicial As String = txtFechaInicio.Text
        Dim fechaFinal As String = txtFechaFinal.Text
        Dim usuarioActual As String = Page.User.Identity.Name
        Dim query As String = ""
        Dim fechas As String = ""
        Dim ingresos As String = ""
        Dim egresos As String = ""
        query += " WITH RangoFechas AS ( " & vbCrLf
        query += "     SELECT CAST('" & txtFechaInicio.Text & "' AS DATE) AS Fecha " & vbCrLf
        query += "     UNION ALL " & vbCrLf
        query += "     SELECT DATEADD(DAY, 1, Fecha) " & vbCrLf
        query += "     FROM RangoFechas " & vbCrLf
        query += "     WHERE Fecha < '" & txtFechaFinal.Text & "' " & vbCrLf
        query += " ) " & vbCrLf
        query += " -- Consulta principal " & vbCrLf
        query += " SELECT Fecha, COALESCE(SUM(Ingresos),0) AS Ingresos, COALESCE(SUM(Egresos),0) AS Egresos FROM (SELECT  " & vbCrLf
        query += "     RF.Fecha,  " & vbCrLf
        query += "     M.Cantidad AS Ingresos, 0 Egresos " & vbCrLf
        query += " FROM  " & vbCrLf
        query += "     RangoFechas RF " & vbCrLf
        query += " LEFT JOIN Movimiento M ON RF.Fecha = M.Fecha " & vbCrLf
        query += " LEFT JOIN Categoria_Movimiento C ON M.ID_Categoria = C.ID_Categoria " & vbCrLf
        query += " WHERE  " & vbCrLf
        query += "     (M.ID_Usuario = '" & usuarioActual & "' OR M.ID_Usuario IS NULL)  " & vbCrLf
        query += "     AND (C.ClaseTipoMovimiento = 1 OR C.ClaseTipoMovimiento IS NULL) " & vbCrLf
        query += " 	UNION ALL " & vbCrLf
        query += " 	SELECT  " & vbCrLf
        query += "     RF.Fecha, 0 Ingresos, " & vbCrLf
        query += "     M.Cantidad AS Egresos " & vbCrLf
        query += " FROM  " & vbCrLf
        query += "     RangoFechas RF " & vbCrLf
        query += " LEFT JOIN Movimiento M ON RF.Fecha = M.Fecha " & vbCrLf
        query += " LEFT JOIN Categoria_Movimiento C ON M.ID_Categoria = C.ID_Categoria " & vbCrLf
        query += " WHERE  " & vbCrLf
        query += "     (M.ID_Usuario = '" & usuarioActual & "' OR M.ID_Usuario IS NULL)  " & vbCrLf
        query += "     AND (C.ClaseTipoMovimiento = 0 OR C.ClaseTipoMovimiento IS NULL)) tabla1 " & vbCrLf
        query += " GROUP BY  " & vbCrLf
        query += "     Fecha " & vbCrLf
        query += " ORDER BY  " & vbCrLf
        query += "     Fecha " & vbCrLf
        query += "OPTION (MAXRECURSION 365);"

        Conexiones.AbrirConexion()
        Conexiones.Cnn.Open()

        Dim user_ As String = Page.User.Identity.Name
        Dim da As New SqlClient.SqlDataAdapter(query, Conexiones.Cnn)
        Dim ds As New DataSet
        da.Fill(ds)
        If ds.Tables(0).Rows.Count > 0 Then
            For i = 0 To ds.Tables(0).Rows.Count - 1
                If i = 0 Then
                    fechas = "'" + ds.Tables(0).Rows(i).Item("Fecha") + "'"
                    ingresos = ds.Tables(0).Rows(i).Item("Ingresos").ToString
                    egresos = ds.Tables(0).Rows(i).Item("Egresos").ToString
                Else
                    fechas += ", '" + ds.Tables(0).Rows(i).Item("Fecha") + "'"
                    ingresos += "," + ds.Tables(0).Rows(i).Item("Ingresos").ToString
                    egresos += "," + ds.Tables(0).Rows(i).Item("Egresos").ToString
                End If
            Next
            graficaIngresosEgresos("Egresos vs Ingresos", Me.Page, fechas, ingresos, egresos)
        End If
        Conexiones.Cnn.Close()


    End Sub
End Class
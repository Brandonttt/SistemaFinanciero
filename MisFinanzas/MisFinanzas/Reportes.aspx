<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pagina_Maestra.Master" CodeBehind="Reportes.aspx.vb" Inherits="MisFinanzas.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <script src="https://code.highcharts.com/modules/accessibility.js"></script>
    <link href="Styles/CSS_cheatsheet2.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <!-- Inputs para fechas y botón -->
        <h2 class="text-center mb-4">Reportes</h2>
        <div class="row mb-3">
            <div class="col-md-4">
                <label for="fechaInicio" class="form-label">Fecha Inicial:</label>
                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" Placeholder="YYYY-MM-DD"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label for="fechaFinal" class="form-label">Fecha Final:</label>
                <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="form-control" Placeholder="YYYY-MM-DD"></asp:TextBox>
            </div>
            <div class="col-md-4 d-flex align-items-end">
                <asp:Button ID="btnGenerarGrafica" runat="server" CssClass="btn btn-primary w-100" Text="Generar Gráfica" />
                <%-- OnClick="btnGenerarGrafica_Click" --%>
            </div>
        </div>

        <!-- Contenedor de la gráfica -->
        <div id="container" style="width: 100%; height: 400px; border: 1px solid #ddd; border-radius: 5px;"></div>
    </div>
</asp:Content>

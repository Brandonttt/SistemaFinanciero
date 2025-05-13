<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pagina_Maestra.Master" CodeBehind="Index.aspx.vb" Inherits="MisFinanzas.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Inicio - Sistema Financiero</title>
    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        .card-container {
            margin-top: 30px;
        }

        .card {
            transition: transform 0.2s ease;
        }

            .card:hover {
                transform: scale(1.05);
            }

        .welcome-message {
            margin-top: 20px;
            margin-bottom: 30px;
        }

        .saldo {
            font-size: 1.2rem;
            /*font-weight: bold;*/
        }

        .table-header {
            font-weight: bold;
            background-color: #f2f2f2;
            text-align: left;
            padding: 5px;
        }

        .ingreso {
            color: green;
        }

        .egreso {
            color: black;
        }

        .financial-table {
            font-family: 'Roboto', Arial, sans-serif;
            font-size: 14px;
            background-color: #f9f9f9;
        }

            .financial-table th, .financial-table td {
                text-align: center;
                vertical-align: middle;
            }

            .financial-table th {
                background-color: #004085;
                color: white;
                font-weight: bold;
                font-size: 16px; /* Aumenta el tamaño de los encabezados */
                padding: 10px; /* Mejora el espaciado */
            }
                   .financial-table-container {
            max-height: 300px; /* Altura máxima del contenedor */
            overflow-y: auto; /* Habilitar desplazamiento vertical */
            border: 1px solid #ccc; /* Opcional: Añadir un borde para definición */
            margin-bottom: 20px; /* Espaciado entre tablas */
        }

            .financial-table td {
                padding: 8px; /* Ajusta el espaciado en las celdas */
            }

            .financial-table .ingreso {
                color: green;
                font-weight: bold;
            }

            .financial-table .egreso {
                color: black;
                font-weight: bold;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <!-- Encabezado -->
        <div class="text-center welcome-message">
            <asp:Label ID="lblBienvenida" runat="server" CssClass="h2" Text="Bienvenid@"></asp:Label>
            <br />
            <asp:Label ID="lblSaldo" CssClass="saldo" runat="server" Text="Saldo:"></asp:Label>
        </div>
        <br />
        <h2>Movimientos</h2>
        <%--        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>--%>
        <asp:Table ID="tbMovimientos" runat="server"></asp:Table>


        <h2>Inversiones</h2>
<div class="financial-table-container">
    <asp:Table ID="tbInversiones" CssClass="financial-table" runat="server"></asp:Table>
</div>



    </div>


</asp:Content>


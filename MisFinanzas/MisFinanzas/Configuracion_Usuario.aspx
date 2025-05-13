<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pagina_Maestra.Master" CodeBehind="Configuracion_Usuario.aspx.vb" Inherits="MisFinanzas.Configuracion_Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="Styles/CSS_cheatsheet.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <h2 class="text-center mb-4">Configuración de cuenta</h2>
        <div class="card shadow p-4">
            <div class="mb-4">
                <h4>Modificar el monto mínimo</h4>
                <div class="input-container">
                    <span class="input-label">Monto mínimo nuevo:</span>
                    <asp:Label ID="lblprb" runat="server" Text=""></asp:Label>
                    <div class="input-group">
                        <span class="input-group-text">$</span>
                        <asp:TextBox ID="txtMontoMinimo" CssClass="form-control input-field" runat="server" placeholder="Monto mínimo"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="mb-4">
                <h4>Modificar el saldo de la cuenta</h4>
                <div class="input-container">
                    <span class="input-label">Saldo actual:</span>
                    <div class="input-group">
                        <span class="input-group-text">$</span>
                        <asp:TextBox ID="txtSaldo" CssClass="form-control input-field" runat="server" placeholder="Saldo"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="text-center">
                <asp:Button ID="btnUpdate" CssClass="btn btn-primary" runat="server" Text="Guardar saldo nuevo"></asp:Button>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LogIn.aspx.vb" Inherits="MisFinanzas.LogIn" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Mis Finanzas</title>
    <link rel="stylesheet" type="text/css" href="Styles/CCS_LogIn.css"/>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" type="text/css" href="Styles/CCS_LogIn.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <asp:Image ID="Logo" ImageUrl="~/Imagenes/Logo.jpg" Width="150px" runat="server" />
        </div>
        <br />
        <div class="form">
            <div class="title">Inicio de sesión</div>
            <div class="input-container ic2">
                <asp:TextBox ID="txtUser" CssClass="input" runat="server"></asp:TextBox>
                <div class="cut cut-short"></div>
                <label for="user" class="placeholder">Usuario</label>
            </div>
            <div class="input-container ic2">
                <asp:TextBox ID="txtPassword" CssClass="input" TextMode="Password" runat="server"></asp:TextBox>
                <div class="cut cut-password"></div>
                <label for="password" class="placeholder">Contraseña</label>
            </div>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            <h2 class="subtitle">¿Quieres crear una cuenta? <a href="CuentaNueva.aspx">Haz click aquí</a></h2>
            <asp:Button ID="btIniciar" runat="server" CssClass="submit" Text="Iniciar Sesión" />
        </div>
    </form>
</body>
</html>

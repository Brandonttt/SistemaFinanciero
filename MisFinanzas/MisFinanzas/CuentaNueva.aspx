<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CuentaNueva.aspx.vb" Inherits="MisFinanzas.CuentaNueva" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Mis Finanzas</title>
    <link rel="stylesheet" type="text/css" href="Styles/CCS_CrearCuenta.css"/>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <asp:Image ID="Logo" ImageUrl="~/Imagenes/Logo.jpg" Width="100px" runat="server" />
        </div>
        <br />
        <div class="form">
            <div class="title">Crear cuenta nueva</div>
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
            <div class="input-container ic2">
                <asp:TextBox ID="txtNombre" CssClass="input" runat="server"></asp:TextBox>
                <div class="cut cut-short"></div>
                <label for="Nombre" class="placeholder">Nombre</label>
            </div>
            <div class="input-container ic2">
                <asp:TextBox ID="txtApellido" CssClass="input" runat="server"></asp:TextBox>
                <div class="cut cut-short"></div>
                <label for="Apellido" class="placeholder">Apellido</label>
            </div>
            <div class="input-container ic2">
                <asp:TextBox ID="txtEmail" CssClass="input" runat="server"></asp:TextBox>
                <div class="cut cut-mega-short"></div>
                <label for="Email" class="placeholder">Email</label>
            </div>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            <h2 class="subtitle">¿Ya tienes una cuenta? <a href="LogIn.aspx">Inicia sesión aquí</a></h2>
            <asp:Button ID="btIniciar" runat="server" CssClass="submit" Text="Crear Cuenta" />
        </div>
    </form>
</body>
    
</html>


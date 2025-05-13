<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pagina_Maestra.Master" CodeBehind="Egresos.aspx.vb" Inherits="MisFinanzas.Egresos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="Styles/CSS_cheatsheet.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container py-5">
    <h2 class="text-center mb-4">Egresos</h2>
    <div class="card shadow p-4">
        <div class="mb-4">
            <h4>Categoría</h4>
            <div class="input-container">
                <div class="input-group">
                    <asp:DropDownList ID="dropList" CssClass="form-control dropdown-style" runat="server"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="mb-4">
            <h4>Concepto</h4>
            <div class="input-container">
                <div class="input-group">
                    <asp:TextBox ID="txtNombre" CssClass="form-control input-field" runat="server" placeholder="Ejemplo: Medicinas"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
        <div class="mb-4">
            <h4>Cantidad</h4>
            <div class="input-container">
                <div class="input-group">
                    <span class="input-group-text">$</span>
                    <asp:TextBox ID="txtCantidad" CssClass="form-control input-field" runat="server" placeholder="Saldo"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="mb-4">
            <h4>Fecha</h4>
            <div class="input-container">
                <div class="input-group">
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" Placeholder="YYYY-MM-DD"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="text-center">
            <asp:Button ID="btnIngresar" CssClass="btn btn-primary" runat="server" Text="Guardar egreso nuevo"></asp:Button>
        </div>
    </div>
</div>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script></asp:Content>



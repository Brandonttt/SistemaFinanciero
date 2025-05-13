<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pagina_Maestra.Master" CodeBehind="Inversiones.aspx.vb" Inherits="MisFinanzas.Inversiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="Styles/CSS_cheatsheet.css" rel="stylesheet">
    <style>
        /* Estilo para habilitar el scroll en el contenedor */
        .scrollable-container {
            max-height: 80vh; /* Ajusta la altura de la ventana según sea necesario */
            overflow-y: auto; /* Agrega la barra de desplazamiento vertical */
            padding: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <h2 class="text-center mb-4">Inversiones</h2>
        <div class="card shadow p-4 scrollable-container" >
            
                  <div class="form-group">
                <label for="dropList">Categoría</label>
                <asp:DropDownList ID="dropList" runat="server" CssClass="form-control" 
                                  AutoPostBack="True" OnSelectedIndexChanged="dropList_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="DropDownList1">Plazo</label>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="mb-4">
                 <h4>Cantidad</h4>
                 <div class="input-container">
                     <div class="input-group">
                         <span class="input-group-text">$</span>
                         <asp:TextBox ID="txtMonto" CssClass="form-control input-field" runat="server" placeholder="Saldo"></asp:TextBox>
                     </div>
                 </div>
            </div>


                       <div class="text-center d-flex justify-content-center gap-3">
                    <asp:Button ID="btnIngresar" CssClass="btn btn-primary" runat="server" Text="Calcular" OnClick="CalcularTotalInversiones" />
                    <asp:Button ID="btnGuardar" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="GuardarInversion" />
                </div>
                 <br />

                <asp:Label ID="lblResultados" runat="server" CssClass="alert alert-info w-100" ></asp:Label>
             
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
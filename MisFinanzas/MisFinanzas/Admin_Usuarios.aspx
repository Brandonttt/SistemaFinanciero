<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pagina_Maestra.Master" CodeBehind="Admin_Usuarios.aspx.vb" Inherits="MisFinanzas.Admin_Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="Styles/CSS_cheatsheet.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <h2 class="text-center mb-4">Administración de Usuarios</h2>

        <!-- Formulario de Creación/Edición -->
        <div class="card shadow p-4 mb-4">
            <h4 class="mb-3"><asp:Label ID="lblFormTitle" runat="server" Text="Crear Nuevo Usuario"></asp:Label></h4>
            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label">Usuario:</label>
                    <asp:TextBox ID="txtUsuario" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Contraseña:</label>
                    <asp:TextBox ID="txtPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Nombre:</label>
                    <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Apellido:</label>
                    <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Email:</label>
                    <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Saldo:</label>
                    <asp:TextBox ID="txtSaldo" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                </div>
            </div>
            <div class="d-flex justify-content-between">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Usuario" CssClass="btn btn-primary" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" Visible="false" />
            </div>
        </div>

        <!-- Tabla de usuarios -->
        <div class="card shadow p-4">
            <div class="table-responsive">
                <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-striped table-bordered table-hover" OnRowCommand="gvUsuarios_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID_Usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:C}" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnModificar" runat="server" Text="Modificar" 
                                    CommandName="ModificarUsuario" 
                                    CommandArgument='<%# Eval("ID_Usuario") %>'
                                    CssClass="btn btn-warning btn-sm me-1" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" 
                                    CommandName="EliminarUsuario" 
                                    CommandArgument='<%# Eval("ID_Usuario") %>'
                                    CssClass="btn btn-danger btn-sm"
                                    OnClientClick="return confirm('¿Está seguro de que desea eliminar este usuario?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
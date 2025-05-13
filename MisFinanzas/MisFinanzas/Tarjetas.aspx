<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pagina_Maestra.Master" CodeBehind="Tarjetas.aspx.vb" Inherits="MisFinanzas.Tarjetas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="Styles/CSS_cheatsheet.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <h2 class="text-center mb-4">Administración de Tarjetas</h2>
        
        <!-- Sección para agregar nueva tarjeta -->
        <div class="card shadow p-4 mb-4">
            <h4>Registrar Nueva Tarjeta</h4>
            <div class="row">
                <div class="col-md-6 mb-3">
                    <label class="form-label">Nombre/Alias de la Tarjeta</label>
                    <asp:TextBox ID="txtNombreTarjeta" CssClass="form-control" runat="server" placeholder="Ej: Tarjeta Oro"></asp:TextBox>
                </div>
                <div class="col-md-6 mb-3">
                    <label class="form-label">Límite de Crédito</label>
                    <div class="input-group">
                        <span class="input-group-text">$</span>
                        <asp:TextBox ID="txtLimiteCredito" CssClass="form-control" runat="server" placeholder="0.00"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 mb-3">
                    <label class="form-label">Día de Corte</label>
                    <asp:TextBox ID="txtDiaCorte" CssClass="form-control" runat="server" placeholder="Día del mes" TextMode="Number"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-3">
                    <label class="form-label">Día de Pago</label>
                    <asp:TextBox ID="txtDiaPago" CssClass="form-control" runat="server" placeholder="Día del mes" TextMode="Number"></asp:TextBox>
                </div>
                <div class="col-md-4 mb-3">
                    <label class="form-label">Pago Mínimo</label>
                    <div class="input-group">
                        <span class="input-group-text">$</span>
                        <asp:TextBox ID="txtPagoMinimo" CssClass="form-control" runat="server" placeholder="0.00"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="text-center">
                <asp:Button ID="btnGuardarTarjeta" CssClass="btn btn-primary" runat="server" Text="Guardar Tarjeta"></asp:Button>
            </div>
        </div>

        <!-- Lista de tarjetas registradas -->
        <div class="card shadow p-4">
            <h4>Mis Tarjetas</h4>
            <asp:GridView ID="gvTarjetas" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Tarjeta" />
                    <asp:BoundField DataField="LimiteCredito" HeaderText="Límite de Crédito" DataFormatString="{0:C2}" />
                    <asp:BoundField DataField="DiaCorte" HeaderText="Día de Corte" />
                    <asp:BoundField DataField="DiaPago" HeaderText="Día de Pago" />
                    <asp:BoundField DataField="PagoMinimo" HeaderText="Pago Mínimo" DataFormatString="{0:C2}" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-sm btn-primary" CommandName="Editar" CommandArgument='<%# Eval("ID_Tarjeta") %>' />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("ID_Tarjeta") %>' OnClientClick="return confirm('¿Está seguro de eliminar esta tarjeta?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
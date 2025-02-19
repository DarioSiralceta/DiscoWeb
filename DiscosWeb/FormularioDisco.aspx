<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="FormularioDisco.aspx.cs" Inherits="DiscosWeb.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtTitulo" class="form-label">Titulo </label>
                <asp:TextBox runat="server" ID="txtTitulo" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtFecha" class="form-label">Fecha de Lanzamiento </label>
                <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtCantidadCanciones" class="form-label">Cantidad de Canciones </label>
                <asp:TextBox runat="server" ID="txtCantidadCanciones" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="ddlEstilo" class="form-label">Estilo</label>
                <asp:DropDownList ID="ddlEstilo" CssClass="form-select" runat="server"></asp:DropDownList>

                <label for="ddlTipo" class="form-label">Tipo de Edicion</label>
                <asp:DropDownList ID="ddlTipo" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>

            
        </div>
    </div>

    <div class="col-6">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="mb-3">
                    <label for="txtImagenUrl" class="form-label">Url Imagen</label>
                    <asp:TextBox runat="server" ID="txtImagenUrl" CssClass="form-control"
                        AutoPostBack="true" OnTextChanged="txtImagenUrl_TextChanged" />
                </div>
                <asp:Image ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png"
                    runat="server" ID="imgTapa" Width="60%" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </div>
    <div class="mb-3">
         <asp:Button Text="Aceptar" ID="btnAceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" runat="server" />
         <a href="DiscosLista.aspx">Cancelar</a>
        <asp:Button Text="Desactivar" ID="btnDesactivar" OnClick="btnDesactivar_click" CssClass="btn btn-warning" runat="server" />
    </div>
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="mb-3">
                    <asp:Button Text="Eliminar" ID="btnEliminar" Onclick="btnEliminar_Click" CssClass="btn btn-danger" runat="server" />
                </div>

                <%if (ConfirmaEliminacion){ %>
                <div class="mb-3">
                    <asp:CheckBox Text="Confirmar Eliminacion" Id="chkconfirmaEliminacion" runat="server" />
                    <asp:Button Text="Eliminar" ID="btnConfirmaEliminar" Onclick="btnConfirmaEliminar_Click" CssClass="btn btn-outline-danger" runat="server" />
                 </div>
                 <%} %>
             </ContentTemplate>
        </asp:UpdatePanel>



    </div>




</asp:Content>

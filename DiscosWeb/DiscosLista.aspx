<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DiscosLista.aspx.cs" Inherits="DiscosWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Lista de Discos</h1>
    <asp:GridView ID="dgvDiscos" runat="server" DataKeyNames="Id"
        CssClass="table" AutoGenerateColumns="false" 
        OnSelectedIndexChanged="dgvDiscos_SelectedIndexChanged" 
        OnPageIndexChanging="dgvDiscos_PageIndexChanging"
        AllowPaging="True" PageSize="2">
        <Columns>
            <asp:BoundField HeaderText="Titulo" DataField="Titulo" />
            <asp:BoundField HeaderText="Cantidad de Canciones" DataField="CantidadCanciones" />
            <asp:BoundField HeaderText="Estilo" DataField="Genero.Descripcion" />  
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="Seleccionar"  />
        </Columns>
    </asp:GridView>
    <a href="FormularioDisco.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>

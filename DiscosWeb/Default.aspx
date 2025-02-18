<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DiscosWeb.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Hola!</h1>
    <p>Discos que hay que escuchar al menos una vez en la vida...</p>

     <div class="row row-cols-1 row-cols-md-3 g-4">


        <asp:Repeater ID="repRepetidor" runat="server">
            <ItemTemplate>  
                <div class="col-md-4">
                <div class="card">
                  <img src='<%# Eval("UrlImagenTapa") %>' class="card-img-top img-fluid" alt="Imagen de tapa">
                  <div class="card-body">
                         <h5 class="card-title"><%# Eval("Titulo") %></h5>
                         <p class="card-text">Cantidad de canciones: <%# Eval("CantidadCanciones") %></p>
                         <a href="DetalleDisco.aspx?id=<%# Eval("Id") %>" class="btn btn-primary">Ver Detalle</a>
                      <asp:Button Text="Ejemplo" CssClass="btn-primary" runat="server"  ID="btnEjemplo" CommandArgument='<%#Eval("Id") %>' CommandName="DiscoId" OnClick="btnEjemplo_Click"/>
                  </div>
                </div>
            </div>
     
          </ItemTemplate>
        </asp:Repeater>

    </div>









</asp:Content>
using data;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DiscosWeb
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            if (!IsPostBack)
            {
                try
                {
                    EstiloDato estilo = new EstiloDato();
                    List<Estilo> lista = estilo.listar();

                    ddlEstilo.DataSource = lista;
                    ddlEstilo.DataValueField = "id";
                    ddlEstilo.DataTextField = "Descripcion";
                    ddlEstilo.DataBind();

                    TipoEdicionDato tipo = new TipoEdicionDato();
                    List<TipoEdicion> serie = tipo.listar();

                    ddlTipo.DataSource = serie;
                    ddlTipo.DataValueField = "id";
                    ddlTipo.DataTextField = "Descripcion";
                    ddlTipo.DataBind();
                }
                catch (Exception ex)
                {

                    Session.Add("error", ex);
                    throw;
                }

            }








        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

                Disco nuevo = new Disco();
                DiscoDato discoDato = new DiscoDato();

                nuevo.Titulo = txtTitulo.Text;
                nuevo.FechaLanzamiento = DateTime.Parse(txtFecha.Text);
                nuevo.CantidadCanciones = int.Parse(txtCantidadCanciones.Text);
                nuevo.UrlImagenTapa = txtImagenUrl.Text;


                nuevo.Genero = new Estilo();
                nuevo.Genero.id = int.Parse(ddlEstilo.SelectedValue);
                nuevo.Formato = new TipoEdicion();
                nuevo.Formato.id = int.Parse(ddlTipo.SelectedValue);



                discoDato.agregarConSP(nuevo);
                Response.Redirect("DiscosLista.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgTapa.ImageUrl = txtImagenUrl.Text;
        }


    }
}
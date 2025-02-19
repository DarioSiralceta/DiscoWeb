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

        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {


                txtId.Enabled = false;
                ConfirmaEliminacion = false;
                try
                {
                    if (!IsPostBack)
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
                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                    if (id != "" && !IsPostBack)
                    {
                        DiscoDato dato = new DiscoDato();
                        Disco seleccionado = (dato.listar(id))[0];

                        txtId.Text = id;
                        txtTitulo.Text = seleccionado.Titulo;
                        txtFecha.Text = seleccionado.FechaLanzamiento.ToString();
                        txtCantidadCanciones.Text = seleccionado.CantidadCanciones.ToString();
                        txtImagenUrl.Text = seleccionado.UrlImagenTapa;

                        ddlEstilo.SelectedValue = seleccionado.Genero.id.ToString();
                        ddlTipo.SelectedValue = seleccionado.Genero.id.ToString();
                        txtImagenUrl_TextChanged(sender, e);
                        
                    }
                }
                catch (Exception ex)
                {

                    Session.Add("error", ex);
                    throw;
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


                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    discoDato.modificarConSp(nuevo);
                }   
                else
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;


        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                if (chkconfirmaEliminacion.Checked)
                {
                    DiscoDato dato = new DiscoDato();
                    dato.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("DiscosLista.aspx");
                }
                
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
            }

        }

        protected void btnDesactivar_click(object sender, EventArgs e)
        {
            try
            {

                DiscoDato dato = new DiscoDato();
                dato.eliminarLogico(int.Parse(txtId.Text));
                Response.Redirect("DiscosLista.aspx");

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
            }




        }

    }
}
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using data;
using System.Diagnostics.Eventing.Reader;


namespace DiscosWeb
{
	public partial class WebForm3 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}



		protected void btnIngresar_Click(object sender, EventArgs e)
		{

			Usuario usuario;
			UsuarioData data = new UsuarioData();
			try
			{
				usuario = new Usuario(txtUser.Text, txtPassword.Text, false);
				if(data.Loguear(usuario))
				{
					Session.Add("usuario", usuario);
					Response.Redirect("MenuLogin1.aspx",false);
				}
				else
				{
					Session.Add("error", "user o pass incorrectos");
					Response.Redirect("Error.aspx", false);



				}

			}
			catch (Exception ex)
				{

					Session.Add("error", ex.ToString());
					Response.Redirect("Error.aspx");
            }

		}

    }
}
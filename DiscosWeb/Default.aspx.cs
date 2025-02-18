using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using data;
using dominio;

namespace DiscosWeb
{
    public partial class Default : System.Web.UI.Page
    {

        public List<Disco> ListaDisco { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            DiscoDato data = new DiscoDato();
            ListaDisco = data.listarConSP();

            if (!IsPostBack)
            {
                repRepetidor.DataSource = ListaDisco;
                repRepetidor.DataBind();

            }
        }

        protected void btnEjemplo_Click(object sender, EventArgs e)
        {

            string valor = ((Button)sender).CommandArgument;

        }
    }
}
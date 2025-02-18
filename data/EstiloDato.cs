using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace data
{
    public class EstiloDato
    {
        public List<Estilo> listar()
        {
            List<Estilo> lista = new List<Estilo>();
            AccesoDatos datos = new AccesoDatos();
            try

            {

                datos.setearConsulta("select id, descripcion from ESTILOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Estilo aux = new Estilo();
                    aux.id = (int)datos.Lector["id"];
                    aux.Descripcion= (string)datos.Lector["Descripcion"];

                    lista.Add(aux);


                }


                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                datos.cerrarConexion();


            }














        }
    }
}

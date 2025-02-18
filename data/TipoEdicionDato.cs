using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace data
{
    public class TipoEdicionDato
    {
        public List<TipoEdicion> listar()
        {	
			List<TipoEdicion> lista = new List<TipoEdicion>();
			AccesoDatos datos = new AccesoDatos();
			try

			{

				datos.setearConsulta("select id, descripcion from TIPOSEDICION");
				datos.ejecutarLectura();

				while(datos.Lector.Read())
				{
					TipoEdicion aux = new TipoEdicion();
					aux.id = (int)datos.Lector["id"];
					aux.Descripcion = (string)datos.Lector["Descripcion"];

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

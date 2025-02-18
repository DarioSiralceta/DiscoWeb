using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Forms;

namespace data
{
    public class DiscoDato
    {
        public List<Disco> listar(string id = "")
        {
            List<Disco> lista = new List<Disco>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, E.Descripcion Genero, T.Descripcion Formato, D.IdEstilo, D.IdTipoEdicion, D.id From DISCOS D, ESTILOS E, TIPOSEDICION T where E.id = D.idEstilo and T.id = D.IdTipoEdicion ";
                if (id != "")
                    comando.CommandText += " and D.id = " + id;
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Id = (int)lector["Id"];
                    aux.Titulo = (string)lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)lector["CantidadCanciones"];

                    if (!(lector["UrlImagenTapa"] is DBNull))
                        aux.UrlImagenTapa = (string)lector["UrlImagenTapa"];

                    aux.Genero = new Estilo();
                    aux.Genero.id = (int)lector["IdEstilo"];
                    aux.Genero.Descripcion = (string)lector["Genero"];
                    aux.Formato = new TipoEdicion();
                    aux.Formato.id = (int)lector["IdTipoEdicion"];
                    aux.Formato.Descripcion = (string)lector["Formato"];

                    lista.Add(aux);


                }

                conexion.Close();
                return lista;
            }

            catch (Exception ex)
            {

                throw ex;
            }


        }

        public List<Disco> listarConSP()
        {
            List<Disco> lista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();
            try
            {


                datos.setearProcedimiento("storedListar");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];

                    if (!(datos.Lector["UrlImagenTapa"] is DBNull))
                        aux.UrlImagenTapa = (string)datos.Lector["UrlImagenTapa"];

                    aux.Genero = new Estilo();
                    aux.Genero.id = (int)datos.Lector["IdEstilo"];
                    aux.Genero.Descripcion = (string)datos.Lector["Genero"];
                    aux.Formato = new TipoEdicion();
                    aux.Formato.id = (int)datos.Lector["IdTipoEdicion"];
                    aux.Formato.Descripcion = (string)datos.Lector["Formato"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex.Message);
                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error: " + ex.Message);
                return lista;
            }

        }
        public void agregarConSP(Disco nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("storedAltaDisco");
                datos.setearParametro("@titulo", nuevo.Titulo);
                datos.setearParametro("@fechalanzamiento", nuevo.FechaLanzamiento);
                datos.setearParametro("@cantidadcanciones", nuevo.CantidadCanciones);
                datos.setearParametro("@urlimagentapa", nuevo.UrlImagenTapa);
                datos.setearParametro("@idestilo", nuevo.Genero.id);
                datos.setearParametro("@idtipoedicion", nuevo.Formato.id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }













        public void agregar(Disco nuevo)
        {
            AccesoDatos datos = new AccesoDatos();


            try
            {
                datos.setearConsulta("Insert into DISCOS (Titulo, FechaLanzamiento, CantidadCanciones, idEstilo, idTipoEdicion,UrlImagenTapa)values('" + nuevo.Titulo + "','" + nuevo.FechaLanzamiento + " ','" + nuevo.CantidadCanciones + "',@idEstilo,@idTipoEdicion,@UrlImagenTapa)");
                datos.setearParametro("@idEstilo", nuevo.Genero.id);
                datos.setearParametro("@idTipoEdicion", nuevo.Formato.id);
                datos.setearParametro("@UrlImagenTapa", nuevo.UrlImagenTapa);
                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                datos.cerrarConexion();
            }


        }

        public void modificar(Disco disco)
        {


            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update DISCOS set Titulo = @titulo, FechaLanzamiento = @fecha, CantidadCanciones = @cantidad, UrlImagenTapa = @img, IdEstilo = @idEstilo, IdTipoEdicion = @idTipoEdicion where Id = @id");
                datos.setearParametro("@titulo", disco.Titulo);
                datos.setearParametro("@fecha", disco.FechaLanzamiento);
                datos.setearParametro("@cantidad", disco.CantidadCanciones);
                datos.setearParametro("@img", disco.UrlImagenTapa);
                datos.setearParametro("@idEstilo", disco.Genero.id);
                datos.setearParametro("@idTipoEdicion", disco.Formato.id);
                datos.setearParametro("@Id", disco.Id);

                datos.ejecutarAccion();



            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                datos.cerrarConexion();
            }
        }

        public void modificarConSp(Disco disco)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedModificarDisco");
                datos.setearParametro("@titulo", disco.Titulo);
                datos.setearParametro("@fechalanzamiento", disco.FechaLanzamiento);
                datos.setearParametro("@cantidadcanciones", disco.CantidadCanciones);
                datos.setearParametro("@urlimagentapa", disco.UrlImagenTapa);
                datos.setearParametro("@idEstilo", disco.Genero.id);
                datos.setearParametro("@idTipoEdicion", disco.Formato.id);
                datos.setearParametro("@Id", disco.Id);

                datos.ejecutarAccion();



            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

                datos.cerrarConexion();
            }




        }



        public void eliminar(int id)
        {

            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from DISCOS where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




        public List<Disco> filtrar(string campo, string criterio, string filtro)
        {
            List<Disco> lista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                if (string.IsNullOrEmpty(filtro))
                {
                    MessageBox.Show("El filtro no puede estar vacío.");
                    return lista;
                }

                string consulta = "SELECT Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, E.Descripcion AS Genero, T.Descripcion AS Formato, D.IdEstilo, D.IdTipoEdicion, D.id FROM DISCOS D INNER JOIN ESTILOS E ON E.id = D.idEstilo INNER JOIN TIPOSEDICION T ON T.id = D.IdTipoEdicion WHERE ";

                if (campo == "FechaLanzamiento" || campo == "CantidadCanciones")
                {

                    if (!int.TryParse(filtro, out int numericFiltro))
                    {
                        MessageBox.Show("El filtro debe ser un número.");
                        return lista;
                    }

                    if (campo == "FechaLanzamiento")
                    {
                        switch (criterio)
                        {
                            case "Año Mayor a":
                                consulta += "YEAR(FechaLanzamiento) > @filtro";
                                break;
                            case "Año Menor a":
                                consulta += "YEAR(FechaLanzamiento) < @filtro";
                                break;
                            default:
                                consulta += "YEAR(FechaLanzamiento) = @filtro";
                                break;
                        }
                    }
                    else if (campo == "CantidadCanciones")
                    {
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "CantidadCanciones > @filtro";
                                break;
                            case "Menor a":
                                consulta += "CantidadCanciones < @filtro";
                                break;
                            default:
                                consulta += "CantidadCanciones = @filtro";
                                break;
                        }
                    }
                    datos.setearParametro("@filtro", numericFiltro);
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "Titulo LIKE @filtro + '%'";
                            break;
                        case "Termina con":
                            consulta += "Titulo LIKE '%' + @filtro";
                            break;
                        default:
                            consulta += "Titulo LIKE '%' + @filtro + '%'";
                            break;
                    }
                    datos.setearParametro("@filtro", filtro);
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];

                    if (!(datos.Lector["UrlImagenTapa"] is DBNull))
                        aux.UrlImagenTapa = (string)datos.Lector["UrlImagenTapa"];

                    aux.Genero = new Estilo();
                    aux.Genero.id = (int)datos.Lector["IdEstilo"];
                    aux.Genero.Descripcion = (string)datos.Lector["Genero"];
                    aux.Formato = new TipoEdicion();
                    aux.Formato.id = (int)datos.Lector["IdTipoEdicion"];
                    aux.Formato.Descripcion = (string)datos.Lector["Formato"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error en la base de datos: " + ex.Message);
                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hubo un error: " + ex.Message);
                return lista;
            }
        }



        public List<Disco> Restablecer()
        {
            List<Disco> lista = new List<Disco>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Titulo, FechaLanzamiento, CantidadCanciones, UrlImagenTapa, E.Descripcion AS Genero, T.Descripcion AS Formato, D.IdEstilo, D.IdTipoEdicion, D.id FROM DISCOS D INNER JOIN ESTILOS E ON E.id = D.idEstilo INNER JOIN TIPOSEDICION T ON T.id = D.IdTipoEdicion";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];

                    if (!(datos.Lector["UrlImagenTapa"] is DBNull))
                        aux.UrlImagenTapa = (string)datos.Lector["UrlImagenTapa"];

                    aux.Genero = new Estilo();
                    aux.Genero.id = (int)datos.Lector["IdEstilo"];
                    aux.Genero.Descripcion = (string)datos.Lector["Genero"];
                    aux.Formato = new TipoEdicion();
                    aux.Formato.id = (int)datos.Lector["IdTipoEdicion"];
                    aux.Formato.Descripcion = (string)datos.Lector["Formato"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}

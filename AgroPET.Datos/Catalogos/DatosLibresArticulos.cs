using AgroPET.Datos.Comun;
using AgroPET.Entidades.Consultas;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AgroPET.Datos.Catalogos
{
    public class DatosLibresArticulos : DatosBase
    {
        public List<ConsultaLibresArticulos> Firebird_ObtenerLibresArticulos()
        {
            try
            {
                string Consulta = "select articulo_id, familia, categoria, Marca from LIBRES_ARTICULOS";
                DataTable dt = new DataTable();
                FbDataAdapter da = new FbDataAdapter(Consulta, ConexionFireBird);
                //da.SelectCommand.Parameters.Add("@id", 123);
                da.Fill(dt);

                List<ConsultaLibresArticulos> lst = DataTableMapToListEntity<ConsultaLibresArticulos>(dt);

                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ConsultaArticulos> Firebird_ObtenerLibresArticulos(string familia, string categoria, string marca)
        {
            try
            {
                string Consulta = " select a.articulo_id, a.linea_articulo_id, a.nombre, la.familia, la.categoria, la.marca, c.clave_articulo, ca.nombre as nombreClave " +
                                  " from articulos as a " +
                                  " inner join LIBRES_ARTICULOS as la on la.articulo_id = a.articulo_id " +
                                  " inner join claves_articulos as c on c.articulo_id = a.articulo_id " +
                                  " inner join roles_claves_articulos as ca on ca.rol_clave_art_id = c.rol_clave_art_id " +
                                  " where(" + "'" + familia + "'" + " is null or familia = " + "'" + familia + "'" + ") and familia != '' " +
                                  " and(" + "'" + categoria + "'" + " is null or categoria = " + "'" + categoria + "'" + ") and categoria != '' " +
                                  " and(" + "'" + marca + "'" + " is null or marca = " + "'" + marca + "'" + ") and marca != '' " ;

                DataTable dt = new DataTable();
                FbDataAdapter da = new FbDataAdapter(Consulta, ConexionFireBird);
                //da.SelectCommand.Parameters.Add("@id", 123);
                da.Fill(dt);

                List<ConsultaArticulos> lst = DataTableMapToListEntity<ConsultaArticulos>(dt);

                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}

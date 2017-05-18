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
    public class DatosGruposLineas : DatosBase
    {
        public List<ConsultaGruposLineas> Firebird_ObtenerGruposLineas()
        {
            try
            {
                string Consulta = "select grupo_linea_id, nombre from grupos_lineas";
                DataTable dt = new DataTable();
                FbDataAdapter da = new FbDataAdapter(Consulta, ConexionFireBird);
                //da.SelectCommand.Parameters.Add("@id", 123);
                da.Fill(dt);

                List<ConsultaGruposLineas> lst = DataTableMapToListEntity<ConsultaGruposLineas>(dt);

                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ConsultaLineasArticulos> Firebird_ObtenerGruposLineasArticulos(int grupo_linea_id)
        {
            try
            {
                string Consulta = " select gp.grupo_linea_id, la.linea_articulo_id, la.nombre " +
                                  " from lineas_articulos as la " +
                                  " inner join grupos_lineas as gp on la.grupo_linea_id = gp.grupo_linea_id " +
                                  " where gp.grupo_linea_id = " + grupo_linea_id.ToString();
                DataTable dt = new DataTable();
                FbDataAdapter da = new FbDataAdapter(Consulta, ConexionFireBird);
                //da.SelectCommand.Parameters.Add("@id", 123);
                da.Fill(dt);

                List<ConsultaLineasArticulos> lst = DataTableMapToListEntity<ConsultaLineasArticulos>(dt);

                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Firebird_ObtenerIdLineaArticulo(int idArticulo)
        {
            try
            {
                string Consulta = "select Linea_articulo_id from articulos where ARTICULO_ID = " + idArticulo;
                DataTable dt = new DataTable();
                FbDataAdapter da = new FbDataAdapter(Consulta, ConexionFireBird);
                //da.SelectCommand.Parameters.Add("@id", 123);
                da.Fill(dt);

                int linea_articulo_id = Convert.ToInt32(dt.Rows[0][0]);

                return linea_articulo_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Firebird_ObtenerIdGrupoLinea(int idLineaArticulo)
        {
            try
            {
                string Consulta = "SELECT grupo_linea_id FROM lineas_articulos where LINEA_ARTICULO_ID = " + idLineaArticulo;
                DataTable dt = new DataTable();
                FbDataAdapter da = new FbDataAdapter(Consulta, ConexionFireBird);
                //da.SelectCommand.Parameters.Add("@id", 123);
                da.Fill(dt);

                int grupo_linea_id = Convert.ToInt32(dt.Rows[0][0]);

                return grupo_linea_id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        
    }
}

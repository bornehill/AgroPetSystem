using AgroPET.Entidades.Consultas;
using AgroPET.Datos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FirebirdSql.Data.FirebirdClient;

namespace AgroPET.Datos.Catalogos
{
    public class DatosArticulos : DatosBase
    {
        public List<ConsultaArticulos> ObtenerArticulos()
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspArticulosObtener";
            return accesoDatos.ConsultaDataList<ConsultaArticulos>();
        }

        public List<ConsultaArticulos> Firebird_ObtenerArticulos(int linea_articulo_id)
        {
            try
            {
                string Consulta = " select a.articulo_id, a.nombre" +
                                  " from articulos as a" +
                                  " inner join lineas_articulos as la on la.linea_articulo_id = a.linea_articulo_id" +
                                  " where a.linea_articulo_id = " + linea_articulo_id.ToString();

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

        public List<ConsultaArticulos> ObtenerArticulosDropDown()
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspArticulosObtenerDropDown";
            return accesoDatos.ConsultaDataList<ConsultaArticulos>();
        }

		public List<T> FbQuery<T>(string query)
		{
			try
			{
				DataTable dt = new DataTable();
				FbDataAdapter da = new FbDataAdapter(query, ConexionFireBird);
				da.Fill(dt);

				List<T> lst = DataTableMapToListEntity<T>(dt);

				return lst;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public int FbInsert(string query)
		{
			try
			{
				FbCommand cmd = new FbCommand(query, new FbConnection(ConexionFireBird));
				cmd.Connection.Open();
				int row = cmd.ExecuteNonQuery();
				cmd.Connection.Close();

				return row;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
	}
}

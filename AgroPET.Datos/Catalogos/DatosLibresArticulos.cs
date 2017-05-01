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

      
    }
}

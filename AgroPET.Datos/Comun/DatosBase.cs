using AccesoDatos.Comun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AgroPET.Datos.Comun
{
    public class DatosBase
    {
        public AccesoBD accesoDatos = new AccesoBD();
        public string ConexionFireBird = string.Empty;

        public DatosBase()
        {
            accesoDatos.conexionSQL = ConexionSQL.DB_ObtenCadenaConexion();
            ConexionFireBird = @"Server=192.168.1.12;User=SYSDBA;Password=llaveacc;Database=C:\Microsip Datos\Pruebas.fdb";
        }

        public static List<T> DataTableMapToListEntity<T>(DataTable dt)
        {
            IDataReader dr = dt.CreateDataReader();

            String ActualPropertyAssign = String.Empty;
            String ActualValueAssign = String.Empty;

            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();

                foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
                {
                    var columnCount = dr.GetSchemaTable().Columns[0].Table.Compute("count(ColumnName)", String.Format("ColumnName = '{0}'", prop.Name));
                    if ((columnCount != null) && (Convert.ToInt16(columnCount) > 0))
                    {
                        ActualPropertyAssign = prop.Name;
                        ActualValueAssign = dr[prop.Name].ToString();
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                            prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }

            return list;

        }

    }
}

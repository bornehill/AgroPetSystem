using AccesoDatos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Datos.Comun
{
    public class DatosBase
    {
        public AccesoBD accesoDatos = new AccesoBD();

        public DatosBase()
        {
            accesoDatos.conexionSQL = ConexionSQL.DB_ObtenCadenaConexion();
        }



    }
}

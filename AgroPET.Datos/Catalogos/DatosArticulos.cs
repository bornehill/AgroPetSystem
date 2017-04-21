using AgroPET.Entidades.Consultas;
using AgroPET.Datos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<ConsultaArticulos> ObtenerArticulosDropDown()
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspArticulosObtenerDropDown";
            return accesoDatos.ConsultaDataList<ConsultaArticulos>();
        }
    }
}

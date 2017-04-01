using AgroPET.Entidades.Consultas;
using AgroPET.Datos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgropPET.Negocio.Catalogos
{
    public class CatalogoArticulos
    {
        public List<ConsultaArticulos> ObtenerArticulos()
        {
            List<ConsultaArticulos> lstImagArt = new DatosArticulos().ObtenerArticulos();

            return lstImagArt;
        }

        public List<ConsultaArticulos> ObtenerArticulosDropDown()
        {
            List<ConsultaArticulos> lstImagArt = new DatosArticulos().ObtenerArticulosDropDown();

            return lstImagArt;
        }
    }
}

using AgroPET.Datos.Catalogos;
using AgroPET.Entidades.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgropPET.Negocio.Catalogos
{
    public class CatalogoLibresArticulos
    {
        public List<ConsultaLibresArticulos> Firebird_ObtenerLibresArticulos()
        {
            List<ConsultaLibresArticulos> lst = new DatosLibresArticulos().Firebird_ObtenerLibresArticulos();

            return lst;
        }

        public List<ConsultaArticulos> Firebird_ObtenerLibresArticulos(string familia, string categoria, string marca)
        {
            List<ConsultaArticulos> lst = new DatosLibresArticulos().Firebird_ObtenerLibresArticulos(familia, categoria, marca);

            return lst;
        }
    }
}

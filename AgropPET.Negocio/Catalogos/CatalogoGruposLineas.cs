using AgroPET.Datos.Catalogos;
using AgroPET.Entidades.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgropPET.Negocio.Catalogos
{
    public class CatalogoGruposLineas
    {
        public List<ConsultaGruposLineas> Firebird_ObtenerGruposLineas()
        {
            List<ConsultaGruposLineas> lst = new DatosGruposLineas().Firebird_ObtenerGruposLineas();

            return lst;
        }

        public List<ConsultaLineasArticulos> Firebird_ObtenerGruposLineasArticulos(int grupo_linea_id)
        {
            List<ConsultaLineasArticulos> lst = new DatosGruposLineas().Firebird_ObtenerGruposLineasArticulos(grupo_linea_id);

            return lst;
        }
    }
}

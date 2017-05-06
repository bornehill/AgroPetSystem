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

        public int Firebird_ObtenerIdLineaArticulo(int idArticulo)
        {
            int Linea_articulo_id = new DatosGruposLineas().Firebird_ObtenerIdLineaArticulo(idArticulo);

            return Linea_articulo_id;
        }

        public int Firebird_ObtenerIdGrupoLinea(int idLineaArticulo)
        {
            int grupo_linea_id = new DatosGruposLineas().Firebird_ObtenerIdGrupoLinea(idLineaArticulo);

            return grupo_linea_id;
        }
    }
}

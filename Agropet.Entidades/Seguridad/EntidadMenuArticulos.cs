using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agropet.Entidades.Base;

namespace Agropet.Entidades.Seguridad
{
    
    [Serializable]
    [Tabla("RelMenuArticulos")]
    public class EntidadMenuArticulos
    {
        [Campo("MenuId", EsLlavePrimaria = true, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int MenuId
        {
            get;
            set;
        }

        [Campo("IdGrupo_Linea", EsLlavePrimaria = true, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int IdGrupo_Linea
        {
            get;
            set;
        }

        [Campo("IdLinea_Articulo", EsLlavePrimaria = true, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int IdLinea_Articulo
        {
            get;
            set;
        }

        [Campo("IdArticulo", EsLlavePrimaria = true, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int IdArticulo
        {
            get;
            set;
        }
    }
}

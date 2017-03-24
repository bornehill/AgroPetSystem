using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Agropet.Entidades.Base;

namespace Agropet.Entidades.Consultas
{
    [Serializable]
    [Tabla("Articulos")]
    public sealed class ConsultaArticulos
    {
        [Campo("Id_Art", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? Id_Art { get; set; }

        [Campo("Id_Linea_Art", EsParametroSP = true, EsCampoRetornoConsulta = false)]
        public int? Id_Linea_Art { get; set; }

        [Campo("Nombre", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string Nombre { get; set; }

        [Campo("Familia", EsParametroSP = true, EsCampoRetornoConsulta = false)]
        public string Familia { get; set; }

        [Campo("Categoria", EsParametroSP = true, EsCampoRetornoConsulta = false)]
        public string Categoria { get; set; }

        [Campo("Marca", EsParametroSP = true, EsCampoRetornoConsulta = false)]
        public string Marca { get; set; }
    }
}

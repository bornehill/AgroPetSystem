using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Agropet.Entidades.Base;

namespace Agropet.Entidades.Consultas
{

    [Serializable]
    [Tabla("ArtLibArt")]
    public sealed class ConsultaArtLibArt
    {
        [Campo("Fam", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string Familia { get; set; }

        [Campo("Cat", EsParametroSP = true, EsCampoRetornoConsulta = false)]
        public string Categoria { get; set; }

        [Campo("Marc", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string Marca { get; set; }
    }
}

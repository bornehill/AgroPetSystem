using Agropet.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agropet.Entidades.CatABCs
{
    [Serializable]
    [Tabla("tbbannerssitiodetalle")]
    public sealed class EntImagenesBannersDetalle
    {
        [Campo("iddetbanners", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdDetBanners { get; set; }

        [Campo("idbanner", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdBanner { get; set; }

        [Campo("pathimagen", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string PathImagen { get; set; }

        [Campo("orden", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? Orden { get; set; }

        [Campo("titulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string titulo { get; set; }

        [Campo("subtitulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string subtitulo { get; set; }
    }
}

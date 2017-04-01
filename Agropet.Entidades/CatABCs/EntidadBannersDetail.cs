using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agropet.Entidades.Base;

namespace Agropet.Entidades.CatABCs
{
  [Serializable]
  [Tabla("tbbannerssitiodetalle")]
  public sealed class EntidadBannersDetail
  {
    [Campo("iddetbanners", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public Int32? iddetbanners { get; set; }

    [Campo("idbanner", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public Int32? idbanner { get; set; }

    [Campo("titulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string titulo { get; set; }

    [Campo("subtitulo", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public string subtitulo { get; set; }

    [Campo("pathimagen", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string pathimagen { get; set; }

    [Campo("orden", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public Int32? orden { get; set; }
  }
}

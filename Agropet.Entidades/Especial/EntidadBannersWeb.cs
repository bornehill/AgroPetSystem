using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroPET.Entidades.Base;

namespace AgroPET.Entidades.Especial
{
  [Serializable]
  [Tabla("tbbannerssitiodetalle")]
  public sealed class EntidadBannersWeb
  {
    [Campo("iddetbanners", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public Int32? iddetbanners { get; set; }

    [Campo("idbanner", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public Int32? idbanner { get; set; }

    [Campo("titulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string titulo { get; set; }

    [Campo("subtitulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string subtitulo { get; set; }

    [Campo("pathimagen", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string pathimagen { get; set; }

    [Campo("orden", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public Int32? orden { get; set; }

    //[Campo("descripcion", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    //public string descripcion { get; set; }

    [Campo("fechaini", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public DateTime? fechaini { get; set; }

    [Campo("fechafin", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public DateTime? fechafin { get; set; }
  }
}

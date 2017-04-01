using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agropet.Entidades.Base;

namespace Agropet.Entidades.CatABCs
{
  [Serializable]
  [Tabla("tbbannerssitio")]
  public sealed class EntidadBanners
  {
    [Campo("idbanner", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public Int32? idbanner { get; set; }

    [Campo("descripcion", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public string descripcion { get; set; }

    [Campo("idusuariocreo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public Int32? idusuariocreo { get; set; }

    [Campo("fechacreacion", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public DateTime? fechacreacion { get; set; }

    [Campo("idusuariomodif", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public Int32? idusuariomodif { get; set; }

    [Campo("fechamodif", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public DateTime? fechamodif { get; set; }

    [Campo("fechainiaplica", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public DateTime? fechainiaplica { get; set; }

    [Campo("fechafinaplica", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public DateTime? fechafinaplica { get; set; }
  }
}

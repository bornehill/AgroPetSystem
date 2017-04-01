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
  public sealed class MenuArticulos
  {
    [Campo("MenuId", EsParametroSP = true, EsCampoRetornoConsulta = false)]
    public int? MenuId { get; set; }

    [Campo("NombreGpoMicrosip", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string NombreGpoMicrosip { get; set; }

    [Campo("NombreLinMicrosip", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string NombreLinMicrosip { get; set; }

    [Campo("NombreArticulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string NombreArticulo { get; set; }

    [Campo("IdArticulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public int IdArticulo { get; set; }

    [Campo("Image", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string Image { get; set; }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroPET.Entidades.Base;

namespace AgroPET.Entidades.Especial
{
  [Serializable]
  [Tabla("TempBuy")]
  public class EntBuy
  {
    [Campo("UserId", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public int UserId { get; set; }

    [Campo("ItemId", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public int ItemId { get; set; }

    [Campo("lot", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public Decimal lot { get; set; }

    [Campo("price", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public Decimal price { get; set; }
  }

  [Serializable]
  [Tabla("TempBuy")]
  public class EntBuyView : EntBuy {
    [Campo("NombreGpoMicrosip", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string NombreGpoMicrosip { get; set; }

    [Campo("NombreLinMicrosip", EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public string NombreLinMicrosip { get; set; }

    [Campo("NombreArticulo", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public string NombreArticulo { get; set; }

    [Campo("Image", EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public string Image { get; set; }
  }
}

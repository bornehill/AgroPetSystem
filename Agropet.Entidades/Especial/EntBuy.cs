using System;

namespace AgroPET.Entidades.Especial
{
    [Serializable]
  public class EntBuy
  {
    public int UserId { get; set; }

    public int ItemId { get; set; }

    public Decimal lot { get; set; }

    public Decimal price { get; set; }

		public string notes { get; set; }

		public Decimal off { get; set; }

		public Decimal tax { get; set; }
	}

  [Serializable]
  public class EntBuyView : EntBuy {
    public string NombreGpoMicrosip { get; set; }

    public string NombreLinMicrosip { get; set; }

    public string NombreArticulo { get; set; }

    public string Image { get; set; }
  }
}

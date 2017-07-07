using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Entidades.Especial
{
	[Serializable]
	public class EntDoctoPvDet
	{
		public int DoctoPvDetId { get; set; }

		public int DoctoPvId { get; set; }

		public string ClaveArticulo { get; set; }

		public int ArticuloId { get; set; }
		
		public Decimal Unidades { get; set; }

		public Decimal PrecioUnitario { get; set; }

		public Decimal PrecioUnitarioImpto { get; set; }

		public Decimal PctjeDscto { get; set; }

		public Decimal PrecioTotalNeto { get; set; }

		public int VendedorId { get; set; }

		public string Notas { get; set; }
	}
}

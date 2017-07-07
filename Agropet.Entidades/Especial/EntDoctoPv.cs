using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Entidades.Especial
{
	[Serializable]
	public class EntDoctoPv
	{
		public int DoctoPvId { get; set; }

		public int Client_Id { get; set; }

		public string TipoDocto { get; set; }

		public string Folio { get; set; }

		public int CajaId { get; set; }

		public int CajeroId { get; set; }

		public string ClaveCliente { get; set; }

		public int AlmacenId { get; set; }

		public int MonedaId { get; set; }

		public string ImpuestoIncluido { get; set; }

		public Decimal DsctoImporte { get; set; }

		public Decimal ImporteNeto { get; set; }

		public Decimal TotalImpuestos { get; set; }

		public int DirCliId { get; set; }

		public int VendedorId { get; set; }

		public List<EntDoctoPvDet> DetDocto { get; set; }

	}	
}

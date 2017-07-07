using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Entidades.Especial
{
	[Serializable]
	public class EntClientWeb
	{
		public int Client_Id { get; set; }

		public string Nombre { get; set; }

		public string TipoCliente { get; set; }

		public string RFC { get; set; }

		public string Calle { get; set; }

		public string Num_Exterior { get; set; }

		public string Num_Interior { get; set; }

		public string Colonia { get; set; }

		public string NomEstado { get; set; }

		public string Codigo_Postal { get; set; }

		public string Telefono1 { get; set; }

		public string Telefono2 { get; set; }

		public int VendedorId { get; set; }

		public string ClaveCliente { get; set; }
	}
}

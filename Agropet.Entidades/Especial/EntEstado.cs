using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Entidades.Especial
{
	[Serializable]
	public class EntEstado
	{
		public int EstadoId { get; set; }

		public string Nombre { get; set; }

		public string Abrev { get; set; }

		public int PaisId { get; set; }

		public int? IsPred { get; set; }
	}
}

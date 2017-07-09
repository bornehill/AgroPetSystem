using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Entidades.Especial
{
	[Serializable]
	public class EntCiudad
	{
		public int CiudadId { get; set; }

		public string Nombre { get; set; }

		public int EstadoId { get; set; }

		public int? IsPred { get; set; }
	}
}

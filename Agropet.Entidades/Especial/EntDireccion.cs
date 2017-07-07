using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Entidades.Especial
{
	[Serializable]
	public class EntDireccion
	{
		public int DirCliId { get; set; }

		public int ClienteId { get; set; }

		public string Calle { get; set; }

		public string NumExt { get; set; }

		public string NumInt { get; set; }

		public string Colonia { get; set; }

		public string Referencia { get; set; }

		public int CiudadId { get; set; }

		public int EstadoId { get; set; }

		public int PaisId { get; set; }

		public string CP { get; set; }

		public string Telefono1 { get; set; }

		public string Telefono2 { get; set; }

		public string Email { get; set; }

		public string Contacto { get; set; }

		public string NomPais { get; set; }

		public string NomEstado { get; set; }

		public string NomCiudad { get; set; }
	}
}

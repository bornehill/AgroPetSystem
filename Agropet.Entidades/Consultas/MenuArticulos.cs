using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Consultas
{
  public class MenuArticulos
  {
    public int? MenuId { get; set; }
    public string NombreGpoMicrosip { get; set; }
    public string NombreLinMicrosip { get; set; }
    public string NombreArticulo { get; set; }
    public int IdArticulo { get; set; }
		public string Clave { get; set; }
		public string Image { get; set; }
		public string Notas { get; set; }
		public Decimal Precio { get; set; }
		public Decimal Descuento { get; set; }
		public Decimal IVA { get; set; }
	}
}

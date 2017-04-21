using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Especial
{
  public class EntidadBannersWeb
  {
    public Int32? iddetbanners { get; set; }
    public Int32? idbanner { get; set; }
    public string titulo { get; set; }
    public string subtitulo { get; set; }
    public string pathimagen { get; set; }
    public Int32? orden { get; set; }
    public DateTime? fechaini { get; set; }
    public DateTime? fechafin { get; set; }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.CatABCs
{
  public class EntidadBanners
  {
    public Int32? idbanner { get; set; }

    public string descripcion { get; set; }

    public Int32? idusuariocreo { get; set; }

    public DateTime? fechacreacion { get; set; }

    public Int32? idusuariomodif { get; set; }

    public DateTime? fechamodif { get; set; }

    public DateTime? fechainiaplica { get; set; }

    public DateTime? fechafinaplica { get; set; }
  }
}

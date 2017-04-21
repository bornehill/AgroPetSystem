using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Seguridad
{
    public class EntidadPerfiles
    {
        public Int64? idperfil { get; set; }
        public string nombreperfil { get; set; }
        public string fechacreacion { get; set; }
        public Int64? idusuariocreo { get; set; }
        public string fechaultmodif { get; set; }
        public Int64? idusuarioultmodif { get; set; }
        public Int64? activo { get; set; }
    }
}

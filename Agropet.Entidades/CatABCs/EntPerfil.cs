using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agropet.Entidades.CatABCs
{
    public class EntPerfil
    {
        public int? idperfil { get; set; }
        public string nombreperfil { get; set; }
        public DateTime fechacreacion { get; set; }
        public int? idusuariocreo { get; set; }
        public DateTime fechaultmodif { get; set; }
        public int? idusuarioultmodif { get; set; }
        public int? activo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Seguridad
{
    public class EntidadDetAccesoPerfil
    {
        public int? idmenu { get; set; }
        public string nombremenu { get; set; }
        public string menuurl { get; set; }
        public int? ordenh { get; set; }
        public int? idpadre { get; set; }
        public int? iddetaccesoxperfil { get; set; }
        public string tipoacceso { get; set; }
        public string espadre { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgroPET.Entidades.Consultas
{
    public class ConsultaDetPerfil
    {
        public int? iddetaccesoxperfil { get; set; }

        public int? idperfil { get; set; }

        public int? idmenu { get; set; }

        public string nombremenu { get; set; }

        public Int64? idmenuusr { get; set; }

        public int? idpadre { get; set; }

        public int? ordenh { get; set; }

        public string espadre { get; set; }
    }
}

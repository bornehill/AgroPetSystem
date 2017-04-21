using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Consultas
{
    public  class ConsultaArticulos
    {
        public int? Id_Art { get; set; }

        public int? Id_Linea_Art { get; set; }

        public string Nombre { get; set; }

        public string Familia { get; set; }

        public string Categoria { get; set; }

        public string Marca { get; set; }
    }
}

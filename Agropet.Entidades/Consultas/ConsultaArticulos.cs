using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Consultas
{
    public  class ConsultaArticulos
    {
        public int? articulo_id { get; set; }

        public int? linea_articulo_id { get; set; }

        public string Nombre { get; set; }

        public string Familia { get; set; }

        public string Categoria { get; set; }

        public string Marca { get; set; }

        public string clave_articulo { get; set; }

        public string nombreClave { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.CatABCs
{
    public class EntImagenesArticulosDetalle
    {
        public int? IdDetImagenArticulo { get; set; }

        public int? IdImagenArticulo { get; set; }

        public string PathImagen { get; set; }

        public string FechaRegistro { get; set; }
    }
}

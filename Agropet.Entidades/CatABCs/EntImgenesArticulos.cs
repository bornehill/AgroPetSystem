using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.CatABCs
{
    public class EntImgenesArticulos
    {
        public int? IdImagenArticulo { get; set; }

        public int? IdArticulo { get; set; }

        public string NombreArticulo { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaModificacion { get; set; }

        public int? IdUsuarioCreo { get; set; }

        public int? IdUsuarioModifico { get; set; }

        public string pathimagen { get; set; }  
    }
}

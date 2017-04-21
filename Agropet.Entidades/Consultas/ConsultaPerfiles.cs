using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Consultas
{
    public class ConsultaPerfiles
    {
        public int? idperfil { get; set; }

        public string nombreperfil { get; set; }

        public DateTime? fechacreacion { get; set; }

        public int? idusuariocreo { get; set; }

        public string UsuarioCreo { get; set; }

        public DateTime? fechaultmodif { get; set; }

        public int? idusuarioultmodif { get; set; }

        public string UsuarioModif { get; set; }

        public int? activo { get; set; }

        public string Estatus { get; set; }
    }
}

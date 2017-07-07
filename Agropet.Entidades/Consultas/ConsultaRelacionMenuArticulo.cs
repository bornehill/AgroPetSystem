using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agropet.Entidades.Consultas
{
    public class ConsultaRelacionMenuArticulo
    {
        public int MenuId { get; set; }
        public int IdGrupo_Linea { get; set; }
        public int IdLinea_Articulo { get; set; }
        public int IdArticulo { get; set; }
        public bool Activo { get; set; }
    }
}

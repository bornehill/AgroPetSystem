using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Seguridad
{
    public class EntidadMenuArticulos
    {
        public int MenuId
        {
            get;
            set;
        }

        public int IdGrupo_Linea
        {
            get;
            set;
        }

        public int IdLinea_Articulo
        {
            get;
            set;
        }

        public int IdArticulo
        {
            get;
            set;
        }
    }
}

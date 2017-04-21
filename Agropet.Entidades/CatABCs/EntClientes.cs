using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Entidades.CatABCs
{
    public class EntClientes
    {
        public Int32? userid { get; set; }

        public string nombre { get; set; }

        public string contacto1 { get; set; }

        public string estatus { get; set; }

        public string causa_susp { get; set; }

        public decimal? limite_credito { get; set; }

        public int? moneda_id { get; set; }

        public int? cond_pago_id { get; set; }

        public int? tipo_cliente_id { get; set; }

        public string usuario_creador { get; set; }

        public DateTime? fecha_hora_creacion { get; set; }

        public string usuario_ult_modif { get; set; }

        public DateTime? fecha_hora_ult_modif { get; set; }
    }
}

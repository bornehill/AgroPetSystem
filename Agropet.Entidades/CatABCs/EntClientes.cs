using Agropet.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agropet.Entidades.CatABCs
{
    [Serializable]
    [Tabla("tbclientes_web")]
    public sealed class EntClientes
    {
        [Campo("userid", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public Int32? userid { get; set; }

        [Campo("nombre", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string nombre { get; set; }

        [Campo("contacto1", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string contacto1 { get; set; }

        [Campo("estatus", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string estatus { get; set; }

        [Campo("causa_susp", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string causa_susp { get; set; }

        [Campo("limite_credito", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public decimal? limite_credito { get; set; }

        [Campo("moneda_id", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? moneda_id { get; set; }

        [Campo("cond_pago_id", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? cond_pago_id { get; set; }

        [Campo("tipo_cliente_id", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? tipo_cliente_id { get; set; }

        [Campo("usuario_creador", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string usuario_creador { get; set; }

        [Campo("fecha_hora_creacion", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public DateTime? fecha_hora_creacion { get; set; }

        [Campo("usuario_ult_modif", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string usuario_ult_modif { get; set; }

        [Campo("fecha_hora_ult_modif", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public DateTime? fecha_hora_ult_modif { get; set; }
    }
}

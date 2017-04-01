using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Seguridad
{
    using AgroPET.Entidades.Base;

    [Serializable]
    [Tabla("tbdetaccesosxperfil")]
    public sealed class EntidadDetAccesoPerfil
    {
        [Campo("idmenu", EsLlavePrimaria = true, EsCampoRetornoConsulta = true)]
        public int? idmenu { get; set; }

        [Campo("nombremenu", EsCampoRetornoConsulta = true)]
        public string nombremenu { get; set; }

        [Campo("menuurl", EsCampoRetornoConsulta = true)]
        public string menuurl { get; set; }

        [Campo("ordenh", EsCampoRetornoConsulta = true)]
        public int? ordenh { get; set; }

        [Campo("idpadre", EsCampoRetornoConsulta = true)]
        public int? idpadre { get; set; }

        [Campo("iddetaccesoxperfil", EsCampoRetornoConsulta = true)]
        public int? iddetaccesoxperfil { get; set; }

        [Campo("tipoacceso", EsCampoRetornoConsulta = true)]
        public string tipoacceso { get; set; }

        [Campo("espadre", EsCampoRetornoConsulta = true)]
        public string espadre { get; set; }
    }
}

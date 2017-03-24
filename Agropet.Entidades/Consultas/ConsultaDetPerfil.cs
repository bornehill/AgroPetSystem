using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Agropet.Entidades.Base;

namespace Agropet.Entidades.Consultas
{
    [Serializable]
    [Tabla("DetPerfiles")]
    public sealed class ConsultaDetPerfil
    {
        [Campo("iddetaccesoxperfil", EsLlavePrimaria = false, EsCampoRetornoConsulta = true)]
        public int? iddetaccesoxperfil { get; set; }

        [Campo("idperfil", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? idperfil { get; set; }

        [Campo("idmenu", EsCampoRetornoConsulta = true, EsParametroSP = false)]
        public int? idmenu { get; set; }

        [Campo("nombremenu", EsCampoRetornoConsulta = true, EsParametroSP = false)]
        public string nombremenu { get; set; }

        [Campo("idmenuusr", EsCampoRetornoConsulta = true, EsParametroSP = false)]
        public Int64? idmenuusr { get; set; }

        [Campo("idpadre", EsCampoRetornoConsulta = true, EsParametroSP = false)]
        public int? idpadre { get; set; }

        [Campo("ordenh", EsCampoRetornoConsulta = true, EsParametroSP = false)]
        public int? ordenh { get; set; }

        [Campo("espadre", EsCampoRetornoConsulta = true, EsParametroSP = false)]
        public string espadre { get; set; }
    }
}

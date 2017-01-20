using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Agropet.Entidades.Base;

namespace Agropet.Entidades.CatABCs
{
    public class DetAccesosPerfil
    {
        [Campo("iddetaccesoxperfil", EsLlavePrimaria = true, EsCampoRetornoConsulta = true)]
        public int? iddetaccesoxperfil { get; set; }

        [Campo("idperfil", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? idperfil { get; set; }

        [Campo("idmenu", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? idmenu { get; set; }

        [Campo("tipoproceso", EsParametroSP = true, EsCampoRetornoConsulta = false)]
        public string tipoproceso { get; set; }

    }
}

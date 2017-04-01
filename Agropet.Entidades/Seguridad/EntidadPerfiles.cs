using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agropet.Entidades.Seguridad
{
    using Agropet.Entidades.Base;

    [Serializable]
    [Tabla("Perfiles")]
    public sealed class EntidadPerfiles
    {
        [Campo("idperfil", EsLlavePrimaria = true, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public Int64? idperfil { get; set; }

        [Campo("nombreperfil", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string nombreperfil { get; set; }

        [Campo("fechacreacion", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string fechacreacion { get; set; }

        [Campo("idusuariocreo", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public Int64? idusuariocreo { get; set; }

        [Campo("fechaultmodif", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string fechaultmodif { get; set; }

        [Campo("idusuarioultmodif", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public Int64? idusuarioultmodif { get; set; }

        [Campo("activo", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public Int64? activo { get; set; }
    }
}

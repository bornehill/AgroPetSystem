using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Agropet.Entidades.Base;

namespace Agropet.Entidades.Consultas
{
    [Serializable]
    [Tabla("Perfiles")]
    public sealed class ConsultaPerfiles
    {
        [Campo("idperfil", EsLlavePrimaria = true, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? idperfil { get; set; }

        [Campo("nombreperfil", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string nombreperfil { get; set; }

        [Campo("fechacreacion", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string fechacreacion { get; set; }

        [Campo("idusuariocreo", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? idusuariocreo { get; set; }

        [Campo("UsuarioCreo", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string UsuarioCreo { get; set; }

        [Campo("fechaultmodif", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string fechaultmodif { get; set; }

        [Campo("idusuarioultmodif", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public Int64? idusuarioultmodif { get; set; }

        [Campo("UsuarioModif", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string UsuarioModif { get; set; }

        [Campo("activo", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = false)]
        public int? activo { get; set; }

        [Campo("Estatus", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string Estatus { get; set; }

    }
}

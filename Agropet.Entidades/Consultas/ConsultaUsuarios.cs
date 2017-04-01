using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgroPET.Entidades.Base;

namespace AgroPET.Entidades.Consultas
{
    [Serializable]
    [Tabla("Usuarios")]
    public sealed class ConsultaUsuarios
    {
        [Campo("IdUsuario", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? IdUsuario { get; set; }

        [Campo("IdPerfil", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? IdPerfil { get; set; }

        [Campo("NombrePerfil", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string NombrePerfil { get; set; }

        [Campo("NombreUsr", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string NombreUsr { get; set; }

        [Campo("ClaveUsr", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string ClaveUsr { get; set; }

        [Campo("PasswordUsr", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string PasswordUsr { get; set; }

        [Campo("Activo", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? Activo { get; set; }

        [Campo("Estatus", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string Estatus { get; set; }

        [Campo("IdUsuarioCreo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdUsuarioCreo { get; set; }

        [Campo("FechaCreacion", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string FechaCreacion { get; set; }

        [Campo("UsuarioCreo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string UsuarioCreo { get; set; }

        [Campo("IdUsuarioModif", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdUsuarioModif { get; set; }

        [Campo("FechaModif", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string FechaUltModif { get; set; }

        [Campo("UsuarioUltModif", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string UsuarioUltModif { get; set; }

    }
}

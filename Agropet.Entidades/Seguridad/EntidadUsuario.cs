using System;

namespace AgroPET.Entidades.Seguridad
{
    using AgroPET.Entidades.Base;

    [Serializable]
    [Tabla("Usuarios")]
    public sealed class EntidadUsuario
    {

        [Campo("idusuario", EsLlavePrimaria = true, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? idusuario { get; set; }

        [Campo("idperfil", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? idperfil { get; set; }

        [Campo("nombreperfil", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string NombrePerfil { get; set; }

        [Campo("nombreusr", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string nombreusr { get; set; }

        [Campo("claveusr", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string claveusr { get; set; }

        [Campo("passwordusr", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string passwordusr { get; set; }

        [Campo("activo", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? activo { get; set; }

        [Campo("UsuarioCreo", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? UsuarioCreo { get; set; }

        [Campo("FechaCreacion", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string FechaCreacion { get; set; }

        [Campo("UsuarioModif", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public Int64? UsuarioModif { get; set; }

        [Campo("FechaUltModif", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string FechaUltModif { get; set; }

    }
}

using System;

namespace Agropet.Entidades.Seguridad
{
    using Agropet.Entidades.Base;
    [Serializable]
    [Tabla("tbusuarios")]
    public sealed class EntidadUsuarioLogeado
    {

        [Campo("idusuario", EsLlavePrimaria = true,EsCampoRetornoConsulta=true)]
        public Int64 idusuario { get; set; }

        [Campo("idperfil", EsCampoRetornoConsulta = true)]
        public Int64 idperfil { get; set; }

        [Campo("nombre", EsCampoRetornoConsulta = true)]
        public string nombre { get; set; }

        [Campo("claveusuario", EsCampoRetornoConsulta = true)]
        public string claveusuario { get; set; }

        [Campo("passwdusr", EsCampoRetornoConsulta = true)]
        public string passwdusr { get; set; }

        [Campo("activo", EsCampoRetornoConsulta = true)]
        public Int64 activo { get; set; }

        [Campo("nombreperfil", EsCampoRetornoConsulta = true)]
        public string nombreperfil { get; set; }

    }
}

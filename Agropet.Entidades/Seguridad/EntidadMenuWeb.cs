using System;

namespace AgroPET.Entidades.Seguridad
{
    using AgroPET.Entidades.Base;

    [Serializable]
    [Tabla("menuweb")]
    public sealed class EntidadMenuWeb
    {
        [Campo("MenuId", EsLlavePrimaria = true, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int MenuId
        {
            get;
            set;
        }

        [Campo("Menu", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string Menu
        {
            get;
            set;
        }

        [Campo("MenuUrl", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string MenuUrl
        {
            get;
            set;
        }

        [Campo("Padre", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int Padre
        {
            get;
            set;
        }

        [Campo("Nivel", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int Nivel
        {
            get;
            set;
        }

        [Campo("Orden", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int Orden
        {
            get;
            set;
        }

        [Campo("Activo", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public bool Activo
        {
            get;
            set;
        }

        [Campo("CreacionUsuarioId", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public long CreacionUsuarioId
        {
            get;
            set;
        }

        [Campo("FechaCreacion", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public DateTime? FechaCreacion
        {
            get;
            set;
        }

        [Campo("ModificacionUsuarioId", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public long ModificacionUsuarioId
        {
            get;
            set;
        }

        [Campo("FechaModificacion", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public DateTime? FechaModificacion
        {
            get;
            set;
        }

        [Campo("PadreNom", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string PadreNom
        {
            get;
            set;
        }
    }
}

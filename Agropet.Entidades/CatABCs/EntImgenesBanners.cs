using Agropet.Entidades.Base;
using System;

namespace Agropet.Entidades
{
    [Serializable]
    [Tabla("tbBannersSitio")]

    public sealed class EntImgenesBanners
    {
        [Campo("IdBanner", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdBanner { get; set; }

        [Campo("Descripcion", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string Descripcion { get; set; }

        [Campo("IdUsuarioCreo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdUsuarioCreo { get; set; }

        [Campo("FechaCreacion", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public DateTime FechaCreacion { get; set; }

        [Campo("IdUsuarioModif", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdUsuarioModif { get; set; }

        [Campo("FechaModif", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public DateTime FechaModif { get; set; }

        [Campo("FechaIniAplica", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public DateTime FechaIniAplica { get; set; }

        [Campo("FechaFinAplica", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public DateTime FechaFinAplica { get; set; }

    }
}

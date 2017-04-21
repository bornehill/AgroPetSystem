using System;

namespace AgroPET.Entidades
{
    public class EntImgenesBanners
    {
        public int? IdBanner { get; set; }

        public string Descripcion { get; set; }

        public int? IdUsuarioCreo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int? IdUsuarioModif { get; set; }

        public DateTime FechaModif { get; set; }

        public DateTime FechaIniAplica { get; set; }

        public DateTime FechaFinAplica { get; set; }

    }
}

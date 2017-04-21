using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.CatABCs
{
    public class EntImagenesBannersDetalle
    {
        public int? IdDetBanners { get; set; }

        public int? IdBanner { get; set; }

        public string PathImagen { get; set; }

        public int? Orden { get; set; }

        public string titulo { get; set; }

        public string subtitulo { get; set; }
    }
}

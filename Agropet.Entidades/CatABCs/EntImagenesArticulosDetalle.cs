using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.CatABCs
{
    using AgroPET.Entidades.Base;

    [Serializable]
    [Tabla("tbImagenesArticulosdet")]
    public sealed class EntImagenesArticulosDetalle
    {
        [Campo("IdDetImagenArticulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdDetImagenArticulo { get; set; }

        [Campo("IdImagenArticulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdImagenArticulo { get; set; }

        [Campo("PathImagen", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string PathImagen { get; set; }

        [Campo("FechaRegistro", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string FechaRegistro { get; set; }
    }
}

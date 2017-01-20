using Agropet.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agropet.Entidades.CatABCs
{
    using Agropet.Entidades.Base;

    [Serializable]
    [Tabla("tbImagenesArticulos")]
    public sealed class EntImgenesArticulos
    {
        [Campo("IdImagenArticulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdImagenArticulo { get; set; }

        [Campo("IdArticulo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdArticulo { get; set; }

        [Campo("NombreArticulo", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public string NombreArticulo { get; set; }

        [Campo("FechaRegistro", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string FechaRegistro { get; set; }

        [Campo("FechaModificacion", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string FechaModificacion { get; set; }

        [Campo("IdUsuarioCreo", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdUsuarioCreo { get; set; }

        [Campo("IdUsuarioModifico", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? IdUsuarioModifico { get; set; }
    }
}

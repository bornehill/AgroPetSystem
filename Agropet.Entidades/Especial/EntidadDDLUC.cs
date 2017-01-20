using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agropet.Entidades.Base;

namespace Agropet.Entidades.Especial
{
    [Serializable]
    [Tabla("DDLItems")]
    public sealed class EntidadDDLUC
    {
        [Campo("nOpcionSeleccion", EsParametroSP = true, EsCampoRetornoConsulta = false)]
        public Int64? nOpcionSeleccion { get; set; }

        [Campo("IdValor", EsParametroSP=false, EsCampoRetornoConsulta = true)]
        public int? IdValor { get; set; }

        [Campo("DescripValor", EsParametroSP=false, EsCampoRetornoConsulta = true)]
        public string DescripValor { get; set; }
    }
}

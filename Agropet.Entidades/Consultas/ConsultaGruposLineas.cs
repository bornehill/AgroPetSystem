using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Agropet.Entidades.Base;

namespace Agropet.Entidades.Consultas
{
    [Serializable]
    [Tabla("grupos_Lineas")]
    public sealed class ConsultaGruposLineas
    {
        [Campo("Id_Gpo_Lin", EsParametroSP = true, EsCampoRetornoConsulta = true)]
        public int? Id_Gpo_Lin { get; set; }

        [Campo("Nombre", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string Nombre { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgroPET.Entidades.Base;

namespace AgroPET.Entidades.Consultas
{
    [Serializable]
    [Tabla("Lineas_Articulos")]
    public sealed class ConsultaLineasArticulos
    {
        [Campo("Id_Lin_Art", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public int? Id_Lin_Art { get; set; }

        [Campo("Id_Gpo_Lin", EsParametroSP = true, EsCampoRetornoConsulta = false)]
        public int? Id_Gpo_Lin { get; set; }

        [Campo("Nombre", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string Nombre { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroPET.Entidades.Base;

namespace AgroPET.Entidades.Especial
{
    [Serializable]
    [Tabla("")]
    public sealed class EntidadResultProceso
    {

        [Campo("MsgProceso", EsParametroSP = false, EsCampoRetornoConsulta = true)]
        public string MsgProceso { get; set; }

    }
}

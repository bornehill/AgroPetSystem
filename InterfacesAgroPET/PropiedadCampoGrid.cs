using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesAgroPET
{
    [Serializable]
    public class PropiedadCampoGrid
    {
        /// <summary>
        /// Obtiene o establece el nombre del campo.
        /// </summary>
        public string NombreCampo { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del encabezado de la columna.
        /// </summary>
        public string EncabezadoCampo { get; set; }

        /// <summary>
        /// Obtiene o establece el formato que se debe desplegar para la columna dada.
        /// </summary>
        public string FormatoColumna { get; set; }

    }
}

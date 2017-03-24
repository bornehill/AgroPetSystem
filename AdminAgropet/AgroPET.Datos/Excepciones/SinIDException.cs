using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Datos.Excepciones
{
    /// <summary>
    /// Excepción cuando no hay un Atributo de ID asignado a una clase de entidad de negocio
    /// </summary>
    public class SinIDException : Exception
    {
        #region Constantes

        private const string MensajeDefault = "La entidad de negocio no tiene asignado un ID.";

        #endregion

        #region Variables privadas

        private string _nombreEntidadNegocio;

        #endregion

        #region Propiedades

        /// <summary>
        /// Obtiene el nombre de la entidad de negocio que no tiene el identificador único.
        /// </summary>
        public string NombreEntidadNegocio
        {
            get { return this._nombreEntidadNegocio; }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Excepción cuando no hay un ID asignado en una entidad de negocio
        /// </summary>
        /// <param name="sNombreEntidadNegocio">Nombre de la entidad de negocio</param>
        public SinIDException(string sNombreEntidadNegocio)
            : base(MensajeDefault)
        {
            this._nombreEntidadNegocio = sNombreEntidadNegocio;
        }

        /// <summary>
        /// Excepción cuando no hay un ID asignado en una entidad de negocio 
        /// con descripción de excepción personalizada
        /// </summary>
        /// <param name="sMensajeExcepcion">Descripción personalizada de la Excepción</param>
        /// <param name="sNombreEntidadNegocio">Nombre de la entidad de negocio</param>
        public SinIDException(string sMensajeExcepcion, string sNombreEntidadNegocio)
            : base(sMensajeExcepcion)
        {
            this._nombreEntidadNegocio = sNombreEntidadNegocio;
        }

        #endregion

    }
}

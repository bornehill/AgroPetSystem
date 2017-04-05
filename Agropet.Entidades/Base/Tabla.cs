using System;
using System.Collections.Generic;

namespace AgroPET.Entidades.Base
{
    /// <summary>
    /// Clase de tipo atributo para asignar el nombre de la tabla en la base de datos 
    /// a una entidad de negocio.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class TablaAttribute : Attribute
    {
        #region Variables

        private string _nombreTabla;
        private string _nombreTipo;

        #endregion

        #region Propiedades

        /// <summary>
        /// Obtiene el nombre de la tabla
        /// </summary>
        public string NombreTabla
        {
            get { return _nombreTabla; }
        }

        /// <summary>
        /// Obtiene el nombre del tipo de Estructura
        /// </summary>
        public string NombreTipo
        {
            get { return _nombreTipo; }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Atributo del nombre de la tabla en la base de datos
        /// </summary>
        /// <param name="nombreTabla">Asigna el nombre de la tabla</param>
        public TablaAttribute(string nombreTabla)
        {
            this._nombreTabla = nombreTabla;
            this._nombreTipo = string.Empty;
        }

        /// <summary>
        /// Constructor para tipo de datos
        /// </summary>
        /// <param name="nombreTabla">Nombre de la Tabla</param>
        /// <param name="nombreTipo">Nombre del Tipo de dato</param>
        public TablaAttribute(string nombreTabla, string nombreTipo)
        {
            this._nombreTabla = nombreTabla;
            this._nombreTipo = nombreTipo;
        }

        #endregion

    }
}

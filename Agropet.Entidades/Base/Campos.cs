using System;
using System.Collections.Generic;

namespace Agropet.Entidades.Base
{
    /// <summary>
    /// Clase de tipo atributo para asignar el nombre del campo de la base de datos 
    /// a una entidad de negocio.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class CampoAttribute : Attribute
    {

        #region Variables

        private string _nombreCampo;
        private bool _esLlavePrimaria;
        private bool _esParametroSP;
        private bool _esCampoRetornoConsulta;

        #endregion

        #region Propiedades

        /// <summary>
        /// Obtiene el nombre del campo
        /// </summary>
        public string NombreCampo
        {
            get { return this._nombreCampo; }
        }

        /// <summary>
        /// Obtiene o establece cuando un campo es llave primaria o no.
        /// </summary>
        public bool EsLlavePrimaria
        {
            get { return this._esLlavePrimaria; }
            set { this._esLlavePrimaria = value; }
        }

        /// <summary>
        /// Obtiene o establece cuando un campo se agrega como parametro en una llamada a un storeprocedure
        /// </summary>
        public bool EsParametroSP
        {
            get { return this._esParametroSP; }
            set { this._esParametroSP = value; }
        }

        /// <summary>
        /// Obtiene o establece cuando un campo es un dato que regresa una consulta llamado de un stroreprocedure
        /// </summary>
        public bool EsCampoRetornoConsulta
        {
            get { return this._esCampoRetornoConsulta; }
            set { this._esCampoRetornoConsulta = value; }
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Atributo del nombre del campo de la base de datos
        /// </summary>
        /// <param name="nombreCampo">Asigna el nombre del campo</param>
        public CampoAttribute(string nombreCampo)
        {
            this._nombreCampo = nombreCampo;
            this._esLlavePrimaria = false;
            this._esParametroSP = true;
            this._esCampoRetornoConsulta = true;
        }

        #endregion
    }
}

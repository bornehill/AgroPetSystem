using System;

namespace Utilidades.Controles
{
    [Serializable()]
    public sealed class CustomListItem
    {
        #region Propiedades
        public string Valor { get; set; }
        public string Descripcion { get; set; }
        #endregion
        #region Métodos
        public override string ToString()
        {
            return Descripcion;
        }
        #endregion
    }
}
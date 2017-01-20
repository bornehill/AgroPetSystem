using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilidades.Controles
{
    [Serializable()]
    public sealed class CustomListFtp
    { 
        #region Propiedades

        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public bool Directory { get; set; }
        public string Predecessor { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        #endregion

        #region Métodos
        public override string ToString()
        {
            return Description;
        }
        #endregion
    }
}
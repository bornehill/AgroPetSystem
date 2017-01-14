using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//By Sinersys
//Created by Emmanuel lohora mejia
//Fecha : 07/20/2011
namespace Utilidades.Ftp
{
    [Serializable]
    public class FTPConnector : ICloneable
    {
        public string Adress { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string locationFolder { get; set; }

        public FTPConnector() { }

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
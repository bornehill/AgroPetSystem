using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Consultas
{
 
    public class ConsultaLibresArticulos
    {
        private string _Familia;
        public string Familia
        {
            get {
                if (string.IsNullOrEmpty(_Familia))
                    _Familia = string.Empty;

                return _Familia;
            }
            set { _Familia = value; }
        }        

        private string _Categoria;
        public string Categoria
        {
            get
            {
                if (string.IsNullOrEmpty(_Categoria))
                    _Categoria = string.Empty;

                return _Categoria;
            }
            set { _Categoria = value; }
        }

        private string _Marca;
        public string Marca
        {
            get
            {
                if (string.IsNullOrEmpty(_Marca))
                    _Marca = string.Empty;

                return _Marca;
            }
            set { _Marca = value; }
        }
    }
}

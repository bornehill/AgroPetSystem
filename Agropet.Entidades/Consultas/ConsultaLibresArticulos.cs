using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agropet.Entidades.Base;

namespace Agropet.Entidades.Consultas
{
    [Serializable]
    [Tabla("LibArt")]
    public class ConsultaLibresArticulos
    {
        //[Campo("Id_Art", EsLlavePrimaria = true, EsParametroSP = true, EsCampoRetornoConsulta = true)]
        //public int Id_Art
        //{
        //    get;
        //    set;
        //}

        [Campo("Familia", EsCampoRetornoConsulta = true)]
        public string Familia
        {
            get;
            set;
        }

        [Campo("Categoria", EsCampoRetornoConsulta = true)]
        public string Categoria
        {
            get;
            set;
        }

        [Campo("Marca", EsCampoRetornoConsulta = true)]
        public string Marca
        {
            get;
            set;
        }
    }
}

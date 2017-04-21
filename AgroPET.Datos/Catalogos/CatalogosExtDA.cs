using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Seguridad;
using AgroPET.Datos.Comun;
using Utilidades;

namespace AgroPET.Datos.Catalogos
{
    public class CatalogosExtDA : DatosBase
    {

        #region Variables privadas

        private string _mensajeAlerta = string.Empty;
        private int? _valorReturn;

        #endregion

        #region Propiedades

        public string MensajeAlerta
        {
            get { return _mensajeAlerta; }
        }

        public int? ValorReturn
        {
            get { return _valorReturn; }
        }

        #endregion

        public List<EntidadMenuWeb> ObtenerMenuWeb(EntidadMenuWeb tEntidadNegocio)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "usp_MenuWebObtener";
            return accesoDatos.ConsultaDataList<EntidadMenuWeb>();
        }

        public List<EntidadBannersWeb> ObtenerBannersWeb(EntidadBannersWeb tEntidadNegocio)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "usp_GetBannersWeb";
            return accesoDatos.ConsultaDataList<EntidadBannersWeb>();
        }

        public List<MenuArticulos> GetMenuArticulos(MenuArticulos tMenuArticulo)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "usp_GetMenuArticulos";
            return accesoDatos.ConsultaDataList<MenuArticulos>();
        }
    }
}
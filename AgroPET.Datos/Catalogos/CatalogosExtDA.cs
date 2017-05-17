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
    public class CatalogosExtDA
    {

        #region Variables privadas
        public AccesoDatos.Comun.AccesoBD accesoDatos = new AccesoDatos.Comun.AccesoBD();
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

        public CatalogosExtDA() {
          accesoDatos.conexionSQL = AccesoDatos.Comun.ConexionSQL.DB_ObtenCadenaConexion("Default");
        }
        #endregion

        public List<EntidadMenuWeb> ObtenerMenuWeb(EntidadMenuWeb tEntidadNegocio)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "usp_MenuWebObtener";
            accesoDatos.parametros.Agrega("@Menuid", tEntidadNegocio.MenuId, true);
            accesoDatos.parametros.Agrega("@Menu", tEntidadNegocio.Menu, true);
            return accesoDatos.ConsultaDataList<EntidadMenuWeb>();
        }

        public List<EntidadMenuWeb> GetMenuHijos(EntidadMenuWeb tEntidadNegocio)
        {
          accesoDatos.parametros.listaParametros.Clear();
          accesoDatos.comandoSP = "usp_GetMenuHijos";
          accesoDatos.parametros.Agrega("@PadreMenuId", tEntidadNegocio.MenuId, true);
          return accesoDatos.ConsultaDataList<EntidadMenuWeb>();
        }
        public List<EntidadBannersWeb> ObtenerBannersWeb(EntidadBannersWeb tEntidadNegocio)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "usp_GetBannersWeb";
            accesoDatos.parametros.Agrega("@idbanner", tEntidadNegocio.idbanner, true);
            accesoDatos.parametros.Agrega("@fechaini", tEntidadNegocio.fechaini, true);
            accesoDatos.parametros.Agrega("@fechafin", tEntidadNegocio.fechafin, true);
            return accesoDatos.ConsultaDataList<EntidadBannersWeb>();
        }

        public List<MenuArticulos> GetMenuArticulos(MenuArticulos tMenuArticulo)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "usp_GetMenuArticulos";
            accesoDatos.parametros.Agrega("@Menuid", tMenuArticulo.MenuId, true);
            return accesoDatos.ConsultaDataList<MenuArticulos>();
        }

        public EntUser GetUser(EntUser user)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspGetUser";
            accesoDatos.parametros.Agrega("@userid", user.UserId, true);
            if (user.User_Name != null && user.User_Name.Length > 0)
                accesoDatos.parametros.Agrega("@username", user.User_Name, true);
            else
                accesoDatos.parametros.Agrega("@username", DBNull.Value, true);
            if (user.Pass != null && user.Pass.Length > 0)
                accesoDatos.parametros.Agrega("@pass", user.Pass, true);
            else
                accesoDatos.parametros.Agrega("@pass", DBNull.Value, true);

            return accesoDatos.ConsultaDataList<EntUser>().FirstOrDefault();
        }

        public EntUser AddUser(EntUser user)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspCreateUser";
            accesoDatos.parametros.Agrega("@username", user.User_Name, true);
            accesoDatos.parametros.Agrega("@pass", user.Pass, true);

            return accesoDatos.ConsultaDataList<EntUser>().FirstOrDefault();
        }

        public EntBuy AddBuy(EntBuy item)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspAddBuy";
            accesoDatos.parametros.Agrega("@userId", item.UserId, true);
            accesoDatos.parametros.Agrega("@itemId", item.ItemId, true);
            accesoDatos.parametros.Agrega("@lot", item.lot, true);
            accesoDatos.parametros.Agrega("@price", item.price, true);

            return accesoDatos.ConsultaDataList<EntBuy>().FirstOrDefault();
        }

        public List<EntBuyView> GetBuyView(EntBuyView item)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspGetBuyView";
            accesoDatos.parametros.Agrega("@userId", item.UserId, true);

            return accesoDatos.ConsultaDataList<EntBuyView>();
        }

        public int GetTotalMenuArt(MenuArticulos menu)
        {
            var t = accesoDatos.ConsultaDataSet("select dbo.ufn_GetTotalMenuArt(" + menu.MenuId + ")");
            return (int)t.Tables[0].Rows[0].ItemArray[0];
        }

        public EntBuy GetTotalBuy(EntBuy buy)
        {
          accesoDatos.parametros.listaParametros.Clear();
          accesoDatos.comandoSP = "uspGetTotalBuy";
          accesoDatos.parametros.Agrega("@userId", buy.UserId, true);

          return accesoDatos.ConsultaDataList<EntBuy>().FirstOrDefault();
        }

        public MenuArticulos GetArticulo(MenuArticulos art)
        {
          accesoDatos.parametros.listaParametros.Clear();
          accesoDatos.comandoSP = "usp_GetArticulo";
          accesoDatos.parametros.Agrega("@IdArticulo", art.IdArticulo, true);

          return accesoDatos.ConsultaDataList<MenuArticulos>().FirstOrDefault();
        }
  }
}
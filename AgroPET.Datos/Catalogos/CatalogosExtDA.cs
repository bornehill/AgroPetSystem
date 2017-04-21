using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;
using AgroPET.Entidades.Base;
using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Seguridad;
using AgroPET.Datos.Comun;
using AgroPET.Datos.Excepciones;
using Utilidades;
using AccesoDatos.Comun;

namespace AgroPET.Datos.Catalogos
{
  public class CatalogosExtDA
  {
    public AccesoBD accesoDatos = new AccesoBD();

    public CatalogosExtDA()
    {
      accesoDatos.conexionSQL = ConexionSQL.DB_ObtenCadenaConexion("Default");
    }

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

    public void GuardaNuevoDetallePerfil(string sCadenaDetalleAccesos)
    {
      int IdPerfil;
      int IdMenu;
      int nPosIgual;
      string sValor;
      string VProceso;
      string[] sCadItems1;
      string[] sCadItems2;
      EjecutaSP espActualizaDetalleAccesos = new EjecutaSP("uspDetAccesosxPerfil_Guarda");

      try
      {
        sCadItems1 = sCadenaDetalleAccesos.Split('|');
        foreach (string sUnDetalleAcceso in sCadItems1)
        {
          espActualizaDetalleAccesos.LimpiarParametros();
          sCadItems2 = sUnDetalleAcceso.Split('#');

          //Obtengo el valor del id del perfil
          sValor = sCadItems2[1].Trim();
          nPosIgual = sValor.IndexOf('=', 0);
          IdPerfil = int.Parse(sValor.Substring(nPosIgual + 1));

          //Obtengo el valor del id del menu
          sValor = sCadItems2[2].Trim();
          nPosIgual = sValor.IndexOf('=', 0);
          IdMenu = int.Parse(sValor.Substring(nPosIgual + 1));

          //Obtengo el valor del proceso a realizar con el item
          sValor = sCadItems2[3].Trim();
          VProceso = sValor;

          #region Asignación de parametros

          //Definicion del parametro del id del perfil
          MySqlParameter spMySqlIdPerfil = new MySqlParameter("IdPerfil", MySqlDbType.Int32);
          spMySqlIdPerfil.Value = IdPerfil;
          espActualizaDetalleAccesos.AgregarParametro(spMySqlIdPerfil);

          //Definicion del parametro del id del menu
          MySqlParameter spMySqlIdMenu = new MySqlParameter("IdMenu", MySqlDbType.Int32);
          spMySqlIdMenu.Value = IdMenu;
          espActualizaDetalleAccesos.AgregarParametro(spMySqlIdMenu);

          //Definicion del parametro del tipo de proceso a realizar
          MySqlParameter spMySqlTipoProc = new MySqlParameter("VProceso", MySqlDbType.VarChar);
          spMySqlTipoProc.Value = VProceso;
          espActualizaDetalleAccesos.AgregarParametro(spMySqlTipoProc);

          #endregion

          this._valorReturn = null;
          this._mensajeAlerta = string.Empty;

          espActualizaDetalleAccesos.EjecutaSinResultado();

        }

        espActualizaDetalleAccesos.Dispose();

      }

      catch (Exception ex)
      {
        string sError;
        sError = ex.Message.Trim();
        this._mensajeAlerta = sError.Trim();
        this._valorReturn = -1;
      }
      finally
      {
        espActualizaDetalleAccesos.Dispose();
      }
    }

    public List<EntidadMenuWeb> ObtenerMenuWeb(EntidadMenuWeb tEntidadNegocio)
    {
      accesoDatos.parametros.listaParametros.Clear();
      accesoDatos.comandoSP = "usp_MenuWebObtener";
      accesoDatos.parametros.Agrega("@MenuId", tEntidadNegocio.MenuId, true);
      accesoDatos.parametros.Agrega("@Menu", tEntidadNegocio.Menu, true);
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
      accesoDatos.parametros.Agrega("@MenuId", tMenuArticulo.MenuId, true);
      return accesoDatos.ConsultaDataList<MenuArticulos>();
    }

    public EntUser GetUser(EntUser user)
    {
      accesoDatos.parametros.listaParametros.Clear();
      accesoDatos.comandoSP = "uspGetUser";
      accesoDatos.parametros.Agrega("@userid", user.UserId, true);
      if (user.User_Name!=null && user.User_Name.Length>0)
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
      var t = accesoDatos.ConsultaDataSet("select dbo.ufn_GetTotalMenuArt("+menu.MenuId+")");
      return (int)t.Tables[0].Rows[0].ItemArray[0];
    }
  }
}
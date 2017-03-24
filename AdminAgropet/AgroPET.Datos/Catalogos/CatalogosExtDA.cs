using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;
using Agropet.Entidades.Base;
using Agropet.Entidades.Especial;
using Agropet.Entidades.Seguridad;
using AgroPET.Datos.Comun;
using AgroPET.Datos.Excepciones;
using Utilidades;

namespace AgroPET.Datos.Catalogos
{
    public class CatalogosExtDA
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
      List<EntidadMenuWeb> listResultado = new List<EntidadMenuWeb>();
      EjecutaSP espSP = new EjecutaSP("usp_MenuWebObtener");

      #region Asignacion de parametros

      foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
      {
        CampoAttribute caAtributoEntidadNegocio = null;
        object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
        if (oAtributos.Length > 0)
        {
          caAtributoEntidadNegocio = (CampoAttribute)oAtributos.ToList().Find(x => x.GetType() == typeof(CampoAttribute));
        }

        if (caAtributoEntidadNegocio == null || caAtributoEntidadNegocio.EsParametroSP)
        {
          object oValor = piPropiedadEntidad.GetValue(tEntidadNegocio, null);
          if (oValor != null)
          {
            string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
            espSP.AgregarParametro(new MySqlParameter(sNombreParametro, oValor));
          }
        }
      }

      #endregion

      MySqlDataReader drEntidad = espSP.ObtenerReader();
      listResultado = ConversionesEntidadesNegocio<EntidadMenuWeb>.ConvertirAListadoEntidadNegocio(drEntidad);

      espSP.Dispose();    //Cierra los accesos a la base de datos.

      return listResultado;
    }

    public List<EntidadBannersWeb> ObtenerBannersWeb(EntidadBannersWeb tEntidadNegocio)
    {
      List<EntidadBannersWeb> listResultado = new List<EntidadBannersWeb>();
      EjecutaSP espSP = new EjecutaSP("usp_GetBannersWeb");

      #region Asignacion de parametros

      foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
      {
        CampoAttribute caAtributoEntidadNegocio = null;
        object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
        if (oAtributos.Length > 0)
        {
          caAtributoEntidadNegocio = (CampoAttribute)oAtributos.ToList().Find(x => x.GetType() == typeof(CampoAttribute));
        }

        if (caAtributoEntidadNegocio == null || caAtributoEntidadNegocio.EsParametroSP)
        {
          object oValor = piPropiedadEntidad.GetValue(tEntidadNegocio, null);
          if (oValor != null)
          {
            string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
            espSP.AgregarParametro(new MySqlParameter(sNombreParametro, oValor));
          }
        }
      }

      #endregion

      MySqlDataReader drEntidad = espSP.ObtenerReader();
      listResultado = ConversionesEntidadesNegocio<EntidadBannersWeb>.ConvertirAListadoEntidadNegocio(drEntidad);

      espSP.Dispose();    //Cierra los accesos a la base de datos.

      return listResultado;
    }
  }
}

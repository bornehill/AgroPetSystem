using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;

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
    }
}

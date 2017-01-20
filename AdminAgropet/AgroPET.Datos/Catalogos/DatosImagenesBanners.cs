using Agropet.Entidades;
using Agropet.Entidades.CatABCs;
using AgroPET.Datos.Comun;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Datos.Catalogos
{
    public class DatosImagenesBanners
    {
        public List<EntImgenesBanners> ObtenerBanners(string Descripcion)
        {
            MySqlDataReader drDatos;
            EjecutaSP spEjecutaSP = new EjecutaSP("uspImagenesBannersObtener");

            MySqlParameter pUnParametro = new MySqlParameter("p_Descripcion", MySqlDbType.VarChar);
            pUnParametro.Value = Descripcion;
            spEjecutaSP.AgregarParametro(pUnParametro);

            drDatos = spEjecutaSP.ObtenerReader();

            List<EntImgenesBanners> lstInfo = ConversionesEntidadesNegocio<EntImgenesBanners>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lstInfo;
        }

        public bool InsertaImagenesBanners(int idusuariocreo, DateTime FechaInicioApp, DateTime FechaFinApp, string descripcion)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspImagenesBannersInsertar");

                MySqlParameter pidusuariocreo = new MySqlParameter("p_idusuariocreo", MySqlDbType.Int32);
                pidusuariocreo.Value = idusuariocreo;
                ejecuta.AgregarParametro(pidusuariocreo);

                MySqlParameter pFechaInicioApp = new MySqlParameter("p_fehcaInicioApp", MySqlDbType.DateTime);
                pFechaInicioApp.Value = FechaInicioApp;
                ejecuta.AgregarParametro(pFechaInicioApp);

                MySqlParameter pFechaFinApp = new MySqlParameter("p_fechaFinApp", MySqlDbType.DateTime);
                pFechaFinApp.Value = FechaFinApp;
                ejecuta.AgregarParametro(pFechaFinApp);

                MySqlParameter p_descripcion = new MySqlParameter("p_descripcion", MySqlDbType.String);
                p_descripcion.Value = descripcion;
                ejecuta.AgregarParametro(p_descripcion);

                ejecuta.EjecutaSinResultado();
            }
            catch (MySqlException ex)
            {
                //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
            return true;
        }

        public bool ActualizarImagenesBanner(int idBanner, int idUsuarioModifico, string descripcion, DateTime fechainiaplica, DateTime fechafinaplica)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspImagenesBannersActualizar");

                MySqlParameter p_idBanner = new MySqlParameter("p_idbanner", MySqlDbType.Int32);
                p_idBanner.Value = idBanner;
                ejecuta.AgregarParametro(p_idBanner);

                MySqlParameter p_idUsuarioModifico = new MySqlParameter("p_idusuariomodif", MySqlDbType.Int32);
                p_idUsuarioModifico.Value = idUsuarioModifico;
                ejecuta.AgregarParametro(p_idUsuarioModifico);

                MySqlParameter p_descripcion = new MySqlParameter("p_descripcion", MySqlDbType.VarChar);
                p_descripcion.Value = descripcion;
                ejecuta.AgregarParametro(p_descripcion);

                MySqlParameter p_fechainiaplica = new MySqlParameter("p_fechainiaplica", MySqlDbType.DateTime);
                p_fechainiaplica.Value = fechainiaplica;
                ejecuta.AgregarParametro(p_fechainiaplica);

                MySqlParameter p_fechafinaplica = new MySqlParameter("p_fechafinaplica", MySqlDbType.DateTime);
                p_fechafinaplica.Value = fechafinaplica;
                ejecuta.AgregarParametro(p_fechafinaplica);

                ejecuta.EjecutaSinResultado();
            }
            catch (MySqlException ex)
            {
                //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
            return true;
        }

        public List<EntImagenesBannersDetalle> ObtenerImagenesBannersDetalle(int idBanner)
        {
            MySqlDataReader drDatos;
            EjecutaSP spEjecutaSP = new EjecutaSP("uspImagenesBannersObtenerDetalle");

            MySqlParameter p_idBanner = new MySqlParameter("p_idBanner", MySqlDbType.Int32);
            p_idBanner.Value = idBanner;
            spEjecutaSP.AgregarParametro(p_idBanner);

            drDatos = spEjecutaSP.ObtenerReader();

            List<EntImagenesBannersDetalle> lstInfo = ConversionesEntidadesNegocio<EntImagenesBannersDetalle>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lstInfo;
        }

        public bool InsertaImagenesBannerDetalle(int idBanner, int idUsuarioModifico, string RutaBannerDetalle, string OrdenDetalle, string titulo, string subtitulo)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspImagenesBannerInsertarDetalle");

                MySqlParameter p_idBanner = new MySqlParameter("p_idBanner", MySqlDbType.Int32);
                p_idBanner.Value = idBanner;
                ejecuta.AgregarParametro(p_idBanner);

                MySqlParameter p_idUsuarioModifico = new MySqlParameter("p_idUsuarioModifico", MySqlDbType.Int32);
                p_idUsuarioModifico.Value = idUsuarioModifico;
                ejecuta.AgregarParametro(p_idUsuarioModifico);

                MySqlParameter p_RutaBannerDetalle = new MySqlParameter("p_RutaBannerDetalle", MySqlDbType.VarChar);
                p_RutaBannerDetalle.Value = RutaBannerDetalle;
                ejecuta.AgregarParametro(p_RutaBannerDetalle);

                MySqlParameter p_OrdenDetalle = new MySqlParameter("p_OrdenDetalle", MySqlDbType.VarChar);
                p_OrdenDetalle.Value = OrdenDetalle;
                ejecuta.AgregarParametro(p_OrdenDetalle);

                MySqlParameter p_titulo = new MySqlParameter("p_titulo", MySqlDbType.VarChar);
                p_titulo.Value = titulo;
                ejecuta.AgregarParametro(p_titulo);

                MySqlParameter p_subtitulo = new MySqlParameter("p_subtitulo", MySqlDbType.VarChar);
                p_subtitulo.Value = subtitulo;
                ejecuta.AgregarParametro(p_subtitulo);

                ejecuta.EjecutaSinResultado();
            }
            catch (MySqlException ex)
            {
                //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
            return true;
        }

        public bool ActualizarImagenesBannerDetalle(int idBannerDetalle, int idBanner, int idUsuarioModifico, string RutaBannerDetalle, string OrdenDetalle, string titulo, string subtitulo)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspImagenesBannersActualizarDetalle");

                MySqlParameter p_idBannerDetalle = new MySqlParameter("p_iddetbanners", MySqlDbType.Int32);
                p_idBannerDetalle.Value = idBannerDetalle;
                ejecuta.AgregarParametro(p_idBannerDetalle);

                MySqlParameter p_idBanner = new MySqlParameter("p_idbanner", MySqlDbType.Int32);
                p_idBanner.Value = idBanner;
                ejecuta.AgregarParametro(p_idBanner);

                MySqlParameter p_idUsuarioModifico = new MySqlParameter("p_idusuarioModifico", MySqlDbType.Int32);
                p_idUsuarioModifico.Value = idUsuarioModifico;
                ejecuta.AgregarParametro(p_idUsuarioModifico);

                MySqlParameter p_RutaBannerDetalle = new MySqlParameter("p_pathimagen", MySqlDbType.VarChar);
                p_RutaBannerDetalle.Value = RutaBannerDetalle;
                ejecuta.AgregarParametro(p_RutaBannerDetalle);

                MySqlParameter p_OrdenDetalle = new MySqlParameter("p_orden", MySqlDbType.VarChar);
                p_OrdenDetalle.Value = OrdenDetalle;
                ejecuta.AgregarParametro(p_OrdenDetalle);

                MySqlParameter p_titulo = new MySqlParameter("p_titulo", MySqlDbType.VarChar);
                p_titulo.Value = titulo;
                ejecuta.AgregarParametro(p_titulo);

                MySqlParameter p_subtitulo = new MySqlParameter("p_subtitulo", MySqlDbType.VarChar);
                p_subtitulo.Value = subtitulo;
                ejecuta.AgregarParametro(p_subtitulo);

                ejecuta.EjecutaSinResultado();
            }
            catch (MySqlException ex)
            {
                //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
            return true;
        }

        public bool EliminaImagenesBannerDetalle(int idBanner, int idBannerDetalle, int idUsuarioModifico)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspImagenesBannerEliminaDetalle");

                MySqlParameter p_idBanner = new MySqlParameter("p_idBanner", MySqlDbType.Int32);
                p_idBanner.Value = idBanner;
                ejecuta.AgregarParametro(p_idBanner);

                MySqlParameter p_idBannerDetalle = new MySqlParameter("p_idBannerDetalle", MySqlDbType.Int32);
                p_idBannerDetalle.Value = idBannerDetalle;
                ejecuta.AgregarParametro(p_idBannerDetalle);

                MySqlParameter p_idUsuarioModifico = new MySqlParameter("p_idUsuarioModifico", MySqlDbType.Int32);
                p_idUsuarioModifico.Value = idUsuarioModifico;
                ejecuta.AgregarParametro(p_idUsuarioModifico);

                ejecuta.EjecutaSinResultado();
            }
            catch (MySqlException ex)
            {
                //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
            return true;
        }
    }
}

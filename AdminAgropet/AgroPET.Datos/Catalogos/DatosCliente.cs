using Agropet.Entidades.CatABCs;
using Agropet.Entidades.Consultas;
using AgroPET.Datos.Comun;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Datos.Catalogos
{
    public class DatosCliente
    {
        public List<EntClientes> ObtenerClientes(string Nombre, string estatus)
        {
            MySqlDataReader drDatos;
            EjecutaSP spEjecutaSP = new EjecutaSP("uspClienteObtener");

            MySqlParameter pNombre = new MySqlParameter("p_nombre", MySqlDbType.VarChar);
            pNombre.Value = Nombre;
            spEjecutaSP.AgregarParametro(pNombre);

            MySqlParameter pestatus = new MySqlParameter("p_estatus", MySqlDbType.VarChar);
            pestatus.Value = estatus;
            spEjecutaSP.AgregarParametro(pestatus);

            drDatos = spEjecutaSP.ObtenerReader();

            List<EntClientes> lstInfo = ConversionesEntidadesNegocio<EntClientes>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lstInfo;
        }

        public bool InsertaCliente(EntClientes cliente)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspClienteInsertar");

                MySqlParameter pnombre = new MySqlParameter("p_nombre", MySqlDbType.VarChar);
                pnombre.Value = cliente.nombre;
                ejecuta.AgregarParametro(pnombre);

                MySqlParameter pcontacto = new MySqlParameter("p_contacto", MySqlDbType.VarChar);
                pcontacto.Value = cliente.contacto1;
                ejecuta.AgregarParametro(pcontacto);

                MySqlParameter pestatus = new MySqlParameter("p_estatus", MySqlDbType.VarChar);
                pestatus.Value = cliente.estatus;
                ejecuta.AgregarParametro(pestatus);

                MySqlParameter pcausasusp = new MySqlParameter("p_causasusp", MySqlDbType.VarChar);
                pcausasusp.Value = cliente.causa_susp;
                ejecuta.AgregarParametro(pcausasusp);

                MySqlParameter plimitecrediro = new MySqlParameter("p_limitecrediro", MySqlDbType.Decimal);
                plimitecrediro.Value = cliente.limite_credito;
                ejecuta.AgregarParametro(plimitecrediro);

                MySqlParameter pidmoneda = new MySqlParameter("p_idmoneda", MySqlDbType.Int32);
                pidmoneda.Value = cliente.moneda_id;
                ejecuta.AgregarParametro(pidmoneda);

                MySqlParameter pidcondicionpago = new MySqlParameter("p_idcondicionpago", MySqlDbType.Int32);
                pidcondicionpago.Value = cliente.cond_pago_id;
                ejecuta.AgregarParametro(pidcondicionpago);

                MySqlParameter pidtipocliente = new MySqlParameter("p_idtipocliente", MySqlDbType.Int32);
                pidtipocliente.Value = cliente.tipo_cliente_id;
                ejecuta.AgregarParametro(pidtipocliente);

                MySqlParameter pusuariocreador = new MySqlParameter("p_usuariocreador", MySqlDbType.VarChar);
                pusuariocreador.Value = cliente.usuario_creador;
                ejecuta.AgregarParametro(pusuariocreador);

                ejecuta.EjecutaSinResultado();

                return true;
            }
            catch (MySqlException ex)
            {
                //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
        }

        //public bool ActualizarImagenesBanner(int idBanner, int idUsuarioModifico, string descripcion, DateTime fechainiaplica, DateTime fechafinaplica)
        //{
        //    try
        //    {
        //        EjecutaSP ejecuta = new EjecutaSP("uspImagenesBannersActualizar");

        //        MySqlParameter p_idBanner = new MySqlParameter("p_idbanner", MySqlDbType.Int32);
        //        p_idBanner.Value = idBanner;
        //        ejecuta.AgregarParametro(p_idBanner);

        //        MySqlParameter p_idUsuarioModifico = new MySqlParameter("p_idusuariomodif", MySqlDbType.Int32);
        //        p_idUsuarioModifico.Value = idUsuarioModifico;
        //        ejecuta.AgregarParametro(p_idUsuarioModifico);

        //        MySqlParameter p_descripcion = new MySqlParameter("p_descripcion", MySqlDbType.VarChar);
        //        p_descripcion.Value = descripcion;
        //        ejecuta.AgregarParametro(p_descripcion);

        //        MySqlParameter p_fechainiaplica = new MySqlParameter("p_fechainiaplica", MySqlDbType.DateTime);
        //        p_fechainiaplica.Value = fechainiaplica;
        //        ejecuta.AgregarParametro(p_fechainiaplica);

        //        MySqlParameter p_fechafinaplica = new MySqlParameter("p_fechafinaplica", MySqlDbType.DateTime);
        //        p_fechafinaplica.Value = fechafinaplica;
        //        ejecuta.AgregarParametro(p_fechafinaplica);

        //        ejecuta.EjecutaSinResultado();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
        //        return false;
        //    }
        //    return true;
        //}

        //public List<EntImagenesBannersDetalle> ObtenerImagenesBannersDetalle(int idBanner)
        //{
        //    MySqlDataReader drDatos;
        //    EjecutaSP spEjecutaSP = new EjecutaSP("uspImagenesBannersObtenerDetalle");

        //    MySqlParameter p_idBanner = new MySqlParameter("p_idBanner", MySqlDbType.Int32);
        //    p_idBanner.Value = idBanner;
        //    spEjecutaSP.AgregarParametro(p_idBanner);

        //    drDatos = spEjecutaSP.ObtenerReader();

        //    List<EntImagenesBannersDetalle> lstInfo = ConversionesEntidadesNegocio<EntImagenesBannersDetalle>.ConvertirAListadoEntidadNegocio(drDatos);

        //    spEjecutaSP.Dispose();

        //    return lstInfo;
        //}

        //public bool InsertaImagenesBannerDetalle(int idBanner, int idUsuarioModifico, string RutaBannerDetalle, string OrdenDetalle, string titulo, string subtitulo)
        //{
        //    try
        //    {
        //        EjecutaSP ejecuta = new EjecutaSP("uspImagenesBannerInsertarDetalle");

        //        MySqlParameter p_idBanner = new MySqlParameter("p_idBanner", MySqlDbType.Int32);
        //        p_idBanner.Value = idBanner;
        //        ejecuta.AgregarParametro(p_idBanner);

        //        MySqlParameter p_idUsuarioModifico = new MySqlParameter("p_idUsuarioModifico", MySqlDbType.Int32);
        //        p_idUsuarioModifico.Value = idUsuarioModifico;
        //        ejecuta.AgregarParametro(p_idUsuarioModifico);

        //        MySqlParameter p_RutaBannerDetalle = new MySqlParameter("p_RutaBannerDetalle", MySqlDbType.VarChar);
        //        p_RutaBannerDetalle.Value = RutaBannerDetalle;
        //        ejecuta.AgregarParametro(p_RutaBannerDetalle);

        //        MySqlParameter p_OrdenDetalle = new MySqlParameter("p_OrdenDetalle", MySqlDbType.VarChar);
        //        p_OrdenDetalle.Value = OrdenDetalle;
        //        ejecuta.AgregarParametro(p_OrdenDetalle);

        //        MySqlParameter p_titulo = new MySqlParameter("p_titulo", MySqlDbType.VarChar);
        //        p_titulo.Value = titulo;
        //        ejecuta.AgregarParametro(p_titulo);

        //        MySqlParameter p_subtitulo = new MySqlParameter("p_subtitulo", MySqlDbType.VarChar);
        //        p_subtitulo.Value = subtitulo;
        //        ejecuta.AgregarParametro(p_subtitulo);

        //        ejecuta.EjecutaSinResultado();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
        //        return false;
        //    }
        //    return true;
        //}

        //public bool ActualizarImagenesBannerDetalle(int idBannerDetalle, int idBanner, int idUsuarioModifico, string RutaBannerDetalle, string OrdenDetalle, string titulo, string subtitulo)
        //{
        //    try
        //    {
        //        EjecutaSP ejecuta = new EjecutaSP("uspImagenesBannersActualizarDetalle");

        //        MySqlParameter p_idBannerDetalle = new MySqlParameter("p_iddetbanners", MySqlDbType.Int32);
        //        p_idBannerDetalle.Value = idBannerDetalle;
        //        ejecuta.AgregarParametro(p_idBannerDetalle);

        //        MySqlParameter p_idBanner = new MySqlParameter("p_idbanner", MySqlDbType.Int32);
        //        p_idBanner.Value = idBanner;
        //        ejecuta.AgregarParametro(p_idBanner);

        //        MySqlParameter p_idUsuarioModifico = new MySqlParameter("p_idusuarioModifico", MySqlDbType.Int32);
        //        p_idUsuarioModifico.Value = idUsuarioModifico;
        //        ejecuta.AgregarParametro(p_idUsuarioModifico);

        //        MySqlParameter p_RutaBannerDetalle = new MySqlParameter("p_pathimagen", MySqlDbType.VarChar);
        //        p_RutaBannerDetalle.Value = RutaBannerDetalle;
        //        ejecuta.AgregarParametro(p_RutaBannerDetalle);

        //        MySqlParameter p_OrdenDetalle = new MySqlParameter("p_orden", MySqlDbType.VarChar);
        //        p_OrdenDetalle.Value = OrdenDetalle;
        //        ejecuta.AgregarParametro(p_OrdenDetalle);

        //        MySqlParameter p_titulo = new MySqlParameter("p_titulo", MySqlDbType.VarChar);
        //        p_titulo.Value = titulo;
        //        ejecuta.AgregarParametro(p_titulo);

        //        MySqlParameter p_subtitulo = new MySqlParameter("p_subtitulo", MySqlDbType.VarChar);
        //        p_subtitulo.Value = subtitulo;
        //        ejecuta.AgregarParametro(p_subtitulo);

        //        ejecuta.EjecutaSinResultado();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
        //        return false;
        //    }
        //    return true;
        //}

        //public bool EliminaImagenesBannerDetalle(int idBanner, int idBannerDetalle, int idUsuarioModifico)
        //{
        //    try
        //    {
        //        EjecutaSP ejecuta = new EjecutaSP("uspImagenesBannerEliminaDetalle");

        //        MySqlParameter p_idBanner = new MySqlParameter("p_idBanner", MySqlDbType.Int32);
        //        p_idBanner.Value = idBanner;
        //        ejecuta.AgregarParametro(p_idBanner);

        //        MySqlParameter p_idBannerDetalle = new MySqlParameter("p_idBannerDetalle", MySqlDbType.Int32);
        //        p_idBannerDetalle.Value = idBannerDetalle;
        //        ejecuta.AgregarParametro(p_idBannerDetalle);

        //        MySqlParameter p_idUsuarioModifico = new MySqlParameter("p_idUsuarioModifico", MySqlDbType.Int32);
        //        p_idUsuarioModifico.Value = idUsuarioModifico;
        //        ejecuta.AgregarParametro(p_idUsuarioModifico);

        //        ejecuta.EjecutaSinResultado();
        //    }
        //    catch (MySqlException ex)
        //    {
        //        //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
        //        return false;
        //    }
        //    return true;
        //}
    }
}

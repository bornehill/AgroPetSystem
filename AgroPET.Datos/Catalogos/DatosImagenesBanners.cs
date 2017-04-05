using AgroPET.Entidades;
using AgroPET.Entidades.CatABCs;
using AgroPET.Datos.Comun;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Datos.Catalogos
{
    public class DatosImagenesBanners : DatosBase
    {
        public List<EntImgenesBanners> ObtenerBanners(string Descripcion)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspImagenesBannersObtener";
            accesoDatos.parametros.Agrega("@p_Descripcion", Descripcion, true);
            return accesoDatos.ConsultaDataList<EntImgenesBanners>();
        }

        public bool InsertaImagenesBanners(int idusuariocreo, DateTime FechaInicioApp, DateTime FechaFinApp, string descripcion)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspImagenesBannersInsertar";
                accesoDatos.parametros.Agrega("@p_idusuariocreo", idusuariocreo, true);
                accesoDatos.parametros.Agrega("@p_fehcaInicioApp", FechaInicioApp, true);
                accesoDatos.parametros.Agrega("@p_fechaFinApp", FechaFinApp, true);
                accesoDatos.parametros.Agrega("@p_descripcion", descripcion, true);
                int afectados = accesoDatos.EjecutaNQuery();

                if (afectados >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ActualizarImagenesBanner(int idBanner, int idUsuarioModifico, string descripcion, DateTime fechainiaplica, DateTime fechafinaplica)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspImagenesBannersActualizar";
                accesoDatos.parametros.Agrega("@p_idbanner", idBanner, true);
                accesoDatos.parametros.Agrega("@p_idusuariomodif", idUsuarioModifico, true);
                accesoDatos.parametros.Agrega("@p_descripcion", descripcion, true);
                accesoDatos.parametros.Agrega("@p_fechainiaplica", fechainiaplica, true);
                accesoDatos.parametros.Agrega("@p_fechafinaplica", fechafinaplica, true);
                int afectados = accesoDatos.EjecutaNQuery();

                if (afectados >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<EntImagenesBannersDetalle> ObtenerImagenesBannersDetalle(int idBanner)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspImagenesBannersObtenerDetalle";
            accesoDatos.parametros.Agrega("@p_idBanner", idBanner, true);
            return accesoDatos.ConsultaDataList<EntImagenesBannersDetalle>();
        }

        public bool InsertaImagenesBannerDetalle(int idBanner, int idUsuarioModifico, string RutaBannerDetalle, string OrdenDetalle, string titulo, string subtitulo)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspImagenesBannerInsertarDetalle";
                accesoDatos.parametros.Agrega("@p_idBanner", idBanner, true);
                accesoDatos.parametros.Agrega("@p_idUsuarioModifico", idUsuarioModifico, true);
                accesoDatos.parametros.Agrega("@p_RutaBannerDetalle", RutaBannerDetalle, true);
                accesoDatos.parametros.Agrega("@p_OrdenDetalle", OrdenDetalle, true);
                accesoDatos.parametros.Agrega("@p_titulo", titulo, true);
                accesoDatos.parametros.Agrega("@p_subtitulo", subtitulo, true);
                int afectados = accesoDatos.EjecutaNQuery();

                if (afectados >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ActualizarImagenesBannerDetalle(int idBannerDetalle, int idBanner, int idUsuarioModifico, string RutaBannerDetalle, string OrdenDetalle, string titulo, string subtitulo)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspImagenesBannersActualizarDetalle";
                accesoDatos.parametros.Agrega("@p_iddetbanners", idBannerDetalle, true);
                accesoDatos.parametros.Agrega("@p_idBanner", idBanner, true);
                accesoDatos.parametros.Agrega("@p_idUsuarioModifico", idUsuarioModifico, true);
                accesoDatos.parametros.Agrega("@p_RutaBannerDetalle", RutaBannerDetalle, true);
                accesoDatos.parametros.Agrega("@p_OrdenDetalle", OrdenDetalle, true);
                accesoDatos.parametros.Agrega("@p_titulo", titulo, true);
                accesoDatos.parametros.Agrega("@p_subtitulo", subtitulo, true);
                int afectados = accesoDatos.EjecutaNQuery();

                if (afectados >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminaImagenesBannerDetalle(int idBanner, int idBannerDetalle, int idUsuarioModifico)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspImagenesBannerEliminaDetalle";
                accesoDatos.parametros.Agrega("@p_iddetbanners", idBannerDetalle, true);
                accesoDatos.parametros.Agrega("@p_idBanner", idBanner, true);
                accesoDatos.parametros.Agrega("@p_idUsuarioModifico", idUsuarioModifico, true);
                int afectados = accesoDatos.EjecutaNQuery();

                if (afectados >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

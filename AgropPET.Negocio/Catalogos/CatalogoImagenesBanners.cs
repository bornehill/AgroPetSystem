using AgroPET.Entidades;
using AgroPET.Entidades.CatABCs;
using AgroPET.Datos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgropPET.Negocio.Catalogos
{
    public class CatalogoImagenesBanners
    {
        public List<EntImgenesBanners> ObtenerBanners(string Descripcion)
        {
            List<EntImgenesBanners> lstImagArt = new DatosImagenesBanners().ObtenerBanners(Descripcion);

            return lstImagArt;
        }

        public bool InsertaImagenesBanners(int idusuariocreo, DateTime FechaInicioApp, DateTime FechaFinApp, string descripcion)
        {
            return new DatosImagenesBanners().InsertaImagenesBanners(idusuariocreo, FechaInicioApp, FechaFinApp, descripcion);
        }

        public bool ActualizarImagenesBanner(int idBanner, int idUsuarioModifico, string descripcion, DateTime fechainiaplica, DateTime fechafinaplica)
        {
            return new DatosImagenesBanners().ActualizarImagenesBanner(idBanner, idUsuarioModifico, descripcion, fechainiaplica, fechafinaplica);
        }

        public List<EntImagenesBannersDetalle> ObtenerImagenesBannersDetalle(int idBanner)
        {
            List<EntImagenesBannersDetalle> lstImagBanners = new DatosImagenesBanners().ObtenerImagenesBannersDetalle(idBanner);

            return lstImagBanners;
        }

        public bool InsertaImagenesBannerDetalle(int idBanner, int idUsuarioModifico, string RutaBannerDetalle, string OrdenDetalle, string titulo, string subtitulo)
        {
            return new DatosImagenesBanners().InsertaImagenesBannerDetalle(idBanner, idUsuarioModifico, RutaBannerDetalle, OrdenDetalle, titulo, subtitulo);
        }

        public bool ActualizarImagenesBannerDetalle(int idBannerDetalle, int idBanner, int idUsuarioModifico, string RutaBannerDetalle, string OrdenDetalle, string titulo, string subtitulo)
        {
            return new DatosImagenesBanners().ActualizarImagenesBannerDetalle(idBannerDetalle, idBanner, idUsuarioModifico, RutaBannerDetalle, OrdenDetalle, titulo, subtitulo);
        }

        public bool EliminaImagenesBannerDetalle(int idBanner, int idBannerDetalle, int idUsuarioModifico)
        {
            return new DatosImagenesBanners().EliminaImagenesBannerDetalle(idBanner, idBannerDetalle, idUsuarioModifico);
        }
    }
}

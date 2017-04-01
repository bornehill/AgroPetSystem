using AgroPET.Entidades.CatABCs;
using AgroPET.Datos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgropPET.Negocio.Catalogos
{
    public class CatalogoCliente
    {
        public List<EntClientes> ObtenerClientes(string Nombre, string estatus)
        {
            List<EntClientes> lst = new DatosCliente().ObtenerClientes(Nombre, estatus);

            return lst;
        }

        public bool InsertaCliente(EntClientes cliente)
        {
            return new DatosCliente().InsertaCliente(cliente);
        }

        //public bool ActualizarImagenesBanner(int idBanner, int idUsuarioModifico, string descripcion, DateTime fechainiaplica, DateTime fechafinaplica)
        //{
        //    return new DatosImagenesBanners().ActualizarImagenesBanner(idBanner, idUsuarioModifico, descripcion, fechainiaplica, fechafinaplica);
        //}

        //public List<EntImagenesBannersDetalle> ObtenerImagenesBannersDetalle(int idBanner)
        //{
        //    List<EntImagenesBannersDetalle> lstImagBanners = new DatosImagenesBanners().ObtenerImagenesBannersDetalle(idBanner);

        //    return lstImagBanners;
        //}

        //public bool InsertaImagenesBannerDetalle(int idBanner, int idUsuarioModifico, string RutaBannerDetalle, string OrdenDetalle, string titulo, string subtitulo)
        //{
        //    return new DatosImagenesBanners().InsertaImagenesBannerDetalle(idBanner, idUsuarioModifico, RutaBannerDetalle, OrdenDetalle, titulo, subtitulo);
        //}

        //public bool ActualizarImagenesBannerDetalle(int idBannerDetalle, int idBanner, int idUsuarioModifico, string RutaBannerDetalle, string OrdenDetalle, string titulo, string subtitulo)
        //{
        //    return new DatosImagenesBanners().ActualizarImagenesBannerDetalle(idBannerDetalle, idBanner, idUsuarioModifico, RutaBannerDetalle, OrdenDetalle, titulo, subtitulo);
        //}

        //public bool EliminaImagenesBannerDetalle(int idBanner, int idBannerDetalle, int idUsuarioModifico)
        //{
        //    return new DatosImagenesBanners().EliminaImagenesBannerDetalle(idBanner, idBannerDetalle, idUsuarioModifico);
        //}

    }
}

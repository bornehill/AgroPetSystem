﻿using AgroPET.Entidades.CatABCs;
using AgroPET.Datos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgropPET.Negocio.Catalogos
{
    public class CatalogoImagenesArticulos
    {
        public List<EntImgenesArticulos> ObtenerImagenesArticulos(string Articulo)
        {
            List<EntImgenesArticulos> lstImagArt = new DatosImagenesArticulos().ObtenerImagenesArticulos(Articulo);

            return lstImagArt;
        }

        public bool InsertaImagenesArticulos(int IdArticulo, int idusuariocreo, string pathimagen)
        {
            return new DatosImagenesArticulos().InsertaImagenesArticulos(IdArticulo, idusuariocreo, pathimagen);
        }

        public bool ActualizarImagenesArticulos(int idImagenArticulo, int idusuarioModifico, string pathimagen)
        {
            return new DatosImagenesArticulos().ActualizarImagenesArticulos(idImagenArticulo, idusuarioModifico, pathimagen);
        }

        public List<EntImagenesArticulosDetalle> ObtenerImagenesArticulosDetalle(int idImagenArticulo)
        {
            List<EntImagenesArticulosDetalle> lstImagArt = new DatosImagenesArticulos().ObtenerImagenesArticulosDetalle(idImagenArticulo);

            return lstImagArt;
        }

        public bool EliminaImagenesArticulos(int IdImagenArticulo)
        {
            return new DatosImagenesArticulos().EliminaImagenesArticulos(IdImagenArticulo);
        }

        public bool InsertaImagenesArticulosDetalle(int idImagenArticulo, int idusuarioModifico, string pathimagen)
        {
            return new DatosImagenesArticulos().InsertaImagenesArticulosDetalle(idImagenArticulo, idusuarioModifico, pathimagen);
        }

        public bool EliminaImagenesArticulosDetalle(int IdDetImagenArticulo, int IdImagenArticulo, int idusuarioModifico)
        {
            return new DatosImagenesArticulos().EliminaImagenesArticulosDetalle(IdDetImagenArticulo, IdImagenArticulo, idusuarioModifico);
        }

        public bool ActualizarImagenesArticulosDetalle(int idImagenArticulo, int idusuarioModifico, string pathimagen, int idDetImagenarticulo)
        {
            return new DatosImagenesArticulos().ActualizarImagenesArticulosDetalle(idImagenArticulo, idusuarioModifico, pathimagen, idDetImagenarticulo);
        }
    }
}

using AgroPET.Entidades.CatABCs;
using AgroPET.Datos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Datos.Catalogos
{
    public class DatosImagenesArticulos : DatosBase
    {
        public List<EntImgenesArticulos> ObtenerImagenesArticulos(string NombreArticulo)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspImagenesArticulosObtener";
            accesoDatos.parametros.Agrega("@p_NombreArticulo", NombreArticulo, true);
            return accesoDatos.ConsultaDataList<EntImgenesArticulos>();
        }

        public bool InsertaImagenesArticulos(int IdArticulo, int idusuariocreo)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspImagenesArticulosInsertar";
                accesoDatos.parametros.Agrega("@p_idArticulo", IdArticulo, true);
                accesoDatos.parametros.Agrega("@p_idusuariocreo", idusuariocreo, true);
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

        public List<EntImagenesArticulosDetalle> ObtenerImagenesArticulosDetalle(int idImagenArticulo)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspImagenesArticulosDetalleObtener";
            accesoDatos.parametros.Agrega("@p_IdImagenArticulo", idImagenArticulo, true);
            return accesoDatos.ConsultaDataList<EntImagenesArticulosDetalle>();
        }

        public bool InsertaImagenesArticulosDetalle(int idImagenArticulo, int idusuarioModifico, string pathimagen)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspImagenesArticulosInsertarDetalle";
                accesoDatos.parametros.Agrega("@p_idImagenArticulo", idImagenArticulo, true);
                accesoDatos.parametros.Agrega("@p_idusuarioModifico", idusuarioModifico, true);
                accesoDatos.parametros.Agrega("@p_pathimagen", pathimagen, true);
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

        public bool ActualizarImagenesArticulosDetalle(int idImagenArticulo, int idusuarioModifico, string pathimagen, int idDetImagenarticulo)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspImagenesArticulosActualizarDetalle";
                accesoDatos.parametros.Agrega("@p_idImagenArticulo", idImagenArticulo, true);
                accesoDatos.parametros.Agrega("@p_idusuarioModifico", idusuarioModifico, true);
                accesoDatos.parametros.Agrega("@p_pathimagen", pathimagen, true);
                accesoDatos.parametros.Agrega("@p_iddetimagenarticulo", idDetImagenarticulo, true);
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

        public bool EliminaImagenesArticulosDetalle(int IdDetImagenArticulo, int idImagenArticulo, int idusuarioModifico)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspImagenesArticulosEliminaDetalle";
                accesoDatos.parametros.Agrega("@p_IdDetImagenArticulo", IdDetImagenArticulo, true);
                accesoDatos.parametros.Agrega("@p_idImagenArticulo", idImagenArticulo, true);
                accesoDatos.parametros.Agrega("@p_idusuarioModifico", idusuarioModifico, true);
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

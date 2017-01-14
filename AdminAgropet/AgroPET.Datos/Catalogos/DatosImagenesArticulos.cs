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
    public class DatosImagenesArticulos
    {
        public List<EntImgenesArticulos> ObtenerImagenesArticulos(string NombreArticulo)
        {
            MySqlDataReader drDatos;
            EjecutaSP spEjecutaSP = new EjecutaSP("uspImagenesArticulosObtener");

            MySqlParameter pUnParametro = new MySqlParameter("p_NombreArticulo", MySqlDbType.VarChar);
            pUnParametro.Value = NombreArticulo;
            spEjecutaSP.AgregarParametro(pUnParametro);

            drDatos = spEjecutaSP.ObtenerReader();

            List<EntImgenesArticulos> lstInfo = ConversionesEntidadesNegocio<EntImgenesArticulos>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lstInfo;
        }

        public bool InsertaImagenesArticulos(int IdArticulo, int idusuariocreo)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspImagenesArticulosInsertar");

                MySqlParameter pidArticulo = new MySqlParameter("p_idArticulo", MySqlDbType.Int32);
                pidArticulo.Value = IdArticulo;
                ejecuta.AgregarParametro(pidArticulo);

                MySqlParameter pidusuariocreo = new MySqlParameter("p_idusuariocreo", MySqlDbType.Int32);
                pidusuariocreo.Value = idusuariocreo;
                ejecuta.AgregarParametro(pidusuariocreo);

                ejecuta.EjecutaSinResultado();
            }
            catch (MySqlException ex)
            {
                //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
            return true;
        }

        public List<EntImagenesArticulosDetalle> ObtenerImagenesArticulosDetalle(int idImagenArticulo)
        {
            MySqlDataReader drDatos;
            EjecutaSP spEjecutaSP = new EjecutaSP("uspImagenesArticulosDetalleObtener");

            MySqlParameter pUnParametro = new MySqlParameter("p_IdImagenArticulo", MySqlDbType.Int32);
            pUnParametro.Value = idImagenArticulo;
            spEjecutaSP.AgregarParametro(pUnParametro);

            drDatos = spEjecutaSP.ObtenerReader();

            List<EntImagenesArticulosDetalle> lstInfo = ConversionesEntidadesNegocio<EntImagenesArticulosDetalle>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lstInfo;
        }

        public bool InsertaImagenesArticulosDetalle(int idImagenArticulo, int idusuarioModifico, string pathimagen)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspImagenesArticulosInsertarDetalle");

                MySqlParameter pidArticulo = new MySqlParameter("p_idImagenArticulo", MySqlDbType.Int32);
                pidArticulo.Value = idImagenArticulo;
                ejecuta.AgregarParametro(pidArticulo);

                MySqlParameter pidusuarioModifico = new MySqlParameter("p_idusuarioModifico", MySqlDbType.Int32);
                pidusuarioModifico.Value = idusuarioModifico;
                ejecuta.AgregarParametro(pidusuarioModifico);

                MySqlParameter ppathimagen = new MySqlParameter("p_pathimagen", MySqlDbType.VarChar);
                ppathimagen.Value = pathimagen;
                ejecuta.AgregarParametro(ppathimagen);

                ejecuta.EjecutaSinResultado();
            }
            catch (MySqlException ex)
            {
                //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
            return true;
        }

        public bool ActualizarImagenesArticulosDetalle(int idImagenArticulo, int idusuarioModifico, string pathimagen, int idDetImagenarticulo)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspImagenesArticulosActualizarDetalle");

                MySqlParameter pidArticulo = new MySqlParameter("p_idImagenArticulo", MySqlDbType.Int32);
                pidArticulo.Value = idImagenArticulo;
                ejecuta.AgregarParametro(pidArticulo);

                MySqlParameter pidusuarioModifico = new MySqlParameter("p_idusuarioModifico", MySqlDbType.Int32);
                pidusuarioModifico.Value = idusuarioModifico;
                ejecuta.AgregarParametro(pidusuarioModifico);

                MySqlParameter ppathimagen = new MySqlParameter("p_pathimagen", MySqlDbType.VarChar);
                ppathimagen.Value = pathimagen;
                ejecuta.AgregarParametro(ppathimagen);

                MySqlParameter piddetimagenarticulo = new MySqlParameter("p_iddetimagenarticulo", MySqlDbType.Int32);
                piddetimagenarticulo.Value = idDetImagenarticulo;
                ejecuta.AgregarParametro(piddetimagenarticulo);

                ejecuta.EjecutaSinResultado();
            }
            catch (MySqlException ex)
            {
                //Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
            return true;
        }

        public bool EliminaImagenesArticulosDetalle(int IdDetImagenArticulo, int idImagenArticulo, int idusuarioModifico)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspImagenesArticulosEliminaDetalle");

                MySqlParameter pidArticulo = new MySqlParameter("p_IdDetImagenArticulo", MySqlDbType.Int32);
                pidArticulo.Value = IdDetImagenArticulo;
                ejecuta.AgregarParametro(pidArticulo);

                MySqlParameter pidImagenArticulo = new MySqlParameter("p_idImagenArticulo", MySqlDbType.Int32);
                pidImagenArticulo.Value = idImagenArticulo;
                ejecuta.AgregarParametro(pidImagenArticulo);

                MySqlParameter pidusuarioModifico = new MySqlParameter("p_idusuarioModifico", MySqlDbType.Int32);
                pidusuarioModifico.Value = idusuarioModifico;
                ejecuta.AgregarParametro(pidusuarioModifico);

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

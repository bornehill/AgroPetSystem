using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agropet.Entidades.Seguridad;
using AgroPET.Datos.Comun;

namespace AgroPET.Datos.Seguridad
{
    public sealed class DatosMenuWeb
    {
        #region Propiedades
        public string Error
        {
            get;
            set;
        }
        #endregion

        #region Metodos
        public List<EntidadMenuWeb> ObtenerMenus()
        {
            MySqlDataReader drDatos;
            List<EntidadMenuWeb> lst = new List<EntidadMenuWeb>();
            EjecutaSP spEjecutaSP = new EjecutaSP("usp_MenuWebObtener");

            MySqlParameter pParamMenuId = new MySqlParameter("MenuId", MySqlDbType.Int32);
            pParamMenuId.Value = DBNull.Value;
            spEjecutaSP.AgregarParametro(pParamMenuId);


            MySqlParameter pParamMenu = new MySqlParameter("Menu", MySqlDbType.Int32);
            pParamMenu.Value = DBNull.Value;
            spEjecutaSP.AgregarParametro(pParamMenu);

            drDatos = spEjecutaSP.ObtenerReader();

            lst = ConversionesEntidadesNegocio<EntidadMenuWeb>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lst;
        }

        public List<EntidadMenuWeb> ObtenerMenus(string buscar)
        {
            MySqlDataReader drDatos;
            List<EntidadMenuWeb> lst = new List<EntidadMenuWeb>();
            EjecutaSP spEjecutaSP = new EjecutaSP("usp_MenuWebObtener");
            MySqlParameter pParamMenuId = new MySqlParameter("MenuId", MySqlDbType.Int32);
            pParamMenuId.Value = DBNull.Value;
            spEjecutaSP.AgregarParametro(pParamMenuId);


            MySqlParameter pParamMenu = new MySqlParameter("Menu", MySqlDbType.Int32);
            pParamMenu.Value = buscar;
            spEjecutaSP.AgregarParametro(pParamMenu);

            drDatos = spEjecutaSP.ObtenerReader();

            lst = ConversionesEntidadesNegocio<EntidadMenuWeb>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lst;
        }

        public List<EntidadMenuWeb> ObtenerMenus(int ibuscar)
        {
            MySqlDataReader drDatos;
            List<EntidadMenuWeb> lst = new List<EntidadMenuWeb>();
            EjecutaSP spEjecutaSP = new EjecutaSP("usp_MenuWebObtener");
            MySqlParameter pParamMenuId = new MySqlParameter("MenuId", MySqlDbType.Int32);
            pParamMenuId.Value = ibuscar;
            spEjecutaSP.AgregarParametro(pParamMenuId);


            MySqlParameter pParamMenu = new MySqlParameter("Menu", MySqlDbType.Int32);
            pParamMenu.Value = DBNull.Value;
            spEjecutaSP.AgregarParametro(pParamMenu);

            drDatos = spEjecutaSP.ObtenerReader();

            lst = ConversionesEntidadesNegocio<EntidadMenuWeb>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lst;
        }

        public bool InsertarMenu(EntidadMenuWeb menu)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspMenuWeb_Alta");

                MySqlParameter pParamMenu = new MySqlParameter("Menu", MySqlDbType.VarChar, 100);
                pParamMenu.Value = menu.Menu;
                ejecuta.AgregarParametro(pParamMenu);

                MySqlParameter pParamMenuUrl = new MySqlParameter("MenuUrl", MySqlDbType.VarChar, 250);
                pParamMenuUrl.Value = menu.MenuUrl;
                ejecuta.AgregarParametro(pParamMenuUrl);

                MySqlParameter pParamPadre = new MySqlParameter("Padre", MySqlDbType.Int32);
                pParamPadre.Value = menu.Padre;
                ejecuta.AgregarParametro(pParamPadre);

                MySqlParameter pParamActivo = new MySqlParameter("Activo", MySqlDbType.Bit);
                pParamActivo.Value = menu.Activo;
                ejecuta.AgregarParametro(pParamActivo);

                MySqlParameter pParamUsuario = new MySqlParameter("CreacionUsuarioId", MySqlDbType.Int32);
                pParamUsuario.Value = menu.CreacionUsuarioId;
                ejecuta.AgregarParametro(pParamUsuario);

                ejecuta.EjecutaSinResultado();
            }
            catch (MySqlException ex)
            {
                Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
            return true;
        }

        public bool ModificarMenu(EntidadMenuWeb menu)
        {
            try
            {
                EjecutaSP ejecuta = new EjecutaSP("uspMenuWeb_Actualiza");

                MySqlParameter pParamMenuId = new MySqlParameter("Id", MySqlDbType.Int32);
                pParamMenuId.Value = menu.MenuId;
                ejecuta.AgregarParametro(pParamMenuId);

                MySqlParameter pParamMenu = new MySqlParameter("Menu", MySqlDbType.VarChar, 100);
                pParamMenu.Value = menu.Menu;
                ejecuta.AgregarParametro(pParamMenu);

                MySqlParameter pParamMenuUrl = new MySqlParameter("MenuUrl", MySqlDbType.VarChar, 250);
                pParamMenuUrl.Value = menu.MenuUrl;
                ejecuta.AgregarParametro(pParamMenuUrl);

                MySqlParameter pParamPadre = new MySqlParameter("Padre", MySqlDbType.Int32);
                pParamPadre.Value = menu.Padre;
                ejecuta.AgregarParametro(pParamPadre);

                MySqlParameter pParamActivo = new MySqlParameter("Activo", MySqlDbType.Bit);
                pParamActivo.Value = menu.Activo;
                ejecuta.AgregarParametro(pParamActivo);

                MySqlParameter pParamUsuario = new MySqlParameter("ModificacionUsuarioId", MySqlDbType.Int32);
                pParamUsuario.Value = menu.CreacionUsuarioId;
                ejecuta.AgregarParametro(pParamUsuario);

                ejecuta.EjecutaSinResultado();
            }
            catch (MySqlException ex)
            {
                Error = "Code Error :: " + ex.ErrorCode + " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
            return true;
        }

        public void AsignarArticulosMicrosip(EntidadMenuArticulos ent)
        {
            EjecutaSP spEjecutaSP = new EjecutaSP("uspAsignarArticulosMenuWeb_Insertar");

            MySqlParameter pUnParametro = new MySqlParameter("MenId", MySqlDbType.Int64);
            pUnParametro.Value = ent.MenuId;
            spEjecutaSP.AgregarParametro(pUnParametro);

            pUnParametro = new MySqlParameter("idGpo_Lin", MySqlDbType.Int64);
            pUnParametro.Value = ent.IdGrupo_Linea;
            spEjecutaSP.AgregarParametro(pUnParametro);

            pUnParametro = new MySqlParameter("idLin_Art", MySqlDbType.Int64);
            pUnParametro.Value = ent.IdGrupo_Linea;
            spEjecutaSP.AgregarParametro(pUnParametro);

            pUnParametro = new MySqlParameter("idArt", MySqlDbType.Int64);
            pUnParametro.Value = ent.IdArticulo;
            spEjecutaSP.AgregarParametro(pUnParametro);

            spEjecutaSP.EjecutaSinResultado();

            spEjecutaSP.Dispose();
        }

        //uspDesAsignarArticulosMenuWeb_Delete
        public void DesAsignarArticulosMicrosip(EntidadMenuArticulos ent)
        {
            EjecutaSP spEjecutaSP = new EjecutaSP("uspDesAsignarArticulosMenuWeb_Delete");

            MySqlParameter pUnParametro = new MySqlParameter("MenId", MySqlDbType.Int64);
            pUnParametro.Value = ent.MenuId;
            spEjecutaSP.AgregarParametro(pUnParametro);

            pUnParametro = new MySqlParameter("idGpo_Lin", MySqlDbType.Int64);
            pUnParametro.Value = ent.IdGrupo_Linea;
            spEjecutaSP.AgregarParametro(pUnParametro);

            pUnParametro = new MySqlParameter("idLin_Art", MySqlDbType.Int64);
            pUnParametro.Value = ent.IdGrupo_Linea;
            spEjecutaSP.AgregarParametro(pUnParametro);

            pUnParametro = new MySqlParameter("idArt", MySqlDbType.Int64);
            pUnParametro.Value = ent.IdArticulo;
            spEjecutaSP.AgregarParametro(pUnParametro);

            spEjecutaSP.EjecutaSinResultado();

            spEjecutaSP.Dispose();
        }

        #endregion
    }
}

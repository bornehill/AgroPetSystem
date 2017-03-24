using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agropet.Entidades.Seguridad;
using AgroPET.Datos.Seguridad;

namespace AgropPET.Negocio.Seguridad
{
    public sealed class NegocioMenuWeb : IDisposable
    {
        #region Propiedades
        public Exception Error { get; set; }
        public string ConnectionString { get; set; }
        private const string MsgException = "Error, no se puede crear una instancia sin la cadena de conexión";
        #endregion

        #region Constructor
        #endregion

        #region Metodos

        /// <summary>
        /// Obtiene una lista de menus filtradas
        /// </summary>
        /// <returns>Lista con la información de los menus</returns>
        public List<EntidadMenuWeb> ObtenerMenus()
        {
            List<EntidadMenuWeb> lstObjs;

            var oDatos = new DatosMenuWeb();
            lstObjs = oDatos.ObtenerMenus();

            if (lstObjs != null)
            {
                lstObjs = lstObjs.OrderBy(oMenu => oMenu.Padre).ToList();
            }

            return lstObjs;
        }

        /// <summary>
        /// Obtiene una lista de menus filtradas
        /// </summary>
        /// <param name="sBuscar">Nombre del menu</param>
        /// <returns>Lista con la información de los menus</returns>
        public List<EntidadMenuWeb> ObtenerMenus(string sBuscar)
        {
            List<EntidadMenuWeb> lstObjs;
            var oDatos = new DatosMenuWeb();
            if (string.IsNullOrEmpty(sBuscar))
            {
                Error = new Exception(string.Format("Error: el parametro de la consulta no es valida: sBuscar = '{0}'", sBuscar));
                return null;
            }
            else
            {
                lstObjs = oDatos.ObtenerMenus(sBuscar);
            }

            lstObjs = lstObjs.OrderBy(oMenu => oMenu.Padre).ToList();

            return lstObjs;
        }

        /// <summary>
        /// Obtiene una lista de menus filtradas
        /// </summary>
        /// <param name="iBuscar">ID del menu</param>
        /// <returns>Lista con la información de los menus</returns>
        public List<EntidadMenuWeb> ObtenerMenus(int iBuscar)
        {
            List<EntidadMenuWeb> lstObjs;
            var oDatos = new DatosMenuWeb();
            if (iBuscar == 0)
            {
                Error = new Exception(string.Format("Error: el parametro de la consulta no es valida: iUsuario = {0}", iBuscar));
                return null;
            }
            else
            {
                lstObjs = oDatos.ObtenerMenus(iBuscar);
            }

            
            lstObjs = lstObjs.OrderBy(oMenu => oMenu.Padre).ToList();

            return lstObjs;
        }

        /// <summary>
        /// Envía a la base de datos la información de un nuevo(true)/edicion(false) menu
        /// <param name="bAccion"></param>
        /// </summary>
        public bool InsertarEditar(bool bAccion, EntidadMenuWeb oMenu)
        {
            try
            {
                //// Se inserta el registro en la base de datos
                var oDatos = new DatosMenuWeb();
                bool bError = false;
                if (bAccion)
                {
                    bError = !oDatos.InsertarMenu(oMenu);
                    if (bError)
                    {
                        Error = new Exception(string.Format("Error al insertar el registro en la base de datos: '{0}'", oDatos.Error));
                        return false;
                    }

                }
                else
                {
                    bError = !oDatos.ModificarMenu(oMenu);
                    if (bError)
                    {
                        Error = new Exception(string.Format("Error al modificar el registro en la base de datos: '{0}'", oDatos.Error));
                        return false;
                    }
                }
                                
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public bool AsignarArticulosMicrosip(EntidadMenuArticulos ent)
        {
            try
            {
                //// Se inserta el registro en la base de datos
                var oDatos = new DatosMenuWeb();
                oDatos.DesAsignarArticulosMicrosip(ent);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        public bool DesAsignarArticulosMicrosip(EntidadMenuArticulos ent)
        {
            try
            {
                //// Se inserta el registro en la base de datos
                var oDatos = new DatosMenuWeb();
                oDatos.DesAsignarArticulosMicrosip(ent);
                return true;
            }
            catch (Exception ex)
            {
                Error = ex;
                return false;
            }
        }

        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            Error = null;
            ConnectionString = string.Empty;
        }
        #endregion
    }
}

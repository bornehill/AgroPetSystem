using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Agropet.Entidades.Seguridad;
using AgropPET.Negocio.Seguridad;

namespace AdminAgropet
{
    public partial class frmLoginModuloAdmin : System.Web.UI.Page
    {

        NegocioSeguridad BRNegocio = new NegocioSeguridad();

        #region Campos

        private const string ApplicationName = "Admin Agropet";

        private const string ScriptsMensajes = "Login_Mensaje";

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //MostrarMensaje(CrearMensaje("Entrando al aplicativo."));
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            GC.Collect();
        }

        #region Funciones

        /// <summary>
        /// Regresa un script que sirve para mostrar un mensaje en el cliente
        /// </summary>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        private string CrearMensaje(string sMensaje)
        {
            sMensaje = sMensaje.Replace("\n", "<br />");
            div_Mensaje.InnerHtml = sMensaje;

            return "MostrarMensaje();";
        }

        /// <summary>
        /// Muestra un mensaje en el explorador cliente
        /// </summary>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        private void MostrarMensaje(string sMensaje)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(string), ScriptsMensajes, sMensaje, true);
        }

        #endregion

        protected void btn_Aceptar_Click(object sender, EventArgs e)
        {
            string sCveUsuario;
            string sPasswdUsuario;
            bool bExisteUsuario = false;
            string sMensaje=string.Empty;
            EntidadUsuarioLogeado UsuarioSesion;
            List<EntidadDetAccesoPerfil> lstMenuUsuario;

            try
            {
                sCveUsuario = this.txt_Usuario.Text.Trim();
                sPasswdUsuario = this.txt_Contrasena.Text.Trim();

                if (sCveUsuario.Equals(string.Empty))
                {
                    MostrarMensaje(CrearMensaje("No ha especificado las credenciales del usuario, Debe proporcionar Usuario y Password."));
                    return;
                }
                else
                {
                    if (sPasswdUsuario.Equals(string.Empty))
                    {
                        MostrarMensaje(CrearMensaje("Debe especificar el password del usuario especificado."));
                        return;
                    }
                }

                /// Aqui se validan las credenciales capturadas

                UsuarioSesion = BRNegocio.ValidaUsuario(sCveUsuario, sPasswdUsuario, ref bExisteUsuario);

                if (bExisteUsuario.Equals(false))
                {
                    MostrarMensaje(CrearMensaje(string.Format("El usuario especificado {0} no existe, o hay un error en las credenciales especificadas.", sCveUsuario)));
                }
                else
                {
                    lstMenuUsuario = BRNegocio.ObtenerMenuPerfilUsuario(UsuarioSesion.idperfil);
                    Session["UsuarioSesion"] = UsuarioSesion;
                    Session["MenuAplicacion"] = lstMenuUsuario;
                    Response.Redirect("~/frmMenuModuloAdmin.aspx", false);
                }
            }
            catch (Exception ex)
            {
                string sMsgError;

                sMsgError = string.Format("Ocurrio un error al intentar validar al usuario: {0}. ", ex.Message.Trim());
                MostrarMensaje(CrearMensaje(sMsgError));
            }
        }
    }
}
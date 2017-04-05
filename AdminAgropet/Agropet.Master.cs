using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using AgroPET.Entidades.Seguridad;
using Utilidades.Extensiones;

namespace AdminAgropet
{
    public partial class Agropet : System.Web.UI.MasterPage
    {

        #region Campos

        private EntidadUsuarioLogeado _objUsuario;

        #endregion

        #region Page Init

        protected void Page_Init(object sender, EventArgs e)
        {
            Page.Header.DataBind();
        }

        #endregion

        #region Page Load

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verificar si la sesion es valida
            if (Session["UsuarioSesion"].IsNull())
            {
                //Error en la sesion
                Session.Abandon();
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }

            _objUsuario = (EntidadUsuarioLogeado)Session["UsuarioSesion"];

            //Cerrar sesion manual
            string sNombre = (Request["__EVENTTARGET"] ?? string.Empty);
            if (!string.IsNullOrEmpty(sNombre) && sNombre.Equals("CerrarSesion"))
            {
                lnkCloseSession_Click(sender, e);
                return;
            }

            if (_objUsuario != null)
            {
                this.lblRolName.Text = _objUsuario.nombreperfil.ToUpper();
                this.lblName.Text = _objUsuario.nombre.ToUpper();
            }

            //this.lblLastAccess.Text = Session["NombreAplicacion"].ToString().ToUpper();

            if (!Page.IsPostBack)
            {
                mnu_Principal.Items.Clear();
                var lstOpcionesMenu = Session["MenuAplicacion"] as List<EntidadDetAccesoPerfil>;
                CargarMenu(lstOpcionesMenu);
            }
        }

        #endregion

        #region Page_Unload

        protected void Page_Unload(object sender, EventArgs e)
        {
            MenuItem m = mnu_Principal.SelectedItem;
            GC.Collect();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Regresa un script que permite mostrar un mensaje en el cliente
        /// </summary>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        /// <returns>String  con el script para mostrar el mensaje</returns>
        public string CrearMensaje(string sMensaje)
        {
            sMensaje = Server.HtmlEncode(sMensaje.Trim().Left(250));
            if (sMensaje != null) sMensaje = sMensaje.Replace(@"\n", "<br />");

            string script = string.Format("$('#div_Mensaje').html(\"{0}\");\n", sMensaje);
            script = string.Concat(script, "$('#div_Mensaje').showTopbarMessage({ background: \"#580000\", foreColor: \"#FFFFFF\", fontSize: \"9pt\" });");

            return script;
        }

        /// <summary>
        /// Regresa un script que permite mostrar un mensaje en el cliente
        /// </summary>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        /// <param name="tipo">Color de la ventana(0:Rojo; 1:Gris; 2:Amarillo)</param>
        /// <returns>String  con el script para mostrar el mensaje</returns>
        public string CrearMensaje(string sMensaje, int tipo)
        {
            string color = "", cfuente = "";
            sMensaje = Server.HtmlEncode(sMensaje.Trim().Left(250));
            if (sMensaje != null) sMensaje = sMensaje.Replace(@"\n", "<br />");

            switch (tipo)
            {
                ////Rojo
                case 0:
                    color = "580000";
                    cfuente = "FFFFFF";
                    break;
                ////Gris
                case 1:
                    color = "666666";
                    cfuente = "FFFFFF";
                    break;
                ////Amarillo
                case 2:
                    color = "FFFF33";//"FFCC00";
                    cfuente = "000000";
                    break;
            }

            string script = string.Format("$('#div_Mensaje').html(\"{0}\");\n", sMensaje);
            script = string.Concat(script, "$('#div_Mensaje').showTopbarMessage({ background: \"#" + color + "\", foreColor: \"#" + cfuente + "\", fontSize: \"9pt\" });");

            return script;
        }

        /// <summary>
        /// Regresa un script que permite mostrar un mensaje en el cliente (de error)
        /// </summary>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        /// <param name="sAltura">Altura del mensaje</param>
        /// <returns>String  con el script para mostrar el mensaje</returns>
        public string CrearMensajeError(string sMensaje, string sAltura)
        {
            sMensaje = Server.HtmlEncode(sMensaje.Trim().Left(250));
            if (sMensaje != null) sMensaje = sMensaje.Replace(@"\n", "<br />");

            string script = string.Format("$('#div_Mensaje').html(\"{0}\");\n", sMensaje);
            script = string.Concat(script, "$('#div_Mensaje').showTopbarMessage({ background: \"#580000\", close: \"click\", foreColor: \"#FFFFFF\", fontSize: \"9pt\", height: \"");
            script = string.Concat(script, sAltura, " \" });");

            return script;
        }

        /// <summary>
        /// Regresa un script que permite mostrar un mensaje en el cliente (de error)
        /// </summary>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        /// <returns>String  con el script para mostrar el mensaje</returns>
        public string CrearMensajeError(string sMensaje)
        {
            sMensaje = Server.HtmlEncode(sMensaje.Trim().Left(250));
            if (sMensaje != null) sMensaje = sMensaje.Replace(@"\n", "<br />");

            string script = string.Format("$('#div_Mensaje').html(\"{0}\");\n", sMensaje);
            script = string.Concat(script, "$('#div_Mensaje').showTopbarMessage({ background: \"#580000\", close: \"click\", foreColor: \"#FFFFFF\", fontSize: \"9pt\" });");

            return script;
        }

        public bool SessionValidate()
        {
            bool blnStatus = false;

            if (Session["UsuarioSesion"] == null)
            {
                blnStatus = false;

                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                return blnStatus;
            }
            else
            {
                blnStatus = true;
            }

            return blnStatus;
        }

        public Label Titulo
        {
            get { return lblTitulo; }
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Crea el menú principal, de acuerdo al perfil del usuario actual
        /// </summary>
        private void CargarMenu(List<EntidadDetAccesoPerfil> oOpciones)
        {
            // Se agregan los niveles principales

            List<EntidadDetAccesoPerfil> lstOpciones = (from opcion in oOpciones
                                                        where opcion.espadre.Equals("SI")
                                                        select opcion).ToList<EntidadDetAccesoPerfil>();

            foreach (EntidadDetAccesoPerfil objOpcion in lstOpciones)
            {
                // Validar que el id de menú no exista con anterioridad
                EntidadDetAccesoPerfil opcion = objOpcion;
                var menues = (from MenuItem itmMenu in mnu_Principal.Items where opcion != null && itmMenu.Value.Equals(Convert.ToString(opcion.idmenu)) select 1);
                if (menues.Any()) continue;

                var objItem = new MenuItem(objOpcion.nombremenu, Convert.ToString(objOpcion.idmenu));

                mnu_Principal.Items.Add(objItem);
                CargarSubmenu(objItem, oOpciones);
            }
        }

        /// <summary>
        /// Carga recursivamente la opción de menú especificada
        /// </summary>
        /// <param name="itmOpcion">Elemento al que inspeccionar recursivamente</param>
        /// <param name="lstOpciones">Lista de opciones a asignar</param>
        private void CargarSubmenu(MenuItem itmOpcion, IEnumerable<EntidadDetAccesoPerfil> lstOpciones)
        {
            //// Obtener las subopciones que dependenden de esta misma opcion
            IEnumerable<EntidadDetAccesoPerfil> entidadPerfilesMenuses = lstOpciones as IList<EntidadDetAccesoPerfil> ?? lstOpciones.ToList();
            var lstSubopciones = (from opcion in entidadPerfilesMenuses
                                  where opcion.idpadre == Convert.ToInt32(itmOpcion.Value)
                                  orderby opcion.ordenh ascending
                                  select opcion).ToList<EntidadDetAccesoPerfil>();

            foreach (EntidadDetAccesoPerfil objSubopcion in lstSubopciones)
            {
                // Validar que el id de menú no exista con anterioridad
                EntidadDetAccesoPerfil subopcion = objSubopcion;
                var menues = (from MenuItem itmMenu in mnu_Principal.Items where subopcion != null && itmMenu.Value.Equals(Convert.ToString(subopcion.idmenu)) select 1);
                if (menues.Any())
                    continue;

                var objItem = new MenuItem(objSubopcion.nombremenu, Convert.ToString(objSubopcion.idmenu), string.Empty, objSubopcion.menuurl);

                itmOpcion.ChildItems.Add(objItem);
                CargarSubmenu(objItem, entidadPerfilesMenuses);
            }
        }


        #endregion

        #region Eventos

        protected void lnkCloseSession_Click(object sender, EventArgs e)
        {

            Session["UsuarioSesion"] = null;
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Session.Clear();

            Session.Clear();
            Session.Abandon();

            FormsAuthentication.SignOut();

            //Response.Redirect("~/frmLogin.aspx", false);
            Response.Redirect(FormsAuthentication.LoginUrl, false);

            //Session.Abandon();
            //FormsAuthentication.SignOut();                
            //FormsAuthentication.RedirectToLoginPage();

        }

        #endregion

        public void mnu_Principal_MenuItemClick(object sender, MenuEventArgs e)
        {
        }

        protected void mnu_Principal_MenuItemDataBound(object sender, MenuEventArgs e)
        {
        }

    }
}
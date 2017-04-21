using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Utilidades.Extensiones;
using Utilidades.Generales;
using AgropPET.Negocio.Catalogos;
using AgroPET.Entidades.Seguridad;

namespace AdminAgropet.Base
{
    public class BaseCatalogo : System.Web.UI.Page
    {
        private EntidadUsuarioLogeado usuarioLogeado;
        public EntidadUsuarioLogeado UsuarioLogeado
        {
            get { return usuarioLogeado = (EntidadUsuarioLogeado)Session["UsuarioSesion"]; }
            set { usuarioLogeado = value; }
        }

        protected const string cgs_ScriptsMensajes = "Mensaje_Seguridad_";
        protected const string cgs_MensajeError = "Error al realizar la operación: {0}";
        protected const string cgs_MensajeErrorConsulta = "Error al intentar recuperar los datos: {0}";
        protected const string cgs_MensajeOk = "Se realizó la operación de manera correcta.";
        protected const string cgs_SeparadorValor = ":";
        protected const string cgs_SeparadorParam = ";";

        protected const string cgs_NombreCheckSeleccionarItem = "chk_SeleccionarItem";

        protected const string cgs_NombreParamUsrMod = "";

        protected enum enm_GridCommands { ViewRow, InsertRow, UpdateRow, DeleteRow, ExportRows };

        //public virtual void SetValuesCtlsDivReg(IEnumerable<object> controles, GridViewRow row)
        //{
        //    foreach (WebControl control in controles.OfType<WebControl>())
        //    {
        //        string campo = control.Attributes["campo"];

        //        if (control.GetType() == typeof(TextBox))
        //        {
        //            TextBox textBox = ((TextBox)control);
        //            //int index = GetColumnIndexByName(row, campo);
        //            textBox.Text = WebUtility.HtmlDecode(GetColumnByName(row, campo).Text).Trim();
        //        }
        //        else if (control.GetType() == typeof(CheckBox))
        //        {
        //            CheckBox checkBox = ((CheckBox)control);
        //            string vls_Valor = GetColumnByName(row, campo).Text;
        //            if ((vls_Valor.Equals("1")) || (vls_Valor.ToUpper().Equals("SI")) || (vls_Valor.ToUpper().Equals("TRUE")))
        //            {
        //                checkBox.Checked = true;
        //            }
        //            else
        //            {
        //                checkBox.Checked = false;
        //            }
        //        }
        //    }

        //    foreach (wucDropDownEntidadNegocio control in controles.OfType<wucDropDownEntidadNegocio>())
        //    {
        //        string campo = control.Attributes["campo"];
        //        string vls_Valor = GetColumnByName(row, campo).Text;

        //        //Checamos si el control indica el tipo de dato del campo para tratarlo de manera especial
        //        if (control.HasAttribute("tipoDatoCampo"))
        //        {
        //            if (control.Attributes["tipoDatoCampo"].Equals("bool"))
        //            {
        //                vls_Valor = vls_Valor.ToUpper().Equals("TRUE") ?
        //                    "1" : (vls_Valor.ToUpper().Equals("FALSE") ?
        //                        "0" :
        //                        "-1");
        //            }
        //        }

        //        control.IDSeleccionado = vls_Valor;
        //    }
        //}

        //public virtual void SetValuesCtlsDivReg(IEnumerable<object> controles, DataRow row)
        //{
        //    foreach (WebControl control in controles.OfType<WebControl>())
        //    {
        //        string campo = control.Attributes["campo"];
        //        string valor = null;
        //        if ((campo != null) && row.Table.Columns.Contains(campo))
        //            valor = (string)row[campo];

        //        if (control.GetType() == typeof(TextBox))
        //        {
        //            TextBox textBox = ((TextBox)control);
        //            //int index = GetColumnIndexByName(row, campo);                    
        //            textBox.Text = WebUtility.HtmlDecode(valor);
        //        }
        //        else if (control.GetType() == typeof(CheckBox))
        //        {
        //            CheckBox checkBox = ((CheckBox)control);
        //            if ((valor.Equals("1")) || (valor.ToUpper().Equals("SI")) || (valor.ToUpper().Equals("TRUE")))
        //            {
        //                checkBox.Checked = true;
        //            }
        //            else
        //            {
        //                checkBox.Checked = false;
        //            }
        //        }
        //    }

        //    foreach (wucDropDownEntidadNegocio control in controles.OfType<wucDropDownEntidadNegocio>())
        //    {
        //        string campo = control.Attributes["campo"];
        //        string valor = null;
        //        if (row.Table.Columns.Contains(campo))
        //            valor = (string)row[campo];

        //        //Checamos si el control indica el tipo de dato del campo para tratarlo de manera especial
        //        if (control.HasAttribute("tipoDatoCampo"))
        //        {
        //            if (control.Attributes["tipoDatoCampo"].Equals("bool"))
        //            {
        //                valor = valor.ToUpper().Equals("TRUE") ?
        //                    "1" : (valor.ToUpper().Equals("FALSE") ?
        //                        "0" :
        //                        "-1");
        //            }
        //        }
        //        control.IDSeleccionado = valor;
        //    }
        //}

        //public void SetValuesCtlsDivReg<T>(IEnumerable<object> controles, T entidad) where T : new()
        //{
        //    foreach (WebControl control in controles.OfType<WebControl>())
        //    {
        //        string campo = control.Attributes["campo"];

        //        if (control.GetType() == typeof(TextBox))
        //        {
        //            TextBox textBox = ((TextBox)control);
        //            //int index = GetColumnIndexByName(row, campo);
        //            textBox.Text = WebUtility.HtmlDecode(GetValuePropEntidad<T>(entidad, campo));
        //        }
        //        else if (control.GetType() == typeof(CheckBox))
        //        {
        //            CheckBox checkBox = ((CheckBox)control);
        //            string vls_Valor = GetValuePropEntidad<T>(entidad, campo);
        //            if ((vls_Valor.Equals("1")) || (vls_Valor.ToUpper().Equals("SI")) || (vls_Valor.ToUpper().Equals("TRUE")))
        //            {
        //                checkBox.Checked = true;
        //            }
        //            else
        //            {
        //                checkBox.Checked = false;
        //            }
        //        }
        //    }

        //    foreach (wucDropDownEntidadNegocio control in controles.OfType<wucDropDownEntidadNegocio>())
        //    {
        //        string campo = control.Attributes["campo"];
        //        string vls_Valor = GetValuePropEntidad<T>(entidad, campo);

        //        //Checamos si el control indica el tipo de dato del campo para tratarlo de manera especial
        //        if (control.HasAttribute("tipoDatoCampo"))
        //        {
        //            if (control.Attributes["tipoDatoCampo"].Equals("bool"))
        //            {
        //                vls_Valor = vls_Valor.ToUpper().Equals("TRUE") ?
        //                    "1" : (vls_Valor.ToUpper().Equals("FALSE") ?
        //                        "0" :
        //                        "-1");
        //            }
        //        }

        //        control.IDSeleccionado = vls_Valor;
        //    }
        //}

        //public void SetModoVisibleCtlsDivReg(IEnumerable<object> controles, int accion, int accionSoloConsulta)
        //{
        //    bool vlb_SoloConsulta = accion == accionSoloConsulta ? true : false;

        //    foreach (WebControl control in controles.OfType<WebControl>().Where(t => t.Attributes["modoVisible"] != null))
        //    {
        //        char[] vlo_ModoVisible = control.Attributes["modoVisible"].ToCharArray();
        //        bool? vlb_Accion = Conversiones.StringCeroUnoABoolean(vlo_ModoVisible[accion].ToString());

        //        control.Visible = vlb_Accion == null ? false : (bool)vlb_Accion;

        //        SetSoloConsultaCtrl(control, accion, vlb_SoloConsulta);
        //    }

        //    foreach (WebControl control in controles.OfType<WebControl>().Where(t => t.Attributes["modoVisible"] == null))
        //    {
        //        SetSoloConsultaCtrl(control, accion, vlb_SoloConsulta);
        //    }

        //    foreach (wucDropDownEntidadNegocio control in controles.OfType<wucDropDownEntidadNegocio>().Where(t => t.HasAttribute("modoVisible")))
        //    {
        //        char[] vlo_ModoVisible = control.Attributes["modoVisible"].ToCharArray();
        //        bool? vlb_Accion = Conversiones.StringCeroUnoABoolean(vlo_ModoVisible[accion].ToString());

        //        control.MostrarDropDown = vlb_Accion == null ? false : (bool)vlb_Accion;

        //        SetSoloConsultaCtrl(control, accion, vlb_SoloConsulta);
        //    }

        //    foreach (wucDropDownEntidadNegocio control in controles.OfType<wucDropDownEntidadNegocio>().Where(t => !t.HasAttribute("modoVisible")))
        //    {
        //        SetSoloConsultaCtrl(control, accion, vlb_SoloConsulta);
        //    }
        //}

        //public void SetSoloConsultaCtrl(object control, int accion, bool soloConsulta)
        //{
        //    if (control.GetType() == typeof(TextBox))
        //    {
        //        TextBox textBox = ((TextBox)control);
        //        string vls_ModoSoloConsulta = textBox.Attributes["modoSoloConsulta"];

        //        if (vls_ModoSoloConsulta != null)
        //        {
        //            char[] vlo_ModoSoloConsulta = vls_ModoSoloConsulta.ToCharArray();
        //            bool? vlb_AccionSoloConsulta = Conversiones.StringCeroUnoABoolean(vlo_ModoSoloConsulta[accion].ToString());

        //            textBox.ReadOnly = vlb_AccionSoloConsulta == null ? false : (bool)vlb_AccionSoloConsulta;
        //        }
        //        else
        //        {
        //            textBox.ReadOnly = soloConsulta;
        //        }
        //    }
        //    else if (control.GetType() == typeof(CheckBox))
        //    {
        //        CheckBox checkBox = ((CheckBox)control);
        //        string vls_ModoSoloConsulta = checkBox.Attributes["modoSoloConsulta"];
        //        if (vls_ModoSoloConsulta != null)
        //        {
        //            char[] vlo_ModoSoloConsulta = vls_ModoSoloConsulta.ToCharArray();
        //            bool? vlb_AccionSoloConsulta = Conversiones.StringCeroUnoABoolean(vlo_ModoSoloConsulta[accion].ToString());

        //            checkBox.Enabled = vlb_AccionSoloConsulta == null ? false : (bool)vlb_AccionSoloConsulta;
        //        }
        //        else
        //        {
        //            checkBox.Enabled = soloConsulta;
        //        }
        //    }
        //    else if (control.GetType() == typeof(wucDropDownEntidadNegocio))
        //    {
        //        wucDropDownEntidadNegocio combo = ((wucDropDownEntidadNegocio)control);
        //        string vls_ModoSoloConsulta = combo.Attributes["modoSoloConsulta"];
        //        if (vls_ModoSoloConsulta != null)
        //        {
        //            char[] vlo_ModoSoloConsulta = vls_ModoSoloConsulta.ToCharArray();
        //            bool? vlb_AccionSoloConsulta = Conversiones.StringCeroUnoABoolean(vlo_ModoSoloConsulta[accion].ToString());

        //            combo.SoloLectura = vlb_AccionSoloConsulta == null ? false : (bool)vlb_AccionSoloConsulta;
        //        }
        //        else
        //        {
        //            combo.SoloLectura = soloConsulta;
        //        }

        //    }

        //}

        //public int GetColumnIndexByName(GridView grd, string columnName)
        //{
        //    int columnIndex = -1;
        //    if (grd.Rows.Count > 0)
        //    {
        //        GridViewRow row = grd.Rows[0];
        //        foreach (DataControlFieldCell cell in row.Cells)
        //        {
        //            columnIndex++; // keep adding 1 while we don't have the correct name

        //            if (cell.ContainingField is BoundField)
        //                if (((BoundField)cell.ContainingField).DataField.ToUpper().Equals(columnName.ToUpper()))
        //                    break;
        //        }
        //    }
        //    return columnIndex;
        //}

        //public int GetColumnIndexByName(GridViewRow row, string columnName)
        //{
        //    int columnIndex = -1;
        //    foreach (DataControlFieldCell cell in row.Cells)
        //    {
        //        if (cell.ContainingField is BoundField)
        //            if (((BoundField)cell.ContainingField).DataField.ToUpper().Equals(columnName.ToUpper()))
        //                break;
        //        columnIndex++; // keep adding 1 while we don't have the correct name
        //    }
        //    return columnIndex;
        //}

        //public TableCell GetColumnByName(GridView grd, string columnName)
        //{
        //    TableCell column = new TableCell();
        //    if (grd.Rows.Count > 0)
        //    {
        //        GridViewRow row = grd.Rows[0];
        //        foreach (DataControlFieldCell cell in row.Cells)
        //        {
        //            if (cell.ContainingField is BoundField)
        //                if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
        //                {
        //                    column = cell;
        //                    break;
        //                }
        //        }
        //    }
        //    return column;
        //}

        //public TableCell GetColumnByName(GridViewRow row, string columnName)
        //{
        //    TableCell column = new TableCell();
        //    foreach (DataControlFieldCell cell in row.Cells)
        //    {
        //        if (cell.ContainingField is BoundField)
        //            if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
        //            {
        //                column = cell;
        //                break;
        //            }
        //    }
        //    return column;
        //}

        //public IEnumerable<object> GetCntrlsDivReg(Panel panel)
        //{
        //    //ControlCollection lista = ;
        //    var controles = (from control in panel.Controls.OfType<WebControl>()
        //                     where control.HasAttributes && ((control.Attributes["campo"] != null) || (control.Attributes["modoVisible"] != null) || (control.Attributes["modoSoloConsulta"] != null))
        //                     select (object)control)
        //                    .Union(
        //                    from control in panel.Controls.OfType<ControlWEB.Comun.wucDropDownEntidadNegocio>()
        //                    where ((control.HasAttribute("campo")) || (control.HasAttribute("modoVisible")) || (control.Attributes["modoSoloConsulta"] != null))
        //                    select (object)control);

        //    return controles;
        //}

        ///// <summary>
        ///// Determina si existe al menos un registro seleccionado en el grid.
        ///// </summary>
        ///// <param name="grdCatalogo">Grid que se desea revisar</param>
        ///// <param name="sNombreCheckBox">Nombre del CheckBox a revisar si esta seleccionado o no</param>
        ///// <returns>True: Si hay al menos un registro seleccionado. False: No hay ninguno seleccionado</returns>
        //public bool HasSelectedRowsByCheckBox(GridView grdCatalogo, string vps_CheckBoxName)
        //{
        //    for (int i = 0; i < grdCatalogo.Rows.Count; i++)
        //    {
        //        CheckBox chk_ItemGrid = (CheckBox)grdCatalogo.Rows[i].FindControl(vps_CheckBoxName);
        //        if (chk_ItemGrid.Checked)
        //            return true;
        //    }

        //    return false;
        //}

        ///// <summary>
        ///// Determina si existe al menos un registro seleccionado en el grid.
        ///// </summary>
        ///// <param name="grdCatalogo">Grid que se desea revisar</param>
        ///// <param name="sNombreCheckBox">Nombre del CheckBox a revisar si esta seleccionado o no</param>
        ///// <returns>True: Si hay al menos un registro seleccionado. False: No hay ninguno seleccionado</returns>
        //public IEnumerable<GridViewRow> GetSelectedRowsByCheckBox(GridView grdCatalogo, string vps_CheckBoxName)
        //{
        //    for (int i = 0; i < grdCatalogo.Rows.Count; i++)
        //    {
        //        CheckBox chk_ItemGrid = (CheckBox)grdCatalogo.Rows[i].FindControl(vps_CheckBoxName);
        //        if (chk_ItemGrid.Checked)
        //            yield return grdCatalogo.Rows[i];
        //    }
        //}

        /// Muestra un mensaje en el explorador cliente
        /// </summary>
        /// <param name="sMensaje">Mensaje a mostrar</param>
        public void MostrarMensaje(Page page, string vps_Llave, string vps_Mensaje, int vpn_Tipo)
        {
            var agropet  = page.Master as Agropet;
            if (agropet != null)
                vps_Mensaje = agropet.CrearMensaje(vps_Mensaje.Replace("\r\n", "").Replace("\n", ""), vpn_Tipo);

            ScriptManager.RegisterStartupScript(page, typeof(string), vps_Llave, vps_Mensaje, true);
        }

        /// <summary>
        /// Obtiene el ID del usuario que esta autenticado
        /// </summary>
        public long GetUserLoggedID()
        {
            EntidadUsuarioLogeado usr=(EntidadUsuarioLogeado)Session["UsuarioSesion"];

            return usr.idusuario;
        }

        /// <summary>
        /// Obtiene el Nombre del usuario que esta autenticado
        /// </summary>
        public string GetNameUserLogged()
        {
            string sNameUser = string.Empty;
            EntidadUsuarioLogeado usr = (EntidadUsuarioLogeado)Session["UsuarioSesion"];

            sNameUser = usr.nombre.Trim();

            return sNameUser;
        }
        


        /// <summary>
        /// Método que Obtiene toda la entidad del usuario que esta loggeado en el sistema
        /// </summary>
        /// <returns>Regresa la entidad «Entidades.Seguridad.EntidadUsuario».</returns>        
        public EntidadUsuarioLogeado GetUserLogged()
        {

            EntidadUsuarioLogeado entidadUsr = (EntidadUsuarioLogeado)Session["UsuarioSesion"];
            return entidadUsr;
        }

        /// <summary>
        /// Obtiene el Nombre del usuario conectado
        /// </summary>
        /// <param name="vpn_Dato">
        /// Indica que dato del nombre regresa la función:
        /// 0 - El nombre completo de la persona (AP, AM, Nombres).
        /// 1 - El nombre completo de la persona (Nombres, AP, AM).
        /// 2 - El apellido paterno de la persona.
        /// 3 - El apellido materno de la persona.
        /// 4 - El nombre de la persona.
        /// </param>

        public string GetUserLoggedName(int vpn_Dato)
        {
            string vls_DatoUsuario = string.Empty;
            EntidadUsuarioLogeado usr = (EntidadUsuarioLogeado)Session["UsuarioSesion"];
            if (vpn_Dato == 0)
            {
                vls_DatoUsuario = usr.nombre.Trim();
            }
            else if (vpn_Dato == 1)
            {
                vls_DatoUsuario = usr.nombre.Trim();
            }
            else if (vpn_Dato == 2)
            {
                vls_DatoUsuario = usr.nombre.Trim();
            }
            else if (vpn_Dato == 3)
            {
                vls_DatoUsuario = usr.nombre.Trim();
            }
            else if (vpn_Dato == 4)
            {
                vls_DatoUsuario = usr.nombre.Trim();
            }

            return vls_DatoUsuario;
        }

        /// <summary>
        /// Método que llena el control ListBox recibido como parametro ref.
        /// </summary>
        /// <param name="sListaDistrib">Cadena que usa el metodo para llenar el control ListBox</param>
        public void LlenaListaDistribucion(string sListaDistrib, ref ListBox lbMiLista)
        {
            char[] sDelim = { '|' };
            string[] asItemsLista;

            lbMiLista.Items.Clear();
            asItemsLista = sListaDistrib.Split(sDelim);
            foreach (string sItem in asItemsLista)
            {
                lbMiLista.Items.Add(sItem);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lbMiLista">Conrol listBox que el método usuara para armar la cadena.</param>
        /// <returns>Cadena con los correos que estaban incluidos en la lista.</returns>
        public string ObtenCadenaListaDist(ListBox lbMiLista)
        {
            string sListaDist = string.Empty;
            bool bInicio = true;
            string sItem = string.Empty;

            foreach (ListItem liItem in lbMiLista.Items)
            {
                sItem = liItem.ToString();
                if (bInicio)
                {
                    bInicio = false;
                    sListaDist = sItem.Trim().ToUpper();
                }
                else
                    sListaDist += "|" + sItem.Trim().ToUpper();
            }

            return sListaDist.Trim();
        }


        /// <summary>
        /// Obtiene una cadena con los parámetros que se van a enviar al SQL dinámico sin generar un objeto Combo
        /// </summary>
        public string GetParametersStringCombo(KeyValuePair<string, string>[] vpo_ParamsValor)
        {
            StringBuilder vls_Resultado = new StringBuilder();

            foreach (KeyValuePair<string, string> vlo_param in vpo_ParamsValor)
            {
                vls_Resultado.Append(vlo_param.Key + cgs_SeparadorValor + vlo_param.Value + cgs_SeparadorParam);
            }

            return vls_Resultado.ToString().Equals("") ? string.Empty : vls_Resultado.ToString();
        }

        /// <summary>
        /// Obtiene el valor de una cadena
        /// </summary>
        public string GetValueString(string vps_String)
        {
            return vps_String == null ? "" : vps_String;
        }

        public string GetValuePropEntidad<T>(T obj_Entidad, string s_Atributo) where T : new()
        {
            string vls_Resultado = string.Empty;

            PropertyInfo[] piPropiedadesEntidad = obj_Entidad.GetType().GetProperties();

            foreach (PropertyInfo piPropiedad in piPropiedadesEntidad.Where(t => t.Name.Equals(s_Atributo)))
            {

                if (piPropiedad.GetValue(obj_Entidad, null) == null)
                {
                    vls_Resultado = string.Empty;
                }
                else if (piPropiedad.PropertyType == typeof(string))
                {
                    vls_Resultado = piPropiedad.GetValue(obj_Entidad, null).ToString();
                }
                else if ((piPropiedad.PropertyType == typeof(int)) || (piPropiedad.PropertyType == typeof(int?)))
                {
                    vls_Resultado = ((int?)piPropiedad.GetValue(obj_Entidad, null)).ToString();
                }
                else if ((piPropiedad.PropertyType == typeof(float)) || (piPropiedad.PropertyType == typeof(float?)))
                {
                    vls_Resultado = ((float?)piPropiedad.GetValue(obj_Entidad, null)).ToString();
                }
                else if ((piPropiedad.PropertyType == typeof(double)) || (piPropiedad.PropertyType == typeof(double?)))
                {
                    vls_Resultado = ((double)piPropiedad.GetValue(obj_Entidad, null)).ToString();
                }
                else if ((piPropiedad.PropertyType == typeof(DateTime)) || (piPropiedad.PropertyType == typeof(DateTime?)))
                {
                    vls_Resultado = ((DateTime)piPropiedad.GetValue(obj_Entidad, null)).ToString();
                }
            }

            return vls_Resultado;
        }

        /// <summary>
        /// Obtiene y establece el valor del tipo de acceso para la página que tiene para el perfil
        /// </summary>
        //////public void EstableceAcceso()
        //////{
        //////    EstableceAcceso(false, string.Empty);
        //////}

        /// <summary>
        /// Obtiene y establece el valor del tipo de acceso para la página que tiene para el perfil
        /// <param name="vpb_Parametros">Indica si se toma el valor de la cadena vps_Parametros para la URL de la pagina en menú</param>
        /// <param name="vps_Parametros">Cadena que trae los parámetros y forma parte de la URL de la página en menú</param>
        /// </summary>
        //////public void EstableceAcceso(bool vpb_Parametros, string vps_Parametros)
        //////{
        //////    long vli_IdPerfil = ((EntidadUsuarioLogeado)Session["UsuarioSesion"]).idperfil;
        //////    string vls_File = string.Empty;

        //////    if (vpb_Parametros)
        //////        vls_File = Request.AppRelativeCurrentExecutionFilePath.ToString() + "?" + vps_Parametros;
        //////    else
        //////        vls_File = Request.AppRelativeCurrentExecutionFilePath.ToString();

        //////    Entidades.Seguridad.EntidadPerfilesMenus pm = new Negocio.Seguridad.NegocioSeguridad().ObtenerPerfilMenuParaAcceso(vli_IdPerfil, vls_File);

        //////    Session["SoloLectura"] = pm.BConsulta;
        //////}

        /// <summary>
        /// Obtiene el valor del tipo de acceso para la página, TRUE (Solo Consulta) FALSE (LECTURA / ESCRITURA)        
        /// </summary>
        public bool ObtieneAcceso()
        {
            return (Session["SoloLectura"] == null ? false : (bool)(Session["SoloLectura"]));
        }

        /// <summary>
        /// Obtiene el parametro de número maximso de dias válidos para las consultas del sistema.
        /// </summary>
        /// <returns></returns>
        //////public int ObtenDiasMaximosConsulta(string sTipoConsulta, string sScriptMsgs, int nTipoMsg)
        //////{
        //////    int nMaxNumDias = 0;
        //////    EntidadParametros datosParametro;
        //////    EntidadParametros filtroParam = new EntidadParametros();

        //////    #region Asginacion de datos a la entidad que sirve como filtro
        //////    filtroParam.NIdParametro = null;
        //////    filtroParam.NIdTipoParametro = 31;  // Tipo de Parametro que indica que es el número de días máx. para consulta.
        //////    filtroParam.NIdUsuarioModifico = null;
        //////    filtroParam.VDescripcion = sTipoConsulta.Trim(); // "Clave" que indica que se traiga el num de dias para racks danados.
        //////    filtroParam.VValor = null;
        //////    filtroParam.VValor2 = null;
        //////    filtroParam.VValor3 = null;
        //////    filtroParam.VValor4 = null;
        //////    filtroParam.BActivo = null;
        //////    #endregion

        //////    using (var clsCatalogos = new NegocioControlCatalogos<EntidadParametros>())
        //////    {
        //////        datosParametro = clsCatalogos.ObtenerEntidadesCatalogo(filtroParam)[0];

        //////        if (datosParametro.IsNull())
        //////        {
        //////            MostrarMensaje(this, sScriptMsgs, clsCatalogos.Error.Message, nTipoMsg);
        //////            nMaxNumDias = -1;
        //////        }
        //////        else
        //////        {
        //////            if (Extensiones.IsNumeric(datosParametro.VValor))
        //////                nMaxNumDias = int.Parse(datosParametro.VValor);
        //////            else
        //////                MostrarMensaje(this, sScriptMsgs, "Error: Se esperaba un dato numérico, no se pudo obtener parámetro.",
        //////                    nTipoMsg);
        //////        }
        //////        clsCatalogos.Dispose();
        //////    }
        //////    return nMaxNumDias;
        //////}



        #region FuncionalidadGenerica

        public enum PrimerElemento
        {
            Todos,
            Selecciona,
            Vacio,
            SinPrimerValor,
            ValorPropio
        }

        protected void LlenarDropDown(DropDownList dropDown, object datos, string campoTexto, string campoValor, PrimerElemento datoInicial)
        {
            LlenarDropDown(dropDown, datos, campoTexto, campoValor, datoInicial, string.Empty);
        }

        protected void LlenarDropDown(DropDownList dropDown, object datos, string campoTexto, string campoValor, PrimerElemento datoInicial, string valorPrimerElemento)
        {
            if (dropDown != null)
            {
                dropDown.DataSource = datos;
                dropDown.DataTextField = campoTexto;
                dropDown.DataValueField = campoValor;
                dropDown.DataBind();

                InsertaPrimerElementoDropDown(dropDown, datoInicial, valorPrimerElemento);
            }
        }

        protected void InsertaPrimerElementoDropDown(DropDownList dropDown, PrimerElemento datoInicial, string valor)
        {
            string primerCampo = string.Empty;
            switch (datoInicial)
            {
                case PrimerElemento.Selecciona:
                    primerCampo = "Selecciona";
                    break;
                case PrimerElemento.Todos:
                    primerCampo = "Todos";
                    break;
                case PrimerElemento.Vacio:
                    primerCampo = "";
                    break;
                case PrimerElemento.ValorPropio:
                    primerCampo = valor;
                    break;
            }

            if (datoInicial != PrimerElemento.SinPrimerValor)
                dropDown.Items.Insert(0, new ListItem(primerCampo, valor));
        }

        #endregion FuncionalidadGenerica
    }
}
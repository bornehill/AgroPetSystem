using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.ComponentModel;

using AgropPET.Negocio.Catalogos;



namespace AdminAgropet.ControlWEB.Comun
{
    /// <summary>
    /// Control Web para mostar una entidad de negocio especificada, en un drop down list.
    /// </summary>
    
    [RefreshProperties(RefreshProperties.All)]
    [Browsable(true)]

    public partial class wucDropDownEntidadNegocio : System.Web.UI.UserControl
    {
        #region Constantes privadas

        private const string ValorIndexDropDownNulo = "-1";
        private const string TextoIndiceCeroDropDown = "Seleccione...";
        private const string NombreDefaultCampoActivo = "Activo";
        private const string GrupoValidacionDefault = "G";
        private const string ValorIdDefault = "0";

        #endregion

        #region Variables privadas

        private bool? _idEncontrado;
        private bool? _ejecutarHabilitarDeshabilitarDropDown;

        #endregion

        #region Constantes estáticas

        public static readonly string NombreCampoValor = "i_NIdValor";
        public static readonly string NombreCampoDescripcion = "s_VDescriValor";

        public static readonly string SeparadorValor = ":";
        public static readonly string SeparadorParam = ";";

        #endregion

        #region Declaración de eventos del control

        /// <summary>
        /// Evento al momento de seleccionar un item del dropdownlist
        /// </summary>
        [Category("AgroPET - Acciones")]
        [Browsable(true)]
        public event EventHandler CambioSeleccionIndice
        {
            add { this.btnEntidadNegocio.Click += value; }
            remove { this.btnEntidadNegocio.Click -= value; }
        }

        #endregion

        #region propiedades publicas

        /// <summary>
        /// Obtiene o establece cuando el dropdown debe hacer un autopostback
        /// </summary>
        [Category("AgroPET - Acciones")]
        [DefaultValue(false)]
        public bool AutoPostBack
        {
            get { return this.hdnDropDownPostBack.Value.Equals("1") ? true : false; }
            set { this.hdnDropDownPostBack.Value = value ? "1" : "0"; }
        }

        /// <summary>
        /// Obtiene o establece el DataSource de un dropDown
        /// </summary>
        [Category("AgroPET - Acciones")]
        public object DataSource
        {
            get { return this.ddlEntidadNegocio.DataSource; }
            set { this.ddlEntidadNegocio.DataSource = value; }
        }

        /// <summary>
        /// Obtiene o establece el DataTextField de un dropDown
        /// </summary>
        [Category("AgroPET - Acciones")]
        public string DataTextField
        {
            get { return this.ddlEntidadNegocio.DataTextField; }
            set { this.ddlEntidadNegocio.DataTextField = value; }
        }

        /// <summary>
        /// Obtiene o establece el DataValueField de un dropDown
        /// </summary>
        [Category("AgroPET - Acciones")]
        public string DataValueField
        {
            get { return this.ddlEntidadNegocio.DataValueField; }
            set { this.ddlEntidadNegocio.DataValueField = value; }
        }

        /// <summary>
        /// Obtiene o establece cuando se activa o desactiva la funcion  de busqueda en el listado del
        /// entidades que tiene el dropdown.
        /// </summary>
        [Category("AgroPET - Acciones")]
        [DefaultValue(true)]
        public bool ActivarListSearch
        {
            get { return this.ddlEnNgListSearchExtender.Enabled; }
            set { this.ddlEnNgListSearchExtender.Enabled = value; }
        }

        /// <summary>
        /// Obtiene o establece cuando se activa o desactiva la funcion para validar la seleccion de un registro
        /// </summary>
        [Category("AgroPET - Acciones")]
        [DefaultValue(false)]
        public bool ActivarValidacionSeleccion
        {
            get { return this.rfvEntidadNegocio.Enabled; }
            set { this.rfvEntidadNegocio.Enabled = value; }
        }

        /// <summary>
        /// Obtiene o estable el grupo de validacion del dropdownlist
        /// </summary>
        [Category("AgroPET - Acciones")]
        [DefaultValue(GrupoValidacionDefault)]
        public string ValidationGroup
        {
            get { return this.ddlEntidadNegocio.ValidationGroup; }
            set
            {
                this.ddlEntidadNegocio.ValidationGroup = value;
                this.rfvEntidadNegocio.ValidationGroup = value;
            }
        }

        /// <summary>
        /// Obtiene o establece si debe o no mostrar el texto de descripcion
        /// </summary>
        [Category("AgroPET - Presentación")]
        [DefaultValue(true)]
        public bool MostrarDescripcion
        {
            get { return this.pnlDescripcion.Visible; }
            set { this.pnlDescripcion.Visible = value; }
        }

        /// <summary>
        /// Obtiene o establece si debe o no mostrar el panel del dropdownlist
        /// </summary>
        [Category("AgroPET - Presentación")]
        [DefaultValue(true)]
        public bool MostrarDropDown
        {
            get { return this.pnlDropDown.Visible; }
            set { this.pnlDropDown.Visible = value; }
        }

        /// <summary>
        /// Obtiene o establece el texto para descripcion del dropdownlist
        /// </summary>
        [Category("AgroPET - Presentación")]
        public string Descripcion
        {
            get { return this.lblDescripcion.Text; }
            set { this.lblDescripcion.Text = value; }
        }

        /// <summary>
        /// Obtiene o establece lo ancho del Drop Down
        /// </summary>
        [Category("AgroPET - Presentación")]
        public Unit Width
        {
            get { return this.ddlEntidadNegocio.Width; }

            set { this.ddlEntidadNegocio.Width = value; }
        }

        /// <summary>
        /// Obtiene o establece cuando el dropdown es unicamente de solo lectura.
        /// </summary>
        [Category("AgroPET - Presentación")]
        public bool SoloLectura
        {
            get { return Utilidades.Generales.Conversiones.ConvertirSiNoABool(this.hidDropDownSoloLectura.Value); }
            set
            {
                this.hidDropDownSoloLectura.Value = Utilidades.Generales.Conversiones.ConvertirBoolASiNo(value);
                this._ejecutarHabilitarDeshabilitarDropDown = true; // Esta es una bandera de apoyo
            }
        }

        /// <summary>
        /// Determina si se deben alinear los mensajes de alerta del lado izq. del control que esta validando
        /// Valor default es lado derecho
        /// </summary>
        [Category("AgroPET - Presentación")]
        [DefaultValue(false)]
        public bool AlineacionIzqAlerta
        {
            get { return (this.vcerfvEntidadNegocio.PopupPosition == AjaxControlToolkit.ValidatorCalloutPosition.Left); }
            set
            {
                if (value) //Se pide que sea alineado a la izquierda
                    this.vcerfvEntidadNegocio.PopupPosition = AjaxControlToolkit.ValidatorCalloutPosition.Left;
                else
                    this.vcerfvEntidadNegocio.PopupPosition = AjaxControlToolkit.ValidatorCalloutPosition.Right;
            }
        }

        /// <summary>
        /// Obtiene o establece el ID del registro seleccionado, en caso de que no hay nada regresa null
        /// </summary>
        public string IDSeleccionado
        {
            get
            {
                if (this.ddlEntidadNegocio.Items.Count <= 0)
                    return null;

                if (this.ddlEntidadNegocio.SelectedValue == ValorIndexDropDownNulo)
                    return null;

                return this.ddlEntidadNegocio.SelectedValue;
            }
            set
            {
                if (this.ddlEntidadNegocio.Items.Count > 0)
                {
                    ListItem itemTemporal = this.ddlEntidadNegocio.Items.FindByValue(value);
                    if ((itemTemporal != null) && (value != this.ValorIDNulo))
                    {
                        this.ddlEntidadNegocio.SelectedValue = value;
                        this.hdnDropDownValue.Value = value;
                        this._idEncontrado = true;
                    }
                    else
                    {
                        this.ddlEntidadNegocio.SelectedValue = this.ValorIDNulo;
                        this.hdnDropDownValue.Value = this.ValorIDNulo;

                        if ((value == this.ValorIDNulo) || (string.IsNullOrEmpty(value)))
                            this._idEncontrado = null;
                        else
                            this._idEncontrado = false;
                    }
                }
            }
        }

        /// <summary>
        /// True: Cuando se asigna un valor en la propiedad de IDSeleccionado y el valor corresponde con uno dentro de la lista.
        /// False: Cuando el valor asignado no esta en la lista o cuando no se ha asignado algun valor.
        /// Null: No se ha intentado asignar un id
        /// </summary>
        public bool? IdEncontrado
        {
            get { return this._idEncontrado; }
        }

        /// <summary>
        /// Obtiene el valor considerado cuando no se ha seleccionado ningun registro
        /// </summary>
        public string TextoSeleccionado
        {
            get
            {
                if (this.ddlEntidadNegocio.Items.Count <= 0)
                    return null;

                if (this.ddlEntidadNegocio.SelectedValue == ValorIndexDropDownNulo)
                    return null;

                return this.ddlEntidadNegocio.SelectedItem.Text;
            }
        }

        /// <summary>
        /// Obtiene el valor considerado cuando no se ha seleccionado ningun registro
        /// </summary>
        public string ValorIDNulo
        {
            get { return ValorIndexDropDownNulo; }
        }

        /// <summary>
        /// Obtiene o establece el estilo para aplicar al DropDownList
        /// </summary>
        [Category("AgroPET - Presentación")]
        [DefaultValue("ddlGeneral")]
        public string EstiloDropDown
        {
            get { return this.ddlEntidadNegocio.CssClass; }
            set { this.ddlEntidadNegocio.CssClass = value; }
        }

        /// <summary>
        /// Obtiene o establece el estilo para aplicar al DropDownList
        /// </summary>
        [Category("AgroPET - Presentación")]
        public string EstiloValidator
        {
            get { return this.vcerfvEntidadNegocio.CssClass; }
            set { this.vcerfvEntidadNegocio.CssClass = value; }
        }

        /// <summary>
        /// Obtiene o establece el estilo para aplicar al DropDownList
        /// </summary>
        [Category("AgroPET - Presentación")]
        public string EstiloValidator_WarningIcon
        {
            get { return this.vcerfvEntidadNegocio.WarningIconImageUrl; }
            set { this.vcerfvEntidadNegocio.WarningIconImageUrl = value; }
        }

        /// <summary>
        /// Obtiene o establece el estilo para aplicar al DropDownList
        /// </summary>
        [Category("AgroPET - Presentación")]
        public string EstiloValidator_HighlightClass
        {
            get { return this.vcerfvEntidadNegocio.HighlightCssClass; }
            set { this.vcerfvEntidadNegocio.HighlightCssClass = value; }
        }

        /// <summary>
        /// Obtiene o establece el ancho "width" del texto para la descripcion
        /// </summary>
        [Category("AgroPET - Presentación")]
        public Unit AnchoTexto
        {
            get { return this.lblDescripcion.Width; }
            set { this.lblDescripcion.Width = value; }
        }

        public int TotalItems
        {
            get { return this.ddlEntidadNegocio.Items.Count; }
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Obtiene una cadena con los parámetros que se van a enviar al SQL dinámico
        /// </summary>
        public string GetParametersString(KeyValuePair<string, string>[] vpo_ParamsValor)
        {
            StringBuilder vls_Resultado = new StringBuilder(string.Empty);

            foreach (KeyValuePair<string, string> vlo_param in vpo_ParamsValor)
            {
                vls_Resultado.Append(vlo_param.Key + SeparadorValor + vlo_param.Value + SeparadorParam);
            }

            return vls_Resultado.ToString();
        }

        public bool HasAttribute(string name)
        {
            bool vlb_Resultado = false;

            try
            {
                if (this.Attributes[name] != null)
                    vlb_Resultado = true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }

            return vlb_Resultado;
        }

        public void LlenarDropDown<T>(T tFiltro) where T : new()
        {
            List<T> lstItems = null;
            CatalogoBR<T> catItems = new CatalogoBR<T>();

            hdnDropDownValue.Value = ValorIdDefault;
            lstItems = catItems.ObtenerListado(tFiltro);

            this.ddlEntidadNegocio.DataSource = lstItems;
            this.ddlEntidadNegocio.DataTextField = "DescripValor";
            this.ddlEntidadNegocio.DataValueField = "IdValor";
            this.ddlEntidadNegocio.DataBind();

            this.ddlEntidadNegocio.Items.Insert(0, new ListItem(TextoIndiceCeroDropDown, ValorIndexDropDownNulo));
        }

        ////public T GetItemByID<T>(T tFiltro) where T : new()
        ////{
        ////    T vlo_Temp = new T();
        ////    List<T> lst_Entidades = null;

        ////    using (var objEntidades = new NegocioControlCatalogos<T>())
        ////    {
        ////        var beFiltro = tFiltro;
        ////        lst_Entidades = objEntidades.ObtenerEntidadesCatalogo(beFiltro);

        ////        if ((lst_Entidades != null) && (lst_Entidades.Count > 0))
        ////        {
        ////            vlo_Temp = lst_Entidades[0];
        ////        }
        ////    }
        ////    return vlo_Temp;
        ////}

        /// <summary>
        /// Limpia la informacion del DropDown, solo deja la opcion Seleccione...
        /// </summary>
        public void LimpiarDatos()
        {
            this.ddlEntidadNegocio.Items.Clear();
            this.hdnDropDownValue.Value = ValorIdDefault;

            //this.ddlEntidadNegocio.Items.Insert(0, new ListItem(TextoIndiceCeroDropDown, ValorIndexDropDownNulo));
            AgregarOpcionDefaultDatos();

            this._idEncontrado = null;
        }

        /// <summary>
        /// Agregar la opción por Default al DropDown (Opción Seleccione...)
        /// </summary>
        public void AgregarOpcionDefaultDatos()
        {
            this.ddlEntidadNegocio.Items.Insert(0, new ListItem(TextoIndiceCeroDropDown, ValorIndexDropDownNulo));
        }

        /// <summary>
        /// Copiar el DataSource de un DropDown a otro, para no tener que cargar N veces la misma entidad a BD
        /// </summary>
        public void CopiarDataSource(wucDropDownEntidadNegocio baseDropDown)
        {
            //Replicamos el DataSource del combo para no tener que ir nuevamente a la BD
            this.ddlEntidadNegocio.DataSource = baseDropDown.ddlEntidadNegocio.DataSource;
            this.ddlEntidadNegocio.DataValueField = wucDropDownEntidadNegocio.NombreCampoValor;
            this.ddlEntidadNegocio.DataTextField = wucDropDownEntidadNegocio.NombreCampoDescripcion;
            this.ddlEntidadNegocio.DataBind();

            AgregarOpcionDefaultDatos();
        }

        /// <summary>
        /// Metodo encargado de Habilitar o deshabilitar el dropdownlist
        /// </summary>
        public void HabilitarDeshabilitarDropDown(bool bHabilitar)
        {
            foreach (ListItem liGeneral in ddlEntidadNegocio.Items)
            {
                liGeneral.Enabled = (liGeneral.Selected ? true : bHabilitar);
            }
        }

        /// <summary>
        /// Metodo para asignar de forma directa la informacion a un Drop Down
        /// </summary>
        /// <param name="DataSource"> DataSource que se asignara al DropDown</param>
        /// <param name="DataText">Nombre del campo que se asigna al DataText del DropDown</param>
        /// <param name="dataValue">Nombre del identificador que se asigna a dataValue del DropDown</param>
        public void AsignaDatosDirecto(object ojDataSource, string sDataText, string sDataValue)
        {
            this.ddlEntidadNegocio.DataSource = ojDataSource;
            this.ddlEntidadNegocio.DataTextField = sDataText;
            this.ddlEntidadNegocio.DataValueField = sDataValue;
            this.DataBind();

            this.ddlEntidadNegocio.Items.Insert(0, new ListItem(TextoIndiceCeroDropDown, ValorIndexDropDownNulo));
        }

        #endregion

        #region Ejecucion de eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this._ejecutarHabilitarDeshabilitarDropDown.HasValue && this._ejecutarHabilitarDeshabilitarDropDown.Value)
                this.HabilitarDeshabilitarDropDown(!this.SoloLectura);
        }

        #endregion
    }
}
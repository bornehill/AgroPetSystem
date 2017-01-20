using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilidades.Controles
{
    /*Requeridas para ControlXmlUtil*/
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    using System.IO;
    using System.Windows.Forms;

    public class ControlXmlUtilEntidad : IDisposable
    {
        #region Campos
        public bool BEstado { get; set; }
        public bool BVisible { get; set; }
        public int TabIndice { get; set; }
        public List<string> OValor { get; set; }
        public string SIndice { get; set; }
        public string SNombre { get; set; }
        public string SPadre { get; set; }
        public string STexto { get; set; }
        public string STipo { get; set; }

        #endregion

        #region ControlXmlUtilEntidad

        #region Constructor

        public ControlXmlUtilEntidad()
        {
            BEstado = false;
            BVisible = false;
            TabIndice = 0;
            OValor = null;
            SIndice = string.Empty;
            SNombre = string.Empty;
            SPadre = string.Empty;
            STexto = string.Empty;
            STipo = string.Empty;
        }

        #endregion

        #endregion

        #region IDisposable Members

        private bool _disposing;

        /// <summary>
        /// Método de IDisposable para desechar la clase.
        /// </summary>
        public void Dispose()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }

        /// <summary>
        /// Método sobrecargado de Dispose que será el que
        /// libera los recursos, controla que solo se ejecute
        /// dicha lógica una vez y evita que el GC tenga que
        /// llamar al destructor de clase.
        /// </summary>
        /// <param name="b"></param>
        protected virtual void Dispose(bool b)
        {
            // Si no se esta destruyendo ya…
            if (!_disposing)
            {
                // Se marca como desechada ó desechandose,
                // de forma que no se puede ejecutar este código
                // dos veces.
                _disposing = true;

                // Se indica al GC que no llame al destructor
                // de esta clase al recolectarla.
                GC.SuppressFinalize(this);

                // … libera los recursos… 
            }
        }

        /// <summary>
        /// Destructor de clase.
        /// En caso de que se olvide “desechar” la clase,
        /// el GC llamará al destructor, que tambén ejecuta la lógica
        /// anterior para liberar los recursos.
        /// </summary>
        ~ControlXmlUtilEntidad()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }

        #endregion
    }

    public class ControlXmlUtil : IDisposable
    {
        #region Campos
        private readonly List<string> _excluirTipos;
        private readonly List<string> _excluirControles;

        #region SoloLectura

        private const string SNombreNodoRaiz = "Controles";
        private const string SNombreNodoElemento = "Control";
        private const string SNombreNodoValores = "Valores";
        private const string SNombreNodoValor = "Valor";
        private const string SNombreRenglon = "Row"; //DataGridViewRow
        private const string SNombreCelda = "Cell"; //DataGridViewCell

        #endregion

        private readonly string _sNombreNodoRaiz = string.Empty;
        private string _sMensaje = string.Empty;
        
        /// <summary>
        /// Mensaje de Error Ocurrido
        /// </summary>
        public string Mensaje
        {
            get { return _sMensaje; }
        }

        #endregion

        #region Constructor

        public ControlXmlUtil()
        {
            _sMensaje = string.Empty;
            _sNombreNodoRaiz=SNombreNodoRaiz;
            
            //_Excluir = new List<string> { typeof(Label).ToString(), typeof(Button).ToString(), typeof(ErrorProvider).ToString() };
            _excluirTipos = new List<string>();

            _excluirControles = new List<string>();
        }       

        #endregion

        #region Metodos

        #region Funciones

        /// <summary>
        /// Convert XmlDocument to String : public static string AsString(this XmlDocument xmlDoc)
        /// http://stackoverflow.com/questions/2407302/convert-xmldocument-to-string
        /// </summary>
        /// <param name="xmlDoc">Documento XML</param>
        /// <returns>Representacion del domcumento XML en texto(cadena/string)</returns>
        public string AsString(XmlDocument xmlDoc)
        {
            string strXmlText = string.Empty;
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            if (xmlDoc != null)
            {
                xmlDoc.WriteTo(tx);
                strXmlText = sw.ToString();
            }

            return strXmlText;
        }

        /// <summary>
        /// Conveirte un Objeto en un XML(Texto/Cadena)
        /// </summary>
        /// <param name="oObject">Objeto a trabajar</param>
        /// <returns>Representacion del Objeto en XML</returns>
        public string ObjectToXml(Object oObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        /// <summary>
        /// Busca un Control segun su nombre(RecursiveFind)
        /// </summary>
        /// <param name="contenedor">Nombre del Objeto(contenedor)</param>
        /// <param name="nombreBusca">Nombre del objeto a buscar</param>
        /// <returns>Objeto Control</returns>
        public Control ObtenerControlPorNombreRecursivamente(Control contenedor, string nombreBusca)
        {
            if (contenedor.Name == nombreBusca)
                return contenedor;

            return (from Control cntl in contenedor.Controls select ObtenerControlPorNombreRecursivamente(cntl, nombreBusca)).FirstOrDefault(resultCntl => resultCntl != null);
            //foreach (Control cntl in contenedor.Controls)
            //{
            //    Control resultCntl = ObtenerControlPorNombreRecursivamente(cntl, nombreBusca);
            //    if (resultCntl != null)
            //        return resultCntl;

            //}
        }

        /// <summary>
        /// Mantenimiento a las propiedades Visible,Enable e inicializa el valro contenido en el control
        /// </summary>
        /// <param name="contenedor">Control Contenedor("Padre")</param>
        /// <param name="controles">Lista de controles a trabajar</param>
        /// <param name="visible">Hace visible el control</param>
        /// <param name="habilitar">Habilita el control</param>
        /// <param name="inicializar">Inicaliza el control</param>
        public void ControlesMantenimiento(Control contenedor, List<string> controles, bool visible, bool habilitar, bool inicializar)
        {
            foreach (string str in controles)
            {
                Control ctrl = ObtenerControlPorNombreRecursivamente(contenedor, str);
                if (ctrl != null)
                {
                    #region TextBox
                    if (ctrl.GetType() == typeof(TextBox))
                    {
                        // Do whatever to the TextBox/((TextBox)ctrl)
                        TextBox txt = ((TextBox)ctrl);
                        txt.Visible = visible;
                        txt.Enabled = habilitar;
                        txt.Text = inicializar ? string.Empty : txt.Text;
                    }
                    #endregion

                    #region RichTextBox
                    if (ctrl.GetType() == typeof(RichTextBox))
                    {
                        // Do whatever to the RichTextBox/((RichTextBox)ctrl)
                        RichTextBox rTxt = ((RichTextBox)ctrl);
                        rTxt.Visible = visible;
                        rTxt.Enabled = habilitar;
                        rTxt.Text = inicializar ? string.Empty : rTxt.Text;
                    }
                    #endregion

                    #region DateTimePicker
                    if (ctrl.GetType() == typeof(DateTimePicker))
                    {
                        // Do whatever to the DateTimePicker/((DateTimePicker)ctrl)
                        DateTimePicker dtp = ((DateTimePicker)ctrl);
                        dtp.Visible = visible;
                        dtp.Enabled = habilitar;
                        dtp.Text = inicializar ? string.Empty : dtp.Text;
                    }
                    #endregion

                    #region CheckedListBox
                    if (ctrl.GetType() == typeof(CheckedListBox))
                    {
                        // Do whatever to the CheckedListBox
                        CheckedListBox chklst = (CheckedListBox)ctrl;
                        chklst.Visible = visible;
                        chklst.Enabled = habilitar;
                        foreach (int idx in chklst.CheckedIndices)
                        {
                            chklst.SetItemCheckState(idx, inicializar ? CheckState.Unchecked : chklst.GetItemCheckState(idx));
                        }
                    }
                    #endregion

                    #region ListBox
                    if (ctrl.GetType() == typeof(ListBox))
                    {
                        // Do whatever to the ListBox
                        ListBox lbx = (ListBox)ctrl;
                        lbx.Visible = visible;
                        lbx.Enabled = habilitar;

                        //foreach (int idx in lbx.SelectedIndices)
                        //{   lbx.SetSelected(idx, Inicializar ? false : lbx.GetSelected(idx));   }

                        lbx.ClearSelected();
                    }
                    #endregion

                    #region ComboBox
                    if (ctrl.GetType() == typeof(ComboBox))
                    {
                        // Do whatever to the ComboBox
                        ComboBox cmb = ((ComboBox)ctrl);
                        cmb.Visible = visible;
                        cmb.Enabled = habilitar;
                        cmb.SelectedIndex = inicializar ? 0 : cmb.SelectedIndex;
                    }
                    #endregion

                    #region Button
                    if (ctrl.GetType() == typeof(Button))
                    {
                        // Do whatever to the Button/((Button)ctrl)
                        Button btn = (Button)ctrl;
                        btn.Visible = visible;
                        btn.Enabled = habilitar;
                    }
                    #endregion

                    #region RadioButton
                    if (ctrl.GetType() == typeof(RadioButton))
                    {
                        // Do whatever to the RadioButton
                        RadioButton rad = (RadioButton)ctrl;
                        rad.Visible = visible;
                        rad.Enabled = habilitar;
                        rad.Checked = !inicializar && rad.Checked;
                    }
                    #endregion

                    #region DataGridView
                    if (ctrl.GetType() == typeof(DataGridView))
                    {
                        // Do whatever to the DataGridView/((DataGridView)ctrl)
                        DataGridView dg = ((DataGridView)ctrl);
                        dg.Visible = visible;
                        dg.Enabled = habilitar;
                    }
                    #endregion

                    #region Panel
                    if (ctrl.GetType() == typeof(Panel))
                    {
                        // Do whatever to the Panel/((Panel)ctrl)
                        Panel pnl = ((Panel)ctrl);
                        pnl.Visible = visible;
                        pnl.Enabled = habilitar;
                    }
                    #endregion

                    #region GroupBox
                    if (ctrl.GetType() == typeof(GroupBox))
                    {
                        // Do whatever to the GroupBox/((GroupBox)ctrl)
                        GroupBox grp = ((GroupBox)ctrl);
                        grp.Visible = visible;
                        grp.Enabled = habilitar;
                    }
                    #endregion
                }
            }
        }

        #endregion

        #region Control -> XML

        /// <summary>
        /// Busqueda de controles dentro de un control especificio(con recursividad)
        /// </summary>
        /// <param name="contenedor">Objeto contenedor principal(Padre/Predecesor)</param>
        /// <param name="sPredecesor">Nombre del objeto padre/predecesor</param>
        /// <param name="controles">Lista de objetos tipo "ControlXmlUtilEntidad"</param>
        /// <param name="seExcluyeTipo">Lista de tipos de controles a exluir</param>
        /// <returns>Lista de objetos encontrados</returns>
        public List<ControlXmlUtilEntidad> ObtenerControles(Control contenedor, string sPredecesor, List<ControlXmlUtilEntidad> controles, List<string> seExcluyeTipo)
        {
            _excluirTipos.AddRange(seExcluyeTipo);

            List<ControlXmlUtilEntidad> controlesObtenidos = ObtenerControles(contenedor, sPredecesor, controles);

            //foreach (string TipoRemover in SeExcluye)
            //{
            //    _Excluir.Remove(TipoRemover);
            //}

            return controlesObtenidos;
        }

        /// <summary>
        /// Busqueda de controles dentro de un control especificio(con recursividad)
        /// </summary>
        /// <param name="contenedor">Objeto contenedor principal(Padre/Predecesor)</param>
        /// <param name="sPredecesor">Nombre del objeto padre/predecesor</param>
        /// <param name="controles">Lista de objetos tipo "ControlXmlUtilEntidad"</param>
        /// <param name="seExcluyeTipo">Lista de tipos de controles a excluir</param>
        /// <param name="seExcluyeControl">Lista de controles a excluir</param>
        /// <returns>Lista de objetos encontrados</returns>
        public List<ControlXmlUtilEntidad> ObtenerControles(Control contenedor, string sPredecesor, List<ControlXmlUtilEntidad> controles, List<string> seExcluyeTipo, List<string> seExcluyeControl)
        {
            _excluirTipos.AddRange(seExcluyeTipo);

            _excluirControles.AddRange(seExcluyeControl);

            List<ControlXmlUtilEntidad> controlesObtenidos = ObtenerControles(contenedor, sPredecesor, controles);

            return controlesObtenidos;
        }

        /// <summary>
        /// Busqueda de controles dentro de un control especificio(con recursividad)
        /// </summary>
        /// <param name="contenedor">Objeto contenedor principal(Padre/Predecesor)</param>
        /// <param name="sPredecesor">Nombre del objeto padre/predecesor</param>
        /// <param name="controles">Lista de objetos tipo "ControlXmlUtilEntidad"</param>
        /// <returns>Lista de objetos encontrados  "ControlXmlUtilEntidad"</returns>
        public List<ControlXmlUtilEntidad> ObtenerControles(Control contenedor, string sPredecesor, List<ControlXmlUtilEntidad> controles)
        {
            _sMensaje = string.Empty;

            foreach (Control ctrl in contenedor.Controls)
            {
                try
                {
                    if ((_excluirTipos.Count(n => n.Equals(ctrl.GetType().ToString())) == 0) &&
                        (_excluirControles.Count(n => n.Equals(ctrl.Name))) == 0)
                    {
                        #region Inicializacion
                        ControlXmlUtilEntidad eControls = new ControlXmlUtilEntidad
                            {
                                BEstado = ctrl.Enabled,
                                BVisible = ctrl.Visible,
                                SIndice = String.Format("{0}.{1}", ctrl.Name, ctrl.TabIndex),
                                SNombre = ctrl.Name,
                                SPadre = sPredecesor,
                                TabIndice = ctrl.TabIndex,
                                STexto = ctrl.Text,
                                STipo = ctrl.GetType().ToString()
                            };
                        //eControls.sIndice = String.Format("{0}.{1}.{2}", sPredecesor, ctrl.Name, ctrl.TabIndex.ToString());
                        //eControls.oValor = new List<System.Object>();
                        #endregion

                        #region Label
                        if (ctrl.GetType() == typeof(Label))
                        {
                            // Do whatever to the Label/((Label)ctrl)
                        }
                        #endregion

                        #region CheckBox
                        else if (ctrl.GetType() == typeof(CheckBox))
                        {
                            // Do whatever to the CheckBox 
                            eControls.OValor = new List<string> {((CheckBox) ctrl).Checked.ToString()};
                        }
                        #endregion

                        #region TextBox
                        else if (ctrl.GetType() == typeof(TextBox))
                        {
                            // Do whatever to the TextBox/((TextBox)ctrl)
                            eControls.OValor = new List<string>();
                            TextBox txt = (TextBox)ctrl;
                            if (txt.Text != string.Empty)
                            {
                                eControls.OValor.Add(txt.Text.ToUpper().TrimStart().TrimEnd());
                            }
                        }
                        #endregion

                        #region RichTextBox
                        else if (ctrl.GetType() == typeof(RichTextBox))
                        {
                            // Do whatever to the RichTextBox/((RichTextBox)ctrl)
                            eControls.OValor = new List<string>();
                            RichTextBox rtxt = (RichTextBox)ctrl;
                            if (rtxt.Text != string.Empty)
                            {                               
                                eControls.OValor.Add(rtxt.Text.ToUpper().TrimStart().TrimEnd());
                            }
                        }
                        #endregion

                        #region DateTimePicker
                        else if (ctrl.GetType() == typeof(DateTimePicker))
                        {
                            // Do whatever to the DateTimePicker/((DateTimePicker)ctrl)
                            DateTimePicker dtp = (DateTimePicker)ctrl;
                            eControls.OValor = new List<string>();
                            if (dtp.ShowCheckBox)
                            {
                                if (dtp.Checked)
                                {   eControls.OValor.Add(dtp.Text);  }
                            }
                            else
                            { eControls.OValor.Add(dtp.Text); }
                        }
                        #endregion

                        #region CheckedListBox
                        else if (ctrl.GetType() == typeof(CheckedListBox))
                        {
                            // Do whatever to the CheckedListBox
                            CheckedListBox chklst = (CheckedListBox)ctrl;
                            eControls.OValor = new List<string>();
                            if(chklst.CheckedItems.Count!=0)
                            {
                                foreach(int idx in chklst.CheckedIndices)
                                {
                                    eControls.OValor.Add(Convert.ToString(idx));
                                }
                            }
                        }
                        #endregion

                        #region ListBox
                        else if (ctrl.GetType() == typeof(ListBox))
                        {
                            // Do whatever to the ListBox
                            ListBox lbx = (ListBox)ctrl;
                            eControls.OValor = new List<string>();
                            if (lbx.SelectedItems.Count != 0)
                            {
                                if (lbx.SelectedItems[0].GetType() == typeof(System.Data.DataRowView))
                                {
                                    foreach (System.Data.DataRowView dr in lbx.SelectedItems)
                                    {
                                        eControls.OValor.Add(dr.Row[0].ToString().ToUpper().TrimEnd().TrimStart());
                                    }
                                }

                                if (lbx.SelectedItems[0] is string)
                                {
                                    foreach (string str in lbx.SelectedItems)
                                    {
                                        eControls.OValor.Add(str);
                                    }
                                }
                            }                           
                        }
                        #endregion

                        #region ComboBox
                        else if (ctrl.GetType() == typeof(ComboBox))
                        {
                            // Do whatever to the ComboBox                            
                            ComboBox cmb = (ComboBox)ctrl;
                            eControls.OValor = new List<string>();
                            if (cmb.SelectedIndex != 0)
                            {                                
                                eControls.OValor.Add(cmb.Text);
                            }
                        }
                        #endregion

                        #region Button
                        else if (ctrl.GetType() == typeof(Button))
                        {
                            // Do whatever to the Button/((Button)ctrl)
                        }
                        #endregion

                        #region RadioButton
                        else if (ctrl.GetType() == typeof(RadioButton))
                        {
                            // Do whatever to the RadioButton
                            eControls.OValor = new List<string> {((RadioButton) ctrl).Checked.ToString()};
                        }
                        #endregion

                        #region DataGridView
                        else if (ctrl.GetType() == typeof(DataGridView))
                        {
                            // Do whatever to the DataGridView/((DataGridView)ctrl)
                            DataGridView dg = ((DataGridView)ctrl);
                            #region Recorrido de Renglones
                            
                            eControls.SIndice = String.Format("{0}.{1}.{2}", sPredecesor, ctrl.Name, ctrl.TabIndex);
                            //eControls.sIndice = String.Format("{0}.{1}", ctrl.Name, ctrl.TabIndex.ToString());

                            eControls.OValor = dg.RowCount != 0 ? new List<string>() : null;

                            foreach (DataGridViewRow dr in dg.Rows)
                            {
                                if (dr.IsNewRow) continue;

                                ControlXmlUtilEntidad eCtrlRow = new ControlXmlUtilEntidad
                                    {
                                        BEstado = dr.Selected,
                                        BVisible = dr.Visible,
                                        SIndice = String.Format("{0}.{1}.{2}", eControls.SIndice, SNombreRenglon, dr.Index),
                                        SNombre = SNombreRenglon,
                                        SPadre = eControls.SIndice,
                                        TabIndice = dr.Index,
                                        STexto = string.Empty,
                                        STipo = dr.GetType().ToString(),
                                        OValor = dr.Cells.Count != 0 ? new List<string>() : null
                                    };

                                //eCtrlRow.sIndice = String.Format("{0}.{1}.{2}", eControls.sIndice, dr.GetType().ToString(), dr.Index);
                                //eCtrlRow.sIndice = String.Format("{0}.{1}", sNombreRenglon, dr.Index);

                                //eCtrlRow.sNombre = dr.GetType().ToString();

                                #region Recorrido de celdas(Columnas)

                                foreach (DataGridViewCell dc in dr.Cells)
                                {
                                    ControlXmlUtilEntidad eCtrlCell = new ControlXmlUtilEntidad
                                        {
                                            BEstado = dc.Selected,
                                            BVisible = dc.Visible,
                                            SIndice =
                                                String.Format("{0}.{1}.{2}", eCtrlRow.SIndice, SNombreCelda, dc.ColumnIndex),
                                            SNombre = dc.OwningColumn.Name,
                                            SPadre = eCtrlRow.SIndice,
                                            TabIndice = dc.ColumnIndex,
                                            STexto = dc.OwningColumn.Name,
                                            STipo = dc.GetType().ToString()
                                        };

                                    //eCtrlCell.sIndice = String.Format("{0}.{1}.{2}", eCtrlRow.sIndice, dc.GetType().ToString(), dc.ColumnIndex.ToString());
                                    //eCtrlCell.sIndice = String.Format("{0}.{1}", sNombreCelda, dc.ColumnIndex.ToString());

                                    if (dc.Value != null)
                                    {
                                        eCtrlCell.OValor = new List<string> {dc.Value.ToString()};
                                    }

                                    //eCtrlRow.oValor.Add(eCtrlCell);
                                    controles.Add(eCtrlCell);

                                    eCtrlCell.Dispose();
                                }

                                #endregion

                                //eControls.oValor.Add(eCtrlRow);
                                controles.Add(eCtrlRow);

                                eCtrlRow.Dispose();
                            }
                            #endregion
                            //dg = null;
                        }
                        #endregion

                        #region Panel
                        else if (ctrl.GetType() == typeof(Panel))
                        {
                            // Do whatever to the Panel/((Panel)ctrl)
                        }
                        #endregion

                        #region GroupBox
                        else if (ctrl.GetType() == typeof(GroupBox))
                        {
                            // Do whatever to the GroupBox/((GroupBox)ctrl)
                        }
                        #endregion                      

                        /*Excluye controles HScrollBar y VScrollBar */
                        if ((ctrl.GetType() != typeof(HScrollBar)) && ctrl.GetType() != typeof(VScrollBar))
                        {
                            /*Llena lista con los controles*/
                            controles.Add(eControls);
                        }

                        #region Llamada Recursiva
                        if (ctrl.HasChildren)
                        {
                            ObtenerControles(ctrl, eControls.SIndice, controles);
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    _sMensaje = string.Format("{0} - {1}", ex.Message, ctrl.Name);
                    return null;
                }
            }

            return controles;
        }

        /// <summary>
        /// Construye docuemnto XML en base a la lista de controles especificada(Lista de tipo ControlXmlUtilEntidad)
        /// </summary>
        /// <param name="sLlave">Indice del nodo raiz</param>
        /// <param name="controles">Lista de controles a trabajar</param>
        /// <param name="bGuardarArchivo">True:Se genera un archivo de manera digital(Archivo)</param>
        /// <param name="sNombreArchivo">Nombre del archivo XML(ruta completa)</param>
        /// <returns>Documeto XML basado en los controles de la lista</returns>
        public XmlDocument InsertarNodosBaseXml(string sLlave, List<ControlXmlUtilEntidad> controles, bool bGuardarArchivo, string sNombreArchivo)
        {
            _sMensaje = string.Empty;
            XmlDocument docXml = new XmlDocument();
            XmlElement raiz = docXml.CreateElement(_sNombreNodoRaiz);

            try
            {
                docXml.AppendChild(InsertarNodosHojasXml(sLlave, docXml, raiz, raiz, controles));

                if (bGuardarArchivo)
                {
                    if (!string.IsNullOrEmpty(sNombreArchivo))
                    {
                        docXml.Save(sNombreArchivo);
                    }
                }

                return docXml;
            }
            catch (Exception ex)
            {
                _sMensaje = string.Format("{0}",ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Inserta los nodos internos del documento XML
        /// </summary>
        /// <param name="sLlave">Indice del predecesor</param>
        /// <param name="docXml">Docuemento XML</param>
        /// <param name="raiz">Indice del nodo raiz</param>
        /// <param name="control">Control a trabajar</param>
        /// <param name="controles">Lista de controles a trabajar(ControlXmlUtilEntidad)</param>
        /// <returns></returns>
        private XmlElement InsertarNodosHojasXml(string sLlave, XmlDocument docXml, XmlElement raiz, XmlElement control, IEnumerable<ControlXmlUtilEntidad> controles)
        {
            _sMensaje = string.Empty;
            List<ControlXmlUtilEntidad> hijos = (from h in controles where ((h.SPadre.Equals(sLlave)) && (!string.IsNullOrEmpty(h.SIndice))) select h).ToList();

            foreach (ControlXmlUtilEntidad hijo in hijos)
            {
                try
                {
                    //XmlElement elemento = DocXml.CreateElement(hijo.sNombre);
                    XmlElement elemento = docXml.CreateElement(SNombreNodoElemento);

                    #region Atributos

                    elemento.SetAttribute("Estado", hijo.BEstado.ToString());
                    elemento.SetAttribute("Indice", hijo.SIndice);
                    elemento.SetAttribute("Nombre", hijo.SNombre);
                    elemento.SetAttribute("Padre", hijo.SPadre);
                    elemento.SetAttribute("TabIndice", Convert.ToString(hijo.TabIndice));
                    elemento.SetAttribute("Texto", hijo.STexto);
                    elemento.SetAttribute("Tipo", hijo.STipo);
                    elemento.SetAttribute("Visible", hijo.BVisible.ToString());

                    #region Atributo Valor
                    if (hijo.OValor != null)
                    {
                        XmlElement valores = docXml.CreateElement(SNombreNodoValores);

                        //int idx = 0;
                        foreach (var obj in hijo.OValor)
                        {
                            //XmlElement valor = DocXml.CreateElement(String.Format("{0}.{1}", "Valor", idx));
                            XmlElement valor = docXml.CreateElement(SNombreNodoValor);
                            //valor.SetAttribute("Id", obj.ToString());
                            valor.InnerText = obj;

                            valores.AppendChild(valor);

                            //elemento.AppendChild(valor);
                            //idx++;                        
                        }
                        elemento.AppendChild(valores);
                    }
                    #endregion

                    #endregion

                    if (control.ParentNode == null)
                    {
                        raiz.AppendChild(elemento);
                    }
                    else
                    {
                        control.AppendChild(elemento);
                    }

                    InsertarNodosHojasXml(hijo.SIndice, docXml, raiz, elemento, controles);
                }
                catch (Exception ex)
                {
                    _sMensaje = string.Format("{0}",ex.Message);
                    return null;
                }
            }

            return raiz;
        }

        #endregion

        #region Control <- XML

        /// <summary>
        /// Busqueda de controles en un documento XML(Controles/Control/Valores/Valor) 
        /// </summary>
        /// <param name="docXml">Documento XML</param>
        /// <returns>Lista de controles(EntidadesControles)</returns>
        public List<ControlXmlUtilEntidad> ObtenerControles(XDocument docXml)
        {
            _sMensaje = string.Empty;

            List<ControlXmlUtilEntidad> controles = null;
            try
            {
                if (docXml != null)
                {
                    controles = (from ctrl in docXml.Descendants("Control")
                                 select new ControlXmlUtilEntidad
                                 {
                                     BEstado = Convert.ToBoolean(ctrl.Attribute("Estado").Value),
                                     BVisible = Convert.ToBoolean(ctrl.Attribute("Visible").Value),
                                     TabIndice = Convert.ToInt32(ctrl.Attribute("TabIndice").Value),
                                     SIndice = ctrl.Attribute("Indice").Value,
                                     SNombre = ctrl.Attribute("Nombre").Value,
                                     SPadre = ctrl.Attribute("Padre").Value,
                                     STexto = ctrl.Attribute("Texto").Value,
                                     STipo = ctrl.Attribute("Tipo").Value,

                                     OValor = (from vls in ctrl.Descendants("Valor")
                                               select
                                                   //{
                                                   //Id = vls.Attribute("Id").Value,
                                                   //valor = vls.Value.ToString()
                                                  vls.Value
                                         //}
                                                     ).ToList()
                                 }).ToList();
                }
            }
            catch (Exception ex)
            {
                _sMensaje = string.Format("{0}", ex.Message);
                return null;
            }

            return controles;
        }

        /// <summary>
        /// Asigna al control los valores contenidos en el XML correspondiente
        /// </summary>
        /// <param name="contenedor">Control en donde se realizara la busquea del Objeto(control) deseado</param>
        /// <param name="control">Control a buscar</param>
        /// <param name="controles">Lista de controles recuperados(ControlXmlUtilEntidad)</param>
        /// <returns>True:se realizo la asignacion de valores exitosamente</returns>
        public bool LLenarControl(Control contenedor, ControlXmlUtilEntidad control, List<ControlXmlUtilEntidad> controles)
        {
            Control frmControl = ObtenerControlPorNombreRecursivamente(contenedor, control.SNombre);

            if ((frmControl != null) && (control.STipo.Equals(frmControl.GetType().ToString())))
            {
                try
                {
                    #region Llenado de Controles

                    #region Label
                    if (frmControl.GetType() == typeof(Label))
                    {
                        // Do whatever to the Label/((Label)ctrl)
                    }
                    #endregion

                    #region CheckBox
                    if (frmControl.GetType() == typeof(CheckBox))
                    {
                        // Do whatever to the CheckBox(CheckState.Checked : CheckState.Unchecked)
                        CheckBox cbx = ((CheckBox)frmControl);
                        foreach (string valor in control.OValor)
                        {
                            cbx.Checked = Convert.ToBoolean(valor);
                        }
                    }
                    #endregion

                    #region TextBox
                    if (control.STipo.Equals(typeof(TextBox).ToString()))
                    {
                        //// Do whatever to the TextBox/((TextBox)ctrl)
                        TextBox txt = ((TextBox)frmControl);
                        foreach (string valor in control.OValor)
                        {
                            txt.Text = !string.IsNullOrEmpty(valor) ? valor : string.Empty;
                        }
                    }
                    #endregion

                    #region RichTextBox
                    if (control.STipo.Equals(typeof(RichTextBox).ToString()))
                    {
                        //// Do whatever to the RichTextBox/((RichTextBox)ctrl)
                        RichTextBox rtxt = ((RichTextBox)frmControl);
                        foreach (string valor in control.OValor)
                        {
                            rtxt.Text = valor;
                        }
                    }
                    #endregion

                    #region DateTimePicker
                    if (control.STipo.Equals(typeof(DateTimePicker).ToString()))
                    {
                        //// Do whatever to the DateTimePicker/((DateTimePicker)ctrl)
                        DateTimePicker dtp = ((DateTimePicker)frmControl);
                        foreach (string valor in control.OValor)
                        {
                            if (!string.IsNullOrEmpty(valor))
                            {
                                dtp.Checked = true;
                                dtp.Text = valor;
                                dtp.Value = Convert.ToDateTime(valor);
                            }
                            else
                            {
                                dtp.Text = string.Empty;
                            }
                            
                        }
                    }
                    #endregion

                    #region CheckedListBox
                    if (control.STipo.Equals(typeof(CheckedListBox).ToString()))
                    {
                        //// Do whatever to the CheckedListBox(CheckState.Checked : CheckState.Unchecked)
                        CheckedListBox clbx = (CheckedListBox)frmControl;
                        foreach (string valor in control.OValor)
                        {
                            clbx.SetItemCheckState(clbx.FindString(valor), CheckState.Checked);
                        }
                    }
                    #endregion

                    #region ListBox
                    if (control.STipo.Equals(typeof(ListBox).ToString()))
                    {
                        //// Do whatever to the ListBox                   
                        ListBox lbx = (ListBox)frmControl;
                        if (control.OValor.Count != 0)
                        {
                            if (lbx.Items.Count != 0)
                            {
                                #region DataRowView
                                //if (lbx.SelectedItems[0].GetType() == typeof(System.Data.DataRowView))
                                if (lbx.Items[0].GetType() == typeof(System.Data.DataRowView))
                                {
                                    foreach (string valor in control.OValor)
                                    {
                                        for (int i = 0; i <= lbx.Items.Count; i++)
                                        {
                                            System.Data.DataRowView dr = (System.Data.DataRowView)lbx.Items[i];
                                            if (dr.Row[0].ToString().ToUpper() == valor)
                                            {
                                                lbx.SetSelected(i, true);
                                                break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region string
                                //if (lbx.SelectedItems[0].GetType() == typeof(string))
                                if (lbx.Items[0] is string)
                                {
                                    foreach (string valor in control.OValor)
                                    {
                                        for (int i = 0; i <= lbx.Items.Count; i++)
                                        {
                                            string sItem = lbx.Items[i].ToString().ToUpper();
                                            if (valor.Equals(sItem))
                                            {
                                                lbx.SetSelected(i, true);
                                                break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                    #endregion

                    #region ComboBox
                    if (control.STipo.Equals(typeof(ComboBox).ToString()))
                    {
                        //// Do whatever to the ComboBox  
                        ComboBox cmb = (ComboBox)frmControl;
                        foreach (string valor in control.OValor)
                        {
                            cmb.SelectedIndex = cmb.FindString(valor);
                        }
                    }
                    #endregion
                   
                    #region Button
                    if (control.STipo.Equals(typeof(Button).ToString()))
                    {
                        // Do whatever to the Button/((Button)ctrl)
                    }
                    #endregion

                    #region RadioButton
                    if (control.STipo.Equals(typeof(RadioButton).ToString()))
                    {
                        //// Do whatever to the RadioButton
                        RadioButton cmb = (RadioButton)frmControl;
                        foreach (string valor in control.OValor)
                        {
                            cmb.Checked = Convert.ToBoolean(valor);
                        }
                    }
                    #endregion

                    #region DataGridView
                    if (control.STipo.Equals(typeof(DataGridView).ToString()))
                    {
                        //// Do whatever to the DataGridView/((DataGridView)ctrl)
                        DataGridView dg = ((DataGridView)frmControl);
                        dg.Rows.Clear();

                        #region Recorrido de Renglones

                        List<ControlXmlUtilEntidad> renglones = (from r in controles where r.SPadre == control.SIndice select r).ToList();

                        foreach (ControlXmlUtilEntidad renglon in renglones)
                        {
                            dg.Rows.Add();

                            #region Recorrido de celdas(Columnas)
                            List<ControlXmlUtilEntidad> celdas = (from c in controles where c.SPadre == renglon.SIndice select c).ToList();

                            #region Agrega Valores a Celdas(Columnas)
                            foreach (ControlXmlUtilEntidad celda in celdas)
                            {
                                foreach (string valor in celda.OValor)
                                {
                                    //Valida que el control pueda ser asignado a la celda(valida los tipos de datos)
                                    if (celda.STipo.Equals(dg.Rows[renglon.TabIndice].Cells[celda.TabIndice].GetType().ToString()))
                                    {
                                        #region Tipo de Columnas
                                        //System.Windows.Forms.DataGridViewButtonCell
                                        if (dg.Rows[renglon.TabIndice].Cells[celda.TabIndice].GetType() == typeof(DataGridViewButtonCell))
                                        {
                                            //System.Windows.Forms.DataGridViewButtonCell vBtn = ((System.Windows.Forms.DataGridViewButtonCell)dg.Rows[renglon.iTabIndice].Cells[celda.iTabIndice]);
                                        }

                                        //System.Windows.Forms.DataGridViewCheckBoxCell
                                        if (dg.Rows[renglon.TabIndice].Cells[celda.TabIndice].GetType() == typeof(DataGridViewCheckBoxCell))
                                        {
                                            DataGridViewCheckBoxCell vChk = ((DataGridViewCheckBoxCell)dg.Rows[renglon.TabIndice].Cells[celda.TabIndice]);
                                            vChk.Value = valor;
                                        }

                                        //System.Windows.Forms.DataGridViewComboBoxCell
                                        if (dg.Rows[renglon.TabIndice].Cells[celda.TabIndice].GetType() == typeof(DataGridViewComboBoxCell))
                                        {
                                            DataGridViewComboBoxCell vCmb = ((DataGridViewComboBoxCell)dg.Rows[renglon.TabIndice].Cells[celda.TabIndice]);
                                            vCmb.Value = Convert.ToInt32(valor);
                                        }

                                        //System.Windows.Forms.DataGridViewImageCell
                                        if (dg.Rows[renglon.TabIndice].Cells[celda.TabIndice].GetType() == typeof(DataGridViewImageCell))
                                        {
                                            //System.Windows.Forms.DataGridViewImageCell vImagen = ((System.Windows.Forms.DataGridViewImageCell)dg.Rows[renglon.iTabIndice].Cells[celda.iTabIndice]);
                                        }

                                        //System.Windows.Forms.DataGridViewTextBoxCell
                                        if (dg.Rows[renglon.TabIndice].Cells[celda.TabIndice].GetType() == typeof(DataGridViewTextBoxCell))
                                        {
                                            dg.Rows[renglon.TabIndice].Cells[celda.TabIndice].Value = Convert.ToString(valor);
                                        }

                                        //System.Windows.Forms.DataGridViewLinkCell
                                        if (dg.Rows[renglon.TabIndice].Cells[celda.TabIndice].GetType() == typeof(DataGridViewLinkCell))
                                        {
                                            //System.Windows.Forms.DataGridViewLinkCell vLink = ((System.Windows.Forms.DataGridViewLinkCell)dg.Rows[renglon.iTabIndice].Cells[celda.iTabIndice]);
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            #endregion
                        }
                        #endregion
                    }
                    #endregion

                    #region Panel
                    if (control.STipo.Equals(typeof(Panel).ToString()))
                    {
                        // Do whatever to the Panel/((Panel)ctrl)
                    }
                    #endregion

                    #region GroupBox
                    if (control.STipo.Equals(typeof(GroupBox).ToString()))
                    {
                        // Do whatever to the GroupBox/((GroupBox)ctrl)
                    }
                    #endregion

                    #endregion
                }
                catch (Exception ex)
                {
                    _sMensaje = string.Format("{0} - {1}", ex.Message, frmControl.Name);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #endregion

        #region IDisposable Members

        private bool _disposing;

        /// <summary>
        /// Método de IDisposable para desechar la clase.
        /// </summary>
        public void Dispose()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }

        /// <summary>
        /// Método sobrecargado de Dispose que será el que
        /// libera los recursos, controla que solo se ejecute
        /// dicha lógica una vez y evita que el GC tenga que
        /// llamar al destructor de clase.
        /// </summary>
        /// <param name="b"></param>
        protected virtual void Dispose(bool b)
        {
            // Si no se esta destruyendo ya…
            if (!_disposing)
            {
                // Se marca como desechada ó desechandose,
                // de forma que no se puede ejecutar este código
                // dos veces.
                _disposing = true;

                // Se indica al GC que no llame al destructor
                // de esta clase al recolectarla.
                GC.SuppressFinalize(this);

                // … libera los recursos… 
            }
        }

        /// <summary>
        /// Destructor de clase.
        /// En caso de que se olvide “desechar” la clase,
        /// el GC llamará al destructor, que tambén ejecuta la lógica
        /// anterior para liberar los recursos.
        /// </summary>
        ~ControlXmlUtil()
        {
            // Llamo al método que contiene la lógica
            // para liberar los recursos de esta clase.
            Dispose(true);
        }

        #endregion
    }

}
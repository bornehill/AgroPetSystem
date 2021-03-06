﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgroPET.Entidades.Seguridad;
using AgropPET.Negocio.Seguridad;

using Utilidades.Extensiones;
using AgroPET.Entidades.Consultas;
using AgropPET.Negocio.Catalogos;

namespace AdminAgropet
{
    public partial class frmMenuModuloWeb : Base.BaseCatalogo
    {
        #region Variables y Constantes
        ////Comandos
        private const string ComandoEditar = "Editar";

        private const string cgs_EncabezadoModulo = " Menu Web";
        private const string cgs_ScriptsMensajes = "Mensaje_Seguridad_";
        private const string cgs_MensajeError = "Error al intentar ";

        // Posición de las columnas del grid de menus
        private const int GrdEditar = 0;
        private const int GrdId = 2;
        private const int GrdMenu = 3;
        private const int GrdUrl = 4;
        private const int GrdPadre = 5;
        //private const int GrdActivo = 6;

        //// Posición de las columnas del grid de menus(DataKeys)
        private const int GrdDkId = 0;
        private const int GrdDkPadreId = 1;
        private const int GrdDkActivo = 2;

        private List<int> LstArticulos
        {
            get; set;
            //set { ViewState["lstArticulos"] = value; }
            //get { return (List<int>)ViewState["lstArticulos"]; }
        }

        private List<int> LstArticulosBorrar
        {
            get; set;
            //set { ViewState["lstArticulosBorrar"] = value; }
            //get { return (List<int>)ViewState["lstArticulosBorrar"]; }
        }

        private string valorSeleccionado = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label lblTituloPagina = (Label)this.Master.FindControl("lblTitulo");
                lblTituloPagina.Text = "Módulo de administración de AgroPET";

                if (Session["UsuarioSesion"].IsNull())
                {
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                    return;
                }
            }
        }

        #region VistaBuscar

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        public void Buscar()
        {
            //Se carga la información del grid con los filtros
            string sBuscar = string.IsNullOrEmpty(txt_Menu_Buscar.Value) ? string.Empty : txt_Menu_Buscar.Value;
            grd_Consultas.DataSource = ObtenerMenus(sBuscar);
            grd_Consultas.DataBind();
        }

        /// <summary>
        /// Obtiene una lista de menus filtradas
        /// </summary>
        /// <param name="sBuscar">Nombre del menu</param>
        /// <returns>Lista con la información de los menus</returns>
        private List<EntidadMenuWeb> ObtenerMenus(string sBuscar)
        {
            List<EntidadMenuWeb> lstObjs;
            try
            {
                using (var ObjMenus = new NegocioMenuWeb())
                {
                    if (string.IsNullOrEmpty(sBuscar))
                    {
                        lstObjs = ObjMenus.ObtenerMenus().ToList<EntidadMenuWeb>();
                    }
                    else
                    {
                        lstObjs = ObjMenus.ObtenerMenus(sBuscar).ToList<EntidadMenuWeb>();
                    }
                }
                return lstObjs;
            }
            catch (Exception ex)
            {
                //MostrarMensaje(ex.Message, 0);
                return null;
            }
        }

        protected void grd_Consultas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == ComandoEditar)
                {
                    int iFila = int.Parse(e.CommandArgument.ToString()) % grd_Consultas.PageSize;
                    txt_Comando.Value = Convert.ToString(grd_Consultas.DataKeys[iFila].Values[GrdDkId]);
                    txt_Menu_Editar.Value = grd_Consultas.Rows[iFila].Cells[GrdMenu].Text;
                    txt_MenuUrl_Editar.Value = string.IsNullOrEmpty(grd_Consultas.Rows[iFila].Cells[GrdUrl].Text)
                                                        ? "#" : grd_Consultas.Rows[iFila].Cells[GrdUrl].Text;
                    chk_Estado_Editar.Checked = Convert.ToBoolean(grd_Consultas.DataKeys[iFila].Values[GrdDkActivo]);

                    LlenalListaMenusBuscar(0);
                    ddl_ListaMenus_Editar.SelectedValue = string.IsNullOrEmpty(grd_Consultas.DataKeys[iFila].Values[GrdDkPadreId].ToString())
                                                                        ? "0" : grd_Consultas.DataKeys[iFila].Values[GrdDkPadreId].ToString();

                    valorSeleccionado = txt_Comando.Value;

                    CrearArbol(tvw_Editar, ObtenerMenus(string.Empty));
                    nodoSeleccionado();
                    div_Principal.Visible = false;
                    div_Editar.Visible = true;
                }
            }
            catch (FormatException ex)
            {
                MostrarMensaje(Page, "Menu Web", "Error en el tipo de dato al llenar los campos de edición", 2);
            }
            catch (KeyNotFoundException ex)
            {
                MostrarMensaje(Page, "Menú Web", "Error, llave del grid invalida", 2);
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, "Menú Web", "Error al intentar llenar los campos para la edicion", 2);
            }
        }

        protected void grd_Consultas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_Consultas.PageIndex = e.NewPageIndex;
            Buscar();
        }

        protected void btn_Nuevo_Click(object sender, EventArgs e)
        {
            txt_Comando.Value = string.Empty;

            txt_Menu_Editar.Value = string.Empty;

            txt_MenuUrl_Editar.Value = string.Empty;

            chk_Estado_Editar.Checked = true;

            LlenalListaMenusBuscar(0);

            CrearArbol(tvw_Editar, ObtenerMenus(string.Empty));

            div_Editar.Visible = true;
            div_Principal.Visible = false;
        }

        #endregion VistaBuscar

        #region VistaEditar


        protected void tvw_Editar_SelectedNodeChanged(object sender, EventArgs e)
        {
            nodoSeleccionado();
        }

        //jval
        private void nodoSeleccionado()
        {
            hdnIdSeleccionado.Value = tvw_Editar.SelectedValue;
        }

        #endregion VistaEditar

        #region Eventos



        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Menu_Editar.Value) && !string.IsNullOrEmpty(txt_MenuUrl_Editar.Value) && ddl_ListaMenus_Editar.SelectedIndex != 0)
            {
                // Se verifica que acción se realiza, si es inserción o modificación
                bool bAccion = string.IsNullOrEmpty(txt_Comando.Value);
                InsertarEditar(bAccion);

                LlenalListaMenusBuscar(0);

                Cancelar();

            }
            else
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                 "Es Necesario ingresar todos los datos: ", 2);
            }
        }

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void Cancelar()
        {
            txt_Comando.Value = string.Empty;
            div_Principal.Visible = true;
            div_Editar.Visible = false;

        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnIdSeleccionado != null && !string.IsNullOrEmpty(hdnIdSeleccionado.Value))
                {
                    if (rdGrupoLineaArticulo.Checked)
                        articulosSeleccionados(grdArt);
                    else
                        articulosSeleccionados(grdLibArt);

                    NegocioMenuWeb negMenuWeb = new NegocioMenuWeb();
                    EntidadMenuArticulos entMenArt;


                    foreach (int art in LstArticulos)
                    {
                        entMenArt = new EntidadMenuArticulos();
                        entMenArt.MenuId = Convert.ToInt32(hdnIdSeleccionado.Value);
                        entMenArt.IdArticulo = art;
                       // negMenuWeb.AsignarArticulosMicrosip(entMenArt);
                    }

                    foreach (int artb in LstArticulosBorrar)
                    {
                        entMenArt = new EntidadMenuArticulos();
                        entMenArt.MenuId = Convert.ToInt32(hdnIdSeleccionado.Value);
                        entMenArt.IdArticulo = artb;
                        negMenuWeb.DesAsignarArticulosMicrosip(entMenArt);
                    }

                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                        "Articulos Asignados", 2);
                }
                else
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Debe seleccionar un nodo del menú antes de asignar articulos", 2);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Error al actualizar datos en el Grid: " + ex.Message.Trim(), 2);
            }
        }

        private void articulosSeleccionados(GridView grid)
        {
            LstArticulos = new List<int>(); //.Clear();
            LstArticulosBorrar = new List<int>();//.Clear();
            for (int index = 0; index < grid.Rows.Count; index++)
            {
                CheckBox chk = (CheckBox)grid.Rows[index].Cells[0].FindControl("chkSelItem");

                if (!chk.Checked)
                {
                    LstArticulosBorrar.Add((int)grid.DataKeys[index].Value);
                }
                else
                {
                    LstArticulos.Add((int)grid.DataKeys[index].Value);
                }
            }
        }

        private void SeleccionarTodos(GridView grid, bool check)
        {
            for (int index = 0; index < grid.Rows.Count; index++)
            {
                CheckBox chk = (CheckBox)grid.Rows[index].Cells[0].FindControl("chkSelItem");
                chk.Checked = check;
            }
        }

        protected void chkSelItem_CheckedChanged(object sender, EventArgs e)
        {
            CargarLineasArticulos();
        }

        protected void grdLinArt_DataBound(object sender, EventArgs e)
        {
            for (int index = 0; index < grdLinArt.Rows.Count; index++)
            {
                CheckBox chk = (CheckBox)grdLinArt.Rows[index].Cells[0].FindControl("chkSelItem");
                chk.Checked = true;
            }
        }

        protected void chkSelItem_CheckedChanged1(object sender, EventArgs e)
        {
            CargarArticulos();
        }

        protected void grdArt_DataBound(object sender, EventArgs e)
        {
            for (int index = 0; index < grdArt.Rows.Count; index++)
            {
                CheckBox chk = (CheckBox)grdArt.Rows[index].Cells[0].FindControl("chkSelItem");
                chk.Checked = true;
            }
        }

        protected void grdGposLin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grdGposLin.PageIndex = e.NewPageIndex;
            CargarGruposLineas();
        }

        protected void grdLinArt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grdLinArt.PageIndex = e.NewPageIndex;
            CargarLineasArticulos();
        }

        //protected void chkSelItem_CheckedChanged2(object sender, EventArgs e)
        //{
        //    for (int index = 0; index < grdArt.Rows.Count; index++)
        //    {
        //        CheckBox chk = (CheckBox)grdArt.Rows[index].Cells[0].FindControl("chkSelItem");

        //        if (!chk.Checked)
        //        {
        //            LstArticulosBorrar.Add((int)grdArt.DataKeys[index].Value);
        //        }
        //    }
        //}

        protected void grdArt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grdArt.PageIndex = e.NewPageIndex;
            CargarArticulos();
        }

        protected void rblFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (rblFiltro.SelectedValue == "0")
            if (rdGrupoLineaArticulo.Checked)
            {
                divGpoLinArt.Visible = true;
                divLibresArt.Visible = false;
                grdGposLin.Visible = true;
                CargarGruposLineas();
            }

            else if (rdLibreArticulo.Checked)
            {
                divGpoLinArt.Visible = false;
                divLibresArt.Visible = true;
                CargarLibresArticulos();
            }
        }

        protected void trvLibArt_SelectedNodeChanged(object sender, EventArgs e)
        {
            CargarLibArticulos();
        }
        #endregion

        #region Métodos Locales


        /// <summary>
        /// Llena la lista de menus(Editar)
        /// </summary>
        /// <param name="iBuscar">Identificador del menu</param>
        private void LlenalListaMenusBuscar(int iBuscar)
        {
            ddl_ListaMenus_Editar.ToDropDownList(ObtenerMenus(string.Empty)
                , "MenuId"
                , "Menu"
                , "- Selecciona -", Convert.ToString(iBuscar));
        }

        #region Arbol

        /// <summary>
        /// Carga de menus(opciones(ramas ò predecesores))
        /// </summary>
        /// <param name="lstMenus">Lista de menus</param>
        /// <param name="tncNodos">Coleccion de nodos a trabajar</param>
        public void CargarMenu(List<EntidadMenuWeb> lstMenus, TreeNodeCollection tncNodos)
        {
            List<EntidadMenuWeb> lstMenusPrincipal = (from subNivel in lstMenus where subNivel.Padre == 0 select subNivel).ToList<EntidadMenuWeb>();

            foreach (EntidadMenuWeb obj in lstMenusPrincipal)
            {
                // Validar que el id de menú no exista con anterioridad
                EntidadMenuWeb obj1 = obj;
                var menus = (from TreeNode itm in tncNodos where itm.Value.Equals(Convert.ToString(obj1.MenuId)) select 1);
                if (menus.Any())
                {
                    continue;
                }

                var tn = new TreeNode(obj.Menu, Convert.ToString(obj.MenuId));
                tncNodos.Add(tn);

                //If node has child nodes, then enable on-demand populating
                if (lstMenus.Count > 1)
                {
                    CargarSubMenu(tn, lstMenus);
                }
            }
        }

        /// <summary>
        /// Carga de menus(opciones(hojas ò suscesores))
        /// </summary>
        /// <param name="tnNodoPadre">Nodo a trabajar(Padre)</param>
        /// <param name="lstOpciones">Lista de menus</param>
        public void CargarSubMenu(TreeNode tnNodoPadre, List<EntidadMenuWeb> lstOpciones)
        {
            //// Obtener las subopciones que dependenden de esta misma opcion
            List<EntidadMenuWeb> lstSubOpciones = (from opcion in lstOpciones
                                                   where opcion.Padre == Convert.ToInt32(tnNodoPadre.Value)
                                                   orderby opcion.Orden ascending
                                                   select opcion).ToList<EntidadMenuWeb>();

            foreach (EntidadMenuWeb objSubopcion in lstSubOpciones)
            {
                var tn = new TreeNode(objSubopcion.Menu, Convert.ToString(objSubopcion.MenuId));
                tnNodoPadre.ChildNodes.Add(tn);

                if (tn.Value == valorSeleccionado && !string.IsNullOrEmpty(valorSeleccionado))
                    tn.Selected = true;

                CargarSubMenu(tn, lstOpciones);
            }
        }

        /// <summary>
        /// Crea el arbol(TreeView) segun la lista de objetos(EntidadMenuWeb)
        /// </summary>
        /// <param name="tArbol">Objeto TreeView</param>
        /// <param name="lstObjs">Lista de opciones(EntidadMenuWeb)</param>
        public void CrearArbol(TreeView tArbol, List<EntidadMenuWeb> lstObjs)
        {
            string vMensaje = string.Empty;

            tArbol.Nodes.Clear();
            //if (!string.IsNullOrEmpty(vMensaje))
            //    MostrarMensaje(vMensaje, Convert.ToInt32(Menssage.Aviso));

            CargarMenu(lstObjs, tArbol.Nodes);
            tArbol.ExpandAll();
        }

        #endregion

        /// <summary>
        /// Envía a la base de datos la información de un nuevo(true)/edicion(false) menu
        /// </summary>
        private void InsertarEditar(bool bAccion)
        {
            try
            {
                bool exitoso = false;
                EntidadMenuWeb OMenu = new EntidadMenuWeb();
                OMenu = ObtenerDatos();
                //// Se inserta el registro en la base de datos
                using (var ObjMenus = new NegocioMenuWeb())
                {
                    exitoso = ObjMenus.InsertarEditar(bAccion, OMenu);
                    if (exitoso)
                        MostrarMensaje(Page, "Menu Web", cgs_MensajeOk, 0);
                }

                div_Principal.Visible = true;
                div_Editar.Visible = false;

                btn_Buscar_Click(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, "Menú Web",
                    "Ocurrio un error al intentar guardar la información: " + ex.Message.Trim(), 2);
            }
        }

        /// <summary>
        /// Regresa un objeto con la información capturada del cliente 
        /// </summary>
        /// <returns>Entidad del menu con la información</returns>
        private EntidadMenuWeb ObtenerDatos()
        {
            try
            {
                var oObj = new EntidadMenuWeb
                {
                    MenuId = string.IsNullOrEmpty(txt_Comando.Value) ? 0 : Convert.ToInt32(txt_Comando.Value),
                    Menu = txt_Menu_Editar.Value,
                    MenuUrl = txt_MenuUrl_Editar.Value,
                    Padre = Convert.ToInt32(ddl_ListaMenus_Editar.SelectedItem.Value),
                    Activo = chk_Estado_Editar.Checked,
                    CreacionUsuarioId = GetUserLoggedID(),
                    ModificacionUsuarioId = GetUserLoggedID(),
                    FechaCreacion = new DateTime(1900, 01, 01),
                    FechaModificacion = new DateTime(1900, 01, 01)
                };

                return oObj;
            }
            catch (FormatException ex)
            {
                throw new System.ArgumentException("Error de tipo de dato: ", ex.InnerException);
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException("Error al intentar Obtener los datos del formulario.", ex.InnerException);
            }
        }

        private void CargarGruposLineas()
        {
            List<ConsultaGruposLineas> objGpoLin = new CatalogoGruposLineas().Firebird_ObtenerGruposLineas();
            //CatalogoBR<ConsultaGruposLineas> objGpoLin = new CatalogoBR<ConsultaGruposLineas>();

            grdGposLin.DataSource = objGpoLin; //objGpoLin.ObtenerListaGrid(new ConsultaGruposLineas());
            grdGposLin.DataBind();
        }

        private void CargarLineasArticulos()
        {

            List<ConsultaLineasArticulos> lstLinArt = new List<ConsultaLineasArticulos>();
            CatalogoGruposLineas objLinArt = new CatalogoGruposLineas();

            for (int index = 0; index < grdGposLin.Rows.Count; index++)
            {
                CheckBox chk = (CheckBox)grdGposLin.Rows[index].Cells[0].FindControl("chkSelItem");
                if (chk.Checked)
                {
                    //lstArt.AddRange(objLinArt.ObtenerListado(new ConsultaLineasArticulos { Id_Gpo_Lin = Convert.ToInt32(grdGposLin.DataKeys[index].Value) }));
                    int grupo_linea_id = Convert.ToInt32(grdGposLin.DataKeys[index].Value);
                    lstLinArt.AddRange(objLinArt.Firebird_ObtenerGruposLineasArticulos(grupo_linea_id));

                }
            }

            grdLinArt.DataSource = lstLinArt;
            grdLinArt.DataBind();

            if (lstLinArt.Count <= 0)
            {
                grdArt.DataSource = null;
                grdArt.DataBind();
            }

        }

        private void CargarArticulos()
        {
            List<ConsultaArticulos> lstArt = new List<ConsultaArticulos>();
            CatalogoArticulos objArt = new CatalogoArticulos();

            for (int index = 0; index < grdLinArt.Rows.Count; index++)
            {
                CheckBox chk = (CheckBox)grdLinArt.Rows[index].Cells[0].FindControl("chkSelItem");
                if (chk.Checked)
                {
                    int linea_articulo_id = Convert.ToInt32(grdLinArt.DataKeys[index].Value);
                    lstArt.AddRange(objArt.Firebird_ObtenerArticulos(linea_articulo_id));
                }
            }

            grdArt.DataSource = lstArt;
            grdArt.DataBind();
            //LstArticulos = lstArt.Select(x => (int)x.articulo_id).ToList();
            //LstArticulosBorrar = new List<int>();
        }

        private void CargarLibArticulos()
        {
            string Familia = trvLibArt.SelectedNode.ValuePath.Split('/')[0];
            string Categoria = trvLibArt.SelectedNode.ValuePath.Split('/').Length > 1 ? trvLibArt.SelectedNode.ValuePath.Split('/')[1] : "";
            string Marca = trvLibArt.SelectedNode.ValuePath.Split('/').Length > 2 ? trvLibArt.SelectedNode.ValuePath.Split('/')[2] : "";

            List<ConsultaArticulos> lstArt = new CatalogoLibresArticulos().Firebird_ObtenerLibresArticulos(Familia, Categoria, Marca);

            grdLibArt.DataSource = lstArt;
            grdLibArt.DataBind();
            //LstArticulos = lstArt.Select(x => (int)x.articulo_id).ToList();
            //LstArticulosBorrar = new List<int>();
        }

        private void CargarLibresArticulos()
        {
            List<ConsultaLibresArticulos> lstLibres = new CatalogoLibresArticulos().Firebird_ObtenerLibresArticulos();
            CargaArbolLibresArt(trvLibArt, lstLibres);
        }

        private void CargaArbolLibresArt(TreeView tArbol, List<ConsultaLibresArticulos> lstObjs)
        {
            string vMensaje = string.Empty;

            tArbol.Nodes.Clear();
            //if (!string.IsNullOrEmpty(vMensaje))
            //    MostrarMensaje(vMensaje, Convert.ToInt32(Menssage.Aviso));

            CargarHojas(lstObjs, tArbol.Nodes);
            tArbol.ExpandAll();
        }

        /// <summary>
        /// Carga de menus(opciones(ramas ò predecesores))
        /// </summary>
        /// <param name="lstMenus">Lista de menus</param>
        /// <param name="tncNodos">Coleccion de nodos a trabajar</param>
        public void CargarHojas(List<ConsultaLibresArticulos> lstMenus, TreeNodeCollection tncNodos)
        {
            //List<ConsultaLibresArticulos> lstMenusPrincipal = (from subNivel in lstMenus where subNivel.Familia != string.Empty select subNivel).ToList<ConsultaLibresArticulos>();
            var lstMenusPrincipal = lstMenus.Select(s => s.Familia).Distinct();

            foreach (string obj in lstMenusPrincipal)
            {
                // Validar que el id de menú no exista con anterioridad
                string obj1 = obj;
                var menus = (from TreeNode itm in tncNodos where itm.Value.Equals(Convert.ToString(obj1)) select 1);
                if (menus.Any())
                {
                    continue;
                }

                var tn = new TreeNode(obj, Convert.ToString(obj));
                tncNodos.Add(tn);

                //If node has child nodes, then enable on-demand populating
                if (lstMenus.Count > 1)
                {
                    CargarSubHoja(tn, lstMenus);
                }
            }
        }

        /// <summary>
        /// Carga de menus(opciones(hojas ò suscesores))
        /// </summary>
        /// <param name="tnNodoPadre">Nodo a trabajar(Padre)</param>
        /// <param name="lstOpciones">Lista de menus</param>
        public void CargarSubHoja(TreeNode tnNodoPadre, List<ConsultaLibresArticulos> lstOpciones)
        {
            //// Obtener las subopciones que dependenden de esta misma opcion
            List<ConsultaLibresArticulos> lstSubOpciones = (from opcion in lstOpciones
                                                            where opcion.Familia == tnNodoPadre.Value
                                                            orderby opcion.Familia ascending
                                                            select opcion).ToList<ConsultaLibresArticulos>();

            var lstSM = lstOpciones.Where(s => s.Familia == tnNodoPadre.Value).Select(s => s.Categoria).Distinct();

            foreach (string objSubopcion in lstSM)
            {
                var tn = new TreeNode(objSubopcion, Convert.ToString(objSubopcion));
                tnNodoPadre.ChildNodes.Add(tn);


                //// Obtener las subopciones que dependenden de esta misma opcion
                List<ConsultaLibresArticulos> lstSubOpc = (from opcion in lstSubOpciones
                                                           where opcion.Marca == tn.Value
                                                           orderby opcion.Familia ascending
                                                           select opcion).ToList<ConsultaLibresArticulos>();

                var lstSMH = lstOpciones.Where(s => s.Familia == tnNodoPadre.Value && s.Categoria == tn.Value).Select(s => s.Marca).Distinct();

                foreach (string objSubopc in lstSMH)
                {
                    var tn1 = new TreeNode(objSubopc, Convert.ToString(objSubopc));
                    tn.ChildNodes.Add(tn1);
                }
            }
        }


        #endregion

        protected void chkSelTodoHdr_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chktodos = (CheckBox)grdLibArt.HeaderRow.FindControl("chkSelTodoHdr");
            SeleccionarTodos(grdLibArt, chktodos.Checked);
        }

        protected void chkGposLin_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chktodos = (CheckBox)grdGposLin.HeaderRow.FindControl("chkGposLin");
            SeleccionarTodos(grdGposLin, chktodos.Checked);
            CargarLineasArticulos();
        }

        protected void chkLinArt_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chktodos = (CheckBox)grdLinArt.HeaderRow.FindControl("chkLinArt");
            SeleccionarTodos(grdLinArt, chktodos.Checked);
            CargarArticulos();
        }

        protected void chkArt_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chktodos = (CheckBox)grdArt.HeaderRow.FindControl("chkArt");
            SeleccionarTodos(grdArt, chktodos.Checked);
        }
    }
}
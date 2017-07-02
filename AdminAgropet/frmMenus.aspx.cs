using Agropet.Entidades.Consultas;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Seguridad;
using AgropPET.Negocio.Catalogos;
using AgropPET.Negocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace AdminAgropet
{
    public partial class frmMenus : Base.BaseCatalogo
    {
        #region Variables y Constantes
        private string valorSeleccionado = string.Empty;
        private const string cgs_EncabezadoModulo = " Menu Web";

        private List<int> LstArticulos { get; set; }
        private List<int> LstArticulosBorrar { get; set; }
        private List<int> LstArticulosExcluidos { get; set; }

        private static bool bAccion;
        public static bool BAccion
        {
            get { return bAccion; }
            set { bAccion = value; }
        }

       
        //////Comandos
        //private const string ComandoEditar = "Editar";


        //private const string cgs_ScriptsMensajes = "Mensaje_Seguridad_";
        //private const string cgs_MensajeError = "Error al intentar ";

        //// Posición de las columnas del grid de menus
        //private const int GrdEditar = 0;
        //private const int GrdId = 2;
        //private const int GrdMenu = 3;
        //private const int GrdUrl = 4;
        //private const int GrdPadre = 5;
        ////private const int GrdActivo = 6;

        ////// Posición de las columnas del grid de menus(DataKeys)
        //private const int GrdDkId = 0;
        //private const int GrdDkPadreId = 1;
        //private const int GrdDkActivo = 2;




        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label lblTituloPagina = (Label)this.Master.FindControl("lblTitulo");
                lblTituloPagina.Text = "Módulo de administración de AgroPET";

                if (Session["UsuarioSesion"] == null)
                {
                    Session.Abandon();
                    FormsAuthentication.SignOut();
                    FormsAuthentication.RedirectToLoginPage();
                    return;
                }

                mvPrincipal.SetActiveView(vwConsulta);
                Buscar();
            }
        }

        #region Consulta

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        public void Buscar()
        {
            //Se carga la información del grid con los filtros
            string sBuscar = string.IsNullOrEmpty(txt_Menu_Buscar.Value) ? string.Empty : txt_Menu_Buscar.Value;
            List<EntidadMenuWeb> lstMenu = ObtenerMenus(sBuscar);
            grd_Consultas.DataSource = lstMenu;
            grd_Consultas.DataBind();

            if (string.IsNullOrEmpty(sBuscar))
                CrearArbol(tvwConsulta, lstMenu);
            
        }

        protected void btn_Nuevo_Click(object sender, EventArgs e)
        {
            hdnIdSeleccionado.Value = string.Empty;
            txt_Menu_Editar.Value = string.Empty;
            txt_MenuUrl_Editar.Value = string.Empty;
            chk_Estado_Editar.Checked = true;

            LlenalListaMenusBuscar(0);

            CrearArbol(tvw_Editar, ObtenerMenus(string.Empty));

            mvPrincipal.SetActiveView(vwAltasModif);
        }

        private void LlenalListaMenusBuscar(int iBuscar)
        {
            LlenarDropDown(ddl_ListaMenus_Editar, ObtenerMenus(string.Empty), "Menu", "MenuId", PrimerElemento.Selecciona);
            //ddl_ListaMenus_Editar.ToDropDownList(ObtenerMenus(string.Empty)
            //    , "MenuId"
            //    , "Menu"
            //    , "- Selecciona -", Convert.ToString(iBuscar));
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

        }

        protected void grd_Consultas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grd_Consultas.PageIndex = e.NewPageIndex;
            Buscar();
        }

        #endregion Consulta

        #region Editar

        protected void tvw_Editar_SelectedNodeChanged(object sender, EventArgs e)
        {
            nodoSeleccionado();

            //if (!string.IsNullOrEmpty(hdnIdSeleccionado.Value))
            //{
                EntidadMenuWeb lst = new NegocioMenuWeb().ObtenerMenus(Convert.ToInt32(hdnIdSeleccionado.Value)).FirstOrDefault();
                txt_Menu_Editar.Value = lst.Menu;
                txt_MenuUrl_Editar.Value = lst.MenuUrl;
                chk_Estado_Editar.Checked = lst.Activo;
                if (lst.Padre.ToString() != "0")
                    ddl_ListaMenus_Editar.SelectedValue = lst.Padre.ToString();
                else
                    ddl_ListaMenus_Editar.ClearSelection();
            //}
        }

        private void nodoSeleccionado()
        {
            //if(string.IsNullOrEmpty(hdnIdSeleccionado.Value) || hdnIdSeleccionado.Value != tvw_Editar.SelectedValue)
                hdnIdSeleccionado.Value = tvw_Editar.SelectedValue;
            //else
            //{
            //    hdnIdSeleccionado.Value = null;
            //    if (tvw_Editar.SelectedNode != null)
            //        tvw_Editar.SelectedNode.Selected = false;
            //}
        }

        protected void btn_NuevoEditar_ServerClick(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void Nuevo()
        {
            txt_Menu_Editar.Value = string.Empty;
            txt_MenuUrl_Editar.Value = string.Empty;
            chk_Estado_Editar.Checked = true;
            ddl_ListaMenus_Editar.ClearSelection();

            BAccion = true;

            txt_Menu_Editar.Focus();
            //hdnIdSeleccionado.Value = null;
            //if(tvw_Editar.SelectedNode != null)
            //    tvw_Editar.SelectedNode.Selected = false;
        }

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void Cancelar()
        {
            Buscar();
            mvPrincipal.SetActiveView(vwConsulta);
        }

        protected void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hdnIdSeleccionado.Value))
            {
                int idMenu = Convert.ToInt32(hdnIdSeleccionado.Value);
                new NegocioMenuWeb().EliminarMenu(idMenu);

                hdnIdSeleccionado.Value = null;
                CrearArbol(tvw_Editar, ObtenerMenus(string.Empty));
                txt_Menu_Editar.Value = string.Empty;
                chk_Estado_Editar.Checked = true;

                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")), "Menu Eliminado", 3);
            }
        }

        protected void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Menu_Editar.Value)) 
            {
                InsertarEditar(BAccion);

                LlenalListaMenusBuscar(0);

                //valorSeleccionado = hdnIdSeleccionado.Value;

                CrearArbol(tvw_Editar, ObtenerMenus(string.Empty));

                txt_Menu_Editar.Value = string.Empty;
                chk_Estado_Editar.Checked = true;
                BAccion = false;
            }
            else
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                 "Es Necesario ingresar todos los datos: ", 2);
            }
        }

        /// <summary>
        /// Envía a la base de datos la información de un nuevo(true)/edicion(false) menu
        /// </summary>
        private void InsertarEditar(bool BAccion)
        {
            try
            {
                EntidadMenuWeb OMenu = new EntidadMenuWeb();
                OMenu.Menu = txt_Menu_Editar.Value;
                OMenu.MenuUrl = txt_MenuUrl_Editar.Value;                  
                OMenu.Activo = chk_Estado_Editar.Checked;
                OMenu.CreacionUsuarioId = GetUserLoggedID();
                OMenu.ModificacionUsuarioId = GetUserLoggedID();
                OMenu.FechaCreacion = new DateTime(1900, 01, 01);
                OMenu.FechaModificacion = new DateTime(1900, 01, 01);
                OMenu.MenuId = string.IsNullOrEmpty(hdnIdSeleccionado.Value) ? 0 : Convert.ToInt32(hdnIdSeleccionado.Value);
                OMenu.Padre = string.IsNullOrEmpty(hdnIdSeleccionado.Value) ? 0 : Convert.ToInt32(hdnIdSeleccionado.Value);

                if (bAccion && string.IsNullOrEmpty(hdnIdSeleccionado.Value) || (bAccion && !string.IsNullOrEmpty(hdnIdSeleccionado.Value)) || (!bAccion && !string.IsNullOrEmpty(hdnIdSeleccionado.Value)))
                {
                    if (new NegocioMenuWeb().InsertarEditar(bAccion, OMenu))
                        MostrarMensaje(Page, "Menu Web", cgs_MensajeOk, 3);
                }

                BAccion = false;
                hdnIdSeleccionado.Value = null;
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, "Menú Web",
                    "Ocurrio un error al intentar guardar la información: " + ex.Message.Trim(), 2);
            }

        }

        //private EntidadMenuWeb ObtenerDatos()
        //{
        //    try
        //    {
        //        var oObj = new EntidadMenuWeb();
                
        //        oObj.Menu = txt_Menu_Editar.Value;
        //        oObj.MenuUrl = txt_MenuUrl_Editar.Value;
        //        if (!string.IsNullOrEmpty(hdnIdSeleccionado.Value))
        //        {
        //            oObj.MenuId = string.IsNullOrEmpty(hdnIdSeleccionado.Value) ? 0 : Convert.ToInt32(hdnIdSeleccionado.Value);
        //            oObj.Padre = Convert.ToInt32(hdnIdSeleccionado.Value);
        //        }
        //        oObj.Activo = chk_Estado_Editar.Checked;
        //        oObj.CreacionUsuarioId = GetUserLoggedID();
        //        oObj.ModificacionUsuarioId = GetUserLoggedID();
        //        oObj.FechaCreacion = new DateTime(1900, 01, 01);
        //        oObj.FechaModificacion = new DateTime(1900, 01, 01);

        //        return oObj;
        //    }
        //    catch (FormatException ex)
        //    {
        //        throw new System.ArgumentException("Error de tipo de dato: ", ex.InnerException);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new System.ArgumentException("Error al intentar Obtener los datos del formulario.", ex.InnerException);
        //    }
        //}

        #endregion Editar

        #region Asignar

        protected void btnAsignaMenu_ServerClick(object sender, EventArgs e)
        {
            mvPrincipal.SetActiveView(vwAsignar);
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

                    if (!divLibresArt.Visible)
                    {
                        entMenArt = new EntidadMenuArticulos();
                        entMenArt.MenuId = Convert.ToInt32(hdnIdSeleccionado.Value);
                        negMenuWeb.DesAsignarArticulosMicrosip(entMenArt);
                    }
                    else
                    {
                        foreach (int artb in LstArticulosBorrar)
                        {
                            entMenArt = new EntidadMenuArticulos();
                            entMenArt.MenuId = Convert.ToInt32(hdnIdSeleccionado.Value);
                            entMenArt.IdArticulo = artb;
                            negMenuWeb.DesAsignarArticulosMicrosip(entMenArt, true);
                        }
                    }

                    foreach (int art in LstArticulos)
                    {
                        entMenArt = new EntidadMenuArticulos();
                        entMenArt.MenuId = Convert.ToInt32(hdnIdSeleccionado.Value);
                        entMenArt.IdArticulo = art;
                        negMenuWeb.AsignarArticulosMicrosip(entMenArt);
                    }


                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                        "Articulos Asignados", 3);
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

        protected void rblFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            limpiarGrids();
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

        private void limpiarGrids()
        {
            //grdGposLin.DataSource = null;
            //grdGposLin.DataBind();
            SeleccionarTodos(grdGposLin, false);

            grdLinArt.DataSource = null;
            grdLinArt.DataBind();

            grdArt.DataSource = null;
            grdArt.DataBind();

            grdLibArt.DataSource = null;
            grdLibArt.DataBind();

            LstArticulosExcluidos = new List<int>();
        }

        private void CargarGruposLineas()
        {
            List<ConsultaGruposLineas> objGpoLin = new CatalogoGruposLineas().Firebird_ObtenerGruposLineas();

            grdGposLin.DataSource = objGpoLin;
            grdGposLin.DataBind();

            CrearArbol(tvwAsignar, ObtenerMenus(string.Empty));
        }

        private void CargarLibresArticulos()
        {
            List<ConsultaLibresArticulos> lstLibres = new CatalogoLibresArticulos().Firebird_ObtenerLibresArticulos();
            CargaArbolLibresArt(trvLibArt, lstLibres);

            CrearArbol(trvMenuLibres, ObtenerMenus(string.Empty));
        }

        protected void tvwAsignar_SelectedNodeChanged(object sender, EventArgs e)
        {
            limpiarGrids();
            hdnIdSeleccionado.Value = tvwAsignar.SelectedValue;
            CargaGridElementoSeleccionado();       
        }

        private void CargaGridElementoSeleccionado()
        {
            List<ConsultaRelacionMenuArticulo> lstRelacion = new List<ConsultaRelacionMenuArticulo>();
            //Consultar Datos del idSeleccionado
            if (!string.IsNullOrEmpty(hdnIdSeleccionado.Value))
                lstRelacion = new NegocioMenuWeb().ConsultaRelacionMenuArticulos(Convert.ToInt32(hdnIdSeleccionado.Value));

            //GRID GRUPOS
            var lstGrupos = lstRelacion.Where(a => a.Activo == true)
                                       .GroupBy(l => l.IdGrupo_Linea)
                                       .Select(grp => grp.First()).ToList();

            for (int index = 0; index < grdGposLin.Rows.Count; index++)
            {
                CheckBox chk = (CheckBox)grdGposLin.Rows[index].Cells[0].FindControl("chkSelItem");
                int idGpoLinea = (int)grdGposLin.DataKeys[index].Value;

                foreach (var item in lstGrupos)
                {
                    if(item.IdGrupo_Linea == idGpoLinea)
                        chk.Checked = item.Activo;
                }
            }


            if (lstGrupos.Count > 0)
            {
                chkSelItem_CheckedChanged(null, null);

                var lstGruposLineas = lstRelacion.GroupBy(l => l.IdLinea_Articulo).Select(grp => grp.First()).ToList();
                if (lstGruposLineas.Count > 0)
                    chkSelItem_CheckedChanged1(null, null);
            }

            //if (lstGrupos.Count > 0)
            //    CargarLineasArticulos();

            ////GRID LINEAS
            ////GRID LINEAS
            //var lstGruposLineas = lstRelacion.GroupBy(l => l.IdLinea_Articulo)
            //                                 .Select(grp => grp.First()).ToList();

            //for (int index = 0; index < grdLinArt.Rows.Count; index++)
            //{
            //    CheckBox chk = (CheckBox)grdLinArt.Rows[index].Cells[0].FindControl("chkSelItem");
            //    int idLinea = (int)grdLinArt.DataKeys[index].Value;

            //    foreach (ConsultaRelacionMenuArticulo item in lstGruposLineas)
            //    {
            //        if (item.IdLinea_Articulo == idLinea)
            //            chk.Checked = item.Activo;
            //    }
            //}

            //if (lstGruposLineas.Count > 0)
            //    CargarArticulos();

            ////GRID ARTICULOS
            //var lsArticulos = lstRelacion.GroupBy(l => l.IdArticulo)
            //                                .Select(grp => grp.First()).ToList();

            //for (int index = 1; index < grdArt.Rows.Count; index++)
            //{
            //    CheckBox chk = (CheckBox)grdArt.Rows[index].Cells[0].FindControl("chkSelItem");
            //    int idArticulo = (int)grdArt.DataKeys[index].Value;

            //    foreach (ConsultaRelacionMenuArticulo item in lsArticulos)
            //    {
            //        if (item.IdArticulo == idArticulo)
            //            chk.Checked = item.Activo;
            //        else
            //            chk.Checked = false;
            //    }
            //}

        }

        protected void grdGposLin_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grdGposLin.PageIndex = e.NewPageIndex;
            CargarGruposLineas();
        }

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

        protected void chkSelItem_CheckedChanged(object sender, EventArgs e)
        {
            CargarLineasArticulos();

            //Marca las lineas de acuerdo al grid de Grupos

            var lstGruposLineas = lstCheck(grdGposLin);
            if (lstGruposLineas.Count > 0)
            {
                List<ConsultaRelacionMenuArticulo> lstRelacion = new List<ConsultaRelacionMenuArticulo>();
                //Consultar Datos del idSeleccionado
                if (!string.IsNullOrEmpty(hdnIdSeleccionado.Value))
                    lstRelacion = new NegocioMenuWeb().ConsultaRelacionMenuArticulos(Convert.ToInt32(hdnIdSeleccionado.Value));

                if (lstRelacion.Count > 0)
                {
                    //var filtro = lstRelacion.Join(lstGruposLineas, x => x.IdGrupo_Linea, y => y, (x, y) => x).ToList();
                    SeleccionarTodos(grdLinArt, false);

                    for (int index = 0; index < grdLinArt.Rows.Count; index++)
                    {
                        CheckBox chk = (CheckBox)grdLinArt.Rows[index].Cells[0].FindControl("chkSelItem");
                        int idLinea = (int)grdLinArt.DataKeys[index].Value;

                        foreach (var item in lstRelacion)
                        {
                            if (item.IdLinea_Articulo == idLinea)
                                chk.Checked = true;
                        }
                    }
                }
            }

            chkSelItem_CheckedChanged1(sender, e);
        }

        private List<int> lstCheck(GridView grid)
        {
            List<int> lstId = new List<int>();
            for (int index = 0; index < grid.Rows.Count; index++)
            {
                CheckBox chk = (CheckBox)grid.Rows[index].Cells[0].FindControl("chkSelItem");
                if (chk.Checked)
                    lstId.Add((int)grid.DataKeys[index].Value);
            }

            return lstId;
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

        protected void chkSelItem_CheckedChanged1(object sender, EventArgs e)
        {
            CargarArticulos();

            //Marca las lineas de acuerdo al grid de Lineas

            var lstLineas = lstCheck(grdLinArt);
            if (lstLineas.Count > 0)
            {
                List<ConsultaRelacionMenuArticulo> lstRelacion = new List<ConsultaRelacionMenuArticulo>();
                //Consultar Datos del idSeleccionado
                if (!string.IsNullOrEmpty(hdnIdSeleccionado.Value))
                    lstRelacion = new NegocioMenuWeb().ConsultaRelacionMenuArticulos(Convert.ToInt32(hdnIdSeleccionado.Value));

                if (lstRelacion.Count > 0)
                {
                    //var filtro = lstRelacion.Join(lstLineas, x => x.IdLinea_Articulo, y => y, (x, y) => x).ToList();

                    SeleccionarTodos(grdArt, false);

                    for (int index = 0; index < grdArt.Rows.Count; index++)
                    {
                        CheckBox chk = (CheckBox)grdArt.Rows[index].Cells[0].FindControl("chkSelItem");
                        int idArt = (int)grdArt.DataKeys[index].Value;

                        foreach (var item in lstRelacion)
                        {
                            if (item.IdArticulo == idArt)
                                chk.Checked = item.Activo;
                        }
                    }
                }
            }
        }

        protected void grdLinArt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grdLinArt.PageIndex = e.NewPageIndex;
            CargarLineasArticulos();
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

        protected void trvMenuLibres_SelectedNodeChanged(object sender, EventArgs e)
        {
            limpiarGrids();
            hdnIdSeleccionado.Value = trvMenuLibres.SelectedValue;
            if (trvLibArt.SelectedNode != null)
                trvLibArt.SelectedNode.Selected = false;
        }

        private void CargaGridLibresElementoSeleccionado()
        {
            //List<ConsultaRelacionMenuArticulo> lstRelacion = new List<ConsultaRelacionMenuArticulo>();
            ////Consultar Datos del idSeleccionado
            //if (!string.IsNullOrEmpty(hdnIdSeleccionado.Value))
            //    lstRelacion = new NegocioMenuWeb().ConsultaRelacionMenuArticulos(Convert.ToInt32(hdnIdSeleccionado.Value));

            ////GRID GRUPOS
            //var lstGrupos = lstRelacion.Where(a => a.Activo == true)
            //                           .GroupBy(l => l.IdGrupo_Linea)
            //                           .Select(grp => grp.First()).ToList();

            //for (int index = 0; index < grdGposLin.Rows.Count; index++)
            //{
            //    CheckBox chk = (CheckBox)grdGposLin.Rows[index].Cells[0].FindControl("chkSelItem");
            //    int idGpoLinea = (int)grdGposLin.DataKeys[index].Value;

            //    foreach (var item in lstGrupos)
            //    {
            //        if (item.IdGrupo_Linea == idGpoLinea)
            //            chk.Checked = item.Activo;
            //    }
            //}


            //if (lstGrupos.Count > 0)
            //{
            //    chkSelItem_CheckedChanged(null, null);

            //    var lstGruposLineas = lstRelacion.GroupBy(l => l.IdLinea_Articulo).Select(grp => grp.First()).ToList();
            //    if (lstGruposLineas.Count > 0)
            //        chkSelItem_CheckedChanged1(null, null);
            //}


        }

        protected void trvLibArt_SelectedNodeChanged(object sender, EventArgs e)
        {
            CargarLibArticulos();

            ////GRID ARTICULOS
            List<ConsultaRelacionMenuArticulo> lstRelacion = new List<ConsultaRelacionMenuArticulo>();
            //Consultar Datos del idSeleccionado
            if (!string.IsNullOrEmpty(hdnIdSeleccionado.Value))
                lstRelacion = new NegocioMenuWeb().ConsultaRelacionMenuArticulos(Convert.ToInt32(hdnIdSeleccionado.Value));

            if (lstRelacion.Count > 0)
            {
                SeleccionarTodos(grdLibArt, false);

                for (int index = 0; index < grdLibArt.Rows.Count; index++)
                {
                    CheckBox chk = (CheckBox)grdLibArt.Rows[index].Cells[0].FindControl("chkSelItem");
                    int idArt = (int)grdLibArt.DataKeys[index].Value;

                    foreach (var item in lstRelacion)
                    {
                        if (item.IdArticulo == idArt)
                            chk.Checked = item.Activo;
                    }
                }
            }
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

        private void SeleccionarTodos(GridView grid, bool check)
        {
            for (int index = 0; index < grid.Rows.Count; index++)
            {
                CheckBox chk = (CheckBox)grid.Rows[index].Cells[0].FindControl("chkSelItem");
                chk.Checked = check;
            }
        }

        protected void grdArt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.grdArt.PageIndex = e.NewPageIndex;
            CargarArticulos();
        }

        private void CargaArbolLibresArt(TreeView tArbol, List<ConsultaLibresArticulos> lstObjs)
        {
            string vMensaje = string.Empty;
            tArbol.Nodes.Clear();
            CargarHojas(lstObjs, tArbol.Nodes);
            tArbol.ExpandAll();
        }

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

        protected void grdArt_DataBound(object sender, EventArgs e)
        {
            for (int index = 0; index < grdArt.Rows.Count; index++)
            {
                CheckBox chk = (CheckBox)grdArt.Rows[index].Cells[0].FindControl("chkSelItem");
                chk.Checked = true;
            }
        }

        #endregion Asignar

        public void CrearArbol(TreeView tArbol, List<EntidadMenuWeb> lstObjs)
        {
            string vMensaje = string.Empty;
            tArbol.Nodes.Clear();
            CargarMenu(lstObjs, tArbol.Nodes);
            tArbol.ExpandAll();
        }

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
    }
}
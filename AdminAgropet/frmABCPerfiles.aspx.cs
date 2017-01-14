using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Agropet.Entidades.Especial;
using Agropet.Entidades.Seguridad;
using Agropet.Entidades.Consultas;
using AgropPET.Negocio.Catalogos;

namespace AdminAgropet
{
    public partial class frmABCPerfiles : Base.BaseCatalogo
    {

        private const string cgs_ScriptsMensajes = "Mensaje_Seguridad_";
        private const string cgs_EncabezadoModulo = " Perfiles";
        private const string cgs_MensajeError = "Error al intentar ";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciaPagina();
            }
        }

        private void IniciaPagina()
        {
            Page.Title = "ABC de Perfiles";
            MasterPage MiMaster = this.Master;
            ((Label)MiMaster.FindControl("lblTitulo")).Text = Page.Title;
            LlenarDrops();
        }

        private void LlenarDrops()
        {
            EntidadDDLUC beFiltro = new EntidadDDLUC
            {
                nOpcionSeleccion = 1,
                IdValor = 0,
                DescripValor = string.Empty
            };

            ddluc_Perfil.LlenarDropDown<EntidadDDLUC>(beFiltro);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            EntidadUsuarioLogeado objUser;

            this.hdnFlag.Value = "1";
            DivPerfilesABC.Visible = true;
            divAlta.Visible = true;
            divCambio.Visible = false;
            this.txtFechaCreacion.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            
            objUser= (EntidadUsuarioLogeado)Session["UsuarioSesion"];
            this.txtUserCreo.Text = objUser.claveusuario;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar_Controles();
            DivPerfilesABC.Visible = false;
        }

        protected void Limpiar_Controles()
        {
            this.txtIdPerfil.Text = string.Empty;
            this.txtPerfil.Text = string.Empty;
            this.txtFechaCreacion.Text = string.Empty;
            this.txtUserCreo.Text = string.Empty;
            this.txtFechaModif.Text = string.Empty;
            this.txtUserModifico.Text = string.Empty;
            this.ddlEstadoABC.SelectedIndex = 0;

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar_Controles();
        }

        protected void Guarda_Alta_Perfil()
        {
            CatalogoBR<EntidadPerfiles> clsCatalogos = new CatalogoBR<EntidadPerfiles>();
            EntidadPerfiles MiPerfil;
            try
            {

                MiPerfil = ObtenInfoForma(true);

                clsCatalogos.Guardar(MiPerfil);

                if (clsCatalogos.ResultadoejecucionSP.Trim().Equals("OK"))
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                            clsCatalogos.MensajeReturnSP.Trim(), 0);
                }
                else
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                            clsCatalogos.MensajeReturnSP.Trim(), 1);
                }
            }

            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Ocurrio un error al intentar guardar la información: " + ex.Message.Trim(), 2);
            }
                
        }

        protected void Guarda_Cambio_Perfil()
        {
            CatalogoBR<EntidadPerfiles> clsCatalogos = new CatalogoBR<EntidadPerfiles>();
            EntidadPerfiles UnPerfil;

            try
            {

                UnPerfil = ObtenInfoForma(false);
                clsCatalogos.Actualizar(UnPerfil);
                if (clsCatalogos.ResultadoejecucionSP.Trim().Equals("OK"))
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                        "Cambio especificado se guardo correctamente.", 0);
                }
                else
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                        "El cambio especificado no se guardo en la Base de datos.", 1);
                }

            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Ocurrio un error al intentar guardar la información: " + ex.Message.Trim(), 2);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.ddlEstadoABC.SelectedValue.Equals("-1"))
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "No se puede guardar la información solicitada, debe especificar Estatus.", 2);
            }
            else
            {
                if (hdnFlag.Value.Trim().Equals("1"))
                    Guarda_Alta_Perfil();
                else
                    Guarda_Cambio_Perfil();
            }
        }

        protected EntidadPerfiles ObtenInfoForma(bool bEsAlta)
        {
            EntidadPerfiles UnPerfil = new EntidadPerfiles();
            EntidadUsuarioLogeado UnUsuario;

            UnUsuario = (EntidadUsuarioLogeado)Session["UsuarioSesion"];

            if (bEsAlta)
            {
                UnPerfil.idperfil = null;
                UnPerfil.nombreperfil = this.txtPerfil.Text.Trim();
                UnPerfil.fechacreacion = this.txtFechaCreacion.Text.Trim();
                UnPerfil.idusuariocreo = UnUsuario.idusuario;
                UnPerfil.fechaultmodif = this.txtFechaModif.Text.Trim();
                UnPerfil.idusuarioultmodif = null;
                UnPerfil.activo = Convert.ToInt64(this.ddlEstadoABC.SelectedValue);
            }
            else
            {
                UnPerfil.idperfil = Convert.ToInt64(this.txtIdPerfil.Text);
                UnPerfil.nombreperfil = this.txtPerfil.Text.Trim();
                UnPerfil.fechacreacion = null;
                UnPerfil.idusuariocreo = null;
                UnPerfil.idusuarioultmodif = UnUsuario.idusuario;
                UnPerfil.fechaultmodif = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                UnPerfil.activo = Convert.ToInt64(this.ddlEstadoABC.SelectedValue);
            }

            return UnPerfil;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            List<ConsultaPerfiles> lstPerfiles;

            divResulBusqueda.Visible = true;
            lstPerfiles = RealizaConsulta();

            this.grdPerfiles.DataSource = lstPerfiles;
            this.grdPerfiles.DataBind();
        }

        private List<ConsultaPerfiles> RealizaConsulta()
        {
            ConsultaPerfiles tFiltro = new ConsultaPerfiles();
            List<ConsultaPerfiles> lstConsultaPerfiles;
            CatalogoBR<ConsultaPerfiles> clsConsultas = new CatalogoBR<ConsultaPerfiles>();
            long nOpcionSel;

            if (this.ddluc_Perfil.IDSeleccionado == null)
            {
                tFiltro.idperfil = 0;
                nOpcionSel = 2;
            }
            else
            {
                tFiltro.idperfil = Convert.ToInt32(this.ddluc_Perfil.IDSeleccionado);
                nOpcionSel = 1;
            }

            tFiltro.activo = Convert.ToInt32(this.SelEstatus.SelectedValue.Trim());

            lstConsultaPerfiles = clsConsultas.ObtenerListado(tFiltro, nOpcionSel);

            return lstConsultaPerfiles;
            
        }

        protected void grdPerfiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton ibtnEditar = (ImageButton)e.Row.FindControl("imgEditItem");
                    ibtnEditar.CommandArgument = e.Row.DataItemIndex.ToString();
                    ibtnEditar.CommandName = "CmdEdit";

                    // Se ajustan las columnas del Grid
                    this.grdPerfiles.Columns[2].ItemStyle.Wrap = false;
                    this.grdPerfiles.Columns[3].ItemStyle.Wrap = false;
                    this.grdPerfiles.Columns[4].ItemStyle.Wrap = false;
                    this.grdPerfiles.Columns[5].ItemStyle.Wrap = false;
                    this.grdPerfiles.Columns[6].ItemStyle.Wrap = false;
                    this.grdPerfiles.Columns[7].ItemStyle.Wrap = false;

                    //////if (this.grdPerfiles.Rows[e.Row.RowIndex].Cells[8].Text.Trim().Equals("1"))
                    //////    this.grdPerfiles.Rows[e.Row.RowIndex].Cells[8].Text = "Activo";
                    //////else
                    //////    this.grdPerfiles.Rows[e.Row.RowIndex].Cells[8].Text = "No Activo";

                    this.grdPerfiles.Columns[8].ItemStyle.Wrap = false;
                }
            }

            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Error al mostrar datos en el Grid: " + ex.Message.Trim(), 2);
            }

        }

        protected void grdPerfiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string sEstatus = string.Empty;
            EntidadUsuarioLogeado Usuario;
            try
            {
                if (e.CommandName.Equals("CmdEdit"))
                {

                    divAlta.Visible = false;
                    divCambio.Visible = true;
                    this.DivPerfilesABC.Visible = true;

                    int nIndice = Convert.ToInt32(e.CommandArgument) - (this.grdPerfiles.PageIndex * this.grdPerfiles.PageSize);

                    this.txtIdPerfil.Text = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[2].Text);
                    this.txtPerfil.Text = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[3].Text);
                    this.txtFechaCreacion.Text = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[4].Text);
                    this.txtUserCreo.Text = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[5].Text);
                    this.txtFechaModif.Text=Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[6].Text);

                    sEstatus = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[8].Text);
                    if (sEstatus.Equals("Activo"))
                        this.ddlEstadoABC.SelectedIndex = 1;
                    else
                        this.ddlEstadoABC.SelectedIndex = 0;

                    Usuario = (EntidadUsuarioLogeado)Session["UsuarioSesion"];
                    this.txtUserModifico.Text = Usuario.claveusuario.Trim();

                    this.txtPerfil.Focus();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Error al mostrar datos en el Grid: " + ex.Message.Trim(), 2);

            }
        }

        protected void grdPerfiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.grdPerfiles.PageIndex = e.NewPageIndex;
                btnBuscar_Click(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Error al actualizar datos en el Grid: " + ex.Message.Trim(), 2);
            }
        }
    }
}
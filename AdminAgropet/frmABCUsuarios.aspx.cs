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
    public partial class ABCUsuarios : Base.BaseCatalogo
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
            Page.Title = "ABC de Usuarios";
            MasterPage MiMaster = this.Master;
            ((Label)MiMaster.FindControl("lblTitulo")).Text = Page.Title;
            LlenarDrops(1);
        }

        private void LlenarDrops(int nCualesDrops)
        {
            EntidadDDLUC beFiltroP = new EntidadDDLUC
            {
                nOpcionSeleccion = 1,
                IdValor = 0,
                DescripValor = string.Empty
            };
            EntidadDDLUC beFiltroUs = new EntidadDDLUC
            {
                nOpcionSeleccion = 10,
                IdValor = 0,
                DescripValor = string.Empty
            };

            if (nCualesDrops.Equals(1))
            {
                ddluc_PerfilBus.LlenarDropDown<EntidadDDLUC>(beFiltroP);
                ddluc_UserBus.LlenarDropDown<EntidadDDLUC>(beFiltroUs);
            }
            else
            {
                ddl_PerfilABC.LlenarDropDown<EntidadDDLUC>(beFiltroUs);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            EntidadUsuarioLogeado objUser;

            this.hdnFlag.Value = "1";
            this.DivUsuariosABC.Visible = true;
            this.divAlta.Visible = true;
            this.divCambio.Visible = false;
            this.txtFechaCreacion.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            LlenarDrops(2);
            objUser = (EntidadUsuarioLogeado)Session["UsuarioSesion"];
            this.txtUsuarioCreo.Text = objUser.claveusuario;
            this.txtNombreUsuario.Focus();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            List<ConsultaUsuarios> lstUsuarios;

            divResulBusqueda.Visible = true;
            lstUsuarios = RealizaConsulta();

            this.grdUsuarios.DataSource = lstUsuarios;
            this.grdUsuarios.DataBind();
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar_Controles();
            DivUsuariosABC.Visible = false;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar_Controles();
            this.txtNombreUsuario.Focus();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlEstadoABC.SelectedValue.Equals("-1"))
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "No se puede guardar la información solicitada, debe especificar el estatus.",0);
            }
            else
            {
                if (hdnFlag.Value.Trim().Equals("1"))
                {
                    Guarda_Alta_Usuario();
                    Limpiar_Controles();
                    this.DivUsuariosABC.Visible = false;
                }
                else
                {
                    Guarda_Cambio_Usuario();
                    this.DivUsuariosABC.Visible = false;
                }

            }
        }

        private void Limpiar_Controles()
        {
            this.txtIdUsuario.Text = string.Empty;
            this.txtNombreUsuario.Text = string.Empty;
            this.txtCveUsuario.Text = string.Empty;
            this.txtPasswordUsr.Text = string.Empty;
            this.txtConfirmPass.Text = string.Empty;
            this.txtUsuarioCreo.Text = string.Empty;
            this.txtFechaCreacion.Text = string.Empty;
            this.txtUserModifico.Text = string.Empty;
            this.txtFechaModif.Text = string.Empty;
            this.ddlEstadoABC.SelectedIndex = 0;
        }

        private List<ConsultaUsuarios> RealizaConsulta()
        {
            ConsultaUsuarios tFiltro = new ConsultaUsuarios();
            List<ConsultaUsuarios> lstConsultaUsuarios;
            CatalogoBR<ConsultaUsuarios> clsConsultas = new CatalogoBR<ConsultaUsuarios>();

            if (this.ddluc_PerfilBus.IDSeleccionado == null)
                tFiltro.IdPerfil = 0;

            if (this.ddluc_UserBus.IDSeleccionado == null)
                tFiltro.IdUsuario = 0;

            tFiltro.Activo = Convert.ToInt32(SelEstatus.SelectedValue.Trim());

            lstConsultaUsuarios = clsConsultas.ObtenerListado(tFiltro);

            return lstConsultaUsuarios;

        }

        private EntidadUsuario ObtenInfoForma(bool bEsAlta)
        {
            EntidadUsuario UnUsuario = new EntidadUsuario();
            EntidadUsuarioLogeado UserLogged;

            UserLogged = (EntidadUsuarioLogeado)Session["UsuarioSesion"];

            if (bEsAlta)
            {
                UnUsuario.idusuario = null;
                UnUsuario.idperfil = Convert.ToInt32(this.ddl_PerfilABC.IDSeleccionado);
                UnUsuario.nombreusr = this.txtNombreUsuario.Text.Trim();
                UnUsuario.claveusr = this.txtCveUsuario.Text.Trim();
                UnUsuario.passwordusr = this.txtConfirmPass.Text.Trim();
                UnUsuario.activo = Convert.ToInt32(this.ddlEstadoABC.SelectedValue);
                UnUsuario.UsuarioCreo = Convert.ToInt32(UserLogged.idusuario);
                UnUsuario.FechaCreacion = this.txtFechaCreacion.Text.Trim();
                UnUsuario.UsuarioModif = null;
                UnUsuario.FechaUltModif = this.txtFechaCreacion.Text.Trim();
            }
            else
            {
                UnUsuario.idusuario = Convert.ToInt32(this.txtIdUsuario.Text);
                UnUsuario.idperfil = Convert.ToInt32(this.ddl_PerfilABC.IDSeleccionado);
                UnUsuario.nombreusr = this.txtNombreUsuario.Text.Trim();
                UnUsuario.claveusr = this.txtCveUsuario.Text.Trim();
                UnUsuario.passwordusr = this.txtConfirmPass.Text.Trim();
                UnUsuario.activo = Convert.ToInt32(this.ddlEstadoABC.SelectedValue);
                UnUsuario.UsuarioCreo = null;
                UnUsuario.FechaCreacion = this.txtFechaCreacion.Text.Trim();
                UnUsuario.UsuarioModif = Convert.ToInt32(UserLogged.idusuario);
                UnUsuario.FechaUltModif = this.txtFechaModif.Text.Trim();
            }

            return UnUsuario;
        }

        private void Guarda_Alta_Usuario()
        {
            CatalogoBR<EntidadUsuario> clsCatalogos = new CatalogoBR<EntidadUsuario>();
            EntidadUsuario UnUsuario;

            try
            {
                UnUsuario = ObtenInfoForma(true);

                clsCatalogos.Guardar(UnUsuario);

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
                    "Ocurrio un error al intentar guardar la información, Error: " + ex.Message.Trim(), 2);
            }
        }

        private void Guarda_Cambio_Usuario()
        {
            CatalogoBR<EntidadUsuario> clsCatalogos = new CatalogoBR<EntidadUsuario>();
            EntidadUsuario UnUsuario;

            try
            {
                UnUsuario = ObtenInfoForma(false);

                clsCatalogos.Actualizar(UnUsuario);

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
                    "Ocurrio un error al intentar guardar la información, Error: " + ex.Message.Trim(), 2);
            }
        }

        protected void grdUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                this.grdUsuarios.PageIndex = e.NewPageIndex;
                btnBuscar_Click(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Error al actualizar datos en el grid, Error: " + ex.Message.Trim(), 2);
            }

        }

        protected void grdUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grdUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ImageButton ibtnEditar = (ImageButton)e.Row.FindControl("imgEditItem");
                    ibtnEditar.CommandArgument = e.Row.DataItemIndex.ToString();
                    ibtnEditar.CommandName = "CmdEdit";

                    //Se ajustan las columnas del grid
                    this.grdUsuarios.Columns[2].ItemStyle.Wrap = false;
                    this.grdUsuarios.Columns[3].ItemStyle.Wrap = false;
                    this.grdUsuarios.Columns[4].ItemStyle.Wrap = false;
                    this.grdUsuarios.Columns[5].ItemStyle.Wrap = false;
                    this.grdUsuarios.Columns[6].ItemStyle.Wrap = false;
                    this.grdUsuarios.Columns[7].ItemStyle.Wrap = false;
                    this.grdUsuarios.Columns[8].ItemStyle.Wrap = false;
                    this.grdUsuarios.Columns[9].ItemStyle.Wrap = false;
                }
            }

            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Error al mostrar datos en el grid, Error: " + ex.Message.Trim(), 2);
            }

        }
    }
}
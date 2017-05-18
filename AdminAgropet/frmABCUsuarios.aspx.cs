using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Seguridad;
using AgroPET.Entidades.Consultas;
using AgropPET.Negocio.Catalogos;
using Agropet.Entidades.CatABCs;

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
                mvwUsuario.SetActiveView(vwConsulta);
                buscar();
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
            if (nCualesDrops.Equals(1))
            {
                List<ConsultaUsuarios> usuarios = new CatalogoUsuario().ObtenerUsuarios(null, null, null);
                List<EntPerfil> perfiles = new CatalogoPerfil().ObtenerPerfiles(null, null);

                LlenarDropDown(ddluc_PerfilBus, perfiles, "nombreperfil", "idPerfil", PrimerElemento.Selecciona);
                LlenarDropDown(ddluc_UserBus, usuarios, "NombreUsr", "IdUsuario", PrimerElemento.Selecciona);
            }
            else
            {
                List<EntPerfil> perfiles = new CatalogoPerfil().ObtenerPerfiles(null, null);
                LlenarDropDown(ddl_PerfilABC, perfiles, "nombreperfil", "idPerfil", PrimerElemento.Selecciona);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            this.hdnFlag.Value = "1";
            mvwUsuario.SetActiveView(vwNuevo);
            lblTituloNuevo.InnerText = "NUEVO USUARIO";
            this.txtFechaCreacion.Value = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            LlenarDrops(2);
            this.txtUsuarioCreo.Value = UsuarioLogeado.claveusuario; 
            this.txtNombreUsuario.Focus();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void buscar()
        {
            List<ConsultaUsuarios> lstUsuarios;
            lstUsuarios = RealizaConsulta();

            this.grdUsuarios.DataSource = lstUsuarios;
            this.grdUsuarios.DataBind();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar_Controles();
            mvwUsuario.SetActiveView(vwConsulta);
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
                    "No se puede guardar la información solicitada, debe especificar el estatus.", 0);
            }
            else
            {
                if (hdnFlag.Value.Trim().Equals("1"))
                {
                    Guarda_Alta_Usuario();
                    //Limpiar_Controles();
                }
                else
                {
                    Guarda_Cambio_Usuario();
                }

                buscar();
            }
        }

        private void Limpiar_Controles()
        {
            this.txtIdUsuario.Value = string.Empty;
            this.txtNombreUsuario.Value = string.Empty;
            this.txtCveUsuario.Value = string.Empty;
            this.txtPasswordUsr.Value = string.Empty;
            this.txtConfirmPass.Value = string.Empty;
            this.txtUsuarioCreo.Value = string.Empty;
            this.txtFechaCreacion.Value = string.Empty;
            this.txtUserModifico.Value = string.Empty;
            this.txtFechaModif.Value = string.Empty;
            this.ddlEstadoABC.SelectedIndex = 0;
        }

        private List<ConsultaUsuarios> RealizaConsulta()
        {
            ConsultaUsuarios tFiltro = new ConsultaUsuarios();

            if (Convert.ToInt32(SelEstatus.SelectedValue.Trim()) != -1)
                tFiltro.Activo = (SelEstatus.SelectedValue == "0") ? false : true; 
            else
                tFiltro.Activo = null;

            if (ddluc_UserBus.SelectedValue.Trim() != "")
                tFiltro.IdUsuario = Convert.ToInt32(ddluc_UserBus.SelectedValue.Trim());
            else
                tFiltro.IdUsuario = null;

            if (ddluc_PerfilBus.SelectedValue.Trim() != "")
                tFiltro.IdPerfil = Convert.ToInt32(ddluc_PerfilBus.SelectedValue.Trim());
            else
                tFiltro.IdPerfil = null;

            List <ConsultaUsuarios> lstUsuarios = new CatalogoUsuario().ObtenerUsuarios(tFiltro.IdPerfil, tFiltro.IdUsuario, tFiltro.Activo);
            return lstUsuarios;
        }

        private EntidadUsuario ObtenInfoForma(bool bEsAlta)
        {
            EntidadUsuario UnUsuario = new EntidadUsuario();

            if (bEsAlta)
            {
                UnUsuario.idusuario = null;
                UnUsuario.idperfil = Convert.ToInt32(this.ddl_PerfilABC.SelectedItem.Value);
                UnUsuario.nombreusr = this.txtNombreUsuario.Value.Trim();
                UnUsuario.claveusr = this.txtCveUsuario.Value.Trim();
                UnUsuario.passwordusr = this.txtConfirmPass.Value.Trim();
                UnUsuario.activo = Convert.ToInt32(this.ddlEstadoABC.SelectedValue);
                UnUsuario.UsuarioCreo = Convert.ToInt32(UsuarioLogeado.idusuario);
                UnUsuario.FechaCreacion = this.txtFechaCreacion.Value.Trim();
                UnUsuario.UsuarioModif = null;
                UnUsuario.FechaUltModif = this.txtFechaCreacion.Value.Trim();
            }
            else
            {
                UnUsuario.idusuario = Convert.ToInt32(this.txtIdUsuario.Value);
                UnUsuario.idperfil = Convert.ToInt32(this.ddl_PerfilABC.SelectedItem.Value);
                UnUsuario.nombreusr = this.txtNombreUsuario.Value.Trim();
                UnUsuario.claveusr = this.txtCveUsuario.Value.Trim();
                UnUsuario.passwordusr = this.txtConfirmPass.Value.Trim();
                UnUsuario.activo = Convert.ToInt32(this.ddlEstadoABC.SelectedValue);
                UnUsuario.UsuarioCreo = null;
                UnUsuario.FechaCreacion = this.txtFechaCreacion.Value.Trim();
                UnUsuario.UsuarioModif = Convert.ToInt32(UsuarioLogeado.idusuario);
                UnUsuario.FechaUltModif = this.txtFechaModif.Value.Trim();
            }

            return UnUsuario;
        }

        private void Guarda_Alta_Usuario()
        {
            if (ddl_PerfilABC.SelectedIndex == 0 
                || string.IsNullOrEmpty(txtNombreUsuario.Value) || string.IsNullOrEmpty(txtCveUsuario.Value)
                || string.IsNullOrEmpty(txtPasswordUsr.Value) || string.IsNullOrEmpty(txtConfirmPass.Value))
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")), "Es necesario Ingresar todos los datos requeridos", 2);
                return;
            }


            try
            {
                ConsultaUsuarios usuario = new ConsultaUsuarios();
                usuario.IdPerfil = Convert.ToInt32(ddl_PerfilABC.SelectedItem.Value);
                usuario.NombreUsr = txtNombreUsuario.Value;
                usuario.ClaveUsr = txtCveUsuario.Value;
                usuario.PasswordUsr = txtPasswordUsr.Value;
                usuario.Activo = (ddlEstadoABC.SelectedItem.Value == "0")?false:true;
                usuario.IdUsuarioCreo = Convert.ToInt32(UsuarioLogeado.idusuario);
             
                int afectados = new CatalogoUsuario().GuardarUsuario(usuario);

                if(afectados>0)
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")), "Guardado con exito", 2);
                    mvwUsuario.SetActiveView(vwConsulta);
                }
                else
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")), 
                        "No es posible Guardar, El nombre de Usuario o clave ya existe", 2);
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Ocurrio un error al intentar guardar la información: " + ex.Message.Trim(), 2);
            }
        }

        private void Guarda_Cambio_Usuario()
        {
            if (ddl_PerfilABC.SelectedIndex == 0
               || string.IsNullOrEmpty(txtNombreUsuario.Value) || string.IsNullOrEmpty(txtCveUsuario.Value)
               || string.IsNullOrEmpty(txtPasswordUsr.Value) || string.IsNullOrEmpty(txtConfirmPass.Value))
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")), "Es necesario Ingresar todos los datos requeridos", 2);
                return;
            }

            try
            {
                ConsultaUsuarios usuario = new ConsultaUsuarios();
                usuario.IdUsuario = Convert.ToInt32(hdnFlag.Value);
                usuario.IdPerfil = Convert.ToInt32(ddl_PerfilABC.SelectedItem.Value);
                usuario.NombreUsr = txtNombreUsuario.Value;
                usuario.ClaveUsr = txtCveUsuario.Value;
                usuario.PasswordUsr = txtPasswordUsr.Value;
                usuario.Activo = (ddlEstadoABC.SelectedItem.Value == "0") ? false : true;
                usuario.IdUsuarioModif = Convert.ToInt32(UsuarioLogeado.idusuario);

                int afectados = new CatalogoUsuario().EditarUsuario(usuario);

                if (afectados > 0)
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")), "Guardado con exito", 2);
                    mvwUsuario.SetActiveView(vwConsulta);
                }
                else
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                        "No es posible Guardar, El Usuario NO existe", 2);
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Ocurrio un error al intentar guardar la información: " + ex.Message.Trim(), 2);
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
            try
            {
                int nFila = int.Parse(e.CommandArgument.ToString()) % grdUsuarios.PageSize;
                if (e.CommandName.Equals("CmdEdit"))
                {
                    LlenarDrops(2);

                    lblTituloNuevo.InnerText = "MODIFICAR PERFIL";
                    hdnFlag.Value = grdUsuarios.DataKeys[nFila].Values[0].ToString();
                    txtNombreUsuario.Value = grdUsuarios.Rows[nFila].Cells[5].Text;
                    txtCveUsuario.Value = grdUsuarios.Rows[nFila].Cells[4].Text;
                    ddl_PerfilABC.SelectedValue = grdUsuarios.DataKeys[nFila].Values[1].ToString();
                    ddlEstadoABC.SelectedValue = (Convert.ToBoolean(grdUsuarios.DataKeys[nFila].Values[2]))?"1":"0";
                    txtUsuarioCreo.Value = grdUsuarios.Rows[nFila].Cells[7].Text;
                    txtFechaCreacion.Value = grdUsuarios.Rows[nFila].Cells[6].Text;
                    txtUserModifico.Value = grdUsuarios.Rows[nFila].Cells[9].Text.Replace("&nbsp;","");
                    txtFechaModif.Value = grdUsuarios.Rows[nFila].Cells[8].Text.Replace("&nbsp;", "");

                    //BEdicion = true;
                    mvwUsuario.SetActiveView(vwNuevo);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Error al mostrar datos en el Grid: " + ex.Message.Trim(), 2);
            }
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
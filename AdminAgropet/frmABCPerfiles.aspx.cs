﻿using System;
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
                mvwPerfiles.SetActiveView(vwConsulta);
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
            List<EntPerfil> perfiles = new CatalogoPerfil().ObtenerPerfiles(null, null);

            LlenarDropDown(ddluc_Perfil, perfiles, "nombreperfil", "idperfil", PrimerElemento.Selecciona);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            EntidadUsuarioLogeado objUser;

            this.hdnFlag.Value = "1";
            mvwPerfiles.SetActiveView(vwNuevo); //DivPerfilesABC.Visible = true;
            lblTituloNuevo.InnerText = "NUEVO PERFIL";
            //divAlta.Visible = true;
            //divCambio.Visible = false;
            this.txtFechaCreacion.Value = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            objUser = (EntidadUsuarioLogeado)Session["UsuarioSesion"];
            this.txtUserCreo.Value = objUser.claveusuario;


        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            CancelarRegresar();
        }

        private void CancelarRegresar()
        {
            Limpiar_Controles();
            Buscar();
            mvwPerfiles.SetActiveView(vwConsulta);
        }

        protected void Limpiar_Controles()
        {
            this.txtIdPerfil.Value = string.Empty;
            this.txtPerfil.Value = string.Empty;
            this.txtFechaCreacion.Value = string.Empty;
            this.txtUserCreo.Value = string.Empty;
            this.txtFechaModif.Value = string.Empty;
            this.txtUserModifico.Value = string.Empty;
            this.ddlEstadoABC.SelectedIndex = 0;

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar_Controles();
        }

        protected void Guarda_Alta_Perfil()
        {
            try
            {
                EntPerfil perfil = new EntPerfil();
                perfil.nombreperfil = txtPerfil.Value;
                perfil.idusuariocreo = Convert.ToInt32(UsuarioLogeado.idusuario);
                perfil.activo = Convert.ToInt32(ddlEstadoABC.SelectedItem.Value);

                new CatalogoPerfil().GuardarPerfil(perfil);
            }

            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Ocurrio un error al intentar guardar la información: " + ex.Message.Trim(), 2);
            }
        }

        protected void Guarda_Cambio_Perfil()
        {
            try
            {
                EntPerfil perfil = new EntPerfil();
                perfil.idperfil = Convert.ToInt32(txtIdPerfil.Value);
                perfil.nombreperfil = txtPerfil.Value;
                perfil.idusuarioultmodif = Convert.ToInt32(UsuarioLogeado.idusuario);
                perfil.activo = Convert.ToInt32(ddlEstadoABC.SelectedItem.Value);

                new CatalogoPerfil().EditarPerfil(perfil);
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

                CancelarRegresar();
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
                UnPerfil.nombreperfil = this.txtPerfil.Value.Trim();
                UnPerfil.fechacreacion = this.txtFechaCreacion.Value.Trim();
                UnPerfil.idusuariocreo = UnUsuario.idusuario;
                UnPerfil.fechaultmodif = this.txtFechaModif.Value.Trim();
                UnPerfil.idusuarioultmodif = null;
                UnPerfil.activo = Convert.ToInt64(this.ddlEstadoABC.SelectedValue);
            }
            else
            {
                UnPerfil.idperfil = Convert.ToInt64(this.txtIdPerfil.Value);
                UnPerfil.nombreperfil = this.txtPerfil.Value.Trim();
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
            Buscar();
        }

        private void Buscar()
        {
            List<ConsultaPerfiles> lstPerfiles = RealizaConsulta();
            this.grdPerfiles.DataSource = lstPerfiles;
            this.grdPerfiles.DataBind();
        }

        private List<ConsultaPerfiles> RealizaConsulta()
        {
            int? idPerfil = null;
            bool? activo = null;

            if (ddluc_Perfil.SelectedItem.Value != "")
                idPerfil = Convert.ToInt32(ddluc_Perfil.SelectedItem.Value);

            if (SelEstatus.SelectedItem.Value != "-1")
                activo = (SelEstatus.SelectedItem.Value == "0") ? false : true;

            List<ConsultaPerfiles> perfiles = new CatalogoPerfil().ObtenerPerfilesGrid(idPerfil, activo);
            return perfiles;
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
                    lblTituloNuevo.InnerText = "MODIFICAR PERFIL";
                    //divAlta.Visible = false;
                    //divCambio.Visible = true;
                    hdnFlag.Value = "0";
                    mvwPerfiles.SetActiveView(vwNuevo);

                    int nIndice = Convert.ToInt32(e.CommandArgument) % grdPerfiles.PageSize;//- (this.grdPerfiles.PageIndex * this.grdPerfiles.PageSize);

                    this.txtIdPerfil.Value = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[2].Text);
                    this.txtPerfil.Value = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[3].Text);
                    this.txtFechaCreacion.Value = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[4].Text);
                    this.txtUserCreo.Value = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[5].Text);
                    this.txtFechaModif.Value = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[6].Text);

                    sEstatus = Server.HtmlDecode(this.grdPerfiles.Rows[nIndice].Cells[8].Text);
                    if (sEstatus.Equals("Activo"))
                        this.ddlEstadoABC.SelectedIndex = 1;
                    else
                        this.ddlEstadoABC.SelectedIndex = 0;

                    Usuario = (EntidadUsuarioLogeado)Session["UsuarioSesion"];
                    this.txtUserModifico.Value = Usuario.claveusuario.Trim();

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
                Buscar();
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Error al actualizar datos en el Grid: " + ex.Message.Trim(), 2);
            }
        }
    }
}
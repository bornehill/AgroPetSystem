using AdminAgropet.Base;
using AgroPET.Entidades.CatABCs;
using AgropPET.Negocio.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminAgropet
{
    public partial class frmABCClientes : BaseCatalogo
    {
        private const string cgs_EncabezadoModulo = " Catalogo Cliente ";
        private static bool _bEdicion;
        public static bool BEdicion
        {
            get { return _bEdicion; }
            set { _bEdicion = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mvwClientes.SetActiveView(vwConsulta);
                //LLenaDropDown();
                Buscar();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            mvwClientes.SetActiveView(vwNuevo);
            lblTituloNuevo.InnerText = "CLIENTE NUEVO";
            BEdicion = false;
            LimpiarDatos();
            //ddlArticulos.SelectedIndex = 0;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        protected void gvwConsulta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int nFila = int.Parse(e.CommandArgument.ToString()) % gvwConsulta.PageSize;
            //if (e.CommandName.Equals("Detalles"))
            //{
            //    hfIdBanner.Value = gvwConsulta.DataKeys[nFila].Values[0].ToString();
            //    BuscarDetalle((int)gvwConsulta.DataKeys[nFila].Values[0]);
            //    mvwBanners.SetActiveView(vwDetalle);
            //}
            if (e.CommandName.Equals("Editar"))
            {
               // hfIdClienteEditar.Value = gvwConsulta.DataKeys[nFila].Values[0].ToString();
                //txtDescripcionBanner.Value = gvwConsulta.Rows[nFila].Cells[5].Text;
                //dtpFechaInicioApp.Value = gvwConsulta.Rows[nFila].Cells[8].Text;
                //dtpFechaFinApp.Value = gvwConsulta.Rows[nFila].Cells[9].Text;

                BEdicion = true;

                mvwClientes.SetActiveView(vwNuevo);
            }
        }

        protected void gvwConsulta_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvwConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwConsulta.PageIndex = e.NewPageIndex;
            Buscar();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarDatos();
        }

        public void LimpiarDatos()
        {
            txtRazonSocial.Value = string.Empty;
            txtNombreContacto.Value = string.Empty;
            chkEstado.Checked = true;
            txtCausaSusp.Value = string.Empty;
            txtLimiteCredito.Value = string.Empty;
            //ddlMoneda.SelectedIndex = 0;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Insertar();
        }

        #region Metoos

        public void Buscar()
        {
            List<EntClientes> lstConsulta = new CatalogoCliente().ObtenerClientes(txtRazonSocialBusca.Value, ddlEstado.SelectedItem.Value);
            gvwConsulta.DataSource = lstConsulta;
            gvwConsulta.DataBind();
        }

        public void Insertar()
        {
            int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);

            bool correcto = false;
            if (BEdicion)
            {
                //ACTUALIZA
            //      int idBanner = Convert.ToInt32(hfIdBannerEditar.Value);
           //     correcto = new CatalogoImagenesBanners().ActualizarImagenesBanner(idBanner, idUsuario, txtDescripcionBanner.Value, fehcaInicioApp, fechaFinApp);
            }
            else
            {
                //INSERTA 
                EntClientes cli = new EntClientes();
                cli.nombre = txtRazonSocial.Value;
                cli.contacto1 = txtNombreContacto.Value;
                cli.estatus = (chkEstado.Checked) ? "Activo":"Inactivo";
                cli.causa_susp = txtCausaSusp.Value;
                cli.limite_credito = Convert.ToDecimal(txtLimiteCredito.Value);
                cli.moneda_id = Convert.ToInt32(ddlMoneda.SelectedValue);
                cli.cond_pago_id = Convert.ToInt32(ddlTipoPago.SelectedValue);
                cli.tipo_cliente_id = Convert.ToInt32(ddlTipoCliente.SelectedValue);
                cli.usuario_creador = UsuarioLogeado.claveusuario;
                correcto = new CatalogoCliente().InsertaCliente(cli);
            }

            if (!correcto)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                "Ocurrio un error al intentar guardar la información: ", 2);
            }
            else
            {
                Buscar();
                mvwClientes.SetActiveView(vwConsulta);
            }
        }

        private void Cancelar()
        {
            Buscar();
            mvwClientes.SetActiveView(vwConsulta);
        }


        #endregion Metodos

        protected void gvwConsultaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvwConsultaDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvwConsultaDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}
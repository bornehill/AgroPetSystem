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

namespace AdminAgropet
{
    public partial class frmAccesosPerfiles : Base.BaseCatalogo
    {

        private const string cgs_ScriptsMensajes = "Mensaje_Seguridad_";
        private const string cgs_EncabezadoModulo = " DetAccesos";
        private const string cgs_MensajeError = "Error al intentar ";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Inicia_Pagina();
            }
        }

        private void Inicia_Pagina()
        {
            Page.Title = "Diseño de detalle de accesos por perfil.";
            MasterPage MiMaster = this.Master;
            ((Label)MiMaster.FindControl("lbltitulo")).Text = Page.Title;
            LlenarDrops();
        }

        private void LlenarDrops()
        {
            EntidadDDLUC beFiltroP = new EntidadDDLUC
            {
                nOpcionSeleccion = 1,
                IdValor = 0,
                DescripValor = string.Empty
            };

            this.ddluc_Perfil.LlenarDropDown<EntidadDDLUC>(beFiltroP);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            List<ConsultaDetPerfil> lstDetPerfil;

            divDetPerfil.Visible = true;
            lstDetPerfil = RealizaConsulta();

            this.grdDetPerfil.DataSource = lstDetPerfil;
            this.grdDetPerfil.DataBind();

            ActualizaPerfilEnGrid(lstDetPerfil);
        }

        private List<ConsultaDetPerfil> RealizaConsulta()
        {
            ConsultaDetPerfil tFiltro = new ConsultaDetPerfil();
            List<ConsultaDetPerfil> lstDetallePerfil;
            CatalogoBR<ConsultaDetPerfil> clsConsultas = new CatalogoBR<ConsultaDetPerfil>();

            tFiltro.idperfil = Convert.ToInt32(this.ddluc_Perfil.IDSeleccionado);

            lstDetallePerfil = clsConsultas.ObtenerListado(tFiltro);

            return lstDetallePerfil;

        }

        private void ActualizaPerfilEnGrid(List<ConsultaDetPerfil> lstItemsPerfil)
        {
            int nRow = 0;
            GridViewRow UnRow;
            CheckBox check = new CheckBox();

            foreach (ConsultaDetPerfil UnItemPerfil in lstItemsPerfil)
            {
                if (UnItemPerfil.espadre.Equals("SI"))
                {
                    UnRow = grdDetPerfil.Rows[nRow];
                    this.grdDetPerfil.Rows[nRow].BackColor = System.Drawing.Color.Gray;

                    check = UnRow.FindControl("chkSelItem") as CheckBox;

                    if (check != null)
                    {
                        check.Checked = true;
                        check.Enabled = false;
                    }
                }

                if (UnItemPerfil.idmenuusr != 0)
                {
                    UnRow = grdDetPerfil.Rows[nRow];
                    check = UnRow.FindControl("chkSelItem") as CheckBox;

                    if (check != null)
                    {
                        check.Checked = true;
                    }
                }

                nRow++;
            }
        }

        protected void grdDetPerfil_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                this.grdDetPerfil.Columns[1].ItemStyle.Wrap = false;
                this.grdDetPerfil.Columns[2].ItemStyle.Wrap = false;
            }
            catch (Exception ex)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Error al cargar datos al grid, Error: " + ex.Message.Trim(), 2);
            }

        }

        protected void btnGuardaDetPerfil_Click(object sender, EventArgs e)
        {
            string sCadenaItems;

            sCadenaItems = ObtenItemsSeleccionados();
        }

        protected void btnCancelarDetPerfil_Click(object sender, EventArgs e)
        {
            this.divDetPerfil.Visible = false;
        }

        private string ObtenItemsSeleccionados()
        {
            string sCadenaItemsSelected;
            string sCadItem;
            CheckBox check = new CheckBox();
            int nRow;

            sCadItem = string.Empty;
            sCadenaItemsSelected = string.Empty;
            nRow = 0;
            foreach (GridViewRow Renglon in this.grdDetPerfil.Rows)
            {
                check = Renglon.FindControl("chkSelItem") as CheckBox;

                if (check.Checked)
                {
                    sCadItem = string.Concat("iddp=", this.grdDetPerfil.DataKeys[nRow].Values[0].ToString().Trim(), "#");
                    sCadItem += string.Concat("idp=", this.grdDetPerfil.DataKeys[nRow].Values[1].ToString().Trim(), "#");
                    sCadItem += string.Concat("idm=", this.grdDetPerfil.DataKeys[nRow].Values[2].ToString().Trim(), "#");
                    sCadItem += "Alta";
                }
                else
                {
                    sCadItem = string.Concat("iddp=", this.grdDetPerfil.DataKeys[nRow].Values[0].ToString().Trim(), "#");
                    sCadItem += string.Concat("idp=", this.grdDetPerfil.DataKeys[nRow].Values[1].ToString().Trim(), "#");
                    sCadItem += string.Concat("idm=", this.grdDetPerfil.DataKeys[nRow].Values[2].ToString().Trim(), "#");
                    sCadItem += "Baja";
                }

                if (sCadenaItemsSelected.Equals(string.Empty))
                {
                    sCadenaItemsSelected = sCadItem.Trim();
                }
                else
                {
                    sCadenaItemsSelected += string.Concat("|", sCadItem.Trim());
                }
                nRow++;
            }

            return sCadenaItemsSelected;
        }

    }
}
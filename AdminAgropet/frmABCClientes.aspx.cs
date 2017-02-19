using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminAgropet
{
    public partial class frmABCClientes : System.Web.UI.Page
    {
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
                //Buscar();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            mvwClientes.SetActiveView(vwNuevo);
            BEdicion = false;
            //LimpiarDatos();
            //ddlArticulos.SelectedIndex = 0;
        }

        protected void gvwConsulta_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvwConsulta_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvwConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mvwArticulos.SetActiveView(vwConsulta);
                //LLenaDropDown();
                //Buscar();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            this.DivClientesABC.Visible = true;
            this.divAlta.Visible = true;
            this.divCambio.Visible = false;

            //mvwArticulos.SetActiveView(vwNuevo);
            //BEdicion = false;
            //LimpiarDatos();
            //ddlArticulos.SelectedIndex = 0;
        }
    }
}
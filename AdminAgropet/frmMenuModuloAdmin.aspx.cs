using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Utilidades.Extensiones;

namespace AdminAgropet
{
    public partial class frmMenuModuloAdmin : System.Web.UI.Page
    {
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
    }
}
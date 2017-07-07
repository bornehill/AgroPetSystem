using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetData.Pet;
using AgroPET.Entidades.Seguridad;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Especial;

namespace AgroPetWeb.website
{
  public partial class Autentica : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      string idUser = (Request["idUser"] ?? string.Empty);
      if (!string.IsNullOrEmpty(idUser))
      {
        ConsultaUsuarios user = new PetService().GetUser(new ConsultaUsuarios() { IdUsuario = Int32.Parse(idUser), Activo=true });
        if (user != null) { 
          Session["UserWeb"] = user;
          Session["UserName"] = user.ClaveUsr;
					Session["ClientWeb"] = new PetService().GetClient(user);
					Response.Redirect("~/index.aspx", false);
				}
      }
    }

    [WebMethod]
    public static string Autenticate(string userName, string passWord)
    {
      EntidadUsuarioLogeado user = new PetService().GetUser(new EntidadUsuarioLogeado() { claveusuario = userName, passwdusr = passWord });
      return user.idusuario==-11?"Error usuario o contraseña incorrecta":user.idusuario.ToString();
    }
  }
}
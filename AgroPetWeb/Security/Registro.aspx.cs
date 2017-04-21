using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetData.Pet;
using AgroPET.Entidades.Seguridad;

namespace AgroPetWeb.website
{
  public partial class Registro : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    [WebMethod]
    public static string ValidaUser(string userName, string pass)
    {
      string regresa = string.Empty;
      EntUser user = new PetService().GetUser(new EntUser() { User_Name = userName});

      if (user != null)
        regresa = "Nombre de usuario ya existe";
      else {
        user = new PetService().AddUser(new EntUser() { User_Name = userName, Pass=pass });
        if(user!=null)
          regresa = "Cuenta de usuario creada...";
        else
          regresa = "Error al crear la cuenta";
      }

      return regresa;
    }
  }
}
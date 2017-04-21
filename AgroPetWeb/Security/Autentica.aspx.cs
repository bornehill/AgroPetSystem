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
  public partial class Autentica : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      string idUser = (Request["idUser"] ?? string.Empty);
      if (!string.IsNullOrEmpty(idUser))
      {
        EntUser user = new PetService().GetUser(new EntUser() { UserId = Int32.Parse(idUser) });
        if (user != null) { 
          Session["UserWeb"] = user;
          Session["UserName"] = user.User_Name;
          Response.Redirect("~/index.aspx", false);
        }
      }
    }

    [WebMethod]
    public static string Autenticate(string userName, string passWord)
    {
      EntUser user = new PetService().GetUser(new EntUser() { User_Name = userName, Pass = passWord });
      return user==null?"Error usuario o contraseña incorrecta":user.UserId.ToString();
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using PetData.Pet;
using Agropet.Entidades.Seguridad;
using Agropet.Entidades.Especial;

namespace AgroPetWeb
{
  public partial class MasterPet : System.Web.UI.MasterPage
  {
    private static string repositorio = System.Web.Configuration.WebConfigurationManager.AppSettings["rutaImagenesBanners"];

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GetMenuWeb()
    {
      PetService service = new PetService();
      var menus = service.GetMenuWeb(new EntidadMenuWeb() { MenuId = 0, Menu = "" });
      foreach (var menu in menus)
      {
        var submenu = service.GetMenuWeb(menu);
        if (submenu.Count > 0)
        {
          Response.Write("<li class=\"dropdown\">");
          Response.Write("<a class=\"dropdown-toggle\" data-toggle=\"dropdown\" href=\"/" + menu.MenuUrl + " \">" + menu.Menu + "<span class=\"caret\"></span></a>");
          Response.Write("<ul class=\"dropdown-menu\">");
          foreach (var sub in submenu)
          {
            Response.Write("<li><a href=\"/" + menu.MenuUrl +"?MenuId="+ menu.MenuId.ToString()+ " \">" + sub.Menu + "</a></li>");
          }
          Response.Write("</ul>");
          Response.Write("</li>");
        }
        else
        {
          Response.Write("<li><a href=\"/" + menu.MenuId + "?MenuId=" + menu.MenuId.ToString() + " \">" + menu.Menu + "</a></li>");
        }
      }
    }

    public void GetBannersWeb()
    {
      PetService service = new PetService();
      var banners = service.GetBannersWeb(new EntidadBannersWeb() { idbanner = 0, fechaini = DateTime.Now, fechafin = DateTime.Now });
      foreach (var banner in banners)
      {
        if (banner.orden == 1)
          Response.Write("<div class=\"item active\">");
        else
          Response.Write("<div class=\"item\">");
        Response.Write("    <img src = \"" + repositorio + banner.pathimagen + "\" alt =\"" + banner.titulo + "\"> ");
        Response.Write("    <div class=\"carousel-caption\">");
        Response.Write("        <h3>" + banner.titulo + "</h3>");
        Response.Write("        <p>" + banner.subtitulo + "</p>");
        Response.Write("    </div> ");
        Response.Write("</div>");
      }
    }

  }
}
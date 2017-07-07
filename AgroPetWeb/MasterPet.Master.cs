using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using PetData.Pet;
using AgroPET.Entidades.Seguridad;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Especial;

namespace AgroPetWeb
{
  public partial class MasterPet : System.Web.UI.MasterPage
  {
    private static string repositorio = System.Web.Configuration.WebConfigurationManager.AppSettings["rutaImagenesBanners"];
    public int totalBuyProd=0;
    protected void Page_Load(object sender, EventArgs e)
    {
      if (Session["UserWeb"] != null)
      {
        PetService service = new PetService();
        var buy = service.GetTotalBuy(new EntBuy() { UserId = (Int32)((ConsultaUsuarios)Session["UserWeb"]).IdUsuario });
        totalBuyProd = buy!=null?(int)buy.lot:0;
      }
    }

    protected void GetMenuWeb()
    {
      PetService service = new PetService();
      var menus = service.GetMenuHijos(new EntidadMenuWeb() { MenuId = 0});
			foreach (var menu in menus)
				PrintMenu(menu);
    }

		internal void PrintMenu(EntidadMenuWeb menu) {
			PetService service = new PetService();
			var submenu = service.GetMenuHijos(menu);
			if (submenu.Count > 0)
			{
				Response.Write("				<li><div>" + menu.Menu + "</div>");
				Response.Write("				<ul>");
				foreach (var sub in submenu)
				{
					PrintMenu(sub);
				}
				Response.Write("				</ul>");
				Response.Write("				</li>");
			}
			else
			{
				Response.Write("<li><div><a href=\"/" + menu.MenuUrl + ".aspx?MenuId=" + menu.MenuId.ToString() + " \">" + menu.Menu + "</a></div></li>");
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

		public void PrintIndicatorBanner()
		{
			PetService service = new PetService();
			var banners = service.GetBannersWeb(new EntidadBannersWeb() { idbanner = 0, fechaini = DateTime.Now, fechafin = DateTime.Now });
			int pos = 0;
			foreach (var banner in banners)
			{
				if(pos==0)
					Response.Write("<li data-target=\"#myCarousel\" data-slide-to=\""+pos+"\" class=\"active\"></li>");
				else
					Response.Write("<li data-target=\"#myCarousel\" data-slide-to=\"" + pos + "\"></li>");
				pos = pos + 1;
			}
		}

	}
}
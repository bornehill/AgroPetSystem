using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetData.Pet;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Seguridad;

namespace AgroPetWeb.Mascotas
{
  public partial class Articulos : System.Web.UI.Page
  {
    private int menuId = 0;
    public string Titulo = string.Empty;
		private int pageQuery = 0;
		private int records = 0;

		protected void Page_Load(object sender, EventArgs e)
    {
      menuId = int.Parse(Request.QueryString["MenuId"]);
			pageQuery = int.Parse(Request.QueryString["pageQuery"] == null ? "0" : Request.QueryString["pageQuery"]);
			records = int.Parse(Request.QueryString["cboRecords"] == null ? "10" : Request.QueryString["cboRecords"]);
			PetService service = new PetService();
      EntidadMenuWeb menu= service.GetMenuWeb(new EntidadMenuWeb() { MenuId = menuId }).FirstOrDefault();
      Titulo = menu.Menu;
    }

    public void PrintArticulos()
    {
      PetService service = new PetService();
      int tot = service.GetTotalMenuArt(new MenuArticulos() { MenuId = menuId }, (EntClientWeb)Session["ClientWeb"], null);
      int rec = 0, recRow = 0;
      var menus = service.GetMenuArticulos(new MenuArticulos() { MenuId = menuId }, new PageQuery() { page = pageQuery, records = this.records }, (EntClientWeb)Session["ClientWeb"], null);
      foreach (var menu in menus)
      {
        if (rec != 0 && recRow == 0)
          Response.Write("</div>");
        if (recRow == 0)
          Response.Write("<div class=\"row\">");

        Response.Write("  <div class=\"col-sm-6 col-md-4\" > ");
        Response.Write("    <div class=\"thumbnail\">");
        Response.Write("      <img src = \"/ImagenesArticulos/" + menu.Image + "\" alt =\"...\" > ");
        Response.Write("      <div class=\"caption\" > ");
        Response.Write("        <h3>" + menu.NombreLinMicrosip + "</h3>");
        Response.Write("        <p>" + menu.NombreArticulo + "</p>");
        Response.Write("        <h4>" + String.Format("{0:c}", menu.Precio) + "</h4>");
        Response.Write("        <input type=\"hidden\" id = \"price_" + menu.IdArticulo + "\" value = \"" + menu.Precio + "\">");
        if (Session["UserWeb"] != null)
        {
          Response.Write("        <p>");
          Response.Write("          <label for= \"spinner_" + menu.IdArticulo + "\" >Cantidad:</ label >");
          Response.Write("          <input class=\"spinner\" id = \"spinner_" + menu.IdArticulo + "\" name = \"value\" >");
          Response.Write("          <a class=\"btn btn-primary addButton\" role =\"button\" name=\"ar_" + menu.IdArticulo + "\" >Agregar</a>");
          Response.Write("        </p>");
        }
        Response.Write("      </div>");
        Response.Write("    </div>");
        Response.Write("  </div>");

        rec = rec + 1;
        recRow = recRow == 2 ? 0 : recRow + 1;
      }

      if (rec > 0)
        Response.Write("</div>");

      if (Session["UserWeb"] != null)
        Response.Write("<input type=\"hidden\" id = \"UserId\" value = \"" + ((ConsultaUsuarios)Session["UserWeb"]).IdUsuario + "\">");
    }

    [WebMethod]
    public static string AddCar(EntBuy item)
    {
      int user = item.UserId;
      string response = string.Empty;
      item = new PetService().AddBuy(item);

      var buy = new PetService().GetTotalBuy(new EntBuy() { UserId = user });
      response = buy != null ? ((int)buy.lot).ToString() : "0";

      return response;
    }
  }
}
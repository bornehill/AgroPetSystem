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
  public partial class ArticulosPedido : System.Web.UI.Page
  {
    private int menuId = 0;
		private int pageQuery = 0;
		private int records = 0;
		private string search = null;
		public string Titulo = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
			search = Request.QueryString["search"];
			menuId = int.Parse(Request.QueryString["MenuId"] == null ? "0" : Request.QueryString["MenuId"]);
			pageQuery = int.Parse(Request.QueryString["pageQuery"]==null?"0":Request.QueryString["pageQuery"]);
			records = int.Parse(Request.QueryString["Record"] == null ? "10" : Request.QueryString["Record"]);
			PetService service = new PetService();
      EntidadMenuWeb menu = service.GetMenuWeb(new EntidadMenuWeb() { MenuId = menuId }).FirstOrDefault();
      Titulo = menu==null?"Busqueda":menu.Menu;
    }

		public void PrintArticulos()
		{
			if (Session["UserWeb"] != null)
				PrintTabla(); 
			else
				PrintMosaico();
		}

		public void PrintMosaico()
		{
			PetService service = new PetService();
			int tot = service.GetTotalMenuArt(new MenuArticulos() { MenuId = menuId }, (EntClientWeb)Session["ClientWeb"], search);
			pageQuery = pageQuery < 0 ? 0 : pageQuery;
			pageQuery = pageQuery == 99999 ? (tot / records) : (pageQuery > ((tot / records)) ? (tot / records) : pageQuery);
			var menus = service.GetMenuArticulos(new MenuArticulos() { MenuId = menuId }, new PageQuery() { page = pageQuery, records = this.records }, (EntClientWeb)Session["ClientWeb"], search);
			int rec = 0, recRow = 0;

			foreach (var menu in menus)
			{
				if (rec != 0 && recRow == 0)
					Response.Write("</div>");
				if (recRow == 0)
					Response.Write("<div class=\"row\">");

				Response.Write("  <div class=\"col-sm-4 col-md-4 col-lg-4\" > ");
				Response.Write("		<div class=\"row\">");
				Response.Write("			<div class=\"thumbnail\">");
				Response.Write("				<div class=\"row\">");
				Response.Write("						<a href=\"DetalleArticulo.aspx?IdArtiulo=" + menu.IdArticulo + "\" title=\"Click para ver detalle\">");
				if (System.IO.File.Exists(Server.MapPath("~/ImagenesArticulos/" + menu.Clave + ".jpg")))
					Response.Write("						<img src = \"/ImagenesArticulos/" + menu.Clave + ".jpg\" alt =\"" + menu.Clave + "\" width='220px' height='220px'> ");
				else
					Response.Write("						<img src = \"/images/agropet.jpg\" alt =\"" + menu.Clave + "\" width='220px' height='220px'> ");
				Response.Write("						</a>");
				Response.Write("				</div>");
				Response.Write("				<div class=\"row\">");
				Response.Write("					<div class=\"caption\" > ");
				Response.Write("						<h3>" + menu.Clave + "</h3>");
				Response.Write("						<p>" + menu.NombreArticulo + "</p>");
				Response.Write("					</div>");
				Response.Write("				</div>");
				Response.Write("			</div>");
				Response.Write("    </div>");
				Response.Write("  </div>");

				rec = rec + 1;
				recRow = recRow == 2 ? 0 : recRow + 1;
			}

			if (rec > 0)
				Response.Write("</div>");
			Response.Write("<input type=\"hidden\" id = \"pageQuery\"  value = \"" + pageQuery + "\">");
			Response.Write("<input type=\"hidden\" id = \"MenuId\"  value = \"" + menuId + "\">");
		}

		public void PrintTabla()
    {
      PetService service = new PetService();
      int tot = service.GetTotalMenuArt(new MenuArticulos() { MenuId = menuId }, (EntClientWeb)Session["ClientWeb"], search);
			pageQuery = pageQuery < 0 ? 0 : pageQuery;
			pageQuery = pageQuery == 99999 ? (tot / records) : (pageQuery > ((tot / records)) ? (tot / records) : pageQuery);
			var menus = service.GetMenuArticulos(new MenuArticulos() { MenuId = menuId }, new PageQuery() { page= pageQuery, records=this.records }, (EntClientWeb)Session["ClientWeb"], search);

      Response.Write("  <div class=\"panel panel-default\">");
      Response.Write("  <div class=\"panel-heading\">Productos "+Titulo+"</div>");
      Response.Write("  <table class=\"table\">");
      Response.Write("    <tr>");
      Response.Write("      <th>Clave</th>");
      Response.Write("      <th>Art&iacute;culo</th>");
      Response.Write("      <th>Imagen</th>");
      Response.Write("      <th>Precio</th>");
			Response.Write("      <th>Descuento</th>");
			Response.Write("      <th></th>");
      Response.Write("    </tr>");
      foreach (var menu in menus)
      {
        Response.Write("    <tr>");
        Response.Write("      <td>" + menu.Clave + "</td>");
        Response.Write("      <td>" + menu.NombreArticulo + "</td>");
        Response.Write("      <td>");
        Response.Write("        <a href=\"DetalleArticulo.aspx?IdArtiulo=" + menu.IdArticulo + "\" title=\"Click para ver detalle\">");
				if(System.IO.File.Exists(Server.MapPath("~/ImagenesArticulos/" + menu.Clave+".jpg")))
					Response.Write("          <img src = \"/ImagenesArticulos/" + menu.Clave + ".jpg\" alt =\"...\" width=\"10%\" > ");
				else
					Response.Write("          <img src = \"/images/agropet.jpg\" alt =\"...\" width=\"10%\" > ");
				Response.Write("        </a>");
        Response.Write("      </td>");
        Response.Write("      <td>" + String.Format("{0:c}", menu.Precio));
				Response.Write("        <input type=\"hidden\" id = \"off_" + menu.IdArticulo + "\" value = \"" + menu.Descuento + "\">");
				Response.Write("        <input type=\"hidden\" id = \"tax_" + menu.IdArticulo + "\" value = \"" + menu.IVA + "\">");
				Response.Write("        <input type=\"hidden\" id = \"price_" + menu.IdArticulo + "\" value = \"" + menu.Precio + "\">");
				Response.Write("      </td>");
				if (menu.Descuento > 0)
					Response.Write("      <td>" + String.Format("{0:c}", menu.Precio * (menu.Descuento / 100)) + "</td>");
				else
					Response.Write("      <td>" + String.Format("{0:c}", 0) + "</td>");
				Response.Write("      <td>");
        if (Session["UserWeb"] != null)
        {
          Response.Write("          <label for= \"spinner_" + menu.IdArticulo + "\" >Cantidad:</ label >");
          Response.Write("          <input class=\"spinner\" id = \"spinner_" + menu.IdArticulo + "\" name = \"value\" value=\"0\">");
          //Response.Write("          <a class=\"btn btn-primary addButton\" role =\"button\" name=\"ar_" + menu.IdArticulo + "\" >Agregar</a>");
        }
        Response.Write("      </td>");
        Response.Write("    </tr>");
      }
      Response.Write("  </table>");
			Response.Write("  </div>");
			Response.Write("  <button type=\"button\" class=\"btn btn-default btn-lg\" id=\"btnSave\"> ");
			Response.Write("		<span class=\"glyphicon glyphicon-save\" aria-hidden=\"true\"></span> Guardar");
			Response.Write("  </button>");

			if (Session["UserWeb"] != null)
        Response.Write("<input type=\"hidden\" id = \"UserId\" value = \"" + ((ConsultaUsuarios)Session["UserWeb"]).IdUsuario + "\">");
			Response.Write("<input type=\"hidden\" id = \"pageQuery\"  value = \"" + pageQuery + "\">");
			Response.Write("<input type=\"hidden\" id = \"MenuId\"  value = \"" + menuId + "\">");
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
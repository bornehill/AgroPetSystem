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
    public string Titulo = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
      menuId = int.Parse(Request.QueryString["MenuId"]);
      PetService service = new PetService();
      EntidadMenuWeb menu = service.GetMenuWeb(new EntidadMenuWeb() { MenuId = menuId }).FirstOrDefault();
      Titulo = menu.Menu;
    }

    public void PrintArticulos()
    {
      PetService service = new PetService();
      int tot = service.GetTotalMenuArt(new MenuArticulos() { MenuId = menuId });
      var menus = service.GetMenuArticulos(new MenuArticulos() { MenuId = menuId });

      Response.Write("  <div class=\"panel panel-default\">");
      Response.Write("  <div class=\"panel -heading\">Productos "+Titulo+"</div>");
      Response.Write("  <table class=\"table\">");
      Response.Write("    <tr>");
      Response.Write("      <th>L&iacute;nea</th>");
      Response.Write("      <th>Art&iacute;culo</th>");
      Response.Write("      <th>Imagen</th>");
      Response.Write("      <th>Precio</th>");
      Response.Write("      <th></th>");
      Response.Write("    </tr>");
      foreach (var menu in menus)
      {
        Response.Write("    <tr>");
        Response.Write("      <td>" + menu.NombreLinMicrosip + "</td>");
        Response.Write("      <td>" + menu.NombreArticulo + "</td>");
        Response.Write("      <td>");
        Response.Write("        <a href=\"DetalleArticulo.aspx?IdArtiulo=" + menu.IdArticulo + "\" title=\"Click para ver detalle\">");
        Response.Write("          <img src = \"/ImagenesArticulos/" + menu.Image + "\" alt =\"...\" width=\"10%\" > ");
        Response.Write("        </a>");
        Response.Write("      </td>");
        Response.Write("      <td>" + String.Format("{0:c}", menu.Precio) + "</td>");
        Response.Write("      <td>");
        if (Session["UserWeb"] != null)
        {
          Response.Write("          <label for= \"spinner_" + menu.IdArticulo + "\" >Cantidad:</ label >");
          Response.Write("          <input class=\"spinner\" id = \"spinner_" + menu.IdArticulo + "\" name = \"value\" >");
          //Response.Write("          <a class=\"btn btn-primary addButton\" role =\"button\" name=\"ar_" + menu.IdArticulo + "\" >Agregar</a>");
        }
        Response.Write("      </td>");
        Response.Write("    </tr>");
      }
      Response.Write("  </table>");
      Response.Write("  </div>");

      if (Session["UserWeb"] != null)
        Response.Write("<input type=\"hidden\" id = \"UserId\" value = \"" + ((EntUser)Session["UserWeb"]).UserId + "\">");
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
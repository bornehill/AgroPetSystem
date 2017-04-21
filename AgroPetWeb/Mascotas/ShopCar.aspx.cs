using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetData.Pet;
using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Seguridad;

namespace AgroPetWeb.Mascotas
{
  public partial class ShopCar : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void GetBuys()
    {
      PetService service = new PetService();
      int rec = 0, recRow = 0;
      var items = service.GetBuyView(new EntBuyView() { UserId = ((EntUser)Session["UserWeb"]).UserId });
      foreach (var item in items)
      {
        if (rec != 0 && recRow == 0)
          Response.Write("</div>");
        if (recRow == 0)
          Response.Write("<div class=\"row\">");

        Response.Write("  <div class=\"col-sm-6 col-md-4\" > ");
        Response.Write("    <div class=\"thumbnail\">");
        Response.Write("      <img src = \"/ImagenesArticulos/" + item.Image + "\" alt =\"...\" > ");
        Response.Write("      <div class=\"caption\" > ");
        Response.Write("        <h3>" + item.NombreLinMicrosip + "</h3>");
        Response.Write("        <p>" + item.NombreArticulo + "</p>");
        Response.Write("        <h4>Precio: " + String.Format("{0:c}", item.price) + "</h4>");
        Response.Write("        <h4>Total: " + String.Format("{0:c}", item.price*item.lot) + "</h5>");
        Response.Write("        <input type=\"hidden\" id = \"price_" + item.ItemId + "\" value = \"" + item.price + "\">");
        Response.Write("        <p>");
        Response.Write("          <label for= \"spinner_" + item.ItemId + "\" >Cantidad:</ label >");
        Response.Write("          <input class=\"spinner\" id = \"spinner_" + item.ItemId + "\" name = \"value\" value=\""+item.lot+"\" > ");
        Response.Write("          <a class=\"btn btn-primary delButton\" role =\"button\" name=\"ar_" + item.ItemId + "\" >Eliminar</a>");
        Response.Write("        </p>");
        Response.Write("      </div>");
        Response.Write("    </div>");
        Response.Write("  </div>");

        rec = rec + 1;
        recRow = recRow == 2 ? 0 : recRow + 1;
      }

      if (rec > 0)
        Response.Write("</div>");

      if (Session["UserWeb"] != null)
        Response.Write("<input type=\"hidden\" id = \"UserId\" value = \"" + ((EntUser)Session["UserWeb"]).UserId + "\">");
    }
  }
}
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
  public partial class DetalleArticulo : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void PrintDetalle() {
      PetService service = new PetService();
      var articulo = service.GetArticulo(new MenuArticulos() { IdArticulo = int.Parse(Request.QueryString["IdArtiulo"]) });
      Response.Write("    <center>");
      Response.Write("    <div class=\"thumbnail\">");
      Response.Write("      <img src = \"/ImagenesArticulos/" + articulo.Image + "\" alt =\"...\" > ");
      Response.Write("      <div class=\"caption\" > ");
      Response.Write("        <h3>" + articulo.NombreLinMicrosip + "</h3>");
      Response.Write("        <p>" + articulo.NombreArticulo + "</p>");
      Response.Write("        <h4>" + String.Format("{0:c}", articulo.Precio) + "</h4>");
      Response.Write("        <input type=\"hidden\" id = \"price_" + articulo.IdArticulo + "\" value = \"" + articulo.Precio + "\">");
      if (Session["UserWeb"] != null)
      {
        Response.Write("        <p>");
        Response.Write("          <label for= \"spinner_" + articulo.IdArticulo + "\" >Cantidad:</ label >");
        Response.Write("          <input class=\"spinner\" id = \"spinner_" + articulo.IdArticulo + "\" name = \"value\" >");
        Response.Write("          <a class=\"btn btn-primary addButton\" role =\"button\" name=\"ar_" + articulo.IdArticulo + "\" >Agregar</a>");
        Response.Write("        </p>");
      }
      Response.Write("      </div>");
      Response.Write("    </div>");
      Response.Write("    </center>");
    }
  }
}
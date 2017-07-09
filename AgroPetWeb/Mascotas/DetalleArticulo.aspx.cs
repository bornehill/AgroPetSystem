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
using System.Text;

namespace AgroPetWeb.Mascotas
{
  public partial class DetalleArticulo : System.Web.UI.Page
  {
		public string Titulo = string.Empty;
		private bool car=false;
		protected void Page_Load(object sender, EventArgs e)
    {

			if (!Page.IsPostBack)
			{
				PetService service = new PetService();
				var articulo = service.GetArticulo(new MenuArticulos() { IdArticulo = int.Parse(Request.QueryString["IdArtiulo"]) }, (EntClientWeb)Session["ClientWeb"]);
				car = Request.QueryString["view"] != null;
				Titulo = articulo.NombreArticulo;
			}
			else {
				var delArtId = Request.QueryString["DelArtId"];
				Response.Redirect("~/Mascotas/ShopCar.aspx?DelArt="+delArtId, true);
			}

		}

    public void PrintDetalle() {
			if (car)
				PrintItem();
			else
				PrintArticulo();
		}

		private void PrintArticulo() {
			PetService service = new PetService();

			var articulo = service.GetArticulo(new MenuArticulos() { IdArticulo = int.Parse(Request.QueryString["IdArtiulo"]) }, (EntClientWeb)Session["ClientWeb"]);
			Titulo = articulo.NombreArticulo;
			Response.Write("    <center>");
			Response.Write("    <div class=\"thumbnail\">");
			if (System.IO.File.Exists(Server.MapPath("~/ImagenesArticulos/" + articulo.Clave + ".jpg")))
				Response.Write("          <img src = \"/ImagenesArticulos/" + articulo.Clave + ".jpg\" alt =\"...\" width='450px' height='450px'> ");
			else
				Response.Write("          <img src = \"/images/agropet.jpg\" alt =\"...\" width='450px' height='450px'> ");
			Response.Write("      <div class=\"caption\" > ");
			Response.Write("        <h3>" + articulo.Clave + "</h3>");
			Response.Write("        <p>" + articulo.NombreArticulo + "</p>");
			Response.Write("        <div class=\"row\">");
			Response.Write("					<div class=\"well well-lg col-md-6 col-md-offset-3\">" + articulo.Notas + "</div>");
			Response.Write("        </div>");
			if (articulo.Descuento > 0) {
				Response.Write("        <h4 class=>\"offItem\"" + String.Format("{0:c}", articulo.Precio) + "</h4>");
				Response.Write("        <h4>" + String.Format("{0:c}", (articulo.Precio*(1-articulo.Descuento/100))) + "</h4>");
			}
			else
				Response.Write("        <h4>" + String.Format("{0:c}", articulo.Precio) + "</h4>");
	
			Response.Write("        <input type=\"hidden\" id = \"price_" + articulo.IdArticulo + "\" value = \"" + articulo.Precio + "\">");
			if (Session["UserWeb"] != null)
			{
				Response.Write("        <p>");
				Response.Write("          <label for= \"spinner_" + articulo.IdArticulo + "\" >Cantidad:</label >");
				Response.Write("          <input class=\"spinner\" id = \"spinner_" + articulo.IdArticulo + "\" name = \"value\" >");
				Response.Write("          <a class=\"btn btn-primary addButton\" role =\"button\" name=\"ar_" + articulo.IdArticulo + "\" >Agregar</a>");
				Response.Write("          <input type=\"hidden\" id = \"off_" + articulo.IdArticulo + "\" value = \"" + articulo.Descuento + "\">");
				Response.Write("          <input type=\"hidden\" id = \"tax_" + articulo.IdArticulo + "\" value = \"" + articulo.IVA + "\">");
				Response.Write("        </p>");
			}
			Response.Write("      </div>");
			Response.Write("    </div>");
			Response.Write("    </center>");
			if (Session["UserWeb"] != null)
				Response.Write("<input type=\"hidden\" id = \"UserId\" value = \"" + ((ConsultaUsuarios)Session["UserWeb"]).IdUsuario + "\">");
		}

		private void PrintItem()
		{
			var user = 0;
			PetService service = new PetService();
			if (Session["UserWeb"] != null)
				user = (Int32)((ConsultaUsuarios)Session["UserWeb"]).IdUsuario;

			var articulo = service.GetBuyView(new EntBuyView() { UserId=user, ItemId = int.Parse(Request.QueryString["IdArtiulo"]) }).Where(p=> p.ItemId==int.Parse(Request.QueryString["IdArtiulo"])).FirstOrDefault();
			var art = service.GetArticulo(new MenuArticulos() { IdArticulo = articulo.ItemId }, (EntClientWeb)Session["ClientWeb"]);
			Titulo = articulo.NombreArticulo;
			Response.Write("    <center>");
			Response.Write("    <div class=\"thumbnail\">");
			if (System.IO.File.Exists(Server.MapPath("~/ImagenesArticulos/" + art.Clave + ".jpg")))
				Response.Write("          <img src = \"/ImagenesArticulos/" + art.Clave + ".jpg\" alt =\"...\" width='450px' height='450px'> ");
			else
				Response.Write("          <img src = \"/images/agropet.jpg\" alt =\"...\" width='450px' height='450px'> ");
			Response.Write("      <div class=\"caption\" > ");
			Response.Write("        <h3>" + art.Clave + "</h3>");
			Response.Write("        <p>" + articulo.NombreArticulo + "</p>");
			Response.Write("        <div class=\"row\">");
			Response.Write("					<div class=\"well well-lg col-md-6 col-md-offset-3\">" + art.Notas + "</div>");
			Response.Write("        </div>");
			Response.Write("        <table>");
			Response.Write("					<tr>");
			Response.Write("						<td style=\"font-size: 20px;\">Cantidad:</td>");
			Response.Write("						<td style=\"font-size: 18px; text-align: right; font-family: Arial; font-weight: bold;\">" + articulo.lot + "</td>");
			Response.Write("					</tr>");
			Response.Write("					<tr>");
			Response.Write("						<td style=\"font-size: 20px;\">Precio:</td>");
			Response.Write("						<td style=\"font-size: 18px; text-align: right; font-family: Arial; font-weight: bold;\">" + $"{articulo.price:c}" + "</td>");
			Response.Write("					</tr>");
			if (articulo.off > 0)
			{
				Response.Write("					<tr>");
				Response.Write("						<td style=\"font-size: 20px;\">SubTotal:</td>");
				Response.Write("						<td style=\"font-size: 24px; color:red; text-decoration:line-through; text-align: right; font-family: Arial; font-weight: bold;\">" + $"{(articulo.lot * articulo.price):c}" + "</td>");
				Response.Write("					</tr>");
				Response.Write("					<tr>");
				Response.Write("						<td style=\"font-size: 20px;\">Descuento:</td>");
				Response.Write("						<td style=\"font-size: 18px; text-align: right; font-family: Arial; font-weight: bold;\">" + String.Format("{0:c}", (articulo.lot * articulo.price * (articulo.off / 100))) + "</td>");
				Response.Write("					</tr>");
				Response.Write("					<tr>");
				Response.Write("						<td style=\"font-size: 20px;\">Total:</td>");
				Response.Write("						<td style=\"font-size: 18px; text-align: right; font-family: Arial; font-weight: bold;\">" + String.Format("{0:c}", (articulo.lot * articulo.price * (1 - articulo.off / 100))) + "</td>");
				Response.Write("					</tr>");
			}
			else{
				Response.Write("					<tr>");
				Response.Write("						<td style=\"font-size: 20px;\">SubTotal:</td>");
				Response.Write("						<td style=\"font-size: 18px; text-align: right; font-family: Arial; font-weight: bold;\">" + $"{(articulo.lot * articulo.price):c}" + "</td>");
				Response.Write("					</tr>");
			}
			Response.Write("        </table>");
			Response.Write("        <p>");
			Response.Write("          <label id = \"lbNotes_" + articulo.ItemId + "\"> Notas:</label >");
			Response.Write("          <textarea id = \"txtNotes_" + articulo.ItemId + "\" rows=\"3\" cols =\"50\">"+(articulo.notes==null?"":articulo.notes)+"</textarea>");
			Response.Write("        </p>");
			if (Session["UserWeb"] != null)
			{
				Response.Write("        <p>");
				Response.Write("          <label for= \"spinner_" + articulo.ItemId + "\" >Cantidad:</label>");
				Response.Write("          <input class=\"spinner\" id = \"spinner_" + articulo.ItemId + "\" name = \"value\" value=\""+articulo.lot+"\">");
				Response.Write("          <a class=\"btn btn-primary updButton\" role =\"button\" name=\"ar_" + articulo.ItemId + "\">Actualizar</a>");
				Response.Write("          <input type=\"submit\" class=\"btn btn-primary\" id=\"btnDelArt\" value=\"Eliminar\" onclick='DelArt();'>");
				Response.Write("        </p>");
			}
			Response.Write("      </div>");
			Response.Write("    </div>");
			Response.Write("    </center>");
			if (Session["UserWeb"] != null)
			{
				Response.Write("<input type=\"hidden\" id = \"UserId\" value = \"" + ((ConsultaUsuarios)Session["UserWeb"]).IdUsuario + "\">");
				Response.Write("<input type=\"hidden\" id = \"DelArtId\" value = \"" + articulo.ItemId + "\">");
			}
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

		[WebMethod]
		public static string UpdateCar(EntBuy item)
		{
			int user = item.UserId;
			string response = string.Empty;
			item = new PetService().UpdateBuy(item);

			var buy = new PetService().GetTotalBuy(new EntBuy() { UserId = user });
			response = buy != null ? ((int)buy.lot).ToString() : "0";

			return response;
		}
	}
}
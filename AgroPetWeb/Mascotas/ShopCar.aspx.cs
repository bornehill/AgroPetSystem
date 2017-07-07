using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetData.Pet;
using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Seguridad;

namespace AgroPetWeb.Mascotas
{
  public partial class ShopCar : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
			if (!Page.IsPostBack)
			{
				var DelArtId = Request.QueryString["DelArt"];
				if (DelArtId != null)
					new PetService().UpdateBuy(new EntBuy() { ItemId=Int32.Parse(DelArtId), UserId = (Int32)((ConsultaUsuarios)Session["UserWeb"]).IdUsuario, lot=-99 });
			}
		}

		public void GetBuys()
		{
			PetService service = new PetService();
			decimal total = 0;
			decimal totaltax = 0;
			decimal totaloff = 0;
			MenuArticulos art = null;
			var items = service.GetBuyView(new EntBuyView() { UserId = (Int32)((ConsultaUsuarios)Session["UserWeb"]).IdUsuario });

			Response.Write("  <div class=\"panel panel-default\">");
			Response.Write("  <div class=\"panel-heading\">Lista de compras</div>");
			Response.Write("  <table id=\"tblItemsShopCar\" class=\"table\" style=\"width:70%\">");
			Response.Write("    <tr>");
			Response.Write("      <th></th>");
			Response.Write("      <th>Clave</th>");
			Response.Write("      <th>Art&iacute;culo</th>");
			Response.Write("      <th>Imagen</th>");
			Response.Write("      <th>Notas</th>");
			Response.Write("      <th>Precio</th>");
			Response.Write("      <th>Cantidad</th>");
			Response.Write("      <th>Descuento</th>");
			Response.Write("      <th>SubTotal</th>");
			Response.Write("    </tr>");
			foreach (var item in items)
			{
				art = service.GetArticulo(new MenuArticulos() { IdArticulo = item.ItemId }, (EntClientWeb)Session["ClientWeb"]);
				Response.Write("    <tr>");
				Response.Write("      <td>");
				Response.Write("				<a href=\"/Mascotas/ShopCar.aspx?DelArt="+item.ItemId+"\" title=\"Eliminar\"><span class=\"glyphicon glyphicon-remove\" aria-hidden=\"true\"></span></a>");
				Response.Write("      </td>");
				Response.Write("      <td>" + art.Clave + "</td>");
				Response.Write("      <td>" + art.NombreArticulo + "</td>");
				Response.Write("      <td>");
				Response.Write("        <a href=\"DetalleArticulo.aspx?IdArtiulo=" + item.ItemId + "&view=car\" title=\"Click para ver detalle\">");
				if (System.IO.File.Exists(Server.MapPath("~/ImagenesArticulos/" + art.Clave + ".jpg")))
					Response.Write("          <img src = \"/ImagenesArticulos/" + art.Clave + ".jpg\" alt =\"...\" width=\"30%\" > ");
				else
					Response.Write("          <img src = \"/images/agropet.jpg\" alt =\"...\" width=\"30%\" > ");
				Response.Write("        </a>");
				Response.Write("      </td>");
				Response.Write("      <td>");

				Response.Write("				<a class=\"dropdown-toggle\" data-toggle=\"collapse\" data-target=\"#clsNote" + item.ItemId + "\"> ");
				Response.Write("					<span class=\"glyphicon glyphicon-pencil\" aria-hidden=\"true\"></span>");
				Response.Write("				</a>");
				Response.Write("				<div class=\"collapse\" id=\"clsNote" + item.ItemId + "\">");
				Response.Write("					<textarea id = \"txtNotes_" + item.ItemId + "\" rows=\"3\" cols =\"50\">" + (item.notes == null ? "" : item.notes) + "</textarea>");
				Response.Write("				</div>");

				Response.Write("      </td>");
				Response.Write("      <td align=\"right\">" + String.Format("{0:c}", item.price) + "</td>");
				Response.Write("      <td align=\"center\">");
				Response.Write("          <input class=\"spinner\" id = \"spinner_" + item.ItemId + "\" name = \"value\" value=\""+ item.lot + "\">");
				Response.Write("      </td>");
				if (item.off > 0){
					Response.Write("      <td align=\"right\">" + String.Format("{0:c}", item.lot * item.price * (item.off/100)) + "</td>");
					Response.Write("      <td align=\"right\">" + String.Format("{0:c}", item.lot * item.price * (1-item.off / 100)) + "</td>");
				}
				else{
					Response.Write("      <td align=\"right\">" + String.Format("{0:c}", 0) + "</td>");
					Response.Write("      <td align=\"right\">" + String.Format("{0:c}", item.lot * item.price) + "</td>");
				}
				Response.Write("    </tr>");
				total = total + ((item.price * item.lot) - (item.price * item.lot * (item.off / 100)));
				totaltax = totaltax + ((item.price * item.lot) - (item.price * item.lot * (item.off / 100))) * (item.tax/100);
				totaloff = totaloff + item.price * item.lot * (item.off/100);
			}
			Response.Write("    <tr>");
			Response.Write("      <td colspan=\"8\" align=\"right\"> SubTotal</td>");
			Response.Write("      <td align=\"right\">" + $"{total:c}" + "</td>");
			Response.Write("    </tr>");
			Response.Write("    <tr>");
			Response.Write("      <td colspan=\"8\" align=\"right\"> Descuento</td>");
			Response.Write("      <td align=\"right\">" + $"{totaloff:c}" + "</td>");
			Response.Write("    </tr>");
			Response.Write("    <tr>");
			Response.Write("      <td colspan=\"8\" align=\"right\"> IVA</td>");
			Response.Write("      <td align=\"right\">" + $"{totaltax:c}" + "</td>");
			Response.Write("    </tr>");
			Response.Write("    <tr>");
			Response.Write("      <td colspan=\"8\" align=\"right\"> Total</td>");
			Response.Write("      <td align=\"right\">" + $"{(total + totaltax):c}" +"</td>");
			Response.Write("    </tr>");
			Response.Write("  </table>");
			Response.Write("  </div>");
			Response.Write("  <button type=\"button\" class=\"btn btn-default btn-lg\" id=\"btnSave\"> ");
			Response.Write("		<span class=\"glyphicon glyphicon-save\" aria-hidden=\"true\"></span> Guardar");
			Response.Write("  </button>");
			Response.Write("  <br />");
			Response.Write("  <br />");
			Response.Write("  <button type=\"button\" class=\"btn btn-default btn-lg\" data-toggle=\"collapse\" data-target=\"#clsRequisition\"> ");
			Response.Write("		<span class=\"glyphicon glyphicon-tag\" aria-hidden=\"true\"></span> Enviar pedido");
			Response.Write("  </button>");
			Response.Write("  <div class=\"collapse\" id=\"clsRequisition\">");
			Response.Write("		<div class=\"panel panel-warning\"> ");
			Response.Write("  		<div class=\"panel-heading\">");
			Response.Write("  			<h3 class=\"panel-title\">Dirección de envío</h3>");
			Response.Write("  		</div>");
			Response.Write("  		<div class=\"panel-body\"> ");

			Response.Write("				<button type=\"button\" id=\"btnAddress\" class=\"btn btn-info btn-lg\"> ");
			Response.Write("					<span class=\"glyphicon glyphicon-send\" aria-hidden=\"true\"></span> Direcci&oacute;n");
			Response.Write("				</button>");
			Response.Write("				<table class=\"table\" style=\"width:70%\">");
			Response.Write("					<tr>");
			Response.Write("						<th style=\"text-align:right\">Calle :</th>");
			Response.Write("						<th>");
			Response.Write("							<input type=\"text\" id=\"txtCalleSend\" class=\"form-control\" style=\"width: 300px;\" placeholder=\"Calle\" disabled/>");
			Response.Write("						</th>");
			Response.Write("					</tr>");
			Response.Write("					<tr>");
			Response.Write("						<th style=\"text-align:right\">Colonia</th>");
			Response.Write("						<th>");
			Response.Write("							<input type=\"text\" id=\"txtColoniaSend\" class=\"form-control\" style=\"width: 300px;\" placeholder=\"Colonia\" disabled/>");
			Response.Write("						</th>");
			Response.Write("					</tr>");
			Response.Write("					<tr>");
			Response.Write("						<th style=\"text-align:right\">Ciudad</th>");
			Response.Write("						<th>");
			Response.Write("							<input type=\"text\" id=\"txtCiudadSend\" class=\"form-control\" style=\"width: 200px;\" placeholder=\"Ciudad\" disabled/>");
			Response.Write("						</th>");
			Response.Write("					</tr>");
			Response.Write("					<tr>");
			Response.Write("						<th style=\"text-align:right\">Estado</th>");
			Response.Write("						<th>");
			Response.Write("							<input type=\"text\" id=\"txtEstadoSend\" class=\"form-control\" style=\"width: 200px;\" placeholder=\"Estado\" disabled/>");
			Response.Write("						</th>");
			Response.Write("					</tr>");
			Response.Write("					<tr>");
			Response.Write("						<th style=\"text-align:right\">País</th>");
			Response.Write("						<th>");
			Response.Write("							<input type=\"text\" id=\"txtPaisSend\" class=\"form-control\" style=\"width: 200px;\" placeholder=\"País\" disabled/>");
			Response.Write("						</th>");
			Response.Write("					</tr>");
			Response.Write("					<tr>");
			Response.Write("						<th style=\"text-align:right\">Contacto</th>");
			Response.Write("						<th>");
			Response.Write("							<input type=\"text\" id=\"txtContactoSend\" class=\"form-control\" style=\"width: 300px;\" placeholder=\"Contacto\" disabled/>");
			Response.Write("						</th>");
			Response.Write("					</tr>");
			Response.Write("				</table>");
			Response.Write("				<button type=\"button\" id=\"btnDispatch\" class=\"btn btn-info btn-lg\"> ");
			Response.Write("					<span class=\"glyphicon glyphicon-save\" aria-hidden=\"true\"></span> Ordenar");
			Response.Write("				</button>");
			Response.Write("  		</div>");
			Response.Write("		</div>");
			Response.Write("  </div>");

			if (Session["UserWeb"] != null)
        Response.Write("<input type=\"hidden\" id = \"UserId\" value = \"" + ((ConsultaUsuarios)Session["UserWeb"]).IdUsuario + "\">");
			Response.Write("<input type=\"hidden\" id = \"hdnDirId\" value = \"0\">");
		}
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using PetData.Pet;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Seguridad;

namespace AgroPetWeb.website
{
	public partial class ClientDirection : System.Web.UI.Page
	{
		public Int32 ClienteId;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			ClienteId = ((EntClientWeb)Session["ClientWeb"]).Client_Id;
		}

		public void PrintAddress()
		{
			PetService service = new PetService();
			var address = service.GetAddress((EntClientWeb)Session["ClientWeb"]);

			Response.Write("  <div class=\"panel panel-default\">");
			Response.Write("  <div class=\"panel-heading\">Direcciones</div>");
			Response.Write("  <table id=\"tblDirCliente\" class=\"table\" style=\"width:70%\">");
			Response.Write("    <tr>");
			Response.Write("      <th>Selecci&oacute;n</th>");
			Response.Write("      <th>Calle</th>");
			Response.Write("      <th>Colonia</th>");
			Response.Write("      <th>Ciudad</th>");
			Response.Write("      <th>Estado</th>");
			Response.Write("      <th>Pa&iacute;s</th>");
			Response.Write("      <th>Contacto</th>");
			Response.Write("    </tr>");

			foreach (var addr in address)
			{
				Response.Write("    <tr>");
				Response.Write("      <td>");
				Response.Write("					<input type=\"radio\" name =\"rdbAddrClt\" value =\""+addr.DirCliId+ "\">");
				Response.Write("      </td>");
				Response.Write("      <td>" + addr.Calle + "</td>");
				Response.Write("      <td>" + addr.Colonia + "</td>");
				Response.Write("      <td>" + addr.NomCiudad + "</td>");
				Response.Write("      <td>" + addr.NomEstado + "</td>");
				Response.Write("      <td>" + addr.NomPais + "</td>");
				Response.Write("      <td>" + addr.Contacto + "</td>");
				Response.Write("    <tr>");
			}
			Response.Write("  </table>");
			Response.Write("  </div>");
			Response.Write("	<button type=\"button\" id=\"btnSelect\" class=\"btn btn-info btn-lg\"> ");
			Response.Write("		<span class=\"glyphicon glyphicon-ok\" aria-hidden=\"true\"></span> Seleccionar");
			Response.Write("	</button>");
		}

		public void DropPais() {
			PetService service = new PetService();
			var countries = service.GetCountries(null);
			Response.Write("      <select id=\"cboPais\" class=\"form-control\" style=\"width: 200px;\">");
			Response.Write("				<option value=\"0\">Seleccionar</option>");
			foreach (var contry in countries)
				Response.Write("				<option value=\""+ contry.PaisId+ "\">"+ contry .Nombre+ "</option>");
			Response.Write("      </select>");
		}

		public void DropEstado()
		{
			Response.Write("      <select id=\"cboEstado\" class=\"form-control\" style=\"width: 200px;\">");
			Response.Write("				<option value=\"0\">Seleccionar</option>");
			Response.Write("      </select>");
		}

		public void DropCiudad()
		{
			Response.Write("      <select id=\"cboCiudad\" class=\"form-control\" style=\"width: 200px;\">");
			Response.Write("				<option value=\"0\">Seleccionar</option>");
			Response.Write("      </select>");
		}

		[WebMethod]
		public static List<EntEstado> GetEstados(EntPais pais)
		{
			PetService service = new PetService();
			List<EntEstado> states = service.GetStates(pais, null);

			return states==null? new List<EntEstado>():states;
		}

		[WebMethod]
		public static List<EntCiudad> GetCiudades(EntEstado estado)
		{
			PetService service = new PetService();
			List<EntCiudad> cities = service.GetCities(estado, null);

			return cities == null ? new List<EntCiudad>() : cities;
		}

		[WebMethod]
		public static EntDireccion AddAddress(EntDireccion address)
		{
			PetService service = new PetService();
			address = service.AddAddress(address);

			return address == null ? new EntDireccion() : address;
		}

		[WebMethod]
		public static EntDireccion GetAddress(EntDireccion address)
		{
			PetService service = new PetService();
			address = service.GetAddress(address);

			return address == null ? new EntDireccion() : address;
		}
	}
}
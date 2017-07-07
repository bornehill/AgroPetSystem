using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Seguridad;
using AgroPET.Entidades.Consultas;
using PetData.Pet;


namespace AgroPetWeb.Mascotas
{
	public partial class DispatchOrder : System.Web.UI.Page
	{
		public string folio = null;
		protected void Page_Load(object sender, EventArgs e)
		{
			var DirId = Request.QueryString["DirId"];
			if (!DirId.Equals("0"))
				Save(DirId);
		}

		internal void Save(string DirId) {
			EntClientWeb client = (EntClientWeb)Session["ClientWeb"];
			EntDoctoPv order = new EntDoctoPv()
			{
				ClaveCliente = client.ClaveCliente,
				Client_Id = client.Client_Id,
				VendedorId = client.VendedorId,
				ImpuestoIncluido = "N",
				TipoDocto = "O",
				CajaId = 855021,
				CajeroId = 855051,//861422
				AlmacenId = 19,
				MonedaId = 1,
				DirCliId = Int32.Parse(DirId)
			};

			List<EntDoctoPvDet> detDcto = new List<EntDoctoPvDet>();
			PetService service = new PetService();
			decimal total = 0;
			decimal totaltax = 0;
			decimal totaloff = 0;
			var items = service.GetBuyView(new EntBuyView() { UserId = (Int32)((ConsultaUsuarios)Session["UserWeb"]).IdUsuario });
			foreach (var i in items)
			{
				var art = service.GetArticulo(new MenuArticulos() { IdArticulo = i.ItemId }, client);
				EntDoctoPvDet d = new EntDoctoPvDet() { ClaveArticulo = art.Clave, ArticuloId = i.ItemId, Unidades = i.lot, PrecioUnitario = i.price, PctjeDscto = i.off, VendedorId = order.VendedorId, Notas = i.notes };
				d.PrecioTotalNeto = (d.Unidades * d.PrecioUnitario) * (d.PctjeDscto > 0 ? 1 - d.PctjeDscto / 100 : 1);
				d.PrecioUnitarioImpto = ((d.Unidades * d.PrecioUnitario) * (d.PctjeDscto > 0 ? 1 - d.PctjeDscto / 100 : 1)) * (i.tax > 0 ? 1 + i.tax / 100 : 1);
				totaltax = totaltax + ((d.Unidades * d.PrecioUnitario) * (d.PctjeDscto > 0 ? 1 - d.PctjeDscto / 100 : 1)) * (i.tax > 0 ? i.tax / 100 : 0);
				totaloff = totaloff + (d.Unidades * d.PrecioUnitario) * (d.PctjeDscto > 0 ? d.PctjeDscto / 100 : 0);
				total = total + d.PrecioTotalNeto;
				order.ImpuestoIncluido = order.ImpuestoIncluido.Equals("N") & i.tax > 0 ? "S" : order.ImpuestoIncluido;
				detDcto.Add(d);
			}
			order.ImporteNeto = total;
			order.DsctoImporte = totaloff;
			order.TotalImpuestos = totaltax;
			order.DetDocto = detDcto;

			order = service.SaveDispatchOrder(order);
			if (order.DoctoPvId > 0)
			{
				folio = order.Folio;
				foreach (var i in items)
					service.UpdateBuy(new EntBuy() { UserId = i.UserId, ItemId = i.ItemId, lot = -99 });


				Page.ClientScript.RegisterStartupScript(this.GetType(), "Clean", "$(\".badge\").html(\"\").append(\"0\"); $(\"#tblItemsShopCar\").find(\"tr:gt(0)\").remove();", true);
			}
		}
	}
}
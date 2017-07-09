using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Configuration;
using AgroPET.Datos.Catalogos;
using AgroPET.Entidades.Seguridad;
using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Consultas;

namespace PetData.Pet
{
    internal class PetProvider
    {
			private string strConnection = ConfigurationManager.ConnectionStrings["FireBird"].ConnectionString;

      internal List<EntidadMenuWeb> GetMenuWeb(EntidadMenuWeb menu)
      {
        CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
        return catalogoGenerico.ObtenerMenuWeb(menu);
      }

      internal List<EntidadMenuWeb> GetMenuHijos(EntidadMenuWeb menu)
      {
        CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
        return catalogoGenerico.GetMenuHijos(menu);
      }

      internal List<EntidadBannersWeb> GetBannersWeb(EntidadBannersWeb banner)
      {
        CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
        return catalogoGenerico.ObtenerBannersWeb(banner);
      }

    internal List<MenuArticulos> GetMenuArticulos(MenuArticulos articulo, PageQuery pag, EntClientWeb client, string search)
    {
      CatalogosExtDA catalogoGenerico = new CatalogosExtDA();

			string query = null;

			if (client == null)
				query = "select " +
											"Articulo_id as IdArticulo, clave_articulo as Clave, NomArticulo as NombreArticulo, NomGrupo as NombreGpoMicrosip, " +
											"NomLineas as NombreLinMicrosip, precio, Cast(0 as Numeric(18,6)) as descuento, pctje_impuesto as iva " +
											"from VW_WEB_ARTICULOS_CON_PRECIO " +
											"where nomprecio='Precio de lista' "+(search!=null? " and (lower(clave_articulo) like '" + search.ToLower()+ "%' or lower(NomArticulo) like '" + search.ToLower()+"%')" : "");
			else
				query = "select " +
										"Articulo_id as IdArticulo, clave_articulo as Clave, NomArticulo as NombreArticulo, NomGrupo as NombreGpoMicrosip, NomLineas as NombreLinMicrosip, precio, descuento, pctje_impuesto as iva " +
										"from VW_WEB_PRECIO_POR_CLIENTE ppc, " +
										"VW_WEB_ARTICULOS_CON_PRECIO ap " +
										"where ppc.cliente_ID = "+ client.Client_Id +" and ppc.precio_empresa_id = ap.precio_empresa_id" + (search != null ? " and (lower(clave_articulo) like '" + search.ToLower() + "%' or lower(NomArticulo) like '" + search.ToLower() + "%')" : "");

			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			List<MenuArticulos> fbList= motor.FbQuery<MenuArticulos>(query);
			List<MenuArticulos> menuList = catalogoGenerico.GetMenuArticulos(articulo, pag);

			if (fbList != null && menuList != null)
				fbList = fbList.Where(item => menuList.Any(category => category.IdArticulo == item.IdArticulo)).Skip(pag.records * pag.page).Take(pag.records).ToList();
			else
				fbList = new List<MenuArticulos>();

			return fbList;
    }

    internal EntidadUsuarioLogeado GetUser(EntidadUsuarioLogeado user)
    {
      DatosUsuario catalogoGenerico = new DatosUsuario();
      return catalogoGenerico.ValidarUsuario(user.claveusuario, user.passwdusr);
    }

    internal ConsultaUsuarios GetUser(ConsultaUsuarios user)
    {
        DatosUsuario catalogoGenerico = new DatosUsuario();
        return catalogoGenerico.ObtenerUsuarios(user.IdPerfil, user.IdUsuario, user.Activo).FirstOrDefault();
    }

    internal EntUser GetUser(EntUser user)
    {
        CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
        return catalogoGenerico.GetUser(user);
    }

    internal ConsultaUsuarios AddUser(ConsultaUsuarios user)
    {
      DatosUsuario catalogoGenerico = new DatosUsuario();
      return catalogoGenerico.GuardarUsuario(user)>0?user:null;
    }

    internal EntBuy AddBuy(EntBuy item)
    {
      CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
      return catalogoGenerico.AddBuy(item);
    }

		internal EntBuy UpdateBuy(EntBuy item)
		{
			CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
			return catalogoGenerico.UpdateBuy(item);
		}

		internal List<EntBuyView> GetBuyView(EntBuyView item)
    {
      CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
      return catalogoGenerico.GetBuyView(item);
    }

    internal int GetTotalMenuArt(MenuArticulos menu, EntClientWeb client, string search)
    {
			CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
			string query = null;

			if (client == null)
				query = "select " +
											"Articulo_id as IdArticulo, clave_articulo as Clave, NomArticulo as NombreArticulo, NomGrupo as NombreGpoMicrosip, " +
											"NomLineas as NombreLinMicrosip, precio, Cast(0 as Numeric(18,6)) as descuento, pctje_impuesto as iva " +
											"from VW_WEB_ARTICULOS_CON_PRECIO " +
											"where nomprecio='Precio de lista' " + (search != null ? " and (lower(clave_articulo) like '" + search.ToLower() + "%' or lower(NomArticulo) like '" + search.ToLower() + "%')" : "");
			else
				query = "select " +
										"Articulo_id as IdArticulo, clave_articulo as Clave, NomArticulo as NombreArticulo, NomGrupo as NombreGpoMicrosip, NomLineas as NombreLinMicrosip, precio, descuento, pctje_impuesto as iva " +
										"from VW_WEB_PRECIO_POR_CLIENTE ppc, " +
										"VW_WEB_ARTICULOS_CON_PRECIO ap " +
										"where ppc.cliente_ID = " + client.Client_Id + " and ppc.precio_empresa_id = ap.precio_empresa_id" + (search != null ? " and (lower(clave_articulo) like '" + search.ToLower() + "%' or lower(NomArticulo) like '" + search.ToLower() + "%')" : "");

			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			List<MenuArticulos> fbList = motor.FbQuery<MenuArticulos>(query);
			List<MenuArticulos> menuList = catalogoGenerico.GetMenuArticulos(menu, null);

			if (fbList != null && menuList != null)
				fbList = fbList.Where(item => menuList.Any(category => category.IdArticulo == item.IdArticulo)).ToList();
			else
				fbList = new List<MenuArticulos>();

			return fbList.Count;




		}

    internal EntBuy GetTotalBuy(EntBuy buy)
    {
      CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
      return catalogoGenerico.GetTotalBuy(buy);
    }

    internal MenuArticulos GetArticulo(MenuArticulos art, EntClientWeb client)
    {
			string query = null;

			if (client == null)
				query = "select " +
											"Articulo_id as IdArticulo, clave_articulo as Clave, NomArticulo as NombreArticulo, NomGrupo as NombreGpoMicrosip, " +
											"NomLineas as NombreLinMicrosip, precio, Cast(0 as Numeric(18,6)) as descuento, pctje_impuesto as iva, notas_compras as Notas " +
											"from VW_WEB_ARTICULOS_CON_PRECIO "+
											"where nomprecio='Precio de lista' and Articulo_id = " + art.IdArticulo;
			else
				query = "select " +
											"ap.Articulo_id as IdArticulo, clave_articulo as Clave, NomArticulo as NombreArticulo, NomGrupo as NombreGpoMicrosip, " +
											"NomLineas as NombreLinMicrosip, precio, descuento, pctje_impuesto as iva, notas_compras as Notas " +
											"from VW_WEB_PRECIO_POR_CLIENTE ppc, " +
											"VW_WEB_ARTICULOS_CON_PRECIO ap " +
											"where ppc.cliente_ID = " + client.Client_Id + " and ap.Articulo_id = " + art.IdArticulo + " and ppc.precio_empresa_id = ap.precio_empresa_id ";

			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;

      return motor.FbQuery<MenuArticulos>(query).FirstOrDefault();
    }

		internal EntClientWeb GetClient(ConsultaUsuarios user)
		{
			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			string query = "select " +
											"vc.cliente_id as Client_Id, vc.nombre, vc.tipocliente, vc.calle, vc.num_exterior, vc.num_interior, vc.colonia, vc.nomestado" +
											", vc.codigo_postal, vc.telefono1, vc.telefono2, dc.rfc_curp as RFC, c.vendedor_id as VendedorId, cc.clave_cliente as ClaveCliente " +
											"from VW_WEB_CLIENTES_MS vc, dirs_clientes dc, clientes c, claves_clientes cc " +
											"where vc.email is not null and lower(vc.email)='" + user.ClaveUsr.ToLower() + "' and vc.estatus = 'A' and vc.dir_cli_id=dc.dir_cli_id " +
											"and vc.cliente_id=c.cliente_id and c.cliente_id=cc.cliente_id and cc.rol_clave_cli_id=2";
			return motor.FbQuery<EntClientWeb>(query).FirstOrDefault();
		}

		internal List<EntPais> GetCountries(EntPais pais)
		{
			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			string query = "select " +
											"pais_id paisid, nombre, nombre_abrev as Abrev, (case when es_predet = 'S' then 1 else 0 end) as isPred " +
											" from paises";
			if (pais != null)
				query += " where pais_id="+pais.PaisId;

				return motor.FbQuery<EntPais>(query);
		}

		internal List<EntEstado> GetStates(EntPais pais, EntEstado estado)
		{
			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			string query = "select " +
											"estado_id estadoId, nombre, nombre_abrev as abrev, pais_id as paisid, (case when es_predet='S' then 1 else 0 end) as isPred " +
											" from estados where pais_id="+pais.PaisId;
			if (estado != null)
				query += " and estado_id="+estado.EstadoId+" order by nombre";
			else
				query += " order by nombre";

			return motor.FbQuery<EntEstado>(query);
		}

		internal List<EntCiudad> GetCities(EntEstado estado, EntCiudad ciudad)
		{
			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			string query = "select " +
											"ciudad_id as CiudadId, nombre, estado_id as estadoid, (case when es_predet='S' then 1 else 0 end) as isPred " +
											" from ciudades where estado_id=" + estado.EstadoId;
			if (ciudad != null)
				query += " and ciudad_id=" + ciudad.CiudadId + " order by nombre";
			else
				query += " order by nombre";

			return motor.FbQuery<EntCiudad>(query);
		}

		internal List<EntDireccion> GetAddress(EntClientWeb cliente)
		{
			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			string query = "select " +
											"d.dir_cli_id as DirCliId, d.cliente_id as ClienteId, d.rfc_curp as RFC, d.calle, d.num_exterior as NumExt, d.num_interior as NumInt, d.Colonia, d.Referencia, d.Ciudad_id as CiudadId, " +
											"d.estado_id as EstadoId, d.pais_id as paisId, d.codigo_postal CP, d.Telefono1, d.telefono2, d.email, d.Contacto, p.nombre as NomPais, e.nombre as NomEstado, c.nombre as NomCiudad " +
											"from dirs_clientes d " +
											"inner join paises p on d.pais_id=p.pais_id " +
											"inner join estados e on d.pais_id=e.pais_id and d.estado_id=e.estado_id " +
											"inner join ciudades c on d.estado_id=c.estado_id and d.ciudad_id=c.ciudad_id " +
											" where cliente_id=" + cliente.Client_Id;

			return motor.FbQuery<EntDireccion>(query);
		}

		internal EntDireccion GetAddress(EntDireccion dir)
		{
			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			string query = "select " +
											"d.dir_cli_id as DirCliId, d.cliente_id as ClienteId, d.rfc_curp as RFC, d.calle, d.num_exterior as NumExt, d.num_interior as NumInt, d.Colonia, d.Referencia, d.Ciudad_id as CiudadId, " +
											"d.estado_id as EstadoId, d.pais_id as paisId, d.codigo_postal CP, d.Telefono1, d.telefono2, d.email, d.Contacto, p.nombre as NomPais, e.nombre as NomEstado, c.nombre as NomCiudad " +
											"from dirs_clientes d " +
											"inner join paises p on d.pais_id=p.pais_id " +
											"inner join estados e on d.pais_id=e.pais_id and d.estado_id=e.estado_id " +
											"inner join ciudades c on d.estado_id=c.estado_id and d.ciudad_id=c.ciudad_id " +
											" where dir_cli_id=" + dir.DirCliId;

			return motor.FbQuery<EntDireccion>(query).FirstOrDefault();
		}

		internal EntDireccion AddAddress(EntDireccion address)
		{
			string nomConsig = DateTime.Now.ToString("yyyyMMddHHmmss");
			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			string query = "insert into dirs_clientes " +
											"(dir_cli_id, cliente_id, nombre_consig, calle, num_exterior, num_interior, Colonia, Referencia, Ciudad_id," +
											"estado_id, pais_id, codigo_postal, Telefono1, telefono2, email, Contacto) " +
											" values (-1,"+address.ClienteId+",'Agregada desde web "+ nomConsig + "','"+address.Calle+ "','" + address.NumExt + "','" + address.NumInt + "','" + address.Colonia + "','" + address.Referencia + "'," +
											address.CiudadId + "," + address.EstadoId+ "," + address.PaisId + ",'" + address.CP + "','" + address.Telefono1 + "','" + address.Telefono2 + "',"+
											"'" + address.Email + "','" + address.Contacto + "')";

			if (motor.FbInsert(query) == 0)
				address = new EntDireccion();
			else {
				query = "select " +
								"d.dir_cli_id as DirCliId, d.cliente_id as ClienteId, d.rfc_curp as RFC, d.calle, d.num_exterior as NumExt, d.num_interior as NumInt, d.Colonia, d.Referencia, d.Ciudad_id as CiudadId, " +
								"d.estado_id as EstadoId, d.pais_id as paisId, d.codigo_postal CP, d.Telefono1, d.telefono2, d.email, d.Contacto, p.nombre as NomPais, e.nombre as NomEstado, c.nombre as NomCiudad " +
								"from dirs_clientes d " +
								"inner join paises p on d.pais_id=p.pais_id " +
								"inner join estados e on d.pais_id=e.pais_id and d.estado_id=e.estado_id " +
								"inner join ciudades c on d.estado_id=c.estado_id and d.ciudad_id=c.ciudad_id " +
								" where cliente_id=" + address.ClienteId + " and nombre_consig='Agregada desde web " + nomConsig + "'";
				address = motor.FbQuery<EntDireccion>(query).FirstOrDefault();
			}

			return address;
		}

		internal EntDoctoPv GetLastFolio()
		{
			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			string query = "select coalesce('W'||lpad(Max(Cast(Right(folio,8) as int))+1,8,'0'),'W00000001') as folio from doctos_pv where folio like 'W%' ";

			return motor.FbQuery<EntDoctoPv>(query).FirstOrDefault();
		}

		internal EntDoctoPv SaveDispatchOrder(EntDoctoPv order)
		{
			order.Folio = GetLastFolio().Folio;

			DatosArticulos motor = new DatosArticulos();
			motor.ConexionFireBird = strConnection;
			string query = "insert into doctos_pv " +
											"(docto_pv_id, caja_id, tipo_docto, folio, fecha, hora, cajero_id, clave_cliente, cliente_id, almacen_id, moneda_id, impuesto_incluido, dscto_importe," +
											"importe_neto, total_impuestos, vendedor_id, usuario_creador, fecha_hora_creacion, sistema_origen, dir_cli_id) " +
											" values (-1," + order.CajaId + ",'O','" + order.Folio + "', cast(CURRENT_TIMESTAMP as date), CURRENT_TIME," + order.CajeroId + ",'" + order.ClaveCliente + "'," + 
											order.Client_Id + "," + order.AlmacenId + "," + order.MonedaId +",'" + order.ImpuestoIncluido + "'," + order.DsctoImporte + "," +
											order.ImporteNeto + "," + order.TotalImpuestos + "," + (order.VendedorId==0?855038: order.VendedorId) + ",'WEB', CURRENT_TIMESTAMP, 'WB',"+ order.DirCliId+")";

			if (motor.FbInsert(query) > 0)
			{
				query = "select " +
								"docto_pv_id as DoctoPvId " +
								"from doctos_pv " +
								" where folio='" + order.Folio+"'";
				try
				{
					order.DoctoPvId = motor.FbQuery<EntDoctoPv>(query).FirstOrDefault().DoctoPvId;
					int pos = 1;
					foreach (var d in order.DetDocto)
					{
						d.DoctoPvId = order.DoctoPvId;
						query = "insert into doctos_pv_det " +
																	"(docto_pv_det_id, docto_pv_id, clave_articulo, articulo_id, unidades, precio_unitario, precio_unitario_impto, " +
																	"pctje_dscto, precio_total_neto, vendedor_id, posicion, rol) " +
																	" values (-1," + d.DoctoPvId + ",'" + d.ClaveArticulo + "'," + d.ArticuloId + "," + d.Unidades + "," + d.PrecioUnitario + "," +
																	d.PrecioUnitarioImpto + "," + d.PctjeDscto + "," + d.PrecioTotalNeto + "," + d.VendedorId + "," + pos + ",'N')";
						if (motor.FbInsert(query) > 0)
							pos++;
					}
					if (pos > 1)
					{
						query = "select " +
										"docto_pv_det_id as DoctoPvDetId, docto_pv_id as DoctoPvId, clave_articulo as ClaveArticulo, articulo_id as ArticuloId, Unidades, precio_unitario as PrecioUnitario, " +
										"precio_unitario_impto as PrecioUnitarioImpto, pctje_dscto as PctjeDscto, precio_total_neto as PrecioTotalNeto, vendedor_id as VendedorId " +
										"from doctos_pv_det " +
										" where docto_pv_id=" + order.DoctoPvId;
						order.DetDocto = motor.FbQuery<EntDoctoPvDet>(query);
					}else
						order = new EntDoctoPv();
				}
				catch (Exception ex) {
					order = new EntDoctoPv();
				}
			}
			else
				order = new EntDoctoPv();

			return order;
		}
	}
}

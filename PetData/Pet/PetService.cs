using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroPET.Entidades.Seguridad;
using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Consultas;

namespace PetData.Pet
{
  public class PetService
  {
    public List<EntidadMenuWeb> GetMenuWeb(EntidadMenuWeb menu)
    {
      return new PetProvider().GetMenuWeb(menu);
    }

    public List<EntidadMenuWeb> GetMenuHijos(EntidadMenuWeb menu)
    {
      return new PetProvider().GetMenuHijos(menu);
    }

    public List<EntidadBannersWeb> GetBannersWeb(EntidadBannersWeb banner)
    {
      return new PetProvider().GetBannersWeb(banner);
    }

    public List<MenuArticulos> GetMenuArticulos(MenuArticulos articulo, PageQuery pag, EntClientWeb client, string search)
    {
      return new PetProvider().GetMenuArticulos(articulo, pag, client, search);
    }

    public EntidadUsuarioLogeado GetUser(EntidadUsuarioLogeado user)
    {
      return new PetProvider().GetUser(user);
    }

    public ConsultaUsuarios GetUser(ConsultaUsuarios user)
    {
        return new PetProvider().GetUser(user);
    }

    public EntUser GetUser(EntUser user)
    {
        return new PetProvider().GetUser(user);
    }

    public ConsultaUsuarios AddUser(ConsultaUsuarios user)
    {
      return new PetProvider().AddUser(user);
    }

    public EntBuy AddBuy(EntBuy item)
    {
      return new PetProvider().AddBuy(item);
    }

		public EntBuy UpdateBuy(EntBuy item)
		{
			return new PetProvider().UpdateBuy(item);
		}

		public List<EntBuyView> GetBuyView(EntBuyView item)
    {
      return new PetProvider().GetBuyView(item);
    }

    public int GetTotalMenuArt(MenuArticulos menu, EntClientWeb client, string search)
    {
      return new PetProvider().GetTotalMenuArt(menu, client, search);
    }

    public EntBuy GetTotalBuy(EntBuy buy)
    {
      return new PetProvider().GetTotalBuy(buy);
    }

    public MenuArticulos GetArticulo(MenuArticulos art, EntClientWeb client)
    {
      return new PetProvider().GetArticulo(art, client);
    }

		public EntClientWeb GetClient(ConsultaUsuarios user)
		{
			return new PetProvider().GetClient(user);
		}

		public List<EntPais> GetCountries(EntPais pais)
		{
			return new PetProvider().GetCountries(pais);
		}

		public List<EntEstado> GetStates(EntPais pais, EntEstado estado)
		{
			return new PetProvider().GetStates(pais, estado);
		}

		public List<EntCiudad> GetCities(EntEstado estado, EntCiudad ciudad)
		{
			return new PetProvider().GetCities(estado, ciudad);
		}

		public List<EntDireccion> GetAddress(EntClientWeb cliente)
		{
			return new PetProvider().GetAddress(cliente);
		}

		public EntDireccion GetAddress(EntDireccion dir)
		{
			return new PetProvider().GetAddress(dir);
		}

		public EntDireccion AddAddress(EntDireccion address)
		{
			return new PetProvider().AddAddress(address);
		}

		public EntDoctoPv SaveDispatchOrder(EntDoctoPv order)
		{
			return new PetProvider().SaveDispatchOrder(order);
		}
	}
}

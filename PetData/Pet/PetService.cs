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

    public List<MenuArticulos> GetMenuArticulos(MenuArticulos articulo)
    {
      return new PetProvider().GetMenuArticulos(articulo);
    }

    public EntUser GetUser(EntUser user)
    {
      return new PetProvider().GetUser(user);
    }

    public EntUser AddUser(EntUser user)
    {
      return new PetProvider().AddUser(user);
    }

    public EntBuy AddBuy(EntBuy item)
    {
      return new PetProvider().AddBuy(item);
    }

    public List<EntBuyView> GetBuyView(EntBuyView item)
    {
      return new PetProvider().GetBuyView(item);
    }

    public int GetTotalMenuArt(MenuArticulos menu)
    {
      return new PetProvider().GetTotalMenuArt(menu);
    }

    public EntBuy GetTotalBuy(EntBuy buy)
    {
      return new PetProvider().GetTotalBuy(buy);
    }

    public MenuArticulos GetArticulo(MenuArticulos art)
    {
      return new PetProvider().GetArticulo(art);
    }
  }
}

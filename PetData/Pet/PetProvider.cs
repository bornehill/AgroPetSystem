using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using AgroPET.Datos.Catalogos;
using AgroPET.Entidades.Seguridad;
using AgroPET.Entidades.Especial;
using AgroPET.Entidades.Consultas;

namespace PetData.Pet
{
    internal class PetProvider
    {
      internal List<EntidadMenuWeb> GetMenuWeb(EntidadMenuWeb menu)
      {
        CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
        return catalogoGenerico.ObtenerMenuWeb(menu);
      }

      internal List<EntidadBannersWeb> GetBannersWeb(EntidadBannersWeb banner)
      {
        CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
        return catalogoGenerico.ObtenerBannersWeb(banner);
      }

    internal List<MenuArticulos> GetMenuArticulos(MenuArticulos articulo)
    {
      CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
      return catalogoGenerico.GetMenuArticulos(articulo);
    }

    internal EntUser GetUser(EntUser user)
    {
      CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
      return catalogoGenerico.GetUser(user);
    }

    internal EntUser AddUser(EntUser user)
    {
      CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
      return catalogoGenerico.AddUser(user);
    }

    internal EntBuy AddBuy(EntBuy item)
    {
      CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
      return catalogoGenerico.AddBuy(item);
    }

    internal List<EntBuyView> GetBuyView(EntBuyView item)
    {
      CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
      return catalogoGenerico.GetBuyView(item);
    }

    internal int GetTotalMenuArt(MenuArticulos menu)
    {
      CatalogosExtDA catalogoGenerico = new CatalogosExtDA();
      return catalogoGenerico.GetTotalMenuArt(menu);
    }
  }
}

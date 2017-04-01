using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agropet.Entidades.Seguridad;
using Agropet.Entidades.Especial;
using Agropet.Entidades.Consultas;

namespace PetData.Pet
{
    public class PetService
    {
      public List<EntidadMenuWeb> GetMenuWeb(EntidadMenuWeb menu)
      {
        return new PetProvider().GetMenuWeb(menu);
      }

      public List<EntidadBannersWeb> GetBannersWeb(EntidadBannersWeb banner)
      {
        return new PetProvider().GetBannersWeb(banner);
      }

    public List<MenuArticulos> GetMenuArticulos(MenuArticulos articulo)
    {
      return new PetProvider().GetMenuArticulos(articulo);
    }

  }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetData.Pet;
using AgroPET.Entidades.Seguridad;
using AgroPET.Entidades.Especial;

namespace AgroPetWeb.website
{
  public partial class Index : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GetBannersWeb()
    {
      (this.Master as MasterPet).GetBannersWeb();
    }

		protected void PrintIndicatorBanner()
		{
			(this.Master as MasterPet).PrintIndicatorBanner();
		}
	}
}
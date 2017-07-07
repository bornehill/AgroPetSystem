using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AgroPetWeb.Mascotas
{
	public partial class Search : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void search(object sender, EventArgs e) { 
			if(!string.IsNullOrEmpty(txtSearch.Text))
				Response.Redirect("~/Mascotas/ArticulosPedido.aspx?search="+txtSearch.Text, true);
		}
	}
}
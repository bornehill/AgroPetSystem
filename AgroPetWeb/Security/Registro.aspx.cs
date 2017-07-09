using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using PetData.Pet;
using AgroPET.Entidades.Seguridad;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Especial;

namespace AgroPetWeb.website
{
  public partial class Registro : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
		}

    [WebMethod]
    public static string ValidaUser(string userName, string pass)
    {
      string regresa = string.Empty;

      var userAdd = new PetService().AddUser(new ConsultaUsuarios() { ClaveUsr = userName, PasswordUsr=pass, IdPerfil=2, Activo=true, IdUsuarioCreo =1 });
      if(userAdd != null)
        regresa = "Cuenta de usuario creada...";
      else
        regresa = "Error al crear la cuenta";

      return regresa;
    }

		[WebMethod]
		public static EntClientWeb SearchUser(string userName)
		{
			string regresa = string.Empty;
			EntClientWeb userAdd = null;
			EntUser user = new PetService().GetUser(new EntUser() { User_Name = userName });

			if (user != null)
				userAdd = new EntClientWeb() { Nombre = "Nombre de usuario ya existe", Client_Id = -1 };
			else
			{
				userAdd = new PetService().GetClient(new ConsultaUsuarios() { ClaveUsr = userName });
				if (userAdd == null)
					userAdd = new EntClientWeb() { Nombre="Su dirección de correo, no esta registrada", Client_Id=-1};
			}

			return userAdd;
		}
	}
}
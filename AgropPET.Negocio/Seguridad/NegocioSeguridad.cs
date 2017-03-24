using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Agropet.Entidades.Seguridad;
using AgroPET.Datos.Seguridad;

namespace AgropPET.Negocio.Seguridad
{

    public sealed class NegocioSeguridad
    {
        DatosUsuario UsuarioDA = new DatosUsuario();

        #region Metodos

        public EntidadUsuarioLogeado ValidaUsuario(string sClaveUser, string sPasswdUser, ref bool bExisteUsuario)
        {
            EntidadUsuarioLogeado UnUsuario;

            UnUsuario = UsuarioDA.ValidarUsuario(sClaveUser, sPasswdUser);

            if (UnUsuario.idusuario.Equals(-11))
                bExisteUsuario = false;
            else
                bExisteUsuario = true;

            return UnUsuario;
        }

        public List<EntidadDetAccesoPerfil> ObtenerMenuPerfilUsuario(long nIdPerfilUsuario)
        {
            List<EntidadDetAccesoPerfil> lstDetAccesosUsuario;

            lstDetAccesosUsuario = UsuarioDA.ObtenerDetPerfilUsuario(nIdPerfilUsuario);

            return lstDetAccesosUsuario;
        }

        #endregion

    }
}

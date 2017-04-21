using AgroPET.Entidades.Consultas;
using AgroPET.Datos.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgropPET.Negocio.Catalogos
{
    public class CatalogoUsuario
    {
        public List<ConsultaUsuarios> ObtenerUsuarios(int? IdPerfil, int? IdUsuario, bool? Activo)
        {
            List<ConsultaUsuarios> lst = new DatosUsuario().ObtenerUsuarios(IdPerfil,IdUsuario,Activo);

            return lst;
        }

        public int GuardarUsuario(ConsultaUsuarios usuario)
        {
            return new DatosUsuario().GuardarUsuario(usuario);
        }

        public int EditarUsuario(ConsultaUsuarios usuario)
        {
            return new DatosUsuario().EditarUsuario(usuario);
        }

    }
}

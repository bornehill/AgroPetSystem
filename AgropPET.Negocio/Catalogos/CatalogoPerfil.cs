using Agropet.Entidades.CatABCs;
using AgroPET.Datos.Catalogos;
using AgroPET.Entidades.CatABCs;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgropPET.Negocio.Catalogos
{
    public class CatalogoPerfil
    {
        public List<EntPerfil> ObtenerPerfiles(int? IdPerfil, bool? Activo)
        {
            List<EntPerfil> lst = new DatosPerfil().ObtenerPerfil(IdPerfil, Activo);

            return lst;
        }

        public List<ConsultaPerfiles> ObtenerPerfilesGrid(int? IdPerfil, bool? Activo)
        {
            List<ConsultaPerfiles> lst = new DatosPerfil().ObtenerPerfilesGrid(IdPerfil, Activo);
            return lst;
        }

        public int GuardarPerfil(EntPerfil perfil)
        {
            return new DatosPerfil().GuardarPerfil(perfil);
        }

        public int EditarPerfil(EntPerfil perfil)
        {
            return new DatosPerfil().EditaPerfil(perfil);
        }
    }
}

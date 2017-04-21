using Agropet.Entidades.CatABCs;
using AgroPET.Datos.Comun;
using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Datos.Catalogos
{
    public class DatosPerfil : DatosBase
    {
        public List<EntPerfil> ObtenerPerfil(int? IdPerfil, bool? Activo)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspPerfiles_Consultar";
            accesoDatos.parametros.Agrega("@IdPerfil", IdPerfil, true);
            accesoDatos.parametros.Agrega("@Activo", Activo, true);
            return accesoDatos.ConsultaDataList<EntPerfil>();
        }

        public List<ConsultaPerfiles> ObtenerPerfilesGrid(int? IdPerfil, bool? Activo)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspPerfiles_Seleccion";
            accesoDatos.parametros.Agrega("@IdPerfil", IdPerfil, true);
            accesoDatos.parametros.Agrega("@Activo", Activo, true);
            return accesoDatos.ConsultaDataList<ConsultaPerfiles>();
        }

        public int GuardarPerfil(EntPerfil perfil)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspPerfiles_Alta";
            accesoDatos.parametros.Agrega("@NombrePerfil", perfil.nombreperfil, true);
            accesoDatos.parametros.Agrega("@IdUsuarioCreo", perfil.idusuariocreo, true);
            accesoDatos.parametros.Agrega("@Activo", perfil.activo, true);
            return accesoDatos.EjecutaNQuery();
        }

        public int EditaPerfil(EntPerfil perfil)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspPerfiles_Actualiza";
            accesoDatos.parametros.Agrega("@IdPerfil", perfil.idperfil, true);
            accesoDatos.parametros.Agrega("@NombrePerfil", perfil.nombreperfil, true);
            accesoDatos.parametros.Agrega("@IdUsuarioUltModif", perfil.idusuarioultmodif, true);
            accesoDatos.parametros.Agrega("@Activo", perfil.activo, true);
            return accesoDatos.EjecutaNQuery();
        }


    }
}

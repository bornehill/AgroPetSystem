﻿using AgroPET.Entidades.Consultas;
using AgroPET.Entidades.Seguridad;
using AgroPET.Datos.Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Datos.Catalogos
{
    public class DatosUsuario : DatosBase
    {
        public EntidadUsuarioLogeado ValidarUsuario(string sClaveUsuario, string sPasswdUsr)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspusuarios_validausr";
            accesoDatos.parametros.Agrega("@vNombreUsuario", sClaveUsuario, true);
            accesoDatos.parametros.Agrega("@vPassUsuario", sPasswdUsr, true);
            return accesoDatos.ConsultaDataList<EntidadUsuarioLogeado>().FirstOrDefault();
        }

        public List<EntidadDetAccesoPerfil> ObtenerDetPerfilUsuario(long nIdPerfil)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspDetAccesosxPerfil_Seleccion";
            accesoDatos.parametros.Agrega("@nIdPerfil", nIdPerfil, true);
            return accesoDatos.ConsultaDataList<EntidadDetAccesoPerfil>();
        }

        public List<ConsultaUsuarios> ObtenerUsuarios(int? IdPerfil, int? IdUsuario, bool? Activo)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspUsuariosConsultar";
            accesoDatos.parametros.Agrega("@param_IdPerfil", IdPerfil, true);
            accesoDatos.parametros.Agrega("@param_IdUsuario", IdUsuario, true);
            accesoDatos.parametros.Agrega("@param_Activo", Activo, true);
            return accesoDatos.ConsultaDataList<ConsultaUsuarios>();
        }

        public int GuardarUsuario(ConsultaUsuarios usuario)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspUsuariosAlta";
            accesoDatos.parametros.Agrega("@idperfil", usuario.IdPerfil, true);
            accesoDatos.parametros.Agrega("@claveusr", usuario.ClaveUsr, true);
            accesoDatos.parametros.Agrega("@nombreusr", usuario.NombreUsr, true);
            accesoDatos.parametros.Agrega("@passwordusr", usuario.PasswordUsr, true);
            accesoDatos.parametros.Agrega("@activo", usuario.Activo, true);
            accesoDatos.parametros.Agrega("@idusuariocreo", usuario.IdUsuarioCreo, true);
            
            return accesoDatos.EjecutaNQuery();
        }

        public int EditarUsuario(ConsultaUsuarios usuario)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspUsuariosActualiza";
            accesoDatos.parametros.Agrega("@idusuario", usuario.IdUsuario, true);
            accesoDatos.parametros.Agrega("@idperfil", usuario.IdPerfil, true);
            accesoDatos.parametros.Agrega("@claveusr", usuario.ClaveUsr, true);
            accesoDatos.parametros.Agrega("@nombreusr", usuario.NombreUsr, true);
            accesoDatos.parametros.Agrega("@passwordusr", usuario.PasswordUsr, true);
            accesoDatos.parametros.Agrega("@activo", usuario.Activo, true);
            accesoDatos.parametros.Agrega("@idusuariomodif", usuario.IdUsuarioModif, true);

            return accesoDatos.EjecutaNQuery();
        }
    }
}

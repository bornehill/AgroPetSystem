﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

using Agropet.Entidades.Seguridad;
using AgroPET.Datos.Comun;

namespace AgroPET.Datos.Seguridad
{
    public sealed class DatosUsuario
    {
        public string sMensajeError { get; set; }

        #region Metodos

        public EntidadUsuarioLogeado ValidarUsuario(string sClaveUsuario, string sPasswdUsr)
        {
            MySqlDataReader drDatos;
            EntidadUsuarioLogeado InfoUsuario = new EntidadUsuarioLogeado();

            EjecutaSP spEjecutaSP = new EjecutaSP("uspusuarios_validausr");

            MySqlParameter pParamUser = new MySqlParameter("vNombreUsuario", MySqlDbType.VarChar, 20);
            pParamUser.Value = sClaveUsuario;
            spEjecutaSP.AgregarParametro(pParamUser);

            MySqlParameter pParamPasswd = new MySqlParameter("vPassUsuario", MySqlDbType.VarChar, 20);
            pParamPasswd.Value = sPasswdUsr;
            spEjecutaSP.AgregarParametro(pParamPasswd);

            drDatos = spEjecutaSP.ObtenerReader();

            InfoUsuario = ConversionesEntidadesNegocio<EntidadUsuarioLogeado>.ConvertirUnaEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return InfoUsuario;

        }

        public List<EntidadDetAccesoPerfil> ObtenerDetPerfilUsuario(long nIdPerfil)
        {
            MySqlDataReader drDatos;
            List<EntidadDetAccesoPerfil> lstInfoPerfil = new List<EntidadDetAccesoPerfil>();
            EjecutaSP spEjecutaSP = new EjecutaSP("uspDetAccesosxPerfil_Seleccion");

            MySqlParameter pUnParametro = new MySqlParameter("nIdPerfil", MySqlDbType.Int64);
            pUnParametro.Value = nIdPerfil;
            spEjecutaSP.AgregarParametro(pUnParametro);

            drDatos = spEjecutaSP.ObtenerReader();

            lstInfoPerfil = ConversionesEntidadesNegocio<EntidadDetAccesoPerfil>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lstInfoPerfil;
        }

        #endregion
    }
}

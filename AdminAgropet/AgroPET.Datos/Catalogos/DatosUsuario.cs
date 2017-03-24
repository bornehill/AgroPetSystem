using Agropet.Entidades.Consultas;
using AgroPET.Datos.Comun;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Datos.Catalogos
{
    public class DatosUsuario
    {
        public List<ConsultaUsuarios> ObtenerUsuarios(int? IdPerfil, int? IdUsuario, int? Activo)
        {
            MySqlDataReader drDatos;
            EjecutaSP spEjecutaSP = new EjecutaSP("uspUsuariosConsultar");

            MySqlParameter param_IdPerfil = new MySqlParameter("param_IdPerfil", MySqlDbType.Int32);
            param_IdPerfil.Value = IdPerfil;
            spEjecutaSP.AgregarParametro(param_IdPerfil);

            MySqlParameter param_IdUsuario = new MySqlParameter("param_IdUsuario", MySqlDbType.Int32);
            param_IdUsuario.Value = IdUsuario;
            spEjecutaSP.AgregarParametro(param_IdUsuario);

            MySqlParameter param_Activo = new MySqlParameter("param_Activo", MySqlDbType.Int32);
            param_Activo.Value = Activo;
            spEjecutaSP.AgregarParametro(param_Activo);

            drDatos = spEjecutaSP.ObtenerReader();

            List<ConsultaUsuarios> lstInfo = ConversionesEntidadesNegocio<ConsultaUsuarios>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lstInfo;
        }
    }
}

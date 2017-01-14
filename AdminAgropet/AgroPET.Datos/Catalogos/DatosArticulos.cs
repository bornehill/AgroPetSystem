using Agropet.Entidades.Consultas;
using AgroPET.Datos.Comun;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Datos.Catalogos
{
    public class DatosArticulos
    {
        public List<ConsultaArticulos> ObtenerArticulos()
        {
            //MySqlDataReader drDatos;
            //EjecutaSP spEjecutaSP = new EjecutaSP("uspArticulosObtener");

            //drDatos = spEjecutaSP.ObtenerReader();

            //List<ConsultaArticulos> lstInfo = ConversionesEntidadesNegocio<ConsultaArticulos>.ConvertirAListadoEntidadNegocio(drDatos);

            //spEjecutaSP.Dispose();

            //return lstInfo;

            return null;
        }

        public List<ConsultaArticulos> ObtenerArticulosDropDown()
        {
            MySqlDataReader drDatos;
            EjecutaSP spEjecutaSP = new EjecutaSP("uspArticulosObtenerDropDown");

            drDatos = spEjecutaSP.ObtenerReader();

            List<ConsultaArticulos> lstInfo = ConversionesEntidadesNegocio<ConsultaArticulos>.ConvertirAListadoEntidadNegocio(drDatos);

            spEjecutaSP.Dispose();

            return lstInfo;
        }
    }
}

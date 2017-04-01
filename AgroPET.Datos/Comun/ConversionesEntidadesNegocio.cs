using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

using AgroPET.Entidades.Base;
using AgroPET.Entidades.Seguridad;

using MySql.Data.MySqlClient;

namespace AgroPET.Datos.Comun
{
    /// <summary>
    /// Clase con la funcionalidad para realizar conversiones de datos extraidos de la base de datos
    /// a entidades de negocio.
    /// </summary>
    /// <typeparam name="T">Entidad de negiocio que se desaa convertir</typeparam>
    public class ConversionesEntidadesNegocio<T> where T : new()
    {
        /// <summary>
        /// Hace la conversión de la información obtenida de un DataReader a un listado 
        /// de Entidades de Negocio.
        /// </summary>
        /// <param name="drInfoBD">Información obtenida de la base de datos</param>
        /// <returns>Listado de entidades de negocio</returns>
        public static List<T> ConvertirAListadoEntidadNegocio(MySqlDataReader drInfoBD)
        {
            List<T> listResultado = new List<T>();
            CampoAttribute caAtributoNombreCampo;

            if (!drInfoBD.IsClosed && drInfoBD.HasRows)
            {
                while (drInfoBD.Read())
                {
                    T tEntidadNegocio = new T();    //Crea la entidad de negocio definido por el "Template" T                    

                    //Obtener todos los nombres de campo de la entidad de negocio
                    //para asignar los valores correspondientes a las propiedades de la entidad de negocio.
                    foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
                    {
                        if (piPropiedadEntidad.CanWrite)
                        {
                            object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(false);
                            if (oAtributos.Length > 0)
                            {
                                foreach (object unAtributo in oAtributos)
                                {
                                    if (unAtributo.GetType() == typeof(CampoAttribute))
                                    {
                                        caAtributoNombreCampo = (CampoAttribute)unAtributo;

                                        if (caAtributoNombreCampo.EsCampoRetornoConsulta)
                                        {
                                            object valorBD = drInfoBD[caAtributoNombreCampo.NombreCampo];
                                            if (valorBD.GetType() == typeof(DBNull))
                                                valorBD = null; //Convertir el tipo de dato DBNull a un "null normal"

                                            piPropiedadEntidad.SetValue(tEntidadNegocio, valorBD, null);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    listResultado.Add(tEntidadNegocio);
                }
            }

            return listResultado;
        }

        /// <summary>
        /// Realiza la conversión de la información obtenida de un DataReader
        /// del primer registro que se lee a una entidad de negocio.
        /// </summary>
        /// <param name="drInfoBD">DataReader que contiene la información a ser transformada</param>
        /// <returns>Regresa una entidad de negocio</returns>
        public static T ConvertirUnaEntidadNegocio(MySqlDataReader drInfoBD)
        {
            T tEntidadNegocio = new T(); //Crea la entidad de negocio definido por el "Template" T
            CampoAttribute caAtributoNombreCampo;

            if ((!drInfoBD.IsClosed) && drInfoBD.HasRows)
            {
                drInfoBD.Read();    //Lee el primer registro

                //Obtener todos los nombres de campo de la entidad de negocio
                //para asignar los valores correspondientes a las propiedades de la entidad de negocio.
                foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
                {
                    if (piPropiedadEntidad.CanWrite)
                    {
                        object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(false);
                        if (oAtributos.Length > 0)
                        {
                            foreach (object unAtributo in oAtributos)
                            {
                                if (unAtributo.GetType() == typeof(CampoAttribute))
                                {
                                    caAtributoNombreCampo = (CampoAttribute)unAtributo;

                                    if (caAtributoNombreCampo.EsCampoRetornoConsulta)
                                    {
                                        object valorBD = drInfoBD[caAtributoNombreCampo.NombreCampo];
                                        if (valorBD.GetType() == typeof(DBNull))
                                            valorBD = null; //Convertir el tipo de dato DBNull a un "null normal"

                                        piPropiedadEntidad.SetValue(tEntidadNegocio, valorBD, null);
                                    }
                                }
                            }

                        }
                    }
                }
            }

            return tEntidadNegocio;
        }

        //////public static T DesSerializarEntidad(MySqlDataReader drInfoBD, int columnaXML)
        //////{
        //////    T entidad = new T();

        //////    if (!drInfoBD.IsClosed && drInfoBD.HasRows)
        //////    {
        //////        if (drInfoBD.Read())
        //////        {
        //////            XmlSerializer seializador = new XmlSerializer(entidad.GetType());
        //////            object objetoDesSerializado = seializador.Deserialize(drInfoBD.GetSqlXml(columnaXML).CreateReader());

        //////            entidad = (T)objetoDesSerializado;
        //////        }
        //////    }

        //////    return entidad;
        //////}

        public static string SerializarEntidad(T configEntidad)
        {
            XmlSerializer xmlSerializador = new XmlSerializer(configEntidad.GetType());
            XmlWriterSettings xmlConfiguracion = new XmlWriterSettings();
            xmlConfiguracion.OmitXmlDeclaration = true;     //Para que no genere información con encabezados el XML.            
            StringBuilder entidadTextoSerializada = new StringBuilder();
            XmlWriter escritorXML = XmlWriter.Create(entidadTextoSerializada, xmlConfiguracion);
            xmlSerializador.Serialize(escritorXML, configEntidad);

            return entidadTextoSerializada.ToString();
        }
    }
}

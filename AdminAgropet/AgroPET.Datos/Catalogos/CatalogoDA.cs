using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;

using AgroPET.Datos.Comun;
using Agropet.Entidades.Base;
using Agropet.Entidades.Especial;
using AgroPET.Datos.Excepciones;
using InterfacesAgroPET.Catalogos;
using Utilidades;

namespace AgroPET.Datos.Catalogos
{
    public class CatalogoDA<T> : ICatalogoBase<T> where T : new()
    {
        #region Constantes privadas

        private const string NombreParametroAlerta = "MsgProceso";
        private const string NombreParametroReturn = "Ret";

        #endregion

        #region enumeradores privados

        private enum AccionSP
        {
            Select,
            Insert,
            Update,
            Delete,
            Carga,
            Proceso
        }

        #endregion

        #region Variables privadas

        private string _NombreCampoID;
        private string _mensajeAlerta = string.Empty;
        private string _resultadoejecucion = string.Empty;
        private int? _valorReturn;

        private MySqlDataReader drEntidad;

        #endregion

        #region propiedades

        /// <summary>
        /// Obtiene el último mensaje de alerta enviado por la base de datos.
        /// </summary>
        public string MensajeAlerta
        {
            get { return _mensajeAlerta; }
        }

        /// <sumary>
        /// Obtiene el resultado de la ejecucion del store
        /// </sumary>
        public string ResultadoEjecucion
        {
            get { return _resultadoejecucion; }
        }

        /// <summary>
        /// Obtiene el valor de return del storeprocedure.
        /// </summary>
        public int? ValorReturn
        {
            get { return _valorReturn; }
        }

        #endregion

        #region metodos privados

        private string ObtenerNombreSP(AccionSP tipoAccion)
        {
            string sResultado = string.Empty;

            switch (tipoAccion)
            {
                case CatalogoDA<T>.AccionSP.Select:
                    sResultado = string.Format("usp{0}_Seleccion", this.ObtenerNombreTabla());
                    break;
                case CatalogoDA<T>.AccionSP.Insert:
                    sResultado = string.Format("usp{0}_Alta", this.ObtenerNombreTabla());
                    break;
                case CatalogoDA<T>.AccionSP.Update:
                    sResultado = string.Format("usp{0}_Actualiza", this.ObtenerNombreTabla());
                    break;
                case CatalogoDA<T>.AccionSP.Delete:
                    sResultado = string.Format("usp{0}_Elimina", this.ObtenerNombreTabla());
                    break;
                case CatalogoDA<T>.AccionSP.Carga:
                    sResultado = string.Format("usp{0}_Carga", this.ObtenerNombreTabla());
                    break;
                case CatalogoDA<T>.AccionSP.Proceso:
                    sResultado = string.Format("usp{0}_Proceso", this.ObtenerNombreTabla());
                    break;
                default:
                    sResultado = string.Empty;
                    break;
            }

            return sResultado;
        }

        #endregion

        #region Metodos protegidos

        /// <summary>
        /// Obtiene el nombre de la tabla especificado en el atributo de la entidad de negocio
        /// </summary>
        /// <returns></returns>
        protected string ObtenerNombreTabla()
        {
            string sResultado = string.Empty;

            object[] oAtributos=typeof(T).GetCustomAttributes(true);

            Agropet.Entidades.Base.TablaAttribute taAtributoNombreTabla;
            foreach (object unAtributo in oAtributos)
            {
                if (unAtributo.GetType() == typeof(Agropet.Entidades.Base.TablaAttribute))
                {
                    taAtributoNombreTabla = (Agropet.Entidades.Base.TablaAttribute)unAtributo;
                    sResultado = taAtributoNombreTabla.NombreTabla;
                }
            }

            return sResultado;
        }

        /// <summary>
        /// Obtiene el nombre del tipo de dato. Genericos
        /// </summary>
        /// <typeparam name="U">Generico BE</typeparam>
        /// <returns>nombre del tipo de dato correspondiente en la BE</returns>
        protected string GetNameTypeAt<U>()
        {
            TablaAttribute AuxTBA = GetTableAttribute<U>();
            if (!AuxTBA.Equals(null))
            {
                return AuxTBA.NombreTipo;
            }
            return string.Empty;
        }

        /// <summary>
        /// Obtiene los campos de la TablaAttribute. Genericos
        /// </summary>
        /// <typeparam name="U">Generico BE</typeparam>
        /// <returns>Objeto TablaAttribute</returns>
        protected TablaAttribute GetTableAttribute<U>()
        {
            object[] oAtributos = typeof(U).GetCustomAttributes(true);
            List<object> listagenerica = oAtributos.ToList();
            TablaAttribute unaTabla = (TablaAttribute)listagenerica.Find(xxx => xxx.GetType() == typeof(TablaAttribute));
            return unaTabla;
        }

        /// <summary>
        /// Obtiene el nombre del campo llave de la entidad de negocio
        /// </summary>
        /// <returns>Nombre del campo llave</returns>
        protected string ObtenerNombreCampoID()
        {
            foreach (PropertyInfo piPropiedad in new T().GetType().GetProperties())
            {
                object[] oAtributos = piPropiedad.GetCustomAttributes(true);
                if (oAtributos.Length > 0)
                {
                    foreach (object unAtributo in oAtributos)
                    {
                        if (unAtributo.GetType() == typeof(CampoAttribute))
                        {
                            CampoAttribute caEntidad = (CampoAttribute)unAtributo;
                            if (caEntidad.EsLlavePrimaria)
                                return caEntidad.NombreCampo;
                        }
                    }
                }
            }

            //No se encontro ningun campo llave.
            return string.Empty;
        }

        #endregion

        #region ICatalogoBase<T> Members (metodos para genericos)

        /// <sumary>
        /// Obtiene el nombre del campo que es el ID
        /// </sumary>
        public string NombreCampoID
        {
            get
            {
                if (string.IsNullOrEmpty(this._NombreCampoID))
                    this._NombreCampoID = this.ObtenerNombreCampoID();

                return this._NombreCampoID;
            }
        }

        /// <summary>
        /// Guarda un registro en la base de datos.
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio que contiene la información a ser almacenada</param>
        public void Guardar(T tEntidadNegocio)
        {
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Insert));
            MySqlDataReader drResultado;
            EntidadResultProceso ResultadoProc = new EntidadResultProceso();

            #region Asignacion de parametros

            CampoAttribute caAtributoEntidadNegocio = null;
            foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
            {
                caAtributoEntidadNegocio = null;
                object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
                if (oAtributos.Length > 0)
                {
                    foreach (object unAtributo in oAtributos)
                    {
                        if (unAtributo.GetType() == typeof(CampoAttribute))
                            caAtributoEntidadNegocio = (CampoAttribute)unAtributo;
                    }
                }

                //Cuando el campo es llave primaria, no se debe asignar el parametro
                if (caAtributoEntidadNegocio ==null || (!caAtributoEntidadNegocio.EsLlavePrimaria && caAtributoEntidadNegocio.EsParametroSP))
                {
                    object oValor = piPropiedadEntidad.GetValue(tEntidadNegocio, null);
                    if (oValor != null)
                    {
                        string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
                        espSP.AgregarParametro(new MySqlParameter(sNombreParametro, oValor));
                    }
                }
            }

            //////Agregar el Parametro para leer las alertas de la base de datos.
            //////MySqlParameter param = new MySqlParameter(NombreParametroAlerta, MySqlDbType.VarChar, 8000);
            //////param.Direction = ParameterDirection.Output;
            //////espSP.AgregarParametro(param);

            //////Agregar el parámetro para leer el dato de resultado.
            //////MySqlParameter paramRet = new MySqlParameter(NombreParametroReturn, MySqlDbType.Int16);
            //////paramRet.Direction = ParameterDirection.ReturnValue;
            //////espSP.AgregarParametro(paramRet);

            #endregion

            //////Se inicializan los valores antes de ejecutar el stored procedure
            ////this._valorReturn = null;
            ////this._mensajeAlerta = string.Empty;

            drResultado = espSP.ObtenerReader();

            ResultadoProc = ConversionesEntidadesNegocio<EntidadResultProceso>.ConvertirUnaEntidadNegocio(drResultado);


            ////espSP.EjecutaSinResultado();

            this._resultadoejecucion = ResultadoProc.MsgProceso.Substring(0, 2);
            this._mensajeAlerta = ResultadoProc.MsgProceso.Substring(2);
            //////this._valorReturn = (int?)espSP.Parametros[NombreParametroReturn].Value;

            espSP.Dispose();
        }

        /// <summary>
        /// Actualiza un registro en la base de datos
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio que contiene la información a ser actualizada</param>
        public void Actualizar(T tEntidadNegocio)
        {
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Update));

            MySqlDataReader drResultado;
            EntidadResultProceso ResultadoProc = new EntidadResultProceso();

            #region Asignación de parámetros

            foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
            {
                CampoAttribute caAtributoEntidadNegocio = null;
                object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
                if (oAtributos.Length > 0)
                {
                    caAtributoEntidadNegocio = (CampoAttribute)oAtributos.ToList().Find(x => x.GetType() == typeof(CampoAttribute));
                }

                if (caAtributoEntidadNegocio == null || caAtributoEntidadNegocio.EsParametroSP)
                {
                    object oValor = piPropiedadEntidad.GetValue(tEntidadNegocio, null);
                    if (oValor != null)
                    {
                        string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
                        espSP.AgregarParametro(new MySqlParameter(sNombreParametro, oValor));
                    }
                }
            }

            ////////Agregar el Parametro para leer las alertas de la base de datos.
            //////MySqlParameter param = new MySqlParameter(NombreParametroAlerta, MySqlDbType.VarChar, 8000);
            //////param.Direction = ParameterDirection.Output;
            //////espSP.AgregarParametro(param);

            ////////Agregar el parámetro para leer el dato de resultado.
            //////MySqlParameter paramRet = new MySqlParameter(NombreParametroReturn, MySqlDbType.Int16);
            //////paramRet.Direction = ParameterDirection.ReturnValue;
            //////espSP.AgregarParametro(paramRet);

            #endregion

            //Inicializa los valores antes de ejecutar el storeprocedure
            this._valorReturn = null;
            this._mensajeAlerta = string.Empty;

            drResultado = espSP.ObtenerReader();

            ResultadoProc = ConversionesEntidadesNegocio<EntidadResultProceso>.ConvertirUnaEntidadNegocio(drResultado);

            this._resultadoejecucion = ResultadoProc.MsgProceso.Substring(0, 2);
            this._mensajeAlerta = ResultadoProc.MsgProceso.Substring(2);

            ////this._mensajeAlerta = espSP.Parametros[NombreParametroAlerta].Value.ToString();
            ////this._valorReturn = (int?)espSP.Parametros[NombreParametroReturn].Value;

            espSP.Dispose();
        }

        /// <summary>
        ///  Actualiza un registro en la base de datos, ejecutando el update asociado a la opcion
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio que contiene la información a ser actualizada</param>
        /// <param name="opcion">opcion de update a ejecutar</param>
        public void Actualizar(T tEntidadNegocio, int opcion)
        {
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Update));

            #region Asignación de parámetros
            espSP.AgregarParametro(new MySqlParameter("OpcionSeleccion", opcion));
            foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
            {
                CampoAttribute caAtributoEntidadNegocio = null;
                object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
                if (oAtributos.Length > 0)
                {
                    caAtributoEntidadNegocio = (CampoAttribute)oAtributos.ToList().Find(x => x.GetType() == typeof(CampoAttribute));
                }

                if (caAtributoEntidadNegocio == null || caAtributoEntidadNegocio.EsParametroSP)
                {
                    object oValor = piPropiedadEntidad.GetValue(tEntidadNegocio, null);
                    if (oValor != null)
                    {
                        string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
                        espSP.AgregarParametro(new MySqlParameter(sNombreParametro, oValor));
                    }
                }
            }

            //Agregar el Parametro para leer las alertas de la base de datos.
            MySqlParameter param = new MySqlParameter(NombreParametroAlerta, MySqlDbType.VarChar, 8000);
            param.Direction = ParameterDirection.Output;
            espSP.AgregarParametro(param);

            //Agregar el parámetro para leer el dato de resultado.
            MySqlParameter paramRet = new MySqlParameter(NombreParametroReturn, MySqlDbType.Int16);
            paramRet.Direction = ParameterDirection.ReturnValue;
            espSP.AgregarParametro(paramRet);

            #endregion

            //Inicializa los valores antes de ejecutar el storeprocedure
            this._valorReturn = null;
            this._mensajeAlerta = string.Empty;

            espSP.EjecutaSinResultado();
            this._mensajeAlerta = espSP.Parametros[NombreParametroAlerta].Value.ToString();
            this._valorReturn = (int?)espSP.Parametros[NombreParametroReturn].Value;

            espSP.Dispose();
        }

        /// <summary>
        /// Elimina un registro en la base de datos
        /// </summary>
        /// <param name="id">identificador unico del registro que se desea eliminar</param>
        public void Eliminar(int id)
        {
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Delete));

            #region Asignacion de parametros

            string sNombreParametro = string.Format("Id", id);
            espSP.AgregarParametro(new MySqlParameter(sNombreParametro, id));

            #endregion

            espSP.EjecutaSinResultado();
            espSP.Dispose();    //Cierra los accesos a la base de datos.
        }

        /// <summary>
        /// Elimina un registro en la base de datos con una llave compuesta.
        /// </summary>
        /// <param name="id1">Primer id para llaves compuestas del registro que se desea eliminar</param>
        /// /// <param name="id2">Segundo id para llaves compuestas del registro que se desea eliminar</param>
        public void Eliminar(int id1, int id2)
        {
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Delete));

            #region Asignacion de parametros

            string sNombreParametro1 = string.Format("Id1", id1);
            string sNombreParametro2 = string.Format("Id2", id2);

            espSP.AgregarParametro(new MySqlParameter(sNombreParametro1, id1));
            espSP.AgregarParametro(new MySqlParameter(sNombreParametro2, id2));
            #endregion

            espSP.EjecutaSinResultado();
            espSP.Dispose();//Cierra los accesos a la base de datos.
        }

        /// <summary>
        /// Elimina un registro en la base de datos, y espera un mensaje de regreso asi como un return
        /// </summary>
        /// <param name="id">identificador unico del registro que se desea eliminar</param>
        public void EliminarConRespuesta(int id)
        {
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Delete));

            #region Asignacion de parametros

            string sNombreParametro = string.Format("Id", id);
            espSP.AgregarParametro(new MySqlParameter(sNombreParametro, id));

            MySqlParameter paramRet = new MySqlParameter(NombreParametroReturn, MySqlDbType.Int16);
            paramRet.Direction = ParameterDirection.ReturnValue;
            espSP.AgregarParametro(paramRet);

            MySqlParameter paramAlert = new MySqlParameter(NombreParametroReturn,  MySqlDbType.VarChar, 8000);
            paramAlert.Direction = ParameterDirection.Output;
            espSP.AgregarParametro(paramAlert);

            #endregion

            espSP.EjecutaSinResultado();
            this._valorReturn = null;
            this._mensajeAlerta = string.Empty;

            this._valorReturn = (int?)espSP.Parametros[NombreParametroReturn].Value; ;
            this._mensajeAlerta = (string)espSP.Parametros[NombreParametroAlerta].Value.ToString();
            espSP.Dispose();    //Cierra los accesos a la base de datos.
        }

        /// <summary>
        /// Obtiene un listado de entides de negocio requerido.
        /// </summary>
        /// <param name="tEntidadNegocio">Informacion utilizada para filtrar la consulta.
        /// Si el valor es nulo no filtra por ese campo.</param>
        /// <returns>Listado de entidades de negocio</returns>
        public List<T> ObtenerListado(T tEntidadNegocio)
        {
            List<T> listResultado = new List<T>();
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Select));

            #region Asignacion de parametros

            foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
            {
                CampoAttribute caAtributoEntidadNegocio = null;
                object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
                if (oAtributos.Length > 0)
                {
                    caAtributoEntidadNegocio = (CampoAttribute)oAtributos.ToList().Find(x => x.GetType() == typeof(CampoAttribute));
                }

                if (caAtributoEntidadNegocio == null || caAtributoEntidadNegocio.EsParametroSP)
                {
                    object oValor = piPropiedadEntidad.GetValue(tEntidadNegocio, null);
                    if (oValor != null)
                    {
                        string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
                        espSP.AgregarParametro(new MySqlParameter(sNombreParametro, oValor));
                    }
                }
            }

            #endregion

            this.drEntidad = espSP.ObtenerReader();
            listResultado = ConversionesEntidadesNegocio<T>.ConvertirAListadoEntidadNegocio(drEntidad);

            espSP.Dispose();    //Cierra los accesos a la base de datos.

            return listResultado;
        }

        /// <summary>
        /// Obtiene un listado de entidades de Negocio seleccionables
        /// </summary>
        /// <param name="tEntidadNegocio">Informacion utilizada para filtrar la consulta.</param>
        /// <param name="opcion">Select en StoreProcedure</param>
        /// <returns>Listado de entidades de negocio</returns>
        public List<T> ObtenerListado(T tEntidadNegocio, long opcion)
        {
            List<T> listResultado = new List<T>();
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Select));

            #region Asignacion de parametros
            espSP.AgregarParametro(new MySqlParameter("OpcionSeleccion", opcion));
            foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
            {
                CampoAttribute caAtributoEntidadNegocio = null;
                object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
                if (oAtributos.Length > 0)
                {
                    caAtributoEntidadNegocio = (CampoAttribute)oAtributos.ToList().Find(x => x.GetType() == typeof(CampoAttribute));
                }

                if (caAtributoEntidadNegocio == null || caAtributoEntidadNegocio.EsParametroSP)
                {
                    object oValor = piPropiedadEntidad.GetValue(tEntidadNegocio, null);
                    if (oValor != null)
                    {
                        string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
                        espSP.AgregarParametro(new MySqlParameter(sNombreParametro, oValor));
                    }
                }
            }

            #endregion

            this.drEntidad = espSP.ObtenerReader();
            listResultado = ConversionesEntidadesNegocio<T>.ConvertirAListadoEntidadNegocio(drEntidad);

            espSP.Dispose();    //Cierra los accesos a la base de datos.

            return listResultado;
        }

        /// <summary>
        /// Obtiene la primer entidad de negocio que encuentre
        /// </summary>
        /// <param name="id">identificador único de la entidad de negocio</param>
        /// <returns>Entidad de negocio</returns>
        public T Obtener(T tEntidadNegocio)
        {
            T tUnaEntidadNegocio = new T();
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Select));

            #region Asignacion de parametros

            foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
            {
                CampoAttribute caAtributoEntidadNegocio = null;
                object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
                if (oAtributos.Length > 0)
                {
                    caAtributoEntidadNegocio = (CampoAttribute)oAtributos.ToList().Find(x => x.GetType() == typeof(CampoAttribute));
                }

                if (caAtributoEntidadNegocio == null || caAtributoEntidadNegocio.EsParametroSP)
                {
                    object oValor = piPropiedadEntidad.GetValue(tEntidadNegocio, null);
                    if (oValor != null)
                    {
                        string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
                        espSP.AgregarParametro(new MySqlParameter(sNombreParametro, oValor));
                    }
                }
            }

            #endregion

            this.drEntidad = espSP.ObtenerReader();

            tUnaEntidadNegocio = ConversionesEntidadesNegocio<T>.ConvertirUnaEntidadNegocio(drEntidad);

            espSP.Dispose();    //Cierra los accesos a la base de datos.

            return tUnaEntidadNegocio;
        }

        /// <summary>
        /// Obtiene la entidad de negocio por su identificador único
        /// </summary>
        /// <param name="id">Identificador único de la base de datos</param>
        /// <returns>Entidad de negocio</returns>
        public T Obtener(int id)
        {
            bool bHayID = false;
            T tUnaEntidadNegocio = new T();
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Select));

            #region Asignacion de parametros

            foreach (PropertyInfo piPropiedadEntidad in tUnaEntidadNegocio.GetType().GetProperties())
            {
                object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
                if (oAtributos.Length > 0)
                {
                    CampoAttribute caAtributoEntidadNegocio;

                    foreach (object unAtributo in oAtributos)
                    {
                        if (unAtributo.GetType() == typeof(CampoAttribute))
                        {
                            caAtributoEntidadNegocio = (CampoAttribute)unAtributo;
                            if (caAtributoEntidadNegocio.EsLlavePrimaria)
                            {
                                string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
                                espSP.AgregarParametro(new MySqlParameter(sNombreParametro, id));

                                bHayID = true;
                            }
                        }
                    }
                }
            }

            #endregion

            if (!bHayID)
            {
                // La entidad de negocio no tiene un atributo que especifique cual es su campo llave
                // por lo tanto manda una excepción.
                throw new SinIDException(tUnaEntidadNegocio.GetType().Name);
            }

            this.drEntidad = espSP.ObtenerReader();
            tUnaEntidadNegocio = ConversionesEntidadesNegocio<T>.ConvertirUnaEntidadNegocio(drEntidad);

            espSP.Dispose();    //Cierra los accesos a la base de datos.

            return tUnaEntidadNegocio;
        }

        /// <summary>
        /// Obtiene la información de la base de datos en un datatable
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio con la información deseada para ser filtrada</param>
        /// <returns>Resultado de la consulta como datatable</returns>
        public DataTable ObtenerDataTable(T tEntidadNegocio)
        {
            DataTable dtResultado = new DataTable();
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Select));

            #region Asignacion de parametros

            foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
            {
                CampoAttribute caAtributoEntidadNegocio = null;
                object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
                if (oAtributos.Length > 0)
                {
                    caAtributoEntidadNegocio = (CampoAttribute)oAtributos.ToList().Find(x => x.GetType() == typeof(CampoAttribute));
                }

                if (caAtributoEntidadNegocio == null || caAtributoEntidadNegocio.EsParametroSP)
                {
                    object oValor = piPropiedadEntidad.GetValue(tEntidadNegocio, null);
                    if (oValor != null)
                    {
                        string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
                        espSP.AgregarParametro(new MySqlParameter(sNombreParametro, oValor));
                    }
                }
            }

            #endregion
            dtResultado = espSP.ObtenerDataTable();
            espSP.Dispose();    //Cierra los accesos a la base de datos.

            return dtResultado;
        }

        /// <summary>
        /// Obtiene un DataTable de la entidad de negocio requerido.
        /// </summary>
        /// <param name="tEntidadNegocio">Informacion utilizada para filtrar la consulta.</param>
        /// <param name="opcion">Select en StoreProcedure</param>
        /// <returns>DataTable de la entidad de negocio</returns>
        public DataTable ObtenerDataTable(T tEntidadNegocio, int opcion)
        {
            DataTable dtResultado = new DataTable();
            EjecutaSP espSP = new EjecutaSP(this.ObtenerNombreSP(CatalogoDA<T>.AccionSP.Select));

            #region Asignacion de parametros
            espSP.AgregarParametro(new MySqlParameter("OpcionSeleccion", opcion));
            foreach (PropertyInfo piPropiedadEntidad in tEntidadNegocio.GetType().GetProperties())
            {
                CampoAttribute caAtributoEntidadNegocio = null;
                object[] oAtributos = piPropiedadEntidad.GetCustomAttributes(true);
                if (oAtributos.Length > 0)
                {
                    caAtributoEntidadNegocio = (CampoAttribute)oAtributos.ToList().Find(x => x.GetType() == typeof(CampoAttribute));
                }

                if (caAtributoEntidadNegocio == null || caAtributoEntidadNegocio.EsParametroSP)
                {
                    object oValor = piPropiedadEntidad.GetValue(tEntidadNegocio, null);
                    if (oValor != null)
                    {
                        string sNombreParametro = string.Format("{0}", piPropiedadEntidad.Name);
                        espSP.AgregarParametro(new MySqlParameter(sNombreParametro, oValor));
                    }
                }
            }

            #endregion
            dtResultado = espSP.ObtenerDataTable();
            espSP.Dispose();    //Cierra los accesos a la base de datos.

            return dtResultado;
        }

        /// <summary>
        /// Obtiene un DataTable de la entidad de negocio requerido.
        /// </summary>
        /// <param name="sInstruccion">Query enviado como cadena de caracteres</param>
        /// <returns>DataTable de la consulta realizada.</returns>
        public DataTable ObtenerDataTable(String sInstruccion)
        {
            DataTable dtResultado = new DataTable();
            EjecutaSP espSP = new EjecutaSP(sInstruccion);
            dtResultado = espSP.ObtenerDataTable(sInstruccion);
            espSP.Dispose();    //Cierra los accesos a la base de datos.

            return dtResultado;
        }        

        #endregion


    }
}

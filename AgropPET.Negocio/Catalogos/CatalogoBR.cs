using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

using AgroPET.Entidades.Base;
using AgroPET.Datos.Catalogos;
using InterfacesAgroPET.Catalogos;
using InterfacesAgroPET;
using AgropPET.Negocio.Eventos;


namespace AgropPET.Negocio.Catalogos
{
    public class CatalogoBR<T> : ICatalogoBase<T>, IListaGrid<T> where T : new()
    {
        #region Variables

        private int? _ultimoIdentity;

        private CatalogoDA<T> catalogoGenerico;

        private List<PropiedadCampoGrid> listaCamposGrid;

        #endregion

        #region Propiedades

        /// <summary>
        /// Obtiene el valor del último valor identity generado al guardar un registro.
        /// </summary>
        public int? UltimoIdentity
        {
            get { return _ultimoIdentity; }
        }

        /// <summary>
        /// Obtiene el valor que ha regresado el storeprocedure de la base de datos.
        /// </summary>
        public int? ValorReturnSP
        {
            get { return this.catalogoGenerico.ValorReturn; }
        }

        /// <summary>
        /// Obtiene el mensaje que ha regresado el storeprocedure de la base de datos.
        /// </summary>
        public string MensajeReturnSP
        {
            get { return this.catalogoGenerico.MensajeAlerta; }
        }

        /// <summary>
        /// Obtiene el resultado de ejecucion que regresa el storeprocedure de la base de datos.
        /// </summary>
        public string ResultadoejecucionSP
        {
            get { return this.catalogoGenerico.ResultadoEjecucion; }
        }
        #endregion

        #region Declaracion de eventos

        /// <summary>
        /// Evento que se ejecuta cuando se detecta un mensaje de alerta enviado por la base de datos
        /// al intentar guardar un registro.
        /// </summary>
        public event InformeEventHandler informeAlertaGuardar;

        /// <summary>
        /// Evento que se ejecuta cuando se detecta un mensaje de alerta enviado por la base de datos
        /// al intentar actualizar un registro
        /// </summary>
        public event InformeEventHandler informeAlertaActualizar;

        #region Metodos para ejecución de eventos

        /// <summary>
        /// Genera el evento para informar una alerta al guardar
        /// </summary>
        /// <param name="e">argumento del informe</param>
        protected void EnInformeAlertaGuardar(InformeEventArgs e)
        {
            InformeEventHandler infoHandler = this.informeAlertaGuardar;
            if (infoHandler != null)
            {
                infoHandler(this, e);
            }
        }

        /// <summary>
        /// Genera el evento para informar una alerta al actualizar
        /// </summary>
        /// <param name="e">argumento del informe</param>
        protected void EnInformeAlertaActualizar(InformeEventArgs e)
        {
            InformeEventHandler infoHandler = this.informeAlertaActualizar;
            if (infoHandler != null)
            {
                infoHandler(this, e);
            }
        }

        #endregion

        #endregion

        #region Metodos protegidos

        /// <summary>
        /// Método encargado de asegurar que no existan espacios en blanco al inicio y final de una cadena
        /// </summary>
        /// <param name="tEntidad">Entidad de negocio</param>
        /// <returns>Regresa la entidad de negocio con sus propiedades de tipo "string" sin espacios al inicio y/o al final</returns>
        protected virtual T QuitarEspacios(T tEntidad)
        {
            T tResultado = tEntidad;
            PropertyInfo[] propiedades = tResultado.GetType().GetProperties();

            foreach (PropertyInfo propiedad in propiedades)
            {
                if (propiedad.PropertyType == typeof(string) && propiedad.CanWrite)
                {
                    string valor = (string)propiedad.GetValue(tResultado, null);
                    if (valor != null)
                    {
                        valor = valor.Trim();
                        propiedad.SetValue(tResultado, valor, null);
                    }
                }
            }

            return tResultado;
        }

        #endregion

        #region ICatalogoBase<T> Members

        /// <summary>
        /// Obtiene el nombre del campo llave de la entidad de negocio.
        /// </summary>
        public string NombreCampoID
        {
            get { return this.catalogoGenerico.NombreCampoID; }
        }

        /// <summary>
        /// Inserta un registro en la base de datos
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio que se va a insertar</param>
        public virtual void Guardar(T tEntidadNegocio)
        {
            this.catalogoGenerico.Guardar(this.QuitarEspacios(tEntidadNegocio));
            ////this._ultimoIdentity = this.catalogoGenerico.ValorReturn;
            if (this.catalogoGenerico.MensajeAlerta != string.Empty && this.informeAlertaGuardar != null)
            {
                this._ultimoIdentity = null;

                //Envia evento del informe con la alerta generada.
                this.EnInformeAlertaGuardar(new InformeEventArgs(this.catalogoGenerico.MensajeAlerta));
            }
        }

        /// <summary>
        /// Actualiza un registro en la base de datos.
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio con la información a ser almacenada</param>
        public virtual void Actualizar(T tEntidadNegocio)
        {
            this.catalogoGenerico.Actualizar(this.QuitarEspacios(tEntidadNegocio));
            if (this.catalogoGenerico.MensajeAlerta != string.Empty && this.informeAlertaActualizar != null)
            {
                //Envia evento del informe con la alerta generada.
                //this.informeAlertaActualizar(this, new InformeEventAgrs(this.catalogoGenerico.MensajeAlerta));
                this.EnInformeAlertaActualizar(new InformeEventArgs(this.catalogoGenerico.MensajeAlerta));
            }
        }

        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único</param>
        public virtual void Eliminar(int id)
        {
            this.catalogoGenerico.Eliminar(id);
        }

        /// <summary>
        /// Elimina un registro de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único</param>
        public virtual void Eliminar(int id1, int id2)
        {
            this.catalogoGenerico.Eliminar(id1, id2);
        }

        /// <summary>
        /// Obtiene el listado de entidades de negocio.
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio con información para filtrar datos</param>
        /// <returns>Listado de entidades de negocio</returns>
        public virtual List<T> ObtenerListado(T tEntidadNegocio)
        {
            return this.catalogoGenerico.ObtenerListado(tEntidadNegocio);
        }

        /// <summary>
        /// Obtiene el listado de entidades de negocio.
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio con información para filtrar datos</param>
        /// <param name="opcion">Select a ejecutar</param>
        /// <returns>Listado de entidades de negocio</returns>
        public virtual List<T> ObtenerListado(T tEntidadNegocio, long opcion)
        {
            return this.catalogoGenerico.ObtenerListado(tEntidadNegocio, opcion);
        }

        /// <summary>
        /// Obtiene la primer entidad de negocio que se encuentre.
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio con información para filtrar datos</param>
        /// <returns>Primer entidad de negocio encontrada</returns>
        public virtual T Obtener(T tEntidadNegocio)
        {
            return this.catalogoGenerico.Obtener(tEntidadNegocio);
        }

        /// <summary>
        /// Obtiene una entidad de negocio por identificador único.
        /// </summary>
        /// <param name="id">Identificador único</param>
        /// <returns>Entidad de negocio</returns>
        public virtual T Obtener(int id)
        {
            return this.catalogoGenerico.Obtener(id);
        }

        /// <summary>
        /// Obtiene la información de la base de datos en un datatable
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio con la información deseada para ser filtrada</param>
        /// <returns>Resultado de la consulta como datatable</returns>
        public virtual DataTable ObtenerDataTable(T tEntidadNegocio)
        {
            return this.catalogoGenerico.ObtenerDataTable(tEntidadNegocio);
        }

        public virtual DataTable ObtenerDataTable(String sInstruccion)
        {
            return this.catalogoGenerico.ObtenerDataTable(sInstruccion);
        }

        ////public virtual void EjecutarProceso(T tEntidadNegocio)
        ////{
        ////    this.catalogoGenerico.EjecutaProceso(tEntidadNegocio);
        ////}

        #endregion

        #region IListaGrid<T> Members

        /// <summary>
        /// Método para obtener el listado completo de los campos que se deben mostrar en el grid.
        /// </summary>
        /// <returns>Listado de las propiedades de los campos para un grid</returns>
        public virtual List<PropiedadCampoGrid> ObtenerCamposMuestra()
        {
            T tTempo = new T(); //Se usa para conocer las propiedades de la entidad de negocio
            this.listaCamposGrid.Clear();

            foreach (PropertyInfo piEntidadNegocio in tTempo.GetType().GetProperties())
            {
                CampoAttribute unAtributo = (CampoAttribute)piEntidadNegocio.GetCustomAttributes(true).First(x => x.GetType() == typeof(CampoAttribute));

                if (unAtributo != null && unAtributo.EsCampoRetornoConsulta && !unAtributo.EsLlavePrimaria)
                {
                    PropiedadCampoGrid unCampo = new PropiedadCampoGrid();
                    unCampo.EncabezadoCampo = piEntidadNegocio.Name;
                    unCampo.NombreCampo = piEntidadNegocio.Name;

                    if ((piEntidadNegocio.PropertyType == typeof(Nullable<DateTime>)) ||
                         piEntidadNegocio.PropertyType == typeof(DateTime))
                    {
                        unCampo.FormatoColumna = "{0:d}";
                    }

                    this.listaCamposGrid.Add(unCampo);
                }
            }

            return this.listaCamposGrid;
        }

        /// <summary>
        /// Método para obtener el listado de las entidades de negocio para llenar un grid
        /// </summary>
        /// <param name="tFiltro">Entidad de negocio con la información para filtrar la consulta</param>
        /// <returns>Objeto compatible con el datasource para llenar un grid</returns>
        public virtual object ObtenerListaGrid(T tFiltro)
        {
            return this.catalogoGenerico.ObtenerListado(tFiltro);
        }

        /// <summary>
        /// Metodo para determinar si un parametro es editable.
        /// </summary>
        /// <param name="parametro">Id del parametro a evaluar</param>
        /// <returns>Regresa un booleano, true: es editable, false: no editable</returns>

        public bool EsParametroEditable(int parametro)
        {
            if (parametro >= 1000)
                return true;
            else
                return false;
        }

        #endregion

        #region Constructores

        /// <summary>
        /// Reglas de negocio para catálogos genéricos.
        /// </summary>
        public CatalogoBR()
        {
            this.catalogoGenerico = new CatalogoDA<T>();
            this.listaCamposGrid = new List<PropiedadCampoGrid>();
        }

        #endregion

    }
}

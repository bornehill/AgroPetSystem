using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InterfacesAgroPET.Catalogos
{
    /// <summary>
    /// Interface que contiene los metodos y propiedades básicos de un catalogo
    /// </summary>
    /// <typeparam name="T">Entidad de Negocio con la que se va a trabajar</typeparam>
    public interface ICatalogoBase<T>
    {
        #region Propiedades Base

        string NombreCampoID { get; }

        #endregion

        #region Funciones base

        /// <summary>
        /// Inserta un registro en la base de datos.
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio que contiene la información a ser guardada</param>
        void Guardar(T tEntidadNegocio);

        /// <summary>
        /// Actualiza la información en la base de datos tomando en cuenta el campo id como registro de búsqueda
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio que contiene la información a ser actualizada</param>
        void Actualizar(T tEntidadNegocio);

        /// <summary>
        /// Elimina un registro de la base de datos. Tomando el dato de id para buscar el registro a ser borrado.
        /// </summary>
        /// <param name="id">id del registro que se desea eliminar</param>
        void Eliminar(int id);

        /// <summary>
        /// Obtiene el listado de entidades de negocio. Filtrando por los campos con valores "no nulos"
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio con la información deseada para ser filtrada</param>
        /// <returns>Listado de las entidades de negocio</returns>
        List<T> ObtenerListado(T tEntidadNegocio);

        /// <summary>
        /// Obtiene una entidad de negocio. Sólo convierte el primer registro encontrado
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio con la información deseada para ser filtrada</param>
        /// <returns>Entidad de negocio</returns>
        T Obtener(T tEntidadNegocio);

        /// <summary>
        /// Obtiene la entidad de negocio por su identificador único
        /// </summary>
        /// <param name="id">Identificador único de la base de datos</param>
        /// <returns>Entidad de negocio</returns>
        T Obtener(int id);

        /// <summary>
        /// Obtiene la información de la base de datos en un datatable
        /// </summary>
        /// <param name="tEntidadNegocio">Entidad de negocio con la información deseada para ser filtrada</param>
        /// <returns>Resultado de la consulta como datatable</returns>
        DataTable ObtenerDataTable(T tEntidadNegocio);

        #endregion
    }
}

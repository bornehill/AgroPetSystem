using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesAgroPET.Catalogos
{
    /// <summary>
    /// Interfaz que declara las funciones para obtener un listado de llenado para un grid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IListaGrid<T> where T : new()
    {
        /// <summary>
        /// Método para obtener el listado completo de los campos que se deben mostrar en el grid.
        /// </summary>
        /// <returns>Listado de las propiedades de los campos para un grid</returns>
        List<PropiedadCampoGrid> ObtenerCamposMuestra();

        /// <summary>
        /// Método para obtener el listado de las entidades de negocio para llenar un grid
        /// </summary>
        /// <param name="tFiltro">Entidad de negocio con la información para filtrar la consulta</param>
        /// <returns>Objeto compatible con el datasource para llenar un grid</returns>
        object ObtenerListaGrid(T tFiltro);
    }
}

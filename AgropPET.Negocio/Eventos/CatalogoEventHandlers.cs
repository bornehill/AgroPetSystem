using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgropPET.Negocio.Eventos
{
    #region Declaracion de delegado

    /// <summary>
    /// Delegado para informe de avisos entre reglas de negocio y fron end de catalogos
    /// </summary>
    /// <param name="sender">Objeto que ejecuta el evento</param>
    /// <param name="e">Argumentos con la informacion a enviar a la capa de front end</param>

    public delegate void InformeEventHandler(object sender, InformeEventArgs e);

    #endregion

    /// <summary>
    /// Clase con la informacion que se desea mostrar en la capa front end
    /// </summary>

    public class InformeEventArgs : EventArgs
    {
        private string _informe;

        #region Propiedades

        public string Informe
        {
            get { return _informe; }
        }

        #endregion

        #region Constructores

        public InformeEventArgs(string sInforme)
        {
            this._informe = sInforme;
        }

        #endregion
    }
}

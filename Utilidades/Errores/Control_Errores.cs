using System;

namespace Utilidades.Errores
{
    public static class ControlErrores
    {
        #region Métodos
        /// <summary>
        /// Genera el error de argumentos
        /// </summary>
        /// <param name="mensaje">Mensaje de error</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void Generar_Error_Argumentos(string mensaje)
        {
            throw new ArgumentNullException(mensaje);
        }

        /// <summary>
        /// Genera el error de aplicación
        /// </summary>
        /// <param name="mensaje">Mensaje de error</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void Generar_Error(string mensaje)
        {
            throw new ApplicationException(mensaje);
        }
        #endregion
    }
}
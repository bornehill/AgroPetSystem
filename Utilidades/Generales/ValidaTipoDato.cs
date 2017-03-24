//==================================================================================
/*
 * Empresa:       Sinersys
 * Elaborado por: Luis A. Rubio Díaz
 * Fecha:         13/01/2012
 * Descripcion:   Clase comun de la solucion para validar la correspondencia de tipos de datos
 *                leidos desde los origenes de datos de archivos de Excel.
 *                
 * Modifico:      Hector Rodriguez.
 * Fecha:         13/01/2012
 * Descripcion:   Se agrego funcion EsFecha(string) y EsDecimal(string).
 *                
 * */
//==================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilidades.Generales
{
    public class ValidaTipoDato
    {
        /// <summary>
        /// Rutina para validar si un tipo de dato object contiene un dato que es valido como numerico.
        /// </summary>
        /// <param name="Valor">Recibe valor tipo Object</param>
        /// <returns>Regresa valor bool que indica si el valor recibido es Numerico</returns>
        public static bool EsNumerico(object Valor)
        {
            int Numero;
            try
            {
                Numero = int.Parse(Valor.ToString());

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Rutina para validar si un tipo de dato string(cadena) contiene un dato que es valido como numerico.
        /// </summary>
        /// <param name="Valor">Recibe valor tipo string</param>
        /// <returns>Regresa valor bool que indica si el valor recibido es Numerico</returns>
        public static bool EsNumerico(string Valor)
        {
            int Numero;

            return int.TryParse(Valor, out Numero);
        }

        /// <summary>
        /// Rutina para validar si un tipo de dato es de tipo numerico "short o int16"
        /// </summary>
        /// <param name="Valor">Valor a ser evaluado</param>
        /// <returns>True: cuando si es un número de tipo short, de lo contrario regresa false</returns>
        public static bool EsNumericoShort(object Valor)
        {
            short tempo;
            return short.TryParse(Valor.ToString(), out tempo);
        }

        /// <summary>
        /// Rutina para validar si una expresion de cadena representa una fecha/hora valida.
        /// </summary>
        /// <param name="ValorFechaHora">Recibe valor tipo string</param>
        /// <returns>Regresa valor bool que indica si el valor recibido es una fecha/hora valida</returns>
        public static bool EsFecha(string auxFecha)
        {
            try
            {
                DateTime fecha;
                return DateTime.TryParse(auxFecha, out fecha);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Metodo para validar Decimales.
        /// </summary>
        /// <param name="auxDecimal">String a evaluar</param>
        /// <returns>True: si es decimal</returns>
        public static bool EsDecimal(string auxDecimal)
        {
            try
            {
                decimal.Parse(auxDecimal);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Metodo para validar Booleanos
        /// </summary>
        /// <param name="auxDecimal">String a evaluar</param>
        /// <returns>True: si es decimal</returns>
        public static bool EsBool(string sValor)
        {
            try
            {
                bool.Parse(sValor);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

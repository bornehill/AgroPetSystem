//==================================================================================
/*
 * Empresa:       Sinersys
 * Elaborado por: Ezdrain Ruiz
 * Fecha:         11/01/2012
 * Descripcion:   Clase utilizada para convertir diferentes tipos de datos
 * 
 * Modificaciones/Agregados
 * Agregado
 * Empresa:       Sinersys
 * elaborado por: Luis Rubio
 * Fecha:         25/01/2012
 * Descripcion:   Se agrego la funcion ConvierteFecha para poder realizar la conversion de un string 
 *                especificado a un objeto datetime con formato de EUA.
 * 
 * */
//==================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Utilidades.Generales
{
    /// <summary>
    /// Realiza distintas conversiones de datos.
    /// </summary>
    public class Conversiones
    {
        #region Constantes

        public const string StringCero = "0";
        public const string StringUno = "1";

        public const string StringSI = "SI";
        public const string StringNO = "NO";

        public const string StringTrue = "TRUE";
        public const string StringFalse = "FALSE";

        #endregion

        #region Métodos públicos estáticos

        #region Descomponer una Cadena en 2 de una longitud especificada
        /// <summary>
        /// Obtiene la cadena especificada y la descompone en 2 cadenas de long. especificada.
        /// </summary>
        /// <returns>
        /// Regresa 2 cadenas de una longitud especificada, mediante parametros tipo Referencia <Ref>.
        /// </returns>
        public static void DivideCadenas(string sCadenaRecibida, int nLongCadenasRegreso, ref string sCadena1, ref string sCadena2,
            int nLadoCadena)
        {
            string sCad1 = string.Empty;
            string sCad2 = string.Empty;
            string sCadTemp = string.Empty;

            if (sCadenaRecibida.Trim() != "")
                if (nLadoCadena == 2) //Se regresan los datos que estan hacia la derecha de la cadena
                {
                    if (sCadenaRecibida.Trim().Length > (nLongCadenasRegreso * 2))
                    {
                        sCad2 = sCadenaRecibida.Substring(sCadenaRecibida.Length - nLongCadenasRegreso, nLongCadenasRegreso);
                        sCadTemp = sCadenaRecibida.Substring(0, sCadenaRecibida.Length - nLongCadenasRegreso);
                        sCadenaRecibida = sCadTemp;
                        sCad1 = sCadenaRecibida.Substring(sCadenaRecibida.Length - nLongCadenasRegreso, nLongCadenasRegreso);
                    }
                    else
                        if (sCadenaRecibida.Trim().Length == (nLongCadenasRegreso * 2))
                        {
                            sCad1 = sCadenaRecibida.Substring(0, nLongCadenasRegreso);
                            sCad2 = sCadenaRecibida.Substring(nLongCadenasRegreso);
                        }
                        else
                            if (sCadenaRecibida.Trim().Length > nLongCadenasRegreso)
                            {
                                sCad1 = sCadenaRecibida.Substring(0, nLongCadenasRegreso);
                                sCad2 = sCadenaRecibida.Substring(nLongCadenasRegreso);
                            }
                            else
                            {
                                sCad1 = sCadenaRecibida.Trim();
                                sCad2 = "";
                            }
                }
                else //Se regresan los datos que estan hacia la izquierda de la cadena
                {
                    if (sCadenaRecibida.Trim().Length >= (nLongCadenasRegreso * 2))
                    {
                        sCad1 = sCadenaRecibida.Substring(0, nLongCadenasRegreso);
                        sCad2 = sCadenaRecibida.Substring(nLongCadenasRegreso, nLongCadenasRegreso);
                    }
                    else
                        if (sCadenaRecibida.Trim().Length > nLongCadenasRegreso)
                        {
                            sCad1 = sCadenaRecibida.Substring(0, nLongCadenasRegreso);
                            sCad2 = sCadenaRecibida.Substring(nLongCadenasRegreso);
                        }
                        else
                        {
                            sCad1 = sCadenaRecibida.Trim();
                            sCad2 = "";
                        }
                }
            else
            {
                sCad1 = "";
                sCad2 = "";
            }

            sCadena1 = sCad1.Trim();
            sCadena2 = sCad2.Trim();
        }
        #endregion

        #region Conversiones entre cadenas

        /// <summary>
        /// Obtiene la cadena con unicamente caracteres númericos y letras, quitando caracteres especiales.
        /// </summary>
        /// <param name="sDato">Cadena que se desea obtener unicamente letras y números</param>
        /// <returns>Cadena con letras y números únicamente</returns>
        public static string ExtraerSoloLetrasNumeros(string sDato)
        {
            string resultado = string.Empty;
            if (sDato != null)
            {
                string cadenaCaracteresValidos = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                char[] caracteresValidos = cadenaCaracteresValidos.ToCharArray();
                sDato = sDato.ToUpper();
                foreach (char caracter in sDato)
                {
                    if (caracteresValidos.Contains(caracter))
                        resultado += caracter;
                }
            }
            else
                resultado = null;

            return resultado;
        }

        #endregion

        #region Conversiones de Cadenas a Números

        /// <summary>
        /// Convierte una cadena de texto a un valor decimal, si no se puede regresa el valor nulo
        /// </summary>
        /// <param name="sDato">Valor a ser convertido</param>
        /// <returns>Valor convertido, null = cuando la conversión no es posible</returns>
        public static decimal? StringADecimal(string sDato)
        {
            decimal sResultado;

            if (decimal.TryParse(sDato, out sResultado))
                return sResultado;

            return null;
        }

        public static short? StringAShort(string sDato)
        {
            short resultado;

            if (short.TryParse(sDato, out resultado))
                return resultado;

            return null;
        }

        /// <summary>
        /// Convierte una cadena de texto con formato de moneda a un valor decimal, si no se puede convertir, regresa el valor nulo.
        /// </summary>
        /// <param name="sDato">Valor a ser convertido</param>
        /// <returns>Valor convertido, null = cuendo la conversión no es posible</returns>
        public static decimal? MonedaADecimal(string sDato)
        {
            decimal resultado;

            if (decimal.TryParse(sDato, NumberStyles.Currency, System.Globalization.CultureInfo.CurrentCulture, out resultado))
                return resultado;

            return null;
        }

        /// <summary>
        /// Convierte una cadena de texto a un valor entero, si no se puede regresa el valor nulo
        /// </summary>
        /// <param name="sDato">Valor a ser convertido</param>
        /// <returns>Valor convertido, null= cuando la conversión no es posible</returns>
        public static int? SringAInt(string sDato)
        {
            int sResultado;

            if (int.TryParse(sDato, out sResultado))
                return sResultado;

            return null;
        }

        /// <summary>
        /// Convierte una cadena de texto en formato de porcentaje a decimal,
        /// si no puede realizar la conversión el valor de resultado es null
        /// </summary>
        /// <param name="sDato">Valor a ser convertido</param>
        /// <returns>Valor en decimal, null = cuando la conversión no es posible</returns>
        public static decimal? StringPorcentajeADecimal(string sDato)
        {
            decimal sResultado;

            if (decimal.TryParse(sDato.Replace("%", ""), out sResultado))
                return sResultado / 100;

            return null;
        }

        #endregion

        #region Conversiones boolean

        /// <summary>
        /// Convierte un valor de tipo string "0" o "1" a un valor Boolean
        /// </summary>
        /// <param name="sDato">Dato a convertir</param>
        /// <returns>1 = True, 0 = False, null = valor no válido</returns>
        public static bool? StringCeroUnoABoolean(string sDato)
        {
            if (sDato == StringCero)
                return false;

            if (sDato == StringUno)
                return true;

            return null;
        }

        /// <summary>
        /// convierte un valor de tipo string con un texto (true, false, si, no, 0, 1) a un tipo boolean
        /// </summary>
        /// <param name="sDato">Dato a convertir</param>
        /// <returns>true = ["true", "si", "0"], false = ["false", "no", "1"], null = cualquier otro valor</returns>
        public static bool? StringTextoABoolean(string sDato)
        {
            //Modificación. Se convierte el parámetro en mayúsculas (si aplica) dado que las constantes con las que se comparan
            //son en mayúsculas
            string vls_Dato = sDato == null ? null : sDato.ToUpper();
            if ((vls_Dato == StringCero || vls_Dato == StringNO || vls_Dato == StringFalse))
                return false;

            if ((vls_Dato == StringUno || vls_Dato == StringSI || vls_Dato == StringTrue))
                return true;

            return null;
        }

        /// <summary>
        /// Convierte un valor de tipo string "0" o "1" a un string "SI o "NO"
        /// </summary>
        /// <param name="sDato">Dato a convertir</param>
        /// <returns>1 = SI, 0 = NO, null = Valor no válido</returns>
        public static string StringCeroUnoASI_NO(string sDato)
        {
            if (sDato == StringCero)
                return StringNO;

            if (sDato == StringUno)
                return StringSI;

            return null;
        }

        /// <summary>
        /// Realiza la conversión de un dato booleano a texto Si o No
        /// </summary>
        /// <param name="valor">Valor booleano a convertir</param>
        /// <returns>True = Si, Flase = No</returns>
        public static string ConvertirBoolASiNo(bool valor)
        {
            return (valor ? StringSI : StringNO);
        }

        /// <summary>
        /// Realiza la conversión de un dato booleano "True o False" a Si o No
        /// </summary>
        /// <param name="valor">Valor booleano en texto "True o False"</param>
        /// <returns>True = "Si", cualquier otro valor = "False"</returns>
        public static string ConvertirBoolASiNo(string valor)
        {
            return (valor.ToUpper() == StringTrue ? StringSI : StringNO);
        }

        /// <summary>
        /// Realiza la conversión de un dato booleano nulleable a Si o No. Cuando Valor es "null" regresa de igual manera "null"
        /// </summary>
        /// <param name="valor">Valor booleano nulleable a convertir</param>
        /// <returns>True = Si, False = No, Null = Null</returns>
        public static string ConvertirBoolNullASiNo(bool? valor)
        {
            if (valor.HasValue)
            {
                return ConvertirBoolASiNo(valor.Value);
            }

            return null;
        }

        /// <summary>
        /// Realiza la conversión de un texto "Si o No" a True o False
        /// </summary>
        /// <param name="valor">Texto de "Si o No"</param>
        /// <returns>Si = True, cualqueir otro valor = false</returns>
        public static bool ConvertirSiNoABool(string valor)
        {
            return (valor.ToUpper() == StringSI ? true : false);
        }

        /////////// <summary>
        /////////// Realiza la conversion de 1 o 0 a Activo o No Activo.
        /////////// </summary>
        /////////// <param name="valor">Valor 1 o 0</param>
        /////////// <return> String = Activo, No Activo</return>
        ////////public static string Convertir_1_0_Estatus(int nValor)
        ////////{
        ////////}

        /////////// <summary>
        /////////// Realiza la conversion de Activo o No Activo a 1 o 0.
        /////////// </summary>
        /////////// <param name="valor">Valor: Activo o No Activo</param>
        /////////// <return> Numerico = 1 o 0</return>
        ////////public static string Convertir_Estatus a_1_0(string sEstatus)
        ////////{
        ////////}


        #endregion

        #region Convertir un string a un datetime con formato de EUA
        /// <summary>
        /// Realiza la conversion de una cadena con formato fecha a un objeto datetime
        /// </summary>
        /// <param name="strFecha">Recibe una cadena con formato de fecha valida</param>
        /// <returns>Si recibe una fecha correcta regresa un datetime con la fecha, sino regresa null </returns>       
        public static DateTime? ConvierteFecha(string strFecha)
        {
            DateTime dFecha;
            try
            {
                IFormatProvider ifpCultura = new System.Globalization.CultureInfo("en-US", true);
                dFecha = DateTime.Parse(strFecha.Trim(), ifpCultura, DateTimeStyles.None);

                return dFecha;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Intenta convertir una cadena a fecha (current culture)
        /// </summary>
        /// <param name="sDato">Cadena de tipo fecha</param>
        /// <returns></returns>
        public static DateTime? StingADateTime(string sDato)
        {
            DateTime fecha;
            if (DateTime.TryParse(sDato, out fecha))
                return fecha;

            return null;
        }

        /// <summary>
        /// Intenta convertir una cadena a una fecha segun un formato dado
        /// </summary>
        /// <param name="sDato">Cadena de tipo fecha</param>
        /// <param name="formato">formato como se debe leer la fecha</param>
        /// <returns>Dato convertido en tipo de fecha. Regresa null cuando no es posible la conversión.</returns>
        public static DateTime? StingADateTime(string sDato, string formato)
        {
            DateTime fecha;
            System.Globalization.DateTimeFormatInfo info = new DateTimeFormatInfo();

            if (DateTime.TryParseExact(sDato, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                return fecha;

            return null;
        }

        /// <summary>
        /// Realiza la conversión de una fecha con null a una cadena de texto en formato corto
        /// </summary>
        /// <param name="dFecha">Valor de fecha null</param>
        /// <returns>Regresa la fecha convertida, si no se puede regresa una cadena vacía</returns>
        public static string ConvierteFechaNullAStringCorto(DateTime? dFecha)
        {
            if (dFecha.HasValue)
                return dFecha.Value.ToShortDateString();

            return string.Empty;
        }

        /// <summary>
        /// Obtiene la hora de una de una fecha dada.
        /// </summary>
        /// <param name="dFecha">Valor de fecha null</param>
        /// <returns>Regresa la hora en formato corto. Si no se puede extraer regresa una cadena vacía</returns>
        public static string ExtraeHora(DateTime? dFecha)
        {
            if (dFecha.HasValue)
                return dFecha.Value.ToShortTimeString();

            return string.Empty;
        }

        /// <summary>
        /// Obtiene la hora de una de una fecha dada en formato de 24 horas
        /// </summary>
        /// <param name="dFecha">Valor de fecha null</param>
        /// <returns>Regresa la hora en formato corto. Si no se puede extraer regresa una cadena vacía</returns>
        public static string ExtraeHora24(DateTime? dFecha)
        {
            if (dFecha.HasValue)
                return dFecha.Value.ToString("HH:mm");

            return string.Empty;
        }

        #endregion

        #region Asignacion de valores de clases

        /// <summary>
        /// Asigna los valores de las propiedades de una clase base a una clase derivada.
        /// </summary>
        /// <typeparam name="B">Tipo de clase base</typeparam>
        /// <typeparam name="D">Tipo de clase derivada (de la clase base)</typeparam>
        /// <param name="tBase">Clase base con la información que se va a asignar a la derivada</param>
        /// <param name="tDerivada">Clase derivada que va a recibir la información de la clase base</param>
        /// <exception cref="ArgumentException">Cuando la clase derivada no contiene el tipo de la clase base</exception>
        public static void AsignarValoresBaseADerivada<B, D>(B tBase, ref D tDerivada)
        {
            //if (tDerivada.GetType().BaseType.Equals(tBase.GetType()))
            if (tDerivada is B)
            {
                foreach (System.Reflection.PropertyInfo piBase in tBase.GetType().GetProperties())
                {
                    if (piBase.CanWrite)
                    {
                        System.Reflection.PropertyInfo piDerivada = tDerivada.GetType().BaseType.GetProperty(piBase.Name);
                        piDerivada.SetValue(tDerivada, piBase.GetValue(tBase, null), null);
                    }
                }
            }
            else
                throw new ArgumentException("La clase derivada y la clase base son de diferente tipo.");
        }

        /// <summary>
        /// Asigna los valores de las propiedades de una clase derivada a una clase base.
        /// </summary>
        /// <typeparam name="D">Tipo de clase derivada (de la clase base)</typeparam>
        /// <typeparam name="B">Tipo de clase base</typeparam>
        /// <param name="tDerivada">Clase derivada que va a recibir la información de la clase base</param>
        /// <param name="tBase">Clase base con la información que se va a asignar a la derivada</param>
        /// <exception cref="ArgumentException">Cuando la clase derivada no contiene el tipo de la clase base</exception>
        public static void AsignarValoresDerivadaABase<D, B>(D tDerivada, ref B tBase)
        {
            //if (tDerivada.GetType().BaseType.Equals(tBase.GetType()))
            if (tDerivada is B)
            {
                foreach (System.Reflection.PropertyInfo piBase in tBase.GetType().GetProperties())
                {
                    if (piBase.CanWrite)
                    {
                        System.Reflection.PropertyInfo piDerivada = tDerivada.GetType().BaseType.GetProperty(piBase.Name);
                        piBase.SetValue(tBase, piDerivada.GetValue(tDerivada, null), null);
                    }
                }
            }
            else
                throw new ArgumentException("La clase derivada y la clase base son de diferentes tipos.");
        }

        /// <summary>
        /// Método que crea una clase "Derivada" iniciando sus propiedades con los valores de la clase "Base"
        /// </summary>
        /// <typeparam name="B">Tipo de clase base</typeparam>
        /// <typeparam name="D">Tipo de clase derivada de "B"</typeparam>
        /// <param name="tBase">Clase base con la información que se le va asignar a la derivada</param>
        /// <returns></returns>
        public static D ConviertePropiedadesBaseADerivada<B, D>(B tBase)
        {
            Type derivada = typeof(D);
            D resultado = (D)Activator.CreateInstance(derivada);
            if (derivada.IsSubclassOf(typeof(B)) &&
                (tBase.GetType() == typeof(B) || tBase.GetType().IsSubclassOf(typeof(B))))
            {
                foreach (System.Reflection.PropertyInfo piBase in tBase.GetType().GetProperties())
                {
                    if (piBase.CanWrite)
                    {
                        System.Reflection.PropertyInfo piDerivada = derivada.GetProperty(piBase.Name);
                        if (piDerivada != null)
                            piDerivada.SetValue(resultado, piBase.GetValue(tBase, null), null);
                    }
                }
            }
            else
                throw new ArgumentException("La clase derivada y la clase base son de diferente tipo.");

            return resultado;
        }

        /// <summary>
        /// Metodo para convertir una lista generica a un DataTable incluye Objetos Nullable 
        /// en base a las PROPIEDADES de la clase.
        /// </summary>
        /// <typeparam name="T">Tipo generico</typeparam>
        /// <param name="data">Lista de objetos</param>
        /// <returns></returns>
        public DataTable ToDataTable<T>(IList<T> data)
        {
            return ToDataTable<T>(data, (int?) null);
        }

        /// <summary>
        /// Metodo para convertir una lista generica a un DataTable incluye Objetos Nullable 
        /// sin necesidad de incluir una lista de atributos para las columnas
        /// </summary>
        /// <typeparam name="T">Tipo generico</typeparam>
        /// <param name="data">Lista de objetos</param>
        /// <param name="startIndexColumnName">Indica la posición de inicio que tomará el nombre de columna</param>
        /// <returns></returns>
        public DataTable ToDataTable<T>(IList<T> data, int? startIndexColumnName)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in props)
            {
                string vls_PropName = 
                    ((startIndexColumnName != null) && (prop.Name.Length > (int)startIndexColumnName)) ? 
                        prop.Name.Substring((int)startIndexColumnName) : 
                        prop.Name;
                if (prop.PropertyType.IsValueType)
                    table.Columns.Add(vls_PropName, Nullable.GetUnderlyingType(prop.PropertyType));
                else
                    table.Columns.Add(vls_PropName, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        /// <summary>
        /// Metodo para convertir una lista generica a un DataTable incluye Objetos Nullable
        /// Los campos de la entidad correspondiente deben soportar valores nulos.
        /// </summary>
        /// <typeparam name="T">Tipo generico</typeparam>
        /// <param name="data">Lista de objetos</param>
        /// <param name="startIndexColumnName">Indica la posición de inicio que tomará el nombre de columna</param>
        /// <returns></returns>
        public DataTable ToDataTable<T>(IList<T> data, SortedList<int, AtributoDataTable> atributos)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            SortedList<int, System.Reflection.PropertyInfo> propsVisibles = new SortedList<int,System.Reflection.PropertyInfo>();
            DataTable table = new DataTable();
            int vln_TotalCampos = 0;

            //Obtenemos las propiedades de la entidad y las agregamos como columnas si son visibles
            foreach (KeyValuePair<int, AtributoDataTable> atributo in
                atributos.Where<KeyValuePair<int, AtributoDataTable>>(t => t.Value.b_IsVisible).OrderBy<KeyValuePair<int, AtributoDataTable>, int>(t => t.Key))
            {
                PropertyDescriptor prop = props.Find(atributo.Value.s_NombreColumna, true);
                if (prop != null)
                {
                    string vls_PropName = atributo.Value.s_TituloColumna;
                    propsVisibles.Add(propsVisibles.Count + 1, prop.ComponentType.GetProperty(prop.Name));

                    if (prop.PropertyType.IsValueType)
                        table.Columns.Add(vls_PropName, Nullable.GetUnderlyingType(prop.PropertyType));
                    else
                        table.Columns.Add(vls_PropName, prop.PropertyType);

                    vln_TotalCampos++;
                }
            }
            

            object[] values;

            if (vln_TotalCampos > 0)
                values = new object[vln_TotalCampos];
            else
                values = null;

            if (vln_TotalCampos > 0)
            {
                //Obtenemos las propiedades de la entidad y las agregamos como columnas si son visibles

                foreach (T item in data)
                {
                    vln_TotalCampos = 0;

                    foreach (KeyValuePair<int, System.Reflection.PropertyInfo> prop in 
                        propsVisibles.OrderBy<KeyValuePair<int, System.Reflection.PropertyInfo>, int>(t => t.Key))
                    {
                        values[vln_TotalCampos] = prop.Value.GetValue(item, null);
                        vln_TotalCampos++;
                    }

                    table.Rows.Add(values);
                }
            }
            return table;
        }
                
        #endregion

        #region Numeros a Letras

        public string Numerosenletras(string num, string NombreMoneda, string AbreviaturaMoneda)
        {

            string res, dec = "";
            Int64 entero;
            int decimales;
            string decim = "";
            double nro;
            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2)); // Original
            decim = Math.Round(nro - entero, 2).ToString();

            if (decimales > 0)
            {
                //dec = " " + NombreMoneda.Trim() + " " + decimales.ToString() + "/100" + " " + AbreviaturaMoneda.Trim();
                dec = " " + NombreMoneda.Trim() + " " + decim.ToString().Substring(2, 2) + "/100" + " " + AbreviaturaMoneda.Trim();
            }
            else
            {
                dec = " " + NombreMoneda.Trim() + " 00/100" + " " + AbreviaturaMoneda.Trim();
            }
            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }
            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }
            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }

            return Num2Text;

        }

        #endregion

        #region Obtener Fecha de Inicio y Fin de una Semana y Año especificados.

        public static DateTime ObtenerInicioSemana(int nSemana, int nAnio)
        {
            DateTime dtStDateY = new DateTime(nAnio, 1, 1);
            DateTime dtInidate;
            int day = (int)dtStDateY.DayOfWeek - 1;
            int delta = (day < 4 ? -day : 7 - day) + 7 * (nSemana - 1);

            dtInidate = dtStDateY.AddDays(delta);

            return dtInidate;
        }

        public static DateTime ObtenerFinSemana(int nSemana, int nAnio)
        {
            DateTime dtFinDate = ObtenerInicioSemana(nSemana, nAnio);
            dtFinDate = dtFinDate.AddDays(6);

            return dtFinDate;
        }

        public static int ObtenNumeroSemana(DateTime fecha)
        {
            Calendar calendario = CultureInfo.CurrentCulture.Calendar;
            return calendario.GetWeekOfYear(fecha, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);
        }

        #endregion


        #region Convertir un objeto a string XML
        public static string ConvertirObjetoAString<T>(T Obj)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T)); 
            XmlSerializerNamespaces xmlnsEmpty = new XmlSerializerNamespaces(new[] { new XmlQualifiedName(string.Empty, string.Empty), }); 
            StringWriter stringWriter = new StringWriter(); 
            xmlSerializer.Serialize(stringWriter, Obj, xmlnsEmpty); 
            return stringWriter.ToString(); 
        }
        #endregion

        #region Convertir un string XML a Objeto
        public static T ConvertirStringAObjeto<T>(String xml)
        {
            T returnedXmlClass = default(T);

            XmlSerializer ser;
            ser = new XmlSerializer(typeof(T));
            StringReader stringReader;
            stringReader = new StringReader(xml);
            XmlTextReader xmlReader;
            xmlReader = new XmlTextReader(stringReader);
            object obj;
            obj = ser.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();
            returnedXmlClass = (T)obj;

            return returnedXmlClass;
        } 
        #endregion
        #endregion
    }

    /// <summary>
    /// Clase auxiliar para convertir un objeto List a un DataTable según una lista de parámetros
    /// </summary>
    public class AtributoDataTable
    {
        #region Campos

        private string _s_NombreColumna;
        private bool _b_IsVisible;
        private string _s_TituloColumna;
        private int _i_Orden;

        #endregion Campos

        #region Propiedades

        public string s_NombreColumna
        {
            get { return this._s_NombreColumna; }
        }

        public string s_TituloColumna
        {
            get { return this._s_TituloColumna; }
        }

        public bool b_IsVisible
        {
            get { return this._b_IsVisible; }
        }

        public int i_Orden
        {
            get { return this._i_Orden; }
        }

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor para indicar la funcionalidad del campo
        /// </summary>
        public AtributoDataTable()
        {
            this._s_NombreColumna = "";
            this._s_TituloColumna = "";
            this._b_IsVisible = false;
            this._i_Orden = 0;
        }

        /// <summary>
        /// Constructor para indicar la funcionalidad del campo
        /// </summary>
        /// <param name="vps_NombreColumna">Nombre de la columna del grid.</param>
        /// <param name="vps_TituloColumna">Título que tomaría la columna del grid, por default sería el nombre de la columna.</param>
        /// <param name="vpb_IsVisible">Indica si esa columna es visible al momento de generar el DataTable</param>
        public AtributoDataTable(string vps_NombreColumna, int vpn_Orden,
            string vps_TituloColumna = null,
            bool vpb_IsVisible = true,
            int vpn_StartColumnName = 0            
            )
        {
            this._s_NombreColumna = vps_NombreColumna;
            if (vps_TituloColumna == null)
                if (vps_NombreColumna.Length > vpn_StartColumnName)
                    this._s_TituloColumna = vps_NombreColumna.Substring(vpn_StartColumnName);
                else
                    this._s_TituloColumna = vps_NombreColumna;
            else
                this._s_TituloColumna = vps_TituloColumna;
            this._b_IsVisible = vpb_IsVisible;
            this._i_Orden = vpn_Orden;
        }

        #endregion Constructores
    }

    /// <summary>
    /// Clase auxiliar para convertir un objeto List a un DataTable según una lista de parámetros
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AtributosDataTable<T> where T : new()
    {
        #region Campos

        private List<AtributoDataTable> _atributos = new List<AtributoDataTable>();

        private SortedList<int, AtributoDataTable> _atributosOrdenados = new SortedList<int, AtributoDataTable>();

        #endregion Campos

        #region Propiedades

        public List<AtributoDataTable> atributos
        {
            get { return this._atributos; }
        }

        public SortedList<int, AtributoDataTable> atributosOrdenados
        {
            get { return this._atributosOrdenados; }
        }

        #endregion Propiedades

        #region Constructores

        /// <summary>
        /// Constructor. Genera una lista de atributos según los campos de la entidad Base
        /// </summary>
        public AtributosDataTable()
        {
            T vlo_Temp = new T();

            //Completa la lista de atributos según los campos de la entidad base
            foreach (System.Reflection.PropertyInfo piBase in vlo_Temp.GetType().GetProperties())
            {
                if (atributos.Count(t => t.s_NombreColumna.Equals(piBase.Name)) == 0)
                {
                    _atributos.Add(new AtributoDataTable(piBase.Name, 0));                    
                    _atributosOrdenados.Add(_atributosOrdenados.Count + 1, new AtributoDataTable(piBase.Name, 0));
                }
            }
        }

        /// <summary>
        /// Constructor. Genera una lista de atributos según la lista de parámetros y los campos de la entidad Base
        /// </summary>
        /// <param name="vpo_Atributo">Lista de Atributos que se tomará para generar el DataTable</param>
        /// <param name="vpn_StartColumnName">Indica la posición inicial de la cual se tomara el nombre de la columna 
        /// para el título de la columna en el DataTable</param>
        public AtributosDataTable(
            List<AtributoDataTable> vpo_Atributo, 
            int vpn_StartColumnName,
            bool vpb_VisibleDefault)
        {
            //Recorremos los atributos que se hayan declarado
            foreach (AtributoDataTable attr in vpo_Atributo)
            {
                _atributos.Add(attr);
            }

            T vlo_Temp = new T();

            //Completar la lista de atributos según la entidad base y los atributos que no se hayan agregado previamente
            foreach (System.Reflection.PropertyInfo piBase in vlo_Temp.GetType().GetProperties())
            {
                if (atributos.Count(t => t.s_NombreColumna.Equals(piBase.Name)) == 0)
                {
                    _atributos.Add(new AtributoDataTable(
                        piBase.Name,
                        vpn_StartColumnName: vpn_StartColumnName,
                        vpb_IsVisible: vpb_VisibleDefault,
                        vpn_Orden: 0));
                }
            }

            //Primero agregamos a la lista ordenada los elementos que si tienen definido un campo Orden
            foreach (AtributoDataTable attr in 
                atributos.Where<AtributoDataTable>(t => t.i_Orden > 0).OrderBy<AtributoDataTable, int>(x => x.i_Orden).ThenBy<AtributoDataTable, string>(y => y.s_TituloColumna))
            {
                _atributosOrdenados.Add(_atributosOrdenados.Count + 1, attr);
            }

            //Primero agregamos a la lista ordenada los elementos que si tienen definido un campo Orden
            foreach (AtributoDataTable attr in
                atributos.Where<AtributoDataTable>(t => t.i_Orden == 0).OrderBy<AtributoDataTable, string>(y => y.s_TituloColumna))
            {
                _atributosOrdenados.Add(_atributosOrdenados.Count + 1, attr);
            }

        }

        /// <summary>
        /// Constructor. Genera una lista de atributos según la lista de parámetros y los campos de la entidad Base. Muestra todas las columnas
        /// </summary>
        /// <param name="vpo_Atributo">Lista de Atributos que se tomará para generar el DataTable</param>
        public AtributosDataTable(List<AtributoDataTable> vpo_Atributo) : this(vpo_Atributo, 0, true)
        {

        }

        /// <summary>
        /// Constructor. Genera una lista de atributos según la lista de parámetros y los campos de la entidad Base
        /// </summary>
        /// <param name="vpo_Atributo">Lista de Atributos que se tomará para generar el DataTable</param>
        /// <param name="vpb_VisibleDefault">Asigna el valor a la propiedad Visible que tendrán los campos por default en caso de no
        /// ser especificados en la lista de atributos.</param>
        public AtributosDataTable(List<AtributoDataTable> vpo_Atributo, bool vpb_VisibleDefault)
            : this(vpo_Atributo, 0, vpb_VisibleDefault)
        {

        }        

        #endregion Constructores

    }
}

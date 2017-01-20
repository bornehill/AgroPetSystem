using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

using System.Web.UI.WebControls;

/*Requeridas para AsString*/
using System.IO;
using System.Xml;
/*-----------------------*/

/*Requeridas para ObjectToXML*/
using System.Xml.Serialization;

/*-----------------------*/
using System.Windows.Forms;
using Utilidades.Controles;

namespace Utilidades.Extensiones
{
    using Enumerados;

    /// <summary>
    /// Clase que contiene los métodos de extensión más usados en la toda la solución
    /// </summary>
    public static class Extensiones
    {
        /// <summary>
        /// Carga la información de un DataTable a un ComboBox
        /// </summary>
        /// <param name="combo">ComboBox a cargar</param>
        /// <param name="datos">DataTable de datos a cargar</param>
        /// <param name="campoValor">Nombre del campo valor</param>
        /// <param name="campoDescripcion">Nombre del campo descripción</param>
        /// <param name="elementoInicial">Determina si se incluye el elemento selecciona</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void ToComboBoxExtended(this ComboBox combo, DataTable datos, string campoValor, string campoDescripcion, bool elementoInicial)
        {
            // Validar que los campos existen
            if (!datos.Columns.Contains(campoValor))
            {
                Errores.ControlErrores.Generar_Error_Argumentos(string.Format("Error el miembro '{0}' no existe en la colección", campoValor));
            }

            if (!datos.Columns.Contains(campoDescripcion))
            {
                Errores.ControlErrores.Generar_Error_Argumentos(string.Format("Error el miembro '{0}' no existe en la colección", campoValor));
            }

            combo.Items.Clear();

            CustomListItem objItem = null;

            // Agregar el elemento inicial si es requerido
            if (elementoInicial)
            {
                objItem = new CustomListItem { Valor = "0", Descripcion = "- Selecciona -" };
                combo.Items.Add(objItem);
                objItem = null;
            }

            combo.ValueMember = campoValor;
            combo.DisplayMember = campoDescripcion;

            foreach (DataRow drElemento in datos.Rows)
            {
                objItem = new CustomListItem
                    {
                        Valor = drElemento[campoValor].ToString(),
                        Descripcion = drElemento[campoDescripcion].ToString()
                    };

                combo.Items.Add(objItem);

                objItem = null;
            }

            // Seleccionar el primer elemento
            if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Carga la información de una lista a un ComboBox
        /// </summary>
        /// <typeparam name="T">Tipo de datos</typeparam>
        /// <param name="combo">ComboBox a cargar</param>
        /// <param name="datos">Lista de datos a cargar</param>
        /// <param name="campoValor">Nombre del campo valor</param>
        /// <param name="campoDescripcion">Nombre del campo descripción</param>
        /// <param name="elementoInicial">Determina si se incluye el elemento Selecciona</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void ToComboBoxExtended<T>(this ComboBox combo, List<T> datos, string campoValor, string campoDescripcion, bool elementoInicial)
        {
            PropertyDescriptorCollection objPropiedades = TypeDescriptor.GetProperties(typeof(T));

            // Validar que los campos existen
            if (objPropiedades.Find(campoValor, true).IsNull())
            {
                Errores.ControlErrores.Generar_Error_Argumentos(string.Format("Error el miembro '{0}' no existe en la colección", campoValor));
            }

            if (objPropiedades.Find(campoDescripcion, true).IsNull())
            {
                Errores.ControlErrores.Generar_Error_Argumentos(string.Format("Error el miembro '{0}' no existe en la colección", campoValor));
            }

            combo.Items.Clear();

            CustomListItem objItem = null;

            // Agregar el elemento inicial si es requerido
            if (elementoInicial)
            {
                objItem = new CustomListItem{ Valor = "0", Descripcion = "- Selecciona -" };
                combo.Items.Add(objItem);
                objItem = null;
            }

            combo.ValueMember = campoValor;
            combo.DisplayMember = campoDescripcion;

            // Agregar la lista en el control
            foreach (T objeto in datos)
            {
                objItem = new CustomListItem
                    {
                        Valor = objPropiedades[campoValor].GetValue(objeto).ToString(),
                        Descripcion = objPropiedades[campoDescripcion].GetValue(objeto).ToString()
                    };

                combo.Items.Add(objItem);

                objItem = null;
            }

            // Seleccionar el primer elemento
            if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Carga la información de un DataTable a un CheckedListBox
        /// </summary>
        /// <param name="lista">Objeto CheckedListBox</param>
        /// <param name="datos">DataTable</param>
        /// <param name="campoValor">Nombre del campo valor</param>
        /// <param name="campoDescripcion">Nombre del campo descripción</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void ToCheckedListBox(this CheckedListBox lista, DataTable datos, string campoValor, string campoDescripcion)
        {
            // Validar que los campos existen
            if (!datos.Columns.Contains(campoValor))
            {
                Errores.ControlErrores.Generar_Error_Argumentos(string.Format("Error el miembro '{0}' no existe en la colección", campoValor));
            }

            if (!datos.Columns.Contains(campoDescripcion))
            {
                Errores.ControlErrores.Generar_Error_Argumentos(string.Format("Error el miembro '{0}' no existe en la colección", campoValor));
            }

            lista.Items.Clear();

            foreach (DataRow drElemento in datos.Rows)
            {
                CustomListItem objItem = new CustomListItem
                    {
                        Valor = drElemento[campoValor].ToString(),
                        Descripcion = drElemento[campoDescripcion].ToString()
                    };

                lista.Items.Add(objItem);

                objItem = null;
            }
        }

        /// <summary>
        /// Carga la información de una lista a un CheckedListBox
        /// </summary>
        /// <typeparam name="T">Clase de objetos</typeparam>
        /// <param name="lista">Objeto CheckedListBox</param>
        /// <param name="datos">Lista de objetos</param>
        /// <param name="campoValor">Nombre del campo valor</param>
        /// <param name="campoDescripcion">Nombre del campo descripción</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void ToCheckedListBox<T>(this CheckedListBox lista, List<T> datos, string campoValor, string campoDescripcion)
        {
            PropertyDescriptorCollection objPropiedades = TypeDescriptor.GetProperties(typeof(T));

            // Validar que los campos existen
            if (objPropiedades.Find(campoValor, true).IsNull())
            {
                Errores.ControlErrores.Generar_Error_Argumentos(string.Format("Error el miembro '{0}' no existe en la colección", campoValor));
            }

            if (objPropiedades.Find(campoDescripcion, true).IsNull())
            {
                Errores.ControlErrores.Generar_Error_Argumentos(string.Format("Error el miembro '{0}' no existe en la colección", campoValor));
            }

            lista.Items.Clear();

            // Agregar la lista en el control
            foreach (T objeto in datos)
            {
                CustomListItem objItem = new CustomListItem
                    {
                        Valor = objPropiedades[campoValor].GetValue(objeto).ToString(),
                        Descripcion = objPropiedades[campoDescripcion].GetValue(objeto).ToString()
                    };

                lista.Items.Add(objItem);

                objItem = null;
            }
        }

        /// <summary>
        /// Extensión que convierte un valor string a Nullable de DateTime
        /// </summary>
        /// <param name="valor">Valor a convertir</param>
        /// <returns>Valor string convertido a Nullable DateTime</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static Nullable<DateTime> TryParseDateTime(this string valor)
        {
            DateTime dTemporal;
            if (DateTime.TryParse(valor, out dTemporal))
            {
                return dTemporal;
            }

            return null;
        }

        /// <summary>
        /// Exntesión que convierte un valor string a Nullable de bool
        /// </summary>
        /// <param name="valor">Valor a convertir</param>
        /// <returns>Valor string convertido a Nullable bool</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static Nullable<bool> TryParseBool(this string valor)
        {
            bool bTemporal;
            if (bool.TryParse(valor, out bTemporal))
            {
                return bTemporal;
            }

            return null;
        }

        /// <summary>
        /// Regresa el valor bool de un campo de un DataRow
        /// </summary>
        /// <param name="fila">Objeto de DataRow</param>
        /// <param name="nombreCampo">Nombre del campo</param>
        /// <returns>DateTime con el valor del campo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static Nullable<bool> ObtenerBoolONulo(this DataRow fila, string nombreCampo)
        {
            bool bReturn = false;
            string sValor = string.Empty;

            try
            {
                sValor = fila[nombreCampo].ToString();
            }
            catch
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, el campo no se encuentra");
            }

            if (bool.TryParse(sValor, out bReturn))
            {
                return bReturn;
            }
            return null;
        }

        /// <summary>
        /// Regresa el valor bool de un campo de un DataRow
        /// </summary>
        /// <param name="fila">Objeto de DataRow</param>
        /// <param name="nombreCampo">Nombre del campo</param>
        /// <returns>DateTime con el valor del campo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static bool ObtenerBool(this DataRow fila, string nombreCampo)
        {
            bool bReturn = false;
            string sValor = string.Empty;

            try
            {
                sValor = fila[nombreCampo].ToString();
            }
            catch
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, el campo no se encuentra");
            }

            if (!bool.TryParse(sValor, out bReturn))
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, no se puede convertir a bool el valor del campo");
            }

            return bReturn;
        }

        /// <summary>
        /// Regresa el valor DateTime de un campo de un DataRow
        /// </summary>
        /// <param name="fila">Objeto de DataRow</param>
        /// <param name="nombreCampo">Nombre del campo</param>
        /// <returns>DateTime con el valor del campo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DateTime ObtenerDateTime(this DataRow fila, string nombreCampo)
        {
            DateTime dReturn;
            string sValor = string.Empty;

            try
            {
                sValor = fila[nombreCampo].ToString();
            }
            catch
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, el campo no se encuentra");
            }

            if (DateTime.TryParse(sValor, out dReturn))
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, no se puede convertir a DateTime el valor del campo");
            }

            return dReturn;
        }

        /// <summary>
        /// Regresa un decimal nullable a partír del valor de un campo de un objeto DataRow
        /// </summary>
        /// <param name="fila">Objeto de DataRow</param>
        /// <param name="nombreCampo">Nombre del campo</param>
        /// <returns>decimal(nullable) con el valor del campo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static Nullable<decimal> ObtenerDecimalONulo(this DataRow fila, string nombreCampo)
        {
            decimal nReturn = 0m;
            string sValor = string.Empty;

            // Validar que el campo exista
            try
            {
                sValor = fila[nombreCampo].ToString();
            }
            catch
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, el campo no se encuentra");
            }

            if (decimal.TryParse(sValor, out nReturn))
            {
                return nReturn;
            }
            return null;
        }

        /// <summary>
        /// Regresa el valor decimal de un campo de un DataRow
        /// </summary>
        /// <param name="fila">Objeto de DataRow</param>
        /// <param name="nombreCampo">Nombre del campo</param>
        /// <returns>decimal con el valor del campo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static decimal ObtenerDecimal(this DataRow fila, string nombreCampo)
        {
            decimal nReturn = 0m;
            string sValor = string.Empty;

            try
            {
                sValor = fila[nombreCampo].ToString();
            }
            catch
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, el campo no se encuentra");
            }

            if (!decimal.TryParse(sValor, out nReturn))
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, no se puede convertir a decimal el valor del campo");
            }

            return nReturn;
        }

        /// <summary>
        /// Regresa el valor long de un campo de un DataRow
        /// </summary>
        /// <param name="fila">Objeto de DataRow</param>
        /// <param name="nombreCampo">Nombre del campo</param>
        /// <returns>long (nullable) con el valor del campo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static Nullable<long> ObtenerInt64ONulo(this DataRow fila, string nombreCampo)
        {
            long nReturn = 0;
            string sValor = string.Empty;
            
            try
            {
                sValor = fila[nombreCampo].ToString();
            }
            catch
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, el campo no se encuentra");
            }

            if (long.TryParse(sValor, out nReturn))
            {
                return nReturn;
            }
            return null;
        }

        /// <summary>
        /// Regresa el valor long de un campo de un DataRow
        /// </summary>
        /// <param name="fila">Objeto de DataRow</param>
        /// <param name="nombreCampo">Nombre del campo</param>
        /// <returns>long con el valor del campo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static long ObtenerInt64(this DataRow fila, string nombreCampo)
        {
            long nReturn = 0;
            string sValor = string.Empty;

            try
            {
                sValor = fila[nombreCampo].ToString();
            }
            catch
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, el campo no se encuentra");
            }

            if (!long.TryParse(sValor, out nReturn))
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, no se puede convertir a long el valor del campo");
            }

            return nReturn;
        }

        /// <summary>
        /// Regresa el valor int de un campo de un DataRow
        /// </summary>
        /// <param name="fila">Objeto de DataRow</param>
        /// <param name="nombreCampo">Nombre del campo</param>
        /// <returns>int con el valor del campo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static int ObtenerInt32(this DataRow fila, string nombreCampo)
        {
            int nReturn = 0;
            string sValor = string.Empty;

            try
            {
                sValor = fila[nombreCampo].ToString();
            }
            catch
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, el campo no se encuentra");
            }

            if (!int.TryParse(sValor, out nReturn))
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, no se puede convertir a int el valor del campo");
            }

            return nReturn;
        }

        /// <summary>
        /// Regresa el valor string de un campo de un DataRow
        /// </summary>
        /// <param name="fila">Objeto de DataRow</param>
        /// <param name="nombreCampo">Nombre del campo</param>
        /// <returns>string con el valor del campo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string ObtenerString(this DataRow fila, string nombreCampo)
        {
            string sValor = string.Empty;

            try
            {
                sValor = fila[nombreCampo].ToString();
            }
            catch
            {
                Errores.ControlErrores.Generar_Error_Argumentos("Error, el campo no se encuentra");
            }

            return sValor;
        }

        /// <summary>
        /// Regresa una cadena con el número de caracteres de la derecha
        /// </summary>
        /// <param name="valor">Cadena de caracteres</param>
        /// <param name="numeroCaracteres">Número de caracteres de la derecha</param>
        /// <returns>Cadena de caracteres de la derecha</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string Right(this string valor, int numeroCaracteres)
        {
            return valor.Substring(valor.Length - numeroCaracteres);
        }

        /// <summary>
        /// Regresa una cadena con el número de caracteres de la izquierda
        /// </summary>
        /// <param name="valor">Cadena de caracteres</param>
        /// <param name="numeroCaracteres">Número de caracteres de la izquierda</param>
        /// <returns>Cadena de caracteres de la izquierda</returns>
        public static string Left(this string valor, int numeroCaracteres)
        {
            return valor.Substring(0, Math.Min(numeroCaracteres, valor.Length));
        }

        /// <summary>
        /// Elimina del DataTable las columnas Especificadas
        /// </summary>
        /// <param name="informacion">DataTable con la información</param>
        /// <param name="columnas">Lista de columnas a eliminar</param>
        /// <returns>DataTable con las columnas necesarias</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DataTable QuitarColumnas(this DataTable informacion, List<string> columnas)
        {
            if (!informacion.IsNull())
            {
                for (int nColumna = informacion.Columns.Count - 1; nColumna > -1; nColumna--)
                {
                    if (columnas.Contains(informacion.Columns[nColumna].ColumnName))
                    {
                        informacion.Columns.Remove(informacion.Columns[nColumna]);
                    }
                }
            }

            return informacion;
        }

        /// <summary>
        /// Conserva en el DataTable sólo las columnas especificadas
        /// </summary>
        /// <param name="informacion">DataTable con la información</param>
        /// <param name="columnas">Lista de columnas a conservar</param>
        /// <returns>DataTable con las columnas necesarias</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DataTable ConservarColumnas(this DataTable informacion, List<string> columnas)
        {
            if (!informacion.IsNull())
            {
                for (int nColumna = informacion.Columns.Count - 1; nColumna > -1; nColumna--)
                {
                    if (!columnas.Contains(informacion.Columns[nColumna].ColumnName))
                    {
                        informacion.Columns.Remove(informacion.Columns[nColumna]);
                    }
                }
            }

            return informacion;
        }

        /// <summary>
        /// Verifica si una cadena de caracteres está vacía
        /// </summary>
        /// <param name="value">Cadena a evaluar</param>
        /// <returns>True si la cadena está vacía</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Metodo para convertir una lista generica a un DataTable incluye Objetos Nullable
        /// </summary>
        /// <typeparam name="T">Tipo generico</typeparam>
        /// <param name="data">Lista de objetos</param>
        /// <returns>DataTable con la información de la lista</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in props)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
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
        /// Metodo para convertir una lista generica a un DataTable (incluye Objetos Nullable?)
        /// </summary>
        /// <typeparam name="T">Tipo generico</typeparam>
        /// <param name="data">Lista de objetos</param>
        /// <returns>DataTable con la información de la lista</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DataTable ListToDataTable<T>(this IList<T> data)
        {
            DataTable table = new DataTable();

            //special handling for value types and string
            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {

                DataColumn dc = new DataColumn("Value");

                table.Columns.Add(dc);

                foreach (T item in data)
                {
                    DataRow dr = table.NewRow();
                    dr[0] = item;

                    table.Rows.Add(dr);
                }
            }
            else
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        try
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        catch
                        {
                            row[prop.Name] = DBNull.Value;
                        }

                    }
                    table.Rows.Add(row);
                }
            }
            return table;

        }

        /// <summary>
        /// Evalúa si una cadena de texto es de Fecha/hora
        /// </summary>
        /// <param name="valor">Cadena de texto a evaluar</param>
        /// <returns>True si la cadena es de tipo Fecha/hora</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static bool IsDate(this string valor)
        {
            bool bReturn = false;
            DateTime dFecha;

            bReturn = DateTime.TryParse(valor, out dFecha) ? (dFecha > new DateTime(1900, 1, 1)) : false;

            return bReturn;
        }

        /// <summary>
        /// Evalúa si una una cadena de texto es numérica
        /// </summary>
        /// <param name="valor">Cadena de texto a evaluar</param>
        /// <returns>True si la cadena es numérica</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static bool IsNumeric(this string valor)
        {
            bool bReturn = false;
            double dNumero = 0.0f;

            bReturn = double.TryParse(valor, out dNumero);

            return bReturn;
        }

        /// <summary>
        /// Devuelve la cadena de texto encriptada
        /// </summary>
        /// <param name="valor">Cadena de texto a encriptar</param>
        /// <param name="key">Llave de encriptación</param>
        /// <param name="iv">Clave de encriptación</param>
        /// <returns>String encriptado</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string Encriptar(this string valor, string key, string iv)
        {
            if (valor == null) throw new ArgumentNullException("valor");
            string sReturn = string.Empty;

            Encriptacion.Crypto objCrypto = new Encriptacion.Crypto {Key = key, Iv = iv};

            sReturn = objCrypto.Encriptar(valor);

            objCrypto = null;

            return sReturn;
        }

        /// <summary>
        /// Devuelve la cadena de texto desencriptada
        /// </summary>
        /// <param name="valor">Cadena de texto a desencriptar</param>
        /// <param name="key">Llave de encriptación</param>
        /// <param name="iv">Clave de encriptación</param>
        /// <returns>String desencriptado</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string Desencriptar(this string valor, string key, string iv)
        {
            string sReturn = string.Empty;

            Encriptacion.Crypto objCrypto = new Encriptacion.Crypto {Key = key, Iv = iv};

            sReturn = objCrypto.Desencriptar(valor);

            objCrypto = null;

            return sReturn;
        }

        /// <summary>
        /// Verifica si un objeto es nulo
        /// </summary>
        /// <param name="valor">Objeto a evaluar</param>
        /// <returns>True si el objeto es nulo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static bool IsNull(this object valor)
        {
            return (valor == null);
        }

        /// <summary>
        /// Devuelve un valor Long que especifica el número de
        /// intervalos de tiempo entre dos valores Date.
        /// </summary>
        /// <param name="valor">Objeto a evaluar</param>
        /// <param name="interval">Obligatorio. Valor de enumeración
        /// DateInterval o expresión String que representa el intervalo
        /// de tiempo que se desea utilizar como unidad de diferencia
        /// entre Date1 y Date2.</param>
        /// <param name="date2">Obligatorio. Date. Segundo valor de
        /// fecha u hora que se desea utilizar en el cálculo.</param>
        /// <returns>Long con la diferencia</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static long DateDiff(this DateTime valor, DateInterval interval, DateTime date2)
        {
            long rs = 0;
            TimeSpan diff = date2.Subtract(valor);
            switch (interval)
            {
                case DateInterval.Day:
                    rs = diff.Days;
                    break;
                case DateInterval.DayOfYear:
                    rs = (long)diff.TotalDays;
                    break;
                case DateInterval.Hour:
                    rs = (long)diff.TotalHours;
                    break;
                case DateInterval.Minute:
                    rs = (long)diff.TotalMinutes;
                    break;
                case DateInterval.Month:
                    rs = (date2.Month - valor.Month) + (12 * valor.DateDiff(DateInterval.Year, date2));
                    break;
                case DateInterval.Quarter:
                    rs = (long)Math.Ceiling((double)(valor.DateDiff(DateInterval.Month, date2) / 3.0));
                    break;
                case DateInterval.Second:
                    rs = (long)diff.TotalSeconds;
                    break;
                case DateInterval.Weekday:
                case DateInterval.WeekOfYear:
                    rs = (long)(diff.TotalDays / 7);
                    break;
                case DateInterval.Year:
                    rs = date2.Year - valor.Year;
                    break;
            }

            return rs;
        }

        /// <summary>
        /// Devuelve un valor string correspondiente al valor bool
        /// </summary>
        /// <param name="valor">Objeto a evaluar</param>
        /// <param name="language">ES: español, EN:Ingles</param>
        /// <returns>String con el nombre</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string StringBool(this bool valor, string language)
        {
            string sReturn = string.Empty;

            switch (language)
            {
                case "ES":
                    sReturn = valor ? StringStatus.Activo.ToString() : StringStatus.Inactivo.ToString();
                    break;
                case "EN":
                    sReturn = valor ? StringStatus.Enable.ToString() : StringStatus.Disable.ToString();
                    break;
            }
            return sReturn;
        }

        /// <summary>
        /// Limpia cadena de careacteres definidos en el Enumerable Utilidades.Enumerados.SQLInjectionMarks
        /// </summary>
        /// <param name="sDirtyString">Cadena que se desea limpiar</param>
        /// <returns>Cadena limpia</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string ToStringClean(this string sDirtyString)
        {
            foreach (var c in Enum.GetValues(typeof( Utilidades.Enumerados.SQLInjectionMarks)))
            {
                char cOldChar = (char)c.GetHashCode();
                sDirtyString = sDirtyString.Replace(cOldChar.ToString() , string.Empty);
            }

            return sDirtyString;
        }

        /// <summary>
        /// Limpia cadena de caracteres especiale(de control)
        /// </summary>
        /// <param name="sDirtyString">Cadena que se desea limpiar</param>
        /// <returns>Cadena limpia</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string RemoveControlCharacters(this string sDirtyString)
        {
            if (string.IsNullOrEmpty(sDirtyString)) return string.Empty;

            System.Text.StringBuilder newString = new System.Text.StringBuilder();

            foreach (char ch in sDirtyString)
            { 
                if (!char.IsControl(ch))
                    newString.Append(ch);
            }

            return newString.ToString();
        }

        /// <summary>
        /// Metodo para llenar un objeto generica(DropDownList) con un DataTable
        /// </summary>
        /// <param name="combo">Objeto a trabajar</param>
        /// <param name="data">Tabla con informacion</param>
        /// <param name="idItem">Nombre del campo a usar como ID</param>
        /// <param name="nombreItem">Nombre del campo a mostrar</param>
        /// <param name="inicioItem">Texto inical del objeto</param>
        /// <returns>DropDownList con la información del DataTable</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DropDownList ToDropDownList(this DropDownList combo, DataTable data, string idItem, string nombreItem, string inicioItem)
        {
            combo.Items.Clear();

            combo.DataSource = data;
            combo.DataValueField = idItem;
            combo.DataTextField = nombreItem;
            combo.DataBind();

            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(inicioItem, "0");
            combo.Items.Insert(0, item);
            combo.SelectedIndex = 0;

            item = null;
            data = null;

            return combo;
        }

        /// <summary>
        /// Metodo para llenar un objeto generica(DropDownList) con un DataTable
        /// </summary>
        /// <param name="combo">Objeto a trabajar</param>
        /// <param name="data">Tabla con informacion</param>
        /// <param name="idItem">Nombre del campo a usar como ID</param>
        /// <param name="nombreItem">Nombre del campo a mostrar</param>
        /// <param name="inicioItem">Texto inical del objeto</param>
        /// <param name="valorInicial">Valor a seleccionar inicialmente</param>
        /// <returns>DropDownList con la información del DataTable</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DropDownList ToDropDownList(this DropDownList combo, DataTable data, string idItem, string nombreItem, string inicioItem, string valorInicial)
        {
            combo.Items.Clear();

            combo.DataSource = data;
            combo.DataValueField = idItem;
            combo.DataTextField = nombreItem;
            combo.DataBind();

            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(inicioItem, "0");
            combo.Items.Insert(0, item);
            combo.SelectedValue = valorInicial;

            item = null;
            data = null;

            return combo;
        }

        /// <summary>
        /// Método para llenar un objeto DropDownList con una lista generica
        /// </summary>
        /// <param name="combo">Objeto a trabajar</param>
        /// <param name="data">Lista de información</param>
        /// <param name="idItem">Nombre del campo a usar como ID</param>
        /// <param name="nombreItem">Nombre del campo a mostrar</param>
        /// <param name="inicioItem">Texto inicial del objeto</param>
        /// <param name="valorInicial">Valor a seleccionar inicialmente</param>
        /// <returns>DropDownList con la infomración de la lista</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DropDownList ToDropDownList<T>(this DropDownList combo, IList<T> data, string idItem, string nombreItem, string inicioItem, string valorInicial)
        {
            combo.Items.Clear();

            combo.DataSource = data;
            combo.DataValueField = idItem;
            combo.DataTextField = nombreItem;
            combo.DataBind();

            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(inicioItem, "0");
            combo.Items.Insert(0, item);
            combo.SelectedValue = valorInicial;

            item = null;
            data = null;

            return combo;
        }

        /// <summary>
        /// Método para llenar un objeto DropDownList con una lista generica
        /// </summary>
        /// <param name="combo">Objeto a trabajar</param>
        /// <param name="data">Lista de información</param>
        /// <param name="idItem">Nombre del campo a usar como ID</param>
        /// <param name="nombreItem">Nombre del campo a mostrar</param>
        /// <param name="inicioItem">Texto inicial del objeto</param>
        /// <returns>DropDownList con la infomración de la lista</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DropDownList ToDropDownList<T>(this DropDownList combo, IList<T> data, string idItem, string nombreItem, string inicioItem)
        {
            combo.Items.Clear();
          
            combo.DataSource = data;
            combo.DataValueField = idItem;
            combo.DataTextField = nombreItem;
            combo.DataBind();

            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(inicioItem, "0");
            combo.Items.Insert(0, item);
            combo.SelectedIndex = 0;

            item = null;
            data = null;

            return combo;
        }        
       
        /// <summary>
        /// Carga el contenido de un DataTable a un ComboBox
        /// </summary>
        /// <param name="combo">ComboBox</param>
        /// <param name="data">DataTable</param>
        /// <param name="idItem">Campo valor</param>
        /// <param name="nombreItem">Campo descripción</param>
        /// <param name="inicioItem">Elemento inicial</param>
        /// <param name="valorInicial">Valor inicial</param>
        /// <returns>ComboBox cargado con la información</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static ComboBox ToComboBox(this ComboBox combo, DataTable data, string idItem, string nombreItem, string inicioItem, int valorInicial)
        {
            #region Agrega elemento inicial
            if (data.Select(string.Format("{0} = {1}", idItem, valorInicial)).Length >= 0)
            {
                DataRow drDefault = data.NewRow();
                drDefault[nombreItem] = inicioItem;
                drDefault[idItem] = 0;
                data.Rows.InsertAt(drDefault, 0);
            }
            #endregion

            combo.DataSource = data;
            combo.DisplayMember = nombreItem;
            combo.ValueMember = idItem;

            #region Seleccion del elemento indicado(ValorInicial(ID))

            try
            {
                combo.SelectedValue = valorInicial;
            }
            catch
            {
            }

            #endregion

            data = null;

            return combo;
        }

        /// <summary>
        /// Metodo para llenar un objeto generica(ComboBox) con una Lista
        /// </summary>
        /// <typeparam name="T">Crea elemento de tipo T</typeparam>
        /// <param name="combo">Objeto a trabajar</param>
        /// <param name="data">Lista con informacion</param>
        /// <param name="idItem">Nombre del campo a usar como ID</param>
        /// <param name="nombreItem">Nombre del campo a mostrar</param>
        /// <param name="inicioItem">Texto inical del objeto</param>
        /// <param name="valorInicial">Valor a seleccionar inicialmente</param>
        /// <returns>ComboBox con la información de la lista</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static ComboBox ToComboBox<T>(this ComboBox combo, IList<T> data, string idItem, string nombreItem, string inicioItem, int valorInicial) where T : new()
        {
            #region Agrega elemento inicial
            
            bool bItemInicialEncontrado = (from tObjeto in data let valueProperty = tObjeto.GetType().GetProperty(idItem) where valueProperty.GetValue(tObjeto, null).ToString().Equals(valorInicial.ToString(CultureInfo.InvariantCulture)) select tObjeto).Any();
            //foreach (T tObjeto in data)
            //{
            //    PropertyInfo valueProperty = tObjeto.GetType().GetProperty(idItem);

            //    if (valueProperty.GetValue(tObjeto, null).ToString().Equals(valorInicial.ToString(CultureInfo.InvariantCulture)))
            //    {
            //        bItemInicialEncontrado = true;
            //        break;
            //    }
            //}

            /* AddIttem*/
            if (!bItemInicialEncontrado)
            {
                T obj = new T();
                //object obj = Activator.CreateInstance(data.GetType());

                //// Gets the Display Property Information
                PropertyInfo displayProperty = obj.GetType().GetProperty(nombreItem);

                //// Sets the required text into the display property
                displayProperty.SetValue(obj, inicioItem, null);

                //// Gets the Value Property Information
                PropertyInfo valueProperty = obj.GetType().GetProperty(idItem);

                //// Sets the required value into the value property
                valueProperty.SetValue(obj, 0, null);

                //// Insert the new object on the list
                data.Insert(0, obj);
            }
            #endregion

            combo.DataSource = data;
            combo.DisplayMember = nombreItem;
            combo.ValueMember = idItem;

            #region Seleccion del elemento indicado(ValorInicial(ID))
            /*LookupAndSetValue*/

            if (combo.Items.Count > 0)
            {
                // Select first item if requested item was not found
                combo.SelectedIndex = 0;

                for (int i = 0; i < combo.Items.Count; i++)
                {
                    object item = combo.Items[i];
                    object thisValue = item.GetType().GetProperty(combo.ValueMember).GetValue(item, null);
                    if (thisValue != null && thisValue.Equals(valorInicial))
                    {
                        combo.SelectedIndex = i;
                        break;
                    }
                }
            }

            #endregion

            data = null;

            return combo;
        }

        public static ComboBox ToComboBox<T>(this ComboBox combo, IList<T> data, string idItem, string nombreItem, int valorInicial) where T : new()
        {
            #region Agrega elemento inicial
            //bool bItemInicialEncontrado = false;

            //foreach (T t_Objeto in data)
            //{
            //    PropertyInfo valueProperty = t_Objeto.GetType().GetProperty(idItem);

            //    if (valueProperty.GetValue(t_Objeto, null).ToString().Equals(ValorInicial.ToString()))
            //    {
            //        bItemInicialEncontrado = true;
            //        break;
            //    }
            //}

            /* AddIttem*/
            //if (!bItemInicialEncontrado)
            //{
                T obj = new T();
                //object obj = Activator.CreateInstance(data.GetType());

                //// Gets the Display Property Information
                PropertyInfo displayProperty = obj.GetType().GetProperty(nombreItem);

                //// Sets the required text into the display property
                displayProperty.SetValue(obj, "- Seleccionar -", null);

                //// Gets the Value Property Information
                PropertyInfo valueProperty = obj.GetType().GetProperty(idItem);

                //// Sets the required value into the value property
                valueProperty.SetValue(obj, 0, null);

                //// Insert the new object on the list
                data.Insert(0, obj);
            //}
            #endregion

            combo.DataSource = data;
            combo.DisplayMember = nombreItem;
            combo.ValueMember = idItem;

            #region Seleccion del elemento indicado(ValorInicial(ID))
            /*LookupAndSetValue*/

            if (combo.Items.Count > 0)
            {
                // Select first item if requested item was not found
                combo.SelectedIndex = 0;

                for (int i = 0; i < combo.Items.Count; i++)
                {
                    object item = combo.Items[i];
                    object thisValue = item.GetType().GetProperty(combo.ValueMember).GetValue(item, null);
                    if (thisValue != null && thisValue.Equals(valorInicial))
                    {
                        combo.SelectedIndex = i;
                        break;
                    }
                }
            }

            #endregion

            data = null;

            return combo;
        }

        /// <summary>
        /// Metodo para llenar un objeto generica(ComboBox) con una Lista
        /// </summary>
        /// <typeparam name="T">Crea elemento de tipo T</typeparam>
        /// <param name="combo">Objeto a trabajar</param>
        /// <param name="data">Lista con informacion</param>
        /// <param name="idItem">Nombre del campo a usar como ID</param>
        /// <param name="nombreItem">Nombre del campo a mostrar</param>
        /// <param name="inicioItem">Texto inical del objeto</param>
        /// <param name="valorInicial">Valor a seleccionar inicialmente</param>
        /// <returns>ComboBox con la información de la lista</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static ComboBox ToComboBoxCustomListItem<T>(this ComboBox combo, IList<T> data, string idItem, string nombreItem, string inicioItem, int valorInicial) where T : new()
        {
            //Limpia el control
            combo.Items.Clear();

            //Agrega elemento inicial
            CustomListItem items = new CustomListItem { Descripcion = inicioItem, Valor = "0" };
            combo.Items.Add(items);

            int index = 1;
            int indexSelected = 0;

            foreach (T tObjeto in data)
            {
                items = null;

                PropertyInfo valueProperty = tObjeto.GetType().GetProperty(idItem);
                PropertyInfo displayProperty = tObjeto.GetType().GetProperty(nombreItem);

                items = new CustomListItem
                    {
                    Descripcion = displayProperty.GetValue(tObjeto, null).ToString(),
                    Valor = valueProperty.GetValue(tObjeto, null).ToString()
                };

                combo.Items.Add(items);

                //Identifica el indice del elemento indicado(ValorInicial(ID))
                if (valueProperty.GetValue(tObjeto, null).ToString().Equals(valorInicial.ToString(CultureInfo.InvariantCulture)))
                {
                    indexSelected = index;
                }

                index++;
            }

            //Seleccion del elemento indicado(ValorInicial(ID))
            combo.SelectedIndex = indexSelected;

            items = null;

            return combo;
        }

        /// <summary>
        /// Seleccion el elmento deseado del combo
        /// </summary>
        /// <param name="combo">Objeto a trabajar</param>
        /// <param name="sValor">Valor a seleccionar</param>
        /// <returns></returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static ComboBox ToSelectedValue(this ComboBox combo, string sValor)
        {
            int index = 0;
            int indexSelected = 0;

            if (combo.Items.Count > 0)
            {
                foreach (object obj in combo.Items)
                {
                    var customListItem = obj as CustomListItem;
                    if (customListItem != null && customListItem.Valor.Equals(sValor))
                    {
                        indexSelected = index;
                        break;
                    }
                    index++;
                }
            }
            combo.SelectedIndex = indexSelected;

            return combo;
        }

        /// <summary>
        /// Metodo para llenar un objeto generica(CheckedListBox) con una Lista
        /// </summary>
        /// <typeparam name="T">Crea elemento de tipo T</typeparam>
        /// <param name="checkList">Objeto a trabajar</param>
        /// <param name="data">Lista con informacion</param>
        /// <param name="nombreItem">Nombre del campo a mostrar</param>
        /// <param name="valores">Valores a seleccionar</param>
        /// <returns>CheckedListBox con la información de la lista</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static CheckedListBox ToCheckedListBox<T>(this CheckedListBox checkList, IList<T> data, string nombreItem, List<string> valores)
        {
            #region Agrega elemento

            checkList.Items.Clear();
            checkList.DisplayMember = nombreItem;
            foreach (object item in data)
            {
                checkList.Items.Add(item);
            }

            #endregion

            #region Seleccion del elemento indicado(Valores)

            if (valores != null)
            {
                foreach (string valor in valores)
                {
                    //object thisValue = valor.GetType().GetProperty(nombreItem).GetValue(valor, null);
                    //CheckList.SetItemCheckState(CheckList.FindString(thisValue.ToString()), CheckState.Checked);

                    checkList.SetItemCheckState(checkList.FindString(valor.ToString(CultureInfo.InvariantCulture)), CheckState.Checked);
                }
            }

            #endregion

            return checkList;
        }

        /// <summary>
        /// Carga un CheckList con la información de un DataTable
        /// </summary>
        /// <param name="checkList">CheckList a cargar</param>
        /// <param name="data">DataTable</param>
        /// <param name="nombreItem">Campo descripción</param>
        /// <param name="nombreValue">Campo valor</param>
        /// <param name="valores">Valores a seleccionar</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void ToCheckedListBox(this CheckedListBox checkList, DataTable data, string nombreItem, string nombreValue, List<string> valores)
        {
            #region Agrega elemento

            checkList.Items.Clear();
            checkList.DisplayMember = nombreItem;
            checkList.ValueMember = nombreValue;

            foreach (DataRow item in data.Rows)
            {
                checkList.Items.Add(item[nombreItem]);
            }

            #endregion

            #region Seleccion del elemento indicado(Valores)

            if (valores.IsNull() == false && valores.Count > 0)
            {
                foreach (string valor in valores)
                {
                    checkList.SetItemCheckState(checkList.FindString(valor.ToString(CultureInfo.InvariantCulture)), CheckState.Checked);
                }
            }

            #endregion
        }       

        /// <summary>
        /// Metodo para llenar un objeto generica(DropDownList) con las horas
        /// </summary>
        /// <param name="combo">Objeto a trabajar</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void LoadHours(this DropDownList combo)
        {
            combo.Items.Clear();

            ListItem[] items = { new ListItem("- Seleccionar -","0"),
                                 new ListItem("00:00","1"), 
                                 new ListItem("01:00","2"), 
                                 new ListItem("02:00","3"),
                                 new ListItem("03:00","4"),
                                 new ListItem("05:00","5"),
                                 new ListItem("06:00","6"),
                                 new ListItem("07:00","7"),
                                 new ListItem("08:00","8"),
                                 new ListItem("09:00","9"),
                                 new ListItem("10:00","10"),
                                 new ListItem("11:00","11"),
                                 new ListItem("12:00","12"),
                                 new ListItem("13:00","13"),
                                 new ListItem("14:00","14"),
                                 new ListItem("15:00","15"),
                                 new ListItem("16:00","16"),
                                 new ListItem("17:00","17"),
                                 new ListItem("18:00","18"),
                                 new ListItem("19:00","19"),
                                 new ListItem("20:00","20"),
                                 new ListItem("21:00","21"),
                                 new ListItem("22:00","22"),
                                 new ListItem("23:00","23"),
                               };
            combo.Items.AddRange(items);
        }

        /// <summary>
        /// Llena un DropDownList con horas extra.
        /// </summary>
        /// <param name="combo">Objeto a trabajar</param>
        /// <param name="horas">Catidad de horas llenar</param>
        /// <returns>DropDownList con las horas extra divididas en 30 minutos</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DropDownList LoadExtraHours(this DropDownList combo, int horas)
        {
            combo.Items.Clear();
            combo.Items.Add(new ListItem("- SELECCIONAR -", "0"));
            combo.Items.Add(new ListItem("00:00", "00"));

            if (horas < 0) 
            {
                return combo;
            }

            string hora = string.Empty;

            for (int i = 1; i <= horas; i++) 
            {
                if (i < 10)
                    hora = "0" + Convert.ToString(i);
                if (i >= 10)
                    hora = Convert.ToString(i);
                
                combo.Items.Add( new ListItem( hora + ":00", Convert.ToString((i*60))));
                
                if (i < horas)
                    combo.Items.Add(new ListItem(hora + ":30", Convert.ToString(((i*60)+30))));
            }
           
            //NOTA: Por practicidad, el "value" asignado al item del combo, corresponde a su valor en minutos.
            //Ejemplos: Text = 01:30 Value = 90
            
            return combo;
        }

        /// <summary>
        /// Metodo para llenar un objeto generica(DropDownList) con los meses
        /// </summary>
        /// <param name="combo">Objeto a trabajar</param>
        /// <returns>DropDownList con los horarios</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static System.Web.UI.WebControls.DropDownList LoadMonths(this System.Web.UI.WebControls.DropDownList combo)
        {
            combo.Items.Clear();

            ListItem[] items = { new ListItem("- Seleccionar -","0"),
                                 new ListItem("Enero","1"), 
                                 new ListItem("Febrero","2"), 
                                 new ListItem("Marzo","3"),
                                 new ListItem("Abril","4"),
                                 new ListItem("Mayo","5"),
                                 new ListItem("Junio","6"),
                                 new ListItem("Julio","7"),
                                 new ListItem("Agosto","8"),
                                 new ListItem("Septiembre","9"),
                                 new ListItem("Octubre","10"),
                                 new ListItem("Noviembre","11"),
                                 new ListItem("Diciembre","12")
                               };
            combo.Items.AddRange(items);
            return combo;
        }

        /// <summary>
        /// Metodo para llenar un objeto generica(ComboBox) con los meses
        /// </summary>
        /// <param name="combo">Objeto a trabajar</param>
        /// <returns>ComboBox con los horarios</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static System.Windows.Forms.ComboBox LoadMonthsWin(this  System.Windows.Forms.ComboBox combo)
        {
            combo.Items.Clear();

            CustomListItem[] items = { new CustomListItem {Descripcion = "- Seleccionar -", Valor = "0"},
                                 new CustomListItem {Descripcion = "Enero", Valor = "1"}, 
                                 new CustomListItem {Descripcion = "Febrero", Valor = "2"}, 
                                 new CustomListItem {Descripcion = "Marzo", Valor = "3"},
                                 new CustomListItem {Descripcion = "Abril", Valor = "4"},
                                 new CustomListItem {Descripcion = "Mayo", Valor = "5"},
                                 new CustomListItem {Descripcion = "Junio", Valor = "6"},
                                 new CustomListItem {Descripcion = "Julio", Valor = "7"},
                                 new CustomListItem {Descripcion = "Agosto", Valor = "8"},
                                 new CustomListItem {Descripcion = "Septiembre", Valor = "9"},
                                 new CustomListItem {Descripcion = "Octubre", Valor = "10"},
                                 new CustomListItem {Descripcion = "Noviembre", Valor = "11"},
                                 new CustomListItem {Descripcion = "Diciembre", Valor = "12"}
                               };
            combo.Items.AddRange(items);
            return combo;
        }

        /// <summary>
        /// Carga las opciones de años en ComboBox
        /// </summary>
        /// <param name="combo">ComboBox</param>
        /// <param name="iYear">Año de incio</param>
        /// <param name="iRange">Rango de años</param>
        /// <returns>ComboBox a cargar</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static System.Windows.Forms.ComboBox LoadYears(this  System.Windows.Forms.ComboBox combo, int iYear, int iRange)
        {
            combo.Items.Clear();

            int iValue = 0;

            CustomListItem items = new CustomListItem { Descripcion = "- Seleccionar -", Valor = Convert.ToString(iValue) };
            //{ new ListItem("- Seleccionar -", iValue.ToString()) };
            combo.Items.Add(items);
            items = null;

            for (int i = 0; i < iRange; i++)
            {
                
                iValue = iYear + i;
                items = new CustomListItem{Descripcion =  Convert.ToString(iValue),  Valor = Convert.ToString(iValue)};
                combo.Items.Add(items);
            }

            items = null;
            return combo;
        }

        /// <summary>
        /// Carga las opciones de sexo en DropDownList
        /// </summary>
        /// <param name="combo">DropDownList</param>
        /// <returns>DropDownList a cargar</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DropDownList LoadSexo(this DropDownList combo)
        {
            combo.Items.Clear();

            ListItem[] items = { new ListItem("- Seleccionar -","0"),
                                 new ListItem("Masculino","1"), 
                                 new ListItem("Femenino","2") 
                               };
            combo.Items.AddRange(items);
            return combo;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void LoadHoursWin(this System.Windows.Forms.ComboBox combo)
        {
            combo.Items.Clear();

            CustomListItem[] items = { new CustomListItem {Descripcion = "- Seleccionar -",Valor = "0"},
                                 new CustomListItem {Descripcion = "00:00",Valor = "1"}, 
                                 new CustomListItem {Descripcion = "01:00",Valor = "2"}, 
                                 new CustomListItem {Descripcion = "02:00",Valor = "3"},
                                 new CustomListItem {Descripcion = "03:00",Valor = "4"},
                                 new CustomListItem {Descripcion = "05:00",Valor = "5"},
                                 new CustomListItem {Descripcion = "06:00",Valor = "6"},
                                 new CustomListItem {Descripcion = "07:00",Valor = "7"},
                                 new CustomListItem {Descripcion = "08:00",Valor = "8"},
                                 new CustomListItem {Descripcion = "09:00",Valor = "9"},
                                 new CustomListItem {Descripcion = "10:00",Valor = "10"},
                                 new CustomListItem {Descripcion = "11:00",Valor = "11"},
                                 new CustomListItem {Descripcion = "12:00",Valor = "12"},
                                 new CustomListItem {Descripcion = "13:00",Valor = "13"},
                                 new CustomListItem {Descripcion = "14:00",Valor = "14"},
                                 new CustomListItem {Descripcion = "15:00",Valor = "15"},
                                 new CustomListItem {Descripcion = "16:00",Valor = "16"},
                                 new CustomListItem {Descripcion = "17:00",Valor = "17"},
                                 new CustomListItem {Descripcion = "18:00",Valor = "18"},
                                 new CustomListItem {Descripcion = "19:00",Valor = "19"},
                                 new CustomListItem {Descripcion = "20:00",Valor = "20"},
                                 new CustomListItem {Descripcion = "21:00",Valor = "21"},
                                 new CustomListItem {Descripcion = "22:00",Valor = "22"},
                                 new CustomListItem {Descripcion = "23:00",Valor = "23"},
                               };
            combo.Items.AddRange(items);
        }

        /// <summary>
        /// Mover elementos de un listBox(arriba(up) y abajo(down))
        /// http://stackoverflow.com/questions/4796109/how-to-move-item-in-listbox-up-and-down
        /// </summary>
        /// <param name="lstBx">Objeto a trabajar</param>
        /// <param name="direction">-1:Up , 1:Down</param>
        /// <returns>ListBox con la infomración de la lista re-organizada</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static System.Web.UI.WebControls.ListBox ToMoveItem(this System.Web.UI.WebControls.ListBox lstBx, int direction)
        {
            // Checking selected item
            if (lstBx.SelectedItem == null || lstBx.SelectedIndex < 0)
            {
                return lstBx; // No selected item - nothing to do
            }

            // Calculate new index using move direction
            int newIndex = lstBx.SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= lstBx.Items.Count)
            {
                return lstBx; // Index out of range - nothing to do
            }

            //object selected = lst_bx.SelectedItem;
            ListItem selected = new ListItem();
            selected = lstBx.SelectedItem;

            // Removing removable element
            lstBx.Items.Remove(selected);
            // Insert it in new position
            lstBx.Items.Insert(newIndex, selected);
            //// Restore selection
            //lst_bx.SetSelected(newIndex, true);

            return lstBx;
        }

        /// <summary>
        /// Método para llenar un string con los valores(Value/Text) de un ListBox
        /// </summary>
        /// <param name="lstBx">Objeto a trabajar</param>
        /// <param name="cSeparador">Caracter usado para separar los valores</param>
        /// <param name="valueText">true:Propiedad Value,false:Propiedad Text</param>
        /// <returns>String de valores(Value/Text) separado por un caracter(char:cSeparador)</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string ToStringListBox(this System.Web.UI.WebControls.ListBox lstBx, char cSeparador, bool valueText)
        {
            string strLst = string.Empty;
            string strRes = string.Empty;

            if (lstBx.Items.Count != 0)
            {                
                foreach (ListItem itm in lstBx.Items)
                {
                    if (valueText)
                    {
                        strLst += itm.Value + cSeparador;
                    }
                    else 
                    {
                        strLst += itm.Text + cSeparador;
                    }
                }

                strRes = strLst.Substring(0, strLst.Length - 1).TrimStart().TrimEnd();
            }

            return strRes;
        }

        /// <summary>
        /// Convierte un decimal a una cadena con formato HH:MM:SS
        /// </summary>
        /// <param name="tiempo">Tiempo en decimal(minutos)</param>
        /// <returns>String con formato HH:MM:SS</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string ConvertFromDecimalToStringHhmmss(this decimal tiempo)
        {
            try
            {
                decimal hours = Math.Floor(tiempo / 60);
                decimal minutes = Math.Floor(tiempo - (hours * 60));
                decimal seconds = (tiempo - (hours * 60) - minutes) * 60;

                int h = (int)Math.Floor(hours);
                int m = (int)Math.Floor(minutes);
                int s = (int)Math.Floor(seconds);

                string hhmmss = String.Format("{0:00}:{1:00}:{2:00}", h, m, s);

                return hhmmss;
            }
            catch (Exception)
            {
                return "00:00:00";
            }
        }

        /// <summary>
        /// Convierte un decimal a una cadena con formato HH:MM:SS
        /// </summary>
        /// <param name="minutos">Tiempo en decimal(minutos) </param>
        /// <returns>String con formato HH:MM:SS</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string ConvertToHhmm(this int minutos)
        {
            int iHoras = (minutos / 60);
            int iMinutos = (minutos % 60);

            string sHoras = Convert.ToString(iHoras);
            string sMinutos = Convert.ToString(iMinutos);

            if (iHoras <= 0)
                sHoras = "0" + sHoras;

            if (iMinutos <= 0)
                sMinutos = "0" + sMinutos;

            return sHoras + ":" + sMinutos;
        }

        /// <summary>
        /// Convierte un decimal a una cadena con formato HH:MM
        /// </summary>
        /// <param name="tiempo">Tiempo en decimal(minutos)</param>
        /// <returns>String con formato HH:MM</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string ConvertFromIntToStringHhmmss(this int tiempo)
        {
            try
            {
                decimal hours = Math.Floor(Convert.ToDecimal(tiempo) / 60);
                decimal minutes = Math.Floor(Convert.ToDecimal(tiempo) - (hours) * 60);
               //decimal seconds = (Tiempo - (hours * 60) - minutes) * 60;

                int h = (int)Math.Floor(hours);
                int m = (int)Math.Floor(minutes);
                //int S = (int)Math.Floor(seconds);

                string hhmm = String.Format("{0:00}:{1:00}", h, m);

                return hhmm;
            }
            catch (Exception)
            {
                return "00:00";
            }
        }

        /// <summary>
        /// Convierte una cadena que representa tiempo con formato HH[:]MM[:]SS a un valor decimal
        /// </summary>
        /// <param name="sTiempo">Cadena con fomato HH[separador]MM[separador]SS</param>
        /// <param name="separador">Separador</param>
        /// <returns>Valor decimal correspodiente a la cadena con fomato que represente tiempo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static decimal ConvertFromStringHhmmssToDecimal(this string sTiempo, char separador)
        {
            decimal hhmmss = 0;
            try
            {
                string[] tiempo = sTiempo.Split(separador);

                if (tiempo.Length != 0)
                {
                    decimal hh = Convert.ToDecimal(tiempo[0]) * 60;
                    decimal mm = Convert.ToDecimal(tiempo[1]);
                    decimal ss = Convert.ToDecimal(tiempo[2]) / 60;

                    hhmmss = hh + mm + ss;
                }

                return hhmmss;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Convierte una cadena que representa tiempo con formato HH[:]MM a un valor entero
        /// </summary>
        /// <param name="sTiempo"/>Cadena con fomato HH[separador]MM/param>
        /// <param name="separador">Separador</param>
        /// <returns>Valor entero correspodiente a la cadena con fomato que represente tiempo</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static int ConvertFromStringHhmmToInt(this string sTiempo, char separador)
        {
            int hhmm = 0;
            try
            {
                string[] tiempo = sTiempo.Split(separador);

                if (tiempo.Length != 0)
                {
                    int hh = string.IsNullOrEmpty(tiempo[0].Trim()) ? 0 : Convert.ToInt32(tiempo[0]) * 60;
                    int mm = string.IsNullOrEmpty(tiempo[1].Trim()) ? 0 : Convert.ToInt32(tiempo[1]);
                    //decimal ss = Convert.ToDecimal(tiempo[2].ToString()) / 60;
                    //hhmmss = hh + mm + ss;
                    hhmm = hh + mm ;
                }

                return hhmm;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Convierte en DataTable un GridView
        /// </summary>
        /// <param name="gv">Objeto a trabajar(GridView)</param>
        /// <param name="bFooter">Indicadaor apra conciderar los pues de paguina</param>
        /// <returns>DataTable con el contenido del GridView</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DataTable ConvertToDataTable(this GridView gv, bool bFooter)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable 
            //DataKey Columns
            foreach (string t in gv.DataKeyNames)
            {
                dt.Columns.Add(t);
            }
            //if (gv.DataKeys != null)
            //{
            //    for (int i = 0; i < gv.DataKeyNames.Length; i++)
            //    {
            //        dt.Columns.Add(gv.DataKeyNames[i]);
            //    }
            //}

            //Header Columns
            if (gv.HeaderRow != null)
            {
                for (int i = 0; i < gv.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(gv.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in gv.Rows)
            {
                int j = 0;
                DataRow dr = dt.NewRow();

                if (gv.DataKeyNames.Length != 0)
                {
                    for (int ky = 0; ky < gv.DataKeyNames.Length; ky++)
                    {
                        DataKey dataKey = gv.DataKeys[row.RowIndex];
                        if (dataKey != null) if (dataKey.Values != null) dr[ky] = dataKey.Values[ky].ToString();
                    }

                    j = gv.DataKeyNames.Length;
                }

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i + j] = row.Cells[i].Text.Replace("&nbsp;", "");
                }
                dt.Rows.Add(dr);
            }

            //  add the footer row to the table
            if (bFooter)
            {
                if (gv.FooterRow != null)
                {
                    DataRow dr = dt.NewRow();

                    for (int i = 0; i < gv.FooterRow.Cells.Count; i++)
                    {
                        dr[i] = gv.FooterRow.Cells[i].Text.Replace("&nbsp;", "");
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static GridView DeleteRowOfSource(this GridView gv, int iFila)
        {
            DataTable dt = ConvertToDataTable(gv, false);
            dt.Rows[iFila].Delete();
            gv.DataSource = dt;
            gv.DataBind();

            return gv;
        }

        /// <summary>
        /// Convierte un documento Xml en texto(Cadena)
        /// -http://stackoverflow.com/questions/2407302/convert-xmldocument-to-string
        /// </summary>
        /// <param name="xmlDoc">Documento XML</param>
        /// <returns>Representacion del domcumento XML en texto(cadena/string)</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string AsString(this XmlDocument xmlDoc)
        {
            string strXmlText = string.Empty;
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            if (xmlDoc != null)
            {
                xmlDoc.WriteTo(tx);
                strXmlText = sw.ToString();
            }

            return strXmlText;
        }

        /// <summary>
        /// Conveirte un Objeto en un XML(Texto/Cadena)
        /// </summary>
        /// <param name="oObject">Objeto a trabajar</param>
        /// <returns>Representacion del Objeto en XML</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static string ObjectToXml(this Object oObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        /// <summary>
        /// Realiza busqueda de un conjunto de valores(cadena) dento de otra
        /// </summary>
        /// <param name="valor">Valor(cadena) en donde se realizara la busqueda</param>
        /// <param name="valores">Arreglo de valores(cadenas) a buscar</param>
        /// <returns>True si todas las cadena estubieron contenidas</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static bool Contains(this string valor, string[] valores)
        {
            bool bReturn = false;

            foreach (string str in valores)
            {
                bReturn = str.Contains(valor);
            }

            return bReturn;
        }

        /// <summary>
        /// Método para llenar un list<string> con los valores de una cadena especificada con un separador ";" o "|"
        /// </summary>
        /// <param name="sListaCadena">Objeto a trabajar</param>
        /// <returns>Regresa una lista de strings con la lista de contactos a los que enviara el correo.</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static List<string> ToListString(string sListaCadena)
        {
            List<string> sListaD = new List<string>();
            char[] cDelim = { ';', '|' };

            string[] sListaDist = sListaCadena.Split(cDelim);

            foreach (string sItem in sListaDist)
            {
                sListaD.Add(sItem);
            }

            return sListaD;
        }

        /// <summary>
        /// Metodo para obtener la diferencia en DIAS, entre 2 fechas especificadas.
        /// </summary>
        /// <param name="dFechaIni">Fecha Inicial</param>
        /// <param name="dFechaFin">Fecha Final</param>
        /// <returns>Regresa un Integer con el numero de días entre las 2 fechas dadas.</returns>
        /// referencia: -http://desarrollox2.blogspot.mx/2012/11/calcular-dias-transcurridos-entre-dos.html
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static int DaysBetweenDates(DateTime dFechaInicio, DateTime dFechaFinal)
        {
            int nDiferDays = 0;
            TimeSpan tsDiferDays = dFechaFinal - dFechaInicio;

            nDiferDays = tsDiferDays.Days;

            return nDiferDays;
        }

        /// <summary>
        /// Metodo para obtener la semana a la que pertenece una fecha especificada.
        /// </summary>
        /// <param name="dFecha">Fecha</param>
        /// <returns>Regresa un Integer con el numero de semana a la que pertenece la fecha especificada.</returns>
        /// referencia: -http://desarrollox2.blogspot.mx/2012/11/calcular-dias-transcurridos-entre-dos.html
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static int GetWeekNumber(DateTime dFecha)
        {
            int nNumeroSemana = 0;

            System.Globalization.Calendar cCalendario = CultureInfo.CurrentCulture.Calendar;
            nNumeroSemana = cCalendario.GetWeekOfYear(dFecha, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Sunday);

            return nNumeroSemana;
        }

        /// <summary>
        /// Metodo para obtener la fecha de inicio de una semana especificada.
        /// </summary>
        /// <param name="dFecha">nSemana</param>
        /// <param name="nAnio">nAnio</param>
        /// <returns>Regresa un datetime con la fecha de inicio de la semana especificada.</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static DateTime GetDateStartWeek(int nSemana, int nAnio)
        {
            DateTime dtStDateY = new DateTime(nAnio, 1, 1);
            DateTime dtInidate;
            int day = (int)dtStDateY.DayOfWeek - 1;
            int delta = (day < 4 ? -day : 7 - day) + 7 * (nSemana - 1);

            dtInidate = dtStDateY.AddDays(delta);

            return dtInidate;
        }

        /// <summary>
        /// Metodo para obtener la fecha de termino de una semana especificada.
        /// </summary>
        /// <param name="dFecha">nSemana</param>
        /// <param name="nAnio">nAnio</param>
        /// <returns>Regresa un datetime con la fecha de termino de la semana especificada.</returns>
        public static DateTime GetDateEndWeek(int nSemana, int nAnio)
        {
            DateTime dtFinDate = GetDateStartWeek(nSemana, nAnio);
            dtFinDate = dtFinDate.AddDays(6);

            return dtFinDate;
        }
    }
}
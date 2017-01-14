using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Utilidades.Extensiones
{

    /// <summary>
    /// Pivots the data
    /// </summary>
    public class Pivot
    {
        private readonly DataTable _sourceTable = new DataTable();
        private readonly IEnumerable<DataRow> _source = new List<DataRow>();

        public Pivot(DataTable sourceTable)
        {
            _sourceTable = sourceTable;
            _source = sourceTable.Rows.Cast<DataRow>();
        }

        /// <summary>
        /// Pivots the DataTable based on provided RowField, DataField, Aggregate Function and ColumnFields.//
        /// </summary>
        /// <param name="rowField">The column name of the Source Table which you want to spread into rows</param>
        /// <param name="dataField">The column name of the Source Table which you want to spread into Data Part</param>
        /// <param name="aggregate">The Aggregate function which you want to apply in case matching data found more than once</param>
        /// <param name="columnFields">The List of column names which you want to spread as columns</param>
        /// <returns>A DataTable containing the Pivoted Data</returns>
        public DataTable PivotData(string rowField, string dataField, AggregateFunction aggregate, params string[] columnFields)
        {
            DataTable dt = new DataTable();
            const string separator = ".";
            List<string> rowList = _source.Select(x => x[rowField].ToString()).Distinct().ToList();
            // Gets the list of columns .(dot) separated.
            var colList = _source.Select(x => (columnFields.Select(n => x[n]).Aggregate((a, b) => a += separator + b.ToString())).ToString()).Distinct();

            dt.Columns.Add(rowField);
            foreach (var colName in colList)
                dt.Columns.Add(colName);  // Cretes the result columns.//

            foreach (string rowName in rowList)
            {
                DataRow row = dt.NewRow();
                row[rowField] = rowName;
                foreach (string colName in colList)
                {
                    string strFilter = rowField + " = '" + rowName + "'";
                    string[] strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                    row[colName] = GetData(strFilter, dataField, aggregate);
                }
                dt.Rows.Add(row);
            }


            return dt;
        }

        public DataTable PivotData(string rowField, string dataField, AggregateFunction aggregate, bool blnTotales, params string[] columnFields)
        {
            DataTable dt = new DataTable();
            const string separator = ".";
            List<string> rowList = _source.Select(x => x[rowField].ToString()).Distinct().ToList();
            // Gets the list of columns .(dot) separated.
            var colList = _source.Select(x => (columnFields.Select(n => x[n]).Aggregate((a, b) => a += separator + b.ToString())).ToString()).Distinct();

            dt.Columns.Add(rowField);
            foreach (var colName in colList)
                dt.Columns.Add(colName);  // Cretes the result columns.//

            if (blnTotales)
                dt.Columns.Add("-Total-");

            foreach (string rowName in rowList)
            {
                DataRow row = dt.NewRow();
                row[rowField] = rowName;
                foreach (string colName in colList)
                {
                    string strFilter = rowField + " = '" + rowName + "'";
                    string[] strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";

                    row[colName] = GetData(strFilter, dataField, aggregate);

                    if (row[colName].ToString() == String.Empty)
                        row[colName] = 0;
                }

                if (blnTotales)
                {
                    string strFilterTotal = rowField + " = '" + rowName + "'";
                    row["-Total-"] = GetData(strFilterTotal, dataField, aggregate);
                }

                dt.Rows.Add(row);
            }

            if (blnTotales && dt.Rows.Count > 0)
            {
                DataRow row = dt.NewRow();
                row[rowField] = "-Total-";
                foreach (string colName in colList)
                {
                    string strFilter = string.Empty;
                    string[] strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += columnFields[i] + " = '" + strColValues[i] + "'";

                    row[colName] = GetData(strFilter, dataField, aggregate);
                }

                row["-Total-"] = GetData("", dataField, aggregate);

                dt.Rows.Add(row);
            }

            return dt;
        }


        public DataTable PivotData(string rowField, string dataField, string dataField2, AggregateFunction aggregate, bool blnTotales, params string[] columnFields)
        {
            DataTable dt = new DataTable();
            const string separator = ".";
            List<string> rowList = _source.Select(x => x[rowField].ToString()).Distinct().ToList();
            // Gets the list of columns .(dot) separated.
            var colList = _source.Select(x => (columnFields.Select(n => x[n]).Aggregate((a, b) => a += separator + b.ToString())).ToString()).Distinct().OrderBy(n => n);

            dt.Columns.Add(rowField);
            dt.Columns.Add("Datos");
            foreach (var colName in colList)
                dt.Columns.Add(colName);  // Cretes the result columns.//    

            if (blnTotales)
                dt.Columns.Add("-Total-");

            foreach (string rowName in rowList)
            {
                DataRow row = dt.NewRow();
                DataRow row2 = dt.NewRow();
                row[rowField] = rowName;
                row["Datos"] = dataField;
                row2[rowField] = rowName;
                row2["Datos"] = dataField2;
                foreach (string colName in colList)
                {
                    // Primer cantidad
                    string strFilter = rowField + " = '" + rowName + "'";
                    string[] strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                    row[colName] = GetData(strFilter, dataField, aggregate);

                    if (row[colName].ToString() == String.Empty)
                        row[colName] = 0;

                    if (blnTotales)
                    {
                        string strFilterTotal1 = rowField + " = '" + rowName + "'";
                        row["-Total-"] = GetData(strFilterTotal1, dataField, aggregate);
                    }

                    //SEgunda Cantidad 
                    strFilter = rowField + " = '" + rowName + "'";
                    strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                    row2[colName] = GetData(strFilter, dataField2, aggregate);

                    if (row2[colName].ToString() == String.Empty)
                        row2[colName] = 0;

                    if (blnTotales)
                    {
                        string strFilterTotal2 = rowField + " = '" + rowName + "'";
                        row2["-Total-"] = GetData(strFilterTotal2, dataField2, aggregate);
                    }

                }
                dt.Rows.Add(row);
                dt.Rows.Add(row2);
            }


            if (blnTotales && dt.Rows.Count > 0)
            {
                DataRow row = dt.NewRow();
                row[rowField] = "-Total-";
                row["Datos"] = dataField;

                foreach (string colName in colList)
                {
                    string strFilter = string.Empty;
                    string[] strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += columnFields[i] + " = '" + strColValues[i] + "'";

                    row[colName] = GetData(strFilter, dataField, aggregate);
                }

                row["-Total-"] = GetData("", dataField, aggregate);
                dt.Rows.Add(row);

                DataRow row2 = dt.NewRow();
                row2[rowField] = "-Total-";
                row2["Datos"] = dataField2;

                foreach (string colName in colList)
                {
                    string strFilter = string.Empty;
                    string[] strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += columnFields[i] + " = '" + strColValues[i] + "'";

                    row2[colName] = GetData(strFilter, dataField2, aggregate);
                }

                row2["-Total-"] = GetData("", dataField2, aggregate);
                dt.Rows.Add(row2);
            }

            return dt;
        }


        public DataTable PivotData(string rowField, string dataField, string dataField2, AggregateFunction aggregate, params string[] columnFields)
        {
            DataTable dt = new DataTable();
            const string separator = ".";
            List<string> rowList = _source.Select(x => x[rowField].ToString()).Distinct().ToList();
            // Gets the list of columns .(dot) separated.
            var colList = _source.Select(x => (columnFields.Select(n => x[n]).Aggregate((a, b) => a += separator + b.ToString())).ToString()).Distinct();

            dt.Columns.Add(rowField);
            dt.Columns.Add("Datos");
            foreach (var colName in colList)
                dt.Columns.Add(colName);  // Cretes the result columns.//      

            foreach (string rowName in rowList)
            {
                DataRow row = dt.NewRow();
                DataRow row2 = dt.NewRow();
                row[rowField] = rowName;
                row["Datos"] = dataField;
                row2[rowField] = rowName;
                row2["Datos"] = dataField2;
                foreach (string colName in colList)
                {
                    // Primer cantidad
                    string strFilter = rowField + " = '" + rowName + "'";
                    string[] strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                    row[colName] = GetData(strFilter, dataField, aggregate);

                    //SEgunda Cantidad 
                    strFilter = rowField + " = '" + rowName + "'";
                    strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                    row2[colName] = GetData(strFilter, dataField2, aggregate);
                }
                dt.Rows.Add(row);
                dt.Rows.Add(row2);
            }

            return dt;
        }


        public DataTable PivotData(string rowField, string dataField, string dataField2, string dataField3, AggregateFunction aggregate, params string[] columnFields)
        {
            DataTable dt = new DataTable();
            const string separator = ".";
            List<string> rowList = _source.Select(x => x[rowField].ToString()).Distinct().ToList();
            // Gets the list of columns .(dot) separated.
            var colList = _source.Select(x => (columnFields.Select(n => x[n]).Aggregate((a, b) => a += separator + b.ToString())).ToString()).Distinct();

            dt.Columns.Add(rowField);
            dt.Columns.Add("Datos");
            foreach (var colName in colList)
                dt.Columns.Add(colName);  // Cretes the result columns.//      

            foreach (string rowName in rowList)
            {
                DataRow row = dt.NewRow();
                DataRow row2 = dt.NewRow();
                DataRow row3 = dt.NewRow();
                row[rowField] = rowName;
                row["Datos"] = dataField;
                row2[rowField] = rowName;
                row2["Datos"] = dataField2;
                row3[rowField] = rowName;
                row3["Datos"] = dataField3;
                foreach (string colName in colList)
                {
                    // Primer cantidad
                    string strFilter = rowField + " = '" + rowName + "'";
                    string[] strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                    row[colName] = GetData(strFilter, dataField, aggregate);

                    //SEgunda Cantidad 
                    strFilter = rowField + " = '" + rowName + "'";
                    strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                    row2[colName] = GetData(strFilter, dataField2, aggregate);

                    //Tercera Cantidad 
                    strFilter = rowField + " = '" + rowName + "'";
                    strColValues = colName.Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        strFilter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                    row3[colName] = GetData(strFilter, dataField3, aggregate);
                }
                dt.Rows.Add(row);
                dt.Rows.Add(row2);
                dt.Rows.Add(row3);
            }

            return dt;
        }

        public DataTable PivotData(string dataField, AggregateFunction aggregate, string[] rowFields, string[] columnFields)
        {
            DataTable dt = new DataTable();
            const string separator = ".";
            var rowList = _sourceTable.DefaultView.ToTable(true, rowFields).AsEnumerable().ToList();
            for (int index = rowFields.Count() - 1; index >= 0; index--)
                rowList = rowList.OrderBy(x => x.Field<object>(rowFields[index])).ToList();
            // Gets the list of columns .(dot) separated.
            var colList = (from x in _sourceTable.AsEnumerable()
                           select new
                           {
                               Name = columnFields.Select(n => x.Field<object>(n))
                                   .Aggregate((a, b) => a += separator + b.ToString())
                           })
                               .Distinct()
                               .OrderBy(m => m.Name);

            //dt.Columns.Add(RowFields);
            foreach (string s in rowFields)
                dt.Columns.Add(s);

            foreach (var col in colList)
                dt.Columns.Add(col.Name.ToString());  // Cretes the result columns.//

            foreach (var rowName in rowList)
            {
                DataRow row = dt.NewRow();
                string strFilter = string.Empty;

                foreach (string field in rowFields)
                {
                    row[field] = rowName[field];
                    strFilter += " and " + field + " = '" + rowName[field].ToString() + "'";
                }
                strFilter = strFilter.Substring(5);

                foreach (var col in colList)
                {
                    string filter = strFilter;
                    string[] strColValues = col.Name.ToString().Split(separator.ToCharArray(), StringSplitOptions.None);
                    for (int i = 0; i < columnFields.Length; i++)
                        filter += " and " + columnFields[i] + " = '" + strColValues[i] + "'";
                    row[col.Name.ToString()] = GetData(filter, dataField, aggregate);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// Retrives the data for matching RowField value and ColumnFields values with Aggregate function applied on them.
        /// </summary>
        /// <param name="filter">DataTable Filter condition as a string</param>
        /// <param name="dataField">The column name which needs to spread out in Data Part of the Pivoted table</param>
        /// <param name="aggregate">Enumeration to determine which function to apply to aggregate the data</param>
        /// <returns></returns>
        private object GetData(string filter, string dataField, AggregateFunction aggregate)
        {
            try
            {
                DataRow[] filteredRows = _sourceTable.Select(filter);
                object[] objList = filteredRows.Select(x => x.Field<object>(dataField)).ToArray();

                // Se usa para cambiar el Aggregate a Count en el caso de que se le mande un dato no numerico.
                // esto para poder realizar un coteo de los registros cuando se le mande este tipo de dato.
                if (objList.Length != 0)
                {
                    Type type = objList[0].GetType();
                    if (type.Name == "String")
                        aggregate = AggregateFunction.Count;

                    switch (aggregate)
                    {
                        case AggregateFunction.Average:
                            return GetAverage(objList);
                        case AggregateFunction.Count:
                            return objList.Count();
                        case AggregateFunction.Exists:
                            return (!objList.Any()) ? "False" : "True";
                        case AggregateFunction.First:
                            return GetFirst(objList);
                        case AggregateFunction.Last:
                            return GetLast(objList);
                        case AggregateFunction.Max:
                            return GetMax(objList);
                        case AggregateFunction.Min:
                            return GetMin(objList);
                        case AggregateFunction.Sum:
                            return GetSum(objList);
                        default:
                            return null;
                    }
                }
                    return 0;
            }
            catch (Exception ex)
            {
                return string.Format("#Error:{0}",ex.Message);
            }
        }

        private object GetAverage(object[] objList)
        {
            return !objList.Any() ? null : (object)(Convert.ToDecimal(GetSum(objList)) / objList.Count());
        }
        private object GetSum(object[] objList)
        {
            return !objList.Any() ? null : (object)(objList.Aggregate(new decimal(), (x, y) => x += Convert.ToDecimal(y)));
        }
        private object GetFirst(object[] objList)
        {
            return (!objList.Any()) ? null : objList.First();
        }
        private object GetLast(object[] objList)
        {
            return (!objList.Any()) ? null : objList.Last();
        }
        private object GetMax(object[] objList)
        {
            return (!objList.Any()) ? null : objList.Max();
        }
        private object GetMin(object[] objList)
        {
            return (!objList.Any()) ? null : objList.Min();
        }
    }

    public enum AggregateFunction
    {
        Count = 1,
        Sum = 2,
        First = 3,
        Last = 4,
        Average = 5,
        Max = 6,
        Min = 7,
        Exists = 8
    }
}
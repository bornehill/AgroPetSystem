using System;
using Excel;
using System.IO;

namespace Utilidades.Excel
{
    //// Excel.dll : https://exceldatareader.codeplex.com/
    
    using System.Data;
    using System.IO;

    public static class ExcelImportUtil
    {
        private const string DataSetName = "Workbook";
        private const string DataTableName = "Sheet";

        #region Metodos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sFilePath">Path of file</param>
        /// <returns></returns>
        public static DataSet ImportDataTable(string sFilePath)
        {
            FileStream stream = null;
            IExcelDataReader excelReader = null;

            try
            {
                stream = File.Open(sFilePath, FileMode.Open, FileAccess.Read);

                string[] ext = sFilePath.Split('.');
                if (ext.Length > 1)
                {
                    if (ext[ext.Length - 1].Length > 0)
                    {
                        string sExtencion = ext[ext.Length - 1];
                        //1. Reading from a binary Excel file ('97-2003 format; *.xls)
                        string patronExt = "(XLS|xls)$";
                        if (System.Text.RegularExpressions.Regex.IsMatch(sExtencion, patronExt))
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream); ////1.1. DataSet - The result of each spreadsheet will be created in the result.Tables
                        //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                        patronExt = "(XLSX|xlsx)$";
                        if (System.Text.RegularExpressions.Regex.IsMatch(sExtencion, patronExt))
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); ////2.1. DataSet - The result of each spreadsheet will be created in the result.Tables
                    }
                }

                ////3. DataSet - Create column names from first row
                if (excelReader != null)
                {
                    excelReader.IsFirstRowAsColumnNames = true;

                    DataSet dsWorkbook = excelReader.AsDataSet();
                    dsWorkbook.DataSetName = DataSetName;

                    excelReader.Close();
                    return dsWorkbook;

                    //4. Data Reader methods
                    //while (excelReader.Read())
                    //{ //excelReader.GetInt32(0); }
                    ////5. Free resources (IExcelDataReader is IDisposable)
                    //excelReader.Close();
                }
            }
            catch (IOException ex)
            {
                throw new System.ArgumentException("Error to read file ", stream.Name, ex);
            }
            catch (Exception e)
            {
                throw e;//throw new System.ArgumentException("Error to import the file ", " Ds.Excel.ImportDataTable", e);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sFilePath">Path of file</param>
        /// <param name="iSheetIndex">Sheet index of Workbook</param>
        /// <returns></returns>
        public static DataTable ImportDataTable(string sFilePath, int iSheetIndex)
        {
            DataTable dtSheet = null;
            try
            {
                DataSet dsWorkbook = ImportDataTable(sFilePath);
                if (dsWorkbook != null)
                {
                    if (iSheetIndex < 0)
                        return null;

                    dtSheet = dsWorkbook.Tables[iSheetIndex];
                    dtSheet.TableName = DataTableName;
                }
            }
            catch (Exception e)
            {
                throw e;// new System.ArgumentException("Error to read file", " Dt.Excel.ImportDataTable", e);
                //throw new System.ArgumentException("Error to read file");
            }

            return dtSheet;
        }

        #endregion

    }
}
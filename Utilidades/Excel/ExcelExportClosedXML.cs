namespace Utilidades.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using ClosedXML.Excel;
    using System.Web;

    public class ExcelExportClosedXML
    {
        public static XLWorkbook ExportDataTable(DataTable datos)
        {
            var wbExcel = new XLWorkbook();

            if (string.IsNullOrEmpty(datos.TableName))
            {
                datos.TableName = "Datos";
            }

            wbExcel.Worksheets.Add(datos);
            return wbExcel;
        }

        public static XLWorkbook ExportDataTableHide(DataTable datos, int columna)
        {
            var wbExcel = new XLWorkbook();

            if (string.IsNullOrEmpty(datos.TableName))
            {
                datos.TableName = "Datos";
            }

            wbExcel.Worksheets.Add(datos).Column(columna).Hide();
            return wbExcel;
        }
    }
}

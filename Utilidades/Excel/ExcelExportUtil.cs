namespace Utilidades.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Web;

    using Utilidades.Extensiones;

    /// <summary>
    /// Clase que exporta y descarga a Excel diferentes origenes de información
    /// </summary>
    public static class ExcelExportUtil
    {
        #region Metodos
        /// <summary>
        /// Método que agrega el encabezado al reporte
        /// </summary>
        /// <param name="sw">StreamWriter que se manda al cliente</param>
        private static void Encabezado(ref StreamWriter sw)
        {
            sw.WriteLine("<?xml version=\"1.0\"?>");
            sw.WriteLine("<?mso-application progid=\"Excel.Sheet\"?>");
            sw.WriteLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sw.WriteLine("xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            sw.WriteLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            sw.WriteLine("xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"");
            sw.WriteLine("xmlns:html=\"http://www.w3.org/TR/REC-html40\">");
            sw.WriteLine("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">");
            sw.WriteLine("<Author>TAMESIS FUNDICION</Author>");
            sw.WriteLine("<LastAuthor>TAMESIS FUNDICION</LastAuthor>");
            sw.WriteLine(string.Format("<Created>{0}T{1}Z</Created>", DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm:ss")));
            sw.WriteLine("<Company>Nissan Mexicana, S.A. de C.V.</Company>");
            sw.WriteLine("<Version>12.00</Version>");
            sw.WriteLine("</DocumentProperties>");

            sw.WriteLine("<ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            sw.WriteLine("<WindowHeight>12015</WindowHeight>");
            sw.WriteLine("<WindowWidth>19035</WindowWidth>");
            sw.WriteLine("<WindowTopX>120</WindowTopX>");
            sw.WriteLine("<WindowTopY>15</WindowTopY>");
            sw.WriteLine("<ActiveSheet>1</ActiveSheet>");
            sw.WriteLine("<ProtectStructure>False</ProtectStructure>");
            sw.WriteLine("<ProtectWindows>False</ProtectWindows>");
            sw.WriteLine("</ExcelWorkbook>");

            sw.WriteLine("<Styles>");
            sw.WriteLine("<Style ss:ID=\"Default\" ss:Name=\"Normal\">");
            sw.WriteLine("<Alignment ss:Vertical=\"Bottom\"/>");
            sw.WriteLine("<Borders/>");
            sw.WriteLine("<Font ss:FontName=\"Calibri\" x:Family=\"Swiss\" ss:Size=\"11\" ss:Color=\"#000000\"/>");
            sw.WriteLine("<Interior/>");
            sw.WriteLine("<NumberFormat/>");
            sw.WriteLine("<Protection/>");
            sw.WriteLine("</Style>");
            sw.WriteLine("<Style ss:ID=\"s62\">");
            sw.WriteLine("<Font ss:FontName=\"Calibri\" x:Family=\"Swiss\" ss:Size=\"11\" ss:Color=\"#000000\"");
            sw.WriteLine("ss:Bold=\"1\"/>");
            sw.WriteLine("</Style>");
            sw.WriteLine("</Styles>");

            sw.Flush();
        }

        /// <summary>
        /// Método que agrega el pie del documento
        /// </summary>
        /// <param name="sw">StreamWriter que se usara</param>
        private static void PiePagina(StreamWriter sw)
        {
            sw.WriteLine("</Workbook>");

            sw.Flush();
            sw.Close();
            sw.Dispose();
            sw = null;
        }

        /// <summary>
        /// Método que crea el cuerpo del reporte
        /// </summary>
        /// <param name="dtDatos">DataTable con los datos</param>
        /// <param name="sw">StreamWriter para escribir el reporte</param>
        private static void Generar_Reporte_DataTable(DataTable dtDatos, ref StreamWriter sw)
        {
            int nFila = 0;
            int nHoja = 1;

            sw.WriteLine(string.Format("<Worksheet ss:Name=\"{0}\">", nHoja));
            sw.WriteLine("<Table>");
            sw.WriteLine("<Row>");

            foreach (DataColumn dcDatos in dtDatos.Columns)
            {
                sw.WriteLine("<Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">");
                sw.WriteLine(dcDatos.ColumnName);
                sw.WriteLine("</Data></Cell>");
            }
            sw.WriteLine("</Row>");

            sw.Flush();

            foreach (DataRow drDatos in dtDatos.Rows)
            {
                // Imprmir fila por fila
                nFila++;

                sw.WriteLine("<Row>");
                for (int nColumna = 0; nColumna < dtDatos.Columns.Count; nColumna++)
                {
                    sw.WriteLine("<Cell><Data ss:Type=\"String\">");
                    sw.WriteLine(drDatos[nColumna].ToString());

                    sw.WriteLine("</Data></Cell>");
                }

                sw.WriteLine("</Row>");

                // Si el número de fila es 60000 entonces generar otra hoja de excel
                if (nFila % 60000 == 0)
                {
                    nHoja++;

                    // Se cierra la hoja
                    sw.WriteLine("</Table>");
                    sw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
                    sw.WriteLine("<PageSetup>");
                    sw.WriteLine("<Header x:Margin=\"0.3\"/>");
                    sw.WriteLine("<Footer x:Margin=\"0.3\"/>");
                    sw.WriteLine("<PageMargins x:Bottom=\"0.75\" x:Left=\"0.7\" x:Right=\"0.7\" x:Top=\"0.75\"/>");
                    sw.WriteLine("</PageSetup>");
                    sw.WriteLine("<ProtectObjects>False</ProtectObjects>");
                    sw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
                    sw.WriteLine("</WorksheetOptions>");
                    sw.WriteLine("</Worksheet>");

                    // Se crea nueva hoja
                    sw.WriteLine(string.Format("<Worksheet ss:Name=\"{0}\">", nHoja));
                    sw.WriteLine("<Table>");
                    sw.WriteLine("<Row>");

                    foreach (DataColumn dcDatos in dtDatos.Columns)
                    {
                        sw.WriteLine("<Cell ss:StyleID=\"s62\"><Data ss:Type=\"String\">");

                        sw.WriteLine(dcDatos.ColumnName);

                        sw.WriteLine("</Data></Cell>");
                    }

                    sw.WriteLine("</Row>");
                    sw.Flush();
                }
            }

            // Se cierra la hoja
            sw.WriteLine("</Table>");
            sw.WriteLine("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">");
            sw.WriteLine("<PageSetup>");
            sw.WriteLine("<Header x:Margin=\"0.3\"/>");
            sw.WriteLine("<Footer x:Margin=\"0.3\"/>");
            sw.WriteLine("<PageMargins x:Bottom=\"0.75\" x:Left=\"0.7\" x:Right=\"0.7\" x:Top=\"0.75\"/>");
            sw.WriteLine("</PageSetup>");
            sw.WriteLine("<ProtectObjects>False</ProtectObjects>");
            sw.WriteLine("<ProtectScenarios>False</ProtectScenarios>");
            sw.WriteLine("</WorksheetOptions>");
            sw.WriteLine("</Worksheet>");           

            sw.Flush();
        }

        /// <summary>
        /// Método que exporta y descarga en formato XLS un DataTable
        /// </summary>
        /// <param name="datos">DataTable con la información</param>
        /// <param name="nombreArchivo">Nombre del archivo XLS</param>
        /// <param name="rutaDescarga">Ruta de descarga (no necesario)</param>
        /// <param name="usuario">Clave usuario</param>
        public static void ExportDataTable(DataTable datos, string nombreArchivo, string rutaDescarga, string usuario)
        {
            StreamWriter sw = new StreamWriter(HttpContext.Current.Response.OutputStream);
            //StreamWriter sw = new StreamWriter(@"D:\\X.txt");

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}.xls", nombreArchivo));
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            Encabezado(ref sw);

            if (!datos.IsNull())
            {
                Generar_Reporte_DataTable(datos, ref sw);
            }
            PiePagina(sw);

            datos.Dispose();
            datos = null;

            HttpContext.Current.Response.End(); 
            //HttpContext.Current.ApplicationInstance.CompleteRequest(); 
        }

        #endregion
    }
}
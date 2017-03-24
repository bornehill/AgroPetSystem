using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

//using PPSDataTypes.Interfaces;
using Utilidades.Ftp;

using System.IO;
using System.Net;
using System.Web;
using System.IO.Compression;
using Utilidades.Controles;
using System.Configuration;

//using PPSDataTypes.Exceptions;

//By Sinersys
//Created by Emmanuel lohora mejia
//Fecha : 7/28/2011
namespace Utilidades.Ftp
{
    [Serializable]
    public class FTPService : IFTPService
    {
        #region IFTPService Members        
        private const string cs_RutaLogs = "/InterfacesDist/LOGS/";
        public Exception Error { get; set; }
        /// <summary>
        /// Sube Un archivo al Servidor FTP.
        /// </summary>
        /// <param name="pFTPCon"></param>
        /// <param name="filename"></param>
        public void Upload(FTPConnector pFTPCon, string filename)
        {
            try
            {
                FileInfo fileInf = new FileInfo(filename);
                string address = pFTPCon.Adress + @"/" + fileInf.Name;
                FtpWebRequest reqFTP;

                // Create FtpWebRequest object from the Uri provided
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));

                // Provide the WebPermission Credintials
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);

                // By default KeepAlive is true, where the control connection is not closed
                // after a command is executed.
                reqFTP.KeepAlive = false;

                // Specify the command to be executed.
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

                // Specify the data transfer type.
                reqFTP.UseBinary = true;

                // Notify the server about the size of the uploaded file
                reqFTP.ContentLength = fileInf.Length;

                // The buffer size is set to 2kb
                byte[] buff = new byte[fileInf.Length];

                int contentLen;

                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
                FileStream fs = fileInf.OpenRead();

                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buff.Length);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buff.Length);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                //ExceptionInterfases FirmaException = new ExceptionInterfases("Upload(FTPConnector pFTPCon, string filename) , nombre archivo" + filename , ex);
                //NotificacionEmail notificacion = new NotificacionEmail("Error en SMEHL", FirmaException);
                //NotificacionEmailService NS = new NotificacionEmailService();
                //NS.EnviarNotificacion(notificacion);
            }
        }

        /// <summary>
        /// Sube Un archivo al Servidor FTP a través de un array de bytes.
        /// </summary>
        /// <param name="pFTPCon"></param>
        /// <param name="filename"></param>
        public void Upload(FTPConnector pFTPCon, Byte[] file, String filename)
        {
            try
            {
                string address = string.Empty;
                Stream stream = null;
                Stream strm = null;
                try
                {
                    // Create FtpWebRequest object from the Uri provided
                    address = pFTPCon.Adress + @"/" + filename;
                    FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));
                    // Provide the WebPermission Credintials
                    reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                    // By default KeepAlive is true, where the control connection is not closed after a command is executed.
                    reqFTP.KeepAlive = false;
                    // Specify the command to be executed.
                    reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                    // Specify the data transfer type.
                    reqFTP.UseBinary = true;
                    // Notify the server about the size of the uploaded file
                    reqFTP.ContentLength = file.Length;//fileInf.Length;
                    // Stream to which the file to be upload is written
                    strm = reqFTP.GetRequestStream();
                    // Create stream to read the file to be uploaded
                    stream = new MemoryStream(file);
                    // Read from the file stream 2kb at a time
                    int contentLen = stream.Read(file, 0, file.Length);
                    // Till Stream content ends
                    while (contentLen != 0)
                    {
                        // Write Content from the file stream to the FTP Upload Stream
                        strm.Write(file, 0, contentLen);
                        contentLen = stream.Read(file, 0, file.Length);
                    }
                }
                catch (Exception ex)
                {
                    //ExceptionInterfases FirmaException = new ExceptionInterfases("Upload(FTPConnector pFTPCon, Byte[] file, String filename), Nombre del archivo: " + filename, ex);
                    //NotificacionEmail notificacion = new NotificacionEmail("Error en SMEHL", FirmaException);
                    //NotificacionEmailService NS = new NotificacionEmailService();
                    //NS.EnviarNotificacion(notificacion);
                }
                finally
                {
                    // Close the file stream and the Request Stream
                    if (stream != null) stream.Close();
                    if (strm != null) strm.Close();
                    if (stream != null) stream.Dispose();
                    if (strm != null) strm.Dispose();
                }
            }
            catch (Exception ex)
            {
                //ExceptionInterfases FirmaException = new ExceptionInterfases("Upload(FTPConnector pFTPCon, Byte[] file, String filename), Nombre del archivo: " + filename, ex);
                //NotificacionEmail notificacion = new NotificacionEmail("Error en SMEHL", FirmaException);
                //NotificacionEmailService NS = new NotificacionEmailService();
                //NS.EnviarNotificacion(notificacion);
            }

        }

        /// <summary>
        /// Sube Un archivo al Servidor FTP.
        /// </summary>
        /// <param name="pFTPCon"></param>
        /// <param name="filename"></param>
        public void UploadWeb(FTPConnector pFTPCon, string filename, byte[] byteimagen)
        {

            string address = pFTPCon.Adress + @"/" + filename;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);

            reqFTP.KeepAlive = false;

            reqFTP.UseBinary = true;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            reqFTP.ContentLength = byteimagen.Length;

            byte[] buff = new byte[byteimagen.Length];

            try
            {
                Stream strm = reqFTP.GetRequestStream();
                MemoryStream ms = new MemoryStream(byteimagen);
                int contentLen = ms.Read(buff, 0, buff.Length);
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = ms.Read(buff, 0, buff.Length);
                }
                strm.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Sube Un archivo al Servidor FTP.
        /// </summary>
        /// <param name="pFTPCon"></param>
        /// <param name="filename"></param>
        public void UploadWeb(FTPConnector pFTPCon, string filename, byte[] byteimagen, bool bArchivoUsuario)
        {
            string address;
            FtpWebRequest reqFTP;

            if (bArchivoUsuario.Equals(true))
                address = pFTPCon.Adress + pFTPCon.locationFolder + @"/" + filename;
            else
                address = pFTPCon.Adress + @"/" + filename;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);

            reqFTP.KeepAlive = false;

            reqFTP.UseBinary = true;
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            reqFTP.ContentLength = byteimagen.Length;

            byte[] buff = new byte[byteimagen.Length];

            try
            {
                Stream strm = reqFTP.GetRequestStream();
                MemoryStream ms = new MemoryStream(byteimagen);
                int contentLen = ms.Read(buff, 0, buff.Length);
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = ms.Read(buff, 0, buff.Length);
                }
                strm.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
  

        /// <summary>
        /// Borrar Archivo FTP
        /// </summary>
        /// <param name="pFTPCon"></param>
        /// <param name="fileName"></param>
        public void DeleteFile(FTPConnector pFTPCon, string fileName)
        {
            try
            {
                string address = pFTPCon.Adress + pFTPCon.locationFolder + @"/" + fileName;
                FtpWebRequest reqFTP;

                // Create FtpWebRequest object from the Uri provided
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));

                // Provide the WebPermission Credintials
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);

                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteFile(string FullPathFile)
        {
            try
            {
                File.Delete(FullPathFile);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("{0}: {1}", ex.Message, FullPathFile));
            }
        }

        public void DeleteFolder(FTPConnector pFTPCon, string FolderName)
        {
            try
            {
                DirectoryInfo DirInf = new DirectoryInfo(FolderName);
                string address = pFTPCon.Adress + @"/" + DirInf.Name;
                FtpWebRequest reqFTP;

                // Create FtpWebRequest object from the Uri provided
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));

                // Provide the WebPermission Credintials
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);

                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Verifica si el archivo existe en la direccion FTP.
        /// </summary>
        /// <param name="pFTPCon"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool ftpFileExist(FTPConnector pFTPCon, string fileName)
        {
            WebClient wc = new WebClient();
            bool exist = false;
            try
            {
                wc.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                byte[] fData = wc.DownloadData(pFTPCon.Adress + pFTPCon.locationFolder + @"/" + fileName);
                if (fData.Length > -1)
                    exist = true;
                else
                    exist = false;
            }
            catch (Exception ex)
            {
                exist = false;
            }
            return exist;
        }

        /// <summary>
        /// Descarga un Archivo desde una direccion remota via FTP
        /// </summary>
        /// <param name="pFTPCon"></param>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public void Download(FTPConnector pFTPCon, string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                //filePath = <<The full path where the file is to be created.>>, 
                //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(pFTPCon.Adress + "/" + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Función para bajar un archivo
        /// en matriz de bytes
        /// </summary>
        /// <param name="pFtpCon"></param>
        /// <param name="strFile"></param>
        /// <returns></returns>
        public byte[] DownloadFile(FTPConnector pFtpCon, string strFile)
        {
            try
            {

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(String.Concat(pFtpCon.Adress, "/", strFile.Trim())));
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(pFtpCon.User, pFtpCon.Password);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                byte[] fileContents = Encoding.UTF8.GetBytes(reader.ReadToEnd());

                reader.Close();
                response.Close();

                return fileContents;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public byte[] DownloadData(FTPConnector pFtpCon, string strFile)
        {
            try
            {
                WebClient client = new WebClient();

                string strAddress = String.Concat("ftp://", pFtpCon.User, ":", pFtpCon.Password, "@", String.Concat(pFtpCon.Adress, "/", strFile).Replace('\\', '/').Replace("ftp://", " ").Trim());

                byte[] fileContents = client.DownloadData(strAddress);

                return fileContents;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Regresa un listado de archivos 
        /// </summary>
        /// <param name="ppFTPCon"></param>
        /// <returns></returns>
        public string[] GetFilesDetailList(FTPConnector ppFTPCon)
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ppFTPCon.Adress + "/"));
                ftp.Credentials = new NetworkCredential(ppFTPCon.User, ppFTPCon.Password);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;


                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }

                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');

            }
            catch (Exception ex)
            {
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public string[] GetFileList(FTPConnector ppFTPCon)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ppFTPCon.Adress + ppFTPCon.locationFolder + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ppFTPCon.User, ppFTPCon.Password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //MessageBox.Show(reader.ReadToEnd());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public long GetFileSize(FTPConnector pFTPCon, string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            string address = pFTPCon.Adress + @"/" + fileInf.Name;

            FtpWebRequest reqFTP;

            long fileSize = 0;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                fileSize = response.ContentLength;

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }
            return fileSize;
        }

        public bool RenombrarArchivo(FTPConnector pFTPCon, string currentFilename, string newFilename, ref Exception exref)
        {
            bool renombrado = false;
            FileInfo fileInf = new FileInfo(currentFilename);
            string address = pFTPCon.Adress + @"/" + fileInf.Name;
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
                renombrado = true;
                exref = null;
            }
            catch (Exception ex)
            {
                renombrado = false;
                exref = ex;
            }
            return renombrado;
        }

        public bool RenombrarArchivo(FTPConnector pFTPCon, string currentFilename, string newFilename)
        {
            bool renombrado = false;
            FileInfo fileInf = new FileInfo(currentFilename);
            string address = pFTPCon.Adress + @"/" + fileInf.Name;
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
                renombrado = true;
            }
            catch (Exception ex)
            {
                renombrado = false;
            }
            return renombrado;
        }

        public void CrearDirectorio(FTPConnector pFTPCon, string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                // dirName = name of the directory to create.
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(pFTPCon.Adress + "/" + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public string LeerArchivoXML(FTPConnector pFTPCon, string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            FtpWebRequest reqFTP;
            string address = pFTPCon.Adress + @"/" + fileInf.Name;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));
            string xmlFile = "";
            try
            {
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                xmlFile = reader.ReadToEnd();
            }
            catch (Exception)
            {

                throw;
            }

            return xmlFile;
        }

        public List<string> LeerArchivoTxt(FTPConnector pFTPCon, string filename)
        {
            FileInfo fileInf = new FileInfo(filename);
            FtpWebRequest reqFTP;
            string address = pFTPCon.Adress + pFTPCon.locationFolder + @"/" + fileInf.Name;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));
            List<string> TxtLineas = new List<string>();
            try
            {
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string linia = "";
                while ((linia = reader.ReadLine()) != null)
                {
                    TxtLineas.Add(linia);
                }
            }
            catch (Exception)
            {
            }

            return TxtLineas;
        }

        public List<string> LeerArchivoTxt(StreamReader reader)
        {
            List<string> TxtLineas = new List<string>();
            try
            {
                string linia = "";
                while ((linia = reader.ReadLine()) != null)
                {
                    TxtLineas.Add(linia);
                }
            }
            catch (Exception ex)
            {
                Error = new ApplicationException("Error LeerArchivoTxt :", ex.InnerException);
                return null;
            }
            return TxtLineas;
        }

        public DataTable LeerArchivoTxtToDataTable(FTPConnector pFTPCon, string filename, List<ListaLongitudCampos> Llc, DataTable LPEntity)
        {
            FileInfo fileInf = new FileInfo(filename);
            FtpWebRequest reqFTP;
            string address = pFTPCon.Adress + pFTPCon.locationFolder + @"/" + fileInf.Name;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));
            try
            {
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string linia = "";
                while ((linia = reader.ReadLine()) != null)
                {
                    DataRow DrFila = LPEntity.NewRow();
                    for (int i = 0; i < LPEntity.Columns.Count; i++)
                    {
                        DataColumn Dc = LPEntity.Columns[i];                        

                        if (Dc.DataType.Equals(typeof(bool)))
                        {
                            DrFila[i] = (linia.Substring(Llc[i].inicial, Llc[i].longitud).Trim() == "Activo") ? true : false;
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(linia.Substring(Llc[i].inicial, Llc[i].longitud).Trim()) ||
                                    linia.Substring(Llc[i].inicial, Llc[i].longitud).Trim().Equals("00000000"))
                                DrFila[i] = DBNull.Value;
                            else if (Dc.DataType.Equals(typeof(DateTime)))
                                DrFila[i] = DateTime.ParseExact(linia.Substring(Llc[i].inicial, Llc[i].longitud).Trim(), "yyyyMMdd", null).ToShortDateString();
                            else
                                DrFila[i] = linia.Substring(Llc[i].inicial, Llc[i].longitud).Trim();
                        }
                    }
                    LPEntity.Rows.Add(DrFila);
                }
            }
            catch (Exception ex)
            {
            }

            return LPEntity;
        }

        public DataTable LeerArchivoTxtToDataTable(StreamReader reader, List<ListaLongitudCampos> Llc, DataTable LPEntity, List<Exception> Errores)
        {
            string linia = "";
            int x = 1;
            try
            {                
                while ((linia = reader.ReadLine()) != null)
                {
                    try
                    {
                        DataRow DrFila = LPEntity.NewRow();
                        for (int i = 0; i < LPEntity.Columns.Count; i++)
                        {
                            DataColumn Dc = LPEntity.Columns[i];

                            if (Dc.DataType.Equals(typeof(bool)))
                            {
                                DrFila[i] = (linia.Substring(Llc[i].inicial, Llc[i].longitud).Trim() == "Activo") ? true : false;
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(linia.Substring(Llc[i].inicial, Llc[i].longitud).Trim()) ||
                                        linia.Substring(Llc[i].inicial, Llc[i].longitud).Trim().Equals("00000000"))
                                    DrFila[i] = DBNull.Value;
                                else if (Dc.DataType.Equals(typeof(DateTime)))
                                    DrFila[i] = DateTime.ParseExact(linia.Substring(Llc[i].inicial, Llc[i].longitud).Trim(), "yyyyMMdd", null).ToShortDateString();
                                else
                                    DrFila[i] = linia.Substring(Llc[i].inicial, Llc[i].longitud).Trim();
                            }
                        }
                        LPEntity.Rows.Add(DrFila);
                    }
                    catch (Exception ex)
                    {
                        Errores.Add(new Exception(String.Format("{0}, {1}:{2}", ex.Message, x, linia)));
                    }
                    finally
                    {
                        x++;
                    }
                }
                reader.Close();
            }            
            catch (Exception ex)
            {
                Errores.Add(new Exception(String.Format("{0}, {1}:{2}", ex.Message, x, linia)));
            }

            return LPEntity;
        }

        //mueve los archivos a la carpeta de procesados
        public void move_file_processed(FTPConnector pFTPCon, string fileName)
        {
            FtpWebRequest reqFTP;
            FtpWebRequest reqFTPd;
            DateTime dt = DateTime.Now;
            string address = pFTPCon.Adress + pFTPCon.locationFolder + @"/" + fileName;
            string addressd = pFTPCon.Adress + pFTPCon.locationFolder + "/PROCESADOS" +
            @"/" + dt.Day + "_" + dt.Month + "_" + dt.Year + "_" + fileName;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));
            reqFTPd = (FtpWebRequest)FtpWebRequest.Create(new Uri(addressd));
            try
            {
                //informacion para la lectura del archivo
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream strdown = response.GetResponseStream();
                StreamReader reader = new StreamReader(strdown);
                byte[] fileContents = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                //informaicon para la escritura del archivo
                reqFTPd.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                reqFTPd.KeepAlive = false;
                reqFTPd.UseBinary = true;
                reqFTPd.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTPd.ContentLength = fileContents.Length;
                //buffer para la escritura del archivo
                byte[] buff = new byte[fileContents.Length];
                Stream strm = reqFTPd.GetRequestStream();
                MemoryStream ms = new MemoryStream(fileContents);
                int contentLen = ms.Read(buff, 0, buff.Length);
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = ms.Read(buff, 0, buff.Length);
                }
                response.Close();
                strdown.Close();
                reader.Close();
                strm.Close();
                DeleteFile(pFTPCon, fileName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Stream download_archivos_zip(FTPConnector ConFTP, string filename)
        {
            MemoryStream strmExit = new MemoryStream();
            string addres = ConFTP.Adress + ConFTP.locationFolder + @"/" + filename;
            FtpWebRequest FTPrq = (FtpWebRequest)FtpWebRequest.Create(new Uri(addres));
            FTPrq.Method = WebRequestMethods.Ftp.DownloadFile;
            FTPrq.UseBinary = true;
            FTPrq.Credentials = new NetworkCredential(ConFTP.User, ConFTP.Password);
            FtpWebResponse response = (FtpWebResponse)FTPrq.GetResponse();
            Stream strm = response.GetResponseStream();
            long cl = response.ContentLength;
            int bufferSize = 2048;
            int readCount;
            byte[] buffer = new byte[bufferSize];

            readCount = strm.Read(buffer, 0, bufferSize);
            while (readCount > 0)
            {
                strmExit.Write(buffer, 0, readCount);
                readCount = strm.Read(buffer, 0, bufferSize);
            }
            return strmExit;
        }

        public void upload_files_zip(FTPConnector ConFTP, Stream strm, string fileName)
        {
            FtpWebRequest reqFTP;
            string address = ConFTP.Adress + ConFTP.locationFolder + @"/" + fileName;
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(address));
            try
            {
                StreamReader reader = new StreamReader(strm);
                byte[] fileContents = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                //informaicon para la escritura del archivo
                reqFTP.Credentials = new NetworkCredential(ConFTP.User, ConFTP.Password);
                reqFTP.KeepAlive = false;
                reqFTP.UseBinary = true;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.ContentLength = fileContents.Length;
                //buffer para la escritura del archivo
                byte[] buff = new byte[fileContents.Length];
                Stream strmUp = reqFTP.GetRequestStream();
                MemoryStream ms = new MemoryStream(fileContents);
                int contentLen = ms.Read(buff, 0, buff.Length);
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream
                    strmUp.Write(buff, 0, contentLen);
                    contentLen = ms.Read(buff, 0, buff.Length);
                }
                reader.Close();
                strm.Close();
                strmUp.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void write_log_ftp_old(FTPConnector FTPCon, string folder, string fileName, string message)
        {
            System.Threading.Thread.Sleep(1000);
            FTPConnector pFTPCon = (FTPConnector)FTPCon.Clone();
            pFTPCon.locationFolder += folder;
            string path = pFTPCon.Adress + pFTPCon.locationFolder + @"/" + fileName;
            try
            {
                if (ftpFileExist(pFTPCon, fileName))
                {
                    Stream strm = download_archivos_zip(pFTPCon, fileName);
                    strm.Flush();
                    strm.Position = 0;
                    StreamReader reader = new StreamReader(strm);
                    MemoryStream ms = new MemoryStream();
                    string files = reader.ReadToEnd();
                    string[] filestring = files.Split('\n');
                    List<string> Lstring = new List<string>();
                    foreach (string line in filestring)
                    {
                        if (!string.IsNullOrEmpty(line))
                            Lstring.Add(line + '\n');
                    }
                    Lstring.Add(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " : " + message + '\n');
                    foreach (string line in Lstring)
                    {
                        byte[] writeData = Encoding.ASCII.GetBytes(line);
                        ms.Write(writeData, 0, writeData.Length);
                    }
                    ms.Flush();
                    ms.Position = 0;
                    upload_files_zip(pFTPCon, ms, fileName);
                    reader.Close();
                    ms.Close();
                    strm.Close();
                }
                else
                {
                    byte[] writeData = Encoding.ASCII.GetBytes(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " : " + message + '\n');
                    MemoryStream ms = new MemoryStream(writeData);
                    upload_files_zip(pFTPCon, ms, fileName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al escribir en el archivo de Log", ex.InnerException);
            }
        }

        int intentosDisponibles = 10;
        public void write_log_ftp(FTPConnector FTPCon, string folder, string fileName, string message)
        {
            System.Threading.Thread.Sleep(1000);
            FTPConnector pFTPCon = (FTPConnector)FTPCon.Clone();
            pFTPCon.locationFolder += folder;
            string path = pFTPCon.Adress + pFTPCon.locationFolder + @"/";
            try
            {
                WebClient request = new WebClient();
                FtpWebRequest dirFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
                request.Credentials = dirFtp.Credentials = new NetworkCredential(pFTPCon.User, pFTPCon.Password);
                dirFtp.Method = WebRequestMethods.Ftp.ListDirectory;
                String filelist = new StreamReader(dirFtp.GetResponse().GetResponseStream()).ReadToEnd();
                if (filelist.Contains(fileName))
                {
                    byte[] newFileData = request.DownloadData(path + fileName);
                    if (newFileData.Length > -1)
                    {
                        intentosDisponibles = 10;
                        Stream postStream = request.OpenWrite(path + fileName);
                        byte[] writeData = Encoding.ASCII.GetBytes(String.Format("{0}{1} : {2}\n", Encoding.UTF8.GetString(newFileData), DateTime.Now.ToString(CultureInfo.InvariantCulture), message));
                        postStream.Write(writeData, 0, writeData.Length);
                        postStream.Flush();
                        postStream.Close();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    //Se crea el archivo en el ftp
                    upload_files_zip(pFTPCon, new MemoryStream(Encoding.ASCII.GetBytes(String.Format("{0} : {1}\n", DateTime.Now.ToString(CultureInfo.InvariantCulture), message))), fileName);
                }
            }
            catch (Exception ex)
            {
                intentosDisponibles--;
                if (intentosDisponibles <= 0)
                    throw new Exception("Error al escribir en el archivo de Log", ex.InnerException);
                else
                    write_log_ftp(FTPCon, folder, fileName, message);
            }
        }

        public void write_log(string fileName, string messageEnc)
        {
            String message = HttpUtility.HtmlDecode(messageEnc);
            try
            {
                string path = ConfigurationManager.AppSettings["Ftp_RutaFisica"];
                string FullPathName = path + cs_RutaLogs + fileName;
                StreamWriter filewritter;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (File.Exists(FullPathName))
                {
                    FileStream files = new FileStream(FullPathName, FileMode.Append, FileAccess.Write);
                    filewritter = new StreamWriter(files);
                }
                else
                {
                    filewritter = File.CreateText(FullPathName);
                }
                filewritter.WriteLine(String.Format("{0} : {1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), message));
                filewritter.Close();
            }
            catch (Exception ex)
            {
                intentosDisponibles--;
                System.Threading.Thread.Sleep(5000);
                if (intentosDisponibles >= 0)
                    write_log(fileName, string.Format("{0} [{1} Reintento de escritura: #{2}]", message, ex.Message, 10 - intentosDisponibles));
            }
        }
        #endregion
    }
}
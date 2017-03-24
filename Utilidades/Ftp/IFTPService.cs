using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using PPSDataTypes.Entities;
using Utilidades.Ftp;

using System.Web;
using System.IO;

namespace Utilidades.Ftp
{
   public interface IFTPService
    {
       void Upload(FTPConnector FTPCon,string filename);
       void Upload(FTPConnector pFTPCon, Byte[] file, String filename);
       void UploadWeb(FTPConnector FTPCon, string filename, byte[] buffer);
       //void UploadWeb(FTPConnector FTPCon, string filename, byte[] buffer);
       void DeleteFile(FTPConnector FTPCon, string fileName);
       void DeleteFolder(FTPConnector FTPCon, string fileFolder);
       bool ftpFileExist(FTPConnector FTPCon, string fileName);
       void Download(FTPConnector FTPCon, string filePath, string fileName);
       byte[] DownloadFile(FTPConnector FTPCon, string fileName);
       string[] GetFilesDetailList(FTPConnector FTPCon);
       string[] GetFileList(FTPConnector FTPCon);
       long GetFileSize(FTPConnector FTPCon, string filename);
       bool RenombrarArchivo(FTPConnector FTPCon, string currentFilename, string newFilename, ref Exception exref);
       bool RenombrarArchivo(FTPConnector FTPCon, string currentFilename, string newFilename);
       void CrearDirectorio(FTPConnector FTPCon, string dirName);
       string LeerArchivoXML(FTPConnector FTPCon, string filename);
       List<string> LeerArchivoTxt(FTPConnector pFTPCon, string filename);
    }
}
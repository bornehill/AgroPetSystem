using System;
using System.Globalization;

namespace Utilidades.Errores
{
    using System.IO;

    public class Log
    {
        public enum FileType
        {
            Log,
            Erro
        }

        string _sPath;
        public string Path
        {
            get { return _sPath; }
            set { _sPath = value; }
        }

        private string _sFileLog;
        public string FileLog
        {
            get { return _sFileLog; }
            set { _sFileLog = value; }
        }

        private string _sFileError;
        public string FileError
        {
            get { return _sFileError; }
            set { _sFileError = value; }
        }

        public string Message { get; set; }

        public void Add(string sMessage, FileType oType)
        {
            string _sFile=string.Empty;
            _sFile = oType == FileType.Log ? _sFileLog : _sFileError;

            var directInf = new DirectoryInfo(_sPath);
            if (!directInf.Exists)
                directInf.Create();
            var fi = new FileInfo(_sPath + "\\" + _sFile);
            if (!fi.Exists)
                fi.Create().Close();
            StreamWriter sWriter = File.AppendText(_sPath + "\\" + _sFile);
            sWriter.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " : " + sMessage);
            sWriter.Close();
        }

        public void Add(string sPath, string sFile, FileType oType, string sMessage)
        {
            string _sFile = string.Empty;
            _sFile = oType == FileType.Log ? _sFileLog : _sFileError;

            var directInf = new DirectoryInfo(_sPath);
            if (!directInf.Exists)
                directInf.Create();
            var fi = new FileInfo(sPath + "\\" + _sFile);
            if (!fi.Exists)
                fi.Create().Close();
            StreamWriter sWriter = File.AppendText(sPath + "\\" + _sFile);
            sWriter.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture)+" : " + sMessage);
            sWriter.Close();
        }

        public void Add(string sFile, FileType oType, string sMessage)
        {
   

            //var directInf = new DirectoryInfo(_sPath);
            //if (!directInf.Exists)
            //    directInf.Create();
            var fi = new FileInfo(sFile);
            if (!fi.Exists)
                fi.Create().Close();
            StreamWriter sWriter = File.AppendText(sFile);
            sWriter.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture) + " : " + sMessage);
            sWriter.Close();
        }

    }
}
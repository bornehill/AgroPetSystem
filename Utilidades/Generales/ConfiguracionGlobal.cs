namespace Utilidades.Generales
{
    /// <summary>
    /// Clase que contiene la información y métodos de configuración general
    /// </summary>
    public sealed class ConfiguracionGlobal
    {
        #region Campos

        #region File

        private static string _ruta;
        private static string _archivoLog;
        private static string _archivoError;

        public static string Ruta
        {
            get { return _ruta; }
        }

        public static string ArchivoLog
        {
            get { return _archivoLog; }
        }

        public static string ArchivoError
        {
            get { return _archivoError; }
        }

        #endregion
        
        #region Generales

        private static string _CadenaConexion;
        private static string _DirectorioActivoRuta;
        private static string _DirectorioActivoDominio;

        #endregion

        private static string _Idioma;

        #endregion

        #region Propiedades

        /// <summary>
        /// Determina si la encriptación de la configuración debe realizarse
        /// </summary>
        public static bool EncriptarConfiguracion { get; set; }

        /// <summary>
        /// Determina si el proceso de encriptación ya fué realizado
        /// </summary>
        public static bool ConfiguracionCargada { get; set; }

        #region Generales

        /// <summary>
        /// Regresa la cadena de conexión de la base de datos PEA
        /// </summary>
        public static string CadenaConexion
        {
            get { return _CadenaConexion; }
        }

        /// <summary>
        /// Regresa la dirección del directorio activo de la red interna
        /// </summary>
        public static string DirectorioActivoRuta
        {
            get { return _DirectorioActivoRuta; }
        }

        /// <summary>
        /// Regresa el dominio predeterminado que se usa para comprobar la información de los usuarios
        /// en el directorio activo
        /// </summary>
        public static string DirectorioActivoDominio
        {
            get { return _DirectorioActivoDominio; }
        }

        /// <summary>
        /// Regresa el idioma a implementar
        /// </summary>
        public static string Idioma
        {
            get { return _Idioma; }
        }

        #endregion

        #endregion

        #region Métodos

        public static void InicializarConfiguracionLogs(
           string directorioActivoRuta,
           string nombreArchivoLog,
           string nombreArchivoError)
        {
            _ruta = directorioActivoRuta;
            _archivoLog = nombreArchivoLog;
            _archivoError = nombreArchivoError;
            _CadenaConexion = string.Empty;
            _DirectorioActivoRuta = string.Empty;
            _DirectorioActivoDominio = string.Empty;
        }

        /// <summary>
        /// Carga la información general de las variables de configuración global
        /// </summary>
        public static void InicializarConfiguracion(
            string CadenaConexion,
            string DirectorioActivoRuta,
            string DirectorioActivoDominio)
        {
            _CadenaConexion = CadenaConexion;
            _DirectorioActivoRuta = DirectorioActivoRuta;
            _DirectorioActivoDominio = DirectorioActivoDominio;

            _Idioma = "ES";
        }

        public static void InicializarConfiguracion(
           string CadenaConexion,
           string DirectorioActivoRuta,
           string DirectorioActivoDominio
            ,string Idioma)
        {
            _CadenaConexion = CadenaConexion;
            _DirectorioActivoRuta = DirectorioActivoRuta;
            _DirectorioActivoDominio = DirectorioActivoDominio;
            _Idioma = Idioma;
        }

        #endregion
    }
}
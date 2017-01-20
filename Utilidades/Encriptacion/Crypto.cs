namespace Utilidades.Encriptacion
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using Extensiones;
    using Errores;

    public sealed class Crypto : IDisposable
    {
        #region Propiedades
        /// <summary>
        /// Clave de encriptación
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Llave de encriptación
        /// </summary>
        public string Iv { get; set; }

        private const string sKey = @"5TGB&YHN7UJM(IK<";
        private const string sIv = @"!QAZ2WSX#EDC4RFV";

        #endregion
        #region Constructor
        /// <summary>
        /// Constructor de la clase de encriptación
        /// </summary>
        /// <param name="criptographyKey">Clave de encriptación</param>
        /// <param name="criptographyIv">Llave de encriptación</param>
        public Crypto(string criptographyKey, string criptographyIv)
        {
            // Se carga desde el archivo de configuración las llaves de encriptación
            Key = criptographyKey;
            Iv = criptographyIv;
            //strkey = ConfigurationManager.AppSettings["CriptographyKey"];
            //strIV = ConfigurationManager.AppSettings["CriptographyIV"];
        }

        // Crear nueva clase de encriptación
        public Crypto() { }
        #endregion
        #region Métodos
        /// <summary>
        /// Encripta una cadena de texto de acuerdo a la llave y a la clave de encriptación
        /// </summary>
        /// <param name="mensaje">Cadena de texto a encriptar</param>
        /// <returns>string con el mensaje encriptado</returns>
        public string Encriptar(string mensaje)
        {
            byte[] inArray = null;

            try
            {
                if (string.IsNullOrEmpty(mensaje))
                {
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(Key))
                {
                    ControlErrores.Generar_Error_Argumentos("Llave de encriptación no puede estar nula o vacia");
                }

                if (string.IsNullOrEmpty(Iv))
                {
                    ControlErrores.Generar_Error_Argumentos("Vector de encriptación no puede estar nula o vacia");
                }

                //byte[] numArray1 = Convert.FromBase64String(Key);
                byte[] numArray1 = System.Text.Encoding.UTF8.GetBytes(Key);
                //byte[] numArray2 = Convert.FromBase64String(Iv);
                byte[] numArray2 = System.Text.Encoding.UTF8.GetBytes(Iv);

                using (AesCryptoServiceProvider cryptoServiceProvider = new AesCryptoServiceProvider())
                {
                    /////2014/Feb/25
                    cryptoServiceProvider.GenerateIV();
                    cryptoServiceProvider.BlockSize = 128;
                    cryptoServiceProvider.KeySize = 256;

                    cryptoServiceProvider.Key = numArray1;
                    cryptoServiceProvider.IV = numArray2;

                    /////2014/Feb/25
                    cryptoServiceProvider.Mode = CipherMode.CBC;
                    cryptoServiceProvider.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = ((SymmetricAlgorithm)cryptoServiceProvider).CreateEncryptor(cryptoServiceProvider.Key, cryptoServiceProvider.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                            {
                                streamWriter.Write(mensaje);
                            }

                            inArray = memoryStream.ToArray();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ControlErrores.Generar_Error(string.Format("Error en encriptación: {0}", ex.Message));
            }

            if (!inArray.IsNull())
            {
                if (inArray != null) return Convert.ToBase64String(inArray, 0, inArray.Length);
            }
            return null;
        }

        /// <summary>
        /// Descencripta una cadena de texto de acuerdo a la llave y la clave de encriptación
        /// </summary>
        /// <param name="mensaje">Cadena de texto a  desencriptar</param>
        /// <returns>string con la cadena de texto desencriptado</returns>
        public string Desencriptar(string mensaje)
        {
            string str = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(mensaje))
                {
                    return string.Empty;
                }

                if (string.IsNullOrEmpty(Key))
                {
                    ControlErrores.Generar_Error_Argumentos("Llave de encriptación no puede estar nula o vacia");
                }

                if (string.IsNullOrEmpty(Iv))
                {
                    ControlErrores.Generar_Error_Argumentos("Vector de encriptación no puede estar nula o vacia");
                }

                //byte[] numArray1 = Convert.FromBase64String(Key);
                byte[] numArray1 = System.Text.Encoding.UTF8.GetBytes(Key);

                //byte[] numArray2 = Convert.FromBase64String(Iv);
                byte[] numArray2 = System.Text.Encoding.UTF8.GetBytes(Iv);

                byte[] buffer = Convert.FromBase64String(mensaje);

                using (AesCryptoServiceProvider cryptoServiceProvider = new AesCryptoServiceProvider())
                {
                    /////2014/Feb/25
                    cryptoServiceProvider.BlockSize = 128;
                    cryptoServiceProvider.KeySize = 128;

                    cryptoServiceProvider.Key = numArray1;
                    cryptoServiceProvider.IV = numArray2;

                    /////2014/Feb/25
                    cryptoServiceProvider.Mode = CipherMode.CBC;
                    cryptoServiceProvider.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = ((SymmetricAlgorithm)cryptoServiceProvider).CreateDecryptor(cryptoServiceProvider.Key, cryptoServiceProvider.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                str = streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ControlErrores.Generar_Error(string.Format("Error en desencriptación: {0}", ex.Message));
            }

            return str;
        }
        #endregion
        #region Funciones
        #endregion
        #region IDisposable Members
        void IDisposable.Dispose()
        {
            Key = string.Empty;
            Iv = string.Empty;

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
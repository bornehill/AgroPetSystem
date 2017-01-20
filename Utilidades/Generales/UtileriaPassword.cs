using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Utilidades.Generales
{
    public class UtileriaPassword
    {
        #region Atributos        
        #endregion Atributos

        #region Propiedades
        public string Password { get; set; }
        public string PasswordEncriptado { get; set; }
        #endregion Propiedades

        #region Constructores

        public UtileriaPassword(string pwd)
        {
            Password = pwd;
            PasswordEncriptado = EncryptSHA256Managed(Password);
        }

        public UtileriaPassword()
        {
            Password = GeneraPassword();
            //PasswordEncriptado = EncryptSHA256Managed(Password);
            
        }
        #endregion Constructores


        #region Metodos
        public string GeneraPassword()
        {
            string resultado = string.Empty;        
            string nuevocaracter = string.Empty;
            int longitud = 0;
            int ncar;
            Random r = new Random();
            longitud = r.Next(10, 15);
            int contador = 0;
            while (contador <= longitud)
            {
                ncar = r.Next(1, 255);
                if ((ncar >= 48 && ncar <= 57) || (ncar >= 65 && ncar <= 90) || (ncar >= 97 && ncar <= 122))
                {
                    char c = (char)ncar;
                    resultado = resultado + c;
                    contador++;
                }
            }
            return resultado;
        }      


        public string EncryptSHA256Managed(string ClearString)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            Byte[] bytClearString = uEncode.GetBytes(ClearString);
            SHA256Managed sha = new SHA256Managed();
            Byte[] hash = sha.ComputeHash(bytClearString);
            return Convert.ToBase64String(hash);
        }

        public bool ValidaPassword(ref string p)
        {
            int digitos = 0;
            int mayusculas = 0;
            int minusculas = 0;
            if (p.Length < 10) return false;
            foreach (char c in p)
            {
                int a = (int)c;
                if (!(
                    (a >= 48 && a <= 57) ||
                    (a >= 65 && a <= 90) ||
                    (a >= 97 && a <= 122))) return false;
                if (a >= 48 && a <= 57) digitos++;
                if (a >= 65 && a <= 90) mayusculas++;
                if (a >= 97 && a <= 122) minusculas++;
            }
            if (!((digitos > 0) && (mayusculas > 0) && (minusculas > 0))) return false;
            return true;
        }
        #endregion Metodos
    }
}

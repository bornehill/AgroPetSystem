using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Configuration;

namespace AgroPET.Datos.Comun
{
    public class EjecutaSP : IDisposable
    {
        #region Variables generales privadas de la clase

        private string sCadenaConexion;
        private MySqlConnection sqlCN;
        private MySqlCommand sqlCmd;

        #endregion

        #region Variables para las propiedades

        private string _nombreSP;

        #endregion

        #region Propiedades

        /// <summary>
        /// Obtiene o establece el nombre del store procedure que se desea ejecutar.
        /// </summary>
        public string NombreSP
        {
            get { return _nombreSP; }
            set { _nombreSP = value; }
        }

        /// <summary>
        /// Obtiene los parametros que se han asignado al store procedure.
        /// </summary>
        public MySqlParameterCollection Parametros
        {
            get
            {
                if (this.sqlCmd == null)
                    return null;
                else
                    return this.sqlCmd.Parameters;
            }
        }

        #endregion

        #region Métodos protegidos

        /// <summary>
        /// Método encargado de convertir los mensajes de excepción de la base de datos.
        /// </summary>
        /// <param name="exSQL">Excepción de SQL a ser convertido</param>
        /// <returns>Excepción convertida con un mensaje más descriptivo para el usuario.</returns>
        protected Exception ConvierteExcepcionSQL(MySqlException exSQL)
        {
            Exception exConvertido = new Exception(exSQL.Message);

            switch (exSQL.Number)
            {
                case 547:   //Constraint error
                    exConvertido = new Exception("No se puede alterar el registro, porque está siendo utilizado en otro módulo del sistema.");
                    break;
                default:
                    break;
            }

            return exConvertido;
        }

        #endregion

        #region Métodos públicos

        /// <summary>
        /// Elimina la información de los parámetros previamente asignada.
        /// </summary>
        public void LimpiarParametros()
        {
            if (this.sqlCmd != null)
            {
                this.sqlCmd.Parameters.Clear();
            }
        }

        /// <summary>
        /// Asigna un nuevo parametro para ser utilizado en la ejecución de los strore procedures
        /// </summary>
        /// <param name="sqlParametro">Parámetro a ser agregado</param>
        public void AgregarParametro(MySqlParameter sqlParametro)
        {
            if (this.sqlCN == null)
                this.sqlCN = new MySqlConnection(this.sCadenaConexion);

            if (this.sqlCmd == null)
                this.sqlCmd = new MySqlCommand(this.NombreSP, this.sqlCN);

            this.sqlCmd.Parameters.Add(sqlParametro);

        }

        /// <summary>
        /// Regresa la conexion a la bd
        /// </summary>
        /// <param name="sqlParametro">Parámetro a ser agregado</param>
        public MySqlConnection RegresaConn()
        {
            if (this.sqlCN == null)
                this.sqlCN = new MySqlConnection(this.sCadenaConexion);

            return sqlCN;

        }

        /// <summary>
        /// Ejecuta un store procedure definido por el párametro de la clase para obtener
        /// la información como un DataReader
        /// </summary>
        /// <returns>DataReader con el resultado del storeprocedure</returns>
        public MySqlDataReader ObtenerReader()
        {
            MySqlDataReader drResultado;

            if (this.sqlCN == null)
            {
                this.sqlCN = new MySqlConnection(this.sCadenaConexion);
            }
            if (this.sqlCmd == null)
            {
                this.sqlCmd = new MySqlCommand(this.NombreSP, this.sqlCN);
            }

            try
            {
                if (this.sqlCN.State == ConnectionState.Closed)
                    this.sqlCN.Open();

                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.CommandText = this.NombreSP;
                this.sqlCmd.CommandTimeout = 3600;

                drResultado = this.sqlCmd.ExecuteReader();

            }
            catch (MySqlException exSQL)
            {
                //Reenvia la excepción con un mensaje mas descriptivo para el usuario
                throw this.ConvierteExcepcionSQL(exSQL);
            }
            catch
            {
                throw;  //Reenvia cualquier error detectado.
            }

            return drResultado;
        }

        /// <summary>
        /// Ejecuta un store procedure definido por el párametro de la clase para obtener
        /// la información como un DataReader con una conexion espesifica
        /// </summary>
        /// <returns>DataReader con el resultado del storeprocedure</returns>
        public MySqlDataReader ObtenerReader(MySqlConnection sqlConn)
        {
            MySqlDataReader drResultado;

            if (this.sqlCN == null)
            {
                this.sqlCN = new MySqlConnection(this.sCadenaConexion);
            }

            if (this.sqlCmd == null)
            {
                this.sqlCmd = new MySqlCommand(this.NombreSP, sqlConn);
            }

            try
            {
                if (sqlConn.State == ConnectionState.Closed)
                    sqlConn.Open();

                this.sqlCmd.Connection = sqlConn;
                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.CommandText = this.NombreSP;

                drResultado = this.sqlCmd.ExecuteReader();

            }
            catch (MySqlException exSQL)
            {
                //Reenvia la excepción con un mensaje mas descriptivo para el usuario
                throw this.ConvierteExcepcionSQL(exSQL);
            }
            catch
            {
                throw;  //Reenvia cualquier error detectado.
            }

            return drResultado;
        }

        /// <summary>
        /// Se obtiene solamente la cadena de conexion para el proceso.
        /// </summary>
        /// <returns>DataReader con el resultado del storeprocedure</returns>
        public string ObtenerCadenaConexion()
        {
            //return System.Configuration.ConfigurationManager.ConnectionStrings["ConnMySQLPruebas"].ConnectionString.ToString();
            return System.Configuration.ConfigurationManager.ConnectionStrings["ConnMySQLAgropet"].ConnectionString.ToString();
        }

        /// <summary>
        /// Ejecuta un store procedure definido por el párametro de la clase para obtener
        /// la información como un DataTable
        /// </summary>
        /// <returns>DataTable con el resultado del StoreProcedure</returns>
        public DataTable ObtenerDataTable()
        {
            MySqlDataAdapter da;
            DataTable dt = new DataTable();

            if (this.sqlCN == null)
            {
                this.sqlCN = new MySqlConnection(this.sCadenaConexion);
            }
            if (this.sqlCmd == null)
            {
                this.sqlCmd = new MySqlCommand(this.NombreSP, this.sqlCN);
            }

            try
            {
                if (this.sqlCN.State == ConnectionState.Closed)
                    this.sqlCN.Open();

                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.CommandText = this.NombreSP;
                this.sqlCmd.CommandTimeout = 1800;

                da = new MySqlDataAdapter(sqlCmd);
                da.Fill(dt);

            }
            catch (MySqlException exSQL)
            {
                //Reenvia la excepción con un mensaje mas descriptivo para el usuario
                throw this.ConvierteExcepcionSQL(exSQL);
            }
            catch
            {
                throw;  //Reenvia cualquier error detectado.
            }
            finally
            {
                if (this.sqlCN.State == ConnectionState.Open)
                    this.sqlCN.Close();
            }
            return dt;
        }

        /// <summary>
        /// Ejecuta el store procedure definido por el parámetro de la clase y obtiene únicamente
        /// un registro.
        /// </summary>
        /// <returns>Un registro de respuesta del strore procedure</returns>
        public object ObtenerEscalar()
        {
            object oResultado;

            if (this.sqlCN == null)
            {
                this.sqlCN = new MySqlConnection(this.sCadenaConexion);
            }
            if (this.sqlCmd == null)
            {
                this.sqlCmd = new MySqlCommand(this.NombreSP, this.sqlCN);
            }

            try
            {
                if (this.sqlCN.State == ConnectionState.Closed)
                    this.sqlCN.Open();

                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.CommandText = this.NombreSP;

                oResultado = this.sqlCmd.ExecuteScalar();

            }
            catch
            {
                throw;  //Reenvia cualquier error detectado.
            }
            finally
            {
                if (this.sqlCN.State == ConnectionState.Open)
                    this.sqlCN.Close();
            }

            return oResultado;
        }

        /// <summary>
        /// Ejecuta el store procedure que no regresa información especifica
        /// </summary>
        /// <returns>Número de registros afectados</returns>
        public int EjecutaSinResultado()
        {
            int nRegistrosAfectados;

            if (this.sqlCN == null)
            {
                this.sqlCN = new MySqlConnection(this.sCadenaConexion);
            }
            if (this.sqlCmd == null)
            {
                this.sqlCmd = new MySqlCommand(this.NombreSP, this.sqlCN);
            }

            try
            {
                if (this.sqlCN.State == ConnectionState.Closed)
                    this.sqlCN.Open();

                this.sqlCmd.CommandType = CommandType.StoredProcedure;
                this.sqlCmd.CommandText = this.NombreSP;

                //this.sqlCmd.CommandTimeout = 1800;
                this.sqlCmd.CommandTimeout = 2700;
                nRegistrosAfectados = this.sqlCmd.ExecuteNonQuery();
            }
            catch (MySqlException exSQL)
            {
                //Reenvia la excepción con un mensaje mas descriptivo para el usuario
                throw this.ConvierteExcepcionSQL(exSQL);
            }
            catch
            {
                throw;  //Reenvia cualquier error detectado.
            }
            finally
            {
                if (this.sqlCN.State == ConnectionState.Open)
                    this.sqlCN.Close();
            }

            return nRegistrosAfectados;
        }

        /// <summary>
        /// Ejecuta el store procedure que no regresa información especifica
        /// </summary>
        /// <returns>Número de registros afectados</returns>
        public int EjecutaSinResultadoTemp()
        {
            int nRegistrosAfectados;

            if (this.sqlCN == null)
            {
                this.sqlCN = new MySqlConnection(this.sCadenaConexion);
            }
            if (this.sqlCmd == null)
            {
                this.sqlCmd = new MySqlCommand(this.NombreSP, this.sqlCN);
            }

            try
            {
                if (this.sqlCN.State == ConnectionState.Closed)
                    this.sqlCN.Open();

                this.sqlCmd.CommandType = CommandType.Text;
                this.sqlCmd.CommandText = this.NombreSP;

                //this.sqlCmd.CommandTimeout = 1800;
                this.sqlCmd.CommandTimeout = 2700;
                nRegistrosAfectados = this.sqlCmd.ExecuteNonQuery();
            }
            catch (MySqlException exSQL)
            {
                //Reenvia la excepción con un mensaje mas descriptivo para el usuario
                throw this.ConvierteExcepcionSQL(exSQL);
            }
            catch
            {
                throw;  //Reenvia cualquier error detectado.
            }
            finally
            {
                if (this.sqlCN.State == ConnectionState.Open)
                    this.sqlCN.Close();
            }

            return nRegistrosAfectados;
        }

        /// <summary>
        /// Ejecuta un query enviando como un parametro String para obtener
        /// la información como un DataTable
        /// </summary>
        /// <returns>DataTable con el resultado del query</returns>
        public DataTable ObtenerDataTable(String sInstruccion)
        {
            MySqlDataAdapter da;
            DataTable dt = new DataTable();

            if (this.sqlCN == null)
            {
                this.sqlCN = new MySqlConnection(this.sCadenaConexion);
            }
            if (this.sqlCmd == null)
            {
                this.sqlCmd = new MySqlCommand(this.NombreSP, this.sqlCN);
            }

            try
            {
                if (this.sqlCN.State == ConnectionState.Closed)
                    this.sqlCN.Open();

                this.sqlCmd.CommandType = CommandType.Text;
                this.sqlCmd.CommandText = this.NombreSP;

                da = new MySqlDataAdapter(sqlCmd);
                da.Fill(dt);

            }
            catch (MySqlException exSQL)
            {
                //Reenvia la excepción con un mensaje mas descriptivo para el usuario
                throw this.ConvierteExcepcionSQL(exSQL);
            }
            catch
            {
                throw;  //Reenvia cualquier error detectado.
            }
            finally
            {
                if (this.sqlCN.State == ConnectionState.Open)
                    this.sqlCN.Close();
            }
            return dt;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Libera de la memoria los objetos de conexión a la base de datos
        /// </summary>
        public void Dispose()
        {
            if (sqlCmd != null)
            {
                sqlCmd.Dispose();
                sqlCmd = null;
            }

            if (sqlCN != null)
            {
                if (sqlCN.State == ConnectionState.Open)
                    sqlCN.Close();

                sqlCN.Dispose();
                sqlCN = null;
            }

        }

        #endregion

        #region Constructores

        /// <summary>
        /// Inicializa con valores vacios.
        /// </summary>
        public EjecutaSP()
        {
            this.NombreSP = string.Empty;
            this.sCadenaConexion = ObtenerCadenaConexion();
        }

        /// <summary>
        /// Inicializa la clase con un nombre de store procedure por default.
        /// </summary>
        /// <param name="nombreSP">Nombre del store procedure</param>
        public EjecutaSP(string nombreSP)
        {
            this.NombreSP = nombreSP;
            this.sCadenaConexion = ObtenerCadenaConexion();
        }

        #endregion

    }
}

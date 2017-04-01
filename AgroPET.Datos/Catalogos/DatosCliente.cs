using AgroPET.Entidades.CatABCs;
using AgroPET.Entidades.Consultas;
using AgroPET.Datos.Comun;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Datos.Catalogos
{
    public class DatosCliente : DatosBase
    {
        public List<EntClientes> ObtenerClientes(string Nombre, string estatus)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspClienteObtener";
            accesoDatos.parametros.Agrega("@p_nombre", Nombre, true);
            accesoDatos.parametros.Agrega("@p_estatus", estatus, true);
            return accesoDatos.ConsultaDataList<EntClientes>();
        }

        public bool InsertaCliente(EntClientes cliente)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspClienteInsertar";
                accesoDatos.parametros.Agrega("@p_nombre", cliente.nombre, true);
                accesoDatos.parametros.Agrega("@p_contacto", cliente.contacto1, true);
                accesoDatos.parametros.Agrega("@p_estatus", cliente.estatus, true);
                accesoDatos.parametros.Agrega("@p_causasusp", cliente.causa_susp, true);
                accesoDatos.parametros.Agrega("@p_limitecrediro", cliente.limite_credito, true);
                accesoDatos.parametros.Agrega("@p_idmoneda", cliente.moneda_id, true);
                accesoDatos.parametros.Agrega("@p_idcondicionpago", cliente.cond_pago_id, true);
                accesoDatos.parametros.Agrega("@p_idtipocliente", cliente.tipo_cliente_id, true);
                accesoDatos.parametros.Agrega("@p_usuariocreador", cliente.usuario_creador, true);
                int afectados = accesoDatos.EjecutaNQuery();

                if (afectados >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

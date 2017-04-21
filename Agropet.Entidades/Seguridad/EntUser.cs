using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgroPET.Entidades.Base;

namespace AgroPET.Entidades.Seguridad
{
  [Serializable]
  [Tabla("tbusers")]
  public class EntUser
  {
    [Campo("userId", EsLlavePrimaria = true, EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public int UserId
    {
      get;
      set;
    }

    [Campo("user_name", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public string User_Name
    {
      get;
      set;
    }

    [Campo("pass", EsLlavePrimaria = false, EsParametroSP = true, EsCampoRetornoConsulta = true)]
    public string Pass
    {
      get;
      set;
    }

    [Campo("cliente_id", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public int Cliente_Id
    {
      get;
      set;
    }

    [Campo("creation_date", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public DateTime Creation_date
    {
      get;
      set;
    }

    [Campo("expiration_date", EsLlavePrimaria = false, EsParametroSP = false, EsCampoRetornoConsulta = true)]
    public DateTime Expiration_date
    {
      get;
      set;
    }
  }
}

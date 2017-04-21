using System;

namespace AgroPET.Entidades.Seguridad
{
    [Serializable]
  public class EntUser
  {
    public int UserId
    {
      get;
      set;
    }

   
    public string User_Name
    {
      get;
      set;
    }

   
    public string Pass
    {
      get;
      set;
    }

    public int Cliente_Id
    {
      get;
      set;
    }

    public DateTime Creation_date
    {
      get;
      set;
    }

    public DateTime Expiration_date
    {
      get;
      set;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgroPET.Entidades.Base
{
  public class PageView
  {
    public int RecordsByPage { get; set; }

    public int TotalRecords { get; set; }

    public int CurrentPage { get; set; }
  }
}

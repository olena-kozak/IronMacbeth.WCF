using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronMacbeth.BFF.Contract
{
  public  class SpecifiedOrderFields
    {
        public string Status { get; set; }

        public DateTime ReceiveDate { get; set; }

        public DateTime DateOfReturning { get; set; }
    }
}

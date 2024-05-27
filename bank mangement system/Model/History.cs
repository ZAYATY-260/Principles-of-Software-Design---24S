using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_mangement_system.Model
{
    class History
    {
        public int from {get; set;}
        public int to {get; set;}
        public decimal Amount { get; set; }
        public History()
        {
        }
    }
}

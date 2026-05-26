
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Repository
{
    public class GlobalModel
    {
    }
    public class PairModel
    {
        public object Key { get; set; }
        public object Value { get; set; }
        public object Selected { get; set; }

    }
    public class IntStringPairModel
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}

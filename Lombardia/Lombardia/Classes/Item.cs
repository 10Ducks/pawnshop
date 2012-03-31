using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lombardia
{
    class Item
    {
        public string itemType { set; get; }
        public string itemSubType { set; get; }
        public string descr { set; get; }
        public string measures { set; get; }
        public string price { set; get; }

        public Item()
        {
        }
    }

    class Measurement
    {
        public string value;
        public string unit;

        public Measurement(string v, string u)
        {
            value = v;
            unit = u;
        }
    }
}

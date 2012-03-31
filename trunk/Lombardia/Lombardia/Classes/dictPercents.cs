using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lombardia.Classes
{
    class dictPercents
    {
        public string type { set; get; }
        public string from_amount { set; get; }
        public string to_amount { set; get; }
        public string monthly { set; get; }
        public string yearly { set; get; }
        public string ratio { set; get; }

        public dictPercents()
        {
        }

        public dictPercents(string type, string from_amount, string to_amount, string monthly,string yearly,string ratio)
        {
            this.type = type;
            this.from_amount = from_amount;
            this.to_amount = to_amount;
            this.monthly = monthly;
            this.yearly = yearly;
            this.ratio = ratio;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lombardia
{   
    class Customer
    {
        public string secondName { set; get; }
        public string firstName { set; get; }
        public string middleName { set; get; }

        public string country { set; get; }
        public string passportData { set; get; }

        public string address { set; get; }
        public string phone { set; get; }
        public string details { set; get; }


        public Customer(string secondName, string firstName, string middleName, string country, string passport, string address, string phone, string details)
        {
            this.secondName = secondName;
            this.firstName = firstName;
            this.middleName = middleName;

            this.country = country;
            this.passportData = passport;

            this.address = address;
            this.phone = phone;
            this.details = details;

        }

    }
}

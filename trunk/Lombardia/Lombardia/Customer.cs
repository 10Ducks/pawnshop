using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lombardia
{   
    class Customer
    { 
        public string secondName;
        public string firstName;
        public string middleName;

        public string passportData1;
        public string passportData2;
        public string passportData3;
        public string passportData4;
        public string passportData5;

        public string address;
        public string phone;
        public string details;


        public Customer(string secondName, string firstName, string middleName, string passport, string address, string phone, string details)
        {
            this.secondName = secondName;
            this.firstName = firstName;
            this.middleName = middleName;

            this.passportData1 = (passport.Split(';'))[0];
            this.passportData2 = (passport.Split(';'))[1];
            this.passportData3 = (passport.Split(';'))[2];
            this.passportData4 = (passport.Split(';'))[3];
            this.passportData5 = (passport.Split(';'))[4];

            this.address = address;
            this.phone = phone;
            this.details = details;

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Security;
using System.Collections;


namespace Lombardia.Classes
{
    class Database
    {
        SqlConnection dbConnection;

        public Database()
        {
            string connectionString = global::Lombardia.Properties.Settings.Default.ConnString;
            dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
        }

        public bool login(String user, String password, out String full_name, out String role)
        {
            full_name = String.Empty;
            role = String.Empty;

            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from users where username = '"
                                                + user + "' and password = '"
                                                + password + "'",
                                                         dbConnection);
                myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    full_name = myReader["full_name"].ToString();
                    role = myReader["role"].ToString();
                }
                if (String.IsNullOrEmpty(full_name) || String.IsNullOrEmpty(role))
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool insertCustomer(Customer customer)
        {
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("execute dbo.add_customer N'"
                            + customer.secondName + "', N'"
                            + customer.firstName + "', N'"
                            + customer.middleName + "', N'"
                            + customer.country + "', N'"
                            + customer.passportData + "', N'"
                            + customer.address + "', N'"
                            + customer.phone + "', N'"
                            + customer.details + "'",
                            dbConnection);
                string resp = String.Empty;
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    resp = myReader[0].ToString();
                }
                if (String.IsNullOrEmpty(resp))
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                return false;
            }
        }

        public ArrayList searchCustomer(string sName, string fName, string mName, string passport, string phone, string address)
        {
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("execute dbo.search_customer N'"
                                + sName.Trim() + "', N'"
                                + fName.Trim() + "', N'"
                                + mName.Trim() + "', N'"
                                + passport.Trim() + "', N'"
                                + phone.Trim() + "', N'"
                                + address.Trim() + "';", dbConnection);

                myReader = myCommand.ExecuteReader();
                ArrayList customers = new ArrayList();

                while (myReader.Read())
                {
                    customers.Add(new Customer(myReader[1].ToString(), myReader[2].ToString(), myReader[3].ToString(), myReader[4].ToString(), myReader[5].ToString(), myReader[6].ToString(), myReader[7].ToString(), myReader[8].ToString()));
                }

                return customers;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                return null;
            }
        }

        public ArrayList getPercents()
        {
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("execute dbo.get_percents"
                                , dbConnection);

                myReader = myCommand.ExecuteReader();
                ArrayList data = new ArrayList();

                while (myReader.Read())
                {
                    data.Add(new dictPercents(myReader[1].ToString(), myReader[2].ToString(), myReader[3].ToString(), myReader[4].ToString(), myReader[5].ToString(), myReader[6].ToString()));
                }

                return data;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}

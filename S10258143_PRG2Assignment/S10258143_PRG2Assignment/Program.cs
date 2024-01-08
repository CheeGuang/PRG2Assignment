using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace S10258143_PRG2Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialising customers
            Dictionary<string, Customer> customers = new Dictionary<string, Customer>();

            // Getting Data from customers.csv
            using (StreamReader sr = new StreamReader("customers.csv"))
            {
                // Remiving Header
                sr.ReadLine();

                // Iterating though each row
                for (int i = 1; i < File.ReadAllLines("customers.csv").Count(); i++)
                {                    
                    string[] lineDetail = sr.ReadLine().Trim().Split(',');

                    // Adding customer record to customers
                    customers.Add(lineDetail[0], new Customer(lineDetail[0], Convert.ToInt32(lineDetail[1]), Convert.ToDateTime(lineDetail[2])));
                }
            }
            foreach (Customer customer in customers.Values)
            {
                Console.WriteLine(customer.ToString());
            }

        }
    }
}

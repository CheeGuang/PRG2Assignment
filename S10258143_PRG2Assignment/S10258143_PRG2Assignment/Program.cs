using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace S10258143_PRG2Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialising Customers ===============================================================================================================
            Dictionary<string, Customer> customers = new Dictionary<string, Customer>();
            InitialiseCustomersData(customers);

            // Q1 List all customers ================================================================================================================
            ListAllCustomers(customers);

            // Q3 Register a new customer ===========================================================================================================
            //RegisterCustomer();

            CreateOrder(customers);
        }

        // Initialising Customers ====================================================================================================================
        static void InitialiseCustomersData(Dictionary<string, Customer> customers)
        {
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
                    customers.Add(lineDetail[1], new Customer(lineDetail[0], Convert.ToInt32(lineDetail[1]), Convert.ToDateTime(lineDetail[2])));
                }
            }
        }

        // Q1 List all customers ====================================================================================================================
        static void ListAllCustomers(Dictionary<string, Customer> customers)
        {
            foreach (Customer customer in customers.Values)
            {
                Console.WriteLine(customer.ToString());
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        // Q3 Register a new customer ===============================================================================================================
        static Customer RegisterCustomer()
        {
            Console.Write("Enter new customer Name: ");

            string name = Console.ReadLine();
            Console.WriteLine();

            // Initilising memberID and dob so to allow new customer object to be instanciated.
            int memberID;
            DateTime dob;

            // Ensure Member ID given by User is Unique
            while (true)
            {
                try
                {
                    Console.Write("Enter new customer Member ID (Must be an Integer): ");
                    memberID = Convert.ToInt32(Console.ReadLine());
                    using (StreamReader sr = new StreamReader("customers.csv"))
                    {
                        sr.ReadLine();
                        for (int i = 1; i < File.ReadAllLines("customers.csv").Count(); i++)
                        {
                            string[] lineDetail = sr.ReadLine().Trim().Split(',');
                            if (memberID == Convert.ToInt32(lineDetail[1]))
                            {
                                throw new ArgumentException();
                            }
                        }
                    }
                    Console.WriteLine();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Member ID has to be an Integer");
                    Console.WriteLine();
                }
                catch (ArgumentException)
                {

                    Console.WriteLine("Member ID must be Unique");
                    Console.WriteLine();
                }

            }

            // Ensure DOB is in correct format
            while (true)
            {
                try
                {
                    Console.Write("Enter new customer Date of Birth (Format -> dd/MM/yyyy): ");
                    string dobBeforeCheck = Console.ReadLine();

                    // Define the regex pattern for the "dd/MM/yyyy" format
                    string pattern = @"^(0[1-9]|[1-2][0-9]|3[0-1])/(0[1-9]|1[0-2])/\d{4}$";

                    // Use Regex.IsMatch to check if the input matches the pattern
                    if (!Regex.IsMatch(dobBeforeCheck, pattern))
                    {
                        throw new ArgumentException();
                    }

                    dob = Convert.ToDateTime(dobBeforeCheck);
                    Console.WriteLine();
                    break;
                }
                catch (ArgumentException)
                {

                    Console.WriteLine("Date of Birth must be in the following Format: dd/MM/yyyy"); ;
                }

            }

            Customer customer = new Customer(name, memberID, dob);
            PointCard pointCard = new PointCard();
            customer.Rewards = pointCard;

            // Appending Newly Registered to customers.csv
            using (StreamWriter sw = new StreamWriter("customers.csv", true))
            {
                sw.WriteLine(customer.Name + ',' + customer.MemberId + ',' + customer.Dob.ToString("dd/MM/yyyy"));
            }

            Console.WriteLine("Successfully Registered New Customer: " + customer.Name);
            Console.WriteLine();
            Console.WriteLine();
            return customer;
        }

        // Q4 Create a customer's order =============================================================================================================
        static void CreateOrder(Dictionary<string, Customer> customers)
        {
            ListAllCustomers(customers);

            // Initilising memberID, customer, option, scoops, flavours, toppings and iceCream so to allow new customer object to be instanciated.
            int memberID;
            Customer customer;
            

            // Enrsure MemberID is Valid and Exists
            while (true)
            {
                try
                {
                    Console.Write("Enter Customer's MemberID: ");
                    memberID = Convert.ToInt32(Console.ReadLine());
                    customer = customers[Convert.ToString(memberID)];

                    Console.WriteLine();
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Member ID has to be an Integer");
                    Console.WriteLine();
                }
                catch (KeyNotFoundException)
                {

                    Console.WriteLine("Please enter a Member ID that exists.");
                    Console.WriteLine();
                }
            }

            // Creating Order
            Order newOrder = new Order();

            while(true)
            {
                IceCream iceCreamOrdered = OrderIceCream();
                newOrder.IceCreamList.Add(iceCreamOrdered);

                string answer;
                while (true)
                {
                    try
                    {
                        Console.Write("Would like to add another ice cream to the order? (Y, N): ");
                        answer = Console.ReadLine();

                        if (answer.ToLower() != "y" && answer.ToLower() != "n")
                        {
                            throw new ArgumentException();
                        }
                        break;
                    }
                    catch (ArgumentException)
                    {

                        Console.WriteLine("Enter either 'Y' or 'N'."); ;
                    }
                }
                
                if (answer.ToLower() == "n")
                {
                    break;
                }
            }
            customer.CurrentOrder = newOrder;

        }

        static IceCream OrderIceCream()
        {
            string option;
            int scoops;
            List<Flavour> flavours = new List<Flavour>();
            List<Topping> toppings = new List<Topping>();
            IceCream iceCream;

            while (true)
            {
                try
                {
                    Console.Write("Enter Ice-Cream Option (Cup, Cone or Waffle): ");
                    option = Console.ReadLine();

                    if (option.ToLower() != "cup" && option.ToLower() != "cone" && option.ToLower() != "waffle")
                    {
                        throw new ArgumentException();
                    }

                    Console.WriteLine();
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Choose between Cup, Cone or Waffle.");
                    Console.WriteLine();
                }
            }
            while (true)
            {
                try
                {
                    Console.Write("Enter Number of Scoops (1-3): ");
                    scoops = Convert.ToInt32(Console.ReadLine());

                    if (scoops < 1 || scoops > 3)
                    {
                        throw new ArgumentException();
                    }

                    Console.WriteLine();
                    break;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Number of Scoops must be between 1 and 3.");
                    Console.WriteLine();
                }
            }

            List<string> regularFlavours = new List<string>() { "vanilla", "chocolate", "strawberry" };
            List<string> premiumFlavours = new List<string>() { "durian", "ube", "sea salt" };

            // Displaying Regular Flavours
            Console.WriteLine("Available Flavours:");
            Console.Write("Regular Flavours -> ");
            foreach (string flavour in regularFlavours)
            {
                Console.Write(flavour);
            }
            Console.WriteLine();

            // Displaying Premium Flavours
            Console.Write("Regular Flavours -> ");
            foreach (string flavour in premiumFlavours)
            {
                Console.Write(flavour);
            }
            Console.WriteLine();

            Dictionary<string, int> selectedFlavours = new Dictionary<string, int>();
            // Ensure Selected Flavours are valid
            for (int i = 0; i < scoops; i++)
            {
                while (true)
                {
                    try
                    {
                        Console.Write("Enter the Flavour you want: ");
                        string selectedFlavour = Console.ReadLine();

                        if (!regularFlavours.Contains(selectedFlavour.ToLower()) && !premiumFlavours.Contains(selectedFlavour.ToLower()))
                        {
                            throw new ArgumentException();
                        }

                        if (selectedFlavours.ContainsKey(selectedFlavour))
                        {
                            selectedFlavours[selectedFlavour]++;
                        }
                        else
                        {
                            selectedFlavours[selectedFlavour] = 1;
                        }
                        break;
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Flavour unavailable.");
                        Console.WriteLine();
                    }

                }
            }

            // Adding Flavours Selected to flavours
            foreach (KeyValuePair<string, int> flavour in selectedFlavours)
            {
                bool isPremium = premiumFlavours.Contains(flavour.Key);
                flavours.Add(new Flavour(flavour.Key, isPremium, flavour.Value));
            }


            Console.WriteLine();


            // Ensure Toppings are valid
            List<string> toppingsAvailable = new List<string>() { "sprinkes", "mochi", "sago", "oreos" };

            // Displaying available Toppings
            Console.WriteLine("Available Toppings:");
            foreach (string topping in toppingsAvailable)
            {
                Console.WriteLine(topping);
            }
            Console.WriteLine();

            for (int i = 0; i < 4; i++)
            {
                string selectedTopping;
                while (true)
                {
                    try
                    {
                        Console.Write("Enter the Toppings you want (Enter 'X' to Exit): ");
                        selectedTopping = Console.ReadLine();

                        // If selectedTopping == 'x', Exit.
                        if (selectedTopping.ToLower() == "x")
                        {
                            break;
                        }

                        if (!toppingsAvailable.Contains(selectedTopping))
                        {
                            throw new ArgumentException();
                        }

                        // Adding Selected Topping to toppings
                        toppings.Add(new Topping(selectedTopping));
                        break;
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Topping unavailable.");
                        Console.WriteLine();
                    }

                }
                if (selectedTopping.ToLower() == "x")
                {
                    break;
                }
            }

            if (option == "cup")
            {
                iceCream = new Cup(option, scoops, flavours, toppings);
            }
            else if (option == "cone")
            {
                string isDipped;
                while (true)
                {
                    try
                    {
                        Console.Write("Do you want your cone Dipped in chocolate? (y, n): ");
                        isDipped = Console.ReadLine();

                        if (isDipped != "y" || isDipped != "n")
                        {
                            throw (new ArgumentException());
                        }
                        break;
                    }
                    catch (ArgumentException)
                    {

                        Console.WriteLine("Enter either 'y' or 'n'."); ;
                    }
                }


                if (isDipped == "y")
                {
                    iceCream = new Cone(option, scoops, flavours, toppings, true);
                }
                else
                {
                    iceCream = new Cone(option, scoops, flavours, toppings, false);
                }
            }
            else
            {
                string selectedWaffleFlavour;
                List<string> waffleFlavourAvailable = new List<string>() { "red velvet", "charcoal", "pandan" };

                // Displaying available waffle flavours
                Console.WriteLine("Available Waffle Flavours:");
                foreach (string waffleFlavour in waffleFlavourAvailable)
                {
                    Console.WriteLine(waffleFlavour);
                }
                Console.WriteLine();
                while (true)
                {
                    try
                    {
                        Console.Write("Enter flavour of Waffle: ");
                        selectedWaffleFlavour = Console.ReadLine();

                        if (!waffleFlavourAvailable.Contains(selectedWaffleFlavour.ToLower()))
                        {
                            throw (new ArgumentException());
                        }

                        break;
                    }
                    catch (ArgumentException)
                    {

                        Console.WriteLine("Enter a valid Waffle Flavour."); ;
                    }
                }


                iceCream = new Waffle(option, scoops, flavours, toppings, selectedWaffleFlavour);
            }
            return iceCream;
        }
    }
}

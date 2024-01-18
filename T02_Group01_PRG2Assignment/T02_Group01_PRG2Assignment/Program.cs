//==========================================================
// Student Number : S10258143
// Student Name : Lee Guang Le, Jeffrey
// Partner Name : Zou Ruining Raeanne
//==========================================================

// Jeffrey will do:
// Q1, 3, 4 -> Basic Feature
// (a) -> Advanced Feature

// Raeanne will do:
// Q2, 5, 6 -> Basic Feature
// (b) -> Advanced Feature

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;

namespace T02_Group01_PRG2Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // =============================================== Basic Features ==================================================

            // Initialising Customers ==================================================
            Dictionary<string, Customer> customers = new Dictionary<string, Customer>();
            InitialiseCustomersData(customers);

            // Initialising Flavours ===================================================
            Dictionary<string,float> flavourMenuDict = new Dictionary<string, float>();
            InitialiseFlavourMenu(flavourMenuDict);

            // Initialising Topping ====================================================
            Dictionary<string, float> toppingMenuDict = new Dictionary<string, float>();
            InitialiseToppingMenu(toppingMenuDict);

            // Initialising Options ====================================================
            const int optScoopIdx = 0;
            const int optPrice = 1;

            List<float[]> cupPriceMenu = new List<float[]>();
            List<float[]> conePriceMenu = new List<float[]>();
            const int coneMenuDippedIdx = 2;

            List<string[]> wafflePriceMenu = new List<string[]>();
            const int waffleMenuFlavorIdx = 2;

            InitialiseOptionMenu(cupPriceMenu, conePriceMenu, wafflePriceMenu);

            Console.WriteLine("waffle Price: ");
            foreach (string[] wafflePrice in wafflePriceMenu)
            {
                Console.WriteLine(wafflePrice[0] + " " + wafflePrice[1] + " " + wafflePrice[2]);
            }

            // Initialising Orders =====================================================
            List<Order> orderList = new List<Order>();
            InitialiseOrdersData(orderList, customers, flavourMenuDict); //------------ COMMENTED OUT TO ALLOW TESTING (Feel free to uncomment) ------------ 


            // Q1 List all customers ===================================================
            ListAllCustomers(customers);

            // Q2 List all orders ======================================================
            Queue<Customer> goldQueue = new Queue<Customer>();
            Queue<Customer> ordinaryQueue = new Queue<Customer>();
            // ListAllGoldRegOrders(orders);     ------------ COMMENTED OUT TO ALLOW TESTING (Feel free to uncomment) ------------ 

            // Q3 Register a new customer ==============================================
            RegisterCustomer();

            // Q4 Create a customer's order ============================================
            CreateOrder(customers, goldQueue, ordinaryQueue);


            // ============================================== Advanced Features =================================================
            // (a) Process an order and checkout ========================================
            //ProcessOrderAndCheckout(goldQueue, ordinaryQueue);

            // (c) Convert from SGD to specified Currency ===============================
            //ConvertCurrency();                 ------------ COMMENTED OUT TO ALLOW TESTING (Do not leave it uncommented) -------
        }

        // Initialising Customers =======================================================
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

        // Initialising Orders =========================================================
        static void InitialiseOrdersData(List<Order> orderList, Dictionary<string, Customer> customers, Dictionary<string, float> flavourMenuDict)
        {
            // Excel orders.csv data structure
            const int orderId = 0;
            const int memberId = 1;
            const int timeRec = 2;
            const int timeFul = 3;
            const int option = 4;
            const int scoops = 5;
            const int dipped = 6;
            const int waffleFlavour = 7;
            const int flavour1 = 8;
            const int flavour2 = 9;
            const int flavour3 = 10;
            const int topping1 = 11;
            const int topping2 = 12;
            const int topping3 = 13;
            const int topping4 = 14;

            // Getting Data from customers.csv
            using (StreamReader sr = new StreamReader("orders.csv")) //Fill in with data later
            {
                // Removing Header
                string header = sr.ReadLine();
                string line = sr.ReadLine();

                // Iterating though each row
                while (line != null)
                {
                    string[] lineDetail = line.Split(',');

                    // Create Order ------------------------------------------------------------------------------------------------------------------------
                    Order newOrder = new Order();
                    newOrder.Id = Convert.ToInt32(lineDetail[orderId]);
                    newOrder.TimeReceived = Convert.ToDateTime(lineDetail[timeRec]);

                    // Determine if time fulfilled has a value ---------------------------------------------------------------------------------------------
                    if (lineDetail[timeFul] != null)
                    {
                        newOrder.TimeFulfilled = Convert.ToDateTime(lineDetail[timeFul]);
                    }

                    // Determine flavour -------------------------------------------------------------------------------------------------------------------
                    List<Flavour> flavours = new List<Flavour>();
                    string[] selectedFlavours = new string[] { lineDetail[flavour1], lineDetail[flavour2], lineDetail[flavour3] };

                    foreach (string flavour in selectedFlavours)
                    {
                        // Check if there is anymore ice cream flavours in order to add
                        if (flavour == "")
                        {
                            break;
                        }
                        else
                        {
                            // Check for repeated flavours in an order
                            Flavour foundFlavour = flavours.FirstOrDefault(x => x.Type == flavour);
                            if (foundFlavour != null)
                            {
                                // Add quantity if repeated flavour
                                foundFlavour.Quantity += 1;
                            }
                            else
                            {
                                // Check if flavour is premium 
                                if (flavourMenuDict[flavour.ToLower()] == 2)
                                {
                                    flavours.Add(new Flavour(flavour, true, 1));
                                }
                                else
                                {
                                    flavours.Add(new Flavour(flavour, false, 1));
                                };
                                // End of checking if flavour is premium 
                            };
                            // End of checking for repeated flavours in an order
                        };
                        // End of checking if there is anymore ice cream flavours in order to add
                    }

                    // Determine Number of Toppings --------------------------------------------------------------------------------------------------------
                    int numToppings = 0;
                    List<Topping> toppingList = new List<Topping>();
                    string[] selectedToppings = new string[] { lineDetail[topping1], lineDetail[topping2], lineDetail[topping3], lineDetail[topping4] };

                    foreach (string tmpTopping in selectedToppings)
                    {
                        if (tmpTopping == "")
                        {
                            break;
                        }
                        else
                        {
                            toppingList.Add(new Topping(tmpTopping));
                            numToppings++;
                        }
                    }

                    // Determine Waffle, Cup or Cone and create the IceCream Object, then add to Order object ----------------------------------------------
                    if (lineDetail[option] == "Waffle")
                    {
                        Waffle orderItem = new Waffle(Convert.ToInt32(lineDetail[scoops]), flavours, toppingList, lineDetail[waffleFlavour]);
                        newOrder.AddIceCream(orderItem);
                    } 
                    else if (lineDetail[option] == "Cone")
                    {
                        Cone orderItem = new Cone(Convert.ToInt32(lineDetail[scoops]), flavours, toppingList, Convert.ToBoolean(lineDetail[dipped].ToLower()));
                        newOrder.AddIceCream(orderItem);
                    }
                    else
                    {
                        Cup orderItem = new Cup(Convert.ToInt32(lineDetail[scoops]), flavours, toppingList);
                        newOrder.AddIceCream(orderItem);
                    }

                    // Add to the Order List
                    orderList.Add(newOrder);

                    // Append to customer's order history
                    string memID = lineDetail[memberId];
                    customers[memID].OrderHistory.Add(newOrder);

                    line = sr.ReadLine();
                }
            }
        }

        /// <summary>
        /// Reading the Flavour csv and instantiating flavour (Dict contains flavourName : Cost)
        /// </summary>
        static void InitialiseFlavourMenu(Dictionary<string, float> flavourMenuDict)
        {
            using (StreamReader sr = new StreamReader("flavours.csv"))
            {
                string header = sr.ReadLine();
                string line = sr.ReadLine();

                while (line != null)
                {
                    string[] lineDetail = line.Split(',');
                    flavourMenuDict[lineDetail[0].ToLower()] = float.Parse(lineDetail[1]);

                    line = sr.ReadLine();
                }
            }
        }

        /// <summary>
        /// Reading the Topping csv and instantiating flavour (Dict contains flavourName : Cost)
        /// </summary>
        static void InitialiseToppingMenu(Dictionary<string, float> toppingMenuDict)
        {
            using (StreamReader sr = new StreamReader("toppings.csv"))
            {
                string header = sr.ReadLine();
                string line = sr.ReadLine();

                while (line != null)
                {
                    string[] lineDetail = line.Split(',');
                    toppingMenuDict[lineDetail[0].ToLower()] = float.Parse(lineDetail[1]);

                    line = sr.ReadLine();
                }
            }
        }

        /// <summary>
        /// Reading the Options csv and instantiating flavour (Dict contains flavourName : Cost)
        /// </summary>
        static void InitialiseOptionMenu(List<float[]>cupPriceMenu, List<float[]> conePriceMenu, List<String[]> wafflePriceMenu)
        {
            int optionIdx = 0;
            int scoopsIdx = 1;
            int dippedIdx = 2;
            int waffleFlavourIdx = 3;
            int costIdx = 4;

            using (StreamReader sr = new StreamReader("options.csv"))
            {
                string header = sr.ReadLine();
                string line = sr.ReadLine();

                while (line != null)
                {
                    string[] lineDetail = line.Split(',');

                    if (lineDetail[optionIdx].ToLower() == "cup")
                    {
                        float[] cScoopsPriceArray = new float[] { Convert.ToInt32(lineDetail[scoopsIdx]), float.Parse(lineDetail[costIdx]) };
                        cupPriceMenu.Add(cScoopsPriceArray);
                    } else if (lineDetail[optionIdx].ToLower() == "cone")
                    {
                        bool isDipped = Convert.ToBoolean(lineDetail[dippedIdx]);

                        // OR save all info as string

                        float[] coScoopsPriceArray = new float[] { Convert.ToInt32(lineDetail[scoopsIdx]), float.Parse(lineDetail[costIdx]), Convert.ToInt32(isDipped) };
                        conePriceMenu.Add(coScoopsPriceArray);
                    }else if (lineDetail[optionIdx].ToLower() == "waffle")
                    {
                        //flavours can only be stored as string
                        String[] wScoopsPriceArray = new String[] { lineDetail[scoopsIdx], lineDetail[costIdx], lineDetail[waffleFlavourIdx] };
                        wafflePriceMenu.Add(wScoopsPriceArray);
                    }

                    line = sr.ReadLine();
                }
            }
        }

        // ================================================ Basic Features ======================================================

        // Q1 List all customers =======================================================
        static void ListAllCustomers(Dictionary<string, Customer> customers)
        {
            foreach (Customer customer in customers.Values)
            {
                Console.WriteLine(customer.ToString());
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        // Q2 List all orders ==========================================================
        /// <summary>
        /// Missing: Need to check for Gold or Regular then print
        /// </summary>
        /// <param name="orders"></param>
        static void ListAllGoldRegOrders(List<Order> orders)
        {
            foreach (Order order in orders)
            {
                Console.WriteLine(order.ToString());
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        // Q3 Register a new customer ==================================================
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

        // Q4a Create a customer's order ===============================================
        static void CreateOrder(Dictionary<string, Customer> customers, Queue<Customer> goldQueue, Queue<Customer> ordinaryQueue)
        {

            ListAllCustomers(customers);

            // Initilising memberID, customer, option, scoops, flavours, toppingList and iceCream so to allow new customer object to be instantiated.
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

            customer.MakeOrder();

            try
            {
                if (customer.Rewards.Tier.ToLower() == "ordinary" || customer.Rewards.Tier.ToLower() == "silver")
                {
                    ordinaryQueue.Enqueue(customer);
                }
                else if (customer.Rewards.Tier.ToLower() == "gold")
                {
                    goldQueue.Enqueue(customer);
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Order made was unsuccessfully");
            }

            Console.WriteLine("Order has been made successfully");

        }


        // Q5 Display Selected customer's order ========================================
        static void DisplayCustomerOrder(Dictionary<string, Customer> customers)
        {
            // List all customers
            ListAllCustomers(customers);

            //Initialise memberId
            int memberId = 0;

            //User Prompt
            while (true)
            {
                try
                {
                    Console.Write("Please select a customer by their ID: ");
                    memberId = int.Parse(Console.ReadLine());

                    // Check if memberId is in the list
                    // NOTE: Check with Jeffrey if customers dict key can be int instead of string
                    if (!customers.ContainsKey(Convert.ToString(memberId)))
                    {
                        // If memberId do not exist in customers dict
                        throw new ArgumentException();

                    }
                    else
                    {
                        // End of checking if memberId exist and displaying order details
                        // Break out of while loop
                        break;
                    }

                }
                catch
                {
                    Console.WriteLine("An invalid response has been entered. Please try again.");
                };

                // Get the selected customer object
                Customer selectedCustomer = customers[Convert.ToString(memberId)];

                // Retrieve all (current + past) order details of the selected customer
                List<Order> selectedCustOrderHist = selectedCustomer.OrderHistory;
                Order selectedCustCurrentOrder = selectedCustomer.CurrentOrder;

                Console.WriteLine("Current Order \n ===============================");
                if (selectedCustCurrentOrder.TimeFulfilled != null)
                {
                    Console.WriteLine(selectedCustCurrentOrder.ToString() + $"\t Time fulfilled: {selectedCustCurrentOrder.TimeFulfilled}");
                }
                else
                {
                    Console.WriteLine(selectedCustCurrentOrder.ToString());
                }

                // Print all Ice Cream associated with the current order
                foreach (IceCream iceCream in selectedCustCurrentOrder.IceCreamList)
                {
                    Console.WriteLine(iceCream.ToString());
                }

                // Check if there are any past orders
                if (selectedCustOrderHist.Count > 0)
                {
                    Console.WriteLine("\nPast Orders \n ===============================");
                    foreach (Order order in selectedCustOrderHist)
                    {
                        if (order.TimeFulfilled != null)
                        {
                            Console.WriteLine(order.ToString() + $"\t Time Fulfilled: {order.TimeFulfilled}");
                        }
                        else
                        {
                            Console.WriteLine(order.ToString());
                        }

                        // Print all Ice Cream associated with the order
                        foreach (IceCream iceCream in order.IceCreamList)
                        {
                            Console.WriteLine(iceCream.ToString());
                        }
                    }
                    // End of printing all past orders
                }
                // End of checking for past orders
            }
        }

        // Q6 Modify order details =====================================================
        static void ModifyOrderDetails(Dictionary<string, Customer> customers)
        {
            // List all customers
            ListAllCustomers(customers);

            // Initialise memberId
            int memberId = 0;

            //User Prompt
            while (true)
            {
                try
                {
                    Console.Write("Please select a customer by their ID: ");
                    memberId = int.Parse(Console.ReadLine());

                    // Check if memberId is in the list
                    // NOTE: Check with Jeffrey if customers dict key can be int instead of string
                    if (!customers.ContainsKey(Convert.ToString(memberId)))
                    {
                        // If memberId do not exist in customers dict
                        throw new ArgumentException();

                    }
                    else
                    {
                        // End of checking if memberId exist and displaying order details
                        // Break out of while loop
                        break;

                    }
                }
                catch
                {
                    Console.WriteLine("An invalid response has been entered. Please try again.");
                };
                // End of try catch 
            }
            // End of while loop

            // Get the selected customer object
            Customer selectedCustomer = customers[Convert.ToString(memberId)];

            // Retrieve and display current order details of the selected customer
            Order selectedCustCurrentOrder = selectedCustomer.CurrentOrder;
            Console.WriteLine(selectedCustCurrentOrder.ToString());

            // Print all Ice Cream associated with the current order
            foreach (IceCream iceCream in selectedCustCurrentOrder.IceCreamList)
            {
                Console.WriteLine(iceCream.ToString());
            }

            // Display Menu
            Console.Write("[1] Modify an existing ice cream");
            Console.Write("[2] Add a new ice cream");
            Console.Write("[3] Delete an existing ice cream");

            // Initialise option variable
            int option;

            // Check if the option typed in is correct
            while (true)
            {
                try
                {
                    Console.Write("Enter your option: ");
                    option = Convert.ToInt32(Console.ReadLine());

                    if (option < 1 || option > 3)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("An invalid option is entered. Please enter a valid option.");
                }
            }

            // Option typed in below will be correct
            switch (option)
            {
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
        // End of function

        // ============================================== Advanced Features ======================================================

        // (a) (i) Process an order and checkout =======================================
        static void ProcessOrderAndCheckout(Queue<Customer> goldQueue, Queue<Customer> ordinaryQueue)
        {
            Customer currentCustomer;

            // If there is a customer in Gold Queue, Dequeue Customer from goldQueue.
            if (goldQueue.Count != 0)
            {
                currentCustomer = goldQueue.Dequeue();
            }

            // Else Dwqueue Customer from ordinaryQueue
            else
            {
                currentCustomer = ordinaryQueue.Dequeue();
            }

            // Storing currentOrder
            Order currentOrder = currentCustomer.CurrentOrder;

            // Storing currentPointCard
            PointCard currentPointCard = currentCustomer.Rewards;

            // display all the ice creams in the order
            foreach (IceCream iceCream in currentOrder.IceCreamList)
            {
                Console.WriteLine(iceCream.ToString());
            }
            Console.WriteLine();

            // display the total bill amount 
            double totalBill = currentOrder.CalculateTotal();
            Console.WriteLine("Total Bill: $" + totalBill.ToString("0.00"));

            // display the membership status & points of the customer
            Console.WriteLine("Membership Status: " + currentPointCard.Tier);
            Console.WriteLine();

            // For Validation of Q4 Part 6
            bool isMostExpensiveIceCreamAtIndex0 = true;

            // Check if it is the customer’s birthday, and if it is, calculate the final bill while having the most expensive ice cream in the order cost $0.00
            if (currentCustomer.IsBirthday())
            {
                IceCream mostExpensiveIceCream;

                // Temporarily storing most expensive ice cream as the first ice cream in IceCreamList
                mostExpensiveIceCream = currentOrder.IceCreamList[0];

                // Looking for most Expensive Ice Cream in IceCreamList. Start from 1 to skip the first ice cream added.
                for (int i = 1; i < currentOrder.IceCreamList.Count; i++)
                {
                    if (currentOrder.IceCreamList[i].CalculatePrice() > mostExpensiveIceCream.CalculatePrice())
                    {
                        mostExpensiveIceCream = currentOrder.IceCreamList[i];
                        isMostExpensiveIceCreamAtIndex0 = false;
                    }
                }

                // Setting most expensive ice cream in the order's cost as $0.00
                totalBill -= mostExpensiveIceCream.CalculatePrice();

                // Checking if Most Expensive IceCream At Index 0
            }

            // Check if the customer has completed their punch card. If so, then calculate the final bill while having the first ice cream in their order cost $0.00 and reset their punch card back to 0
            if (currentPointCard.PunchCard == 10)
            {
                // Checking if the first ice cream's total price was already set to $0
                if (!isMostExpensiveIceCreamAtIndex0)
                {
                    totalBill -= currentOrder.IceCreamList[0].CalculatePrice();
                }
                // If first ice cream already set to $0, make 2nd ice cream price's $0
                else
                {
                    totalBill -= currentOrder.IceCreamList[1].CalculatePrice();
                }

                // Resets punch card back to 0 will be done at the End
            }

            // Check Pointcard status to determine if the customer can redeem points. If they cannot, skip to displaying the final bill amount
            if (currentPointCard.Tier.ToLower() != "ordinary")
            {
                while (true)
                {
                    int noOfPointsToRedeem;

                    // Ensuring Input value is an Integer
                    try
                    {
                        Console.Write("Enter number of points to redeem to offset final bill (Enter 0 to not Redeem any points): ");
                        noOfPointsToRedeem = Convert.ToInt32(Console.ReadLine());

                        if (noOfPointsToRedeem == 0)
                        {
                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Please Enter an Integer.");
                        continue;
                    }

                    // Ensuring Point Card has sufficient Points to be redeemed
                    try
                    {
                        currentPointCard.RedeemPoints(noOfPointsToRedeem);
                        totalBill -= noOfPointsToRedeem * 0.02;
                        break;
                    }
                    catch (ArgumentException)
                    {

                        Console.WriteLine("Insufficient Points in Point Card");
                        continue;
                    }
                }

            }

            // Display the final total bill amount
            Console.WriteLine("Final Bill: $" + totalBill.ToString("0.00"));

            // Prompt user to press any key to make payment
            Console.Write("\nPress any key to make payment: ");
            Console.ReadLine();

            // Increment the punch card for every ice cream in the order (if it goes above 10 just set it back down to 10)
            for (int i = 0; i < currentOrder.IceCreamList.Count; i++)
            {
                currentPointCard.Punch();
            }

            // Earn points
            currentPointCard.AddPoints(totalBill);

            // While earning points, upgrade the member status accordingly
            // Done in PointCard AddPoints() method

            // Mark the order as fulfilled with the current datetime
            currentOrder.TimeFulfilled = DateTime.Now;

            // Add this fulfilled order object to the customer’s order history
            currentCustomer.OrderHistory.Add(currentOrder);
        }

        // (c) (Jeffrey) Convert from SGD to specified Currency ========================
        static public void ConvertCurrency()
        {
            List<string> availableCurrencies = new List<string>() { "AED", "AFN", "ALL", "AMD", "ANG", "AOA", "ARS", "AUD", "AWG", "AZN", "BAM", "BBD", "BDT", "BGN", "BHD", "BIF", "BMD", "BND", "BOB", "BOV", "BRL", "BSD", "BTN", "BWP", "BYN", "BZD", "CAD", "CDF", "CHF", "CLF", "CLP", "CNY", "COP", "CRC", "CUC", "CUP", "CVE", "CZK", "DJF", "DKK", "DOP", "DZD", "EGP", "ERN", "ETB", "EUR", "FJD", "FKP", "GBP", "GEL", "GHS", "GIP", "GMD", "GNF", "GTQ", "GYD", "HKD", "HNL", "HRK", "HTG", "HUF", "IDR", "ILS", "INR", "IQD", "IRR", "ISK", "JMD", "JOD", "JPY", "KES", "KGS", "KHR", "KMF", "KPW", "KRW", "KWD", "KYD", "KZT", "LAK", "LBP", "LKR", "LRD", "LSL", "LTL", "LVL", "LYD", "MAD", "MDL", "MGA", "MKD", "MMK", "MNT", "MOP", "MRO", "MUR", "MVR", "MWK", "MXN", "MYR", "MZN", "NAD", "NGN", "NIO", "NOK", "NPR", "NZD", "OMR", "PAB", "PEN", "PGK", "PHP", "PKR", "PLN", "PYG", "QAR", "RON", "RSD", "RUB", "RWF", "SAR", "SBD", "SCR", "SDG", "SEK", "SGD", "SHP", "SLL", "SOS", "SRD", "SSP", "STD", "SVC", "SYP", "SZL", "THB", "TJS", "TMT", "TND", "TOP", "TRY", "TTD", "TWD", "TZS", "UAH", "UGX", "USD", "UYU", "UZS", "VEF", "VND", "VUV", "WST", "XAF", "XCD", "XOF", "XPF", "YER", "ZAR", "ZMW", "ZWL" };

            // Displaying supported currencies
            Console.WriteLine("Available Currencies to Convert To:");
            for (int i = 1; i < availableCurrencies.Count; i++)
            {
                if (i % 20 == 0)
                {
                    Console.WriteLine(availableCurrencies[i]);
                    continue;
                }
                Console.Write($"{availableCurrencies[i]}, ");

            }

            // Ensuring Currrency to convert to is valid
            string convertTo;
            while (true)
            {
                try
                {
                    Console.Write("Enter currency you want to convert to: ");
                    convertTo = Console.ReadLine();
                    if (!availableCurrencies.Contains(convertTo.ToUpper()))
                    {
                        throw new ArgumentException();
                    }
                    break;

                }
                catch (Exception)
                {

                    Console.WriteLine("Please enter a valid country code to convert to."); ;
                }
            }

            Console.Write("Enter amount: ");
            double amount = Convert.ToDouble(Console.ReadLine());
            double convertedAmount = GetConvertedAmount(amount, convertTo);

            if (convertedAmount != -1)
            {
                Console.WriteLine($"Aount to pay in {convertTo.ToUpper()}: ${convertedAmount.ToString("0.00")}\n\n");
            }
            else
            {
                Console.WriteLine("API Call Unsuccessful.");
            }
        }

        // Getting the Converted Amount by calling CurrencyBeacon's Convert API Endpoint
        static public double GetConvertedAmount(double amount, string convertTo)
        {
            string apiKey = "ZP5D2ofy27CdcrkWlvFDFyHoZLDI40vR";

            // Create an instance of HttpClient
            HttpClient httpClient = new HttpClient();

            // Specify the API endpoint URL
            string apiUrl = $"https://api.currencybeacon.com/v1/convert?api_key={apiKey}&from=SGD&to={convertTo}&amount={amount}";
            try
            {
                // Make an HTTP GET request
                HttpResponseMessage response = httpClient.GetAsync(apiUrl).Result;

                // Check if the response is successful (HTTP status code 200-299)
                if (response.IsSuccessStatusCode)
                {
                    // Parse and process the response content here
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    Rootobject result = JsonConvert.DeserializeObject<Rootobject>(responseBody);
                    return result.value;
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    // Handle the error response here if needed
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                // Handle exceptions related to the request itself
            }
            finally
            {
                // Dispose of the HttpClient instance in the finally block
                httpClient.Dispose();
            }
            return -1;
        }
    }

    // Created Classes to store Response from CurrencyBeacon's Convert API Endpoint
    public class Rootobject
    {
        public Meta meta { get; set; }
        public Response response { get; set; }
        public int timestamp { get; set; }
        public string date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int amount { get; set; }
        public float value { get; set; }
    }

    public class Meta
    {
        public int code { get; set; }
        public string disclaimer { get; set; }
    }

    public class Response
    {
        public int timestamp { get; set; }
        public string date { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int amount { get; set; }
        public float value { get; set; }
    }

}

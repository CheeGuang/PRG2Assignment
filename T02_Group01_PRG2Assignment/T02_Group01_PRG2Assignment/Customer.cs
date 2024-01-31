//==========================================================
// Student Number : S10258143
// Student Name : Lee Guang Le, Jeffrey
// Partner Name : Zou Ruining Raeanne
//==========================================================

using System;
using System.Collections.Generic;

namespace T02_Group01_PRG2Assignment
{
    class Customer
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int memberId;

        public int MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }
        private DateTime dob;

        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }
        private Order currentOrder;

        public Order CurrentOrder
        {
            get 
            { 
                try 
                {
                    return currentOrder;
                    
                }
                catch
                {
                    throw new NullReferenceException();
                }
            }
            set { currentOrder = value; }
        }
        private List<Order> orderHistory = new List<Order>();

        public List<Order> OrderHistory
        {
            get { return orderHistory; }
            set { orderHistory = value; }
        }
        private PointCard rewards = new PointCard();

        public PointCard Rewards
        {
            get { return rewards; }
            set { rewards = value; }
        }
        public Customer()
        {
        }
        public Customer(string name, int memberId, DateTime dob)
        {
            Name = name;
            MemberId = memberId;
            Dob = dob;
        }
        public Order MakeOrder()
        {
            // Q4b Create a customer's order (Basic Features) ==============================

            // Creating newOrder
            Order newOrder = new Order(Global.orderDict.Count + 1, DateTime.Now);

            while (true)
            {
                // Delcaring Variables to create IceCream object
                IceCream iceCreamOrdered;
                string option;
                int scoops;
                List<Flavour> flavours = new List<Flavour>();
                List<Topping> toppings = new List<Topping>();

                // Ensure option is either cup, cone or waffle
                while (true)
                {
                    try
                    {
                        Console.Write("Enter Ice-Cream Option (Cup, Cone or Waffle): ");
                        option = Console.ReadLine().ToLower();

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
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine();
                        continue;
                    }
                }

                // Ensure number of scoops is within 1 to 3
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
                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter a number between 1 and 3.");
                        Console.WriteLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine();
                    }
                }

                // Initialsing List of Regular and Premium Flavours
                List<string> regularFlavours = new List<string>();
                List<string> premiumFlavours = new List<string>();
                foreach(KeyValuePair<string,float> flavour in Global.flavourMenuDict)
                {
                    if (flavour.Value == 0)
                    {
                        regularFlavours.Add(flavour.Key);
                    }
                    else
                    {
                        premiumFlavours.Add(flavour.Key);
                    }
                }

                // Display Flavour Menu Header
                Console.WriteLine("======================= Flavour Menu =========================");

                // Displaying Regular Flavours
                Console.WriteLine("Available Regular Flavours:");
                foreach (string flavour in regularFlavours)
                {
                    Console.WriteLine(flavour);
                }
                Console.WriteLine();

                // Displaying Premium Flavours
                Console.WriteLine("Available Premium Flavours:");
                foreach (string flavour in premiumFlavours)
                {
                    Console.WriteLine(flavour);
                }
                Console.WriteLine();

                // Intialsing Selected Flavour Dictionary
                Dictionary<string, int> selectedFlavours = new Dictionary<string, int>();

                // Ensure Selected Flavours are valid
                for (int i = 0; i < scoops; i++)
                {
                    // Ensures Selected FLavour is valid
                    while (true)
                    {
                        try
                        {
                            Console.Write($"Enter Scoop {i + 1} Flavour: ");
                            string selectedFlavour = Console.ReadLine().ToLower();

                            if (!regularFlavours.Contains(selectedFlavour) && !premiumFlavours.Contains(selectedFlavour))
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
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
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

                // Adding available toppings from CSV to toppingsAvailable
                List<string> toppingsAvailable = new List<string>();
                foreach(string topping in Global.toppingMenuDict.Keys)
                {
                    toppingsAvailable.Add(topping);
                }

                Console.WriteLine("====================== Toppings Menu =========================");

                // Displaying available Toppings
                foreach (string topping in toppingsAvailable)
                {
                    Console.WriteLine(topping);
                }
                Console.WriteLine();

                for (int i = 0; i < 4; i++)
                {
                    // Ensure Selected Toppings are valid
                    string selectedTopping;
                    while (true)
                    {
                        try
                        {
                            Console.Write($"Enter Topping {i + 1} (Enter 'X' to Exit): ");
                            selectedTopping = Console.ReadLine().ToLower();

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
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine();
                            continue;
                        }

                    }
                    if (selectedTopping.ToLower() == "x")
                    {
                        break;
                    }
                }

                // Logic to create Cup IceCream
                if (option.ToLower() == "cup")
                {
                    iceCreamOrdered = new Cup(scoops, flavours, toppings);
                }

                // Logic to create Cone IceCream
                else if (option.ToLower() == "cone")
                {
                    // Ensure User enters y or n
                    string isDippedInput;
                    while (true)
                    {
                        try
                        {
                            Console.Write("Do you want your cone Dipped in chocolate? (Y, N): ");
                            isDippedInput = Console.ReadLine().ToLower();

                            if (isDippedInput != "y" && isDippedInput != "n")
                            {
                                throw new ArgumentException();
                            }
                            break;
                        }
                        catch (ArgumentException)
                        {

                            Console.WriteLine("Enter either 'y' or 'n'."); ;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine();
                        }
                    }

                    iceCreamOrdered = new Cone(scoops, flavours, toppings, isDippedInput == "y");
                }

                // Logic to create Waffle IceCream
                else
                {
                    string selectedWaffleFlavour;
                    List<string> waffleFlavourAvailable = new List<string>() { "original", "red velvet", "charcoal", "pandan" };

                    // Displaying available waffle flavours
                    Console.WriteLine("=================== Waffle Flavour Menu ======================");
                    foreach (string waffleFlavour in waffleFlavourAvailable)
                    {
                        Console.WriteLine(waffleFlavour);
                    }
                    Console.WriteLine();

                    // Ensures selectedWaffleFlavour is valid
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
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine();
                            continue;
                        }
                    }


                    iceCreamOrdered = new Waffle(scoops, flavours, toppings, selectedWaffleFlavour);
                }

                // Adding the newly created iceCream to the newOrder's IceCreamList
                newOrder.AddIceCream(iceCreamOrdered);

                string answer;

                // Prompt user if they want to order another iceCream
                // Ensure answer is y or n
                while (true)
                {
                    try
                    {
                        Console.Write("Would you like to add another ice cream to the order? (Y, N): ");
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
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine();
                        continue;
                    }
                }

                if (answer.ToLower() == "n")
                {
                    Global.orderDict.Add(newOrder.Id, newOrder);
                    break;
                }
            }



            // Adding newOrder to CurrentOrder
            CurrentOrder = newOrder;

            return newOrder;
        }
        public bool IsBirthday()
        {            
            return (DateTime.Now.Month == Dob.Month && DateTime.Now.Day == Dob.Day);
        }
        public override string ToString()
        {
            return string.Format("| {0,-10} | {1,-15} | {2,-14} | {3,-7} | {4,-6} | {5, -12} |", MemberId, Name, Dob.ToString("dd/MM/yyyy"), Rewards.Points, Rewards.PunchCard, Rewards.Tier);
        }
    }
}

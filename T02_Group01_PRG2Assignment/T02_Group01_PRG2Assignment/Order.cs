//==========================================================
// Student Number : S10258772G
// Student Name : Zou Ruining Raeanne
// Partner Name : Lee Guang Le, Jeffrey
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace T02_Group01_PRG2Assignment
{
    class Order
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private DateTime timeReceived;
                
        public DateTime TimeReceived
        {
            get { return timeReceived; }
            set { timeReceived = value; }
        }
        private DateTime? timeFulfilled;

        public DateTime? TimeFulfilled
        {
            get { return timeFulfilled; }
            set { timeFulfilled = value; }
        }
        private List<IceCream> iceCreamList = new List<IceCream>();

        public List<IceCream> IceCreamList
        {
            get { return iceCreamList; }
            set { iceCreamList = value; }
        }
        public Order()
        {
            
        }
        public Order(int id, DateTime timeReceived)
        {
            Id = id;
            TimeReceived = timeReceived;
        }
        public void ModifyIceCream(int index)
        {
            // Q6a Modify an Ice Cream (Basic Features) 
            IceCream selectedIceCream = iceCreamList[index];

            while (true)
            {
                try
                {
                    // Menu 
                    Console.WriteLine("\nWhat would you like to modify?");
                    Console.WriteLine("[1] Option (Cup/Cone/Waffle)");
                    Console.WriteLine("[2] Number of Scoops");
                    Console.WriteLine("[3] Flavours");
                    Console.WriteLine("[4] Toppings");
                    Console.WriteLine("[5] Choice of Dipped Cone (for cone option only)");
                    Console.WriteLine("[6] Choice of Waffle Flavour (for waffle option only)\n");

                    Console.Write("Please enter the number (1 - 6): ");
                    int selectedNumber = Convert.ToInt32(Console.ReadLine());

                    switch(selectedNumber)
                    {
                        case 1: 
                            Console.Write("What would you like to change your Ice Cream option to (Cone, Cup, Waffle): ");
                            string iceCreamOption = Console.ReadLine().ToLower();

                            if (iceCreamOption == selectedIceCream.Option.ToLower())
                            {
                                Console.WriteLine("You have picked the same option, no changes will be made");
                            }
                            else if (iceCreamOption == "cone" )
                            {
                                Console.Write("Would you like to upgrade to a chocolate-dipped cone (y/n) [+ $2]: ");
                                string dippedResponse = Console.ReadLine().ToLower();
                                
                                if (dippedResponse == "y")
                                {
                                    selectedIceCream = new Cone(selectedIceCream.Scoops, selectedIceCream.Flavours, selectedIceCream.Toppings, true);
                                }
                                else if (dippedResponse == "n")
                                {
                                    selectedIceCream = new Cone(selectedIceCream.Scoops, selectedIceCream.Flavours, selectedIceCream.Toppings, false);
                                }
                                else
                                {
                                    throw new ArgumentOutOfRangeException();
                                }

                                //This is for display purposes
                                Console.WriteLine("\n\n------------------------------------------------");
                                Console.WriteLine("Ice Cream modification successful. \nYour modified ice cream is: ");
                                Global.DisplayOrder(selectedIceCream);
                                Console.WriteLine("------------------------------------------------");

                            }
                            else if (iceCreamOption == "cup")
                            {
                                //This is for display purposes
                                Console.WriteLine("\n\n------------------------------------------------");
                                Console.WriteLine("Ice Cream modification successful. \nYour modified ice cream is: ");
                                Global.DisplayOrder(selectedIceCream);
                                Console.WriteLine("------------------------------------------------");
                            }
                            else if (iceCreamOption == "waffle")
                            {
                                // Print Waffle Flavour menu
                                Console.WriteLine("\n======================== Waffle Flavour Menu ========================");
                                foreach (string tmpWaffleFlav in Global.waffleFlavList)
                                {
                                    Console.WriteLine($"{tmpWaffleFlav}");
                                };

                                Console.Write("Which waffle flavour would you like: ");
                                int waffleFlavourResponse = Convert.ToInt32(Console.ReadLine());

                                if (waffleFlavourResponse == 1)
                                {
                                    selectedIceCream = new Waffle(selectedIceCream.Scoops, selectedIceCream.Flavours, selectedIceCream.Toppings, "Original");
                                }
                                else if (waffleFlavourResponse == 2)
                                {
                                    selectedIceCream = new Waffle(selectedIceCream.Scoops, selectedIceCream.Flavours, selectedIceCream.Toppings, "Charcoal");
                                }
                                else if (waffleFlavourResponse == 3)
                                {
                                    selectedIceCream = new Waffle(selectedIceCream.Scoops, selectedIceCream.Flavours, selectedIceCream.Toppings, "Pandan");
                                }
                                else if (waffleFlavourResponse == 4)
                                {
                                    selectedIceCream = new Waffle(selectedIceCream.Scoops, selectedIceCream.Flavours, selectedIceCream.Toppings, "Red Velvet");
                                }
                                else
                                {
                                    throw new ArgumentOutOfRangeException();
                                }

                                //This is for display purposes
                                Console.WriteLine("\n\n------------------------------------------------");
                                Console.WriteLine("Ice Cream modification successful. \nYour modified ice cream is: ");
                                Global.DisplayOrder(selectedIceCream);
                                Console.WriteLine("------------------------------------------------");
                            }
                            else
                            {
                                throw new ArgumentOutOfRangeException();
                            }
                            break;

                        case 2: 
                            Console.Write("How many scoops of ice cream would you like (1 - 3): ");
                            int scoopsIp = Convert.ToInt32(Console.ReadLine());

                            if (selectedIceCream.Scoops == scoopsIp)
                            {
                                Console.WriteLine("You have entered the same option, no changes will be made");
                            }
                            else if (scoopsIp >= 1 && scoopsIp <= 3)
                            {
                                //selectedIceCream.Flavours.Clear();

                                // Print flavour menu
                                Console.WriteLine("\n======================== Flavour Menu ========================");
                                foreach (string tmpFlavour in Global.flavourMenuDict.Keys)
                                {
                                    Console.WriteLine($"{tmpFlavour}");
                                }

                                int numOfFlavours = scoopsIp;
                                if (scoopsIp > selectedIceCream.Scoops)
                                {
                                    // Find the additional number of flavours
                                    numOfFlavours = scoopsIp - selectedIceCream.Scoops;
                                }
                                else
                                {
                                    // If modification reduces number of scoops, clear the entire list and repopulate
                                    selectedIceCream.Flavours.Clear();
                                }

                                for (int i = 1; i <= numOfFlavours; i++)
                                {
                                    Console.Write($"New Flavour {i}: ");
                                    string flavourIp = Console.ReadLine().ToLower();

                                    bool isFound = false;

                                    foreach (Flavour tmpFlavour in selectedIceCream.Flavours)
                                    {
                                        if (tmpFlavour.Type == flavourIp)
                                        {
                                            tmpFlavour.Quantity++;
                                            isFound = true;

                                            //Break the foreach loop
                                            break;
                                        }
                                    }
                                    // Move on to the next for loop element
                                    if (isFound)
                                    {
                                        break;
                                    }

                                    bool isPrem = true;
                                    float price = 0;
                                    while (!Global.flavourMenuDict.TryGetValue(flavourIp, out price))
                                    {
                                        Console.WriteLine("You have entered an invalid flavour. Please try again");
                                        Console.Write($"Flavour {i}: ");
                                        flavourIp = Console.ReadLine();
                                    }
                                    if (Global.flavourMenuDict[flavourIp] == 0)
                                    {
                                        isPrem = false;
                                    }

                                    selectedIceCream.Flavours.Add(new Flavour(flavourIp, isPrem, 1));
                                }
                                Console.WriteLine("Order Updated!");
                                Console.WriteLine("\n\n------------------------------------------------");
                                Console.WriteLine("Ice Cream modification successful. \nYour modified ice cream is: ");
                                Global.DisplayOrder(selectedIceCream);
                            }
                            break;

                        case 3: 
                            Console.WriteLine("\n======================== Current Order ========================");
                            Global.DisplayOrder(selectedIceCream);

                            // Print flavour menu
                            Console.WriteLine("\n======================== Flavour Menu ========================");
                            foreach (string tmpFlavour in Global.flavourMenuDict.Keys)
                            {
                                Console.WriteLine($"{tmpFlavour}");
                            }

                            // Loop through all the flavours in the order detail and prompt to change 
                            selectedIceCream.Flavours.Clear();

                            for (int i = 1; i <= selectedIceCream.Scoops; i++)
                            {
                                Console.Write($"New Flavour {i}: ");
                                string flavourIp = Console.ReadLine().ToLower();

                                //Check if flavour is valid 
                                while (!Global.flavourMenuDict.Keys.Contains(flavourIp))
                                {
                                    Console.WriteLine("You have entered an invalid flavour. Please try again");
                                    Console.Write($"New Flavour {i}: ");
                                    flavourIp = Console.ReadLine().ToLower();
                                }

                                bool isFound = false;
                                foreach (Flavour tmpFlavour in selectedIceCream.Flavours)
                                {
                                    if (tmpFlavour.Type == flavourIp)
                                    {
                                        tmpFlavour.Quantity++;
                                        isFound = true;

                                        //Break the foreach loop
                                        break;
                                    }
                                }
                                // Move on to the next for loop element
                                if (isFound)
                                {
                                    break;
                                }

                                bool isPrem = true;
                                if (Global.flavourMenuDict[flavourIp] == 0)
                                {
                                    isPrem = false;
                                }

                                // Add new flavour object
                                selectedIceCream.Flavours.Add(new Flavour(flavourIp, isPrem, 1));
                            }

                            Console.WriteLine("Order Updated!");
                            Console.WriteLine("\n\n------------------------------------------------");
                            Console.WriteLine("Ice Cream modification successful. \nYour modified ice cream is: ");
                            Global.DisplayOrder(selectedIceCream);

                            break;

                        case 4: 
                            Console.WriteLine("\n======================== Current Order ========================");
                            Global.DisplayOrder(selectedIceCream);

                            // Print Toppings menu
                            Console.WriteLine("\n======================== Topping Menu ========================");
                            foreach (string tmpTopping in Global.toppingMenuDict.Keys)
                            {
                                Console.WriteLine($"{tmpTopping}");
                            }

                            // Clear selection
                            selectedIceCream.Toppings.Clear();

                            // Ask for how many toppings user will want
                            Console.Write("How many topppings would you like (max 4 toppings) : ");
                            int toppingNum = Convert.ToInt32(Console.ReadLine());

                            if (toppingNum == 0)
                            {
                                Console.WriteLine("You have removed all toppings from your ice cream");
                                break;
                            } 
                            else if (toppingNum < 0 || toppingNum > 4)
                            {
                                throw new ArgumentOutOfRangeException();
                            }

                            // Loop through all the toppings in the order detail and prompt to change 
                            for (int i = 1; i <= toppingNum; i++)
                            {
                                Console.Write($"New Topping {i}: ");
                                string toppingIp = Console.ReadLine().ToLower();

                                //Check if flavour is valid 
                                while (!Global.toppingMenuDict.Keys.Contains(toppingIp))
                                {
                                    Console.WriteLine("You have entered an invalid topping. Please try again");
                                    Console.Write($"New Topping {i}: ");
                                    toppingIp = Console.ReadLine().ToLower();
                                }

                                // Add new flavour object
                                selectedIceCream.Toppings.Add(new Topping(toppingIp));
                            }

                            Console.WriteLine("Order Updated!");
                            Console.WriteLine("\n\n------------------------------------------------");
                            Console.WriteLine("Ice Cream modification successful. \nYour modified ice cream is: ");
                            Global.DisplayOrder(selectedIceCream);

                            break;

                        case 5: // ===========================================================================================

                            // Check if current order ice cream is a Cone
                            if (selectedIceCream.Option.ToLower() != "cone")
                            {
                                Console.WriteLine("Sorry! Your selected option must be a cone to pick this option. \nNo changes will be made");
                                break;
                            }
                            else
                            {
                                Cone selectedOrder = (Cone) selectedIceCream;

                                Console.Write("Would you like to dip your cone? (y/n): ");
                                string dipResponse = Console.ReadLine().ToLower();

                                if (dipResponse == "y")
                                {
                                    selectedOrder.Dipped = true;
                                }
                                else
                                {
                                    selectedOrder.Dipped = false;
                                }
                            }

                            Console.WriteLine("Order Updated!");
                            Console.WriteLine("\n\n------------------------------------------------");
                            Console.WriteLine("Ice Cream modification successful. \nYour modified ice cream is: ");
                            Global.DisplayOrder(selectedIceCream);

                            break;

                        case 6: // ===========================================================================================

                            // Check if selectedIceCream is a waffle
                            if (selectedIceCream.Option.ToLower() != "waffle")
                            {
                                Console.WriteLine("Sorry! Your selected option must be a waffle to pick this option. \nNo changes will be made");
                                break;
                            }
                            else
                            {
                                Waffle selectedOrder = (Waffle) selectedIceCream;

                                // Print Waffle Flavour menu
                                Console.WriteLine("\n======================== Waffle Flavour Menu ========================");
                                foreach(string tmpWaffleFlav in Global.waffleFlavList) 
                                {
                                    Console.WriteLine($"{tmpWaffleFlav}");
                                }

                                Console.Write("Which flavour would you like to change your waffle flavour to: ");
                                string waffleFlavIp = Console.ReadLine().ToLower();

                                if (!Global.waffleFlavList.Contains(waffleFlavIp.ToLower()))
                                {
                                    throw new ArgumentOutOfRangeException();
                                }
                                else if (waffleFlavIp == selectedOrder.WaffleFlavour.ToLower())
                                {
                                    Console.WriteLine("You have selected the same option. No changes will be made");
                                    break;
                                }
                                else
                                {
                                    selectedOrder.WaffleFlavour = waffleFlavIp;
                                }
                            }

                            Console.WriteLine("Order Updated!");
                            Console.WriteLine("\n\n------------------------------------------------");
                            Console.WriteLine("Ice Cream modification successful. \nYour modified ice cream is: ");
                            Global.DisplayOrder(selectedIceCream);

                            break;

                        default:
                            throw new ArgumentException(); 
                    }
                    break; 
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a number");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("You have entered an invalid option. No changes have been made");
                }
                catch
                {
                    Console.WriteLine("This is an invalid input. Please re-enter a valid menu option");
                }
            }
            
        }
        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }
        public void DeleteIceCream(int index)
        {
            IceCreamList.RemoveAt(index);
        }
        public double CalculateTotal()
        {
            double total = 0;

            // Adding price of each icecream in iceCreamList to total
            foreach (IceCream iceCream in iceCreamList)
            {
                total += iceCream.CalculatePrice();
            }

            return total;
        }
        public override string ToString()
        {
            return "\tID: " + id + "\tTime Received: " + timeReceived.ToString("dd/MM/yyyy");
        }
    }
}

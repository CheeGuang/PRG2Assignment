using System.Collections.Generic;
using System.Linq;

namespace T02_Group01_PRG2Assignment
{
    class Cup : IceCream
    {
        public Cup()
        {
            
        }
        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base (option, scoops, flavours, toppings)
        {
            
        }
        public override double CalculatePrice()
        {
            double total = 0;

            // Defining the Pricing per Scoop
            Dictionary<int, double> scoopPrice = new Dictionary<int, double>
            {
                { 1, 4 },
                { 2, 5.5 },
                { 3, 6.5 }
            };
            total += scoopPrice[Flavours.Count()];

            // Accounting for Premium Flavours
            foreach (Flavour flavour in Flavours)
            {
                if (flavour.Premium)
                {
                    total += 2;
                }
            }

            // Accounting and adding to total for every Toppings
            foreach (Topping topping in Toppings)
            {
                total++;
            }

            return total;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}

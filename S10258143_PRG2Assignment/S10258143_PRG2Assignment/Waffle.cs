using System.Collections.Generic;
using System.Linq;

namespace S10258143_PRG2Assignment
{
    class Waffle : IceCream
    {
        private string waffleFlavour;

        public string WaffleFlavour
        {
            get { return waffleFlavour; }
            set { waffleFlavour = value; }
        }


        public Waffle()
        {

        }
        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour) : base(option, scoops, flavours, toppings)
        {
            WaffleFlavour = waffleFlavour;
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

            // Accounting for Dipped Cone
            if (WaffleFlavour.ToLower() == "red velvet" || WaffleFlavour.ToLower() == "charcoal" || WaffleFlavour.ToLower() == "pandan")
            {
                total += 3;
            }

            return total;
        }
        public override string ToString()
        {
            return base.ToString() + "\tWaffle Flavour: " + waffleFlavour;
        }
    }
}

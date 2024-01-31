//==========================================================
// Student Number : S10258143
// Student Name : Lee Guang Le, Jeffrey
// Partner Name : Zou Ruining Raeanne
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace T02_Group01_PRG2Assignment
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
        public Waffle(int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour) : base("Waffle", scoops, flavours, toppings)
        {
            WaffleFlavour = waffleFlavour;
        }
        public override double CalculatePrice()
        {
            double total = 0;

            // Accounting for Pricing of Scoops and Dipped
            foreach (string[] waffleRecord in Global.wafflePriceMenu)
            {
                if (waffleRecord[Global.optScoopIdx] == Convert.ToString(Scoops) && waffleRecord[Global.waffleMenuFlavorIdx] == WaffleFlavour)
                {
                    total += Convert.ToDouble(waffleRecord[Global.optPriceIdx]);
                }
            }

            // Accounting for Flavours Price
            foreach (Flavour flavour in Flavours)
            {
                total += Global.flavourMenuDict[flavour.Type.ToLower()] * flavour.Quantity;
            }

            // Accounting and adding to total for every Toppings
            foreach (Topping topping in Toppings)
            {
                total += Global.toppingMenuDict[topping.Type.ToLower()];
            }

            return total;
        }
        public override string ToString()
        {
            string formattedWaffleFlavour = waffleFlavour.Substring(0, 1).ToUpper() + waffleFlavour.Substring(1).ToLower();
            return base.ToString() + formattedWaffleFlavour + " Waffle";
        }
    }
}

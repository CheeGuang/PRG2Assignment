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
    class Cup : IceCream
    {
        public Cup()
        {
            
        }
        public Cup(int scoops, List<Flavour> flavours, List<Topping> toppings) : base ("Cup", scoops, flavours, toppings)
        {
            
        }
        public override double CalculatePrice()
        {
            double total = 0;

            // Accounting for Pricing of Scoops and Dipped
            foreach (float[] cupRecord in Global.cupPriceMenu)
            {
                if (cupRecord[Global.optScoopIdx] == Scoops)
                {
                    total += cupRecord[Global.optPriceIdx];
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
            return base.ToString() + "Cup";
        }
    }
}

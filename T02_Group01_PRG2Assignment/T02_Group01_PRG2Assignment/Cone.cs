//==========================================================
// Student Number : S10258143
// Student Name : Lee Guang Le, Jeffrey
// Partner Name : Zou Ruining, Raeanne
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace T02_Group01_PRG2Assignment
{
    class Cone : IceCream
    {
        private bool dipped;

        public bool Dipped
        {
            get { return dipped; }
            set { dipped = value; }
        }

        public Cone()
        {

        }
        public Cone(int scoops, List<Flavour> flavours, List<Topping> toppings, bool dipped) : base("Cone", scoops, flavours, toppings)
        {
            Dipped = dipped;
        }
        public override double CalculatePrice()
        {
            double total = 0;

            // Accounting for Pricing of Scoops and Dipped
            foreach (float[] coneRecord in Global.conePriceMenu)
            {
                if (coneRecord[Global.optScoopIdx] == Scoops && coneRecord[Global.coneMenuDippedIdx] == Convert.ToInt16(Dipped))
                {
                    total += coneRecord[Global.optPriceIdx];
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
            if (Dipped)
            {
                return base.ToString() + "Dipped Cone";
            }
            else
            {
                return base.ToString() + "Cone";
            }
            
        }
    }
}

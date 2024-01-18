//==========================================================
// Student Number : S10258143
// Student Name : Lee Guang Le, Jeffrey
// Partner Name : Zou Ruining, Raeanne
//==========================================================

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
            if(Dipped)
            {
                total += 2;
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

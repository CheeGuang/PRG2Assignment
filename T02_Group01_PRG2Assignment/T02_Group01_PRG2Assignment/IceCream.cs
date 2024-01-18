﻿//==========================================================
// Student Number : S10258143
// Student Name : Lee Guang Le, Jeffrey
// Partner Name : Zou Ruining Raeanne
//==========================================================

using System.Collections.Generic;

namespace T02_Group01_PRG2Assignment
{
    abstract class IceCream
    {
        private string option;

        public string Option
        {
            get { return option; }
            set { option = value; }
        }
        private int scoops;

        public int Scoops
        {
            get { return scoops; }
            set { scoops = value; }
        }
        private List<Flavour> flavours = new List<Flavour>();

        public List<Flavour> Flavours 
        {
            get { return flavours; }
            set 
            { 
                if(Flavours.Count < 3)
                {
                    flavours = value;
                }
            }
        } 
        private List<Topping> toppings = new List<Topping>();

        public List<Topping> Toppings
        {
            get { return toppings; }
            set
            {
                if (Toppings.Count < 4)
                {
                    toppings = value;
                }
            }
        }
        public IceCream()
        {
            
        }
        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }
        public abstract double CalculatePrice();

        public override string ToString()
        {
            //return "\tOption: " + option + "\tScoops: " + scoops + "\tFlavours: " + string.Join(", ", flavours) + "\tToppings: " + string.Join(", ", toppings);
            return "Ice Cream Order: \n";
        }
    }
}

//==========================================================
// Student Number : S10258143
// Student Name : Lee Guang Le, Jeffrey
// Partner Name : Zou Ruining Raeanne
//==========================================================

using System.Collections.Generic;

namespace T02_Group01_PRG2Assignment
{
    class Topping
    {
        private string type;
        private static Dictionary<string, int> toppingPriceDict;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public static void SetPrice(Dictionary<string, int> toppingDict)
        {
            toppingPriceDict = toppingDict;
        }

        public Topping()
        {
            
        }
        public Topping(string type)
        {
            Type = type;
        }

        public float GetPrice()
        {
            return toppingPriceDict[Type];
        }

        public override string ToString()
        {
            return "Type: " + Type;
        }
    }
}

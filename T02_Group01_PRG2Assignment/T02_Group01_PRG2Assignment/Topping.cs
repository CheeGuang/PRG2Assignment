//==========================================================
// Student Number : S10258772G
// Student Name : Zou Ruining Raeanne
// Partner Name : Lee Guang Le, Jeffrey
//==========================================================

using System.Collections.Generic;

namespace T02_Group01_PRG2Assignment
{
    class Topping
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Topping()
        {
            
        }
        public Topping(string type)
        {
            Type = type;
        }

        public override string ToString()
        {
            string formattedType = Type.Substring(0, 1).ToUpper() + Type.Substring(1).ToLower();
            return formattedType;
        }
    }
}

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
            return Type;
        }
    }
}

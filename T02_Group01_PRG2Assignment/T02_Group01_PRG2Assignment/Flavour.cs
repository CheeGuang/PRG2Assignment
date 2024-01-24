//==========================================================
// Student Number : S10258772G
// Student Name : Zou Ruining Raeanne
// Partner Name : Lee Guang Le, Jeffrey
//==========================================================

namespace T02_Group01_PRG2Assignment
{
    class Flavour
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private bool premium;

        public bool Premium
        {
            get { return premium; }
            set { premium = value; }
        }
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        public Flavour()
        {
            
        }
        public Flavour(string type, bool premium, int quantity)
        {
            Type = type;
            Premium = premium;
            Quantity = quantity;
        }
        public override string ToString()
        {
            if (Premium)
            {
                return  Quantity + " scoop(s) " + Type + "(Premium)";
            }

            return Quantity + " scoop(s) " + Type;  
        }
    }
}

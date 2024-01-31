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
            string formattedType = Type.Substring(0, 1).ToUpper() + Type.Substring(1).ToLower();

            if (Quantity == 1)
            {
                if (Premium)
                {
                    return  Quantity + "x scoop: " + formattedType + " (Premium)";
                }

                return Quantity + "x scoop: " + formattedType;  
            }
            else
            {
                if (Premium)
                {
                    return Quantity + "x scoops: " + formattedType + " (Premium)";
                }

                return Quantity + "x scoops: " + formattedType;
            }
        }
    }
}

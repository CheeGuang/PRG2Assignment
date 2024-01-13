//==========================================================
// Student Number : S10258143
// Student Name : Lee Guang Le, Jeffrey
// Partner Name : Zou Ruining Raeanne
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
            return "Type: " + Type + "Premium: " + Premium + "Quantity: " + Quantity;
        }
    }
}

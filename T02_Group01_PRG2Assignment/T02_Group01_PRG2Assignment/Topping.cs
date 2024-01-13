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
            return "Type: " + Type;
        }
    }
}

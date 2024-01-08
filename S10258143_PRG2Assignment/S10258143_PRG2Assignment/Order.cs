using System;
using System.Collections.Generic;

namespace S10258143_PRG2Assignment
{
    class Order
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private DateTime timeReceived;
                
        public DateTime TimeReceived
        {
            get { return timeReceived; }
            set { timeReceived = value; }
        }
        private DateTime? timeFulfilled;

        public DateTime? TimeFulfilled
        {
            get { return timeFulfilled; }
            set { timeFulfilled = value; }
        }
        private List<IceCream> iceCreamList;

        public List<IceCream> IceCreamList
        {
            get { return iceCreamList; }
            set { iceCreamList = value; }
        }
        public Order()
        {
            
        }
        public Order(int id, DateTime timeReceived)
        {
            Id = id;
            TimeReceived = timeReceived;
        }
        public void ModifyIceCream(int index)
        {

        }
        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }
        public void DeleteIceCream(int index)
        {
            IceCreamList.RemoveAt(index);
        }
        public double CalculateTotal()
        {
            double total = 0;

            // Adding price of each icecream in iceCreamList to total
            foreach (IceCream iceCream in iceCreamList)
            {
                total += iceCream.CalculatePrice();
            }

            return total;
        }
        public override string ToString()
        {
            return;
        }
    }
}

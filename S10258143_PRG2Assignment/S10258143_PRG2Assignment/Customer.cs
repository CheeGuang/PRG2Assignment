using System;
using System.Collections.Generic;

namespace S10258143_PRG2Assignment
{
    class Customer
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int memberId;

        public int MemberId
        {
            get { return memberId; }
            set { memberId = value; }
        }
        private DateTime dob;

        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }
        private Order currentOrder;

        public Order CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value; }
        }
        private List<Order> orderHistory;

        public List<Order> OrderHistory
        {
            get { return orderHistory; }
            set { orderHistory = value; }
        }
        private PointCard rewards;

        public PointCard Rewards
        {
            get { return rewards; }
            set { rewards = value; }
        }
        public Customer()
        {
            
        }
        public Customer(string name, int memberId, DateTime dob)
        {
            Name = name;
            MemberId = memberId;
            Dob = dob;
        }
        public Order MakeOrder()
        {
            // Previous Current Order gets added to Order History
            orderHistory.Add(CurrentOrder);

            // Innitialising a new Order and assigning it to CurrentOrder
            CurrentOrder = new Order();

            // Returning customer's new Order
            return CurrentOrder;
        }
        public bool IsBirthday()
        {            
            return (DateTime.Now.Month == Dob.Month && DateTime.Now.Day == Dob.Day);
        }
        public override string ToString()
        {
            return "Name: " + Name + "\tMemberID: " + MemberId + "\tDOB: " + Dob.ToString("dd MMMM yyyy");
        }
    }
}

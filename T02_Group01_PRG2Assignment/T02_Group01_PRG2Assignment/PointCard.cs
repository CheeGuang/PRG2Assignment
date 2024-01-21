//==========================================================
// Student Number : S10258143
// Student Name : Lee Guang Le, Jeffrey
// Partner Name : Zou Ruining Raeanne
//==========================================================

using System;

namespace T02_Group01_PRG2Assignment
{
    class PointCard
    {
        private int points;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }
        private int punchCard;

        public int PunchCard
        {
            get { return punchCard; }
            set { punchCard = value; }
        }
        private string tier;

        public string Tier
        {
            get { return tier; }
            set { tier = value; }
        }
        public PointCard()
        {
            Points = 0;
            PunchCard = 0;
            Tier = "ordinary";
        }
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
            // If Points more than 100, set tier as Gold
            if (Points >= 100)
            {
                Tier = "gold";
            }
            // If Points more than 50, set tier as Silver
            else if (Points >= 50)
            {
                Tier = "silver";
            }
            // Else, Points is less than 50. Set tier as Ordinary
            else
            {
                Tier = "ordinary";
            }
        }
        public void AddPoints(int pts)
        {
            Points += pts;

            // (a) (ii) Process an order and checkout (Additional Feature) =================
            if (Points >= 100 && Tier != "gold")
            {
                Tier = "gold";
            }
            else if (Points >= 50 && Tier == "ordinary")
            {
                Tier = "silver";
            }
        }
        public void RedeemPoints(int pts)
        {
            if (Points < pts)
            {
                throw new ArgumentException();
            }
            else if (Tier.ToLower() != "ordinary")
            {
                points -= pts;
            }
        }
        public void Punch()
        {
            // Punch the Card
            PunchCard++;
            if (PunchCard == 11)
            {
                PunchCard = 0;
            }
        }
        public override string ToString()
        {
            return "Points: " + points + "\tPunch Card: " + punchCard;
        }
    }
}

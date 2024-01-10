using System;

namespace S10258143_PRG2Assignment
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
            
        }
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
        }
        public void AddPoints(int amount)
        {
            // Explicit Downcasting from double to int needed
            points += (int)Math.Floor(amount * 0.72);
        }
        public void RedeemPoints(int pts)
        {
            points -= pts;
        }
        public void Punch()
        {
            // Punch the Card
            PunchCard++;
            if (PunchCard == 11)
            {
                PunchCard = 1;
            }
        }
        public override string ToString()
        {
            return "Points: " + points + "\tPunch Card: " + punchCard;
        }
    }
}

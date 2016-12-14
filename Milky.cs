using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CourseWork.wpf
{
    [Serializable]
    public class Milky : FoodGoods
    {
        private int capacity; //объем
        private Randomize rnd;
        private string milkyType; //тип молочного изделия

        #region builders
        public Milky() : base()
        {
            rnd = Network.rand;
            capacity = rnd.Next(1, 5);
            this.foodGoodsType = 1;
            this.name = rnd.MilkyName();
            this.producer = rnd.MilkyProducer();
            this.price = rnd.Next(50, 150);

        }
        public Milky(Randomize rnd) : base(rnd)
        {
            this.rnd = rnd;
            capacity = rnd.Next(1, 5);
            this.foodGoodsType = 1;
            this.name = rnd.MilkyName();
            this.producer = rnd.MilkyProducer();
            this.price = rnd.Next(50, 150);
            this.quantity = rnd.Next(1, 25);

        }


        public Milky(string name, int quantity, int price, int deadline, string date, int capacity, string milkyType, string producer) : base(name, quantity, price, deadline, date)
        {
            rnd = new Randomize();
            this.capacity = capacity;
            this.foodGoodsType = 1;
            this.milkyType = milkyType;
            this.producer = producer;
        }
        #endregion

        #region methods
        public int Capacity
        {
            get { return capacity; }
            set
            {
                if (value < 0) capacity = 0;
                else capacity = value;
            }
        }

        public override Goods New()
        {
            return new Milky() as Goods;
        }

        public override string ToString()
        {
            return base.ToString() + " Объем(л): " + this.capacity + "\n";
        }
        #endregion
    }

}

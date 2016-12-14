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
    public class Meat : FoodGoods
    {
        private int weigth; //  вес
        private Randomize rnd;

        #region builders
        public Meat() : base()
        {
            rnd = Network.rand;
            weigth = rnd.Next(1, 20);
            this.producer = rnd.MeatProducer();
            this.name = rnd.MeatName();
            this.foodGoodsType = 2;
            this.price = rnd.Next(100, 500);
            this.quantity = rnd.Next(1, 25);
        }

        public Meat(Randomize rnd) : base()
        {
            this.rnd = rnd;
            weigth = rnd.Next(1, 20);
            this.producer = rnd.MeatProducer();
            this.name = rnd.MeatName();
            this.foodGoodsType = 2;
            this.price = rnd.Next(100, 500);
            this.quantity = rnd.Next(1, 25);
        }

        public Meat(string name, int quantity, int price, int deadline, string date, string producer) : base(name, quantity, price, deadline, date)
        {
            rnd = new Randomize();
            this.producer = producer;
            this.foodGoodsType = 2;
        }
        #endregion

        #region methods
        public override Goods New()
        {
            return new Meat() as Goods;
        }

        public override string ToString()
        {
            return base.ToString() + " Вес(кг): " + this.weigth + "\n";
        }
        #endregion
    }

}

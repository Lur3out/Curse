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
    public class FoodGoods : Goods
    {
        //foodGoodsType:
        //1 - Молочные
        //2 - Мясные

        protected int deadline; //срок годности
        private Randomize rnd;
        protected string date; // дата изготовления
        protected int month, day, year = 2016, hours, minutes, seconds;
        protected int foodGoodsType; // подтип товара

        #region builders
        public FoodGoods() : base()
        {
            this.productionType = 1;
            rnd = Network.rand;
            deadline = rnd.Next(1, 365);
            month = rnd.Next(1, 12);
            day = rnd.Next(1, 28);
            hours = rnd.Next(1, 23);
            minutes = rnd.Next(1, 59);
            seconds = rnd.Next(1, 59);
            this.quantity = rnd.Next(1, 25);
            this.date = day + "." + month + "." + year + " " + hours + ":" + minutes + ":" + seconds;
        }
        public FoodGoods(Randomize rnd) : base(rnd)
        {
            this.productionType = 1;
            this.rnd = rnd;
            deadline = rnd.Next(1, 365);
            month = rnd.Next(1, 12);
            day = rnd.Next(1, 28);
            hours = rnd.Next(0, 23);
            minutes = rnd.Next(0, 59);
            seconds = rnd.Next(0, 59);
            this.date = day + "." + month + "." + year + " " + hours + ":" + minutes + ":" + seconds;
        }

        public FoodGoods(string name, int quantity, int price, int deadline, string date) : base(name, quantity, price)
        {
            this.productionType = 1;
            rnd = new Randomize();
            this.deadline = deadline;
            this.date = date;
        }

        #endregion

        #region methods
        public override Goods New()
        {
            return new FoodGoods() as Goods;
        }

        public override string ToString()
        {
            return base.ToString() + " Срок годности(дни): " + this.deadline + "\n Дата изготовления: " + this.date + "\n";
        }
        #endregion
    }

}

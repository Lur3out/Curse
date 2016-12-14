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
    abstract public class Goods
    {

        protected string name; //имя товара
        protected int quantity; //количество данного товара
        protected int price; //стоимость товара
        public int productionType = 0; //тип товара 1 - продовольственный, 2 - производственный
        protected string producer; // производитель

        #region builders

        public Goods()
        {
            name = "";
            price = 0;
            quantity = 0;
            producer = "";
        }

        public Goods(Randomize rnd)
        {
            name = "";
            price = 0;
            quantity = 0;
            producer = "";
        }

        public Goods(string name, int quantity, int price)
        {
            this.name = name;
            this.quantity = quantity;
            this.price = price;
            producer = "";
        }

        #endregion

        #region methods

        public int Price
        {
            get { return price; }

            set
            {
                if (value < 0) price = 0;
                else price = value;
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value < 0) quantity = 0;
                else quantity = value;
            }
        }

        public string Name
        {
            get { return name; }
            set {; }
        }

        public bool IsFood()
        {
            if (productionType == 1) return true;
            return false;
        }

        abstract public Goods New(); // метод для автоматическогo оформирования объектов.

        public override string ToString()
        {
            return "\n Название товара: " + this.name + "\n Количество: " + this.quantity + "\n Стоимость: " + this.price + "\n Производитель: " + this.producer + "\n";
        }

        #endregion
    }

}

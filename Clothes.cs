using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace CourseWork.wpf
{
    [Serializable]
    public class Clothes : IndustrialGoods
    {
        private string clothesType; //вид одежды
        private string clothesSubType; //подвид вида одежды
        private Randomize rnd; //создание экзмепляра класса с полем Random rnd и различными методами.

        #region builders
        public Clothes() : base()
        {
            rnd = Network.rand;
            this.price = rnd.Next(1000, 5000);
            this.quantity = rnd.Next(1, 20);
            this.producer = rnd.ClothesProducer();
            this.clothesType = rnd.ClothesType();
            if (clothesType == "Головной убор") clothesSubType = rnd.ClothesType_Head();
            if (clothesType == "Верхняя одежда") clothesSubType = rnd.ClothesType_Wear();
            if (clothesType == "Обувь") clothesSubType = rnd.ClothesType_Boots();
            if (clothesType == "Спортивная одежда") clothesSubType = rnd.ClothesType_Sports();
        }

        public Clothes(Randomize rnd) : base(rnd)
        {
            this.rnd = rnd;
            this.price = rnd.Next(1000, 5000);
            this.quantity = rnd.Next(1, 20);
            this.producer = rnd.ClothesProducer();
            this.clothesType = rnd.ClothesType();
            if (clothesType == "Головной убор") clothesSubType = rnd.ClothesType_Head();
            if (clothesType == "Верхняя одежда") clothesSubType = rnd.ClothesType_Wear();
            if (clothesType == "Обувь") clothesSubType = rnd.ClothesType_Boots();
            if (clothesType == "Спортивная одежда") clothesSubType = rnd.ClothesType_Sports();
        }

        public Clothes(string name, int quantity, int price, string clothesType, string clothesSubType, string producer) : base(name, quantity, price)
        {
            rnd = new Randomize();
            this.clothesType = clothesType;
            this.clothesSubType = clothesSubType;
            this.producer = producer;
        }
        #endregion

        #region methods
        public override Goods New()
        {
            return new Clothes() as Goods;
        }

        public override string ToString()
        {
            return "\n" + "\n Тип одежды: " + this.clothesType + "\n Количество: " + this.quantity + "\n Стоимость: " + this.price + "\n Производитель: " + this.producer + "\n Подвид: " + this.clothesSubType + "\n";
        }
        #endregion
    }

}

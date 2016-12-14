using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace CourseWork.wpf
{
    [Serializable]
    public class Randomize
    {
        private Random rand;
        private string[] names = { "NotFixPrice_A_", "NotFixPrice_B_", "NotFixPrice_C_", "NotFixPrice_D_", "Not_Fix_Price_E_" };


        public Randomize()
        {
            rand = new Random();
        }


        public int Next(int low, int high)
        {
            return rand.Next(low, high);
        }

        public Goods GetNew(Randomize rnd)
        {
            //x:1 - IndustrialGoods, 2 - FoodGoods
            //y:1 - Clothes/Milky, 2 - Mechanics/Meat

            int type = rnd.Next(1, 5);
            switch (type)
            {
                case 1: return new Clothes(rnd);
                case 2: return new Milky(rnd);
                case 3: return new Mechanics(rnd);
                case 4: return new Meat(rnd);
            }
            return null;
        }

        public string StoreName(int i)
        {
            return names[rand.Next(0, 5)] + i;
        }

        #region Goods
        #region IndustrialGoods

        #region Clothes
        public string ClothesType_Head()
        {
            int type = rand.Next(1, 5);
            switch (type)
            {
                case 1: return "Берет";
                case 2: return "Кепка";
                case 3: return "Шапка-ушанка";
                case 4: return "Шляпа";
            }

            return "";
        }

        public string ClothesType_Wear()
        {
            int type = rand.Next(1, 5);
            switch (type)
            {
                case 1: return "Пуховик";
                case 2: return "Пальто";
                case 3: return "Дубленка";
                case 4: return "Шуба";
            }

            return "";
        }

        public string ClothesType_Boots()
        {
            int type = rand.Next(1, 5);
            switch (type)
            {
                case 1: return "Туфли";
                case 2: return "Кроссовки";
                case 3: return "Сандалии";
                case 4: return "Тапки";
            }
            return "";
        }

        public string ClothesType_Sports()
        {
            int type = rand.Next(1, 5);
            switch (type)
            {
                case 1: return "Футболка";
                case 2: return "Шорты";
                case 3: return "Толстовка";
                case 4: return "Олимпийка";
            }
            return "";
        }

        public string ClothesType()
        {
            int type = rand.Next(1, 5);
            switch (type)
            {
                case 1: return "Головной убор";
                case 2: return "Верхняя одежда";
                case 3: return "Обувь";
                case 4: return "Спортивная одежда";
            }
            return "";
        }

        public string ClothesProducer()
        {
            int type = rand.Next(1, 5);
            switch (type)
            {
                case 1: return "Molotov";
                case 2: return "Nike";
                case 3: return "Termit";
                case 4: return "Columbia";
            }
            return "";
        }
        #endregion

        #region Mechanics
        //генерирования названий
        public string MechanicsName_Computer()
        {
            int x = rand.Next(1, 5);
            switch (x)
            {
                case 1: return "AS783/22";
                case 2: return "DeepReality LP734.56";
                case 3: return "SlowMo GH55F78";
                case 4: return "Super H577";
                case 5: return "Quick 57HJ22";
            }

            return "";
        }

        public string MechanicsName_Car()
        {
            int x = rand.Next(1, 5);
            switch (x)
            {
                case 1: return "F5";
                case 2: return "Z4";
                case 3: return "A2";
                case 4: return "H7";
            }
            return "";
        }

        public string MechanicsName_Bike()
        {
            int x = rand.Next(1, 5);
            switch (x)
            {
                case 1: return "H7788/1";
                case 2: return "Mountin'22D.2";
                case 3: return "Black";
                case 4: return "X2278";
            }

            return "";
        }
        //генерирование производителей
        public string MechanicsProducer_Computer()
        {
            int x = rand.Next(1, 5);
            switch (x)
            {
                case 1: return "Dell";
                case 2: return "Asus";
                case 3: return "Lenovo";
                case 4: return "Apple";
                case 5: return "HP";
            }

            return "";
        }

        public string MechanicsProducer_Car()
        {
            int x = rand.Next(1, 5);
            switch (x)
            {
                case 1: return "Opel";
                case 2: return "Mazda";
                case 3: return "Toyota";
                case 4: return "BMW";
            }
            return "";
        }

        public string MechanicsProducer_Bike()
        {
            int x = rand.Next(1, 5);
            switch (x)
            {
                case 1: return "Honda";
                case 2: return "Kawasaki";
                case 3: return "Ducati";
                case 4: return "BMW";
            }

            return "";
        }

        public string MechanicsType()
        {
            int type = rand.Next(1, 4);
            switch (type)
            {
                case 1: return "Компьютер";
                case 2: return "Машина";
                case 3: return "Мотоцикл";
            }
            return "";
        }
        #endregion //rdy


        #endregion

        #region FoodGoods
        #region Milky
        public string MilkyProducer()
        {
            int x = rand.Next(1, 5);
            switch (x)
            {
                case 1: return "Юнимилк";
                case 2: return "Danone";
                case 3: return "Магнат";
                case 4: return "Вимм-Билль-Данн";
            }
            return "";
        }

        public string MilkyName()
        {
            int x = rand.Next(1, 5);
            switch (x)
            {
                case 1: return "Простоквашино";
                case 2: return "Веселый молочник";
                case 3: return "Домик в деревне";
                case 4: return "Инмарко";
            }

            return "";
        }

        public string MilkyType()
        {
            int type = rand.Next(1, 5);
            switch (type)
            {
                case 1: return "Молоко";
                case 2: return "Бифидок";
                case 3: return "Кефир";
                case 4: return "Йогурт";
            }

            return "";
        }

        #endregion

        #region Meat
        public string MeatName()
        {
            int x = rand.Next(1, 5);
            switch (x)
            {
                case 1: return "Шницель";
                case 2: return "Котлета";
                case 3: return "Шашлык";
                case 4: return "Стейк";
            }

            return "";
        }

        public string MeatProducer()
        {
            int x = rand.Next(1, 5);
            switch (x)
            {
                case 1: return "ООО Мясокомбинат №1";
                case 2: return "ООО Мясокомбинат №2";
                case 3: return "ООО Мясокомбинат №3";
                case 4: return "OOO Мясокомбинат №4";
            }

            return "";
        }
        #endregion
        #endregion
        #endregion
    }

}

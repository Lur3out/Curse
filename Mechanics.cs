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
    public class Mechanics : IndustrialGoods
    {
        private string mechanicType; //тип мех. изделия
        private Randomize rnd;

        #region builders
        public Mechanics() : base()
        {
            rnd = Network.rand;
            this.quantity = rnd.Next(1, 20);
            this.price = rnd.Next(20000, 2000000);
            this.mechanicType = rnd.MechanicsType();
            if (mechanicType == "Компьютер") { this.name = rnd.MechanicsName_Computer(); this.producer = rnd.MechanicsProducer_Computer(); }
            if (mechanicType == "Машина") { this.name = rnd.MechanicsName_Car(); this.producer = rnd.MechanicsProducer_Car(); }
            if (mechanicType == "Мотоцикл") { this.name = rnd.MechanicsName_Bike(); this.producer = rnd.MechanicsProducer_Bike(); }
        }
        public Mechanics(Randomize rnd) : base(rnd)
        {
            this.rnd = rnd;
            this.quantity = rnd.Next(1, 20);
            this.price = rnd.Next(20000, 2000000);
            this.mechanicType = rnd.MechanicsType();
            if (mechanicType == "Компьютер") { this.name = rnd.MechanicsName_Computer(); this.producer = rnd.MechanicsProducer_Computer(); }
            if (mechanicType == "Машина") { this.name = rnd.MechanicsName_Car(); this.producer = rnd.MechanicsProducer_Car(); }
            if (mechanicType == "Мотоцикл") { this.name = rnd.MechanicsName_Bike(); this.producer = rnd.MechanicsProducer_Bike(); }
        }

        public Mechanics(string name, int quantity, int price, string mechType, string producer) : base(name, quantity, price)
        {
            rnd = new Randomize();
            this.mechanicType = mechType;
            this.producer = producer;
        }
        #endregion

        #region methods
        public override Goods New()
        {
            return new Mechanics() as Goods;
        }

        public override string ToString()
        {
            return base.ToString() + " Тип изделия: " + this.mechanicType + "\n";
        }
        #endregion
    }

}

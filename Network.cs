using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace CourseWork.wpf
{
    [Serializable]
    public class Network : List <Store>
    {

        protected string network_name;
        static public Randomize rand;
        protected int i = 0; //уникальный счетчик для названия магазина

        #region builders
        public Network() : base()
        {
            rand = new Randomize();
  
            network_name = "";
        }

        public Network(string name, int capacity) : base(capacity)
        {
            rand = new Randomize();
            network_name = name;
        }

        public Network(Network obj) : base(obj)
        {
            rand = new Randomize();
        }
        #endregion

        #region methods

        public string Name
        {
            get { return network_name; }
            set { network_name = value; }
        }

        public override string ToString()
        {
            return "\n Название сети: " + this.network_name + "\n";
        }
        #endregion
    }

}

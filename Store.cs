using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace CourseWork.wpf
{
    [Serializable]
    public class Store : List <Goods>
    {
        private string store_name; 
        private Randomize rand;

        #region builders
        public Store() : base ()
        {
            rand = Network.rand;
            store_name = "";
        }

        public Store(int num) : base()
        {
            rand = Network.rand;
            store_name = rand.StoreName(num);
        }

        public Store(string name, int capacity) : base(capacity)
        {
            rand = Network.rand;
            store_name = name;
        }

        public Store(Store obj) : base(obj)
        {
            rand = Network.rand;
            this.Name = obj.Name;
        }
        #endregion


        #region methods

        public string Name
        {
            get { return store_name; }
            set
            {
                store_name = value;
            }
        }
        
        public void Add()
        {
            this.Add(rand.GetNew(rand));
        }
        
        public void Replace(int num)
        {
            this[num - 1] = rand.GetNew(rand);
        }
        
        public void Replace(int num, Goods obj)
        {
            this[num - 1] = obj;
        }

        public void Update(int num, int quantity, int price)
        {
            this[num].Quantity = quantity;
            this[num].Price = price;
        }

        public int Count_Industial()
        {
            int count = 0;
            for (int i = 0; i < this.Count; i++)
                if (this[i].productionType == 2) count++;
            return count;
        }

        public int Count_Food()
        {
            int count = 0;
            for (int i = 0; i < this.Count; i++) 
                if (this[i].productionType == 1) count++;
            return count;
        }

        public override string ToString()
        {
            return "\n Магазин: " + store_name + "\n" + this + base.ToString() + "\n";
        }
        #endregion
    }

}

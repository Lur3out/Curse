using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CourseWork.wpf
{
    [Serializable]
    public class IndustrialGoods : Goods
    {
        // productionType:
        //1 - продовольственный(FoodGoods)
        //2 - производственный(IndustrialGoods)
        //
        //indGoodsType:
        //1 - Одежда
        //2 - Техника


        protected int indGoodsType = 0;

        #region builders

        public IndustrialGoods() : base()
        {
            this.productionType = 2;
        }

        public IndustrialGoods(Randomize rnd) : base(rnd)
        {
            this.productionType = 2;
        }

        public IndustrialGoods(string name, int quantity, int price) : base(name, quantity, price)
        {
            this.productionType = 2;
        }

        #endregion

        #region methods

        public override Goods New()
        {
            return new IndustrialGoods() as Goods;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }

}

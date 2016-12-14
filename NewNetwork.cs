using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace CourseWork.wpf
{
    [Serializable]
    public class NewNetwork : Network
    {

        public delegate void CollectionHandler(object source, CollectionHandlerEventArgs arg);
        public event CollectionHandler NetworkCountWasChanged;
        public event CollectionHandler NetworkReferenceWasChanged;
        public event CollectionHandler StoreCountWasChanged;
        public event CollectionHandler StoreReferenceWasChanged;
        public event CollectionHandler StoreObjectWasUpdated;

        #region builders
        public NewNetwork() : base()
        {

        }

        public NewNetwork(string name, int capacity) : base(name, capacity)
        {

        }

        public NewNetwork(NewNetwork obj) : base(obj)
        {

        }
        #endregion

        #region Network_actions
        public void Add_Store() // добавление магазина в сеть
        {
            Store s = new Store(i++);
            OnNetworkCountWasChanged(this, new CollectionHandlerEventArgs(network_name, "Добавлен", s.Name));
            this.Add(s);
        }

        public void Remove(string store_name) //удаление магазина из сети
        {
            if (this.Count > 0)
            {
                foreach (var s in this)
                    if (s.Name == store_name)
                    {
                        OnNetworkCountWasChanged(this, new CollectionHandlerEventArgs(network_name, "Удален", s.Name));
                        this.Remove(s as Store);
                    }
            }
            else throw new Exception("Попытка удалить из пустой коллекции!");
        }

        public void RemoveAt_Event(int num)
        {
            if (this.Count > 0)
            {
                OnNetworkCountWasChanged(this, new CollectionHandlerEventArgs(network_name, "Удален", this[num - 1].Name));
                this.RemoveAt(num - 1);
            }
            else throw new Exception("Попытка удалить из пустой коллекции!");
        }

        public void RemoveAll()
        {
            this.RemoveAll();
            OnNetworkCountWasChanged(this, new CollectionHandlerEventArgs(this.Name, "Очищена", ""));
        }

        public void Replace(Store s, int num)
        {
            if (this.Count > 0)
            {
                Store old = this[num - 1];
                this[num - 1] = s;
                OnNetworkReferenceWasChanged(this, new CollectionHandlerEventArgs(network_name, "Заменен", old.Name));
            }
            else throw new Exception("Попытка заменить элемент в пустой коллекции!");
        }
        #endregion

        #region Store_actions
        public void Add_inStore(int num)
        {
            if (num > 0 && num < this.Count + 1)
            {
                Goods g = rand.GetNew(rand);
                this[num - 1].Add(g); // network == this
                
                OnStoreCountWasChanged(this, new CollectionHandlerEventArgs(this[num - 1].Name, "Добавлен", g.ToString()));
            }
            else throw new Exception("Индекс магазина находится вне границ коллекции при добавлении товара в магазин.");
        }

        public void Remove(int store_num, int num)
        {
            if (this.Count > 0 && this[store_num - 1].Count > 0)
            {
                Goods g = this[store_num - 1][num - 1];
                this[store_num - 1].RemoveAt(num - 1);
                OnStoreCountWasChanged(this, new CollectionHandlerEventArgs(this[store_num - 1].Name, "Удален", g.ToString()));
            }
            else throw new Exception("Попытка удаления товара из пустой коллекции.");
        }

        public void Replace(int store_num, int num)
        {
            if (this.Count > 0 && this[store_num - 1].Count > 0)
            {
                Goods g = this[store_num - 1][num];
                Goods obj = rand.GetNew(rand);
                this[store_num - 1].Replace(num, obj);
                OnStoreReferenceWasChanged(this, new CollectionHandlerEventArgs(this[store_num - 1].Name, "Заменен", g.ToString()));
            }
            else throw new Exception("Попытка замещения товара из пустой коллекции");
        }

        public void Update(int store_num, int num, int quantity, int price)
        {
            if (this.Count > 0 && this[store_num].Count > 0)
            {
                Goods g = this[store_num][num];
                this[store_num].Update(num, quantity, price);
                OnStoreObjectWasUpdated(this, new CollectionHandlerEventArgs(this[store_num].Name, "Обновлен", g.ToString()));
            }
            else throw new Exception("Попытка обновления товара из пустой коллекции.");
        }
        #endregion

        #region on_methods
        public virtual void OnNetworkCountWasChanged(object source, CollectionHandlerEventArgs arg)
        {
            if (NetworkCountWasChanged != null) NetworkCountWasChanged(source, arg);
        }
        public virtual void OnNetworkReferenceWasChanged(object source, CollectionHandlerEventArgs arg)
        {
            if (NetworkReferenceWasChanged != null) NetworkReferenceWasChanged(source, arg);
        }
        public virtual void OnStoreCountWasChanged(object source, CollectionHandlerEventArgs arg)
        {
            if (StoreCountWasChanged != null) StoreCountWasChanged(source, arg);
        }
        public virtual void OnStoreReferenceWasChanged(object source, CollectionHandlerEventArgs arg)
        {
            if (StoreReferenceWasChanged != null) StoreReferenceWasChanged(source, arg);
        }
        public virtual void OnStoreObjectWasUpdated(object source, CollectionHandlerEventArgs arg)
        {
            if (StoreObjectWasUpdated != null) StoreObjectWasUpdated(source, arg);
        }
        #endregion
    }

}

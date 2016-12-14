using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace CourseWork.wpf
{
    [Serializable]
    public class CollectionHandlerEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public string Object { get; set; }

        public CollectionHandlerEventArgs(string name, string action, string obj)
        {
            Name = name;
            Action = action;
            Object = obj;

        }
    }

}

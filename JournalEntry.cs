using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace CourseWork.wpf
{
    [Serializable]
    public class JournalEntry
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public string Object { get; set; }
        public JournalEntry(string name, string action, string obj)
        {
            Name = name;
            Action = action;
            Object = obj;
        }

        public string CollectionName
        {
            get { return Name; }
            set { Name = value; }
        }

        public override string ToString()
        {
            return "\nНазвание: " + Name + "\n" + "Действие: " + Action + "\nОбъект: " + "\n" + Object;
        }

    }
}

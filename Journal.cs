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
    public class Journal
    {
        private List<JournalEntry> list;

        public Journal()
        {
            list = new List<JournalEntry>();
        }

        public override string ToString()
        {
            string s = "";
            foreach (JournalEntry i in list)
                s += i.ToString() + "\n";
            if (s.Length == 0)
                throw new Exception("Пустой журнал");
            return s;
        }

        public void Save()
        {
            string dir = @"../../Journal";
            string serialFile = Path.Combine(dir, "Journal.txt");
            using (Stream fstream = File.Open(serialFile, FileMode.Create))
            {
                var form = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                form.Serialize(fstream, list);
            }

        }

        #region Methods
        public void NetworkCountWasChanged(object source, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.Name, e.Action, e.Object);
            list.Add(je);
        }

        public void NetworkReferenceWasChanged(object source, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.Name, e.Action, e.Object.ToString());
            list.Add(je);
        }
        public void StoreCountWasChanged(object source, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.Name, e.Action, e.Object.ToString());
            list.Add(je);
        }
        public void StoreReferenceWasChanged(object source, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.Name, e.Action, e.Object.ToString());
            list.Add(je);
        }
        public void StoreObjectWasUpdated(object source, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.Name, e.Action, e.Object.ToString());
            list.Add(je);
        }
        #endregion
    }

}

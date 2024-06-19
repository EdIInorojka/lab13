using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba133
{
    public class Journal
    {
        private List<JournalEntry> entries;

        public Journal()
        {
            entries = new List<JournalEntry>();
        }

        public void HandleCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            entries.Add(new JournalEntry("MyObservableCollection", "CollectionCountChanged", args.Action + ": " + args.Item.ToString()));
        }

        public void HandleCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            entries.Add(new JournalEntry("MyObservableCollection", "CollectionReferenceChanged", args.Action + ": " + args.Item.ToString()));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var entry in entries)
            {
                sb.AppendLine(entry.ToString());
            }
            return sb.ToString();
        }
    }
}

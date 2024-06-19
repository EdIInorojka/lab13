using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba133
{
    public class JournalEntry
    {
        public string CollectionName { get; }
        public string ActionType { get; }
        public string ItemData { get; }

        public JournalEntry(string collectionName, string actionType, string itemData)
        {
            CollectionName = collectionName;
            ActionType = actionType;
            ItemData = itemData;
        }

        public override string ToString()
        {
            return $"Коллекция: {CollectionName}, Действие: {ActionType}, Данные: {ItemData}";
        }
    }

}

using Core.Data;

using SaveSystem;

using System;
using System.Collections.Generic;

namespace Core.Inventory
{
    [Serializable]
    public class Storage
    {
        private static Storage _instance;
        public static Storage Instance => _instance ??= new Storage();
        public static void Clear() => _instance = null;

        public Action OnInventoryChanged;

        private int _gold;
        public int Gold => _gold;

        public readonly Dictionary<Item, int> Items;

        public Storage()
        {
            Items = SimpleSaver.Items;
            _gold = SimpleSaver.Gold;
        }

        public void AddItems(params Item[] items) => AddItems(ConvertToDict(items));
        public void AddItems(Dictionary<Item, int> items)
        {
            foreach (var item in items)
            {
                if (Items.ContainsKey(item.Key))
                    Items[item.Key] += item.Value;
                else
                    Items[item.Key] = item.Value;
            }

            OnInventoryChanged?.Invoke();
            SimpleSaver.Items = Items;
        }

        public bool TryToRemoveItems(params Item[] items) => TryToRemoveItems(ConvertToDict(items));
        public bool TryToRemoveItems(Dictionary<Item, int> items) 
        {
            foreach (var item in items)
            {
                if (!Items.ContainsKey(item.Key))
                    return false;

                if (Items[item.Key] < item.Value)
                    return false;
            }

            foreach (var item in items)
                Items[item.Key] -= item.Value;

            OnInventoryChanged?.Invoke();
            SimpleSaver.Items = Items;
            return true;
        }

        public bool TrySell(Item item)
        {
            if (!TryToRemoveItems(item))
                return false;

            _gold += item.Cost;
            OnInventoryChanged.Invoke();
            SimpleSaver.Gold = _gold;
            return true;
        }

        private static Dictionary<Item, int> ConvertToDict(params Item[] items)
        {
            var itemsDict = new Dictionary<Item, int>();
            if (items == null)
                return itemsDict;

            foreach (var item in items)
            {
                if (itemsDict.ContainsKey(item))
                    itemsDict[item] += 1;
                else
                    itemsDict[item] = 1;
            }
            return itemsDict;
        }
    }
}
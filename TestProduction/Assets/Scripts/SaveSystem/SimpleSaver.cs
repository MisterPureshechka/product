using Core.Data;
using Core.Inventory;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace SaveSystem
{
    public static class SimpleSaver
    {
        private const string _productBuildsName = "productBuilds";
        public static int ProductBuilds
        {
            get => PlayerPrefs.GetInt(_productBuildsName, 3);
            set => PlayerPrefs.SetInt(_productBuildsName, value);
        }

        public static bool AnySave => PlayerPrefs.HasKey(_productBuildsName);

        private const string _goldName = "gold";
        public static int Gold
        {
            get => PlayerPrefs.GetInt(_goldName, 0);
            set => PlayerPrefs.SetInt(_goldName, value);
        }

        //проще как-то через NewtonSoft сериализовать, но, это стороннее творчество
        private const string _itemsName = "items";
        public static Dictionary<Item, int> Items
        {
            get 
            {
                var result = new Dictionary<Item, int>();
                var rawItemsData = PlayerPrefs.GetString(_itemsName, "").Split("|");
                foreach (var rawItem in rawItemsData)
                {
                    var itemsTupple = rawItem.Split("/");
                    if (itemsTupple.Length == 2)
                        result.Add(Item.LoadItem(itemsTupple[0]), int.Parse(itemsTupple[1]));
                }
                return result;
            }
            set
            {
                var currentItems = Storage.Instance.Items;
                var stringItems = new StringBuilder();
                stringItems.AppendJoin('|', currentItems.Select(i => $"{i.Key.Name}/{i.Value}"));
                PlayerPrefs.SetString(_itemsName, stringItems.ToString());
                Debug.Log(stringItems.ToString());
            }
        }

        public static void ClearAll() => PlayerPrefs.DeleteAll();
    }
}


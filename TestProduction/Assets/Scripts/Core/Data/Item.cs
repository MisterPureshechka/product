using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
    public class Item: ScriptableObject
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private Sprite _sprite;
        [SerializeField]
        private int _cost;

        public static Item LoadItem(string itemPath) => Resources.Load<Item>(itemPath);

        public string Name => _name;
        public Sprite Sprite => _sprite;
        public int Cost => _cost;
    }
}
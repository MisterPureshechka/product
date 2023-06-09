using UnityEngine;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "ScriptableObjects/Recipe", order = 2)]
    public class Recipe: ScriptableObject
    {
        [SerializeField]
        private float _createTime;
        [SerializeField]
        private Item[] _cost;
        [SerializeField]
        private Item[] _reward;

        public float CreateTime => _createTime;
        public Item[] Cost => _cost;
        public Item[] Reward => _reward;
    }
}

using Core.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UI.ProductButtonView;

namespace UI
{
    public class ItemButtonView : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Image _image;

        private Item[] _items;
        private int _targetItemIndex;
        private readonly List<Sprite> _sprites = new();

        public Sprite TargetSprite => _sprites[_targetItemIndex];

        private void Awake()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(UpdateItemIndex);
        }

        public void Init(Item[] items)
        {
            _items = items;

            _sprites.Clear();
            _sprites.Add(null);
            _sprites.AddRange(_items.Select(i => i.Sprite));

            UpdateSprite();
        }

        private void UpdateItemIndex()
        {
            _targetItemIndex++;
            if (_targetItemIndex >= _sprites.Count)
                _targetItemIndex = 0;

            UpdateSprite();
        }

        private void UpdateSprite()
        {
            _image.sprite = TargetSprite;
        }
    }
}


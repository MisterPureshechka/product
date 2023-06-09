using Core.Data;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProductButtonView : MonoBehaviour
    {
        public enum ButtonType
        {
            Rewards,
            Costs
        }

        [SerializeField]
        private Button _button;
        [SerializeField]
        private Image _image;

        private ButtonType _buttonType;

        private Recipe[] _recipes;

        private int _targetRecipeIndex;

        private readonly List<Sprite> _sprites = new();

        public bool Interactable => _button.interactable;
        public Sprite TargetSprite => _sprites[_targetRecipeIndex];

        private void Awake()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(UpdateRecipeIndex);
        }

        public void Init(Recipe[] recipes, ButtonType buttonType)
        {
            _targetRecipeIndex = 0;

            _recipes = recipes;

            _buttonType = buttonType;

            _sprites.Clear();
            _sprites.Add(null);

            Sprite[] sprites = new Sprite[0];
            bool interactable = false;
            switch (_buttonType)
            {
                case ButtonType.Rewards:
                    sprites = _recipes.SelectMany(rec => rec.Reward.Select(rew => rew.Sprite)).ToArray();
                    interactable = _recipes != null && _recipes.All(r => r.Cost.Length == 0);
                    break;
                case ButtonType.Costs:
                    sprites = _recipes.SelectMany(rec => rec.Cost.Select(c => c.Sprite)).ToArray();
                    interactable = _recipes != null && _recipes.Any(r => r.Cost.Length > 0);
                    break;
            }
            _sprites.AddRange(sprites.Distinct());
            _button.interactable = interactable;

            UpdateSprite();
        }

        public void SetRecipe(Recipe targetRecipe)
        {
            _targetRecipeIndex = targetRecipe != null
                ? _sprites.IndexOf(_sprites.FirstOrDefault(s => s == targetRecipe.Reward[0].Sprite))
                : 0;
            UpdateSprite();
        }

        private void UpdateRecipeIndex()
        {
            _targetRecipeIndex++;
            if (_targetRecipeIndex >= _sprites.Count)
                _targetRecipeIndex = 0;

            UpdateSprite();
        }

        private void UpdateSprite()
        {
            _image.sprite = TargetSprite;
        }
    }
}

    

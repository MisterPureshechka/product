using Core.Data;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Views;

namespace UI
{
    public class PlantViewUI : BaseScreenView
    {
        private const string _startLabel = "start";
        private const string _stopLabel = "stop";

        [SerializeField]
        private ProductButtonView _firstCostButton;
        [SerializeField]
        private ProductButtonView _secondCostButton;
        [SerializeField]
        private ProductButtonView _rewardButton;

        [SerializeField]
        private Button _startStopButton;
        private Text _startStopLabel;

        private PlantView _targetPlantView;        

        protected override void Awake()
        {
            base.Awake();

            _startStopLabel = _startStopButton.GetComponentInChildren<Text>();

            _startStopButton.onClick.RemoveAllListeners();
            _startStopButton.onClick.AddListener(SetProduction);

            gameObject.SetActive(false);
        }

        public static void Open(PlantView plantView)
        {
            var screen = _instances.FirstOrDefault(i => i is PlantViewUI);
            if (screen == null)
                return;

            ((PlantViewUI)screen).Init(plantView);
            screen.Show();
        }

        public void Init(PlantView plantView)
        {
            _targetPlantView = plantView;
            var recipes = _targetPlantView.Recipes;

            _firstCostButton.Init(recipes, ProductButtonView.ButtonType.Costs);
            _secondCostButton.Init(recipes, ProductButtonView.ButtonType.Costs);
            _rewardButton.Init(recipes, ProductButtonView.ButtonType.Rewards);
        }

        //стоит сделать некий реактивный контроллер для отлова моментов для обновления, но решил сэкономить время
        private void Update()
        {
            _startStopLabel.text = _targetPlantView.CreatingInProcess ? _stopLabel : _startLabel;
            TryUpdateRewardIndex();
        }

        private void SetProduction()
        {
            var targetRecipe = !_targetPlantView.CreatingInProcess 
                ? _targetPlantView.Recipes
                    .FirstOrDefault(rec => rec.Reward
                        .Any(rew => rew.Sprite == _rewardButton.TargetSprite))
                : null;

            _targetPlantView.SetProduction(targetRecipe);
        }

        private void TryUpdateRewardIndex()
        {
            if (_rewardButton.Interactable)
                return;

            Recipe targetRecipe = null;
            if (_firstCostButton.TargetSprite != null && _secondCostButton.TargetSprite != null)
            {
                var currentCost = new[] { _firstCostButton.TargetSprite, _secondCostButton.TargetSprite };
                targetRecipe = _targetPlantView.Recipes
                    .FirstOrDefault(rec => rec.Cost.Select(c => c.Sprite).OrderBy(a => a.name)
                        .SequenceEqual(currentCost.OrderBy(a => a.name)));
            }

            _rewardButton.SetRecipe(targetRecipe);
        }
    }
}
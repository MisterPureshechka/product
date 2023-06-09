using Core.Data;
using Core.Inventory;

using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using Views;

namespace UI
{
    public class MarketViewUI : BaseScreenView
    {
        [SerializeField]
        private ItemButtonView _selectButton;
        [SerializeField]
        private Button _sellButton;
        [SerializeField]
        private Text _goldLabel;

        private Item _targetItem;
        private MarketView _marketView;

        public static void Open(MarketView marketView)
        {
            var screen = _instances.FirstOrDefault(i => i is MarketViewUI);
            if (screen == null)
                return;

            ((MarketViewUI)screen).Init(marketView);
            screen.Show();
        }

        public override void Show()
        {
            base.Show();
            Storage.Instance.OnInventoryChanged += UpdateItems;
        }

        public override void Hide()
        {
            base.Hide();
            Storage.Instance.OnInventoryChanged -= UpdateItems;
        }

        public void Init(MarketView marketView)
        {
            _marketView = marketView;
            UpdateItems();
        }

        private void UpdateItems()
        {
            _selectButton.Init(Storage.Instance.Items.Keys.ToArray());
        }

        //стоит сделать некий реактивный контроллер для отлова моментов для обновления, но решил сэкономить время
        private void Update()
        {
            _targetItem = Storage.Instance.Items.Keys.FirstOrDefault(i => i.Sprite == _selectButton.TargetSprite); 
            _goldLabel.text = _targetItem != null ? _targetItem.Cost.ToString() : "-";
        }

        protected override void Awake()
        {
            base.Awake();

            _sellButton.onClick.RemoveAllListeners();
            _sellButton.onClick.AddListener(Sell);

            gameObject.SetActive(false);
        }

        private void Sell()
        {
            _marketView.Sell(_targetItem);
        }
    }
}
    

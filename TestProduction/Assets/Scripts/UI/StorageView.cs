using Core.Inventory;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StorageView : MonoBehaviour
    {
        [SerializeField]
        private Text _goldArea;
        [SerializeField]
        private StorageUnitView _defaultStorageUnitView;

        private readonly Queue<StorageUnitView> _unitViews = new();

        private void OnEnable()
        {
            Storage.Instance.OnInventoryChanged += UpdateData;
            UpdateData();
        }

        private void OnDisable()
        {
            Storage.Instance.OnInventoryChanged -= UpdateData;
        }

        private void UpdateData()
        {
            _goldArea.text = Storage.Instance.Gold.ToString();

            while (_unitViews.Count < Storage.Instance.Items.Count)
                _unitViews.Enqueue(Instantiate(_defaultStorageUnitView, _defaultStorageUnitView.transform.parent));

            foreach (var view in _unitViews)
                view.gameObject.SetActive(false);

            foreach (var item in Storage.Instance.Items)
            {
                var view = _unitViews.Dequeue();
                view.UpdateData(item.Key.Sprite, item.Value);
                view.gameObject.SetActive(true);
                _unitViews.Enqueue(view);
            }
        }
    }
}
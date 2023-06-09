using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StorageUnitView : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private Text _countTextArea;

        public void UpdateData(Sprite sprite, int count)
        {
            _image.sprite = sprite;
            UpdateData(count);
        }

        public void UpdateData(int count) => _countTextArea.text = count.ToString();
    }
}


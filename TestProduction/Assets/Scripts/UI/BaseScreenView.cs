using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class BaseScreenView : MonoBehaviour
    {
        protected static List<BaseScreenView> _instances = new();
        public static bool IsOpened => _instances.Any(i => i.gameObject.activeSelf);

        [SerializeField]
        private Button _hideButton;

        protected virtual void Awake()
        {
            _instances.Add(this);

            if (_hideButton)
            {
                _hideButton.onClick.RemoveAllListeners();
                _hideButton.onClick.AddListener(Hide);
            }
        }

        protected virtual void OnDestroy()
        {
            _instances.Remove(this);
        }

        public virtual void Hide() => gameObject.SetActive(false);

        public virtual void Show() => gameObject.SetActive(true);
    }
}
   
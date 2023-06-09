using SaveSystem;
using Scenes;

using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WinScreen : BaseScreenView
    {
        [SerializeField]
        private Button _okButton;

        protected override void Awake()
        {
            base.Awake();

            _okButton.onClick.RemoveAllListeners();
            _okButton.onClick.AddListener(() => Loader.Load(0));

            gameObject.SetActive(false);
        }

        public static void Open()
        {
            foreach (var screenInst in _instances)
                screenInst.Hide();

            var screen = _instances.FirstOrDefault(i => i is WinScreen);
            if (screen == null)
                return;

            SimpleSaver.ClearAll();
            screen.Show();
        }
    }
}
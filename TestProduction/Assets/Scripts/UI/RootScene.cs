using Core.Inventory;
using SaveSystem;

using Scenes;

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RootScene : MonoBehaviour
    {
        [SerializeField]
        private Button _build1;
        [SerializeField]
        private Button _build2;
        [SerializeField]
        private Button _build3;
        [SerializeField]
        private Button _continue;

        private void Awake()
        {
            Storage.Clear();

            _build1.onClick.RemoveAllListeners();
            _build1.onClick.AddListener(() =>
            {
                SimpleSaver.ClearAll();
                SimpleSaver.ProductBuilds = 1;
                Loader.Load(1);
            });

            _build2.onClick.RemoveAllListeners();
            _build2.onClick.AddListener(() =>
            {
                SimpleSaver.ClearAll();
                SimpleSaver.ProductBuilds = 2;
                Loader.Load(1);
            });

            _build3.onClick.RemoveAllListeners();
            _build3.onClick.AddListener(() =>
            {
                SimpleSaver.ClearAll();
                SimpleSaver.ProductBuilds = 3;
                Loader.Load(1);
            });

            _continue.onClick.RemoveAllListeners();
            _continue.onClick.AddListener(() =>
            {
                Loader.Load(1);
            });

            _continue.gameObject.SetActive(SimpleSaver.AnySave);
        }
    }
}
using Core.Inventory;
using SaveSystem;
using UI;

using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int _targetGold = 15;
        [SerializeField]
        private string _startBuildTag;

        private void OnEnable()
        {
            Storage.Instance.OnInventoryChanged += CheckWin;

            var firstBuildsCount = SimpleSaver.ProductBuilds;
            var firstBuilds = GameObject.FindGameObjectsWithTag(_startBuildTag);
            for (var i = 0; i < firstBuilds.Length; i++)
                firstBuilds[i].SetActive(firstBuildsCount > i);
        }

        private void OnDisable() 
        {
            Storage.Instance.OnInventoryChanged -= CheckWin;
        }

        private void CheckWin()
        {
            if (Storage.Instance.Gold < _targetGold)
                return;

            WinScreen.Open();
        }
    }
}
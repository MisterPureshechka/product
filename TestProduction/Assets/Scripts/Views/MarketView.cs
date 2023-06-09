using Core.Data;
using Core.Inventory;

using UI;

using UnityEngine;

namespace Views
{
    public class MarketView : MonoBehaviour
    {
        //����� �������� ��� ������ ������� �����������, ������� �������� �� ����������, ��, �������, ��� �� ��� �� � �����
        private void Update()
        {
            if (!Input.GetMouseButtonUp(0) || BaseScreenView.IsOpened)
                return;

            if (Physics.Raycast(Camera.allCameras[0].ScreenPointToRay(Input.mousePosition), out var hit))
            {
                if (hit.transform != transform)
                    return;

                MarketViewUI.Open(this);
            }
        }

        public void Sell(Item item)
        {
            if (item == null)
                return;

            Storage.Instance.TrySell(item);
        }
    }
}
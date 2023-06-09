using Core.Data;
using Core.Plants;

using UI;

using UnityEngine;

namespace Views
{
    public class PlantView : MonoBehaviour
    {
        [SerializeField]
        private Recipe[] _recipes;

        private Plant _plant;

        public Recipe[] Recipes => _recipes;
        public bool CreatingInProcess => _plant.CreatingInProcess;

        private void Awake()
        {
            _plant = new Plant();
        }

        //стоит передать эту логику некоему контроллеру, который отвечает за управление, но, кажется, это не так уж и важно
        private void Update()
        {
            if (!Input.GetMouseButtonUp(0) || BaseScreenView.IsOpened)
                return;

            if (Physics.Raycast(Camera.allCameras[0].ScreenPointToRay(Input.mousePosition), out var hit))
            {
                if (hit.transform != transform)
                    return;

                PlantViewUI.Open(this);
            }
        }

        public void SetProduction(Recipe recipe)
        {
            _plant.SetRecipe(recipe);
        }
    }
}
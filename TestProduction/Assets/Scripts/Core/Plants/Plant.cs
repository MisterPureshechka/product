using Core.Data;
using Core.Inventory;

using System.Threading.Tasks;

using UnityEngine;

namespace Core.Plants
{
    public class Plant
    {
        public bool CreatingInProcess => _creating != null;

        private Recipe _targetRecipe;
        private Task _creating;

        public async void SetRecipe(Recipe recipe)
        {
            _targetRecipe = recipe;
            if (_creating == null)
            {
                _creating = TryCreating();
                await _creating;
                _creating = null;
            }  
        }

        private async Task TryCreating()
        {
            while (_targetRecipe != null)
            {
                var items = await TryCreate(_targetRecipe);
                if (items == null)
                    break;

                Storage.Instance.AddItems(items);
            }
        }

        private static async Task<Item[]> TryCreate(Recipe recipe)
        {
            if (!Storage.Instance.TryToRemoveItems(recipe.Cost))
                return null;

            const int millieSecondsConvertValue = 1000;
            await Task.Delay(Mathf.RoundToInt(recipe.CreateTime * millieSecondsConvertValue));
            return recipe.Reward;
        }
    }
}


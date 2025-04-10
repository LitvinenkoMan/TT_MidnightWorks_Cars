using System;
using Systems.Inventory;
using UnityEngine;

namespace UI
{
    public class GameplayPresenter : MonoBehaviour
    {
        // Model
        [SerializeField] private InventorySystem inventory;
        
        // View
        [SerializeField] private InventoryView inventoryUI;

        private void OnEnable()
        {
            inventory.OnPartAdded += UpdateInventoryUI;
        }

        private void OnDisable()
        {
            inventory.OnPartAdded -= UpdateInventoryUI;
        }

        public void UpdateInventoryUI()
        {
            inventoryUI.RefreshUI(inventory);
        }
    }
}

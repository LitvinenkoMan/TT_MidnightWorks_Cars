using System.Collections.Generic;
using Data;
using Systems.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject partItemPrefab;
        public Transform partGridContent;
        public GridLayoutGroup Grid;

        [Header("Data")]
        public PartSpriteDatabase spriteDatabase; // для иконок

        private List<GameObject> spawnedItems = new();
        private List<PartItemUI> PartsItemsUI = new();

        public void RefreshUI(InventorySystem inventory)
        {
            foreach (var item in spawnedItems)
                Destroy(item);
            spawnedItems.Clear();
            PartsItemsUI.Clear();
            
            foreach (var part in inventory.ownedParts)
            {
                bool partItemUIExist = false;
                foreach (var partItemUI in PartsItemsUI)
                {
                    if (part.PartType == partItemUI.PartType && part.Level == partItemUI.PartLevel)
                    {
                        partItemUI.SetItemsAmount(partItemUI.ItemsAmount + 1);
                        partItemUIExist = true;
                        break;
                    }
                }

                if (partItemUIExist == false)
                {
                    GameObject go = Instantiate(partItemPrefab, partGridContent);
                    PartItemUI ui = go.GetComponent<PartItemUI>();
                    Sprite sprite = spriteDatabase.GetSpriteForPart(part.PartType); // по типу
                    ui.Setup(part, sprite);
                    PartsItemsUI.Add(ui);
                    spawnedItems.Add(go);
                }
            }
        }
    }
}

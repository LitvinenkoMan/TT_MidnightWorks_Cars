using System.Collections.Generic;
using Data;
using Systems.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryView : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject partItemPrefab;
        public Transform partGridContent;
        public GridLayoutGroup Grid;

        [Header("Data")]
        public PartSpriteDatabase spriteDatabase; // для иконок

        private List<GameObject> spawnedItems = new();
        private List<PartItemUI> PartsInUi = new();

        public void RefreshUI(InventorySystem inventory)
        {
            foreach (var item in spawnedItems)
                Destroy(item);
            spawnedItems.Clear();
            PartsInUi.Clear();
            
            foreach (var part in inventory.ownedParts)
            {
                bool partUIItemExist = false;
                foreach (var partUI in PartsInUi)
                {
                    if (part.PartType == partUI.PartType && part.Level == partUI.PartLevel)
                    {
                        partUI.SetItemsAmount(partUI.ItemsAmount + 1);
                        partUIItemExist = true;
                        break;
                    }
                }

                if (partUIItemExist == false)
                {
                    GameObject go = Instantiate(partItemPrefab, partGridContent);
                    PartItemUI ui = go.GetComponent<PartItemUI>();
                    Sprite sprite = spriteDatabase.GetSpriteForPart(part.PartType); // по типу
                    ui.Setup(part, sprite);
                    PartsInUi.Add(ui);
                    spawnedItems.Add(go);
                }
            }
        }
    }
}

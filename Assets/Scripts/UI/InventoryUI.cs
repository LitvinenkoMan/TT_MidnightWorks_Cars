using System.Collections.Generic;
using Core;
using Data;
using Systems.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("UI References")] 
        [SerializeField] private GameObject partItemPrefab;
        [SerializeField] private RectTransform partGridContent;
        [SerializeField] private GridLayoutGroup grid;

        [Header("Data")]
        public PartSpriteDatabase spriteDatabase; // для иконок

        private List<GameObject> _spawnedItems = new();
        private List<PartItemUI> _partsItemsUI = new();

        public void RefreshUI(InventorySystem inventory)
        {
            foreach (var item in _spawnedItems)
                Destroy(item);
            _spawnedItems.Clear();
            _partsItemsUI.Clear();
            
            foreach (var part in inventory.ownedParts)
            {
                bool partItemUIExist = false;
                foreach (var partItemUI in _partsItemsUI)
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
                    _partsItemsUI.Add(ui);
                    _spawnedItems.Add(go);
                }
            }
            StretchContentPanel();
        }

        private void StretchContentPanel()
        {
            partGridContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, grid.preferredHeight);
        }
    }
}

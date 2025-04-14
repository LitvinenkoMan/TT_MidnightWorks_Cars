using System;
using System.Collections.Generic;
using Core;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MarketUI : MonoBehaviour
    {
        [SerializeField] private GameObject sellingItemPanelPrefab;
        [SerializeField] private RectTransform partListParent;
        [SerializeField] private GridLayoutGroup grid;
        
        [Header("Data")]
        public PartSpriteDatabase spriteDatabase; // для иконок

        private List<SellingItemPanelUI> _sellingItemsUI = new();
        private List<GameObject> _spawnedItems = new();

        private void OnEnable()
        {
            GameManager.Instance.InventorySystem.OnInventoryUpdated += RenderSellingItems;
            
            RenderSellingItems();
        }

        private void OnDisable()
        {
            GameManager.Instance.InventorySystem.OnInventoryUpdated -= RenderSellingItems;
        }

        private void Start()
        {
            RenderSellingItems();
        }

        public void RenderSellingItems()
        {
            foreach (var item in _spawnedItems)
                Destroy(item);
            _spawnedItems.Clear();
            _sellingItemsUI.Clear();
            
            foreach (var part in GameManager.Instance.InventorySystem.ownedParts)
            {
                bool sellingItemUIExist = false;
                foreach (var sellingItemUI in _sellingItemsUI)
                {
                    if (part.PartType == sellingItemUI.Part.PartType && part.Level == sellingItemUI.Part.Level)
                    {
                        sellingItemUI.SetItemsAmount(sellingItemUI.ItemsAmount + 1);
                        sellingItemUIExist = true;
                        break;
                    }
                }

                if (sellingItemUIExist == false)
                {
                    GameObject go = Instantiate(sellingItemPanelPrefab, partListParent);
                    SellingItemPanelUI ui = go.GetComponent<SellingItemPanelUI>();
                    Sprite sprite = spriteDatabase.GetSpriteForPart(part.PartType); // по типу
                    ui.Setup(part, sprite);
                    _sellingItemsUI.Add(ui);
                    _spawnedItems.Add(go);
                }
            }
            partListParent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, grid.preferredHeight);
        }
    }
}

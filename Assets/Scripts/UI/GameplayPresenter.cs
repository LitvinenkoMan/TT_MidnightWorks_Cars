using System;
using Core;
using Data;
using Systems.CarAssemble;
using Systems.Economy;
using Systems.Inventory;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameplayPresenter : MonoBehaviour
    {
        [Space(10), Header("Logic Scripts")]
        [SerializeField] private InventorySystem inventory;
        [SerializeField] private ScrapMiner scrapMiner;
        
        [Space(10), Header("UI Scripts")]
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private CreationToolUI creationToolUI;
        [SerializeField] private ScrapMinerUI scrapMinerUI;

        [Space(15), Header("Data")] 
        [SerializeField] private PartSpriteDatabase spriteDatabase;

        private void OnEnable()
        {
            inventory.OnPartAdded += UpdateInventoryUI;
            inventory.OnPlayerEntered += ShowInventoryWindow;
            inventory.OnPlayerLeave += HideInventoryWindow;
            
            scrapMiner.OnPlayerEntered += ShowScrapsMinerWindow;
            scrapMiner.OnPlayerLeave += HideScrapsMinerWindow;
            scrapMiner.OnEarnScrapsAmountChanged += OnScrapsEarnAmountChangedResponse;
        }

        private void OnDisable()
        {
            inventory.OnPartAdded -= UpdateInventoryUI;
            inventory.OnPlayerEntered -= ShowInventoryWindow;
            inventory.OnPlayerLeave -= HideInventoryWindow;
            
            scrapMiner.OnPlayerEntered -= ShowScrapsMinerWindow;
            scrapMiner.OnPlayerLeave -= HideScrapsMinerWindow;
            scrapMiner.OnEarnScrapsAmountChanged -= OnScrapsEarnAmountChangedResponse;
        }

        public void ShowCreationToolWindow(PartCreationTool partCreationTool)
        {
            Sprite sprite = spriteDatabase.GetSpriteForPart(partCreationTool.PartTypeCreation);
            creationToolUI.Setup(partCreationTool, sprite);
            creationToolUI.gameObject.SetActive(true);
        }

        public void HideCreationTollWindow()
        {
            creationToolUI.gameObject.SetActive(false);
        }

        public void EarnScraps()
        {
            scrapMiner.EarnScraps();
        }

        private void UpdateInventoryUI()
        {
            inventoryUI.RefreshUI(inventory);
        }

        private void ShowInventoryWindow()
        {
            inventoryUI.gameObject.SetActive(true);
        }

        private void HideInventoryWindow()
        {
            inventoryUI.gameObject.SetActive(false);
        }

        private void ShowScrapsMinerWindow()
        {
            scrapMinerUI.gameObject.SetActive(true);
        }

        private void HideScrapsMinerWindow()
        {
            scrapMinerUI.gameObject.SetActive(false);
        }

        private void OnScrapsEarnAmountChangedResponse(int earnAmount)
        {
            scrapMinerUI.UpdateEarningAmount(earnAmount);
        }
    }
}

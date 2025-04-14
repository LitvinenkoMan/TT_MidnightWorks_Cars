using Assets.Scripts.Common;
using Data;
using Systems.CarAssemble;
using Systems.Economy;
using Systems.Inventory;
using UnityEngine;

namespace UI
{
    public class GameplayPresenter : MonoBehaviour
    {
        [Space(10), Header("Logic Scripts")]
        [SerializeField] private InventorySystem inventory;
        [SerializeField] private ScrapMiner scrapMiner;
        [SerializeField] private SellingTable sellingTable;
        [SerializeField] private CarAssembler carAssembler;
        
        [Space(10), Header("UI Scripts")]
        [SerializeField] private InventoryUI inventoryUI;
        [SerializeField] private CreationToolUI creationToolUI;
        [SerializeField] private ScrapMinerUI scrapMinerUI;
        [SerializeField] private MarketUI marketUI;
        [SerializeField] private FollowWorldTargetUI browserButton;
        [SerializeField] private FollowWorldTargetUI carAssemblerUI;

        [Space(15), Header("Data")] 
        [SerializeField] private PartSpriteDatabase spriteDatabase;

        private void OnEnable()
        {
            inventory.OnInventoryUpdated += UpdateInventoryUI;
            inventory.OnPlayerEntered += ShowInventoryWindow;
            inventory.OnPlayerLeave += HideInventoryWindow;

            sellingTable.OnPlayerEntered += ShowBrowser;
            sellingTable.OnPlayerLeave += HideBrowser;

            carAssembler.OnPlayerEntered += ShowAssembler;
            carAssembler.OnPlayerLeave += HideAssembler;
            
            scrapMiner.OnPlayerEntered += ShowScrapsMinerWindow;
            scrapMiner.OnPlayerLeave += HideScrapsMinerWindow;
            scrapMiner.OnEarnScrapsAmountChanged += OnScrapsEarnAmountChangedResponse;
        }

        private void OnDisable()
        {
            inventory.OnInventoryUpdated -= UpdateInventoryUI;
            inventory.OnPlayerEntered -= ShowInventoryWindow;
            inventory.OnPlayerLeave -= HideInventoryWindow;

            sellingTable.OnPlayerEntered -= ShowBrowser;
            sellingTable.OnPlayerLeave -= HideBrowser;

            carAssembler.OnPlayerEntered -= ShowAssembler;
            carAssembler.OnPlayerLeave -= HideAssembler;
            
            scrapMiner.OnPlayerEntered -= ShowScrapsMinerWindow;
            scrapMiner.OnPlayerLeave -= HideScrapsMinerWindow;
            scrapMiner.OnEarnScrapsAmountChanged -= OnScrapsEarnAmountChangedResponse;
        }

        public void ShowCreationToolWindow(PartCreationTool partCreationTool)
        {
            Sprite sprite = spriteDatabase.GetSpriteForPart(partCreationTool.PartTypeCreation);
            creationToolUI.Setup(partCreationTool, sprite);
            creationToolUI.UpdateTextUI();
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

        public void ShowMarket()
        {
            marketUI.SetActive(true);
            marketUI.RenderSellingItems();
        }

        public void AssembleCar(int level)
        {
            carAssembler.TryAssembleCar(level);
        }

        private void ShowAssembler()
        {
            carAssemblerUI.SetActive(true);
        }

        private void HideAssembler()
        {
            carAssemblerUI.SetActive(false);
        }

        private void ShowBrowser()
        {
            browserButton.SetActive(true);
        }

        private void HideBrowser()
        {
            browserButton.SetActive(false);
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

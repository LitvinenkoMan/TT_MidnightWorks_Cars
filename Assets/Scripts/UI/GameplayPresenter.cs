using Data;
using Systems.CarAssemble;
using Systems.Inventory;
using UnityEngine;

namespace UI
{
    public class GameplayPresenter : MonoBehaviour
    {
        [Space(10), Header("Logic Scripts")]
        [SerializeField] private InventorySystem inventory;
        
        [Space(15), Header("UI Scripts")]
        [SerializeField] private InventoryView inventoryUI;
        [SerializeField] private CreationToolUI creationToolUI;

        [Space(15), Header("Data")] 
        [SerializeField] private PartSpriteDatabase spriteDatabase;

            private void OnEnable()
        {
            inventory.OnPartAdded += UpdateInventoryUI;
            inventory.OnPlayerEntered += ShowInventoryWindow;
            inventory.OnPlayerLeave += HideInventoryWindow;
        }

        private void OnDisable()
        {
            inventory.OnPartAdded -= UpdateInventoryUI;
            inventory.OnPlayerEntered -= ShowInventoryWindow;
            inventory.OnPlayerLeave -= HideInventoryWindow;
        }

        public void UpdateInventoryUI()
        {
            inventoryUI.RefreshUI(inventory);
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

        public void ShowInventoryWindow()
        {
            inventoryUI.gameObject.SetActive(true);
        }

        public void HideInventoryWindow()
        {
            inventoryUI.gameObject.SetActive(false);
        }
    }
}

using Core;
using Systems.CarAssemble;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SellingItemPanelUI : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private TMP_Text itemLevel;
        [SerializeField] private TMP_Text itemAmount;
        [SerializeField] private TMP_Text itemPriceByOne;
        [SerializeField] private TMP_Text itemPriceAll;
        [SerializeField] private Button sellOneButton;
        [SerializeField] private Button sellAllButton;

        public CarPart Part { get; private set; }
        public int ItemsAmount { get; private set; }

        public void Setup(CarPart part, Sprite sprite)
        {
            Part = part;
            itemIcon.sprite = sprite;
            itemLevel.text = $"Lv. {part.Level}";
            itemAmount.text = $"x1";
            itemPriceByOne.text = $"+{part.SellPrice}";
            ItemsAmount = 1;
            itemPriceAll.text = $"+{ItemsAmount * Part.SellPrice}";
            
            sellOneButton.onClick.AddListener(SellOneItem);
            sellAllButton.onClick.AddListener(SellAllItems);
        }

        public void SetItemsAmount(int amount)
        {
            ItemsAmount = amount;
            itemAmount.text = $"x{ItemsAmount}";
            itemPriceAll.text = $"+{ItemsAmount * Part.SellPrice}";
        }

        private void SellOneItem()
        {
            GameManager.Instance.Wallet.AddMoney(Part.SellPrice);
            GameManager.Instance.InventorySystem.RemovePart(Part);
        }

        private void SellAllItems()
        {
            GameManager.Instance.Wallet.AddMoney(Part.SellPrice * ItemsAmount);
            GameManager.Instance.InventorySystem.RemoveParts(Part);
        }
    }
}

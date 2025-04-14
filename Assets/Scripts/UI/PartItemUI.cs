using Systems.CarAssemble;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PartItemUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text amountText;

        public CarPartType PartType { get; private set; }
        public int PartLevel { get; private set; }
        public int ItemsAmount { get; private set; }

        public void Setup(CarPart part, Sprite sprite)
        {
            icon.sprite = sprite;
            levelText.text = $"Lv. {part.Level}";
            amountText.text = $"x1";
            PartType = part.PartType;
            PartLevel = part.Level;
            ItemsAmount = 1;
        }

        public void SetItemsAmount(int amount)
        {
            ItemsAmount = amount;
            amountText.text = $"x{ItemsAmount}";
        }
    }
}

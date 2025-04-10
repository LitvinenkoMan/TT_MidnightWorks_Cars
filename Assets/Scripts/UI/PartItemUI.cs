using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PartItemUI : MonoBehaviour
    {
        [SerializeField] public Image icon;
        [SerializeField] public TMP_Text levelText;
        [SerializeField] public TMP_Text AmountText;

        public CarPartType PartType { get; private set; }
        public int PartLevel { get; private set; }
        public int ItemsAmount { get; private set; }

        public void Setup(CarPart part, Sprite sprite)
        {
            icon.sprite = sprite;
            levelText.text = $"Lv. {part.Level}";
            AmountText.text = $"x1";
            PartType = part.PartType;
            PartLevel = part.Level;
            ItemsAmount = 1;
        }

        public void SetItemsAmount(int amount)
        {
            ItemsAmount = amount;
            AmountText.text = $"x{ItemsAmount}";
        }
    }
}

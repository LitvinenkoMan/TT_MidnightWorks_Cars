using TMPro;
using UnityEngine;

namespace UI
{
    public class ScrapMinerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text earnAmountText;

        public void UpdateEarningAmount(int earningAmount)
        {
            earnAmountText.text = $"+{earningAmount}";
        }
    }
}

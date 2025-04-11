using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MainUpperPanelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyUI;
        [SerializeField] private TMP_Text scrapsUI;

        private void OnEnable()
        {
            if (GameManager.Instance == null)
                GameManager.OnInitialized += SubscribeToEvents;
            else 
                SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.Wallet.OnMoneyChanged += UpdateMoneyUI;
            GameManager.Instance.Wallet.OnScrapsChanged += UpdateScrapsUI;
            
            UpdateMoneyUI(GameManager.Instance.Wallet.Money);
            UpdateScrapsUI(GameManager.Instance.Wallet.Scraps);
        }

        private void UnsubscribeFromEvents()
        {
            GameManager.Instance.Wallet.OnMoneyChanged -= UpdateMoneyUI;
            GameManager.Instance.Wallet.OnScrapsChanged -= UpdateScrapsUI;
        }

        private void UpdateMoneyUI(int amount)
        {
            moneyUI.text = $"{amount}";
        }

        private void UpdateScrapsUI(int amount)
        {
            scrapsUI.text = $"{amount}";
        }
    }
}

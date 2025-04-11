using System;
using Core;
using Player;
using UnityEngine;

namespace Systems.Economy
{
    public class ScrapMiner : MonoBehaviour
    {
        [Header("Reward Settings")]
        private int scrapEarnAmount = 10;
        
        public event Action OnPlayerEntered;
        public event Action OnPlayerLeave;
        public event Action<int> OnEarnScrapsAmountChanged;

        private void Start()
        {
            OnEarnScrapsAmountChanged?.Invoke(scrapEarnAmount);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
            {
                OnPlayerEntered?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
            {
                OnPlayerLeave?.Invoke();
            }
        }

        public void EarnScraps()
        {
           GameManager.Instance.Wallet.AddScraps(scrapEarnAmount);
           
        }

        public void SetNewEarnAmount(int newEarnAmount)
        {
            scrapEarnAmount = newEarnAmount;
            OnEarnScrapsAmountChanged?.Invoke(scrapEarnAmount);
        }
    }
}

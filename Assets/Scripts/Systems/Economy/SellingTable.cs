using System;
using Player;
using UnityEngine;

namespace Systems.Economy
{
    public class SellingTable : MonoBehaviour
    {
        public event Action OnPlayerEntered;
        public event Action OnPlayerLeave;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement movement))
            {
                OnPlayerEntered?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovement movement))
            {
                OnPlayerLeave?.Invoke();
            }
        }
    }
}

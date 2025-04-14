using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Player;
using Systems.Inventory;
using UnityEngine;

namespace Systems.CarAssemble
{
    public class CarAssembler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> carPrefabsByLevel;
        [SerializeField] private ParkingManager parkingManager;
        private InventorySystem _inventory;
        
        
        public event Action OnPlayerEntered;
        public event Action OnPlayerLeave;
        
        private void OnEnable()
        {
            if (GameManager.Instance == null)
                GameManager.OnInitialized += Initialize;
            else Initialize();
        }

        private void Initialize()
        {
            _inventory = GameManager.Instance.InventorySystem;
        }

        public void TryAssembleCar(int level)
        {
            if (_inventory.CanAssembleCar(level))
            {
                List<CarPart> partsUsed = _inventory.GetPartsForAssembly(level);
                
                AssembleCar(partsUsed, level);

                Debug.Log($"Car assembled at level {level}!");

                // Можно начислить игроку награду, деньги, опыт и т.п.
            }
            else
            {
                Debug.Log("Not enough parts to assemble the car.");
            }
        } 
        
        private void AssembleCar(List<CarPart> parts, int level)
        {
            if (!HasAllRequiredParts(parts))
            {
                return;
            }

            ParkingSpot spot = parkingManager.GetFreeSpot();
            if (spot == null)
            {
                return;
            }
            
            _inventory.RemovePartsForAssembly(level);

            GameObject newCar = Instantiate(carPrefabsByLevel[level], spot.GetParkingPosition(), spot.GetParkingRotation());
            Car car = newCar.GetComponent<Car>();
            
            int totalPrice = 0;
            parts.ForEach(x =>
            {
                totalPrice += x.SellPrice;
                totalPrice *= 3;
            });
            car.Initialize(parts, level, totalPrice);

            spot.Occupy();
            Debug.Log($"Машина уровня {level} успешно собрана и размещена на парковке.");
        }

        private bool HasAllRequiredParts(List<CarPart> parts)
        {
            var requiredTypes = System.Enum.GetValues(typeof(CarPartType)).Cast<CarPartType>();
            return requiredTypes.All(requiredType => parts.Any(p => p.PartType == requiredType));
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
    }
}

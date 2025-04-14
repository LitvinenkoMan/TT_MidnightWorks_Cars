using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Interfaces;
using Player;
using Systems.CarAssemble;
using UnityEngine;

namespace Systems.Inventory
{
    public class InventorySystem : MonoBehaviour, ISavable
    {
        public List<CarPart> ownedParts = new();

        public event Action OnInventoryUpdated;
        public event Action OnPlayerEntered;
        public event Action OnPlayerLeave;

        private void Awake()
        {
            GameManager.Instance.SaveManager.AddSavableObject(this);
        }

        public void AddPart(CarPart part)
        {
            ownedParts.Add(part);
            OnInventoryUpdated?.Invoke();
        }

        public void RemovePart(CarPart part)
        {
            ownedParts.Remove(part);
            OnInventoryUpdated?.Invoke();
        }

        public void RemoveParts(CarPart part)
        {
            ownedParts.RemoveAll(p => p.PartType == part.PartType && p.Level == part.Level);
            OnInventoryUpdated?.Invoke();
        }

        public List<CarPart> GetAllParts()
        {
            return ownedParts;
        }

        public bool CanAssembleCar(int level)
        {
            return Enum.GetValues(typeof(CarPartType)).Cast<CarPartType>()
                .All(type => ownedParts.Any(p => p.PartType == type && p.Level == level));
        }

        public List<CarPart> GetPartsForAssembly(int level)
        {
            return Enum.GetValues(typeof(CarPartType)).Cast<CarPartType>()
                .Select(type => ownedParts.FirstOrDefault(p => p.PartType == type && p.Level == level))
                .Where(p => p != null)
                .ToList();
        }

        public void RemovePartsForAssembly(int level)
        {
            foreach (var type in Enum.GetValues(typeof(CarPartType)).Cast<CarPartType>())
            {
                var part = ownedParts.FirstOrDefault(p => p.PartType == type && p.Level == level);
                if (part != null)
                    ownedParts.Remove(part);
            }
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

        public SaveData Save()
        {
            var data = new SaveData();
            data.ownedParts = ownedParts;
            return data;
        }

        public void Load(SaveData data)
        {
            ownedParts = data.ownedParts;
            OnInventoryUpdated?.Invoke();
        }
    }
}

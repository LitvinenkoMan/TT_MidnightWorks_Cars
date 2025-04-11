using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Interfaces;
using Player;
using UnityEngine;

namespace Systems.Inventory
{
    public class InventorySystem : MonoBehaviour, ISavable
    {
        public List<CarPart> ownedParts = new();

        public event Action OnPartAdded;
        public event Action OnPlayerEntered;
        public event Action OnPlayerLeave;

        public void AddPart(CarPart part)
        {
            ownedParts.Add(part);
            OnPartAdded?.Invoke();
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
            OnPartAdded?.Invoke();
        }
    }
}

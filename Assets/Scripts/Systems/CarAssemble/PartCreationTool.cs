using System;
using Core;
using Interfaces;
using Player;
using Systems.Inventory;
using UI;
using UnityEngine;

namespace Systems.CarAssemble
{
    public class PartCreationTool : MonoBehaviour, ISavable
    {
        [Header("Dependencies")]
        [SerializeField] private InventorySystem inventory;
        [SerializeField] private GameplayPresenter presenter;

        [Header("Part Creation Settings")]
        [SerializeField] private CarPartType partType = CarPartType.Engine;
        [SerializeField] private int partLevel;
        [SerializeField] private int partPrice = 0;
        [SerializeField] private int maxPartLevel = 7;
        [SerializeField] private int creationCost;
        [SerializeField] private int upgradeCost;

        public CarPartType PartTypeCreation => partType;
        public int PartLevelCreation => partLevel;
        public int UpgradeCost => upgradeCost;
        public int CreationCost => creationCost;

        [ContextMenu("Create Part")]
        public void CreatePart()
        {
            if (GameManager.Instance.Wallet.SpendScraps(creationCost))
            {
                var newPart = new CarPart(partType, partLevel, partPrice);
                inventory.AddPart(newPart);
            }
        }

        // [ContextMenu("Create Full Set")]    // For Debugging
        // public void CreateFullSet()
        // {
        //     foreach (CarPartType type in Enum.GetValues(typeof(CarPartType)))
        //     {
        //         var part = new CarPart(type, partLevel);
        //         inventory.AddPart(part);
        //     }
        //
        // }

        [ContextMenu("Upgrade Tool")]
        public void UpgradeTool()
        {
            if (partLevel < maxPartLevel && GameManager.Instance.Wallet.SpendMoney(upgradeCost))
            {
                partLevel++;
                creationCost += creationCost / partLevel;
                upgradeCost += upgradeCost / partLevel;
                partPrice += (int)(partPrice / partLevel * 1.2f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
            {
                presenter.ShowCreationToolWindow(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerMovement playerMovement))
            {
                presenter.HideCreationTollWindow();
            }
        }

        public SaveData Save()
        {
            throw new NotImplementedException();
        }

        public void Load(SaveData data)
        {
            throw new NotImplementedException();
        }
    }
}

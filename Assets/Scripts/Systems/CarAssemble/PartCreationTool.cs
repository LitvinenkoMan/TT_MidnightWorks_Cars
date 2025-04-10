using System;
using Player;
using Systems.Inventory;
using UI;
using UnityEngine;

namespace Systems.CarAssemble
{
    public class PartCreationTool : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private InventorySystem inventory;
        [SerializeField] private GameplayPresenter presenter;

        [Header("Part Creation Settings")]
        [SerializeField] private CarPartType partType = CarPartType.Engine;
        [SerializeField] private int partLevel = 0;

        public CarPartType PartTypeCreation => partType;
        public int PartLevelCreation => partLevel;

        [ContextMenu("Create Part")]
        public void CreatePart()
        {
            var newPart = new CarPart(partType, partLevel);
            inventory.AddPart(newPart);
            Debug.Log($"✅ Created and added part: {partType} level {partLevel}");
        }

        [ContextMenu("Create Full Set")]
        public void CreateFullSet()
        {
            foreach (CarPartType type in Enum.GetValues(typeof(CarPartType)))
            {
                var part = new CarPart(type, partLevel);
                inventory.AddPart(part);
                Debug.Log($"✅ Created {type} L{partLevel}");
            }

            Debug.Log("✅ Full set of parts added to inventory.");
        }

        [ContextMenu("Upgrade Tool")]
        public void UpgradeTool()
        {
            partLevel++;
            Debug.Log("Tool Upgraded");
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
    }
}

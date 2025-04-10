using System;
using Systems.Inventory;
using UnityEngine;

namespace Systems.CarAssemble
{
    public class PartCreationTool : MonoBehaviour
    {
        [Header("Dependencies")]
        public InventorySystem inventory;

        [Header("Part Creation Settings")]
        public CarPartType partType = CarPartType.Engine;
        public int partLevel = 0;

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
            Debug.Log("Tool Upgrated");
        }
    }
}

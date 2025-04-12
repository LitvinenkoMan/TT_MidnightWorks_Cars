using System.Collections.Generic;
using Systems.Inventory;
using UnityEngine;

namespace Systems.CarAssemble
{
    public class CarAssembler : MonoBehaviour
    {
        public InventorySystem inventory;

        public void TryAssembleCar(int level)
        {
            if (inventory.CanAssembleCar(level))
            {
                List<CarPart> partsUsed = inventory.GetPartsForAssembly(level);
                inventory.RemovePartsForAssembly(level);

                Debug.Log($"Car assembled at level {level}!");

                // Можно начислить игроку награду, деньги, опыт и т.п.
            }
            else
            {
                Debug.Log("Not enough parts to assemble the car.");
            }
        }
    }
}

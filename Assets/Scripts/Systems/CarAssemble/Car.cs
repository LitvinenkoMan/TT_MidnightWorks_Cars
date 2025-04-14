using System.Collections.Generic;
using UnityEngine;

namespace Systems.CarAssemble
{
    public class Car : MonoBehaviour
    {
        public int Level { get; private set; }
        public int Price { get; private set; }
        public List<CarPart> InstalledParts { get; private set; }

        public void Initialize(List<CarPart> parts, int level, int price)
        {
            InstalledParts = parts;
            Level = level;
            Price = price;
        }
    }
}

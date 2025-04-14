using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Systems.CarAssemble
{
    public class ParkingManager : MonoBehaviour
    {
        [SerializeField] private List<ParkingSpot> spots;

        public ParkingSpot GetFreeSpot()
        {
            return spots.FirstOrDefault(s => !s.IsOccupied);
        }
    }
}

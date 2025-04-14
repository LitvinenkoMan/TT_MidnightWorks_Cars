using UnityEngine;

namespace Systems.CarAssemble
{
    public class ParkingSpot : MonoBehaviour
    {
        public bool IsOccupied { get; private set; }

        [SerializeField] private Transform spawnPoint;

        public Vector3 GetParkingPosition()
        {
            return spawnPoint.position;
        }

        public Quaternion GetParkingRotation()
        {
            return spawnPoint.rotation;
        }

        public void Occupy()
        {
            IsOccupied = true;
        }

        public void Free()
        {
            IsOccupied = false;
        }
    }
}
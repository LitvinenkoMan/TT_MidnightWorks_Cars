using System;

namespace Systems.CarAssemble
{
    [Serializable]
    public enum CarPartType
    {
        Engine,
        Wheels,
        Transmission,
        Electronics,
        Body,
        SteeringSystem,
        Suspension
    }

    [Serializable]
    public class CarPart
    {
        public CarPartType PartType;
        public int Level;
        public int SellPrice;

        public CarPart(CarPartType type, int level)
        {
            PartType = type;
            Level = level;
        }

        public CarPart(CarPartType type, int level, int price)
        {
            PartType = type;
            Level = level;
            SellPrice = price;
        }
    }
}
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

[System.Serializable]
public class CarPart
{
    public CarPartType PartType;
    public int Level;

    public CarPart(CarPartType type, int level)
    {
        PartType = type;
        Level = level;
    }

    public string GetID() => $"{PartType}_L{Level}";
}
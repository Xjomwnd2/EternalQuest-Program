using System;

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points) 
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        // Eternal goals are never complete but still give points each time
        return GetPoints();
    }

    public override string GetDetailsString()
    {
        return $"[ ] {GetName()} ({GetDescription()})";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{GetName()},{GetDescription()},{GetPoints()}";
    }
}
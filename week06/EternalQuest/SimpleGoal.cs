using System;

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points)
    {
    }

    // Constructor for loading from file
    public SimpleGoal(string name, string description, int points, bool isComplete) 
        : base(name, description, points)
    {
        if (isComplete)
        {
            SetComplete(true);
        }
    }

    public override int RecordEvent()
    {
        if (!IsComplete())
        {
            SetComplete(true);
            return GetPoints();
        }
        return 0;
    }

    public override string GetDetailsString()
    {
        string status = IsComplete() ? "[X]" : "[ ]";
        return $"{status} {GetName()} ({GetDescription()})";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{GetName()},{GetDescription()},{GetPoints()},{IsComplete()}";
    }
}
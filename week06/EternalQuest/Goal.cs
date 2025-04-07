using System;

public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;
    private bool _isComplete;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
        _isComplete = false;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetDescription()
    {
        return _description;
    }

    public int GetPoints()
    {
        return _points;
    }

    public bool IsComplete()
    {
        return _isComplete;
    }

    protected void SetComplete(bool complete)
    {
        _isComplete = complete;
    }

    // Abstract methods to be implemented by derived classes
    public abstract int RecordEvent();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();
}
using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public int GetScore()
    {
        return _score;
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void DisplayGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created yet.");
            return;
        }

        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created yet.");
            return;
        }

        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }

        Console.Write("Which goal did you accomplish? ");
        if (int.TryParse(Console.ReadLine(), out int goalIndex) && goalIndex > 0 && goalIndex <= _goals.Count)
        {
            int pointsEarned = _goals[goalIndex - 1].RecordEvent();
            _score += pointsEarned;
            
            Console.WriteLine($"Congratulations! You have earned {pointsEarned} points!");
            Console.WriteLine($"You now have {_score} points.");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    public void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            // Save the score on the first line
            outputFile.WriteLine(_score);

            // Save each goal on its own line
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved successfully!");
    }

    public void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found!");
            return;
        }

        _goals.Clear(); // Clear existing goals

        string[] lines = File.ReadAllLines(filename);
        
        if (lines.Length > 0)
        {
            // First line is the score
            if (int.TryParse(lines[0], out int score))
            {
                _score = score;
            }

            // Process each goal
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(':');
                
                if (parts.Length == 2)
                {
                    string goalType = parts[0];
                    string[] goalData = parts[1].Split(',');

                    switch (goalType)
                    {
                        case "SimpleGoal":
                            if (goalData.Length == 4 && bool.TryParse(goalData[3], out bool isComplete))
                            {
                                _goals.Add(new SimpleGoal(goalData[0], goalData[1], int.Parse(goalData[2]), isComplete));
                            }
                            break;
                        case "EternalGoal":
                            if (goalData.Length == 3)
                            {
                                _goals.Add(new EternalGoal(goalData[0], goalData[1], int.Parse(goalData[2])));
                            }
                            break;
                        case "ChecklistGoal":
                            if (goalData.Length == 6)
                            {
                                _goals.Add(new ChecklistGoal(
                                    goalData[0], 
                                    goalData[1], 
                                    int.Parse(goalData[2]), 
                                    int.Parse(goalData[3]), 
                                    int.Parse(goalData[4]), 
                                    int.Parse(goalData[5])
                                ));
                            }
                            break;
                    }
                }
            }
        }

        Console.WriteLine("Goals loaded successfully!");
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        
        if (int.TryParse(Console.ReadLine(), out int goalType) && goalType >= 1 && goalType <= 3)
        {
            Console.Write("What is the name of your goal? ");
            string name = Console.ReadLine();
            
            Console.Write("What is a short description of it? ");
            string description = Console.ReadLine();
            
            Console.Write("What is the amount of points associated with this goal? ");
            if (int.TryParse(Console.ReadLine(), out int points))
            {
                switch (goalType)
                {
                    case 1: // Simple Goal
                        _goals.Add(new SimpleGoal(name, description, points));
                        break;
                    case 2: // Eternal Goal
                        _goals.Add(new EternalGoal(name, description, points));
                        break;
                    case 3: // Checklist Goal
                        Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                        if (int.TryParse(Console.ReadLine(), out int target))
                        {
                            Console.Write("What is the bonus for accomplishing it that many times? ");
                            if (int.TryParse(Console.ReadLine(), out int bonus))
                            {
                                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                            }
                        }
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid goal type.");
        }
    }
}
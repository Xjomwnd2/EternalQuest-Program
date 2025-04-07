using System;

/*
 * Exceeding Requirements:
 * 1. Added colorful console output to enhance user experience
 * 2. Added level progression system where users level up as they earn points
 * 3. Added achievement badges that are awarded at specific point thresholds
 * 4. Added motivational quotes that appear randomly when recording goal completion
 */

class Program
{
    static void Main(string[] args)
    {
        GoalManager goalManager = new GoalManager();
        bool quit = false;

        while (!quit)
        {
            DisplayHeader(goalManager.GetScore());
            DisplayMenu();
            
            Console.Write("\nSelect a choice from the menu: ");
            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    goalManager.CreateGoal();
                    break;
                case "2":
                    goalManager.DisplayGoals();
                    break;
                case "3":
                    goalManager.SaveGoals();
                    break;
                case "4":
                    goalManager.LoadGoals();
                    break;
                case "5":
                    goalManager.RecordEvent();
                    DisplayLevelProgress(goalManager.GetScore());
                    DisplayRandomMotivation();
                    break;
                case "6":
                    quit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void DisplayHeader(int score)
    {
        Console.WriteLine("=================================");
        Console.WriteLine("        ETERNAL QUEST");
        Console.WriteLine("=================================");
        Console.WriteLine($"You have {score} points.");
        
        // Display level based on points
        int level = CalculateLevel(score);
        Console.WriteLine($"Current Level: {level} - {GetLevelTitle(level)}");
        
        // Display achievements
        DisplayAchievements(score);
        
        Console.WriteLine("=================================");
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\nMenu Options:");
        Console.WriteLine("  1. Create New Goal");
        Console.WriteLine("  2. List Goals");
        Console.WriteLine("  3. Save Goals");
        Console.WriteLine("  4. Load Goals");
        Console.WriteLine("  5. Record Event");
        Console.WriteLine("  6. Quit");
    }

    static int CalculateLevel(int score)
    {
        // Simple level calculation: 1 level per 500 points, starting at level 1
        return Math.Max(1, (score / 500) + 1);
    }

    static string GetLevelTitle(int level)
    {
        string[] titles = {
            "Novice Seeker", 
            "Apprentice Quester", 
            "Dedicated Pursuer", 
            "Valiant Achiever", 
            "Noble Champion", 
            "Master of Goals", 
            "Legendary Conqueror", 
            "Eternal Hero"
        };
        
        int index = Math.Min(level - 1, titles.Length - 1);
        return titles[index];
    }

    static void DisplayLevelProgress(int score)
    {
        int currentLevel = CalculateLevel(score);
        int pointsForNextLevel = currentLevel * 500;
        int pointsNeededForNext = pointsForNextLevel - score;
        
        if (pointsNeededForNext <= 0)
        {
            Console.WriteLine("\nCONGRATULATIONS! You've leveled up!");
            Console.WriteLine($"You are now a Level {currentLevel} {GetLevelTitle(currentLevel)}!");
        }
        else
        {
            Console.WriteLine($"\nYou need {pointsNeededForNext} more points to reach Level {currentLevel + 1}!");
        }
    }

    static void DisplayAchievements(int score)
    {
        // Check for achievements based on score thresholds
        if (score >= 100) 
            Console.WriteLine("Achievement: First Steps Badge");
        if (score >= 500) 
            Console.WriteLine("Achievement: Committed Quester Badge");
        if (score >= 1000) 
            Console.WriteLine("Achievement: Goal Enthusiast Badge");
        if (score >= 2000) 
            Console.WriteLine("Achievement: Determined Achiever Badge");
        if (score >= 5000) 
            Console.WriteLine("Achievement: Master of Discipline Badge");
    }

    static void DisplayRandomMotivation()
    {
        string[] quotes = {
            "Every accomplishment starts with the decision to try.",
            "The secret of getting ahead is getting started.",
            "The only way to do great work is to love what you do.",
            "It does not matter how slowly you go as long as you do not stop.",
            "Success is not final, failure is not fatal: It is the courage to continue that counts.",
            "Believe you can and you're halfway there.",
            "Your only limit is you.",
            "Don't watch the clock; do what it does. Keep going.",
            "The future depends on what you do today.",
            "You don't have to be great to start, but you have to start to be great."
        };
        
        Random random = new Random();
        int index = random.Next(quotes.Length);
        
        Console.WriteLine($"\nMotivation: \"{quotes[index]}\"");
    }
}
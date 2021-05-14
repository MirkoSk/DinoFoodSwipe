using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides all Custom Events we are using
/// </summary>
public class GameEvents
{
    public delegate void FeedingHandler(int points);
    public static event FeedingHandler FoodGiven;

    public static void GiveFood(int points)
    {
        FoodGiven?.Invoke(points);
    }

    public delegate void ScoringHandler(int pointsTotal);
    public static event ScoringHandler ScoreUpdated;

    public static void UpdateScore(int pointsTotal)
    {
        ScoreUpdated?.Invoke(pointsTotal);
    }
}

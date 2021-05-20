using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides all Custom Events we are using
/// </summary>
public class GameEvents
{
    public delegate void FeedingHandler(int points, Animal animalFed, FoodType foodGiven);
    public static event FeedingHandler FoodGiven;

    public static void GiveFood(int points, Animal animalFed, FoodType foodGiven)
    {
        FoodGiven?.Invoke(points, animalFed, foodGiven);
    }

    public delegate void ScoringHandler(int pointsTotal);
    public static event ScoringHandler ScoringEnded;

    public static void EndScoring(int pointsTotal)
    {
        ScoringEnded?.Invoke(pointsTotal);
    }
}

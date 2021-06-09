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

    public delegate void DinoSwitchHandler(Animal animalSwitching);
    public static event DinoSwitchHandler DinoRetreated;

    public static void RetreatDino(Animal animalSwichting)
    {
        DinoRetreated?.Invoke(animalSwichting);
    }

    public static event DinoSwitchHandler DinoComingIn;

    public static void MoveDinoIn(Animal animalSwitching)
    {
        DinoComingIn?.Invoke(animalSwitching);
    }
}

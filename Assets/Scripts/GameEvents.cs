using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides all Custom Events we are using
/// </summary>
public class GameEvents
{
    public delegate void FeedingHandler(Diet diet);
    public static event FeedingHandler FoodGiven;

    public static void GiveFood(Diet diet)
    {
        FoodGiven?.Invoke(diet);
    }
}
